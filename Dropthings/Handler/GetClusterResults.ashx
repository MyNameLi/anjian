<%@ WebHandler Language="C#" Class="GetClusterResults" %>

using System;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;
using System.Configuration;
using Dropthings.Util;
using IdolACINet;

public class GetClusterResults : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) 
    {   
        string clusterId = context.Request.QueryString["cluster_id"];
        string endDate = context.Request.QueryString["end_date"];
        string jobName = context.Request.QueryString["job_name"];
        if (string.IsNullOrEmpty(jobName))
        {
            jobName = ConfigurationManager.AppSettings["HotJobName"];
        }
        Command query = Command.ClusterResults;
        query.SetParam("SourceJobName", jobName);
        query.SetParam(QueryParams.Cluster, clusterId);
        query.SetParam("DREOutputEncoding", "utf8");
        query.SetParam("NumResults", 50);
        query.SetParam("MaxTerms", 0);
        query.SetParam("EndDate", endDate);
        Connection cnn = new Connection(ConfigurationManager.AppSettings["IdolHttp"], 9000);
        Response result = cnn.Execute(query);
        XmlDocument contentDoc = result.Data;
      
        //Create an XmlNamespaceManager for resolving namespaces.
        XmlNamespaceManager nsmgr = new XmlNamespaceManager(contentDoc.NameTable);
        nsmgr.AddNamespace("autn", "http://schemas.autonomy.com/aci/");

        //Select the book node with the matching attribute value.
        XmlNodeList clusterDocs = contentDoc.SelectNodes("autnresponse/responsedata/autn:clusters/autn:cluster/autn:docs/autn:doc", nsmgr);

        StringBuilder html = new StringBuilder();
        string title;
        string reference;
        int NodeCount = clusterDocs.Count;
        if (NodeCount > 10) {
            NodeCount = 10;
        }
        //   foreach (XmlNode clusterDoc in clusterDocs)
        for (int index = 0; index < NodeCount; index++)
        {
            XmlNode clusterDoc = clusterDocs[index];
            title = clusterDoc.SelectSingleNode("autn:title", nsmgr).InnerText;
            reference = clusterDoc.SelectSingleNode("autn:ref", nsmgr).InnerText;
            html.Append("<li>");
            html.Append("<h2><a href=\"").Append(reference).Append("\">").Append(title).Append("</a></h2>");	        
	        html.Append("</li>");
        }
        
        string call = context.Request["callback"];

        if (string.IsNullOrEmpty(call))
        {
            context.Response.Write(EncodeByEscape.GetEscapeStr(html.ToString()));
        }
        else
        {
            string jsonpstr = string.Format("{0}({1})", call, "{\"html\":\"" + EncodeByEscape.GetEscapeStr(html.ToString()) + "\"}");
            context.Response.Write(jsonpstr);
        }

    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}