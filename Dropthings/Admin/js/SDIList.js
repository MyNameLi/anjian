$(document).ready(function() {
    SDIList.Init();
});
var _sdilist = new Object;
var SDIList = _sdilist.property = {
    Init: function() {
        //listTable.ShowFrame("sdi_detail_move", "sdi_detail_frame", "sdi_detail_close");
        $("#all_data_list").find("a[name='start_task']").click(function() {
            var id = $(this).attr("pid");
            var status = $(this).attr("cate");
            if (status && status != "0") {
                alert("该任务已经启动！");
                return;
            }
            var current_obj = this;
            $.post(location.href,
                { "act": "starttask", "ajaxString": 1, "idList": id },
                function(data) {
                    if (data.Error == "1") {
                        alert("删除失败，原因是：" + data.ErrorStr);
                    }
                    if (data.Success == "1") {
                        $(current_obj).attr("cate", "1");
                        $("#task_id_" + id).empty().html("已启动");
                    }
                },
                "json"
            );
        });

        $("#all_data_list").find("span[id^='task_id_']").each(function() {
            var status = $(this).html();
            var status_str = SDIList.GetStatus(status);
            $(this).empty().html(status_str);
        });

        $("#all_data_list").find("a[name='look_event']").click(function() {
            var status = $(this).attr("cate");
            if (status != "3") {
                alert("此任务未启动或未完成！");
                return;
            }
            var taskid = $(this).attr("pid");
            SDIList.GetDetailData(taskid);
            //window.open("Detail.aspx?taskid=" + taskid);
        });
    },
    GetStatus: function(type) {
        switch (type) {
            case "0":
                return "等待启动";
            case "1":
                return "已启动";
            case "2":
                return "正在抓取";
            case "3":
                return "抓取完毕";
            default:
                return "等待启动";
        }
    },
    GetDetailData: function(task_id) {
        var InnitData = { "page_size": 5, "result_id": "TaskSearchResult", "status_bar_id": "TaskSearchPage",
            "info_id": "task_item_info_id", "sql_tag": "task_item", "web_url": location.href
        };
        var queryparams = { "act": "getdetail", "orderby": " ADDDATE DESC,ID", "ajaxString": 1 };
        queryparams["taskid"] = task_id;
        var sqlpager = new SqlPager(InnitData);
        sqlpager.Display = function(obj, data) {
            delete data["Count"];
            $("#" + obj).empty().html(SDIList.GetDataList(data, true));
            $("#" + obj).find("a[name='look_more_info']").click(function() {
                var clusterid = $(this).attr("pid");
                SDIList.GetMore(clusterid);
            });
            listTable.ShowFrame("sdi_detail_move", "sdi_detail_frame", "sdi_detail_close");
        }
        sqlpager.LoadData(1, queryparams);
    },
    GetMore: function(clusterid) {
        var InnitData = { "page_size": 10, "result_id": "MoreInfoResult", "status_bar_id": "MoreInfoPager",
            "info_id": "more_item_info_id", "sql_tag": "more_item", "web_url": location.href
        };
        var queryparams = { "act": "getmore", "orderby": " ADDDATE DESC,ID", "ajaxString": 1 };
        queryparams["clusterid"] = clusterid;
        var sqlpager = new SqlPager(InnitData);
        sqlpager.Display = function(obj, data) {
            delete data["Count"];
            $("#" + obj).empty().html(SDIList.GetDataList(data, false));
            listTable.ShowFrame("sdi_more_move", "sdi_more_frame", "sdi_more_close");
        }
        sqlpager.LoadData(1, queryparams);
    },
    GetDataList: function(data, tag) {
        var content = [];
        for (var item in data) {
            var row = data[item];
            content.push("<li>");
            content.push("<h2><a target=\"_blank\" title=\"" + unescape(row["title"]) + "\"");
            content.push(" href=\"" + unescape(row["url"]) + "\">" + unescape(row["title"]) + "</a></h2>");
            content.push("<div class=\"d\"><span>&nbsp;&nbsp;" + unescape(row["sitename"]) + "</span>");
            content.push(" - " + unescape(row["adddate"]));
            if (tag) {
                content.push("&nbsp;&nbsp;&nbsp;<a href=\"javascript:void(null);\" name=\"look_more_info\" pid=\"" + unescape(row["clusterid"]) + "\"");
                content.push(">" + row["totalsub"] + "条相同信息</a>");
            }
            content.push("</div>");
            content.push("<p>" + unescape(row["summary"]) + "</p>");
            content.push("</li>");
        }
        return content.join("");
    }
}