$(document).ready(function() {
    MenuManage.InnitTree();
    MenuManage.innitBtnFn();
});

var MenuManage = new Object;
MenuManage.posturl = location.href;
MenuManage.InnitTree = function() {
    $.post(MenuManage.posturl,
        { "act": "innittree", "ajaxString": 1 },
        function(data) {
            if (data["Error"] == 1) {
                alert("加载栏目数失败，原因" + unescape(data["ErrorStr"]));
            }
            if (data["Success"] == 1) {
                $("#column_tree").empty().html(unescape(data["TreeStr"]));
                MenuManage.InnitTreeClick();
            }
        },
        "json"
    );
}
MenuManage.InnitTreeClick = function() {
    $("#column_tree").find("a[name='menu_edit']").click(function() {
        var menu_id = $(this).attr("pid");
        $.post(MenuManage.posturl,
            { "act": "innitedit", "ajaxString": 1, "idList": menu_id },
            function(data) {
                if (data["Error"] == 1) {
                    alert("加载失败，原因" + unescape(data["ErrorStr"]));
                }
                if (data["Success"] == 1) {
                    delete data["Success"];
                    for (var item in data) {
                        $("#" + item).val(unescape(data[item]));
                    }
                    $("#btn_add").hide();
                    $("#btn_reset").hide();
                    $("#btn_edit").show();
                    var Linput = document.createElement("INPUT");
                    $(Linput).val(menu_id.toString());
                    $(Linput).attr("id", "ID");
                    $(Linput).attr("pid", "valueList");
                    $(Linput).css("display", "none");
                    $("#column_edit_frame").append($(Linput));
                    MenuManage.ShowEditFrame("move_column", "column_edit_frame", "close_edit_frame", true);
                }
            },
            "json"
        );
    });

    $("#column_tree").find("a[name='menu_delete']").click(function() {
        if (confirm("您确定要删除该频道么？")) {
            var id = $(this).attr("pid");
            var len = $("tr[name='column_parent_" + id + "']").length;
            if (len > 0) {
                alert("该栏目下有子栏目，不能删除！");
                return;
            }

            var current_obj = this;
            $.post(MenuManage.posturl,
                { "act": "remove", "ajaxString": 1, "idList": id },
                function(data) {
                    if (data["Error"] == 1) {
                        alert("删除失败，原因" + unescape(data["ErrorStr"]));
                    }
                    if (data["Success"] == 1) {
                        $("#column_list_" + id).remove();
                    }
                },
                "json"
            );
        }
    });
},
MenuManage.innitBtnFn = function() {
    $("#add_one").click(function() {
        $("#btn_add").show();
        $("#btn_reset").show();
        $("#btn_edit").hide();
        listTable.Rest();
        MenuManage.ShowEditFrame("move_column", "column_edit_frame", "close_edit_frame",true);
    });

}

MenuManage.ShowEditFrame = function(child_div, parent_div, close_btn, tag) {
    $("#" + parent_div).show();
    var iframe_width = $(document).width();
    var l_width = parseInt($("#" + parent_div).width()) / 2;
    iframe_width = parseInt(iframe_width / 2) - l_width;
    //$("#" + parent_div).css({ "position": "absolute", "top": "50px", "left": iframe_width + "px", "background": "#f9f5f5" });
    $("#" + parent_div).css({ "position": "absolute", "top": "50px", "left": iframe_width + "px" });
    $("#" + parent_div).show();
    $("#" + close_btn).click(function() {
        $("#" + parent_div).hide();
        if (tag) {
            location.replace(location.href);
        }
    });
    var div_move = new divMove(child_div, parent_div);
    div_move.init();
}