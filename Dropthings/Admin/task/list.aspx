<%@ Page Language="C#" AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="Admin_task_list" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>采集任务管理</title>
    <link rel="Stylesheet" href="../css/tab.css" type="text/css" />
    <link rel="Stylesheet" href="../css/frame.css" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.js"></script>
    <script type="text/javascript" src="../js/divMove.js"></script>
    <script type="text/javascript" src="../js/task.js"></script>
    <script type="text/javascript" src="../js/display.js" charset="gb2312"></script>
    <script type="text/javascript" src="../js/BaseMenu.js" charset="gb2312"></script>
</head>
<body style="background:#ffffff;">
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="30"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="15" height="30"><img src="../images/tab_03.gif" width="15" height="30" /></td>
        <td background="../images/tab_05.gif" align="left"><img src="../images/311.gif" width="16" height="16" /> <span class="STYLE4">采集任务列表</span>
        
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
                    <td width="8%" background="../images/tab_14.gif" align="center">任务名称</td>
                    <td width="24%" background="../images/tab_14.gif" align="center">网站入口</td> 
                    <td width="5%" background="../images/tab_14.gif" align="center">网站编码</td>
                    <td width="3%" background="../images/tab_14.gif" align="center">采集深度</td>                    
                    <td width="5%" background="../images/tab_14.gif" align="center">编辑</td>
                    <td width="5%" background="../images/tab_14.gif" align="center">删除</td>
                    <td width="10%" background="../images/tab_14.gif" align="center">生成XML</td>
                    <td colspan="3" width="24%" background="../images/tab_14.gif" align="center">操作</td>
                  </tr>          
                  <tbody id="all_data_list">     
                </HeaderTemplate>
                <ItemTemplate>
                  <tr>
                    <td height="26" bgcolor="#FFFFFF" align="center" width="6%">
                      <%#Eval("TASKID")%>
                    </td>
                    <td bgcolor="#FFFFFF" align="center" width="15%"><%#Eval("TASKNAME")%></td>
                    <td bgcolor="#FFFFFF" align="left" width="24%"><div style="width:300px; overflow:hidden; word-wrap:break-word; word-break:break-asll;"><a href="#"><%#Eval("URLENTRY")%></a></div></td>
                    <td bgcolor="#FFFFFF" align="center" width="5%"><a href="#"><%#Eval("SITECODE")%></a></td>
                    <td bgcolor="#FFFFFF" align="center" width="3%"><a href="#"><%#Eval("SPIDERDEGREE")%></a></td>
                    <td bgcolor="#FFFFFF" align="center" width="5%"><img src="../images/037.gif" width="9" height="9" />[ <a href="javascript:void(null);" name="edit_task" pid='<%#Eval("TASKID") %>'>编辑</a> ]</td>
                    <td bgcolor="#FFFFFF" align="center" width="5%"><img src="../images/010.gif" width="9" height="9" />[ <a href="javascript:void(null);" name="delete_task" pid='<%#Eval("TASKID") %>'>删除</a> ]</td>
                    <td bgcolor="#FFFFFF" align="center" width="12%"><img src="../images/037.gif" width="9" height="9" />[ <a href="javascript:void(null);" name="create_task" pid='<%#Eval("TASKID") %>'>生成XML</a> ]</td>
                    <td bgcolor="#FFFFFF" align="center" width="9%"><img src="../images/037.gif" width="9" height="9" />[ <a href="javascript:void(null);" name="add_task" pid='<%#Eval("TASKID") %>'>添加任务</a> ]</td>
                    <td bgcolor="#FFFFFF" align="center" width="9%"><img src="../images/037.gif" width="9" height="9" />[ <a href="javascript:void(null);" name="start_task" pid='<%#Eval("TASKID") %>'>启动任务</a> ]</td>
                    <td bgcolor="#FFFFFF" align="center" width="9%"><img src="../images/037.gif" width="9" height="9" />[ <a href="javascript:void(null);" name="stop_task" pid='<%#Eval("TASKID") %>'>停止任务</a> ]</td>
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
                <%--<a href="javascript:void(null);" id="choose_all" name="0" onclick="javascript:listTable.chooseAll('choose_all','all_data_list');">
                    <img alt="全选" src="../images/q.gif" width="40" height="19" /> 
                </a>--%>
                <a href="javascript:void(null);" id="add_item">
                    <img alt="新增" src="../images/x.gif" width="40" height="19" /> 
                </a>
                <%--<a href="javascript:void(null);" onclick="javascript:listTable.RemoveAll('all_data_list','removeall','您确定要删除选中内容么？');">
                    <img alt="删除" src="../images/s.gif" width="40" height="19" />
                </a>--%>
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
    <div class="layer_move" id="move_column" title="点击此处可自由拖动"><h1>任务基本信息</h1></div>
    <a class="btn_close" href="javascript:void(null);" id="close_edit_frame"></a>
    <div class="clear"></div>
    <div id="add_frame">
    
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
