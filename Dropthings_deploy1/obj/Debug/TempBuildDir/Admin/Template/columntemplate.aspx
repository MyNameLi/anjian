<%@ page language="C#" autoeventwireup="true" inherits="Admin_Template_columntemplate, Dropthings_deploy1" enableEventValidation="false" theme="GreenBlue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>栏目对应域</title>
    <link rel="Stylesheet" href="../css/tab.css" type="text/css" />
    <link rel="Stylesheet" href="../css/frame.css" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.js"></script>
    <script type="text/javascript" src="../js/divMove.js"></script>
    <script type="text/javascript" src="../js/listTable.js"></script>
    <script type="text/javascript" src="../js/ColumnTemplate.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="30"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="15" height="30"><img src="../images/tab_03.gif" width="15" height="30" /></td>
        <td background="../images/tab_05.gif" align="left"><img src="../images/311.gif" width="16" height="16" /><span class="STYLE4">栏目对应域</span></td>
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
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td width="21%">
                              
                </td>
                <td width="79%" class="STYLE1">
                   
                </td>
              </tr>
            </table>
            <div class="layerdiv" style="display:block; float:left; width:500px;">
              <%--<span class="layer_line"></span>--%>
              <div class="layer_outer">
              <%--<span class="layer_top_L"></span>
              <span class="layer_top_R"></span>--%>
              <div class="layer_inner">                 
                <div class="layer_C" id="column_tree">
                   
                </div>
              </div>
              <%--<span class="layer_bottom_L"></span>
              <span class="layer_bottom_R"></span>--%>
              </div>
              <%--<span class="layer_line"></span>--%>
            </div> 
            <div id="column_template_frame" class="layerdiv" style="float:left; width:200px; margin-left:30px;">
              <%--<span class="layer_line"></span>--%>
              <div class="layer_outer">
              <%--<span class="layer_top_L"></span>
              <span class="layer_top_R"></span>--%>
              <div class="layer_inner"> 
                <div class="clear"></div>
                <div class="layer_T">
                  <h1><b>域列表</b></h1>
                  <span class="btn"></span>
                  <div class="clear"></div>
                </div>
                <div class="layer_C">
                   <ul id="column_template_tree">
                                
                    </ul>
                </div>    
              </div>
              <%--<span class="layer_bottom_L"></span>
              <span class="layer_bottom_R"></span>--%>
              </div>
              <%--<span class="layer_line"></span>--%>
            </div>               
        </td>
        <td width="9" background="../images/tab_16.gif">&nbsp;</td>
      </tr>
    </table>
    </td>
  </tr>
  <tr>
    <td><table width="100%" border="0" cellspacing="0" cellpadding="0" height="29">
      <tr>
        <td width="15" height="29"><img src="../images/tab_20.gif" width="15" height="29" /></td>
        <td background="../images/tab_21.gif">&nbsp;</td>
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
    <div class="layer_move" id="move_column" title="点击此处可自由拖动"><h1>域管理</h1></div>
    <a class="btn_close" href="javascript:void(null);" id="close_edit_frame"></a>
    <div class="clear"></div>
    <%--<div class="layer_T">
      <h1><b>域管理</b></h1>
      <span class="btn"></span>
      <div class="clear"></div>
    </div>--%>
    <div class="layer_C">
        <table cellspacing="0" cellpadding="0" class="form_list">
          <tbody>
           <tr>
            <td class="form_name" >栏目ID：</td>
            <td class="form_field"><input disabled="disabled" id="ColumnID" pid="valueList" type="text" /></td>
        </tr>  
         <tr>
            <td class="form_name" >域所在位置：</td>
            <td class="form_field">
                <input id="TemplateID" type="text" pid="valueList" />
            </td>
        </tr>  
         <tr>
            <td class="form_name" >域名称：</td>
            <td class="form_field">
                <input id="TemplateName" type="text" pid="valueList" />
            </td>
        </tr>  
        <tr>
            <td class="form_name" >域类型：</td>
            <td class="form_field">
                <select id="TemplateType" pid="valueList">
                    <option value="-1">—请选择域类型—</option>
                    <option value="1">内容域</option>
                    <option value="2">列表域</option>
                    <option value="3">新闻图片轮换域</option>
                    <option value="4">热点聚焦</option>
                </select>
            </td>
        </tr>               
          </tbody>
        </table>
    </div>
    <div class="layer_B">
        <a href="javascript:void(null);">
            <img src="../images/btn_send.gif" alt="" onclick="javascript:listTable.Add('valueList','back_msg');" id="btn_add"/>
        </a>
        <a href="javascript:void(null);">
            <img src="../images/btn_rewrite.gif" alt="" onclick="javascript:listTable.Rest();" id="btn_reset"/>
        </a>
         <a href="javascript:void(null);">
            <img src="../images/btn_send.gif" alt="" onclick="javascript:listTable.EditOne('valueList');" id="btn_edit"/>
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
