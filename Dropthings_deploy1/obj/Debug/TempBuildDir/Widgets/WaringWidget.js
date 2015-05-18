var _warning = new Object;

var Warning = _warning.property = {
    WordRule: null,
    initData: { "page_size": 3, "result_id": "alarm_result_list", "status_bar_id": "alarm_pager_list" },
    queryParams: { "action": "getwordwarninglist" },
    Load: function() {
        this.InnitSensetiveWord();
        this.InnitTabClick();
    },
    InnitTabClick: function() {
        var tab_list = $("#warning_tab").find("a[name='warning_tab_item']");
        tab_list.click(function() {
            tab_list.parent("li").attr("class", "tab_off");
            $(this).parent("li").attr("class", "tab_on");
            var type = $(this).attr("pid");
            Warning.Search(type);
        });
    },
    InnitSensetiveWord: function() {
        if (!this.WordRule) {
            Warning.Search("1");
        }
    },
    Search: function(type) {
        var Lpager = new SqlPager(this.initData);
        switch (type) {
            case "1":
                Warning.queryParams["action"] = "getwordwarninglist";
                Lpager.OtherFn = function(obj, data) {
                    Warning.WordDisResult(obj, data);
                }
                break;
            case "2":
                Warning.queryParams["action"] = "getpageviewwarninglist";
                Lpager.OtherFn = function(obj, data) {
                    Warning.PageViewDisResult(obj, data);
                }
                break;
            default:
                break;
        }
        Lpager.LoadData(1, this.queryParams);
    },
    WordDisResult: function(obj, data) {
        var content = [];
        for (var item in data) {
            var row = data[item];
            content.push("<div class=\"gw_news_title\"><a href=\"" + unescape(row["dochref"]) + "\" target=\"_blank\">");
            content.push(unescape(row["doctitle"]) + "</a><span>" + unescape(row["doctime"]) + "</span>");
            content.push("<span>" + unescape(row["docsite"]) + "</span></div>");
            content.push("<div class=\"gw_news_text\">预警对应敏感词规则：" + unescape(row["wordrule"]) + "。预警值：");
            content.push("<span style=\"color:red;\">" + row["alarmrulenum"] + "</span>，");
            content.push("设定值：" + row["rulenum"] + "</div>");
        }
        $("#" + obj).empty().html(content.join(""));
    },
    PageViewDisResult: function(obj, data) {
        var content = [];
        for (var item in data) {
            var row = data[item];
            content.push("<div class=\"gw_news_title\"><a href=\"" + unescape(row["dochref"]) + "\" target=\"_blank\">");
            content.push(unescape(row["doctitle"]) + "</a><span>" + unescape(row["doctime"]) + "</span>");
            content.push("<span>" + unescape(row["docsite"]) + "</span></div>");
            content.push("<div class=\"gw_news_text\">预警值：回复数(");
            content.push("<span style=\"color:red;\">" + row["alarmreplynum"] + "</span>),点击数(");
            content.push("<span style=\"color:red;\">" + row["alarmclickrate"] + ")</span>;");
            content.push("预警设定值：回复数(" + row["replynum"] + ")，点击数(" + row["clickrate"] + ")</div>");
        }
        $("#" + obj).empty().html(content.join(""));
    }
}