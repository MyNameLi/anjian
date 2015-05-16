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
    public partial class MENUINROLEEntity
    {
        private SqlHelper _oracleHelper;

        #region const fields
        public const string DBName = "Sentiment";
        public const string TableName = "MENUINROLE";
        public const string PrimaryKey = "PK_MENUINROLE";
        #endregion

        #region columns
        public struct Columns
        {
            public const string ROLEID = "ROLEID";
            public const string MENUID = "MENUID";
        }
        #endregion

        #region constructors
        public MENUINROLEEntity()
        {
            _oracleHelper = new SqlHelper(DBName);
        }

        public MENUINROLEEntity(int roleid, int menuid)
        {

            this.ROLEID = roleid;

            this.MENUID = menuid;

        }
        #endregion

        #region Properties

        public int? ROLEID
        {
            get;
            set;
        }


        public int? MENUID
        {
            get;
            set;
        }

        #endregion

        public class MENUINROLEDAO : SqlDAO<MENUINROLEEntity>
        {
            private SqlHelper _oracleHelper;
            public const string DBName = "SentimentConnStr";

            public MENUINROLEDAO()
            {
                _oracleHelper = new SqlHelper(DBName);
            }

            public override void Add(MENUINROLEEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into MENUINROLE(");
                strSql.Append("ROLEID,MENUID)");
                strSql.Append(" values (");
                strSql.Append("@ROLEID,@MENUID)");
                SqlParameter[] parameters = {					
					new SqlParameter("@ROLEID",SqlDbType.Int),
					new SqlParameter("@MENUID",SqlDbType.Int)
					};
                parameters[0].Value = entity.ROLEID;
                parameters[1].Value = entity.MENUID;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public bool DeleteByRoleId(int roleid)
            {
                try
                {
                    string strSql = "delete from MENUINROLE where ROLEID = :ROLEID";
                    SqlParameter[] parameters = {					
						new SqlParameter("@ROLEID",SqlDbType.Int)						
						};
                    parameters[0].Value = roleid;
                    _oracleHelper.ExecuteSql(strSql, parameters);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            public List<MENUINROLEEntity> Find(string strWhere) {
                return Find(strWhere, null);
            }

            public override List<MENUINROLEEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM MENUINROLE ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<MENUINROLEEntity> list = new List<MENUINROLEEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        MENUINROLEEntity entity = new MENUINROLEEntity();
                        if (!Convert.IsDBNull(row["ROLEID"]))
                        {
                            entity.ROLEID = Convert.ToInt32(row["ROLEID"]);
                        }
                        if (!Convert.IsDBNull(row["MENUID"]))
                        {
                            entity.MENUID = Convert.ToInt32(row["MENUID"]);
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
                strSql.Append(" FROM MENUINROLE");
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
                string sql = "select count(*) from MENUINROLE ";
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
                PagerSql.Append("FROM (SELECT * FROM MENUINROLE ");
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

                    PagerSql.Append(" ORDER BY ID");//默认按主键排序

                }
                PagerSql.AppendFormat(" ) A WHERE ROWNUM <= {0})", endNumber);
                PagerSql.AppendFormat(" WHERE RN >= {0}", startNumber);

                return _oracleHelper.ExecuteDateSet(PagerSql.ToString(), param).Tables[0];
            }

            #endregion

        }
    }
}

