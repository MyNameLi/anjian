<%@ page language="C#" autoeventwireup="true" inherits="statistic, Dropthings_deploy" enableEventValidation="false" theme="GreenBlue" %>
<%@ OutputCache Location="None" NoStore="true" %>
<%@ Register Src="~/ScriptManagerControl.ascx" TagName="ScriptManagerControl" TagPrefix="dropthings" %>
<%@ Register Src="~/Header.ascx" TagName="Header" TagPrefix="dropthings" %>
<%@ Register Src="~/TabBar.ascx" TagName="TabBar" TagPrefix="dropthings" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>首页</title>
       <link href="Styles/common.css" rel="Stylesheet" type="text/css" />
    <link href="styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script src="Scripts/statistic.js" type="text/javascript"></script>
    
    <script type="text/javascript" src="Scripts/jquery-ui-1.7.1.custom.min.js"></script>

    <script type="text/javascript" src="Scripts/jquery.micro_template.js"></script>

    <script type="text/javascript" src="Scripts/Ensure.js"></script>

    <script type="text/javascript" src="Scripts/MyFramework.js"></script>

    <script type="text/javascript" src="Scripts/tabscroll.js"></script>

    <!--[if IE]><script language="javascript" type="text/javascript" src="Scripts/excanvas.min.js"></script><![endif]-->

    
    <script src="Scripts/statement.js" type="text/javascript"></script>

    <script type="text/javascript" src="Scripts/jquery.flash.js"></script>

    <script type="text/javascript" src="Scripts/FusionCharts.js"></script>

    <script src="Scripts/Pie.js" type="text/javascript"></script>

    <script src="Scripts/swfobject.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <dropthings:ScriptManagerControl ID="TheScriptManager" runat="server" />
    <div class="header">
  <div class="wraper"> 
    <!-- Render header first so that user can start typing search criteria while the huge runtime and other scripts download -->
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
  </div>
</div>
<div class="main">
 <div class="wraper">
            <div id="contents">
                <div id="contents_wrapper">
                    <div id="widget_area">
                        <div id="widget_area_wrapper">
                            <div>
                                <div class="column" style="width: 49%; float: left;">
                                    <div class="widget_zone_container">
                                        <div id="WidgetTabHost_WidgetZone192_WidgetHolderPanel" class="widget_zone ui-sortable">
                                            <div id="Widget1038_Widget" class="widget widget_hover">
                                                <div class="widget_header draggable">
                                                    <div>
                                                        <table class="widget_header_table" cellpadding="0" cellspacing="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="widget_title" colspan="4">
                                                                        信息汇总
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="widget_resize_frame" style="display: block;">
                                            <table>
                                                <tr>
                                                    <td valign="top">
                                                        <div class="map" style="width: 550px; height: 360px; clear: both;" id="total_faan_frame">
                                                            <div id="total_faan_flash">
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div>
                                        </div>
                                    </div>
                                </div>
                                <div class="column" style="width: 49%; float: right;">
                                    <div class="widget_zone_container">
                                        <div id="WidgetTabHost_WidgetZone192_WidgetHolderPanel" class="widget_zone ui-sortable">
                                            <div id="Widget1038_Widget" class="widget widget_hover">
                                                <div class="widget_header draggable">
                                                    <div>
                                                        <table class="widget_header_table" cellpadding="0" cellspacing="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="widget_title" colspan="4">
                                                                        日期
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                       <div class="widget_resize_frame" style="display: block;">
                                            <table>
                                                <tr>
                                                    <td valign="top">
                                                        <div class="map" style="width:550px; height:360px; clear:both;" id="total_date_frame">
                                                            <div id="total_date_flash">
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="kong_1">
                            </div>
                            <div>
                                <div class="column" style="width: 49%; float: left;">
                                    <div class="widget_zone_container">
                                        <div id="WidgetTabHost_WidgetZone192_WidgetHolderPanel" class="widget_zone ui-sortable">
                                            <div id="Widget1038_Widget" class="widget widget_hover">
                                                <div class="widget_header draggable">
                                                    <div>
                                                        <table class="widget_header_table" cellpadding="0" cellspacing="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="widget_title" colspan="4">
                                                                       站点统计
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="widget_resize_frame" style="display: block;">
                                        <table>
                                                <tr>
                                                    <td valign="top">
                                                        <div class="map" style="width:550px; height:360px; clear:both;" id="total_faan_frame">
                                                            <div id="total_site_flash">
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="column" style="width: 49%; float: right;">
                                    <div class="widget_zone_container">
                                        <div id="WidgetTabHost_WidgetZone192_WidgetHolderPanel" class="widget_zone ui-sortable">
                                            <div id="Widget1038_Widget" class="widget widget_hover">
                                                <div class="widget_header draggable">
                                                    <div>
                                                        <table class="widget_header_table" cellpadding="0" cellspacing="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="widget_title" colspan="4">
                                                                        舆情分类-贪污受贿
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="widget_resize_frame" style="display: block;">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="kong-1">
                        </div>
                    </div>
                </div>
            </div>
        </div>
          </div>       
<div class="footer">
  <div class="wraper">
    <div id="footer">
      <div id="footer_wrapper">
        <p class="copyright"> © <a href="#">版权信息</a>. 版权所有。 <br>
        </p>
      </div>
    </div>
  </div>
</div>
    </form>
</body>
</html>
