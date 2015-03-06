<%@ WebHandler Language="C#" Class="Task" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using System.Text;
using Dropthings.Data;
using Dropthings.Util;
using System.IO;
using Dropthings.Business.Facade;

public class Task : IHttpHandler
{

    private static ClawerNotify.Server server = new ClawerNotify.Server();

    public void ProcessRequest(HttpContext context)
    {
        string type = context.Request.Form["type"];
        switch (type)
        {
            case "add":
                AddTask(context);
                break;
            case "update"://
                UpdateData(context);
                break;
            case "edit"://
                InnitData(context);
                break;
            case "delete":
                DeleteData(context);
                break;
            case "createxml":
                CreateXml(context);
                break;
            case "addtask":
                nodeCrawl(context, "add");
                break;
            case "starttask":
                nodeCrawl(context, "start");
                break;
            case "stoptask":
                nodeCrawl(context, "stop");
                break;
            default:
                break;
        }

    }

    private void nodeCrawl(HttpContext context, string action)
    {
        try
        {
            string taskid = context.Request.Form["task_id"];
            TaskXml.InformTheMsg(Convert.ToInt32(taskid), action);
            context.Response.Write("{\"SuccessCode\":1}");
        }
        catch (Exception e)
        {
            context.Response.Write("{\"SuccessCode\":0,\"Error\":\"" + e.ToString() + "\"}");
        }
    }

    private void AddTask(HttpContext context)
    {
        try
        {
            TASKEntity entity = CrawlTaskFacade.GetTaskEntity(context);
            if (entity != null)
            {
                CrawlTaskFacade.AddTask(context, entity);
            }
            context.Response.Write("{\"SuccessCode\":1}");
        }
        catch (Exception e)
        {
            context.Response.Write("{\"SuccessCode\":0,\"Error\":\"" + e.ToString() + "\"}");
        }
    }

    private void InnitData(HttpContext context)
    {
        string taskid = context.Request.Form["task_id"];
        StringBuilder jsonStr = new StringBuilder();
        jsonStr.Append("{\"taskEntity\":").Append(GetTaskEntityJsonStr(taskid)).Append(",");
        jsonStr.Append("\"urlRuleEntityList\":").Append(GetUrlRuleEntityListJsonStr(taskid)).Append(",");
        jsonStr.Append("\"contentRuleEntityList\":").Append(GetContentRuleEntityListJsonStr(taskid)).Append("}");
        context.Response.Write(jsonStr.ToString());
    }

    private void CreateXml(HttpContext context)
    {
        string taskid = context.Request.Form["task_id"];
        try
        {
            TaskXml.Load(Convert.ToInt32(taskid));
            context.Response.Write("{\"SuccessCode\":1}");
        }
        catch (Exception e)
        {
            context.Response.Write("{\"SuccessCode\":0,\"Error\":\"" + e.ToString() + "\"}");
        }
    }

    private void DeleteData(HttpContext context)
    {
        try
        {
            string taskid = context.Request.Form["task_id"];
            CrawlTaskFacade.DeleteTask(taskid);
            TaskXml.delete(Convert.ToInt32(taskid));
            context.Response.Write("{\"SuccessCode\":1}");
        }
        catch (Exception e)
        {
            context.Response.Write("{\"SuccessCode\":0,\"Error\":\"" + e.ToString() + "\"}");
        }
    }

    private void UpdateData(HttpContext context)
    {
        try
        {
            string taskid = context.Request.Form["task_id"];
            if (!string.IsNullOrEmpty(taskid))
            {
                CrawlTaskFacade.UpDateTask(context, taskid);
            }
            context.Response.Write("{\"SuccessCode\":1}");
        }
        catch (Exception e)
        {
            context.Response.Write("{\"SuccessCode\":0,\"Error\":\"" + e.ToString() + "\"}");
        }
    }

