<%@ WebHandler Language="C#" Class="TestCategoryMenu" %>
using System;
using System.Web;
using System.Xml;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Dropthings.Data;
using Dropthings.Util;
using Dropthings.Business.Facade;
using IdolACINet;

public class TestCategoryMenu : IHttpHandler
{
    private IList<int> parentList = new List<int>();    
    public void ProcessRequest(HttpContext context)
    {
        string strWhere = context.Request.Form["str_where"];        
        IList<CATEGORYEntity> entityList = CategoryFacade.GetCategoryEntityList(strWhere.ToUpper());
        IList<CATEGORYEntity> AllList = CategoryFacade.GetCategoryEntityList(" ID > 0 order by SEQUEUE");
        foreach (CATEGORYEntity Lentity in AllList)
        {
            parentList.Add(Lentity.PARENTCATE.Value);        
        }
        StringBuilder HtmlStr = new StringBuilder();
        foreach (CATEGORYEntity entity in entityList)
        {
            HtmlStr.AppendFormat("<li pid=\"{0}_{1}_{2}\" class=\"on\"><a href=\"javascript:void(null);\"><b>{3}</b></a>", entity.CATEGORYID, entity.PARENTCATE, entity.ID, entity.CATEGORYNAME);          
            GetChildHtml(entity.ID.Value, ref HtmlStr,AllList);
            HtmlStr.Append("</li>");    
        }
        context.Response.Write(EncodeByEscape.GetEscapeStr(HtmlStr.ToString()));
    }

    private void GetChildHtml(int parentCate, ref StringBuilder HtmlStr, IList<CATEGORYEntity> list)
    {
        if(parentList.Contains(parentCate))
        {
            HtmlStr.Append("<div style=\"display:none;\"><ul class=\"submenu\">");           
            foreach (CATEGORYEntity entity in list)
            {
                if (entity.PARENTCATE.Value == parentCate)
                {
                    HtmlStr.AppendFormat("<li pid=\"{0}_{1}_{2}\" class=\"on\"><a href=\"javascript:void(null);\"><b>{3}</b></a>", entity.CATEGORYID, entity.PARENTCATE, entity.ID, entity.CATEGORYNAME);          
                    GetChildHtml(entity.ID.Value, ref HtmlStr, list);
                    HtmlStr.Append("</li>");                            
                }  
            }
            HtmlStr.Append("</ul></div>");
        }
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
