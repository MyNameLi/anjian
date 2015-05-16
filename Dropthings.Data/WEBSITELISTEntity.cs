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
    public partial class WEBSITELISTEntity
    {
        private SqlHelper _oracleHelper;

        #region const fields
        public const string DBName = "Sentiment";
        public const string TableName = "WEBSITELIST";
        public const string PrimaryKey = "PK_WEBSITELIST";
        #endregion

        #region columns
        public struct Columns
        {
            public const string ID = "ID";
            public const string SITENAME = "SITENAME";
            public const string DOMAINNAME = "DOMAINNAME";
            public const string TEMPLATEPATH = "TEMPLATEPATH";
            public const string PUBLISHPATH = "PUBLISHPATH";
        }
        #endregion

        #region constructors
        public WEBSITELISTEntity()
        {
            _oracleHelper = new SqlHelper(DBName);
        }

        public WEBSITELISTEntity(int id, string sitename, string domainname, string templatepath, string publishpath)
        {
            this.ID = id;

            this.SITENAME = sitename;

            this.DOMAINNAME = domainname;

            this.TEMPLATEPATH = templatepath;

            this.PUBLISHPATH = publishpath;

        }
        #endregion

        #region Properties

        public int? ID
        {
            get;
            set;
        }


        public string SITENAME
        {
            get;
            set;
        }


        public string DOMAINNAME
        {
            get;
            set;
        }


        public string TEMPLATEPATH
        {
            get;
            set;
        }


        public string PUBLISHPATH
        {
            get;
            set;
        }

        #endregion

        public class WEBSITELISTDAO : SqlDAO<WEBSITELISTEntity>
        {
            private SqlHelper _oracleHelper;
            public const string DBName = "SentimentConnStr";

            public WEBSITELISTDAO()
            {
                _oracleHelper = new SqlHelper(DBName);
            }

            public override void Add(WEBSITELISTEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into WEBSITELIST(");
                strSql.Append("SITENAME,DOMAINNAME,TEMPLATEPATH,PUBLISHPATH)");
                strSql.Append(" values (");
                strSql.Append("@SITENAME,@DOMAINNAME,@TEMPLATEPATH,@PUBLISHPATH)");
                SqlParameter[] parameters = {
					new SqlParameter("@SITENAME",SqlDbType.NVarChar),
					new SqlParameter("@DOMAINNAME",SqlDbType.NVarChar),
					new SqlParameter("@TEMPLATEPATH",SqlDbType.NVarChar),
					new SqlParameter("@PUBLISHPATH",SqlDbType.NVarChar)
					};
                parameters[0].Value = entity.SITENAME;
                parameters[1].Value = entity.DOMAINNAME;
                parameters[2].Value = entity.TEMPLATEPATH;
                parameters[3].Value = entity.PUBLISHPATH;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override void Update(WEBSITELISTEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update WEBSITELIST set ");
                strSql.Append("SITENAME=@SITENAME,");
                strSql.Append("DOMAINNAME=@DOMAINNAME,");
                strSql.Append("TEMPLATEPATH=@TEMPLATEPATH,");
                strSql.Append("PUBLISHPATH=@PUBLISHPATH");

                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@SITENAME",SqlDbType.NVarChar),
					new SqlParameter("@DOMAINNAME",SqlDbType.NVarChar),
					new SqlParameter("@TEMPLATEPATH",SqlDbType.NVarChar),
					new SqlParameter("@PUBLISHPATH",SqlDbType.NVarChar),
					new SqlParameter("@ID",SqlDbType.Int)
				};
                parameters[0].Value = entity.SITENAME;
                parameters[1].Value = entity.DOMAINNAME;
                parameters[2].Value = entity.TEMPLATEPATH;
                parameters[3].Value = entity.PUBLISHPATH;
                parameters[4].Value = entity.ID;

                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update WEBSITELIST set ");
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
                string strSql = "delete from WEBSITELIST where ID=" + ID;
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
                string strSql = "delete from WEBSITELIST where ID in (" + ID + ")";
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

            public override void Delete(WEBSITELISTEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from WEBSITELIST ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.ID;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override WEBSITELISTEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from WEBSITELIST ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    WEBSITELISTEntity entity = new WEBSITELISTEntity();
                    if (!Convert.IsDBNull(row["ID"]))
                    {
                        entity.ID = Convert.ToInt32(row["ID"]);
                    }
                    entity.SITENAME = row["SITENAME"].ToString();
                    entity.DOMAINNAME = row["DOMAINNAME"].ToString();
                    entity.TEMPLATEPATH = row["TEMPLATEPATH"].ToString();
                    entity.PUBLISHPATH = row["PUBLISHPATH"].ToString();
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public List<WEBSITELISTEntity> Find(string strWhere)
            {
                return Find("", null);
            }

            public override List<WEBSITELISTEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM WEBSITELIST ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<WEBSITELISTEntity> list = new List<WEBSITELISTEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        WEBSITELISTEntity entity = new WEBSITELISTEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        entity.SITENAME = row["SITENAME"].ToString();
                        entity.DOMAINNAME = row["DOMAINNAME"].ToString();
                        entity.TEMPLATEPATH = row["TEMPLATEPATH"].ToString();
                        entity.PUBLISHPATH = row["PUBLISHPATH"].ToString();

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
                strSql.Append(" FROM WEBSITELIST");
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
            /// 
            public int GetPagerRowsCount(string where) {
                return GetPagerRowsCount(where, null);
            }
            public int GetPagerRowsCount(string where, SqlParameter[] param)
            {
                string sql = "select count(*) from WEBSITELIST ";
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
            /// 

            public DataTable GetPager(string where, string orderBy, int pageSize, int pageNumber)
            {
                return GetPager(where, null, orderBy, pageSize, pageNumber);
            }

            public DataTable GetPager(string where, SqlParameter[] param, string orderBy, int pageSize, int pageNumber)
            {
                int startNumber = pageSize * (pageNumber - 1) + 1;
                int endNumber = pageSize * pageNumber;

                if (string.IsNullOrEmpty(where))
                {
                    where = "  1=1 ";
                }

                StringBuilder PagerSql = new StringBuilder();
                PagerSql.AppendFormat("SELECT * FROM( SELECT ROW_NUMBER()OVER( ORDER BY ID) AS RN,* FROM dbo.WEBSITELIST where {0} ) AS T WHERE T.RN BETWEEN {1} AND {2}", where, startNumber, endNumber);

                //StringBuilder PagerSql = new StringBuilder();
                //PagerSql.Append("SELECT * FROM (");
                //PagerSql.Append(" SELECT A.*, ROWNUM RN ");
                //PagerSql.Append("FROM (SELECT * FROM WEBSITELIST ");
                //if (!string.IsNullOrEmpty(where))
                //{
                //    PagerSql.Append(" where " + where);
                //}
                //if (!string.IsNullOrEmpty(orderBy))
                //{
                //    PagerSql.AppendFormat(" ORDER BY {0}", orderBy);
                //}
                //else
                //{

                //    PagerSql.Append(" ORDER BY ID");//默认按主键排序

                //}
                //PagerSql.AppendFormat(" ) A WHERE ROWNUM <= {0})", endNumber);
                //PagerSql.AppendFormat(" WHERE RN >= {0}", startNumber);

                return _oracleHelper.ExecuteDateSet(PagerSql.ToString(), param).Tables[0];
            }

            #endregion

        }
    }
}

