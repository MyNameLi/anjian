// JavaScript Document
function Pager(s) {
    this.start = 1;
    this.page_size = s.page_size == null ? 10 : s.page_size;
    this.status_bar_id = s.status_bar_id;
    this.result_id = s.result_id;
    this.page_count = 20;
    this.status = [];
    this.query_params = null;
    this.total_count = 0;
    this.Start_time = new Date();
    this.end_time = null;
    this.newsList = {};
    this.disdel = s.dis_del == null ? false : s.dis_del;
    this.disedit = s.dis_edit == null ? false : s.dis_edit;
    this.snapurl = "http://" + Config.PortalIP + "/snapshot.html?url=";
    this.dissame = s.dissame == null ? false : s.dissame;
    this.currpage = 0;
}

Pager.prototype.GetStatus = function (current_page) {
    this.status = [];
    if (current_page < 6) {
        this.status.push(["start", 1]);
        if (this.page_count < 10) {
            this.status.push(["end", this.page_count]);
        }
        else {
            this.status.push(["end", 10]);
        }

    }
    else if (current_page >= 6 && current_page <= (this.page_count - 4)) {
        this.status.push(["start", current_page - 5]);
        this.status.push(["end", current_page + 4]);
    }
    else {
        if (this.page_count - 9 > 0) {
            this.status.push(["start", this.page_count - 9]);
        }
        else {
            this.status.push(["start", 1])
        }
        this.status.push(["end", this.page_count]);
    }
}

