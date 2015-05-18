<%@ WebHandler Language="C#" Class="TrainTag" %>

using System;
using System.Web;
using System.Configuration;
using IdolACINet;
using System.Xml;
using System.Threading;

public class TrainTag : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string docIdList = context.Request.QueryString["docid_list"];
        string fieldName = context.Request.QueryString["field_name"];
        string fieldValue = context.Request.QueryString["field_value"];
        Connection cnn = new Connection(ConfigurationManager.AppSettings["IdolHttp"], 9001);
        Drereplace drereplace = new Drereplace();
        drereplace.ReplaceAllRefs = true;
        //drereplace.SetParam("ReplaceAllRefs",true);
        //drereplace.SetParam("InsertValue", true);
        //drereplace.SetParam("DatabaseMatch", "News");
        drereplace.PostData = "#DREDOCID " + docIdList + "\n#DREFIELDNAME " + fieldName + "\n#DREFIELDVALUE " + fieldValue + "\n#DREENDDATANOOP\n\n";
        
        try
        {
            cnn.Execute(drereplace);
            Thread.Sleep(1000);
            context.Response.Write("success");
        }
        catch(Exception e)
        {
            context.Response.Write("lost");
        }                
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}