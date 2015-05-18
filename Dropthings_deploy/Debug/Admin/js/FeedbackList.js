/// <reference path="jquery-1.4.2.js" />
/// <reference path="../ckfinder/ckfinder.js" />
/// <reference path="../ckeditorfull/ckeditor.js" />


var FeedbackList = {
    UpdateId: 0,
    Init: function () {
        FeedbackList.LoadEvent();
    },
    LoadEvent: function () {
        $("#submit_btn").unbind().bind("click", FeedbackList.CreateFeedBack);
        $("#create_opinion_feedback_btn").unbind().bind("click", FeedbackList.CreateEvent);
    }, CreateEvent: function () {
        listTable.ShowFrame("move_column", "column_edit_frame", "close_edit_frame", false);
        FeedbackList.ClearMsg();
        $("#submit_btn").unbind().bind("click", FeedbackList.CreateFeedBack);

    }, UpdateEvent: function () {
        listTable.ShowFrame("move_column", "column_edit_frame", "close_edit_frame", false);
        $("#submit_btn").unbind().bind("click", FeedbackList.UpdateFeedBack);
    }, CreateFeedBack: function () {
        //提交新的舆情反馈
        var data = FeedbackList.GetAddOrUpdateDate("create");
        function _callFun(data) {
            alert("创建成功!");
            FeedbackList.RefreshPage();
        }
        FeedbackList.FeedBackAjax(data, _callFun)
    }, UpdateFeedBack: function () {
        var data = FeedbackList.GetAddOrUpdateDate("update");
        function _callFun(xdata) {
            alert("修改成功!");
        }
        FeedbackList.FeedBackAjax(data, _callFun);
    }, GetAddOrUpdateDate: function (action) {
        var oTitle = $("#OpinionName").val();
        var oContent = CKEDITOR.instances["editor1"].getData();
        var oName = $("#CreateName_txt").val();
        var oDate = $("#CreateTime_txt").val();
        oContent = escape(oContent);
        var data = { "action": action, "title": oTitle, "content": oContent, "name": oName, "datetime": oDate, "id": FeedbackList.UpdateId };
        return data;
    }, LoadCKeditor: function () {
        var editor = CKEDITOR.replace('editor1', {});
        CKFinder.setupCKEditor(editor, 'ckfinder/');
    }, DelFeedBack: function (obj) {
        var id = $(obj).attr("pid");
        var data = { "action": "delete", "id": id }
        function _callFun(data) {
            alert("删除成功！");
            FeedbackList.RefreshPage();
        }
        FeedbackList.FeedBackAjax(data, _callFun);
    }, FindById: function (obj) {
        var id = $(obj).attr("pid");
        var data = { "action": "findbyid", "id": id }
        function _callFun(xdata) {
            $("#OpinionName").val(xdata["TITLE"]);
            CKEDITOR.instances["editor1"].setData(unescape(xdata["OPINIONCONTENT"]));
            $("#CreateName_txt").val(xdata["CREATER"]);
            $("#CreateTime_txt").val(xdata["CREATETIME"]);
            FeedbackList.UpdateId = xdata["ID"];
            //显示
            //
            FeedbackList.UpdateEvent();
        }
        FeedbackList.FeedBackAjax(data, _callFun);
    }, FeedBackAjax: function (data, callfun) {
        $.ajax({
            url: "../Handler/Feedback.ashx",
            data: data,
            type: "post",
            dataType: "json",
            success: callfun
        });
    }, ClearMsg: function () {
        var d = new Date();
        var dtiem = d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate() + " " + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds(); //new Date().toLocaleString().replace(/年|月/g, "-").replace(/日/g, " ")
        $("#OpinionName").val("");
        CKEDITOR.instances["editor1"].setData("<p></p>");
        //$("#CreateName_txt").val(dtiem);
        $("#CreateTime_txt").val(dtiem);
    }, RefreshPage: function () {
        window.location.href = window.location.href;
        //var data = { "action": "del" };

        //        $.ajax({
        //            url: "FeedbackList.aspx",
        //            data: data,
        //            type: "post",
        //            dataType: "json",
        //            success: function (d) {
        //            }
        //        });
    }
}


$(document).ready(function () {
    FeedbackList.Init();
    FeedbackList.LoadCKeditor();
});