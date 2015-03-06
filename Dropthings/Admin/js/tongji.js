/// <reference path="jquery-1.4.2.js" />
var TongJi = {
    init: function () {
        //TongJi.init();
        $("#databasematch_type,#dateperiod_type").change(TongJi.resetGetQueryTagValues);
        $("#query_article_btn").unbind().bind("click", function () {
            TongJi.getHangYeCount();
            TongJi.getShiGuCount();
        });
    }, resetGetQueryTagValues: function () {
        $("#getquerytagvalues_txt").empty();
        var databasematch = $("#databasematch_type").val();
        var dateperiod = $("#dateperiod_type").val();
        var result = "http://10.16.0.100:9000/action=GetQueryTagValues&mindate=-100&documentcount=True&databasematch=" + databasematch + "&dateperiod=" + dateperiod + "&fieldname=autn_date&text=*&sort=ReverseDate&predict=False";
        $("#getquerytagvalues_txt").append(result);
        $("#get_query_tag_values").attr("href", result);
    }, getHangYeCount: function () {
        $("#hangye_count").empty().html("查询中..");
        var stime = $("#start_time").val();
        var etime = $("#end_time").val();
        var _where = " COLUMNID IN(95,96,97,98,99,100,101,170,171,172,173,174,175,176) " + "and ARTICLEEDITDATE >= to_date('" + stime + "' ,'yyyy-mm-dd') and ARTICLEEDITDATE < to_date('" + etime + "' ,'yyyy-mm-dd')";
        $.ajax({
            url: "Handler/TongJi.ashx",
            type: "POST",
            data: { "_w": _where },
            dataType: "JSON",
            success: function (rq) {
                $("#hangye_count").empty().html(rq);
            },
            error: function () {
                $("#hangye_count").empty().html("查询失败");
            }
        });

    }, getShiGuCount: function () {
        $("#shigu_count").empty().html("查询中..");
        var stime = $("#start_time").val();
        var etime = $("#end_time").val();
        var _where = " COLUMNID >=201 " + "and ARTICLEEDITDATE >= to_date('" + stime + "' ,'yyyy-mm-dd') and ARTICLEEDITDATE < to_date('" + etime + "' ,'yyyy-mm-dd')";

        $.ajax({
            url: "Handler/TongJi.ashx",
            type: "POST",
            data: { "_w": _where },
            dataType: "JSON",
            success: function (rq) {
                $("#shigu_count").empty().html(rq);
            },
            error: function () {
                $("#shigu_count").empty().html("查询失败");
            }
        });
    }
}
$(document).ready(function () {
    TongJi.init();
});