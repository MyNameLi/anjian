<%@ WebHandler Language="C#" Class="Publish" %>

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

public class Publish : TemplatePageBase
{
    protected override void InitPageTemplate() {
        string ArticleId = Context.Request.Form["id"];       
        ARTICLEEntity entity = ArticleFacade.GetEntityById(Convert.ToInt64(ArticleId));
        this.Document.SetValue("Article", entity);
        if (!string.IsNullOrEmpty(entity.PUBLISHPATH)) {
            string filepath = entity.PUBLISHPATH;
            int lastindex = filepath.LastIndexOf("/");
            base.PublishFileName = filepath.Substring(lastindex + 1);
        }
        //base.PublishPath = "Article\\" + entity.ColumnID.ToString() + "\\";
    }
}
