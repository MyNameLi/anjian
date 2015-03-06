<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZhuantiSetting.aspx.cs" Inherits="Admin_category_ZhuantiSetting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/common.css" type="text/css" rel="stylesheet" rev="stylesheet"
        media="all" />
    <link href="../css/BriefReport.css" type="text/css" rel="stylesheet" rev="stylesheet"
        media="all" />
    <script type="text/javascript" src="../js/jquery-1.4.2.js"></script>
    <script type="text/javascript" src="../js/My97DatePicker/WdatePicker.js"></script>
    <script src="../js/listTable.js" type="text/javascript"></script>
    <script src="../js/ajaxfileupload.js" type="text/javascript"></script>
    <script type="text/javascript">

        var tbList = [];
        $.ajax({
            url: "../../xmldata/zhuanti.xml",
            type: "GET",
            contentType: "application/x-www-form-urlencoded",
            dataType: "xml",
            cache: false,
            error: function () {
                //_listOther();
            },
            success: function (xml) {
                $(xml).find("data historytopic").each(function (a, b) {
                    var historyTopic = [];
                    var status = $(b).find("status").text();
                    var url = $(b).find("url").text();
                    var title = $(b).find("title").text();
                    var imgurl = $(b).find("imgurl").text();
                    var data = $(b).find("data").text();
                    historyTopic.push("<tr>");
                    historyTopic.push("<td>" + title + "</td>");
                    historyTopic.push("<td align='left'>" + url + "</td>");
                    historyTopic.push("<td align='left'>" + imgurl + "</td>");
                    historyTopic.push("<td align='left'>" + data + "</td>");
                    // historyTopic.push("<td></td>"); //<a class="del btn_delete" href="ZhuantiSetting.aspx?act='delxmlnode'&url='www.google.com'">删除</a>                    </td>
                    historyTopic.push("</tr>");
                    tbList.push(historyTopic.join(""));
                    // _listOther();
                });
                $("#tbdTopic").empty().append(tbList.reverse().join(""));

            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" action="ZhuantiSetting.aspx" method="post">
    <table width="100%" cellspacing="0" cellpadding="0" border="0" align="center">
        <tbody>
            <tr>
                <td height="30">
                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td width="15" height="30">
                                    <img width="15" height="30" src="../images/tab_03.gif">
                                </td>
                                <td background="../images/tab_05.gif" align="left">
                                    <img width="16" height="16" src="../images/311.gif">
                                    <span class="STYLE4">当前位置：专题更新&nbsp;&nbsp;</span>
                                </td>
                                <td width="14">
                                    <img width="14" height="30" src="../images/tab_07.gif" alt="" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td width="9" background="../images/tab_12.gif">
                                    &nbsp;
                                </td>
                                <td bgcolor="#d3e7fc">
                                    <div class="crumbsTitle">
                                        <b>专题更新</b></div>
                                    <div class="crumbsContent">
                                        <ul class="form_list">
                                            <li><span class="name">链接：</span> <span class="input">
                                                <asp:TextBox ID="ztUrl" Width="300" runat="server"></asp:TextBox>
                                            </span></li>
                                            <li><span class="name">标题：</span><span class="input">
                                                <asp:TextBox ID="ztTitle" Width="300" runat="server"></asp:TextBox>
                                            </span></li>
                                            <li><span class="name">时间：</span><span class="input">
                                                <asp:TextBox ID="ztDate" Width="300" onclick="WdatePicker();" runat="server"></asp:TextBox></span>
                                            </li>
                                            <li><span class="name">图片：</span> <span class="input">
                                                <asp:TextBox ID="txt_imgpath" Width="300" runat="server"></asp:TextBox>
                                            </span>
                                                <input type="file" name="post_file_path" id="post_file_path" />
                                                <input type="button" class="input_button31" value="上传" onclick="javascript:listTable.UpLoadFile('post_file_path','txt_imgpath','CommonFileLoad.ashx');" />
                                            </li>
                                        </ul>
                                        <asp:Button ID="Button1" runat="server" CssClass=" tj_two_search" Text="更新专题" OnClick="Button1_Click" />
                                    </div>
                                    <div class="crumbsTitle">
                                        <b>历史专题列表</b></div>
                                    <div class="elent_tablebg">
                                        <table border="1" cellspacing="1">
                                            <thead>
                                                <tr>
                                                    <td class="topic_tit">
                                                        标题
                                                    </td>
                                                    <td class="topic_tit">
                                                        页面连接
                                                    </td>
                                                    <td class="topic_tit">
                                                        图片连接
                                                    </td>
                                                    <td class="topic_tit">
                                                        时间
                                                    </td>
                                                    <%--<td colspan="2" class="topic_tit">
                                                        操作
                                                    </td>--%>
                                                </tr>
                                            </thead>
                                            <tbody id="tbdTopic">
                                                <tr>
                                                    <td>
                                                        加载中...
                                                    </td>
                                                    <td align="left">
                                                    </td>
                                                    <td align="left">
                                                    </td>
                                                    <td align="left">
                                                    </td>
                                                    <td>
                                                        <a class="del btn_delete" href="ZhuantiSetting.aspx?act='delxmlnode'&url='www.google.com'">
                                                            删除</a>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </td>
                                <td width="9" background="../images/tab_16.gif">
                                    &nbsp;
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
