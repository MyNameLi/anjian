<%@ page language="C#" autoeventwireup="true" inherits="Admin_warning_keywordlist, Dropthings_deploy" enableEventValidation="false" theme="GreenBlue" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta http-equiv="Content-Type" content="text/html; charset=utf8" />
    <title>敏感词预警设置</title>
    <link rel="Stylesheet" href="../css/tab.css" type="text/css" />
    <link rel="Stylesheet" href="../css/frame.css" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.js"></script>
    <script type="text/javascript" src="../js/divMove.js"></script>
    <script type="text/javascript" src="../js/listTable.js"></script>
    <script type="text/javascript" src="../js/warning.js"></script>
</head>
<body style="background:#ffffff;">
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="30"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="15" height="30"><img src="../images/tab_03.gif" width="15" height="30" /></td>
        <td background="../images/tab_05.gif" align="left"><img src="../images/311.gif" width="16" height="16" /> <span class="STYLE4">敏感词预警设置</span>
        
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
                    <td width="6%" height="26" background="../images/tab_14.gif" align="center">编号</td>                    
                    <td width="53%" background="../images/tab_14.gif" align="center">预警规则</td>
                    <td width="15%" background="../images/tab_14.gif" align="center">信息量阀值</td> 
                    <td width="12%" background="../images/tab_14.gif" align="center">信息接受者设置</td>
                    <td width="7%" background="../images/tab_14.gif" align="center">编辑</td>
                    <td width="7%" background="../images/tab_14.gif" align="center">删除</td>
                  </tr>          
                  <tbody id="all_data_list">     
                </HeaderTemplate>
                <ItemTemplate>
                  <tr>
                    <td height="26" bgcolor="#FFFFFF" align="center">
                      <input name="checkbox" type="checkbox" value='<%#Eval("ID") %>' /><%#Eval("ID") %>
                    </td>
                    <td bgcolor="#FFFFFF" align="center"><%# Eval("WORDRULE").ToString()%></td>
                    <td bgcolor="#FFFFFF" align="center" onclick='javascript:listTable.Edit(this,"editthresholds",<%#Eval("ID") %>,50)'><%#Eval("THRESHOLDS")%></td>                    
                    <td bgcolor="#FFFFFF" align="center"><img src="../images/037.gif" width="9" height="9" />[ <a href="javascript:void(null);" name="set_accpters" pid='<%#Eval("ID") %>'>设置</a> ]</td>
                    <td bgcolor="#FFFFFF" align="center"><img src="../images/037.gif" width="9" height="9" />[ <a href="javascript:void(null);" onClick="javascript:listTable.initEdit(<%#Eval("ID") %>,'ID','back_msg');">编辑</a> ]</td>
                    <td bgcolor="#FFFFFF" align="center"><img src="../images/010.gif" width="9" height="9" />[ <a href="javascript:void(null);" onClick="javascript:listTable.Remove(<%#Eval("ID") %>,'您确定要删除么？');">删除</a> ]</td>
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
                    <img alt="全选" src="../images/q.gif" width="40" height="19" /> 
                </a>
                 <a href="javascript:void(null);" onClick="javascript:listTable.InnitAdd();">
                    <img alt="新增" src="../images/x.gif" width="40" height="19" /> 
                </a>
                <a href="javascript:void(null);" onClick="javascript:listTable.RemoveAll('all_data_list','removeall','您确定要删除选中内容么？');">
                    <img alt="删除" src="../images/s.gif" width="64" height="19" />
                </a>
            </td>
            <td width="79%" class="STYLE1">
                <webdiyer:AspNetPager ID="PagerList" runat="server" PageSize="20" 
                        FirstPageText="首页" LastPageText="尾页" 
                        NextPageText="下一页"  PrevPageText="上一页" 
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
    <div class="layer_move" id="move_column" title="点击此处可自由拖动"><h1>敏感词预警设置</h1></div>
    <a class="btn_close" href="javascript:void(null);" id="close_edit_frame"></a>
    <div class="clear"></div>
    <%--<div class="layer_T">
      <h1><b>敏感词预警设置</b></h1>
      <span class="btn"></span>
      <div class="clear"></div>
    </div>--%>
    <div class="layer_C">
        <table cellspacing="0" cellpadding="0" class="form_list">
          <tbody>
            <tr>
            <td class="form_name" >敏感词规则：</td>
            <td class="form_field">
                <textarea id="WordRule" cols="40" rows="2" pid="valueList"></textarea>
                
            </td>
            </tr>  
            <tr>
                <td class="form_name" >预警阀值：</td>
                <td class="form_field">
                    <input id="Thresholds" pid="valueList" type="text" />
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

<div id="accept_list_frame" class="layerdiv">
  <%--<span class="layer_line"></span>--%>
  <div class="layer_outer">
  <%--<span class="layer_top_L"></span>
  <span class="layer_top_R"></span>--%>
  <div class="layer_inner"> 
    <div class="layer_move" id="accept_list_move" title="点击此处可自由拖动"></div>
    <a class="btn_close" href="javascript:void(null);" id="accept_list_close"></a>
    <div class="clear"></div>
    <div class="layer_T">
      <h1><b>接受信息用户列表</b></h1>
      <span class="btn"></span>
      <div class="clear"></div>
    </div>
    <div class="layer_C"  style="overflow:hidden;height:auto;">
        <ul id="belong_accept_list">
        
        </ul>
    </div>
    <div class="layer_T">
      <h1><b>未接受信息用户列表</b></h1>
      <span class="btn"></span>
      <div class="clear"></div>
    </div>
    <div class="layer_C"  style="overflow:hidden;height:auto;">
        <ul id="other_accept_list">
            
        </ul>
    </div>
    <div style="width:95%; text-align:right; margin-bottom:10px;">
    <input type="button" id="btn_save_accpet" value="保存" />
    <input type="button" id="btn_accept_list" value="确定" /><span style="color:Red;"></span></div>    
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
