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
    public partial class ALARMEntity
    {
        private SqlHelper sqlHelper;

        #region const fields
        public const string DBName = "Sentiment";
        public const string TableName = "ALARM";
        public const string PrimaryKey = "PK_WORDALARM";
        #endregion

        #region columns
        public struct Columns
        {
            public const string ID = "ID";
            public const string DOCREF = "DOCREF";
            public const string DOCTITLE = "DOCTITLE";
            public const string DOCTIME = "DOCTIME";
            public const string DOCSITE = "DOCSITE";
            public const string WARNINGMSGID = "WARNINGMSGID";
            public const string WORDWARNINGID = "WORDWARNINGID";
            public const string RULEALARMNUM = "RULEALARMNUM";
            public const string ALARMREPLYNUM = "ALARMREPLYNUM";
            public const string ALARMCLICKRATE = "ALARMCLICKRATE";
            public const string USERNAME = "USERNAME";
            public const string ISLOOK = "ISLOOK";
        }
        #endregion

        #region constructors
        public ALARMEntity()
        {
            sqlHelper = new SqlHelper(DBName);
        }

        public ALARMEntity(int id, string docref, string doctitle, string doctime, string docsite, int warningmsgid, int wordwarningid, int rulealarmnum, int alarmreplynum, int alarmclickrate, string username, int islook)
        {
            this.ID = id;

            this.DOCREF = docref;

            this.DOCTITLE = doctitle;

            this.DOCTIME = doctime;

            this.DOCSITE = docsite;

            this.WARNINGMSGID = warningmsgid;

            this.WORDWARNINGID = wordwarningid;

            this.RULEALARMNUM = rulealarmnum;

            this.ALARMREPLYNUM = alarmreplynum;

            this.ALARMCLICKRATE = alarmclickrate;

            this.USERNAME = username;

            this.ISLOOK = islook;

        }
        #endregion

        #region Properties

        public int? ID
        {
            get;
            set;
        }


        public string DOCREF
        {
            get;
            set;
        }


        public string DOCTITLE
        {
            get;
            set;
        }


        public string DOCTIME
        {
            get;
            set;
        }


        public string DOCSITE
        {
            get;
            set;
        }


        public int? WARNINGMSGID
        {
            get;
            set;
        }


        public int? WORDWARNINGID
        {
            get;
            set;
        }


        public int? RULEALARMNUM
        {
            get;
            set;
        }


        public int? ALARMREPLYNUM
        {
            get;
            set;
        }


        public int? ALARMCLICKRATE
        {
            get;
            set;
        }


        public string USERNAME
        {
            get;
            set;
        }


        public int? ISLOOK
        {
            get;
            set;
        }

        #endregion

        public class ALARMDAO : SqlDAO<ALARMEntity>
        {
            private SqlHelper sqlHelper;
            public const string DBName = "SentimentConnStr";

            public ALARMDAO()
            {
                sqlHelper = new SqlHelper(DBName);
            }

            public override void Add(ALARMEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into ALARM(");
                strSql.Append("DOCREF,DOCTITLE,DOCTIME,DOCSITE,WARNINGMSGID,WORDWARNINGID,RULEALARMNUM,ALARMREPLYNUM,ALARMCLICKRATE,USERNAME,ISLOOK)");
                strSql.Append(" values (");
                strSql.Append("@DOCREF,@DOCTITLE,@DOCTIME,@DOCSITE,@WARNINGMSGID,@WORDWARNINGID,@RULEALARMNUM,@ALARMREPLYNUM,@ALARMCLICKRATE,@USERNAME,@ISLOOK)");
                SqlParameter[] parameters = {
					new SqlParameter("@DOCREF",SqlDbType.NVarChar),
					new SqlParameter("@DOCTITLE",SqlDbType.NVarChar),
					new SqlParameter("@DOCTIME",SqlDbType.NVarChar),
					new SqlParameter("@DOCSITE",SqlDbType.NVarChar),
					new SqlParameter("@WARNINGMSGID",SqlDbType.Int),
					new SqlParameter("@WORDWARNINGID",SqlDbType.Int),
					new SqlParameter("@RULEALARMNUM",SqlDbType.Int),
					new SqlParameter("@ALARMREPLYNUM",SqlDbType.Int),
					new SqlParameter("@ALARMCLICKRATE",SqlDbType.Int),
					new SqlParameter("@USERNAME",SqlDbType.NText),
					new SqlParameter("@ISLOOK",SqlDbType.Int)
					};
                parameters[0].Value = entity.DOCREF;
                parameters[1].Value = entity.DOCTITLE;
                parameters[2].Value = entity.DOCTIME;
                parameters[3].Value = entity.DOCSITE;
                parameters[4].Value = entity.WARNINGMSGID;
                parameters[5].Value = entity.WORDWARNINGID;
                parameters[6].Value = entity.RULEALARMNUM;
                parameters[7].Value = entity.ALARMREPLYNUM;
                parameters[8].Value = entity.ALARMCLICKRATE;
                parameters[9].Value = entity.USERNAME;
                parameters[10].Value = entity.ISLOOK;
                sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override void Update(ALARMEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update ALARM set ");
                strSql.Append("DOCREF=@DOCREF,");
                strSql.Append("DOCTITLE=@DOCTITLE,");
                strSql.Append("DOCTIME=@DOCTIME,");
                strSql.Append("DOCSITE=@DOCSITE,");
                strSql.Append("WARNINGMSGID=@WARNINGMSGID,");
                strSql.Append("WORDWARNINGID=@WORDWARNINGID,");
                strSql.Append("RULEALARMNUM=@RULEALARMNUM,");
                strSql.Append("ALARMREPLYNUM=@ALARMREPLYNUM,");
                strSql.Append("ALARMCLICKRATE=@ALARMCLICKRATE,");
                strSql.Append("USERNAME=@USERNAME,");
                strSql.Append("ISLOOK=@ISLOOK");

                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@DOCREF",SqlDbType.NVarChar),
					new SqlParameter("@DOCTITLE",SqlDbType.NVarChar),
					new SqlParameter("@DOCTIME",SqlDbType.NVarChar),
					new SqlParameter("@DOCSITE",SqlDbType.NVarChar),
					new SqlParameter("@WARNINGMSGID",SqlDbType.Int),
					new SqlParameter("@WORDWARNINGID",SqlDbType.Int),
					new SqlParameter("@RULEALARMNUM",SqlDbType.Int),
					new SqlParameter("@ALARMREPLYNUM",SqlDbType.Int),
					new SqlParameter("@ALARMCLICKRATE",SqlDbType.Int),
					new SqlParameter("@USERNAME",SqlDbType.NText),
					new SqlParameter("@ISLOOK",SqlDbType.Int),
					new SqlParameter("@ID",SqlDbType.Int)
				};
                parameters[0].Value = entity.DOCREF;
                parameters[1].Value = entity.DOCTITLE;
                parameters[2].Value = entity.DOCTIME;
                parameters[3].Value = entity.DOCSITE;
                parameters[4].Value = entity.WARNINGMSGID;
                parameters[5].Value = entity.WORDWARNINGID;
                parameters[6].Value = entity.RULEALARMNUM;
                parameters[7].Value = entity.ALARMREPLYNUM;
                parameters[8].Value = entity.ALARMCLICKRATE;
                parameters[9].Value = entity.USERNAME;
                parameters[10].Value = entity.ISLOOK;
                parameters[11].Value = entity.ID;

                sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update ALARM set ");
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
                string strSql = "delete from ALARM where ID=" + ID;
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
                string strSql = "delete from ALARM where ID in (" + ID + ")";
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

            public override void Delete(ALARMEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from ALARM ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.ID;
                sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override ALARMEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from ALARM ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    ALARMEntity entity = new ALARMEntity();
                    if (!Convert.IsDBNull(row["ID"]))
                    {
                        entity.ID = Convert.ToInt32(row["ID"]);
                    }
                    entity.DOCREF = row["DOCREF"].ToString();
                    entity.DOCTITLE = row["DOCTITLE"].ToString();
                    entity.DOCTIME = row["DOCTIME"].ToString();
                    entity.DOCSITE = row["DOCSITE"].ToString();
                    if (!Convert.IsDBNull(row["WARNINGMSGID"]))
                    {
                        entity.WARNINGMSGID = Convert.ToInt32(row["WARNINGMSGID"]);
                    }
                    if (!Convert.IsDBNull(row["WORDWARNINGID"]))
                    {
                        entity.WORDWARNINGID = Convert.ToInt32(row["WORDWARNINGID"]);
                    }
                    if (!Convert.IsDBNull(row["RULEALARMNUM"]))
                    {
                        entity.RULEALARMNUM = Convert.ToInt32(row["RULEALARMNUM"]);
                    }
                    if (!Convert.IsDBNull(row["ALARMREPLYNUM"]))
                    {
                        entity.ALARMREPLYNUM = Convert.ToInt32(row["ALARMREPLYNUM"]);
                    }
                    if (!Convert.IsDBNull(row["ALARMCLICKRATE"]))
                    {
                        entity.ALARMCLICKRATE = Convert.ToInt32(row["ALARMCLICKRATE"]);
                    }
                    entity.USERNAME = row["USERNAME"].ToString();
                    if (!Convert.IsDBNull(row["ISLOOK"]))
                    {
                        entity.ISLOOK = Convert.ToInt32(row["ISLOOK"]);
                    }
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public override List<ALARMEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM ALARM ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<ALARMEntity> list = new List<ALARMEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ALARMEntity entity = new ALARMEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        entity.DOCREF = row["DOCREF"].ToString();
                        entity.DOCTITLE = row["DOCTITLE"].ToString();
                        entity.DOCTIME = row["DOCTIME"].ToString();
                        entity.DOCSITE = row["DOCSITE"].ToString();
                        if (!Convert.IsDBNull(row["WARNINGMSGID"]))
                        {
                            entity.WARNINGMSGID = Convert.ToInt32(row["WARNINGMSGID"]);
                        }
                        if (!Convert.IsDBNull(row["WORDWARNINGID"]))
                        {
                            entity.WORDWARNINGID = Convert.ToInt32(row["WORDWARNINGID"]);
                        }
                        if (!Convert.IsDBNull(row["RULEALARMNUM"]))
                        {
                            entity.RULEALARMNUM = Convert.ToInt32(row["RULEALARMNUM"]);
                        }
                        if (!Convert.IsDBNull(row["ALARMREPLYNUM"]))
                        {
                            entity.ALARMREPLYNUM = Convert.ToInt32(row["ALARMREPLYNUM"]);
                        }
                        if (!Convert.IsDBNull(row["ALARMCLICKRATE"]))
                        {
                            entity.ALARMCLICKRATE = Convert.ToInt32(row["ALARMCLICKRATE"]);
                        }
                        entity.USERNAME = row["USERNAME"].ToString();
                        if (!Convert.IsDBNull(row["ISLOOK"]))
                        {
                            entity.ISLOOK = Convert.ToInt32(row["ISLOOK"]);
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
                strSql.Append(" FROM ALARM");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                return sqlHelper.ExecuteDateSet(strSql.ToString(), param);
            }

            #region paging methods

            public int GetPagerRowsCount(string where) {
                return GetPagerRowsCount(where, null);
            }

            /// <summary>
            /// 获取分页记录总数
            /// </summary>
            /// <param name="where">条件，等同于GetPaer()方法的where</param>
            /// <returns>返回记录总数</returns>
            public int GetPagerRowsCount(string where, SqlParameter[] param)
            {
                string sql = "select count(*) from ALARM ";
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
                PagerSql.Append("FROM (SELECT * FROM ALARM ");
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

            public DataTable GetSiteWarningPager(string where, string orderBy, int pageSize, int pageNumber) {
                return GetSiteWarningPager(where, null, orderBy, pageSize, pageNumber);
            }

            public DataTable GetSiteWarningPager(string where, SqlParameter[] param, string orderBy, int pageSize, int pageNumber)
            {
                int startNumber = pageSize * (pageNumber - 1) + 1;
                int endNumber = pageSize * pageNumber;

                StringBuilder PagerSql = new StringBuilder();
                PagerSql.Append("SELECT * FROM ( SELECT A.*, ROWNUM RN FROM ");
                PagerSql.Append("(SELECT B.DOCREF,B.DOCTITLE,B.DOCTIME,B.DOCSITE,B.ALARMREPLYNUM,");
                PagerSql.Append("B.ALARMCLICKRATE,C.PAGEVIEW,C.INVITATION FROM ALARM B,WARNINGMSG C WHERE B.WARNINGMSGID = C.ID");               
                if (!string.IsNullOrEmpty(where))
                {
                    PagerSql.Append(" AND " + where);
                }
                if (!string.IsNullOrEmpty(orderBy))
                {
                    PagerSql.AppendFormat(" ORDER BY {0}", orderBy);
                }
                else
                {
                    PagerSql.Append(" ORDER BY B.ID");//默认按主键排序

                }
                PagerSql.AppendFormat(" ) A WHERE ROWNUM <= {0})", endNumber);
                PagerSql.AppendFormat(" WHERE RN >= {0}", startNumber);

                return sqlHelper.ExecuteDateSet(PagerSql.ToString(), param).Tables[0];
            }

            public DataTable GetWordRuleWarningPager(string where, string orderBy, int pageSize, int pageNumber)
            {
                return GetWordRuleWarningPager(where, null, orderBy, pageSize, pageNumber);
            }

            public DataTable GetWordRuleWarningPager(string where, SqlParameter[] param, string orderBy, int pageSize, int pageNumber)
            {
                int startNumber = pageSize * (pageNumber - 1) + 1;
                int endNumber = pageSize * pageNumber;

                StringBuilder PagerSql = new StringBuilder();
                PagerSql.Append("SELECT * FROM ( SELECT A.*, ROWNUM RN FROM ");
                PagerSql.Append("(SELECT B.DOCREF,B.DOCTITLE,B.DOCTIME,B.DOCSITE,B.RULEALARMNUM,");
                PagerSql.Append("C.WORDRULE,C.THRESHOLDS FROM ALARM B,WORDWARNING C WHERE B.WORDWARNINGID = C.ID");
                if (!string.IsNullOrEmpty(where))
                {
                    PagerSql.Append(" AND " + where);
                }
                if (!string.IsNullOrEmpty(orderBy))
                {
                    PagerSql.AppendFormat(" ORDER BY {0}", orderBy);
                }
                else
                {
                    PagerSql.Append(" ORDER BY B.ID");//默认按主键排序

                }
                PagerSql.AppendFormat(" ) A WHERE ROWNUM <= {0})", endNumber);
                PagerSql.AppendFormat(" WHERE RN >= {0}", startNumber);

                return sqlHelper.ExecuteDateSet(PagerSql.ToString(), param).Tables[0];
            }

            #endregion

        }
    }
}

