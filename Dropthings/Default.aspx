<%@ Page Language="C#" Culture="auto:zh-CN" UICulture="auto:zh-CN" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" ValidateRequest="false" %>

<%-- Trace="False"
    TraceMode="SortByCategory" %>--%>
<%@ OutputCache Location="None" NoStore="true" %>
<%@ Register Src="~/Header.ascx" TagName="Header" TagPrefix="dropthings" %>
<%@ Register Src="~/Footer.ascx" TagName="Footer" TagPrefix="dropthings" %>
<%@ Register Src="~/WidgetTabHost.ascx" TagName="WidgetTabHost" TagPrefix="dropthings" %>
<%@ Register Src="~/TabBar.ascx" TagName="TabBar" TagPrefix="dropthings" %>
<%@ Register Src="~/ScriptManagerControl.ascx" TagName="ScriptManagerControl" TagPrefix="dropthings" %>
<%@ Register Src="~/TabControlPanel.ascx" TagName="TabControlPanel" TagPrefix="dropthings" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf8" />
    <title>网络舆情监控系统</title>
    <%--<meta http-equiv="X-UA-Compatible" content="IE=IE7" />--%>
    <%--<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />--%>
    <meta name="Description" content="网络舆情监控系统" />
    <meta name="Keywords" content="网络舆情监控系统" />
    <meta name="Page-topic" content="网络舆情监控系统" />
    <link href="Styles/common.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="Scripts/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="Scripts/jquery-ui-1.7.1.custom.min.js"></script>

    <script type="text/javascript" src="Scripts/jquery.micro_template.js"></script>

    <script type="text/javascript" src="Scripts/Ensure.js"></script>

    <script type="text/javascript" src="Scripts/MyFramework.js"></script>

    <script type="text/javascript" src="Scripts/tabscroll.js"></script>

    <!--[if IE]><script language="javascript" type="text/javascript" src="Scripts/excanvas.min.js"></script><![endif]-->

    <script type="text/javascript" src="Scripts/common.js"></script>

    <script type="text/javascript">
        jQuery(function() {
            //jQuery("div.left-tupian").parents("div.widget").css({ 'float': 'left', "clear": "none" }).last().css("clear", "right");
            //jQuery("#onpage_menu_panels").hide();
            var columns = jQuery("#widget_area_wrapper .column");
            var size = columns.size();
            columns.each(function(i) {
                if ((size == 2 && i == 0) || (size == 3 && i < 2)) {
                    jQuery(this).css("margin-right", "1%");
                }
            });

        });
    </script>

</head>
<body>
    <form id="default_form" runat="server">
    <dropthings:ScriptManagerControl ID="TheScriptManager" runat="server" />
    <%--<div id="container">--%>
    <div class="header">
        <div class="wraper">
            <!-- Render header first so that user can start typing search criteria while the huge runtime and other scripts download -->
            <dropthings:Header ID="Header1" runat="server" />
            <dropthings:TabBar ID="UserTabBar" runat="server" />
            <div class="clear">
            </div>
        </div>
    </div>
    <div id="Progress">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="5" DynamicLayout="false">
            <ProgressTemplate>
                <span>
                    <img id="Img1" src="indicator.gif" align="middle" runat="server" alt="<%$Resources:SharedResources, Loading%>" /></span></ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <div class="main">
        <div class="wraper">
            <div id="contents">
                <div id="contents_wrapper">
                    <div id="widget_area">
                        <div id="widget_area_wrapper">
                            <dropthings:WidgetTabHost runat="server" ID="WidgetTabHost" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="footer">
        <div class="wraper">
            <dropthings:Footer ID="Footer1" runat="server" />
        </div>
    </div>
    <%--</div>--%>
    <!-- Fades the UI -->
    <div id="blockUI" style="display: none; background-color: black; width: 100%; height: 100px;
        position: absolute; left: 0px; top: 0px; z-index: 50000; -moz-opacity: 0.5; opacity: 0.5;
        filter: alpha(opacity=50);" onclick="return false" onmousedown="return false"
        onmousemove="return false" onmouseup="return false" ondblclick="return false">
        &nbsp;
    </div>
    <textarea id="TraceConsole" rows="10" cols="80" style="display: none"></textarea>
    <!-- Template for a new widget placeholder. It's used to create a fake widget
    when you drag & drop a widget from the widget gallery, until the real widget
    is loaded. -->
    <!-- Begin template -->
    <div class="nodisplay">
        <div id="new_widget_template" class="widget">
            <div class="widget_border">
            </div>
            <div class="widget_line">
            </div>
            <div class="widget_outer">
                <div class="widget_header draggable">
                    <table class="widget_header_table" cellspacing="0" cellpadding="0">
                        <tbody>
                            <tr>
                                <td class="widget_title">
                                    <a class="widget_title_label"><!=json.title!></a>
                                </td>
                                <td class="widget_button">
                                    <a class="widget_close widget_box"></a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div id="WidgetResizeFrame" class="widget_resize_frame">
                    <div class="widget_body">
                        正在加载模块...
                    </div>
                </div>
            </div>
        </div>
        <div class="widget_line">
        </div>
        <div class="widget_border">
        </div>
    </div>
    </div>
    <!-- End template -->
    <!-- HTML for the delete popup dialog box -->
    <textarea id="DeleteConfirmPopupPlaceholder" style="display: none">
    &lt;div id="DeleteConfirmPopup"&gt;
        &lt;h1&gt;删除挂件&lt;/h1&gt;
        &lt;p&gt;您确定要删除这个挂件吗?&lt;/p&gt;
        &lt;input id="DeleteConfirmPopup_Yes" type="button" value="是" /&gt;&lt;input id="DeleteConfirmPopup_No" type="button" value="否" /&gt;
    &lt;/div&gt;    
    </textarea>
    </form>
</body>
</html>
