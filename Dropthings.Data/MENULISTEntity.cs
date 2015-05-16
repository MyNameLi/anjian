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
    public partial class MENULISTEntity
    {
        private SqlHelper _oracleHelper;

        #region const fields
        public const string DBName = "Sentiment";
        public const string TableName = "MENULIST";
        public const string PrimaryKey = "PK_MENULIST";
        #endregion

        #region columns
        public struct Columns
        {
            public const string ID = "ID";
            public const string NAME = "NAME";
            public const string LEFTURL = "LEFTURL";
            public const string URLPATH = "URLPATH";
            public const string PARENTID = "PARENTID";
            public const string SEQUECE = "SEQUECE";
        }
        #endregion

        #region constructors
        public MENULISTEntity()
        {
            _oracleHelper = new SqlHelper(DBName);
        }

        public MENULISTEntity(int id, string name, string lefturl, string urlpath, int parentid, int sequece)
        {
            this.ID = id;

            this.NAME = name;

            this.LEFTURL = lefturl;

            this.URLPATH = urlpath;

            this.PARENTID = parentid;

            this.SEQUECE = sequece;

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


        public string LEFTURL
        {
            get;
            set;
        }


        public string URLPATH
        {
            get;
            set;
        }


        public int? PARENTID
        {
            get;
            set;
        }


        public int? SEQUECE
        {
            get;
            set;
        }

        #endregion

        public class MENULISTDAO : SqlDAO<MENULISTEntity>
        {
            private SqlHelper _oracleHelper;
            public const string DBName = "SentimentConnStr";

            public MENULISTDAO()
            {
                _oracleHelper = new SqlHelper(DBName);
            }

            public override void Add(MENULISTEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into MENULIST(");
                strSql.Append("ID,NAME,LEFTURL,URLPATH,PARENTID,SEQUECE)");
                strSql.Append(" values (");
                strSql.Append("@ID,@NAME,@LEFTURL,@URLPATH,@PARENTID,@SEQUECE)");
                SqlParameter[] parameters = {
					new SqlParameter("@ID",SqlDbType.Int),
					new SqlParameter("@NAME",SqlDbType.NVarChar),
					new SqlParameter("@LEFTURL",SqlDbType.NVarChar),
					new SqlParameter("@URLPATH",SqlDbType.NVarChar),
					new SqlParameter("@PARENTID",SqlDbType.Int),
					new SqlParameter("@SEQUECE",SqlDbType.Int)
					};
                parameters[0].Value = entity.ID;
                parameters[1].Value = entity.NAME;
                parameters[2].Value = entity.LEFTURL;
                parameters[3].Value = entity.URLPATH;
                parameters[4].Value = entity.PARENTID;
                parameters[5].Value = entity.SEQUECE;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override void Update(MENULISTEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update MENULIST set ");
                strSql.Append("NAME=@NAME,");
                strSql.Append("LEFTURL=@LEFTURL,");
                strSql.Append("URLPATH=@URLPATH,");
                strSql.Append("PARENTID=@PARENTID,");
                strSql.Append("SEQUECE=@SEQUECE");

                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@NAME",SqlDbType.NVarChar),
					new SqlParameter("@LEFTURL",SqlDbType.NVarChar),
					new SqlParameter("@URLPATH",SqlDbType.NVarChar),
					new SqlParameter("@PARENTID",SqlDbType.Int),
					new SqlParameter("@SEQUECE",SqlDbType.Int),
					new SqlParameter("@ID",SqlDbType.Int)
				};
                parameters[0].Value = entity.NAME;
                parameters[1].Value = entity.LEFTURL;
                parameters[2].Value = entity.URLPATH;
                parameters[3].Value = entity.PARENTID;
                parameters[4].Value = entity.SEQUECE;
                parameters[5].Value = entity.ID;

                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update MENULIST set ");
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
                string strSql = "delete from MENULIST where ID=" + ID;
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
                string strSql = "delete from MENULIST where ID in (" + ID + ")";
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

            public override void Delete(MENULISTEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from MENULIST ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.ID;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override MENULISTEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from MENULIST ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    MENULISTEntity entity = new MENULISTEntity();
                    if (!Convert.IsDBNull(row["ID"]))
                    {
                        entity.ID = Convert.ToInt32(row["ID"]);
                    }
                    entity.NAME = row["NAME"].ToString();
                    entity.LEFTURL = row["LEFTURL"].ToString();
                    entity.URLPATH = row["URLPATH"].ToString();
                    if (!Convert.IsDBNull(row["PARENTID"]))
                    {
                        entity.PARENTID = Convert.ToInt32(row["PARENTID"]);
                    }
                    if (!Convert.IsDBNull(row["SEQUECE"]))
                    {
                        entity.SEQUECE = Convert.ToInt32(row["SEQUECE"]);
                    }
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public List<MENULISTEntity> Find(string strWhere) {
                return Find(strWhere, null);
            }

            public override List<MENULISTEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM MENULIST ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<MENULISTEntity> list = new List<MENULISTEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        MENULISTEntity entity = new MENULISTEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        entity.NAME = row["NAME"].ToString();
                        entity.LEFTURL = row["LEFTURL"].ToString();
                        entity.URLPATH = row["URLPATH"].ToString();
                        if (!Convert.IsDBNull(row["PARENTID"]))
                        {
                            entity.PARENTID = Convert.ToInt32(row["PARENTID"]);
                        }
                        if (!Convert.IsDBNull(row["SEQUECE"]))
                        {
                            entity.SEQUECE = Convert.ToInt32(row["SEQUECE"]);
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

            public IList<MENULISTEntity> GetMenuList(int roleid) {
                IList<MENULISTEntity> list = new List<MENULISTEntity>();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select A.* from MENULIST A,MENUINROLE B WHERE");
                strSql.Append(" A.ID = B.MENUID");                
                strSql.Append(" AND B.ROLEID=@ROLEID");
                strSql.Append(" order by A.SEQUECE");
                SqlParameter[] parameters = {					
					new SqlParameter("@ROLEID",SqlDbType.Int)
				};
                parameters[0].Value = roleid;                
                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);                
                if (ds != null)
                {
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows) {
                        MENULISTEntity entity = new MENULISTEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        entity.NAME = row["NAME"].ToString();
                        entity.LEFTURL = row["LEFTURL"].ToString();
                        entity.URLPATH = row["URLPATH"].ToString();
                        if (!Convert.IsDBNull(row["PARENTID"]))
                        {
                            entity.PARENTID = Convert.ToInt32(row["PARENTID"]);
                        }
                        if (!Convert.IsDBNull(row["SEQUECE"]))
                        {
                            entity.SEQUECE = Convert.ToInt32(row["SEQUECE"]);
                        }
                        list.Add(entity);
                    }
                }
                return list; 
            }

            public IList<MENULISTEntity> GetMenuList(string roleidlist)
            {
                IList<MENULISTEntity> list = new List<MENULISTEntity>();
                StringBuilder strSql = new StringBuilder();


                strSql.Append("SELECT * FROM MENULIST WHERE ID in (");
                strSql.Append("SELECT MENUID FROM MENUINROLE");
                strSql.Append(" where ROLEID in (").Append(roleidlist).Append(")");
                strSql.Append(" group by MENUID) order by SEQUECE");
                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), null);
                if (ds != null)
                {
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        MENULISTEntity entity = new MENULISTEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        entity.NAME = row["NAME"].ToString();
                        entity.LEFTURL = row["LEFTURL"].ToString();
                        entity.URLPATH = row["URLPATH"].ToString();
                        if (!Convert.IsDBNull(row["PARENTID"]))
                        {
                            entity.PARENTID = Convert.ToInt32(row["PARENTID"]);
                        }
                        if (!Convert.IsDBNull(row["SEQUECE"]))
                        {
                            entity.SEQUECE = Convert.ToInt32(row["SEQUECE"]);
                        }
                        list.Add(entity);
                    }
                }
                return list;
            }


            public DataSet GetDataSet(string strWhere, SqlParameter[] param)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM MENULIST");
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
                string sql = "select count(*) from MENULIST ";
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
                PagerSql.Append("FROM (SELECT * FROM MENULIST ");
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

