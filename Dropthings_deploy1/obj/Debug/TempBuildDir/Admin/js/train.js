$(document).ready(function() {
    Train.Init(0);
    Train.InitBtnFn();
    Train.InnitParentSelect("0");
});
var _train = new Object;
var Train = _train.property = {
    articleid: 1,
    term_num: 1,
    PostParams: {},
    editData: {},
    initparentid: null,
    initcate: null,
    clickTag: false,
    queryparams: { "action": "getcategorydata", "orderby": " SEQUEUE DESC,ID", "ajaxString": 1 },
    InnitData: { "page_size": 10, "result_id": "all_data_list", "status_bar_id": "item_pager_list",
        "info_id": "item_info_id", "sql_tag": "item", "web_url": location.href
    },
    Init: function(parentid, cate) {
        Train.initparentid = parentid;
        Train.initcate = cate;
        Train.queryparams["strwhere"] = " PARENTCATE=" + parentid;
        var sqlpager = new SqlPager(Train.InnitData);
        sqlpager.Display = function(obj, data) {
            delete data["Count"];
            var content = [];
            var count = 1;
            var start_str = "<td bgcolor=\"#FFFFFF\" align=\"center\" height=\"25\">";
            var end_str = "</td>";
            var edit_pic_str = "<img src=\"../images/037.gif\" width=\"9\" height=\"9\" />";
            var del_pic_str = "<img src=\"../images/010.gif\" width=\"9\" height=\"9\" />";
            for (var item in data) {
                var row = data[item];
                content.push("<tr>");
                content.push(start_str + unescape(row["categoryname"]) + end_str);
                content.push(start_str + edit_pic_str + "[ <a name=\"category_keyword_train\" pid=\"" + row["categoryid"] + "\"");
                content.push(" href=\"javascript:void(null);\">关键词训练</a> ]" + end_str);
                content.push(start_str + edit_pic_str + "[ <a name=\"category_addchild_train\" pid=\"" + row["categoryid"] + "\"");
                content.push(" cate=\"" + row["id"] + "\" href=\"javascript:void(null);\">增加子分类</a> ]" + end_str);
                //
                content.push(start_str + "[ <a ");
                content.push(" cate=\"" + row["id"] + "\" href=\"javascript:window.parent.frames['tabFrame'].location='eventscenter.html?eventid=" + row["id"] + "'\">查看详细</a> ]" + end_str);

                content.push(start_str + edit_pic_str + "[ <a name=\"category_edit_train\" pid=\"" + row["categoryid"] + "\"");
                content.push(" cate=\"" + row["id"] + "\" href=\"javascript:void(null);\">编辑</a> ]" + end_str);
                content.push(start_str + edit_pic_str + "[ <a name=\"category_del_train\" pid=\"" + row["categoryid"] + "\"");
                content.push(" cate=\"" + row["id"] + "\" href=\"javascript:void(null);\">删除</a> ]" + end_str);
                content.push(start_str + edit_pic_str + "[ <a name=\"category_look_child\" pid=\"" + row["id"] + "\"");
                content.push(" cate=\"" + row["parentcate"] + "\" href=\"javascript:void(null);\">查看子分类</a> ]" + end_str);
                content.push("</tr>");
                if (count == 1) {
                    $("#btn_category_back").attr("pid", row["backparentcate"]);
                }
                count++;
            }
            $("#" + obj).empty().html(content.join(""));
            Train.InitClickFn(obj);
        }
        sqlpager.OtherDisplay = function() {
            var len = $("#all_data_list").find("td").length;
            if (len == 1) {
                $("#btn_category_back").attr("pid", cate);
            }
        }
        sqlpager.LoadData(1, Train.queryparams);

    },
    InitClickFn: function(obj) {
        $("#" + obj).find("a[name='category_look_child']").click(function() {
            var parentid = $(this).attr("pid");
            var cate = $(this).attr("cate");
            Train.Init(parentid, cate);
        });
        $("#" + obj).find("a[name='category_del_train']").click(function() {
            if (!confirm("此操作不可恢复，您确定要删除该分类么？")) {
                return;
            }
            var current_obj = this;
            var train_category_id = $(this).attr("pid");
            var category_id = $(this).attr("cate");
            $.post("../Handler/TrainEdit.ashx",
                { "act": "remove", "train_category_id": train_category_id, "category_id": category_id },
                function(data) {
                    if (data) {
                        if (data.errorCode == 0) {
                            $(current_obj).parent("td").parent("tr").remove();
                        } else if (data.errorCode == 1 && data.ChildCount > 0) {
                            alert("该分类下有子分类，不能进行删除！");
                        }
                    }
                },
                "json"
            );
        });

        $("#" + obj).find("a[name='category_edit_train']").click(function() {
            $("#btnSubmit").attr("pid", "edit");
            $("#btnSubmit").val("确定修改");
            var category_id = $(this).attr("cate");
            var parent_category = $(this).attr("pid");
            Train.PostParams = {};
            Train.PostParams["act"] = "innit_edit";
            Train.PostParams["category_id"] = category_id;
            Train.PostParams["parent_category"] = parent_category;
            Train.PostCommand();
            var params = {};
            params["act"] = "choose_article_init";
            params["train_category_id"] = parent_category;
            Train.OtherPostCommand(params);
        });

        $("#" + obj).find("a[name='category_addchild_train']").click(function() {
            $("#btnSubmit").attr("pid", "add");
            $("#btnSubmit").val("确定");
            Train.PostParams = {};
            var parent_cate = $(this).attr("cate");
            var parent_category = $(this).attr("pid");
            var data = {};
            data["ParentCate"] = parent_cate;
            Train.PostParams["parent_category"] = parent_category;
            Train.InitCategoryTrainCodition(data);
            listTable.ShowFrame("move_column", "column_edit_frame", "close_edit_frame");
        });

        $("#" + obj).find("a[name='category_keyword_train']").click(function() {
            var category_id = $(this).attr("pid");
            $("#retrain_term_category").attr("pid", category_id);
            var params = {};
            params["act"] = "term_init";
            params["train_category_id"] = category_id;
            Train.OtherPostCommand(params);
            params["act"] = "weight_init";
            $("#afresh_term").siblings("li[id^='a_term_']").remove();
            Train.OtherPostCommand(params);
            listTable.ShowFrame("category_term_move", "category_term_frame", "category_term_close");
        });
    },
    InitBtnFn: function() {
        $("#btn_category_back").click(function() {
            var parentid = $(this).attr("pid");
            Train.Init(parentid);
        });

        $("#add_new_catagory").click(function() {
            $("#btnSubmit").attr("pid", "add");
            $("#btnSubmit").val("确定");
            listTable.ShowFrame("move_column", "column_edit_frame", "close_edit_frame");
            var data = {};
            data["ParentCate"] = "0";
            Train.PostParams = {};
            Train.InitCategoryTrainCodition(data);
        });

        $("#btnSubmit").click(function() {
            var act_type = $(this).attr("pid");
            Train.PostParams["act"] = act_type;
            if (Train.CheckAndInitPostParams()) {
                Train.PostCommand();
            }
        });

        $("#article_trainning").click(function() {
            var value = $.trim($("#cate_train_info").val());
            if (!value) {
                alert("请输入初始分类条件");
                $(this).removeAttr("checked");
                return;
            }
            var tag = $(this).attr("checked");
            if (tag) {
                $("#trainingArticle").show();
                var init_data = { "page_size": 10, "status_bar_id": "PagerList", "result_id": "SearchResult" };
                var l_Pager = new Pager(init_data);
                var query_params = { "action": "query", "display_style": 3, "totalresults": "true", "outputencoding": "utf8",
                    "minscore": "60", "summary": "Context", "sort": "Relevance", "languagetype": "chineseUTF8"
                };
                query_params["text"] = value;
                var min_date = $.trim($("#EventDate").val());
                if (min_date) {
                    query_params["mindate"] = Common.GetTimeStr(min_date);
                } else {
                    delete query_params["mindate"];
                }
                l_Pager.LoadData(1, query_params);
            }
            else {
                $("#trainingArticle").hide();
                $("#SearchResult").empty();
                $("#PagerList").empty();
                $("#article_train").next("span").html("");
            }
        });

        $("#BtnSearch").click(function() {
            var init_data = { "page_size": 10, "status_bar_id": "PagerList", "result_id": "SearchResult" };
            var l_Pager = new Pager(init_data);
            var query_params = { "action": "query", "display_style": 3, "totalresults": "true", "outputencoding": "utf8",
                "minscore": "60", "summary": "Context", "sort": "Relevance", "languagetype": "chineseUTF8"
            };
            var keyword_value = $.trim($("#key_words").val());
            if (keyword_value) {
                query_params["text"] = keyword_value;
            } else {
                query_params["text"] = $.trim($("#cate_train_info").val());
            }
            var min_date = $.trim($("#EventDate").val());
            if (min_date) {
                query_params["mindate"] = Common.GetTimeStr(min_date);
            } else {
                delete query_params["mindate"];
            }
            l_Pager.LoadData(1, query_params);
        });

        $("#look_article_list").live("click", function() {
            var value = $(this).attr("pid");
            if (value == "hidden") {
                $("#choose_article_list_id").siblings("div[id^='article_list_']").show();
                $(this).attr("pid", "show");
                $(this).empty().html("隐藏文章列表");
            } else {
                $("#choose_article_list_id").siblings("div[id^='article_list_']").hide();
                $(this).attr("pid", "hidden");
                $(this).empty().html("展开文章列表");
            }
        });

        $("#choose_all").click(function() {
            var article_list = $("input[name=train_article_list]");
            if ($(this).attr("checked")) {
                $(article_list).each(function() {
                    $(this).attr("checked", "true");
                });
            }
            else {
                $(article_list).each(function() {
                    $(this).attr("checked", "");
                });
            }
        });

        $("#save_docs").click(function() {
            $("#article_train").next("span").html("");
            var doc_id_list = [];
            var article_list = $("input[name=train_article_list]");
            $(article_list).each(function() {
                if ($(this).attr("checked")) {
                    doc_id_list.push($(this).attr("pid"));
                }
            });
            var choose_article = [];
            for (var i in doc_id_list) {
                var article_href = doc_id_list[i];
                choose_article.push("<div class=\"article_list\" id=\"article_list_" + Train.articleid + "\"><ul>");
                choose_article.push("<li class=\"article_list_num\">&nbsp;</li>");
                choose_article.push("<li class=\"article_list_href\" ><a href=\"" + article_href + "\" target=\"_blank\" name=\"article_list_href\">" + article_href + "</a></li>");
                choose_article.push("<li class=\"article_list_del\"><a href=\"#g\" name=\"article_list_del\" pid=\"" + Train.articleid + "\">删除</a></li>");
                choose_article.push("</ul></div>");
                Train.articleid++;
            }
            $("#choose_article_list_id").before(choose_article.join(""));
            $("a[name=article_list_del]").each(function() {
                $(this).click(function() {
                    $("#article_list_" + $(this).attr("pid")).remove();

                });
            });
        });

        $("#term_add").click(function() {
            var content = [];
            content.push("<li style=\"float:left; margin-left:5px; width:160px;\" id=\"a_term_" + Train.term_num + "\">");
            content.push("<span><input text=\"text\" id=\"term_value_" + Train.term_num + "\" size=\"6\"/></span>");
            content.push("—<b><input id=\"weight_value_" + Train.term_num + "\" text=\"text\"  size=\"6\"/>");
            content.push("</b><a href=\"#g\" style=\"color:red;\" name=\"term_del\">删除</a></li>");
            $("#afresh_term").before(content.join(""));

            $("#term_value_" + Train.term_num).blur(function() {
                if ($.trim($(this).val()) != "") {
                    $(this).parent("span").empty().html($(this).val());
                }
            });

            $("#weight_value_" + Train.term_num).blur(function() {
                if ($.trim($(this).val()) != "") {
                    $(this).parent("b").empty().html($(this).val());
                }
            });
            Train.term_num++;
            Train.initEdit();
            $("a[name=term_del]").each(function() {
                $(this).click(function() {
                    $(this).parent("li").remove();
                });
            })
        });

        $("#save_term").click(function() {
            $("#original_term").find(":checkbox[checked=true]").each(function(n) {
                var value = $(this).siblings("b").html().split('：');
                $("#afresh_term").before("<li style=\"float:left; margin-left:5px; width:160px;\" id=\"a_term_" + Train.term_num + "\"><span>" + value[0] + "</span>—<b>" + value[1] + "</b><a href=\"#g\" style=\"color:red;\" name=\"term_del\">删除</a></li>");
                Train.term_num++;
            });
            Train.initEdit();
            $("a[name=term_del]").each(function() {
                $(this).click(function() {
                    $(this).parent("li").remove();
                });
            })
        });

        $("#re_train").click(function() {
            var category_id = $("#retrain_term_category").attr("pid");
            if (category_id) {
                function ClearMessage() {
                    $("#back_msg").empty();
                }
                var Terms = new Array();
                var Weights = new Array();
                $("#afresh_term").siblings("li").each(function() {
                    Terms.push($(this).children("span").html());
                    Weights.push($(this).children("b").html());
                });
                $.post("../Handler/TrainEdit.ashx",
                    { "act": "weight_train", "train_category_id": category_id, "weight_terms": escape(Terms.join(",").toString()), "weight_values": Weights.join(",").toString() },
                    function(data, textStatus) {
                        $("#back_msg").empty().html(data);
                        location.replace(location.href);
                        //setTimeout(ClearMessage, 2000);
                    }
                );
            }
        });
    },
    InnitParentSelect: function(id) {
        $.post("../Handler/GetTrainMenu.ashx",
            function(data, textstatus) {
                $("#ParentCate").html(data);
                $("#ParentCate").val(id);
            }
        );
    },
    PostCommand: function() {
        function ClearMessage() {
            $("#Message").empty();
        }
        $.post("../Handler/TrainEdit.ashx",
            Train.PostParams,
            function(data) {
                if (data) {
                    var type = Train.PostParams["act"];
                    switch (type) {
                        case "innit_edit":
                            Train.editData = data;
                            Train.InitCategoryTrainCodition(data);
                            listTable.ShowFrame("move_column", "column_edit_frame", "close_edit_frame");
                            break;
                        case "add":
                            alert("操作成功！");
                            //                            setTimeout(ClearMessage, 2000);
                            //                            Train.Init(Train.initparentid, Train.initcate);
                            //                            Train.InnitParentSelect(Train.PostParams["parent_cate"]);
                            location.replace(location.href);
                            break;
                        case "edit":
                            alert("修改成功！");
                            //                            setTimeout(ClearMessage, 2000);
                            //                            Train.Init(Train.initparentid, Train.initcate);
                            //                            Train.InnitParentSelect(Train.PostParams["parent_cate"]);
                            location.replace(location.href);
                            break;
                        default:
                            break;
                    }
                } else {
                    var type = Train.PostParams["act"];
                    switch (type) {
                        case "add":
                            $("#Message").empty().html("操作失败，该分类名已存在！");
                            setTimeout(ClearMessage, 2000);
                            break;
                        default:
                            break;
                    }
                }
            },
            "json"
        );
    },
    OtherPostCommand: function(param) {
        var type = param["act"];
        $.post("../Handler/TrainEdit.ashx",
            param,
            function(data) {
                if (data) {
                    switch (type) {
                        case "choose_article_init":
                            Train.InitCategoryArticle(data, "choose_article_list_id");
                            break;
                        case "term_init":
                            if (data["successCode"] == "1") {
                                var Term_list = data["TermList"].split(',');
                                var weight_list = data["ValuesList"].split(',');
                                var content = [];
                                content.push("<ul>");
                                for (var i = 0; i < Term_list.length; i++) {
                                    content.push("<li style=\"float:left; margin-left:5px; width:160px;\">");
                                    content.push("<input type=\"checkbox\" />");
                                    content.push("<b>" + Term_list[i] + "：" + weight_list[i] + "</b></li>");
                                }
                                content.push("</ul>");
                                $("#original_term").empty().html(content.join(""));
                            }
                            else if (data["successCode"] == "0") {
                                $("#original_term").empty().html("暂时没有Term");
                            }
                            break;
                        case "weight_init":
                            if (data["successCode"] != "0") {
                                for (var item in data) {
                                    $("#afresh_term").before("<li style=\"float:left; margin-left:5px; width:160px;\" id=\"a_term_" + Train.term_num + "\"><span>" + unescape(item) + "</span>—<b>" + data[item] + "</b><a href=\"#g\" style=\"color:red;\" name=\"term_del\">删除</a></li>");
                                    Train.term_num++;
                                }
                                Train.initEdit();
                                $("a[name=term_del]").each(function() {
                                    $(this).click(function() {
                                        $(this).parent("li").remove();
                                    });
                                });
                            }
                            break;
                        default:
                            break;
                    }
                }
            },
             "json"
        );
    },
    InitCategoryArticle: function(data, divId) {
        Train.articleid = 1;
        var content = [];
        for (var item in data) {
            content.push("<div style=\"display:none;\" class=\"article_list\" id=\"article_list_" + Train.articleid + "\"><ul>");
            content.push("<li class=\"article_list_num\">&nbsp;</li>");
            content.push("<li class=\"article_list_href\"><a href=\"" + data[item] + "\" target=\"_blank\" name=\"article_list_href\">" + data[item] + "</a></li>");
            content.push("<li class=\"article_list_del\"><a href=\"#g\" name=\"article_list_del\" pid=\"" + Train.articleid + "\">删除</a></li>");
            content.push("</ul></div>");
            Train.articleid++;
        }
        $("#" + divId).siblings().each(function(n) {
            if (n > 0)
                $(this).remove();
        });
        $("#" + divId).before(content.join(""));
        $("a[name=article_list_del]").each(function() {
            $(this).click(function() {
                $("#article_list_" + $(this).attr("pid")).remove();
            });
        });
    },
    InitCategoryTermWeight: function(data, divId) {

    },
    InitCategoryTrainCodition: function(data) {
        var category_name = data.CategoryName;
        if (category_name) {
            $("#CategoryName").val(unescape(category_name));
        } else {
            $("#CategoryName").val("");
        }
        var cate_train_info = data.CateTrainInfo;
        if (cate_train_info) {
            $("#cate_train_info").val(unescape(cate_train_info));
        } else {
            $("#cate_train_info").val("");
        }
        var cate_path = data.CatePath;
        if (cate_path) {
            $("#CatePath").val(unescape(cate_path));
        } else {
            $("#CatePath").val("Relevance");
        }
        var parent_cate = data.ParentCate;
        if (parent_cate) {
            $("#ParentCate").val(unescape(parent_cate));
        } else {
            $("#CatePath").val("0");
        }
        var is_effect = data.IsEffect;
        if (is_effect) {
            $("#IsEffect").val(unescape(is_effect));
        } else {
            $("#IsEffect").val("1");
        }
        var query_type = data.QueryType;
        if (query_type) {
            $("#QueryType").val(unescape(query_type));
        } else {
            $("#QueryType").val("commonquery");
        }
        var keyword = data.Keyword;
        if (keyword) {
            $("#Keyword").val(unescape(keyword));
        } else {
            $("#Keyword").val("");
        }
        var min_score = data.MinScore;
        if (min_score) {
            $("#MinScore").val(unescape(min_score));
        } else {
            $("#MinScore").val("10");
        }
        var event_reson = data.EventReson;
        if (event_reson) {
            $("#EventReson").val(unescape(event_reson));
        } else {
            $("#EventReson").val("");
        }
        var event_measure = data.EventMeasure;
        if (event_measure) {
            $("#EventMeasure").val(unescape(event_measure));
        } else {
            $("#EventMeasure").val("");
        }
        var event_about = data.EventAbout;
        if (event_about) {
            $("#EventAbout").val(unescape(event_about));
        } else {
            $("#EventAbout").val("");
        }
        var event_minscore = data.EventMinScore;
        if (event_minscore) {
            $("#EventMinScore").val(unescape(event_minscore));
        } else {
            $("#EventMinScore").val("10");
        }
        var event_date = data.EventDate;
        if (event_date) {
            $("#EventDate").val(unescape(event_date));
        } else {
            $("#EventDate").val("");
        }
        var event_type = data.EventType;
        if (event_type) {
            $("#EventType").val(unescape(event_type));
        } else {
            $("#EventType").val("0");
        }
        var event_sort = data.EventSort;
        if (event_sort) {
            $("#EventSort").val(unescape(event_sort));
        } else {
            $("#EventSort").val("0");
        }
        var img_path = data.ImgPath;
        if (img_path) {
            $("#ImgPath").val(unescape(img_path));
        } else {
            $("#ImgPath").val("");
        }
        var is_new = data.IsNew;
        if (is_new) {
            $("#IsNew").val(unescape(is_new));
        } else {
            $("#IsNew").val("0");
        }
        if (data["ParentCate"] == Config.ThemeParentCate) {
            $("tr[name='event_div']").show();
        } else {
            $("tr[name='event_div']").hide();
        }
        $("#trainingArticle").hide();
        $("#article_trainning").removeAttr("checked");
    },
    CheckAndInitPostParams: function() {
        var category_name = $.trim($("#CategoryName").val());
        if (category_name) {
            Train.PostParams["category_name"] = escape(category_name);
        } else {
            alert("请输入分类名称！");
            return false;
        }
        Train.PostParams["cate_path"] = escape($("#CatePath").val());
        Train.PostParams["cate_train_info"] = escape($.trim($("#cate_train_info").val()));
        Train.PostParams["parent_cate"] = $("#ParentCate").val();
        Train.PostParams["weight_values"] = escape(Train.GetTrainArticleList());
        Train.PostParams["is_effect"] = $("#IsEffect").val();
        Train.PostParams["keyword"] = escape($("#Keyword").val());
        Train.PostParams["minscore"] = $("#MinScore").val();
        Train.PostParams["event_reson"] = escape($("#EventReson").val());
        Train.PostParams["event_measure"] = escape($("#EventMeasure").val());
        Train.PostParams["event_about"] = escape($("#EventAbout").val());
        Train.PostParams["event_minscore"] = $("#EventMinScore").val();
        Train.PostParams["query_type"] = $("#QueryType").val();
        Train.PostParams["event_date"] = $("#EventDate").val();
        Train.PostParams["event_type"] = $("#EventType").val();
        Train.PostParams["img_path"] = escape($("#ImgPath").val());
        var event_sort = $.trim($("#EventSort").val());
        if (event_sort) {
            Train.PostParams["event_sort"] = event_sort;
        } else {
            Train.PostParams["event_sort"] = "0";
        }
        Train.PostParams["is_new"] = $("#IsNew").val();
        return true;
    },
    GetTrainArticleList: function() {
        var article_url_list = [];
        $("a[name=article_list_href]").each(function() {
            article_url_list.push($(this).attr("href"));
        });
        return article_url_list.join("+");
    },
    initEdit: function() {
        $("#train_term").find("span").each(function() {
            $(this).click(function() {
                Train.LableEdit(this);
            });
        });
        $("#train_term").find("b").each(function() {
            $(this).click(function() {
                Train.LableEdit(this);
            });
        });
    },
    LableEdit: function(obj) {
        var input = $(obj).children(":text")[0];
        if (input == null) {
            var l_value = $(obj).html();
            var a_input = document.createElement("INPUT");
            $(a_input).attr("size", "5");
            $(a_input).attr("value", l_value);
            $(obj).empty().append(a_input);
            $(a_input).focus();
            $(a_input).blur(function() {
                $(obj).empty().html($.trim($(a_input).val()));
            });
        }
    }
}