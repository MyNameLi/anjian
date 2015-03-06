<%@ WebHandler Language="C#" Class="TongJi" %>

using System;
using System.Web;

public class TongJi : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string _w = context.Request["_w"] == null ? " 1=1 " : context.Request["_w"].ToString();
        string strwhere = _w;
        int count = Dropthings.Business.Facade.ArticleFacade.GetPagerCount(strwhere);
        string result = count.ToString();//"{count:" + count.ToString() + "}";
        context.Response.Write(result);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}