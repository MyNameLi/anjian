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
    public partial class CONTENTRULEEntity
    {
        private SqlHelper _oracleHelper;

        #region const fields
        public const string DBName = "Sentiment";
        public const string TableName = "CONTENTRULE";
        public const string PrimaryKey = "PK_CONTENTRULE";
        #endregion

        #region columns
        public struct Columns
        {
            public const string FIELDSTR = "FIELDSTR";
            public const string FIELDTYPE = "FIELDTYPE";
            public const string FIELDREG = "FIELDREG";
            public const string FIELDSOURCE = "FIELDSOURCE";
            public const string TASKID = "TASKID";
            public const string FIELDSUFFIX = "FIELDSUFFIX";
            public const string ISREMOVEHTML = "ISREMOVEHTML";
            public const string ISINTERVAR = "ISINTERVAR";
            public const string ISDATE = "ISDATE";
            public const string PARAM1 = "PARAM1";
            public const string PARAM2 = "PARAM2";
            public const string PARAM3 = "PARAM3";
            public const string PARAM4 = "PARAM4";	
        }
        #endregion

        #region constructors
        public CONTENTRULEEntity()
        {
            _oracleHelper = new SqlHelper(DBName);
        }

        public CONTENTRULEEntity(string fieldstr, int fieldtype, string fieldreg, string fieldsource, int taskid, string fieldsuffix, int isremovehtml, int isintervar, int isdate, string param1, string param2, string param3, string param4)
        {            

            this.FIELDSTR = fieldstr;

            this.FIELDTYPE = fieldtype;

            this.FIELDREG = fieldreg;

            this.FIELDSOURCE = fieldsource;

            this.TASKID = taskid;

            this.FIELDSUFFIX = fieldsuffix;

            this.ISREMOVEHTML = isremovehtml;

            this.ISINTERVAR = isintervar;

            this.ISDATE = isdate;

            this.PARAM1 = param1;

            this.PARAM2 = param2;

            this.PARAM3 = param3;

            this.PARAM4 = param4;

        }
        #endregion

        #region Properties


        public string FIELDSTR
        {
            get;
            set;
        }


        public int? FIELDTYPE
        {
            get;
            set;
        }


        public string FIELDREG
        {
            get;
            set;
        }


        public string FIELDSOURCE
        {
            get;
            set;
        }


        public int? TASKID
        {
            get;
            set;
        }


        public string FIELDSUFFIX
        {
            get;
            set;
        }


        public int? ISREMOVEHTML
        {
            get;
            set;
        }


        public int? ISINTERVAR
        {
            get;
            set;
        }


        public int? ISDATE
        {
            get;
            set;
        }

        public string PARAM1
        {
            get;
            set;
        }


        public string PARAM2
        {
            get;
            set;
        }


        public string PARAM3
        {
            get;
            set;
        }


        public string PARAM4
        {
            get;
            set;
        }

        #endregion

        public class CONTENTRULEDAO : SqlDAO<CONTENTRULEEntity>
        {
            private SqlHelper _oracleHelper;
            public const string DBName = "SentimentConnStr";

            public CONTENTRULEDAO()
            {
                _oracleHelper = new SqlHelper(DBName);
            }

            public override void Add(CONTENTRULEEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into CONTENTRULE(");
                strSql.Append("FIELDSTR,FIELDTYPE,FIELDREG,FIELDSOURCE,TASKID,FIELDSUFFIX,ISREMOVEHTML,ISINTERVAR,ISDATE,PARAM1,PARAM2,PARAM3,PARAM4)");
                strSql.Append(" values (");
                strSql.Append("@FIELDSTR,@FIELDTYPE,@FIELDREG,@FIELDSOURCE,@TASKID,@FIELDSUFFIX,@ISREMOVEHTML,@ISINTERVAR,@ISDATE,@PARAM1,@PARAM2,@PARAM3,@PARAM4)");
                SqlParameter[] parameters = {
					new SqlParameter("@FIELDSTR",SqlDbType.NVarChar),
					new SqlParameter("@FIELDTYPE",SqlDbType.Int),
					new SqlParameter("@FIELDREG",SqlDbType.NVarChar),
					new SqlParameter("@FIELDSOURCE",SqlDbType.NVarChar),
					new SqlParameter("@TASKID",SqlDbType.Int),
					new SqlParameter("@FIELDSUFFIX",SqlDbType.NVarChar),
					new SqlParameter("@ISREMOVEHTML",SqlDbType.Int),
					new SqlParameter("@ISINTERVAR",SqlDbType.Int),
					new SqlParameter("@ISDATE",SqlDbType.Int),
					new SqlParameter("@PARAM1",SqlDbType.NVarChar),
					new SqlParameter("@PARAM2",SqlDbType.NVarChar),
					new SqlParameter("@PARAM3",SqlDbType.NVarChar),
					new SqlParameter("@PARAM4",SqlDbType.NVarChar)
					};
                parameters[0].Value = entity.FIELDSTR;
                parameters[1].Value = entity.FIELDTYPE;
                parameters[2].Value = entity.FIELDREG;
                parameters[3].Value = entity.FIELDSOURCE;
                parameters[4].Value = entity.TASKID;
                parameters[5].Value = entity.FIELDSUFFIX;
                parameters[6].Value = entity.ISREMOVEHTML;
                parameters[7].Value = entity.ISINTERVAR;
                parameters[8].Value = entity.ISDATE;
                parameters[9].Value = entity.PARAM1;
                parameters[10].Value = entity.PARAM2;
                parameters[11].Value = entity.PARAM3;
                parameters[12].Value = entity.PARAM4;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);		
            }

            public bool DeleteByTaskID(int TaskID)
            {
                string strSql = "delete from CONTENTRULE where TASKID=" + TaskID;
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

            public bool DeleteByTaskID(string TaskID)
            {
                string strSql = "delete from CONTENTRULE where TASKID in (" + TaskID + ")";
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

            public List<CONTENTRULEEntity> Find(string strWhere) {
                return Find(strWhere, null);
            }

            public override List<CONTENTRULEEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM CONTENTRULE ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<CONTENTRULEEntity> list = new List<CONTENTRULEEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        CONTENTRULEEntity entity = new CONTENTRULEEntity();
                        entity.FIELDSTR = row["FIELDSTR"].ToString();
                        if (!Convert.IsDBNull(row["FIELDTYPE"]))
                        {
                            entity.FIELDTYPE = Convert.ToInt32(row["FIELDTYPE"]);
                        }
                        entity.FIELDREG = row["FIELDREG"].ToString();
                        entity.FIELDSOURCE = row["FIELDSOURCE"].ToString();
                        if (!Convert.IsDBNull(row["TASKID"]))
                        {
                            entity.TASKID = Convert.ToInt32(row["TASKID"]);
                        }
                        entity.FIELDSUFFIX = row["FIELDSUFFIX"].ToString();
                        if (!Convert.IsDBNull(row["ISREMOVEHTML"]))
                        {
                            entity.ISREMOVEHTML = Convert.ToInt32(row["ISREMOVEHTML"]);
                        }
                        if (!Convert.IsDBNull(row["ISINTERVAR"]))
                        {
                            entity.ISINTERVAR = Convert.ToInt32(row["ISINTERVAR"]);
                        }
                        if (!Convert.IsDBNull(row["ISDATE"]))
                        {
                            entity.ISDATE = Convert.ToInt32(row["ISDATE"]);
                        }
                        entity.PARAM1 = row["PARAM1"].ToString();
                        entity.PARAM2 = row["PARAM2"].ToString();
                        entity.PARAM3 = row["PARAM3"].ToString();
                        entity.PARAM4 = row["PARAM4"].ToString();	

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
                strSql.Append(" FROM CONTENTRULE");
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
                string sql = "select count(*) from CONTENTRULE ";
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
                PagerSql.Append("FROM (SELECT * FROM CONTENTRULE ");
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

                    PagerSql.Append(" ORDER BY FIELDID");//默认按主键排序

                }
                PagerSql.AppendFormat(" ) A WHERE ROWNUM <= {0})", endNumber);
                PagerSql.AppendFormat(" WHERE RN >= {0}", startNumber);

                return _oracleHelper.ExecuteDateSet(PagerSql.ToString(), param).Tables[0];
            }

            #endregion

        }
    }
}

