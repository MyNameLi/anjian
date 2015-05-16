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
    public partial class PAGEOFROLEEntity
    {
        private SqlHelper _oracleHelper;

        #region const fields
        public const string DBName = "Sentiment";
        public const string TableName = "PAGEOFROLE";
        public const string PrimaryKey = "PK_PAGEOFROLE";
        #endregion

        #region columns
        public struct Columns
        {
            public const string ID = "ID";
            public const string ROLEID = "ROLEID";
            public const string PAGEID = "PAGEID";
        }
        #endregion

        #region constructors
        public PAGEOFROLEEntity()
        {
            _oracleHelper = new SqlHelper(DBName);
        }

        public PAGEOFROLEEntity(int id, int roleid, int pageid)
        {
            this.ID = id;

            this.ROLEID = roleid;

            this.PAGEID = pageid;

        }
        #endregion

        #region Properties

        public int? ID
        {
            get;
            set;
        }


        public int? ROLEID
        {
            get;
            set;
        }


        public int? PAGEID
        {
            get;
            set;
        }

        #endregion

        public class PAGEOFROLEDAO : SqlDAO<PAGEOFROLEEntity>
        {
            private SqlHelper _oracleHelper;
            public const string DBName = "SentimentConnStr";

            public PAGEOFROLEDAO()
            {
                _oracleHelper = new SqlHelper(DBName);
            }

            public override void Add(PAGEOFROLEEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into PAGEOFROLE(");
                strSql.Append("ROLEID,PAGEID)");
                strSql.Append(" values (");
                strSql.Append("@ROLEID,@PAGEID)");
                SqlParameter[] parameters = {
					new SqlParameter("@ROLEID",SqlDbType.Int),
					new SqlParameter("@PAGEID",SqlDbType.Int)
					};
                parameters[0].Value = entity.ROLEID;
                parameters[1].Value = entity.PAGEID;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override void Update(PAGEOFROLEEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update PAGEOFROLE set ");
                strSql.Append("ROLEID=@ROLEID,");
                strSql.Append("PAGEID=@PAGEID");

                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@ROLEID",SqlDbType.Int),
					new SqlParameter("@PAGEID",SqlDbType.Int),
					new SqlParameter("@ID",SqlDbType.Int)
				};
                parameters[0].Value = entity.ROLEID;
                parameters[1].Value = entity.PAGEID;
                parameters[2].Value = entity.ID;

                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public bool Delete(int PageId)
            {
                string strSql = "delete from PAGEOFROLE where PAGEID=@PAGEID";
                SqlParameter[] parameters = {
					new SqlParameter("@PAGEID",SqlDbType.Int)					
				};
                parameters[0].Value = PageId;
                try
                {
                    _oracleHelper.ExecuteSql(strSql, parameters);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            public bool DeleteByRoleId(int roleId)
            {
                string strSql = "delete from PAGEOFROLE where ROLEID=@ROLEID";
                SqlParameter[] parameters = {
					new SqlParameter("@ROLEID",SqlDbType.Int)					
				};
                parameters[0].Value = roleId;
                try
                {
                    _oracleHelper.ExecuteSql(strSql, parameters);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            public override void Delete(PAGEOFROLEEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from PAGEOFROLE ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.ID;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override PAGEOFROLEEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from PAGEOFROLE ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    PAGEOFROLEEntity entity = new PAGEOFROLEEntity();
                    if (!Convert.IsDBNull(row["ID"]))
                    {
                        entity.ID = Convert.ToInt32(row["ID"]);
                    }
                    if (!Convert.IsDBNull(row["ROLEID"]))
                    {
                        entity.ROLEID = Convert.ToInt32(row["ROLEID"]);
                    }
                    if (!Convert.IsDBNull(row["PAGEID"]))
                    {
                        entity.PAGEID = Convert.ToInt32(row["PAGEID"]);
                    }
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public  List<PAGEOFROLEEntity> Find(string strWhere,int roleid)
            {
                SqlParameter[] parameters = {
						new SqlParameter("@ROLEID", SqlDbType.Int)};
                parameters[0].Value = roleid;
                return Find(strWhere, parameters);
            }

            public override List<PAGEOFROLEEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM PAGEOFROLE ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<PAGEOFROLEEntity> list = new List<PAGEOFROLEEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        PAGEOFROLEEntity entity = new PAGEOFROLEEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        if (!Convert.IsDBNull(row["ROLEID"]))
                        {
                            entity.ROLEID = Convert.ToInt32(row["ROLEID"]);
                        }
                        if (!Convert.IsDBNull(row["PAGEID"]))
                        {
                            entity.PAGEID = Convert.ToInt32(row["PAGEID"]);
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
                strSql.Append(" FROM PAGEOFROLE");
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
                string sql = "select count(*) from PAGEOFROLE ";
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
                PagerSql.Append("FROM (SELECT * FROM PAGEOFROLE ");
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

