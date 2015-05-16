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
    public partial class LEXICONTYPEEntity
    {
        private SqlHelper _oracleHelper;

        #region const fields
        public const string DBName = "Sentiment";
        public const string TableName = "LEXICONTYPE";
        public const string PrimaryKey = "PK_LEXICONTYPE";
        #endregion

        #region columns
        public struct Columns
        {
            public const string ID = "ID";
            public const string TYPENAME = "TYPENAME";
            public const string TYPEINTRODUCE = "TYPEINTRODUCE";
            public const string PARENTID = "PARENTID";
            public const string ORGID = "ORGID";
        }
        #endregion

        #region constructors
        public LEXICONTYPEEntity()
        {
            _oracleHelper = new SqlHelper(DBName);
        }

        public LEXICONTYPEEntity(int id, string typename, string typeintroduce, int parentid, int orgid)
        {
            this.ID = id;

            this.TYPENAME = typename;

            this.TYPEINTRODUCE = typeintroduce;

            this.PARENTID = parentid;

            this.ORGID = orgid;

        }
        #endregion

        #region Properties

        public int? ID
        {
            get;
            set;
        }


        public string TYPENAME
        {
            get;
            set;
        }


        public string TYPEINTRODUCE
        {
            get;
            set;
        }


        public int? PARENTID
        {
            get;
            set;
        }


        public int? ORGID
        {
            get;
            set;
        }

        #endregion

        public class LEXICONTYPEDAO : SqlDAO<LEXICONTYPEEntity>
        {
            private SqlHelper _oracleHelper;
            public const string DBName = "SentimentConnStr";

            public LEXICONTYPEDAO()
            {
                _oracleHelper = new SqlHelper(DBName);
            }

            public override void Add(LEXICONTYPEEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into LEXICONTYPE(");
                strSql.Append("TYPENAME,TYPEINTRODUCE,PARENTID,ORGID)");
                strSql.Append(" values (");
                strSql.Append("@TYPENAME,@TYPEINTRODUCE,@PARENTID,@ORGID)");
                SqlParameter[] parameters = {
					new SqlParameter("@TYPENAME",SqlDbType.NVarChar),
					new SqlParameter("@TYPEINTRODUCE",SqlDbType.NVarChar),
					new SqlParameter("@PARENTID",SqlDbType.Int),
					new SqlParameter("@ORGID",SqlDbType.Int)
					};
                parameters[0].Value = entity.TYPENAME;
                parameters[1].Value = entity.TYPEINTRODUCE;
                parameters[2].Value = entity.PARENTID;
                parameters[3].Value = entity.ORGID;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public int AddEntity(LEXICONTYPEEntity entity) {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into LEXICONTYPE(");
                strSql.Append("TYPENAME,TYPEINTRODUCE,PARENTID,ORGID)");
                strSql.Append(" values (");
                strSql.Append("@TYPENAME,@TYPEINTRODUCE,@PARENTID,@ORGID)");
                SqlParameter[] parameters = {
					new SqlParameter("@TYPENAME",SqlDbType.NVarChar),
					new SqlParameter("@TYPEINTRODUCE",SqlDbType.NVarChar),
					new SqlParameter("@PARENTID",SqlDbType.Int),
					new SqlParameter("@ORGID",SqlDbType.Int)
					};
                parameters[0].Value = entity.TYPENAME;
                parameters[1].Value = entity.TYPEINTRODUCE;
                parameters[2].Value = entity.PARENTID;
                parameters[3].Value = entity.ORGID;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
                return ReturnNewRowId();
            }

            private int ReturnNewRowId()
            {
                string sql = "select LEXICONTYPE_ID_SEQ.currval NEWID from dual";
                object NewId = _oracleHelper.GetSingle(sql, null);
                return Convert.ToInt32(NewId);
            }

            public override void Update(LEXICONTYPEEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update LEXICONTYPE set ");
                strSql.Append("TYPENAME=@TYPENAME,");
                strSql.Append("TYPEINTRODUCE=@TYPEINTRODUCE,");
                strSql.Append("PARENTID=@PARENTID,");
                strSql.Append("ORGID=@ORGID");

                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@TYPENAME",SqlDbType.NVarChar),
					new SqlParameter("@TYPEINTRODUCE",SqlDbType.NVarChar),
					new SqlParameter("@PARENTID",SqlDbType.Int),
					new SqlParameter("@ORGID",SqlDbType.Int),
					new SqlParameter("@ID",SqlDbType.Int)
				};
                parameters[0].Value = entity.TYPENAME;
                parameters[1].Value = entity.TYPEINTRODUCE;
                parameters[2].Value = entity.PARENTID;
                parameters[3].Value = entity.ORGID;
                parameters[4].Value = entity.ID;

                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update LEXICONTYPE set ");
                    StrSql.Append(ColumnName + "='" + Value + "'");
                    StrSql.Append(" where ID=" + ID);
                    _oracleHelper.ExecuteSql(StrSql.ToString(), null);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public bool Delete(int ID)
            {
                string strSql = "delete from LEXICONTYPE where ID=" + ID;
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

            public bool Delete(string ID)
            {
                string strSql = "delete from LEXICONTYPE where ID in (" + ID + ")";
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

            public override void Delete(LEXICONTYPEEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from LEXICONTYPE ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.ID;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override LEXICONTYPEEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from LEXICONTYPE ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    LEXICONTYPEEntity entity = new LEXICONTYPEEntity();
                    if (!Convert.IsDBNull(row["ID"]))
                    {
                        entity.ID = Convert.ToInt32(row["ID"]);
                    }
                    entity.TYPENAME = row["TYPENAME"].ToString();
                    entity.TYPEINTRODUCE = row["TYPEINTRODUCE"].ToString();
                    if (!Convert.IsDBNull(row["PARENTID"]))
                    {
                        entity.PARENTID = Convert.ToInt32(row["PARENTID"]);
                    }
                    if (!Convert.IsDBNull(row["ORGID"]))
                    {
                        entity.ORGID = Convert.ToInt32(row["ORGID"]);
                    }
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public List<LEXICONTYPEEntity> Find(string strWhere)
            {
                return Find(strWhere, null);
            }

            public override List<LEXICONTYPEEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM LEXICONTYPE ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<LEXICONTYPEEntity> list = new List<LEXICONTYPEEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        LEXICONTYPEEntity entity = new LEXICONTYPEEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        entity.TYPENAME = row["TYPENAME"].ToString();
                        entity.TYPEINTRODUCE = row["TYPEINTRODUCE"].ToString();
                        if (!Convert.IsDBNull(row["PARENTID"]))
                        {
                            entity.PARENTID = Convert.ToInt32(row["PARENTID"]);
                        }
                        if (!Convert.IsDBNull(row["ORGID"]))
                        {
                            entity.ORGID = Convert.ToInt32(row["ORGID"]);
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
                strSql.Append(" FROM LEXICONTYPE");
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
                string sql = "select count(*) from LEXICONTYPE ";
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
                PagerSql.Append("FROM (SELECT * FROM LEXICONTYPE ");
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

