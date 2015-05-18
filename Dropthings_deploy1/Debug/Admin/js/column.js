$(document).ready(function() {
    column.InnitTree();
    column.Innit();
});

var _column = new Object;
var column = _column.property = {
    posturl: location.href,
    postparams: {},
    postNewsIdlist: null,
    ckeditor: null,
    postdata: null,
    rulestr: "<li style=\"text-align:left;height:25px;line-height:25px;\"><a target=\"_blank\" href=\"{url}\">{title}</a><span>{date}</span></li>",
    Innit: function() {
        $("#close_edit_frame").click(function() {
            $("#column_edit_frame").hide();
            location.replace(location.href);
        });
        $("#close_content_frame").click(function() {
            $("#column_content_frame").hide();
        });
        $("input[name='valueList']").css("width", "300px");

    },
    InnitTree: function() {
        $.post(column.posturl,
            { "act": "innittree", "ajaxString": 1 },
            function(data) {
                if (data["Error"] == 1) {
                    alert("加载栏目数失败，原因" + unescape(data["ErrorStr"]));
                }
                if (data["Success"] == 1) {
                    $("#column_tree").empty().html(unescape(data["TreeStr"]));
                    column.InnitTreeClickFn();
                }
            },
            "json"
        );
    },
    InnitTreeClickFn: function() {
        $("#add_one").click(function() {
            $("#btn_add").show();
            $("#btn_reset").show();
            $("#btn_edit").hide();
            listTable.Rest();
            column.ShowEditFrame("move_column", "column_edit_frame");
        });
        $("a[name='column_edit_content']").click(function() {
            var id = $(this).attr("pid");
            $.post(column.posturl,
                { "act": "innittelemplante", "ajaxString": 1, "idList": id },
                function(data) {
                    if (data["Error"] == 1) {
                        alert("加载失败，原因" + unescape(data["ErrorStr"]));
                    }
                    if (data["Success"] == 1) {
                        delete data["Success"];
                        var content = [];
                        var indexcount = 1;
                        for (var item in data) {
                            var row = data[item];
                            content.push("<li><span>" + indexcount + "、</span>");
                            content.push("<span>" + unescape(row["TelemplateName"]) + "</span>&nbsp;&nbsp;");
                            content.push("<span><a href=\"javascript:void(null);\"");
                            content.push(" name=\"edit_template\" pid=\"" + row["ColumnID"] + "_" + row["TelemplateID"] + "_");
                            content.push(row["TelemplateType"] + "\">编辑</a></span>");
                            content.push("</li>");
                            indexcount++;
                        }
                        $("#telemplate_list").empty().html(content.join(""));
                        $("#column_edit_frame").hide();
                        $("#edit_content_area").empty();
                        $("#btn_edit_content").hide();
                        column.InnitTemplateEdit();
                        column.ShowEditFrame("move_content_frame", "column_content_frame");
                    }
                },
                "json"
            );
        });

        $("a[name='column_edit']").click(function() {
            var id = $(this).attr("pid");
            $.post(column.posturl,
                { "act": "innitedit", "ajaxString": 1, "idList": id },
                function(data) {
                    if (data["Error"] == 1) {
                        alert("加载失败，原因" + unescape(data["ErrorStr"]));
                    }
                    if (data["Success"] == 1) {
                        delete data["Success"];
                        for (var item in data) {
                            $("#" + item).val(unescape(data[item]));
                        }
                        $("#btn_add").hide();
                        $("#btn_reset").hide();
                        $("#btn_edit").show();
                        var Linput = document.createElement("INPUT");
                        $(Linput).val(id.toString());
                        $(Linput).attr("id", "ID");
                        $(Linput).attr("pid", "valueList");
                        $(Linput).css("display", "none");
                        $("body").append($(Linput));
                        $("#column_content_frame").hide();
                        column.ShowEditFrame("move_column", "column_edit_frame");
                    }
                },
                "json"
            );
        });
        $("a[name='column_delete']").click(function() {
            if (confirm("您确定要删除该栏目么？")) {
                var id = $(this).attr("pid");
                var len = $("tr[name='column_parent_" + id + "']").length;
                if (len > 0) {
                    alert("该栏目下有子栏目，不能删除！");
                    return;
                }

                var current_obj = this;
                $.post(column.posturl,
                    { "act": "remove", "ajaxString": 1, "idList": id },
                    function(data) {
                        if (data["Error"] == 1) {
                            alert("删除失败，原因" + unescape(data["ErrorStr"]));
                        }
                        if (data["Success"] == 1) {
                            $("#column_list_" + id).remove();
                        }
                    },
                    "json"
                );
            }
        });

        $("#btn_edit_content").click(function() {
            var html_str = null;
            if (column.ckeditor) {
                html_str = column.ckeditor.getData();
            } else {
                html_str = column.postdata;
            }
            column.postparams["htmlstr"] = escape(html_str);
            column.postparams["NewsIdList"] = escape(column.postNewsIdlist);
            $.post(column.posturl,
                column.postparams,
                function(data) {
                    if (data["Error"] == 1) {
                        alert("编辑失败，原因" + unescape(data["ErrorStr"]));
                    }
                    if (data["Success"] == 1) {
                        alert("保存成功");
                    }
                },
                "json"
            );
        });
    },
    ShowEditFrame: function(child_div, parent_div) {
        $("#" + parent_div).show();
        var iframe_width = $(document).width();
        var l_width = parseInt($("#" + parent_div).width()) / 2;
        iframe_width = parseInt(iframe_width / 2) - l_width;
        //$("#" + parent_div).css({ "position": "absolute", "top": "50px", "left": iframe_width + "px", "background": "#f9f5f5" });
        $("#" + parent_div).css({ "position": "absolute", "top": "0px", "left": iframe_width + "px" });
        $("#" + parent_div).show();
        var div_move = new divMove(child_div, parent_div);
        div_move.init();
    },
    InnitTemplateEdit: function() {
        $("a[name='edit_template']").click(function() {
            var infolist = $(this).attr("pid").split("_");
            var column_id = infolist[0];
            var templante_id = infolist[1];
            var templante_type = infolist[2];
            $.post(column.posturl,
                { "act": "innitHtmlStr", "ajaxString": 1, "idList": column_id, "templante_id": templante_id },
                function(data) {
                    if (data["Error"] == 1) {
                        alert("加载失败，原因" + unescape(data["ErrorStr"]));
                    }
                    if (data["Success"] == 1) {
                        column.InnitDisplayContent(unescape(data["HtmlStr"]), column_id, templante_id, templante_type, unescape(data["RuleStr"]));
                    }
                },
                "json"
            );
        });
    },
    InnitDisplayContent: function(htmlstr, column_id, templante_id, type, rulestr) {
        $("#btn_edit_content").show();
        if (CKEDITOR.instances['ckeditor']) {
            CKEDITOR.remove(CKEDITOR.instances['ckeditor']);
            column.ckeditor = null;
        }
        switch (type) {
            case "1":
                column.InnitCKEditorContent(htmlstr);
                break;
            case "2":
                column.InnitListContent(htmlstr, rulestr);
                break;
            case "3":
                column.InnitListContent(htmlstr, rulestr);
                break;
            case "4":
                column.InnitClusterContent();
                break;
            default:
                break;
        }
        column.postparams = { "act": "EditHtmlStr", "ajaxString": 1, "idList": column_id,
            "templante_id": templante_id, "htmlstr": "", "NewsIdList": ""
        };
    },
    InnitClusterContent: function() {
        
    },
    InnitCKEditorContent: function(content) {
        var div = document.createElement("DIV");
        $(div).attr("id", "ckeditor");
        $("#edit_content_area").empty();
        $("#edit_content_area").append("<div  class=\"layer_T\"><h1><b>内容编辑</b></h1><span class=\"btn\"></span><div class=\"clear\"></div></div>");
        $("#edit_content_area").append($(div));
        var config = { height: 300,
            filebrowserBrowseUrl: '../ckfinder/ckfinder.html',
            filebrowserImageBrowseUrl: '../ckfinder/ckfinder.html?Type=Images',
            filebrowserUploadUrl: '../ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files'
        };
        column.ckeditor = CKEDITOR.replace('ckeditor', config);
        column.ckeditor.setData(content);
    },
    InnitListContent: function(htmlstr, rulestr) {
        $("#edit_content_area").empty();
        column.postdata = null;
        column.postNewsIdlist = null;
        column.CreateSerchFrame(rulestr, htmlstr);
    },
    CreateSerchFrame: function(rulestr, htmlstr) {
        $("#edit_content_area").append("<div  class=\"layer_T\"><h1><b>新闻列表</b></h1><span class=\"btn\"></span><div class=\"clear\"></div></div>");
        var div = document.createElement("DIV");
        $(div).css({ "width": "95%", "height": "auto" });
        var content = [];
        //content.push("<center style=\"font-size:14px;font-weight:bold;\">新闻列表</center>");
        content.push("<div id=\"news_list\"></div>");
        content.push("<center><div id=\"pager_list\"></div></center>");
        content.push("<div style=\"text-align:left;\"><input id=\"save_choose_news\" type=\"button\" value=\"选中文章\" />&nbsp;&nbsp;");
        content.push("<input id=\"delete_choose_news\" type=\"button\" value=\"清除文章\" /></div>");

        $(div).html(content.join(""));
        $("#edit_content_area").append($(div));
        $("#edit_content_area").append("<div class=\"clear\"></div>");
        $("#edit_content_area").append("<div class=\"layer_T\"><h1><b>选中文章列表</b></h1><span class=\"btn\"></span><div class=\"clear\"></div></div>");
        var newsdiv = document.createElement("DIV");
        $(newsdiv).css({ "width": "95%", "height": "auto" });
        $("#edit_content_area").append($(newsdiv));
        if (htmlstr) {
            $(newsdiv).empty().html(htmlstr);
        }
        $("#delete_choose_news").click(function() {
            column.postdata = "";
            $(newsdiv).empty();
        });
        var initData = { "page_size": 5, "result_id": "news_list", "status_bar_id": "pager_list" };
        var params = {};
        var Lpager = new Pager(initData);
        Lpager.LoadData(1, params);
        $("#save_choose_news").click(function() {
            var html_str = [];
            var idlist = [];
            $("#news_list").find("input:checked").each(function() {
                var key = $(this).attr("pid");
                var entity = Lpager.newsList[key];
                var str = column.ReplaceAll(column.rulestr, "{title}", unescape(entity["title"]));
                str = column.ReplaceAll(str, "{url}", unescape(entity["url"]));
                str = column.ReplaceAll(str, "{date}", unescape(entity["date"]));
                html_str.push(str);
                idlist.push(unescape(entity["id"]));
            });
            $(newsdiv).append("<ul>" + html_str.join("") + "</ul>");
            if (column.postdata) {
                column.postdata = column.postdata + html_str.join("");
            } else {
                column.postdata = html_str.join("");
            }
            if (column.postNewsIdlist) {
                column.postNewsIdlist = column.postNewsIdlist + "," + idlist.join(",");
            } else {
                column.postNewsIdlist = idlist.join(",");
            }
        });
    },
    ReplaceAll: function(baseStr, Regx, ReplaceStr) {
        return baseStr.replace(new RegExp(Regx, "gm"), ReplaceStr);
    },
    CreateCommonFrame: function() {
        var content = [];
        content.push("<span class=\"layer_line\"></span><div class=\"layer_outer\" style=\"background:white;\">");
        content.push("<span class=\"layer_top_L\"></span><span class=\"layer_top_R\"></span>");
        content.push("<div class=\"layer_inner\">");
        content.push("<a class=\"btn_close\" href=\"javascript:void(null);\" id=\"close_window\"></a>");
        content.push("<div class=\"clear\"></div>");
        content.push("<div class=\"layer_C\" id=\"frame_content\"></div>");
        content.push("</div><span class=\"layer_bottom_L\"></span>");
        content.push("<span class=\"layer_bottom_R\"></span></div><span class=\"layer_line\"></span>");
        return content.join("");
    }
}