<%@ WebHandler Language="C#" Class="SuggestResult" %>

using System;
using System.Web;
using System.Xml;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Dropthings.Business.Facade;
using Dropthings.Util;
using IdolACINet;

public class SuggestResult : IHttpHandler
{    
    public void ProcessRequest(HttpContext context)
    {
        string type = context.Request["type"];
        string DocIdList = context.Request["doc_id_list"];
        string Keyword = context.Request["keyword"];
        Connection conn = new Connection(ConfigurationManager.AppSettings["IdolHttp"], 9000);
        IdolNewsEntity.IdolNewsDao Dao = new IdolNewsEntity.IdolNewsDao();
       
        switch (type)
        {
            case "suggest":
                StringBuilder htmlstr = new StringBuilder();
                string[] docId = DocIdList.Split(',');
                htmlstr.Append("{");
                foreach (string id in docId)
                {
                    try
                    {
                        Command query = new Command("Suggest");
                        query.SetParam("ID", id);
                        query.SetParam("MaxResults", 3);
                        query.SetParam(QueryParams.Print, QueryParamValue.Fields);
                        query.SetParam(QueryParams.PrintFields, "DREDATE,MYSITENAME,DOMAINSITENAME");
                        query.SetParam(QueryParams.DatabaseMatch, ConfigurationManager.AppSettings["DATABASE"]);
                        query.SetParam(QueryParams.Combine, "DRETITLE");
                        IList<IdolNewsEntity> list = Dao.GetNewsList(conn.Execute(query).Data);
                        htmlstr.AppendFormat("\"{0}\":\"", id);
                        StringBuilder jsonstr = new StringBuilder();
                        jsonstr.Append("<div class=\"open\"><p>");                       
                        foreach (IdolNewsEntity entity in list)
                        {                            
                            jsonstr.AppendFormat("<a href=\"{0}\" target=\"_blank\">{1}</a><span>{2}</span><span>{3}</span><br>", entity.Href, entity.Title, entity.TimeStr, entity.SiteName);
                            
                        }
                        jsonstr.Append("</p></div><br style=\"clear: both; height: 0pt; font-size: 1px; line-height: 0px;\">");
                        htmlstr.Append(EncodeByEscape.GetEscapeStr(jsonstr.ToString())).Append("\",");

                    }
                    catch (Exception ex)
                    {
                        //LogWriter.WriteErrLog(ex.ToString());
                        continue;
                    }
                }
                context.Response.Write(htmlstr.ToString().Substring(0, htmlstr.Length - 1) + "}");
                break;
            case "BbsOrBlog":
                context.Response.Write("{\"bbs\":\"" + GetHtmlStr(conn, Dao, Keyword, "bbs") + "\",\"blog\":\"" + GetHtmlStr(conn, Dao, Keyword, "blog") + "\"}");
                break;
            default:
                break;
        }
    }
    private string GetHtmlStr(Connection conn, IdolNewsEntity.IdolNewsDao dao, string keyword, string type)
    {
        StringBuilder htmlstr = new StringBuilder();
        try
        {
            QueryCommand query = new QueryCommand();
            query.Text = keyword;
            query.SetParam(QueryParams.FieldText, "MATCH{" + type + "}:MYSRCTYPE");
            query.MaxResults = 4;           
            query.SetParam(QueryParams.Print, "none");
            query.SetParam(QueryParams.DatabaseMatch, ConfigurationManager.AppSettings["DATABASE"]);
            query.SetParam(QueryParams.Combine, "DRETITLE");
            query.MinScore = 50;
            XmlDocument xmldoc = conn.Execute(query).Data;
            IList<IdolNewsEntity> list = dao.GetNewsList(xmldoc);
            htmlstr.Append("<ul class=\"news_list\">");
            foreach (IdolNewsEntity entity in list)
            {
                htmlstr.AppendFormat("<li><a href=\"{0}\" title=\"{1}\" target=\"_blank\">{2}</a></li>", entity.Href, entity.Title, entity.Title.Length > 11 ? entity.Title.Substring(0, 11) + "..." : entity.Title);
            }
            htmlstr.Append("</ul>");
        }
        catch (Exception ex)
        {
            //LogWriter.WriteErrLog("加载博客论坛数据出错：" + ex.ToString());
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
