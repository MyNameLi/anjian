using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using Dropthings.Data;
using Dropthings.Util;
using System.Web;

namespace Dropthings.Business.Facade
{
    public static class ArticleFacade
    {
        private static ARTICLEEntity.ARTICLEDAO dao = new ARTICLEEntity.ARTICLEDAO();
        public static DataTable GetPagerDataTable(string strWhere,int pageSize,int start) {
            
            DataTable dt = dao.GetPager(strWhere,  null, pageSize, start);
            return dt;
        }

        public static int GetPagerCount(string strWhere)
        {
            int PagerCount = dao.GetPagerRowsCount(strWhere);
            return PagerCount;
        }

        public static IList<ARTICLEEntity> Find(string strWhere)
        {
            return dao.Find(strWhere);
        }

        public static ARTICLEEntity GetEntityById(long id) {
            ARTICLEEntity entity = dao.FindById(id);
            return entity;
        }

        private static DataTable GetArticleColumnDt(string urlStr, int parentid)
        {
            return dao.GetArticleColumnDt(urlStr, parentid);
        }

        public static void DeleteByUrl(string url,int columnid) {
            dao.Delete(url, columnid);
        }

        public static Dictionary<string, string> GetArticleColumnDict(string urlStr, int parentid)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            DataTable dt = GetArticleColumnDt(urlStr, parentid);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows) {
                    string url = row["ARTICLEEXTERNALURL"].ToString();
                    string columnname = row["COLUMNNAME"].ToString();
                    if (dict.ContainsKey(url))
                    {
                        string othercolumnname = dict[url] + "，" + columnname;
                        dict[url] = othercolumnname;
                    }
                    else {
                        dict.Add(url, columnname);
                    }
                }
            }
            return dict;
        }

        public static string GetPagerJsonStr(string strwhere, string orderby, int pagesize, int pagernumber)
        {
            DataTable dt = dao.GetPager(strwhere, orderby, pagesize, pagernumber);
            int totalcount = GetPagerCount(strwhere);
            StringBuilder jsonstr = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                jsonstr.Append("{");
                int count = 1;
                foreach (DataRow row in dt.Rows)
                {
                    jsonstr.AppendFormat("\"entity_{0}\":", count);
                    jsonstr.Append("{");
                    jsonstr.AppendFormat("\"id\":\"{0}\",", row["ID"].ToString());
                    jsonstr.AppendFormat("\"url\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["ARTICLEEXTERNALURL"].ToString()));
                    jsonstr.AppendFormat("\"articletitle\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["ArticleTitle"].ToString()));
                    String basedatestr = row["ARTICLEBASEDATE"].ToString();
                    if (!String.IsNullOrEmpty(basedatestr))
                    {
                        jsonstr.AppendFormat("\"basedate\":\"{0}\",", EncodeByEscape.GetEscapeStr(Convert.ToDateTime(basedatestr).ToString("yyyy-MM-dd")));
                    }
                    jsonstr.AppendFormat("\"articlesource\":\"{0}\"", EncodeByEscape.GetEscapeStr(row["ARTICLESOURCE"].ToString()));
                    jsonstr.Append("},");
                    count++;
                }
                jsonstr.AppendFormat("\"Count\":{0}", totalcount);
                jsonstr.Append("}");
            }
            return jsonstr.ToString();
        }

        public static string GetArticleColumnJsonStr(string urlStr,int parentid)
        {
            Dictionary<string, string> dict = GetArticleColumnDict(urlStr, parentid);
            StringBuilder jsonstr = new StringBuilder();
            if (dict.Keys.Count > 0)
            {
                jsonstr.Append("{");
                foreach (string key in dict.Keys)
                {
                    string url = EncodeByEscape.GetEscapeStr(key);
                    string columnstr = EncodeByEscape.GetEscapeStr(dict[key]);
                    jsonstr.AppendFormat("\"{0}\":\"{1}\",", url, columnstr);
                }
                jsonstr.Append("\"Success\":1}");
            }
            return jsonstr.ToString();
        }

        public static bool ExportToTxt(IList<ARTICLEEntity> list,out string path) {
            try
            {
                string filename = DateTime.Now.ToString("yyyyMMddHHssmm") + ".txt";
                string folderpath = HttpContext.Current.Server.MapPath("~/admin/reportcontent");
                string filepath = folderpath + "/" + filename;
                StringBuilder txtstr = new StringBuilder();               
                foreach(ARTICLEEntity entity in list){
                    txtstr.AppendFormat("{0} {1} {2} \n", entity.ARTICLETITLE, entity.ARTICLEBASEDATE, entity.ARTICLESOURCE);
                    Regex objRegExp = new Regex("<(.|\n)+?>");
                    string strOutput = objRegExp.Replace(entity.ARTICLECONTENT, "");
                    txtstr.AppendFormat("{0}\n", strOutput);               
                }
                FileManage.EditFile(filepath, txtstr.ToString());
                path = filename;
                return true;
            }
            catch {
                path = String.Empty;
                return false;
            }
            
        }
    }
}
