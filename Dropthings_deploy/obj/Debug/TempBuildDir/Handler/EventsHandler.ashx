<%@ WebHandler Language="C#" Class="EventsHandler" %>

using System;
using System.Web;
using System.Text;
using System.Data;
using Dropthings.Business.Facade;
using Dropthings.Util;
using Dropthings.Data.SqlServerEntity;
public class EventsHandler : IHttpHandler
{


    public void ProcessRequest(HttpContext context)
    {
        string act = context.Request["queryact"];
        string action = string.Empty;
        string regjson = string.Empty;
        try
        {
            if (!string.IsNullOrEmpty(act) && act == "querystr")
            {
                action = context.Request["act"];
                int eventid = int.Parse(context.Request["eventid"]);
                DataSet ds = null;
                switch (action)
                {
                    case "initEvent":
                        regjson = "{\"success\":1,\"data\":" + GetSingleJson(EventsFacade.GetSingleEventByID(eventid)) + "}";
                        break;
                    case "initTopic":
                        ds = EventsFacade.GetEventTopicByEventID(eventid);
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            regjson = ds.Tables[0].ToJson(true);
                        }
                        break;
                    case "initClue":
                        ds = EventsFacade.GetEventClueByEventID(eventid);
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            //regjson = ds.Tables[0].ToJson();
                            regjson = ds.Tables[0].ToJson(true);
                        }
                        break;
                    case "initImg":
                        ds = EventsFacade.GetEventImgByEventID(eventid);
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            regjson = ds.Tables[0].ToJson();
                        }
                        break;
                    
                    default:
                        break;
                }
            }
            else
            {
                regjson = "{\"success\":1}";
            }
        }
        catch (Exception ex)
        {
            regjson = "{\"error\":1,\"errorMsg\":\"" + ex.Message + "\"}";
        }
        finally
        {
            context.Response.Write(regjson);
        }

    }
    private string GetSingleJson(EventsEntity entity)
    {
        System.Text.StringBuilder Eventjson = new StringBuilder();
        Eventjson.Append("{");
        Eventjson.AppendFormat("\"id\":\"{0}\",", entity.ID.ToString());
        Eventjson.AppendFormat("\"eventid\":\"{0}\",", entity.EventId.ToString());
        Eventjson.AppendFormat("\"eventname\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.EventName));
        Eventjson.AppendFormat("\"eventtime\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.EventTime.ToString()));
        Eventjson.AppendFormat("\"keywords\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.KeyWords));
        Eventjson.AppendFormat("\"summary\":\"{0}\"", EncodeByEscape.GetEscapeStr(entity.Summary));
        Eventjson.Append("}");
        return Eventjson.ToString();
    }
    

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}