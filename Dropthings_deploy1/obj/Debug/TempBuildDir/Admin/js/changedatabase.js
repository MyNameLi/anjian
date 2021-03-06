﻿$(document).ready(function() {

    IdolNew.InnitClickFn();
    IdolNew.InitSiteSelect();
    if ($("#databse_list")) {
        IdolNew.InitDabase();
    }
    IdolNew.queryParams["database"] = IdolNew.GetDataBase();
    IdolNew.Innit();
});
var _idolnew = new Object;
var IdolNew = _idolnew.property = {
    websitejson: ["bbs", "newssource", "safety", "portalsafety"],
    postUrl: location.href,
    DelPostUrl: "list.aspx",
    initData: { "page_size": 20, "result_id": "all_data_list", "status_bar_id": "pager_list", "dis_del": true },
    queryParams: { "action": "query", "display_style": 6, "text": "*", "sort": "date",
        "printfields": "DRETITLE,MYSITENAME,DREDATE,DOMAINSITENAME,C1,SAMENUM", "predict": "true"
    },
    ArchiveRefs: null,
    Innit: function() {
        if (IdolNew.queryParams["fieldtext"]) {
            IdolNew.queryParams["fieldtext"] += "+AND+EMPTY{}:CONTURL";
        } else {
            IdolNew.queryParams["fieldtext"] = "EMPTY{}:CONTURL";
        }
        var Lpager = new Pager(IdolNew.initData);
        Lpager.OtherFn = function(totalcount) {
            //IdolNew.InnitWebSite();
            $("a[name='idol_news_store']").empty().html("信息归档");
            $("a[name='idol_news_store']").click(function() {
                var columns = $(this).attr("cate");
                var href = $(this).attr("pid");
                var current_obj = this;
                $.post(IdolNew.postUrl,
                    { "act": "store", "ajaxString": 1, "url": href, "siteidlist": Config.AimDataBase },
                    function(data) {
                        if (data.Success == 1) {
                            $(current_obj).parent("td").parent("tr").remove();
                        }
                        if (data.Error == 1) {
                            alert("信息归档失败,原因：" + data["ErrorStr"]);
                        }
                    },
                    "json"
                );
                //IdolNew.InnitChooseSiteFrame(href, columns);
            });
            $("a[name='idol_news_delete']").click(function() {
                var href = $(this).attr("pid");
                var current_obj = this;
                $.post(IdolNew.DelPostUrl,
                    { "act": "newsdelete", "ajaxString": 1, "url": href },
                    function(data) {
                        if (data.Success == 1) {
                            $(current_obj).parent("td").parent("tr").remove();
                        }
                        if (data.Error == 1) {
                            alert("删除失败,原因：" + data["ErrorStr"]);
                        }
                    },
                    "json"
                );
            });

            var discount = 0;
            $("a[name='dis_look_same_news']").each(function() {
                var same_num = $(this).attr("cate");
                if (same_num && parseInt(same_num) > 0) {
                    discount++;
                    $(this).click(function() {
                        var href = $(this).attr("pid");
                        var initdata = { "page_size": 10, "result_id": "same_news_all_list",
                            "status_bar_id": "same_news_pager", "dissame": true
                        };
                        var query_params = { "action": "query", "display_style": 6, "characters": 600, "combine": "simple", "totalresults": true,
                            "summary": "context", "predict": "false"
                        }
                        query_params["fieldtext"] = "MATCH{" + unescape(href) + "}:CONTURL";
                        var Lpager = new Pager(initdata);
                        Lpager.LoadData(1, query_params);
                        IdolNew.ShowEditFrame("move_same_news", "same_news_frame", "close_same_news_frame");
                    });

                } else {
                    $(this).remove();
                }
            });
            if (discount > 0) {
                $("#look_same_news").show();
                $("td[name='dis_same_news_td']").show();
            } else {
                $("#look_same_news").hide();
                $("td[name='dis_same_news_td']").hide();
            }
        }
        Lpager.LoadData(1, IdolNew.queryParams);
    },
    InitDabase: function() {
        var database_list = Config.ClawerDataBase;
        var default_database = database_list["DefaultDataBase"];
        delete database_list["DefaultDataBase"];
        var content = [];
        content.push("数据源：");
        for (var item in database_list) {
            content.push("<input type=\"checkbox\" name=\"databse_list\"");
            if (item == default_database) {
                content.push(" checked=\"checked\" ");
            }
            content.push(" value=\"" + item + "\" />" + database_list[item] + "&nbsp;&nbsp;&nbsp;")
        }
        $("#databse_list").empty().html(content.join(""));
    },
    InitSiteSelect: function() {
        $.post("../Handler/GetSiteList.ashx",
            null,
            function(data) {
                if (data) {
                    if (data.Success == 1) {
                        delete data.Success;
                        var content = [];
                        content.push("<option value=\"all\">全部</option>");
                        for (var item in data) {
                            content.push("<option  value=\"" + item + "\">" + unescape(data[item]) + "</option>");
                        }
                        $("#info_site").empty().html(content.join(""));
                    }
                }
            },
            "json"
        );
    },
    InnitWebSite: function() {
        if (!IdolNew.websitejson) {
            $.post(IdolNew.postUrl,
            { "act": "innitwebsite", "ajaxString": 1 },
            function(data) {
                if (data.Success == 1) {
                    delete data["Success"];
                    IdolNew.websitejson = data;
                } else {
                    alert("加载站点列表失败！");
                }
            },
            "json"
        );
        }
    },
    InnitClickFn: function() {
        $("#all_news_store").click(function() {
            var list = $("#all_data_list").find("input:checked");
            var len = list.length;
            if (len == 0) {
                alert("请选择信息归档文章！");
                return;
            }
            var post_url = [];
            list.each(function() {
                var url = unescape($(this).val());
                post_url.push(url);
            });

            $.post(IdolNew.postUrl,
                { "act": "store", "ajaxString": 1, "url": escape(post_url.join("+")), "siteidlist": "PortalSafety" },
                function(data) {
                    if (data.Success == 1) {
                        list.each(function() {
                            $(this).parent("td").parent("tr").remove();
                        });
                    }
                    if (data.Error == 1) {
                        alert("信息归档失败,原因：" + data["ErrorStr"]);
                    }
                },
                "json"
            );
            //IdolNew.InnitChooseSiteFrame(escape(post_url.join("+")));
        });
        $("#btn_delete_all").click(function() {
            var url_list = [];
            var current_list = $("#all_data_list").find(":checkbox:checked");
            current_list.each(function() {
                var val = $(this).val();
                url_list.push(unescape(val));
            });
            if (url_list.length == 0) {
                alert("请选择要删除的信息！");
                return;
            }
            if (!confirm("您确定要删除吗？")) {
                return;
            }
            $.post(IdolNew.DelPostUrl,
                { "act": "newsdelete", "ajaxString": 1, "url": escape(url_list.join("+")) },
                function(data) {
                    if (data.Success == 1) {
                        current_list.parent("td").parent("tr").remove();
                    }
                    if (data.Error == 1) {
                        alert("删除失败,原因：" + data["ErrorStr"]);
                    }
                },
                "json"
            );
        });

        IdolNew.InitCategory("category_id", 0, true);

        $("#category_id").change(function() {
            var parentcate = $(this).val().split('_')[1];
            IdolNew.InitCategory("category_child_id", parentcate, false);
        });

        $("input[name='search_codition']").click(function() {
            var tag = $(this).val();
            $("span[id$='_search_frame']").hide();
            $("#" + tag + "_search_frame").show();
        });

        $("#btn_store").click(function() {
            if (!IdolNew.ArchiveRefs) {
                alert("请选择移库的信息！");
                return;
            }
            var site_list = [];
            $("#web_site_list").find("input:checked").each(function() {
                var site_id = $(this).attr("pid");
                site_list.push(site_id);
            });
            if (site_list.length == 0) {
                alert("请选择移库的的站点");
                return;
            }
            var columnid = $("#site_column_list").val();

            $.post(IdolNew.postUrl,
                { "act": "store", "ajaxString": 1, "url": IdolNew.ArchiveRefs, "siteidlist": site_list.join(","), "columnid": columnid },
                function(data) {
                    if (data.Success == 1) {
                        alert("信息归档成功");
                    }
                    if (data.Error == 1) {
                        alert("信息归档失败,原因：" + data["ErrorStr"]);
                    }
                },
                "json"
            );
        });

        $("#btn_look_info").click(function() {
            var keyword = $.trim($("#keyword").val());

            var action_value = $("input[name='search_codition']:checked").val()
            IdolNew.queryParams["action"] = action_value;
            if (action_value == "query") {
                if (!keyword) {
                    alert("请输入关键字！");
                    return;
                }
                delete IdolNew.queryParams["category"];
                IdolNew.queryParams["text"] = keyword;
            } else {
                delete IdolNew.queryParams["text"];
                IdolNew.queryParams["category"] = $("#category_child_id").val().split('_')[0];
            }
            var database = IdolNew.GetDataBase();
            if (!database) {
                alert("请选择数据源");
                return;
            }
            if (action_value == "query") {
                IdolNew.queryParams["database"] = database;
            } else if (action_value == "categoryquery") {
                IdolNew.queryParams["database"] = database;
            }
            IdolNew.queryParams["sort"] = $("#sort_style").val();
            var min_date = $.trim($("#start_time").val());
            if (min_date) {
                IdolNew.queryParams["mindate"] = min_date;
            } else {
                delete IdolNew.queryParams["mindate"];
            }
            var max_date = $.trim($("#end_time").val());
            if (max_date) {
                IdolNew.queryParams["maxdate"] = max_date;
            } else {
                delete IdolNew.queryParams["maxdate"];
            }
            var min_score = $.trim($("#min_score").val());
            if (min_score) {
                IdolNew.queryParams["minscore"] = min_score;
            } else {
                delete IdolNew.queryParams["minscore"];
            }
            var infotype = $("#info_type").val();
            if (infotype != "all") {
                IdolNew.queryParams["fieldtext"] = "MATCH{" + infotype + "}:C1";
            } else {
                delete IdolNew.queryParams["fieldtext"];
            }
            var info_site = $("#info_site").val();
            if (info_site != "all") {
                if (IdolNew.queryParams["fieldtext"] && IdolNew.queryParams["fieldtext"] != "") {
                    IdolNew.queryParams["fieldtext"] = IdolNew.queryParams["fieldtext"] + " AND MATCH{" + info_site + "}:TASKID";
                } else {
                    IdolNew.queryParams["fieldtext"] = "MATCH{" + info_site + "}:TASKID";
                }
            } else {
                if (!IdolNew.queryParams["fieldtext"] && IdolNew.queryParams["fieldtext"] == "") {
                    delete IdolNew.queryParams["fieldtext"];
                }
            }

            IdolNew.Innit();
        });
    },
    GetDataBase: function() {
        var data_base_list = [];
        $("input[name='databse_list']").each(function() {
            if ($(this).attr("checked")) {
                var data_base = $(this).val();
                data_base_list.push(data_base);
            }
        });
        if (data_base_list.length > 0) {
            return data_base_list.join("+");
        } else {
            return null;
        }
    },
    InnitChooseSiteFrame: function(url, columns) {
        IdolNew.ArchiveRefs = url;
        if (IdolNew.websitejson) {
            var data = IdolNew.websitejson;
            var content = [];
            for (var i = 0, j = data.length; i < j; i++) {
                var databse = data[i];
                content.push("<li><span><input type=\"radio\" name=\"site_list_select\" ");
                if (i == 0) {
                    content.push(" checked=\"checked\"");
                }
                content.push(" pid=\"" + databse + "\" /></span>");
                content.push("<span>" + databse + "</span></li>");
            }
            $("#web_site_list").empty().html(content.join(""));
            IdolNew.ShowEditFrame("move_column", "column_edit_frame", "close_edit_frame");
        }
    },
    ShowEditFrame: function(child_div, parent_div, close_btn) {
        $("#" + parent_div).show();
        var iframe_width = $(document).width();
        var l_width = parseInt($("#" + parent_div).width()) / 2;
        iframe_width = parseInt(iframe_width / 2) - l_width;
        var body_offsetTop = $(document).scrollTop() + 150;
        //$("#" + parent_div).css({ "position": "absolute", "top": "50px", "left": iframe_width + "px", "background": "#f9f5f5" });
        $("#" + parent_div).css({ "position": "absolute", "top": body_offsetTop + "px", "left": iframe_width + "px" });
        $("#" + parent_div).show();
        $("#" + close_btn).click(function() {
            $("#" + parent_div).hide();
        });
        var div_move = new divMove(child_div, parent_div);
        div_move.init();
    },
    InitCategory: function(id, parentcate, tag) {
        $.post(location.href,
            { "act": "innitcategory", "ajaxString": 1, "parentcate": parentcate },
            function(data) {
                if (data) {
                    delete data.Success;
                    var content = [];
                    var count = 1;
                    for (var item in data) {
                        var entity = data[item];
                        if (count == 1 && tag) {
                            IdolNew.InitCategory("category_child_id", entity["id"], false);
                        }

                        content.push("<option value=\"" + entity["categoryid"] + "_" + entity["id"] + "\">");
                        content.push(unescape(entity["categoryname"]) + "</option>");
                        count++;
                    }
                    $("#" + id).empty().html(content.join(""));
                }
            },
            "json"
        );
    }
}