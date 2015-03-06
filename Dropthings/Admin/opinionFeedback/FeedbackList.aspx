<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FeedbackList.aspx.cs" Inherits="Admin_opinionFeedback_FeedbackList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/tab.css" rel="stylesheet" type="text/css" />
    <link href="../css/frame.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.4.2.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/divMove.js"></script>
    <script type="text/javascript" src="../js/listTable.js"></script>
    <%--<script src="../ckeditor/ckeditor.js" type="text/javascript"></script>--%>
    <script src="../ckeditorfull/ckeditor.js" type="text/javascript"></script>
    <script src="../ckfinder/ckfinder.js" type="text/javascript"></script>
    <script src="../js/FeedbackList.js" type="text/javascript"></script>
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
                                <span class="STYLE4">舆情反馈</span> <span style="float: right;">
                                    <input id="create_opinion_feedback_btn" type="button" value="创建" /></span>
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
                                                <td width="34%" background="../images/tab_14.gif" align="center">
                                                    名称
                                                </td>
                                                <td width="10%" background="../images/tab_14.gif" align="center">
                                                    制作人
                                                </td>
                                                <td width="10%" background="../images/tab_14.gif" align="center">
                                                    制作时间
                                                </td>
                                                <td width="10%" background="../images/tab_14.gif" align="center">
                                                    编辑
                                                </td>
                                                <td  style="display: none;"  width="10%" background="../images/tab_14.gif" align="center">
                                                    查看
                                                </td>
                                                <td style="display: none;" width="10%" background="../images/tab_14.gif" align="center">
                                                    审批
                                                </td>
                                                <td width="10%" height="26" background="../images/tab_14.gif" align="center">
                                                    删除
                                                </td>
                                            </tr>
                                            <tbody id="all_data_list">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="display: block;" height="26" bgcolor="#FFFFFF" align="center">
                                                    <%--<input name="checkbox" type="checkbox" value='<%#Eval("ID") %>' />--%><%#Eval("ID") %>
                                                </td>
                                                <td height="26" bgcolor="#FFFFFF" align="center">
                                                    <%#Eval("TITLE")%>
                                                </td>
                                                <td bgcolor="#FFFFFF" align="center">
                                                    <%#Eval("CREATER")%>
                                                </td>
                                                <td bgcolor="#FFFFFF" align="center">
                                                    <%#Eval("CREATETIME")%>
                                                </td>
                                                <td bgcolor="#FFFFFF" align="center">
                                                    <img src="../images/037.gif" width="9" height="9" alt="编辑" />[ <a href="javascript:void(null);"
                                                        onclick="FeedbackList.FindById(this)" name="look_report" pid='<%#Eval("ID") %>'>
                                                        编辑</a> ]
                                                </td>
                                                <td  style="display: none;" bgcolor="#FFFFFF" align="center">
                                                    <img src="../images/037.gif" width="9" height="9" alt="查看" />
                                                    [ <a href="Preview.aspx?id=<%#Eval("ID") %>" name="look_report" pid='<%#Eval("ID") %>'>
                                                        查看</a>]
                                                </td>
                                                <td style="display: none;" bgcolor="#FFFFFF" align="center">
                                                    <img src="../images/037.gif" width="9" height="9" alt="审批" />[ <a href="javascript:void(null);"
                                                        name="audit_report" pid='<%#Eval("ID") %>' status='<%# Eval("STATUS") %>'>审批</a>
                                                    ]
                                                </td>
                                                <td bgcolor="#FFFFFF" align="center">
                                                    <img src="../images/010.gif" width="9" height="9" alt="删除" />[ <a href="javascript:void(null);"
                                                        name="delete_report" onclick="FeedbackList.DelFeedBack(this)" cate="<%#Eval("URL")%>"
                                                        pid='<%#Eval("ID") %>'>删除</a> ]
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
                                            &nbsp;
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
    </div>
    </form>
    <div id="column_edit_frame" class="layerdiv" style="display: none; width: 800px;">
        <%--<span class="layer_line"></span>--%>
        <div class="layer_outer">
            <%--<span class="layer_top_L"></span>
  <span class="layer_top_R"></span>--%>
            <div class="layer_inner">
                <div class="layer_move" id="move_column" title="点击此处可自由拖动">
                    <h1>
                        舆情反馈</h1>
                </div>
                <a class="btn_close" href="javascript:void(null);" id="close_edit_frame"></a>
                <div class="clear">
                </div>
                <div class="layer_C">
                    <table cellspacing="0" cellpadding="0" class="form_list">
                        <tbody>
                            <tr>
                                <td class="form_name">
                                    反馈标题：
                                </td>
                                <td class="form_field">
                                    <input style="height: 30px; line-height: 30px; padding: 0 6px" id="OpinionName" type="text"
                                        value="系统使用情况反馈" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_name">
                                    反馈内容：
                                </td>
                                <td class="form_field">
                                    <textarea cols="100" id="editor1" name="editor1" rows="10">
                                    </textarea>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_name">
                                    创建时间：
                                </td>
                                <td class="form_field">
                                    <input type="text" id="CreateTime_txt" value="2013-08-16 12:55:23" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_name">
                                    创建人：
                                </td>
                                <td class="form_field">
                                    <input type="text" id="CreateName_txt" value="<%=UserName %>" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="layer_B">
                    <a href="javascript:void(null);" id="submit_btn">
                        <img src="../images/btn_send.gif" alt="" id="btn_upload" />
                    </a><span style="color: Red;" id="back_msg"></span>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
