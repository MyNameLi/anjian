<%@ page language="C#" autoeventwireup="true" inherits="Admin_system_SnapShot, Dropthings_deploy" enableEventValidation="false" theme="GreenBlue" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf8" />
    <title>获取快照</title>
    <link rel="Stylesheet" href="../css/tab.css" type="text/css" />
    <link rel="Stylesheet" href="../css/frame.css" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            $("#WebUrl").blur(function() {
                var web_url = $.trim($(this).val());
                if (!web_url) {
                    alert("请填写URL地址");
                    return;
                }
                $("#WebFrame").attr("src", web_url);
            });
            $("#BtnLook").click(function() {
                var web_url = $.trim($("#WebUrl").val());
                $.post(location.href,
                    { "act": "getsnapimgpath", "ajaxString": 1, "web_url": escape(web_url) },
                    function(data) {
                        if (data) {
                            alert("生成成功！");
                        }
                    },
                    "json"
                );
            });
        });
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
        <td background="../images/tab_05.gif" align="left">
        <img src="../images/311.gif" width="16" height="16" /> <span class="STYLE4">获取快照图片</span>
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
        地址：<input type="text" id="WebUrl" runat="server" size="100"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <input type="button" id="BtnLook" value="查看快照" />
            <br />
            <iframe src="" scrolling="auto" width="100%" height="600" id="WebFrame" name="WebFrame" style="background:white;"></iframe>
        
        </td>
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
