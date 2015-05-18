<%@ WebHandler Language="C#" Class="amPieData" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Web.SessionState;
using Dropthings.Business.Facade;
public class amPieData : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest (HttpContext context) {
        string info = context.Request["act"];
        Dictionary<string, string> paramdict = AmChartFacade.GetRequestParamDict(info);
        string res = string.Empty;
        if (paramdict != null && paramdict.Keys.Count > 0)
        {
            string type = string.Empty;
            if (paramdict.ContainsKey("type"))
            {
                type = paramdict["type"];
            }            
            switch (type) { 
                case "categorystatic":
                    break;
                default:
                    res = GetCategoryStaticPieXmlStr(paramdict, context);
                    break;    
            }
        }
        context.Response.Write(res);
    }

    private string GetCategoryStaticPieXmlStr(Dictionary<string, string> paramdict,HttpContext context)
    {
        string startTime = paramdict["starttime"];
        string endTime = paramdict["endtime"];
        string fieldName = paramdict["fieldname"];
        string filedValue = string.Empty;
        //if (paramdict.ContainsKey("fieldvalue"))
        //{
        //    filedValue = paramdict["fieldvalue"];
        //}
        //string filedStr = string.Empty;
        //if (paramdict.ContainsKey("fieldstr"))
        //{
        //    filedValue = paramdict["fieldstr"];
        //}
        //int disnum = Convert.ToInt32(paramdict["disnum"]);
        //string database = paramdict["database"];
        
        //QueryParamEntity paramEntity = new QueryParamEntity();
        //if (!string.IsNullOrEmpty(filedStr))
        //{
        //    paramEntity.FieldText = "MATCH{" + filedValue + "}:" + filedStr;
        //}
        //paramEntity.FieldName = fieldName;
        //paramEntity.Sort = "DocumentCount";
        //if (disnum > -1)
        //{
        //    paramEntity.DisNum = disnum - 1;
        //    paramEntity.DisOther = true;
        //}
        //if (!string.IsNullOrEmpty(startTime))
        //{
        //    paramEntity.MinDate = startTime;
        //}
        //if (!string.IsNullOrEmpty(endTime))
        //{
        //    paramEntity.MaxDate = endTime;
        //}
        //if (!string.IsNullOrEmpty(database))
        //{
        //    paramEntity.DataBase = database;
        //}
        //Dictionary<string, string> dict = IdolStaticFacade.GetSiteStaticInfo(paramEntity);
        //Dictionary<string, string> categorydict = GetCategoryDict(context);
        //Dictionary<string, string> datadict = new Dictionary<string, string>();
        //string xmlstr = string.Empty;
        //if (dict.Keys.Count > 0)
        //{            
        //    foreach (string key in dict.Keys)
        //    {
        //        string name = key;
        //        if (categorydict.ContainsKey(key))
        //        {
        //            name = categorydict[key];
        //        }
        //        string discount = dict[key];
        //        datadict.Add(name, discount);          
        //    }
        string xmlstr = "";// AmChartFacade.GetPieXmlStr(datadict);      
        //}
        return xmlstr;
    }

    //private Dictionary<string, string> GetCategoryDict(HttpContext context) {
    //    Dictionary<string, string> dict = new Dictionary<string, string>();
    //    string SessionKey = "categorydict";
    //    if (HttpContext.Current.Session[SessionKey] == null)
    //    {
    //        IList<Category_1Entity> list = Category_1Facade.GetCategoryEntityList("");
    //        if (list != null && list.Count > 0) {
    //            foreach (Category_1Entity entity in list)
    //            {
    //                string key = entity.CategoryID.ToString();
    //                string name = entity.CategoryName;
    //                if (!dict.ContainsKey(key)) {
    //                    dict.Add(key, name);
    //                }
    //            }
    //            HttpContext.Current.Session[SessionKey] = dict;
    //        }
    //    }
    //    else
    //    {
    //        dict = HttpContext.Current.Session[SessionKey] as Dictionary<string, string>;
    //    }
    //    return dict;
    //}
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}