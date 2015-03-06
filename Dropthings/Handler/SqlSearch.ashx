<%@ WebHandler Language="C#" Class="SqlSearch" %>

using System;
using System.Web;
using System.Collections.Generic;
using Dropthings.Data;
using Dropthings.Util;
using Dropthings.Business.Facade;
using System.Data;
using System.Text;
using WeiboDataSource;
public class SqlSearch : IHttpHandler
{
    private WeiboDataService wds = null;
    public void ProcessRequest(HttpContext context)
    {
        wds = new WeiboDataService();
        string action = context.Request["action"];
        int Start = Convert.ToInt32(context.Request["start"]);
        int PageSize = Convert.ToInt32(context.Request["page_size"]);
        switch (action)
        {
            case "getwordwarninglist":
                string wordwarningstrWhere = " B.WORDWARNINGID IS NOT NULL AND B.USERNAME LIKE '%admin%'";
                string wordwarningstrcountWhere = " WORDWARNINGID IS NOT NULL AND USERNAME LIKE '%admin%'";
                context.Response.Write(AlarmFacade.GetWordWarningJsonStr(wordwarningstrWhere, "", Start, PageSize, wordwarningstrcountWhere));
                break;
            case "getpageviewwarninglist":
                string pageviewwarningstrWhere = " B.WARNINGMSGID IS NOT NULL AND B.USERNAME LIKE '%admin%'";
                string pageviewwarningstrcountWhere = " WARNINGMSGID IS NOT NULL AND USERNAME LIKE '%admin%'";
                context.Response.Write(AlarmFacade.GetSiteWarningJsonStr(pageviewwarningstrWhere, "", Start, PageSize, pageviewwarningstrcountWhere));
                break;
            case "weibotopiccontent":
                string keywords = context.Request["topickeywds"];
                string topicindustry = context.Request["topicIndustry"];
                string stime = context.Request["stime"];
                string etime = context.Request["etime"];
                //string sqlwhere = "where isdel=0 ";
                //if (!string.IsNullOrEmpty(keywords))
                //{
                //    sqlwhere += "and name like '%" + keywords + "%'";
                //}
               // if (!string.IsNullOrEmpty(topicindustry)) { sqlwhere += " and industry='" + topicindustry + "'"; }
               
                //if (!string.IsNullOrEmpty(stime))
                //{
                //    string[] tampTimestr = stime.Split('/');
                //    stime = tampTimestr[2] + "-" + tampTimestr[1] + "-" + tampTimestr[0];
                    
                //    if (!string.IsNullOrEmpty(etime))
                //    {
                //        tampTimestr = etime.Split('/');
                //        etime = tampTimestr[2] + "-" + tampTimestr[1] + "-" + tampTimestr[0];
                //        sqlwhere += " and starttime between  '" + stime + "' and '" + etime + "'";
                //    }
                //    else
                //    {
                //        sqlwhere += " and starttime >'" + stime + "'";
                //    }

                //}
                
                //int totalcount = getTotalCount("Topic", sqlwhere);
                context.Response.Write(wds.GetTopicPager(keywords, topicindustry, stime, etime, Start, PageSize));
                break;
            case "friendsContent":
                string friendsGroupid = context.Request["groupid"];
               // string friendswhere = string.Format("where MainUID='2659126494' and WebSource = '1' and AttentionType='1' and IsDel='0' and GroupID='{0}'", string.IsNullOrEmpty(friendsGroupid) ? "0" : friendsGroupid);
               // int friendscount = getTotalCount("Attention", friendswhere);
                
                context.Response.Write(wds.GetUserPager(friendsGroupid, "2659126494", "1", "1", Start, PageSize));//GetCategoryList(getPager("Attention", friendswhere, "", Start, PageSize), friendscount));
                break;
            case "fellowContent":
                string fellowGroupid = context.Request["groupid"];
               //   string fellowwhere = string.Format("where MainUID='2659126494' and WebSource = '1' and AttentionType='2' and IsDel='0' and GroupID='{0}'", string.IsNullOrEmpty(fellowGroupid) ? "0" : fellowGroupid);
               // int fellowcount = getTotalCount("Attention", fellowwhere);
                context.Response.Write(wds.GetUserPager(fellowGroupid, "2659126494", "1", "2", Start, PageSize));//GetCategoryList(getPager("Attention", fellowwhere, "", Start, PageSize), fellowcount));
                break;
            default:
                break;
        }
    }

    private string GetCategoryList(DataTable dt,int totalcount)
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
        jsonstr.Append("\"Count\":" + totalcount + "}");
        return jsonstr.ToString();
    }
    private int getTotalCount(string tableName, string strwhere)
    {
        SqlHelper sqlh = new SqlHelper("WeiboDBStr");
        int count = 0;
        string searchcountsql = string.Format("select count(*) from {0} {1}", tableName, strwhere);
        count = Convert.ToInt32(sqlh.ExecuteScalar(CommandType.Text, searchcountsql, null));
        return count;
    }
    private DataTable getPager(string tableName, string strwhere, string order, int pageindex, int pagesize)
    {
        SqlHelper sqlh = new SqlHelper("WeiboDBStr");
        int snum = pageindex * pagesize - pagesize;
        int endnum = pageindex * pagesize + 1;
        string sql = string.Format("SELECT TOP {0} * FROM (SELECT ROW_NUMBER() OVER (ORDER BY ID) AS RowNumber,* FROM  {1} {2} ) _myResults WHERE RowNumber>{3} and RowNumber<{4} {5}", pagesize, tableName, strwhere, snum, endnum, order);
        DataSet ds = sqlh.ExecuteDateSet(sql, null);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}