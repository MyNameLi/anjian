$(document).ready(function(){
    LexiconManage.Init();
});

var _lexiconmanage = new Object;
var LexiconManage = _lexiconmanage.property = {
    addtermcount: 0,
    Init: function() {
        listTable.EditOneBackFn = function() {
            Tree.initForm();
            LexiconManage.InitTypeParent();
        }
        listTable.InitEditShowFrame = function() {
            listTable.ShowFrame("move_column", "column_edit_frame", "close_edit_frame", false);
        }
        Tree.initForm();
        LexiconManage.InitClickFn();
        LexiconManage.InitTypeParent();
    },
    InitClickFn: function() {
        $("#BtnAddType").click(function() {
            $("*[pid='valueList']").each(function() {
                $(this).val("");
            });
            $("#back_msg").empty();
            $("#btn_add").show();
            $("#btn_reset").show();
            $("#btn_edit").hide();
            listTable.ShowFrame("move_column", "column_edit_frame", "close_edit_frame", false);

        });
        $("#BtnEditType").click(function() {
            var tree_node = Tree.CurrentTreeNode;
            if (!tree_node) {
                alert("请选择类别");
                return;
            }
            var id = tree_node.id;
            listTable.initEdit(id, "ID", "back_msg", "initlexicontype", true);
        });

        $("#BtnDeleteType").click(function() {
            var tree_node = Tree.CurrentTreeNode;
            if (!tree_node) {
                alert("请选择类别");
                return;
            }
            var childNodes = tree_node.nodes;
            if (childNodes.length > 0) {
                alert("该类别下有子类别，不能删除！");
                return;
            }
            if (!confirm("该类别下的所有信息将被删除，您确定要删除么?")) {
                return;
            }
            var id = tree_node.id;
            $.post(location.href,
                { "act": "deletelexcionType", "ajaxString": 1, "idList": id
                },
                function(data) {
                    if (data) {
                        if (data.Success == 1) {
                            $("#term_list").empty();
                            Tree.initForm();
                            LexiconManage.InitTypeParent();
                        } else {
                            alert("删除失败！");
                        }
                    } else {
                        alert("删除失败！");
                    }
                },
                "json"
            );
        });


        $("#close_edit_frame").click(function() {
            Tree.initForm();
            LexiconManage.InitTypeParent();
        });

        $("#add_term").click(function() {
            var type_id = $("#current_type").attr("pid");
            if (!type_id) {
                alert("请选择类别");
                return;
            }
            $("#term_list").find("li[name='no_info_li']").remove();
            var content = [];
            content.push("<li style=\"margin:0px;\">");
            content.push("<span style=\"margin:0px;\" class=\"word_reg\" name=\"word_reg\">");
            content.push("<textarea id=\"word_reg_" + LexiconManage.addtermcount + "\" ");
            content.push("></textarea></span>");

            content.push("<span style=\"margin:0px;\" class=\"word_weight\" name=\"word_weight\">");
            content.push("<input type=\"text\" id=\"word_weight_" + LexiconManage.addtermcount + "\" ");
            content.push("/></span>");

            content.push("<span style=\"margin:0px;\" class=\"word_btn\">");
            content.push("<a href=\"javascript:void(null);\" id=\"btn_addterm_" + LexiconManage.addtermcount + "\" ");
            content.push(">确定</a>　");
            content.push("<a href=\"javascript:void(null);\" id=\"btn_drop_" + LexiconManage.addtermcount + "\" ");
            content.push(">取消</a></span>");

            content.push("</li>");
            $("#term_list").append(content.join(""));
            $("#word_reg_" + LexiconManage.addtermcount).focus();
            $("#btn_addterm_" + LexiconManage.addtermcount).click(function() {
                var account = $(this).attr("id").split("_")[2];
                var obj = $(this).parent("span").parent("li");
                LexiconManage.InsertLexicon(account, type_id, obj);
            });

            $("#btn_drop_" + LexiconManage.addtermcount).click(function() {
                $(this).parent("span").parent("li").remove();
            });

            LexiconManage.addtermcount++;

        });
    },
    InsertLexicon: function(account, type_id, obj) {
        var word = $.trim($("#word_reg_" + account).val());
        if (!word) {
            alert("请填写词库表达式");
            return;
        }
        var weight = $.trim($("#word_weight_" + account).val());
        if (!weight) {
            alert("请填写相关度");
            return;
        }
        $.post(location.href,
            { "act": "insertlexcion", "ajaxString": 1,
                "typeid": type_id, "weight": weight, "wordreg": escape(word)
            },
            function(data) {
                if (data) {
                    if (data.Success == 1) {
                        var id = data.id;
                        var content = [];
                        content.push("<span style=\"margin:0px;\" class=\"word_reg\" name=\"word_reg\">");
                        content.push(word + "</span>");
                        content.push("<span style=\"margin:0px;\" class=\"word_weight\" name=\"word_weight\">");
                        content.push(weight + "</span>");
                        content.push("<span style=\"margin:0px;\" class=\"word_btn\">");
                        content.push("<a href=\"javascript:void(null);\" id=\"btn_delete_" + id + "\" ");
                        content.push("><img src=\"../images/Lexicon_delete.gif\" border=\"0\" />删除</a></span>");
                        $(obj).empty().html(content.join(""));
                        LexiconManage.InitTermClickFn();
                    } else {
                        alert("添加失败");
                    }
                } else {
                    alert("添加失败");
                }
            },
            "json"
        );
    },
    InitTypeParent: function() {
        $.get(location.href,
            { "act": "getparentSelect", "ajaxString": 1 },
            function(data) {
                $("#TypeParentId").empty().html(data);
            }
        )
    },
    InitTermList: function(data) {
        delete data.SuccessCode;
        $("#term_list").find("li[name='no_info_li']").remove();
        var content = [];
        for (var item in data) {
            var entity = data[item];
            content.push("<li style=\"margin:0px;\">");
            content.push("<span style=\"margin:0px;\" class=\"word_reg\" pid=\"" + entity["id"] + "\" name=\"word_reg\">");
            content.push(unescape(entity["word"]) + "</span>");
            content.push("<span style=\"margin:0px;\" class=\"word_weight\" pid=\"" + entity["id"] + "\" name=\"word_weight\">");
            content.push(entity["weight"] + "</span>");
            content.push("<span style=\"margin:0px;\" class=\"word_btn\">");
            content.push("<a href=\"javascript:void(null);\" id=\"btn_delete_" + entity["id"] + "\" ");
            content.push("><img src=\"../images/Lexicon_delete.gif\" border=\"0\" />删除</a></span>");
            content.push("</li>");
            $("#term_list").empty().html(content.join(""));
            LexiconManage.InitTermClickFn();
        }
    },
    InitTermClickFn: function() {
        $("#term_list").find("a[id^='btn_delete_']").each(function() {
            var word_id = $(this).attr("id").split("_")[2];
            var cate = $(this).attr("cate");
            if (!cate || cate == "0") {
                $(this).click(function() {
                    if (!confirm("您确定要删除么")) {
                        return;
                    }
                    var li = $(this).parent("span").parent("li");
                    $.post(location.href,
                        { "act": "deletelexcionword", "ajaxString": 1,
                            "idList": word_id
                        },
                        function(data) {
                            if (data) {
                                if (data.Success == 1) {
                                    li.remove();
                                } else {
                                    alert("删除失败");
                                }
                            } else {
                                alert("删除失败");
                            }
                        },
                        "json"
                    );
                });
            }
            $(this).attr("cate", "1");
        });

        $("#term_list").find("span[class='word_reg']").each(function() {
            var word_id = $(this).attr("pid");
            var cate = $(this).attr("cate");
            if (!cate || cate == "0") {
                $(this).click(function() {
                    var current_span = this;
                    var textarea_len = $(this).find("textarea").length;
                    if (textarea_len > 0) {
                        return;
                    }
                    var base_val = $(this).html();
                    var textarea_obj = document.createElement("textarea");
                    $(textarea_obj).val(base_val);
                    $(current_span).empty().append($(textarea_obj));
                    $(textarea_obj).focus();
                    $(textarea_obj).blur(function() {
                        var post_val = $(this).val();
                        if (post_val == base_val) {
                            $(current_span).empty().html(post_val);
                            return;
                        }
                        $.post(location.href,
                            { "act": "updatewordreg", "ajaxString": 1,
                                "idList": word_id, "wordreg": escape(post_val)
                            },
                            function(data) {
                                if (data) {
                                    if (data.Success == 1) {
                                        $(current_span).empty().html(post_val);
                                    } else {
                                        alert("修改失败");
                                    }
                                } else {
                                    alert("修改失败");
                                }
                            },
                            "json"
                        );
                    });

                });
            }
            $(this).attr("cate", "1");
        });

        $("#term_list").find("span[class='word_weight']").each(function() {
            var word_id = $(this).attr("pid");
            var cate = $(this).attr("cate");
            if (!cate || cate == "0") {
                $(this).click(function() {
                    var current_span = this;
                    var input_len = $(this).find("input").length;
                    if (input_len > 0) {
                        return;
                    }
                    var base_val = $(this).html();
                    var input_obj = document.createElement("input");
                    $(input_obj).val(base_val);
                    $(current_span).empty().append($(input_obj));
                    $(input_obj).focus();
                    $(input_obj).blur(function() {
                        var post_val = $(this).val();
                        if (post_val == base_val) {
                            $(current_span).empty().html(post_val);
                            return;
                        }
                        $.post(location.href,
                            { "act": "updatewordweight", "ajaxString": 1,
                                "idList": word_id, "wordweight": post_val
                            },
                            function(data) {
                                if (data) {
                                    if (data.Success == 1) {
                                        $(current_span).empty().html(post_val);
                                    } else {
                                        alert("修改失败");
                                    }
                                } else {
                                    alert("修改失败");
                                }
                            },
                            "json"
                        );
                    });
                });
            }
            $(this).attr("cate", "1");
        });
    }
}


