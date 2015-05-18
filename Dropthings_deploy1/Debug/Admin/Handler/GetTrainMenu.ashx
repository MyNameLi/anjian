<%@ WebHandler Language="C#" Class="GetTrainMenu" %>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Text;
using Dropthings.Data;
using Dropthings.Business.Facade;   

public class GetTrainMenu : IHttpHandler {  
    public void ProcessRequest (HttpContext context) {

        IList<CATEGORYEntity> entityList = CategoryFacade.GetCategoryEntityList("");    
        StringBuilder ItemList = new StringBuilder();
        ItemList.Append("<option value=\"0\">根目录</option>");
        foreach (CATEGORYEntity entity in entityList)
        {
            if (entity.ID.Value != 0)
            {
                ItemList.Append("<option value=\"" + entity.ID.Value.ToString() + "\">" + entity.CATEGORYNAME + "</option>");
            }         
        }
        context.Response.Write(ItemList.ToString());
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }   
}