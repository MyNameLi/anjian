<%@ page language="C#" autoeventwireup="true" inherits="Admin_lable_list, Dropthings_deploy1" enableEventValidation="false" theme="GreenBlue" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf8" />
    <title>��ǩ����</title>
    <link rel="Stylesheet" href="../css/tab.css" type="text/css" />
    <link rel="Stylesheet" href="../css/frame.css" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.js"></script>
    <script type="text/javascript" src="../js/divMove.js"></script>
    <script type="text/javascript" src="../js/listTable.js"></script>
</head>
<body style="background:#ffffff;">
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="30"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="15" height="30"><img src="../images/tab_03.gif" width="15" height="30" /></td>
        <td background="../images/tab_05.gif" align="left"><img src="../images/311.gif" width="16" height="16" /> <span class="STYLE4">��ǩ�б�</span>
        &nbsp;&nbsp;&nbsp;<span><asp:Button ID="BtnRefresh" runat="server" Text="ˢ�����б�ǩ" 
                onclick="BtnRefresh_Click" /></span>
        </td>
        <td width="14"><img src="../images/tab_07.gif" width="14" height="30" /></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="9" background="../images/tab_12.gif">&nbsp;</td>
        <td bgcolor="#d3e7fc">
        
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CECECE">
            <asp:Repeater ID="dataList" runat="server">
                <HeaderTemplate>
                  <tr>
                    <td width="6%" height="26" background="../images/tab_14.gif" align="center">���</td>                    
                    <td width="24%" background="../images/tab_14.gif" align="center">��ǩ����</td>
                    <td width="56%" background="../images/tab_14.gif" align="center">��ǩ��Ӧֵ</td>                    
                    <td width="7%" background="../images/tab_14.gif" align="center">�༭</td>
                    <td width="7%" background="../images/tab_14.gif" align="center">ɾ��</td>
                  </tr>          
                  <tbody id="all_data_list">     
                </HeaderTemplate>
                <ItemTemplate>
                  <tr>
                    <td height="18" bgcolor="#FFFFFF" align="center">
                      <input name="checkbox" type="checkbox" value='<%#Eval("ID") %>' /><%#Eval("ID") %>
                    </td>
                    <td bgcolor="#FFFFFF" align="center"><%#Eval("LableName")%></td>
                    <td bgcolor="#FFFFFF" align="center"><a href="#"><%#Eval("LableStr")%></a></td>
                    <td bgcolor="#FFFFFF" align="center"><img src="../images/037.gif" width="9" height="9" />[ <a href="javascript:void(null);" onClick="javascript:listTable.initEdit(<%#Eval("ID") %>,'ID','back_msg');">�༭</a> ]</td>
                    <td bgcolor="#FFFFFF" align="center"><img src="../images/010.gif" width="9" height="9" />[ <a href="javascript:void(null);" onClick="javascript:listTable.Remove(<%#Eval("ID") %>,'��ȷ��Ҫɾ��ô��');">ɾ��</a> ]</td>
                  </tr>  
                </ItemTemplate>  
                <FooterTemplate>
                    </tbody>  
                </FooterTemplate>              
            </asp:Repeater>      
        </table></td>
        <td width="9" background="../images/tab_16.gif">&nbsp;</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td height="29"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="15" height="29"><img src="../images/tab_20.gif" width="15" height="29" /></td>
        <td background="../images/tab_21.gif"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="21%" align="left">
                <a href="javascript:void(null);" id="choose_all" name="0" onClick="javascript:listTable.chooseAll('choose_all','all_data_list');">
                    <img alt="ȫѡ" src="../images/q.gif" width="40" height="19" /> 
                </a>
                <a href="javascript:void(null);" onClick="javascript:listTable.InnitAdd();">
                    <img alt="����" src="../images/x.gif" width="55" height="19" /> 
                </a>
                <a href="javascript:void(null);" onClick="javascript:listTable.RemoveAll('all_data_list','removeall','��ȷ��Ҫɾ��ѡ������ô��');">
                    <img alt="ɾ��" src="../images/s.gif" width="55" height="19" />
                </a>
            </td>
            <td width="79%" class="STYLE1">
                <webdiyer:AspNetPager ID="PagerList" runat="server" PageSize="20" 
                        FirstPageText="��ҳ" LastPageText="βҳ" 
                        NextPageText="��һҳ"  PrevPageText="��һҳ" 
                    onpagechanging="PagerList_PageChanging" >
                </webdiyer:AspNetPager>
            </td>
          </tr>
        </table></td>
        <td width="14"><img src="../images/tab_22.gif" width="14" height="29" /></td>
      </tr>
    </table></td>
  </tr>
</table>
<div id="column_edit_frame" class="layerdiv">
  <%--<span class="layer_line"></span>--%>
  <div class="layer_outer">
  <%--<span class="layer_top_L"></span>
  <span class="layer_top_R"></span>--%>
  <div class="layer_inner"> 
    <div class="layer_move" id="move_column" title="����˴��������϶�"><h1>��ǩ����</h1></div>
    <a class="btn_close" href="javascript:void(null);" id="close_edit_frame"></a>
    <div class="clear"></div>
    <%--<div class="layer_T">
      <h1><b>��ǩ����</b></h1>
      <span class="btn"></span>
      <div class="clear"></div>
    </div>--%>
    <div class="layer_C">
        <table cellspacing="0" cellpadding="0" class="form_list">
          <tbody>
            <tr>
            <td class="form_name" >��ǩ���ƣ�</td>
            <td class="form_field">
                <input id="LableName" pid="valueList" type="text" />
            </td>
            </tr>  
            <tr>
                <td class="form_name" >��ǩ������</td>
                <td class="form_field">
                    <textarea id="LableDes" cols="40" rows="2" pid="valueList"></textarea>
                </td>
            </tr> 
            <tr>
                <td class="form_name" >��ǩ���ݣ�</td>
                <td class="form_field">
                    <textarea id="LableStr" cols="40" rows="2" pid="valueList"></textarea>
                </td>
            </tr>                 
          </tbody>
        </table>
    </div>
    <div class="layer_B">
        <a href="javascript:void(null);">
            <img src="../images/btn_send.gif" alt="" onClick="javascript:listTable.Add('valueList','back_msg');" id="btn_add"/>
        </a>
        <a href="javascript:void(null);">
            <img src="../images/btn_rewrite.gif" alt="" onClick="javascript:listTable.Rest();" id="btn_reset"/>
        </a>
         <a href="javascript:void(null);">
            <img src="../images/btn_send.gif" alt="" onClick="javascript:listTable.EditOne('valueList');" id="btn_edit"/>
        </a>
        <span style="color:Red;" id="back_msg"></span>
       </div>
  </div>
  <%--<span class="layer_bottom_L"></span>
  <span class="layer_bottom_R"></span>--%>
  </div>
  <%--<span class="layer_line"></span>--%>
</div>
    </div>
    </form>
</body>
</html>
