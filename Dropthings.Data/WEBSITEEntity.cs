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
    public partial class WEBSITEEntity
    {
        private SqlHelper sqlHelper;

        #region const fields
        public const string DBName = "Sentiment";
        public const string TableName = "WEBSITE";
        public const string PrimaryKey = "PK_WEBSITE";
        #endregion

        #region columns
        public struct Columns
        {
            public const string ID = "ID";
            public const string WEBSITENAME = "WEBSITENAME";
            public const string WEBSITEURL = "WEBSITEURL";
        }
        #endregion

        #region constructors
        public WEBSITEEntity()
        {
            sqlHelper = new SqlHelper(DBName);
        }

        public WEBSITEEntity(int id, string websitename, string websiteurl)
        {
            this.ID = id;

            this.WEBSITENAME = websitename;

            this.WEBSITEURL = websiteurl;

        }
        #endregion

        #region Properties

        public int? ID
        {
            get;
            set;
        }


        public string WEBSITENAME
        {
            get;
            set;
        }


        public string WEBSITEURL
        {
            get;
            set;
        }

        #endregion

        public class WEBSITEDAO : SqlDAO<WEBSITEEntity>
        {
            private SqlHelper sqlHelper;
            public const string DBName = "SentimentConnStr";

            public WEBSITEDAO()
            {
                sqlHelper = new SqlHelper(DBName);
            }

            public override void Add(WEBSITEEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into WEBSITE(");
                strSql.Append("WEBSITENAME,WEBSITEURL)");
                strSql.Append(" values (");
                strSql.Append("@WEBSITENAME,@WEBSITEURL)");
                SqlParameter[] parameters = {
					new SqlParameter("@WEBSITENAME",SqlDbType.NVarChar),
					new SqlParameter("@WEBSITEURL",SqlDbType.NVarChar)
					};
                parameters[0].Value = entity.WEBSITENAME;
                parameters[1].Value = entity.WEBSITEURL;
                sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override void Update(WEBSITEEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update WEBSITE set ");
                strSql.Append("WEBSITENAME=@WEBSITENAME,");
                strSql.Append("WEBSITEURL=@WEBSITEURL");

                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@WEBSITENAME",SqlDbType.NVarChar),
					new SqlParameter("@WEBSITEURL",SqlDbType.NVarChar),
					new SqlParameter("@ID",SqlDbType.Int)
				};
                parameters[0].Value = entity.WEBSITENAME;
                parameters[1].Value = entity.WEBSITEURL;
                parameters[2].Value = entity.ID;

                sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update WEBSITE set ");
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
                string strSql = "delete from WEBSITE where ID=" + ID;
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
                string strSql = "delete from WEBSITE where ID in (" + ID + ")";
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

            public override void Delete(WEBSITEEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from WEBSITE ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.ID;
                sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override WEBSITEEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from WEBSITE ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    WEBSITEEntity entity = new WEBSITEEntity();
                    if (!Convert.IsDBNull(row["ID"]))
                    {
                        entity.ID = Convert.ToInt32(row["ID"]);
                    }
                    entity.WEBSITENAME = row["WEBSITENAME"].ToString();
                    entity.WEBSITEURL = row["WEBSITEURL"].ToString();
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public List<WEBSITEEntity> Find(string strWhere) {
                return Find(strWhere, null);
            }

            public override List<WEBSITEEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM WEBSITE ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<WEBSITEEntity> list = new List<WEBSITEEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        WEBSITEEntity entity = new WEBSITEEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        entity.WEBSITENAME = row["WEBSITENAME"].ToString();
                        entity.WEBSITEURL = row["WEBSITEURL"].ToString();

                        list.Add(entity);
                    }

                    return list;
                }
                else
                {
                    return null;
                }
            }

            public IList<WEBSITEEntity> GetWebSiteListByUserName(string userName)
            {               
                string strWhere = "ID NOT IN (SELECT WEBSITEID FROM WARNINGMSG WHERE USERNAME=@USERNAME)";                
                SqlParameter[] parameters = {
						new SqlParameter("@USERNAME", SqlDbType.NVarChar)};
                parameters[0].Value = userName;
                return Find(strWhere, parameters);
            }

            public DataSet GetDataSet(string strWhere, SqlParameter[] param)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM WEBSITE");
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
                string sql = "select count(*) from WEBSITE ";
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
                PagerSql.Append("FROM (SELECT * FROM WEBSITE ");
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

