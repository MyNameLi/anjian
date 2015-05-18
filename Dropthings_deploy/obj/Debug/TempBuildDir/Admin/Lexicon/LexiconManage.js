
$(document).ready(function() {
    LexiconManage.Init();
    window.MyAppView = new AppView();
});

var _lexiconmanage = new Object;
var LexiconManage = _lexiconmanage.property = {
    addtermcount: 0,
    Init: function() {
        Tree.initForm();
    },
    InitTermList: function(data) {/*绑定数据*/
        //        delete data.SuccessCode;
        //        $("#term_list").find("li[name='no_info_li']").remove();
        //        var content = [];
        //        for (var item in data) {
        //            var entity = data[item];
        //            content.push("<li style=\"margin:0px;\">");
        //            content.push("<span style=\"margin:0px;\" class=\"word_reg\" pid=\"" + entity["id"] + "\" name=\"word_reg\">");
        //            content.push(unescape(entity["word"]) + "</span>");
        //            content.push("<span style=\"margin:0px;\" class=\"word_weight\" pid=\"" + entity["id"] + "\" name=\"word_weight\">");
        //            content.push(entity["weight"] + "</span>");
        //            content.push("<span style=\"margin:0px;\" class=\"word_btn\">");
        //            content.push("<a href=\"javascript:void(null);\" id=\"btn_delete_" + entity["id"] + "\" ");
        //            content.push("><img src=\"../images/Lexicon_delete.gif\" border=\"0\" />删除</a></span>");
        //            content.push("</li>");
        //            $("#term_list").empty().html(content.join(""));
        //            LexiconManage.InitTermClickFn();
        //        }

    },
    InitClickFn: function() {
        //        $("#add_term").click(function() {
        //            var type_id = $("#current_type").attr("pid");
        //            if (!type_id) {
        //                alert("请选择类别");
        //                return;
        //            }
        //            $("#term_list").find("li[name='no_info_li']").remove();
        //            var content = [];
        //            content.push("<li style=\"margin:0px;\">");
        //            content.push("<span style=\"margin:0px;\" class=\"word_reg\" name=\"word_reg\">");
        //            content.push("<textarea id=\"word_reg_" + LexiconManage.addtermcount + "\" ");
        //            content.push("></textarea></span>");

        //            content.push("<span style=\"margin:0px;\" class=\"word_weight\" name=\"word_weight\">");
        //            content.push("<input type=\"text\" id=\"word_weight_" + LexiconManage.addtermcount + "\" ");
        //            content.push("/></span>");

        //            content.push("<span style=\"margin:0px;\" class=\"word_btn\">");
        //            content.push("<a href=\"javascript:void(null);\" id=\"btn_addterm\" ");
        //            content.push(">确定</a>　");
        //            content.push("<a href=\"javascript:void(null);\" id=\"btn_drop\" ");
        //            content.push(">取消</a></span>");

        //            content.push("</li>");
        //            $("#term_list").append(content.join(""));
        //            $("#word_reg_" + LexiconManage.addtermcount).focus();
        //            $("#btn_addterm_" + LexiconManage.addtermcount).click(function() {
        //                var account = $(this).attr("id").split("_")[2];
        //                var obj = $(this).parent("span").parent("li");
        //                LexiconManage.InsertLexicon(account, type_id, obj);
        //            });

        //            $("#btn_drop_" + LexiconManage.addtermcount).click(function() {
        //                $(this).parent("span").parent("li").remove();
        //            });

        //            LexiconManage.addtermcount++;

        //        });
    }
}







//左边树结构
var _tree = new Object;
var Tree = _tree.property = {
    zTree1: null,
    CurrentTreeNode: null,
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
    if (allnodes) {
        for (var item in allnodes) {
            Tree.zTree1.expandNode(allnodes[item]);
        }
    }
}


/*加载右边List*/
function zTreeOnDblclick(event, treeId, treeNode) {
    if (treeNode == null) {
        alert(' -- zTree -- ');
    } else {
        Tree.CurrentTreeNode = treeNode;
        var id = treeNode.id;
        var name = treeNode.name;
        $("#current_type").empty().html("&gt;&gt;" + name);
        $("#current_type").attr("pid", id);
        var url = location.href + "?" + "act=" + "getlexiconwordlist" + "&ajaxString=" + "1" + "&idList=" + id;
        _Models.fetch({ url: url,
            success: function(collection, response) {
                // alert("Ok");
            },
            error: function() {
                alert('error');
            }
        });
    }
}


/*Model*/
var _Model = Backbone.Model.extend({
    validate: function(attrs) {
        for (var key in attrs) {
            if (attrs[key] == '') {
                return key + " 不能为空";
            }
        }
    }
});

