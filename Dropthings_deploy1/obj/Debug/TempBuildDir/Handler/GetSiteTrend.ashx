<%@ WebHandler Language="C#" Class="GetSiteTrend" %>

using System;
using System.Web;
using System.Xml;
using System.Text;
using System.Collections.Generic;
using System.Configuration;
using Dropthings.Data;
using Dropthings.Util;
using Dropthings.Business.Facade;

public class GetSiteTrend : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        string category =context.Request["category"];// "986003804631063099";
        if (!string.IsNullOrEmpty(category))
        {            
            
            string strWhere = " category =" + category + " order by firsttime ASC";
            IList<TRANSROUTEEntity> list = TransRouteFacede.Find(strWhere);
            StringBuilder jsonstr = new StringBuilder();            
            jsonstr.Append("{");
            Dictionary<DateTime, string> dict = new Dictionary<DateTime, string>();
            foreach (TRANSROUTEEntity entity in list)
            {
                DateTime key = entity.FIRSTTIME.Value;
                if (dict.ContainsKey(key))
                {
                    dict[key] = dict[key] + "," + EncodeByEscape.GetEscapeStr(entity.SITENAME);
                }
                else {
                    dict.Add(entity.FIRSTTIME.Value, EncodeByEscape.GetEscapeStr(entity.SITENAME));
                }
            }
            foreach (DateTime dt in dict.Keys)
            {
                jsonstr.AppendFormat("\"{0}\":\"{1}\",", dt, dict[dt]);
            }
            jsonstr.AppendFormat("\"Count\":{0}", dict.Keys.Count).Append("}");
            context.Response.Write(jsonstr.ToString());
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
