$(document).ready(function() {
    IdolNew.Innit();
    IdolNew.InnitClickFn();
    //IdolNew.InitSiteSelect();
});
var _idolnew = new Object;
var IdolNew = _idolnew.property = {
    websitejson: null,
    postUrl: location.href,
    initData: { "page_size": 20, "result_id": "all_data_list", "status_bar_id": "pager_list", "dis_del": true, "dis_edit": false },
    queryParams: { "action": "query", "display_style": 6, "text": "*", "sort": "date",
        "printfields": "DRETITLE,MYSITENAME,DREDATE,DOMAINSITENAME,C1", "fieldtext": ""//MATCH{21,23,103}:TASKID
    },
    ArchiveRefs: null,
    Innit: function() {
        IdolNew.queryParams["database"] = IdolNew.GetDataBase();
        //        var info_site = $("#info_site").val();
        //        if (info_site != "all") {
        //            IdolNew.queryParams["fieldtext"] = "MATCH{" + info_site + "}:TASKID";
        //        } else {
        //            delete IdolNew.queryParams["fieldtext"]
        //        }
        var Lpager = new Pager(IdolNew.initData);
        Lpager.OtherFn = function(totalcount) {
            if (!IdolNew.websitejson) {
                IdolNew.InnitWebSite();
            }
            $("a[name='idol_news_store']").click(function() {
                var columns = $(this).attr("cate");
                var href = $(this).attr("pid");
                IdolNew.InnitChooseSiteFrame(href, columns);
            });

            $("a[name='idol_news_delete']").click(function() {
                if (!confirm("您确定要删除么？")) {
                    return;
                }
                var href = $(this).attr("pid");
                var current_obj = this;
                $.post(IdolNew.postUrl,
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
        }
        Lpager.DisFn = function(urllist) {
            $.post(IdolNew.postUrl,
                { "act": "getcolumnname", "ajaxString": 1, "url_list": escape(urllist) },
                function(data) {
                    if (data) {
                        if (data.Success == 1) {
                            delete data["Success"];
                            for (var item in data) {
                                $("#all_data_list").find("td[name='column_article_list'][pid='" + item + "']").empty().html(unescape(data[item]));
                            }
                            $("#all_data_list").find("td[name='column_article_list']").show();
                            $("#dis_article_column").show();
                        }
                    }
                },
                "json"
            );
        }
        Lpager.LoadData(1, IdolNew.queryParams);
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
                alert("请选择栏目定制信息！");
                return;
            }
            var post_url = [];
            list.each(function() {
                var url = unescape($(this).val());
                post_url.push(url);
            });
            IdolNew.InnitChooseSiteFrame(escape(post_url.join("+")));
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
            $.post(IdolNew.postUrl,
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


        IdolNew.InitCategory("category_id", 691, false);

        //        $("#category_id").change(function() {
        //            var parentcate = $(this).val().split('_')[1];
        //            IdolNew.InitCategory("category_child_id", parentcate, false);
        //        });

        $("input[name='search_codition']").click(function() {
            var tag = $(this).val();
            $("span[id$='_search_frame']").hide();
            $("#" + tag + "_search_frame").show();
        });

        $("#btn_store").click(function() {
            if (!IdolNew.ArchiveRefs) {
                alert("请选择栏目定制信息！");
                return;
            }
            var site_list = [];
            $("#web_site_list").find("input:checked").each(function() {
                var site_id = $(this).attr("pid");
                site_list.push(site_id);
            });
            if (site_list.length == 0) {
                alert("请选择栏目定制的站点");
                return;
            }
            var columnid = $("#site_column_list").val();

            $.post(IdolNew.postUrl,
                { "act": "store", "ajaxString": 1, "url": IdolNew.ArchiveRefs, "siteidlist": site_list.join(","), "columnid": columnid },
                function(data) {
                    if (data.Success == 1) {
                        alert("栏目定制成功");
                        location.replace(location.href);
                    }
                    if (data.Error == 1) {
                        alert("栏目定制失败,原因：" + data["ErrorStr"]);
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
                IdolNew.queryParams["category"] = $("#category_id").val().split('_')[0];
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
                IdolNew.queryParams["fieldtext"] = "MATCH{" + infotype + "}:C1"; //+AND+MATCH{21,23,103}:TASKID";
            } else {
                IdolNew.queryParams["fieldtext"] = "";//"MATCH;{21,23,103}:TASKID";
            }
            IdolNew.Innit();
        });
    },
    InnitChooseSiteFrame: function(url, columns) {
        IdolNew.ArchiveRefs = null;
        IdolNew.ArchiveRefs = url;
        if (IdolNew.websitejson) {
            var len = $("#web_site_list").find("input[name='site_list_select']:checked").length;
            if (len == 0) {
                var data = IdolNew.websitejson;
                var content = [];
                for (var item in data) {
                    var entity = data[item];
                    content.push("<li><span><input type=\"radio\" name=\"site_list_select\"  pid=\"" + entity["SiteId"] + "\" /></span>");
                    content.push("<span>" + unescape(entity["SiteName"]) + "</span></li>");
                }
                $("#web_site_list").empty().html(content.join(""));
                $("#web_site_list").find("input[name='site_list_select']").click(function() {
                    var siteid = $(this).attr("pid");
                    $.post(location.href,
                    { "act": "innitsitecolumn", "ajaxString": 1, "siteid": siteid },
                    function(data) {
                        if (data) {
                            $("#site_column_list").empty().html(unescape(data["optionstr"]));
                        }
                    },
                    "json"
                )
                });
            }
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
    }
}