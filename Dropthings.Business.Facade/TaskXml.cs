using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using Dropthings.Util;
using Dropthings.Data;
using System.Threading;
using System.Xml;
using System.Net;

namespace Dropthings.Business.Facade
{
	public class TaskXml
	{
        private static readonly TASKEntity.TASKDAO taskDao = new TASKEntity.TASKDAO();
        private static readonly URLRULEEntity.URLRULEDAO urlDao = new URLRULEEntity.URLRULEDAO();
        private static readonly CONTENTRULEEntity.CONTENTRULEDAO contentDao = new CONTENTRULEEntity.CONTENTRULEDAO();
        private static readonly string TaskForder = ConfigurationManager.AppSettings["TaskForder"];

        public static void Load(int taskid)
        {
            SetTaskXmlStr(taskid);
        }

        public static void delete(int taskid)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath(TaskForder);
            path = path + "/task_" + taskid.ToString() + ".xml";
            if (File.Exists(path))
            {
                FileManage.DeleteFile(path);                
            }
        }

        private static void SetTaskXmlStr(int id)
        {
            string strwhere = " TASKID = " + id.ToString(); 
            IList<TASKEntity> list = taskDao.Find(strwhere);
            if (list.Count > 0)
            {
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
                    if (entity.ISAGENT.Value == 1)
                    {
                        xmlstr.Append("<agentrule>");
                        xmlstr.AppendFormat("<agentserverid><![CDATA[{0}]]></agentserverid>", entity.AGENTSERVERIP);
                        xmlstr.AppendFormat("<agentserverport><![CDATA[{0}]]></agentserverport>", entity.AGENTSERVERPORT);
                        xmlstr.AppendFormat("<agentserveruser><![CDATA[{0}]]></agentserveruser>", entity.AGENTSERVERUSER);
                        xmlstr.AppendFormat("<agentserverupwd><![CDATA[{0}]]></agentserverupwd>", entity.AGENTSERVERPWD);
                        xmlstr.Append("</agentrule>");
                    }
                    if (entity.ISLOGIN.Value == 1)
                    {
                        xmlstr.Append("<login>");
                        xmlstr.AppendFormat("<loginsite><![CDATA[{0}]]></loginsite>", entity.LOGINSITE);
                        xmlstr.AppendFormat("<logindata><![CDATA[{0}]]></logindata>", entity.LOGINDATA);
                        xmlstr.Append("</login>");
                    }
                    if (entity.ISUPDATE.Value == 1)
                    {
                        xmlstr.Append("<update>");
                        xmlstr.AppendFormat("<updatetimespan><![CDATA[{0}]]></updatetimespan>", entity.UPDATETIMESPAN);
                        xmlstr.Append("</update>");
                    }
                    IList<CONTENTRULEEntity> contentrulelist = new List<CONTENTRULEEntity>();
                    string urlxmlstr = GetUrlRuleStr(entity.TASKID.ToString());
                    if (urlxmlstr != null)
                    {
                        xmlstr.Append(urlxmlstr);
                    }
                    string contentxmlstr = GetContentRuleStr(entity.TASKID.ToString(), ref contentrulelist);
                    if (contentxmlstr != null)
                    {
                        xmlstr.Append(contentxmlstr);
                    }
                    string filedstr = GetFieldStr(entity, contentrulelist);
                    if (filedstr != null)
                    {
                        xmlstr.Append(filedstr);
                    }
                    xmlstr.Append("</taskrule>");
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.LoadXml(xmlstr.ToString());
                    string path = System.Web.HttpContext.Current.Server.MapPath(TaskForder);
                    //string path = ConfigurationManager.AppSettings["TaskFilePath"].ToString();
                    FileManage.CreateForder(path);
                    path = path + "\\task_" + entity.TASKID.ToString() + ".xml";
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

        private static void DoInform(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Timeout = 300000;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string html = reader.ReadToEnd();
        }

        private static string GetUrlRuleStr(string taskid)
        {
            StringBuilder xmlstr = new StringBuilder();
            string strWhere = " TASKID=" + taskid;
            IList<URLRULEEntity> list = urlDao.Find(strWhere);
            if (list.Count > 0)
            {
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
            else
            {
                return null;
            }
        }


        private static string GetContentRuleStr(string taskid, ref IList<CONTENTRULEEntity> list)
        {
            StringBuilder xmlstr = new StringBuilder();
            string strWhere = " TASKID=" + taskid;
            list = contentDao.Find(strWhere);
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

        private static string GetFieldStr(TASKEntity entity, IList<CONTENTRULEEntity> contentrulelist)
        {
            StringBuilder xmlstr = new StringBuilder();
            xmlstr.Append("<passconfig charset=\"UTF-8\">");
            xmlstr.Append("<var-def name=\"htmlnode\" isfield=\"false\">");
            xmlstr.Append("<html-to-htmlnode name=\"contentnode\">");
            xmlstr.Append("<var name=\"htmlcontent\"/></html-to-htmlnode></var-def>");
            xmlstr.AppendFormat("<var-def name=\"SITENAME\" isfield=\"true\">{0}</var-def>", entity.TASKNAME);
            xmlstr.AppendFormat("<var-def name=\"SITETYPE\" isfield=\"true\">{0}</var-def>", entity.TASKTYPE);
            foreach (CONTENTRULEEntity contententity in contentrulelist)
            {                
                xmlstr.Append(contententity.GetParserConfig());
            }
            xmlstr.Append("<outputidx><path><var name=\"OUTIDX\"/></path></outputidx>");
            xmlstr.Append("</passconfig>");
            if (xmlstr.Length > 0)
            {
                return xmlstr.ToString();
            }
            else
            {
                return null;
            }
        }

        private static void LoadXPathXmlStr(CONTENTRULEEntity contententity, ref StringBuilder xmlstr)
        {
            xmlstr.AppendFormat("<var-def name=\"{0}\" ", contententity.FIELDSTR);
            if (contententity.ISINTERVAR.Value == 1)
            {
                xmlstr.Append("isfield=\"false\">");
            }
            else
            {
                xmlstr.Append("isfield=\"true\">");
            }
            if (contententity.FIELDSTR.Contains("CONTENT"))
            {
                xmlstr.AppendFormat("<htmlxpath expression=\"{0}\" outputtype=\"nodetextnohtml\"", contententity.FIELDREG);
            }
            else
            {
                xmlstr.AppendFormat("<htmlxpath expression=\"{0}\" outputtype=\"nodeinnertext\"", contententity.FIELDREG);
            }
            if (contententity.ISREMOVEHTML.Value == 1)
            {
                xmlstr.Append(" nohtml=\"true\">");
            }
            else
            {
                xmlstr.Append(">");
            }
            xmlstr.AppendFormat("<var name=\"{0}\"/></htmlxpath>", contententity.FIELDSOURCE);
            xmlstr.Append("</var-def>");
        }

        private static void LoadPSXmlStr(CONTENTRULEEntity contententity, ref StringBuilder xmlstr)
        {
            xmlstr.AppendFormat("<var-def name=\"{0}\" ", contententity.FIELDSTR);
            if (contententity.ISINTERVAR.Value == 1)
            {
                xmlstr.Append("isfield=\"false\">");
            }
            else
            {
                xmlstr.Append("isfield=\"true\">");
            }
            if (contententity.ISREMOVEHTML.Value == 1)
            {
                xmlstr.Append("<textstartend nohtml=\"true\">");
            }
            else
            {
                xmlstr.Append("<textstartend>");
            }
            xmlstr.AppendFormat("<start><![CDATA[{0}]]></start>", contententity.FIELDREG);
            xmlstr.AppendFormat("<end><![CDATA[{0}]]></end>", contententity.FIELDSUFFIX);
            xmlstr.AppendFormat("<source><var name=\"{0}\"/></source>", contententity.FIELDSOURCE);
            xmlstr.Append("</textstartend>");
            xmlstr.Append("</var-def>");
        }
	}
}
