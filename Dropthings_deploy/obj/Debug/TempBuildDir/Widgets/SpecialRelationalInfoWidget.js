var _SpecialRelationalInfoWidget = new Object;

var SpecialRelationalInfoWidget = _SpecialRelationalInfoWidget.property = {
    Load: function() {
        if (jQuery("#category_menu li").size() > 0) {
            var data = $("#title_cue").attr("pid");
            var text = $("#title_cue").html();
            var s = { "tab_news": "content_news", "tab_furm": "content_furm", "tab_blog": "content_blog" };

            Common.TabControl(s, "tab_on", "tab_off");

            Special.LookNews(data, text);
        }
    }
}