/*集合*/
var _ModelList = Backbone.Collection.extend({
    model: _Model
});
var _Models = new _ModelList();
/*删除模板视图*/
var _TempView = Backbone.View.extend({
    initialize: function() {
        //删除事件
        this.model.bind('destroy', this.remove, this);
    },
    template: _.template("<li><span style=\"margin: 0px;\" class=\"word_reg\"  name=\"word\" cate=\"1\"><%=word %></span>" +
                        "<span style=\"margin: 0px;\" class=\"word_weight\" name=\"weight\" cate=\"1\"><%=weight %></span>" +
                        "<span style=\"margin:0px;\" class=\"word_btn\">" +
                        "<a id=\"btn_delete\" href=\"javascript:void(null);\" cate=\"1\" >" +
                        "<img border=\"0\" src=\"../images/Lexicon_delete.gif\">删除</a></span></li>"),
    events: {
        "click #btn_delete": "clear", /*删除数据操作*/
        "click li .word_reg,li .word_weight": "edit", /*修改数据操作*/
        "blur textarea,input": "close"/*保存修改数据操作*/
    },
    render: function() {
        $(this.el).html(this.template(this.model.toJSON()));
        return this;
    },
    clear: function() {
        if (confirm("确定删除吗!")) {
            var url = location.href + "?act=" + "deletelexcionword" + "&ajaxString=" + "1" + "&idList=" + this.model.id;
            this.model.destroy({ url: url });
        }
    }, remove: function() {
        $(this.el).remove();
    },
    edit: function(e) {
        var txtlength = $(e.currentTarget).find("textarea").length;
        if (txtlength > 0) {
            return;
        }
        var tempTxt = $("<textarea></textarea>");
        tempTxt.val(this.model.get($(e.currentTarget).attr("name")));
        $(e.currentTarget).empty();
        $(e.currentTarget).append(($(tempTxt)));
        $(e.currentTarget).find("textarea").focus();

    },
    close: function(e) {
        var tempVal = $(e.currentTarget).val();
        var propName = $(e.currentTarget).parent().attr("name");
        $(e.currentTarget).parent().html(tempVal);
        var obj = {};
        obj[propName] = tempVal;
        //如果数据未做任何修改不执行保存
        if (tempVal == this.model.get(propName)) {
            return;
        }
        if (this.model.set(obj)) {
            var url = location.href + "?act=updateword" + propName + "&ajaxString=1&idList=" + this.model.get("id") + "&wordval=" + tempVal;
            this.model.save(obj, { url: url, success: function() {
            //保存成功
            }, error: function() {
                alert("保存失败");
            }
            });
        }
    }
});
/*添加新数据模版视图*/
var _AddTempView = Backbone.View.extend({
    initialize: function() {
    },
    template: _.template("<li style=\"margin:0px;\">" +
                        "<span style=\"margin:0px;\" class=\"word_reg\">" +
                        "<textarea name=\"word\"  ></textarea></span>" +
                        "<span style=\"margin:0px;\" class=\"word_weight\" >" +
                        "<input type=\"text\" name=\"weight\" /></span>" +
                        "<span style=\"margin:0px;\" class=\"word_btn\">" +
                        "<a href=\"javascript:void(null);\" id=\"btn_addterm\" >确定</a>　" +
                        "<a href=\"javascript:void(null);\" id=\"btn_drop\" >取消</a></span>" +
                        "</li>"
    ),
    events: {
        "click #btn_addterm": "addmodel",
        "click #btn_drop": "remove"
    },
    render: function() {
        $(this.el).html(this.template({}));
        return this;
    },
    addmodel: function() {
        var model = new _Model;
        var attr = {};
        attr["typeid"] = $("#current_type").attr("pid");
        attr["id"] = "0";
        $(this.el).find("textarea,input").each(function(i) {
            var input = $(this);
            attr[input.attr('name')] = input.val();
        });
        model.bind('error', function(model, error) {
            alert(error);
        });
        if (model.set(attr)) {
            //保存Model
            var url = location.href + "?act=insertlexcion&ajaxString=1" + "&typeid=" + model.get("typeid") + "&weight=" + model.get("weight") + "&wordreg=" + escape(model.get("word"));
            _Models.create(model, { url: url, success: function() {
                var url = location.href + "?" + "act=" + "getlexiconwordlist" + "&ajaxString=" + "1" + "&idList=" + $("#current_type").attr("pid");
                _Models.fetch({ url: url });
            }, error: function() {
                alert("操作错误")
            }
            });
        }
    },
    remove: function() {
        $(this.el).remove();
    }
});

/*页面绑定视图*/
var AppView = Backbone.Collection.extend({
    el: "#body_div",
    events: {
        "click #add_term": "createOnEnter" /*绑定页面添加按钮事件*/
    },
    initialize: function() {
        /*绑定集合add操作*/
        _Models.bind("add", this.addOne, this);
        /*调用fetch的时候触发reset*/
        _Models.bind("reset", this.addAll, this);
    },
    createOnEnter: function() {
        var _addTempView = new _AddTempView();
        $("#term_list").append(_addTempView.render().el);
    },
    addOne: function(model) {
        var _tempView = new _TempView({ model: model });
        $("#term_list").append(_tempView.render().el);
    },
    addAll: function() {
        $("#term_list").empty();
        _Models.each(this.addOne);
    }
});
