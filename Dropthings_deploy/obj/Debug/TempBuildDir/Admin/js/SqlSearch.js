// JavaScript Document
function SqlPager(s) {
    this.start = 1;
    this.page_size = s.page_size == null ? 10 : s.page_size;
    this.status_bar_id = s.status_bar_id;
    this.result_id = s.result_id;
    this.info_id = s.info_id;
    this.web_url = s.web_url;
    this.page_count = 20;
    this.status = [];
    this.query_params = null;
    this.total_count = 0;
    this.Start_time = new Date();
    this.end_time = null;
    this.sql_tag = s.sql_tag
}

SqlPager.prototype.Init_status_bar = function(current_page) {
    var obj = this;
    if (this.status) {
        var content = [];

        if (this.page_count > 1) {
            if (current_page == 1) {
                content.push("<td width=\"40\"><img width=\"37\" height=\"15\" src=\"../images/first.gif\" /></td>");
                content.push("<td width=\"40\"><img width=\"37\" height=\"15\" src=\"../images/back.gif\" /></td>");
            }
            else {
                content.push("<td width=\"40\" name=\"Pager1\"><a href=\"javascript:void(null);\" ><img width=\"37\" height=\"15\" src=\"../images/first.gif\" /></a></td>");
                content.push("<td width=\"40\" name=\"Pager" + (current_page - 1) + "\"><a href=\"javascript:void(null);\" ><img width=\"37\" height=\"15\" src=\"../images/back.gif\" /></a></td>");
            }
        }
        if (this.page_count > 1) {
            if (current_page == this.page_count) {
                content.push("<td width=\"40\"><img width=\"37\" height=\"15\" src=\"../images/next.gif\"></td>");
                content.push("<td width=\"40\"><img width=\"37\" height=\"15\" src=\"../images/last.gif\"></td>");
            }
            else {
                content.push("<td width=\"40\" name=\"Pager" + (current_page + 1) + "\"><a href=\"javascript:void(null);\" ><img width=\"37\" height=\"15\" src=\"../images/next.gif\" /></a></td>");
                content.push("<td width=\"40\" name=\"Pager" + this.page_count + "\"><a href=\"javascript:void(null);\" ><img width=\"37\" height=\"15\" src=\"../images/last.gif\"></a></td>");
            }
        }
        if (this.page_count > 1) {
            content.push("<td width=\"100\"><div align=\"center\"><span class=\"color_5\">转到第");
            content.push("<input type=\"text\" style=\"height: 12px; width: 40px; border: 1px solid rgb(153, 153, 153);\" ");
            content.push("value=\"\" id=\"l_look_pager_" + this.sql_tag + "\" size=\"4\" name=\"textfield\">");
            content.push("页</span></div></td>");
            content.push("<td width=\"40\"><a href=\"javascript:void(null);\" id=\"btn_look_skip_" + this.sql_tag + "\"><img width=\"37\" height=\"15\" src=\"../images/go.gif\"></td>");
        }

        $("#" + this.status_bar_id).empty().html(content.join(""));

        $("#" + this.status_bar_id).find("td[name^='Pager']").click(function() {
            var page_index = parseInt($(this).attr("name").replace("Pager", ""));
            obj.query_params["Start"] = (page_index - 1) * obj.page_size + 1;
            obj.LoadData(page_index, obj.query_params);
        });
        $("#btn_look_skip_" + obj.sql_tag).click(function() {
            var num = $.trim($("#l_look_pager_" + obj.sql_tag).val());
            if (!num) {
                alert("请输入您要搜索的页数");
                return;
            }
            var page_index = parseInt(num);
            if (page_index <= 0) {
                alert("请输入大于0的整数");
                return;
            } else if (page_index > obj.page_count) {
                alert("您输入的页数超过总页数");
                return;
            }
            obj.query_params["Start"] = (page_index - 1) * obj.page_size + 1;
            obj.LoadData(page_index, obj.query_params);
        });
    }
}
SqlPager.prototype.LoadData = function(page_index, query_params) {
    if (!this.query_params) {
        this.query_params = query_params;
        this.query_params["page_size"] = this.page_size;
    }
    this.query_params["Start"] = page_index;
    var newparams = this.DealParams(this.query_params);
    var obj = this;
    $.ajax({
        type: "post",
        url: obj.web_url,
        data: newparams,
        beforeSend: function(XMLHttpRequest) {
            $("#" + obj.result_id).empty().html("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td align=\"center\" class=\"loading_text\"><img src=\"../images/loading_icon.gif\" /></td></tr></table>");
            $("#" + obj.info_id).empty().html("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td align=\"center\" class=\"loading_text\"><img src=\"../images/loading_icon.gif\" /></td></tr></table>");
        },
        dataType: "json",
        success: function(data) {
            if (data) {
                var total_count = parseInt(data["Count"]);
                var count = parseInt(total_count / obj.page_size);
                obj.page_count = total_count % obj.page_size == 0 ? count : count + 1;
                $("#" + obj.info_id).empty().html("共有&nbsp;" + total_count + "&nbsp;条记录，当前第&nbsp;" + page_index + "/" + obj.page_count + "&nbsp;页");
                obj.Init_status_bar(page_index);
                obj.Display(obj.result_id, data);
            }
            else {
                $("#" + obj.result_id).html("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td align=\"center\" class=\"loading_text\">对不起，没有数据</td></tr></table>");
                $("#" + obj.status_bar_id).empty();
                $("#" + obj.info_id).empty();
                obj.OtherDisplay();
            }
        }
    });
}
SqlPager.prototype.DealParams = function(params) {
    var new_params = {};
    for (var item in params) {
        var l_item = item.toLowerCase();
        new_params[l_item] = params[item];
    }
    return new_params;
}

SqlPager.prototype.Display = function(data) {
    return null;
}

SqlPager.prototype.OtherDisplay = function() {
    return;
}