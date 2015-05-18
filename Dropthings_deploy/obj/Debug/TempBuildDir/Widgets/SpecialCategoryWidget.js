//$(function() {
//    var str_where = "ParentCate =263 and IsEffect=1";
//    Common.CategoryMenu("category_menu", "left-center-nav-ul-link", "left-center-nav-ul", str_where, "193");
//});

var _Special = new Object;
var Special = _Special.property = {
    transitemstyle: { "border": "1px solid #ffffff", "background": "url(img/line_orange.gif) repeat-x left top", "color": "#ffffff" },
    linecolor: "#A4C7DD",
    objId: { "news": "news", "blog": "blog", "n": "nagtive", "BBS": "furm", "reson": "event_reson", "measure": "event_measure", "about": "event_about" },
    Load: function() {
        if ($("#title_cue").size() < 1) {
            $("div.column").eq(0).prepend('<div class="rogjt-top-b" style="display:none;"><h1 id="title_cue"></h1></div>');
        }
        var s = { "tab_news": "content_news", "tab_furm": "content_furm", "tab_blog": "content_blog" };
        Common.TabControl(s, "tab_on", "tab_off");

        var event = { "event_reson": "content_event_reson", "event_measure": "content_event_measure", "event_about": "content_event_about" };
        Common.TabControl(event, "tab_on", "tab_off");

        Common.CategoryMenuClick = function(data, text) {
            $("#title_cue").attr("pid", data);
            $("#title_cue").empty().html(text.split("(")[0]);
            var category_id = data.split("_")[0];
            var parent_cate = data.split("_")[1];
            var query_text = text;
            var from_date = Special.GetTimeStr(-30); //$("#from_date").val();
            var to_date = Special.GetTimeStr(0); // $("#to_date").val();
            Special.InitPicData(category_id);
            var params = { "category_id": category_id, "from_date": from_date, "to_date": to_date, "Start": 1, "page_size": 5 };
            Special.GetSpecialData(params);
            //if (parent_cate == "202") {
            //   $("#category_event").parents("div.widget").show();
            //    $("#trans_route").parents("div.widget").show();
            //    Special.InitTransData(category_id, 1);
            //}
            //else {
            //    $("#category_event").parents("div.widget").hide();
            //    //$("#trans_route").parents("div.widget").hide();
            //}
            Special.InitTransData(category_id, 1);

        }
        var config = Common.Config;
        var str_where = config["categorycondition"];
        Common.CategoryMenu("category_menu", "left-center-nav-ul-link", "left-center-nav-ul", str_where, "106");


        //$("#from_date").val(Special.GetTimeStr(-30));
        //$("#to_date").val(Special.GetTimeStr(0));
        var id = $.query.get('id');
        var name = $.query.get('name');
        if (id) {
            Special.InitData(id, name);
            Special.BtnLookData();
            Special.InitPicData(id.split('_')[0]);
        }
        else {
            Special.InitData(config["categoryinnit"]["categoryid"] + "_" + config["categoryinnit"]["parentcate"], config["categoryinnit"]["categoryname"]);
            Special.BtnLookData();
            Special.InitPicData(config["categoryinnit"]["categoryid"]);
        }

        $("a").focus(function() {
            $(this).blur();
        });

        Special.InitTransData(config["categoryinnit"]["categoryid"], 1);
    },
    GetTimeStr: function(day) {
        var date = new Date();
        var time = new Date(date.getTime() + day * 1000 * 60 * 60 * 24);
        return time.getFullYear() + "-" + (time.getMonth() + 1) + "-" + time.getDate();
    },
    InitTransData: function(category, index) {
        $("#transcs").find("div").remove();
        $("#transcs").find("canvas").remove();
        var transobj = document.createElement("canvas");
        transobj.width = 680;
        transobj.height = 200;
        if ($.browser.msie) {
            transobj = window.G_vmlCanvasManager.initElement(transobj);
        }

        $("#transcs").empty().append(transobj);
        var totalData = {};
        var sequece = 1;
        var url = "Handler/GetSiteTrend.ashx";
        $.post(url,
		        { "category": category },
		        function(data) {
		            if (data) {
		                var count = data["Count"];
		                delete data["Count"];
		                var content = [];
		                var l_count = 1;
		                for (var item in data) {
		                    var info = unescape(data[item]).split(",");
		                    var l_item = [];
		                    for (var i = 0, j = info.length; i < j; i++) {
		                        l_item.push(info[i]);
		                    }
		                    content.push([item, l_item]);
		                    if (l_count == 5) {
		                        totalData["data_" + sequece] = content;
		                        l_count = 0;
		                        content = [];
		                        sequece++;
		                    }
		                    l_count++;
		                }
		                if (content.length > 0) {
		                    totalData["data_" + sequece] = content;
		                } else {
		                    sequece = sequece - 1;
		                }
		                var l_data = totalData["data_" + index];
		                if (l_data) {
		                    var TransMap = new TransRoute(transobj, l_data, Special.transitemstyle, Special.linecolor);
		                    TransMap.Init();
		                    var prev_a = $("<a></a>");
		                    var next_a = prev_a.clone(); // document.createElement("A");
		                    prev_a.html("<image src=\"img/trans_prev.gif\" />");
		                    prev_a.attr("pid", "0");
		                    prev_a.attr("title", "上一个节点");
		                    prev_a.css({ "top": "82px", "left": "-20px", "position": "absolute" });
		                    next_a.html("<image src=\"img/trans_next.gif\" />");
		                    next_a.attr("pid", "2");
		                    next_a.attr("title", "下一个节点");
		                    next_a.css({ "top": "82px", "right": "-23px", "position": "absolute" });

		                    prev_a.click(function() {
		                        var page = parseInt(prev_a.attr("pid"));
		                        /*if (page == 0) {
		                        alert("已经是传播链的最顶端");
		                        return;
		                        } else {*/
		                        if (page == 1) {
		                            prev_a.hide();
		                        }
		                        prev_a.attr("pid", page - 1);
		                        next_a.attr("pid", page + 1).show();
		                        var ll_data = totalData["data_" + page];
		                        Special.GetOtherTransData(ll_data);
		                        /*}*/
		                    });
		                    next_a.click(function() {
		                        var page = parseInt(next_a.attr("pid"));
		                        /*if (page == (sequece + 1)) {
		                        alert("已经是传播链的最末端");
		                        return;
		                        } else {*/
		                        if (page == sequece) {
		                            next_a.hide();
		                        }
		                        prev_a.attr("pid", page - 1).show();
		                        next_a.attr("pid", page + 1);
		                        var ll_data = totalData["data_" + page];
		                        Special.GetOtherTransData(ll_data);
		                        /*}*/
		                    });
		                    prev_a.hide();
		                    $("#transcs").append(prev_a);
		                    $("#transcs").append(next_a);
		                }
		            }
		        },
		        "json"
	        );
    },
    GetOtherTransData: function(data) {
        $("#transcs").find("div").remove();
        $("#transcs").find("canvas").remove();
        var transobj = document.createElement("canvas");
        transobj.width = 680;
        transobj.height = 200;
        if ($.browser.msie) {
            transobj = window.G_vmlCanvasManager.initElement(transobj);
        }

        $("#transcs").append(transobj);
        var l_TransMapMap = new TransRoute(transobj, data, Special.transitemstyle, Special.linecolor);
        l_TransMapMap.Init();
    },
    InitData: function(data, text) {
        $("#title_cue").attr("pid", data);
        $("#title_cue").empty().html(text.split("(")[0]);
        if (data.split("_")[1] == "202") {
            $("#category_event").show();
        }
        else {
            $("#category_event").hide();
        }
        Special.LookNews(data, text);
    },
    BtnLookData: function() {
        var current_obj = this;
        $("#look_data").click(function() {
            var data = $("#title_cue").attr("pid");
            var text = $("#title_cue").html();
            current_obj.LookNews(data, text);
            current_obj.InitPicData(data.split("_")[0]);
        });
    },
    LookNews: function(data, text) {
        var from_date = Special.GetTimeStr(-30); //$("#from_date").val();
        var to_date = Special.GetTimeStr(0); // $("#to_date").val();
        var category_id = data.split("_")[0];
        var parent_cate = data.split("_")[1];
        var params = { "category_id": category_id, "from_date": from_date, "to_date": to_date, "Start": 1, "page_size": 5 };
        Special.GetSpecialData(params);
    },
    GetSpecialData: function(params, obj_id) {
        $.ajax({
            type: "post",
            url: "Handler/SpecialData.ashx",
            data: params,
            dataType: "json",
            beforeSend: function(XMLHttpRequest) {
                if (obj_id) {
                    $("#content_" + obj_id).empty().html("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" height=\"180\" width=\"100%\"><tr><td align=\"center\" class=\"widget_loading\">数据正在加载中...</td></tr></table>");
                }
                else {
                    $("[id^='content_']").empty().html("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" height=\"180\" width=\"100%\"><tr><td align=\"center\" class=\"widget_loading\">数据正在加载中...</td></tr></table>");
                }
            },
            success: function(data) {
                for (var item in data) {
                    if (unescape(data[item]) != "") {
                        $("#" + item).empty().html(unescape(data[item]));
                        $("#" + item).find("[name='content_prev']").each(function() {
                            $(this).click(function() {
                                var l_params = $(this).attr("pid").split('_');
                                params["type"] = l_params[0];
                                params["Start"] = l_params[1];
                                Special.GetSpecialData(params, Special.objId[l_params[0]]);
                            });
                        });
                        $("#" + item).find("[name='content_next']").each(function() {
                            $(this).click(function() {
                                var l_params = $(this).attr("pid").split('_');
                                params["type"] = l_params[0];
                                params["Start"] = l_params[1];
                                Special.GetSpecialData(params, Special.objId[l_params[0]]);
                            });
                        });
                    }
                    else {
                        $("#" + item).empty().html("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" height=\"180\" width=\"100%\"><tr><td align=\"center\" class=\"widget_none\">暂时没有数据</td></tr></table>");
                    }
                }
                $(".left-center").css("height", $(".right").height() - 27);
            }
        });
    },
    InitPicData: function(category_id) {
        /*var current_obj = this;
        var from_date = Special.GetTimeStr(-30); //$("#from_date").val();
        var to_date = Special.GetTimeStr(0); //$("#to_date").val();

        $.ajax({
        type: "post",
        url: "Handler/TrendData.ashx",
        data: { "category_id": category_id, "from_date": from_date, "to_date": to_date },
        dataType: "json",
        beforeSend: function(XMLHttpRequest) {
        $("#placeholder").empty().html("<div class=\"widget_loading\"><span class=\"btn_loading\">数据正在加载中...</span></div>");
        $("#overview").empty().html("<div class=\"widget_loading\"><span class=\"btn_loading\">数据正在加载中...</span></div>");
        },
        success: function(data) {
        var trend_data = [];
        for (var item in data) {
        trend_data.push([parseInt(item), parseInt(data[item])]);
        }
        current_obj.GetTrendPic(trend_data);
        }
        });*/
        if ($("#trend_data_pic")) {
            var flash_url = "../Chart/FCF_MSLine.swf";
            var victor_id = "trend_data_pic";
            var data_url = "../Handler/TrendData.ashx?category_id=" + category_id;
            var start_time = $.trim($("#from_date").val());
            if (start_time) {
                data_url = data_url + "," + start_time;
            }
            var end_time = $.trim($("#to_date").val());
            if (end_time) {
                data_url = data_url + "," + end_time;
            }
            //var data_url = "xmldata/MSLine.xml";
            var width = 950;
            var height = 500;
            FlashChart.InsertFusionChartsByDataUrl(flash_url, victor_id, data_url, width, height);
        }
    },

    GetTrendPic: function(trend_data) {
        var d = trend_data;
        if (d) {

            /* 对placeholder的canvas进行初始化 */
            $.plot($("#TrendPlaceholder"), [d], { xaxis: { ticks: [] }, series: { lines: { show: true, lineWidth: 1 }, shadowSize: 0 }, grid: { color: "#91bae1", borderWidth: 1} });


            /* 对选择器overview进行初始化 */
            $.plot($("#overview"), [d], { xaxis: { mode: "time", timeformat: "%y-%m-%d" }, yaxis: { ticks: [] }, selection: { mode: "x" }, series: { lines: { show: true, lineWidth: 1 }, shadowSize: 0 }, grid: { color: "#91bae1", borderWidth: 1} });

            $("#overview").bind("plotselected", function(event, ranges) {
                var level = 0;
                var min_date = ranges.xaxis.from;
                var max_date = ranges.xaxis.to;
                var date_gap = (max_date - min_date) / 86400000;
                var l = [];
                if (date_gap <= 70) {
                    level = 1;
                    $("#date_unit").empty().html("天");
                }
                else if (date_gap > 70 && date_gap <= 600) {
                    level = 7;
                    $("#date_unit").empty().html("周");
                }
                else {
                    level = 30;
                    $("#date_unit").empty().html("月");
                }

                l = get_Data(d, min_date, max_date, level);

                $("#time_span").empty().html(GetFormatTime(new Date(min_date)) + "至" + GetFormatTime(new Date(max_date)));
                /* 根据重新组合的数据l再绘图 */
                $.plot($("#TrendPlaceholder"), [l], {
                    xaxis: {
                        mode: null,
                        min: ranges.xaxis.from,
                        max: ranges.xaxis.to,
                        ticks: l
                    },
                    series: { lines: { show: true, lineWidth: 1 }, shadowSize: 0, points: { show: true} },

                    grid: { color: "#91bae1", borderWidth: 1 }

                });

                innit_tag(l, level);
            });
        }
        /* 根据选择的min_date、max_date对原有数据d进行再组合 */
        function get_Data(d, min_date, max_date, level) {
            var list = [];
            var count = 0;
            var article_count = 0;
            for (var i = 0; i < d.length; i++) {
                if (d[i][0] >= min_date && d[i][0] <= max_date) {

                    article_count += d[i][1];
                    count++;
                    if (count % level == 0) {
                        list.push([d[i][0], article_count]);
                        count = 0;
                        article_count = 0;
                    }
                }
            }
            return list;
        }

        /* 根据X轴数据生成覆盖在placeholder上DIV，并初始化mouseover事件 */
        function innit_tag(d, level) {
            var width = $("#TrendPlaceholder").width();
            var height = $("#TrendPlaceholder").height();
            var div = document.createElement("DIV");
            $(div).css({ "position": "absolute", "width": width, "height": height, "left": "-2px", "top": "0px" });
            var list = $("#TrendPlaceholder").find(".tickLabel");
            for (var i = 0; i < list.length; i++) {
                var l_right = $(list[i]).position().left;
                var Len = list.length - 1;
                var plot_left = $(list[Len]).width();
                if (l_right != 0) {
                    try {
                        var value = $(list[i]).html();
                        $(list[i]).empty();
                        var l_div = list[i];
                        var prev_time = GetFormatTime(new Date(d[i][0]));
                        var next_time = GetFormatTime(new Date(d[i][0] + 86400000 * (level - 1)));
                        var time_str;
                        if (next_time == prev_time) {
                            time_str = prev_time;
                        }
                        else {
                            time_str = prev_time + "/" + next_time;
                        }
                        $(l_div).attr("date", time_str);
                        $(l_div).attr("pid", value);
                        $(l_div).attr("id", "item_" + i);
                        $(l_div).css({ "top": 5, "height": "275px", "float": "left", "cursor": "pointer" });

                        var item = GetPlotItem(plot_data, i);

                        var left = parseInt(item.series.xaxis.p2c(item.datapoint[0])) + plot_left + 2;
                        var top = parseInt(item.series.yaxis.p2c(item.datapoint[1])) + 4;
                        item_div = document.createElement("DIV");
                        $(item_div).attr("id", "litem_" + i);
                        $(item_div).css({ "width": "8px", "height": "8px", "overflow": "hidden", "position": "absolute", "left": left, "top": top, "background": "black", "cursor": "pointer", "display": "none" });
                        $("#TrendPlaceholder").append(item_div);
                        var node_text = document.createElement("DIV");
                        $(node_text).attr("id", "node_text_" + i);
                        if (left < 580)
                            $(node_text).css({ "width": "180px", "height": "auto", "overflow": "hidden", "position": "absolute", "left": left + 15, "top": top, "display": "none", "text-align": "left", "border": "1px solid gray" });
                        else
                            $(node_text).css({ "width": "180px", "height": "auto", "overflow": "hidden", "position": "absolute", "left": left - 195, "top": top, "display": "none", "text-align": "left", "border": "1px solid gray" });
                        $(node_text).html("时间：" + time_str + "<br />文章数：" + value);
                        $("#TrendPlaceholder").append(node_text);
                        div.appendChild(l_div);
                    }
                    catch (e) {

                    }
                }

            }
            $("#TrendPlaceholder").append(div);

            var current_item_num;

            /* 对placeholder的mouseover时间进行初始化 */
            $(document).mouseover(function(e) {
                var type = $(e.target).attr("id").split('_');
                if (type[0] == "item") {
                    if (!current_item_num) {
                        current_item_num = type[1];
                    }
                    else {
                        if (type[1] != current_item_num) {
                            $("#litem_" + current_item_num).hide();
                            $("#node_text_" + current_item_num).hide();
                            current_item_num = type[1];
                        }
                    }
                    $("#litem_" + type[1]).show();
                    $("#node_text_" + type[1]).show();
                }
            });
        }
        /* 对时间进行格式化 返回格式yyyy-mm-dd */
        function GetFormatTime(date) {
            var year = date.getFullYear();
            var month = date.getMonth() + 1;    //js从0开始取 
            var date1 = date.getDate();
            return year + "-" + month + "-" + date1;
        }
        /* 获取根据索引point_no获取对应元素 */
        function GetPlotItem(series, point_no) {
            i = 0;
            j = point_no;
            ps = series[i].datapoints.pointsize;

            return { datapoint: series[i].datapoints.points.slice(j * ps, (j + 1) * ps),
                dataIndex: j,
                series: series[i],
                seriesIndex: i
            };
        }
    }
}


