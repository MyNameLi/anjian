
var _config = new Object;
var Config = _config.property = {
    IdolDataBase: { "newanjian": "newanjian", "statistic": "safety+newssource+portalsafety",
        "statisticInfoSummary": "safety+newssource+portalsafety"
    }
}
// JavaScript Document
var _Common = new Object;
var MenuList = { "首页": "Default.aspx?首页", "高级搜索": "Default.aspx?高级搜索", "舆情专题": "Default.aspx?舆情专题", "舆情分类": "Default.aspx?舆情分类", "热点分布": "Default.aspx?热点分布", "舆情趋势": "Default.aspx?舆情趋势",
    "舆情词库": "sensitive.html", "定制信息": "agent.html", "系统设置": { "分类训练": "training.html", "关键词训练": "cluster_train.html", "专题设置": "topic_setting.html", "定制信息管理": "AgentManage.html", "词库维护": "SensitiveWord.html",
        "后台管理中心": "demo_admin/admin_Default.htm"
    }
};
var Common = _Common.prototype = {
    Config: { "hotmapname": "MYJOB_SAFETY_CLUSTERS", "idolhttp": "Handler/GetImage.ashx", "hotmaplist": { "总体热点图": "JOB_SAFETY_CLUSTERS" }, "sgmapname": "myjob_SG",
        "categorycondition": " PARENTCATE=0 and ISEFFECT=1", "categoryinnit": { "categoryid": "523510803107128336", "parentcate": "202", "categoryname": "河南平禹“10·16”特别重大瓦斯突出事故" },
        "datediff": 730,
        "weibodatabase": "source"
    },
    LogVisitor: function() {
        $.post("Handler/User.ashx", { 'action': 'log_visitor', 'page_url': location.href },
            function(Data, textStatus) {

            }, "json");
    }, replaceAll: function(s, s1, s2) {
        return s.replace(new RegExp(s1, "gm"), s2);
    },
    //g_div 背景层，模拟锁屏的层
    ShowEditFrame: function(g_div, child_div, parent_div, close_btn) {
        document.documentElement.scrollTop = 0;
        document.documentElement.scrollLeft = 0;
        var top = document.documentElement.scrollTop;
        var left = document.documentElement.scrollLeft;
        var height = document.documentElement.clientHeight + 20;
        var width = document.documentElement.clientWidth + 20;
        $("#" + g_div).css("top", top + "px");
        $("#" + g_div).css("left", left + "px");
        $("#" + g_div).css("width", width + "px");
        $("#" + g_div).css("height", height + "px");
        $("#" + g_div).css("backgroundColor", "#000");
        $("#" + g_div).css("opacity", "0.5"); //div层透明度为50%，在FF下
        $("#" + g_div).css("position", "absolute");
        $("#" + g_div).css("z-index", "20");
        $("#" + parent_div).show();
        $("#" + g_div).show();
        $("html").css("overflow", "hidden");

        var l_height = parseInt($("#" + child_div).height()) / 2;
        var l_width = parseInt($("#" + child_div).width()) / 2;
        var body_offsetTop = $(document).scrollTop() + 150;
        $("#" + parent_div).css({ "position": "absolute", "top": top + height / 2 - l_height + "px", "left": left + width / 2 - l_width + "px" });
        $("#" + close_btn).click(function() {
            $("#" + parent_div).hide();
            $("#" + g_div).hide();
            $("html").css("overflow", "");
        });
        var div_move = new divMove(child_div, parent_div);
        div_move.init();
        var mydiv = $("#" + g_div);
        var mydiv_resize = function() {
            mydiv.css("top", document.documentElement.scrollTop + "px");
            mydiv.css("left", document.documentElement.scrollLeft + "px");
            mydiv.height(document.body.clientHeight);
            mydiv.width(document.body.clientWidth);
        }
        window.onresize = mydiv_resize;
    },
    CloseEditFrame: function(g_div, parent_div) {

        $("#" + parent_div).hide();
        $("#" + g_div).hide();
        $("html").css("overflow", "");
    },
    GetTab: function(tabid) {
        var tabs = $("#" + tabid);
        $.post("Handler/Statistic.ashx",
               { "action": "gettab" },
                function(data) {
                    if (data.Success == 1) {
                        delete data.Success;
                        var tabcontainer = [];
                        tabcontainer.push("<ul class=\"tabs tab-strip ui-sortable\">");
                        for (var item in data["data"]) {
                            var entity = data["data"][item];
                            var hrf = location.href;
                            var hrfs = hrf.split("/");

                            if (entity.url == "0") {
                                tabcontainer.push(" <li class=\"tab inactivetab nodrag\"><a href=\"Default.aspx?" + entity.title + "\"><b>" + entity.title + "</b></a></li>");
                            }
                            else {
                                if (entity.url == hrfs[hrfs.length - 1]) {
                                    tabcontainer.push(" <li class=\"tab activetab\"><span class=\"current_tab\"><b>" + entity.title + "</b></span></li>");
                                }
                                else {
                                    tabcontainer.push(" <li class=\"tab inactivetab nodrag\"><a href=\"" + entity.url + "\"><b>" + entity.title + "</b></a></li>");

                                }
                            }

                        }
                        tabcontainer.push("</ul>");
                        tabs.empty().html(tabcontainer.join(""));
                    } else {
                        location.href = "LoginPage.aspx";
                    }
                },
                "json"
            )
    },
    Navigation: function(tab_num, menu_id) {
        var obj = typeof (menu_id) == "object" ? menu_id : $("#" + menu_id);
        var count = 1;
        var html_str = [];
        for (var item in MenuList) {
            if (typeof (MenuList[item]) != "object") {
                if (tab_num == count) {
                    html_str.push("<div class=\"nav_link\"><a href=\"" + MenuList[item] + "\">" + item + "</a></div>");
                }
                else {
                    html_str.push("<div class=\"nav_no_link\"><a href=\"" + MenuList[item] + "\">" + item + "</a></div>");
                }
            }
            else {
                if (tab_num == count) {
                    html_str.push("<div class=\"nav_link\"><a href=\"javascript:void(null);\">" + item + "</a>");
                }
                else {
                    html_str.push("<div class=\"nav_no_link\"><a href=\"javascript:void(null);\">" + item + "</a>");
                }
                for (var i in MenuList[item]) {
                    if (tab_num == count) {
                        html_str.push("<div class=\"nav_child\" ><a href=\"" + MenuList[item][i] + "\">" + i + "</a></div>");
                    }
                    else {
                        html_str.push("<div class=\"nav_child\" style=\"display:none;\" ><a href=\"" + MenuList[item][i] + "\">" + i + "</a></div>");
                    }
                }
                html_str.push("</div>");
            }
            count++;
        }
        $(obj).empty().html(html_str.join(""));
        $(obj).children("div").eq(tab_num - 1).attr("class", "nav_link");
        var div_list = $(obj).children("div");
        var current_div = new Object;
        $(obj).children("div").each(function() {
            if ($(this).attr("class") == "nav_link")
                current_div = this;
            $(this).click(function() {
                if (this != current_div) {
                    $(this).siblings("div").attr("class", "nav_no_link");
                    $(current_div).children("div").hide();
                    $(this).children("div").show();
                    $(this).attr("class", "nav_link");
                    current_div = this;
                }
            });
        });
    },
    OtherNavigation: function(tab_num, nav_id, type) {
        var obj = typeof (nav_id) == "object" ? nav_id : $("#" + nav_id);
        var count = 1;
        var content = [];
        content.push("<ul>");
        for (var item in MenuList) {
            if (typeof (MenuList[item]) != "object") {
                content.push("<li><a href=\"" + MenuList[item] + "\" >" + item + "</a></li>");
            }
            else {
                if (type) {
                    content.push("<li><span><a href=\"javascript:void(null);\">" + item + "</a></span><ul class=\"subnav\">");
                    for (var i in MenuList[item]) {
                        content.push("<li ><a href=\"" + MenuList[item][i] + "\">" + i + "</a></li>");
                    }
                    content.push("</ul></li>");
                } else {
                    content.push("<li style=\"position:relative;\"><a href=\"javascript:void(null);\" >" + item + "</a>");
                    content.push("<ol style=\"z-index:100; width:90px; display:none; top:32px; left:0px; position:absolute;background:white;");
                    content.push("border-bottom:1px solid gray;border-left:1px solid gray;border-right:1px solid gray;\">")
                    for (var i in MenuList[item]) {
                        content.push("<li ><a href=\"" + MenuList[item][i] + "\">" + i + "</a></li>");
                    }
                    content.push("</ol></li>");
                }

            }
            count++;
        }
        content.push("</ul>");
        $(obj).empty().html(content.join(""));
        $(obj).children("ul").children("li").eq(tab_num - 1).attr("class", "current");
        $(obj).children("ul").children("li").each(function() {
            $(this).hover(
	            function() {
	                if (type) {
	                    $(this).children("ul").show();
	                } else {
	                    $(this).attr("class", "current");
	                    $(this).children("ol").show();
	                }

	            },
	            function() {
	                if (type) {
	                    $(this).children("ul").hide();
	                } else {
	                    $(this).attr("class", "");
	                    $(this).children("ol").hide();
	                }
	            }
	        );

        });


    },
    TabControl: function(s, current_class, nocurrent_class) {
        var TabList = [];
        var DisList = [];
        for (var item in s) {
            TabList.push(item);
            DisList.push(s[item]);
        }
        for (var i = 0, j = TabList.length; i < j; i++) {
            $("#" + TabList[i]).click(function() {
                for (var k = 0; k < j; k++) {
                    $("#" + TabList[k]).attr("class", nocurrent_class);
                    $("#" + DisList[k]).hide();
                }
                $(this).attr("class", current_class);
                $("#" + s[$(this).attr("id")]).show();
            });
        }
    },
    ClickTab: function(obj_list, current_class, no_current_class) {
        obj_list.each(function() {
            var current_obj = this;
            $(this).click(function() {
                obj_list.each(function() {
                    $(this).parent("dl").attr("class", no_current_class);
                });
                $(current_obj).parent("dl").attr("class", current_class);
            });
        });
    },
    GetMonth: function(obj) {
        var time = new Date();
        $("#" + obj).empty().html(time.getFullYear() + "年" + (time.getMonth() + 1) + "月");
    },
    GetDay: function(obj) {
        var time = new Date();
        $("#" + obj).empty().html(time.getDate());
    },
    DownFlash: function(obj, falsh_url, width, height) {
        $("#" + obj).flash(
			{
			    src: falsh_url,
			    width: width,
			    height: height
			},
			{ version: 10 }
       );
    },
    BtnLginInit: function() {
        $("#login_cancel").click(function() {
            $.post("Handler/User.ashx",
                { "action": "clear_cookie" },
                function(data, textstatus) {
                    if (data.SuccessCode == "1")
                        location.href = "Login.html";
                },
                "json"
            );
        });
        $("#login_out").click(function() {
            $.post("Handler/User.ashx",
                { "action": "clear_cookie" },
                function(data, textstatus) {
                    if (data.SuccessCode == "1")
                        window.close();
                },
                "json"
            );

        });
    },
    CategoryMenu: function(obj, current_class, nocurrent_class, str_where, left, tag) {
        var current_obj = this;
        $.post("Handler/CategoryMenu.ashx",
		    { "str_where": str_where, "left": left },
		    function(data) {
		        $("#" + obj).empty().html(unescape(data));
		        var t1 = jQuery("div.menu_img b.color_2");
		        var t2 = jQuery("#" + obj + " > li");
		        t1.text(t2.size());

		        var child_list = $("#" + obj).children("li");
		        child_list.click(function() {
		            $(this).siblings("li").children("div").hide();
		            $(this).siblings("li").removeClass("on");
		            $(this).addClass("on");
		            $(this).children("div").show(300);
		        });
		        $("#" + obj).find("a").each(function() {
		            var len = $(this).siblings("div").length;
		            if (len == 0) {
		                $(this).click(function() {
		                    current_obj.CategoryMenuClick($(this).attr("pid"), $(this).html());
		                });
		            }
		        });
		        child_list.find("li").each(function() {
		            if ($(this).children("ol").length > 0) {
		                $(this).hover(
					        function() {
					            $(this).children("ol").css("top", $(this).position().top - 1);
					            $(this).children("ol").show();
					        },
					        function() {
					            $(this).find("ol").hide();
					        }
				        );
		            }
		        });
		        current_obj.CategoryMenuInit();
		    }
		)
    },
    CategoryMenuClick: function(data, text) {
        return null;
    },
    CategoryMenuInit: function() {
        return null;
    },
    HotMapData: function(s, page_tag) {
        $.ajax({
            type: "get",
            url: "Handler/GetMapData.ashx",
            data: s,
            beforeSend: function(XMLHttpRequest) {
                $("#hot_image").html("热点数据加载中……");
            },
            success: function(data, textStatus) {

                var map = $("#mapData");

                map.empty().html(data);
                //隐藏文字说明
                $(".node_text").each(function() {
                    $(this).hide();
                });

                $(".node").each(function() {
                    $(this).mouseover(function() {

                        var num = $(this).attr("id").split("_")[1];
                        $("#clustertitle_" + num).show();
                    });

                    $(this).mouseout(function() {

                        var num = $(this).attr("id").split("_")[1];
                        $("#clustertitle_" + num).hide();
                    });

                    $(this).click(function() {
                        $(".node").each(function() {
                            $(this).css("background", "red");
                        });
                        $(this).css("background", "green");
                        var cluster_id = $(this).attr("id").split("_")[1];
                        if (page_tag == "index") {
                            location.href = "hot.html";
                        }
                        else {
                            Hot.GetClusterResults(cluster_id, s.end_date, s.job_name);
                        }

                    });
                }); //each end

            },
            complete: function(XMLHttpRequest, textStatus) {
                $("#hot_image").html("舆情热点图<br/>(点击红色方块，可在右侧区域获取文章列表)");
            },
            error: function() {
                //请求出错处理
            }
        });
    },
    GetCookie: function(name) {
        var arg = name + "=";
        var alen = arg.length;
        var clen = document.cookie.length;
        var i = 0;
        while (i < clen) {
            var j = i + alen;
            if (document.cookie.substring(i, j) == arg)
                return getCookieVal(j);
            i = document.cookie.indexOf(" ", i) + 1;
            if (i == 0) break;
        }
        return null;

        function getCookieVal(offset) {
            var endstr = document.cookie.indexOf(";", offset);
            if (endstr == -1)
                endstr = document.cookie.length;
            return unescape(document.cookie.substring(offset, endstr));
        }
    }
}