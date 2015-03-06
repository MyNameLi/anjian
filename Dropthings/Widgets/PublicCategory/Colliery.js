//<![CDATA[
function Colliery(categoryID) {
    this.CategoryID = categoryID;
    this.QueryParams = { "action": "categoryquery", "display_style": 6, "totalresults": "true",
        "PrintFields": "MYSITENAME,DREDATE,SENTIMENT"
    };
    if (this.CategoryID == undefined || this.CategoryID == null || this.CategoryID == "")
        this.CategoryID = "127638024140890829";  //_226_227
    //CategoryName: "煤矿",
    this.InitData = { "page_size": 6, "status_bar_id": "PagerList", "result_id": "SearchResult", "sorter_name": "sort_style", "display_number": "DisplayNumber" };
    this.tag = false;
}

Colliery.prototype.Load = function() {
    var sort_value = $("input[name='" + this.InitData["sorter_name"] + "']:checked").val();
    this.InitData["page_size"] = $("#" + this.InitData["display_number"]).val();    
    this.QueryParams["summary"] = "context"
    this.QueryParams["sort"] = sort_value;    
    this.QueryParams["category"] = this.CategoryID;
    var context = this;

    var l_Pager = new Pager(this.InitData);

    l_Pager.Display = function(data) {
        var result = "";
        for (d in data) {
            if (d.toLowerCase() != "success" && d.toLowerCase() != "totalcount") {
                result += "<li><a target=\"_blank\" href=\"" + unescape(data[d].href) + "\">" + unescape(data[d].title) + "</a></li>";
            }
        }
        $("#" + context.InitData["result_id"]).html(result);
    };
    l_Pager.LoadData(1, this.QueryParams);

    if (!this.tag) {
        this.InnitSort();
        this.InitDisplayNumber();
    }
}
Colliery.prototype.InnitSort = function() {
    var context = this;
    $("input[name='" + context.InitData["sorter_name"] + "']").click(function() {
        context.tag = true;
        context.Load();
    });
}
Colliery.prototype.InitDisplayNumber = function() {
    var context = this;
    $("#" + context.InitData["display_number"]).change(function() {
        context.tag = true;
        context.Load();
    });
}
//]]>