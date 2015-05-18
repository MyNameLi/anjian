<%@ page language="C#" autoeventwireup="true" inherits="Admin_user_MenuList, Dropthings_deploy" enableEventValidation="false" theme="GreenBlue" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>频道管理</title>
    <link rel="Stylesheet" href="../css/tab.css" type="text/css" />
    <link rel="Stylesheet" href="../css/frame.css" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.js"></script>
    <script type="text/javascript" src="../js/divMove.js"></script>
    <script type="text/javascript" src="../js/listTable.js"></script>
    <script type="text/javascript" src="../js/MenuManage.js"></script>
</head>
<body style="background:#ffffff;">
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="30"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="15" height="30"><img src="../images/tab_03.gif" width="15" height="30" /></td>
        <td background="../images/tab_05.gif" align="left"><img src="../images/311.gif" width="16" height="16" /><span class="STYLE4">频道列表</span></td>
        <td width="14"><img src="../images/tab_07.gif" width="14" height="30" /></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="9" background="../images/tab_12.gif">&nbsp;</td>
        <td bgcolor="#d3e7fc">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CECECE">
              <tr>                
                <td width="31%" height="26" background="../images/tab_14.gif" align="center">频道名称</td>                
                <td width="14%" background="../images/tab_14.gif" align="center">频道对应地址</td>
                <td width="12%" background="../images/tab_14.gif" align="center">编辑属性</td>
                <td width="7%" background="../images/tab_14.gif" align="center">删除</td>
              </tr>
              <tbody id="column_tree"></tbody>             
            </table>
        </td>
        <td width="9" background="../images/tab_16.gif">&nbsp;</td>
      </tr>
    </table>
    </td>
  </tr>
  <tr>
    <td height="29"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="15" height="29"><img src="../images/tab_20.gif" width="15" height="29" /></td>
        <td background="../images/tab_21.gif">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td width="21%" align="left">                   
                    <a href="javascript:void(null);" id="add_one">
                        <img src="../images/x.gif" width="40" height="19" /> 
                    </a>                
                </td>
                <td width="79%">
                   
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
    <div class="layer_move" id="move_column" title="点击此处可自由拖动"><h1>频道管理</h1></div>
    <a class="btn_close" href="javascript:void(null);" id="close_edit_frame"></a>
    <div class="clear"></div>
    <%--<div class="layer_T">
      <h1><b>频道管理</b></h1>
      <span class="btn"></span>
      <div class="clear"></div>
    </div>--%>
    <div class="layer_C">
        <table cellspacing="0" cellpadding="0" class="form_list">
          <tbody>
           <tr>
            <td class="form_name" >频道名称：</td>
            <td class="form_field"><input id="Name" pid="valueList" type="text" /></td>
        </tr>  
         <tr>
            <td class="form_name" >对应内容地址：</td>
            <td class="form_field">
                <input id="UrlPath" pid="valueList" type="text" />
            </td>
        </tr>  
         <tr>
            <td class="form_name" >对应菜单地址：</td>
            <td class="form_field">
                <input id="LeftPath" pid="valueList" type="text" />
            </td>
        </tr>  
        <tr>
            <td class="form_name" >排序：</td>
            <td class="form_field">
                <input id="Sequece" pid="valueList" type="text" />
            </td>
        </tr>       
         <tr>
            <td class="form_name" >父级频道：</td>
            <td class="form_field">
                <select id="ParentID" runat="server" pid="valueList">
                
                </select>
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
