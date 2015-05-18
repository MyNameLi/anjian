$(document).ready(function() {
    IdolNew.queryParams["database"] = "portalsafety";
    IdolNew.Innit();
    IdolNew.InnitClickFn();
});
var _idolnew = new Object;
var IdolNew = _idolnew.property = {
    websitejson: ["bbs", "newssource", "safety", "portalsafety"],
    leaderinfo: null,
    postUrl: location.href,
    initData: { "page_size": 10, "result_id": "all_data_list", "status_bar_id": "pager_list" },
    queryParams: { "action": "query", "display_style": 6, "text": "*", "sort": "date", "printfields": "DRETITLE,MYSITENAME,DREDATE,DOMAINSITENAME,C1"
    },
    ArchiveRefs: null,
    Innit: function() {
        IdolNew.InitLeaderInfo();
        var Lpager = new Pager(IdolNew.initData);
        Lpager.OtherFn = function(totalcount) {
            //IdolNew.InnitWebSite();
            $("a[name='idol_news_store']").empty().html("推送");
            $("a[name='idol_news_store']").click(function() {
                var userid = $("#leader_list").find(":checked").val();
                if (undefined == userid) {
                    alert("请选择所要推送信息的领导");
                    return;
                }
                var url = $(this).attr("pid");
                var title = $(this).attr("distitle");
                var content = [];
                content.push("<tr name=\"push_info_item\" pid=\"" + url + "\" cate=\"0\"><td height=\"26\" bgcolor=\"#FFFFFF\" align=\"center\">");
                content.push("<a title=\"" + unescape(title) + "\" target=\"_blank\" href=\"" + unescape(url) + "\">" + unescape(title) + "</a></td>");
                content.push("<td bgcolor=\"#FFFFFF\" align=\"center\"><img width=\"9\" height=\"9\" src=\"../images/010.gif\">");
                content.push("[ <a name=\"info_delete\" href=\"javascript:void(null);\">删除</a>]</td>");
                content.push("<td bgcolor=\"#FFFFFF\" align=\"center\"><input type=\"checkbox\" name=\"info_record\">是</td></tr>");
                $("#push_info_list").append(content.join(""));
                $("a[name='info_delete']").click(function() {
                    $(this).parent("td").parent("tr").remove();
                });
                $("input[name='info_record']").click(function() {
                    if ($(this).attr("checked")) {
                        $(this).parent("td").parent("tr").attr("cate", "1");
                    } else {
                        $(this).parent("td").parent("tr").attr("cate", "0");
                    }
                });
            });
        }
        Lpager.LoadData(1, IdolNew.queryParams);
    },
    InitLeaderInfo: function() {
        var role_str = [];
        var rolejson = Config.RoleStr;
        for (var item in rolejson) {
            role_str.push(item);
        }
        $.post(IdolNew.postUrl,
            { "act": "initleaderinfo", "ajaxString": 1, "role_str": role_str.join(",") },
            function(l_data) {
                if (l_data.Success == 1) {
                    delete l_data["Success"];
                    var data = l_data["leader"];
                    delete data["Success"];
                    var content = [];
                    var historycontent = [];
                    var role_data = {};
                    for (var item in data) {
                        var entity = data[item];
                        var user_name = unescape(entity["username"]);
                        role_data[user_name] = entity;
                    }
                    for (var item in rolejson) {
                        content.push(IdolNew.GetLeaderHtmlStr(role_data, item));
                        historycontent.push(IdolNew.GetLeaderHtmlStr(role_data, item, true));
                    }
                    $("#leader_list").empty().html(content.join(""));
                    $("#history_leader_list").empty().html(historycontent.join(""));
                    $("#leader_list").find("input").click(function() {
                        $("#push_info_list").empty();
                    });
                    $("input[name='leader_list_item']").click(function() {
                        var userid_list = [];
                        $("input[name='leader_list_item']").each(function() {
                            if ($(this).attr("checked")) {
                                var userid = $(this).val();
                                userid_list.push(userid);
                            }
                        });
                        if (userid_list.length == 0) {
                            $("#push_info_list").empty();
                            return;
                        }
                        IdolNew.ClickFnObject("push_info_list", userid_list, null, true);
                    });

                } else {
                    alert("加载站点列表失败！");
                }
            },
            "json"
        );
    },
    ClickFnObject: function(obj, userid_list, pushtime, tag) {
        var post_param = { "act": "getleaderinfo", "ajaxString": 1, "userid": escape(userid_list.join(",")) };
        if (pushtime) {
            post_param["push_time"] = escape(pushtime);
        }
        $.post(IdolNew.postUrl,
            post_param,
            function(data) {
                if (data) {
                    delete data["Success"];
                    var content = [];
                    for (var item in data) {
                        var entity = data[item];
                        var url = entity["href"];
                        var title = entity["title"];
                        content.push("<tr name=\"push_info_item\" pid=\"" + url + "\" ");
                        content.push("cate=\"" + entity["type"] + "\"");
                        content.push("><td height=\"26\" bgcolor=\"#FFFFFF\" align=\"left\">");
                        content.push("<a title=\"" + unescape(title) + "\" target=\"_blank\" href=\"" + unescape(url) + "\">" + unescape(title) + "</a></td>");
                        if (tag) {
                            content.push("<td bgcolor=\"#FFFFFF\" align=\"center\"><img width=\"9\" height=\"9\" src=\"../images/010.gif\">");
                            content.push("[ <a name=\"info_delete\" href=\"javascript:void(null);\">删除</a>]</td>");
                            content.push("<td bgcolor=\"#FFFFFF\" align=\"center\"><input type=\"checkbox\" ");
                            if (entity["type"] == "1") {
                                content.push("checked=\"checked\"");
                            }
                            content.push(" name=\"info_record\">是</td>");
                        }
                        content.push("</tr>");
                    }
                    $("#" + obj).empty().html(content.join(""));
                    if (tag) {
                        $("#" + obj).find("a[name='info_delete']").click(function() {
                            $(this).parent("td").parent("tr").remove();
                        });
                        $("#" + obj).find("input[name='info_record']").click(function() {
                            if ($(this).attr("checked")) {
                                $(this).parent("td").parent("tr").attr("cate", "1");
                            } else {
                                $(this).parent("td").parent("tr").attr("cate", "0");
                            }
                        });
                    }
                } else {
                    $("#" + obj).empty();
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
                alert("请选择推送文章！");
                return;
            }
            var post_url = [];
            list.each(function() {
                var url = unescape($(this).val());
                post_url.push(url);
            });
            IdolNew.InnitChooseSiteFrame(escape(post_url.join("+")));
        });

        $("#choose_all_leader").click(function() {
            if ($(this).attr("checked")) {
                $("#leader_list").find("input").attr("checked", "checked");
            } else {
                $("#leader_list").find("input").removeAttr("checked");
            }
        });

        $("#look_push_history").click(function() {
            var userid_list = [];
            $("input[name='leader_history_item']").each(function() {
                if ($(this).attr("checked")) {
                    var userid = $(this).val();
                    userid_list.push(userid);
                }
            });
            if (userid_list.length == 0) {
                alert("请选择领导！");
                return;
            }
            var push_time = $.trim($("#push_time").val());
            IdolNew.ClickFnObject("history_leader_info", userid_list, push_time, false);
        });

        $("#look_history_list").click(function() {
            var tag = $(this).attr("pid");
            if (tag == "0") {
                $("*[name='history_list_frame']").show();
                $(this).empty().html("隐藏历史记录");
                $(this).attr("pid", "1");
            } else {
                $("*[name='history_list_frame']").hide();
                $(this).empty().html("展开历史记录");
                $(this).attr("pid", "0");
            }
        });

        $("#look_push_info").click(function() {
            var tag = $(this).attr("pid");
            if (tag == "0") {
                $("#push_info_list").show();
                $("#push_info_btn").show();
                $(this).attr("pid", "1");
            } else {
                $("#push_info_list").hide();
                $("#push_info_btn").hide();
                $(this).attr("pid", "0");
            }
        });

        $("#all_news_push").click(function() {
            var url_list = $("#all_data_list").find(":checkbox:checked");
            if (url_list.length == 0) {
                alert("请选择要推送的信息");
                return;
            }
            var userid = $("#leader_list").find(":checked").val();
            if (undefined == userid) {
                alert("请选择所要推送信息的领导");
                return;
            }
            var content = [];
            url_list.each(function() {
                var url = $(this).val();
                var title = $(this).attr("pid");
                content.push("<tr name=\"push_info_item\" pid=\"" + url + "\" cate=\"0\"><td height=\"26\" bgcolor=\"#FFFFFF\" align=\"center\">");
                content.push("<a title=\"" + unescape(title) + "\" target=\"_blank\" href=\"" + unescape(url) + "\">" + unescape(title) + "</a></td>");
                content.push("<td bgcolor=\"#FFFFFF\" align=\"center\"><img width=\"9\" height=\"9\" src=\"../images/010.gif\">");
                content.push("[ <a name=\"info_delete\" href=\"javascript:void(null);\">删除</a>]</td>");
                content.push("<td bgcolor=\"#FFFFFF\" align=\"center\"><input type=\"checkbox\" name=\"info_record\">是</td></tr>");

            });
            $("#push_info_list").append(content.join(""));
            $("a[name='info_delete']").click(function() {
                $(this).parent("td").parent("tr").remove();
            });
            $("input[name='info_record']").click(function() {
                if ($(this).attr("checked")) {
                    $(this).parent("td").parent("tr").attr("cate", "1");
                } else {
                    $(this).parent("td").parent("tr").attr("cate", "0");
                }
            });
        });

        $("#btn_push_info").click(function() {
            var url_list = [];
            var push_type = [];
            $("#push_info_list").find("*[name='push_info_item']").each(function() {
                url_list.push(unescape($(this).attr("pid")));
                push_type.push($(this).attr("cate"));
            });
            if (url_list.length > 0) {
                var userid_list = [];
                var roleid_list = [];
                $("#leader_list").find("input[name='leader_list_item']").each(function() {
                    if ($(this).attr("checked")) {
                        var userid = $(this).val();
                        var roleid = $(this).attr("pid");
                        userid_list.push(userid);
                        roleid_list.push(roleid);
                    }
                });
                if (userid_list.length == 0) {
                    $("#push_info_list").empty();
                    return;
                }
                $.post(IdolNew.postUrl,
                    { "act": "store", "ajaxString": 1, "url": escape(url_list.join("+")), "type": push_type.join("+"), "roleid": escape(roleid_list.join(",")), "userid": escape(userid_list.join(",")) },
                    function(data) {
                        if (data.Success == 1) {
                            alert("推送成功！");
                        } else {
                            alert("推送失败！");
                        }
                    },
                    "json"
                );
            }
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
        //$("#" + parent_div).css({ "position": "absolute", "top": "50px", "left": iframe_width + "px", "background": "#f9f5f5" });
        $("#" + parent_div).css({ "position": "absolute", "top": "0px", "left": iframe_width + "px" });
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
    GetLeaderHtmlStr: function(role_data, role_id, tag) {
        var content = [];
        content.push("<span class=\"name\">" + Config.RoleStr[role_id] + "：</span><div>");
        var leader_info = Config.LeaderList[role_id].split(',');
        for (var i = 0, j = leader_info.length; i < j; i++) {
            var key = leader_info[i];
            var entity = role_data[key];
            if (entity) {
                content.push("<span class=\"input\">");
                if (tag) {
                    content.push("<input type=\"radio\" name=\"leader_history_item\"");
                    content.push(" value=\"" + entity["userid"] + "\" pid=\"" + entity["roleid"] + "\" />");
                } else {
                    content.push("<input type=\"checkbox\" name=\"leader_list_item\"");
                    content.push(" value=\"" + entity["userid"] + "\" pid=\"" + entity["roleid"] + "\" />");
                }
                content.push("</span><span class=\"name\"><b>" + unescape(entity["username"]) + "</b></span>");
                content.push("<span class=\"name\">&nbsp;</span>");
            }
        }
        content.push("</div><div class=\"clear\"></div>");
        return content.join("");
    }
}