<%@ WebHandler Language="C#" Class="GetMenuList" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using Dropthings.Data;
using Dropthings.Business.Facade;
using Dropthings.Util;
using System.Text;

public class GetMenuList : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string type = context.Request["type"];
        string parentId = context.Request["parent_id"];
        string roleidlist = UserFacade.GetUserRoleList();
        var facade = Services.Get<Facade>();
        switch (type)
        {
            case "getstairmenuhtml":
                if (!string.IsNullOrEmpty(roleidlist))
                {
                    string userName = UserFacade.GetUserName();
                    IList<MENULISTEntity> stairmenulist = facade.GetMenuListByRoleIdAndParent(roleidlist, 0);
                    string backstr = "{\"username\":\"" + userName + "\",\"menulist\":" + GetStairMenuHtml(stairmenulist) + "}";
                    context.Response.Write(backstr);
                }
                break;
            case "getitemmenuhtml":
                if (!string.IsNullOrEmpty(roleidlist))
                {
                    StringBuilder leftmenuhtml = new StringBuilder();
                    IList<MENULISTEntity> leftmenulist = facade.GetMenuChildListByRoleIdAndParent(roleidlist, Convert.ToInt32(parentId));
                    GetLeftrMenuHtml(leftmenulist, Convert.ToInt32(parentId), ref leftmenuhtml);
                    context.Response.Write(leftmenuhtml.ToString());
                }
                break;
            default:
                break;
        }
    }

    private string GetStairMenuHtml(IList<MENULISTEntity> list)
    {
        StringBuilder htmlstr = new StringBuilder();
        if (list.Count > 0)
        {
            htmlstr.Append("{");
            foreach (MENULISTEntity entity in list)
            {
                htmlstr.AppendFormat("\"{0}\":", EncodeByEscape.GetEscapeStr(entity.NAME));
                htmlstr.Append("{");
                htmlstr.AppendFormat("\"id\":\"{0}\",", entity.ID);
                htmlstr.AppendFormat("\"lefturl\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.LEFTURL));
                htmlstr.AppendFormat("\"righturl\":\"{0}\"", EncodeByEscape.GetEscapeStr(entity.URLPATH));
                htmlstr.Append("},");
            }
            htmlstr.Append("\"SuccessCode\":1}");
        }
        return htmlstr.ToString();
    }

    private void GetLeftrMenuHtml(IList<MENULISTEntity> leftmenulist, int parentid, ref StringBuilder leftmenuhtml)
    {
        int count = 1;
        foreach (MENULISTEntity entity in leftmenulist)
        {
            if (entity.PARENTID.Value == parentid)
            {
                leftmenuhtml.Append("<div class=\"bar\">");
                if (IsLeafNode(leftmenulist, entity.ID.Value))
                {
                    leftmenuhtml.Append("<span class=\"img\"><img src=\"../images/left_img.gif\" /></span>");
                    leftmenuhtml.AppendFormat("<span class=\"fon\">{0}</span>", entity.NAME);
                    GetLeafNodeHtml(leftmenulist, entity.ID.Value, ref leftmenuhtml, count);
                }
                else
                {
                    leftmenuhtml.Append("<div class=\"menu_child\"><ul>");
                    if (count == 1)
                    {
                        leftmenuhtml.Append("<li style=\"background:#3a7fb6; color:#ffffff;\"><a  style=\"color:#ffffff;\"");
                        leftmenuhtml.AppendFormat("href=\"javascript:void(null);\" pid=\"{0}\">{1}</a></li>", entity.URLPATH, entity.NAME);
                    }
                    else
                    {
                        leftmenuhtml.AppendFormat("<li><a href=\"javascript:void(null);\" pid=\"{0}\">{1}</a></li>", entity.URLPATH, entity.NAME);
                    }
                    leftmenuhtml.Append("</ul></div> ");
                }
                count++;
            }
        }
    }

    private void GetLeafNodeHtml(IList<MENULISTEntity> leftmenulist, int parentid, ref StringBuilder leftmenuhtml, int level)
    {
        int count = 1;
        leftmenuhtml.Append("<div class=\"menu_child\"");
        if (level > 1)
        {
            leftmenuhtml.Append(" style=\"display:none;\"");
        }
        leftmenuhtml.Append("><ul>");
        foreach (MENULISTEntity entity in leftmenulist)
        {
            if (entity.PARENTID.Value == parentid)
            {
                if (count == 1)
                {
                    if (level == 1)
                    {
                        leftmenuhtml.Append("<li style=\"background:#3a7fb6; color:#ffffff;\"><a  style=\"color:#ffffff;\"");
                    }
                    else
                    {
                        leftmenuhtml.Append("<li><a ");
                    }
                    leftmenuhtml.AppendFormat("href=\"javascript:void(null);\" pid=\"{0}\">{1}</a></li>", entity.URLPATH, entity.NAME);

                }
                else
                {
                    leftmenuhtml.AppendFormat("<li><a href=\"javascript:void(null);\" pid=\"{0}\">{1}</a></li>", entity.URLPATH, entity.NAME);
                }
                count++;
            }
        }
        leftmenuhtml.Append("</ul></div> ");
    }

    private bool IsLeafNode(IList<MENULISTEntity> leftmenulist, int id)
    {
        bool tag = false;
        foreach (MENULISTEntity entity in leftmenulist)
        {
            if (entity.PARENTID == id)
            {
                tag = true;
            }
        }
        return tag;
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}