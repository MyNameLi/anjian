//<![CDATA[
var _OriginalSearch = new Object;
var OriginalSearch = _OriginalSearch.prototype = {
    Load: function(cssPath) {
        //google.setOnLoadCallback(OnLoad);
        var s = { "google": "originalSearchResult", "baidu": "originalSearchResult", "bing": "originalSearchResult", "soso": "originalSearchResult" };
        Common.TabControl(s, "tab_on", "tab_off");

        $("#google").click(function() {
            $("#originalSearchResult").html("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" height=\"180\" width=\"100%\"><tr><td align=\"center\" class=\"widget_loading\">数据正在加载中...</td></tr></table>");
            ensure({ css: [cssPath] }, function() {
                google.load('search', '1', { "callback": OriginalSearch.GoogleSearch, "language": "zh-CN", "nocss": true });
            });
        });
        $("#baidu").click(function() {
            $("#originalSearchResult").html("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" height=\"180\" width=\"100%\"><tr><td align=\"center\" class=\"widget_loading\">数据正在加载中...</td></tr></table>");
            ensure({ css: [cssPath] }, function() {
                var keywords = OriginalSearch.GetKeywords();
                if (keywords != "") {
                    OriginalSearch.BaiduSearch("newsA", 8, 0, keywords);
                }
            });
        });
        $("#bing").click(function() {
            var pageSize = 8;
            var offset = 0;

            $("#originalSearchResult").html("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" height=\"180\" width=\"100%\"><tr><td align=\"center\" class=\"widget_loading\">数据正在加载中...</td></tr></table>");

            ensure({ css: [cssPath] }, function() {
                var keywords = OriginalSearch.GetKeywords();
                if (keywords != "") {
                    OriginalSearch.BingSearch(keywords, "Web", pageSize, offset);
                }
            });
        });

        $("#google").click();
    },
    GetKeywords: function() {
        var keywords = document.getElementById("queryword");
        var word = "";
        if (null != keywords && keywords.value != "") {
            /*防止返回的中文结果乱码 begin*/
            var searchTerms = keywords.value.split(" ");
            for (term in searchTerms) {
                word += encodeURIComponent(searchTerms[term]) + "+";
            }
            if (word.length > 0) {
                word = word.slice(0, word.length - 1); //去掉最后的+字符
            }
            /*end*/
        }
        if (word.length == 0) {
            alert("请输入关键字！");
        }
        return word;
    },
    BaiduSearch: function(tn, rn, pn, keywords) {
        /*var url = "http://news.baidu.com/ns?cl=2&et=0&si=&ie=utf-8&ct=1&rn=" + rn + "&pn=" + pn + "&tn=" + tn + "&word=" + keywords;
        OriginalSearch.ShowBaiduNewsResult(url);*/
        $.getJSON("Handler/Baidu.ashx",
        { "tn": tn, "rn": rn, "pn": pn, "keyWords": keywords }
        , function(data) {
            OriginalSearch.ShowBaiduNewsResult(data);
        });
    },
    ShowBaiduNewsResult: function(data) {
        var output = document.getElementById("originalSearchResult");
        /*$.getScript(url, function(data, status) {
        if (status == "error") {
        alert(status);
        }
        else {
        alert("loaded successfully! ");
        $("#originalSearchResult").html(data);
        }
        });*/
        output.innerHTML = "";
        var resultStr = "<ul>";

        for (item in data.news) {
            resultStr += "<li><div class=\"gs-webResult gs-result\"><div class=\"gs-title\"><a class=\"gs-title\" target=\"_blank\" href=\""
        + data.news[item].Reference
        + "\">"
        + unescape(data.news[item].Title)
        + "</a></div><div class=\"gs-snippet\">"
        + unescape(data.news[item].Content)
        + "</div><div class=\"gs-visibleUrl gs-visibleUrl-short\">"
        + data.news[item].SiteName
        + "</div></div></li>";
        }
        resultStr += "</ul>";
        //var data = { "count": 12000, "pn": 0, "rn": 8 };

        var count = parseInt(data.count);
        var pageNumber = 10;
        var pageSize = data.rn;
        var current = Math.floor(data.pn / pageSize) + 1;

        var pager = document.createElement("div");
        pager.setAttribute("id", "pager");
        pager.setAttribute("class", "viciao");
        pager.setAttribute("style", "text-align: center;");
        pager.innerHTML = OriginalSearch.DisplayPager(pageSize, pageNumber, current, count);

        output.innerHTML = resultStr;
        output.appendChild(pager);

        $("div.gs-snippet").each(function() {
            var hrefs = $(this).find("a");
            var size = hrefs.size();
            if (size > 1) {
                $(hrefs[0]).attr("href", "http://news.baidu.com" + $(hrefs[0]).attr("href")).attr("target", "_blank");
                $(hrefs[1]).hide();
            }
            if (size == 1) {
                $(hrefs[0]).hide();
            }
        });

        OriginalSearch.InitBaiduPager(pageSize);
    },
    InitBaiduPager: function(pageSize) {
        $("#pager span").each(function() {
            var offset = (parseInt($(this).attr("name").split("_")[1]) - 1) * pageSize;

            $(this).children("a").click(function() {
                //$("div.gsc-tabsArea").append("<div class=\"widget_loading\"><span class=\"btn_loading\">数据正在加载中...</span></div>");
                var keywords = OriginalSearch.GetKeywords();
                if (keywords != "") {
                    /*var searchType = $("div.gsc-tabhActive").text();
                    if (searchType == "网络") {
                    searchType = "web";
                    }
                    if (searchType == "新闻") {
                    searchType = "news";
                    }*/
                    OriginalSearch.BaiduSearch("newsA", pageSize, offset, keywords);
                    //$("#pager").html(OriginalSearch.DisplayPager(pageSize, 10, current, pageTotal));

                    return false;
                }
            });
        });
    },
    GoogleSearch: function() {
        // Create a search control
        var searchControl = new google.search.SearchControl();
        //google.search.Search.getBranding(document.getElementById("branding")); 

        // Add in a full set of searchers
        //var localSearch = new google.search.LocalSearch();
        //searchControl.addSearcher(localSearch);
        var web = new google.search.WebSearch();
        web.setUserDefinedLabel("网络");
        searchControl.addSearcher(web); //new google.search.WebSearch());

        //searchControl.addSearcher(new google.search.VideoSearch());
        //searchControl.addSearcher(new google.search.BlogSearch());
        var bs = new google.search.BlogSearch();
        bs.setUserDefinedLabel("博客");
        searchControl.addSearcher(bs); //new google.search.BlogSearch());

        //searchControl.addSearcher(new google.search.NewsSearch());
        var news = new google.search.NewsSearch();
        news.setUserDefinedLabel("新闻");
        searchControl.addSearcher(news); //new google.search.NewsSearch());

        //searchControl.addSearcher(new google.search.ImageSearch());
        //searchControl.addSearcher(new google.search.BookSearch());
        //searchControl.addSearcher(new google.search.PatentSearch());

        /* 
        var amazon = new google.search.WebSearch();
        amazon.setUserDefinedLabel("amazon.com");//用户自定义TAB标题
        amazon.setSiteRestriction("amazon.com");//搜索指定网站的内容
        */

        // Set the Local Search center point
        //localSearch.setCenterPoint("New York, NY");

        // tell the searcher to draw itself and tell it where to attach
        var result = document.getElementById("originalSearchResult");
        //searchControl.draw(result);

        // create a drawOptions object
        var drawOptions = new google.search.DrawOptions();

        // tell the searcher to draw itself in linear mode
        //            drawOptions.setDrawMode(google.search.SearchControl.DRAW_MODE_LINEAR);
        //            searchControl.draw(result, drawOptions);

        // tell the searcher to draw itself in tabbed mode
        //drawOptions.setDrawMode(google.search.SearchControl.DRAW_MODE_TABBED);
        drawOptions.setDrawMode(GSearchControl.DRAW_MODE_TABBED);
        drawOptions.setInput(document.getElementById("queryword")); //指定输入关键字的input元素，如果不指定，则会自动在当前页面插入一个input元素

        searchControl.setResultSetSize(8); //设置每页显示的结果条数
        searchControl.draw(result, drawOptions);
        var temp = searchControl.execute();
        //如果不使用drawOptions.setInput,则使用下面的代码        
        /*var keywords = document.getElementById("queryword");
        if (null != keywords && keywords.value != "") {
        // execute an search
        searchControl.execute(keywords.value);
        }*/
    },
    BingSearch: function(keywords, searchType, pageSize, offSet) {
        var AppId = "1AD18CF73B9FB09F406A95DFE52194A70C6E3F85";

        var requestStr = "http://api.bing.net/json.aspx?"
        // Common request fields (required)
            + "AppId=" + AppId
        //    + "&Query=msdn blogs"
            + "&Query=" + keywords
        //    + "&Sources=Web"
            + "&Sources=" + searchType//eg. web/web+news

        // Common request fields (optional)
            + "&Version=2.0"
        + "&Market=zh-CN"
        + "&Adult=Moderate"
         + "&Options=EnableHighlighting";

        // Web-specific request fields (optional)
        if (searchType.toLowerCase() == "web") {
            requestStr += "&Web.Count=" + pageSize
            + "&Web.Offset=" + offSet
        + "&Web.Options=DisableHostCollapsing+DisableQueryAlterations";
        }
        if (searchType.toLowerCase() == "news") {
            requestStr += "&News.Count=" + pageSize
            + "&News.Offset=" + offSet
        + "&News.Options=DisableHostCollapsing+DisableQueryAlterations"
        + "&News.SortBy=Relevance";
        }
        // JSON-specific request fields (optional)
        requestStr += "&JsonType=callback"
            + "&JsonCallback=OriginalSearch.SearchCompleted";

        //        ensure({ js: [requestStr] }, function() {

        //        });

        //                var requestScript = document.getElementById("searchCallback");
        //                requestScript.src = requestStr;

        $.getScript(requestStr); //, function(data) {
        //OriginalSearch.SearchCompleted(data);
        //});
    },
    SearchCompleted: function(response) {
        if (response == null)
            return;

        var errors = response.SearchResponse.Errors;
        if (errors != null) {
            // There are errors in the response. Display error details.
            OriginalSearch.DisplayErrors(errors);
        }
        else {
            // There were no errors in the response. Display the
            // Web results.
            OriginalSearch.DisplayResults(response);
        }
    },
    DisplayResults: function(response) {
        var output = document.getElementById("originalSearchResult");
        output.innerHTML = "";
        var resultsHeader = document.createElement("h4");
        var resultsList = document.createElement("ul");
        var pager = document.createElement("div");
        pager.setAttribute("id", "pager");
        pager.setAttribute("class", "viciao");
        pager.setAttribute("style", "text-align: center;");

        output.appendChild(resultsHeader);
        output.appendChild(resultsList);
        output.appendChild(pager);

        var pageSize = 8;
        var pageNumber = 10;
        var totalCount = 0;
        var current = 1;
        var offset = 0;

        var searchType = "web";
        var results = null;
        if (response.SearchResponse.Web) {
            results = response.SearchResponse.Web.Results;
            totalCount = parseInt(response.SearchResponse.Web.Total);
            offset = parseInt(response.SearchResponse.Web.Offset)
        }
        if (response.SearchResponse.News) {
            results = response.SearchResponse.News.Results;
            totalCount = parseInt(response.SearchResponse.News.Total);
            offset = parseInt(response.SearchResponse.News.Offset)
            searchType = "news";
        }
        // Display the results header.
        resultsHeader.innerHTML = "" /*"Bing API Version "
            + response.SearchResponse.Version
            + "<br />网络搜索："
            + response.SearchResponse.Query.SearchTerms
            + "<br />结果显示：第"
            + (offset + 1)
            + " 至 "
            + (offset + results.length)
            + " 条 ，约"
            + totalCount
            + " 条结果<br />";*/

        + "<div class=\"gsc-tabsArea\">"
        + "<div class=\" gsc-tabHeader gsc-tabhInactive\">网络</div><span class=\"gs-spacer\"> </span>"
        /*+ "<div class=\" gsc-tabHeader gsc-tabhInactive\">博客</div><span class=\"gs-spacer\"> </span>"*/
        + "<div class=\" gsc-tabHeader gsc-tabhInactive\">新闻</div><span class=\"gs-spacer\"> </span>"
        + "</div>";
        if (searchType == "web") {
            $("div.gsc-tabHeader").removeClass("gsc-tabhActive").addClass("gsc-tabhInactive");
            $("div.gsc-tabHeader").eq(0).removeClass("gsc-tabhInactive").addClass("gsc-tabhActive");
        }
        if (searchType == "news") {
            $("div.gsc-tabHeader").removeClass("gsc-tabhActive").addClass("gsc-tabhInactive");
            $("div.gsc-tabHeader").eq(1).removeClass("gsc-tabhInactive").addClass("gsc-tabhActive");
        }

        $("div.gsc-tabHeader").click(function() {
            searchType = $(this).text();
            if (searchType == "新闻") {
                searchType = "news";
            }
            if (searchType == "网络") {
                searchType = "web";
            }
            var keywords = OriginalSearch.GetKeywords();
            if (keywords != "") {
                OriginalSearch.BingSearch(keywords, searchType, pageSize, offset);
            }
            //$(this).removeClass("gsc-tabhInactive").addClass("gsc-tabhActive").siblings().removeClass("gsc-tabhActive").addClass("gsc-tabhInactive");
        });

        // Display the Web results.
        var resultsListItem = null;
        var resultStr = "";
        for (var i = 0; results != undefined && i < results.length; ++i) {
            resultsListItem = document.createElement("li");
            resultsList.appendChild(resultsListItem);
            resultStr = "<div class=\"gs-webResult gs-result\"><div class=\"gs-title\"><a class=\"gs-title\" target=\"_blank\" href=\"";
            resultStr += results[i].Url
                + "\">"
                + results[i].Title
                + "</a></div><div class=\"gs-snippet\">"
            if (searchType == "web") {
                resultStr += results[i].Description;
            }
            if (searchType == "news") {
                resultStr += results[i].Snippet;
            }

            //+ "<br />Last Crawled: "
            //+ results[i].DateTime
            //+ "<br /><br />";
            resultStr += "</div><div class=\"gs-visibleUrl gs-visibleUrl-short\">";
            if (searchType == "web") {
                resultStr += results[i].DisplayUrl;
            }
            if (searchType == "news") {
                resultStr += results[i].Source;
            }

            resultStr += "</div></div>";
            // Replace highlighting characters with strong tags.
            resultsListItem.innerHTML = OriginalSearch.ReplaceHighlightingCharacters(
                resultStr,
                "<b>",
                "</b>", "bing");
        }

        current = Math.floor(offset / pageSize) + 1;
        if ((offset % pageSize) > 0) {
            current += 1;
        }

        pager.innerHTML = OriginalSearch.DisplayPager(pageSize, pageNumber, current, totalCount);
        OriginalSearch.InitPager();
    },
    InitPager: function() {
        $("#pager span").each(function() {
            var pageSize = 8;
            var offset = (parseInt($(this).attr("name").split("_")[1]) - 1) * pageSize;

            $(this).children("a").click(function() {
                //$("div.gsc-tabsArea").append("<div class=\"widget_loading\"><span class=\"btn_loading\">数据正在加载中...</span></div>");
                var keywords = OriginalSearch.GetKeywords();
                if (keywords != "") {
                    var searchType = $("div.gsc-tabhActive").text();
                    if (searchType == "网络") {
                        searchType = "web";
                    }
                    if (searchType == "新闻") {
                        searchType = "news";
                    }
                    OriginalSearch.BingSearch(keywords, searchType, pageSize, offset);
                    //$("#pager").html(OriginalSearch.DisplayPager(pageSize, 10, current, pageTotal));

                    return false;
                }
            });
        });
    },
    DisplayPager: function(pageSize, pageNumber, current, totalCount) {//pageNumber显示的结果页数；current当前页；pageTotal总页数

        var pager = ""; // "<div id=\"pager_list\" class=\"viciao\" style=\"text-align: center;\">";
        if (current == 1) {
            pager += "<span style=\"margin-left: 5px;\" name=\"Pager_1\" class=\"current\">上一页</span>";
        } else {
            pager += "<span style=\"margin-left: 5px;\" name=\"Pager_" + (current - 1) + "\"><a href=\"javascript:void(null);\">上一页</a></span>";
        }
        var pageTotal = Math.floor(totalCount / pageSize);
        if (totalCount % pageSize > 0) {
            pageTotal += 1;
        }
        var startNumber = 1;
        if (current > 5) {
            startNumber = current - 5;
        }
        var endNumber = startNumber + pageNumber - 1;
        if ((pageTotal - startNumber) < pageNumber) {
            endNumber = pageTotal;
        }

        for (i = startNumber; i <= endNumber; i++) {
            if (current == i) {
                pager += "<span style=\"margin-left: 2px;\" name=\"Pager_" + current + "\" class=\"current\">" + current + "</span>";
            } else {
                pager += "<span style=\"margin-left: 2px;\" name=\"Pager_" + i + "\"><a href=\"javascript:void(null);\">" + i + "</a></span>";
            }
        }

        if (current == pageTotal) {
            pager += "<span style=\"margin-left: 2px;\" name=\"Pager_" + (current + 1) + "\" class=\"current\">下一页</span>";
        } else {
            pager += "<span style=\"margin-left: 2px;\" name=\"Pager_" + (current + 1) + "\"><a href=\"javascript:void(null);\">下一页</a></span>";
        }
        //pager += "</div>";

        return pager;
    },
    ReplaceHighlightingCharacters: function(text, beginStr, endStr, type) {
        if (type == "bing") {
            // Replace all occurrences of U+E000 (begin highlighting) with
            // beginStr. Replace all occurrences of U+E001 (end highlighting)
            // with endStr.
            var regexBegin = new RegExp("\uE000", "g");
            var regexEnd = new RegExp("\uE001", "g");

            return text.replace(regexBegin, beginStr).replace(regexEnd, endStr);
        }
        /*if (type = "baidu") {
        var keywords = document.getElementById("queryword");
        var searchTerms = keywords.value.split(" ");
        var result = text;
        for (term in searchTerms) {
        result = result.replace(searchTerms[term], beginStr + searchTerms[term] + endStr);
        }
        return result;
        }*/
    },
    DisplayErrors: function(errors) {
        var output = document.getElementById("originalSearchResult");
        output.innerHTML = "";
        var errorsHeader = document.createElement("h4");
        var errorsList = document.createElement("ul");
        output.appendChild(errorsHeader);
        output.appendChild(errorsList);

        // Iterate over the list of errors and display error details.
        errorsHeader.innerHTML = "错误信息:";
        var errorsListItem = null;
        for (var i = 0; i < errors.length; ++i) {
            errorsListItem = document.createElement("li");
            errorsList.appendChild(errorsListItem);
            errorsListItem.innerHTML = "";
            for (var errorDetail in errors[i]) {
                errorsListItem.innerHTML += errorDetail
                    + ": "
                    + errors[i][errorDetail]
                    + "<br />";
            }
            errorsListItem.innerHTML += "<br />";
        }
    }
}
//]]>