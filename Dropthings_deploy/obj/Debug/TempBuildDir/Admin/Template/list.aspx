<%@ page language="C#" autoeventwireup="true" inherits="Admin_Template_list, Dropthings_deploy" enableEventValidation="false" theme="GreenBlue" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>模板管理</title>
    <link rel="Stylesheet" href="../css/tab.css" type="text/css" />
    <link rel="Stylesheet" href="../css/frame.css" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.js"></script>
    <script type="text/javascript" src="../js/divMove.js"></script>
    <script type="text/javascript" src="../js/listTable.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="30"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="15" height="30"><img src="../images/tab_03.gif" width="15" height="30" /></td>
        <td background="../images/tab_05.gif" align="left"><img src="../images/311.gif" width="16" height="16" /> <span class="STYLE4">模板列表</span></td>
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
                    <td width="6%" height="26" background="../images/tab_14.gif" class="STYLE1"><div align="center" class="STYLE2 STYLE1">编号</div></td>                    
                    <td width="24%" height="18" background="../images/tab_14.gif" class="STYLE1"><div align="center" class="STYLE2 STYLE1">模板名称</div></td>
                    <td width="41%" height="18" background="../images/tab_14.gif" class="STYLE1"><div align="center" class="STYLE2 STYLE1">模板所在路径</div></td>     
                    <td width="15%" height="18" background="../images/tab_14.gif" class="STYLE1"><div align="center" class="STYLE2 STYLE1">模板类型</div></td>                    
                    <td width="7%" height="18" background="../images/tab_14.gif" class="STYLE1"><div align="center" class="STYLE2">编辑</div></td>
                    <td width="7%" height="18" background="../images/tab_14.gif" class="STYLE1"><div align="center" class="STYLE2">删除</div></td>
                  </tr>          
                  <tbody id="all_data_list">     
                </HeaderTemplate>
                <ItemTemplate>
                  <tr>
                    <td height="18" bgcolor="#FFFFFF"><div align="center" class="STYLE1">
                      <input name="checkbox" type="checkbox" class="STYLE2" value='<%#Eval("ID") %>' /><%#Eval("ID") %>
                    </div></td>
                    <td height="18" bgcolor="#FFFFFF"><div align="center" class="STYLE2 STYLE1"><%#Eval("TemplateName")%></div></td>
                    <td height="18" bgcolor="#FFFFFF"><div align="center" ><a href="#"><%#Eval("TemplatePath")%></a></div></td>
                    <td height="18" bgcolor="#FFFFFF"><div align="center" ><a href="#"><%# GetTemplateType(Eval("TemplateType").ToString())%></a></div></td>
                    <td height="18" bgcolor="#FFFFFF">
                        <div align="center"><img src="../images/037.gif" width="9" height="9" />
                        <span class="STYLE1"> [</span>
                        <a href="javascript:void(null);" onClick="javascript:listTable.initEdit(<%#Eval("ID") %>,'ID','back_msg');">编辑</a>
                        <span class="STYLE1">]</span>
                        </div>
                    </td>
                    <td height="18" bgcolor="#FFFFFF">
                        <div align="center">
                            <span class="STYLE2">
                                <img src="../images/010.gif" width="9" height="9" />
                            </span>
                            <span class="STYLE1">[</span>
                            <a href="javascript:void(null);" onClick="javascript:listTable.Remove(<%#Eval("ID") %>,'您确定要删除么？');">删除</a>
                            <span class="STYLE1">]</span>
                        </div>
                    </td>
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
    <div class="layer_move" id="move_column" title="点击此处可自由拖动"><h1>编辑模板</h1></div>
    <a class="btn_close" href="javascript:void(null);" id="close_edit_frame"></a>
    <div class="clear"></div>
    <%--<div class="layer_T">
      <h1><b>编辑模板</b></h1>
      <span class="btn"></span>
      <div class="clear"></div>
    </div>--%>
    <div class="layer_C">
        <table cellspacing="0" cellpadding="0" class="form_list">
          <tbody>
            <tr>
                <td class="form_name" >模板名称：</td>
                <td class="form_field">
                    <input id="TemplateName" pid="valueList" type="text" />
                </td>
            </tr>  
            <tr>
                <td class="form_name" >模板描述：</td>
                <td class="form_field">
                    <textarea id="TemplateDes" cols="40" rows="2" pid="valueList"></textarea>
                </td>
            </tr>             
            <tr>
                <td class="form_name" >所在路径：</td>
                <td class="form_field">
                    <input id="TemplatePath" pid="valueList" type="text" value="~\" />~代表根目录
                </td>
            </tr> 
            <tr>
                <td class="form_name" >模板类型：</td>
                <td class="form_field">
                    <select id="TemplateType" runat="server" pid="valueList">
                        <option value="-1">—请选择模板类型—</option>
                    </select>
                </td>
            </tr>   
            <tr>
                <td class="form_name" >每页显示条数：</td>
                <td class="form_field">
                    <input id="TemplateParam" pid="valueList" type="text" value="10" />
                </td>
            </tr>
            <tr>
                <td class="form_name" >模板内容：</td>
                <td class="form_field">
                    <textarea id="TemplateCotent" cols="80" rows="20" pid="valueList"></textarea>
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
