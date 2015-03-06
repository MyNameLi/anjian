<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdvancedSearchCondition.ascx.cs"
    Inherits="Widgets_Search_AdvancedSearchCondition" %>
<!--//left_column_begin-->
<div class="leftcolumn">
    <div class="leftcolumn_inner">
        <div class="gw_left clearfix">
            <%--<div class="gw_right_supers">
                高级搜索</div>--%>
            <div class="gw_left_super_content">
                <textarea name="textarea" style="overflow-y: hidden;" id="queryword" rows="5" class="gw_left_super_text"></textarea>
                <ul>
                    <li class="gw_left_super_item">数量:
                        <select name="select" id="select_num">
                            <option value="4">4</option>
                            <option value="5">5</option>
                            <option value="6">6</option>
                        </select>
                        相关度：<select name="select" id="quality">
                            <option value="40">40</option>
                            <option value="50">50</option>
                            <option value="60">60</option>
                            <option value="70">70</option>
                            <option value="80">80</option>
                            <option value="90">90</option>
                        </select></li>
                    <li class="gw_left_super_item">开始时间：<input type="text" id="start_time" onfocus="WdatePicker({isShowClear:true,readOnly:true,dateFmt:'dd/MM/yyyy'})" />
                        <%--<select name="select" id="time_min_day">
                            <option>01</option>
                        </select>&nbsp;
                        <select name="select" id="time_min_month">
                            <option>01</option>
                        </select>&nbsp;
                        <select name="select" id="time_min_year">
                            <option>2001</option>
                        </select>--%><br />
                        结束时间：<input type="text" id="end_time" onfocus="WdatePicker({isShowClear:true,readOnly:true,dateFmt:'dd/MM/yyyy'})" />
                        <%--<select name="select" id="time_max_day">
                            <option>01</option>
                        </select>&nbsp;
                        <select name="select" id="time_max_month">
                            <option>01</option>
                        </select>&nbsp;
                        <select name="select" id="time_max_year">
                            <option>2001</option>
                        </select>--%>
                    </li>
                    <li class="gw_left_super_item">新闻类型:
                        <select id="news_type">
                            <option value="all">全部</option>
                            <option value="news">新闻</option>
                            <option value="blog">博客</option>
                            <option value="bbs">论坛</option>
                        </select></li>
                    <li class="gw_left_super_item">站点名称:
                        <input type="text" id="blog_name" size="15" />
                    </li>
                    <li class="gw_left_super_item">查询方式:
                        <select name="select" id="select">
                            <option value="Query">普通查询</option>
                            <option value="SuggestOnText">概念查询</option>
                        </select></li>
                    <li class="gw_left_super_item">排序方式:
                        <select name="select" id="sortrule">
                            <option value="Relevance">相关度</option>
                            <option value="Date">时间</option>
                        </select></li>
                    <li class="gw_left_super_item">
                        <input name="searchButton" type="button" id="BtnSearch" value="" class="super_input_button" />
                    </li>
                </ul>
            </div>
        </div>
        <%--<div class="gw_left clearfix">
            <div class="gw_left_title">
                <a href="#">相关搜索</a></div>
            <div class="gw_left_content">
                <a id="timesel_1" href="javascript:void(null);">过去一天</a><br />
                <a id="timesel_7" href="javascript:void(null);">过去一周</a><br />
                <a id="timesel_30" href="javascript:void(null);">过去一月</a><br />
            </div>
        </div>
        <div class="gw_left">
            <div class="gw_right_blog">
                博客：</div>
            <div class="gw_right_blog_list" id="content_blog">
            </div>
        </div>
        <div class="gw_left">
            <div class="gw_right_bbs">
                论坛：</div>
            <div class="gw_right_bbs_list" id="content_bbs">
            </div>
        </div>
        <div class="gw_left clearfix">
            <div class="gw_left_title_more">
                <a href="#">更多使用工具</a></div>
        </div>--%>
    </div>
</div>
<!--//left_column_end-->
<%--<script type="text/javascript">
    jQuery(function() {
        ensure({ js: ["Scripts/jquery.query.js", "Scripts/jquery.flash.js", "Scripts/Pager.js", "Scripts/inputcue.js", "Widgets/search/AdvancedSearch.js"] }, function() { advanceSearch.Load(); });
    });
</script>--%>
