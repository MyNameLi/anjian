<%@ WebHandler Language="C#" Class="GetSiteList" %>

using System;
using System.Web;
using Dropthings.Business.Facade;
using Dropthings.Data;
using Dropthings.Util;
using System.Collections.Generic;
using System.Text;

public class GetSiteList : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.Write(getSiteJson());
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }


    private string getSiteJson() {
        IList<TASKEntity> tasklist = CrawlTaskFacade.GetTaskList("");
        Dictionary<string, string> alldict = new Dictionary<string,string>(); 
        Dictionary<string, string> Dbtaskdict = new Dictionary<string, string>();
        if (tasklist != null && tasklist.Count > 0) {
            foreach (TASKEntity entity in tasklist) {
                string taskid = entity.TASKID.ToString();
                string taskname = entity.TASKNAME;
                if (!Dbtaskdict.ContainsKey(taskid))
                {
                    Dbtaskdict.Add(taskid, taskname);
                }
            }
        }
        Dictionary<string, string> taskDict = IdolStaticFacade.GetSiteStaticByTop(0, "TASKID", null, null, false);

        foreach (string key in taskDict.Keys) {
            if (Dbtaskdict.ContainsKey(key)) {
                alldict.Add(key, Dbtaskdict[key]);
            }
        }

        StringBuilder jsonstr = new StringBuilder();
        jsonstr.Append("{");
        foreach (string key in alldict.Keys) {
            jsonstr.AppendFormat("\"{0}\":\"{1}\",",key,EncodeByEscape.GetEscapeStr(alldict[key]));
        }
        jsonstr.Append("\"Success\":1}");
        return jsonstr.ToString();
    }
}