// Copyright (c) Omar AL Zabir. All rights reserved.
// For continued development and updates, visit http://msmvps.com/omar

var FastRssWidget = function(url, container, count, cachedJson)
{
    this.url = url;
    this.container = container;
    this.count = count;
    this.cachedJson = cachedJson;
}

FastRssWidget.prototype = {

    load: function() {
        if (this.cachedJson == null) {
            var div = $get(this.container);
            div.innerHTML = "正在加载中...";

            Proxy.getRss(this.url, this.count, 10, Function.createDelegate(this, this.onContentLoad));
        }
        else {
            this.onContentLoad(this.cachedJson);
        }
    },

    onContentLoad: function(rss) {
        var div = $get(this.container);
        div.innerHTML = "";
        if (rss == null) {
            div.innerHTML = "加载RSS出错.";
        }
        else {
            for (var i = 0; i < rss.length; i++) {
                var item = rss[i];
                
                var li = document.createElement("LI");
                var a = document.createElement("A");
                a.href = item.Link;
                a.innerHTML = item.Title;
                a.title = item.Description;
                a.className = "feed_item_link";
                a.target = "_blank";
                li.appendChild(a);
                
                div.appendChild(li);
            }
        }
    }
};
