<%@ WebHandler Language="C#" Class="Info" %>

using System;
using System.Web;
using System.Xml;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dropthings.Data;
using Dropthings.Util;
using Dropthings.Business.Facade;
using IdolACINet;

/// <summary>
/// 张涛2010-12-31修改
/// </summary>
public class Info : IHttpHandler
{
    private static IdolQuery idolQuery = IdolQueryFactory.GetDisStyle("query");

    public void ProcessRequest(HttpContext context)
    {
        string dataType = context.Request["data_type"] ?? "";
        string newsType = context.Request["news_type"];
        Connection conn = new Connection(ConfigurationManager.AppSettings["IdolHttp"], 9000);
        string ResponseStr = string.Empty;
        if (!string.IsNullOrEmpty(dataType)) {
            switch (dataType) { 
                case "keyword_info":
                    ResponseStr = GetHotKeyWordStr();
                    break;
                case "statistics":
                    ResponseStr = GetSiteInfo();
                    break;
                case "national-map":
                    ResponseStr = "{\"AreaData\":" + GetAreaData() + "}"; 
                    break;
                case "news_list":
                    ResponseStr = GetNewsList(newsType);
                    break;
                case "special_event":
                    ResponseStr = GetSpecialEventStr(context);
                    break;
                default:
                    break;
            }
        }
        context.Response.Write(ResponseStr);
    }

    private string GetHotKeyWordStr() {
        QueryParamEntity paramsEntity = new QueryParamEntity();
        paramsEntity.action = "query";
        paramsEntity.MinScore = "40";
        Dictionary<string, string> articleDict = new Dictionary<string, string>();
        string[] keywords = ConfigurationManager.AppSettings["HOTKEYWORD"].Split(';');        
        foreach (string keyword in keywords)
        {
            try
            {               
                paramsEntity.Text = "\"" + keyword + "\"";
                idolQuery.queryParamsEntity = paramsEntity;              
                articleDict.Add(keyword, idolQuery.GetTotalCount());
            }
            catch (Exception ex)
            {
                
            }
        }
        string backstr = "{" + string.Format("\"keyword_info\":{0}", GetArticleJson(articleDict)) + "}";
        return backstr;
    }

    private string GetSiteInfo() {       
        Dictionary<string, string> MySiteDict = IdolStaticFacade.GetSiteStaticByTop(4, "MYSITENAME", null, null, null, true);
        string backstr = "{\"site_info\":" + GetArticleJson(MySiteDict) + "}";
        return backstr;
    }

    private string GetNewsList(string newsType)
    {
        StringBuilder jsonstr = new StringBuilder();
        jsonstr.Append("{");
        try
        { 
            QueryParamEntity paramsEntity = new QueryParamEntity();
            paramsEntity.Text = "*";
            paramsEntity.PageSize = 20;
            paramsEntity.Sort = "Date";
            
            if (!string.IsNullOrEmpty(newsType))
            {
                if (newsType != "weibo")                
                {
                    paramsEntity.Combine = "DRETITLE";
                    paramsEntity.FieldText = "MATCH{" + newsType + "}:C1";
                }
            }
            idolQuery.queryParamsEntity = paramsEntity;
            IList<IdolNewsEntity> list = idolQuery.GetResultList();
            int count = 1;
            int totalcount = list.Count;
            foreach (IdolNewsEntity entity in list)
            {
                string title = entity.Title;
                if (string.IsNullOrEmpty(title)) {
                    title = entity.Content.Substring(0, 25);
                }
                if (count == totalcount)
                {
                    jsonstr.AppendFormat("\"{0}\":\"{1}_{2}_{3}\"", entity.Href, EncodeByEscape.GetEscapeStr(title).Replace("_", ""), EncodeByEscape.GetEscapeStr(entity.SiteName), EncodeByEscape.GetEscapeStr(entity.TimeStr));
                }
                else
                {
                    jsonstr.AppendFormat("\"{0}\":\"{1}_{2}_{3}\",", entity.Href, EncodeByEscape.GetEscapeStr(title).Replace("_", ""), EncodeByEscape.GetEscapeStr(entity.SiteName), EncodeByEscape.GetEscapeStr(entity.TimeStr));
                }
                count++;
            }
        }
        catch (Exception ex)
        {
            //LogWriter.WriteErrLog(ex.ToString());
        }
        jsonstr.Append("}");
        string backstr = "{\"newsList\":" + jsonstr.ToString() + "}";
        return backstr;
    }

    private string GetSpecialEventStr(HttpContext context)
    {
        IdolQuery categoryquery = IdolQueryFactory.GetDisStyle("categoryquery");
        Dictionary<string, string> categoryDict = new Dictionary<string, string>();
        if (context.Application["CateDict"] == null)
        {
            IList<CATEGORYEntity> entitylist = CategoryFacade.Find(9, " PARENTCATE =263");
            foreach (CATEGORYEntity entity in entitylist)
            {
                try
                {                    

                    QueryParamEntity paramsEntity = new QueryParamEntity();
                    paramsEntity.Category = entity.CATEGORYID.ToString();
                    paramsEntity.MinScore = entity.MINSCORE;
                    categoryquery.queryParamsEntity = paramsEntity;
                    categoryDict.Add(EncodeByEscape.GetEscapeStr(entity.CATEGORYNAME + "_" + entity.CATEGORYID.ToString() + "_" + entity.PARENTCATE.ToString() + "_"), categoryquery.GetTotalCount());
                }
                catch (Exception ex)
                {
                    categoryDict.Add(EncodeByEscape.GetEscapeStr(entity.CATEGORYNAME), "0");
                    //LogWriter.WriteErrLog(ex.ToString());
                    continue;
                }
                finally
                {
                    context.Application["CateDict"] = categoryDict;
                }
            }
        }
        else
        {
            categoryDict = context.Application["CateDict"] as Dictionary<string, string>;
        }
        string backstr = "{\"categoryData\":" + GetArticleJson(categoryDict) + "}";
        return backstr;
    }    
    private string GetArticleJson(Dictionary<string, string> dict)
    {
        int count = 1;
        int totalcount = dict.Keys.Count;
        StringBuilder jsonStr = new StringBuilder();
        jsonStr.Append("{");
        foreach (string key in dict.Keys)
        {
            if (count == totalcount)
            {
                jsonStr.AppendFormat("\"{0}\":\"{1}\"", key, dict[key]);
            }
            else
            {
                jsonStr.AppendFormat("\"{0}\":\"{1}\",", key, dict[key]);
            }
            count++;
        }
        jsonStr.Append("}");

        return jsonStr.ToString();
    }

    private string GetAreaData()
    {
        StringBuilder jsonStr = new StringBuilder();

        string strSql = "SELECT SUM(TOTALHITS) AS HITS,TAG FROM CITYHITS GROUP BY TAG ORDER BY HITS DESC";
        OracleHelper Helper = new OracleHelper("SentimentConnStr");
        DataSet ds = Helper.ExecuteDateSet(strSql);
        jsonStr.Append("{");
        if (ds != null)
        {
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                jsonStr.AppendFormat("\"{0}\":\"{1}\",", row["tag"].ToString().Trim(), row["hits"]);
            }
        }
        return jsonStr.ToString().Substring(0, jsonStr.Length - 1) + "}";
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}

