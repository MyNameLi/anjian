var _hotKeywords = new Object;
var HotKeywords = _hotKeywords.property = {    
    Load: function() {
        var now_date = HotKeywords.GetDateStr(new Date());
        $.getJSON("Handler/Info.ashx",
	        { "time_str": now_date, "data_type": "keyword_info" },
	        function(data) {
	            if (data) {
	                HotKeywords.Static(data["keyword_info"]);
	            }
	        }
	        )
    },
    Static: function(data) {
        var obj = document.getElementById("canvas");
        /*if (jQuery.browser.msie) {
            obj = window.G_vmlCanvasManager.initElement(obj);
        }*/
        var l_data = [];
        for (var item in data) {
            l_data.push([item, parseInt(data[item])]);
        }
        var Map = new LineMap(obj, l_data);
        Map.init();

    },
    GetDateStr: function(time) {
        return time.getFullYear() + "-" + (time.getMonth() + 1) + "-" + time.getDate();
    }
}