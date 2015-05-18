$(document).ready(function() {
    ClusterList.InitPager();
    ClusterList.InitOtherClickFn();
    ClusterList.InitCluster();
});
var _clusterlist = new Object;
var ClusterList = _clusterlist.property = {
    queryparams: { "act": "getclusterlist", "strwhere": "", "orderby": " DISTYPE DESC,PARAM,ID", "ajaxString": 1 },
    InnitData: { "page_size": 10, "result_id": "all_data_list", "status_bar_id": "item_pager_list",
        "info_id": "item_info_id", "sql_tag": "item", "web_url": location.href
    },
    IdolInitData: { "page_size": 10, "result_id": "idol_all_data_list", "status_bar_id": "idol_pager_list" },
    IdolQueryParams: { "action": "query", "display_style": 6, "text": "*", "sort": "date", "printfields": "DRETITLE,MYSITENAME,DREDATE,DOMAINSITENAME,C1"
    },
    InitPager: function () {
        var sqlpager = new SqlPager(ClusterList.InnitData);
        sqlpager.Display = function (obj, data) {
            delete data["Count"];
            var content = [];
            var count = 1;
            var start_str = "<td bgcolor=\"#FFFFFF\" align=\"center\" height=\"25\">";
            var left_start_str = "<td bgcolor=\"#FFFFFF\" align=\"left\" height=\"25\">";
            var end_str = "</td>";
            var edit_pic_str = "<img src=\"../images/037.gif\" width=\"9\" height=\"9\" />";
            var del_pic_str = "<img src=\"../images/010.gif\" width=\"9\" height=\"9\" />";
            for (var item in data) {
                var row = data[item];
                content.push("<tr>");
                content.push(start_str + "<input name=\"checkbox\" type=\"checkbox\" value='" + row["id"] + "' />" + row["id"] + end_str);
                content.push(left_start_str + "<span onclick='javascript:listTable.Edit(this,\"editclustername\"," + row["id"] + ",400)'>");
                content.push(unescape(row["clustername"]) + "</span>" + end_str);
                content.push(start_str + "<img class=\"status_img\" id='Audit_" + row["distype"] + "' ");
                content.push("onclick='javascript:listTable.EditStatus(this," + row["id"] + ",\"editdistype\");' pid='" + row["distype"] + "'");
                content.push(" src='../images/status_" + row["distype"] + ".gif' alt=\"\" />" + end_str);
                content.push(start_str + "<span onclick='javascript:listTable.Edit(this,\"editparam\"," + row["id"] + ",20)'>");
                content.push(row["param"] + end_str);
                content.push(start_str + edit_pic_str + "[ <a name=\"category_edit_train\" pid=\"" + row["id"] + "\"");
                content.push(" href=\"javascript:void(null);\">编辑信息</a> ]" + end_str);
                content.push(start_str + edit_pic_str + "[ <a name=\"category_del_train\" pid=\"" + row["id"] + "\"");
                content.push(" href=\"javascript:void(null);\" onClick=\"javascript:listTable.Remove(" + row["id"] + ",'您确定要删除么？',null,null);\">");
                content.push("删除</a> ]" + end_str);
                content.push("</tr>");
                count++;
            }
            $("#" + obj).empty().html(content.join(""));
            ClusterList.InitClickFn(obj);
        }
        sqlpager.LoadData(1, ClusterList.queryparams);
    },
    InitClickFn: function (obj) {
        $("#" + obj).find("a[name='category_edit_train']").click(function () {
            var clusterid = $(this).attr("pid");
            $("#cluster_info_id").attr("pid", clusterid);
            ClusterList.InitClusterInfo(clusterid);
            listTable.ShowFrame("move_cluster", "cluster_edit_frame", "close_cluster_edit_frame");
        });
    },
    InitClusterInfo: function (clusterid) {
        $.post(location.href,
            { "act": "initclusterinfo", "ajaxString": 1, "clusterid": clusterid },
            function (data) {
                if (data) {
                    delete data.SuccessCode;
                    var content = [];
                    for (var item in data) {
                        var entity = data[item];
                        var url = entity["url"];
                        var title = entity["title"];
                        var site = entity["site"];
                        var date = entity["date"];
                        var date = entity["date"];
                        var tag = entity["tag"];
                        content.push(ClusterList.GetPushInfoHtml(url, title, site, date, tag));
                    }
                    $("#push_info_list").empty().html(content.join(""));
                    ClusterList.InitPushClick();
                } else {
                    $("#push_info_list").empty();
                }
            },
            "json"
        );
    },
    InitOtherClickFn: function () {
        ClusterList.InitCategory("category_id", 0, true);

        $("#category_id").change(function () {
            var parentcate = $(this).val().split('_')[1];
            ClusterList.InitCategory("category_child_id", parentcate, false);
        });

        $("#job_name_list").change(function () {
            var job_name = $(this).val();
            if (job_name != "-1") {
                ClusterList.InitClusterId(job_name);
            }
        });

        $("#btn_ref").click(function () {
            ClusterList.InitPager();
        });

        $("#btn_back").click(function () {
            if (confirm("您确定要还原所有信息状态么？")) {
                $.post(location.href,
                    { "act": "restoreclusterlist", "ajaxString": 1 },
                    function (data) {
                        if (data.Success == 1) {
                            ClusterList.InitPager();
                        } else {
                            alert("推送失败！");
                        }
                    },
                    "json"
                );
            }
        });

        $("input[name='search_codition']").click(function () {
            var tag = $(this).val();
            if (tag == "cluster") {
                $("*[name='search_frame']").hide();
                $("*[name='cluster_frame']").show();
            } else {
                $("*[name='search_frame']").show();
                $("*[name='cluster_frame']").hide();
                $("span[id$='_search_frame']").hide();
                $("#" + tag + "_search_frame").show();
            }
            $("#idol_all_data_list").empty();
            $("#idol_pager_list").empty();
        });

        $("#btn_push_info").click(function () {
            var urllist = [];
            var typelist = [];
            $("#push_info_list").find("tr").each(function () {
                var url = unescape($(this).attr("pid"));
                var type = $(this).attr("cate");
                urllist.push(url);
                typelist.push(type);
            });
            if (urllist.length == 0) {
                alert("请选择要推送的信息！");
                return;
            }
            var clusterid = $("#cluster_info_id").attr("pid");
            $.post(location.href,
                { "act": "pushclusterinfo", "ajaxString": 1, "urllist": escape(urllist.join("+")),
                    "typelist": escape(typelist.join("+")), "clusterid": clusterid
                },
                function (data) {
                    if (data.Success == 1) {
                        alert("推送成功！");
                        location.replace(location.href);
                    } else {
                        alert("推送失败！");
                    }
                },
                "json"
            );
        });

        $("#btn_look_cluster").click(function () {
            var job_name = $("#job_name_list").val();
            var job_cluster_id = $("#job_cluster_id").val();
            ClusterList.IdolQueryParams["action"] = "getclusterinfolist";
            ClusterList.IdolQueryParams["job_name"] = escape(job_name);
            ClusterList.IdolQueryParams["job_cluster_id"] = job_cluster_id;
            ClusterList.SearchResult();
        });

        $("#all_news_push").click(function () {
            var content = [];
            $("#idol_all_data_list").find("input:checked").each(function () {
                var url = $(this).val();
                var title = $(this).attr("pid");
                var site = $(this).attr("site");
                var date = $(this).attr("date");
                content.push(ClusterList.GetPushInfoHtml(url, title, site, date));
            });
            if (content.length == 0) {
                alert("请选择要推送的信息！");
                return;
            }
            $("#push_info_list").append(content.join(""));
            ClusterList.InitPushClick();
        });

        $("#btn_look_info").click(function () {
            delete ClusterList.IdolQueryParams["job_name"]
            delete ClusterList.IdolQueryParams["job_cluster_id"];
            var keyword = $.trim($("#keyword").val());
            var action_value = $("input[name='search_codition']:checked").val()
            ClusterList.IdolQueryParams["action"] = action_value;
            if (action_value == "query") {
                if (!keyword) {
                    alert("请输入关键字！");
                    return;
                }
                delete ClusterList.IdolQueryParams["category"];
                ClusterList.IdolQueryParams["text"] = keyword;
            } else {
                delete ClusterList.IdolQueryParams["text"];
                ClusterList.IdolQueryParams["category"] = $("#category_child_id").val().split('_')[0];
            }
            var database = ClusterList.GetDataBase();
           // alert(database);
            if (!database) {
                alert("请选择数据源");
                return;
            }
            if (action_value == "query") {
                ClusterList.IdolQueryParams["database"] = database;
            } else if (action_value == "categoryquery") {
                ClusterList.IdolQueryParams["database"] = database;
            }
            ClusterList.IdolQueryParams["sort"] = $("#sort_style").val();
            var min_date = $.trim($("#start_time").val());
            if (min_date) {
                ClusterList.IdolQueryParams["mindate"] = min_date;
            } else {
                delete ClusterList.IdolQueryParams["mindate"];
            }
            var max_date = $.trim($("#end_time").val());
            if (max_date) {
                ClusterList.IdolQueryParams["maxdate"] = max_date;
            } else {
                delete ClusterList.IdolQueryParams["maxdate"];
            }
            var min_score = $.trim($("#min_score").val());
            if (min_score) {
                ClusterList.IdolQueryParams["minscore"] = min_score;
            } else {
                delete ClusterList.IdolQueryParams["minscore"];
            }
            var infotype = $("#info_type").val();
            if (infotype != "all") {
                ClusterList.IdolQueryParams["fieldtext"] = "MATCH{" + infotype + "}:C1";
            } else {
                delete ClusterList.IdolQueryParams["fieldtext"];
            }
            ClusterList.SearchResult();
        });
    },
    InitPushClick: function () {
        $("a[name='info_delete']").click(function () {
            $(this).parent("td").parent("tr").remove();
        });
        $("input[name='info_record']").click(function () {
            if ($(this).attr("checked")) {
                $("input[name='info_record']").parent("td").parent("tr").attr("cate", "0");
                $(this).parent("td").parent("tr").attr("cate", "1");

            }
        });
    },
    SearchResult: function () {
        var Lpager = new Pager(ClusterList.IdolInitData);
        Lpager.OtherFn = function (totalcount) {
            //IdolNew.InnitWebSite();
            $("a[name='idol_news_store']").empty().html("推送");
            $("a[name='idol_news_store']").click(function () {
                var clusterlistid = $("#cluster_info_id").attr("pid");
                if (undefined == clusterlistid) {
                    alert("请选择所要推送的热点");
                    return;
                }
                var url = $(this).attr("pid");
                var title = $(this).attr("distitle");
                var site = $(this).attr("site");
                var date = $(this).attr("date");
                var content = [];
                content.push(ClusterList.GetPushInfoHtml(url, title, site, date));
                $("#push_info_list").append(content.join(""));
                ClusterList.InitPushClick();
            });
        }
        Lpager.LoadData(1, ClusterList.IdolQueryParams);
    },
    GetPushInfoHtml: function (url, title, site, date, tag) {
        var content = [];
        content.push("<tr name=\"push_info_item\" pid=\"" + url + "\" ");
        if (tag == "1") {
            content.push("cate=\"1\"");
        } else {
            content.push("cate=\"0\"");
        }
        content.push("><td height=\"26\" bgcolor=\"#FFFFFF\" align=\"center\">");
        content.push("<a title=\"" + unescape(title) + "\" target=\"_blank\" href=\"" + unescape(url) + "\">" + unescape(title) + "</a></td>");
        content.push("<td bgcolor=\"#FFFFFF\" align=\"center\">" + unescape(site) + "</td>");
        content.push("<td bgcolor=\"#FFFFFF\" align=\"center\">" + unescape(date) + "</td>");
        content.push("<td bgcolor=\"#FFFFFF\" align=\"center\"><img width=\"9\" height=\"9\" src=\"../images/010.gif\">");
        content.push("[ <a name=\"info_delete\" href=\"javascript:void(null);\">删除</a>]</td>");
        content.push("<td bgcolor=\"#FFFFFF\" align=\"center\"><input type=\"radio\"");
        if (tag == "1") {
            content.push(" checked=\"checked\"");
        }
        content.push(" name=\"info_record\">是</td></tr>");
        return content.join("");
    },
    InitCategory: function (id, parentcate, tag) {
        $.post("../IdolNew/InfoPush.aspx",
            { "act": "innitcategory", "ajaxString": 1, "parentcate": parentcate },
            function (data) {
                if (data) {
                    delete data.Success;
                    var content = [];
                    var count = 1;
                    for (var item in data) {
                        var entity = data[item];
                        if (count == 1 && tag) {
                            ClusterList.InitCategory("category_child_id", entity["id"], false);
                        }
                        content.push("<option value=\"" + entity["categoryid"] + "_" + entity["id"] + "\">");
                        content.push(unescape(entity["categoryname"]) + "</option>");
                        count++;
                    }
                    $("#" + id).empty().html(content.join(""));
                }
            },
            "json"
        );
    },
    GetDataBase: function () {
        var data_base_list = [];
        $("input[name='databse_list']").each(function () {
            if ($(this).attr("checked")) {
                var data_base = $(this).val();
                data_base_list.push(data_base);
            }
        });
        if (data_base_list.length > 0) {
            return data_base_list.join("+");
        } else {
            return Config.DefaultQueryDatabase;
        }
    },
    InitCluster: function () {
        $.post(location.href,
            { "act": "initJobList", "ajaxString": 1 },
            function (data) {
                if (data) {
                    delete data.Success;
                    var content = [];
                    var count = 1;
                    for (var item in data) {
                        if (count == 1) {
                            ClusterList.InitClusterId(item);
                        }
                        content.push("<option value=\"" + item + "\">" + unescape(data[item]) + "</option>");
                        count++;
                    }
                    $("#job_name_list").empty().html(content.join(""));
                }
            },
            "json"
        );
    },
    InitClusterId: function (job_name) {
        $.post(location.href,
            { "act": "initClusterIdList", "ajaxString": 1, "jobname": job_name },
            function (data) {
                if (data) {
                    delete data["Success"];
                    var content = [];
                    for (var item in data) {
                        var entity = data[item];
                        content.push("<option value=\"" + entity["clusterid"] + "\">" + unescape(entity["title"]) + "</option>");
                    }
                    $("#job_cluster_id").empty().html(content.join(""));
                }
            },
            "json"
        );
    }
}