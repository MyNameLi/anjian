<%@ WebHandler Language="C#" Class="GetSGDataResults" %>

using System;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;
using System.Configuration;
using IdolACINet;

public class GetSGDataResults : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        
        string pointId = context.Request.QueryString["point_id"];
        string fromTimeId = context.Request.QueryString["from_time_id"];
        string endTimeId = context.Request.QueryString["end_time_id"];
        if (!string.IsNullOrEmpty(pointId) & !string.IsNullOrEmpty(fromTimeId) & !string.IsNullOrEmpty(endTimeId))
        {
            Command query = new Command("ClusterSGDocsServe");
            query.SetParam("SourceJobname", ConfigurationManager.AppSettings["SGJobName"]);
            query.SetParam(QueryParams.Cluster, pointId);
            query.SetParam("StartDate", fromTimeId);
            query.SetParam("EndDate", endTimeId);
            query.SetParam("NumResults", 10);

            Connection conn = new Connection(ConfigurationManager.AppSettings["IdolHttp"], 9000);
            XmlDocument contentDoc = conn.Execute(query).Data;

            //Create an XmlNamespaceManager for resolving namespaces.
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(contentDoc.NameTable);
            nsmgr.AddNamespace("autn", "http://schemas.autonomy.com/aci/");


            //Select the book node with the matching attribute value.
            XmlNodeList docsList = contentDoc.SelectNodes("autnresponse/responsedata/autn:clusters/autn:cluster/autn:docs/autn:doc", nsmgr);

            StringBuilder html = new StringBuilder();

            foreach (XmlNode doc in docsList)
            {
                html.Append("<li>");
                html.Append("<h2><a href=\"" + doc.ChildNodes[1].InnerText + "\" target=\"_blank\">" + doc.ChildNodes[0].InnerText + "</a></h2>");
                html.Append("</li>");
            }
            
            context.Response.Write(html);  
        }
        else
        {
            context.Response.Write("没有数据！");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}