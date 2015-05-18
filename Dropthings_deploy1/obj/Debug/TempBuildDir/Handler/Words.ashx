<%@ WebHandler Language="C#" Class="Words" %>

using System;
using System.Web;
using Dropthings.Data;
using System.Collections.Generic;
using System.Text;
using Dropthings.Util;
using Dropthings.Business.Facade;

public class Words : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        string type = context.Request["type"];        
        StringBuilder JsonStr = new StringBuilder();        
        switch (type)
        {            
            case "innitwarningword":
                IList<WORDWARNINGEntity> list = WordWarningFacade.FindByWhere("");
                context.Response.Write(GetWordWarningRule(list));
                break;
            default:
                break;
        }       
        context.Response.Write(JsonStr.ToString());
    }

    private string GetWordWarningRule(IList<WORDWARNINGEntity> list)
    {
        StringBuilder ruleStr = new StringBuilder();
        foreach (WORDWARNINGEntity entity in list) {
            if (ruleStr.Length > 0) {
                ruleStr.Append("+OR+");
            }
            ruleStr.Append(entity.WORDRULE);
        }
        return ruleStr.ToString();
    }
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}