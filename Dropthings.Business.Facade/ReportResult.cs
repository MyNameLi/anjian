using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Dropthings.Data;
using IdolACINet;
using System.Data;
using System.Configuration;
using Dropthings.Util;

namespace Dropthings.Business.Facade
{
    public static class ReportResult
    {
        private static Connection conn = new Connection(ConfigurationManager.AppSettings["IdolHttp"], 9000);
        private static IdolNewsEntity.IdolNewsDao newsDao = new IdolNewsEntity.IdolNewsDao();

        /// <summary>
        /// 获取文章总数
        /// </summary>
        /// <param name="mindate">最小时间</param>
        /// <param name="maxdate">最大时间</param>
        /// <returns></returns>
        public static string GetTotalResult(string mindate, string maxdate)
        {
            try
            {
                QueryCommand query = new QueryCommand();
                query.Text = "*";
                query.SetParam(QueryParams.TotalResults, QueryParamValue.True);
                query.SetParam(QueryParams.DatabaseMatch, ConfigurationManager.AppSettings["DATABASE"]);
                query.SetParam(QueryParams.MinDate, mindate);
                query.SetParam(QueryParams.MaxDate, maxdate);
                query.SetParam("Predict", QueryParamValue.False);
                XmlDocument xmldoc = conn.Execute(query).Data;
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
                nsmgr.AddNamespace("autn", "http://schemas.autonomy.com/aci/");
                return xmldoc.SelectSingleNode("/autnresponse/responsedata/autn:totalhits", nsmgr).InnerText;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GetTotalResult(string mindate, string maxdate, string fieldtext, string fieldstr)
        {
            try
            {
                QueryCommand query = new QueryCommand();
                query.Text = "*";
                query.SetParam(QueryParams.TotalResults, QueryParamValue.True);
                query.SetParam(QueryParams.DatabaseMatch, ConfigurationManager.AppSettings["DATABASE"]);
                query.SetParam(QueryParams.MinDate, mindate);
                query.SetParam(QueryParams.MaxDate, maxdate);
                query.SetParam(QueryParams.FieldText, "STRINGALL{" + fieldtext + "}:" + fieldstr);
                query.SetParam("Predict", QueryParamValue.False);
                XmlDocument xmldoc = conn.Execute(query).Data;
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
                nsmgr.AddNamespace("autn", "http://schemas.autonomy.com/aci/");
                return xmldoc.SelectSingleNode("/autnresponse/responsedata/autn:totalhits", nsmgr).InnerText;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GetPicBase64String(string filepath) {
            return FileManage.GetBase64Str(filepath);
        }

        public static IList<IdolNewsEntity> GetNewsList(string urllist)
        {
            IList<IdolNewsEntity> list = new List<IdolNewsEntity>();
            string[] urlList = urllist.Split('+');
            StringBuilder AllUrlList = new StringBuilder();
            for (int i = 0, j = urlList.Length; i < j; i++)
            {
                if (AllUrlList.Length > 0)
                {
                    AllUrlList.Append("+");
                }
                var url = System.Web.HttpUtility.UrlEncode(EncodeByEscape.GetUnEscapeStr(urlList[i]), Encoding.UTF8);
                AllUrlList.Append(url);
            }
            QueryParamEntity param = new QueryParamEntity();
            param.Text = "*";
            param.Summary = "Context";
            param.Characters = 600;
            param.MatchReference = AllUrlList.ToString();
            param.Sort = "Date";
            param.Combine = "DREREFERENCE";
            param.PageSize = urlList.Length;
            IdolQuery query = IdolQueryFactory.GetDisStyle("query");
            query.queryParamsEntity = param;
            list = query.GetResultList();
            return list;
        }
        
        public static IList<IdolNewsEntity> GetCategotyList(Dictionary<string, string> dict, string ReportTitle, Dictionary<string, string> newsdict)
        { 
            IList<IdolNewsEntity> list = new List<IdolNewsEntity>();
            foreach (string key in dict.Keys)
            {
                list.Add(GetCategoryEntity(key, dict[key], ReportTitle, newsdict[key]));
            }
            return list;
        }

        private static IdolNewsEntity GetCategoryEntity(string CategoryId, string CategoryName, string ReportTitle, string href)
        {
            IdolNewsEntity entity = new IdolNewsEntity();
            try
            {
                entity.Href = href;
                entity.Title = CategoryName;
                entity.ClusterId = CategoryId;  
            }
            catch (Exception ex)
            {
                
            }
            return entity;
        }
    }
    
}
