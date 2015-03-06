<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LatestNewsWidget.ascx.cs"
    Inherits="Widgets_LatestNewsWidget" %>
<asp:Panel ID="SettingsPanel" runat="Server" Visible="False">
    <asp:Literal ID="ltlShow" EnableViewState="false" runat="server" Text="<%$Resources:SharedResources, Show%>" />
    <asp:DropDownList ID="LatestNewsCountDropDownList" runat="Server">
        <asp:ListItem>3</asp:ListItem>
        <asp:ListItem>6</asp:ListItem>
        <asp:ListItem>10</asp:ListItem>
        <asp:ListItem>15</asp:ListItem>
    </asp:DropDownList>
    <asp:Literal ID="Literal1" EnableViewState="false" runat="server" Text="<%$Resources:SharedResources, Items%>" />
    <asp:Button ID="SaveSettings" runat="Server" OnClick="SaveSettings_Click" Text="<%$Resources:SharedResources, Save%>" />
</asp:Panel>
<div style="padding:10px 15px 0 13px;">
    <ul>
        <li style=" line-height:24px;">
        信息类型：
        <input name="last_news_radio" checked="checked" type="radio" value=""/>所有&nbsp;&nbsp;&nbsp;
        <input name="last_news_radio" type="radio" value="news"/>新闻&nbsp;&nbsp;&nbsp;
        <input name="last_news_radio" type="radio" value="blog"/>博客&nbsp;&nbsp;&nbsp;
        <input name="last_news_radio" type="radio" value="bbs"/>论坛&nbsp;&nbsp;&nbsp;
        <input name="last_news_radio" type="radio" value="weibo"/>微博&nbsp;&nbsp;&nbsp;    
        </li>
    </ul>  
</div>
<asp:Panel ID="LatestNewsContainer" runat="server">  
    
    <ul class="news_list" id="last_news_info">
        <table border="0" cellpadding="0" cellspacing="0" height="180" width="100%"><tr><td align="center" class="widget_loading">数据正在加载中...</td></tr></table>
    </ul>
    
</asp:Panel>
<div class="news_list" id="last_news_more" style="margin:0 3%; position:relative; text-align:right;"><a href='Widgets/LatestNewsDetail.aspx?type=more' target='_blank'>更多信息&raquo;</a></div>
