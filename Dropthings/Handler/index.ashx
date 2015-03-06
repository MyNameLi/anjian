<%@ WebHandler Language="C#" Class="index" %>
using System;
using System.Web;
using System.Xml;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Dropthings.Business.Facade;
using Dropthings.Util;
using Dropthings.Data;
using IdolACINet;

public class index : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        string TimeStr = context.Request["date_time"];
        string[] keywords = new string[] { "群众举报","中纪委","信访监督","群众上访","纪检监察" };
        IdolQueryEntity.IdolQueryEntityDao dao = new IdolQueryEntity.IdolQueryEntityDao();
        
        IdolClusterEntity.IdolClusterEntityDao clusterDao = new IdolClusterEntity.IdolClusterEntityDao();
        
        IdolNewsEntity.IdolNewsDao newsDao = new IdolNewsEntity.IdolNewsDao();
        
        Connection conn = new Connection(ConfigurationManager.AppSettings["IdolHttp"], 9000);
        Dictionary<string, IList<IdolQueryEntity>> dict = new Dictionary<string, IList<IdolQueryEntity>>();        
        Queue<IdolQueryEntity> entityQueue = new Queue<IdolQueryEntity>();
        Dictionary<string, string> Tag = new Dictionary<string, string>();
        DateTime time = Convert.ToDateTime(TimeStr);
        foreach (string key in keywords)
        {
            try
            {
                QueryCommand query = new QueryCommand();
                query.Text = key;                               
                query.SetParam(QueryParams.DatabaseMatch, ConfigurationManager.AppSettings["DATABASE"]);
                query.SetParam(QueryParams.Sort, "Date");
                query.SetParam(QueryParams.NumResults, 6);
                query.SetParam(QueryParams.Print,"none");               
                query.SetParam(QueryParams.TotalResults, QueryParamValue.True);
                query.SetParam("predict", QueryParamValue.False);
                XmlDocument xmldoc = conn.Execute(query).Data;
                dict.Add(key, dao.GetNewsList(xmldoc));               
            }
            catch (Exception ex)
            {
                //LogWriter.WriteErrLog("首页最新新闻请求数据失败：" + ex.ToString());
                continue;
            }
        }
        
        while(entityQueue.Count < 6)
        {
            foreach (string key in dict.Keys)
            {                
                if (dict[key].Count > 0)
                {
                    if (Tag.ContainsKey(dict[key][0].DocId))
                    {
                        dict[key].RemoveAt(0);
                    }
                    else
                    {
                        entityQueue.Enqueue(dict[key][0]);
                        Tag.Add(dict[key][0].DocId, "1");
                        dict[key].RemoveAt(0);
                        
                    }
                }
                if (entityQueue.Count == 6)
                {
                    break;
                }
            }
        }
        IList<IdolClusterEntity> hotNewsList = clusterDao.GetClusterNews(2, 6, 4);
        
        context.Response.Write("{\"lastNews\":\"" + EncodeByEscape.GetEscapeStr(GetHtmlStr(entityQueue, conn, time))
            + "\",\"hotNews\":\"" + EncodeByEscape.GetEscapeStr(GetHtmlStr(hotNewsList)) + "\"}");
    }

    private string GetHtmlStr(Queue<IdolQueryEntity> entityQueue, Connection conn,DateTime time)
    {       
        StringBuilder HtmlStr = new StringBuilder();
        int count = 1;     
        while (entityQueue.Count > 0)
        {
            IdolQueryEntity entity = entityQueue.Dequeue();
            if (count == 1)
            {
                HtmlStr.Append("<dl class=\"main-left-conter_visit\"><dt>0" + count + "</dt>");
            }
            else
            {
                HtmlStr.Append("<dl class=\"main-left-conter_novisit\"><dt>0" + count + "</dt>");
            }
            HtmlStr.AppendFormat("<dd class=\"url_click\">{0}</dd>", entity.Title);
            HtmlStr.AppendFormat("<dd class=\"play\"><a href=\"{0}\" target=\"_blank\">{1}</a></dd>", entity.Href, entity.Title);
            Command query = new Command("Suggest");
            query.SetParam("ID", entity.DocId);
            query.SetParam("print", "fileds");
            query.SetParam("printfields", "none");
            query.SetParam(QueryParams.DatabaseMatch, ConfigurationManager.AppSettings["DATABASE"]);
            query.SetParam(QueryParams.MaxResults, 3);
            query.SetParam(QueryParams.Sort, "Relevance");
            XmlDocument xmldoc = conn.Execute(query).Data;            
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
            nsmgr.AddNamespace("autn", "http://schemas.autonomy.com/aci/");
            XmlNodeList nodelist = xmldoc.SelectNodes("autnresponse/responsedata/autn:hit", nsmgr); 
            foreach (XmlNode node in nodelist)
            {
                HtmlStr.AppendFormat("<dd class=\"play\"><a href=\"{0}\" target=\"_blank\">{1}</a></dd>", node.SelectSingleNode("autn:reference", nsmgr).InnerText, node.SelectSingleNode("autn:title", nsmgr).InnerText);
                
            }
            HtmlStr.Append("</dl>");
            count++;
        }
        return HtmlStr.ToString();
    }

    private string GetHtmlStr(IList<IdolClusterEntity> list)
    {
        StringBuilder HtmlStr = new StringBuilder();        
        int count = 1;
        foreach (IdolClusterEntity entity in list)
        {
            if (count == 1)
            {
                HtmlStr.Append("<dl class=\"main-left-conter_visit\"><dt>0" + count + "</dt>");
            }
            else
            {
                HtmlStr.Append("<dl class=\"main-left-conter_novisit\"><dt>0" + count + "</dt>");
            }
            int entitycount = 1;     
            foreach (ClusterDocEntity docEntity in entity.DocList)
            {
                if (entitycount == 1)
                {
                    HtmlStr.AppendFormat("<dd class=\"url_click\">{0}</dd>", docEntity.Title);                    
                }
                HtmlStr.AppendFormat("<dd class=\"play\"><a href=\"{0}\" target=\"_blank\">{1}</a></dd>", docEntity.Href, docEntity.Title);
                entitycount++;
            }
            HtmlStr.Append("</dl>");          
            count++;
        }
        return HtmlStr.ToString();
    }
    private string GetTimeStr(DateTime time)
    {
        try
        {
            return time.Day.ToString() + "/" + time.Month.ToString() + "/" + time.Year.ToString();
        }
        catch
        {
            return "";
        }
    }    

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}