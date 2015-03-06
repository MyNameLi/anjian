var _SpecialPipelineGraph = new Object;

var SpecialPipelineGraph = _SpecialPipelineGraph.property = {
    Load: function() {
        if (jQuery("#category_menu li").size() > 0) {
            var data = $("#title_cue").attr("pid");
            var category_id = data.split("_")[0];
            var parent_cate = data.split("_")[1];
            Special.InitTransData(category_id, 1);
        }
    }
}