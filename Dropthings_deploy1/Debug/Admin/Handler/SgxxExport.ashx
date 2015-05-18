<%@ WebHandler Language="C#" Class="SgxxExport" %>

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Dropthings.Business.Facade;
using Dropthings.Data;
using Dropthings.Util;
using System.Web.SessionState;

public class SgxxExport : BriefReportTemplatePageBase
{

    protected override void InitPageTemplate()
    {
        //throw new NotImplementedException();
        try
        {
            string idlist = EncodeByEscape.GetUnEscapeStr(Context.Request["idList"]);
            string exportsqlstrwhere = " ID IN(" + idlist + ")";
            IList<ARTICLEEntity> list = ArticleFacade.Find(exportsqlstrwhere);
            this.Document.SetValue("InfoList", list);
            base.PublishFileName = "sgxxinfolist.html";
        }
        catch (Exception ex)
        {
            Context.Response.Write("{\"Error\":\"1\",\"error_msg\":\"" + ex.ToString() + "\"}");
        }     
    }

}