var _nationalPublicSituationMap = new Object;
var NationalPublicSituationMap = _nationalPublicSituationMap.property = {    
    MapArea: { "xj": "新疆", "xz": "西藏", "qh": "青海", "gs": "甘肃", "nmg": "内蒙古", "hlj": "黑龙江", "jl": "吉林",
        "ln": "辽宁", "sd": "山东", "heb": "河北", "shx": "山西", "bj": "北京", "tj": "天津", "sx": "陕西", "nx": "宁夏",
        "hen": "河南", "js": "江苏", "ah": "安徽", "sh": "上海", "zj": "浙江", "jx": "江西", "fj": "福建", "gd": "广东",
        "han": "海南", "gx": "广西", "gz": "贵州", "yn": "云南", "sc": "四川", "cq": "重庆", "hn": "湖南", "hb": "湖北",
        "tw": "台湾", "xg": "香港", "am": "澳门"
    },
    Load: function() {
        var now_date = NationalPublicSituationMap.GetDateStr(new Date());
        $.getJSON("Handler/Info.ashx",
	        { "time_str": now_date, "data_type": "national-map" },
	        function(data) {
	            NationalPublicSituationMap.Map(data);
	        }
	        );
    },
    GetDateStr: function(time) {
        return time.getFullYear() + "-" + (time.getMonth() + 1) + "-" + time.getDate();
    },
    Map: function(data) {
        var row = data["AreaData"];
        var current_obj = this;
        var current_div = null;
        var width = $(".ditu").width();
        var height = $(".ditu").height();
        var stand_data = parseInt(row["shx"]);
        for (var item in row) {
            $(".chinese_" + item).attr("pid", row[item]);
            if (row[item] >= stand_data) {
                $(".chinese_" + item).empty().html("<img src=\"img/tb_03.gif\" border=\"0\" />");
            }
            else if (row[item] >= 200 && row[item] < stand_data) {
                $(".chinese_" + item).empty().html("<img src=\"img/tb_06.gif\" border=\"0\" />");
            }
            else {
                $(".chinese_" + item).empty().html("<img src=\"img/tb_08.gif\" border=\"0\" />");
            }
        }
        $(".chinese_ditu").children("div").each(function(n) {
            $(this).hover(
                function() {
                    var position = $(this).position();
                    var left = position.left;
                    var top = position.top;
                    var div = document.createElement("DIV");
                    $(div).attr("class", "map_cue");
                    if ((left + 75) > width) {
                        if ((top + 75) >= height) {
                            $(div).css({ "left": position.left - 105, "top": position.top - 50 });
                        }
                        else {
                            $(div).css({ "left": position.left - 105, "top": position.top + 15 });
                        }
                    }
                    else {
                        if ((top + 75) >= height) {
                            $(div).css({ "left": position.left + 15, "top": position.top - 50 });
                        }
                        else {
                            $(div).css({ "left": position.left + 15, "top": position.top + 15 });
                        }
                    }
                    current_div = div;
                    $(div).html(current_obj.MapArea[$(this).attr("class").split("_")[1]] + "<br/>文章数：" + $(this).attr("pid"));
                    $(".chinese_ditu").append(div);
                },
                function() {
                    $(current_div).remove();
                }
          );
        });
    }
}