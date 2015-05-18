var Agent = function(s){
	this.agent_type = s.agent_type;
	this.page_size = s.page_size == null ? 10 : s.page_size;
	this.result_id = s.result_id;
	this.pager_status_id = s.pager_status_id;
	this.display_style = s.display_style;
}
Agent.prototype = {
    AgentType: { "AgentAdd": 1, "AgentGetResults": 2, "AgentEdit": 3, "AgentDelete": 4, "UserReadAgentList": 5, "Community": 6 },
    AgentParams: {
        "AgentAdd": { "action": "AgentAdd", "fieldprivate": "true", "fieldshared": "false", "fieldalert": "false", "fieldemail": "false",
            "fielddrelanguagetype": "chineseUTF8"
        },
        "AgentGetResults": { "action": "AgentGetResults" },
        "AgentEdit": { "action": "AgentEdit", "Mode": "Reference" },
        "AgentDelete": { "action": "AgentDelete" },
        "UserReadAgentList": { "action": "UserReadAgentList" },
        "Community": { "action": "Community", "Agents": "true", "Profile": "false", "AgentsFindProfiles": "false", "DeferLogin": "true", "DREPrint": "all" }
    },
    PostCommand: function(params) {
        var type = this.agent_type;
        var post_params = {};
        if (type) {
            switch (type) {
                case "AgentGetResults":
                    var init_data = {};
                    init_data["page_size"] = this.page_size;
                    init_data["result_id"] = this.result_id;
                    init_data["status_bar_id"] = this.pager_status_id;
                    post_params = this.CombileJson(params, this.AgentParams[type]);
                    post_params["display_style"] = this.display_style;
                    var Lpager = new Pager(init_data);
                    Lpager.OtherFn = function(totalcount) {
                        AgentDis.AgentResultFn(this, totalcount);
                    };
                    Lpager.LoadData(1, post_params);
                    break;
                case "UserReadAgentList":
                    post_params = this.CombileJson(params, this.AgentParams[type]);
                    post_params["agent_type"] = this.AgentType[type];
                    $.post("../Handler/Agent.ashx",
						post_params,
						function(data) {
						    if (data) {
						        if (data["SuccessCode"] == 1) {
						            delete data["SuccessCode"];
						            AgentDis.AgentListDis(data);
						        }
						        else {
						            AgentDis.AgentListDis(null);
						        }
						    }
						    else {
						        AgentDis.AgentListDis(null);
						    }
						},
						"json"
					);
                    break;
                case "Community":
                    post_params = this.CombileJson(params, this.AgentParams[type]);
                    post_params["agent_type"] = this.AgentType[type];
                    $.post("../Handler/Agent.ashx",
						post_params,
						function(data) {
						    if (data) {
						        if (data["SuccessCode"] == 1) {
						            delete data["SuccessCode"];
						            AgentDis.CommunityDis(data);
						        }
						        else {
						            AgentDis.CommunityDis(null);
						        }
						    }
						    else {
						        AgentDis.CommunityDis(null);
						    }
						},
						"json"
					);
                    break;
                default:
                    post_params = this.CombileJson(params, this.AgentParams[type]);
                    post_params["agent_type"] = this.AgentType[type];
                    $.post("../Handler/Agent.ashx",
						post_params,
						function(data) {
						    if (data) {
						        if (data["SuccessCode"] == 1) {
						            AgentDis.AgentManageFn(post_params);
						        }
						        else {
						            AgentDis.AgentManageFn(null);
						        }
						    }
						    else {
						        AgentDis.AgentManageFn(null);
						    }
						},
						"json"
					)
                    break;
            }
        }

    },
    CombileJson: function(sourse_json, air_json) {
        var soursedata = sourse_json;
        parseJson(soursedata, air_json);
        return soursedata;
        function parseJson(o, a_data) {
            for (var item in a_data) {
                if (o[item] == null) {
                    o[item] = a_data[item];
                }
                else {
                    parseChildJson(o, a_data[item], item, o);
                }
            }
        }

        function parseChildJson(o, a_data, index, l_data) {

            for (var item in a_data) {
                if (l_data[index][item] == null) {
                    l_data[index][item] = a_data[item];
                }
                else {

                    if (typeof (a_data[item]) == "object") {
                        parseChildJson(o, a_data[item], item, o[index]);
                    }
                }
            }
        }
    }
}
var _AgentDis = new Object;
var AgentDis = _AgentDis.prototype = {
    AgentResultFn: function(obj, totalCount) {
        return null;
    },
    AgentListDis: function(data) {
        return null;
    },
    AgentManageFn: function(params) {
        return null;
    },
    CommunityDis: function(data) {
        return null;
    }
}