<%@ control language="C#" autoeventwireup="true" inherits="WidgetInstanceZone, Dropthings_deploy1" %>
<asp:UpdatePanel ID="WidgetZoneUpdatePanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="widget_zone_container">        
            <asp:Panel ID="WidgetHolderPanel" runat="server" CssClass="widget_zone" >

            </asp:Panel>
            <asp:LinkButton ID="WidgetHolderPanelTrigger" CssClass="widget_holder_panel_post_link" runat="server" OnClick="WidgetHolderPanelTrigger_Click" />        
        </div>        
    </ContentTemplate>
</asp:UpdatePanel>
