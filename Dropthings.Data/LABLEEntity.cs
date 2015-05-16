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
    public partial class LABLEEntity
    {
        private SqlHelper _oracleHelper;

        #region const fields
        public const string DBName = "Sentiment";
        public const string TableName = "LABLE";
        public const string PrimaryKey = "PK_LABLEDB";
        #endregion

        #region columns
        public struct Columns
        {
            public const string ID = "ID";
            public const string LABLENAME = "LABLENAME";
            public const string LABLESTR = "LABLESTR";
            public const string LABLEDES = "LABLEDES";
        }
        #endregion

        #region constructors
        public LABLEEntity()
        {
            _oracleHelper = new SqlHelper(DBName);
        }

        public LABLEEntity(int id, string lablename, string lablestr, string labledes)
        {
            this.ID = id;

            this.LABLENAME = lablename;

            this.LABLESTR = lablestr;

            this.LABLEDES = labledes;

        }
        #endregion

        #region Properties

        public int? ID
        {
            get;
            set;
        }


        public string LABLENAME
        {
            get;
            set;
        }


        public string LABLESTR
        {
            get;
            set;
        }


        public string LABLEDES
        {
            get;
            set;
        }

        #endregion

        public class LABLEDAO : SqlDAO<LABLEEntity>
        {
            private SqlHelper _oracleHelper;
            public const string DBName = "SentimentConnStr";

            public LABLEDAO()
            {
                _oracleHelper = new SqlHelper(DBName);
            }

            public override void Add(LABLEEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into LABLE(");
                strSql.Append("LABLENAME,LABLESTR,LABLEDES)");
                strSql.Append(" values (");
                strSql.Append("@LABLENAME,@LABLESTR,@LABLEDES)");
                SqlParameter[] parameters = {
					new SqlParameter("@LABLENAME",SqlDbType.NVarChar),
					new SqlParameter("@LABLESTR",SqlDbType.NVarChar),
					new SqlParameter("@LABLEDES",SqlDbType.NVarChar)
					};
                parameters[0].Value = entity.LABLENAME;
                parameters[1].Value = entity.LABLESTR;
                parameters[2].Value = entity.LABLEDES;

                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override void Update(LABLEEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update LABLE set ");
                strSql.Append("LABLENAME=@LABLENAME,");
                strSql.Append("LABLESTR=@LABLESTR,");
                strSql.Append("LABLEDES=@LABLEDES");
                strSql.AppendFormat(" where ID=@ID", entity.ID);
                SqlParameter[] parameters = {
                    new SqlParameter("@LABLENAME",SqlDbType.NVarChar),
                    new SqlParameter("@LABLESTR",SqlDbType.NVarChar),
                    new SqlParameter("@LABLEDES",SqlDbType.NVarChar),
                    new SqlParameter("@ID",SqlDbType.Int)
                                               };

                parameters[0].Value = entity.LABLENAME;
                parameters[1].Value = entity.LABLESTR;
                parameters[2].Value = entity.LABLEDES;
                parameters[3].Value = entity.ID;

                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);

                //string strSql = "update LABLE set LABLENAME='A',LABLESTR='B',LABLEDES='C' where ID=2";
                //_sqlHelper.ExecuteNonQuery(_oracleHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            }

            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update LABLE set ");
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
                string strSql = "delete from LABLE where ID=" + ID;
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
                string strSql = "delete from LABLE where ID in (" + ID + ")";
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

            public override void Delete(LABLEEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from LABLE ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.ID;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override LABLEEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from LABLE ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    LABLEEntity entity = new LABLEEntity();
                    if (!Convert.IsDBNull(row["ID"]))
                    {
                        entity.ID = Convert.ToInt32(row["ID"]);
                    }
                    entity.LABLENAME = row["LABLENAME"].ToString();
                    entity.LABLESTR = row["LABLESTR"].ToString();
                    entity.LABLEDES = row["LABLEDES"].ToString();
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public List<LABLEEntity> Find(string strWhere) {
                return Find(strWhere, null);
            }

            public override List<LABLEEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM LABLE ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<LABLEEntity> list = new List<LABLEEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        LABLEEntity entity = new LABLEEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        entity.LABLENAME = row["LABLENAME"].ToString();
                        entity.LABLESTR = row["LABLESTR"].ToString();
                        entity.LABLEDES = row["LABLEDES"].ToString();

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
                strSql.Append(" FROM LABLE");
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
                string sql = "select count(*) from LABLE ";
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
                PagerSql.Append("FROM (SELECT * FROM LABLE ");
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

