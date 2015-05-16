﻿using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;
using System.Text;
using System.Configuration;
using Dropthings.Util;

namespace Dropthings.Data
{
    [Serializable]
    public partial class PUSHINFOEntity
    {
        private SqlHelper _oracleHelper;

        #region const fields
        public const string DBName = "Sentiment";
        public const string TableName = "PUSHINFO";
        public const string PrimaryKey = "PK_PUSHINFO";
        #endregion

        #region columns
        public struct Columns
        {
            public const string ID = "ID";
            public const string URL = "URL";
            public const string ROLEID = "ROLEID";
            public const string USERID = "USERID";
            public const string PUSHDATE = "PUSHDATE";
            public const string PUSHTYPE = "PUSHTYPE";
        }
        #endregion

        #region constructors
        public PUSHINFOEntity()
        {
            _oracleHelper = new SqlHelper(DBName);
        }

        public PUSHINFOEntity(int id, string url, int roleid, int userid, DateTime pushdate, int pushtype)
        {
            this.ID = id;

            this.URL = url;

            this.ROLEID = roleid;

            this.USERID = userid;

            this.PUSHDATE = pushdate;

            this.PUSHTYPE = pushtype;

        }
        #endregion

        #region Properties

        public int? ID
        {
            get;
            set;
        }


        public string URL
        {
            get;
            set;
        }


        public int? ROLEID
        {
            get;
            set;
        }


        public int? USERID
        {
            get;
            set;
        }


        public DateTime? PUSHDATE
        {
            get;
            set;
        }


        public int? PUSHTYPE
        {
            get;
            set;
        }

        #endregion

        public class PUSHINFODAO : SqlDAO<PUSHINFOEntity>
        {
            private SqlHelper _oracleHelper;
            public const string DBName = "SentimentConnStr";

            public PUSHINFODAO()
            {
                _oracleHelper = new SqlHelper(DBName);
            }

