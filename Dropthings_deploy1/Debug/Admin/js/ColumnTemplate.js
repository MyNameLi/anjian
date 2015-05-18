$(document).ready(function() {
    ColumnTemplate.InnitTree();
    ColumnTemplate.innit();
});

var _columntemplate = new Object;
var ColumnTemplate = _columntemplate.property = {
    posturl: location.href,
    innit: function() {
        $("#close_edit_frame").click(function() {
            $("#column_edit_frame").hide();
        });
    },
    InnitTree: function() {
        $.post(ColumnTemplate.posturl,
            { "act": "innittree", "ajaxString": 1 },
            function(data) {
                if (data["Error"] == 1) {
                    alert("加载栏目数失败，原因" + unescape(data["ErrorStr"]));
                }
                if (data["Success"] == 1) {
                    $("#column_tree").empty().html(unescape(data["TreeStr"]));
                    ColumnTemplate.InnitTreeClickFn();
                }
            },
            "json"
        );
    },
    InnitTreeClickFn: function() {
        $("a[name='column_template_look']").click(function() {
            var columnid = $(this).attr("pid");
            $.post(ColumnTemplate.posturl,
                { "act": "innittemplatelist", "ajaxString": 1, "idlist": columnid },
                function(data) {
                    if (data["Error"] == 1) {
                        alert("加载模板列表失败，原因" + unescape(data["ErrorStr"]));
                    }
                    if (data["Success"] == 1) {
                        $("#column_template_frame").show();
                        delete data["Success"];
                        var content = [];
                        for (var item in data) {
                            var row = data[item];
                            content.push("<li>");
                            content.push("<span>" + unescape(row["TemplateName"]) + "</span>");
                            content.push("&nbsp;&nbsp;");
                            content.push("<a name=\"template_edit\" pid=\"" + row["ColumnID"] + "_");
                            content.push(row["TemplateID"] + "\"");
                            content.push(" href=\"javascript:void(null);\">编辑</a>&nbsp;&nbsp;");
                            content.push("<a name=\"template_delete\" pid=\"" + row["ColumnID"] + "_");
                            content.push(row["TemplateID"] + "\"");
                            content.push(" href=\"javascript:void(null);\">删除</a>");
                            content.push("</li>");
                        }
                        if (content.length > 0) {
                            $("#column_template_tree").empty().html(content.join(""));
                        } else {
                            $("#column_template_tree").empty().html("<li>暂时没有域</li>");
                        }
                        $("a[name='template_edit']").click(function() {
                            var info = $(this).attr("pid").split("_");
                            var id = info[0];
                            var TelemplateID = info[1];
                            $.post(ColumnTemplate.posturl,
		                        { "act": "innitedit", "ajaxString": 1, "idlist": id, "templateid": TelemplateID },
		                        function(data) {
		                            if (data.Error == "1") {
		                                alert("lost");
		                            }
		                            if (data.Success == "1") {
		                                $("#btn_add").hide();
		                                $("#btn_reset").hide();
		                                $("#btn_edit").show();
		                                $("#back_msg").empty();
		                                delete data["Success"];
		                                var l_data = data["entity_1"];
		                                for (var item in l_data) {
		                                    var value = unescape(l_data[item]);
		                                    $("#" + item).val(value);
		                                }
		                                ColumnTemplate.ShowEditFrame("move_column", "column_edit_frame");
		                            }
		                        },
		                        "json"
	                        );
                        });
                        $("a[name='template_delete']").click(function() {
                            var info = $(this).attr("pid").split("_");
                            var id = info[0];
                            var TelemplateID = info[1];
                            var current_obj = this;
                            $.post(ColumnTemplate.posturl,
		                        { "act": "delete", "ajaxString": 1, "idlist": id, "templateid": TelemplateID },
		                        function(data) {
		                            if (data.Error == "1") {
		                                alert("lost");
		                            }
		                            if (data.Success == "1") {
		                                $(current_obj).parent("li").remove();
		                            }
		                        },
		                        "json"
	                        );
                        });
                    }
                },
                "json"
            );
        });

        $("a[name='column_template_add']").click(function() {
            var columnid = $(this).attr("pid");
            listTable.Rest();
            $("#ColumnID").val(columnid);
            $("#btn_add").show();
            $("#btn_reset").show();
            $("#btn_edit").hide();
            ColumnTemplate.ShowEditFrame("move_column", "column_edit_frame");
        });
    },
    ShowEditFrame: function(child_div, parent_div) {
        $("#" + parent_div).show();
        var iframe_width = $(document).width();
        var l_width = parseInt($("#" + parent_div).width()) / 2;
        iframe_width = parseInt(iframe_width / 2) - l_width;
        //$("#" + parent_div).css({ "position": "absolute", "top": "50px", "left": iframe_width + "px", "background": "#f9f5f5" });
        $("#" + parent_div).css({ "position": "absolute", "top": "0px", "left": iframe_width + "px" });
        $("#" + parent_div).show();
        var div_move = new divMove(child_div, parent_div);
        div_move.init();
    }
}