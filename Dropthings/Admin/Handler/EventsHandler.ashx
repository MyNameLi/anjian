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
                //public field
                int eventid = int.Parse(context.Request["eventid"]);
                String id = context.Request["ID"];
                String title = context.Request["title"];
                String docId = context.Request["docId"];
                //event field
                String summary = context.Request["summary"];
                String eventName = context.Request["eventName"];
                String eventTime = context.Request["eventTime"];
                String keyword = context.Request["keyword"];
                //clue field
                String clueTime = context.Request["clueTime"];
                //topic field
                String topicImage = context.Request["topicImage"];
                String topicContent = context.Request["topicContent"];
                //img field
                String imgPath = context.Request["imgPath"];
                String imgUrl = context.Request["imgUrl"];
                String imgType = context.Request["imgType"];
                string imgAlt = context.Request["imgAlt"];

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
                            regjson = ds.Tables[0].ToJson(true);
                        }
                        break;
                    case "updateEvent":
                        EventsFacade.EditSingleEvent(Convert.ToInt32(id), eventid, summary, eventName, Convert.ToDateTime(eventTime), keyword, false);
                        regjson = "{\"success\":1}";
                        break;
                    case "addEvent":
                        EventsFacade.EditSingleEvent(0, eventid, summary, eventName, Convert.ToDateTime(eventTime), keyword, true);
                        regjson = "{\"success\":1}";
                        break;
                    case "updateEventClue":
                        EventsFacade.EditEventClue(Convert.ToInt32(id), eventid, Convert.ToDateTime(clueTime), title, Convert.ToInt32(docId), false);
                        regjson = "{\"success\":1}";
                        break;
                    case "addEventClue":
                        EventsFacade.EditEventClue(Convert.ToInt32(id), eventid, Convert.ToDateTime(clueTime), title, Convert.ToInt32(docId), true);
                        regjson = "{\"success\":1}";
                        break;
                    case "delEventClue":
                        EventsFacade.DelEventClue(Convert.ToInt32(id));
                        regjson = "{\"success\":1}";
                        break;
                    case "updateEventTopic":
                        EventsFacade.EditEventTopic(Convert.ToInt32(id), eventid, title, topicImage, topicContent, Convert.ToInt32(docId), false);
                        regjson = "{\"success\":1}";
                        break;
                    case "addEventTopic":
                        EventsFacade.EditEventTopic(Convert.ToInt32(id), eventid, title, topicImage, topicContent, Convert.ToInt32(docId), true);
                        regjson = "{\"success\":1}";
                        break;
                    case "delEventTopic":
                        EventsFacade.DelEventTopicByID(Convert.ToInt32(id));
                        regjson = "{\"success\":1}";
                        break;
                    case "updateEventImg":
                        EventsFacade.EditEventImg(Convert.ToInt32(id), eventid, imgPath, imgUrl, imgAlt, Convert.ToInt32(imgType), false);
                        regjson = "{\"success\":1}";
                        break;
                    case "addEventImg":
                        EventsFacade.EditEventImg(Convert.ToInt32(id), eventid, imgPath, imgUrl, imgAlt, Convert.ToInt32(imgType), true);
                        regjson = "{\"success\":1}";
                        break;
                    case "delEventImg":
                        EventsFacade.DelEventImgByID(Convert.ToInt32(id));
                        regjson = "{\"success\":1}";
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
            return true;
        }
    }

}