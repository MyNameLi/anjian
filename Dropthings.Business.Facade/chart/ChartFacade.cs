using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dropthings.Data;
using IdolACINet;
using System.Configuration;
using Dropthings.Util;

namespace Dropthings.Business.Facade
{
	public static class ChartFacade
	{
        private static readonly Connection idolconn = new Connection(ConfigurationManager.AppSettings["IdolHttp"], 9000);private static readonly string FilterKeyWords = ConfigurationManager.AppSettings["FilterKeyWords"].ToString();        
        private static readonly IdolNewsEntity.IdolNewsDao newsdao = new IdolNewsEntity.IdolNewsDao();
        public static string GetTrendChart(string categoryid, string categoryname, int type)
        {
            DataTable dt = CategoryFacade.GetTrendDt(Convert.ToInt64(categoryid));
            Dictionary<string, int> datadict = new Dictionary<string, int>();
            foreach (DataRow row in dt.Rows)
            {
                string key = Convert.ToDateTime(row["DATE"]).ToString("yyyy-MM-dd");
                int count = Convert.ToInt32(row["ARTICLECOUNT"]);
                if (!datadict.ContainsKey(key))
                {
                    datadict.Add(key, count);
                }
            }
            string filename = BrokenChart.DrawChart(categoryname + "趋势图", "文章数/篇", "时间", 664, 200, datadict);
            return GetPicStr(filename, type);
        }

        public static string GetSiteStaticChart(string pictitle, string mindate, string maxdate, string fieldname, string fieldtext, int top, bool tag, int type,int colortype)
        {
            Dictionary<string, int> datadict = new Dictionary<string, int>();
            Dictionary<string, string> dict = IdolStaticFacade.GetSiteStaticByTop(top, fieldname, fieldtext, mindate, maxdate, tag);
            foreach (string key in dict.Keys)
            {
                int count = Convert.ToInt32(dict[key]);
                datadict.Add(key, count);
            }
            if (datadict.Keys.Count > 0)
            {
                string filename = IveBarchart.DrawChart(pictitle, "文章数/篇", 664, datadict,colortype);
                return GetPicStr(filename, type);
            }
            else {
                return "";
            }
        }

        public static string GetTransChart(string categoryid, string categoryname, int type)
        {
            string strWhere = " CATEGORY=" + categoryid;
            IList<TRANSROUTEEntity> list = TransRouteFacede.Find(strWhere);
            Dictionary<string, IList<string>> datadict = new Dictionary<string, IList<string>>();
            foreach (TRANSROUTEEntity entity in list)
            {
                string key = entity.FIRSTTIME.Value.ToString("yyyy-MM-dd");
                if (!datadict.ContainsKey(key))
                {
                    IList<string> sitelist = new List<string>();
                    sitelist.Add(entity.SITENAME);
                    datadict.Add(key, sitelist);
                }
                else
                {
                    if (datadict[key].Count < 4)
                    {
                        datadict[key].Add(entity.SITENAME);
                    }
                }
            }
            string filename = TransPic.DrawTransPic(categoryname + "传播链", datadict);
            return GetPicStr(filename, type);
        }

        public static string GetCategoryStaticChart(string mindate, string maxdate, string pictitle, string subtitle, string ParentCate, int type)
        {
            Dictionary<string, IList<int>> datadict = new Dictionary<string, IList<int>>();
            string strWhere = " PARENTCATE=" + ParentCate;
            IList<CATEGORYEntity> list = CategoryFacade.GetCategoryEntityList(strWhere);
            foreach (CATEGORYEntity entity in list)
            {
                IList<int> docnum = new List<int>();
                string querytype = entity.QUERYTYPE;
                if (querytype == "categoryquery")
                {
                    IdolQuery categoryquery = IdolQueryFactory.GetDisStyle("categoryquery");
                    QueryParamEntity param = new QueryParamEntity();
                    param.Category = entity.CATEGORYID.Value.ToString();
                    param.QueryText = CommonHelp.GetFilterKeyWords();
                    param.Print = "NoResults";
                    param.TotalResults = true;
                    param.MinDate = mindate;
                    param.MaxDate = maxdate;
                    string minscore = entity.MINSCORE;
                    if (!string.IsNullOrEmpty(minscore))
                    {
                        param.MinScore = minscore;
                    }
                    else
                    {
                        param.MinScore = "30";
                    }
                    param.FieldText = "MATCH{www}:C1+OR+MATCH{news}:C1";
                    categoryquery.queryParamsEntity = param;
                    int newscount = Convert.ToInt32(categoryquery.GetTotalCount());
                    docnum.Add(newscount);
                    param.FieldText = "MATCH{blog}:C1";
                    categoryquery.queryParamsEntity = param;
                    int blogcount = Convert.ToInt32(categoryquery.GetTotalCount());
                    docnum.Add(blogcount);
                    param.FieldText = "MATCH{bbs}:C1";
                    categoryquery.queryParamsEntity = param;
                    int bbscount = Convert.ToInt32(categoryquery.GetTotalCount());
                    docnum.Add(bbscount);
                    datadict.Add(entity.CATEGORYNAME, docnum);
                }
                else {
                    IdolQuery query = IdolQueryFactory.GetDisStyle("categoryquery");
                    QueryParamEntity queryparam = new QueryParamEntity();
                    queryparam.Text = entity.KEYWORD;
                    queryparam.Print = "NoResults";
                    queryparam.TotalResults = true;
                    queryparam.MinDate = mindate;
                    queryparam.MaxDate = maxdate;

                    string minscore = entity.MINSCORE;
                    if (!string.IsNullOrEmpty(minscore))
                    {
                        queryparam.MinScore = minscore;
                    }
                    else
                    {
                        queryparam.MinScore = "30";
                    }
                    queryparam.FieldText = "MATCH{www}:C1+OR+MATCH{news}:C1";
                    query.queryParamsEntity = queryparam;
                    int newscount = Convert.ToInt32(query.GetTotalCount());
                    docnum.Add(newscount);
                    queryparam.FieldText = "MATCH{blog}:C1";
                    query.queryParamsEntity = queryparam;
                    int blogcount = Convert.ToInt32(query.GetTotalCount());
                    docnum.Add(blogcount);
                    queryparam.FieldText = "MATCH{bbs}:C1";
                    query.queryParamsEntity = queryparam;
                    int bbscount = Convert.ToInt32(query.GetTotalCount());
                    docnum.Add(bbscount);
                    datadict.Add(entity.CATEGORYNAME, docnum);
                }
                
            }
            Dictionary<string,int> subdict = new Dictionary<string,int>();
            subdict.Add("新闻",-1);
            subdict.Add("博客",0);
            subdict.Add("论坛",1);
            string filename = BarChart.DrawChart(pictitle, subtitle, 664, 300, datadict, subdict);
            return GetPicStr(filename, type);
        }

        private static string GetPicStr(string filename,int type) {            
            switch (type) { 
                case 1:
                    string filePath = CommonLabel.GetHttpPortUrl() + "/Admin/dataImg/" + filename;
                    return filePath;
                case 2:
                    string OutPutPath = "~/Admin/dataImg/";            
                    string strAbsolutePath = System.Web.HttpContext.Current.Server.MapPath(OutPutPath) + filename;
                    string base64Str = FileManage.GetBase64Str(strAbsolutePath);
                    return base64Str;
                default:
                    return "";
                
            }
        }
        
	}
}
