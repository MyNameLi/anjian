$(document).ready(function() {
    $("#afresh_term").hide();
    $("#look_article").toggle(
        function() {
            $(this).empty().html("隐藏文章列表");
            $("#all_article").show();
        },
        function() {
            $(this).empty().html("展开文章列表");
            $("#all_article").hide();
        }
    );
    Common.CategoryMenuClick = function(data, text) {
        var infoList = data.split("_");
        var category_id = infoList[0];
        $("#category_name").empty().html(text);
        KeyWordTrain.categoryId = category_id;
        KeyWordTrain.TermInit(category_id);
        KeyWordTrain.InitRetrainTerm(category_id);
    }
    var str_where = "ParentCate = 0 and IsEffect=1";
    Common.CategoryMenu("MenuList", "left-center-nav-ul-link", "left-center-nav-ul", str_where, "150", true);
    KeyWordTrain.InnitBtn();
});

var _keyWordTrain = new Object;
var KeyWordTrain = _keyWordTrain.property = {
    term_num: 0,
    categoryId: null,
    TermInit: function(category_id) {
        $.post("../Handler/TrainEdit.ashx",
            { "act": "term_init", "train_category_id": category_id },
            function(data, textStatus) {
                if (data["successCode"] == "1") {
                    var Term_list = data["TermList"].split(',');
                    var weight_list = data["ValuesList"].split(',');
                    var content = [];
                    for (var i = 0; i < Term_list.length; i++) {
                        content.push("<li><input type=\"checkbox\" />");
                        content.push("<b>" + Term_list[i] + "：" + weight_list[i] + "</b></li>");
                    }
                    $("#original_term").empty().html(content.join(""));
                }
                else if (data["successCode"] == "0") {
                    $("#original_term").empty().html("暂时没有Term");
                    $("#original_term").height(25);
                }
            },
            "json"
        );
    },
    InitRetrainTerm: function(category_id) {
        $.post("../Handler/TrainEdit.ashx",
            { "act": "weight_init", "train_category_id": category_id },
            function(data, textStatus) {
                $("#afresh_term").siblings("li[id^='a_term_']").remove();
                if (data) {
                    for (var item in data) {
                        $("#afresh_term").before("<li id=\"a_term_" + KeyWordTrain.term_num + "\"><span>" + unescape(item) + "</span>—<b>" + data[item] + "</b><a href=\"#g\" style=\"color:red;\" name=\"term_del\">删除</a></li>");
                        KeyWordTrain.term_num++;
                    }
                    $("#afresh_term").show();
                    KeyWordTrain.initEdit();
                    $("a[name=term_del]").each(function() {
                        $(this).click(function() {
                            $(this).parent("li").remove();
                        });
                    })
                } else {
                    $("#afresh_term").show();
                }

            },
            "json"
        );
    },
    InnitBtn: function() {
        $("#term_add").click(function() {
            $("#afresh_term").before("<li id=\"a_term_" + KeyWordTrain.term_num + "\"><span><input text=\"text\" id=\"term_value_" + KeyWordTrain.term_num + "\" size=\"3\"/></span>—<b><input id=\"weight_value_" + KeyWordTrain.term_num + "\" text=\"text\"  size=\"3\"/></b><a href=\"#g\" style=\"color:red;\" name=\"term_del\">删除</a></li>");

            $("#term_value_" + KeyWordTrain.term_num).blur(function() {
                if ($.trim($(this).val()) != "") {
                    $(this).parent("span").empty().html($(this).val());
                }
            });

            $("#weight_value_" + KeyWordTrain.term_num).blur(function() {
                if ($.trim($(this).val()) != "") {
                    $(this).parent("b").empty().html($(this).val());
                }
            });
            KeyWordTrain.term_num++;
            KeyWordTrain.initEdit();
            $("a[name=term_del]").each(function() {
                $(this).click(function() {
                    $(this).parent("li").remove();
                });
            })
        });

        $("#look_result").click(function() {
            if (KeyWordTrain.categoryId) {
                var init_data = { "page_size": 10, "status_bar_id": "PagerList", "result_id": "SearchResult" };
                var l_Pager = new Pager(init_data);
                var query_params = { "action": "categoryquery", "display_style": 2, "category": KeyWordTrain.categoryId, "totalresults": "true", "params": "Summary,Print,Sort,MinScore",
                    "values": "context,all,Relevance,10"
                };
                l_Pager.LoadData(1, query_params);
                $("#article_result").show();
                $("#look_close").click(function() {
                    $("#article_result").hide();
                });
            } else {
                alert("请选择分类！");
            }
        });

        $("#save_term").click(function() {
            $("#original_term").find(":checkbox[checked=true]").each(function(n) {
                var value = $(this).siblings("b").html().split('：');
                $("#afresh_term").before("<li id=\"a_term_" + KeyWordTrain.term_num + "\"><span>" + value[0] + "</span>—<b>" + value[1] + "</b><a href=\"#g\" style=\"color:red;\" name=\"term_del\">删除</a></li>");
                KeyWordTrain.term_num++;
            });
            $("#afresh_term").show();
            KeyWordTrain.initEdit();
            $("a[name=term_del]").each(function() {
                $(this).click(function() {
                    $(this).parent("li").remove();
                });
            })
        });

        $("#re_train").click(function() {
            if (KeyWordTrain.categoryId) {
                var Terms = new Array();
                var Weights = new Array();
                $("#afresh_term").siblings("li").each(function() {
                    Terms.push($(this).children("span").html());
                    Weights.push($(this).children("b").html());
                });
                $.post("../Handler/TrainEdit.ashx",
                    { "act": "weight_train", "train_category_id": KeyWordTrain.categoryId, "weight_terms": escape(Terms.join(",").toString()), "weight_values": Weights.join(",").toString() },
                    function(data, textStatus) {
                        alert(data);
                    }
                );
            }
        });
    },
    initEdit: function() {
        $("#train_term").find("span").each(function() {
            $(this).click(function() {
                KeyWordTrain.LableEdit(this);
            });
        });
        $("#train_term").find("b").each(function() {
            $(this).click(function() {
                KeyWordTrain.LableEdit(this);
            });
        });
    },
    LableEdit: function(obj) {
        var input = $(obj).children(":text")[0];
        if (input == null) {
            var l_value = $(obj).html();
            var a_input = document.createElement("INPUT");
            $(a_input).attr("size", "3");
            $(a_input).attr("value", l_value);
            $(obj).empty().append(a_input);
            $(a_input).focus();
            $(a_input).blur(function() {
                $(obj).empty().html($.trim($(a_input).val()));
            });
        }
    }
}