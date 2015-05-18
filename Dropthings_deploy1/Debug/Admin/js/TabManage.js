var _tabmanage = new Object;
var TabManage = _tabmanage.propety = {
    Remove: function(id, msg) {
        if (confirm(msg)) {
            var id = id;
            var current_obj = this;
            $.post(location.href,
                        { "act": "remove", "ajaxString": 1, "idList": id },
                        function(data) {
                            if (data["Error"] == 1) {
                                alert("删除失败，原因" + unescape(data["ErrorStr"]));
                            }
                            if (data["Success"] == 1) {
                                location.replace(location.href);
                            }
                        },
                        "json"
                    );
        }
    }
}