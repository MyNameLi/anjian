<%@ page language="C#" autoeventwireup="true" inherits="Admin_BriefReport_AccidentReport, Dropthings_deploy" enableEventValidation="false" theme="GreenBlue" %>

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
    <script type="text/javascript">
        function CheckEmputy() {
            var result = true;

            $("input[type='text'],textarea").each(function () {
                if ($(this).val() == "") {
                    $("#Label1").text("数据不可为空");
                    this.focus();
                    result = false;
                    return false;
                }
            });
            return result;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" action="AccidentReport.aspx" method="post">
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
                                    <span class="STYLE4">当前位置：事故网络信息&nbsp;&nbsp;</span>
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
                                        <b>事故网络信息</b></div>
                                    <div class="crumbsContent">
                                        <ul class="form_list">
                                            <li><span class="name">监测日期：</span> <span class="input">
                                                <asp:TextBox ID="JCData" onclick="WdatePicker();" Text="2013-07-01" Width="300" runat="server"></asp:TextBox>
                                            </span></li>
                                            <li><span class="name">标题链接：</span><span class="input">
                                                <asp:TextBox ID="TiteHref" Width="300" Text="http://finance.qq.com/a/20130701/015492.htm"
                                                    runat="server"></asp:TextBox>
                                            </span></li>
                                            <li><span class="name">标&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;题：</span><span
                                                class="input">
                                                <asp:TextBox ID="Title" Width="300" Text="重庆朝天门码头趸船脱缰翻船" runat="server"></asp:TextBox>
                                            </span></li>
                                            <li><span class="name">信息来源：</span><span class="input">
                                                <asp:TextBox ID="InformationSource" Width="300" Text="证券时报" runat="server"></asp:TextBox>
                                            </span></li>
                                            <li><span class="name">发生时间：</span><span class="input">
                                                <asp:TextBox ID="OccurrenceTime" Width="300" onclick="WdatePicker({dateFmt:'MM月dd日 HH时mm分'});"
                                                    Text="07月01日 11时01分" runat="server"></asp:TextBox></span> </li>
                                            <li><span class="name">发生地点：</span><span class="input">
                                                <asp:TextBox ID="OccurrenceAddress" Width="300" Text="重庆，渝中区" runat="server"></asp:TextBox></span>
                                            </li>
                                            <li><span class="name">死伤人数：</span><span class="input">
                                                <asp:TextBox ID="NumberOfDeath" Width="50" Text="1" runat="server"></asp:TextBox>死
                                                <asp:TextBox ID="NumberOfInjured" Width="50" Text="5" runat="server"></asp:TextBox>伤</span>
                                            </li>
                                            <li><span class="name">发布时间：</span><span class="input">
                                                <asp:TextBox ID="ReleaseTime" Width="300" onclick="WdatePicker({dateFmt:'MM月dd日 HH:mm'});"
                                                    Text="07月01日 12:01" runat="server"></asp:TextBox></span> </li>
                                            <li><span class="name">事故经过：</span><span class="input">
                                                <asp:TextBox ID="AccidentProcess" runat="server" TextMode="MultiLine" Columns="90"
                                                    Rows="5">受昨日嘉陵江流域强降暴雨影响，嘉陵江重庆段水位24小时内陡涨10.77米。今日上午11时许，重庆市轮船总公司所属63号工作趸船因洪峰冲击发生断缆漂流翻覆，趸船上共有18人落水。</asp:TextBox></span>
                                            </li>
                                            <li><span class="name">处理报道：</span><span class="input">
                                                <asp:TextBox ID="AccidentStory" runat="server" TextMode="MultiLine" Columns="90"
                                                    Rows="5">重庆市海事局7月1日消息称，受昨日嘉陵江流域强降暴雨影响，嘉陵江重庆段水位24小时内陡涨10.77米。今日上午11时许，重庆市轮船总公司所属63号工作趸船因洪峰冲击发生断缆漂流翻覆，趸船上共有18人落水（其中公司员工17人、员工小孩1人）。</asp:TextBox></span>
                                            </li>
                                            <li><span class="name">舆情分析：</span><span class="input">
                                                <asp:TextBox ID="PublicOpinionAnalysis" runat="server" TextMode="MultiLine" Columns="90"
                                                    Rows="5">6月26日00:08最早一篇报道出现，截至27日10时，已产生新闻报道113条，新浪微博消息14条。</asp:TextBox>
                                                <%--<textarea cols="90" rows="5" id="Textarea2"></textarea>--%></span> </li>
                                            <li><span class="name">是否审批：</span><span class="input">
                                                <asp:CheckBox ID="CheckShenHe" runat="server" Checked="false" Text="是" />
                                                <%--<asp:RadioButton ID="RadioButton1" runat="server" GroupName="shenhe" Text="需要审核" />--%>
                                                <%--<asp:RadioButton ID="RadioButton2" runat="server" GroupName="shenhe" Text="不需要" />--%>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="制作人："></asp:Label>
                                            </span></li>
                                        </ul>
                                        <asp:Button ID="Button1" runat="server" CssClass=" tj_two_search" Text="生成报表" OnClientClick="return CheckEmputy();"
                                            OnClick="Button1_Click" />
                                        <asp:Label ID="Label1" runat="server" ForeColor="#FF3300"></asp:Label>
                                    </div>
                                    <%--<div class="crumbsTitle">
                                        <b>历史专题列表</b></div>--%>
                                    <%--<div class="elent_tablebg">
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
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>--%>
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
