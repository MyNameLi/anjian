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
    public partial class TEMPLATEEntity
    {
        private SqlHelper _oracleHelper;

        #region const fields
        public const string DBName = "Sentiment";
        public const string TableName = "TEMPLATE";
        public const string PrimaryKey = "PK_TELEMPLATEDB";
        #endregion

        #region columns
        public struct Columns
        {
            public const string ID = "ID";
            public const string TEMPLATENAME = "TEMPLATENAME";
            public const string TEMPLATEDES = "TEMPLATEDES";
            public const string TEMPLATEPATH = "TEMPLATEPATH";
            public const string TEMPLATETYPE = "TEMPLATETYPE";
            public const string TEMPLATEPARAM = "TEMPLATEPARAM";
            public const string SITEID = "SITEID";
        }
        #endregion

        #region constructors
        public TEMPLATEEntity()
        {
            _oracleHelper = new SqlHelper(DBName);
        }

        public TEMPLATEEntity(int id, string templatename, string templatedes, string templatepath, int templatetype, string templateparam, int siteid)
        {
            this.ID = id;

            this.TEMPLATENAME = templatename;

            this.TEMPLATEDES = templatedes;

            this.TEMPLATEPATH = templatepath;

            this.TEMPLATETYPE = templatetype;

            this.TEMPLATEPARAM = templateparam;

            this.SITEID = siteid;

        }
        #endregion

        #region Properties

        public int? ID
        {
            get;
            set;
        }


        public string TEMPLATENAME
        {
            get;
            set;
        }


        public string TEMPLATEDES
        {
            get;
            set;
        }


        public string TEMPLATEPATH
        {
            get;
            set;
        }


        public int? TEMPLATETYPE
        {
            get;
            set;
        }


        public string TEMPLATEPARAM
        {
            get;
            set;
        }


        public int? SITEID
        {
            get;
            set;
        }

        #endregion

        public class TEMPLATEDAO : SqlDAO<TEMPLATEEntity>
        {
            private SqlHelper _oracleHelper;
            public const string DBName = "SentimentConnStr";

            public TEMPLATEDAO()
            {
                _oracleHelper = new SqlHelper(DBName);
            }

            public override void Add(TEMPLATEEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into TEMPLATE(");
                strSql.Append("TEMPLATENAME,TEMPLATEDES,TEMPLATEPATH,TEMPLATETYPE,TEMPLATEPARAM,SITEID)");
                strSql.Append(" values (");
                strSql.Append("@TEMPLATENAME,@TEMPLATEDES,@TEMPLATEPATH,@TEMPLATETYPE,@TEMPLATEPARAM,@SITEID)");
                SqlParameter[] parameters = {
					new SqlParameter("@TEMPLATENAME",SqlDbType.NVarChar),
					new SqlParameter("@TEMPLATEDES",SqlDbType.NVarChar),
					new SqlParameter("@TEMPLATEPATH",SqlDbType.NVarChar),
					new SqlParameter("@TEMPLATETYPE",SqlDbType.Int),
					new SqlParameter("@TEMPLATEPARAM",SqlDbType.NVarChar),
					new SqlParameter("@SITEID",SqlDbType.Int)
					};
                parameters[0].Value = entity.TEMPLATENAME;
                parameters[1].Value = entity.TEMPLATEDES;
                parameters[2].Value = entity.TEMPLATEPATH;
                parameters[3].Value = entity.TEMPLATETYPE;
                parameters[4].Value = entity.TEMPLATEPARAM;
                parameters[5].Value = entity.SITEID;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override void Update(TEMPLATEEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update TEMPLATE set ");
                strSql.Append("TEMPLATENAME=@TEMPLATENAME,");
                strSql.Append("TEMPLATEDES=@TEMPLATEDES,");
                strSql.Append("TEMPLATEPATH=@TEMPLATEPATH,");
                strSql.Append("TEMPLATETYPE=@TEMPLATETYPE,");
                strSql.Append("TEMPLATEPARAM=@TEMPLATEPARAM,");
                strSql.Append("SITEID=@SITEID");

                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@TEMPLATENAME",SqlDbType.NVarChar),
					new SqlParameter("@TEMPLATEDES",SqlDbType.NVarChar),
					new SqlParameter("@TEMPLATEPATH",SqlDbType.NVarChar),
					new SqlParameter("@TEMPLATETYPE",SqlDbType.Int),
					new SqlParameter("@TEMPLATEPARAM",SqlDbType.NVarChar),
					new SqlParameter("@SITEID",SqlDbType.Int),
					new SqlParameter("@ID",SqlDbType.Int)
				};
                parameters[0].Value = entity.TEMPLATENAME;
                parameters[1].Value = entity.TEMPLATEDES;
                parameters[2].Value = entity.TEMPLATEPATH;
                parameters[3].Value = entity.TEMPLATETYPE;
                parameters[4].Value = entity.TEMPLATEPARAM;
                parameters[5].Value = entity.SITEID;
                parameters[6].Value = entity.ID;

                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update TEMPLATE set ");
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
                string strSql = "delete from TEMPLATE where ID=" + ID;
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
                string strSql = "delete from TEMPLATE where ID in (" + ID + ")";
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

            public override void Delete(TEMPLATEEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from TEMPLATE ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.ID;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override TEMPLATEEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from TEMPLATE ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    TEMPLATEEntity entity = new TEMPLATEEntity();
                    if (!Convert.IsDBNull(row["ID"]))
                    {
                        entity.ID = Convert.ToInt32(row["ID"]);
                    }
                    entity.TEMPLATENAME = row["TEMPLATENAME"].ToString();
                    entity.TEMPLATEDES = row["TEMPLATEDES"].ToString();
                    entity.TEMPLATEPATH = row["TEMPLATEPATH"].ToString();
                    if (!Convert.IsDBNull(row["TEMPLATETYPE"]))
                    {
                        entity.TEMPLATETYPE = Convert.ToInt32(row["TEMPLATETYPE"]);
                    }
                    entity.TEMPLATEPARAM = row["TEMPLATEPARAM"].ToString();
                    if (!Convert.IsDBNull(row["SITEID"]))
                    {
                        entity.SITEID = Convert.ToInt32(row["SITEID"]);
                    }
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public List<TEMPLATEEntity> Find(string strWhere)
            {
                return Find(strWhere, null);
            }


            public override List<TEMPLATEEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM TEMPLATE ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<TEMPLATEEntity> list = new List<TEMPLATEEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        TEMPLATEEntity entity = new TEMPLATEEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        entity.TEMPLATENAME = row["TEMPLATENAME"].ToString();
                        entity.TEMPLATEDES = row["TEMPLATEDES"].ToString();
                        entity.TEMPLATEPATH = row["TEMPLATEPATH"].ToString();
                        if (!Convert.IsDBNull(row["TEMPLATETYPE"]))
                        {
                            entity.TEMPLATETYPE = Convert.ToInt32(row["TEMPLATETYPE"]);
                        }
                        entity.TEMPLATEPARAM = row["TEMPLATEPARAM"].ToString();
                        if (!Convert.IsDBNull(row["SITEID"]))
                        {
                            entity.SITEID = Convert.ToInt32(row["SITEID"]);
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
                strSql.Append(" FROM TEMPLATE");
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
                string sql = "select count(*) from TEMPLATE ";
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
                PagerSql.Append("FROM (SELECT * FROM TEMPLATE ");
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

