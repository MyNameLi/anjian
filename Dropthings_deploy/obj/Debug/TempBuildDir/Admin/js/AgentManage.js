$(document).ready(function() {    
    AgentManage.initList();
    AgentManage.innitCommunity();
    AgentManage.BtnInit();

});
var _agentmanage = new Object;
var AgentManage = _agentmanage.prototype = {
    agentData: {},
    agentType: null,
    initList: function() {
        var s = { "agent_type": "UserReadAgentList" };
        var params = {};
        var LAgent = new Agent(s);
        AgentDis.AgentListDis = function(data) {
            var content = [];
            if (data) {
                for (var item in data) {
                    var row = data[item];
                    var l_agent_name = unescape(row["name"]);
                    AgentManage.agentData["agent_" + l_agent_name] = row;
                    content.push("<li name=\"agent_item\" pid=\"" + l_agent_name + "\"><dl><dt>" + l_agent_name + "</dt>");
                    content.push("<dd><a href=\"javascript:void(null);\" pid=\"" + l_agent_name + "\" name=\"agent_edit\">编辑</a>");
                    content.push("<a href=\"javascript:void(null);\" pid=\"" + l_agent_name + "\" name=\"agent_del\">删除</a></dd>");
                    content.push("</dl></li>");
                }

            } else {
                content.push("<li>暂时没有聚焦！</li>");
            }
            $("#agent_list").empty().html(content.join(""));
            $("#agent_list").find("[name='agent_edit']").click(function() {
                var agent_name = $.trim($(this).attr("pid"));
                $("#btntrain").attr("pid", agent_name);
                AgentManage.InitCodition(agent_name);
            });
            $("#agent_list").find("[name='agent_del']").click(function() {
                var value = $.trim($(this).attr("pid"));
                if (confirm("此操作不可逆，您确定要删除么？")) {
                    AgentDis.AgentManageFn = function(post_params) {
                        if (post_params) {
                            var name = unescape(post_params["agentname"]);
                            $("li[name='agent_item'][pid='" + name + "']").remove();
                        }
                        AgentManage.innitCommunity();
                    }
                    var s = { "agent_type": "AgentDelete" };
                    var params = {};
                    params["agentname"] = escape(value);
                    var LAgent = new Agent(s);
                    LAgent.PostCommand(params);
                }
            });
        }
        LAgent.PostCommand(params);
    },
    innitCommunity: function() {
        var s = { "agent_type": "Community" };
        var params = {};
        var LAgent = new Agent(s);
        AgentDis.CommunityDis = function(data) {
            if (data) {
                var content = [];
                content.push("<li style=\"color:black;\"><dl><dt style=\"width: 150px;\">");
                content.push("您的定制信息</dt><dd><div style=\"width:200px; float:left;\">");
                content.push("类似定制信息</div><div style=\"float:left\">所有者</div></dd></dl></li>")
                for (var item in data) {
                    var row = data[item];
                    var agent_name = row["agentname"];
                    var commulity_list = row["communitylist"];
                    content.push("<li style=\"background:#e9f3fc;\">" + agent_name + "</li>");
                    delete commulity_list["SuccessCode"];
                    for (var one in commulity_list) {
                        var commulity_row = commulity_list[one];
                        content.push("<li style=\"color:black;\"><dl><dt style=\"width: 150px;\"></dt>");
                        content.push("<dd><div style=\"width:200px; float:left;\">" + commulity_row["communityname"] + "</div>");
                        content.push("<div style=\"float:left;\">" + commulity_row["communityusername"] + "</div></dd></dl>");
                        content.push("</li>");
                    }
                }
            } else {
                content.push("<li>暂时没有数据！</li>");
            }
            $("#CommunityList").empty().html(content.join(""));
        }
        LAgent.PostCommand(params);
    },
    SearchResult: function(l_value) {
        if (!l_value) {
            var initData = { "result_id": "result_id", "paze_size": 5, "status_bar_id": "pager_list" };
            var params = { "action": "query", "display_style": 1 };
            var value = $.trim($("#Trainname").val());
            params["text"] = value;
            params["minscore"] = $("#min_score").val();
            params["sort"] = $("#score_style").val();
            var Lpager = new Pager(initData);
            Lpager.LoadData(1, params);
        }
        else {
            var s = { "agent_type": "AgentGetResults", "result_id": "result_id", "paze_size": 5,
                "pager_status_id": "pager_list", "display_style": 5
            };
            var params = {};
            params["agentname"] = escape(l_value);
            var LAgent = new Agent(s);
            LAgent.PostCommand(params);
        }
    },
    GetDocHref: function() {
        var list = [];
        $("#article_list").find("span").each(function() {
            list.push($(this).html());
        });
        return list.join("+");
    },
    BtnInit: function() {
        $("#btntrain").click(function() {
            if (AgentManage.CheckCondition()) {
                if (AgentManage.agentType) {
                    var s = { "agent_type": AgentManage.agentType };
                    var params = {};
                    if (AgentManage.agentType == "AgentAdd") {
                        params["agentname"] = escape($("#name").val());
                        delete params["NewAgentName"];
                    } else {

                        params["agentname"] = escape($(this).attr("pid"));
                        if ($(this).attr("pid") != $("#name").val()) {
                            params["NewAgentName"] = escape($("#name").val());
                        }
                    }

                    params["training"] = escape($("#Trainname").val());
                    params["fielddresort"] = $("#score_style").val();
                    params["fielddremaxresults"] = $("#max_result").val();
                    params["fielddreminscore"] = $("#min_score").val();
                    if (AgentManage.GetDocHref()) {
                        params["PositiveDocs"] = AgentManage.GetDocHref();
                    }
                    AgentDis.AgentManageFn = function(post_params) {
                        if (post_params) {
                            $("#btntrain").next("span").empty().html("训练成功");
                            function SpanHide() {
                                $("#btntrain").next("span").empty();
                            }
                            setTimeout(SpanHide, 1500);
                            AgentManage.initList();
                            AgentManage.innitCommunity();
                        } else {
                            $("#btntrain").next("span").empty().html("训练失败，请核查该代理是否已存在");
                            function SpanHide() {
                                $("#btntrain").next("span").empty();
                            }
                            setTimeout(SpanHide, 1500);
                        }
                    }
                    var LAgent = new Agent(s);
                    LAgent.PostCommand(params);
                }
            }
        });
        $("#btnsearch").click(function() {
            if (AgentManage.CheckCondition()) {
                $("#article_train").show();
                AgentManage.SearchResult();
            }
        });

        $("#btnAdd").click(function() {
            $("#btntrain").attr("pid", "");
            AgentManage.InitCodition();
        });

        $("#sava_docs").click(function() {
            var content = [];
            $("#result_id").find("input:checked").each(function() {
                content.push("<li>");
                content.push("<span>" + $(this).attr("pid") + "</span>");
                content.push("&nbsp;&nbsp;<a href=\"javascript:void(null);\">删除</a>");
                content.push("</li>");
            });
            $("#article_list").append(content.join(""));
            $("#article_list").find("a").each(function() {
                $(this).click(function() {
                    $(this).parent("li").remove();
                });
            });
        });

        $("#choose_all").click(function() {
            if ($(this).attr("checked")) {
                $("#result_id").find(":checkbox").attr("checked", "checked");
            } else {
                $("#result_id").find(":checkbox").removeAttr("checked");
            }
        });
    },
    InitCodition: function(key) {
        if (key) {
            AgentManage.agentType = "AgentEdit";
            var data = AgentManage.agentData["agent_" + key];
            $("#name").val(unescape(data["name"]));
            $("#Trainname").val(unescape(data["train"]));
            $("#btnsearch").removeAttr("checked");
            if (data["MaxResult"]) {
                $("#max_result").val(data["MaxResult"]);
            }
            if (data["Score"]) {
                $("#min_score").val(data["Score"]);
            }
            if (data["Sort"]) {
                $("#score_style").val(data["Sort"]);
            }
            if (data["Dabase"]) {
                var database_list = data["Dabase"].split("+");
                $("input[name='database_list']").removeAttr("checked");
                for (var i = 0, j = database_list.length; i < j; i++) {
                    $("input[name='database_list'][value='" + database_list[i] + "']").attr("checked", "checked");
                }
            }
            if (data["reflist"]) {
                var list = data["reflist"].split(",");
                var len = list.length;
                var content = [];
                for (var i = 0; i < len; i++) {
                    content.push("<li>");
                    content.push("<span>" + list[i] + "</span>");
                    content.push("&nbsp;&nbsp;<a href=\"javascript:void(null);\">删除</a>");
                    content.push("</li>");
                }
                $("#article_list").empty().html(content.join(""));
                $("#article_list").find("a").each(function() {
                    $(this).click(function() {
                        $(this).parent("li").remove();
                    });
                });
            } else {
                $("#article_list").empty();
            }
        } else {
            AgentManage.agentType = "AgentAdd";
            $("#max_result").val("20");
            $("#min_score").val("40");
            $("#name").val("");
            $("#Trainname").val("");
            $("#btnsearch").removeAttr("checked");
            $("#article_list").empty();
            $("#score_style").val("relevance");
        }
        $("#result_id").empty();
        $("#pager_list").empty();
        $("#article_train").hide();
        $("#train_div").show();
    },
    InitDataBase: function() {
        $.post("Common",
			{ "type": 2 },
			function(data) {
			    if (data) {
			        var content = [];
			        delete data["SuccessCode"];
			        for (var item in data) {
			            content.push("<input type=\"checkbox\" value=\"" + unescape(data[item]) + "\" name=\"database_list\" />" + unescape(data[item]) + "&nbsp;&nbsp;");

			        }
			        $("#agent_db_list").empty().html(content.join(""));
			    }
			},
			"json"
		);
    },
    CheckCondition: function() {
        if (!$.trim($("#name").val())) {
            alert("请输入聚焦名");
            return false;
        }
        if (!$.trim($("#Trainname").val())) {
            alert("请输入聚焦条件！");
            return false;
        }
        return true;
    }
}