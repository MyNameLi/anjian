$(document).ready(function() {
    Warning.Innit();
});

var Warning = new Object;
Warning.PostUrl = location.href;
Warning.Innit = function() {
    $("#add_new_warning").click(function() {
        Warning.InnitSiteList();
    });
    $("#add_web_site").click(function() {
        var list = [];
        $("#web_site_list").find("input[name='item_web_site']:checked").each(function() {
            var val = $(this).val();
            list.push(val);
        });
        if (list.length == 0) {
            alert("请选择站点");
            return;
        }
        $("#add_web_site").next("span").empty().html("正在添加");
        Warning.AddWarning(list);
    });
    $("#all_data_list").find("a[name='set_accpters']").click(function() {
        var id = $(this).attr("pid");
        Warning.InnitAccept(id);
    });

    $("#btn_accept_list").click(function() {
        var id = $(this).attr("pid");
        Warning.AddAccept(id);
    });

    $("#btn_save_accpet").click(function() {
        $("#other_accept_list").find("input:checked").each(function() {
            $(this).parent("li").remove();
            var username = $(this).siblings("span").html();
            var userid = $(this).val();
            var addcontent = [];
            addcontent.push("<li style=\"float: left; margin-left: 10px; width: 200px;\">");
            addcontent.push("<span name=\"accept_user_list\" pid=\"" + userid + "\">" + username + "</span>");
            addcontent.push("</li>");
            var len = $("#belong_accept_list").find("input").length;
            if (len == 0) {
                $("#belong_accept_list").empty().append(addcontent.join(""));
            } else {
                $("#belong_accept_list").append(addcontent.join(""));
            }
        });
        Warning.InnitAcceptClick();
    });
}
Warning.InnitSiteList = function() {
    $.post(Warning.PostUrl,
        { "act": "innitwebsite", "ajaxString": 1 },
        function(data) {
            if (data) {
                if (data.Success == 1) {
                    if (data["sitecount"] == 0) {
                        $("#web_site_list").empty().html("<center>您已经监测了所有网站</center>");
                    } else {
                        delete data["Success"];
                        var content = [];
                        content.push("<ul>");
                        for (var item in data) {
                            content.push("<li style=\"float: left; margin-left: 10px; width: 200px;\">");
                            content.push("<input type=\"checkbox\" value=\"" + item + "\" ");
                            content.push("name=\"item_web_site\">" + unescape(data[item]) + "</li>");
                        }
                        content.push("</ul>");
                        $("#web_site_list").empty().html(content.join(""));
                    }
                }
            }
        },
        "json"
    );
}

Warning.AddWarning = function(websitelist) {
    $.post(Warning.PostUrl,
        { "act": "addwarning", "ajaxString": 1, "websitelist": websitelist.join(",") },
        function(data) {
            if (data) {
                if (data.Success == 1) {
                    //$("#add_web_site").next("span").empty().html("添加成功");
                    alert("添加成功");
                    location.replace(location.href);
//                    for (var i = 0, j = websitelist.length; i < j; i++) {
//                        $("#web_site_list").find("input[name='item_web_site'][value='" + websitelist[i] + "']").parent("li").remove();
//                    }
//                    Warning.ClearBackMsg("add_web_site");

                }
            }
        },
        "json"
    );
}

Warning.InnitAccept = function(id) {
    $.post(Warning.PostUrl,
        { "act": "innitacceptlist", "ajaxString": 1, "idlist": id },
        function(data) {            
            if (data) {
                if (data.Success == 1) {
                    var belonglist = data["belonglist"];
                    var otherlist = data["otherlist"];
                    delete belonglist["Success"];
                    $("#belong_accept_list").empty().html("<center>暂时没有用户</center>");
                    var belongcontent = [];
                    for (var item1 in belonglist) {
                        belongcontent.push("<li style=\"float: left; margin-left: 10px; width: 200px;\">");
                        belongcontent.push("<span name=\"accept_user_list\" pid=\"" + item1 + "\">" + unescape(belonglist[item1]) + "</span></li>");
                    }
                    if (belongcontent.length > 0) {
                        $("#belong_accept_list").empty().html(belongcontent.join(""));
                    }
                    Warning.InnitAcceptClick();
                    delete otherlist["Success"];
                    $("#other_accept_list").empty().html("<center>已经全部是接受用户</center>");
                    var othercontent = [];
                    for (var item2 in otherlist) {
                        othercontent.push("<li style=\"float: left; margin-left: 10px; width: 200px;\">");
                        othercontent.push("<input type=\"checkbox\" value=\"" + item2 + "\" ");
                        othercontent.push("><span>" + unescape(otherlist[item2]) + "</span></li>");
                    }
                    if (othercontent.length > 0) {
                        $("#other_accept_list").empty().html(othercontent.join(""));
                    }
                    $("#btn_accept_list").attr("pid", id);
                    listTable.ShowFrame("accept_list_move", "accept_list_frame", "accept_list_close");
                }
            }
        },
        "json"
    );
}

Warning.AddAccept = function(id) {
    var list = [];
    $("#belong_accept_list").find("span[name='accept_user_list']").each(function() {
        var val = $(this).attr("pid");
        list.push(val);
    });
    if (list.length == 0) {
        alert("请选择用户！");
        return;
    }
    $("#btn_accept_list").next("span").empty().html("正在添加");
    $.post(Warning.PostUrl,
        { "act": "addaccept", "ajaxString": 1, "idlist": id, "accpetlist": list.join(",") },
        function(data) {
            if (data) {
                if (data.Success == 1) {

                    //                    $("#btn_accept_list").next("span").empty().html("添加成功");                    
                    //                    var len = $("#belong_accept_list").find("input").length; 
                    //                    Warning.InnitAcceptClick();
                    //                    Warning.ClearBackMsg("btn_accept_list");
                    alert("添加成功");
                    location.replace(location.href);
                }
            }
        },
        "json"
    );
}

Warning.InnitAcceptClick = function() {
    $("#belong_accept_list").find("li").each(function() {
        var len = $(this).find("a[name='delete_accept']").length;
        if (len == 0) {
            $(this).append("<span><a style=\"display:none;\" name=\"delete_accept\" href=\"javascript:void(null);\">删除</a></span>");
        }
    })
    $("#belong_accept_list").find("li").hover(
        function() {
            $(this).find("a[name='delete_accept']").show();
        },
        function() {
            $(this).find("a[name='delete_accept']").hide();
        }
    );

    $("#belong_accept_list").find("a[name='delete_accept']").click(function() {
        $(this).parent("span").parent("li").remove();
        var username = $(this).parent("span").siblings("span").html();
        var userid = $(this).parent("span").siblings("span").attr("pid");
        var addcontent = [];
        addcontent.push("<li style=\"float: left; margin-left: 10px; width: 200px;\">");
        addcontent.push("<input type=\"checkbox\" value=\"" + userid + "\" ");
        addcontent.push("><span>" + username + "</span></li>");
        var len = $("#other_accept_list").find("input").length;
        if (len == 0) {
            $("#other_accept_list").empty().append(addcontent.join(""));
        } else {
            $("#other_accept_list").append(addcontent.join(""));
        }
    });

}

Warning.ClearBackMsg = function(obj) {
    function clear() {
        $("#" + obj).next("span").empty();
    }
    setTimeout(clear, 1000);
}
