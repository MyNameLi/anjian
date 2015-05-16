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
    public partial class ARCHIVEINFOEntity
    {
        private SqlHelper _oracleHelper;

        #region const fields
        public const string DBName = "Sentiment";
        public const string TableName = "ARCHIVEINFO";
        public const string PrimaryKey = "PK_ARCHIVEINFO";
        #endregion

        #region columns
        public struct Columns
        {
            public const string ID = "ID";
            public const string TITLE = "TITLE";
            public const string BASETIME = "BASETIME";
            public const string EDITTIME = "EDITTIME";
            public const string DISCONTENT = "DISCONTENT";
            public const string SITENAME = "SITENAME";
            public const string URL = "URL";
            public const string TYPE = "TYPE";
            public const string USERID = "USERID";
        }
        #endregion

        #region constructors
        public ARCHIVEINFOEntity()
        {
            _oracleHelper = new SqlHelper(DBName);
        }

        public ARCHIVEINFOEntity(long id, string title, DateTime basetime, DateTime edittime, string discontent, string sitename, string url, int type, int userid)
        {
            this.ID = id;

            this.TITLE = title;

            this.BASETIME = basetime;

            this.EDITTIME = edittime;

            this.DISCONTENT = discontent;

            this.SITENAME = sitename;

            this.URL = url;

            this.TYPE = type;

            this.USERID = userid;

        }
        #endregion

        #region Properties

        public long? ID
        {
            get;
            set;
        }


        public string TITLE
        {
            get;
            set;
        }


        public DateTime? BASETIME
        {
            get;
            set;
        }


        public DateTime? EDITTIME
        {
            get;
            set;
        }


        public string DISCONTENT
        {
            get;
            set;
        }


        public string SITENAME
        {
            get;
            set;
        }


        public string URL
        {
            get;
            set;
        }


        public int? TYPE
        {
            get;
            set;
        }


        public int? USERID
        {
            get;
            set;
        }

        #endregion

        public class ARCHIVEINFODAO : SqlDAO<ARCHIVEINFOEntity>
        {
            private SqlHelper _oracleHelper;
            public const string DBName = "SentimentConnStr";

            public ARCHIVEINFODAO()
            {
                _oracleHelper = new SqlHelper(DBName);
            }

            public override void Add(ARCHIVEINFOEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into ARCHIVEINFO(");
                strSql.Append("TITLE,BASETIME,EDITTIME,DISCONTENT,SITENAME,URL,TYPE,USERID)");
                strSql.Append(" values (");
                strSql.Append("@TITLE,@BASETIME,@EDITTIME,@DISCONTENT,@SITENAME,@URL,@TYPE,@USERID)");
                SqlParameter[] parameters = {
					new SqlParameter("@TITLE",SqlDbType.NVarChar),
					new SqlParameter("@BASETIME",SqlDbType.DateTime),
					new SqlParameter("@EDITTIME",SqlDbType.DateTime),
					new SqlParameter("@DISCONTENT",SqlDbType.Text),
					new SqlParameter("@SITENAME",SqlDbType.NVarChar),
					new SqlParameter("@URL",SqlDbType.NVarChar),
					new SqlParameter("@TYPE",SqlDbType.Int),
					new SqlParameter("@USERID",SqlDbType.Int)
					};
                parameters[0].Value = entity.TITLE;
                parameters[1].Value = entity.BASETIME;
                parameters[2].Value = entity.EDITTIME;
                parameters[3].Value = entity.DISCONTENT;
                parameters[4].Value = entity.SITENAME;
                parameters[5].Value = entity.URL;
                parameters[6].Value = entity.TYPE;
                parameters[7].Value = entity.USERID;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }


            public long AddEntity(ARCHIVEINFOEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into ARCHIVEINFO(");
                strSql.Append("TITLE,BASETIME,EDITTIME,DISCONTENT,SITENAME,URL,TYPE,USERID)");
                strSql.Append(" values (");
                strSql.Append("@TITLE,@BASETIME,@EDITTIME,@DISCONTENT,@SITENAME,@URL,@TYPE,@USERID)");
                SqlParameter[] parameters = {
					new SqlParameter("@TITLE",SqlDbType.NVarChar),
					new SqlParameter("@BASETIME",SqlDbType.DateTime),
					new SqlParameter("@EDITTIME",SqlDbType.DateTime),
					new SqlParameter("@DISCONTENT",SqlDbType.Text),
					new SqlParameter("@SITENAME",SqlDbType.NVarChar),
					new SqlParameter("@URL",SqlDbType.NVarChar),
					new SqlParameter("@TYPE",SqlDbType.Int),
					new SqlParameter("@USERID",SqlDbType.Int)
					};
                parameters[0].Value = entity.TITLE;
                parameters[1].Value = entity.BASETIME;
                parameters[2].Value = entity.EDITTIME;
                parameters[3].Value = entity.DISCONTENT;
                parameters[4].Value = entity.SITENAME;
                parameters[5].Value = entity.URL;
                parameters[6].Value = entity.TYPE;
                parameters[7].Value = entity.USERID;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
                return ReturnNewRowId();
            }

            private long ReturnNewRowId()
            {
                string sql = "select ARCHIVEINFO_ID_SEQ.currval NEWID from dual";
                object NewId = _oracleHelper.GetSingle(sql, null);
                return Convert.ToInt64(NewId);
            }

            public override void Update(ARCHIVEINFOEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update ARCHIVEINFO set ");
                strSql.Append("TITLE=@TITLE,");
                strSql.Append("BASETIME=@BASETIME,");
                strSql.Append("EDITTIME=@EDITTIME,");
                strSql.Append("DISCONTENT=@DISCONTENT,");
                strSql.Append("SITENAME=@SITENAME,");
                strSql.Append("URL=@URL,");
                strSql.Append("TYPE=@TYPE,");
                strSql.Append("USERID=@USERID");

                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@TITLE",SqlDbType.NVarChar),
					new SqlParameter("@BASETIME",SqlDbType.DateTime),
					new SqlParameter("@EDITTIME",SqlDbType.DateTime),
					new SqlParameter("@DISCONTENT",SqlDbType.Text),
					new SqlParameter("@SITENAME",SqlDbType.NVarChar),
					new SqlParameter("@URL",SqlDbType.NVarChar),
					new SqlParameter("@TYPE",SqlDbType.Int),
					new SqlParameter("@USERID",SqlDbType.Int),
					new SqlParameter("@ID",SqlDbType.BigInt)
				};
                parameters[0].Value = entity.TITLE;
                parameters[1].Value = entity.BASETIME;
                parameters[2].Value = entity.EDITTIME;
                parameters[3].Value = entity.DISCONTENT;
                parameters[4].Value = entity.SITENAME;
                parameters[5].Value = entity.URL;
                parameters[6].Value = entity.TYPE;
                parameters[7].Value = entity.USERID;
                parameters[8].Value = entity.ID;

                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update ARCHIVEINFO set ");
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
                string strSql = "delete from ARCHIVEINFO where ID=" + ID;
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
                string strSql = "delete from ARCHIVEINFO where ID in (" + ID + ")";
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


            public bool Delete(string url,int type)
            {
                string strSql = "delete from ARCHIVEINFO where URL in (" + url + ") AND TYPE=" + type.ToString();
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

            public override void Delete(ARCHIVEINFOEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from ARCHIVEINFO ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.ID;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override ARCHIVEINFOEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from ARCHIVEINFO ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    ARCHIVEINFOEntity entity = new ARCHIVEINFOEntity();
                    if (!Convert.IsDBNull(row["ID"]))
                    {
                        entity.ID = Convert.ToInt64(row["ID"]);
                    }
                    entity.TITLE = row["TITLE"].ToString();
                    if (!Convert.IsDBNull(row["BASETIME"]))
                    {
                        entity.BASETIME = Convert.ToDateTime(row["BASETIME"]);
                    }
                    if (!Convert.IsDBNull(row["EDITTIME"]))
                    {
                        entity.EDITTIME = Convert.ToDateTime(row["EDITTIME"]);
                    }
                    entity.DISCONTENT = row["DISCONTENT"].ToString();
                    entity.SITENAME = row["SITENAME"].ToString();
                    entity.URL = row["URL"].ToString();
                    if (!Convert.IsDBNull(row["TYPE"]))
                    {
                        entity.TYPE = Convert.ToInt32(row["TYPE"]);
                    }
                    if (!Convert.IsDBNull(row["USERID"]))
                    {
                        entity.USERID = Convert.ToInt32(row["USERID"]);
                    }
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public bool Exist(string url, int type)
            {
                string where = " URL=@URL AND TYPE=@TYPE";
                SqlParameter[] parameters = {
						new SqlParameter("@URL",SqlDbType.NVarChar),
                        new SqlParameter("@TYPE", SqlDbType.Int)                  
                };
                parameters[0].Value = url;
                parameters[1].Value = type;
                List<ARCHIVEINFOEntity> list = Find(where, parameters);
                if (list != null && list.Count > 0)
                {
                    return true;
                }
                else {
                    return false;
                }
            }

            public List<ARCHIVEINFOEntity> Find(string strWhere)
            {
                return Find(strWhere, null);
            }

            public override List<ARCHIVEINFOEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM ARCHIVEINFO ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<ARCHIVEINFOEntity> list = new List<ARCHIVEINFOEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ARCHIVEINFOEntity entity = new ARCHIVEINFOEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt64(row["ID"]);
                        }
                        entity.TITLE = row["TITLE"].ToString();
                        if (!Convert.IsDBNull(row["BASETIME"]))
                        {
                            entity.BASETIME = Convert.ToDateTime(row["BASETIME"]);
                        }
                        if (!Convert.IsDBNull(row["EDITTIME"]))
                        {
                            entity.EDITTIME = Convert.ToDateTime(row["EDITTIME"]);
                        }
                        entity.DISCONTENT = row["DISCONTENT"].ToString();
                        entity.SITENAME = row["SITENAME"].ToString();
                        entity.URL = row["URL"].ToString();
                        if (!Convert.IsDBNull(row["TYPE"]))
                        {
                            entity.TYPE = Convert.ToInt32(row["TYPE"]);
                        }
                        if (!Convert.IsDBNull(row["USERID"]))
                        {
                            entity.USERID = Convert.ToInt32(row["USERID"]);
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
                strSql.Append(" FROM ARCHIVEINFO");
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
                string sql = "select count(*) from ARCHIVEINFO ";
                if (!string.IsNullOrEmpty(where))
                {
                    sql += "where " + where;
                }

                object obj = _oracleHelper.GetSingle(sql, param);

                return obj == null ? 0 : Convert.ToInt32(obj);
            }

            public int GetPagerRowsCount(string where) {
                return GetPagerRowsCount(where, null);
            }


            public DataTable GetPager(string where, string orderBy, int pageSize, int pageNumber) {
                return GetPager(where, null, orderBy, pageSize, pageNumber);
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
                PagerSql.Append("FROM (SELECT * FROM ARCHIVEINFO ");
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

