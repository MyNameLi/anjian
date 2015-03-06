$(document).ready(function() {
    Common.CategoryMenuClick = function(data, text) {
        $("#CategoryName").empty().html(text);
        var infoList = data.split("_");
        var category_id = infoList[0];
        var init_data = { "page_size": 10, "status_bar_id": "PagerList", "result_id": "SearchResult" };
        var l_Pager = new Pager(init_data);
        var query_params = { "action": "categoryquery", "display_style": 5, "totalresults": "true"
        };
        l_Pager.OtherFn = function(total_count) {
            $("#result_info").empty().html("共查询到<font color=\"black\">" + total_count + "</font>条记录");
        }
        query_params["category"] = category_id;
        l_Pager.LoadData(1, query_params);
    }
    var str_where = "ParentCate = 0 and IsEffect=1";
    Common.CategoryMenu("MenuList", "left-center-nav-ul-link", "left-center-nav-ul", str_where, "150");

});        

         