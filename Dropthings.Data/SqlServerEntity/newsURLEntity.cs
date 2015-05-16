using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace Dropthings.Data.SqlServerEntity
{
    [Serializable]
    public partial class newsURLEntity
    {
        private SqlHelper sqlHelper;

        #region const fields
        public const string DBName = "SqlEventsConnStr";
        public const string TableName = "newsURL";
        public const string PrimaryKey = "PK__newsURL__3213E83F1A14E395";
        #endregion

        #region columns
        public struct Columns
        {
            public const string id = "id";
            public const string title = "title";
            public const string url = "url";
            public const string source = "source";
            public const string datetime = "datetime";
            public const string summary = "summary";
            public const string SE = "SE";
            public const string kid = "kid";
            public const string inserttime = "inserttime";
            public const string urlmd5 = "urlmd5";
            public const string tag = "tag";
            public const string processTime = "processTime";
        }
        #endregion

        #region constructors
        public newsURLEntity()
        {
            sqlHelper = new SqlHelper(DBName);
        }

        public newsURLEntity(long id, string title, string url, string source, string datetime, string summary, string se, int kid, DateTime inserttime, string urlmd5, int tag, DateTime processtime)
        {
            this.id = id;

            this.title = title;

            this.url = url;

            this.source = source;

            this.datetime = datetime;

            this.summary = summary;

            this.SE = se;

            this.kid = kid;

            this.inserttime = inserttime;

            this.urlmd5 = urlmd5;

            this.tag = tag;

            this.processTime = processtime;

        }
        #endregion

        #region Properties

        public long? id
        {
            get;
            set;
        }


        public string title
        {
            get;
            set;
        }


        public string url
        {
            get;
            set;
        }


        public string source
        {
            get;
            set;
        }


        public string datetime
        {
            get;
            set;
        }


        public string summary
        {
            get;
            set;
        }


        public string SE
        {
            get;
            set;
        }


        public int? kid
        {
            get;
            set;
        }


        public DateTime? inserttime
        {
            get;
            set;
        }


        public string urlmd5
        {
            get;
            set;
        }


        public int? tag
        {
            get;
            set;
        }


        public DateTime? processTime
        {
            get;
            set;
        }

        #endregion

        public class newsURLDAO : SqlDAO<newsURLEntity>
        {
            private SqlHelper sqlHelper;

            public const string DBName = "SqlEventsConnStr";
            public newsURLDAO()
            {
                sqlHelper = new SqlHelper(DBName);
            }

            public override void Add(newsURLEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into newsURL(");
                strSql.Append("title,url,source,datetime,summary,SE,kid,inserttime,urlmd5,tag,processTime)");
                strSql.Append(" values (");
                strSql.Append("@title,@url,@source,@datetime,@summary,@SE,@kid,@inserttime,@urlmd5,@tag,@processTime)");
                SqlParameter[] parameters = {
					new SqlParameter("@title",SqlDbType.NVarChar),
					new SqlParameter("@url",SqlDbType.NVarChar),
					new SqlParameter("@source",SqlDbType.NVarChar),
					new SqlParameter("@datetime",SqlDbType.NVarChar),
					new SqlParameter("@summary",SqlDbType.NVarChar),
					new SqlParameter("@SE",SqlDbType.NVarChar),
					new SqlParameter("@kid",SqlDbType.Int),
					new SqlParameter("@inserttime",SqlDbType.DateTime),
					new SqlParameter("@urlmd5",SqlDbType.NVarChar),
					new SqlParameter("@tag",SqlDbType.Int),
					new SqlParameter("@processTime",SqlDbType.DateTime)
					};
                parameters[0].Value = entity.title;
                parameters[1].Value = entity.url;
                parameters[2].Value = entity.source;
                parameters[3].Value = entity.datetime;
                parameters[4].Value = entity.summary;
                parameters[5].Value = entity.SE;
                parameters[6].Value = entity.kid;
                parameters[7].Value = entity.inserttime;
                parameters[8].Value = entity.urlmd5;
                parameters[9].Value = entity.tag;
                parameters[10].Value = entity.processTime;

                sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override void Update(newsURLEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update newsURL set ");
                strSql.Append("title=@title,");
                strSql.Append("url=@url,");
                strSql.Append("source=@source,");
                strSql.Append("datetime=@datetime,");
                strSql.Append("summary=@summary,");
                strSql.Append("SE=@SE,");
                strSql.Append("kid=@kid,");
                strSql.Append("inserttime=@inserttime,");
                strSql.Append("urlmd5=@urlmd5,");
                strSql.Append("tag=@tag,");
                strSql.Append("processTime=@processTime");

                strSql.Append(" where id=@id");
                SqlParameter[] parameters = {
					new SqlParameter("@id",SqlDbType.BigInt),
					new SqlParameter("@title",SqlDbType.NVarChar),
					new SqlParameter("@url",SqlDbType.NVarChar),
					new SqlParameter("@source",SqlDbType.NVarChar),
					new SqlParameter("@datetime",SqlDbType.NVarChar),
					new SqlParameter("@summary",SqlDbType.NVarChar),
					new SqlParameter("@SE",SqlDbType.NVarChar),
					new SqlParameter("@kid",SqlDbType.Int),
					new SqlParameter("@inserttime",SqlDbType.DateTime),
					new SqlParameter("@urlmd5",SqlDbType.NVarChar),
					new SqlParameter("@tag",SqlDbType.Int),
					new SqlParameter("@processTime",SqlDbType.DateTime)
					};
                parameters[0].Value = entity.id;
                parameters[1].Value = entity.title;
                parameters[2].Value = entity.url;
                parameters[3].Value = entity.source;
                parameters[4].Value = entity.datetime;
                parameters[5].Value = entity.summary;
                parameters[6].Value = entity.SE;
                parameters[7].Value = entity.kid;
                parameters[8].Value = entity.inserttime;
                parameters[9].Value = entity.urlmd5;
                parameters[10].Value = entity.tag;
                parameters[11].Value = entity.processTime;

                sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override void Delete(newsURLEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from newsURL ");
                strSql.Append(" where id=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.id;
                sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override newsURLEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from newsURL ");
                strSql.Append(" where id=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    newsURLEntity entity = new newsURLEntity();
                    if (!Convert.IsDBNull(row["id"]))
                    {
                        entity.id = (long)row["id"];
                    }
                    entity.title = row["title"].ToString();
                    entity.url = row["url"].ToString();
                    entity.source = row["source"].ToString();
                    entity.datetime = row["datetime"].ToString();
                    entity.summary = row["summary"].ToString();
                    entity.SE = row["SE"].ToString();
                    if (!Convert.IsDBNull(row["kid"]))
                    {
                        entity.kid = (int)row["kid"];
                    }
                    if (!Convert.IsDBNull(row["inserttime"]))
                    {
                        entity.inserttime = (DateTime)row["inserttime"];
                    }
                    entity.urlmd5 = row["urlmd5"].ToString();
                    if (!Convert.IsDBNull(row["tag"]))
                    {
                        entity.tag = (int)row["tag"];
                    }
                    if (!Convert.IsDBNull(row["processTime"]))
                    {
                        entity.processTime = (DateTime)row["processTime"];
                    }
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public override List<newsURLEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM newsURL(nolock) ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<newsURLEntity> list = new List<newsURLEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        newsURLEntity entity = new newsURLEntity();
                        if (!Convert.IsDBNull(row["id"]))
                        {
                            entity.id = (long)row["id"];
                        }
                        entity.title = row["title"].ToString();
                        entity.url = row["url"].ToString();
                        entity.source = row["source"].ToString();
                        entity.datetime = row["datetime"].ToString();
                        entity.summary = row["summary"].ToString();
                        entity.SE = row["SE"].ToString();
                        if (!Convert.IsDBNull(row["kid"]))
                        {
                            entity.kid = (int)row["kid"];
                        }
                        if (!Convert.IsDBNull(row["inserttime"]))
                        {
                            entity.inserttime = (DateTime)row["inserttime"];
                        }
                        entity.urlmd5 = row["urlmd5"].ToString();
                        if (!Convert.IsDBNull(row["tag"]))
                        {
                            entity.tag = (int)row["tag"];
                        }
                        if (!Convert.IsDBNull(row["processTime"]))
                        {
                            entity.processTime = (DateTime)row["processTime"];
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
                strSql.Append(" FROM newsURL(nolock)");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                return sqlHelper.ExecuteDateSet(strSql.ToString(), param);
            }
            public int UpdateEntitys(string log, string ids)
            {
                string sql = "update newsURL set tag=" + log + ",processtime=GETDATE() where id in(" + ids + ")";
                return sqlHelper.ExecuteSql(sql, null);
            }
            #region paging methods

            /// <summary>
            /// 获取分页记录总数
            /// </summary>
            /// <param name="where">条件，等同于GetPaer()方法的where</param>
            /// <returns>返回记录总数</returns>
            public int GetPagerRowsCount(string where, SqlParameter[] param)
            {
                string sql = "select count(*) from newsURL ";
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
                int startNumber = pageSize * (pageNumber - 1);

                string sql = string.Format("SELECT TOP {0} * FROM (SELECT ROW_NUMBER() OVER", pageSize);

                if (!string.IsNullOrEmpty(orderBy))
                {
                    sql += string.Format(" (ORDER BY {0})", orderBy);
                }
                else
                {

                    sql += " (ORDER BY id)";//默认按主键排序

                }

                sql += " AS RowNumber,* FROM newsURL";

                if (!string.IsNullOrEmpty(where))
                {
                    sql += " where " + where;
                }

                sql += " ) _myResults WHERE RowNumber>" + startNumber.ToString();

                return sqlHelper.ExecuteDateSet(sql, param).Tables[0];
            }

            #endregion

        }
    }
}
