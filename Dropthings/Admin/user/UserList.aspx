<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserList.aspx.cs" Inherits="Admin_user_UserList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>用户管理</title>
    <link rel="Stylesheet" href="../css/tab.css" type="text/css" />
    <link rel="Stylesheet" href="../css/frame.css" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.js"></script>
    <script type="text/javascript" src="../js/divMove.js"></script>
    <script type="text/javascript" src="../js/listTable.js"></script>
    <script type="text/javascript">
        listTable.Display = function(data) {
            var rolelist = unescape(data["RoleIdList"]).split(',');
            User.rolelist = rolelist;
            $("#RoleId").val(rolelist);
            $("#role_list").find(":checkbox[name='rolelist']").removeAttr("checked");
            for (var i = 0, j = rolelist.length; i < j; i++) {
                var roleid = rolelist[i];
                $("#role_list").find(":checkbox[name='rolelist'][value='" + roleid + "']").attr("checked", "checked");
                User.InnitRoleListClickFn();
            }
        }
        $(document).ready(function() {
            User.Innit();
        });
        var User = new Object;
        User.rolelist = null;
        User.Innit = function() {
            $.post(location.Href,
		        { "act": "innitRole", "ajaxString": 1 },
		        function(data) {
		            if (data.Error == "1") {
		                alert("初始化失败！");
		            }
		            if (data.Success == "1") {
		                delete data["Success"];
		                var content = [];
		                for (var item in data) {
		                    content.push("<input name=\"rolelist\" type=\"checkbox\" value=\"" + item + "\"/>" + unescape(data[item]) + "&nbsp;&nbsp;");
		                }
		                $("#role_list").empty().html(content.join(""));
		                User.InnitRoleListClickFn();
		            }
		        },
		        "json"
	        );
        }
        User.InnitRoleListClickFn = function() {
            $("#role_list").find(":checkbox[name='rolelist']").click(function() {
                var list = [];
                $("#role_list").find("input:checked").each(function() {
                    var key = $(this).val();
                    list.push(key);
                });
                $("#RoleId").val(list.join(","));
            });
        }
    </script>
</head>
<body style="background:#ffffff;">
    <form id="form1" runat="server">
    <div>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="30"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="15" height="30"><img src="../images/tab_03.gif" width="15" height="30" /></td>
        <td background="../images/tab_05.gif" align="left"><img src="../images/311.gif" width="16" height="16" /> <span class="STYLE4">用户列表</span>   
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="SearchUserName" runat="server"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp; 
            <asp:Button ID="BtnSearchUsers" runat="server" Text="搜索" 
                onclick="BtnSearchUsers_Click" />   
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
                    <td width="16%" background="../images/tab_14.gif" align="center">用户帐号</td>
                    <td width="8%" background="../images/tab_14.gif" align="center">用户名</td>
                    <td width="28%" background="../images/tab_14.gif" align="center">电子信箱</td>
                    <td width="16%" background="../images/tab_14.gif" align="center">机构名称</td> 
                    <td width="12%" background="../images/tab_14.gif" align="center">职位</td>
                    <td width="7%" background="../images/tab_14.gif" align="center">编辑</td>
                    <td width="7%" background="../images/tab_14.gif" align="center">删除</td>
                  </tr>          
                  <tbody id="all_data_list">     
                </HeaderTemplate>
                <ItemTemplate>
                  <tr>
                    <td height="26" bgcolor="#FFFFFF" align="center">
                      <input name="checkbox" type="checkbox" value='<%#Eval("USERID") %>' /><%#Eval("USERID")%>
                    </td>
                    <td bgcolor="#FFFFFF" align="center"><%#Eval("ACCID")%></td>
                    <td bgcolor="#FFFFFF" align="center"><%#Eval("USERNAME")%></td>
                    <td bgcolor="#FFFFFF" align="center"><%#Eval("EMAIL")%></td>                    
                    <td bgcolor="#FFFFFF" align="center"><%#Eval("ORGNAME")%></a></td>
                    <td bgcolor="#FFFFFF" align="center"><%#Eval("POSITION")%></td>
                    <td bgcolor="#FFFFFF" align="center"><img src="../images/037.gif" width="9" height="9" />[ <a href="javascript:void(null);" onClick="javascript:listTable.initEdit(<%#Eval("USERID") %>,'USERID','back_msg','','',true);">编辑</a> ]</td>
                    <td bgcolor="#FFFFFF" align="center"><img src="../images/010.gif" width="9" height="9" />[ <a href="javascript:void(null);" onClick="javascript:listTable.Remove(<%#Eval("USERID") %>,'您确定要删除么？');">删除</a> ]</td>
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
    <div class="layer_move" id="move_column" title="点击此处可自由拖动"><h1>用户管理</h1></div>
    <a class="btn_close" href="javascript:void(null);" id="close_edit_frame"></a>
    <div class="clear"></div>
    <%--<div class="layer_T">
          <h1><b>用户管理</b></h1>
          <span class="btn"></span>
          <div class="clear"></div>
        </div>--%>
    <div class="layer_C">
        <table cellspacing="0" cellpadding="0" class="form_list">
          <tbody>
            <tr>
            <td class="form_name" >用户帐号：</td>
            <td class="form_field">
                <input id="AccId" pid="valueList" type="text" />
            </td>
            </tr>  
            <tr>
                <td class="form_name" >用户密码：</td>
                <td class="form_field">
                    <input id="PassWord" pid="valueList" type="text" />
                </td>
            </tr> 
            <tr>
                <td class="form_name" >用户名：</td>
                <td class="form_field">
                    <input id="UserName" pid="valueList" type="text" />
                </td>
            </tr>    
            <tr>
                <td class="form_name" >电子邮件：</td>
                <td class="form_field">
                    <input id="Email" pid="valueList" type="text" />
                </td>
            </tr>    
            <tr>
                <td class="form_name" >手机号码：</td>
                <td class="form_field">
                    <input id="Mobile" pid="valueList" type="text" />
                </td>
            </tr>     
            <tr>
                <td class="form_name" >所属角色：</td>
                <td class="form_field" >
                    <input id="RoleId" pid="valueList" type="hidden" />
                    <div id="role_list"></div>   
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
