<%@ control language="C#" autoeventwireup="true" inherits="Widgets_Search_AdvancedSearchResult, Dropthings_deploy1" %>
<!--//right_column_begin-->
<div class="rightcolumn">
    <div id="advanceSearch_tab" class="news_tab">
        <ul>
            <li class="tab_on" pid="all"><a href="javascript:void(null);">全部</a></li>
            <li class="tab_off" pid="news"><a href="javascript:void(null);">新闻</a></li>
            <li class="tab_off" pid="blog"><a href="javascript:void(null);">博客</a></li>
            <li class="tab_off" pid="bbs"><a href="javascript:void(null);">论坛</a></li>
        </ul>
    </div>
    <div id="tab_advanceSearch">
        <span id="SearchInResult" style="display: none;">
            <input type="checkbox" id="BtnSearchInResult" style="padding-left: 5%;" />在结果中检索</span>
        <div class="subsearch" style="display: none;">
            <div class="subsearch_L">
                按关键词：
            </div>
            <div class="subsearch_R">
                <input type="text" id="store_keyword" style="width: 500px;" />
                <input type="button" id="store_search" value="搜索" />
            </div>
            <div class="clear">
            </div>
        </div>
        <div class="subsearch" style="display: none;">
            <div class="subsearch_L">
                按类别：
            </div>
            <div class="subsearch_R">
                <ul class="form_list" id="category_list">
                </ul>
            </div>
            <div class="clear">
            </div>
        </div>
        <div class="gw_info">
            约有<b class="color_2" id="total_count">0</b>项符合<b class="color_2" id="search_keyword"></b>的查询结果，以下是第<b
                class="color_2" id="dis_num">0</b>项。（搜索用时<b class="color_2" id="search_second">0</b>秒）</div>
        <%--<div class="hot_line">
            </div>--%>
        <div class="gw_news" id="result_list">
        </div>
        <div class="viciao" id="pager_list">
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="clear">
    </div>
</div>
<!--//right_column_end-->
