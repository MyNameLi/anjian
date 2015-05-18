<%@ control language="C#" autoeventwireup="true" inherits="Widgets_DataSourceStatisticsMap, Dropthings_deploy1" %>
<asp:Panel ID="DataSourceStatisticsMap" CssClass="left-tupian" runat="server">
    <div class="left-rdfb-center">
        <div>
            统计时间：<input type="text" style="width:70px;" id="data_source_starttime" onfocus="WdatePicker({isShowClear:true,readOnly:true,dateFmt:'dd/MM/yyyy'})"  />—
            <input type="text" style="width:70px;" id="data_source_endtime" onfocus="WdatePicker({isShowClear:true,readOnly:true,dateFmt:'dd/MM/yyyy'})" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            显示条数：<select id="data_source_disnum">
                <option value="5">5</option>
                <option value="7">7</option>
                <option value="9">9</option>
                <option value="11">11</option>
            </select>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <input  type="button" id="data_source_look" value="查看" />
        </div>
        <div id="data_source_flash">            
        </div>
    </div>
</asp:Panel>
<%--<script type="text/javascript">
    jQuery(function() {--%>
<%-- switch (Common.GetCookie("user_login")) { case "admin": $("#leader_info").hide();
DataSourceStatisticsMap.LeaderName = ""; break; default: location.href = "Leaderindex.html";
break; } jQuery(window).load(function() { ensure({ js: ["Scripts/Pie.js", "Widgets/DataSourceStatisticsMap.js"]
}, function() { DataSourceStatisticsMap.Load(); }); });});--%>
<%--</script>--%>