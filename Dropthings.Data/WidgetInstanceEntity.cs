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
    public partial class WidgetInstanceEntity
    {
        private SqlHelper sqlHelper;

        #region const fields
        public const string DBName = "SentimentConnStr";
        public const string TableName = "WIDGETINSTANCE";
        public const string PrimaryKey = "PK_WIDGETINSTANCE";
        #endregion

        #region columns
        public struct Columns
        {
            public const string ID = "ID";
            public const string WIDGETZONEID = "WIDGETZONEID";
            public const string WIDGETID = "WIDGETID";
            public const string ORDERNO = "ORDERNO";
            public const string EXPANDED = "EXPANDED";
            public const string MAXIMIZED = "MAXIMIZED";
            public const string RESIZED = "RESIZED";
            public const string WIDTH = "WIDTH";
            public const string HEIGHT = "HEIGHT";
            public const string TITLE = "TITLE";
            public const string STATE = "STATE";
            public const string VERSIONNO = "VERSIONNO";
            public const string CREATEDDATE = "CREATEDDATE";
            public const string LASTUPDATE = "LASTUPDATE";
        }
        #endregion

        #region constructors
        public WidgetInstanceEntity()
        {
            sqlHelper = new SqlHelper(DBName);
        }

        public WidgetInstanceEntity(int id, int widgetzoneid, int widgetid, int orderno, bool expanded, bool maximized, bool resized, int width, int height, string title, string state, int versionno, DateTime createddate, DateTime lastupdate)
        {
            this.ID = id;

            this.WIDGETZONEID = widgetzoneid;

            this.WIDGETID = widgetid;

            this.ORDERNO = orderno;

            this.EXPANDED = expanded;

            this.MAXIMIZED = maximized;

            this.RESIZED = resized;

            this.WIDTH = width;

            this.HEIGHT = height;

            this.TITLE = title;

            this.STATE = state;

            this.VERSIONNO = versionno;

            this.CREATEDDATE = createddate;

            this.LASTUPDATE = lastupdate;

        }
        #endregion

        #region Properties
        public WidgetEntity Widget
        {
            set;
            get;
        }
        public WidgetZoneEntity WidgetZone
        {
            set;
            get;
        }
        //public EntityReference<WidgetZoneEntity> WidgetZoneReference
        //{
        //    set;
        //    get;
        //}
        public int? ID
        {
            get;
            set;
        }


        public int? WIDGETZONEID
        {
            get;
            set;
        }


        public int? WIDGETID
        {
            get;
            set;
        }


        public int? ORDERNO
        {
            get;
            set;
        }


        public bool? EXPANDED
        {
            get;
            set;
        }


        public bool? MAXIMIZED
        {
            get;
            set;
        }


        public bool? RESIZED
        {
            get;
            set;
        }


        public int? WIDTH
        {
            get;
            set;
        }


        public int? HEIGHT
        {
            get;
            set;
        }


        public string TITLE
        {
            get;
            set;
        }


        public string STATE
        {
            get;
            set;
        }


        public int? VERSIONNO
        {
            get;
            set;
        }


        public DateTime? CREATEDDATE
        {
            get;
            set;
        }


        public DateTime? LASTUPDATE
        {
            get;
            set;
        }

        #endregion

        public class WIDGETINSTANCEDAO : SqlDAO<WidgetInstanceEntity>
        {
            private SqlHelper sqlHelper;
            public const string DBName = "SentimentConnStr";

            public WIDGETINSTANCEDAO()
            {
                sqlHelper = new SqlHelper(DBName);
            }

            public string GetWidgetInstanceOwnerName(int widgetInstanceId)
            {
                string result = string.Empty;

                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT U.USERNAME FROM USERS U ,WIDGETZONE WZ, WIDGETINSTANCE WI,COLUMNOFPAGE C,PAGE P ");
                strSql.Append("WHERE WI.ID=:ID AND WI.WIDGETZONEID=WZ.ID AND C.WIDGETZONEID=WZ.ID AND P.ID=C.PAGEID AND P.USERID=U.USERID ");
                SqlParameter[] parameters = {
					new SqlParameter(":ID",SqlDbType.Int)};
                parameters[0].Value = widgetInstanceId;

                result = Convert.ToString(sqlHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));

                return result;
            }

            public override void Add(WidgetInstanceEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into WIDGETINSTANCE(");
                strSql.Append("WIDGETZONEID,WIDGETID,ORDERNO,EXPANDED,MAXIMIZED,RESIZED,WIDTH,HEIGHT,TITLE,STATE,VERSIONNO,CREATEDDATE,LASTUPDATE)");
                strSql.Append(" values (");
                strSql.Append(":WIDGETZONEID,:WIDGETID,:ORDERNO,:EXPANDED,:MAXIMIZED,:RESIZED,:WIDTH,:HEIGHT,:TITLE,:STATE,:VERSIONNO,:CREATEDDATE,:LASTUPDATE)");
                SqlParameter[] parameters = {
					new SqlParameter(":WIDGETZONEID",SqlDbType.Int),
					new SqlParameter(":WIDGETID",SqlDbType.Int),
					new SqlParameter(":ORDERNO",SqlDbType.Int),
					new SqlParameter(":EXPANDED",SqlDbType.Bit),
					new SqlParameter(":MAXIMIZED",SqlDbType.Bit),
					new SqlParameter(":RESIZED",SqlDbType.Bit),
					new SqlParameter(":WIDTH",SqlDbType.Int),
					new SqlParameter(":HEIGHT",SqlDbType.Int),
					new SqlParameter(":TITLE",SqlDbType.NVarChar),
					new SqlParameter(":STATE",SqlDbType.NVarChar),
					new SqlParameter(":VERSIONNO",SqlDbType.Int),
					new SqlParameter(":CREATEDDATE",SqlDbType.DateTime),
					new SqlParameter(":LASTUPDATE",SqlDbType.DateTime)
					};
                parameters[0].Value = entity.WIDGETZONEID;
                parameters[1].Value = entity.WIDGETID;
                parameters[2].Value = entity.ORDERNO;
                parameters[3].Value = entity.EXPANDED;
                parameters[4].Value = entity.MAXIMIZED;
                parameters[5].Value = entity.RESIZED;
                parameters[6].Value = entity.WIDTH;
                parameters[7].Value = entity.HEIGHT;
                parameters[8].Value = entity.TITLE;
                parameters[9].Value = entity.STATE;
                parameters[10].Value = entity.VERSIONNO;
                parameters[11].Value = entity.CREATEDDATE;
                parameters[12].Value = entity.LASTUPDATE;

                sqlHelper.ExecuteSql(strSql.ToString(), parameters);
                entity.ID = ReturnNewRowId();
            }

            private int ReturnNewRowId()
            {
                string sql = "select WIDGETINSTANCE_ID_SEQ.currval NEWID from dual";
                object NewId = sqlHelper.GetSingle(sql, null);
                return Convert.ToInt32(NewId);
            }

            public override void Update(WidgetInstanceEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update WIDGETINSTANCE set ");
                strSql.Append("WIDGETZONEID=:WIDGETZONEID,");
                strSql.Append("WIDGETID=:WIDGETID,");
                strSql.Append("ORDERNO=:ORDERNO,");
                strSql.Append("EXPANDED=:EXPANDED,");
                strSql.Append("MAXIMIZED=:MAXIMIZED,");
                strSql.Append("RESIZED=:RESIZED,");
                strSql.Append("WIDTH=:WIDTH,");
                strSql.Append("HEIGHT=:HEIGHT,");
                strSql.Append("TITLE=:TITLE,");
                strSql.Append("STATE=:STATE,");
                strSql.Append("VERSIONNO=:VERSIONNO,");
                strSql.Append("CREATEDDATE=:CREATEDDATE,");
                strSql.Append("LASTUPDATE=:LASTUPDATE");

                strSql.Append(" where ID=:ID");
                SqlParameter[] parameters = {
					new SqlParameter(":WIDGETZONEID",SqlDbType.Int),
					new SqlParameter(":WIDGETID",SqlDbType.Int),
					new SqlParameter(":ORDERNO",SqlDbType.Int),
					new SqlParameter(":EXPANDED",SqlDbType.Bit),
					new SqlParameter(":MAXIMIZED",SqlDbType.Bit),
					new SqlParameter(":RESIZED",SqlDbType.Bit),
					new SqlParameter(":WIDTH",SqlDbType.Int),
					new SqlParameter(":HEIGHT",SqlDbType.Int),
					new SqlParameter(":TITLE",SqlDbType.NVarChar),
					new SqlParameter(":STATE",SqlDbType.NVarChar),
					new SqlParameter(":VERSIONNO",SqlDbType.Int),
					new SqlParameter(":CREATEDDATE",SqlDbType.DateTime),
					new SqlParameter(":LASTUPDATE",SqlDbType.DateTime),
                    new SqlParameter(":ID",SqlDbType.Int)
					};
                parameters[0].Value = entity.WIDGETZONEID;
                parameters[1].Value = entity.WIDGETID;
                parameters[2].Value = entity.ORDERNO;
                parameters[3].Value = entity.EXPANDED;
                parameters[4].Value = entity.MAXIMIZED;
                parameters[5].Value = entity.RESIZED;
                parameters[6].Value = entity.WIDTH;
                parameters[7].Value = entity.HEIGHT;
                parameters[8].Value = entity.TITLE;
                parameters[9].Value = entity.STATE;
                parameters[10].Value = entity.VERSIONNO;
                parameters[11].Value = entity.CREATEDDATE;
                parameters[12].Value = entity.LASTUPDATE;
                parameters[13].Value = entity.ID;

                sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update WIDGETINSTANCE set ");
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
                string strSql = "delete from WIDGETINSTANCE where ID=" + ID;
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
                string strSql = "delete from WIDGETINSTANCE where ID in (" + ID + ")";
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

            public override void Delete(WidgetInstanceEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from WIDGETINSTANCE ");
                strSql.Append(" where ID=:primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter(":primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.ID;
                sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override WidgetInstanceEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from WIDGETINSTANCE ");
                strSql.Append(" where ID=:primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter(":primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    WidgetInstanceEntity entity = new WidgetInstanceEntity();
                    if (!Convert.IsDBNull(row["ID"]))
                    {
                        entity.ID = Convert.ToInt32(row["ID"]);
                    }
                    if (!Convert.IsDBNull(row["WIDGETZONEID"]))
                    {
                        entity.WIDGETZONEID = Convert.ToInt32(row["WIDGETZONEID"]);
                    }
                    if (!Convert.IsDBNull(row["WIDGETID"]))
                    {
                        entity.WIDGETID = Convert.ToInt32(row["WIDGETID"]);
                    }
                    if (!Convert.IsDBNull(row["ORDERNO"]))
                    {
                        entity.ORDERNO = Convert.ToInt32(row["ORDERNO"]);
                    }
                    if (!Convert.IsDBNull(row["EXPANDED"]))
                    {
                        entity.EXPANDED = Convert.ToBoolean(row["EXPANDED"]);
                    }
                    if (!Convert.IsDBNull(row["MAXIMIZED"]))
                    {
                        entity.MAXIMIZED = Convert.ToBoolean(row["MAXIMIZED"]);
                    }
                    if (!Convert.IsDBNull(row["RESIZED"]))
                    {
                        entity.RESIZED = Convert.ToBoolean(row["RESIZED"]);
                    }
                    if (!Convert.IsDBNull(row["WIDTH"]))
                    {
                        entity.WIDTH = Convert.ToInt32(row["WIDTH"]);
                    }
                    if (!Convert.IsDBNull(row["HEIGHT"]))
                    {
                        entity.HEIGHT = Convert.ToInt32(row["HEIGHT"]);
                    }
                    entity.TITLE = row["TITLE"].ToString();
                    entity.STATE = row["STATE"].ToString();
                    if (!Convert.IsDBNull(row["VERSIONNO"]))
                    {
                        entity.VERSIONNO = Convert.ToInt32(row["VERSIONNO"]);
                    }
                    if (!Convert.IsDBNull(row["CREATEDDATE"]))
                    {
                        entity.CREATEDDATE = Convert.ToDateTime(row["CREATEDDATE"]);
                    }
                    if (!Convert.IsDBNull(row["LASTUPDATE"]))
                    {
                        entity.LASTUPDATE = Convert.ToDateTime(row["LASTUPDATE"]);
                    }
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public override List<WidgetInstanceEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM WIDGETINSTANCE ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<WidgetInstanceEntity> list = new List<WidgetInstanceEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        WidgetInstanceEntity entity = new WidgetInstanceEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        if (!Convert.IsDBNull(row["WIDGETZONEID"]))
                        {
                            entity.WIDGETZONEID = Convert.ToInt32(row["WIDGETZONEID"]);
                        }
                        if (!Convert.IsDBNull(row["WIDGETID"]))
                        {
                            entity.WIDGETID = Convert.ToInt32(row["WIDGETID"]);
                        }
                        if (!Convert.IsDBNull(row["ORDERNO"]))
                        {
                            entity.ORDERNO = Convert.ToInt32(row["ORDERNO"]);
                        }
                        if (!Convert.IsDBNull(row["EXPANDED"]))
                        {
                            entity.EXPANDED = Convert.ToBoolean(row["EXPANDED"]);
                        }
                        if (!Convert.IsDBNull(row["MAXIMIZED"]))
                        {
                            entity.MAXIMIZED = Convert.ToBoolean(row["MAXIMIZED"]);
                        }
                        if (!Convert.IsDBNull(row["RESIZED"]))
                        {
                            entity.RESIZED = Convert.ToBoolean(row["RESIZED"]);
                        }
                        if (!Convert.IsDBNull(row["WIDTH"]))
                        {
                            entity.WIDTH = Convert.ToInt32(row["WIDTH"]);
                        }
                        if (!Convert.IsDBNull(row["HEIGHT"]))
                        {
                            entity.HEIGHT = Convert.ToInt32(row["HEIGHT"]);
                        }
                        entity.TITLE = row["TITLE"].ToString();
                        entity.STATE = row["STATE"].ToString();
                        if (!Convert.IsDBNull(row["VERSIONNO"]))
                        {
                            entity.VERSIONNO = Convert.ToInt32(row["VERSIONNO"]);
                        }
                        if (!Convert.IsDBNull(row["CREATEDDATE"]))
                        {
                            entity.CREATEDDATE = Convert.ToDateTime(row["CREATEDDATE"]);
                        }
                        if (!Convert.IsDBNull(row["LASTUPDATE"]))
                        {
                            entity.LASTUPDATE = Convert.ToDateTime(row["LASTUPDATE"]);
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
                strSql.Append(" FROM WIDGETINSTANCE");
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
            public int GetPagerRowsCount(string where, SqlParameter[] param)
            {
                string sql = "select count(*) from WIDGETINSTANCE ";
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
            public DataTable GetPager(string where, SqlParameter[] param, string orderBy, int pageSize, int pageNumber)
            {
                int startNumber = pageSize * (pageNumber - 1) + 1;
                int endNumber = pageSize * pageNumber;

                StringBuilder PagerSql = new StringBuilder();
                PagerSql.Append("SELECT * FROM (");
                PagerSql.Append(" SELECT A.*, ROWNUM RN ");
                PagerSql.Append("FROM (SELECT * FROM WIDGETINSTANCE ");
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

