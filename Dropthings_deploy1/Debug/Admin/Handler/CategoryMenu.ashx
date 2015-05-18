<%@ WebHandler Language="C#" Class="CategoryMenu" %>
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

public class CategoryMenu : IHttpHandler
{
    private IList<int> parentList = new List<int>();
    private static string l_width = "250px";
    private static string l_left = "190px";
    public void ProcessRequest(HttpContext context)
    {
        string strWhere = context.Request.Form["str_where"];
        string left = context.Request.Form["left"];
        if (!string.IsNullOrEmpty(left))
        {
            l_left = left + "px";
        }
        IList<CATEGORYEntity> entityList = CategoryFacade.GetCategoryEntityList(strWhere.ToUpper());
        IList<CATEGORYEntity> AllList = CategoryFacade.GetCategoryEntityList(" ID > 0 order by SEQUEUE");
        foreach (CATEGORYEntity Lentity in AllList)
        {
            parentList.Add(Lentity.PARENTCATE.Value);        
        }
        StringBuilder HtmlStr = new StringBuilder();
        foreach (CATEGORYEntity entity in entityList)
        {
            HtmlStr.AppendFormat("<li class=\"left-center-nav-ul\"><img src=\"../images/yqzt/yqzt_34.jpg\"/>&nbsp;&nbsp;<span><a href=\"javascript:void(null);\" pid=\"{0}_{1}_{2}\">{3}</a></span>", entity.CATEGORYID, entity.PARENTCATE, entity.ID, entity.CATEGORYNAME);
            GetChildHtml(entity.ID.Value, ref HtmlStr,AllList,1);
            HtmlStr.Append("</li>");
        }
        context.Response.Write(EncodeByEscape.GetEscapeStr(HtmlStr.ToString()));
    }

    private void GetChildHtml(int parentCate, ref StringBuilder HtmlStr, IList<CATEGORYEntity> list, int level)
    {
        if(parentList.Contains(parentCate))
        {  
            HtmlStr.Append("<ol style=\"position:absolute; left:" + l_left + ";  background:#e9f4f8; width:" + l_width + "; top:-1px; border-top:#8fa1a3 solid 1px; color:#6b7074; border-bottom:#8fa1a3 solid 1px; border-right:#8fa1a3 solid 1px; display:none;\">");
            int count = 1;
            foreach (CATEGORYEntity entity in list)
            {
                if (entity.PARENTCATE.Value == parentCate)
                {
                    if (count == 1)
                    {
                        HtmlStr.AppendFormat("<li style=\"padding-left:10px; border-bottom:gray dotted 1px;\">→&nbsp;&nbsp;<a href=\"javascript:void(null)\" pid=\"{0}_{1}_{2}\">{3}</a>", entity.CATEGORYID, entity.PARENTCATE, entity.ID, entity.CATEGORYNAME);
                        GetChildHtml(entity.ID.Value, ref HtmlStr, list, (level + 1));
                        HtmlStr.Append("</li>");
                    }
                    else
                    {
                        HtmlStr.AppendFormat("<li style=\"border-left:#8fa1a3 solid 1px; padding-left:10px; border-bottom:gray dotted 1px;\">→&nbsp;&nbsp;<a href=\"javascript:void(null)\" pid=\"{0}_{1}_{2}\">{3}</a>", entity.CATEGORYID, entity.PARENTCATE, entity.ID, entity.CATEGORYNAME);
                        GetChildHtml(entity.ID.Value, ref HtmlStr, list, (level + 1));
                        HtmlStr.Append("</li>");
                    }
                    count++;
                }  
            }            
            HtmlStr.Append("</ol>");
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
