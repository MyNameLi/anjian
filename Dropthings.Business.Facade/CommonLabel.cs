using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dropthings.Data;
using System.Data;
using System.Web;
using System.Web.SessionState;

namespace Dropthings.Business.Facade
{
    public static class CommonLabel
    {
        private static ARTICLEEntity.ARTICLEDAO articleDao = new ARTICLEEntity.ARTICLEDAO();
        private static COLUMNDEFEntity.COLUMNDEFDAO columnDao = new COLUMNDEFEntity.COLUMNDEFDAO();
        private static COLUMNTEMPLATEEntity.COLUMNTEMPLATEEntityDao templateDao = new COLUMNTEMPLATEEntity.COLUMNTEMPLATEEntityDao();
        private static OracleHelper SqlHelp = new OracleHelper("SentimentConnStr");
        public static IList<ARTICLEEntity> GetArticleListByTop(int top, int type, int columnid)
        {
            IList<ARTICLEEntity> list = new List<ARTICLEEntity>();
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" ID > 0 AND ArticleAudit = 1 AND ArticleStatus = 0");
            if (type > 0)
            {
                strWhere.AppendFormat(" AND ArticleDisStyle = {0}", type);
            }
            if (columnid > 0)
            {
                strWhere.AppendFormat(" AND ColumnID = {0}", columnid);
            }


            list = articleDao.Find(top, strWhere.ToString());
            return list;
        }

        public static IList<ARTICLEEntity> GetArticleListByIDList(string idlist)
        {
            if (string.IsNullOrEmpty(idlist))
            {
                return null;
            }
            IList<ARTICLEEntity> list = new List<ARTICLEEntity>();
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" ID > 0 AND ARTICLEAUDIT = 1 AND ARTICLESTATUS = 0");
            strWhere.AppendFormat(" AND ID in ({0})", idlist);
            list = articleDao.Find(strWhere.ToString());
            return list;
        }

        public static void InnitLableApplication()
        {
            LABLEEntity.LABLEDAO dao = new LABLEEntity.LABLEDAO();
            IList<LABLEEntity> list = dao.Find("");
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (LABLEEntity entity in list)
            {
                string key = entity.LABLENAME;
                if (!dict.ContainsKey(key))
                {
                    string str = entity.LABLESTR;
                    dict.Add(key, str);
                }
            }
            if (System.Web.HttpContext.Current.Session["Lable"] == null)
            {
                System.Web.HttpContext.Current.Session.Add("Lable", dict);
            }
            else
            {
                System.Web.HttpContext.Current.Session["Lable"] = dict;
            }

        }

        public static IList<COLUMNDEFEntity> GetColumnList(int parentID, bool isDis)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" COLUMNSTATUS = 1 ");
            if (parentID != -1)
            {
                strWhere.AppendFormat("AND PARENTID = {0}", parentID);
            }
            if (isDis)
            {
                strWhere.Append(" AND ISDIS = 1");
            }
            strWhere.Append(" ORDER BY COLUMNORDER ASC");
            IList<COLUMNDEFEntity> list = new List<COLUMNDEFEntity>();
            list = columnDao.Find(strWhere.ToString());
            return list;
        }

        public static COLUMNDEFEntity GetColumnById(int ID)
        {
            COLUMNDEFEntity entity = columnDao.FindById(Convert.ToInt64(ID));
            return entity;
        }

        public static string GetTemplateContent(int columnid, int position)
        {
            string strwhere = " COLUMNID=" + columnid.ToString() + " AND TEMPLATEID=" + position.ToString();
            IList<COLUMNTEMPLATEEntity> list = templateDao.Find(strwhere);
            if (list.Count > 0)
            {
                COLUMNTEMPLATEEntity entity = list[0];
                return entity.HTMLSTR;
            }
            else
            {
                return "";
            }
        }

        public static string GetTemplateNewsList(int columnid, int position)
        {
            string strwhere = " COLUMNID=" + columnid.ToString() + " AND TEMPLATEID=" + position.ToString();
            IList<COLUMNTEMPLATEEntity> list = templateDao.Find(strwhere);
            if (list.Count > 0)
            {
                COLUMNTEMPLATEEntity entity = list[0];
                return entity.NEWSIDLIST;
            }
            else
            {
                return "";
            }
        }

        public static void LoadLable()
        {
            LABLEEntity.LABLEDAO dao = new LABLEEntity.LABLEDAO();
            IList<LABLEEntity> list = dao.Find("");
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (LABLEEntity entity in list)
            {
                string key = entity.LABLENAME;
                if (!dict.ContainsKey(key))
                {
                    string str = entity.LABLESTR;
                    dict.Add(key, str);
                }
            }
            System.Web.HttpContext.Current.Session.Add("Lable", dict);
        }

        public static string GetBaseFileUrl()
        {
            string path = System.Web.HttpContext.Current.Session["{#sitepublishpath}"] + "upload/";
            return path;
        }
        public static string GetHttpPortUrl()
        {
            if (System.Web.HttpContext.Current.Session["Lable"] == null)
            {
                LoadLable();
            }
            Dictionary<string, string> dict = (Dictionary<string, string>)System.Web.HttpContext.Current.Session["Lable"];
            string path = dict["{#rootpath}"].ToString();
            return path;
        }

        public static string ReplaceTheRootPath(string htmlStr)
        {
            if (System.Web.HttpContext.Current.Session["Lable"] == null)
            {
                LoadLable();
            }
            Dictionary<string, string> dict = (Dictionary<string, string>)System.Web.HttpContext.Current.Session["Lable"];
            string basepath = System.Web.HttpContext.Current.Session["{#sitepublishpath}"] + "upload/";
            string path = dict["{#rootpath}"].ToString();
            return htmlStr.Replace("src=\"" + basepath, "src=\"" + path + basepath);
        }
    }
}
