<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleList.aspx.cs" Inherits="Admin_user_RoleList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>角色管理</title>
    <link rel="Stylesheet" href="../css/tab.css" type="text/css" />
    <link rel="Stylesheet" href="../css/frame.css" type="text/css" />

    <script type="text/javascript" src="../js/jquery-1.4.2.js"></script>

    <script type="text/javascript" src="../js/divMove.js"></script>

    <script type="text/javascript" src="../js/listTable.js"></script>

    <script type="text/javascript" src="../js/RoleManage.js"></script>

</head>
<body style="background: #ffffff;">
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td height="30">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="15" height="30">
                                <img src="../images/tab_03.gif" width="15" height="30" />
                            </td>
                            <td background="../images/tab_05.gif" align="left">
                                <img src="../images/311.gif" width="16" height="16" />
                                <span class="STYLE4">角色列表</span>
                            </td>
                            <td width="14">
                                <img src="../images/tab_07.gif" width="14" height="30" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="9" background="../images/tab_12.gif">
                                &nbsp;
                            </td>
                            <td bgcolor="#d3e7fc">
                                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CECECE">
                                    <asp:Repeater ID="dataList" runat="server">
                                        <HeaderTemplate>
                                            <tr>
                                                <td width="6%" height="26" background="../images/tab_14.gif" align="center">
                                                    编号
                                                </td>
                                                <td width="15%" background="../images/tab_14.gif" align="center">
                                                    角色名
                                                </td>
                                                <td width="18%" background="../images/tab_14.gif" align="center">
                                                    角色描述
                                                </td>
                                                <td width="11%" background="../images/tab_14.gif" align="center">
                                                    栏目分配
                                                </td>
                                                <td width="12%" background="../images/tab_14.gif" align="center">
                                                    自定义挂件分配
                                                </td>
                                                <td width="12%" background="../images/tab_14.gif" align="center">
                                                    后台模块分配
                                                </td>
                                                <td width="12%" background="../images/tab_14.gif" align="center">
                                                    查看用户
                                                </td>
                                                <td width="7%" background="../images/tab_14.gif" align="center">
                                                    编辑
                                                </td>
                                                <td width="7%" background="../images/tab_14.gif" align="center">
                                                    删除
                                                </td>
                                            </tr>
                                            <tbody id="all_data_list">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td height="26" bgcolor="#FFFFFF" align="center">
                                                    <input name="checkbox" type="checkbox" value='<%#Eval("ROLEID") %>' /><%#Eval("ROLEID")%>
                                                </td>
                                                <td bgcolor="#FFFFFF" align="center">
                                                    <%#Eval("ROLENAME")%>
                                                </td>
                                                <td bgcolor="#FFFFFF" align="center">
                                                    <a href="#">
                                                        <%#Eval("DESCRIPTION")%></a>
                                                </td>
                                                <td bgcolor="#FFFFFF" align="center">
                                                    <img src="../images/037.gif" width="9" height="9" />[ <a href="javascript:void(null);"
                                                        name="role_tab" pid='<%#Eval("ROLEID")%>'>栏目分配</a> ]
                                                </td>
                                                <td bgcolor="#FFFFFF" align="center">
                                                    <img src="../images/037.gif" width="9" height="9" />[ <a href="javascript:void(null);"
                                                        name="role_permission" pid='<%#Eval("ROLEID")%>'>自定义挂件分配</a> ]
                                                </td>
                                                <td bgcolor="#FFFFFF" align="center">
                                                    <img src="../images/037.gif" width="9" height="9" />[ <a href="javascript:void(null);"
                                                        name="role_menu_permission" pid='<%#Eval("ROLEID")%>'>后台模块分配</a> ]
                                                </td>
                                                <td bgcolor="#FFFFFF" align="center">
                                                    <img src="../images/037.gif" width="9" height="9" />[ <a href="javascript:void(null);"
                                                        name="look_user" pid='<%#Eval("ROLEID")%>'>查看用户</a> ]
                                                </td>
                                                <td bgcolor="#FFFFFF" align="center">
                                                    <img src="../images/037.gif" width="9" height="9" />[ <a href="javascript:void(null);"
                                                        onclick="javascript:listTable.initEdit(<%#Eval("ROLEID") %>,'ROLEID','back_msg');">
                                                        编辑</a> ]
                                                </td>
                                                <td bgcolor="#FFFFFF" align="center">
                                                    <img src="../images/010.gif" width="9" height="9" />[ <a href="javascript:void(null);"
                                                        onclick="javascript:listTable.Remove(<%#Eval("ROLEID") %>,'您确定要删除么？');">删除</a>
                                                    ]
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                            <td width="9" background="../images/tab_16.gif">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="29">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="15" height="29">
                                <img src="../images/tab_20.gif" width="15" height="29" />
                            </td>
                            <td background="../images/tab_21.gif">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="21%">
                                            <a href="javascript:void(null);" onclick="javascript:listTable.InnitAdd();">
                                                <img alt="新增" src="../images/x.gif" width="40" height="19" />
                                            </a>
                                        </td>
                                        <td width="79%" class="STYLE1">
                                            <webdiyer:AspNetPager ID="PagerList" runat="server" PageSize="20" FirstPageText="首页"
                                                LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" OnPageChanging="PagerList_PageChanging">
                                            </webdiyer:AspNetPager>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="14">
                                <img src="../images/tab_22.gif" width="14" height="29" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="column_edit_frame" class="layerdiv">
            <%--<span class="layer_line"></span>--%>
            <div class="layer_outer">
                <%--<span class="layer_top_L"></span><span class="layer_top_R"></span>--%>
                <div class="layer_inner">
                    <div class="layer_move" id="move_column" title="点击此处可自由拖动"><h1>角色管理</h1></div>
                    <a class="btn_close" href="javascript:void(null);" id="close_edit_frame"></a>
                    <div class="clear">
                    </div>
                    <%--<div class="layer_T">
                        <h1>
                            <b>角色管理</b></h1>
                        <span class="btn"></span>
                        <div class="clear">
                        </div>
                    </div>--%>
                    <div class="layer_C">
                        <table cellspacing="0" cellpadding="0" class="form_list">
                            <tbody>
                                <tr>
                                    <td class="form_name">
                                        角色名称：
                                    </td>
                                    <td class="form_field">
                                        <input id="RoleName" pid="valueList" type="text" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_name">
                                        角色描述：
                                    </td>
                                    <td class="form_field">
                                        <textarea id="Description" cols="40" rows="2" pid="valueList"></textarea>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="layer_B">
                        <a href="javascript:void(null);">
                            <img src="../images/btn_send.gif" alt="" onclick="javascript:listTable.Add('valueList','back_msg');"
                                id="btn_add" />
                        </a><a href="javascript:void(null);">
                            <img src="../images/btn_rewrite.gif" alt="" onclick="javascript:listTable.Rest();"
                                id="btn_reset" />
                        </a><a href="javascript:void(null);">
                            <img src="../images/btn_send.gif" alt="" onclick="javascript:listTable.EditOne('valueList');"
                                id="btn_edit" />
                        </a><span style="color: Red;" id="back_msg"></span>
                    </div>
                </div>
                <%--<span class="layer_bottom_L"></span><span class="layer_bottom_R"></span>--%>
            </div>
            <%--<span class="layer_line"></span>--%>
        </div>
        <div id="role_user_frame" class="layerdiv">
            <%--<span class="layer_line"></span>--%>
            <div class="layer_outer">
                <%--<span class="layer_top_L"></span><span class="layer_top_R"></span>--%>
                <div class="layer_inner">
                    <div class="layer_move" id="role_user_move" title="点击此处可自由拖动"><h1>用户列表</h1></div>
                    <a class="btn_close" href="javascript:void(null);" id="role_user_close"></a>
                    <div class="clear">
                    </div>
                    <%--<div class="layer_T">
                        <h1>
                            <b>用户列表</b></h1>
                        <span class="btn"></span>
                        <div class="clear">
                        </div>
                    </div>--%>
                    <div class="layer_C" id="role_user_list" style="overflow: hidden; height: auto;">
                    </div>
                </div>
                <%--<span class="layer_bottom_L"></span><span class="layer_bottom_R"></span>--%>
            </div>
            <%--<span class="layer_line"></span>--%>
        </div>
        <div id="role_tab_frame" class="layerdiv">
            <%--<span class="layer_line"></span>--%>
            <div class="layer_outer">
                <%--<span class="layer_top_L"></span><span class="layer_top_R"></span>--%>
                <div class="layer_inner">
                    <div class="layer_move" id="role_tab_move" title="点击此处可自由拖动"><h1>栏目分配</h1></div>
                    <a class="btn_close" href="javascript:void(null);" id="role_tab_close"></a>
                    <div class="clear">
                    </div>
                    <%--<div class="layer_T">
                        <h1>
                            <b>栏目分配</b></h1>
                        <span class="btn"></span>
                        <div class="clear">
                        </div>
                    </div>--%>
                    <div class="layer_C" id="role_tab_list" style="overflow: hidden; height: auto;">
                    </div>
                    <div style="width: 95%; text-align: right; margin-bottom: 10px;">
                        <input type="button" id="edit_tab_list" value="模块分配" /><span style="color: Red;"></span></div>
                </div>
                <%--<span class="layer_bottom_L"></span><span class="layer_bottom_R"></span>--%>
            </div>
            <%--<span class="layer_line"></span>--%>
        </div>
        
        <div id="role_permission_frame" class="layerdiv">
            <%--<span class="layer_line"></span>--%>
            <div class="layer_outer">
                <%--<span class="layer_top_L"></span><span class="layer_top_R"></span>--%>
                <div class="layer_inner">
                    <div class="layer_move" id="role_permission_move" title="点击此处可自由拖动"><h1>前台模块列表</h1></div>
                    <a class="btn_close" href="javascript:void(null);" id="role_permission_close"></a>
                    <div class="clear">
                    </div>
                    <%--<div class="layer_T">
                        <h1>
                            <b>前台模块列表</b></h1>
                        <span class="btn"></span>
                        <div class="clear">
                        </div>
                    </div>--%>
                    <div class="layer_C" id="widget_list" style="overflow: hidden; height: auto;">
                    </div>
                    <div style="width: 95%; text-align: right; margin-bottom: 10px;">
                        <input type="button" id="edit_widget_list" value="模块分配" /><span style="color: Red;"></span></div>
                </div>
                <%--<span class="layer_bottom_L"></span><span class="layer_bottom_R"></span>--%>
            </div>
            <%--<span class="layer_line"></span>--%>
        </div>
        
        
        <div id="role_menu_farme" class="layerdiv">
            <%--<span class="layer_line"></span>--%>
            <div class="layer_outer">
                <%--<span class="layer_top_L"></span><span class="layer_top_R"></span>--%>
                <div class="layer_inner">
                    <div class="layer_move" id="role_menu_move" title="点击此处可自由拖动"><h1>后台模块列表</h1></div>
                    <a class="btn_close" href="javascript:void(null);" id="role_menu_close"></a>
                    <div class="clear">
                    </div>
                    <%--<div class="layer_T">
                        <h1>
                            <b>后台模块列表</b></h1>
                        <span class="btn"></span>
                        <div class="clear">
                        </div>
                    </div>--%>
                    <div class="layer_C" id="all_menu_list" style="overflow: hidden; height: auto;">
                    </div>
                    <div style="width: 95%; text-align: right; margin-bottom: 10px;">
                        <input type="button" id="edit_menu_list" value="模块分配" /><span style="color: Red;"></span></div>
                </div>
                <%--<span class="layer_bottom_L"></span><span class="layer_bottom_R"></span>--%>
            </div>
            <%--<span class="layer_line"></span>--%>
        </div>
    </div>
    </form>
</body>
</html>
