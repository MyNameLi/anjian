<%@ WebHandler Language="C#" Class="IdolSearch" %>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Threading;
using Dropthings.Data;
using Dropthings.Business.Facade;
using Dropthings.Util;

public class IdolSearch : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {
        string action = context.Request["action"];
        if (!string.IsNullOrEmpty(action)) {
            action = action.ToLower();
            QueryParamEntity queryParamsEntity = new QueryParamEntity();
            switch (action) { 
                case "query":
                    DoSearch(context, queryParamsEntity);
                    break;
                case "categoryquery":
                    DoSearch(context, queryParamsEntity);
                    break;
                case "agentgetresults":
                    DoSearch(context, queryParamsEntity);
                    break;
                case "getclusterinfolist":
                    string jobName = EncodeByEscape.GetUnEscapeStr(context.Request["job_name"]);
                    string jobClusterId = EncodeByEscape.GetUnEscapeStr(context.Request["job_cluster_id"]);
                    int PageSize = Convert.ToInt32(context.Request["page_size"]);
                    int Start = Convert.ToInt32(context.Request["start"]);
                    int pageNumber = Start / PageSize + 1;
                    context.Response.Write(ClusterResultFacade.GetClusterJsonStr(pageNumber, PageSize, jobName, jobClusterId));
                    break;
                default:
                    break;
            }
        }
    }

    private void DoSearch(HttpContext context, QueryParamEntity queryParamsEntity)
    {
        string action = context.Request["action"];
        if (!string.IsNullOrEmpty(action))
        {
            action = action.ToLower();
            queryParamsEntity = QueryParamsDao.GetEntity(context);
            if (action == "categoryquery")
            {
                string categoryid = context.Request["category"];
                CATEGORYEntity entity = CategoryFacade.GetCategoryEntity(categoryid);
                if (entity.QUERYTYPE == "commonquery")
                {
                    action = "query";
                    string text = context.Request["text"];
                    if (!string.IsNullOrEmpty(text))
                    {
                        queryParamsEntity.Text = text;
                    }
                    else
                    {
                        queryParamsEntity.Text = entity.KEYWORD;
                    }
                    string minscore = entity.MINSCORE;
                    if (!string.IsNullOrEmpty(minscore))
                    {
                        queryParamsEntity.MinScore = minscore;
                    }
                    else
                    {
                        queryParamsEntity.MinScore = "10";
                    }
                }
                else
                {
                    string minscore = entity.MINSCORE;
                    if (!string.IsNullOrEmpty(minscore))
                    {
                        queryParamsEntity.MinScore = minscore;
                    }
                    else
                    {
                        queryParamsEntity.MinScore = "10";
                    }
                }
            }
            queryParamsEntity.action = action;
            IdolQuery idolquery = IdolQueryFactory.GetDisStyle(action);
            idolquery.queryParamsEntity = queryParamsEntity;
            context.Response.Write(idolquery.GetHtmlStr());
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}
