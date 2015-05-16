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
    public partial class LEXICONWORDEntity
    {
        private SqlHelper _oracleHelper;

        #region const fields
        public const string DBName = "Sentiment";
        public const string TableName = "LEXICONWORD";
        public const string PrimaryKey = "PK_LEXICONWORD";
        #endregion

        #region columns
        public struct Columns
        {
            public const string ID = "ID";
            public const string WORD = "WORD";
            public const string TYPEID = "TYPEID";
            public const string WEIGHT = "WEIGHT";
        }
        #endregion

        #region constructors
        public LEXICONWORDEntity()
        {
            _oracleHelper = new SqlHelper(DBName);
        }

        public LEXICONWORDEntity(int id, string word, int typeid, int weight)
        {
            this.ID = id;

            this.WORD = word;

            this.TYPEID = typeid;

            this.WEIGHT = weight;

        }
        #endregion

        #region Properties

        public int? ID
        {
            get;
            set;
        }


        public string WORD
        {
            get;
            set;
        }


        public int? TYPEID
        {
            get;
            set;
        }


        public int? WEIGHT
        {
            get;
            set;
        }

        #endregion

        public class LEXICONWORDDAO : SqlDAO<LEXICONWORDEntity>
        {
            private SqlHelper _oracleHelper;
            public const string DBName = "SentimentConnStr";

            public LEXICONWORDDAO()
            {
                _oracleHelper = new SqlHelper(DBName);
            }

            public int AddEntity(LEXICONWORDEntity entity) {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into LEXICONWORD(");
                strSql.Append("WORD,TYPEID,WEIGHT)");
                strSql.Append(" values (");
                strSql.Append("@WORD,@TYPEID,@WEIGHT)");
                SqlParameter[] parameters = {
					new SqlParameter("@WORD",SqlDbType.Text),
					new SqlParameter("@TYPEID",SqlDbType.Int),
					new SqlParameter("@WEIGHT",SqlDbType.Int)
					};
                parameters[0].Value = entity.WORD;
                parameters[1].Value = entity.TYPEID;
                parameters[2].Value = entity.WEIGHT;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
                return ReturnNewRowId();
            }

            private int ReturnNewRowId()
            {
                string sql = "SELECT MAX(ID) FROM dbo.LEXICONWORD ";
                object NewId = _oracleHelper.GetSingle(sql, null);
                return Convert.ToInt32(NewId);
            }

            public override void Add(LEXICONWORDEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into LEXICONWORD(");
                strSql.Append("WORD,TYPEID,WEIGHT)");
                strSql.Append(" values (");
                strSql.Append("@WORD,@TYPEID,@WEIGHT)");
                SqlParameter[] parameters = {
					new SqlParameter("@WORD",SqlDbType.NText),
					new SqlParameter("@TYPEID",SqlDbType.Int),
					new SqlParameter("@WEIGHT",SqlDbType.Int)
					};
                parameters[0].Value = entity.WORD;
                parameters[1].Value = entity.TYPEID;
                parameters[2].Value = entity.WEIGHT;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override void Update(LEXICONWORDEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update LEXICONWORD set ");
                strSql.Append("WORD=@WORD,");
                strSql.Append("TYPEID=@TYPEID,");
                strSql.Append("WEIGHT=@WEIGHT");

                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@WORD",SqlDbType.NText),
					new SqlParameter("@TYPEID",SqlDbType.Int),
					new SqlParameter("@WEIGHT",SqlDbType.Int),
					new SqlParameter("@ID",SqlDbType.Int)
				};
                parameters[0].Value = entity.WORD;
                parameters[1].Value = entity.TYPEID;
                parameters[2].Value = entity.WEIGHT;
                parameters[3].Value = entity.ID;

                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update LEXICONWORD set ");
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

            public void DeleteByTypeId(int TypeId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from LEXICONWORD ");
                strSql.Append(" where TYPEID=@TYPEID");
                SqlParameter[] parameters = {
						new SqlParameter("@TYPEID", SqlDbType.Int)
					};
                parameters[0].Value = TypeId;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public void UpSetWordReg(string value, int id)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update LEXICONWORD set ");
                strSql.Append("WORD=@WORD");
                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {					
					new SqlParameter("@WORD",SqlDbType.NText),
					new SqlParameter("@ID",SqlDbType.Int)
					};                
                parameters[0].Value = value;
                parameters[1].Value = id;

                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public void UpSetWordWeight(int value, int id)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update LEXICONWORD set ");
                strSql.Append("WEIGHT=@WEIGHT");
                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {					
					new SqlParameter("@WEIGHT",SqlDbType.Int),
				    new SqlParameter("@ID",SqlDbType.Int)
					};
                
                parameters[0].Value = value;
                parameters[1].Value = id;

                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public void DeleteById(int id)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from LEXICONWORD ");
                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
						new SqlParameter("@ID", SqlDbType.Int)
					};
                parameters[0].Value = id;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }
            public override void Delete(LEXICONWORDEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from LEXICONWORD ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.ID;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override LEXICONWORDEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from LEXICONWORD ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    LEXICONWORDEntity entity = new LEXICONWORDEntity();
                    if (!Convert.IsDBNull(row["ID"]))
                    {
                        entity.ID = Convert.ToInt32(row["ID"]);
                    }
                    entity.WORD = row["WORD"].ToString();
                    if (!Convert.IsDBNull(row["TYPEID"]))
                    {
                        entity.TYPEID = Convert.ToInt32(row["TYPEID"]);
                    }
                    if (!Convert.IsDBNull(row["WEIGHT"]))
                    {
                        entity.WEIGHT = Convert.ToInt32(row["WEIGHT"]);
                    }
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public List<LEXICONWORDEntity> Find(string strWhere)
            {
                return Find(strWhere, null);
            }

            public override List<LEXICONWORDEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM LEXICONWORD ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<LEXICONWORDEntity> list = new List<LEXICONWORDEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        LEXICONWORDEntity entity = new LEXICONWORDEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        entity.WORD = row["WORD"].ToString();
                        if (!Convert.IsDBNull(row["TYPEID"]))
                        {
                            entity.TYPEID = Convert.ToInt32(row["TYPEID"]);
                        }
                        if (!Convert.IsDBNull(row["WEIGHT"]))
                        {
                            entity.WEIGHT = Convert.ToInt32(row["WEIGHT"]);
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
                strSql.Append(" FROM LEXICONWORD");
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
                string sql = "select count(*) from LEXICONWORD ";
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
                PagerSql.Append("FROM (SELECT * FROM LEXICONWORD ");
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

