var _dataSourceStatisticsMap = new Object;
var DataSourceStatisticsMap = _dataSourceStatisticsMap.property = {
    Load: function() {
        $("#data_source_look").click(function() {
            DataSourceStatisticsMap.InnitFlash();
        });
        DataSourceStatisticsMap.InnitFlash();
    },
    GetDateStr: function(time) {
        return time.getFullYear() + "-" + (time.getMonth() + 1) + "-" + time.getDate();
    },
    InnitFlash: function(data) {
        var flash_url = "Chart/ampie.swf";
        var start_time = $.trim($("#data_source_starttime").val());
        var end_time = $.trim($("#data_source_endtime").val());
        var dis_num = $("#data_source_disnum").val();
        var set_url = [];
        set_url.push("static/DataSourceStatic.ashx?act=MYSITENAME");
        set_url.push(dis_num);
        if (start_time) {
            set_url.push(start_time);
        } else {
            set_url.push("");
        }
        if (end_time) {
            set_url.push(end_time);
        } else {
            set_url.push("");
        }
        var flashVars =
            {
                settings_file: "static/DataSourcesettings.xml",
                data_file: set_url.join("|")
            };

        swfobject.embedSWF(flash_url, "data_source_flash", "900", "400", "8.0.0", "amchatsflash/expressInstall.swf", flashVars);
    }
}