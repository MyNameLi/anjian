$(document).ready(function() {
    Report.Innit();
});

var _report = new Object;
var Report = _report.property = {
    templateEcoding: "gb2312",
    keywords: ["矿难", "瓦斯爆炸", "交通事故", "火灾", "旱灾", "用工荒", "工业事故", "安全生产", "安监"],
    PostInfo: {},
    reportUrlList: { "weekly": "Report" },
    reportType: "weekly",
    EventData: null,
    SpecialData: null,
    ClutersData: null,
    QueryType: null,
    ClustersJob: Config.ReportClusterJobName,
    InitData: { "page_size": 5, "result_id": "result_list", "status_bar_id": "pager_list" },
    IdolQueryParams: { "action": "query", "display_style": 3, "characters": 600, "combine": "simple", "totalresults": true,
        "highlight": "summaryterms", "print": "fields", "summary": "context", "predict": "false"
    },
    FirstType: null,
    SecondType: null,
    PostType: null,
    OtherPostType: null,
    PostTag: false,    
    Innit: function() {
        Report.InnitInfo();
        Report.InnitReportData();
        Report.BtnSelInit();
        Report.InnitBtn();
    },
    InnitInfo: function() {
        $("#time_str").val(Report.GetTimeStr());
    },
    InnitReportData: function() {
        $.post("../Handler/BriefReportData.ashx",
            { "type": "innit" },
            function(data) {
                if (data) {
                    var special_data = data["SpecialData"];
                    delete special_data["SuccessCode"];
                    Report.SpecialData = special_data;
                }
            },
            "json"
        );
    },
    InnitBtn: function() {
        $("#look_more_param").click(function() {
            var type = $(this).attr("pid");
            if (type == "0") {
                $(this).empty().html("<img src=\"../images/btn_form_more_on.gif\">&nbsp;隐藏更多基本设置");
                $("#more_param_list").show();
                $(this).attr("pid", "1");
            } else {
                $(this).empty().html("<img src=\"../images/btn_form_more_off.gif\">&nbsp;展开更多基本设置");
                $("#more_param_list").hide();
                $(this).attr("pid", "0");
            }
        });
        $("#look_more_pic").click(function() {
            var type = $(this).attr("pid");
            if (type == "0") {
                $(this).empty().html("<img src=\"../images/btn_form_more_on.gif\">&nbsp;隐藏更多图片类型");
                $("#more_pic_list").show();
                $(this).attr("pid", "1");
            } else {
                $(this).empty().html("<img src=\"../images/btn_form_more_off.gif\">&nbsp;展开更多图片类型");
                $("#more_pic_list").hide();
                $(this).attr("pid", "0");
            }
        });

        $("#Btn_submit").click(function() {
            if (!$.trim($("#report_title").val())) {
                alert("请输入简报期号!");
                return;
            }
            var yqzs = $.trim($("#report_summarize").val());
            if (!yqzs) {
                alert("请输入舆情综述");
                return;
            }
            Report.getPostParams();
            if (Report.PostTag) {
                var pic_list = Report.GetPicList();
                Report.PostInfo["piclist"] = pic_list;
                Report.PostInfo["all"] = yqzs;
                Report.PostInfo["time_str"] = $("#time_str").val();
                Report.PostInfo["report_start_time"] = Report.GetTimeStr($("#report_start_time").val());
                Report.PostInfo["report_end_time"] = Report.GetTimeStr($("#report_end_time").val());
                Report.PostInfo["report_title"] = escape($("#report_title").val());
                Report.PostInfo["report_people"] = escape($("#report_people").val());
                Report.PostInfo["report_org"] = escape($("#report_org").val());
                Report.PostInfo["report_object"] = escape($("#report_object").val());
                Report.PostInfo["report_mobile"] = escape($("#report_mobile").val());
                Report.PostInfo["report_editer"] = escape($("#report_editer").val());
                Report.PostInfo["report_dean"] = escape($("#report_dean").val());
                Report.PostInfo["org_unit"] = escape($("#org_unit").val());
                Report.PostInfo["make_unit"] = escape($("#make_unit").val());
                Report.PostInfo["send_unit"] = escape($("#send_unit").val());
                Report.PostInfo["template_encoding"] = Report.templateEcoding;
                var report_type = Report.reportType;
                var url = "../Handler/" + Report.reportUrlList[report_type] + ".ashx";
                $("#report_back_msg").empty().html("后台正在生成简报，请稍候！");
                $.post(url,
                    Report.PostInfo,
                    function(data) {
                        if (data["SucceseCode"] == "1") {
                            var htmlStr = unescape(data["HtmlStr"]);
                            Report.SaveDoc(htmlStr);
                            $("#report_back_msg").empty();
                        }
                        else {
                            $("#Btn_submit").next("span").empty();
                            $("#report_back_msg").empty();
                            alert("生成失败！");
                        }
                    },
                    "json"
                );
            }
            else {
                alert("请选择生成的数据源！");
            }
        });
    },
    BtnSelInit: function() {
        $("[id^='sel_']").each(function() {
            $(this).click(function() {
                if (Report.checkTime()) {
                    $("#content_info").empty();
                    $("#result_list").empty();
                    $("#pager_list").empty();
                    var param = $(this).attr("id").split('_')[1];
                    Report.QueryType = Report.GetQueryType(param);
                    Report.InfoSelectInit(param);
                }
                else {
                    alert("请选择时间范围！");
                    $("input[id^='sel_']").removeAttr("checked");
                }
            });

        });
    },
    checkTime: function() {
        if ($("#report_start_time").val() == "") {
            return false;
        }
        if (Report.reportType != "daily") {
            if ($("#report_end_time").val() == "") {
                return false;
            }
        }
        return true;
    },
    GetQueryType: function(param) {
        switch (param) {
            case "all":
            case "news":
            case "bbs":
            case "wmry":
            case "wmryblog":
                return "query";
            case "important":
            case "event":
                return "categoryquery";
            case "clusters":
                return "clusters";
            default:
                return null;
        }
    },
    InfoSelectInit: function(param) {
        var content = [];
        content.push("<ul>");
        switch (param) {
            case "event":
                var special_data = Report.EventData;
                if (special_data) {
                    for (var item in special_data) {
                        content.push("<li><input type=\"radio\" pid=\"event\" cate=\"" + special_data[item]);
                        content.push("\" name=\"Info\" value=\"" + item + "\"/>" + unescape(special_data[item]) + "</li>");
                    }                    
                    Report.FirstType = "事故专题";
                    Report.OtherPostType = "event_";
                } else {
                    var mindate = $("#report_start_time").val();
                    var maxdate = $("#report_end_time").val();
                    $.post("../Handler/BriefReportData.ashx",
                        { "type": "geteventcategory", "mindate": mindate, "maxdate": maxdate },
                        function(data) {
                            if (data) {
                                var event_data = data["EventData"];
                                delete event_data["SuccessCode"];
                                Report.EventData = event_data;
                                for (var item in event_data) {
                                    content.push("<li><input type=\"radio\" pid=\"event\" cate=\"" + event_data[item]);
                                    content.push("\" name=\"Info\" value=\"" + item + "\"/>" + unescape(event_data[item]) + "</li>");
                                }
                                Report.FirstType = "事故专题";
                                Report.OtherPostType = "event_";
                                $("#content_info").empty().html(content.join(""));
                                Report.InnitInfoSelectFn();                          
                            }
                        },
                        "json"
                    );
                }
                
                break;
            case "important":
                var event_data = Report.SpecialData;
                if (event_data) {
                    for (var item in event_data) {
                        content.push("<li><input type=\"radio\" pid=\"event\" cate=\"" + event_data[item]);
                        content.push("\" name=\"Info\" value=\"" + item + "\"/>" + unescape(event_data[item]) + "</li>");
                    }
                }
                Report.FirstType = "安监要闻";
                Report.OtherPostType = "special_"
                break;
            case "news":
                Report.IdolQueryParams["text"] = "*";
                delete Report.IdolQueryParams["category"];
                Report.IdolQueryParams["fieldText"] = "MATCH{yaowen}:C2";
                Report.PostCommand();
                Report.FirstType = "时政要闻";
                Report.SecondType = null;
                Report.PostType = "news";
                Report.OtherPostType = "news"
                break;
            case "wmry":
                Report.IdolQueryParams["text"] = Report.keywords.join("+OR+");
                delete Report.IdolQueryParams["category"];
                Report.IdolQueryParams["fieldText"] = "MATCH{bbs}:C1";
                Report.PostCommand();
                Report.FirstType = "网名热议论坛摘要";
                Report.SecondType = null;
                Report.PostType = "wmry";
                Report.OtherPostType = "wmry"
                break;
            case "wmryblog":
                Report.IdolQueryParams["text"] = Report.keywords.join("+OR+");
                delete Report.IdolQueryParams["category"];
                Report.IdolQueryParams["fieldText"] = "MATCH{blog}:C1";
                Report.PostCommand();
                Report.FirstType = "网名热议博客摘要";
                Report.SecondType = null;
                Report.PostType = "wmryblog";
                Report.OtherPostType = "wmryblog"
                break;
            case "clusters":
                Report.InnitCluster(Report.ClustersJob);
                Report.FirstType = "一周热点";
                Report.OtherPostType = "clusters_"
            default:
                break;
        }
        content.push("</ul>");
        if (param != "clusters") {
            $("#content_info").empty().html(content.join(""));
            Report.InnitInfoSelectFn();
        }
    },
    InnitCluster: function(job) {
        if (!Report.ClutersData) {
            $("#content_info").empty().html("<center><span style=\"color:black;\">数据正在加载中...</span></center>");
            $.post("../Handler/BriefReportData.ashx",
                { "type": "clusters", "job_name": job },
                function(data) {
                    if (data) {
                        Report.ClutersData = data;
                        var content = [];
                        delete data["SuccessCode"];
                        for (var item in data) {
                            content.push("<li><input type=\"radio\"  cate=\"" + item);
                            content.push("\" name=\"Info\" value=\"" + item + "\"/>" + unescape(item) + "</li>");
                        }
                        $("#content_info").empty().html(content.join(""));
                        Report.InnitInfoSelectFn();
                    }
                },
                "json"
            );
        } else {
            var data = Report.ClutersData;
            var content = [];
            delete data["SuccessCode"];
            for (var item in data) {
                content.push("<li><input type=\"radio\"  cate=\"" + item);
                content.push("\" name=\"Info\" value=\"" + item + "\"/>" + unescape(item) + "</li>");
            }
            $("#content_info").empty().html(content.join(""));
            Report.InnitInfoSelectFn();
        }
    },
    InnitInfoSelectFn: function() {
        $("#content_info").find("input[name='Info']").click(function() {
            var type = Report.QueryType;
            switch (type) {
                case "query":
                    delete Report.IdolQueryParams["category"];
                    break;
                case "categoryquery":
                    delete Report.IdolQueryParams["text"];
                    delete Report.IdolQueryParams["fieldText"];
                    var categoryname = $(this).attr("cate");
                    var categoryid = $(this).val();
                    Report.IdolQueryParams["category"] = categoryid;
                    Report.PostCommand();
                    Report.SecondType = unescape($(this).attr("cate"));
                    Report.PostType = Report.OtherPostType + categoryid + "_" + categoryname;
                    break;
                case "clusters":
                    var key = $(this).attr("cate");
                    var data = Report.ClutersData[key];
                    Report.SecondType = unescape($(this).attr("cate"));
                    Report.PostType = Report.OtherPostType + key;
                    Report.InnitClustersHtml(data);
                    break;
                default:
                    break;
            }
        });
    },
    InnitClustersHtml: function(data) {
        delete data["SuccessCode"];
        var content = [];
        for (var item in data) {
            var entity = data[item];
            var basetitle = unescape(entity["title"]);
            var title = basetitle.length > 27 ? basetitle.slice(0, 27) + "..." : basetitle;
            content.push("<li><div class=\"trainSelect\"><input  type=\"checkbox\" name=\"train_article_list\" pid=\"" + unescape(entity["href"]) + "\"/>&nbsp;</div>");
            content.push("<h2><a href=\"" + unescape(entity["href"]) + "\" title=\"" + basetitle + "\" target=\"_blank\">");
            content.push(title + "</a></h2>");
            content.push("<div class=\"d\"><span>" + unescape(entity["sitename"]) + "</span> - " + unescape(entity["timestr"]) + "</div>");
            content.push("<p>" + unescape(entity["content"]) + " <b>...</b></p>");
            content.push("</li>");
        }
        var result_id = Report.InitData["result_id"];
        var pager_id = Report.InitData["status_bar_id"];
        $("#" + result_id).empty().html(content.join(""));
        $("#" + pager_id).empty();
        Report.InnitCheckBoxFn();
    },
    PostCommand: function() {
        Report.IdolQueryParams["action"] = Report.QueryType;
        Report.IdolQueryParams["mindate"] = Report.GetTimeStr($("#report_start_time").val());
        Report.IdolQueryParams["maxdate"] = Report.GetTimeStr($("#report_end_time").val());
        var Lpager = new Pager(Report.InitData);
        Lpager.OtherFn = function(total_count) {
            Report.InnitCheckBoxFn();
        }
        Lpager.LoadData(1, Report.IdolQueryParams);
    },
    GetTimeStr: function(timestr) {
        if (timestr) {
            timestr = Report.replaceAll(timestr, "-", "/");
            var time = new Date(timestr);
            return time.getDate() + "/" + (time.getMonth() + 1) + "/" + time.getFullYear();
        }
        else {
            var time = new Date();
            var basemonth = time.getMonth() + 1;
            var month = basemonth < 10 ? "0" + basemonth : basemonth;
            var basedate = time.getDate();
            var date = basedate < 10 ? "0" + basedate : basedate;
            return time.getFullYear() + "年" + month + "月" + date + "日";
        }
    },
    replaceAll: function(s, s1, s2) {
        return s.replace(new RegExp(s1, "gm"), s2);
    },
    getPostParams: function() {
        Report.PostInfo = {};
        $("#select_result").children("li").each(function() {
            if (!Report.PostTag) {
                Report.PostTag = true;
            }
            var type = $(this).attr("name");
            var postval = $(this).attr("pid");
            if (Report.PostInfo[type]) {
                Report.PostInfo[type] = Report.PostInfo[type] + "+" + postval;
            } else {
                Report.PostInfo[type] = postval;
            }
        });
    },
    SaveDoc: function(htmlstr) {
        var checkstr = $("#is_check").val();
        var title = $("#report_title").val();
        var filename = Report.reportUrlList[Report.reportType];
        var reportPeople = $("#report_editer").val();
        $.post("../Handler/ManageFile.ashx",
            { "type": "editFile", "time_str": Report.GetTimeStr(), "report_title": escape(title), "ecoding": Report.templateEcoding,
                "report_people": reportPeople, "file_name": filename, "html_str": htmlstr, "chec_kstr": checkstr
            },
            function(l_data) {
                function HideSpan() {
                    $("#report_back_msg").empty();
                }
                if (l_data["SucceseCode"] == "1") {
                    $("#report_back_msg").empty().html("生成成功！");
                    setTimeout(HideSpan, 1000);
                }
                else {
                    $("#report_back_msg").empty().html("生成失败！");
                    setTimeout(HideSpan, 1000);
                }
            },
            "json"
        );
    },
    InnitCheckBoxFn: function() {
        $("#result_list").find("input[name='train_article_list']").click(function() {
            if ($(this).attr("checked")) {
                var title_tag = null;
                if (Report.SecondType) {
                    title_tag = Report.FirstType + ">>" + Report.SecondType + ">>";
                } else {
                    title_tag = Report.FirstType + ">>";
                }
                var content = [];
                var href = $(this).attr("pid");
                var title = $(this).parent("div").siblings("h2").children("a").attr("title");
                content.push("<li name=\"" + Report.PostType + "\" pid=\"" + escape(href) + "\"><span>" + title_tag);
                content.push("</span>&nbsp;&nbsp;<span><a href=\"" + href + "\" target=\"_blank\" title=\"" + title + "\">" + title + "</a></span>&nbsp;&nbsp;");
                content.push("<span class=\"title_del\" name=\"title_del\"><a href=\"javascript:void(null);\">删除</a></span></li>");
                $("#select_result").append(content.join(""));
                $("#select_result").find("span[name='title_del']").click(function() {
                    $(this).parent("li").remove();
                });
            }
        });
    },
    GetPicList: function() {
        var pic_list = [];
        $(":checkbox[name='pic_list']").each(function() {
            if ($(this).attr("checked")) {
                var pic_type = $(this).attr("pid");
                pic_list.push(pic_type);
            }
        });
        if (pic_list.length > 0) {
            return pic_list.join(",");
        } else {
            return "";
        }
    }
}