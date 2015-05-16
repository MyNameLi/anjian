using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;
using System.Text;


namespace Dropthings.Data
{
    [Serializable]
    public partial class LexiconEntity
    {
        private SqlHelper sqlHelper;

        #region const fields
        public const string DBName = "SentimentConnStr";
        public const string TableName = "Lexicon";
        public const string PrimaryKey = "PK_Lexicon";
        #endregion

        #region columns
        public struct Columns
        {
            public const string ID = "ID";
            public const string FieldName = "FieldName";
            public const string FiledText = "FiledText";
        }
        #endregion

        #region constructors
        public LexiconEntity()
        {
            sqlHelper = new SqlHelper(DBName);
        }

        public LexiconEntity(long id, string fieldname, string filedtext)
        {
            this.ID = id;

            this.FieldName = fieldname;

            this.FiledText = filedtext;

        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public long? ID
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string FieldName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string FiledText
        {
            get;
            set;
        }

        #endregion

        public class LexiconDAO : SqlDAO<LexiconEntity>
        {
            private SqlHelper sqlHelper;
            public const string DBName = "SentimentConnStr";

            public LexiconDAO()
            {
                sqlHelper = new SqlHelper(DBName);
            }

            public override void Add(LexiconEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into Lexicon(");
                strSql.Append("FieldName,FiledText)");
                strSql.Append(" values (");
                strSql.Append("@FieldName,@FiledText)");
                SqlParameter[] parameters = {
					new SqlParameter("@FieldName",SqlDbType.NVarChar),
					new SqlParameter("@FiledText",SqlDbType.NVarChar)
					};
                parameters[0].Value = entity.FieldName;
                parameters[1].Value = entity.FiledText;

                sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override void Update(LexiconEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update Lexicon set ");
                strSql.Append("FieldName=@FieldName,");
                strSql.Append("FiledText=@FiledText");

                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@ID",SqlDbType.BigInt),
					new SqlParameter("@FieldName",SqlDbType.NVarChar),
					new SqlParameter("@FiledText",SqlDbType.NVarChar)
					};
                parameters[0].Value = entity.ID;
                parameters[1].Value = entity.FieldName;
                parameters[2].Value = entity.FiledText;

                sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update Lexicon set ");
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
                string strSql = "delete from Lexicon where ID=" + ID;
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

            public override void Delete(LexiconEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from Lexicon ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.ID;
                sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override LexiconEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from Lexicon ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    LexiconEntity entity = new LexiconEntity();
                    if (!Convert.IsDBNull(row["ID"]))
                    {
                        entity.ID = (long)row["ID"];
                    }
                    entity.FieldName = row["FieldName"].ToString();
                    entity.FiledText = row["FiledText"].ToString();
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public override List<LexiconEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM Lexicon(nolock) ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<LexiconEntity> list = new List<LexiconEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        LexiconEntity entity = new LexiconEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = (long)row["ID"];
                        }
                        entity.FieldName = row["FieldName"].ToString();
                        entity.FiledText = row["FiledText"].ToString();

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
                strSql.Append("select FieldName,FiledText");
                strSql.Append(" FROM Lexicon(nolock)");
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
                string sql = "select count(*) from Lexicon ";
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
            public DataTable GetPager(string where, SqlParameter[] param, Hashtable ht, int pageSize, int pageNumber)
            {
                int startNumber = pageSize * (pageNumber - 1);

                string sql = string.Format("SELECT TOP {0} * FROM (SELECT ROW_NUMBER() OVER", pageSize);

                if (ht != null && ht.Count > 0)
                {
                    sql += " (ORDER BY";
                    foreach (string key in ht.Keys)
                    {
                        sql += string.Format(" {0} {1},", key, ht[key].ToString());
                    }
                    sql += "  ID )";
                }
                else
                {

                    sql += " (ORDER BY ID )";//默认按主键排序

                }

                sql += " AS RowNumber,* FROM Lexicon";

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

