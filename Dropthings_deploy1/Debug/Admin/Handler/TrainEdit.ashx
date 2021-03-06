﻿<%@ WebHandler Language="C#" Class="TrainEdit" %>

using System;
using System.Web;
using Dropthings.Data;
using Dropthings.Util;
using Dropthings.Business.Facade;
using System.Text;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using IdolACINet;
using System.Threading;

public class TrainEdit : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        string act = context.Request["act"];
        string trainCategoryId = context.Request["train_category_id"];
        string parentCategory = context.Request["parent_category"];
        string categoryId = context.Request["category_id"];
        string categoryName = EncodeByEscape.GetUnEscapeStr(context.Request["category_name"]);
        string catePath = context.Request["cate_path"];
        string cateTraininfo = EncodeByEscape.GetUnEscapeStr(context.Request["cate_train_info"]);
        string parentCate = context.Request["parent_cate"];
        string weightTerms = EncodeByEscape.GetUnEscapeStr(context.Request["weight_terms"]);
        string weightValues = EncodeByEscape.GetUnEscapeStr(context.Request["weight_values"]);
        string IsEffect = context.Request["is_effect"];
        string keyword = EncodeByEscape.GetUnEscapeStr(context.Request["keyword"]);
        string minscore = context.Request["minscore"];
        string eventReson = EncodeByEscape.GetUnEscapeStr(context.Request["event_reson"]);
        string eventMeasure = EncodeByEscape.GetUnEscapeStr(context.Request["event_measure"]);
        string eventAbout = EncodeByEscape.GetUnEscapeStr(context.Request["event_about"]);
        string eventMinscore = context.Request["event_minscore"];
        string queryType = context.Request["query_type"];
        string eventDate = context.Request["event_date"];
        string eventType = context.Request["event_type"];
        string imgpath = EncodeByEscape.GetUnEscapeStr(context.Request["img_path"]);
        string eventsort = context.Request["event_sort"];
        string isnew = context.Request["is_new"]; 
        string jsonStr = string.Empty;
        Command query;
        Connection conn = new Connection(ConfigurationManager.AppSettings["IdolHttp"], 9000);
        try
        {
            switch (act)
            {
                case "remove":
                    string strwhere = " PARENTCATE=" + categoryId;
                    int count = CategoryFacade.GetCount(strwhere);
                    if (count == 0)
                    {
                        query = new Command("CategoryDelete");
                        query.SetParam("category", trainCategoryId);
                        XmlDocument delXmlDoc = conn.Execute(query).Data;
                        if (delXmlDoc != null)
                        {
                            if (CategoryFacade.Delete(Convert.ToInt32(categoryId)))
                                jsonStr = "{\"act\":\"remove\",\"errorCode\":0}";
                        }
                    }
                    else
                    {
                        jsonStr = "{\"act\":\"remove\",\"errorCode\":1,\"ChildCount\":" + count.ToString() + "}";
                    }
                    break;
                case "add":
                    if (!string.IsNullOrEmpty(categoryName))
                    {
                        query = new Command("CategoryCreate");
                        string LCategoryId = getCategoryID(categoryName, parentCategory, query, conn);
                        if (!string.IsNullOrEmpty(LCategoryId))
                        {
                            query = new Command("CategorySetTraining");
                            query.SetParam("category", LCategoryId);
                            query.SetParam("language", "CHINESE");
                            query.SetParam("encoding", "UTF8");
                            if (!string.IsNullOrEmpty(weightValues))
                            {
                                query.SetParam("DocRef", GetIdolUrl(weightValues));
                            }
                            query.SetParam("Training", cateTraininfo);
                            query.SetParam("DatabaseMatch", ConfigurationManager.AppSettings["DATABASE"]);
                            query.SetParam("BuildNow", "true");
                            if (conn.Execute(query).Data != null)
                            {
                                if (isnew == "1" && parentCate == "202")
                                {
                                    CategoryFacade.UpdateCateType();
                                }
                                CATEGORYEntity Addentity = new CATEGORYEntity();
                                Addentity.CATEGORYID = Convert.ToInt64(LCategoryId);
                                Addentity.CATEGORYNAME = categoryName;
                                Addentity.DATABASEID = "0";
                                Addentity.CATEPATH = catePath;
                                Addentity.CATETRAININFO = cateTraininfo;
                                Addentity.PARENTCATE = Convert.ToInt32(parentCate);
                                Addentity.ISEFFECT = Convert.ToInt32(IsEffect);
                                Addentity.KEYWORD = keyword;
                                Addentity.MINSCORE = minscore;
                                Addentity.EVENTRESON = eventReson;
                                Addentity.EVENTMEASURE = eventMeasure;
                                Addentity.EVENTABOUT = eventAbout;
                                Addentity.EVENTMINSCORE = eventMinscore;
                                Addentity.QUERYTYPE = queryType;
                                Addentity.CATEDISPLAY = imgpath;
                                Addentity.CATETYPE = Convert.ToInt32(isnew);
                                if (!string.IsNullOrEmpty(eventDate))
                                {
                                    Addentity.EVENTDATE = Convert.ToDateTime(eventDate);
                                }
                                if (!string.IsNullOrEmpty(eventsort))
                                {
                                    Addentity.SEQUEUE = Convert.ToInt32(eventsort);
                                }
                                else
                                {
                                    Addentity.SEQUEUE = 0;
                                }
                                Addentity.EVENTTYPE = Convert.ToInt32(eventType);
                                try
                                {
                                    CategoryFacade.Add(Addentity);
                                    jsonStr = "{\"act\":\"add\",\"errorCode\":0,\"trainId\":\"" + LCategoryId + "\"}";
                                }
                                catch (Exception e)
                                {

                                }
                            }
                        }
                    }
                    break;
                case "innit_edit":
                    CATEGORYEntity entity = CategoryFacade.FindById(Convert.ToInt64(categoryId));
                    jsonStr = GetEntityJson(entity);
                    break;
                case "edit":
                    query = new Command("CategoryDeleteTraining");
                    query.SetParam("category", parentCategory);
                    query.SetParam("BuildNow", "true");
                    if (conn.Execute(query).Data != null)
                    {
                        Command TrainQuery = new Command("CategorySetTraining");
                        TrainQuery.SetParam("category", parentCategory);
                        if (!string.IsNullOrEmpty(weightValues))
                        {
                            TrainQuery.SetParam("DocRef", GetIdolUrl(weightValues));
                        }
                        TrainQuery.SetParam("Training", cateTraininfo);
                        TrainQuery.SetParam("DatabaseMatch", ConfigurationManager.AppSettings["DATABASE"]);
                        TrainQuery.SetParam("language", "CHINESE");
                        TrainQuery.SetParam("encoding", "UTF8");
                        TrainQuery.SetParam("BuildNow", "true");
                        if (conn.Execute(TrainQuery).Data != null)
                        {
                            Command EditQuery = new Command("CategorySetDetails");
                            EditQuery.SetParam("category", parentCategory);
                            EditQuery.SetParam("name", categoryName);
                            EditQuery.SetParam("Databases", ConfigurationManager.AppSettings["DATABASE"]);
                            EditQuery.SetParam("fields", "drelanguagetype,dreoutputencoding");
                            EditQuery.SetParam("values", "CHINESE,UTF8");
                            if (conn.Execute(EditQuery).Data != null)
                            {
                                if (isnew == "1" && parentCate == "202")
                                {
                                    CategoryFacade.UpdateCateType();
                                }
                                CATEGORYEntity Editentity = CategoryFacade.FindById(Convert.ToInt64(categoryId));
                                Editentity.CATEGORYNAME = categoryName;
                                Editentity.CATEPATH = catePath;
                                Editentity.CATETRAININFO = cateTraininfo;
                                Editentity.PARENTCATE = Convert.ToInt32(parentCate);
                                Editentity.ISEFFECT = Convert.ToInt32(IsEffect);
                                Editentity.KEYWORD = keyword;
                                Editentity.MINSCORE = minscore;
                                Editentity.EVENTRESON = eventReson;
                                Editentity.EVENTMEASURE = eventMeasure;
                                Editentity.EVENTABOUT = eventAbout;
                                Editentity.EVENTMINSCORE = eventMinscore;
                                Editentity.QUERYTYPE = queryType;
                                Editentity.CATEDISPLAY = imgpath;
                                Editentity.CATETYPE = Convert.ToInt32(isnew);
                                if (!string.IsNullOrEmpty(eventDate))
                                {
                                    Editentity.EVENTDATE = Convert.ToDateTime(eventDate);
                                }
                                if (!string.IsNullOrEmpty(eventsort))
                                {
                                    Editentity.SEQUEUE = Convert.ToInt32(eventsort);
                                }
                                Editentity.EVENTTYPE = Convert.ToInt32(eventType);
                                CategoryFacade.Update(Editentity);
                                jsonStr = "{\"act\":\"edit\",\"errorCode\":0}";
                            }
                        }
                    }
                    break;
                case "weight_init":
                    query = new Command("CategoryGetTNW");
                    query.SetParam("category", trainCategoryId);

                    XmlDocument xmldoc = conn.Execute(query).Data;

                    if (xmldoc != null)
                    {
                        XmlNode node = xmldoc.SelectSingleNode("autnresponse/responsedata");
                        string weight_list = node.ChildNodes[2].InnerText;
                        if (weight_list != "not modified")
                        {
                            string[] weightTermList = node.ChildNodes[2].InnerText.Split(',');
                            string[] weightValuesList = node.ChildNodes[3].InnerText.Split(',');
                            int length = weightTermList.Length;
                            StringBuilder weightJsonStr = new StringBuilder();
                            weightJsonStr.Append("{");
                            for (int i = 0; i < length; i++)
                            {
                                weightJsonStr.AppendFormat("\"{0}\":\"{1}\"", EncodeByEscape.GetEscapeStr(weightTermList[i]), weightValuesList[i]);
                                if (i < length - 1)
                                    weightJsonStr.Append(",");
                            }
                            weightJsonStr.Append("}");
                            context.Response.Write(weightJsonStr.ToString());
                        }
                    }
                    break;
                case "choose_article_init":
                    query = new Command("CategoryGetTraining");
                    query.SetParam("category", trainCategoryId);
                    query.SetParam("includecontents", QueryParamValue.False);

                    XmlDocument articleXmldoc = conn.Execute(query).Data;

                    if (articleXmldoc != null)
                    {
                        XmlNamespaceManager nsmgr = new XmlNamespaceManager(articleXmldoc.NameTable);
                        nsmgr.AddNamespace("autn", "http://schemas.autonomy.com/aci/");
                        XmlNodeList trainArticleList = articleXmldoc.SelectNodes("autnresponse/responsedata/autn:trainingdoc", nsmgr);
                        StringBuilder trainArticleJsonStr = new StringBuilder();
                        trainArticleJsonStr.Append("{");
                        int hitcount = 1;
                        foreach (XmlNode hitnode in trainArticleList)
                        {
                            trainArticleJsonStr.Append("\"" + hitcount + "\":\"" + hitnode.ChildNodes[3].InnerText + "\"");
                            if (hitcount < trainArticleList.Count)
                            {
                                trainArticleJsonStr.Append(",");
                            }
                            hitcount++;
                        }
                        trainArticleJsonStr.Append("}");
                        context.Response.Write(trainArticleJsonStr.ToString());
                    }
                    break;
                case "weight_train":
                    query = new Command("CategorySetTNW");
                    query.SetParam("category", trainCategoryId);
                    query.SetParam("Terms", weightTerms);
                    query.SetParam("Weights", weightValues);
                    query.SetParam("BuildNow", "true");
                    if (conn.Execute(query).Data != null)
                        context.Response.Write("训练成功");
                    break;
                case "article_train":
                    query = new Command("CategoryDeleteTraining");
                    query.SetParam("category", trainCategoryId);
                    query.SetParam("BuildNow", "true");

                    if (conn.Execute(query).Data != null)
                    {
                        Command TrainQuery = new Command("CategorySetTraining");
                        TrainQuery.SetParam("category", trainCategoryId);
                        TrainQuery.SetParam("language", "CHINESE");
                        TrainQuery.SetParam("encoding", "UTF8");
                        TrainQuery.SetParam("DocRef", weightValues);
                        TrainQuery.SetParam("BuildNow", "true");
                        if (conn.Execute(TrainQuery).Data != null)
                            context.Response.Write("训练成功");
                    }
                    break;
                case "term_init":
                    query = new Command("CategoryGetTNW");
                    query.SetParam("category", trainCategoryId);
                    XmlDocument termXmldoc = conn.Execute(query).Data;
                    if (termXmldoc != null)
                    {
                        XmlNode node = termXmldoc.SelectSingleNode("autnresponse/responsedata");
                        string TermList = node.ChildNodes[0].InnerText;
                        string ValuesList = node.ChildNodes[1].InnerText;
                        if (TermList != "not built")
                            context.Response.Write("{\"successCode\":1,\"TermList\":\"" + TermList + "\",\"ValuesList\":\"" + ValuesList + "\"}");
                        else
                            context.Response.Write("{\"successCode\":0}");
                    }
                    break;
                default:
                    break;
            }
            context.Response.Write(jsonStr);
        }
        catch
        {
            context.Response.Write("{\"successCode\":0}");
        }
    }

    private string getCategoryID(string name, string parentCategory, Command query, Connection conn)
    {
        string CategoryID = string.Empty;       
        query.SetParam("Category", name);
        if (!string.IsNullOrEmpty(parentCategory) && parentCategory != "0")
        {
            query.SetParam("Parent", parentCategory);
        }
        XmlDocument xmlDoc = conn.Execute(query).Data;

        XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
        nsmgr.AddNamespace("autn", "http://schemas.autonomy.com/aci/");

        XmlNode node = xmlDoc.SelectSingleNode("autnresponse/responsedata/autn:category/autn:id", nsmgr);
        if (node != null)
            CategoryID = node.InnerText;
        return CategoryID;
    }     
    public bool IsReusable {
        get {
            return false;
        }
    }

    private string GetEntityJson(CATEGORYEntity entity)
    {
        StringBuilder jsonStr = new StringBuilder();
        jsonStr.Append("{");
        jsonStr.AppendFormat("\"ID\":\"{0}\",", entity.ID);
        jsonStr.AppendFormat("\"ParentCate\":\"{0}\",", entity.PARENTCATE);
        jsonStr.AppendFormat("\"CategoryID\":\"{0}\",", entity.CATEGORYID);        
        jsonStr.AppendFormat("\"CategoryName\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.CATEGORYNAME));
        jsonStr.AppendFormat("\"CatePath\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.CATEPATH));
        jsonStr.AppendFormat("\"CateTrainInfo\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.CATETRAININFO));
        jsonStr.AppendFormat("\"QueryType\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.QUERYTYPE));
        jsonStr.AppendFormat("\"IsEffect\":\"{0}\",", entity.ISEFFECT);
        jsonStr.AppendFormat("\"Keyword\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.KEYWORD));
        jsonStr.AppendFormat("\"MinScore\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.MINSCORE));
        jsonStr.AppendFormat("\"CatePath\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.CATEPATH));
        jsonStr.AppendFormat("\"EventReson\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.EVENTRESON));
        jsonStr.AppendFormat("\"EventMeasure\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.EVENTMEASURE));
        jsonStr.AppendFormat("\"EventAbout\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.EVENTABOUT));
        jsonStr.AppendFormat("\"ImgPath\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.CATEDISPLAY));
        jsonStr.AppendFormat("\"EventSort\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.SEQUEUE.ToString()));
        jsonStr.AppendFormat("\"IsNew\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.CATETYPE.ToString()));
        jsonStr.AppendFormat("\"EventMinScore\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.EVENTMINSCORE));
        if (entity.EVENTDATE != null)
        {
            jsonStr.AppendFormat("\"EventDate\":\"{0}\",", entity.EVENTDATE.Value.ToString("yyyy-MM-dd"));
        }
        else {
            jsonStr.Append("\"EventDate\":\"\",");
        }
        jsonStr.AppendFormat("\"EventType\":\"{0}\"", entity.EVENTTYPE);
        jsonStr.Append("}");
        return jsonStr.ToString();
    }


    private string GetIdolUrl(string urllist) { 
        string[] list = urllist.Split('+');
        return UrlEncode.GetIdolQueryUrlStr(list);
    }
}