<%@ WebHandler Language="C#" Class="SpecialData" %>
using System;
using System.Web;
using System.Xml;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Dropthings.Data;
using Dropthings.Util;
using IdolACINet;
using Dropthings.Business.Facade;

public class SpecialData : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string categoryId = context.Request["category_id"];
        string fromDate = context.Request["from_date"];
        string toDate = context.Request["to_date"];
        string type = context.Request["type"];
        int Start = Convert.ToInt32(context.Request["Start"].ToString());
        int PageSize = Convert.ToInt32(context.Request["page_size"].ToString());

        IList<CATEGORYEntity> entityList = CategoryFacade.GetCategoryEntityList(" CATEGORYID=" + categoryId);
        StringBuilder jsonStr = new StringBuilder();
        if (entityList.Count > 0)
        {
            if (!string.IsNullOrEmpty(type))
            {
                if (type == "BBS")
                {
                    CATEGORYEntity entity = entityList[0];
                    query BbsQuery = QueryFactory.GetQueryStyle("commonquery");
                    BbsQuery.CategoryID = categoryId;
                    BbsQuery.Text = entity.KEYWORD;
                    BbsQuery.Sentiment = type;
                    BbsQuery.SortType = entity.CATEPATH;
                    BbsQuery.Start = Start;
                    BbsQuery.PageSize = PageSize;
                    //BbsQuery.MinScore = Convert.ToInt32(entity.MinScore);
                    if (!string.IsNullOrEmpty(entity.MINSCORE))
                    {
                        BbsQuery.MinScore = Convert.ToInt32(entity.MINSCORE);
                    }

                    IList<IdolNewsEntity> BbsList = BbsQuery.DoQuery();
                    jsonStr.Append("{\"content_furm\":\"" + JoinHtmlStr(BbsList, type, PageSize, Start) + "\"}");
                }
                else if (type == "news")
                {
                    CATEGORYEntity entity = entityList[0];
                    query query = QueryFactory.GetQueryStyle(entity.QUERYTYPE);
                    query.CategoryID = categoryId;
                    query.Text = entity.KEYWORD;
                    query.SortType = entity.CATEPATH;
                    query.Start = Start;
                    query.PageSize = PageSize;
                    //query.MinScore = Convert.ToInt32(entity.MinScore);
                    if (!string.IsNullOrEmpty(entity.MINSCORE))
                    {
                        query.MinScore = Convert.ToInt32(entity.MINSCORE);
                    }

                    query.Sentiment = type;
                    IList<IdolNewsEntity> NagitiveList = query.DoQuery();
                    jsonStr.Append("{\"content_news\":\"" + JoinHtmlStr(NagitiveList, type, PageSize, Start) + "\"}");
                }
                else if (type == "blog")
                {
                    CATEGORYEntity entity = entityList[0];
                    query BbsQuery = QueryFactory.GetQueryStyle("commonquery");
                    BbsQuery.CategoryID = categoryId;
                    BbsQuery.Text = entity.KEYWORD;
                    BbsQuery.Sentiment = type;
                    BbsQuery.SortType = entity.CATEPATH;
                    BbsQuery.Start = Start;
                    BbsQuery.PageSize = PageSize;
                    //BbsQuery.MinScore = Convert.ToInt32(entity.MinScore);
                    if (!string.IsNullOrEmpty(entity.MINSCORE))
                    {
                        BbsQuery.MinScore = Convert.ToInt32(entity.MINSCORE);
                    }
                    IList<IdolNewsEntity> BbsList = BbsQuery.DoQuery();
                    jsonStr.Append("{\"content_blog\":\"" + JoinHtmlStr(BbsList, type, PageSize, Start) + "\"}");
                }
                else
                {
                    string querytext = string.Empty;
                    CATEGORYEntity entity = entityList[0];
                    switch (type)
                    {
                        case "reson":
                            querytext = entity.EVENTRESON;
                            break;
                        case "measure":
                            querytext = entity.EVENTMEASURE;
                            break;
                        case "about":
                            querytext = entity.EVENTABOUT;
                            break;
                        default:
                            break;
                    }
                    query query = QueryFactory.GetQueryStyle(entity.QUERYTYPE);
                    query.CategoryID = categoryId;
                    query.Text = entity.KEYWORD;
                    query.SortType = entity.CATEPATH;
                    query.Start = Start;
                    query.PageSize = PageSize;
                    //query.MinScore = Convert.ToInt32(entity.EventMinScore);
                    if (!string.IsNullOrEmpty(entity.EVENTMINSCORE))
                    {
                        query.MinScore = Convert.ToInt32(entity.EVENTMINSCORE);
                    }

                    query.Sentiment = null;
                    query.QueryText = querytext;
                    IList<IdolNewsEntity> NagitiveList = query.DoQuery();
                    jsonStr.Append("{\"content_event_" + type + "\":\"" + JoinHtmlStr(NagitiveList, type, PageSize, Start) + "\"}");
                }
            }
            else
            {
                CATEGORYEntity entity = entityList[0];
                query query = QueryFactory.GetQueryStyle(entity.QUERYTYPE);
                query.CategoryID = categoryId;
                query.Text = entity.KEYWORD;
                query.SortType = entity.CATEPATH;
                query.Start = Start;
                query.PageSize = PageSize;
                //query.MinScore = Convert.ToInt32(entity.MinScore);
                if (!string.IsNullOrEmpty(entity.MINSCORE))
                {
                    query.MinScore = Convert.ToInt32(entity.MINSCORE);
                }
                query.Sentiment = "news";
                IList<IdolNewsEntity> NewsList = query.DoQuery();
                query BbsQuery = QueryFactory.GetQueryStyle("commonquery");
                BbsQuery.CategoryID = categoryId;
                BbsQuery.Text = entity.KEYWORD;
                BbsQuery.Sentiment = "BBS";
                BbsQuery.SortType = entity.CATEPATH;
                BbsQuery.Start = Start;
                BbsQuery.PageSize = PageSize;
                //BbsQuery.MinScore = Convert.ToInt32(entity.MinScore);
                if (!string.IsNullOrEmpty(entity.MINSCORE))
                {
                    BbsQuery.MinScore = Convert.ToInt32(entity.MINSCORE);
                }

                IList<IdolNewsEntity> BbsList = BbsQuery.DoQuery();
                BbsQuery.Sentiment = "blog";
                IList<IdolNewsEntity> blogList = BbsQuery.DoQuery();
                if (entity.PARENTCATE.Value != 202 || string.IsNullOrEmpty(entity.EVENTMINSCORE))
                {
                    jsonStr.Append("{\"content_news\":\"" + JoinHtmlStr(NewsList, "news", PageSize, Start)
                        + "\",\"content_furm\":\"" + JoinHtmlStr(BbsList, "BBS", PageSize, Start)
                        + "\",\"content_blog\":\"" + JoinHtmlStr(blogList, "blog", PageSize, Start) + "\"}");
                }
                else
                {
                    query.Sentiment = null;
                    //query.MinScore = Convert.ToInt32(entity.EventMinScore);
                    if (!string.IsNullOrEmpty(entity.EVENTMINSCORE))
                    {
                        query.MinScore = Convert.ToInt32(entity.EVENTMINSCORE);
                    }

                    query.QueryText = entity.EVENTRESON;
                    IList<IdolNewsEntity> ResonList = query.DoQuery();
                    query.QueryText = entity.EVENTMEASURE;
                    IList<IdolNewsEntity> MeasureList = query.DoQuery();
                    query.QueryText = entity.EVENTABOUT;
                    IList<IdolNewsEntity> AboutList = query.DoQuery();
                    jsonStr.Append("{\"content_news\":\"" + JoinHtmlStr(NewsList, "news", PageSize, Start)
                        + "\",\"content_furm\":\"" + JoinHtmlStr(BbsList, "BBS", PageSize, Start)
                        + "\",\"content_blog\":\"" + JoinHtmlStr(blogList, "blog", PageSize, Start)
                        + "\",\"content_event_reson\":\"" + JoinHtmlStr(ResonList, "reson", PageSize, Start)
                        + "\",\"content_event_measure\":\"" + JoinHtmlStr(MeasureList, "measure", PageSize, Start)
                        + "\",\"content_event_about\":\"" + JoinHtmlStr(AboutList, "about", PageSize, Start)
                        + "\"}");
                }

            }
        }
        context.Response.Write(jsonStr.ToString());
    }

    private string JoinHtmlStr(IList<IdolNewsEntity> list, string type, int pagesize, int start)
    {

        StringBuilder htmlstr = new StringBuilder();
        htmlstr.Append("<ul class=\"news_list\">");

        foreach (IdolNewsEntity entity in list)
        {
            htmlstr.AppendFormat("<li><a href=\"{0}\" target=\"_blank\">{1}<b class=\"rss\">【{2}】</b><b>{3}</b></a></li>", entity.Href, entity.Title, entity.SiteName, entity.TimeStr);
        }
        htmlstr.Append("</ul>");

        if (list.Count > 0)
        {
            int totalcount = Convert.ToInt32(list[0].TotalCount);
            if (totalcount > pagesize)
            {
                if (start == 1)
                {
                    htmlstr.Append("<div class=\"right_table_pager\"><span>上一页</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    htmlstr.AppendFormat("<a name=\"content_next\" href=\"javascript:void(null);\" pid=\"{0}_{1}\">下一页</a></div>", type, start + pagesize);
                }
                else if ((start + pagesize) > totalcount)
                {

                    htmlstr.AppendFormat("<div class=\"right_table_pager\"><a name=\"content_prev\" href=\"javascript:void(null);\" pid=\"{0}_{1}\">上一页</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", type, start - pagesize);
                    htmlstr.Append("<span>下一页</span></div>");
                }
                else
                {
                    htmlstr.AppendFormat("<div class=\"right_table_pager\"><a name=\"content_prev\" href=\"javascript:void(null);\" pid=\"{0}_{1}\">上一页</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", type, start - pagesize);
                    htmlstr.AppendFormat("<a name=\"content_next\" href=\"javascript:void(null);\" pid=\"{0}_{1}\">下一页</a></div>", type, start + pagesize);
                }
            }
        }
        return EncodeByEscape.GetEscapeStr(htmlstr.ToString());
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
