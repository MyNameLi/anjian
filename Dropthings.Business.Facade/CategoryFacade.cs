using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dropthings.Data;
using Dropthings.Util;
using Oracle.DataAccess.Client;

namespace Dropthings.Business.Facade
{
    public static class CategoryFacade
    {
        private static CATEGORYEntity.CATEGORYDAO Dao = new CATEGORYEntity.CATEGORYDAO();
        public static IList<CATEGORYEntity> GetCategoryEntityList(string strwhere)
        {

            IList<CATEGORYEntity> list = Dao.Find(strwhere);
            return list;
        }

        public static bool Delete(int categoryid)
        {
            return Dao.Delete(categoryid);
        }

        public static void Add(CATEGORYEntity entity)
        {
            Dao.Add(entity);
        }

        public static CATEGORYEntity FindById(long id)
        {
            return Dao.FindById(id);
        }
        public static IList<CATEGORYEntity> Find(int top, string where)
        {
            return Dao.Find(top, where);
        }
        public static void Update(CATEGORYEntity entity)
        {
            Dao.Update(entity);
        }

        public static void UpdateCateType()
        {
            Dao.UpdateCateType();
        }

        public static DataTable GetDtByCategoryId(long categoryid)
        {
            return Dao.GetDTByCategoryId(categoryid);
        }

        public static DataTable GetTrendDt(long categoryid)
        {
            DataSet ds = Dao.GetTrebdDataSet(categoryid);
            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        public static int GetCount(string strwhere)
        {
            return Dao.GetPagerRowsCount(strwhere);
        }

        public static CATEGORYEntity GetCategoryEntity(string categoryid)
        {
            string strWhere = " CATEGORYID=" + categoryid;
            IList<CATEGORYEntity> list = Dao.Find(strWhere);
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }
        public static CATEGORYEntity GetCategoryEntityByExpress(string cityName)
        {
            //OracleParameter[] parameters = {
            //        new OracleParameter(":cityName",OracleDbType.Varchar2)					
            //        };
            //parameters[0].Value = cityName;
            string strWhere = " categoryname='" + cityName + "'";
            IList<CATEGORYEntity> list = Dao.Find(strWhere);
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        public static string GetDataStr(string where, string orderby, int start, int pagesize)
        {
            DataTable dt = Dao.GetPager(where, orderby, pagesize, start);
            int totalcount = GetCount(where);
            StringBuilder jsonstr = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                long id = Convert.ToInt64(dt.Rows[0]["PARENTCATE"]);
                CATEGORYEntity entity = Dao.FindById(id);
                int backparent = 0;
                if (entity != null)
                {
                    backparent = entity.PARENTCATE.Value;
                }
                jsonstr.Append("{");
                int count = 1;
                foreach (DataRow row in dt.Rows)
                {
                    jsonstr.AppendFormat("\"entity_{0}\":", count);
                    jsonstr.Append("{");
                    jsonstr.AppendFormat("\"id\":\"{0}\",", row["ID"].ToString());
                    jsonstr.AppendFormat("\"categoryid\":\"{0}\",", row["CATEGORYID"].ToString());
                    jsonstr.AppendFormat("\"parentcate\":\"{0}\",", row["PARENTCATE"].ToString());
                    jsonstr.AppendFormat("\"backparentcate\":\"{0}\",", backparent);
                    jsonstr.AppendFormat("\"categoryname\":\"{0}\"", EncodeByEscape.GetEscapeStr(row["CATEGORYNAME"].ToString()));
                    jsonstr.Append("},");
                    count++;
                }
                jsonstr.AppendFormat("\"Count\":{0}", totalcount);
                jsonstr.Append("}");
            }
            return jsonstr.ToString();
        }
    }
}
