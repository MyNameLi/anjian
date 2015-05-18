<%@ WebHandler Language="C#" Class="WeiboTopicHandler" %>

using System;
using System.Web;
using Dropthings.Data;
using System.Data;
using System.Collections.Generic;
using Dropthings.Data.SqlServerEntity;
using System.Web.Script.Serialization;
using System.Text;
using Dropthings.Util;
using WeiboDataSource;
public class WeiboTopicHandler : IHttpHandler {
    private WeiboDataService wds = null;
    public void ProcessRequest (HttpContext context) {
        string act = context.Request["act"];
        string retJson = string.Empty;
        if (!string.IsNullOrEmpty(act))
        {
           
            try
            {
                SqlHelper sqlh = new SqlHelper("WeiboDBStr");
                wds = new WeiboDataService();
                switch (act)
                {
                    case "hotComment":
                        //string inittopicsql = "select * from Topic where IsStop=0 order by CommentCount desc";
                       // DataSet ds = sqlh.ExecuteDateSet(inittopicsql, null);
                        
                        //if (ds != null && ds.Tables.Count > 0)
                        //{
                        //    retJson = Dropthings.Business.Facade.TopicFacade.weibotopicjson(ds.Tables[0]);
                        //}
                        retJson = wds.GetHotTopic("1");
                        break;
                    case "hotForward":
                        //string inittopicsql1 = "select * from Topic where IsStop=0 order by ForwardCount desc";
                        //DataSet ds1 = sqlh.ExecuteDateSet(inittopicsql1, null);
                        //if (ds1 != null && ds1.Tables.Count > 0)
                        //{
                        //    retJson = Dropthings.Business.Facade.TopicFacade.weibotopicjson(ds1.Tables[0]);
                        //}
                        retJson = wds.GetHotTopic("2");
                        break;
                    case "initCategoryList":
                        //string initCategorySql = "select * from CustomCategory where ParentCate=1";
                        //DataSet cates = sqlh.ExecuteDateSet(initCategorySql, null);
                        //if (cates != null && cates.Tables.Count > 0)
                        //{
                        //    retJson = GetCategoryList(cates.Tables[0]);
                        //}
                        retJson = wds.InitCategory();
                        break;
                    case "addCategory"://addCategory","stype":AddState,"cateName":IndstName,"cateKeyword":IndstKeyword,"sid":categoryId
                        string stype = context.Request["stype"];
                        string cateName = context.Request["cateName"];
                        string catekeyword = context.Request["cateKeyword"];
                        string sid = context.Request["sid"];
                        string minscore = context.Request["minscore"];
                        string ModifyCategorySQL = string.Empty;
                        //if (!string.IsNullOrEmpty(stype) && !string.IsNullOrEmpty(sid))
                        //{
                        //    ModifyCategorySQL = string.Format("update customCategory set CategoryName='{0}',Keywords='{1}',MinScore='{2}' where ID='{3}'", cateName.Replace("'", ""),  catekeyword.Replace("'", ""), minscore.Replace("'", ""),sid);
                        //}
                        //else
                        //{
                        //    ModifyCategorySQL = string.Format("insert into customCategory values('{0}','{1}','{2}','{3}','{4}','{5}')", cateName.Replace("'", ""), "1", "0", catekeyword.Replace("'", ""), minscore.Replace("'", ""), "1");
                        //}
                        //sqlh.ExecuteSql(ModifyCategorySQL);
                        if (wds.CategoryUpdate(stype, cateName, catekeyword, sid, minscore))
                        {
                            retJson = "{\"Success\":1}";
                        }
                        else
                        {
                            retJson = "{\"Success\":0}";
                        }
                        break;
                    case "delCategory":
                        string cateid = context.Request["cateid"];

                        if (wds.DelCategory(cateid))
                        {
                            //string delcategorySQL = string.Format("delete customCategory where id='{0}'", cateid);
                            //sqlh.ExecuteSql(delcategorySQL, null);
                            retJson = "{\"Success\":1}";
                        }
                        else
                        {
                            retJson = "{\"Success\":0}";
                        }
                        break;
                    case "initKeywordsList":
                        //string initkeywordsSql = "select * from KeyWords";
                        //DataSet keywordList = sqlh.ExecuteDateSet(initkeywordsSql, null);
                        //if (keywordList != null && keywordList.Tables.Count > 0)
                        //{
                        //    retJson = GetCategoryList(keywordList.Tables[0]);
                        //}
                        retJson = wds.InitKeywordsList();
                        break;
                    case "delKeywods":
                        string kwid = context.Request["cateid"];
                        if (wds.DelKeywords(kwid))//!string.IsNullOrEmpty(kwid))
                        {
                            //string delKeywordsSql = string.Format("delete [KeyWords] where ID='{0}'", kwid);
                            //sqlh.ExecuteSql(delKeywordsSql, null);
                            retJson = "{\"Success\":1}";
                        }
                        else
                        {
                            retJson = "{\"Success\":0}";
                        }
                        
                        break;
                    case "addKeywords":
                        string kwstype = context.Request["stype"];
                        string kwcateName = context.Request["cateName"];
                        string kwRelevance = context.Request["KWRelevance"];
                        string kwsid = context.Request["sid"];
                        //if (!string.IsNullOrEmpty(kwstype) && !string.IsNullOrEmpty(kwsid))
                        //{
                        //    ModifyCategorySQL = string.Format("update [KeyWords] set [Name]='{0}',[KWRelevance]='{1}' where ID='{2}'", kwcateName.Replace("'", ""), kwRelevance.Replace("'", ""), kwsid);
                        //}
                        //else
                        //{
                        //    ModifyCategorySQL = string.Format("insert into [KeyWords] values('{0}','{1}')", kwcateName.Replace("'", ""), kwRelevance.Replace("'", ""));
                        //}
                        //sqlh.ExecuteSql(ModifyCategorySQL);
                        if (wds.KeywordsUpdate(kwstype, kwcateName, kwRelevance, kwsid))
                        {
                            retJson = "{\"Success\":1}";
                        }
                        else
                        {
                            retJson = "{\"Success\":0}";
                        }
                        break;
                    case "FriendsGroupList":
                        string attentiontype= context.Request["attionType"];
                        
                        if (!string.IsNullOrEmpty(attentiontype))
                        {
                            //string friendsGroupListSql = string.Format("select A.id,COUNT(b.ID)  'count',a.groupname from dbo.AttentionGroup a left join dbo.Attention b on a.ID=b.GroupID where a.AttentionType='{0}' and a.MainUID = '2659126494' group by a.id,a.groupname union select 0,count(1) 'count','未分组' from dbo.Attention where AttentionType='{1}'  and groupid=0 and MainUID='2659126494'", attentiontype.Replace("'", ""), attentiontype.Replace("'", ""));
                            //DataSet groupList = sqlh.ExecuteDateSet(friendsGroupListSql, null);
                            //if (groupList != null && groupList.Tables.Count > 0)
                            //{
                            //    retJson = GetCategoryList(groupList.Tables[0]);
                            //}
                            //else
                            //{
                            //    retJson = "{\"Success\":0}";
                            //}
                            retJson = wds.GetUserGroupList(attentiontype, "2659126494");
                        }
                        else
                        {
                            retJson = "{\"Success\":0}";
                        }
                        break;
                    case "addAttentionGroup":
                        string attype = context.Request["attype"];
                        string gpName = context.Request["gpName"];
                       // retJson = 
                        if (!string.IsNullOrEmpty(attype) && !string.IsNullOrEmpty(gpName))
                        {
                          //  string insertGroupSQL = string.Format("insert into AttentionGroup values('{0}','{1}','{2}') select @@IDENTITY", "2659126494", attype, gpName.Replace("'", ""));
                            int identityId = wds.AddGroupAttention(gpName, attype, "2659126494");//sqlh.ExecuteScalar(CommandType.Text, insertGroupSQL, null).ToString();
                            if (identityId > 0)
                            {
                                retJson = "{\"ID\":\"" + identityId + "\",\"groupName\":\"" + gpName + "\",\"Success\":1}";
                            }
                            else
                            {
                                retJson = "{\"Success\":0}";
                            }
                        }
                        else
                        {
                            retJson = "{\"Success\":0}";
                        }
                        break;
                    case "GroupAttention":
                        string groupid = context.Request["groupId"];
                        string grids = context.Request["grids"];
                       //;
                       // string groupaSQL = string.Format("update Attention set GroupID='{0}' where ID in ({1}) ", groupid, grids);
                       // int sidentity = sqlh.ExecuteSql(groupaSQL, null);
                        if (wds.GroupToAttention(groupid, grids))
                        {
                            retJson = "{\"Success\":1}";
                        }
                        else
                        {
                            retJson = "{\"Success\":0}";
                        }
                        break;
                    case "updateAttentionGroup":
                      
                        string updategroupid = context.Request["groupId"];
                        string updategrouptxt = context.Request["grouptxt"];
                        if (!string.IsNullOrEmpty(updategroupid) && !string.IsNullOrEmpty(updategrouptxt))
                        {
                            wds.GroupAttentionUpdate(updategroupid, updategrouptxt);
                            //string updategroupSQL = string.Format("update AttentionGroup set GroupName='{0}' where ID='{1}'", updategrouptxt.Replace("'", ""), updategroupid);
                            //sqlh.ExecuteSql(updategroupSQL, null);
                            retJson = "{\"Success\":1}";
                        }
                        else
                        {
                            retJson = "{\"Success\":0}";
                        }
                        break;
                    case "DelAttentionGroup":
                        string delagid = context.Request["delAttentionGroupid"];
                        if (wds.DelAttentionGroup(delagid))//!string.IsNullOrEmpty(delagid))
                        {
                            //string updateAttentionGroupIdSql = string.Format("update Attention set GroupID = 0 where GroupID = {0}", Convert.ToInt32(delagid));
                            //string delAttentionGroupSql = string.Format("delete AttentionGroup where id = {0}", Convert.ToInt32(delagid));
                            //sqlh.ExecuteSql(updateAttentionGroupIdSql, null);
                            //sqlh.ExecuteSql(delAttentionGroupSql, null);
                            retJson = "{\"Success\":1}";
                        }
                        else
                        {
                            retJson = "{\"Success\":0}";
                        }
                        break;
                    case "addAttentionGroupName":
                        string addgroupname = context.Request["groupName"];
                        string addgroupAttentionType = context.Request["attionType"];
                        string addgroupmainid = context.Request["mainuid"];
                        //"groupName": groupName ,"attionType":"1"
                       
                        if (!string.IsNullOrEmpty(addgroupname) && !string.IsNullOrEmpty(addgroupAttentionType))
                        {
                            //string insertgroupSQL = string.Format("insert into AttentionGroup values('{0}','{1}','{2}')", addgroupmainid, addgroupAttentionType, addgroupname.Replace("'", ""));
                            //sqlh.ExecuteSql(insertgroupSQL, null);
                            int a = wds.AddGroupAttention(addgroupname, addgroupAttentionType, addgroupmainid);
                            if (a != 0)
                            {
                                retJson = "{\"Success\":1}";
                            }
                            else
                            {
                                retJson = "{\"Success\":0}";
                            }
                        }
                        else
                        {
                            retJson = "{\"Success\":0}";
                        }
                        break;
                    case "statisticTopic":
                        string topicid = context.Request["topicid"];
                        string statisticSQL = string.Format("select * from Topic where id='{0}'", topicid);
                        //wds.StatisticTopic(topicid);
                        //DataSet topicset = sqlh.ExecuteDateSet(statisticSQL, null);
                        //if (topicset != null && topicset.Tables.Count > 0)
                        //{
                        //    retJson = GetCategoryList(topicset.Tables[0]);
                        //}
                        //else
                        //{
                        //    retJson = "{\"Success\":0}";
                        //}
                        retJson = wds.StatisticTopic(topicid);
                        break; 
                    default:
                        break;
                }
            }
            catch
            {

                throw;
            }

        }
        else {
            retJson = "{\"Success\":0}";
        }
        context.Response.Write(retJson);
    }
    private string GetCategoryList(DataTable dt)
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
                jsonstr.AppendFormat("\"{0}\":\"{1}\",", captions[i], EncodeByEscape.GetEscapeStr(row[captions[i]].ToString()));
            }
            jsonstr.Length = jsonstr.Length - 1;
            jsonstr.Append("},");
            count++;
        }
        jsonstr.Append("\"Success\":1}");
        return jsonstr.ToString();
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}