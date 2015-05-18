<%@ WebHandler Language="C#" Class="BriefReportData" %>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;
using System.Configuration;
using IdolACINet;
using Dropthings.Data;
using Dropthings.Business.Facade;
using Dropthings.Util;

public class BriefReportData : IHttpHandler
{
    protected REPORTLISTEntity.REPORTLISTDAO Dao = new REPORTLISTEntity.REPORTLISTDAO();

    public void ProcessRequest(HttpContext context)
    {
        string type = context.Request["type"];
        string mindate = context.Request["mindate"];
        string maxdate = context.Request["maxdate"];
        int ThemeParent = Convert.ToInt32(ConfigurationManager.AppSettings["ThemeParent"]);
        int categoryParent = Convert.ToInt32(ConfigurationManager.AppSettings["CategoryParent"]);
        switch (type)
        {
            case "innit":
                string strWhere = " PARENTCATE= " + categoryParent.ToString();
                IList<CATEGORYEntity> list = CategoryFacade.GetCategoryEntityList(strWhere);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{");
                jsonStr.AppendFormat("\"SpecialData\":{0}", getCategoryList(categoryParent, list));
                jsonStr.Append("}");
                context.Response.Write(jsonStr.ToString());
                break;
            case "clusters":
                string jobName = context.Request["job_name"];
                if (string.IsNullOrEmpty(jobName))
                {
                    jobName = ConfigurationManager.AppSettings["ReportJobName"];
                }
                IdolClusterEntity.IdolClusterEntityDao clustersdao = new IdolClusterEntity.IdolClusterEntityDao();
                IList<IdolClusterEntity> clusterslist = clustersdao.GetClusterNews(jobName, 10, 6);
                if (clusterslist.Count > 0)
                {
                    context.Response.Write(getClustersJson(clusterslist));
                }
                break;
            case "audit":
                context.Response.Write(GetAuditStr(context));
                break;
            case "geteventcategory":
                string strEventWhere = " PARENTCATE=" + ThemeParent.ToString() + " AND  EVENTDATE>=to_date('" + mindate + "','yyyy-MM-dd')"
                + "AND EVENTDATE<=to_date('" + maxdate + "','yyyy-MM-dd')";
                IList<CATEGORYEntity> eventlist = CategoryFacade.GetCategoryEntityList(strEventWhere);
                StringBuilder jsoneventStr = new StringBuilder();
                jsoneventStr.Append("{");
                jsoneventStr.AppendFormat("\"EventData\":{0}", getCategoryList(ThemeParent, eventlist));
                jsoneventStr.Append("}");
                context.Response.Write(jsoneventStr.ToString());
                break;
            default:
                break;
        }



    }

    private string GetAuditStr(HttpContext context)
    {
        StringBuilder jsonStr = new StringBuilder();
        jsonStr.Append("{");
        string idlist = context.Request["idlist"];
        string step = context.Request["step"];
        string path = context.Server.MapPath("~/Audit.config.xml");

        if (string.IsNullOrEmpty(step))
        {
            string reportID = context.Request["id"];
            if (string.IsNullOrEmpty(reportID))
            {
                string auditData = GetAuditData(path);
                jsonStr.AppendFormat("\"audit\":{0}", auditData);
            }
            else
            {
                if (Dao.UpdateSetScalar(int.Parse(reportID), "STATUS", "STATUS+1"))
                {
                    jsonStr.AppendFormat("\"success\":\"1\"");
                }
                else
                {
                    jsonStr.AppendFormat("\"success\":\"0\"");
                }
            }
        }
        else
        {
            if (SaveAuditData(path, step, idlist))
            {
                jsonStr.AppendFormat("\"success\":\"1\"");
            }
            else
            {
                jsonStr.AppendFormat("\"success\":\"0\"");
            }
        }

        jsonStr.Append("}");
        return jsonStr.ToString();
    }


    private bool SaveAuditData(string path, string step, string idlist)
    {
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            string filePath = path;// Server.MapPath("~/Audit.config.xml");
            xmlDoc.Load(filePath);
            if (xmlDoc.ChildNodes.Count > 0)
            {
                XmlNode node = xmlDoc.SelectSingleNode("//step[@name='" + step + "']/auditors");

                node.RemoveAll();
                if (!string.IsNullOrEmpty(idlist))
                {
                    string[] ids = idlist.Split(',');
                    foreach (string id in ids)
                    {
                        string[] temp = id.Split('|');
                        XmlNode child = xmlDoc.CreateElement("auditor");

                        XmlAttribute attrId = xmlDoc.CreateAttribute("id");
                        attrId.Value = temp[0];
                        child.Attributes.Append(attrId);

                        XmlAttribute attrName = xmlDoc.CreateAttribute("name");
                        attrName.Value = temp[1];
                        child.Attributes.Append(attrName);

                        node.AppendChild(child);
                    }
                }

                xmlDoc.Save(path);
                return true;
            }
        }
        catch
        {
            return false;
        }

        return false;
    }

