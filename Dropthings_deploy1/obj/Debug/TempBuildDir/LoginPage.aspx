<%@ page language="C#" culture="auto:en-US" uiculture="auto:en-US" autoeventwireup="true" inherits="LoginPage, Dropthings_deploy1" theme="" enableEventValidation="false" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户登录</title>
    <link href="styles/login.css" rel="Stylesheet" type="text/css" />
    <script src="Scripts/form.js" type="text/javascript"></script>
</head>
<body>
    <form id="loginform" runat="server">
    <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="login_center" valign="top">
                            <table align="center" cellpadding="0" cellspacing="0" class="login_T">
                                <tr>
                                    <td class="login_T_L">
                                    </td>
                                    <td class="login_T_C" valign="top">
                                        <h1 class="login_logo">
                                        </h1>
                                    </td>
                                    <td class="login_T_R">
                                    </td>
                                </tr>
                            </table>
                            <table align="center" cellpadding="0" cellspacing="0" class="login_C">
                                <tr>
                                    <td class="login_C_L">
                                    </td>
                                    <td class="login_C_C" valign="top">
                                        <h1 class="login_title">
                                        </h1>
                                        <ul class="form_list">
                                            <asp:Label ID="LabelError" CssClass="tig" runat="server" Visible="false"></asp:Label>
                                            <%--<li class="tig">
                                                </li>--%>
                                            <li><span class="form_id">用户名：</span><span class="form_input">
                                                <asp:TextBox ID="Email" runat="Server" Width="196" />
                                            </span><span class="form_note"></span></li>
                                            <li><span class="form_id">密 码：</span><span class="form_input">
                                                <asp:TextBox ID="Password" TextMode="Password" runat="Server" Width="196" /></span><span
                                                    class="form_note"></span></li>
                                            <li><span class="form_id">验证码：</span><span class="form_input"><asp:TextBox ID="VerifyCode"
                                                runat="Server" Width="98" AutoCompleteType="Disabled" autocomplete="off" /></span><span
                                                    class="form_note"></span></li>
                                            <li class="verify"><span class="form_verify">
                                                <img id="verifyCode" src="CheckCode.aspx" alt="看不清，点击换一个" onclick="javascript:this.src='CheckCode.aspx?id='+Math.random();" /></span><span
                                                    class="form_verify_txt"><%--<a href="javascript:void(0);">--%>看不清，点击图片换一个<%--</a>--%></span></li>
                                        </ul>
                                        <p class="form_btn">
                                            <asp:Button ID="ButtonLogin" CssClass="btn_login" runat="server" OnClick="LoginButton_Click" />
                                            <span class="btn_login_txt">&copy; 版权信息</span>
                                            <%--<asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />--%>
                                        </p>
                                    </td>
                                    <td class="login_C_R">
                                    </td>
                                </tr>
                            </table>
                            <table align="center" cellpadding="0" cellspacing="0" class="login_B">
                                <tr>
                                    <td class="login_B_L">
                                    </td>
                                    <td class="login_B_R">
                                        <a class="btn_login_help" href="#"></a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <%--<uc2:Footer ID="Footer1" runat="server"></uc2:Footer>--%>
    </form>
</body>
</html>
