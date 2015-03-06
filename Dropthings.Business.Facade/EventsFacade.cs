using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dropthings.Data.SqlServerEntity;
using System.Data;

namespace Dropthings.Business.Facade
{
    public static class EventsFacade
    {
        #region field
        private static readonly EventsEntity.EventsDAO eventsdao = new EventsEntity.EventsDAO();
        private static readonly EventClueEntity.EventClueDAO eventcluedao = new EventClueEntity.EventClueDAO();
        private static readonly EventImgEntity.EventImgDAO eventimgdao = new EventImgEntity.EventImgDAO();
        private static readonly EventTopicEntity.EventTopicDAO eventtopicdao = new EventTopicEntity.EventTopicDAO();
        #endregion

        #region Events
        public static EventsEntity GetSingleEventByID(int id)
        {
            return eventsdao.FindById(id);
        }
        public static void EditSingleEvent(int id, int eventId, String summary, String eventName, DateTime eventTime, String keyWord, Boolean isNew)
        {
            if (isNew)
            {
                eventsdao.Add(new EventsEntity() { EventId = eventId, EventName = eventName, EventTime = eventTime, Summary = summary, KeyWords = keyWord });
            }
            else
            {
                eventsdao.Update(new EventsEntity() { ID = id, EventId = eventId, EventName = eventName, EventTime = eventTime, Summary = summary, KeyWords = keyWord });
            }
        }
        public static void DelSingleEvent(int id)
        {
            eventsdao.Delete(new EventsEntity() { ID = id });
        }
        #endregion

        #region EventClue
        public static DataSet GetEventClueByEventID(int id)
        {
            return eventcluedao.GetDataSet(" EventId=" + id, null);
        }
        public static EventClueEntity GetEventClueByID(int id)
        {
            return eventcluedao.FindById(id);
        }
        public static void EditEventClue(int clueId, int eventId, DateTime clueTime, String clueTitle, int clueDocid, Boolean isNew)
        {
            if (isNew)
            {
                eventcluedao.Add(new EventClueEntity() { ClueId = clueId, ClueDocid = clueDocid, ClueTime = clueTime, ClueTitle = clueTitle, EventId = eventId });
            }
            else
            {
                eventcluedao.Update(new EventClueEntity() { ClueId = clueId, ClueDocid = clueDocid, ClueTime = clueTime, ClueTitle = clueTitle, EventId = eventId });
            }
        }
        public static void DelEventClue(int clueId)
        {
            eventcluedao.Delete(new EventClueEntity() { ClueId = clueId });
        }
        #endregion

        #region EventImg
        public static DataSet GetEventImgByEventID(int id)
        {
            return eventimgdao.GetDataSet(" EventId=" + id, null);
        }
        public static EventImgEntity GetEventImgByID(int id)
        {
            return eventimgdao.FindById(id);
        }
        public static void EditEventImg(int imgId, int eventId, String imgPath, String imgUrl, String imgAlt, int imgType, Boolean isNew)
        {
            if (isNew)
            {
                eventimgdao.Add(new EventImgEntity() { ImgId = imgId, EventId = eventId, ImgPath = imgPath, ImgUrl = imgUrl, ImgTitle = imgAlt, ImgType = imgType });
            }
            else
            {
                eventimgdao.Update(new EventImgEntity() { ImgId = imgId, EventId = eventId, ImgPath = imgPath, ImgUrl = imgUrl, ImgTitle = imgAlt, ImgType = imgType });
            }
        }
        public static void DelEventImgByID(int id)
        {
            eventimgdao.Delete(new EventImgEntity() { ImgId = id });
        }
        #endregion

        #region EventTopic
        public static DataSet GetEventTopicByEventID(int id)
        {
            return eventtopicdao.GetDataSet(" EventId=" + id, null);
        }

        public static EventTopicEntity GetEventTopicByID(int id)
        {
            return eventtopicdao.FindById(id);
        }
        public static void EditEventTopic(int topicId, int eventId, String topicTitle, String topicImage, String topicContent, int topicDocid, Boolean isNew)
        {
            if (isNew)
            {
                eventtopicdao.Add(new EventTopicEntity() { TopicId = topicId, EventId = eventId, TopicTitle = topicTitle, TopicImage = topicImage, TopicContent = topicContent, TopicDocid = topicDocid });
            }
            else
            {
                eventtopicdao.Update(new EventTopicEntity() { TopicId = topicId, EventId = eventId, TopicTitle = topicTitle, TopicImage = topicImage, TopicContent = topicContent, TopicDocid = topicDocid });
            }
        }
        public static void DelEventTopicByID(int id)
        {
            eventtopicdao.Delete(new EventTopicEntity() { TopicId = id });
        }
        #endregion

        #region orderFun(ToJson)
        public static string ToJson(this DataTable dt)
        {
            StringBuilder jsonstr = new StringBuilder();
            int count = 1;
            string[] captions = new string[dt.Columns.Count];
            for (int i = 0; i != dt.Columns.Count; i++)
            {
                captions[i] = dt.Columns[i].Caption;
            }
            jsonstr.Append("{");
            foreach (DataRow row in dt.Rows)
            {
                jsonstr.AppendFormat("\"entity_{0}\":", count);
                jsonstr.Append("{");
                for (int i = 0; i != captions.Length; i++)
                {
                    jsonstr.AppendFormat("\"{0}\":\"{1}\",", captions[i], Util.EncodeByEscape.GetEscapeStr(row[captions[i]].ToString()));
                }
                jsonstr.Length = jsonstr.Length - 1;
                jsonstr.Append("},");
                count++;
            }
            jsonstr.Append("\"success\":1}");
            return jsonstr.ToString();
        }
        public static string ToJson(this DataTable dt, bool bockbone)
        {
            StringBuilder jsonstr = new StringBuilder();
            int count = 1;
            string[] captions = new string[dt.Columns.Count];
            for (int i = 0; i != dt.Columns.Count; i++)
            {
                captions[i] = dt.Columns[i].Caption;
            }
            jsonstr.Append("[");
            foreach (DataRow row in dt.Rows)
            {
                jsonstr.Append("{");
                for (int i = 0; i != captions.Length; i++)
                {
                    jsonstr.AppendFormat("\"{0}\":\"{1}\",", captions[i], Util.EncodeByEscape.GetEscapeStr(row[captions[i]].ToString()));
                }
                jsonstr.Length = jsonstr.Length - 1;
                jsonstr.Append("},");
                count++;
            }
            jsonstr.Remove(jsonstr.Length - 1, 1);
            jsonstr.Append("]");
            //jsonstr.Append("{\"success\":1}]");
            return jsonstr.ToString();
        }
        #endregion
    }
}
