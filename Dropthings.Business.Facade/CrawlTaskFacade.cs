using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dropthings.Data;
using Dropthings.Util;
using System.Web;

namespace Dropthings.Business.Facade
{
    public class CrawlTaskFacade
    {

        private static TASKEntity.TASKDAO taskDao = new TASKEntity.TASKDAO();
        private static URLRULEEntity.URLRULEDAO urlDao = new URLRULEEntity.URLRULEDAO();
        private static CONTENTRULEEntity.CONTENTRULEDAO contentDao = new CONTENTRULEEntity.CONTENTRULEDAO();
        /// <summary>
        /// return taskentity
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>

        public static TASKEntity GetTaskEntity(HttpContext context)
        {
            TASKEntity entity = new TASKEntity();
            string taskName = context.Request.Form["task_name"];
            string urlEntry = context.Request.Form["task_entryurl"];
            string taskType = context.Request.Form["task_type"];
            string siteCode = context.Request.Form["site_code"];
            string spiderDegree = context.Request.Form["spider_degree"];
            string isAgent = context.Request.Form["is_agent"];
            string agentServerIp = context.Request.Form["agent_server"];
            string agentServerPort = context.Request.Form["agent_port"];
            string agentServerUser = context.Request.Form["agent_user"];
            string agentServerPwd = context.Request.Form["agent_password"];
            string isLogin = context.Request.Form["is_login"];
            string loginSite = context.Request.Form["login_site"];
            string loginData = context.Request.Form["login_data"];
            string isUpdate = context.Request.Form["is_check"];
            string updateTimeSpan = context.Request.Form["update_timespan"];
            string pageUrlReg = context.Request.Form["page_url_rule"];
            string pageContentReg = context.Request.Form["content_url_rule"];
            string taskDes = context.Request.Form["task_des"];
            string urlPrefix = context.Request.Form["url_prefix"];            
            if (!string.IsNullOrEmpty(taskName))
            {
                entity.TASKNAME = EncodeByEscape.GetUnEscapeStr(taskName);
            }
            if (!string.IsNullOrEmpty(urlEntry))
            {
                entity.URLENTRY = EncodeByEscape.GetUnEscapeStr(urlEntry);
            }
            if (!string.IsNullOrEmpty(taskType)) {
                entity.TASKTYPE = EncodeByEscape.GetUnEscapeStr(taskType);
            }
            if (!string.IsNullOrEmpty(siteCode))
            {
                entity.SITECODE = EncodeByEscape.GetUnEscapeStr(siteCode);
            }
            if (!string.IsNullOrEmpty(spiderDegree))
            {
                entity.SPIDERDEGREE = Convert.ToInt32(spiderDegree);
            }
            if (!string.IsNullOrEmpty(taskDes))
            {
                entity.TASKDES = EncodeByEscape.GetUnEscapeStr(taskDes);
            }
            if (!string.IsNullOrEmpty(urlPrefix))
            {
                entity.URLPREFIX = EncodeByEscape.GetUnEscapeStr(urlPrefix);
            }
            if (isAgent == "1")
            {
                entity.ISAGENT = 1;
                if (!string.IsNullOrEmpty(agentServerIp))
                {
                    entity.AGENTSERVERIP = EncodeByEscape.GetUnEscapeStr(agentServerIp);
                }
                if (!string.IsNullOrEmpty(agentServerPort))
                {
                    entity.AGENTSERVERPORT = Convert.ToInt32(agentServerPort);
                }
                if (!string.IsNullOrEmpty(agentServerPwd))
                {
                    entity.AGENTSERVERPWD = EncodeByEscape.GetUnEscapeStr(agentServerPwd);
                }
                if (!string.IsNullOrEmpty(agentServerUser))
                {
                    entity.AGENTSERVERUSER = EncodeByEscape.GetUnEscapeStr(agentServerUser);
                }
            }
            else
            {
                entity.ISAGENT = 0;
            }

            if (isLogin == "1")
            {
                entity.ISLOGIN = 1;
                if (!string.IsNullOrEmpty(loginSite))
                {
                    entity.LOGINSITE = EncodeByEscape.GetUnEscapeStr(loginSite);
                }
                if (!string.IsNullOrEmpty(loginData))
                {
                    entity.LOGINDATA = EncodeByEscape.GetUnEscapeStr(loginData);
                }
            }
            else
            {
                entity.ISLOGIN = 0;
            }

            if (isUpdate == "1")
            {
                entity.ISUPDATE = 1;
                if (!string.IsNullOrEmpty(updateTimeSpan))
                {
                    entity.UPDATETIMESPAN = Convert.ToInt32(updateTimeSpan);

                }
            }
            else
            {
                entity.ISUPDATE = 0;
            }
            if (!string.IsNullOrEmpty(pageUrlReg))
            {
                entity.PAGEURLREG = EncodeByEscape.GetUnEscapeStr(pageUrlReg);
            }
            if (!string.IsNullOrEmpty(pageContentReg))
            {
                entity.PAGECONTENTREG = EncodeByEscape.GetUnEscapeStr(pageContentReg);
            }
            return entity;
        }

