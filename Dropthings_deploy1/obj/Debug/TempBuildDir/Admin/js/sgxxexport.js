$(document).ready(function() {
    SgxxExport.Init();
});

var _sgxxexport = new Object;
var SgxxExport = _sgxxexport.property = {
    queryparams: { "act": "getinfolist", "strwhere": "", "orderby": " ID", "ajaxString": 1 },
    InnitData: { "page_size": 10, "result_id": "all_data_list", "status_bar_id": "item_pager_list",
        "info_id": "item_info_id", "sql_tag": "item", "web_url": location.href
    },
    Init: function() {
        SgxxExport.InitPager();
        SgxxExport.InitClickBtn();
    },
    CurrentData: null,
    InitPager: function() {
        var sqlpager = new SqlPager(SgxxExport.InnitData);
        sqlpager.Display = function(obj, data) {
            delete data["Count"];
            var content = [];
            var count = 1;
            var start_str = "<td bgcolor=\"#FFFFFF\" align=\"center\" height=\"25\">";
            var left_start_str = "<td bgcolor=\"#FFFFFF\" align=\"left\" height=\"25\">";
            var end_str = "</td>";
            var edit_pic_str = "<img src=\"../images/037.gif\" width=\"9\" height=\"9\" />";
            var del_pic_str = "<img src=\"../images/010.gif\" width=\"9\" height=\"9\" />";
            for (var item in data) {
                var row = data[item];
                content.push("<tr>");
                content.push(start_str + "<input name=\"checkbox\" pid=\"" + item + "\" type=\"checkbox\" value='" + row["id"] + "' />" + row["id"] + end_str);
                content.push(start_str + "<a target=\"_blank\" href=\"" + unescape(row["url"]) + "\">" + unescape(row["articletitle"]) + "</a>" + end_str);
                content.push(start_str + unescape(row["basedate"]) + end_str);
                content.push(start_str + unescape(row["articlesource"]) + end_str);
                content.push(start_str + edit_pic_str + "[ <a name=\"info_export\" pid=\"" + item + "\"");
                content.push(" href=\"javascript:void(null);\">加入导出列表</a> ]" + end_str);
                content.push("</tr>");
                count++;
            }
            $("#" + obj).empty().html(content.join(""));
            SgxxExport.CurrentData = data;
            SgxxExport.InitClickFn(obj);
        }
        sqlpager.LoadData(1, SgxxExport.queryparams);
    },
    InitClickFn: function(obj) {
        var start_str = "<td bgcolor=\"#FFFFFF\" align=\"center\" height=\"25\">";
        var left_start_str = "<td bgcolor=\"#FFFFFF\" align=\"left\" height=\"25\">";
        var end_str = "</td>";
        var edit_pic_str = "<img src=\"../images/037.gif\" width=\"9\" height=\"9\" />";
        var del_pic_str = "<img src=\"../images/010.gif\" width=\"9\" height=\"9\" />";
        $("#" + obj).find("a[name='info_export']").click(function() {
            var item = $(this).attr("pid");
            var row = SgxxExport.CurrentData[item];
            var content = [];
            content.push("<tr>");
            content.push(start_str + "<span name=\"export_id_list\" pid=\"" + row["id"] + "\">" + row["id"] + "</span>" + end_str);
            content.push(start_str + "<a target=\"_blank\" href=\"" + unescape(row["url"]) + "\">" + unescape(row["articletitle"]) + "</a>" + end_str);
            content.push(start_str + unescape(row["basedate"]) + end_str);
            content.push(start_str + unescape(row["articlesource"]) + end_str);
            content.push(start_str + del_pic_str + "[ <a name=\"info_delete\" cate=\"0\" pid=\"" + item + "\"");
            content.push(" href=\"javascript:void(null);\">删除</a> ]" + end_str);
            content.push("</tr>");
            $("#exportlist").append(content.join(""));
            $("a[name='info_delete'][cate='0']").each(function() {
                $(this).attr("cate", "1");
                $(this).click(function() {
                    $(this).parent("td").parent("tr").remove();
                });
            });
        });
    },
    InitClickBtn: function() {
        var start_str = "<td bgcolor=\"#FFFFFF\" align=\"center\" height=\"25\">";
        var left_start_str = "<td bgcolor=\"#FFFFFF\" align=\"left\" height=\"25\">";
        var end_str = "</td>";
        var edit_pic_str = "<img src=\"../images/037.gif\" width=\"9\" height=\"9\" />";
        var del_pic_str = "<img src=\"../images/010.gif\" width=\"9\" height=\"9\" />";
        $("#export_all").click(function() {
            var content = [];
            $("#all_data_list").find("input:checked").each(function() {
                var item = $(this).attr("pid");
                var row = SgxxExport.CurrentData[item];
                content.push("<tr>");
                content.push(start_str + "<span name=\"export_id_list\" pid=\"" + row["id"] + "\">" + row["id"] + "</span>" + end_str);
                content.push(start_str + "<a target=\"_blank\" href=\"" + unescape(row["url"]) + "\">" + unescape(row["articletitle"]) + "</a>" + end_str);
                content.push(start_str + unescape(row["basedate"]) + end_str);
                content.push(start_str + unescape(row["articlesource"]) + end_str);
                content.push(start_str + del_pic_str + "[ <a name=\"info_delete\" cate=\"0\" pid=\"" + item + "\"");
                content.push(" href=\"javascript:void(null);\">删除</a> ]" + end_str);
                content.push("</tr>");
            });
            $("#exportlist").append(content.join(""));
            $("a[name='info_delete'][cate='0']").each(function() {
                $(this).attr("cate", "1");
                $(this).click(function() {
                    $(this).parent("td").parent("tr").remove();
                });
            });
        });


        $("#export_doc").click(function() {
            var id_list = [];
            $("#exportlist").find("span[name='export_id_list']").each(function() {
                var id = $(this).html();
                id_list.push(id);
            });
            if (id_list.length <= 0) {
                alert("导出列表为空！");
                return;
            }
            $.post("../Handler/SgxxExport.ashx",
                { "idList": id_list.join(",") },
                function(data) {
                    if (data) {
                        alert("导出成功！");                        
                        window.open(unescape(data.path));
                    }
                },
                "json"
            );
        });
    }
}