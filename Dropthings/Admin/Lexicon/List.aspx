﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="Admin_Lexicon_List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>词库维护</title>
    <link rel="Stylesheet" href="../css/tab.css" type="text/css" />
    <link rel="Stylesheet" href="../css/frame.css" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/zTreeStyle.css" />  
    <script type="text/javascript" src="../js/jquery-1.4.2.js"></script>
    <script type="text/javascript" src="../js/jquery.ztree-2.6.js"></script>   
    <script type="text/javascript" src="../js/divMove.js"></script>
    <script type="text/javascript" src="../js/listTablebak.js"></script>
    <script type="text/javascript" src="../js/LexiconManage.js"></script> 
</head>
<body style="background:none;">
    <form id="form1" runat="server">
    <div>
    <table width="28%" cellspacing="0" cellpadding="0" border="0" align="center" style="float:left;">
  <tbody><tr>
    <td height="30"><table width="100%" cellspacing="0" cellpadding="0" border="0">
      <tbody><tr>
        <td width="15" height="30"><img width="15" height="30" src="../images/tab_03.gif"></td>
        <td background="../images/tab_05.gif" align="left"><img width="16" height="16" src="../images/311.gif"> <span class="STYLE4">词库类别</span>
        &nbsp;&nbsp;&nbsp;&nbsp;<a href="javascript:void(null);" title="增加" id="BtnAddType">
        <img src="../images/Lexicon_add.gif" border="0" />增加
        </a>
        &nbsp;&nbsp;&nbsp;&nbsp;<a href="javascript:void(null);" title="修改" id="BtnEditType">
             <img src="../images/Lexicon_edit.jpg" border="0" />修改
        </a>
        &nbsp;&nbsp;&nbsp;&nbsp;<a href="javascript:void(null);" title="删除" id="BtnDeleteType">
             <img src="../images/Lexicon_delete.gif" border="0" />删除
        </a>
        </td>
        <td width="14"><img width="14" height="30" src="../images/tab_07.gif"></td>
      </tr>
    </tbody></table></td>
  </tr>
  <tr>
    <td><table width="100%" cellspacing="0" cellpadding="0" border="0">
      <tbody><tr>
        <td width="9" background="../images/tab_12.gif">&nbsp;</td>
        <td bgcolor="#d3e7fc">
			<div class="zTreeDemoBackground">
			    <ul id="lexicon_tree" class="tree"></ul>
		    </div>
        </td>
        <td width="9" background="../images/tab_16.gif">&nbsp;</td>
      </tr>
    </tbody></table></td>
  </tr>
  <tr>
    <td height="29"><table width="100%" cellspacing="0" cellpadding="0" border="0">
      <tbody><tr>
        <td width="15" height="29"><img width="15" height="29" src="../images/tab_20.gif"></td>
        <td background="../images/tab_21.gif">&nbsp;</td>
        <td width="14"><img width="14" height="29" src="../images/tab_22.gif"></td>
      </tr>
    </tbody></table></td>
  </tr>
</tbody></table>
    
    <table width="70%" cellspacing="0" cellpadding="0" border="0" align="center" style="float:left;">
  <tbody><tr>
    <td height="30"><table width="100%" cellspacing="0" cellpadding="0" border="0">
      <tbody><tr>
        <td width="15" height="30"><img width="15" height="30" src="../images/tab_03.gif"></td>
        <td background="../images/tab_05.gif" align="left"><img width="16" height="16" src="../images/311.gif"> <span class="STYLE4">词库列表</span>
        <span id="current_type"></span>
        </td>
        <td width="14"><img width="14" height="30" src="../images/tab_07.gif"></td>
      </tr>
    </tbody></table></td>
  </tr>
  <tr>
    <td><table width="100%" cellspacing="0" cellpadding="0" border="0">
      <tbody><tr>
        <td width="9" background="../images/tab_12.gif">&nbsp;</td>
        <td bgcolor="#d3e7fc">
			<div id="wrapper" >	
	            <div class="content">		            
		            <div class="word_term">
		                <ul><li style="margin:0px;">
		                    <span class="word_reg_head" style="text-align:center; margin:0px;">关键字</span>
		                    <span class="word_weight_head" style="margin:0px;">相关度</span>
		                    <span class="word_btn_head" style="margin:0px;">操作</span>
		                    </li>
		                </ul>
            		    <ul id="term_list"></ul>
		            </div>
		            <div class="clear"></div>
		            <div style="float:left;margin-top:12px;"><a href="javascript:void(null);" id="add_term">
		                <img src="../images/Lexicon_add.gif" border="0" />
		            增加</a></div>
	            </div>
	            <div class="clear"></div>	
            </div>	
        </td>
        <td width="9" background="../images/tab_16.gif">&nbsp;</td>
      </tr>
    </tbody></table></td>
  </tr>
  <tr>
    <td height="29"><table width="100%" cellspacing="0" cellpadding="0" border="0">
      <tbody><tr>
        <td width="15" height="29"><img width="15" height="29" src="../images/tab_20.gif"></td>
        <td background="../images/tab_21.gif">&nbsp;</td>
        <td width="14"><img width="14" height="29" src="../images/tab_22.gif"></td>
      </tr>
    </tbody></table></td>
  </tr>
</tbody></table>


<div id="column_edit_frame" class="layerdiv">
  <%--<span class="layer_line"></span>--%>
  <div class="layer_outer">
  <%--<span class="layer_top_L"></span>
  <span class="layer_top_R"></span>--%>
  <div class="layer_inner"> 
    <div class="layer_move" id="move_column" title="点击此处可自由拖动"><h1>类别管理</h1></div>
    <a class="btn_close" href="javascript:void(null);" id="close_edit_frame"></a>
    <div class="clear"></div>
    <%--<div class="layer_T">
      <h1><b>类别管理</b></h1>
      <span class="btn"></span>
      <div class="clear"></div>
    </div>--%>
    <div class="layer_C">
        <table cellspacing="0" cellpadding="0" class="form_list">
          <tbody>
            <tr>
            <td class="form_name" >类别名称：</td>
            <td class="form_field">
                <input id="TypeName" pid="valueList" type="text" />
            </td>
            </tr>  
            <tr>
                <td class="form_name" >类别描述：</td>
                <td class="form_field">
                    <textarea id="TypeIntroduce" cols="40" rows="2" pid="valueList"></textarea>
                </td>
            </tr> 
            <tr>
                <td class="form_name" >父级类别：</td>
                <td class="form_field">
                    <select id="TypeParentId" pid="valueList"></select>
                </td>
            </tr>
            <tr style="display:none;">
                <td class="form_name" >机构：</td>
                <td class="form_field">
                    <select id="TypeOrgId"  pid="valueList">
                        <option value="-1">—请选择机构—</option>
                        <option value="1">机构1</option>
                        <option value="2">机构2</option>
                    </select>
                </td>
            </tr>    
          </tbody>
        </table>
    </div>
    <div class="layer_B">
        <a href="javascript:void(null);">
            <img src="../images/btn_send.gif" alt="" onclick="javascript:listTable.Add('valueList','back_msg','addlexicontype');" id="btn_add"/>
        </a>
        <a href="javascript:void(null);">
            <img src="../images/btn_rewrite.gif" alt="" onclick="javascript:listTable.Rest();" id="btn_reset"/>
        </a>
         <a href="javascript:void(null);">
            <img src="../images/btn_send.gif" alt="" onclick="javascript:listTable.EditOne('valueList','editlexicontype',null,'column_edit_frame');" id="btn_edit"/>
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
