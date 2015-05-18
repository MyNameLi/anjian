<%@ WebHandler Language="C#" Class="Telempalte" %>


using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using System.Text;
using Dropthings.Data;
using Dropthings.Util;
using Dropthings.Business.Facade;

public class Telempalte : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {


        IList<TEMPLATEEntity> list = TemplateFacade.GetList("");       
        string ResponseStr = GetJson(list);
        context.Response.Write(ResponseStr);
    }

    private string GetJson(IList<TEMPLATEEntity> list)
    {
        StringBuilder jsonStr = new StringBuilder();
        if (list.Count > 0) {
            jsonStr.Append("{");
            int count = 1;
            foreach (TEMPLATEEntity entity in list)
            {
                jsonStr.AppendFormat("\"entity_{0}\":", count);
                jsonStr.Append("{");
                jsonStr.AppendFormat("\"Name\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.TEMPLATENAME));
                jsonStr.AppendFormat("\"Path\":\"{0}\"", EncodeByEscape.GetEscapeStr(entity.TEMPLATEPATH));
                jsonStr.Append("},");
                count++;
            }
            jsonStr.Append("\"SuccessCode\":1}");
        }
        return jsonStr.ToString();
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }
}