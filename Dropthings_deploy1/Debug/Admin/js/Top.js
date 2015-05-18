$(document).ready(function() {
    Top.Innit();
});
var _top = new Object;
var Top = _top.property = {
    MenuData: {},
    WebSiteFrame: null,
    Innit: function() {
        $.post("Handler/GetMenuList.ashx",
            { "type": "getstairmenuhtml" },
            function(data) {
                if (data) {
                    var username = data["username"];                    
                    $("#login_user_name").empty().html(username);
                    var menudata = data["menulist"];
                    delete menudata["SuccessCode"];
                    Top.MenuData = menudata;
                    Top.InnitStairMenu(menudata);
                }
            },
            "json"
        );
    },
    InnitStairMenu: function(data) {
        var content = [];
        var count = 1;
        for (var item in data) {
            content.push("<li ");
            if (count == 1) {
                content.push("class=\"menu_current\"");
                var row = data[item];
                Top.ClickEventFn(row["id"], row["righturl"]);
            }
            content.push("pid=\"" + item + "\"><a  ");
            content.push(" href=\"javascript:void(null);\" ><b>");
            content.push(unescape(item));
            content.push("</b></a></li>");
            count++;
        }
        $("#top_up_center").empty().html(content.join(""));
        var menu_list = $("#top_up_center").find("li");
        menu_list.click(function() {
            var key = $(this).attr("pid");
            var row = Top.MenuData[key];
            menu_list.removeClass("menu_current");
            $(this).addClass("menu_current");
            Top.ClickEventFn(row["id"], row["righturl"]);
        });
    },
    ClickEventFn: function(id, web_url) {
        if (id == 2) {
            parent.document.getElementById("mainFrame").src = "leftMenu/Left.html";
            parent.document.getElementById("tabFrame").src = "sitemanage/list.aspx";
        } else {
            if (web_url) {
                parent.document.getElementById("mainFrame").src = "leftMenu/Left.html";
                parent.document.getElementById("tabFrame").src = web_url;
            } else {
                parent.document.getElementById("mainFrame").src = "leftMenu/Left.html?parentid=" + id;
            }
        }
    },
    InnitChooseWebSite: function() {
        $.post("Handler/WebSiteManage.ashx",
            { "type": "InnitList" },
            function(data) {
                var div = Top.CreateDiv();
                parent.document.body.appendChild(div);
            },
            "json"
        )
    },
    CreateDiv: function() {
        var div = document.createElement("DIV");
        $(div).css({ "border": "1px solid red", "width": "300px", "height": "200px", "position": "absolute", "top": "0px", "z-index": "1000", "display": "block" });
        $(div).html("dsadsa");
        return div;
    }
}