        /// <summary>
        /// return urlruleEntity list
        /// </summary>
        /// <param name="context"></param>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public static IList<URLRULEEntity> GetUrlRuleEntityList(HttpContext context, int taskid)
        {
            IList<URLRULEEntity> list = new List<URLRULEEntity>();
            for (int i = 0; i < 10; i++)
            {
                var key = "url_rule_list[item" + i.ToString() + "]";
                if (IsInTheParams(context, key))
                {
                    URLRULEEntity entity = new URLRULEEntity();
                    string ruleObject = context.Request.Form[key + "[rule_object]"];
                    string ruleActive = context.Request.Form[key + "[rule_active]"];
                    string keyword = context.Request.Form[key + "[rule_keyword]"];
                    if (!string.IsNullOrEmpty(ruleObject))
                    {
                        entity.RULEOBJECT = Convert.ToInt32(ruleObject);
                    }
                    if (!string.IsNullOrEmpty(ruleActive))
                    {
                        entity.RULEACTIVE = Convert.ToInt32(ruleActive);
                    }
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        entity.RULEKEYWORD = EncodeByEscape.GetUnEscapeStr(keyword);
                    }
                    entity.TASKID = taskid;
                    list.Add(entity);
                }
                else
                {
                    break;
                }
            }
            return list;
        }
        /// <summary>
        /// return contentruleEntity list
        /// </summary>
        /// <param name="context"></param>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public static IList<CONTENTRULEEntity> GetContentRuleList(HttpContext context, int taskid)
        {
            IList<CONTENTRULEEntity> list = new List<CONTENTRULEEntity>();
            for (int i = 0; i < 10; i++)
            {
                var key = "content_rule_list[item" + i.ToString() + "]";
                if (IsInTheParams(context, key))
                {
                    CONTENTRULEEntity entity = new CONTENTRULEEntity();
                    string fieldStr = context.Request.Form[key + "[field_str]"];
                    string isintervar = context.Request.Form[key + "[is_intervar]"];
                    string isdate = context.Request.Form[key + "[is_date]"];
                    string fieldaction = context.Request.Form[key + "[field_action]"];
                    string fieldexp = context.Request.Form[key + "[field_exp]"];
                    string fieldsuffix = context.Request.Form[key + "[field_suffix]"];
                    string isremovehtml = context.Request.Form[key + "[is_remove_html]"];
                    string filedsource = context.Request.Form[key + "[field_source]"];
                    string fieldParam1 = context.Request.Form[key + "[field_param1]"];
                    string fieldParam2 = context.Request.Form[key + "[field_param2]"];
                    string fieldParam3 = context.Request.Form[key + "[field_param3]"];
                    string fieldParam4 = context.Request.Form[key + "[field_param4]"];
                    if (!string.IsNullOrEmpty(fieldStr))
                    {
                        entity.FIELDSTR = EncodeByEscape.GetUnEscapeStr(fieldStr);
                    }
                    if (!string.IsNullOrEmpty(isintervar))
                    {
                        entity.ISINTERVAR = Convert.ToInt32(isintervar);
                    }
                    if (!string.IsNullOrEmpty(isdate))
                    {
                        entity.ISDATE = Convert.ToInt32(isdate);
                    }
                    if (!string.IsNullOrEmpty(fieldaction))
                    {
                        entity.FIELDTYPE = Convert.ToInt32(fieldaction);
                    }
                    if (!string.IsNullOrEmpty(fieldexp))
                    {
                        entity.FIELDREG = EncodeByEscape.GetUnEscapeStr(fieldexp);
                    }
                    if (!string.IsNullOrEmpty(fieldsuffix))
                    {
                        entity.FIELDSUFFIX = EncodeByEscape.GetUnEscapeStr(fieldsuffix);
                    }
                    if (!string.IsNullOrEmpty(isremovehtml))
                    {
                        entity.ISREMOVEHTML = Convert.ToInt32(isremovehtml);
                    }
                    if (!string.IsNullOrEmpty(filedsource))
                    {
                        entity.FIELDSOURCE = EncodeByEscape.GetUnEscapeStr(filedsource);
                    }
                    if (!string.IsNullOrEmpty(fieldParam1))
                    {
                        entity.PARAM1 = EncodeByEscape.GetUnEscapeStr(fieldParam1);
                    }
                    if (!string.IsNullOrEmpty(fieldParam2))
                    {
                        entity.PARAM2 = EncodeByEscape.GetUnEscapeStr(fieldParam2);
                    }
                    if (!string.IsNullOrEmpty(fieldParam3))
                    {
                        entity.PARAM3 = EncodeByEscape.GetUnEscapeStr(fieldParam3);
                    }
                    if (!string.IsNullOrEmpty(fieldParam4))
                    {
                        entity.PARAM4 = EncodeByEscape.GetUnEscapeStr(fieldParam4);
                    }
                    entity.TASKID = taskid;
                    list.Add(entity);
                }
                else
                {
                    break;
                }
            }
            return list;
        }

