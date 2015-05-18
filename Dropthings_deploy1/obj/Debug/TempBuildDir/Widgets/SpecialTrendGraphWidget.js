var _SpecialTrendGraphWidget = new Object;

var SpecialTrendGraphWidget = _SpecialTrendGraphWidget.property = {
    Load: function() {
        if (jQuery("#category_menu li").size() > 0) {
            var data = $("#title_cue").attr("pid");

            Special.InitPicData(data.split("_")[0]);
        }
    }
}