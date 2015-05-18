$(document).ready(function() {
    ReportList.InitBtnClick();
});

var _reportlist = new Object;
var ReportList = _reportlist.property = {
    InitBtnClick: function() {
        $("#all_data_list a[name='audit_report']").each(function() {
            var status = $(this).attr("status");
            if (status == "3") {
                $(this).parent().text("审批通过");
            }
        });

        $("#all_data_list").find("a[name='look_report']").click(function() {
            var url = $(this).attr("pid");
            window.open("../reportcontent/" + url);
        });
        $("#all_data_list").find("a[name='audit_report']").click(function() {
            var status = $(this).attr("status");
            if (status == "3") {
                alert("审批流程已经完成");
            } else {
                var id = $(this).attr("pid"); //fileName_ID，如123.doc_185
                $.post("../Handler/BriefReportData.ashx",
                    { "type": "audit", "id": id },
                    function(data) {
                        if (data) {
                            if (data.success == "1") {
                                alert("审批成功");
                                location.href = location.href;
                            } else {
                                alert("审批失败");
                            }
                        }
                    }, "json");
            }
        });
        $("#all_data_list").find("a[name='edit_report']").click(function() {
            var url = $(this).attr("pid"); //fileName_ID，如123.doc_185
            window.open("BriefReport.html?d=" + url);
        });
        $("#all_data_list").find("a[name='delete_report']").click(function() {
            if (!confirm("您确定要删除该简报？")) {
                return;
            }
            var id = $(this).attr("pid");
            var url = $(this).attr("cate");
            $.post("../Handler/ManageFile.ashx",
                { "type": "deleteFile", "file_name": url, "id": id },
                function(data) {
                    if (data) {
                        if (data["SucceseCode"] == "0") {
                            alert(data["error"]);
                        } else {
                            alert("删除成功！");
                            location.href = location.href;
                        }
                    }
                });
            //window.open("../reportcontent/" + url);
        });

        $("#report_upload").click(function() {
            listTable.ShowFrame("move_column", "column_edit_frame", "close_edit_frame", true);
        });

        $("#btn_upload").click(function() {
            if (ReportList.CheckInfo()) {
                var url = ReportList.GetPostUrl();
                //var url = "ManageFile.ashx";
                listTable.UpLoadFile("ReportFilePath", "back_msg", url, true);
            }
        });
    },
    CheckInfo: function() {
        var report_name = $.trim($("#ReportName").val());
        if (!report_name) {
            alert("请输入简报名称！");
            return false;
        }
        var report_creater = $.trim($("#ReportCreater").val());
        if (!report_creater) {
            alert("请输入简报制作人！");
            return false;
        }
        var report_create_time = $.trim($("#ReportCreateTime").val());
        if (!report_create_time) {
            alert("请输入简报制作时间！");
            return false;
        }
        var report_file_path = $.trim($("#ReportFilePath").val());
        if (!report_file_path) {
            alert("请选择上传简报地址！");
            return false;
        }
        return true;
    },
    GetPostUrl: function() {
        var url = "ManageFile.ashx?type=uploadreport&";
        var params = [];
        params.push("reportname=" + escape($("#ReportName").val()));
        params.push("reportcreater=" + escape($("#ReportCreater").val()));
        params.push("reportcreatetime=" + escape($("#ReportCreateTime").val()));
        params.push("reporttag=" + $("#ReportTag").val());
        params.push("reporttype=" + $("#ReportType").val());
        return url + params.join("&");
    }
}