var _tree = new Object;
var Tree = _tree.property = {
    zTree1: null,
    CurrentTreeNode:null,    
    setting: {
        async: true,
        checkable: false,
        callback: {
            asyncSuccess: zTreeExpand,
            click: zTreeOnDblclick
        }
    },
    initForm: function() {
        this.CurrentTreeNode = null;
        this.refreshTree();
    },
    getCheckBoxType: function() {
        var py = "p";
        var sy = "s";
        var pn = "p";
        var sn = "s";

        var type = { "Y": py + sy, "N": pn + sn };
        return type;
    },
    refreshTree: function() {
        var checkType = Tree.getCheckBoxType();
        Tree.setting["checkType"] = checkType;
        Tree.setting["asyncUrl"] = Tree.getAsyncUrl();
        Tree.zTree1 = $("#lexicon_tree").zTree(Tree.setting);

        //getCheckedNodesLength();
    },
    getAsyncUrl: function() {
        var url = location.href;
        var l_index = url.indexOf("id");
        if (l_index > 0) {
            url = url + "&ajaxString=1&act=inittree";
        } else {
            url = url + "?ajaxString=1&act=inittree";
        }
        return url;
    }
}

function zTreeExpand() {
    var allnodes = Tree.zTree1.getNodes();
    if(allnodes){
        for (var item in allnodes) {
            Tree.zTree1.expandNode(allnodes[item]);
        }
    }
}

function zTreeOnDblclick(event, treeId, treeNode) {
    if (treeNode == null) {
        alert(' -- zTree -- ');
    } else {
        Tree.CurrentTreeNode = treeNode;
        /*var nodes = treeNode.nodes;        
        if(!nodes || nodes.length == 0) {
            
        }*/
        var id = treeNode.id;
        var name = treeNode.name;
        $("#current_type").empty().html("&gt;&gt;" + name);
        $("#current_type").attr("pid", id);
        $.post(location.href,
            { "act": "getlexiconwordlist", "ajaxString": 1, "idList": id },
            function(data) {
                if (data) {
                    if (data.SuccessCode == 1) {
                        LexiconManage.InitTermList(data);
                    } else {
                        $("#term_list").empty().html("<li style=\"width:98%; margin:0px;padding-left:1px;padding-right:1px; \" class=\"no_info_list\" name=\"no_info_li\">该类别下没有词库</li>");
                    }
                } else {
                $("#term_list").empty().html("<li style=\"margin:0px;width:98%; padding-left:1px;padding-right:1px; \" class=\"no_info_list\" name=\"no_info_li\">该类别下没有词库</li>");
                }
            },
            "json"
        )
    }
}