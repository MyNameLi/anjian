<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HotMapWidget.ascx.cs"
    Inherits="Widgets_HotMapWidget" %>
<asp:Panel ID="HotMapContainer" runat="server" >
    <%--<div class="left-rdfb-center" style="overflow: visible; width: 340px;">--%>
        <div class="left-tupian" style="clear: both; height:360px; overflow: visible; position: relative; width:360px;">
            <img id="hot_map_img" alt="最新热点分布" width="360" height="360" src="" />
            <div id="mapData" style="z-index: 80; overflow: visible;">
            </div>
        </div>
    <%--</div>--%>
    <div class="publicFeelSearchContent">
        <%--<span id="hot_prompt" style="margin-left: 50px; font-weight: bold">点击左侧图中红色方块，可在此区域获取文章列表</span>--%>
        <ul id="whats_hot" class="news_list">
        </ul>
    </div>
</asp:Panel>
