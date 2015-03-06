// Copyright (c) zhang tao. All rights reserved.

var _index = new Object;
var HotMapWidget = _index.property = {
    //HotMapWidget.prototype = {
    ImageID: "hot_map_img",
    Container: null,
    JobName: null,
    MapDataUrl: "Handler/GetMapData.ashx",
    ClusterResultUrl: "Handler/GetClusterResults.ashx",
    Url: "Handler/ClusterJobTime.ashx",
    load: function(container, jobname) {
        HotMapWidget.JobName = jobname;
        HotMapWidget.Container = container;

        $.getJSON(HotMapWidget.Url, function(data) { HotMapWidget.onContentLoad(data, HotMapWidget.ImageID); });
    },
    onContentLoad: function(data, obj) {
        var img = $("#" + HotMapWidget.ImageID);
        if (data) {
            delete data["SuccessCode"];
            var time_str = null;
            for (var item in data) {
                time_str = data[item];
            }
            var config = Common.Config;
            img.attr("src", config["idolhttp"] + "?action=ClusterServe2DMap&SourceJobname=" + config["hotmapname"] + "&enddate=" + time_str);
            var width = $("#" + obj).attr("width");
            var height = $("#" + obj).attr("height");
            scal_size = width / 525.0;
            height_size = height / 520.0;
            var s = { "scal_size": scal_size, "height_size": height_size, "end_date": time_str, "job_name": config["hotmapname"] };
            HotMapWidget.HotMapData(s, "widget");
        }
    },
    HotMapData: function(s, page_tag) {
        $.get(HotMapWidget.MapDataUrl,
             s, function(data, textStatus) {

                 var map = $("#mapData");

                 map.empty().html(unescape(data));
                 //隐藏文字说明
                 $("#mapData .node_text").each(function() {
                     $(this).hide();
                 });
                 $("#mapData .node").each(function() {
                     $(this).mouseover(function() {

                         var num = $(this).attr("id").split("_")[1];
                         $("#clustertitle_" + num).show();
                     });

                     $(this).mouseout(function() {

                         var num = $(this).attr("id").split("_")[1];
                         $("#clustertitle_" + num).hide();
                     });

                     $(this).click(function() {
                         /*$("#mapData.node").each(function() {
                         $(this).css("background", "red");
                         });*/
                         $(this).css("background", "green").siblings().css("background", "red");
                         var cluster_id = $(this).attr("id").split("_")[1];
                         /*if (page_tag == "index") {
                         location.href = "hot.html";
                         }
                         else {*/
                         HotMapWidget.GetClusterResults(cluster_id, s.end_date, s.job_name);
                         /*}*/
                     });

                 }); //each end
                 $("#mapData .node").eq(0).click();
             });
    },
    GetClusterResults: function(cluster_id, end_date, job_name) {
        $.get(HotMapWidget.ClusterResultUrl, { 'cluster_id': cluster_id, "end_date": end_date, "job_name": job_name },
            function(data) {
                //alert(unescape(data));
                //$("#hot_prompt").empty();                
                $("#whats_hot").empty().html(unescape(data));
                $("#whats_hot").show();
            }
        );
    }
};
