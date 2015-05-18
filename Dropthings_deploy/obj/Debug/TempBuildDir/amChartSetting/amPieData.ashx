<%@ WebHandler Language="C#" Class="amPieData" %>

using System;
using System.Web;
using System.Collections.Generic;
using Dropthings.Business.Facade;
using Dropthings.Data;
using System.Data;
using System.Text;
public class amPieData : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string info = context.Request["act"];
        Dictionary<string, string> paramdict = AmChartFacade.GetRequestParamDict(info);
        string res = string.Empty;
        if (paramdict != null && paramdict.Keys.Count > 0)
        {
            if (!string.IsNullOrEmpty(paramdict["action"]))
            {
                switch (paramdict["action"])
                {
                    case "":
                        break;
                    default:
                        res = GetCategoryStaticPieXmlStr(context, paramdict);//, context);
                        break;
                }
            }
        }
        context.Response.Write(res);
    }

    private string GetCategoryStaticPieXmlStr(HttpContext context, Dictionary<string, string> paramdict)//, HttpContext context)
    {
        string action = paramdict["action"];
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
        string disnum = string.Empty;
        if (paramdict.ContainsKey("disnum"))
        {
            disnum = paramdict["disnum"];
        }
        int dnum;
        QueryParamEntity paramEntity = new QueryParamEntity();

        paramEntity.action = action;
        paramEntity.FieldName = fieldName;
        paramEntity.Sort = sort;
        paramEntity.DatePeriod = dateperiod;
        if (bool.TryParse(documentcount, out dcount))
        {
            paramEntity.DocumentCount = dcount;
        }
        if (int.TryParse(disnum, out dnum))
        {
            paramEntity.DisNum = dnum;
        }
        paramEntity.DataBase = databasematch;
        paramEntity.Predict = predict;
        Dictionary<string, string> dict = null;//
        //Dictionary<string, string> categorydict = GetCategoryDict(context);
        // Dictionary<string, string> datadict = new Dictionary<string, string>();
        string xmlstr = string.Empty;

        //foreach (string key in dict.Keys)
        //{
        //    string name = key;
        //    if (categorydict.ContainsKey(key))
        //    {
        //        name = categorydict[key];
        //    }
        //    string discount = dict[key];
        //    datadict.Add(name, discount);
        //}
        switch (SelType)
        {
            case "pie":
                dict = IdolStaticFacade.GetSiteStaticInfo(paramEntity);
                dict.ToString();
                if (dict.Keys.Count > 0)
                {
                    xmlstr = AmChartFacade.GetPieXmlStr(dict);
                }
                break;
            case "pieISVIP":
                dict = IdolStaticFacade.GetSiteStaticInfo(paramEntity);
                if (dict.Keys.Count > 0)
                {
                    xmlstr = AmChartFacade.GetISVIPPieXmlStr(dict);
                }
                break;
            case "piePROVINCE":
                dict = IdolStaticFacade.GetSiteStaticInfo(paramEntity);
                context.Response.ContentEncoding = Encoding.GetEncoding("gb2312");
                if (dict.Keys.Count > 0)
                {
                    xmlstr = AmChartFacade.GetPROVINCEPieXmlStr("地区 Top10", "", "", dict);
                }
                break;
            case "trend":
                dict = IdolStaticFacade.GetSiteStaticInfo(paramEntity);
                if (dict.Keys.Count > 0)
                {
                    string mindate, maxdate;
                    Dictionary<string, string> dict1 = IdolStaticFacade.GetTrendStaticInfo(paramEntity, "yyyy-MM-dd", out mindate, out maxdate);
                    xmlstr = AmChartFacade.GetTrendDataStr(dict1);
                }
                break;
            case "topicTrend":
                if (!string.IsNullOrEmpty(paramEntity.DatePeriod))
                {
                    SqlHelper sqlh = new SqlHelper("WeiboDBStr");
                    string seltopicdateSQL = string.Format("select * from TopicStatistics where topicid='{0}' order by statisticalTime desc", paramEntity.DatePeriod);
                    System.Data.DataSet ds = sqlh.ExecuteDateSet(seltopicdateSQL, null);
                    StringBuilder datastr = new StringBuilder();
                    if (ds != null && ds.Tables.Count > 0)
                    {

                        DataTable dt = ds.Tables[0];
                        foreach (DataRow item in dt.Rows)
                        {
                            int ocount = Convert.ToInt32(item["ItemCount"]);
                            int fcount = Convert.ToInt32(item["ForwardCount"]);
                            int ccount = Convert.ToInt32(item["CommontsCount"]);
                            int allcount = ocount + fcount + ccount;
                            string dtime = Convert.ToDateTime(item["StatisticalTime"]).ToString("yyyy-MM-dd");
                            datastr.AppendFormat("{0},{1},{2}", dtime, allcount, allcount);
                            datastr.Append("\r\n");
                        }
                        xmlstr = datastr.ToString();
                    }
                }
                break;
            default:
                break;

        }
        return xmlstr;
    }



    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}