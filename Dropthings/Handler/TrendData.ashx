<%@ WebHandler Language="C#" Class="TrendData" %>
using System;
using System.Web;
using System.Xml;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Dropthings.Business.Facade;
using Dropthings.Util;

public class TrendData : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        /*string fromDate = context.Request["from_date"];
        string toDate = context.Request["to_date"];
        string categoryId = context.Request["category_id"];
        StringBuilder strSql = new StringBuilder();
        strSql.Append("SELECT * FROM TRENDDATA  T  ,CATEGORY C  where T.CATEGORYID = C.CATEGORYID");
        strSql.Append(" AND C.CATEGORYID = :CATEGORYID ORDER BY T.\"DATE\" DESC");

        OracleParameter[] parameters = {
					new OracleParameter(":CATEGORYID",SqlDbType.Int)					
					};
        parameters[0].Value = Convert.ToInt64(categoryId);
        
        OracleHelper helper = new OracleHelper("SentimentConnStr");
        DataTable dt = helper.ExecuteDateSet(strSql.ToString(), parameters).Tables[0];
        StringBuilder jsonStr = new StringBuilder();
        jsonStr.Append("{");
        int count = 0;
        for (int i = 0, j = dt.Rows.Count; i < j; i++)
        {
            DateTime time = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[i][3]).ToString("yyyy-MM-dd"));
            string timeStr = TimeHelp.GetMilliSecond(time).ToString();
            jsonStr.Append("\"" + timeStr + "\":\"" + dt.Rows[i][2].ToString() + "\",");
            if (count == j - 1)
                jsonStr.Append("\"" + timeStr + "\":\"" + dt.Rows[i][2].ToString() + "\"");
            count++;
        }
        jsonStr.Append("}");
        context.Response.Write(jsonStr.ToString());*/
        string fromDate = string.Empty;
        string toDate = string.Empty;
        string categoryId = string.Empty;
        string[] info = context.Request["category_id"].Split(',');        
        int len = info.Length;
        if (len > 0)
        {
            categoryId = info[0];
        }
        if (len > 1)
        {
            fromDate = info[1];
        }
        if (len > 2)
        {
            toDate = info[2];
        }
        string backstr = TrendFacade.GetTrendStr(categoryId, fromDate, toDate);
        context.Response.ContentEncoding = Encoding.GetEncoding("gb2312");
        context.Response.Write(backstr);
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