            public override void Add(PUSHINFOEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into PUSHINFO(");
                strSql.Append("URL,ROLEID,USERID,PUSHDATE,PUSHTYPE)");
                strSql.Append(" values (");
                strSql.Append("@URL,@ROLEID,@USERID,@PUSHDATE,@PUSHTYPE)");
                SqlParameter[] parameters = {
					new SqlParameter("@URL",SqlDbType.NVarChar),
					new SqlParameter("@ROLEID",SqlDbType.Int),
					new SqlParameter("@USERID",SqlDbType.Int),
					new SqlParameter("@PUSHDATE",SqlDbType.DateTime),
					new SqlParameter("@PUSHTYPE",SqlDbType.Int)
					};
                parameters[0].Value = entity.URL;
                parameters[1].Value = entity.ROLEID;
                parameters[2].Value = entity.USERID;
                parameters[3].Value = entity.PUSHDATE;
                parameters[4].Value = entity.PUSHTYPE;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override void Update(PUSHINFOEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update PUSHINFO set ");
                strSql.Append("URL=@URL,");
                strSql.Append("ROLEID=@ROLEID,");
                strSql.Append("USERID=@USERID,");
                strSql.Append("PUSHDATE=@PUSHDATE,");
                strSql.Append("PUSHTYPE=@PUSHTYPE");

                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@URL",SqlDbType.NVarChar),
					new SqlParameter("@ROLEID",SqlDbType.Int),
					new SqlParameter("@USERID",SqlDbType.Int),
					new SqlParameter("@PUSHDATE",SqlDbType.DateTime),
					new SqlParameter("@PUSHTYPE",SqlDbType.Int),
					new SqlParameter("@ID",SqlDbType.Int)
				};
                parameters[0].Value = entity.URL;
                parameters[1].Value = entity.ROLEID;
                parameters[2].Value = entity.USERID;
                parameters[3].Value = entity.PUSHDATE;
                parameters[4].Value = entity.PUSHTYPE;
                parameters[5].Value = entity.ID;

                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update PUSHINFO set ");
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
                string strSql = "delete from PUSHINFO where ID=" + ID;
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
                string strSql = "delete from PUSHINFO where ID in (" + ID + ")";
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

            public override void Delete(PUSHINFOEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from PUSHINFO ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.ID;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public void DeleteByUserId(int userid) {                
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from PUSHINFO ");
                strSql.Append(" where USERID=@USERID");
                strSql.AppendFormat(" AND PUSHDATE>=trunc(to_date('{0}','yyyy-MM-dd')) and PUSHDATE<trunc(to_date('{0}','yyyy-MM-dd')+1)", TimeHelp.GetDateTimeStr(0));
                SqlParameter[] parameters = {
						new SqlParameter("@USERID", SqlDbType.Int)
					};
                parameters[0].Value = userid;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public List<PUSHINFOEntity> FindByUserid(string useridwhere, string pushTime)
            {
                string[] list = useridwhere.Split(',');
                int time = Convert.ToInt32(ConfigurationManager.AppSettings["TimeSpan"]);
                StringBuilder strWhere = new StringBuilder();
                strWhere.AppendFormat(" URL IN (SELECT URL FROM PUSHINFO WHERE USERID IN ({0})", useridwhere);
                if (!string.IsNullOrEmpty(pushTime))
                {
                    strWhere.AppendFormat(" AND PUSHDATE>=trunc(to_date('{0}','yyyy-MM-dd')) and PUSHDATE<trunc(to_date('{0}','yyyy-MM-dd')+1)", pushTime);
                }
                else
                {
                    strWhere.AppendFormat(" AND PUSHDATE>=trunc(to_date('{0}','yyyy-MM-dd')) and PUSHDATE<trunc(to_date('{0}','yyyy-MM-dd')+1)", TimeHelp.GetDateTimeStr(0));
                }

                strWhere.AppendFormat("GROUP BY URL HAVING COUNT(*)={0})", list.Length);
                return Find(strWhere.ToString(), null);
            }

            public List<PUSHINFOEntity> FindByRoleid(string roleidwhere, string pushTime)
            {
                string[] list = roleidwhere.Split(',');                
                StringBuilder strWhere = new StringBuilder();
                strWhere.AppendFormat(" URL IN (SELECT URL FROM PUSHINFO WHERE ROLEID IN ({0})", roleidwhere);
                if (!string.IsNullOrEmpty(pushTime))
                {
                    strWhere.AppendFormat(" AND PUSHDATE>=trunc(to_date('{0}','yyyy-MM-dd')) and PUSHDATE<trunc(to_date('{0}','yyyy-MM-dd')+1)", pushTime);
                }
                else
                {
                    strWhere.AppendFormat(" AND PUSHDATE>=trunc(to_date('{0}','yyyy-MM-dd')) and PUSHDATE<trunc(to_date('{0}','yyyy-MM-dd')+1)", TimeHelp.GetDateTimeStr(0));
                }
                
                strWhere.AppendFormat("GROUP BY URL HAVING COUNT(*)={0})",list.Length);
                return Find(strWhere.ToString(), null);
            }

            public override PUSHINFOEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from PUSHINFO ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    PUSHINFOEntity entity = new PUSHINFOEntity();
                    if (!Convert.IsDBNull(row["ID"]))
                    {
                        entity.ID = Convert.ToInt32(row["ID"]);
                    }
                    entity.URL = row["URL"].ToString();
                    if (!Convert.IsDBNull(row["ROLEID"]))
                    {
                        entity.ROLEID = Convert.ToInt32(row["ROLEID"]);
                    }
                    if (!Convert.IsDBNull(row["USERID"]))
                    {
                        entity.USERID = Convert.ToInt32(row["USERID"]);
                    }
                    if (!Convert.IsDBNull(row["PUSHDATE"]))
                    {
                        entity.PUSHDATE = Convert.ToDateTime(row["PUSHDATE"]);
                    }
                    if (!Convert.IsDBNull(row["PUSHTYPE"]))
                    {
                        entity.PUSHTYPE = Convert.ToInt32(row["PUSHTYPE"]);
                    }
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public override List<PUSHINFOEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM PUSHINFO ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<PUSHINFOEntity> list = new List<PUSHINFOEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        PUSHINFOEntity entity = new PUSHINFOEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        entity.URL = row["URL"].ToString();
                        if (!Convert.IsDBNull(row["ROLEID"]))
                        {
                            entity.ROLEID = Convert.ToInt32(row["ROLEID"]);
                        }
                        if (!Convert.IsDBNull(row["USERID"]))
                        {
                            entity.USERID = Convert.ToInt32(row["USERID"]);
                        }
                        if (!Convert.IsDBNull(row["PUSHDATE"]))
                        {
                            entity.PUSHDATE = Convert.ToDateTime(row["PUSHDATE"]);
                        }
                        if (!Convert.IsDBNull(row["PUSHTYPE"]))
                        {
                            entity.PUSHTYPE = Convert.ToInt32(row["PUSHTYPE"]);
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
                strSql.Append(" FROM PUSHINFO");
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
                string sql = "select count(*) from PUSHINFO ";
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
                PagerSql.Append("FROM (SELECT * FROM PUSHINFO ");
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