var _flashchart = new Object;
var FlashChart = _flashchart.property = {
    GetLineXmlData: function(headparams, data, disYcount, totalcount) {
        var content = [];
        content.push(FlashChart.GetXmlHead(headparams));
        var dispan = parseInt(totalcount / disYcount);
        var count = 0;
        for (var item in data) {
            if (count % dispan == 0) {
                content.push("<set name='" + item + "' value='" + parseInt(data[item]) + "'/>");
            } else {
                content.push("<set name='' value='" + parseInt(data[item]) + "'/>");
            }
            count++;
        }
        content.push("</graph>");
        return content.join("");
    },
    GetXmlHead: function(params) {
        var content = [];
        content.push("<?xml version='1.0' encoding='gb2312'?>");
        content.push("<graph");
        /*设置标题*/
        var caption = params.caption;
        if (caption) {
            content.push(" caption='" + caption + "' ");
        }
        /*设置X轴名称*/
        var xaxisname = params.xaxisname;
        if (xaxisname) {
            content.push(" xAxisName='" + xaxisname + "'");
        }
        /*设置小数位的位数，默认为0*/
        var decimalprecision = params.decimalprecision == null ? "0" : params.decimalprecision;
        content.push(" decimalPrecision='" + decimalprecision + "'");

        /*设置画布边框厚度，默认为1*/
        var canvasborderthickness = params.canvasborderthickness == null ? "1" : params.canvasborderthickness;
        content.push(" canvasBorderThickness='" + canvasborderthickness + "'");

        /*设置画布边框颜色，默认为a5d1ec*/
        var canvasbordercolor = params.canvasbordercolor == null ? "a5d1ec" : params.canvasbordercolor;
        content.push(" canvasBorderColor='" + canvasbordercolor + "'");

        /*设置图标字体大小，默认为12*/
        var basefontsize = params.basefontsize == null ? "12" : params.basefontsize;
        content.push(" baseFontSize='" + basefontsize + "'");

        /*设置是否格式化数据（如3000为3K），默认为0(否)*/
        var formatnumberscale = params.formatnumberscale == null ? "0" : params.formatnumberscale;
        content.push(" formatNumberScale='" + formatnumberscale + "'");

        /*设置是否显示横向坐标轴(x轴)标签名称，默认为1(是)*/
        var shownames = params.shownames == null ? "1" : params.shownames;
        content.push(" showNames='" + shownames + "'");

        /*设置是否在图表显示对应的数据值，默认为0（否）*/
        var showvalues = params.showvalues == null ? "0" : params.showvalues;
        content.push(" showValues='" + showvalues + "'");

        /*设置是否在横向网格带交替的颜色，默认为1（是）*/
        var showalternatehgridcolor = params.showalternatehgridcolor == null ? "1" : params.showalternatehgridcolor;
        content.push(" showAlternateHGridColor='" + showalternatehgridcolor + "'");

        /*设置横向网格带交替的颜色，默认为ff5904*/
        var alternatehgridcolor = params.alternatehgridcolor == null ? "ff5904" : params.alternatehgridcolor;
        content.push(" AlternateHGridColor='" + alternatehgridcolor + "'");

        /*设置水平分区线颜色，默认为ff5904*/
        var divlinecolor = params.divlinecolor == null ? "ff5904" : params.divlinecolor;
        content.push(" divLineColor='" + divlinecolor + "'");

        /*设置水平分区线透明度，默认为20*/
        var divlinealpha = params.divlinealpha == null ? "20" : params.divlinealpha;
        content.push(" divLineAlpha='" + divlinealpha + "'");

        /*设置横向网格带的透明度，默认为5*/
        var alternatehgridalpha = params.alternatehgridalpha == null ? "5" : params.alternatehgridalpha;
        content.push(" alternateHGridAlpha='" + divlinealpha + "'");

        /*以下为折线图的各项参数*/
        if (params.type == "brokenline") {
            /*设置折线节点半径，默认为0*/
            var anchorradius = params.anchorradius == null ? "0" : params.anchorradius;
            content.push(" anchorRadius='" + anchorradius + "'");
        }
        content.push(" >");
        return content.join("");
    },
    InsertFusionChartsByDataUrl: function(flash_url, vector_id, data_url, width, height) {
        var chart = new FusionCharts(flash_url, vector_id, width, height);
        chart.setDataURL(data_url);
        chart.render(vector_id);
    },
    InsertFusionChartsByDataXml: function(flash_url, vector_id, data_xml, width, height) {
        var chart = new FusionCharts(flash_url, vector_id, width, height);
        chart.setDataXML(data_xml);
        chart.render(vector_id);
    }
}