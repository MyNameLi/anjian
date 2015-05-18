<%@ WebHandler Language="C#" Class="Feedback" %>

using System;
using System.Web;

public class Feedback : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {

        context.Response.ContentType = "text/plain";

        string switch_on = context.Request.Form["action"].ToString();
        string result = "{\"success\":1}";
        switch (switch_on)
        {
            case "create":
                CreateFeedback(context);
                break;
            case "delete":
                DelFeedBack(context);
                break;
            case "findbyid":
                result = GetFeedBackByID(context);
                break;
            case "update":
                UpdateFeedBack(context);
                break;
            default:
                break;
        }
        context.Response.Write(result);
    }
    public void UpdateFeedBack(HttpContext context)
    {
        Dropthings.Data.REPORTLISTEntity.REPORTLISTDAO Dao = new Dropthings.Data.REPORTLISTEntity.REPORTLISTDAO();
        Dropthings.Data.REPORTLISTEntity entity = ContextToEntity(context);
        Dao.Update(entity);
    }
    public void CreateFeedback(HttpContext context)
    {
        Dropthings.Data.REPORTLISTEntity.REPORTLISTDAO Dao = new Dropthings.Data.REPORTLISTEntity.REPORTLISTDAO();
        Dropthings.Data.REPORTLISTEntity entity = ContextToEntity(context);
        Dao.Add(entity);
    }
    public Dropthings.Data.REPORTLISTEntity ContextToEntity(HttpContext context)
    {
        string id = context.Request["id"];
        string title = context.Request["title"].ToString();
        string content = context.Request["content"].ToString();
        string name = context.Request["name"].ToString();
        string datetime = context.Request["datetime"].ToString();
        if (string.IsNullOrEmpty(id))
        {
            id = "0";
        }
        Dropthings.Data.REPORTLISTEntity entity = new Dropthings.Data.REPORTLISTEntity();
        entity.TITLE = title;
        entity.CREATER = name;
        entity.CREATETIME = DateTime.Parse(datetime);
        entity.STATUS = "3";
        entity.OPINIONCONTENT = content;
        entity.TYPE = 5;
        entity.ID = int.Parse(id);
        return entity;
    }
    public void DelFeedBack(HttpContext context)
    {
        string id = context.Request["id"];
        Dropthings.Data.REPORTLISTEntity.REPORTLISTDAO Dao = new Dropthings.Data.REPORTLISTEntity.REPORTLISTDAO();
        Dao.Delete(id);
    }
    public string GetFeedBackByID(HttpContext context)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        Dropthings.Data.REPORTLISTEntity.REPORTLISTDAO Dao = new Dropthings.Data.REPORTLISTEntity.REPORTLISTDAO();
        string id = context.Request["id"].ToString();
        var entity = Dao.FindById(int.Parse(id));
        sb.Append("{");
        sb.AppendFormat("\"ID\":\"{0}\",", entity.ID);
        sb.AppendFormat("\"TITLE\":\"{0}\",", entity.TITLE);
        sb.AppendFormat("\"CREATER\":\"{0}\",", entity.CREATER);
        sb.AppendFormat("\"CREATETIME\":\"{0}\",", entity.CREATETIME.ToString());
        sb.AppendFormat("\"OPINIONCONTENT\":\"{0}\"", entity.OPINIONCONTENT);
        sb.Append("}");
        return sb.ToString();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}