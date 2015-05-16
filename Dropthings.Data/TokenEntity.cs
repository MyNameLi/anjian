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
    public partial class TokenEntity
    {
        private SqlHelper _sqlHelper;

        #region const fields
        public const string DBName = "SentimentConnStr";
        public const string TableName = "TOKEN";
        public const string PrimaryKey = "PK_TOKEN";
        #endregion

        #region columns
        public struct Columns
        {
            public const string ID = "ID";
            public const string UNIQUEID = "UNIQUEID";
            public const string USERID = "USERID";
            public const string USERNAME = "USERNAME";
            public const string LASTUPDATED = "LASTUPDATED";
        }
        #endregion

        #region constructors
        public TokenEntity()
        {
            _sqlHelper = new SqlHelper(DBName);
        }

        public TokenEntity(int id, string uniqueid, int userid, string username, DateTime lastupdated)
        {
            this.ID = id;

            this.UNIQUEID = uniqueid;

            this.USERID = userid;

            this.USERNAME = username;

            this.LASTUPDATED = lastupdated;

        }
        #endregion

        #region Properties

        public int? ID
        {
            get;
            set;
        }


        public string UNIQUEID
        {
            get;
            set;
        }


        public int? USERID
        {
            get;
            set;
        }


        public string USERNAME
        {
            get;
            set;
        }


        public DateTime LASTUPDATED
        {
            get;
            set;
        }

        #endregion

        public class TokenDAO : SqlDAO<TokenEntity>
        {
            private SqlHelper _sqlHelper;
            public const string DBName = "SentimentConnStr";

            public TokenDAO()
            {
                _sqlHelper = new SqlHelper(DBName);
            }

            public override void Add(TokenEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into TOKEN(");
                strSql.Append("UNIQUEID,USERID,USERNAME,LASTUPDATED)");
                strSql.Append(" values (");
                strSql.Append("@UNIQUEID,@USERID,@USERNAME,@LASTUPDATED)");
                SqlParameter[] parameters = {
					new SqlParameter("@UNIQUEID",SqlDbType.NVarChar),
					new SqlParameter("@USERID",SqlDbType.Int),
					new SqlParameter("@USERNAME",SqlDbType.NVarChar),
					new SqlParameter("@LASTUPDATED",SqlDbType.DateTime)
					};
                parameters[0].Value = entity.UNIQUEID;
                parameters[1].Value = entity.USERID;
                parameters[2].Value = entity.USERNAME;
                parameters[3].Value = entity.LASTUPDATED;

                _sqlHelper.ExecuteSql(strSql.ToString(), parameters);
                entity.ID = ReturnNewRowId();
            }

            private int ReturnNewRowId()
            {
                string sql = "select TOKEN_ID_SEQ.currval NEWID from dual";
                object NewId = _sqlHelper.GetSingle(sql, null);
                return Convert.ToInt32(NewId);
            }

            public override void Update(TokenEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update TOKEN set ");
                strSql.Append("UNIQUEID=@UNIQUEID,");
                strSql.Append("USERID=@USERID,");
                strSql.Append("USERNAME=@USERNAME,");
                strSql.Append("LASTUPDATED=@LASTUPDATED");

                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@UNIQUEID",SqlDbType.VarChar),
					new SqlParameter("@USERID",SqlDbType.Int),
					new SqlParameter("@USERNAME",SqlDbType.NVarChar),
					new SqlParameter("@LASTUPDATED",SqlDbType.DateTime),
                    new SqlParameter("@ID",SqlDbType.Int)
					};
                parameters[0].Value = entity.UNIQUEID;
                parameters[1].Value = entity.USERID;
                parameters[2].Value = entity.USERNAME;
                parameters[3].Value = entity.LASTUPDATED;
                parameters[4].Value = entity.ID;

                _sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update TOKEN set ");
                    StrSql.Append(ColumnName + "='" + Value + "'");
                    StrSql.Append(" where ID=" + ID);
                    _sqlHelper.ExecuteSql(StrSql.ToString(), null);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public bool Delete(int ID)
            {
                string strSql = "delete from TOKEN where ID=" + ID;
                try
                {
                    _sqlHelper.ExecuteSql(strSql, null);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public bool Delete(string ID)
            {
                string strSql = "delete from TOKEN where ID in (" + ID + ")";
                try
                {
                    _sqlHelper.ExecuteSql(strSql, null);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public override void Delete(TokenEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from TOKEN ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.ID;
                _sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override TokenEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from TOKEN ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = _sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    TokenEntity entity = new TokenEntity();
                    if (!Convert.IsDBNull(row["ID"]))
                    {
                        entity.ID = Convert.ToInt32(row["ID"]);
                    }
                    if (!Convert.IsDBNull(row["UNIQUEID"]))
                    {
                        entity.UNIQUEID = row["UNIQUEID"].ToString();
                    }
                    if (!Convert.IsDBNull(row["USERID"]))
                    {
                        entity.USERID = Convert.ToInt32(row["USERID"]);
                    }
                    entity.USERNAME = row["USERNAME"].ToString();
                    if (!Convert.IsDBNull(row["LASTUPDATED"]))
                    {
                        entity.LASTUPDATED = Convert.ToDateTime(row["LASTUPDATED"]);
                    }
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public override List<TokenEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM TOKEN ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = _sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<TokenEntity> list = new List<TokenEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        TokenEntity entity = new TokenEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        if (!Convert.IsDBNull(row["UNIQUEID"]))
                        {
                            entity.UNIQUEID = row["UNIQUEID"].ToString();
                        }
                        if (!Convert.IsDBNull(row["USERID"]))
                        {
                            entity.USERID = Convert.ToInt32(row["USERID"]);
                        }
                        entity.USERNAME = row["USERNAME"].ToString();
                        if (!Convert.IsDBNull(row["LASTUPDATED"]))
                        {
                            entity.LASTUPDATED = Convert.ToDateTime(row["LASTUPDATED"]);
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

            public override DataSet GetDataSet(string strWhere, SqlParameter[] param)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM TOKEN");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                return _sqlHelper.ExecuteDateSet(strSql.ToString(), param);
            }

            #region paging methods

            /// <summary>
            /// 获取分页记录总数
            /// </summary>
            /// <param name="where">条件，等同于GetPaer()方法的where</param>
            /// <returns>返回记录总数</returns>
            public int GetPagerRowsCount(string where, SqlParameter[] param)
            {
                string sql = "select count(*) from TOKEN ";
                if (!string.IsNullOrEmpty(where))
                {
                    sql += "where " + where;
                }

                object obj = _sqlHelper.GetSingle(sql, param);

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
                PagerSql.Append("FROM (SELECT * FROM TOKEN ");
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

                return _sqlHelper.ExecuteDateSet(PagerSql.ToString(), param).Tables[0];
            }

            #endregion

        }
    }
}

