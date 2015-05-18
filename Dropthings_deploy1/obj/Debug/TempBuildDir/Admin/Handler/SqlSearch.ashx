<%@ WebHandler Language="C#" Class="SqlSearch" %>

using System;
using System.Web;
using System.Xml;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Dropthings.Util;
using Dropthings.Data.SqlServerEntity;
using System.Data;
public class SqlSearch : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    private static string SessionKey = ConfigurationManager.AppSettings["SessionKey"].ToString();
    private newsURLEntity.newsURLDAO lexiconWord = new newsURLEntity.newsURLDAO();
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            string action = context.Request["action"];
            string strwhere = Dropthings.Util.EncodeByEscape.GetUnEscapeStr(context.Request["strwhere"]);
            string strorder = Dropthings.Util.EncodeByEscape.GetUnEscapeStr(context.Request["orderby"]);
            int Start = Convert.ToInt32(context.Request["start"]);
            int PageSize = Convert.ToInt32(context.Request["page_size"]);
            switch (action)
            {

                case "getkeyworddata":
                    context.Response.Write(GetDataStr(strwhere, strorder, Start, PageSize));
                    break;
                default:
                    break;
            }
        }
        catch (Exception e)
        {
            context.Response.Write(e.Message);
        }
    }
    private DataTable GetDataTable(string where, string order, int start, int pagesize)
    {
        return lexiconWord.GetPager(where, null, order, pagesize, start);
    }
    private int GetCount(string where)
    {
        return lexiconWord.GetPagerRowsCount(where, null);
    }
    private string GetDataStr(string where, string orderby, int start, int pagesize)
    {
        DataTable dt = GetDataTable(where, orderby, start, pagesize);
        int totalcount = GetCount(where);
        StringBuilder jsonstr = new StringBuilder();
        if (dt.Rows.Count > 0)
        {
            jsonstr.Append("{");
            int count = 1;
            foreach (DataRow row in dt.Rows)
            {
                jsonstr.AppendFormat("\"entity_{0}\":", count);
                jsonstr.Append("{");
                jsonstr.AppendFormat("\"id\":\"{0}\",", row["id"].ToString());
                jsonstr.AppendFormat("\"title\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["title"].ToString()));
                jsonstr.AppendFormat("\"url\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["url"].ToString()));
                jsonstr.AppendFormat("\"source\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["source"].ToString()));
                jsonstr.AppendFormat("\"summary\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["summary"].ToString()));
                jsonstr.AppendFormat("\"tag\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["tag"].ToString()));
                jsonstr.AppendFormat("\"datetime\":\"{0}\"", EncodeByEscape.GetEscapeStr(row["datetime"].ToString()));
                jsonstr.Append("},");
                count++;
            }
            jsonstr.AppendFormat("\"Count\":{0}", totalcount);
            jsonstr.Append("}");
        }
        return jsonstr.ToString();
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
