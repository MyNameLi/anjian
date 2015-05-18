<%@ page language="C#" autoeventwireup="true" inherits="Admin_New_sgxxpush, Dropthings_deploy" enableEventValidation="false" theme="GreenBlue" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf8" />
    <title>事故信息导出</title>
    <link rel="Stylesheet" href="../css/tab.css" type="text/css" />
    <link rel="Stylesheet" href="../css/frame.css" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.js"></script>
    <script type="text/javascript" src="../js/SqlSearch.js"></script>
    <script type="text/javascript" src="../js/listTable.js"></script>
    <script src="../js/sgxxexport.js" type="text/javascript"></script>
</head>
<body style="background:#ffffff;">
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="30"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="15" height="30"><img src="../images/tab_03.gif" width="15" height="30" /></td>
        <td background="../images/tab_05.gif" align="left"><img src="../images/311.gif" width="16" height="16" /> <span class="STYLE4">事故信息列表</span>
        
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
                  <tr>    
                    <td width="6%" height="26" background="../images/tab_14.gif" align="center">编号</td>                                          
                    <td height="25" width="40%" background="../images/tab_14.gif" align="center">标题</td>                           
                    <td width="9%" background="../images/tab_14.gif" align="center">时间</td>       
                    <td width="9%" background="../images/tab_14.gif" align="center">来源</td>
                    <td width="9%" background="../images/tab_14.gif" align="center">操作</td>
                  </tr>          
                  <tbody id="all_data_list">     
                
                  <%--<tr>                    
                    <td bgcolor="#FFFFFF" align="center"><%#Eval("LableName")%></td>
                    <td bgcolor="#FFFFFF" align="center"><a href="#"><%#Eval("LableStr")%></a></td>
                    <td bgcolor="#FFFFFF" align="center"><img src="../images/037.gif" width="9" height="9" />[ <a href="javascript:void(null);" onClick="javascript:listTable.initEdit(<%#Eval("ID") %>,'ID','back_msg');">编辑</a> ]</td>
                    <td bgcolor="#FFFFFF" align="center"><img src="../images/010.gif" width="9" height="9" />[ <a href="javascript:void(null);" onClick="javascript:listTable.Remove(<%#Eval("ID") %>,'您确定要删除么？');">删除</a> ]</td>
                  </tr>     --%>           
                    </tbody>  
                
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
                <a href="javascript:void(null);">
                    <img alt="新增" id="export_all" src="../images/x.gif" width="40" height="19" /> 
                </a>
               
            </td>
            <td width="79%" class="STYLE1">   
                <table><tr>     
                <td>       
                <span id="item_info_id"></span></td>
                <td><table class="page" ><tr id="item_pager_list"></tr></table></td>
                </tr> </table>            
            </td>
          </tr>
        </table></td>
        <td width="14"><img src="../images/tab_22.gif" width="14" height="29" /></td>
      </tr>
    </table></td>
  </tr>
</table>
    </div>
    
    <!-- 推送列表 -->
    <div>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="30"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="15" height="30"><img src="../images/tab_03.gif" width="15" height="30" /></td>
        <td background="../images/tab_05.gif" align="left"><img src="../images/311.gif" width="16" height="16" /> <span class="STYLE4">事故信息推送列表</span>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="javascript:void(null);" id="export_doc" name="0" >
                    导出列表
                </a>
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
                  <tr>    
                    <td width="6%" height="26" background="../images/tab_14.gif" align="center">编号</td>                                          
                    <td height="25" width="40%" background="../images/tab_14.gif" align="center">标题</td>                           
                    <td width="9%" background="../images/tab_14.gif" align="center">时间</td>       
                    <td width="9%" background="../images/tab_14.gif" align="center">来源</td>
                    <td width="9%" background="../images/tab_14.gif" align="center">操作</td>
                  </tr>          
                  <tbody id="exportlist">     
                
                  <%--<tr>                    
                    <td bgcolor="#FFFFFF" align="center"><%#Eval("LableName")%></td>
                    <td bgcolor="#FFFFFF" align="center"><a href="#"><%#Eval("LableStr")%></a></td>
                    <td bgcolor="#FFFFFF" align="center"><img src="../images/037.gif" width="9" height="9" />[ <a href="javascript:void(null);" onClick="javascript:listTable.initEdit(<%#Eval("ID") %>,'ID','back_msg');">编辑</a> ]</td>
                    <td bgcolor="#FFFFFF" align="center"><img src="../images/010.gif" width="9" height="9" />[ <a href="javascript:void(null);" onClick="javascript:listTable.Remove(<%#Eval("ID") %>,'您确定要删除么？');">删除</a> ]</td>
                  </tr>     --%>           
                    </tbody>  
                
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
                &nbsp;
            </td>
            <td width="79%" class="STYLE1">   
               &nbsp;            
            </td>
          </tr>
        </table></td>
        <td width="14"><img src="../images/tab_22.gif" width="14" height="29" /></td>
      </tr>
    </table></td>
  </tr>
</table>
    </div>
    </form>
</body>
</html>
