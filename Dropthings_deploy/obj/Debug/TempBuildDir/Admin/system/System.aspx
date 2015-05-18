<%@ page language="C#" autoeventwireup="true" inherits="Admin_system_System, Dropthings_deploy" enableEventValidation="false" theme="GreenBlue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统状态</title>
<link type="text/css" href="../css/tab.css" rel="Stylesheet" />
<link type="text/css" href="../css/frame.css" rel="Stylesheet" />
<script type="text/javascript" src="../js/jquery-1.4.2.js"></script>
<script type="text/javascript" src="../js/Config.js"></script>
<script type="text/javascript" src="../js/System.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%" cellspacing="0" cellpadding="0" border="0">
    <tbody>
    
    <tr>
      <td height="30"><table width="100%" cellspacing="0" cellpadding="0" border="0">
          <tbody>
            <tr>
              <td width="15" height="30"><img width="15" height="30" src="../images/tab_03.gif"></td>
              <td background="../images/tab_05.gif" align="left"><img width="16" height="16" src="../images/311.gif"><span class="STYLE4">信息源列表</span></td>
              <td width="14"><img width="14" height="30" src="../images/tab_07.gif"></td>
            </tr>
          </tbody>
        </table></td>
    </tr>
    <tr>
      <td>
      <table width="100%" cellspacing="0" cellpadding="0" border="0">
        <tbody>
        
        <tr>
          <td width="9" background="../images/tab_12.gif">&nbsp;</td>
          <td valign="top" bgcolor="#d3e7fc">
          <table width="99%" cellspacing="1" cellpadding="0" border="0" bgcolor="#CECECE" align="center">
            <tbody>
              <tr>
                <td width="48" background="../images/tab_14.gif" align="center">编号</td>
                <td background="../images/tab_14.gif" align="center">信息源</td>
                <td width="12%" background="../images/tab_14.gif" align="center">今日数据</td>
                <td width="12%" height="26" background="../images/tab_14.gif" align="center">累计数据</td>               
              </tr>
            </tbody>
            <tbody id="system_list">
                          
            </tbody>
          </table>
        </td>
        <td width="9" background="../images/tab_16.gif">&nbsp;</td>
        </tr>
        </tbody>
        
      </table>
      </td>
    </tr>
    <tr>
      <td><table width="100%" height="29" cellspacing="0" cellpadding="0" border="0">
          <tbody>
            <tr>
              <td width="15" height="29"><img width="15" height="29" src="../images/tab_20.gif"></td>
              <td background="../images/tab_21.gif" width="21%" align="left">&nbsp;</td>
              <td align="center" background="../images/tab_21.gif">
                &nbsp;                
              </td>
              <td background="../images/tab_21.gif" width="21%">&nbsp;</td>
              <td width="14"><img width="14" height="29" src="../images/tab_22.gif"></td>
            </tr>
          </tbody>
        </table></td>
    </tr>
    </tbody>
  </table>
    </div>
    </form>
</body>
</html>