    private string GetAuditData(string path)
    {
        XmlDocument xmlDoc = new XmlDocument();
        string filePath = path;// Server.MapPath("~/Audit.config.xml");
        xmlDoc.Load(filePath);
        StringBuilder data = new StringBuilder();
        data.Append("{");
        if (xmlDoc.ChildNodes.Count > 0)
        {
            XmlNodeList list = xmlDoc.SelectNodes("//step");
            if (list.Count > 0)
            {
                foreach (XmlNode node in list)
                {
                    string name = node.Attributes["name"].Value.ToLower().Trim();
                    data.AppendFormat("\"{0}\":{{", name);

                    XmlNodeList auditorList = node.SelectNodes("auditors/auditor");
                    foreach (XmlNode child in auditorList)
                    {
                        string cname = child.Attributes["name"].Value;
                        string cid = child.Attributes["id"].Value;
                        data.AppendFormat("\"{0}\":\"{1}\",", cid, cname);
                    }
                    data.Remove(data.Length - 1, 1);
                    data.Append("},");
                }
            }
        }
        //xmlDoc.Save(filePath);
        data.Remove(data.Length - 1, 1);

        data.Append("}");

        return data.ToString();
    }

    private string getCategoryList(int parentcate, IList<CATEGORYEntity> list)
    {
        StringBuilder jsonstr = new StringBuilder();
        jsonstr.Append("{");
        foreach (CATEGORYEntity entity in list)
        {
            if (entity.PARENTCATE.Value == parentcate)
            {
                jsonstr.AppendFormat("\"{0}\":\"{1}\",", entity.CATEGORYID, EncodeByEscape.GetEscapeStr(entity.CATEGORYNAME));
            }
        }
        jsonstr.Append("\"SuccessCode\":1}");
        return jsonstr.ToString();
    }

    private string getClustersJson(IList<IdolClusterEntity> clusterslist)
    {
        StringBuilder jsonstr = new StringBuilder();
        jsonstr.Append("{");
        foreach (IdolClusterEntity entity in clusterslist)
        {
            jsonstr.AppendFormat("\"{0}\":", EncodeByEscape.GetEscapeStr(entity.ClusterTitle));
            jsonstr.Append("{");
            IList<ClusterDocEntity> doclist = entity.DocList;
            if (doclist.Count > 0)
            {
                int count = 1;
                foreach (ClusterDocEntity docentity in doclist)
                {
                    jsonstr.AppendFormat("\"entity_{0}\":", count);
                    jsonstr.Append("{");
                    jsonstr.AppendFormat("\"href\":\"{0}\",", EncodeByEscape.GetEscapeStr(docentity.Href));
                    jsonstr.AppendFormat("\"title\":\"{0}\",", EncodeByEscape.GetEscapeStr(docentity.Title));
                    jsonstr.AppendFormat("\"timestr\":\"{0}\",", EncodeByEscape.GetEscapeStr(docentity.TimeStr));
                    jsonstr.AppendFormat("\"sitename\":\"{0}\",", EncodeByEscape.GetEscapeStr(docentity.SiteName));
                    jsonstr.AppendFormat("\"content\":\"{0}\"", EncodeByEscape.GetEscapeStr(docentity.Content));
                    jsonstr.Append("},");
                    count++;
                }
            }
            jsonstr.Append("\"SuccessCode\":1},");
        }
        jsonstr.Append("\"SuccessCode\":1}");
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