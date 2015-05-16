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
    public partial class UserSettingEntity
    {
        private SqlHelper _sqlHelper;

        #region const fields
        public const string DBName = "SentimentConnStr";
        public const string TableName = "USERSETTING";
        public const string PrimaryKey = "PK_USERSETTING_1";
        #endregion

        #region columns
        public struct Columns
        {
            public const string USERID = "USERID";
            public const string CURRENTPAGEID = "CURRENTPAGEID";
            public const string CREATEDDATE = "CREATEDDATE";
            public const string TIMESTAMP = "TIMESTAMP";
        }
        #endregion

        #region constructors
        public UserSettingEntity()
        {
            _sqlHelper = new SqlHelper(DBName);
        }

        public UserSettingEntity(int userid, int currentpageid, DateTime createddate, DateTime timestamp)
        {
            this.USERID = userid;

            this.CURRENTPAGEID = currentpageid;

            this.CREATEDDATE = createddate;

            this.TIMESTAMP = timestamp;

        }
        #endregion

        #region Properties

        public int? USERID
        {
            get;
            set;
        }


        public int? CURRENTPAGEID
        {
            get;
            set;
        }


        public DateTime? CREATEDDATE
        {
            get;
            set;
        }


        public DateTime? TIMESTAMP
        {
            get;
            set;
        }

        #endregion

        public class UserSettingDAO : SqlDAO<UserSettingEntity>
        {
            private SqlHelper _sqlHelper;
            public const string DBName = "SentimentConnStr";

            public UserSettingDAO()
            {
                _sqlHelper = new SqlHelper(DBName);
            }

            public override void Add(UserSettingEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into USERSETTING(");
                strSql.Append("USERID,CURRENTPAGEID,CREATEDDATE,TIMESTAMP)");
                strSql.Append(" values (");
                strSql.Append("@USERID,@CURRENTPAGEID,@CREATEDDATE,@TIMESTAMP)");
                SqlParameter[] parameters = {
					new SqlParameter("@USERID",SqlDbType.Int),
					new SqlParameter("@CURRENTPAGEID",SqlDbType.Int),
					new SqlParameter("@CREATEDDATE",SqlDbType.DateTime),
					new SqlParameter("@TIMESTAMP",SqlDbType.DateTime)
					};
                parameters[0].Value = entity.USERID;
                parameters[1].Value = entity.CURRENTPAGEID;
                parameters[2].Value = entity.CREATEDDATE;
                parameters[3].Value = entity.TIMESTAMP;

                _sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override void Update(UserSettingEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update USERSETTING set ");
                strSql.Append("CURRENTPAGEID=@CURRENTPAGEID,");
                strSql.Append("CREATEDDATE=@CREATEDDATE,");
                strSql.Append("TIMESTAMP=@TIMESTAMP");

                strSql.Append(" where USERID=@USERID");
                SqlParameter[] parameters = {
					new SqlParameter("@CURRENTPAGEID",SqlDbType.Int),
					new SqlParameter("@CREATEDDATE",SqlDbType.DateTime),
					new SqlParameter("@TIMESTAMP",SqlDbType.DateTime),
  					new SqlParameter("@USERID",SqlDbType.Int)
                                               };
                parameters[0].Value = entity.CURRENTPAGEID;
                parameters[1].Value = entity.CREATEDDATE;
                parameters[2].Value = entity.TIMESTAMP;
                parameters[3].Value = entity.USERID;

                _sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update USERSETTING set ");
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
                string strSql = "delete from USERSETTING where ID=" + ID;
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
                string strSql = "delete from USERSETTING where ID in (" + ID + ")";
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

            public override void Delete(UserSettingEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from USERSETTING ");
                strSql.Append(" where USERID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.USERID;
                _sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override UserSettingEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from USERSETTING ");
                strSql.Append(" where USERID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = _sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    UserSettingEntity entity = new UserSettingEntity();
                    if (!Convert.IsDBNull(row["USERID"]))
                    {
                        entity.USERID = Convert.ToInt32(row["USERID"]);
                    }
                    if (!Convert.IsDBNull(row["CURRENTPAGEID"]))
                    {
                        entity.CURRENTPAGEID = Convert.ToInt32(row["CURRENTPAGEID"]);
                    }
                    if (!Convert.IsDBNull(row["CREATEDDATE"]))
                    {
                        entity.CREATEDDATE = Convert.ToDateTime(row["CREATEDDATE"]);
                    }
                    if (!Convert.IsDBNull(row["TIMESTAMP"]))
                    {
                        entity.TIMESTAMP = Convert.ToDateTime(row["TIMESTAMP"]);
                    }
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public override List<UserSettingEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM USERSETTING ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = _sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<UserSettingEntity> list = new List<UserSettingEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        UserSettingEntity entity = new UserSettingEntity();
                        if (!Convert.IsDBNull(row["USERID"]))
                        {
                            entity.USERID = Convert.ToInt32(row["USERID"]);
                        }
                        if (!Convert.IsDBNull(row["CURRENTPAGEID"]))
                        {
                            entity.CURRENTPAGEID = Convert.ToInt32(row["CURRENTPAGEID"]);
                        }
                        if (!Convert.IsDBNull(row["CREATEDDATE"]))
                        {
                            entity.CREATEDDATE = Convert.ToDateTime(row["CREATEDDATE"]);
                        }
                        if (!Convert.IsDBNull(row["TIMESTAMP"]))
                        {
                            entity.TIMESTAMP = Convert.ToDateTime(row["TIMESTAMP"]);
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
                strSql.Append(" FROM USERSETTING");
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
                string sql = "select count(*) from USERSETTING ";
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
                PagerSql.Append("FROM (SELECT * FROM USERSETTING ");
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

                    PagerSql.Append(" ORDER BY USERID");//默认按主键排序

                }
                PagerSql.AppendFormat(" ) A WHERE ROWNUM <= {0})", endNumber);
                PagerSql.AppendFormat(" WHERE RN >= {0}", startNumber);

                return _sqlHelper.ExecuteDateSet(PagerSql.ToString(), param).Tables[0];
            }

            #endregion

        }
    }
}

