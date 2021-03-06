﻿using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace Dropthings.Data
{
    [Serializable]
    public partial class WidgetZoneEntity
    {
        private SqlHelper _sqlHelper;

        #region const fields
        public const string DBName = "SentimentConnStr";
        public const string TableName = "WIDGETZONE";
        public const string PrimaryKey = "PK_WIDGETZONE";
        #endregion

        #region columns
        public struct Columns
        {
            public const string ID = "ID";
            public const string TITLE = "TITLE";
            public const string UNIQUEID = "UNIQUEID";
            public const string ORDERNO = "ORDERNO";
        }
        #endregion

        #region constructors
        public WidgetZoneEntity()
        {
            _sqlHelper = new SqlHelper(DBName);
        }

        public WidgetZoneEntity(int id, string title, string uniqueid, int orderno)
        {
            this.ID = id;

            this.TITLE = title;

            this.UNIQUEID = uniqueid;

            this.ORDERNO = orderno;

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


        public string UNIQUEID
        {
            get;
            set;
        }


        public int? ORDERNO
        {
            get;
            set;
        }

        #endregion

        public class WidgetZoneDAO : SqlDAO<WidgetZoneEntity>
        {
            private SqlHelper _sqlHelper;
            public const string DBName = "SentimentConnStr";

            public WidgetZoneDAO()
            {
                _sqlHelper = new SqlHelper(DBName);
            }
            public WidgetZoneEntity GetWidgetZoneByTabId_ColumnNo(int TabId, int columnNo)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select WZ.* from WIDGETZONE WZ, COLUMNOFPAGE C, PAGE P ");
                strSql.Append(" where P.ID=C.PAGEID AND C.WIDGETZONEID=WZ.ID AND P.ID=@ID AND C.COLUMNNO=@COLUMNNO");
                SqlParameter[] parameters = {
						new SqlParameter("@ID", SqlDbType.Int),
                        new SqlParameter("@COLUMNNO", SqlDbType.Int)};
                parameters[0].Value = TabId;
                parameters[1].Value = columnNo;
                using (SqlDataReader odr = _sqlHelper.ExecuteReader(strSql.ToString(), parameters))
                {
                    if (odr != null && odr.Read())
                    {
                        WidgetZoneEntity entity = new WidgetZoneEntity();
                        if (!Convert.IsDBNull(odr["ID"]))
                        {
                            entity.ID = Convert.ToInt32(odr["ID"]);
                        }
                        entity.TITLE = odr["TITLE"].ToString();
                        entity.UNIQUEID = odr["UNIQUEID"].ToString();
                        if (!Convert.IsDBNull(odr["ORDERNO"]))
                        {
                            entity.ORDERNO = Convert.ToInt32(odr["ORDERNO"]);
                        }
                        return entity;
                    }
                    return null;
                }
            }

            public override void Add(WidgetZoneEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into WIDGETZONE(");
                strSql.Append("TITLE,UNIQUEID,ORDERNO)");
                strSql.Append(" values (");
                strSql.Append("@TITLE,@UNIQUEID,@ORDERNO)");
                SqlParameter[] parameters = {
					new SqlParameter("@TITLE",SqlDbType.NVarChar),
					new SqlParameter("@UNIQUEID",SqlDbType.NVarChar),
					new SqlParameter("@ORDERNO",SqlDbType.Int)
					};
                parameters[0].Value = entity.TITLE;
                parameters[1].Value = entity.UNIQUEID;
                parameters[2].Value = entity.ORDERNO;

                _sqlHelper.ExecuteSql(strSql.ToString(), parameters);
                entity.ID = ReturnNewRowId();
            }

            private int ReturnNewRowId()
            {
                string sql = "select WIDGETZONE_ID_SEQ.currval NEWID from dual";
                object NewId = _sqlHelper.GetSingle(sql, null);
                return Convert.ToInt32(NewId);
            }

            public override void Update(WidgetZoneEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update WIDGETZONE set ");
                strSql.Append("TITLE=@TITLE,");
                strSql.Append("UNIQUEID=@UNIQUEID,");
                strSql.Append("ORDERNO=@ORDERNO");

                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@TITLE",SqlDbType.NVarChar),
					new SqlParameter("@UNIQUEID",SqlDbType.NVarChar),
					new SqlParameter("@ORDERNO",SqlDbType.Int),
   					new SqlParameter("@ID",SqlDbType.Int)
					};
                parameters[0].Value = entity.TITLE;
                parameters[1].Value = entity.UNIQUEID;
                parameters[2].Value = entity.ORDERNO;
                parameters[3].Value = entity.ID;

                _sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public string GetWidgetZoneOwnerName(int widgetZoneId)
            {
                string result = string.Empty;

                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT U.USERNAME FROM USERS U ,WIDGETZONE WZ, COLUMNOFPAGE C,PAGE P ");
                strSql.Append("WHERE WZ.ID=@ID AND C.WIDGETZONEID=WZ.ID AND P.ID=C.PAGEID AND P.USERID=U.USERID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ID",SqlDbType.Int)};
                parameters[0].Value = widgetZoneId;

                result = Convert.ToString(_sqlHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));

                return result;
            }
            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update WIDGETZONE set ");
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
                string strSql = "delete from WIDGETZONE where ID=" + ID;
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
                string strSql = "delete from WIDGETZONE where ID in (" + ID + ")";
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

            public override void Delete(WidgetZoneEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from WIDGETZONE ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.ID;
                _sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override WidgetZoneEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from WIDGETZONE ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = _sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    WidgetZoneEntity entity = new WidgetZoneEntity();
                    if (!Convert.IsDBNull(row["ID"]))
                    {
                        entity.ID = Convert.ToInt32(row["ID"]);
                    }
                    entity.TITLE = row["TITLE"].ToString();
                    entity.UNIQUEID = row["UNIQUEID"].ToString();
                    if (!Convert.IsDBNull(row["ORDERNO"]))
                    {
                        entity.ORDERNO = Convert.ToInt32(row["ORDERNO"]);
                    }
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public override List<WidgetZoneEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM WIDGETZONE ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = _sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<WidgetZoneEntity> list = new List<WidgetZoneEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        WidgetZoneEntity entity = new WidgetZoneEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        entity.TITLE = row["TITLE"].ToString();
                        entity.UNIQUEID = row["UNIQUEID"].ToString();
                        if (!Convert.IsDBNull(row["ORDERNO"]))
                        {
                            entity.ORDERNO = Convert.ToInt32(row["ORDERNO"]);
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
                strSql.Append(" FROM WIDGETZONE");
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
                string sql = "select count(*) from WIDGETZONE ";
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
                PagerSql.Append("FROM (SELECT * FROM WIDGETZONE ");
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

