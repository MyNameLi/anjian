$(document).ready(function() {
    RoleManage.InnitWidGetList();
    RoleManage.InnitMenuList();
    RoleManage.Innit();
    RoleManage.InnitBtnFn();
    RoleManage.initialTabList();
});

var RoleManage = new Object;
RoleManage.InnitWidGetList = function() {
    $.post(location.Href,
        { "act": "innitwidgetlist", "ajaxString": 1 },
        function(data) {
            if (data) {
                if (data.Error == "1") {
                    alert("加载失败！");
                }
                if (data.Success == "1") {
                    delete data["Success"];
                    var content = [];
                    content.push("<ul>");
                    for (var item in data) {
                        content.push("<li style=\"float:left; margin-left:10px; width:200px;\">");
                        content.push("<input type=\"checkbox\" name=\"all_widget_list\" value=\"" + item + "\" />" + unescape(data[item]));
                        content.push("</li>");
                    }
                    content.push("</ul>");
                    $("#widget_list").empty().html(content.join(""));
                }
            } else {
                $("#widget_list").empty().html("暂时没有用户！");
            }
        },
        "json"
    );
}
RoleManage.initialTabList = function() {
    $.post(location.Href,
        { "act": "initialroletab", "ajaxString": 1 },
        function(data) {
            if (data) {
                if (data.Error == "1") {
                    alert("加载失败！");
                }
                if (data.Success == "1") {
                    delete data["Success"];
                    var content = [];
                    content.push("<ul>");
                    for (var item in data) {
                        content.push("<li style=\"float:left; margin-left:10px; width:200px;\">");
                        content.push("<input type=\"checkbox\" name=\"item_tab_list\" value=\"" + item + "\" />" + unescape(data[item]));
                        content.push("</li>");
                    }
                    content.push("</ul>");
                    $("#role_tab_list").empty().html(content.join(""));
                }
            } else {
                $("#role_tab_list").empty().html("暂时没有用户！");
            }
        },
        "json"
    );
}
RoleManage.InnitMenuList = function() {
    $.post(location.Href,
        { "act": "innitmenulist", "ajaxString": 1 },
        function(data) {
            if (data) {
                if (data.Error == "1") {
                    alert("加载失败！");
                }
                if (data.Success == "1") {
                    delete data["Success"];
                    $("#all_menu_list").empty().html(unescape(data["TreeStr"]));
                    $("#all_menu_list").css({ "line-height": "20px", "padding": "15px" });
                    RoleManage.InnitMenuListFn();
                }
            } else {
                $("#all_menu_list").empty().html("暂时没有频道！");
            }
        },
        "json"
    );
}

RoleManage.Innit = function() {
    $("#all_data_list").find("a[name='look_user']").click(function() {
        var roleid = $(this).attr("pid");
        $.post(location.Href,
	        { "act": "innituser", "ajaxString": 1, "RoleId": roleid },
	        function(data) {
	            if (data) {
	                if (data.Error == "1") {
	                    alert("初始化失败！");
	                }
	                if (data.Success == "1") {
	                    delete data["Success"];
	                    var content = [];
	                    content.push("<ul>");
	                    for (var item in data) {
	                        content.push("<li style=\"float:left; margin-left:10px; width:250px;\">");
	                        content.push("<span style=\"width:200px; margin-right:5px;\">" + unescape(data[item]) + "</span>");
	                        content.push("<a name=\"delete_role_user\" href=\"javascript:void(null);\" pid=\"" + roleid + "_" + item + "\">");
	                        content.push("删除</a>");
	                        content.push("</li>");
	                    }
	                    content.push("</ul>");
	                    $("#role_user_list").empty().html(content.join(""));
	                    RoleManage.InnitDeleteUserByRole();

	                }
	            } else {
	                $("#role_user_list").empty().html("暂时没有用户！");
	            }
	        },
	        "json"
        );
        RoleManage.ShowEditFrame("role_user_move", "role_user_frame", "role_user_close");
    });
    $("#all_data_list").find("a[name='role_tab']").click(function() {
        var roleid = $(this).attr("pid");
        $.post(location.Href,
	        { "act": "initeditroletab", "ajaxString": 1, "RoleId": roleid },
	        function(data) {
	            if (data.Success == "1") {
	                delete data["Success"];
	                $("#role_tab_list").find("input[name='item_tab_list']").removeAttr("checked");
	                for (var item in data) {
	                    $("#role_tab_list").find("input[name='item_tab_list'][value='" + item + "']").attr("checked", "checked");
	                }
	                $("#role_tab_list").attr("pid", roleid);
	                RoleManage.ShowEditFrame("role_tab_move", "role_tab_frame", "role_tab_close");
	            }
	        },
	        "json"
        );

    });
    $("#all_data_list").find("a[name='role_permission']").click(function() {
        var roleid = $(this).attr("pid");
        $.post(location.Href,
	        { "act": "innitrolewidgetlist", "ajaxString": 1, "RoleId": roleid },
	        function(data) {
	            if (data.Success == "1") {
	                delete data["Success"];
	                $("#widget_list").find("input[name='all_widget_list']").removeAttr("checked");
	                for (var item in data) {
	                    $("#widget_list").find("input[name='all_widget_list'][value='" + item + "']").attr("checked", "checked");
	                }
	                $("#edit_widget_list").attr("pid", roleid);
	            }
	        },
	        "json"
        );
        RoleManage.ShowEditFrame("role_permission_move", "role_permission_frame", "role_permission_close");
    });
    $("#all_data_list").find("a[name='role_menu_permission']").click(function() {
        var roleid = $(this).attr("pid");
        $.post(location.Href,
	        { "act": "innitrolemenulist", "ajaxString": 1, "RoleId": roleid },
	        function(data) {
	            if (data) {
	                if (data.Success == "1") {
	                    delete data["Success"];
	                    $("#all_menu_list").find("input[name='item_menu_list']").removeAttr("checked");
	                    for (var item in data) {
	                        $("#all_menu_list").find("input[name='item_menu_list'][value='" + item + "']").attr("checked", "checked");
	                    }
	                }
	            }
	            $("#edit_menu_list").attr("pid", roleid);
	        },
	        "json"
        );
        RoleManage.ShowEditFrame("role_menu_move", "role_menu_farme", "role_menu_close");
    });


}

