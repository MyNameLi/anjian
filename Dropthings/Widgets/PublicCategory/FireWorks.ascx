﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FireWorks.ascx.cs" Inherits="Widgets_PublicCategory_FireWorks" %>
<div class="news_tab">
  <ul class="form_list">
    <li> <span class="name">显示条数：</span> <span class="input">
      <select id="DisplayNumber">
        <option value="5">5</option>
        <option value="6" selected="selected">6</option>
        <option value="8">8</option>
        <option value="9">9</option>
        <option value="10">10</option>
        <option value="15">15</option>
        <option value="20">20</option>
        <option value="30">30</option>
      </select>
      </span> <span class="name">排序方式：</span> <span class="input">
      <input type="radio" name="sort_style" value="Date">
      </span> <span class="name">时间</span> <span class="input">
      <input type="radio" name="sort_style" checked="checked" value="Relevance">
      </span> <span class="name">相关度</span> </li>
  </ul>
</div>
<div class="content_news">
    <ul id="SearchResult_FireWorks" class="news_list">
    </ul>
    <div id="PagerList_FireWorks" class="publicFeelSearchPage">
    </div>
</div>