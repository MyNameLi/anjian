<%@ page language="C#" autoeventwireup="true" inherits="Admin_BriefReport_list, Dropthings_deploy" enableEventValidation="false" theme="GreenBlue" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf8" />
    <title>�������</title>
    <link rel="Stylesheet" href="../css/tab.css" type="text/css" />
    <link rel="Stylesheet" href="../css/frame.css" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.js"></script>
    <script type="text/javascript" src="../js/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../js/ajaxfileupload.js"></script>
    <script type="text/javascript" src="../js/divMove.js"></script>
    <script type="text/javascript" src="../js/listTable.js"></script>
    <script type="text/javascript" src="../js/ReportList.js"></script>
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
                                <span class="STYLE4">�����б�</span> <span style="float: right;">
                                    <input id="report_upload" type="button" value="�ϴ���" /></span>
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
                                                    ���
                                                </td>
                                                <td width="34%" background="../images/tab_14.gif" align="center">
                                                    ����
                                                </td>
                                                <td width="10%" background="../images/tab_14.gif" align="center">
                                                    ������
                                                </td>
                                                <td width="10%" background="../images/tab_14.gif" align="center">
                                                    ����ʱ��
                                                </td>
                                                <td width="10%" background="../images/tab_14.gif" align="center">
                                                    �鿴
                                                </td>
                                                <td width="10%" background="../images/tab_14.gif" align="center">
                                                    ����
                                                </td>
                                                <td width="10%" background="../images/tab_14.gif" align="center">
                                                    ɾ��
                                                </td>
                                            </tr>
                                            <tbody id="all_data_list">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td height="26" bgcolor="#FFFFFF" align="center">
                                                    <input name="checkbox" type="checkbox" value='<%#Eval("ID") %>' /><%#Eval("ID") %>
                                                </td>
                                                <td bgcolor="#FFFFFF" align="center">
                                                    <%#Eval("TITLE")%>
                                                </td>
                                                <td bgcolor="#FFFFFF" align="center">
                                                    <%#Eval("CREATER")%>
                                                </td>
                                                <td bgcolor="#FFFFFF" align="center">
                                                    <%#Eval("CREATETIME")%>
                                                </td>
                                                <td bgcolor="#FFFFFF" align="center">
                                                    <img src="../images/037.gif" width="9" height="9" alt="�鿴" />[ <a href="javascript:void(null);"
                                                        name="look_report" pid='<%#Eval("URL")%>'>�鿴</a> ]
                                                </td>
                                                <td bgcolor="#FFFFFF" align="center">
                                                    <img src="../images/037.gif" width="9" height="9" alt="����" />[ <a href="javascript:void(null);"
                                                        name="audit_report" pid='<%#Eval("ID") %>' status='<%# Eval("STATUS") %>'>����</a>
                                                    ]
                                                </td>
                                                <td bgcolor="#FFFFFF" align="center">
                                                    <img src="../images/010.gif" width="9" height="9" alt="ɾ��" />[ <a href="javascript:void(null);"
                                                        name="delete_report" cate="<%#Eval("URL")%>" pid='<%#Eval("ID") %>'>ɾ��</a> ]
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
                                            <webdiyer:AspNetPager ID="PagerList" runat="server" PageSize="20" FirstPageText="��ҳ"
                                                LastPageText="βҳ" NextPageText="��һҳ" PrevPageText="��һҳ" OnPageChanging="PagerList_PageChanging">
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
    <div id="column_edit_frame" class="layerdiv">
        <%--<span class="layer_line"></span>--%>
        <div class="layer_outer">
            <%--<span class="layer_top_L"></span>
  <span class="layer_top_R"></span>--%>
            <div class="layer_inner">
                <div class="layer_move" id="move_column" title="����˴��������϶�">
                    <h1>
                        ���ϴ�</h1>
                </div>
                <a class="btn_close" href="javascript:void(null);" id="close_edit_frame"></a>
                <div class="clear">
                </div>
                <%--    <div class="layer_T">
      <h1><b>���ϴ�</b></h1>
      <span class="btn"></span>
      <div class="clear"></div>
    </div>--%>
                <div class="layer_C">
                    <table cellspacing="0" cellpadding="0" class="form_list">
                        <tbody>
                            <tr>
                                <td class="form_name">
                                    �����ƣ�
                                </td>
                                <td class="form_field">
                                    <input id="ReportName" type="text" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_name">
                                    �����ˣ�
                                </td>
                                <td class="form_field">
                                    <input id="ReportCreater" value="�������鴦" type="text" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_name">
                                    ����ʱ�䣺
                                </td>
                                <td class="form_field">
                                    <input id="ReportCreateTime" style="width: 100px;" onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})"
                                        type="text" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_name">
                                    �ļ���ַ��
                                </td>
                                <td class="form_field">
                                    <input id="ReportFilePath" name="ReportFilePath" size="60" type="file" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_name">
                                    �����ͣ�
                                </td>
                                <td class="form_field">
                                    <select id="ReportType">
                                        <option value="1">�ܱ�</option>
                                        <option value="2">ר��</option>
                                        <option value="3">�±�</option>
                                        <option value="4">�¹���Ϣ</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_name">
                                    �Ƿ�������
                                </td>
                                <td class="form_field">
                                    <select id="ReportTag">
                                        <option value="3">��</option>
                                        <option value="1">��</option>
                                    </select>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="layer_B">
                    <a href="javascript:void(null);">
                        <img src="../images/btn_send.gif" alt="" id="btn_upload" />
                    </a><span style="color: Red;" id="back_msg"></span>
                </div>
            </div>
            <%--<span class="layer_bottom_L"></span>
  <span class="layer_bottom_R"></span>--%>
        </div>
        <%--<span class="layer_line"></span>--%>
    </div>
</body>
</html>
