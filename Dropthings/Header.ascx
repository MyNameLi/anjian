<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="Header"
    EnableViewState="false" %>
<%@ Register Src="~/TabControlPanel.ascx" TagName="TabControlPanel" TagPrefix="dropthings" %>
<div id="header">
    <div id="onpage_menu">
        <asp:UpdatePanel ID="OnPageMenuUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="onpage_menu_wrapper">
                    <div class="login_panel_label">
                        <asp:Label ID="UserNameLabel" runat="server" EnableViewState="false" Text="" Visible="false"></asp:Label>
                        <%--<asp:HyperLink CssClass="btn_user" ID="AccountLinkButton" Text="<%$Resources:SharedResources, MyAccount%>"
                            runat="server" NavigateUrl="~/ManageAccount.aspx" />--%>
                        <%--<a href="ManageAccount.aspx" class="btn_user" onclick="javascript:windows.open();return false;"
                            target="_blank">
                            <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:SharedResources, MyAccount%>" /></a>--%>
                        <asp:HyperLink CssClass="btn_login" ID="LoginLinkButton" Text="<%$Resources:SharedResources, Login%>"
                            runat="server" NavigateUrl="~/LoginPage.aspx" />
                        <asp:HyperLink CssClass="btn_logout" ID="LogoutLinkButton" Text="<%$Resources:SharedResources, Logout%>"
                            runat="server" NavigateUrl="~/Handler/Logout.ashx" />
                        <asp:HyperLink CssClass="btn_logout" ID="HyperLinkManage" Text="<%$Resources:SharedResources, ManagementCenter%>"
                            runat="server" NavigateUrl="~/Admin/Default.aspx" Target="_blank" />
                        <asp:HyperLink CssClass="btn_logout" ID="StartOverButton" Text="<%$Resources:SharedResources, StartOver%>"
                            runat="server" NavigateUrl="~/Handler/Logout.ashx" />
                        <%--<a id="HelpLink" class="btn_help" href="javascript:void(0)" onclick="DropthingsUI.Actions.showHelp()">
                            <asp:Literal ID="ltlHelp" EnableViewState="false" runat="server" Text="<%$Resources:SharedResources, Help%>" /></a>--%>
                        <dropthings:TabControlPanel ID="TabControlPanel" runat="server" />
                    </div>
                    <%--<div class="onpage_menu_wrapper_line_1">
                    </div>
                    <div class="onpage_menu_wrapper_line_2">
                    </div>--%>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <%--<h1>
        <a href="#"></a>
    </h1>
    <div id="HelpDiv">
    </div>--%>
</div>
