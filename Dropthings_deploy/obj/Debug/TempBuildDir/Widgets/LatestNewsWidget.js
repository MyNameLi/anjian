// Copyright (c) zhang tao. All rights reserved.
var _latestNewsWidget = new Object;
var LatestNewsWidget = _latestNewsWidget.property = {
    url: "handler/Info.ashx",
    TMarquee: null,
    container: "",    
    load: function() {
        /*var ul = jQuery("#" + LatestNewsWidget.container +" ul.news_list");
        ul.empty().html("<li>加载数据中...</li>");*/
        $("input[name='last_news_radio']").click(function() {
            clearInterval(LatestNewsWidget.TMarquee);
            LatestNewsWidget.InitNews();
        });
        LatestNewsWidget.InitNews();
    },
    InitNews: function() {
        var news_type = $("input[name='last_news_radio']:checked").val();
        $.getJSON(LatestNewsWidget.url,
        { "news_type": news_type, "data_type": "news_list" },
           LatestNewsWidget.onContentLoad
        );
    },
    GetDateStr: function(time) {
        return time.getFullYear() + "-" + (time.getMonth() + 1) + "-" + time.getDate();
    },
    NewsListInit: function() {
        var containerid = "#" + LatestNewsWidget.container;
        var id = "#last_news_info";
        var moreid = "#last_news_more";
        var all_height = 198;
        $(containerid).css({ "height": all_height + "px", "overflow": "hidden", "margin-bottom": "10px" });
        var height = "-" + all_height + "px";
        //setInterval(Marquee, 5000);

        function Marquee() {
            $(id).animate({
                marginTop: height
            }, 300, function() {
                var current_obj = this;
                $(this).css({ marginTop: "0px" });
                $(this).find("li").each(function(n) {
                    if (n < 6) {
                        $(this).appendTo(current_obj);
                    }
                });
            });
        }
        LatestNewsWidget.TMarquee = setInterval(Marquee, 5000);
        $(id).find("li").each(function() {
            $(this).hover(
                function() {
                    clearInterval(LatestNewsWidget.TMarquee);
                },
                function() {
                    LatestNewsWidget.TMarquee = setInterval(Marquee, 5000);
                }
            );
        });

    },
    RefleshLastData: function() {
        clearInterval(LatestNewsWidget.TMarquee);
        LatestNewsWidget.InitNews();
    },
    onContentLoad: function(data) {
        var ul = jQuery("#" + LatestNewsWidget.container + " ul.news_list");
        //alert("#" + LatestNewsWidget.container + " ul.news_list");
        ul.empty();
        var newscount = 0;
        if (data == null) {
            ul.html("<li>数据加载时出现错误.</li>");
        }
        else {
            var newsrow = data["newsList"]
            for (var item in newsrow) {
                var str = newsrow[item].split('_');
                var content = unescape(str[0]);
                var site = unescape(str[1]);
                var time_str = unescape(str[2]);

                var a = $("<a></a>");
                a.attr("href", item);
                a.attr("target", "_blank");
                a.attr("title", content);
                var timeHTML = "<b>" + time_str + "</b>&nbsp;&nbsp;";
                var siteHTML = "<b class='rss'>" + "【" + site + "】" + "</b>";
                var aHTML = timeHTML + content + siteHTML;
                a.html(aHTML);
                var li = $("<li></li>");
                li.append(a);
                ul.append(li);

                newscount++;
            }

            //var more_a = $("<a href='Widgets/LatestNewsDetail.aspx?type=more' target='_blank'>更多信息</a>");
            //ul.append(more_a);

            if (newscount > 6) {
                LatestNewsWidget.NewsListInit();
            }

            //            var timespan = 10 * 1000;
            //            setInterval(LatestNewsWidget.RefleshLastData, timespan);
        }
    }
};
