// JavaScript Document

var _display = new Object;
var Display = _display.prototype = {
    filedstr: {},
    innit: function() {
        Display.innitcheckbox();
        Display.innitbtnnext();
        Display.innitbtnprev();
        Display.innitbtnsubmit();
        $("a[name='close_move_div']").click(function() {
            $("#add_frame").hide();
            location.replace(location.href);
        });
    },
    innitcheckbox: function() {
        $(":checkbox").click(function() {
            var type = $(this).attr("id").split("_")[1];
            if ($(this).attr("checked")) {
                $("#" + type + "_iframe").show();
            } else {
                $("#" + type + "_iframe").hide();
            }
        });
    },
    innitbtnnext: function() {
        $("input[id^='btn_next_']").click(function() {
            var type = $(this).attr("id").split("_")[2];
            $("div[id$='_move_iframe']").hide();
            $("#" + type + "_move_iframe").show();
        });
    },
    innitbtnprev: function() {
        $("input[id^='btn_prev_']").click(function() {
            var type = $(this).attr("id").split("_")[2];
            $("div[id$='_move_iframe']").hide();
            $("#" + type + "_move_iframe").show();
        });
    },
    innitbtnsubmit: function() {
        $("#btn_submit").click(function() {
            Display.innitparams();
        });
    },
    innitparams: function() {
        var task_name = $.trim($("#task_name").val());
        Display.filedstr["task_name"] = task_name;
        if (!task_name) {
            alert("请输入站点名称！");
            return;
        }

        var task_type = $("#task_type").val();
        if (task_type == "0") {
            alert("请选择站点类型！");
            return;
        }
        Display.filedstr["task_type"] = task_type;

        var task_entryurl = $.trim($("#task_entryurl").val());

        if (!task_entryurl) {
            alert("请输入入口URL！");
            return;
        }
        Display.filedstr["task_entryurl"] = task_entryurl;


        var task_des = $.trim($("#task_des").val());
        Display.filedstr["task_des"] = task_des;

        var url_prefix = $.trim($("#url_prefix").val());
        Display.filedstr["url_prefix"] = url_prefix;

        var site_code = $("#site_code").val();
        Display.filedstr["site_code"] = site_code;
        if (site_code == "0") {
            alert("请选择网站编码！");
            return;
        }

        var page_url_rule = $.trim($("#page_url_rule").val());
        Display.filedstr["page_url_rule"] = page_url_rule;

        if (!page_url_rule) {
            alert("请输入分页URL规则！");
            return;
        }
        var content_url_rule = $.trim($("#content_url_rule").val());
        Display.filedstr["content_url_rule"] = content_url_rule;
        if (!content_url_rule) {
            alert("请输入具体页URL规则！");
            return;
        }
        var spider_degree = $.trim($("#spider_degree").val());
        Display.filedstr["spider_degree"] = spider_degree;
        if (!spider_degree) {
            alert("请输入采集深度！");
            return;
        }

        var is_agent = Display.GetCheckBoxValue($("#is_agent"));
        Display.filedstr["is_agent"] = is_agent;

        if (is_agent == "1") {
            var agent_server = $.trim($("#agent_server").val());
            Display.filedstr["agent_server"] = agent_server;

            var agent_port = $.trim($("#agent_port").val());
            Display.filedstr["agent_port"] = agent_port;

            var agent_user = $.trim($("#agent_user").val());
            Display.filedstr["agent_user"] = agent_user;

            var agent_password = $.trim($("#agent_password").val());
            Display.filedstr["agent_password"] = agent_password;
        }

        var is_login = Display.GetCheckBoxValue($("#is_login"));
        Display.filedstr["is_login"] = is_login;
        if (is_login == "1") {
            var login_site = $.trim($("#login_site").val());
            Display.filedstr["login_site"] = login_site;

            var login_data = $.trim($("#login_data").val());
            Display.filedstr["login_data"] = login_data;
        }

        var is_check = Display.GetCheckBoxValue($("#is_check"));
        Display.filedstr["is_check"] = is_check;
        if (is_check == "1") {
            var time_str = parseInt($.trim($("#check_timespan").val()));
            var update_timespan = Display.GetTimeSpan(time_str);
            Display.filedstr["update_timespan"] = update_timespan;
        }
        if (Display.GetUrlRuleList()) {
            Display.filedstr["url_rule_list"] = Display.GetUrlRuleList();
        }
        if (Display.GetContentRuleList()) {
            Display.filedstr["content_rule_list"] = Display.GetContentRuleList();
        }
        Display.FormatParams(Display.filedstr);
        $("#submit_msg").empty().html("正在提交数据");
        $.post("../Handler/Task.ashx",
		        Display.filedstr,
		        function(data) {
		            if (data) {
		                if (data["SuccessCode"] == 1) {
		                    alert("提交成功");
		                    location.replace(location.href);
		                    //$("#submit_msg").empty().html("成功");
		                } else {
		                    $("#submit_msg").empty().html("失败");
		                    alert("失败原因：" + data["Error"]);
		                }
		            }
		        },
		        "json"
		    );
    },
    FormatParams: function(params) {
        for (var item in params) {
            var type = typeof (params[item]);
            if (type == "object") {
                Display.FormatParams(params[item]);
            } else {
                params[item] = escape(params[item]);
            }
        }
    },
    GetCheckBoxValue: function(obj) {
        if (obj.attr("checked")) {
            return 1;
        } else {
            return 0;
        }
    },
    GetTimeSpan: function(time_str) {
        var time_span_type = parseInt($("#check_timeunit").val());
        switch (time_span_type) {
            case 1:
                return time_str;
            case 2:
                return time_str * 60;
            case 3:
                return time_str * 60 * 60;
            default:
                break;
        }
    },
    GetUrlRuleList: function() {
        var len = $("#url_rule_list").find("tr").length;
        if (len > 0) {
            var data = {};
            $("#url_rule_list").find("tr").each(function(n) {
                var item = {};
                $(this).find("td").each(function(n) {
                    if (n == 0) {
                        item["rule_object"] = $(this).attr("pid");
                    } else if (n == 1) {
                        item["rule_active"] = $(this).attr("pid");
                    } else if (n == 2) {
                        item["rule_keyword"] = Display.GetRegStr($(this).html());
                    }
                });
                data["item" + n] = item;
            });
            return data;
        } else {
            return null;
        }
    },
    GetContentRuleList: function() {
        var len = $("#content_rule_list").find("tr").length;
        if (len > 0) {
            var data = {};
            $("#content_rule_list").find("tr").each(function(n) {
                var item = {};
                item["field_str"] = $(this).find("td[name='field_str']").attr("pid");
                item["is_intervar"] = $(this).find("td[name='is_intervar']").attr("pid");
                item["is_date"] = $(this).find("td[name='is_date']").attr("pid");
                item["field_action"] = $(this).find("td[name='field_action']").attr("pid");
                item["field_exp"] = $(this).find("td[name='field_exp']").html();
                if ($(this).find("td[name='field_suffix']").length > 0) {
                    item["field_suffix"] = $(this).find("td[name='field_suffix']").html();
                }
                item["is_remove_html"] = $(this).find("td[name='is_remove_html']").attr("pid");
                item["field_source"] = $(this).find("td[name='field_source']").attr("pid");
                item["field_param1"] = $(this).find("td[name='field_param1']").attr("pid");
                item["field_param2"] = $(this).find("td[name='field_param2']").attr("pid");
                item["field_param3"] = $(this).find("td[name='field_param3']").attr("pid");
                item["field_param4"] = $(this).find("td[name='field_param4']").attr("pid");
                data["item" + n] = item;
            });
            return data;
        } else {
            return null;
        }
    },
    GetRegStr: function(str) {
        str = str.replace(new RegExp("&lt;", 'gm'), "<");
        str = str.replace(new RegExp("&gt;", 'gm'), ">");
        return str;
    }
}