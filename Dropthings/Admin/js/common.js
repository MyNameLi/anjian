var _Common = new Object;
var Common = _Common.prototype = {
    CategoryMenu: function(obj, current_class, nocurrent_class, str_where, left, tag) {
        var current_obj = this;
        $.post("../Handler/CategoryMenu.ashx",
		    { "str_where": str_where, "left": left },
		    function(data) {
		        $("#" + obj).empty().html(unescape(data));
		        var child_list = $("#" + obj).children("li");
		        child_list.each(function() {
		            if ($(this).children("ol").length > 0) {
		                $(this).hover(
					        function() {
					            $(this).attr("class", current_class);
					            $(this).children("ol").show();
					        },
					        function() {
					            $(this).attr("class", nocurrent_class);
					            $(this).find("ol").hide();
					        }
				        );
		            }
		        });
		        $("#" + obj).find("li").each(function() {
		            if (tag) {
		                $(this).children("a").click(function() {
		                    current_obj.CategoryMenuClick($(this).attr("pid"), $(this).html());
		                });
		                $(this).children("span").children("a").click(function() {
		                    current_obj.CategoryMenuClick($(this).attr("pid"), $(this).html());
		                });
		            } else {
		                if ($(this).children("ol").length == 0) {
		                    $(this).children("a").click(function() {
		                        current_obj.CategoryMenuClick($(this).attr("pid"), $(this).html());
		                    });
		                    $(this).children("span").children("a").click(function() {
		                        current_obj.CategoryMenuClick($(this).attr("pid"), $(this).html());
		                    });
		                }
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
    CheckUser: function(permission, obj) {
        $.post("../Handler/user.ashx",
			{ "type": "checkuser" },
			function(data) {
			    if (data) {
			        if (data[permission]) {
			            delete data["SuccessCode"];
			            Comman.DoWord();
			        } else {
			            history.go(-1);
			        }
			    } else {
			        history.go(-1);
			    }
			},
			"json"
		)
    },
    DoWork: function() {
        return null;
    },
    GetTimeStr: function(timestr) {
        if (timestr) {
            var time = new Date(timestr);
            return time.getDate() + "/" + (time.getMonth() + 1) + "/" + time.getFullYear();
        } else {
            return null;
        }
    }
}