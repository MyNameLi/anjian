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
    public partial class PageEntity
    {
        private SqlHelper _sqlHelper;

        #region const fields
        public const string DBName = "DropthingsConnectionString";
        public const string TableName = "PAGE";
        public const string PrimaryKey = "PK_PAGE";
        #endregion

        #region columns
        public struct Columns
        {
            public const string ID = "ID";
            public const string TITLE = "TITLE";
            public const string USERID = "USERID";
            public const string CREATEDDATE = "CREATEDDATE";
            public const string VERSIONNO = "VERSIONNO";
            public const string LAYOUTTYPE = "LAYOUTTYPE";
            public const string PAGETYPE = "PAGETYPE";
            public const string COLUMNCOUNT = "COLUMNCOUNT";
            public const string LASTUPDATED = "LASTUPDATED";
            public const string ISLOCKED = "ISLOCKED";
            public const string LASTLOCKEDSTATUSCHANGEDAT = "LASTLOCKEDSTATUSCHANGEDAT";
            public const string ISDOWNFORMAINTENANCE = "ISDOWNFORMAINTENANCE";
            public const string LASTDOWNFORMAINTENANCEAT = "LASTDOWNFORMAINTENANCEAT";
            public const string SERVEASSTARTPAGEAFTERLOGIN = "SERVEASSTARTPAGEAFTERLOGIN";
            public const string ORDERNO = "ORDERNO";
            public const string URL = "URL";
        }
        #endregion

        #region constructors
        public PageEntity()
        {
            _sqlHelper = new SqlHelper(DBName);
        }

        public PageEntity(int id, string title, int userid, DateTime createddate, int versionno, int layouttype, int pagetype, int columncount, DateTime lastupdated, int islocked, DateTime lastlockedstatuschangedat, int isdownformaintenance, DateTime lastdownformaintenanceat, int serveasstartpageafterlogin, int orderno, string url)
        {
            this.ID = id;

            this.TITLE = title;

            this.USERID = userid;

            this.CREATEDDATE = createddate;

            this.VERSIONNO = versionno;

            this.LAYOUTTYPE = layouttype;

            this.PAGETYPE = pagetype;

            this.COLUMNCOUNT = columncount;

            this.LASTUPDATED = lastupdated;

            this.ISLOCKED = islocked;

            this.LASTLOCKEDSTATUSCHANGEDAT = lastlockedstatuschangedat;

            this.ISDOWNFORMAINTENANCE = isdownformaintenance;

            this.LASTDOWNFORMAINTENANCEAT = lastdownformaintenanceat;

            this.SERVEASSTARTPAGEAFTERLOGIN = serveasstartpageafterlogin;

            this.ORDERNO = orderno;

            this.URL = url;

        }
        #endregion

        #region Properties

        public int? ID
        {
            get;
            set;
        }


        public string TITLE
        {
            get;
            set;
        }


        public int? USERID
        {
            get;
            set;
        }


        public DateTime? CREATEDDATE
        {
            get;
            set;
        }


        public int? VERSIONNO
        {
            get;
            set;
        }


        public int? LAYOUTTYPE
        {
            get;
            set;
        }


        public int? PAGETYPE
        {
            get;
            set;
        }


        public int? COLUMNCOUNT
        {
            get;
            set;
        }


        public DateTime LASTUPDATED
        {
            get;
            set;
        }


        public int ISLOCKED
        {
            get;
            set;
        }


        public DateTime? LASTLOCKEDSTATUSCHANGEDAT
        {
            get;
            set;
        }


        public int ISDOWNFORMAINTENANCE
        {
            get;
            set;
        }


        public DateTime? LASTDOWNFORMAINTENANCEAT
        {
            get;
            set;
        }


        public int SERVEASSTARTPAGEAFTERLOGIN
        {
            get;
            set;
        }


        public int? ORDERNO
        {
            get;
            set;
        }

        public string URL
        {
            get;
            set;
        }

        #endregion

        public class PAGEDAO : SqlDAO<PageEntity>
        {
            private SqlHelper _sqlHelper;
            public const string DBName = "SentimentConnStr";

            public PAGEDAO()
            {
                _sqlHelper = new SqlHelper(DBName);
            }

            public string GetTabOwnerName(int TabId)
            {
                string result = string.Empty;

                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT U.USERNAME FROM USERS U ,PAGE P ");
                strSql.Append("WHERE P.ID=@ID AND P.USERID=U.USERID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID",SqlDbType.Int)};
                parameters[0].Value = TabId;

                result = Convert.ToString(_sqlHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));

                return result;
            }

            public override void Add(PageEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into PAGE(");
                strSql.Append("TITLE,USERID,CREATEDDATE,VERSIONNO,LAYOUTTYPE,PAGETYPE,COLUMNCOUNT,LASTUPDATED,ISLOCKED,LASTLOCKEDSTATUSCHANGEDAT,ISDOWNFORMAINTENANCE,LASTDOWNFORMAINTENANCEAT,SERVEASSTARTPAGEAFTERLOGIN,ORDERNO,URL)");
                strSql.Append(" values (");
                strSql.Append("@TITLE,@USERID,@CREATEDDATE,@VERSIONNO,@LAYOUTTYPE,@PAGETYPE,@COLUMNCOUNT,@LASTUPDATED,@ISLOCKED,@LASTLOCKEDSTATUSCHANGEDAT,@ISDOWNFORMAINTENANCE,@LASTDOWNFORMAINTENANCEAT,@SERVEASSTARTPAGEAFTERLOGIN,@ORDERNO,@URL)");
                SqlParameter[] parameters = {
					new SqlParameter("@TITLE",SqlDbType.NVarChar),
					new SqlParameter("@USERID",SqlDbType.Int),
					new SqlParameter("@CREATEDDATE",SqlDbType.DateTime),
					new SqlParameter("@VERSIONNO",SqlDbType.Int),
					new SqlParameter("@LAYOUTTYPE",SqlDbType.Int),
					new SqlParameter("@PAGETYPE",SqlDbType.Int),
					new SqlParameter("@COLUMNCOUNT",SqlDbType.Int),
					new SqlParameter("@LASTUPDATED",SqlDbType.DateTime),
					new SqlParameter("@ISLOCKED",SqlDbType.Int),
					new SqlParameter("@LASTLOCKEDSTATUSCHANGEDAT",SqlDbType.DateTime),
					new SqlParameter("@ISDOWNFORMAINTENANCE",SqlDbType.Int),
					new SqlParameter("@LASTDOWNFORMAINTENANCEAT",SqlDbType.DateTime),
					new SqlParameter("@SERVEASSTARTPAGEAFTERLOGIN",SqlDbType.Int),
					new SqlParameter("@ORDERNO",SqlDbType.Int),
                    new SqlParameter("@URL",SqlDbType.NVarChar)
					};
                parameters[0].Value = entity.TITLE;
                parameters[1].Value = entity.USERID.Value;
                parameters[2].Value = entity.CREATEDDATE.Value;
                parameters[3].Value = entity.VERSIONNO.Value;
                parameters[4].Value = entity.LAYOUTTYPE.Value;
                parameters[5].Value = entity.PAGETYPE.Value;
                parameters[6].Value = entity.COLUMNCOUNT.Value;
                parameters[7].Value = entity.LASTUPDATED;
                parameters[8].Value = entity.ISLOCKED;
                parameters[9].Value = entity.LASTLOCKEDSTATUSCHANGEDAT.Value;
                parameters[10].Value = entity.ISDOWNFORMAINTENANCE;
                parameters[11].Value = entity.LASTDOWNFORMAINTENANCEAT.Value;
                parameters[12].Value = entity.SERVEASSTARTPAGEAFTERLOGIN;
                parameters[13].Value = entity.ORDERNO.Value;
                parameters[14].Value = entity.URL;
                _sqlHelper.ExecuteSql(strSql.ToString(), parameters);
                entity.ID = ReturnNewRowId();
            }

            private int ReturnNewRowId()
            {
                string sql = "select PAGE_ID_SEQ.currval NEWID from dual";
                object NewId = _sqlHelper.GetSingle(sql, null);
                return Convert.ToInt32(NewId);
            }

            public override void Update(PageEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update PAGE set ");
                strSql.Append("TITLE=@TITLE,");
                strSql.Append("USERID=@USERID,");
                strSql.Append("CREATEDDATE=@CREATEDDATE,");
                strSql.Append("VERSIONNO=@VERSIONNO,");
                strSql.Append("LAYOUTTYPE=@LAYOUTTYPE,");
                strSql.Append("PAGETYPE=@PAGETYPE,");
                strSql.Append("COLUMNCOUNT=@COLUMNCOUNT,");
                strSql.Append("LASTUPDATED=@LASTUPDATED,");
                strSql.Append("ISLOCKED=@ISLOCKED,");
                strSql.Append("LASTLOCKEDSTATUSCHANGEDAT=@LASTLOCKEDSTATUSCHANGEDAT,");
                strSql.Append("ISDOWNFORMAINTENANCE=@ISDOWNFORMAINTENANCE,");
                strSql.Append("LASTDOWNFORMAINTENANCEAT=@LASTDOWNFORMAINTENANCEAT,");
                strSql.Append("SERVEASSTARTPAGEAFTERLOGIN=@SERVEASSTARTPAGEAFTERLOGIN,");
                strSql.Append("ORDERNO=@ORDERNO,");
                strSql.Append("URL=@URL");

                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@TITLE",SqlDbType.NVarChar),
					new SqlParameter("@USERID",SqlDbType.Int),
					new SqlParameter("@CREATEDDATE",SqlDbType.DateTime),
					new SqlParameter("@VERSIONNO",SqlDbType.Int),
					new SqlParameter("@LAYOUTTYPE",SqlDbType.Int),
					new SqlParameter("@PAGETYPE",SqlDbType.Int),
					new SqlParameter("@COLUMNCOUNT",SqlDbType.Int),
					new SqlParameter("@LASTUPDATED",SqlDbType.DateTime),
					new SqlParameter("@ISLOCKED",SqlDbType.Int),
					new SqlParameter("@LASTLOCKEDSTATUSCHANGEDAT",SqlDbType.DateTime),
					new SqlParameter("@ISDOWNFORMAINTENANCE",SqlDbType.Int),
					new SqlParameter("@LASTDOWNFORMAINTENANCEAT",SqlDbType.DateTime),
					new SqlParameter("@SERVEASSTARTPAGEAFTERLOGIN",SqlDbType.Int),
					new SqlParameter("@ORDERNO",SqlDbType.Int),
                    new SqlParameter("@URL",SqlDbType.NVarChar),
                    new SqlParameter("@ID",SqlDbType.Int)
					};
                parameters[0].Value = entity.TITLE;
                parameters[1].Value = entity.USERID.Value;
                parameters[2].Value = entity.CREATEDDATE.Value;
                parameters[3].Value = entity.VERSIONNO.Value;
                parameters[4].Value = entity.LAYOUTTYPE.Value;
                parameters[5].Value = entity.PAGETYPE.Value;
                parameters[6].Value = entity.COLUMNCOUNT.Value;
                parameters[7].Value = entity.LASTUPDATED;
                parameters[8].Value = entity.ISLOCKED;
                parameters[9].Value = entity.LASTLOCKEDSTATUSCHANGEDAT;
                parameters[10].Value = entity.ISDOWNFORMAINTENANCE;
                parameters[11].Value = entity.LASTDOWNFORMAINTENANCEAT;
                parameters[12].Value = entity.SERVEASSTARTPAGEAFTERLOGIN;
                parameters[13].Value = entity.ORDERNO.Value;
                parameters[14].Value = entity.URL;
                parameters[15].Value = entity.ID.Value;

                _sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update PAGE set ");
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
                string strSql = "delete from PAGE where ID=" + ID;
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
                string strSql = "delete from PAGE where ID in (" + ID + ")";
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

            public override void Delete(PageEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from PAGE ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.ID;
                _sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override PageEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from PAGE ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = _sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    PageEntity entity = new PageEntity();
                    if (!Convert.IsDBNull(row["ID"]))
                    {
                        entity.ID = Convert.ToInt32(row["ID"]);
                    }
                    entity.TITLE = row["TITLE"].ToString();
                    if (!Convert.IsDBNull(row["USERID"]))
                    {
                        entity.USERID = Convert.ToInt32(row["USERID"]);
                    }
                    if (!Convert.IsDBNull(row["CREATEDDATE"]))
                    {
                        entity.CREATEDDATE = Convert.ToDateTime(row["CREATEDDATE"]);
                    }
                    if (!Convert.IsDBNull(row["VERSIONNO"]))
                    {
                        entity.VERSIONNO = Convert.ToInt32(row["VERSIONNO"]);
                    }
                    if (!Convert.IsDBNull(row["LAYOUTTYPE"]))
                    {
                        entity.LAYOUTTYPE = Convert.ToInt32(row["LAYOUTTYPE"]);
                    }
                    if (!Convert.IsDBNull(row["PAGETYPE"]))
                    {
                        entity.PAGETYPE = Convert.ToInt32(row["PAGETYPE"]);
                    }
                    if (!Convert.IsDBNull(row["COLUMNCOUNT"]))
                    {
                        entity.COLUMNCOUNT = Convert.ToInt32(row["COLUMNCOUNT"]);
                    }
                    if (!Convert.IsDBNull(row["LASTUPDATED"]))
                    {
                        entity.LASTUPDATED = Convert.ToDateTime(row["LASTUPDATED"]);
                    }
                    if (!Convert.IsDBNull(row["ISLOCKED"]))
                    {
                        entity.ISLOCKED = Convert.ToInt32(row["ISLOCKED"].ToString());
                    }
                    if (!Convert.IsDBNull(row["LASTLOCKEDSTATUSCHANGEDAT"]))
                    {
                        entity.LASTLOCKEDSTATUSCHANGEDAT = Convert.ToDateTime(row["LASTLOCKEDSTATUSCHANGEDAT"]);
                    }
                    if (!Convert.IsDBNull(row["ISDOWNFORMAINTENANCE"]))
                    {
                        entity.ISDOWNFORMAINTENANCE = Convert.ToInt32(row["ISDOWNFORMAINTENANCE"]);
                    }
                    if (!Convert.IsDBNull(row["LASTDOWNFORMAINTENANCEAT"]))
                    {
                        entity.LASTDOWNFORMAINTENANCEAT = Convert.ToDateTime(row["LASTDOWNFORMAINTENANCEAT"]);
                    }
                    if (!Convert.IsDBNull(row["SERVEASSTARTPAGEAFTERLOGIN"]))
                    {
                        entity.SERVEASSTARTPAGEAFTERLOGIN = Convert.ToInt32(row["SERVEASSTARTPAGEAFTERLOGIN"]);
                    }
                    if (!Convert.IsDBNull(row["ORDERNO"]))
                    {
                        entity.ORDERNO = Convert.ToInt32(row["ORDERNO"]);
                    }
                    entity.URL = row["URL"].ToString();
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public List<PageEntity> FindPagesByRoleId(string roleid)
            {
                string strsql = "SELECT P.* FROM PAGE P,PAGEOFROLE PR where PR.ROLEID in ("
                + roleid + ") AND PR.PAGEID = P.ID AND P.USERID=-1";

                DataSet ds = _sqlHelper.ExecuteDateSet(strsql, null);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<PageEntity> list = new List<PageEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        PageEntity entity = new PageEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        entity.TITLE = row["TITLE"].ToString();
                        if (!Convert.IsDBNull(row["USERID"]))
                        {
                            entity.USERID = Convert.ToInt32(row["USERID"]);
                        }
                        if (!Convert.IsDBNull(row["CREATEDDATE"]))
                        {
                            entity.CREATEDDATE = Convert.ToDateTime(row["CREATEDDATE"]);
                        }
                        if (!Convert.IsDBNull(row["VERSIONNO"]))
                        {
                            entity.VERSIONNO = Convert.ToInt32(row["VERSIONNO"]);
                        }
                        if (!Convert.IsDBNull(row["LAYOUTTYPE"]))
                        {
                            entity.LAYOUTTYPE = Convert.ToInt32(row["LAYOUTTYPE"]);
                        }
                        if (!Convert.IsDBNull(row["PAGETYPE"]))
                        {
                            entity.PAGETYPE = Convert.ToInt32(row["PAGETYPE"]);
                        }
                        if (!Convert.IsDBNull(row["COLUMNCOUNT"]))
                        {
                            entity.COLUMNCOUNT = Convert.ToInt32(row["COLUMNCOUNT"]);
                        }
                        if (!Convert.IsDBNull(row["LASTUPDATED"]))
                        {
                            entity.LASTUPDATED = Convert.ToDateTime(row["LASTUPDATED"]);
                        }
                        if (!Convert.IsDBNull(row["ISLOCKED"]))
                        {
                            entity.ISLOCKED = Convert.ToInt32(row["ISLOCKED"]);
                        }
                        if (!Convert.IsDBNull(row["LASTLOCKEDSTATUSCHANGEDAT"]))
                        {
                            entity.LASTLOCKEDSTATUSCHANGEDAT = Convert.ToDateTime(row["LASTLOCKEDSTATUSCHANGEDAT"]);
                        }
                        if (!Convert.IsDBNull(row["ISDOWNFORMAINTENANCE"]))
                        {
                            entity.ISDOWNFORMAINTENANCE = Convert.ToInt32(row["ISDOWNFORMAINTENANCE"]);
                        }
                        if (!Convert.IsDBNull(row["LASTDOWNFORMAINTENANCEAT"]))
                        {
                            entity.LASTDOWNFORMAINTENANCEAT = Convert.ToDateTime(row["LASTDOWNFORMAINTENANCEAT"]);
                        }
                        if (!Convert.IsDBNull(row["SERVEASSTARTPAGEAFTERLOGIN"]))
                        {
                            entity.SERVEASSTARTPAGEAFTERLOGIN = Convert.ToInt32(row["SERVEASSTARTPAGEAFTERLOGIN"]);
                        }
                        if (!Convert.IsDBNull(row["ORDERNO"]))
                        {
                            entity.ORDERNO = Convert.ToInt32(row["ORDERNO"]);
                        }
                        entity.URL = row["URL"].ToString();
                        list.Add(entity);
                    }

                    return list;
                }
                else
                {
                    return null;
                }
            }

            public override List<PageEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM PAGE ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = _sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<PageEntity> list = new List<PageEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        PageEntity entity = new PageEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        entity.TITLE = row["TITLE"].ToString();
                        if (!Convert.IsDBNull(row["USERID"]))
                        {
                            entity.USERID = Convert.ToInt32(row["USERID"]);
                        }
                        if (!Convert.IsDBNull(row["CREATEDDATE"]))
                        {
                            entity.CREATEDDATE = Convert.ToDateTime(row["CREATEDDATE"]);
                        }
                        if (!Convert.IsDBNull(row["VERSIONNO"]))
                        {
                            entity.VERSIONNO = Convert.ToInt32(row["VERSIONNO"]);
                        }
                        if (!Convert.IsDBNull(row["LAYOUTTYPE"]))
                        {
                            entity.LAYOUTTYPE = Convert.ToInt32(row["LAYOUTTYPE"]);
                        }
                        if (!Convert.IsDBNull(row["PAGETYPE"]))
                        {
                            entity.PAGETYPE = Convert.ToInt32(row["PAGETYPE"]);
                        }
                        if (!Convert.IsDBNull(row["COLUMNCOUNT"]))
                        {
                            entity.COLUMNCOUNT = Convert.ToInt32(row["COLUMNCOUNT"]);
                        }
                        if (!Convert.IsDBNull(row["LASTUPDATED"]))
                        {
                            entity.LASTUPDATED = Convert.ToDateTime(row["LASTUPDATED"]);
                        }
                        if (!Convert.IsDBNull(row["ISLOCKED"]))
                        {
                            entity.ISLOCKED = Convert.ToInt32(row["ISLOCKED"]);
                        }
                        if (!Convert.IsDBNull(row["LASTLOCKEDSTATUSCHANGEDAT"]))
                        {
                            entity.LASTLOCKEDSTATUSCHANGEDAT = Convert.ToDateTime(row["LASTLOCKEDSTATUSCHANGEDAT"]);
                        }
                        if (!Convert.IsDBNull(row["ISDOWNFORMAINTENANCE"]))
                        {
                            entity.ISDOWNFORMAINTENANCE = Convert.ToInt32(row["ISDOWNFORMAINTENANCE"]);
                        }
                        if (!Convert.IsDBNull(row["LASTDOWNFORMAINTENANCEAT"]))
                        {
                            entity.LASTDOWNFORMAINTENANCEAT = Convert.ToDateTime(row["LASTDOWNFORMAINTENANCEAT"]);
                        }
                        if (!Convert.IsDBNull(row["SERVEASSTARTPAGEAFTERLOGIN"]))
                        {
                            entity.SERVEASSTARTPAGEAFTERLOGIN = Convert.ToInt32(row["SERVEASSTARTPAGEAFTERLOGIN"]);
                        }
                        if (!Convert.IsDBNull(row["ORDERNO"]))
                        {
                            entity.ORDERNO = Convert.ToInt32(row["ORDERNO"]);
                        }
                        entity.URL = row["URL"].ToString();
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
                strSql.Append(" FROM PAGE");
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
                string sql = "select count(*) from PAGE ";
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
                PagerSql.Append("FROM (SELECT * FROM PAGE ");
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

