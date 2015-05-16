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
    public partial class RoleTemplateEntity
    {
        private SqlHelper _sqlHelper;

        #region const fields
        public const string DBName = "SentimentConnStr";
        public const string TableName = "ROLETEMPLATE";
        public const string PrimaryKey = "PK_ROLETEMPLATE";
        #endregion

        #region columns
        public struct Columns
        {
            public const string ID = "ID";
            public const string ROLEID = "ROLEID";
            public const string TEMPLATEUSERID = "TEMPLATEUSERID";
            public const string PRIORITY = "PRIORITY";
        }
        #endregion

        #region constructors
        public RoleTemplateEntity()
        {
            _sqlHelper = new SqlHelper(DBName);
        }

        public RoleTemplateEntity(int id, int roleid, int templateuserid, int priority)
        {
            this.ID = id;

            this.ROLEID = roleid;

            this.TEMPLATEUSERID = templateuserid;

            this.PRIORITY = priority;

        }
        #endregion

        #region Properties

        public int? ID
        {
            get;
            set;
        }


        public int? ROLEID
        {
            get;
            set;
        }


        public int? TEMPLATEUSERID
        {
            get;
            set;
        }


        public int? PRIORITY
        {
            get;
            set;
        }

        #endregion

        public class ROLETEMPLATEDAO : SqlDAO<RoleTemplateEntity>
        {
            private SqlHelper _sqlHelper;
            public const string DBName = "SentimentConnStr";

            public ROLETEMPLATEDAO()
            {
                _sqlHelper = new SqlHelper(DBName);
            }

            public override void Add(RoleTemplateEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into ROLETEMPLATE(");
                strSql.Append("ROLEID,TEMPLATEUSERID,PRIORITY)");
                strSql.Append(" values (");
                strSql.Append("@ROLEID,@TEMPLATEUSERID,@PRIORITY)");
                SqlParameter[] parameters = {
					new SqlParameter("@ROLEID",SqlDbType.Int),
					new SqlParameter("@TEMPLATEUSERID",SqlDbType.Int),
					new SqlParameter("@PRIORITY",SqlDbType.Int)
					};
                parameters[0].Value = entity.ROLEID;
                parameters[1].Value = entity.TEMPLATEUSERID;
                parameters[2].Value = entity.PRIORITY;

                _sqlHelper.ExecuteSql(strSql.ToString(), parameters);
                entity.ID = ReturnNewRowId();
            }

            private int ReturnNewRowId()
            {
                string sql = "select ROLETEMPLATE_ID_SEQ.currval NEWID from dual";
                object NewId = _sqlHelper.GetSingle(sql, null);
                return Convert.ToInt32(NewId);
            }

            public override void Update(RoleTemplateEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update ROLETEMPLATE set ");
                strSql.Append("ROLEID=@ROLEID,");
                strSql.Append("TEMPLATEUSERID=@TEMPLATEUSERID,");
                strSql.Append("PRIORITY=@PRIORITY");

                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@ID",SqlDbType.Int),
					new SqlParameter("@ROLEID",SqlDbType.Int),
					new SqlParameter("@TEMPLATEUSERID",SqlDbType.Int),
					new SqlParameter("@PRIORITY",SqlDbType.Int)
					};
                parameters[0].Value = entity.ROLEID;
                parameters[1].Value = entity.TEMPLATEUSERID;
                parameters[2].Value = entity.PRIORITY;
                parameters[3].Value = entity.ID;

                _sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update ROLETEMPLATE set ");
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
                string strSql = "delete from ROLETEMPLATE where ID=" + ID;
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
                string strSql = "delete from ROLETEMPLATE where ID in (" + ID + ")";
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

            public override void Delete(RoleTemplateEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from ROLETEMPLATE ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.ID;
                _sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override RoleTemplateEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from ROLETEMPLATE ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = _sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    RoleTemplateEntity entity = new RoleTemplateEntity();
                    if (!Convert.IsDBNull(row["ID"]))
                    {
                        entity.ID = Convert.ToInt32(row["ID"]);
                    }
                    if (!Convert.IsDBNull(row["ROLEID"]))
                    {
                        entity.ROLEID = Convert.ToInt32(row["ROLEID"]);
                    }
                    if (!Convert.IsDBNull(row["TEMPLATEUSERID"]))
                    {
                        entity.TEMPLATEUSERID = Convert.ToInt32(row["TEMPLATEUSERID"]);
                    }
                    if (!Convert.IsDBNull(row["PRIORITY"]))
                    {
                        entity.PRIORITY = Convert.ToInt32(row["PRIORITY"]);
                    }
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public override List<RoleTemplateEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM ROLETEMPLATE ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = _sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<RoleTemplateEntity> list = new List<RoleTemplateEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        RoleTemplateEntity entity = new RoleTemplateEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        if (!Convert.IsDBNull(row["ROLEID"]))
                        {
                            entity.ROLEID = Convert.ToInt32(row["ROLEID"]);
                        }
                        if (!Convert.IsDBNull(row["TEMPLATEUSERID"]))
                        {
                            entity.TEMPLATEUSERID = Convert.ToInt32(row["TEMPLATEUSERID"]);
                        }
                        if (!Convert.IsDBNull(row["PRIORITY"]))
                        {
                            entity.PRIORITY = Convert.ToInt32(row["PRIORITY"]);
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

            public List<RoleTemplateEntity> GetRoleTemplateByTemplateUserName(string userName)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM ROLETEMPLATE rt, USERS u WHERE rt.TEMPLATEUSERID=u.USERID AND u.USERNAME=@USERNAME");
                SqlParameter[] parameters = {
						new SqlParameter("@USERNAME",SqlDbType.NVarChar)};
                parameters[0].Value = userName;

                DataSet ds = _sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<RoleTemplateEntity> list = new List<RoleTemplateEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        RoleTemplateEntity entity = new RoleTemplateEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        if (!Convert.IsDBNull(row["ROLEID"]))
                        {
                            entity.ROLEID = Convert.ToInt32(row["ROLEID"]);
                        }
                        if (!Convert.IsDBNull(row["TEMPLATEUSERID"]))
                        {
                            entity.TEMPLATEUSERID = Convert.ToInt32(row["TEMPLATEUSERID"]);
                        }
                        if (!Convert.IsDBNull(row["PRIORITY"]))
                        {
                            entity.PRIORITY = Convert.ToInt32(row["PRIORITY"]);
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

            public List<RoleTemplateEntity> GetRoleTemplateByRoleName(string roleName)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM ROLETEMPLATE rt, ROLES r WHERE rt.ROLEID=r.ROLEID AND r.ROLENAME=@ROLENAME");
                SqlParameter[] parameters = {
						new SqlParameter("@ROLENAME",SqlDbType.NVarChar)};
                parameters[0].Value = roleName;

                DataSet ds = _sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<RoleTemplateEntity> list = new List<RoleTemplateEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        RoleTemplateEntity entity = new RoleTemplateEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        if (!Convert.IsDBNull(row["ROLEID"]))
                        {
                            entity.ROLEID = Convert.ToInt32(row["ROLEID"]);
                        }
                        if (!Convert.IsDBNull(row["TEMPLATEUSERID"]))
                        {
                            entity.TEMPLATEUSERID = Convert.ToInt32(row["TEMPLATEUSERID"]);
                        }
                        if (!Convert.IsDBNull(row["PRIORITY"]))
                        {
                            entity.PRIORITY = Convert.ToInt32(row["PRIORITY"]);
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
                strSql.Append(" FROM ROLETEMPLATE");
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
                string sql = "select count(*) from ROLETEMPLATE ";
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
                PagerSql.Append("FROM (SELECT * FROM ROLETEMPLATE ");
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

