<%@ WebHandler Language="C#" Class="Baidu" %>
using System;
using System.Web;
using System.Xml;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Dropthings.Web.Util;

public class Baidu : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        string tn = context.Request.QueryString["tn"];
        string rn = context.Request.QueryString["rn"];
        string pn = context.Request.QueryString["pn"];
        string keyWords = context.Request.QueryString["keyWords"];

        string url = BaiduHelper.FormatNewsUrl(rn, tn, "utf-8", pn, keyWords);
        string content = Utility.GetHtml(url);

        string json = BaiduHelper.GenerateBaiduNewsInJson(content, false);
        string count = BaiduHelper.GetAllBaiduNewsCount(content);

        context.Response.Write(string.Format("{{\"count\":{0},\"news\":{1},\"rn\":{2},\"pn\":{3}}}", count, json, rn, pn));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
