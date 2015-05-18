<%@ page language="C#" autoeventwireup="true" inherits="Admin_Default, Dropthings_deploy1" enableEventValidation="false" theme="GreenBlue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>后台管理中心</title>
</head>
<frameset rows="55,*,23" cols="*" framespacing="0" frameborder="no" border="0">
  <frame src="top.html" name="topFrame" scrolling="No" noresize="noresize" id="topFrame" />
  <frameset id="allFrame" rows="*" cols="171,*">
    <frame src="" name="mainFrame" id="mainFrame" noresize="noresize"/>
    <frame src="" id="tabFrame" name="tabFrame" />
  </frameset>
  
  <frame src="down.html" name="bottomFrame" scrolling="No" noresize="noresize" id="bottomFrame" />
</frameset>
<noframes>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</noframes>
</html>