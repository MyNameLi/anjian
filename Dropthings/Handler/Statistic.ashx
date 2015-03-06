<%@ WebHandler Language="C#" Class="Statistic" %>

using System;
using System.Web;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Dropthings.Business.Facade;
using System.Configuration;
using Dropthings.Data;
public class Statistic : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        if (!string.IsNullOrEmpty(context.Request["action"]))
        {
            string action = context.Request["action"].Trim();//paramdict["action"];
            switch (action)
            {
                case "GetQueryTagValues":
                    QueryParamEntity paramEntity = QueryParamsDao.GetEntity(context);
                    Dictionary<string, string> dict = IdolStaticFacade.GetSiteStaticInfo(paramEntity);
                    if (context.Request["isBackbone"].Trim() == "true")
                    {
                        StringBuilder sbNames = new StringBuilder();
                        StringBuilder sbData = new StringBuilder();

                        sbNames.Append("[");
                        sbData.Append("[");
                        var i = 0;
                        foreach (var item in dict)
                        {
                            sbNames.AppendFormat("\"{0}\",", item.Key);
                            sbData.Append("{");
                            sbData.AppendFormat("\"y\":{0},\"color\":\"#{1}\"", item.Value, Statistic.ColumnColors[i++]);
                            sbData.Append("},");
                        }
                        sbNames.Remove(sbNames.Length - 1, 1);
                        sbData.Remove(sbData.Length - 1, 1);
                        sbNames.Append("]");
                        sbData.Append("]");
                        string result = "{\"Success\":0,\"data\":" + sbData.ToString() + ",\"names\":" + sbNames.ToString() + "}";
                        context.Response.Write(result);
                    }
                    else
                    {
                        context.Response.ContentEncoding = Encoding.GetEncoding("gb2312");
                        context.Response.Write(GetColumn3DXmlStr("站点", "", "", dict));
                    }
                    break;
                case "gettab":
                    string userid = UserFacade.GetUserId();
                    string roleid = UserFacade.GetUserRoleList();
                    string pjson = string.Empty;
                    if (string.IsNullOrEmpty(userid))
                    {
                        pjson = "{\"Success\":0}";
                    }
                    else
                    {
                        string pageid = string.Empty;
                        PageEntity.PAGEDAO pdao = new PageEntity.PAGEDAO();
                        PAGEOFROLEEntity pgoe = new PAGEOFROLEEntity();

                        List<PAGEOFROLEEntity> pgoes = new List<PAGEOFROLEEntity>();
                        if (!string.IsNullOrEmpty(roleid))
                        {
                            pgoes = PageOfRoleFacade.Find(Convert.ToInt32(roleid)) as List<PAGEOFROLEEntity>;
                            for (int i = 0; i < pgoes.Count; i++)
                            {
                                pageid += pgoes[i].PAGEID + ",";
                            }
                            pageid = pageid.Substring(0, pageid.Length - 1);
                        }

                        List<Dropthings.Data.PageEntity> pagelist = new List<Dropthings.Data.PageEntity>();

                        pagelist = pdao.Find("USERID=" + userid + " or id in(" + pageid + ") order by orderno asc", null) as List<Dropthings.Data.PageEntity>;
                        if (pagelist != null && pagelist.Count > 0)
                        {
                            pjson = PageEntityTransJson(pagelist);
                        }
                        else
                        {
                            pjson = "{\"Success\":0}";
                        }
                    }
                    context.Response.Write(pjson);
                    break;
                default:
                    break;
            }
        }
    }

    private static string[] ColumnColors = new string[] { 
            "AFD8F8", "F6BD0F", "8BBA00", "FF8E46", "008E8E", "D64646",
            "8E468E","588526","B3AA00","008ED6","9D080D","A186BE"
        };
    private static string PageEntityTransJson(List<Dropthings.Data.PageEntity> list)
    {
        StringBuilder _pE = new StringBuilder();
        _pE.Append("{\"data\":[");
        string url = string.Empty;
        for (int i = 0; i < list.Count; i++)
        {
            url = list[i].URL;
            _pE.Append("{");
            _pE.AppendFormat("\"title\":\"{0}\",\"url\":\"{1}\"", list[i].TITLE, url == "" ? "0" : url);
            _pE.Append("},");
        }
        _pE.Length = _pE.Length - 1;
        _pE.Append("],\"Success\":1}");
        return _pE.ToString();
    }
    private static string GetColumn3DXmlStr(string caption, string xAxisName, string yAxisName, Dictionary<string, string> datadict)
    {
        StringBuilder xmlstr = new StringBuilder();
        if (datadict != null && datadict.Keys.Count > 0)
        {
            xmlstr.AppendFormat("<graph caption='{0}' xAxisName='{1}' yAxisName='{2}'", caption, xAxisName, yAxisName);
            xmlstr.Append("baseFontSize='12' decimalPrecision='0' formatNumberScale='0'>");
            int index = 0;
            foreach (string key in datadict.Keys)
            {
                xmlstr.AppendFormat("<set name='{0}' value='{1}' color='{2}' />", key, datadict[key], GetColumnColor(index));
                index++;
            }
            xmlstr.Append("</graph>");
        }
        return xmlstr.ToString();
    }
    public static string GetColumnColor(int index)
    {
        return ColumnColors[index];
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}