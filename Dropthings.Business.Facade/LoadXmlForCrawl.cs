using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using Dropthings.Data;
using System.Net;
using System.Threading;
using System.Configuration;

namespace Dropthings.Business.Facade
{
    public class LoadXmlForCrawl
    {
        private static TASKEntity.TASKDAO taskDao = new TASKEntity.TASKDAO();
        private static URLRULEEntity.URLRULEDAO urlDao = new URLRULEEntity.URLRULEDAO();
        private static CONTENTRULEEntity.CONTENTRULEDAO contentDao = new CONTENTRULEEntity.CONTENTRULEDAO();
        public static void LoadXml(string id){
            SetTaskXmlStr(id);            
        }

        private static void SetTaskXmlStr(string id) {
            
            string strwhere = "";
            
            if (!string.IsNullOrEmpty(id)) {
                strwhere = " TASKID = " + id.ToString();
            }
            
            IList<TASKEntity> list = taskDao.Find(strwhere);

            if (list.Count > 0) {
                foreach (TASKEntity entity in list)
                {
                    StringBuilder xmlstr = new StringBuilder();
                    xmlstr.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    xmlstr.Append("<taskrule>");
                    xmlstr.AppendFormat("<taskname><![CDATA[{0}]]></taskname>", entity.TASKNAME);
                    xmlstr.AppendFormat("<urlentry><![CDATA[{0}]]></urlentry>", entity.URLENTRY);
                    xmlstr.AppendFormat("<sitecode><![CDATA[{0}]]></sitecode>", entity.SITECODE);
                    xmlstr.AppendFormat("<spiderdegree><![CDATA[{0}]]></spiderdegree>", entity.SPIDERDEGREE);
                    xmlstr.AppendFormat("<pageurlreg><![CDATA[{0}]]></pageurlreg>", entity.PAGEURLREG);
                    xmlstr.AppendFormat("<pagecontenturlreg><![CDATA[{0}]]></pagecontenturlreg>", entity.PAGECONTENTREG);
                    if (entity.ISAGENT.Value == 1) {
                        xmlstr.Append("<agentrule>");
                        xmlstr.AppendFormat("<agentserverid><![CDATA[{0}]]></agentserverid>", entity.AGENTSERVERIP);
                        xmlstr.AppendFormat("<agentserverport><![CDATA[{0}]]></agentserverport>", entity.AGENTSERVERPORT);
                        xmlstr.AppendFormat("<agentserveruser><![CDATA[{0}]]></agentserveruser>", entity.AGENTSERVERUSER);
                        xmlstr.AppendFormat("<agentserverupwd><![CDATA[{0}]]></agentserverupwd>", entity.AGENTSERVERPWD);
                        xmlstr.Append("</agentrule>");
                    }
                    if (entity.ISLOGIN.Value == 1) {
                        xmlstr.Append("<login>");
                        xmlstr.AppendFormat("<loginsite><![CDATA[{0}]]></loginsite>", entity.LOGINSITE);
                        xmlstr.AppendFormat("<logindata><![CDATA[{0}]]></logindata>", entity.LOGINDATA);                        
                        xmlstr.Append("</login>");
                    }
                    if (entity.ISUPDATE.Value == 1) {
                        xmlstr.Append("<update>");
                        xmlstr.AppendFormat("<updatetimespan><![CDATA[{0}]]></updatetimespan>", entity.UPDATETIMESPAN);                        
                        xmlstr.Append("</update>");
                    }
                    string urlxmlstr = GetUrlRuleStr(entity.TASKID.ToString());
                    if (urlxmlstr != null) {
                        xmlstr.Append(urlxmlstr);
                    }
                    string contentxmlstr = GetContentRuleStr(entity.TASKID.ToString());
                    if (contentxmlstr != null) {
                        xmlstr.Append(contentxmlstr);
                    }
                    xmlstr.Append("</taskrule>");
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.LoadXml(xmlstr.ToString());
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/crawlxml");
                    path = path + "/task_" + entity.TASKID.ToString() + ".xml";
                    string action = "add";
                    if (File.Exists(path))
                    {
                        action = "update";
                    }
                    xmldoc.Save(path);
                    //InformTheMsg(entity.TASKID.Value, action);
                    Thread.Sleep(500);
                }                
            }  
        }
        public static void InformTheMsg(int taskid, string action)
        {            
            string url = ConfigurationManager.AppSettings["TaskPort"].ToString();
            url = url + "?taskid=" + taskid.ToString() + "&action=" + action;
            DoInform(url);
        }

        private static void DoInform(string url) {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Timeout = 300000;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string html = reader.ReadToEnd();            
        }

        private static string GetUrlRuleStr(string taskid) {
            StringBuilder xmlstr = new StringBuilder();
            string strWhere = " TASKID=" + taskid;
            IList<URLRULEEntity> list = urlDao.Find(strWhere);
            if (list.Count > 0) {
                xmlstr.Append("<urlrulelist>");
                foreach (URLRULEEntity entity in list)
                {
                    xmlstr.Append("<urlrule>");
                    xmlstr.AppendFormat("<ruleobject><![CDATA[{0}]]></ruleobject>", entity.RULEOBJECT);
                    xmlstr.AppendFormat("<ruleactive><![CDATA[{0}]]></ruleactive>", entity.RULEACTIVE);
                    xmlstr.AppendFormat("<rulereg><![CDATA[{0}]]></rulereg>", entity.RULEKEYWORD);
                    xmlstr.Append("</urlrule>");
                }
                xmlstr.Append("</urlrulelist>");
            }
            if (xmlstr.Length > 0)
            {
                return xmlstr.ToString();
            }
            else {
                return null;
            }
        }
        

        private static string GetContentRuleStr(string taskid) {
            StringBuilder xmlstr = new StringBuilder();
            string strWhere = " TASKID=" + taskid;
            IList<CONTENTRULEEntity> list = contentDao.Find(strWhere);
            if (list.Count > 0)
            {
                xmlstr.Append("<fieldrulelist>");
                foreach (CONTENTRULEEntity entity in list)
                {
                    xmlstr.Append("<fieldrule>");
                    xmlstr.AppendFormat("<fieldname><![CDATA[{0}]]></fieldname>", entity.FIELDSTR);
                    xmlstr.AppendFormat("<fieldtype><![CDATA[{0}]]></fieldtype>", entity.FIELDTYPE);
                    xmlstr.AppendFormat("<fieldreg><![CDATA[{0}]]></fieldreg>", entity.FIELDREG);
                    xmlstr.Append("</fieldrule>");
                }
                xmlstr.Append("</fieldrulelist>");
            }
            if (xmlstr.Length > 0)
            {
                return xmlstr.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
