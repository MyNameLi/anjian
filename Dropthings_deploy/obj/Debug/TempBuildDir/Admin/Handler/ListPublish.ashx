<%@ WebHandler Language="C#" Class="ListPublish" %>

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

public class ListPublish : TemplatePageBase
{
    protected override void InitPageTemplate() {    
        string ColumnID = Context.Request.Form["id"];
        int start = Convert.ToInt32(Context.Request.Form["start"]);
        int pagesize = Convert.ToInt32(Context.Request.Form["pagesize"]);        
        int pagecount = Convert.ToInt32(Context.Request.Form["pagecount"]);        
        string strwhere = Context.Request.Form["strwhere"];
        COLUMNDEFEntity entity = ColumnFacade.GetEntityById(Convert.ToInt64(ColumnID));
        string filename = entity.COLUMNNAMERULE;
        int index = filename.LastIndexOf(".");
        string l_filename = filename.Substring(0, index);
        base.UpdatePath = filename;       
        if (start == 1)
        {
            base.PublishFileName = l_filename + ".html";
        }
        else {
            base.PublishFileName = l_filename + "_" + start.ToString() + ".html";
        }
        this.Document.SetValue("ColumnEntity", entity);
        this.Document.SetValue("start", start);
        this.Document.SetValue("prev", start - 1);
        this.Document.SetValue("next", start + 1);
        this.Document.SetValue("listsql", GetPagerSql(start, pagesize, null, strwhere));        
        this.Document.SetValue("pagecount", pagecount);
        this.Document.SetValue("baseurl", l_filename);
        this.Document.SetValue("pagerstartindex", PagerStarIndex(start, pagecount));
        this.Document.SetValue("pagerendindex", PagerEndIndex(start, pagecount));
    }

    private int PagerStarIndex(int start,int totalcount) {        
        if (start < 6) {
            return 1;
        }
        else if (start >= 6 && start <= totalcount - 4)
        {
            return start - 5;
        }
        else
        {
            if(totalcount-9 > 0)
		    {
                return totalcount-9;			    
		    }
		    else
		    {
                return 1;
		    }
        }
    
    }

    private int PagerEndIndex(int start, int totalcount)
    {
        if (start < 6)
        {
            if (totalcount < 10)
            {
                return totalcount;
            }
            else
            {
                return 10;
            }

        }
        else if (start >= 6 && start <= (totalcount - 4))
        {
            return start + 4;
        }
        else
        {

            return totalcount;
        }
    }

    private string GetPagerSql(int start, int pageSize,string orderBy, string where)
    {
        int startNumber = pageSize * (start - 1) + 1;
        int endNumber = pageSize * start;
        StringBuilder sql = new StringBuilder();


        sql.Append("SELECT * FROM (");
        sql.Append(" SELECT A.*, ROWNUM RN ");
        sql.Append("FROM (SELECT * FROM ARTICLE ");
        if (!string.IsNullOrEmpty(where))
        {
            sql.Append(" where " + where);
        }
        if (!string.IsNullOrEmpty(orderBy))
        {
            sql.AppendFormat(" ORDER BY {0}", orderBy);
        }
        else
        {

            sql.Append(" ORDER BY ID");//默认按主键排序

        }
        sql.AppendFormat(" ) A WHERE ROWNUM <= {0})", endNumber);
        sql.AppendFormat(" WHERE RN >= {0}", startNumber);
        return sql.ToString();
    }
}