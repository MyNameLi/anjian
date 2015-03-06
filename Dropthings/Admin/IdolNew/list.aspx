<%@ Page Language="C#" AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="Admin_IdolNew_list" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>信息管理</title>
    <link rel="Stylesheet" href="../css/tab.css" type="text/css" />
    <link rel="Stylesheet" href="../css/frame.css" type="text/css" />
    <script src="../js/Config.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/jquery-1.4.2.js"></script>
    <script type="text/javascript" src="../js/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../js/divMove.js"></script>
    <script type="text/javascript" src="../js/listTable.js"></script>
    <script type="text/javascript" src="../js/IdolPager.js"></script>
    <script type="text/javascript" src="../js/IdolNew.js"></script>
</head>
<body style="background: #ffffff;">
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td height="30">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="15" height="30">
                                <img src="../images/tab_03.gif" width="15" height="30" />
                            </td>
                            <td background="../images/tab_05.gif" align="left">
                                <img src="../images/311.gif" width="16" height="16" /><span class="STYLE4">信息列表</span>
                            </td>
                            <td width="14">
                                <img src="../images/tab_07.gif" width="14" height="30" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="9" background="../images/tab_12.gif">
                                &nbsp;
                            </td>
                            <td bgcolor="#d3e7fc">
                                <table align="center" bgcolor="#CECECE" border="0" cellpadding="0" cellspacing="1"
                                    width="99%">
                                    <tbody>
                                        <tr>
                                            <td bgcolor="#ffffff">
                                                <ul class="form_list">
                                                    <li><span class="name"></span><span class="input">
                                                        <input type="radio" name="search_codition" checked="checked" value="query" /></span>
                                                        <span class="name"><b>高级搜索</b></span> <span class="name">&nbsp;</span> <span class="input">
                                                            <input type="radio" name="search_codition" value="categoryquery" /></span> <span
                                                                class="name"><b>分类查询</b></span> </li>
                                                    <li><span id="query_search_frame"><span class="name">关键字 ：</span> <span class="input">
                                                        <input id="keyword" style="width: 350px;" type="text" value="*"></span> </span><span
                                                            style="display: none;" id="categoryquery_search_frame"><span class="name">分 类：</span>
                                                            <span class="input">
                                                                <select id="category_id">
                                                                </select>
                                                                <select id="category_child_id">
                                                                </select>
                                                            </span></span><span class="name">最低相关度：</span> <span class="input">
                                                                <input id="min_score" style="width: 48px;" type="text"></span> </li>
                                                    <li><span class="name">时间选择：</span> <span class="input">
                                                        <input id="start_time" onfocus="WdatePicker({isShowClear:true,readOnly:true,dateFmt:'dd/MM/yyyy'})"
                                                            style="width: 100px;" type="text"></span> <span class="name">—</span> <span class="input">
                                                                <input id="end_time" onfocus="WdatePicker({isShowClear:true,readOnly:true,dateFmt:'dd/MM/yyyy'})"
                                                                    style="width: 100px;" type="text"></span> <span class="name">排序方式：</span>
                                                        <span class="input">
                                                            <select id="sort_style" style="width: 60px;">
                                                                <option selected="selected" value="Date">时间</option>
                                                                <option value="Relevance">相关度</option>
                                                            </select></span> <span class="name">信息类型：</span> <span class="input">
                                                                <select id="info_type" style="width: 60px;">
                                                                    >
                                                                    <option selected="selected" value="all">全部</option>
                                                                    <option value="news">新闻</option>
                                                                    <option value="blog">博客</option>
                                                                    <option value="bbs">论坛</option>
                                                                </select></span> <span class="name">站点选择：</span> <span class="input">
                                                                    <select id="info_site">
                                                                        <option selected="selected" value="all">全部</option>
                                                                    </select></span> </li>
                                                    <li style="display: none;"><span>
                                                        <%-- 数据源：
                           <input type="checkbox" name="databse_list"  value="Safety" />安全生产库&nbsp;&nbsp;&nbsp;
                           <input type="checkbox" name="databse_list" checked="checked"  value="portalsafety" />发布库 --%>
                                                    </span></li>
                                                    <li><span class="name"><a id="btn_look_info" style="font-size: 18px;" href="javascript:void(null);">
                                                        <img src="../images/btn_search.gif" border="0" />
                                                    </a></span></li>
                                                </ul>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CECECE">
                                    <tr>
                                        <td width="6%" height="26" background="../images/tab_14.gif" align="center">
                                            选择
                                        </td>
                                        <td width="25%" background="../images/tab_14.gif" align="center">
                                            标题
                                        </td>
                                        <td width="14%" background="../images/tab_14.gif" align="center">
                                            时间
                                        </td>
                                        <td width="14%" background="../images/tab_14.gif" align="center">
                                            所属站点
                                        </td>
                                        <td width="7%" background="../images/tab_14.gif" align="center">
                                            栏目定制
                                        </td>
                                        <td width="7%" background="../images/tab_14.gif" align="center">
                                            信息过滤
                                        </td>
                                        <td width="7%" background="../images/tab_14.gif" align="center">
                                            相关度
                                        </td>
                                        <td width="7%" background="../images/tab_14.gif" align="center">
                                            查看快照
                                        </td>
                                        <td width="10%" id="look_same_news" style="display: none;" background="../images/tab_14.gif"
                                            align="center">
                                            查看相同新闻
                                        </td>
                                        <td width="15%" id="dis_article_column" style="display: none;" background="../images/tab_14.gif"
                                            align="center">
                                            所属栏目
                                        </td>
                                    </tr>
                                    <tbody id="all_data_list">
                                    </tbody>
                                </table>
                            </td>
                            <td width="9" background="../images/tab_16.gif">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" height="29">
                        <tr>
                            <td width="15" height="29">
                                <img src="../images/tab_20.gif" width="15" height="29" />
                            </td>
                            <td background="../images/tab_21.gif">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="21%" align="left">
                                            <a href="javascript:void(null);" id="choose_all" name="0" onclick="javascript:listTable.chooseAll('choose_all','all_data_list');">
                                                <img alt="全选" src="../images/q.gif" width="40" height="19" />
                                            </a><a href="javascript:void(null);" id="all_news_store">
                                                <img alt="归档" src="../images/gd.gif" width="64" height="19" />
                                            </a><a href="javascript:void(null);" id="btn_delete_all">
                                                <img alt="删除" src="../images/s.gif" width="64" height="19" />
                                            </a>
                                        </td>
                                        <td width="79%" class="STYLE1">
                                            <div id="pager_list">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="14">
                                <img src="../images/tab_22.gif" width="14" height="29" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="column_edit_frame" class="layerdiv">
        <%--<span class="layer_line"></span>--%>
        <div class="layer_outer">
            <%--<span class="layer_top_L"></span>
      <span class="layer_top_R"></span>--%>
            <div class="layer_inner">
                <div class="layer_move" id="move_column" title="点击此处可自由拖动">
                    &nbsp;</div>
                <a class="btn_close" href="javascript:void(null);" id="close_edit_frame"></a>
                <div class="clear">
                </div>
                <div class="layer_T">
                    <h1>
                        <b>站点选择</b></h1>
                    <span class="btn"></span>
                    <div class="clear">
                    </div>
                </div>
                <div class="layer_C">
                    <ul id="web_site_list">
                    </ul>
                </div>
                <div class="layer_T">
                    <h1>
                        <b>栏目选择</b></h1>
                    <span class="btn"></span>
                    <div class="clear">
                    </div>
                </div>
                <div class="layer_C">
                    <ul>
                        <li>
                            <select id="site_column_list">
                            </select>
                        </li>
                    </ul>
                </div>
                <div class="layer_B">
                    <a href="javascript:void(null);">
                        <img src="../images/btn_send.gif" alt="" id="btn_store" />
                    </a>
                </div>
                <div>
                    <label>
                        归档进度</label>
                    <label id="gdjd_lb">
                        0/0
                    </label>
                </div>
            </div>
            <%--<span class="layer_bottom_L">&nbsp;</span>
      <span class="layer_bottom_R">&nbsp;</span>--%>
        </div>
        <%--<span class="layer_line"></span>--%>
    </div>
    <div id="same_news_frame" class="layerdiv">
        <%--<span class="layer_line"></span>--%>
        <div class="layer_outer">
            <%--<span class="layer_top_L"></span>
      <span class="layer_top_R"></span>--%>
            <div class="layer_inner">
                <div class="layer_move" id="move_same_news" title="点击此处可自由拖动">
                    &nbsp;</div>
                <a class="btn_close" href="javascript:void(null);" id="close_same_news_frame"></a>
                <div class="clear">
                </div>
                <div class="layer_T">
                    <h1>
                        <b>相同新闻列表</b></h1>
                    <span class="btn"></span>
                    <div class="clear">
                    </div>
                </div>
                <div class="layer_C">
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CECECE">
                        <tr>
                            <td width="50%" height="26" background="../images/tab_14.gif" align="center">
                                标题
                            </td>
                            <td width="25%" background="../images/tab_14.gif" align="center">
                                时间
                            </td>
                            <td width="25%" background="../images/tab_14.gif" align="center">
                                来源
                            </td>
                        </tr>
                        <tbody id="same_news_all_list">
                        </tbody>
                    </table>
                </div>
                <div class="layer_B">
                    <div id="same_news_pager">
                    </div>
                </div>
            </div>
            <%--<span class="layer_bottom_L">&nbsp;</span>
      <span class="layer_bottom_R">&nbsp;</span>--%>
        </div>
        <%--<span class="layer_line"></span>--%>
    </div>
    </form>
</body>
</html>
