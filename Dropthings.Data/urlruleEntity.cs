using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace Dropthings.Data
{
    [Serializable]
    public partial class URLRULEEntity
    {
        private SqlHelper _oracleHelper;

        #region const fields
        public const string DBName = "Sentiment";
        public const string TableName = "URLRULE";
        public const string PrimaryKey = "PK_URLRULE";
        #endregion

        #region columns
        public struct Columns
        {           
            public const string RULEOBJECT = "RULEOBJECT";
            public const string RULEACTIVE = "RULEACTIVE";
            public const string RULEKEYWORD = "RULEKEYWORD";
            public const string RULEREG = "RULEREG";
            public const string TASKID = "TASKID";
        }
        #endregion

        #region constructors
        public URLRULEEntity()
        {
            _oracleHelper = new SqlHelper(DBName);
        }

        public URLRULEEntity( int ruleobject, int ruleactive, string rulekeyword, string rulereg, int taskid)
        {           

            this.RULEOBJECT = ruleobject;

            this.RULEACTIVE = ruleactive;

            this.RULEKEYWORD = rulekeyword;

            this.RULEREG = rulereg;

            this.TASKID = taskid;

        }
        #endregion

        #region Properties
       
        public int? RULEOBJECT
        {
            get;
            set;
        }


        public int? RULEACTIVE
        {
            get;
            set;
        }


        public string RULEKEYWORD
        {
            get;
            set;
        }


        public string RULEREG
        {
            get;
            set;
        }


        public int? TASKID
        {
            get;
            set;
        }

        #endregion

        public class URLRULEDAO : SqlDAO<URLRULEEntity>
        {
            private SqlHelper _oracleHelper;
            public const string DBName = "SentimentConnStr";

            public URLRULEDAO()
            {
                _oracleHelper = new SqlHelper(DBName);
            }

            public override void Add(URLRULEEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into URLRULE(");
                strSql.Append("RULEOBJECT,RULEACTIVE,RULEKEYWORD,RULEREG,TASKID)");
                strSql.Append(" values (");
                strSql.Append("@RULEOBJECT,@RULEACTIVE,@RULEKEYWORD,@RULEREG,@TASKID)");
                SqlParameter[] parameters = {
					new SqlParameter("@RULEOBJECT",SqlDbType.Int),
					new SqlParameter("@RULEACTIVE",SqlDbType.Int),
					new SqlParameter("@RULEKEYWORD",SqlDbType.NVarChar),
					new SqlParameter("@RULEREG",SqlDbType.NVarChar),
					new SqlParameter("@TASKID",SqlDbType.Int)
					};
                parameters[0].Value = entity.RULEOBJECT;
                parameters[1].Value = entity.RULEACTIVE;
                parameters[2].Value = entity.RULEKEYWORD;
                parameters[3].Value = entity.RULEREG;
                parameters[4].Value = entity.TASKID;

                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }            

            
            public bool DeleteByTaskID(int TaskID)
            {
                string strSql = "delete from URLRULE where TASKID=" + TaskID.ToString();
                try
                {
                    _oracleHelper.ExecuteSql(strSql, null);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public bool DeleteByTaskID(string TaskID)
            {
                string strSql = "delete from URLRULE where TASKID in (" + TaskID + ")";
                try
                {
                    _oracleHelper.ExecuteSql(strSql, null);
                    return true;
                }
                catch
                {
                    return false;
                }
            }


            public List<URLRULEEntity> Find(string strWhere)
            {
                return Find(strWhere, null);
            }

            public override List<URLRULEEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM URLRULE ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<URLRULEEntity> list = new List<URLRULEEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        URLRULEEntity entity = new URLRULEEntity();                       
                        if (!Convert.IsDBNull(row["RULEOBJECT"]))
                        {
                            entity.RULEOBJECT = Convert.ToInt32(row["RULEOBJECT"]);
                        }
                        if (!Convert.IsDBNull(row["RULEACTIVE"]))
                        {
                            entity.RULEACTIVE = Convert.ToInt32(row["RULEACTIVE"]);
                        }
                        entity.RULEKEYWORD = row["RULEKEYWORD"].ToString();
                        entity.RULEREG = row["RULEREG"].ToString();
                        if (!Convert.IsDBNull(row["TASKID"]))
                        {
                            entity.TASKID = Convert.ToInt32(row["TASKID"]);
                        }

                        list.Add(entity);
                    }

                    return list;
                }
                else
                {
                    return null;
                }
            }

            public DataSet GetDataSet(string strWhere, SqlParameter[] param)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM URLRULE");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                return _oracleHelper.ExecuteDateSet(strSql.ToString(), param);
            }

            #region paging methods

            /// <summary>
            /// 获取分页记录总数
            /// </summary>
            /// <param name="where">条件，等同于GetPaer()方法的where</param>
            /// <returns>返回记录总数</returns>
            public int GetPagerRowsCount(string where, SqlParameter[] param)
            {
                string sql = "select count(*) from URLRULE ";
                if (!string.IsNullOrEmpty(where))
                {
                    sql += "where " + where;
                }

                object obj = _oracleHelper.GetSingle(sql, param);

                return obj == null ? 0 : Convert.ToInt32(obj);
            }

            /// <summary>
            /// 查询分页信息，返回当前页码的记录集
            /// </summary>
            /// <param name="where">查询条件，可为empty</param>
            /// <param name="orderBy">排序条件，可为empty</param>
            /// <param name="pageSize">每页显示记录数</param>
            /// <param name="pageNumber">当前页码</param>
            /// <returns>datatable</returns>
            public DataTable GetPager(string where, SqlParameter[] param, string orderBy, int pageSize, int pageNumber)
            {
                int startNumber = pageSize * (pageNumber - 1) + 1;
                int endNumber = pageSize * pageNumber;

                StringBuilder PagerSql = new StringBuilder();
                PagerSql.Append("SELECT * FROM (");
                PagerSql.Append(" SELECT A.*, ROWNUM RN ");
                PagerSql.Append("FROM (SELECT * FROM URLRULE ");
                if (!string.IsNullOrEmpty(where))
                {
                    PagerSql.Append(" where " + where);
                }
                if (!string.IsNullOrEmpty(orderBy))
                {
                    PagerSql.AppendFormat(" ORDER BY {0}", orderBy);
                }
                else
                {

                    PagerSql.Append(" ORDER BY RULEID");//默认按主键排序

                }
                PagerSql.AppendFormat(" ) A WHERE ROWNUM <= {0})", endNumber);
                PagerSql.AppendFormat(" WHERE RN >= {0}", startNumber);

                return _oracleHelper.ExecuteDateSet(PagerSql.ToString(), param).Tables[0];
            }

            #endregion

        }
    }
}

