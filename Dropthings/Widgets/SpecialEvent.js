var _sepcialEvent = new Object;
var SepcialEvent = _sepcialEvent.property = {    
    Load: function() {
        var now_date = SepcialEvent.GetDateStr(new Date());
        $.getJSON("Handler/Info.ashx",
	        { "time_str": now_date, "data_type": "special_event" },
	        function(data) {
	            if (data) {
	                SepcialEvent.CategoryData(data);
	            }
	        }
	    )
    },
    GetDateStr: function(time) {
        return time.getFullYear() + "-" + (time.getMonth() + 1) + "-" + time.getDate();
    },
    CategoryData: function(data) {
        var content = [];
        var row = data["categoryData"];
        var count = 1;
        var Percent = 0;
        content.push("<ul class=\"news_list\">");

        for (var item in row) {
            var info = unescape(item).split('_');
            content.push("<li><a href=\"javascript:void(null);\" pid=\"" + info[1] + "_" + info[2] + "\">");
            content.push("<span class=\"tabler_count\">" + count + "</span><span class=\"tabler_trend_up\"></span><span>" + info[0]);
            Percent = parseInt(row[item]) / 10;
            content.push("</span><span class=\"tabler_weight\" style=\"width:" + Percent + "px;\" title=\"" + row[item] + "\"></span></a></li>");
            count++;
        }
        content.push("</ul>");

        $("#category_list").empty().html(content.join(""));
        $("#category_list").find("a").click(function() {
            if ($(this).attr("pid") != "undefined") {
                //TODO: 此处需要修改
                //1、首先设置隐藏字段保存相关条件，然后跳转到搜索页面，并增加搜索页面初始化时获取关键词的代码。不可行，跳转后丢失
                //2、使用查询字符串传递参数
                var href = "Default.aspx?" + escape("舆论专题") + "&id=" + $(this).attr("pid") + "&name=" + $(this).children("span:eq(2)").text();
                location.href = href;
            }
        });
    } /*,
    GetTrendHtml: function(time_str) {
        var trend_time = parseInt(Date.parse(new Date(time_str.replace(/-/g, "/"))));
        var now_time = parseInt(Date.parse(new Date()));
        var day_time = 24 * 60 * 60 * 1000;
        var span_day = parseInt((now_time - trend_time) / day_time);
        if (span_day < 8) {
            return "</span><span class=\"tabler_trend_up\"></span>";
        } else if (span_day >= 8 && span_day <= 18) {
            return "</span><span class=\"tabler_trend_gentle\"></span>";
        } else {
            return "</span><span class=\"tabler_trend_down\"></span>";
        }
    }*/
}