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
        string res = string.Empty;
        string act = context.Request["action"];
        if (!string.IsNullOrEmpty(act))
        {
            switch (act.ToLower())
            {
                case "":
                    break;
                default:
                    res = GetCategoryStaticPieXmlStr(context);
                    break;
            }
        }
        context.Response.Write(res);
    }
    private string GetCategoryStaticPieXmlStr(HttpContext context)
    {
        string SelType = context.Request["selType"].ToLower().Trim();
        //string fieldName = string.Empty;
        //if (paramdict.ContainsKey("fieldname"))
        //{
        //    fieldName = paramdict["fieldname"];
        //}

        //string sort = string.Empty;
        //if (paramdict.ContainsKey("sort"))
        //{
        //    sort = paramdict["sort"];
        //}
        //bool dcount;
        //string documentcount = string.Empty;
        //if (paramdict.ContainsKey("documentcount"))
        //{
        //    documentcount = paramdict["documentcount"];
        //}
        //string databasematch = string.Empty;
        //if (paramdict.ContainsKey("databasematch"))
        //{
        //    databasematch = paramdict["databasematch"];
        //}
        //string minDate = string.Empty;
        //if (paramdict.ContainsKey("mindate"))
        //{
        //    minDate = paramdict["mindate"];
        //}
        //string dateperiod = string.Empty;
        //string predict = string.Empty;
        //if (paramdict.ContainsKey("dateperiod"))
        //{
        //    dateperiod = paramdict["dateperiod"];
        //}
        //if (paramdict.ContainsKey("predict"))
        //{
        //    predict = paramdict["predict"];
        //}
        //string text = string.Empty;
        //if (paramdict.ContainsKey("text"))
        //{
        //    text = paramdict["text"];
        //}
        //string disnum = string.Empty;
        //if (paramdict.ContainsKey("disnum"))
        //{
        //    disnum = paramdict["disnum"];
        //}
        //int dnum;
        QueryParamEntity paramEntity = QueryParamsDao.GetEntity(context);
        Dictionary<string, string> dict = null;
        string xmlstr = string.Empty;
        switch (SelType)
        {
            case "pie":
                dict = IdolStaticFacade.GetSiteStaticInfo(paramEntity);
                dict.ToString();
                StringBuilder resultBud = new StringBuilder();
                resultBud.Append("[");
                foreach (var item in dict)
                {
                    resultBud.Append("{");
                    resultBud.AppendFormat("\"name\":\"{0}\",\"y\":{1}", item.Key, item.Value);
                    resultBud.Append("},");

                }
                resultBud.Remove(resultBud.Length - 1, 1);
                resultBud.Append("]");
                if (dict.Keys.Count > 0)
                {
                    xmlstr = "{\"data\":" + resultBud.ToString() + "}";
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
                StringBuilder strBder = new StringBuilder();
                strBder.Append("[");
                foreach (var item in dict)
                {
                    strBder.Append("{\"x\":" + (Double.Parse(item.Key) * 1000) + ",\"y\":" + item.Value + "},");
                }
                strBder.Remove(strBder.Length - 1, 1);
                strBder.Append("]");


                if (dict.Keys.Count > 0)
                {
                    xmlstr = "{\"data\":" + strBder.ToString() + "}";
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