    private string GetTaskEntityJsonStr(string taskid)
    {
        StringBuilder jsonStr = new StringBuilder();

        if (!string.IsNullOrEmpty(taskid))
        {
            jsonStr.Append("{");
            TASKEntity entity = CrawlTaskFacade.GetTaskEntityById(taskid);
            jsonStr.AppendFormat("\"task_id\":{0},", entity.TASKID);
            jsonStr.AppendFormat("\"task_name\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.TASKNAME));
            jsonStr.AppendFormat("\"task_type\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.TASKTYPE));
            jsonStr.AppendFormat("\"url_entry\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.URLENTRY));
            jsonStr.AppendFormat("\"site_code\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.SITECODE));
            jsonStr.AppendFormat("\"spider_degree\":{0},", entity.SPIDERDEGREE);
            jsonStr.AppendFormat("\"is_agent\":\"{0}\",", entity.ISAGENT);
            jsonStr.AppendFormat("\"agent_server_ip\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.AGENTSERVERIP));
            jsonStr.AppendFormat("\"agent_server_port\":\"{0}\",", entity.AGENTSERVERPORT);
            jsonStr.AppendFormat("\"agent_server_user\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.AGENTSERVERUSER));
            jsonStr.AppendFormat("\"agent_server_pwd\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.AGENTSERVERPWD));
            jsonStr.AppendFormat("\"is_login\":\"{0}\",", entity.ISLOGIN);
            jsonStr.AppendFormat("\"login_site\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.LOGINSITE));
            jsonStr.AppendFormat("\"login_data\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.LOGINDATA));
            jsonStr.AppendFormat("\"is_update\":\"{0}\",", entity.ISUPDATE);
            jsonStr.AppendFormat("\"update_timespan\":\"{0}\",", entity.UPDATETIMESPAN);
            jsonStr.AppendFormat("\"page_url_reg\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.PAGEURLREG));
            jsonStr.AppendFormat("\"task_des\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.TASKDES));
            jsonStr.AppendFormat("\"url_prefix\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.URLPREFIX));
            jsonStr.AppendFormat("\"page_content_reg\":\"{0}\"", EncodeByEscape.GetEscapeStr(entity.PAGECONTENTREG));
            jsonStr.Append("}");
        }
        return jsonStr.ToString();
    }

    private string GetUrlRuleEntityListJsonStr(string taskid)
    {
        StringBuilder jsonStr = new StringBuilder();
        if (!string.IsNullOrEmpty(taskid))
        {
            IList<URLRULEEntity> list = CrawlTaskFacade.GetUrlEntityList(taskid);
            if (list.Count > 0)
            {
                jsonStr.Append("{");
                int count = 0;
                foreach (URLRULEEntity entity in list)
                {
                    jsonStr.Append("\"entity").Append(count).Append("\":{");
                    jsonStr.AppendFormat("\"rule_object\":{0},", entity.RULEOBJECT.Value);
                    jsonStr.AppendFormat("\"rule_active\":{0},", entity.RULEACTIVE.Value);
                    jsonStr.AppendFormat("\"rule_keyword\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.RULEKEYWORD));
                    jsonStr.AppendFormat("\"task_id\":{0}", entity.TASKID);
                    jsonStr.Append("},");
                    count++;
                }
                jsonStr.Append("\"SuccessCode\":1}");
            }
        }
        if (jsonStr.Length == 0)
        {
            jsonStr.Append("\"\"");
        }
        return jsonStr.ToString();
    }

    private string GetContentRuleEntityListJsonStr(string taskid)
    {
        StringBuilder jsonStr = new StringBuilder();
        if (!string.IsNullOrEmpty(taskid))
        {
            IList<CONTENTRULEEntity> list = CrawlTaskFacade.GetContentRuleEntityList(taskid);
            if (list.Count > 0)
            {
                jsonStr.Append("{");
                int count = 0;
                foreach (CONTENTRULEEntity entity in list)
                {
                    jsonStr.Append("\"entity").Append(count).Append("\":{");
                    jsonStr.AppendFormat("\"field_str\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.FIELDSTR));
                    jsonStr.AppendFormat("\"field_type\":\"{0}\",", entity.FIELDTYPE);
                    jsonStr.AppendFormat("\"field_source\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.FIELDSOURCE));
                    jsonStr.AppendFormat("\"field_suffix\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.FIELDSUFFIX));
                    jsonStr.AppendFormat("\"is_remove_html\":\"{0}\",", entity.ISREMOVEHTML);
                    jsonStr.AppendFormat("\"is_intervar\":\"{0}\",", entity.ISINTERVAR);
                    jsonStr.AppendFormat("\"is_date\":\"{0}\",", entity.ISDATE);
                    jsonStr.AppendFormat("\"field_reg\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.FIELDREG));
                    jsonStr.AppendFormat("\"field_param1\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.PARAM1));
                    jsonStr.AppendFormat("\"field_param2\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.PARAM2));
                    jsonStr.AppendFormat("\"field_param3\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.PARAM3));
                    jsonStr.AppendFormat("\"field_param4\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.PARAM4));
                    jsonStr.AppendFormat("\"task_id\":\"{0}\"", entity.TASKID);
                    jsonStr.Append("},");
                    count++;
                }
                jsonStr.Append("\"SuccessCode\":1}");
            }
        }
        if (jsonStr.Length == 0)
        {
            jsonStr.Append("\"\"");
        }
        return jsonStr.ToString();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}