        public static void AddTask(HttpContext context,TASKEntity entity)
        {
            int taskId = taskDao.AddGetTaskId(entity);
            if (taskId > 0)
            {
                IList<URLRULEEntity> urlRuleList = GetUrlRuleEntityList(context, taskId);
                if (urlRuleList.Count > 0)
                {
                    foreach (URLRULEEntity urlEntity in urlRuleList)
                    {
                        urlDao.Add(urlEntity);
                    }
                }
                IList<CONTENTRULEEntity> contentRuleList = GetContentRuleList(context, taskId);
                if (contentRuleList.Count > 0)
                {
                    foreach (CONTENTRULEEntity contentEntity in contentRuleList)
                    {
                        contentDao.Add(contentEntity);
                    }
                }
            }
        }

        public static void DeleteTask(string taskid) {           
            taskDao.Delete(taskid);
            urlDao.DeleteByTaskID(taskid);
            contentDao.DeleteByTaskID(taskid);
        }


        public static void UpDateTask(HttpContext context, string taskid) {
            TASKEntity entity = GetTaskEntity(context);

            entity.TASKID = Convert.ToInt32(taskid);           
            taskDao.Update(entity);
            urlDao.DeleteByTaskID(taskid);
            IList<URLRULEEntity> urlRuleList = GetUrlRuleEntityList(context, Convert.ToInt32(taskid));
            if (urlRuleList.Count > 0)
            {
                foreach (URLRULEEntity urlEntity in urlRuleList)
                {
                    urlDao.Add(urlEntity);
                }
            }
            contentDao.DeleteByTaskID(taskid);
            IList<CONTENTRULEEntity> contentRuleList = GetContentRuleList(context, Convert.ToInt32(taskid));
            if (contentRuleList.Count > 0)
            {
                foreach (CONTENTRULEEntity contentEntity in contentRuleList)
                {
                    contentDao.Add(contentEntity);
                }
            }
        }

        public static IList<TASKEntity> GetTaskList(string where) {
            return taskDao.Find(where);
        }

        public static TASKEntity GetTaskEntityById(string taskid) {
            TASKEntity entity = taskDao.FindById(Convert.ToInt64(taskid));
            return entity;
        }

        public static IList<URLRULEEntity> GetUrlEntityList(string taskid) {
            string strWhere = " TASKID=" + taskid;
            IList<URLRULEEntity> list = urlDao.Find(strWhere);
            return list;
        }

        public static IList<CONTENTRULEEntity> GetContentRuleEntityList(string taskid)
        {
            string strWhere = " TASKID=" + taskid;
            IList<CONTENTRULEEntity> list = contentDao.Find(strWhere);
            return list;
        }

        /// <summary>
        /// check the keyreg is in the context
        /// </summary>
        /// <param name="context"></param>
        /// <param name="keyreg"></param>
        /// <returns></returns>
        private static bool IsInTheParams(HttpContext context, string keyreg)
        {
            bool tag = false;
            foreach (string key in context.Request.Form.AllKeys)
            {
                if (key.Contains(keyreg))
                {
                    tag = true;
                    break;
                }
            }
            return tag;
        }
    }
}
