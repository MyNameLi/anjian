// JavaScript Document
$(document).ready(function(){		
	Menu.innit();	
});

var _Menu = new Object;
var Menu = _Menu.prototype = {
    innit: function() {
        this.innitaddurl();
        this.innitaddcontent();
        this.innitAddItem();
        this.innitfieldactionchangeFn();
    },
    innitAddItem: function() {
        $("#add_item").click(function() {
            Display.filedstr["type"] = "add";
            $("#add_frame").empty();
            BaseMenu.innitMoveDiv();
            Menu.ShowEditFrame();
        });

        $("a[name='edit_task']").click(function() {
            var task_id = $(this).attr("pid");
            Display.filedstr["type"] = "update";
            Display.filedstr["task_id"] = task_id;
            $("#add_frame").empty();
            BaseMenu.innitMoveDiv();
            function show_add_frame() {
                BaseMenu.InnitTaskData("edit", task_id);
                Menu.ShowEditFrame();
            }
            setTimeout(show_add_frame, 1000);
        });

        $("a[name='create_task']").click(function() {
            var task_id = $(this).attr("pid");
            BaseMenu.InnitTaskData("createxml", task_id);
        });

        $("a[name='add_task']").click(function() {
            var task_id = $(this).attr("pid");
            BaseMenu.InnitTaskData("addtask", task_id);
        });

        $("a[name='start_task']").click(function() {
            var task_id = $(this).attr("pid");
            BaseMenu.InnitTaskData("starttask", task_id);
        });

        $("a[name='stop_task']").click(function() {
            if (!confirm("您确定要停止该任务么？")) {
                return;
            }
            var task_id = $(this).attr("pid");
            BaseMenu.InnitTaskData("stoptask", task_id);
        });

        $("a[name='delete_task']").click(function() {
            $("#add_frame").empty();
            var task_id = $(this).attr("pid");
            if (confirm("您确定要删除任务么？")) {
                $("#add_frame").hide();
                BaseMenu.InnitTaskData("delete", task_id);
            }
        });
    },
    innitaddurl: function() {
        $("#l_add_url_rule").live('click', function() {
            if (!$.trim($("#filter_rule").val())) {
                alert("请填写表达式");
                return;
            }
            var content = [];
            content.push("<tr>");
            content.push("<td pid=\"" + $("#filter_field").val() + "\">" + $("#filter_field option:selected").html() + "</td>");
            content.push("<td pid=\"" + $("#filter_action").val() + "\">" + $("#filter_action option:selected").html() + "</td>");
            content.push("<td>" + Menu.SetRegStr($("#filter_rule").val()) + "</td>");
            content.push("<td name=\"del_url_rule_tr\"><a href=\"javascript:void(null);\">删除</a></td>");
            content.push("</tr>");
            $("#url_rule_list").append(content.join(""));
            $("td[name='del_url_rule_tr']").click(function() {
                $(this).parent("tr").remove();
            });
        });
    },
    innitfieldactionchangeFn: function() {
        $("#field_action").live('click', function() {
            var val = $(this).val();
            if (val == "3") {
                $("#field_suffix_input").show();
            } else {
                $("#field_suffix_input").hide();
            }
        });
    },
    innitaddcontent: function() {
        $("#reset_content_rule").live('click', function() {
            $("#field_active_iframe").find(":text").val("");
            $("#FieldSource").val("htmlcontent");
        });

        $("#add_content_rule").live('click', function() {
            if (!$.trim($("#field_exp").val())) {
                alert("请填写表达式");
                return;
            }
            var content = [];
            content.push("<tr>");
            if ($.trim($("#l_field_name").val())) {
                content.push("<td name=\"field_str\" pid=\"" + $("#l_field_name").val() + "\">" + $("#l_field_name").val() + "</td>");
            } else {
                content.push("<td name=\"field_str\" pid=\"" + $("#field_str").val() + "\">" + $("#field_str option:selected").html() + "</td>");
            }
            content.push("<td name=\"is_intervar\" pid=\"" + $("#is_intervar").val() + "\">" + $("#is_intervar option:selected").html() + "</td>");
            content.push("<td name=\"is_date\" pid=\"" + $("#is_date").val() + "\">" + $("#is_date option:selected").html() + "</td>");
            content.push("<td name=\"field_action\" pid=\"" + $("#field_action").val() + "\">" + $("#field_action option:selected").html() + "</td>");
            content.push("<td name=\"is_remove_html\" pid=\"" + $("#is_remove_html").val() + "\">" + $("#is_remove_html option:selected").html() + "</td>");
            content.push("<td name=\"field_source\" pid=\"" + $("#field_source").val() + "\">" + $("#field_source").val() + "</td>");
            content.push("<td style=\"display:none;\" name=\"field_exp\" pid=\"" + $("#field_exp").val() + "\">" + $("#field_exp").val() + "</td>");
            content.push("<td style=\"display:none;\" name=\"field_suffix\" pid=\"" + $("#field_suffix").val() + "\">" + $("#field_suffix").val() + "</td>");
            content.push("<td style=\"display:none;\" name=\"field_param1\" pid=\"" + $("#field_param1").val() + "\">" + $("#field_param1").val() + "</td>");
            content.push("<td style=\"display:none;\" name=\"field_param2\" pid=\"" + $("#field_param2").val() + "\">" + $("#field_param2").val() + "</td>");
            content.push("<td style=\"display:none;\" name=\"field_param3\" pid=\"" + $("#field_param3").val() + "\">" + $("#field_param3").val() + "</td>");
            content.push("<td style=\"display:none;\" name=\"field_param4\" pid=\"" + $("#field_param4").val() + "\">" + $("#field_param4").val() + "</td>");
            content.push("<td name=\"del_content_rule_tr\"><a name=\"btn_edit_content\" href=\"javascript:void(null);\">编辑</a>&nbsp;&nbsp;");
            content.push("<a name=\"btn_delete_content\" href=\"javascript:void(null);\">删除</a></td>");
            content.push("</tr>");
            $("#content_rule_list").append(content.join(""));
            $("td a[name='btn_delete_content']").click(function() {
                $(this).parent("td").parent("tr").remove();
            });
            $("td a[name='btn_edit_content']").click(function() {
                $(this).parent("td").siblings("td").each(function() {
                    var key = $(this).attr("name");
                    var val = $(this).attr("pid");
                    $("#" + key).val(val);
                });
                $(this).parent("td").parent("tr").remove();
            });
        });
    },
    SetRegStr: function(str) {
        str = str.replace(new RegExp("<", 'gm'), "&lt;");
        str = str.replace(new RegExp(">", 'gm'), "&gt;");
        return str;
    },
    ShowEditFrame: function() {
        var iframe_width = $(document).width();
        var l_width = parseInt($("#column_edit_frame").width()) / 2;
        iframe_width = parseInt(iframe_width / 2) - l_width;
        $("#column_edit_frame").css({ "position": "absolute", "top": "50px", "left": iframe_width + "px", "background": "#f9f5f5" });
        $("#column_edit_frame").show();
        var div_move = new divMove("move_column", "column_edit_frame");
        div_move.init();
        $("#close_edit_frame").click(function() {
            $("#column_edit_frame").hide();
            location.replace(location.href);
        })
    }
}		