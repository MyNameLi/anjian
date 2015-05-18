$(document).ready(function() {
    System.Init();
});

var _system = new Object;
var System = _system.property = {
    Init: function() {
        var database_data = Config.DataBase;
        var database_list = System.GetDataBaseStr(database_data);
        $.post(location.href,
            { "act": "initstatus", "ajaxString": 1, "database_list": escape(database_list) },
            function(data) {
                if (data) {
                    var start_str = "<td height=\"26\" bgcolor=\"#FFFFFF\" align=\"center\">";
                    var end_str = "</td>";
                    delete data["Success"];
                    var content = [];
                    var count = 1;
                    var static_day_data = 0;
                    var static_all_data = 0;
                    for (var item in data) {
                        var daydata = parseInt(data[item]["daydata"]);
                        var alldata = parseInt(data[item]["alldata"]);
                        static_day_data = static_day_data + daydata;
                        static_all_data = static_all_data + alldata;
                        content.push("<tr>");
                        content.push(start_str + count + end_str);
                        content.push(start_str + database_data[item] + end_str);
                        content.push(start_str + "<b class=\"color_2\">" + daydata + " </b>" + end_str);
                        content.push(start_str + "<b class=\"color_2\">" + alldata + " </b>" + end_str);
                        content.push("</tr>");
                        count++;
                    }
                    content.push("<tr>");
                    content.push(start_str + "总计" + end_str);
                    content.push(start_str + "&nbsp;" + end_str);
                    content.push(start_str + "<b class=\"color_2\">" + static_day_data + " </b>" + end_str);
                    content.push(start_str + "<b class=\"color_2\">" + static_all_data + " </b>" + end_str);
                    content.push("</tr>");
                    $("#system_list").empty().html(content.join(""));
                }
            },
            "json"
        );
    },
    GetDataBaseStr: function(data) {
        var str = [];
        for (var item in data) {
            str.push(item);
        }
        return str.join(",");
    }
}