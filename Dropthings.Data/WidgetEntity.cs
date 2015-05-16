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
    public partial class WidgetEntity
    {
        private SqlHelper sqlHelper;

        #region const fields
        public const string DBName = "SentimentConnStr";
        public const string TableName = "WIDGET";
        public const string PrimaryKey = "PK_WIDGET";
        #endregion

        #region columns
        public struct Columns
        {
            public const string ID = "ID";
            public const string NAME = "NAME";
            public const string URL = "URL";
            public const string DESCRIPTION = "DESCRIPTION";
            public const string DEFAULTSTATE = "DEFAULTSTATE";
            public const string ICON = "ICON";
            public const string ORDERNO = "ORDERNO";
            public const string ROLENAME = "ROLENAME";
            public const string ISLOCKED = "ISLOCKED";
            public const string ISDEFAULT = "ISDEFAULT";
            public const string CREATEDDATE = "CREATEDDATE";
            public const string VERSIONNO = "VERSIONNO";
            public const string LASTUPDATE = "LASTUPDATE";
            public const string WIDGETTYPE = "WIDGETTYPE";
        }
        #endregion

        #region constructors
        public WidgetEntity()
        {
            sqlHelper = new SqlHelper(DBName);
        }

        public WidgetEntity(int id, string name, string url, string description, string defaultstate, string icon, int orderno, string rolename, bool islocked, bool isdefault, DateTime createddate, int versionno, DateTime lastupdate, int widgettype)
        {
            this.ID = id;

            this.NAME = name;

            this.URL = url;

            this.DESCRIPTION = description;

            this.DEFAULTSTATE = defaultstate;

            this.ICON = icon;

            this.ORDERNO = orderno;

            this.ROLENAME = rolename;

            this.ISLOCKED = islocked;

            this.ISDEFAULT = isdefault;

            this.CREATEDDATE = createddate;

            this.VERSIONNO = versionno;

            this.LASTUPDATE = lastupdate;

            this.WIDGETTYPE = widgettype;

        }
        #endregion

        #region Properties

        public int? ID
        {
            get;
            set;
        }


        public string NAME
        {
            get;
            set;
        }


        public string URL
        {
            get;
            set;
        }


        public string DESCRIPTION
        {
            get;
            set;
        }


        public string DEFAULTSTATE
        {
            get;
            set;
        }


        public string ICON
        {
            get;
            set;
        }


        public int? ORDERNO
        {
            get;
            set;
        }


        public string ROLENAME
        {
            get;
            set;
        }


        public bool? ISLOCKED
        {
            get;
            set;
        }


        public bool? ISDEFAULT
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


        public DateTime LASTUPDATE
        {
            get;
            set;
        }


        public int? WIDGETTYPE
        {
            get;
            set;
        }

        #endregion

        public class WIDGETDAO : SqlDAO<WidgetEntity>
        {
            private SqlHelper sqlHelper;

            public const string DBName = "SentimentConnStr";

            public WIDGETDAO()
            {
                sqlHelper = new SqlHelper(DBName);
            }



            public override void Add(WidgetEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into WIDGET(");
                strSql.Append("NAME,URL,DESCRIPTION,DEFAULTSTATE,ICON,ORDERNO,ROLENAME,ISLOCKED,ISDEFAULT,CREATEDDATE,VERSIONNO,LASTUPDATE,WIDGETTYPE)");
                strSql.Append(" values (");
                strSql.Append(":NAME,:URL,:DESCRIPTION,:DEFAULTSTATE,:ICON,:ORDERNO,:ROLENAME,:ISLOCKED,:ISDEFAULT,:CREATEDDATE,:VERSIONNO,:LASTUPDATE,:WIDGETTYPE)");
                SqlParameter[] parameters = {
					new SqlParameter("@NAME",SqlDbType.NVarChar),
					new SqlParameter("@URL",SqlDbType.NVarChar),
					new SqlParameter("@DESCRIPTION",SqlDbType.NVarChar),
					new SqlParameter("@DEFAULTSTATE",SqlDbType.NVarChar),
					new SqlParameter("@ICON",SqlDbType.VarChar),
					new SqlParameter("@ORDERNO",SqlDbType.Int),
					new SqlParameter("@ROLENAME",SqlDbType.VarChar),
					new SqlParameter("@ISLOCKED",SqlDbType.Bit),
					new SqlParameter("@ISDEFAULT",SqlDbType.Bit),
					new SqlParameter("@CREATEDDATE",SqlDbType.DateTime),
					new SqlParameter("@VERSIONNO",SqlDbType.Int),
					new SqlParameter("@LASTUPDATE",SqlDbType.DateTime),
					new SqlParameter("@WIDGETTYPE",SqlDbType.Int)
					};
                parameters[0].Value = entity.NAME;
                parameters[1].Value = entity.URL;
                parameters[2].Value = entity.DESCRIPTION;
                parameters[3].Value = entity.DEFAULTSTATE;
                parameters[4].Value = entity.ICON;
                parameters[5].Value = entity.ORDERNO;
                parameters[6].Value = entity.ROLENAME;
                parameters[7].Value = entity.ISLOCKED;
                parameters[8].Value = entity.ISDEFAULT;
                parameters[9].Value = entity.CREATEDDATE;
                parameters[10].Value = entity.VERSIONNO;
                parameters[11].Value = entity.LASTUPDATE;
                parameters[12].Value = entity.WIDGETTYPE;
                //parameters[13].Direction = ParameterDirection.Output;

                sqlHelper.ExecuteSql(strSql.ToString(), parameters);
                //object obj = sqlHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters);
                entity.ID = ReturnNewRowId();
            }

            private int ReturnNewRowId()
            {
                string sql = "select WIDGET_ID_SEQ.currval NEWID from dual";
                object NewId = sqlHelper.GetSingle(sql, null);
                return Convert.ToInt32(NewId);
            }

            public override void Update(WidgetEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update WIDGET set ");
                strSql.Append("NAME=@NAME,");
                strSql.Append("URL=@URL,");
                strSql.Append("DESCRIPTION=@DESCRIPTION,");
                strSql.Append("DEFAULTSTATE=@DEFAULTSTATE,");
                strSql.Append("ICON=@ICON,");
                strSql.Append("ORDERNO=@ORDERNO,");
                strSql.Append("ROLENAME=@ROLENAME,");
                strSql.Append("ISLOCKED=@ISLOCKED,");
                strSql.Append("ISDEFAULT=@ISDEFAULT,");
                strSql.Append("CREATEDDATE=@CREATEDDATE,");
                strSql.Append("VERSIONNO=@VERSIONNO,");
                strSql.Append("LASTUPDATE=@LASTUPDATE,");
                strSql.Append("WIDGETTYPE=@WIDGETTYPE");

                strSql.Append(" where ID=@ID");
                SqlParameter[]parameters = {
					new SqlParameter("@NAME",SqlDbType.NVarChar),
					new SqlParameter("@URL",SqlDbType.NVarChar),
					new SqlParameter("@DESCRIPTION",SqlDbType.NVarChar),
					new SqlParameter("@DEFAULTSTATE",SqlDbType.NVarChar),
					new SqlParameter("@ICON",SqlDbType.VarChar),
					new SqlParameter("@ORDERNO",SqlDbType.Int),
					new SqlParameter("@ROLENAME",SqlDbType.VarChar),
					new SqlParameter("@ISLOCKED",SqlDbType.Bit),
					new SqlParameter("@ISDEFAULT",SqlDbType.Bit),
					new SqlParameter("@CREATEDDATE",SqlDbType.DateTime),
					new SqlParameter("@VERSIONNO",SqlDbType.Int),
					new SqlParameter("@LASTUPDATE",SqlDbType.DateTime),
					new SqlParameter("@WIDGETTYPE",SqlDbType.Int),
   					new SqlParameter("@ID",SqlDbType.Int),
					};
                parameters[0].Value = entity.NAME;
                parameters[1].Value = entity.URL;
                parameters[2].Value = entity.DESCRIPTION;
                parameters[3].Value = entity.DEFAULTSTATE;
                parameters[4].Value = entity.ICON;
                parameters[5].Value = entity.ORDERNO;
                parameters[6].Value = entity.ROLENAME;
                parameters[7].Value = entity.ISLOCKED;
                parameters[8].Value = entity.ISDEFAULT;
                parameters[9].Value = entity.CREATEDDATE.Value;
                parameters[10].Value = entity.VERSIONNO;
                parameters[11].Value = entity.LASTUPDATE;
                parameters[12].Value = entity.WIDGETTYPE;
                parameters[13].Value = entity.ID;

                sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update WIDGET set ");
                    StrSql.Append(ColumnName + "='" + Value + "'");
                    StrSql.Append(" where ID=" + ID);
                    sqlHelper.ExecuteSql(StrSql.ToString(), null);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public bool Delete(int ID)
            {
                string strSql = "delete from WIDGET where ID=" + ID;
                try
                {
                    sqlHelper.ExecuteSql(strSql, null);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public bool Delete(string ID)
            {
                string strSql = "delete from WIDGET where ID in (" + ID + ")";
                try
                {
                    sqlHelper.ExecuteSql(strSql, null);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public override void Delete(WidgetEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from WIDGET ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[]parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.ID;
                sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public List<WidgetEntity> GetInstanceWidgetsByTabId(int tabId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select W.*");
                strSql.Append(" FROM WIDGET W , WIDGETINSTANCE WI, PAGE P, COLUMNOFPAGE C, WIDGETZONE WZ");
                strSql.Append(" WHERE P.ID=@ID AND P.ID=C.PAGEID AND WI.WIDGETID=W.ID AND WZ.ID=C.WIDGETZONEID AND WI.WIDGETZONEID=WZ.ID");
                SqlParameter[]parameters ={
					new SqlParameter("@ID",SqlDbType.Int)};
                parameters[0].Value = tabId;

                DataSet ds = sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<WidgetEntity> list = new List<WidgetEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        WidgetEntity entity = new WidgetEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        entity.NAME = row["NAME"].ToString();
                        entity.URL = row["URL"].ToString();
                        entity.DESCRIPTION = row["DESCRIPTION"].ToString();
                        entity.DEFAULTSTATE = row["DEFAULTSTATE"].ToString();
                        entity.ICON = row["ICON"].ToString();
                        if (!Convert.IsDBNull(row["ORDERNO"]))
                        {
                            entity.ORDERNO = Convert.ToInt32(row["ORDERNO"]);
                        }
                        entity.ROLENAME = row["ROLENAME"].ToString();
                        if (!Convert.IsDBNull(row["ISLOCKED"]))
                        {
                            entity.ISLOCKED = Convert.ToBoolean(row["ISLOCKED"]);
                        }
                        if (!Convert.IsDBNull(row["ISDEFAULT"]))
                        {
                            entity.ISDEFAULT = Convert.ToBoolean(row["ISDEFAULT"]);
                        }
                        if (!Convert.IsDBNull(row["CREATEDDATE"]))
                        {
                            entity.CREATEDDATE = Convert.ToDateTime(row["CREATEDDATE"]);
                        }
                        if (!Convert.IsDBNull(row["VERSIONNO"]))
                        {
                            entity.VERSIONNO = Convert.ToInt32(row["VERSIONNO"]);
                        }
                        if (!Convert.IsDBNull(row["LASTUPDATE"]))
                        {
                            entity.LASTUPDATE = Convert.ToDateTime(row["LASTUPDATE"]);
                        }
                        if (!Convert.IsDBNull(row["WIDGETTYPE"]))
                        {
                            entity.WIDGETTYPE = Convert.ToInt32(row["WIDGETTYPE"]);
                        }

                        list.Add(entity);
                    }

                    return list;
                }
                return null;
            }
            public void DeleteInstanceByWidgetID(int widgetID)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from WIDGETINSTANCE ");
                strSql.Append(" where WIDGETID=@WIDGETID");
                SqlParameter[]parameters = {
						new SqlParameter("@WIDGETID", SqlDbType.Int)
					};
                parameters[0].Value = widgetID;
                sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override WidgetEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from WIDGET ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[]parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    WidgetEntity entity = new WidgetEntity();
                    if (!Convert.IsDBNull(row["ID"]))
                    {
                        entity.ID = Convert.ToInt32(row["ID"]);
                    }
                    entity.NAME = row["NAME"].ToString();
                    entity.URL = row["URL"].ToString();
                    entity.DESCRIPTION = row["DESCRIPTION"].ToString();
                    entity.DEFAULTSTATE = row["DEFAULTSTATE"].ToString();
                    entity.ICON = row["ICON"].ToString();
                    if (!Convert.IsDBNull(row["ORDERNO"]))
                    {
                        entity.ORDERNO = Convert.ToInt32(row["ORDERNO"]);
                    }
                    entity.ROLENAME = row["ROLENAME"].ToString();
                    if (!Convert.IsDBNull(row["ISLOCKED"]))
                    {
                        entity.ISLOCKED = Convert.ToBoolean(row["ISLOCKED"]);
                    }
                    if (!Convert.IsDBNull(row["ISDEFAULT"]))
                    {
                        entity.ISDEFAULT = Convert.ToBoolean(row["ISDEFAULT"]);
                    }
                    if (!Convert.IsDBNull(row["CREATEDDATE"]))
                    {
                        entity.CREATEDDATE = Convert.ToDateTime(row["CREATEDDATE"]);
                    }
                    if (!Convert.IsDBNull(row["VERSIONNO"]))
                    {
                        entity.VERSIONNO = Convert.ToInt32(row["VERSIONNO"]);
                    }
                    if (!Convert.IsDBNull(row["LASTUPDATE"]))
                    {
                        entity.LASTUPDATE = Convert.ToDateTime(row["LASTUPDATE"]);
                    }
                    if (!Convert.IsDBNull(row["WIDGETTYPE"]))
                    {
                        entity.WIDGETTYPE = Convert.ToInt32(row["WIDGETTYPE"]);
                    }
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public override List<WidgetEntity> Find(string strWhere, SqlParameter[]parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM WIDGET ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<WidgetEntity> list = new List<WidgetEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        WidgetEntity entity = new WidgetEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        entity.NAME = row["NAME"].ToString();
                        entity.URL = row["URL"].ToString();
                        entity.DESCRIPTION = row["DESCRIPTION"].ToString();
                        entity.DEFAULTSTATE = row["DEFAULTSTATE"].ToString();
                        entity.ICON = row["ICON"].ToString();
                        if (!Convert.IsDBNull(row["ORDERNO"]))
                        {
                            entity.ORDERNO = Convert.ToInt32(row["ORDERNO"]);
                        }
                        entity.ROLENAME = row["ROLENAME"].ToString();
                        if (!Convert.IsDBNull(row["ISLOCKED"]))
                        {
                            entity.ISLOCKED = Convert.ToBoolean(row["ISLOCKED"]);
                        }
                        if (!Convert.IsDBNull(row["ISDEFAULT"]))
                        {
                            entity.ISDEFAULT = Convert.ToBoolean(row["ISDEFAULT"]);
                        }
                        if (!Convert.IsDBNull(row["CREATEDDATE"]))
                        {
                            entity.CREATEDDATE = Convert.ToDateTime(row["CREATEDDATE"]);
                        }
                        if (!Convert.IsDBNull(row["VERSIONNO"]))
                        {
                            entity.VERSIONNO = Convert.ToInt32(row["VERSIONNO"]);
                        }
                        if (!Convert.IsDBNull(row["LASTUPDATE"]))
                        {
                            entity.LASTUPDATE = Convert.ToDateTime(row["LASTUPDATE"]);
                        }
                        if (!Convert.IsDBNull(row["WIDGETTYPE"]))
                        {
                            entity.WIDGETTYPE = Convert.ToInt32(row["WIDGETTYPE"]);
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

            public override DataSet GetDataSet(string strWhere, SqlParameter[]param)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM WIDGET");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                return sqlHelper.ExecuteDateSet(strSql.ToString(), param);
            }

            #region paging methods

            /// <summary>
            /// 获取分页记录总数
            /// </summary>
            /// <param name="where">条件，等同于GetPaer()方法的where</param>
            /// <returns>返回记录总数</returns>
            public int GetPagerRowsCount(string where, SqlParameter[]param)
            {
                string sql = "select count(*) from WIDGET ";
                if (!string.IsNullOrEmpty(where))
                {
                    sql += "where " + where;
                }

                object obj = sqlHelper.GetSingle(sql, param);

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
            public DataTable GetPager(string where, SqlParameter[]param, string orderBy, int pageSize, int pageNumber)
            {
                int startNumber = pageSize * (pageNumber - 1) + 1;
                int endNumber = pageSize * pageNumber;

                StringBuilder PagerSql = new StringBuilder();
                PagerSql.Append("SELECT * FROM (");
                PagerSql.Append(" SELECT A.*, ROWNUM RN ");
                PagerSql.Append("FROM (SELECT * FROM WIDGET ");
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

                return sqlHelper.ExecuteDateSet(PagerSql.ToString(), param).Tables[0];
            }

            #endregion

        }
    }
}

