var _hotSpectrumMap = new Object;
var HotSpectrumMap = _hotSpectrumMap.property = {
    LeaderName: "",
    Current_id: null,
    Load: function() {
        HotSpectrumMap.SGDataMapInit("SGMap");
    },
    SGDataMapInit: function(obj) {
        var config = Common.Config;
        var sgmapstr = config["idolhttp"] + "?action=ClusterSGPicServe&SourceJobname=" + config["sgmapname"];        
        $("#" + obj).attr("src", sgmapstr);
        var width = $("#" + obj).attr("width");
        var height = $("#" + obj).attr("height");
        scal_size = width / 525.0;
        height_size = height / 510.0;        
        var s = { "hot_scal_size": scal_size, "height_size": height_size };
        HotSpectrumMap.LightPicData(s, "widget");
    },
    GetSGDataResults: function(point_id, from_time_id, end_time_id) {
        $.get("Handler/GetSGDataResults.ashx", { 'point_id': point_id, "from_time_id": from_time_id, "end_time_id": end_time_id },
            function(data) {
                $("#hot_prompt").empty();
                $("#doc_list").empty().html(data);
            }
        );
    },
    LightPicData: function(s, page_tag, current_id) {
        $.get("Handler/GetSGData.ashx",
             s,
            function(data, textStatus) {
                var map = $("#hotspotMapData");
                map.html(data);
                if (current_id)
                    $("#hotclusternode_" + current_id).css({ "border-right": "2px solid white", "border-left": "2px solid white" });
                //隐藏文字说明
                $(".hot_node_text").each(function() {
                    $(this).hide();
                });
                if ($.browser.msie) {
                    $(".hotnode").each(function(n) {
                        var width = $(this).width();
                        var height = $(this).height();
                        $(this).html("<div style=\"background-color:#CCC; filter:alpha(opacity=0); width:" + width + "px;height:" + height + "px;\"></div>");
                    });
                }

                $(".hotnode").each(function(n) {

                    $(this).mouseover(function() {
                        $(this).css({ "border-right": "2px solid white", "border-left": "2px solid white" });
                        var num = $(this).attr("id").split("_")[1];
                        $("#hotclustertitle_" + num).show();
                    });

                    $(this).mouseout(function() {
                        if ($(this).attr("id") != current_id)
                            $(this).css("border", "none");
                        if (current_id != null)
                            $("#hotclusternode_" + current_id).css({ "border-right": "2px solid white", "border-left": "2px solid white" });
                        var num = $(this).attr("id").split("_")[1];
                        $("#hotclustertitle_" + num).hide();
                    });

                    $(this).click(function() {
                        $("#hotclusternode_" + current_id).css("border", "none");
                        current_id = $(this).attr("id").split('_')[1];
                        $(this).css({ "border-right": "2px solid white", "border-left": "2px solid white" });
                        var info_list = $(this).attr("pid").split("※");
                        //if (page_tag == "index") {
                        //    location.href = "trend.html?point_id=" + info_list[0] + "&from_time_id=" + info_list[1] + "_&end_time_id=" + info_list[2] + "_&current_id=" + current_id;
                        //}
                        //else
                        HotSpectrumMap.GetSGDataResults(info_list[0], info_list[1], info_list[2]);
                    });
                }); //each end
                $("#hotspotMapData .hotnode").eq(0).click();
            });
    },
    GetDateStr: function(time) {
        return time.getFullYear() + "-" + (time.getMonth() + 1) + "-" + time.getDate();
    }
}