Pager.prototype.Init_status_bar = function (current_page) {
    var obj = this;
    this.GetStatus(current_page);

    if (this.status) {
        var start_index = this.status[0][1];
        var end_index = this.status[1][1];
        var content = [];
        if (this.page_count > 1) {
            if (current_page > 1) {
                content.push("<span  name=\"Pager1\" style=\"margin-left:10px;\"><a href=\"javascript:void(null);\" >首页</a></span>");
                content.push("<span  name=\"Pager" + (current_page - 1) + "\" style=\"margin-left:10px;\"><a href=\"javascript:void(null);\" >上一页</a></span>");
            }
        }
        for (var i = start_index; i <= end_index; i++) {
            if (i == current_page) {
                content.push("<span class=\"current\" name=\"Pager" + i + "\" style=\"margin-left:10px;\" >" + i + "</span>");
            }
            else {
                content.push("<span name=\"Pager" + i + "\" style=\"margin-left:10px;\" ><a href=\"javascript:void(null);\" >" + i + "</a></span>");
            }
        }
        if (this.page_count > 1) {
            if (current_page < this.page_count) {
                content.push("<span name=\"Pager" + (current_page + 1) + "\" style=\"margin-left:10px;\"><a href=\"javascript:void(null);\" >下一页</a></span>");
            }
        }
        $("#" + this.status_bar_id).html(content.join(""));
        $("#" + this.status_bar_id).find("span").each(function () {
            if ($(this).attr("class") != "current") {
                $(this).click(function () {
                    var page_index = parseInt($(this).attr("name").replace("Pager", ""));
                    obj.query_params["Start"] = (page_index - 1) * obj.page_size + 1;
                    obj.LoadData(page_index, obj.query_params);
                });
            }
        });
    }
}
Pager.prototype.LoadData = function (page_index, query_params) {

    if (!this.query_params) {
        this.query_params = query_params;
        this.query_params["page_size"] = this.page_size;
    }
    this.query_params["Start"] = (page_index - 1) * this.page_size + 1;
    this.query_params["Anticache"] = Math.floor(Math.random() * 100);
    var newparams = this.DealParams(this.query_params);
    var obj = this;
    obj.currpage = page_index;
    $.ajax({
        type: "get",
        url: "../Handler/IdolSearch.ashx",
        data: newparams,
        dataType: "json",
        beforeSend: function (XMLHttpRequest) {
            $("#" + obj.result_id).empty().html("<tr><td colspan=\"5\"><center style=\"font-size:12px;\">数据正在加载中...</center></td></tr>");
        },
        success: function (data) {
            if (data) {
                var total_count = parseInt(data["totalcount"]);
                delete data["Success"];
                delete data["totalcount"];
                obj.newsList = data;
                var news_content = [];
                var url_list = [];
                var start_str = "<td height=\"26\" bgcolor=\"#FFFFFF\" align=\"center\">";
                var start_str_left = "<td height=\"26\" bgcolor=\"#FFFFFF\" align=\"left\">";
                var end_str = "</td>";
                for (var item in data) {
                    var news_row = data[item];
                    var columns = news_row["columns"];
                    news_content.push("<tr>");
                    if (!obj.dissame) {
                        news_content.push(start_str + "<input type=\"checkbox\" site=\"" + news_row["site"] + "\" date=\"" + news_row["time"]);
                        news_content.push("\" pid=\"" + news_row["title"] + "\" value=\"" + news_row["href"] + "\" />" + end_str);
                    }

                    news_content.push(start_str_left + "<a href=\"" + unescape(news_row["href"]) + "\" target=\"_blank\">");
                    news_content.push(unescape(news_row["title"]) + "</a>" + end_str);

                    news_content.push(start_str + unescape(news_row["time"]) + end_str);

                    news_content.push(start_str + unescape(news_row["site"]) + end_str);
                    if (!obj.dissame) {
                        news_content.push(start_str + "<img src=\"../images/037.gif\" width=\"9\" height=\"9\" />");
                        news_content.push("[ <a href=\"javascript:void(null);\" site=\"" + news_row["site"] + "\" date=\"" + news_row["time"] + "\"");
                        news_content.push(" cate=\"" + unescape(columns) + "\" distitle=\"" + news_row["title"] + "\" name=\"idol_news_store\" pid=\"" + news_row["href"] + "\">");
                        if (!columns) {
                            news_content.push("栏目定制");
                        } else {
                            news_content.push("修改栏目定制");
                        }
                        news_content.push("</a>]" + end_str);
                    }

                    if (obj.disdel) {
                        news_content.push(start_str + "<img src=\"../images/037.gif\" width=\"9\" height=\"9\" />");
                        news_content.push("[ <a href=\"javascript:void(null);\"");
                        news_content.push("  name=\"idol_news_delete\" pid=\"" + news_row["href"] + "\">");
                        news_content.push("删除");
                        news_content.push("</a>]" + end_str);
                    }
                    if (obj.disedit) {
                        news_content.push(start_str + "<img src=\"../images/037.gif\" width=\"9\" height=\"9\" />");
                        news_content.push("[ <a href=\"javascript:void(null);\"");
                        news_content.push("  name=\"idol_news_edit\" pid=\"" + news_row["href"] + "\">");
                        news_content.push("编辑");
                        news_content.push("</a>]" + end_str);
                    }
                    if (!obj.dissame) {
                        news_content.push(start_str + unescape(news_row["weight"]) + end_str);
                        news_content.push(start_str + "<img src=\"../images/037.gif\" width=\"9\" height=\"9\" />");
                        news_content.push("[ <a href=\"" + obj.snapurl + news_row["href"] + "\"");
                        news_content.push(" target=\"_blank\" >");
                        news_content.push("查看快照");
                        news_content.push("</a>]" + end_str);

                        news_content.push("<td height=\"26\" name=\"dis_same_news_td\" style=\"display:none;\" ");
                        news_content.push(" bgcolor=\"#FFFFFF\" align=\"center\"><a href=\"javascript:void(null);\" ");
                        news_content.push(" name=\"dis_look_same_news\" pid=\"" + news_row["href"] + "\" ");
                        news_content.push("cate=\"" + news_row["samenum"] + "\">共有<b style=\"color:red;\">" + news_row["samenum"] + "</b>条新闻</a>" + end_str);

                        news_content.push("<td height=\"26\" style=\"display:none;\" name=\"column_article_list\" ");
                        news_content.push("pid=\"" + news_row["href"] + "\" bgcolor=\"#FFFFFF\" align=\"center\">&nbsp;" + end_str);
                    }
                    news_content.push("</tr>");
                    url_list.push("'" + unescape(news_row["href"]) + "'");
                }
                $("#" + obj.result_id).empty().html(news_content.join(""));
                var count = parseInt(total_count / obj.page_size);
                obj.page_count = total_count % obj.page_size == 0 ? count : count + 1;
                obj.Init_status_bar(page_index);
                obj.total_count = total_count;
                obj.OtherFn(total_count);
                obj.DisFn(url_list.join(","));

            }
            else {
                $("#" + obj.result_id).html("<tr><td colspan=\"5\"><center style=\"font-size:12px;\">对不起，暂时没有数据</center></td></tr>");
                $("#" + obj.status_bar_id).empty();
                obj.OtherFn(0);
            }
        }
    });
}
Pager.prototype.DealParams = function (params) {
    var new_params = {};
    for (var item in params) {
        var l_item = item.toLowerCase();
        new_params[l_item] = params[item];
    }
    return new_params;
}
Pager.prototype.OtherFn = function (totalcount) {
    return null;
}
Pager.prototype.DisFn = function (urllist) {
    return null;
}