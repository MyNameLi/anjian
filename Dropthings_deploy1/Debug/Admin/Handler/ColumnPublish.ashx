<%@ WebHandler Language="C#" Class="ColumnPublish" %>


using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using System.Text;
using System.Data;
using Dropthings.Data;
using Dropthings.Util;
using Dropthings.Business.Facade;

public class ColumnPublish : TemplatePageBase
{
    protected override void InitPageTemplate() {
        string ColumnID = Context.Request.Form["id"];       
        COLUMNDEFEntity entity = ColumnFacade.GetEntityById(Convert.ToInt64(ColumnID));
        base.PublishFileName = entity.COLUMNNAMERULE;
        this.Document.SetValue("ColumnEntity", entity);
    }    
}
