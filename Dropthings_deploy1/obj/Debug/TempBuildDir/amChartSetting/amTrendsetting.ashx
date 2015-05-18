<%@ WebHandler Language="C#" Class="amTrendsetting" %>

using System;
using System.Web;
using System.Collections.Generic;
using Dropthings.Business.Facade;
using System.Text;
public class amTrendsetting : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string serverUrl = System.Configuration.ConfigurationManager.AppSettings["ServerUrl"];
        string info = context.Request["act"];
        //Dictionary<string, string> paramdict = new Dictionary<string, string>();

        Dictionary<string, string> paramdict = AmChartFacade.GetRequestParamDict(info);
        //var allKeys = context.Request.QueryString.AllKeys;
        //foreach (var item in context.Request.QueryString.AllKeys)
        //{
        //    paramdict.Add(item, context.Request.QueryString[item]);
        //}

        StringBuilder dataurl = new StringBuilder();
        if (paramdict != null && paramdict.Keys.Count > 0)
        {
            string type = paramdict["action"];
            string SelType = paramdict["selType"];
            string fieldName = string.Empty;
            if (paramdict.ContainsKey("fieldname"))
            {
                fieldName = paramdict["fieldname"];
            }
            string sort = string.Empty;
            if (paramdict.ContainsKey("sort"))
            {
                sort = paramdict["sort"];
            }
            bool dcount;
            string documentcount = string.Empty;
            if (paramdict.ContainsKey("documentcount"))
            {
                documentcount = paramdict["documentcount"];
            }
            string databasematch = string.Empty;
            if (paramdict.ContainsKey("databasematch"))
            {
                databasematch = paramdict["databasematch"];
            }
            string dateperiod = string.Empty;
            string predict = string.Empty;
            if (paramdict.ContainsKey("dateperiod"))
            {
                dateperiod = paramdict["dateperiod"];
            }
            if (paramdict.ContainsKey("predict"))
            {
                predict = paramdict["predict"];
            }
            string text = string.Empty;
            if (paramdict.ContainsKey("text"))
            {
                text = paramdict["text"];
            }

            switch (type)
            {
                case "GetQueryTagValues":
                    string fieldname = paramdict["fieldname"];
                    dataurl.AppendFormat("amChartSetting/amPieData.ashx?act=");
                    dataurl.Append("action=GetQueryTagValues|");
                    dataurl.AppendFormat("fieldname={0}|", fieldname);

                    if (bool.TryParse(documentcount, out dcount))
                    {
                        dataurl.AppendFormat("documentcount={0}|", dcount);
                    }
                    dataurl.AppendFormat("sort={0}|", sort);
                    dataurl.AppendFormat("dateperiod={0}|", dateperiod);
                    dataurl.AppendFormat("databasematch={0}|", databasematch);
                    dataurl.AppendFormat("text={0}|", text);
                    dataurl.AppendFormat("selType={0}", SelType);
                    break;
                case "getTopicDate":
                    dataurl.AppendFormat("amChartSetting/amPieData.ashx?act=");
                    dataurl.Append("action=GetQueryTagValues|");
                    dataurl.AppendFormat("dateperiod={0}|", dateperiod);
                    dataurl.AppendFormat("selType={0}", SelType);
                    break;
                default:
                    break;
            }
        }
        string setfilepath = context.Server.MapPath("~/amChartSetting/trendSettings.xml");
        string fileStr = Dropthings.Util.FileManage.ReadStr(setfilepath);
        string backstr = fileStr.Replace("{$.#datafilepath}", dataurl.ToString());
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