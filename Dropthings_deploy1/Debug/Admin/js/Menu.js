$(document).ready(function() {
    Menu.InnitMenuHtml();
})

var _menu = new Object;
var Menu = _menu.property = {
    InnitMenuHtml: function() {
        var parent_id = $.query.get('parentid');
        if (parent_id) {
            $.get("../Handler/GetMenuList.ashx",
                { "type": "getitemmenuhtml", "parent_id": parent_id },
                function(data) {
                    $("#left_menu_down").empty().html(data);
                    Menu.InnitOriginalState();
                    Menu.InnitMenuClick();
                }
            );
        }
    },
    InnitOriginalState: function() {
        var url = $("#left_menu_down").find("li").first().children("a").attr("pid");
        parent.document.getElementById("tabFrame").src = url;
    },
    InnitMenuClick: function() {
        $(".fon").click(function() {
            $("#left_menu_down").find(".menu_child").hide();
            $(this).siblings(".menu_child").show(200);
        });
        $(".menu_child").find("a").click(function() {
            $(".menu_child").find("li").css({ "background": "#c4e2ff", "color": "#3a6580" });
            $(".menu_child").find("li a").css("color", "#042F59");
            $(this).css("color", "#ffffff");
            $(this).parent("li").css({ "background": "#3a7fb6", "color": "#ffffff" });
            var path = $(this).attr("pid");
            parent.document.getElementById("tabFrame").src = path;
        });
    }
}