<%@ WebHandler Language="C#" Class="ReportList" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using Dropthings.Data;
using Dropthings.Business.Facade;
using System.Text;

public class ReportList : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        IList<REPORTLISTEntity> list = ReportListFacade.Find(" ID>0 order by CreateTime DESC");
        StringBuilder htmlStr = new StringBuilder();
        foreach (REPORTLISTEntity entity in list)
        {

            htmlStr.Append("<div class=\"statementList\"><ul>");
            htmlStr.AppendFormat("<li><strong>{0}</strong></li>", entity.TITLE);
            htmlStr.AppendFormat("<li><strong>{0}</strong></li>", entity.CREATER);
            htmlStr.AppendFormat("<li><strong>{0}</strong></li>	", entity.CREATETIME.ToString());
            htmlStr.AppendFormat("<li><a href=\"{0}\" target=\"_blank\">报表查看</a></li>", "content/" + entity.URL);
            htmlStr.Append("</ul></div>");
        }
        context.Response.Write(htmlStr.ToString());
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}