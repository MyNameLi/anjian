<%@ WebHandler Language="C#" Class="user" %>


using System;
using System.Collections.Generic;
using System.Web;
using Dropthings.Data;
using Dropthings.Business.Facade;
using Dropthings.Util;
using System.Text;
using System.Data;

public class user : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        string type = context.Request["type"];
        string curparentid = context.Request.Form["ParentId"];
        string responsestr = string.Empty;
        var facade = Services.Get<Facade>();
        switch (type)
        {
            case "alluser":
                IList<UsersEntity> list = facade.GetUserList(null, false);
                if (list.Count == 0) { responsestr = "{}"; }
                else { responsestr = GetUserListInJSON(list); }
                break;
            case "checkuser":

                break;
            case "getcurrentmenulist":
                if (!string.IsNullOrEmpty(curparentid))
                {
                    int ParentId = Convert.ToInt32(curparentid);
                    IList<MENULISTEntity> menulist = facade.GetMenuListByRoleIdAndParent(1, ParentId);
                    responsestr = GetMenuJson(menulist);
                }
                break;
            case "getchildmenulist":
                if (!string.IsNullOrEmpty(curparentid))
                {
                    int ParentId = Convert.ToInt32(curparentid);
                    IList<MENULISTEntity> menulist = facade.GetMenuChildListByRoleIdAndParent(1, ParentId);
                }
                break;
            default:
                break;
        }
        context.Response.Write(responsestr);
    }

    private string GetUserListInJSON(IList<UsersEntity> userList)
    {
        StringBuilder jsonstr = new StringBuilder();
        jsonstr.Append("{");
        foreach (UsersEntity entity in userList)
        {
            jsonstr.AppendFormat("\"{0}\":\"{1}\",", entity.USERID, EncodeByEscape.GetEscapeStr(entity.USERNAME));
        }
        jsonstr.Remove(jsonstr.Length - 1, 1);
        jsonstr.Append("}");

        return jsonstr.ToString();
    }

    private string GetMenuJson(IList<MENULISTEntity> menulist)
    {
        StringBuilder jsonstr = new StringBuilder();
        jsonstr.Append("{");
        int count = 1;
        foreach (MENULISTEntity entity in menulist)
        {
            jsonstr.AppendFormat("\"entity_{0}\":", count);
            jsonstr.Append("{");
            jsonstr.AppendFormat("\"name\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.NAME));
            jsonstr.AppendFormat("\"lefturl\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.LEFTURL));
            jsonstr.AppendFormat("\"urlpath\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.URLPATH));
            jsonstr.AppendFormat("\"menuid\":\"{0}\"", entity.ID);
            jsonstr.Append("},");
            count++;
        }
        jsonstr.Append("\"Success\":1}");
        return jsonstr.ToString();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}

