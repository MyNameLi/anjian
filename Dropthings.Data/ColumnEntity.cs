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
    public partial class ColumnEntity
    {
        private SqlHelper _oracleHelper;

        #region const fields
        public const string DBName = "SentimentConnStr";
        public const string TableName = "COLUMNOFPAGE";
        public const string PrimaryKey = "PK_COLUMN";
        #endregion

        #region columns
        public struct Columns
        {
            public const string ID = "ID";
            public const string PAGEID = "PAGEID";
            public const string WIDGETZONEID = "WIDGETZONEID";
            public const string COLUMNNO = "COLUMNNO";
            public const string COLUMNWIDTH = "COLUMNWIDTH";
            public const string LASTUPDATED = "LASTUPDATED";
        }
        #endregion

        #region constructors

        public ColumnEntity()
        {
            SqlHelper = new SqlHelper(DBName);
        }

        public ColumnEntity(int id, int pageid, int widgetzoneid, int columnno, int columnwidth, DateTime lastupdated)
        {
            this.ID = id;

            this.PAGEID = pageid;

            this.WIDGETZONEID = widgetzoneid;

            this.COLUMNNO = columnno;

            this.COLUMNWIDTH = columnwidth;

            this.LASTUPDATED = lastupdated;

        }
        #endregion

        #region Properties

        public int? ID
        {
            get;
            set;
        }


        public int? PAGEID
        {
            get;
            set;
        }


        public int? WIDGETZONEID
        {
            get;
            set;
        }


        public int? COLUMNNO
        {
            get;
            set;
        }


        public int? COLUMNWIDTH
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

        public class ColumnDAO : SqlDAO<ColumnEntity>
        {
            private SqlHelper _oracleHelper;
            public const string DBName = "SentimentConnStr";

            public ColumnDAO()
            {
                _oracleHelper = new SqlHelper(DBName);
            }

            public override void Add(ColumnEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into COLUMNOFPAGE(");
                strSql.Append("PAGEID,WIDGETZONEID,COLUMNNO,COLUMNWIDTH,LASTUPDATED)");
                strSql.Append(" values (");
                strSql.Append("@PAGEID,@WIDGETZONEID,@COLUMNNO,@COLUMNWIDTH,@LASTUPDATED)");
                SqlParameter[] parameters = {
					new SqlParameter("@PAGEID",SqlDbType.Int),
					new SqlParameter("@WIDGETZONEID",SqlDbType.Int),
					new SqlParameter("@COLUMNNO",SqlDbType.Int),
					new SqlParameter("@COLUMNWIDTH",SqlDbType.Int),
					new SqlParameter("@LASTUPDATED",SqlDbType.DateTime)
					};
                parameters[0].Value = entity.PAGEID;
                parameters[1].Value = entity.WIDGETZONEID;
                parameters[2].Value = entity.COLUMNNO;
                parameters[3].Value = entity.COLUMNWIDTH;
                parameters[4].Value = entity.LASTUPDATED;

                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
                entity.ID = ReturnNewRowId();
            }

            private int ReturnNewRowId()
            {
                string sql = "select COLUMNOFPAGE_ID_SEQ.currval NEWID from dual";
                object NewId = _oracleHelper.GetSingle(sql, null);
                return Convert.ToInt32(NewId);
            }

            public override void Update(ColumnEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update COLUMNOFPAGE set ");
                strSql.Append("PAGEID=@PAGEID,");
                strSql.Append("WIDGETZONEID=@WIDGETZONEID,");
                strSql.Append("COLUMNNO=@COLUMNNO,");
                strSql.Append("COLUMNWIDTH=@COLUMNWIDTH,");
                strSql.Append("LASTUPDATED=@LASTUPDATED");

                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@PAGEID",SqlDbType.Int),
					new SqlParameter("@WIDGETZONEID",SqlDbType.Int),
					new SqlParameter("@COLUMNNO",SqlDbType.Int),
					new SqlParameter("@COLUMNWIDTH",SqlDbType.Int),
					new SqlParameter("@LASTUPDATED",SqlDbType.DateTime),
   					new SqlParameter("@ID",SqlDbType.Int)
					};
                parameters[0].Value = entity.PAGEID;
                parameters[1].Value = entity.WIDGETZONEID;
                parameters[2].Value = entity.COLUMNNO;
                parameters[3].Value = entity.COLUMNWIDTH;
                parameters[4].Value = entity.LASTUPDATED;
                parameters[5].Value = entity.ID;

                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public void UpdateList(List<ColumnEntity> columns)
            {
                foreach (ColumnEntity entity in columns)
                {
                    Update(entity);
                }
            }

            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update COLUMNOFPAGE set ");
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
                string strSql = "delete from COLUMNOFPAGE where ID=" + ID;
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
                string strSql = "delete from COLUMNOFPAGE where ID in (" + ID + ")";
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

            public override void Delete(ColumnEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from COLUMNOFPAGE ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.ID;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override ColumnEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from COLUMNOFPAGE ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    ColumnEntity entity = new ColumnEntity();
                    if (!Convert.IsDBNull(row["ID"]))
                    {
                        entity.ID = Convert.ToInt32(row["ID"]);
                    }
                    if (!Convert.IsDBNull(row["PAGEID"]))
                    {
                        entity.PAGEID = Convert.ToInt32(row["PAGEID"]);
                    }
                    if (!Convert.IsDBNull(row["WIDGETZONEID"]))
                    {
                        entity.WIDGETZONEID = Convert.ToInt32(row["WIDGETZONEID"]);
                    }
                    if (!Convert.IsDBNull(row["COLUMNNO"]))
                    {
                        entity.COLUMNNO = Convert.ToInt32(row["COLUMNNO"]);
                    }
                    if (!Convert.IsDBNull(row["COLUMNWIDTH"]))
                    {
                        entity.COLUMNWIDTH = Convert.ToInt32(row["COLUMNWIDTH"]);
                    }
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

            public ColumnEntity GetColumnByTabId_ColumnNo(int TabId, int columnNo)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from COLUMNOFPAGE ");
                strSql.Append(" where PAGEID=@PAGEID AND COLUMNNO=@COLUMNNO");
                SqlParameter[] parameters = {
						new SqlParameter("@PAGEID", SqlDbType.Int),
                                               new SqlParameter("@COLUMNNO", SqlDbType.Int)};
                parameters[0].Value = TabId;
                parameters[1].Value = columnNo;

                SqlDataReader odr = _oracleHelper.ExecuteReader(strSql.ToString(), parameters);
                while (odr.Read())
                {
                    ColumnEntity entity = new ColumnEntity();
                    if (!Convert.IsDBNull(odr["ID"]))
                    {
                        entity.ID = Convert.ToInt32(odr["ID"]);
                    }
                    if (!Convert.IsDBNull(odr["COLUMNNO"]))
                    {
                        entity.COLUMNNO = Convert.ToInt32(odr["COLUMNNO"]);
                    }
                    if (!Convert.IsDBNull(odr["COLUMNWIDTH"]))
                    {
                        entity.COLUMNWIDTH = Convert.ToInt32(odr["COLUMNWIDTH"]);
                    }
                    if (!Convert.IsDBNull(odr["LASTUPDATED"]))
                    {
                        entity.LASTUPDATED = Convert.ToDateTime(odr["LASTUPDATED"]);
                    }
                    if (!Convert.IsDBNull(odr["PAGEID"]))
                    {
                        entity.PAGEID = Convert.ToInt32(odr["PAGEID"]);
                    }
                    if (!Convert.IsDBNull(odr["WIDGETZONEID"]))
                    {
                        entity.WIDGETZONEID = Convert.ToInt32(odr["WIDGETZONEID"]);
                    }

                    return entity;
                }
                return null;
            }

            public List<ColumnEntity> GetColumnsByTabId(int TabId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from COLUMNOFPAGE ");
                strSql.Append(" where PAGEID=@PAGEID");
                SqlParameter[] parameters = {
						new SqlParameter("@PAGEID", SqlDbType.Int)
                                               };
                parameters[0].Value = TabId;

                List<ColumnEntity> entityList = new List<ColumnEntity>();

                SqlDataReader odr = _oracleHelper.ExecuteReader(strSql.ToString(), parameters);
                while (odr.Read())
                {
                    ColumnEntity entity = new ColumnEntity();

                    if (!Convert.IsDBNull(odr["ID"]))
                    {
                        entity.ID = Convert.ToInt32(odr["ID"]);
                    }
                    if (!Convert.IsDBNull(odr["COLUMNNO"]))
                    {
                        entity.COLUMNNO = Convert.ToInt32(odr["COLUMNNO"]);
                    }
                    if (!Convert.IsDBNull(odr["COLUMNWIDTH"]))
                    {
                        entity.COLUMNWIDTH = Convert.ToInt32(odr["COLUMNWIDTH"]);
                    }
                    if (!Convert.IsDBNull(odr["LASTUPDATED"]))
                    {
                        entity.LASTUPDATED = Convert.ToDateTime(odr["LASTUPDATED"]);
                    }
                    if (!Convert.IsDBNull(odr["PAGEID"]))
                    {
                        entity.PAGEID = Convert.ToInt32(odr["PAGEID"]);
                    }
                    if (!Convert.IsDBNull(odr["WIDGETZONEID"]))
                    {
                        entity.WIDGETZONEID = Convert.ToInt32(odr["WIDGETZONEID"]);
                    }

                    entityList.Add(entity);
                }

                return entityList;
            }

            public override List<ColumnEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM COLUMNOFPAGE ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<ColumnEntity> list = new List<ColumnEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ColumnEntity entity = new ColumnEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        if (!Convert.IsDBNull(row["PAGEID"]))
                        {
                            entity.PAGEID = Convert.ToInt32(row["PAGEID"]);
                        }
                        if (!Convert.IsDBNull(row["WIDGETZONEID"]))
                        {
                            entity.WIDGETZONEID = Convert.ToInt32(row["WIDGETZONEID"]);
                        }
                        if (!Convert.IsDBNull(row["COLUMNNO"]))
                        {
                            entity.COLUMNNO = Convert.ToInt32(row["COLUMNNO"]);
                        }
                        if (!Convert.IsDBNull(row["COLUMNWIDTH"]))
                        {
                            entity.COLUMNWIDTH = Convert.ToInt32(row["COLUMNWIDTH"]);
                        }
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
                strSql.Append(" FROM COLUMNOFPAGE");
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
                string sql = "select count(*) from COLUMNOFPAGE ";
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
                PagerSql.Append("FROM (SELECT * FROM COLUMNOFPAGE ");
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

