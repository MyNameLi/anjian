// JavaScript Document

var _menufactory = new Object;
var MenuFactory = _menufactory.prototype = {
	GetMenu : function(type){
		switch(type){
			case 1:
				break;
			case 2:
				break;
			default:
				break;
		}
	}	
}


var _basemenu = new Object;
var BaseMenu = _basemenu.prototype = {
    FieldStrList: ["DRECONTENT", "DRETITLE", "MYPUBDATE", "DREDATE", "READNUM", "REPLYNUM"],
    PageList: ["display", "urlrule", "contentrule"],
    innitMoveDiv: function() {
        for (var i = 0, j = BaseMenu.PageList.length; i < j; i++) {
            var url_prefix = BaseMenu.PageList[i];
            BaseMenu.GetIframe(url_prefix);
        }
    },
    GetIframe: function(url_prefix) {
        var get_url = url_prefix + ".html";
        $.get(get_url,
			null,
			function(data) {
			    var div = document.createElement("DIV");
			    $(div).attr("id", url_prefix + "_move_iframe");
			    if (url_prefix != "display") {
			        $(div).css("display", "none");
			    }
			    $(div).html(data);
			    $("#add_frame").append($(div));
			    if (url_prefix == "contentrule") {
			        BaseMenu.InnitFieldStr();
			    }
			    Display.innit();
			}
		);
    },
    InnitFieldStr: function() {
        var filedlist = this.FieldStrList;
        var content = [];
        for (var i = 0, j = filedlist.length; i < j; i++) {
            var key = filedlist[i];
            content.push("<option value=\"" + key + "\">" + key + "</option>");
        }
        $("#field_str").empty().html(content.join(""));
    },
    InnitTaskData: function(type, taskid) {
        $.post("../Handler/Task.ashx",
            { "type": type, "task_id": taskid },
            function(data) {
                if (data) {
                    if (type == "edit") {
                        BaseMenu.InnitTaskEntityData(data["taskEntity"]);
                        BaseMenu.InnitUrlRuleData(data["urlRuleEntityList"]);
                        BaseMenu.InnitContentRuleData(data["contentRuleEntityList"]);
                    } else if (type == "createxml") {
                        if (data.SuccessCode == 1) {
                            alert("生成成功");
                        } else {
                            alert("失败！失败原因" + data["Error"]);
                        }
                    } else if (type == "addtask" || type == "starttask" || type == "stoptask") {
                       alert("操作成功"); 
                    }
                    else {
                        location.replace(location.href);
                    }
                }
            },
            "json"
        );
    },
    InnitTaskEntityData: function(data) {
        $("#task_name").val(unescape(data["task_name"]));
        $("#task_type").val(unescape(data["task_type"]));
        $("#task_entryurl").val(unescape(data["url_entry"]));
        $("#site_code").val(unescape(data["site_code"]));
        $("#spider_degree").val(data["spider_degree"]);
        $("#page_url_rule").val(unescape(data["page_url_reg"]));
        $("#url_prefix").val(unescape(data["url_prefix"]));
        $("#task_des").val(unescape(data["task_des"]));
        $("#content_url_rule").val(unescape(data["page_content_reg"]));
        if (data["is_agent"] == 1) {
            $("#is_agent").attr("checked", "checked");
            $("#agent_iframe").show();
            $("#agent_server").val(unescape(data["agent_server_ip"]));
            $("#agent_port").val(data["agent_server_port"]);
            $("#agent_user").val(unescape(data["agent_server_user"]));
            $("#agent_password").val(unescape(data["agent_server_pwd"]));
        }
        if (data["is_login"] == 1) {
            $("#is_login").attr("checked", "checked");
            $("#login_iframe").show();
            $("#login_site").val(unescape(data["login_site"]));
            $("#login_data").val(data["login_data"]);
        }
        if (data["is_update"] == 1) {
            $("#is_check").attr("checked", "checked");
            $("#check_iframe").show();
            BaseMenu.InnitTimeSpan(data["update_timespan"]);
        }
    },
    InnitTimeSpan: function(timespan) {
        if (parseInt(timespan / 3600) > 0) {
            $("#check_timespan").val(parseInt(timespan / 3600));
            $("#check_timeunit").val("3");
            return;
        } else if (parseInt(timespan / 60) > 0) {
            $("#check_timespan").val(parseInt(timespan / 60));
            $("#check_timeunit").val("2");
            return;
        }
        $("#check_timespan").val(timespan);
        $("#check_timeunit").val("1");
        return;
    },
    InnitUrlRuleData: function(data) {
        if (data) {
            delete data["SuccessCode"];
            var content = [];
            for (var item in data) {
                var entity = data[item];
                content.push("<tr>");
                content.push("<td pid=\"" + entity["rule_object"] + "\">" + BaseMenu.GetRuleObject(entity["rule_object"]) + "</td>");
                content.push("<td pid=\"" + entity["rule_active"] + "\">" + BaseMenu.GetRuleActive(entity["rule_active"]) + "</td>");
                content.push("<td>" + BaseMenu.SetRegStr(unescape(entity["rule_keyword"])) + "</td>");
                content.push("<td name=\"del_url_rule_tr\"><a href=\"javascript:void(null);\">删除</a></td>");
                content.push("</tr>");
            }

            $("#url_rule_list").empty().html(content.join(""));
            $("td[name='del_url_rule_tr']").click(function() {
                $(this).parent("tr").remove();
            });
        }
    },
    InnitContentRuleData: function(data) {
        if (data) {
            delete data["SuccessCode"];
            var content = [];
            for (var item in data) {
                var entity = data[item];
                content.push("<tr>");

                content.push("<td name=\"field_str\" pid=\"" + unescape(entity["field_str"]) + "\">" + unescape(entity["field_str"]) + "</td>");
                content.push("<td name=\"is_intervar\" pid=\"" + entity["is_intervar"] + "\">" + BaseMenu.GetBitType(entity["is_intervar"]) + "</td>");
                content.push("<td name=\"is_date\" pid=\"" + entity["is_date"] + "\">" + BaseMenu.GetBitType(entity["is_date"]) + "</td>");
                content.push("<td name=\"field_action\" pid=\"" + entity["field_type"] + "\">" + BaseMenu.GetFieldType(entity["field_type"]) + "</td>");                
                content.push("<td name=\"is_remove_html\" pid=\"" + entity["is_remove_html"] + "\">" + BaseMenu.GetBitType(entity["is_remove_html"]) + "</td>");
                content.push("<td name=\"field_source\" pid=\"" + unescape(entity["field_source"]) + "\">" + unescape(entity["field_source"]) + "</td>");
                content.push("<td style=\"display:none;\" name=\"field_exp\" pid=\"" + unescape(entity["field_reg"]) + "\">" + unescape(entity["field_reg"]) + "</td>");
                content.push("<td style=\"display:none;\" name=\"field_suffix\" pid=\"" + unescape(entity["field_suffix"]) + "\">" + unescape(entity["field_suffix"]) + "</td>");
                content.push("<td style=\"display:none;\" name=\"field_param1\" pid=\"" + unescape(entity["field_param1"]) + "\">" + unescape(entity["field_param1"]) + "</td>");
                content.push("<td style=\"display:none;\" name=\"field_param2\" pid=\"" + unescape(entity["field_param2"]) + "\">" + unescape(entity["field_param2"]) + "</td>");
                content.push("<td style=\"display:none;\" name=\"field_param3\" pid=\"" + unescape(entity["field_param3"]) + "\">" + unescape(entity["field_param3"]) + "</td>");
                content.push("<td style=\"display:none;\" name=\"field_param4\" pid=\"" + unescape(entity["field_param4"]) + "\">" + unescape(entity["field_param4"]) + "</td>");
                content.push("<td name=\"del_content_rule_tr\"><a name=\"btn_edit_content\" href=\"javascript:void(null);\">编辑</a>&nbsp;&nbsp;");
                content.push("<a name=\"btn_delete_content\" href=\"javascript:void(null);\">删除</a></td>");
                content.push("</tr>");
            }

            $("#content_rule_list").empty().html(content.join(""));
            $("td a[name='btn_delete_content']").click(function() {
                $(this).parent("td").parent("tr").remove();
            });
            $("td a[name='btn_edit_content']").click(function() {
                $(this).parent("td").siblings("td").each(function() {
                    var key = $(this).attr("name");
                    var val = $(this).attr("pid");
                    $("#" + key).val(val);
                });
                $(this).parent("td").parent("tr").remove();
            });
        }
    },
    GetRuleObject: function(index) {
        switch (index) {
            case 1:
                return "连接地址";
            case 2:
                return "连接标题";
        }
    },
    GetRuleActive: function(index) {
        switch (index) {
            case 1:
                return "包含";
            case 2:
                return "不包含";
            case 3:
                return "满足";
        }
    },
    GetFieldType: function(index) {
        switch (index) {
            case "1":
                return "正则表达式";
            case "2":
                return "XPATH";
            case "3":
                return "前后缀";
            default:
                return "未定义";
        }
    },
    GetBitType: function(index) {
        switch (index) {
            case "0":
                return "否";
            case "1":
                return "是";
            default:
                return "未定义";
        }
    },
    SetRegStr: function(str) {
        str = str.replace(new RegExp("<", 'gm'), "&lt;");
        str = str.replace(new RegExp(">", 'gm'), "&gt;");
        return str;
    },
    GetRegStr: function(str) {
        str = str.replace(new RegExp("&lt;", 'gm'), "<");
        str = str.replace(new RegExp("&gt;", 'gm'), ">");
        return str;
    }
}