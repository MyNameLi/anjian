$(document).ready(function() {
    NodeMessage.Init();
});

var _nodemessage = new Object;
var NodeMessage = _nodemessage.property = {
    Init: function() {
        NodeMessage.InitClickFn();
    },
    InitClickFn: function() {
        $("#all_data_list").find("a[name='BtnInfoLook']").click(function() {
            var id = $(this).attr("pid");
            if (!id) {
                return;
            }
            $.post(location.href,
                { "act": "lookinfo", "ajaxString": 1, "idList": id },
                function(data) {
                    if (data.Success == 1) {
                        delete data.Success;
                        for (var item in data) {
                            $("#" + item).empty().html(unescape(data[item]));
                        }
                        listTable.ShowFrame("move_column", "column_edit_frame", "close_edit_frame", false);
                    }
                },
                "json"
            )
        });
    }
}