RoleManage.ShowEditFrame = function(child_div, parent_div, close_btn) {
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
}

RoleManage.InnitDeleteUserByRole = function() {
    $("#role_user_list").find("a[name='delete_role_user']").click(function() {
        if (!confirm("您确定要从此角色中删除该用户么？")) {
            return;
        }
        var info = $(this).attr("pid").split('_');
        var roleid = info[0];
        var userid = info[1];
        var current = this;
        $.post(location.Href,
	        { "act": "deleteuserbyroleid", "ajaxString": 1, "RoleId": roleid, "userid": userid },
	        function(data) {
	            if (data.Error == "1") {
	                alert("初始化失败！");
	            }
	            if (data.Success == "1") {
	                $(current).parent("li").remove();
	            }

	        },
	        "json"
        );
    });
}

RoleManage.InnitBtnFn = function() {

    $("#edit_tab_list").click(function() {
        var roleid = $("#role_tab_list").attr("pid");
        var list = RoleManage.GetCheckBoxList("role_tab_list", "item_tab_list");
        var url = location.href;
        $(this).next("span").empty().html("模块正在分配当中...");
        $.post(url,
         { "act": "edittablist", "RoleId": roleid, "TabList": list, "ajaxString": 1 },
                function(data) {
                    if (data.Error == "1") {
                        alert("初始化失败！");
                    }
                    if (data.Success == "1") {
                        function clearbackmsg() {
                            $("#edit_tab_list").next("span").empty();
                        }
                        alert("分配成功");
                        location.replace(location.href);
                        //setTimeout(clearbackmsg, 1000);
                    }
                }, "json"
        );
    });
    $("#edit_widget_list").click(function() {
        var roleid = $(this).attr("pid");
        var list = RoleManage.GetCheckBoxList("widget_list", "all_widget_list");
        $(this).next("span").empty().html("模块正在分配当中...");
        $.post(location.Href,
	        { "act": "editwidgetlist", "ajaxString": 1, "RoleId": roleid, "widgetlist": list },
	        function(data) {
	            if (data.Error == "1") {
	                alert("初始化失败！");
	            }
	            if (data.Success == "1") {
	                function clearbackmsg() {
	                    $("#edit_widget_list").next("span").empty();
	                }
	                //$("#edit_widget_list").next("span").empty().html("分配成功");
	                alert("分配成功");
	                location.replace(location.href);
	                //setTimeout(clearbackmsg, 1000);
	            }
	        },
	        "json"
        );
    });
    $("#edit_menu_list").click(function() {
        var roleid = $(this).attr("pid");
        var list = RoleManage.GetCheckBoxList("all_menu_list", "item_menu_list");
        $(this).next("span").empty().html("模块正在分配当中...");
        $.post(location.Href,
	        { "act": "editmenulist", "ajaxString": 1, "RoleId": roleid, "menulist": list },
	        function(data) {
	            if (data.Error == "1") {
	                alert("初始化失败！");
	            }
	            if (data.Success == "1") {
	                function clearbackmsg() {
	                    $("#edit_menu_list").next("span").empty();
	                }
	                alert("分配成功");
	                location.replace(location.href);
	               // $("#edit_menu_list").next("span").empty().html("分配成功");
	               // setTimeout(clearbackmsg, 1000);
	            }
	        },
	        "json"
        );
    });
}

RoleManage.GetCheckBoxList = function(obj, name) {
    var list = [];
    $("#" + obj).find("input[name='" + name + "']:checked").each(function() {
        var val = $(this).val();
        list.push(val);
    });
    return list.join(",");
}

RoleManage.InnitMenuListFn = function() {
    $("#all_menu_list").find("input[name='item_menu_list']").click(function() {
        var id = $(this).val();
        var parentid = $(this).attr("pid");
        var tag = $(this).attr("checked");
        if (tag) {
            RoleManage.DisParent(parentid);
        } else {
            RoleManage.NoDisChild(id);
        }
    });
}

RoleManage.DisParent = function(parentid) {
    var obj = $("#all_menu_list").find("input[name='item_menu_list'][value='" + parentid + "']");
    if (obj.length > 0) {
        $(obj).attr("checked", "checked");
        var l_parentid = $(obj).attr("pid");
        RoleManage.DisParent(l_parentid);
    }
}

RoleManage.NoDisChild = function(id) {
    var obj = $("#all_menu_list").find("input[name='item_menu_list'][pid='" + id + "']");
    if (obj.length > 0) {
        $(obj).removeAttr("checked");
        $(obj).each(function() {
            var l_id = $(this).val();
            RoleManage.NoDisChild(l_id);
        });
    }
}