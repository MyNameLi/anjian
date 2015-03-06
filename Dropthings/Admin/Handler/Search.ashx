<%@ WebHandler Language="C#" Class="Search" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using System.Data;
using System.Text;
using Dropthings.Data;
using Dropthings.Util;
using Dropthings.Business.Facade;

public class Search : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string siteid = context.Session["{#siteid}"].ToString();
        string strwhere = " ARTICLESTATUS = 0 AND ARTICLEAUDIT = 1 AND SITEID=" + siteid;
        int pageSize = Convert.ToInt32(context.Request["page_size"]);
        int start = Convert.ToInt32(context.Request["start"]);
        int count = ArticleFacade.GetPagerCount(strwhere);
        DataTable dt = ArticleFacade.GetPagerDataTable(strwhere, pageSize, start);
        string backStr = GetBackStr(count, dt);
        context.Response.Write(backStr);
    }

    private string GetBackStr(int count, DataTable dt) {
        StringBuilder jsonStr = new StringBuilder();
        jsonStr.Append("{");
        jsonStr.AppendFormat("\"totalcount\":\"{0}\",", count);
        int num = 1;
        foreach (DataRow row in dt.Rows) {
            jsonStr.AppendFormat("\"newsentity_{0}\":", num);
            jsonStr.Append("{");
            jsonStr.AppendFormat("\"id\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["ID"].ToString()));
            jsonStr.AppendFormat("\"title\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["ARTICLETITLE"].ToString()));
            jsonStr.AppendFormat("\"date\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["ArticleBaseDate"].ToString()));
            jsonStr.AppendFormat("\"url\":\"{0}\"", EncodeByEscape.GetEscapeStr(row["PUBLISHPATH"].ToString()));
            jsonStr.Append("},");
            num++;
        }
        jsonStr.Append("\"Success\":1}");
        return jsonStr.ToString();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
