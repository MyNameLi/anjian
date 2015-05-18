<%@ page language="C#" culture="auto:zh-CN" uiculture="auto:zh-CN" autoeventwireup="true" inherits="_LatestNewsDetail, Dropthings_deploy" validaterequest="false" enableEventValidation="false" theme="GreenBlue" %>

<%@ OutputCache Location="None" NoStore="true" %>
<%@ Register Src="~/Footer.ascx" TagName="Footer" TagPrefix="dropthings" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf8" />
    <title>网络舆情监控系统</title>
    <link href="../styles/common.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.query.js"></script>
    <script type="text/javascript" src="../Scripts/Pager.js"></script>
    <script type="text/javascript" src="Search/AdvancedSearch.js"></script>
    
    <script type="text/javascript">
        $(document).ready(function() {
            advanceSearch.initData["post_url"] = "../Handler/Search.ashx";
            advanceSearch.initData["page_size"] = 7;
            advanceSearch.Load();
        });
    </script>
</head>
<body>
<div class="header">
  <div class="wraper">
	<div id="header"></div>
	<div class="clear"></div>
  </div>
</div>
<div class="main">
  <div class="wraper">
    <div class="widget">
      <div class="widget_border"></div>
      <div class="widget_line"></div>
      <div class="widget_outer">
        <div class="widget_header draggable">
          <table class="widget_header_table" cellpadding="0" cellspacing="0">
            <tbody>
              <tr>
                <td class="widget_title" colspan="4"><a style="display:inline;" class="widget_title_label">最新消息信息列表</a></td>
                <td class="widget_button">&nbsp;</td>
              </tr>
            </tbody>
          </table>
        </div>
        <div>
          <div class="widget_resize_frame" style="display:block;">
            <div class="widget_body">
              <ul class="news_list" id="result_list">
               <table border="0" cellpadding="0" cellspacing="0" height="180" width="100%"><tr><td align="center" class="widget_loading">数据正在加载中...</td></tr></table>
              </ul>
			  <div class="clear"></div>
			  <div class="viciao" id="pager_list">
              </div>			  
            </div>
          </div>
        </div>
      </div>
      <div class="widget_line"></div>
      <div class="widget_border"></div>
    </div>
  </div>
</div>
<div class="footer">
  <div class="wraper">
	<dropthings:Footer ID="Footer1" runat="server" />
  </div>
</div>
</body>
<%--<body>
    <form id="default_form" runat="server">
    <div class="header">
        <div class="wraper">
            <div id="header"></div>
            <div class="clear"></div>
        </div>
    </div>
    <div class="main">
        <div class="wraper">
            <div id="contents">
                <div id="contents_wrapper">
                   <ul id="result_list" class="news_list">
                        <li>数据正在加载中...</li>
                    </ul>
                    <div id="pager_list"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="footer">
        <div class="wraper">
            <dropthings:Footer ID="Footer1" runat="server" />
        </div>
    </div>
    </form>
</body>--%>
</html>
