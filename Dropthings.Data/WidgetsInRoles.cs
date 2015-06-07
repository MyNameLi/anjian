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
    public partial class WidgetsInRolesEntity
    {
        private SqlHelper _sqlHelper;

        #region const fields
        public const string DBName = "SentimentConnStr";
        public const string TableName = "WIDGETSINROLES";
        public const string PrimaryKey = "PK_WIDGETSINROLES";
        #endregion

        #region columns
        public struct Columns
        {
            public const string ID = "ID";
            public const string WIDGETID = "WIDGETID";
            public const string ROLEID = "ROLEID";
        }
        #endregion

        #region constructors
        public WidgetsInRolesEntity()
        {
            _sqlHelper = new SqlHelper(DBName);
        }

        public WidgetsInRolesEntity(int id, int widgetid, int roleid)
        {
            this.ID = id;

            this.WIDGETID = widgetid;

            this.ROLEID = roleid;

        }
        #endregion

        #region Properties

        public int? ID
        {
            get;
            set;
        }


        public int? WIDGETID
        {
            get;
            set;
        }


        public int? ROLEID
        {
            get;
            set;
        }

        #endregion

        public class WidgetsInRolesDAO : SqlDAO<WidgetsInRolesEntity>
        {
            private SqlHelper _sqlHelper;
            public const string DBName = "SentimentConnStr";

            public WidgetsInRolesDAO()
            {
                _sqlHelper = new SqlHelper(DBName);
            }

            public override void Add(WidgetsInRolesEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into WIDGETSINROLES(");
                strSql.Append("WIDGETID,ROLEID)");
                strSql.Append(" values (");
                strSql.Append("@WIDGETID,@ROLEID)");
                SqlParameter[] parameters = {
					new SqlParameter("@WIDGETID",SqlDbType.Int),
					new SqlParameter("@ROLEID",SqlDbType.Int)
					};
                parameters[0].Value = entity.WIDGETID;
                parameters[1].Value = entity.ROLEID;

                _sqlHelper.ExecuteSql(strSql.ToString(), parameters);
                entity.ID = ReturnNewRowId();
            }

            private int ReturnNewRowId()
            {
                string sql = "SELECT TOP 1 ID FROM dbo.WIDGETSINROLES ORDER BY ID DESC";
                object NewId = _sqlHelper.GetSingle(sql, null);
                return Convert.ToInt32(NewId);
            }
            public override void Update(WidgetsInRolesEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update WIDGETSINROLES set ");
                strSql.Append("WIDGETID=@WIDGETID,");
                strSql.Append("ROLEID=@ROLEID");

                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@WIDGETID",SqlDbType.Int),
					new SqlParameter("@ROLEID",SqlDbType.Int),
                    new SqlParameter("@ID",SqlDbType.Int)
                                               };
                parameters[0].Value = entity.WIDGETID;
                parameters[1].Value = entity.ROLEID;
                parameters[2].Value = entity.ID;

                _sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update WIDGETSINROLES set ");
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
                string strSql = "delete from WIDGETSINROLES where ID=" + ID;
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
                string strSql = "delete from WIDGETSINROLES where ID in (" + ID + ")";
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

            public bool DeleteByRoleId(int RoleId)
            {
                string strSql = "delete from WIDGETSINROLES where ROLEID = " + RoleId.ToString();
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

            public override void Delete(WidgetsInRolesEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from WIDGETSINROLES ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.ID;
                _sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override WidgetsInRolesEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from WIDGETSINROLES ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = _sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    WidgetsInRolesEntity entity = new WidgetsInRolesEntity();
                    if (!Convert.IsDBNull(row["ID"]))
                    {
                        entity.ID = Convert.ToInt32(row["ID"]);
                    }
                    if (!Convert.IsDBNull(row["WIDGETID"]))
                    {
                        entity.WIDGETID = Convert.ToInt32(row["WIDGETID"]);
                    }
                    if (!Convert.IsDBNull(row["ROLEID"]))
                    {
                        entity.ROLEID = Convert.ToInt32(row["ROLEID"]);
                    }
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public override List<WidgetsInRolesEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM WIDGETSINROLES ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = _sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<WidgetsInRolesEntity> list = new List<WidgetsInRolesEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        WidgetsInRolesEntity entity = new WidgetsInRolesEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        if (!Convert.IsDBNull(row["WIDGETID"]))
                        {
                            entity.WIDGETID = Convert.ToInt32(row["WIDGETID"]);
                        }
                        if (!Convert.IsDBNull(row["ROLEID"]))
                        {
                            entity.ROLEID = Convert.ToInt32(row["ROLEID"]);
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
                strSql.Append(" FROM WIDGETSINROLES");
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
                string sql = "select count(*) from WIDGETSINROLES ";
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
                PagerSql.Append("FROM (SELECT * FROM WIDGETSINROLES ");
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

