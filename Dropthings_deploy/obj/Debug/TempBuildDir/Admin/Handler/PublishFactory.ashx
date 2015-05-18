<%@ WebHandler Language="C#"  Class="PublishFactory" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using System.Text;
using System.Data;
using System.Net;
using System.IO;
using System.Threading;
using Dropthings.Data;
using Dropthings.Util;
using Dropthings.Business.Facade;

public class PublishFactory : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        int ColumnID = Convert.ToInt32(context.Request.Form["id"]);
        string Telemplatepath = context.Request.Form["Telemplatepath"];
        Dictionary<string, string> paramsdict = GetTemplateInfo(ColumnID);
        int type = Convert.ToInt32(paramsdict["type"]);
        string pagesize = paramsdict["pagesize"];
        string posturl = GetPostUrl();
        string res = string.Empty;        
        switch (type)
        {
            case 3:
                posturl = posturl + "ColumnPublish.ashx";
                string postData = "id=" + ColumnID.ToString() + "&Telemplatepath=" + Telemplatepath;
                res = DoPublish(posturl, postData);
                break;
            case 6:
                int postpagesize = 0;
                int.TryParse(pagesize, out postpagesize);
                if (postpagesize > 0)
                {
                    string strwhere = " ARTICLERELESE=1 AND ARTICLESTATUS=0 AND COLUMNID=" + ColumnID.ToString();
                    int totalcount = GetNewsCount(strwhere);
                    int pageCount = totalcount % postpagesize == 0 ? totalcount / postpagesize : totalcount / postpagesize + 1;
                    posturl = posturl + "ListPublish.ashx";
                    if (pageCount > 0)
                    {
                        for (int i = 1; i <= pageCount; i++)
                        {
                            string postlistData = "id=" + ColumnID.ToString() + "&Telemplatepath=" + Telemplatepath;
                            postlistData = postlistData + "&start=" + i.ToString() + "&pagesize=" + postpagesize.ToString()
                                + "&strwhere=" + strwhere + "&pagecount=" + pageCount.ToString();
                            res = DoPublish(posturl, postlistData);
                        }
                    }
                    else
                    {
                        string postlistData = "id=" + ColumnID.ToString() + "&Telemplatepath=" + Telemplatepath;
                        postlistData = postlistData + "&start=1&pagesize=2&strwhere="
                            + strwhere + "&pagecount=" + pageCount.ToString();
                        res = DoPublish(posturl, postlistData);
                    }
                }
                else {
                    res = "{\"ErrorCode\":\"1\",\"Reason\":\"每页的数量不能为零\"}";
                }
                break;
            default:
                break;
        }
        context.Response.Write(res);
    }

    private int GetNewsCount(string strwhere) {
        int count = ArticleFacade.GetPagerCount(strwhere);
        return count;
    }
    
    private Dictionary<string,string> GetTemplateInfo(int columnid)
    {
        OracleHelper helper = new OracleHelper("SentimentConnStr");
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.Append("SELECT * FROM COLUMNDEF a");
        sqlstr.Append(" inner join TEMPLATE b ");
        sqlstr.AppendFormat("on a.COLUMNTELEMPLATEPATH = b.TEMPLATEPATH and a.ID ={0}", columnid);
        DataSet ds = helper.ExecuteDateSet(sqlstr.ToString(), null);
        Dictionary<string, string> dict = new Dictionary<string, string>();
        if (ds != null)
        {
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                dict.Add("type", dt.Rows[0]["TEMPLATETYPE"].ToString());
                dict.Add("pagesize", dt.Rows[0]["TEMPLATEPARAM"].ToString());
            }
        }
        return dict;
    }

    private static string DoPublish(string url, string postData)
    {
        ASCIIEncoding encoding=new ASCIIEncoding();       
        byte[] data = encoding.GetBytes(postData);   
        HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = data.Length;
        Stream newStream = request.GetRequestStream();
        // Send the data.  
        newStream.Write(data, 0, data.Length);
        newStream.Close();   
        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
        Stream stream = response.GetResponseStream();
        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
        string html = reader.ReadToEnd();
        Thread.Sleep(500);
        return html;
    }

    private string GetPostUrl() {
        string posturl = CommonLabel.GetHttpPortUrl() + "/Admin/Handler/";
        return posturl;
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}