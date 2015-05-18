<%@ page language="C#" culture="auto:en-US" uiculture="auto:en-US" autoeventwireup="true" inherits="ManageAccountPage, Dropthings_deploy1" enableEventValidation="false" theme="GreenBlue" %>

<%@ Register Src="~/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="~/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Account</title>
</head>
<body bgcolor="#ffffff">
    <form id="loginform" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"
        ScriptMode="Release">
    </asp:ScriptManager>

    <script type="text/javascript" src="Scripts/MyFramework.js"></script>

    <div id="altpage">
        <div id="altpageWrapper">
            <div id="altpageContent">
                <%--<asp:HyperLink ID="GoBack" EnableViewState="false" runat="server" NavigateUrl="~/Default.aspx">
                    &lt;&lt;&nbsp;<asp:Literal ID="Literal1" EnableViewState="false" runat="server" Text="<%$Resources:SharedResources, Back%>" />
                </asp:HyperLink>
                <br />
                <br />
                <div id="altpageHeading1">
                    <asp:Literal ID="ltlAccountSetting" EnableViewState="false" runat="server" Text="<%$Resources:SharedResources, AccountSetting%>" />
                </div>
                <div id="altpageHeading2">
                    <asp:Literal ID="ltlCareAboutNote" EnableViewState="false" runat="server" Text="<%$Resources:SharedResources, CareAboutNote%>" />
                </div>--%>
                <div id="onpage_menu_wrapper">
                    <div class="login_panel_label">
                        <span>我的账户</span>
                    </div>
                    <div id="onpage_menu_panels">
                        <div id="Widget_Gallery">
                            <div class="onpage_menu_panel widget_showcase">
                            <div class="news_tab">
                              <ul>
                    <li class="tab_on"><a href="#">舆情分类</a></li>
       				<li class="tab_off"><a href="#">舆情推送</a></li>
                    <li class="tab_off"><a href="#">舆情热点</a></li>
                    <li class="tab_off"><a href="#">舆情推送</a></li>
                    <li class="tab_off"><a href="#">实用小工具</a></li>
</ul></div>
                                <table border="0" cellpadding="3" cellspacing="3" class="widget_list">
                                     <tr>
                            <td colspan="2" style="white-space: nowrap;">
                                <asp:Label ID="Message" runat="server" EnableViewState="false" CssClass="altpageHeading1" />
                            </td>
                        </tr>
                        
                           <%--<tr>
                              <td style="white-space:nowrap;" class="align_right">
                                <asp:Label ID="lblEditPersonalInformation" EnableViewState="false" runat="server" 
                                Text="<%$Resources:SharedResources, EditPersonalInformation%>" CssClass="altpageHeading1" />
                              </td>
                              <td width="100%">&nbsp;</td>
                            </tr>
                            <tr>
                              <td width="20%" nowrap="nowrap" valign="top" class="align_right">
                                  <asp:Label ID="ltlEmail" EnableViewState="false" runat="server" 
                                  Text="<%$Resources:SharedResources, Email%>" CssClass="altpageHeading2" />
                              </td>
                              <td>
                                  <asp:TextBox ID="EmailTextbox" runat="server"></asp:TextBox>
                              </td>
                            </tr>
                            <tr>
                                <td />
                                <td >
                                    <asp:Button ID="SaveButton" runat="server" Text="<%$Resources:SharedResources, Save%>" 
                                    EnableViewState="false" OnClick="SaveButton_Click" />&nbsp;
                                </td>                
                            </tr>
                            --%>
                                    <tr>
                                        <td style="white-space: nowrap;" class="align_right">
                                            <asp:Label ID="ltlChangePassword" EnableViewState="false" runat="server" Text="<%$Resources:SharedResources, ChangePassword%>"
                                                CssClass="altpageHeading1" />
                                        </td>
                                        <td width="100%">&nbsp;
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" nowrap="nowrap" valign="top" class="align_right">
                                            <asp:Label ID="ltlOldPassword" EnableViewState="false" runat="server" Text="<%$Resources:SharedResources, OldPassword%>"
                                                CssClass="altpageHeading2" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="OldPasswordTextbox" runat="server" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" nowrap="nowrap" valign="top" class="align_right">
                                            <asp:Label ID="ltlNewPassword" EnableViewState="false" runat="server" Text="<%$Resources:SharedResources, NewPassword%>"
                                                CssClass="altpageHeading2" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="NewPasswordTextbox" runat="server" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" nowrap="nowrap" valign="top" class="align_right">
                                            <asp:Label ID="ConfirmPasswordLabel" EnableViewState="false" runat="server" Text="<%$Resources:SharedResources, ConfirmPassword%>"
                                                CssClass="altpageHeading2" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="ConfirmPasswordTextBox" runat="server" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                        </td>
                                        <td>
                                            <asp:Button ID="ChangePasswordButton" runat="server" Text="<%$Resources:SharedResources, Change%>"
                                                EnableViewState="false" OnClick="ChangePasswordButton_Click" CssClass="onpage_menu_panel_column_btn" />&nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <div>
                                </div>
                            </div>
                        </div>
                        <div class="onpage_menu_wrapper_line_1">
                        </div>
                        <div class="onpage_menu_wrapper_line_2">
                        </div>
                    </div>
                    <%--<asp:HyperLink ID="HyperLink1" EnableViewState="false" runat="server" NavigateUrl="~/Default.aspx">
                    &lt;&lt;&nbsp;<asp:Literal ID="Literal2" EnableViewState="false" runat="server" Text="<%$Resources:SharedResources, Back%>" />
                </asp:HyperLink>--%>
                </div>
            </div>
        </div>
        <%--<uc2:Footer ID="Footer1" runat="server">
    </uc2:Footer>--%>
    </div>
    </form>
</body>
</html>
