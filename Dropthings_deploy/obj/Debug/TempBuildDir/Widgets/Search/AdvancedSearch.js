var _advanceSearch = new Object;
var advanceSearch = _advanceSearch.prototype = {
    StateMatchID: null,
    Load: function() {
        var params = { "InputObj": "keyword", "SelObj": "sel", "ItemList": "term_list" };
        $("#BtnSearchInResult").click(function() {
            if ($(this).attr("checked")) {
                $("div.subsearch").show(300);
            } else {
                $("div.subsearch").hide(300);
            }
        });
        //advanceSearch.TimeInit();
        var keyword_value = jQuery.query.get('keyword');
        var btn_value = jQuery.query.get('searchButton');
        var type = jQuery.query.get('type');
        if ($.trim(keyword_value) != "") {
            delete advanceSearch.CommanQuery["maxdate"];
            delete advanceSearch.CommanQuery["mindate"];
            advanceSearch.KeywordSearch(keyword_value);
            var inputcue = new inputcue_("keyword", "sel", "term_list");
            inputcue.GetCueValue(keyword_value);
        }
        if (type == "more") {
            //advanceSearch.CommanQuery["mindate"] = advanceSearch.GetTimeStr(-1);
            //advanceSearch.CommanQuery["maxdate"] = advanceSearch.GetTimeStr(0);
            advanceSearch.CommanQuery["Sort"] = "Date";
            advanceSearch.KeywordSearch("*");
        } else {
            advanceSearch.InnitCategoryList();
        }
        $("#BtnSearch").click(function() {
            $("#total_count").empty();
            $("#search_second").empty();
            $("#dis_num").empty();
            advanceSearch.Search();
            advanceSearch.ExecuteOriginalSearch();
        });
        $("#store_search").click(function() {
            $("#total_count").empty();
            $("#search_second").empty();
            $("#dis_num").empty();
            advanceSearch.Search("commonqueryinresults", "query", 1);
        });
        var advanceSearch_tab_list = $("#advanceSearch_tab").find("li");
        $(advanceSearch_tab_list).find("a").click(function() {
            $(advanceSearch_tab_list).attr("class", "tab_off");
            $(this).parent("li").attr("class", "tab_on");
            $("#total_count").empty();
            $("#search_second").empty();
            $("#dis_num").empty();
            if (advanceSearch.queryParams["action"] == "categoryinresults") {
                advanceSearch.Search("categoryinresults", "category", 2, advanceSearch.queryParams["category"]);
            } else if (advanceSearch.queryParams["action"] == "commonqueryinresults") {
                advanceSearch.Search("commonqueryinresults", "query", 1);
            } else {
                advanceSearch.Search();
            }
        });
    },
    initData: { "page_size": 5, "result_id": "result_list", "status_bar_id": "pager_list" },
    queryParams: { "action": "query", "display_style": 4 },
    CommanQuery: { "action": "query", "display_style": 4, "TotalResults": true, "Highlight": "summaryterms",
        "summary": "context", "Predict": "false", "Combine": "DRETITLE"
    },
    InnitCategoryList: function() {
        var str_where = " PARENTCATE=226";
        $.post("Handler/CategoryMenu.ashx",
            { "str_where": str_where, "type": "getcategorylist" },
            function(data) {
                if (data) {
                    if (data.SuccessCode == 1) {
                        delete data["SuccessCode"];
                        var content = [];
                        for (var item in data) {
                            content.push("<li><span class=\"radio\">");
                            content.push("<input type=\"radio\" name=\"category\" value=\"" + unescape(data[item]) + "\" pid=\"" + item + "\" /></span>");
                            content.push("<span class=\"name\">" + unescape(data[item]) + "</span></li>");
                        }
                        $("#category_list").empty().html(content.join(""));
                        advanceSearch.InnitCategoryRadio();
                    }
                }
            },
            "json"
        );
    },
    InnitCategoryRadio: function() {
        $("#category_list").find(":radio").click(function() {
            var category = $(this).attr("pid");
            var categoryname = $(this).val();
            $("#total_count").empty();
            $("#search_second").empty();
            $("#dis_num").empty();
            advanceSearch.queryParams["text"] = categoryname;
            advanceSearch.Search("categoryinresults", "category", 2, category);
        });
    },
    InnitParams: function(storeaction, type, categoryid) {
        this.initData["page_size"] = parseInt($("#select_num").val());
        this.queryParams["sort"] = $("#sortrule").val();
        if ($("#quality").val() != "0") {
            this.queryParams["MinScore"] = $("#quality").val();
        }
        else {
            delete this.queryParams["MinScore"];
        }
        if ($.trim($("#start_time").val())) {
            this.queryParams["MinDate"] = $("#start_time").val();
        } else {
            delete this.queryParams["MinDate"];
        }
        if ($.trim($("#end_time").val())) {
            this.queryParams["MaxDate"] = $("#end_time").val();
        } else {
            delete this.queryParams["MaxDate"];
        }
        if (advanceSearch.GetNewsType()) {
            this.queryParams["FieldText"] = advanceSearch.GetNewsType();
        }
        else {
            delete this.queryParams["FieldText"];
        }
        if (type) {
            switch (type) {
                case 1:
                    this.queryParams = { "action": "query" };
                    this.queryParams["statematchid"] = advanceSearch.StateMatchID;
                    //this.queryParams["storetext"] = $.trim($("#queryword").val());
                    this.queryParams["text"] = $.trim($("#store_keyword").val());
                    this.queryParams["display_style"] = 4;
                    break;
                case 2:
                    this.queryParams = { "action": "categoryquery" };
                    this.queryParams["statematchid"] = advanceSearch.StateMatchID;
                    //this.queryParams["storetext"] = $.trim($("#queryword").val());
                    this.queryParams["category"] = categoryid;
                    this.queryParams["display_style"] = 4;
                    break;
                default:
                    break;
            }
        } else {
            delete this.queryParams["category"];
            delete this.queryParams["statematchid"];
            delete this.queryParams["storetext"];
            delete this.queryParams["params"];
            delete this.queryParams["values"]
            this.queryParams["text"] = $.trim($("#queryword").val());
        }
    },
    InnitCategoryParams: function(params) {
        var paramslist = [];
        var valuelist = [];
        if (this.queryParams["MinScore"]) {
            paramslist.push("MinScore");
            valuelist.push(this.queryParams["MinScore"]);
        }
        if (this.queryParams["sort"]) {
            paramslist.push("sort");
            valuelist.push(this.queryParams["sort"]);
        }
        if (this.queryParams["MinDate"]) {
            paramslist.push("MinDate");
            valuelist.push(this.queryParams["MinDate"]);
        }
        if (this.queryParams["MaxDate"]) {
            paramslist.push("MaxDate");
            valuelist.push(this.queryParams["MaxDate"]);
        }
        if (this.queryParams["FieldText"]) {
            paramslist.push("FieldText");
            valuelist.push(this.queryParams["FieldText"]);
        }
        paramslist.push("Summary");
        valuelist.push("context");
        paramslist.push("Characters");
        valuelist.push("600");
        this.queryParams["params"] = paramslist.join(",");
        this.queryParams["values"] = valuelist.join(",");
    },
    Search: function(searchtype, storeaction, type, categoryid) {
        if (this.check()) {
            $("#pager_list").show().empty().html("<center style=\"font-size:12px;\"><img src=\"img/loading_icon.gif\" /></center>");
            if (searchtype) {
                this.queryParams["action"] = storeaction;
                delete this.queryParams["isresult"];
                this.InnitParams(searchtype, type, categoryid);
            } else {
                this.queryParams["action"] = $("#select").val();
                this.queryParams["isresult"] = true;
                this.InnitParams();
                $("#SearchInResult").show();
                $("#store_keyword").val("");
                $("#BtnSearchInResult").removeAttr("checked");
                $("div.subsearch").hide();
            }
            var Lpager = new Pager(this.initData);
            Lpager.OtherFn = function(totalcount, stateid) {
                if (stateid) {
                    advanceSearch.StateMatchID = stateid;
                }
                advanceSearch.PgerOtherFn(this, totalcount, true);
            };
            Lpager.LoadData(1, this.queryParams);
            //advanceSearch.ExecuteOriginalSearch();
            //advanceSearch.Suggest($.trim($("#queryword").val()));            
        }
    },
    GetNewsType: function() {
        var str = [];
        var type = $("#news_type").val();
        if (type != "all") {
            if (str.length == 0) {
                str.push("MATCH{" + type + "}:C1");
            } else {
                str.push("+AND+MATCH{" + type + "}:C1");
            }
        }
        var blog_name = $("#blog_name").val();
        if (blog_name) {
            if (str.length == 0) {
                str.push("MATCH{" + blog_name + "}:MYSITENAME");
            } else {
                str.push("+AND+MATCH{" + blog_name + "}:MYSITENAME");
            }
        }
        var mcr_type = $("#advanceSearch_tab").find("li[class='tab_on']").attr("pid");
        if (mcr_type != "all") {
            if (str.length == 0) {
                str.push("MATCH{" + mcr_type + "}:C1");
            } else {
                str.push("+AND+MATCH{" + mcr_type + "}:C1");
            }
        }
        if (str.length == 0) {
            return "";
        } else {
            return str.join("");
        }
    },
    check: function() {
        if ($.trim($("#queryword").val()) == "") {
            alert("请输入关键字");
            return false;
        }
        else {
            return true;
        }
    },
    Suggest: function(keyword) {
        if (!keyword) {
            var doc_id = [];
            $("[id^='suggest_']").each(function(n) {
                var l_doc_id = $(this).attr("id").split('_')[1];
                if (n == 0) {
                    $("#news_flash").empty();
                    Common.DownFlash("news_flash", "flash/news.swf?doc_id=s" + l_doc_id, 600, 550);
                }
                doc_id.push(l_doc_id);
            });
            $.post("Handler/SuggestResult.ashx",
                { "doc_id_list": doc_id.join(","), "type": "suggest" },
                function(data) {
                    for (var item in data) {
                        $("#suggest_" + item).empty().html(unescape(data[item]));
                        $("[name^='doc_']").show(500);
                    }
                },
                "json"
            )
        }
    },
    /*TimeInit: function() {

        $("[id^='time_']").each(function() {
    var type = $(this).attr("id").split('_')[2];
    $(this).empty();
    switch (type) {
    case "day":
    for (var i = 1; i <= 31; i++) {
    if (i < 10) {
    $(this).append("<option value=\"0" + i + "\">0" + i + "</option>");
    }
    else {
    $(this).append("<option value=\"" + i + "\">" + i + "</option>");
    }
    }
    break;
    case "month":
    for (var i = 1; i <= 12; i++) {
    if (i < 10) {
    $(this).append("<option value=\"0" + i + "\">0" + i + "</option>");
    }
    else {
    $(this).append("<option value=\"" + i + "\">" + i + "</option>");
    }
    }
    break;
    case "year":
    for (var i = 2000; i <= 2020; i++) {
    $(this).append("<option value=\"" + i + "\">" + i + "</option>");
    }
    break;
    default:
    break;
    }
    });
    setTimeout(advanceSearch.InitTimeSelect, 50);
    },
    InitTimeSelect: function() {
    var end_time = new Date();
    var start_time = new Date(now_time - 86400000);
    //$("#start_time").text(start_time.getFullYear() + "-" + start_time.getMonth() + "-");
    //$("#end_time").text(end_time);
    //        var now_time = new Date();
    //        var ago_time = new Date(now_time - 86400000);
    //        $("#time_min_day").val(ago_time.getDate() < 10 ? "0" + ago_time.getDate() : ago_time.getDate());
    //        $("#time_min_month").val((ago_time.getMonth() + 1) < 10 ? "0" + (ago_time.getMonth() + 1) : (ago_time.getMonth() + 1));

        //        $("#time_max_day").val(now_time.getDate() < 10 ? "0" + now_time.getDate() : now_time.getDate());
    //        $("#time_max_month").val((now_time.getMonth() + 1) < 10 ? "0" + (now_time.getMonth() + 1) : (now_time.getMonth() + 1));
    //        $("#time_max_year").val(now_time.getFullYear());
    },*/
    GetDate: function(type) {
        var time_str = "";
        $("[id^='time_" + type + "_']").each(function() {
            time_str = time_str + $(this).val() + "/";
        });
        return time_str.slice(0, time_str.length - 1);
    },
    KeywordSearch: function(keyword) {
        $("#pager_list").show().empty().html("<center style=\"font-size:12px;\"><img src=\"img/loading_icon.gif\" /></center>");
        //this.initData["page_size"] = 5;
        this.CommanQuery["text"] = keyword;
        var Lpager = new Pager(this.initData);
        Lpager.OtherFn = function(totalcount) {
            advanceSearch.PgerOtherFn(this, totalcount);
        };
        Lpager.LoadData(1, this.CommanQuery);

        //advanceSearch.ExecuteOriginalSearch();
        $("#SearchInResult").show();
    },
    ExecuteOriginalSearch: function() {
        if ($("#originalSearchResult").size() > 0) {
            // ensure({ js: ["https://www.google.com/jsapi", "Widgets/Search/OriginalSearch.js"], css: ["Widgets/Search/default.css"] }, function() {
            OriginalSearch.Load($.trim($("#queryword").val()));
            //});
        }
    }
    ,
    PgerOtherFn: function(obj, totalcount, tag) {
        var html_str = $.trim($("#total_count").html());
        if (totalcount) {
            if (totalcount == obj.query_params["Start"]) {
                $("#dis_num").empty().html(obj.query_params["Start"]);
            }
            else if (totalcount < (obj.page_size + obj.query_params["Start"])) {
                $("#dis_num").empty().html(obj.query_params["Start"] + "-" + totalcount);
            }
            else {
                $("#dis_num").empty().html(obj.query_params["Start"] + "-" + (obj.query_params["page_size"] + obj.query_params["Start"] - 1));
            }
        }
        if (totalcount && html_str == "") {
            $("#total_count").empty().html(totalcount);
            obj.end_time = obj.end_time - 86400000;
            obj.Start_time = obj.Start_time - 86400000;
            var time_span = (obj.end_time - obj.Start_time) / 1000 / 5 + "";
            time_span = time_span.slice(0, time_span.indexOf(".") + 4);
            $("#search_second").empty().html(time_span);
            if (obj.query_params["text"] != "*") {
                $("#search_keyword").empty().html(obj.query_params["text"]);
                if (!obj.query_params["Characters"])
                    $("#keyword").val(obj.query_params["text"]);
            }
        }
        if (tag) {
            advanceSearch.Suggest();
        }
    },
    GetTimeStr: function(day) {
        var now = new Date;
        var time = new Date(now.getTime() + 1000 * 60 * 60 * 24 * day);
        return time.getDate() + "/" + (time.getMonth() + 1) + "/" + time.getFullYear();
    }
}