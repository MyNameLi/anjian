$(document).ready(function() {


    ThesaurusList.InitThesaurus();

});


var zTreeNoodeId;
var setting = {
    callback: {
        onClick: zTreeOnClick
    }
};

var _thesaurusList = new Object;
var ThesaurusList = _thesaurusList.property = {
    queryparams: { "action": "getkeyworddata", "orderby": "" },
    innitdata: { "page_size": 15, "result_id": "newsList", "status_bar_id": "all_pager_list",
        "info_id": "all_info_id", "sql_tag": "all", "web_url": "../Handler/SqlSearch.ashx"
    },
    //线索来源词典，待增加词。。。
    HashSite: { "58": "58同城", "ganji": "赶集网" }
    ,
    zTreeObj: null,
    zNodes: null,
    currNode: null,
    InitThesaurus: function() {
        var url = location.url;
        var act = "initial";
        $.post(url,
		    { "act": act, "ajaxString": 1 },
		    function(data) {
		        if (data != "") {
		            ThesaurusList.zNodes = data;
		            $.fn.zTree.init($("#treeThesaurus"), setting, ThesaurusList.zNodes);
		            ThesaurusList.zTreeObj = $.fn.zTree.getZTreeObj("treeThesaurus");
		            var allnodes = ThesaurusList.zTreeObj.getNodes();
		            if (allnodes) {
		                ThesaurusList.ExpandTree(allnodes, 1, 1);
		            }
		        }
		    },
		    "json"
	    );
    },
    ExpandTree: function(data, level, expandlevel) {
        for (var item in data) {
            ThesaurusList.zTreeObj.expandNode(data[item]);
            if (level <= expandlevel) {
                var childnodes = data[item];
                if (childnodes) {
                    ThesaurusList.ExpandTree(childnodes, level + 1, expandlevel);
                }
            }
        }
    },
    Search: function(keywordid) {
        var where = " kid= " + keywordid;
        if (keywordid.indexOf(",") != -1) {
            where = " kid in(" + keywordid + ")";
        }
        var tag = $("#processType").val();
        where = where + " and tag=" + tag;
        ThesaurusList.queryparams["strwhere"] = where;
        var sqlpager = new SqlPager(ThesaurusList.innitdata);
        sqlpager.NoDataDis = function(obj) {
            $("#" + obj).empty().html("<tr><td colspan=\"3\" align=\"center\">没有数据</td></tr>");
        }
        sqlpager.Display = function(obj, data) {
            delete data["Count"];
            var content = [];
            for (var item in data) {
                var row = data[item];
                content.push("<tr>");
                content.push("<td width=\"30%\" align=\"left\" class=\"wmry_table_count\"><a href=\"" + unescape(row["url"]) + "\"");
                content.push(" target=\"_blank\" title=\"" + unescape(row["title"]) + "\">" + unescape(row["title"]) + "</a></td>");
                content.push("<td align=\"center\"  width=\"9%\" class=\"wmry-table_date\">" + (unescape(row["source"]) == "" ? "无" : unescape(row["source"])) + "</td>");
                content.push("<td align=\"center\"  width=\"40%\" class=\"wmry-table\">" + unescape(row["summary"]) + "</td>");
                content.push("<td align=\"center\" width=\"9%\" class=\"wmry-table_date\">" + unescape(row["datetime"]).substr(0, 10) + "</td>");
                content.push("<td align=\"center\"  width=\"7%\"  class=\"wmry-table_form\">" + (unescape(row["tag"]) == "0" ? "未处理" : unescape(row["tag"]) == "1" ? "已删除" : "已忽略") + "</td>");
                content.push("<td align=\"center\" width=\"5%\" class=\"wmry-table_date\"><input name=\"cbProcessSel\" type=\"checkbox\" value=\"" + unescape(row["id"]) + "\"/></td>");
                content.push("</tr>");
            }
            $("#" + obj).empty().html(content.join(""));
            ThesaurusList.InitFinishLoadFun();
        }
        sqlpager.LoadData(1, ThesaurusList.queryparams);
    }, InitFinishLoadFun: function() {

        $("#cbProcessAllSel").unbind("click").click(function() {
            var isChecked = $(this).attr("checked");
            $("input[name='cbProcessSel']").attr("checked", isChecked);
        });
        $("#markDel").unbind("click").click(function() {
            var selLength = $("input[name='cbProcessSel']:checked").length;
            if (selLength <= 0) {
                alert("请选择要操作的记录");
                return;
            }
            var content = [];
            $("input[name='cbProcessSel']:checked").each(function() {
                content.push($(this).val());
            });
            $.post(location.href,
			{ "act": "processed", "ajaxString": 1, "idList": content.join(",") },
			function(data) {
			    if (data.Success == "1") {
			        ThesaurusList.Search(zTreeNoodeId);
			    }
			},
			"json"
		);
        });
        $("#markIgnored").unbind("click").click(function() {
            var selLength = $("input[name='cbProcessSel']:checked").length;
            if (selLength <= 0) {
                alert("请选择要操作的记录");
                return;
            }
            var content = [];
            $("input[name='cbProcessSel']:checked").each(function() {
                content.push($(this).val());
            });
            $.post(location.href,
			{ "act": "ignore", "ajaxString": 1, "idList": content.join(",") },
			function(data) {
			    if (data.Success == "1") {
			        ThesaurusList.Search(zTreeNoodeId);
			    }
			},
			"json"
		);
        });
    }, InnitAdd: function() {
        listTable.InnitAdd();
    },
    InitEdit: function(id_str, back_msg) {
        if (!zTreeNoodeId) {
            alert("请先选择要编辑的节点!");
            return;
        }
        listTable.initEdit(zTreeNoodeId, id_str, back_msg);


    }, Remove: function(msg, act, url) {

        if (!zTreeNoodeId) {
            alert("请先选择要删除的节点!");
            return;
        }
        if (!act) {
            act = "remove";
        }
        if (confirm(msg)) {
            if (!url) {
                url = location.href;
            }
            $.post(url,
			{ "act": act, "ajaxString": 1, "idList": zTreeNoodeId },
			function(data) {
			    if (data.Error == "1") {
			        alert("删除失败，原因是：" + data.ErrorStr);
			    }
			    if (data.Success == "1") {
			        location.replace(location.href);
			    }
			},
			"json"
		);
        }
    },
    EditOne: function(params, act, url, disframe) {
        listTable.EditOne(params, act, url, disframe);
    }
}
//单击树节点事件
//treeNode:当前点击的节点
function zTreeOnClick(event, treeId, treeNode) {
    var l_nodes = treeNode.childs;
    zTreeNoodeId = treeNode.id
    //当前节点没有子节点  或者 当前节点的子节点数量为0 就认为当前节点为最子节点
    //做where is 条件查询
    // if (!l_nodes || l_nodes.length == 0) {
    //  var keyword_id = treeNode.id.slice(1);
    ThesaurusList.Search(treeNode.id);
    // }
    //当前节点包含子节点&&子节点数量大于0&&子节点的id格式为"_"+数字(zTree插件定义的json格式T_T)
    //就认为当前节点为二级父节点，可将子节点id汇总，做where in 条件查询
    //    else if (l_nodes && l_nodes.length > 0 && l_nodes[0].id.slice(1) != "") {
    //        var kid = [];
    //        for (var i = 0; i < l_nodes.length; i++) {
    //            kid.push(l_nodes[i].id.slice(1));
    //        }
    //        ThesaurusList.Search(kid.join(","));
    //    }
}
function GetOriginalSiteName(link) {
    var newlink = link.split('.')[1];
    return ThesaurusList.HashSite[newlink];
}
