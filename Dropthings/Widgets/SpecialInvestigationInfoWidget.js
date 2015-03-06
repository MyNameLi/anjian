var _SpecialInvestigationInfoWidget = new Object;

var SpecialInvestigationInfoWidget = _SpecialInvestigationInfoWidget.property = {
    Load: function() {
        if (jQuery("#category_menu li").size() > 0) {
            var data = $("#title_cue").attr("pid");
            var text = $("#title_cue").html();
            var parent_cate = data.split("_")[1];
            //            if (parent_cate == "202") {
            //                $("#category_event").parents("div.widget").show();
            //            }
            //            else {
            //                $("#category_event").parents("div.widget").hide();
            //            }
            var event = { "event_reson": "content_event_reson", "event_measure": "content_event_measure", "event_about": "content_event_about" };
            Common.TabControl(event, "tab_on", "tab_off");

            Special.LookNews(data, text);
        }
    }
}