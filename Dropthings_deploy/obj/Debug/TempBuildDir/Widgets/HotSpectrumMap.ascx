<%@ control language="C#" autoeventwireup="true" inherits="Widgets_HotSpectrumMap, Dropthings_deploy" %>
<div class="left-tupian" style="position: relative; clear: both; overflow: visible;">
    <img id="SGMap" width="360" height="360" alt="最新热点分布" src="" />
    <div id="hotspotMapData" style="z-index: 80; overflow: visible;">
    </div>
</div>
<div class="publicFeelSearchContent">
    <%--<span id="hot_prompt" style="margin-left: 50px; font-weight: bold">点击左侧图中红色方块，可在此区域获取文章列表</span>--%>
    <ul id="doc_list" class="news_list">
    </ul>
    <!--<div class="publicFeelSearchPage"><span class="current">1</span> <a href="#10">[2]</a> <a href="#20">[3]</a> <a href="#30">[4]</a> <a href="#40">[5]</a> <a href="#50">[6]</a> <a href="#60">[7]</a> <a href="#70">[8]</a> <a href="#80">[9]</a> <a href="#90">[10]</a> <a href="#10">下一页</a> </div>-->
</div>

<%--<script type="text/javascript">
    jQuery(function() {
        ensure({ js: ["Widgets/HotSpectrumMap.js"] }, function() { HotSpectrumMap.Load(); });
    });
</script>--%>
