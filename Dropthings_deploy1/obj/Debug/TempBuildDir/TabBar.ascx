<%@ control language="C#" autoeventwireup="true" inherits="TabBar, Dropthings_deploy1" %>
<%@ Register Src="WidgetContainer.ascx" TagName="WidgetContainer" TagPrefix="widget" %>
<asp:UpdatePanel ID="TabUpdatePanel" runat="server" UpdateMode="conditional">
    <ContentTemplate>
       <div id="tab_container" class="tab_container">
            <div class="tab_container_header" id="tabHeader">
                <ul class="tabs tab-strip" runat="server" id="tabList">
                    <li class="tab inactivetab"><a id="Page1Tab" href="javascript:void(0)"><b>新页面1</b></a></li>
                    <li class="tab inactivetab"><a id="Page2Tab" href="javascript:void(0)"><b>新页面2</b></a></li>
                </ul>
           </div>
         <%-- 2011-11-21 郭同禹 
          <div class="tab_container_options">
                <div class="newtabscrolling" style="display:none;"><asp:LinkButton id="addNewTabLinkButton1" runat="server" CssClass="newtab_add newtab_add_block" OnClick="addNewTabLinkButton_Click"></asp:LinkButton></div>
           </div>--%>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
    jQuery(document).ready(function () {
        DropthingsUI.init("<%= this.EnableTabSorting %>");
    });
</script>

