﻿using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace Dropthings.Data
{
    [Serializable]
    public partial class CITYTOTALHITSEntity
    {
        private SqlHelper sqlHelper;

        #region const fields
        public const string DBName = "Sentiment";
        public const string TableName = "CITYTOTALHITS";
        public const string PrimaryKey = "PK_CITYTOTALHITS";
        #endregion
        #region columns
        public struct Columns
        {
            public const string ID = "ID";
            public const string PROVINCE = "PROVINCE";
            public const string TOTALHITS = "TOTALHITS";
            public const string INTIME = "INTIME";
            public const string TAG = "TAG";
        }
        #endregion
        #region constructors
        public CITYTOTALHITSEntity()
        {
            sqlHelper = new SqlHelper(DBName);
        }

        public CITYTOTALHITSEntity(int id, string province, int totalhits, DateTime intime, string tag)
        {
            //this.ID = id;

            this.PROVINCE = province;

            this.TOTALHITS = totalhits;

            this.INTIME = intime;

            this.TAG = tag;

        }
        #endregion
        #region Properties

        //public int? ID
        //{
        //    get;
        //    set;
        //}
        public string PROVINCE
        {
            get;
            set;
        }


        public int? TOTALHITS
        {
            get;
            set;
        }


        public DateTime? INTIME
        {
            get;
            set;
        }


        public string TAG
        {
            get;
            set;
        }

        #endregion
        public class CITYTOTALHITSDAO : SqlDAO<CITYTOTALHITSEntity>
        {
            private SqlHelper sqlHelper;
            public const string DBName = "SentimentConnStr";
            public CITYTOTALHITSDAO()
            {
                sqlHelper = new SqlHelper(DBName);
            }
            public override void Add(CITYTOTALHITSEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into CITYTOTALHITS(");
                strSql.Append("PROVINCE,TOTALHITS,INTIME,TAG)");
                strSql.Append(" values (");
                strSql.Append("@PROVINCE,@TOTALHITS,@INTIME,@TAG)");
                SqlParameter[] parameters = {
					new SqlParameter("@PROVINCE",SqlDbType.NVarChar),
					new SqlParameter("@TOTALHITS",SqlDbType.Int),
					new SqlParameter("@INTIME",SqlDbType.DateTime),
					new SqlParameter("@TAG",SqlDbType.NVarChar)
					};
                parameters[0].Value = entity.PROVINCE;
                parameters[1].Value = entity.TOTALHITS;
                parameters[2].Value = entity.INTIME;
                parameters[3].Value = entity.TAG;
                sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }
            public override void Update(CITYTOTALHITSEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update CITYTOTALHITS set ");
                strSql.Append("PROVINCE=@PROVINCE,");
                strSql.Append("TOTALHITS=@TOTALHITS,");
                strSql.Append("INTIME=@INTIME,");
                strSql.Append("TAG=@TAG");

                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@PROVINCE",SqlDbType.NVarChar),
					new SqlParameter("@TOTALHITS",SqlDbType.Int),
					new SqlParameter("@INTIME",SqlDbType.DateTime),
					new SqlParameter("@TAG",SqlDbType.NVarChar)//,
					//new SqlParameter("@ID",SqlDbType.Int)
				};
                parameters[0].Value = entity.PROVINCE;
                parameters[1].Value = entity.TOTALHITS;
                parameters[2].Value = entity.INTIME;
                parameters[3].Value = entity.TAG;
                //parameters[4].Value = entity.ID;

                sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }
            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update CITYTOTALHITS set ");
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
                string strSql = "delete from CITYTOTALHITS where ID=" + ID;
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
                string strSql = "delete from CITYTOTALHITS where ID in (" + ID + ")";
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
            public override void Delete(CITYTOTALHITSEntity entity)
            {
                //StringBuilder strSql = new StringBuilder();
                //strSql.Append("delete from CITYTOTALHITS ");
                //strSql.Append(" where ID=@primaryKeyId");
                //SqlParameter[] parameters = {
                //        new SqlParameter("@primaryKeyId", SqlDbType.Int)
                //    };
                //parameters[0].Value = entity.ID;
                //sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }
            public override CITYTOTALHITSEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from CITYTOTALHITS ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    CITYTOTALHITSEntity entity = new CITYTOTALHITSEntity();
                    //if (!Convert.IsDBNull(row["ID"]))
                    //{
                    //    entity.ID = Convert.ToInt32(row["ID"]);
                    //}
                    entity.PROVINCE = row["PROVINCE"].ToString();
                    if (!Convert.IsDBNull(row["TOTALHITS"]))
                    {
                        entity.TOTALHITS = Convert.ToInt32(row["TOTALHITS"]);
                    }
                    if (!Convert.IsDBNull(row["INTIME"]))
                    {
                        entity.INTIME = Convert.ToDateTime(row["INTIME"]);
                    }
                    entity.TAG = row["TAG"].ToString();
                    return entity;
                }
                else
                {
                    return null;
                }
            }
            public DataTable FindNew()
            {
                string strSql = "SELECT * FROM ( SELECT C.*, ROWNUM RN FROM "
                + "(SELECT A.TAG,A.TOTALHITS,B.ID FROM CITYTOTALHITS A,COLUMNDEF B "
                + "WHERE A.PROVINCE = B.COLUMNNAME ORDER BY A.INTIME DESC,A.TOTALHITS DESC) C)  WHERE ROWNUM <= 34";
                DataSet ds = sqlHelper.ExecuteDateSet(strSql, null);
                if (ds != null)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            public DataTable FindNewByCategory()
            {
                string strSql = "SELECT * FROM ( SELECT C.*, ROWNUM RN FROM "
                + "(SELECT A.TAG,A.TOTALHITS,B.CATEGORYID FROM CITYTOTALHITS A,CATEGORY B "
                + "WHERE A.PROVINCE = B.CATEGORYNAME ORDER BY A.INTIME DESC,A.TOTALHITS DESC) C)  WHERE ROWNUM <= 34";
                DataSet ds = sqlHelper.ExecuteDateSet(strSql, null);
                if (ds != null)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            public DataTable GetClusterResultDT(string jobName, int ClusterNum)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select JOBNAME,CLUSTERID,TITLE,count(TITLE) as NUM,min(URL) as URL,TAG");
                strSql.AppendFormat(" from clusterresult where JOBNAME='{0}'", jobName);
                strSql.AppendFormat(" AND CLUSTERID={0} group by JOBNAME, CLUSTERID, TITLE,TAG ORDER BY CLUSTERID,NUM DESC", ClusterNum);
                DataSet ds = sqlHelper.ExecuteDateSet(strSql.ToString(), null);
                if (ds != null)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            public DataTable GetJobListDT()
            {
                string strSql = "select JOBNAME,MIN(SYSTIMESPAN) AS SYSTIMESPAN from CLUSTERRESULT  GROUP BY JOBNAME ORDER BY JOBNAME DESC";
                DataSet ds = sqlHelper.ExecuteDateSet(strSql.ToString(), null);
                if (ds != null)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            public DataTable GetClusterIdDT(string JobName)
            {
                string strSql = "select CLUSTERID,MAX(TITLE) AS TITLE from CLUSTERRESULT WHERE JOBNAME='" + JobName + "' GROUP BY CLUSTERID ORDER BY CLUSTERID";
                DataSet ds = sqlHelper.ExecuteDateSet(strSql.ToString(), null);
                if (ds != null)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            public DataTable GetClusterResultPagerDT(int pageNumber, int pageSize, string jobName, string clusterid)
            {
                int startNumber = pageSize * (pageNumber - 1) + 1;
                int endNumber = pageSize * pageNumber;
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT * FROM ( SELECT A.*, ROWNUM RN FROM (SELECT JOBNAME,CLUSTERID,TITLE,count(TITLE) as NUM,min(URL) as URL,TAG");
                strSql.AppendFormat(" FROM CLUSTERRESULT  where JOBNAME='{0}' and CLUSTERID={1}", jobName, clusterid);
                strSql.AppendFormat(" group by JOBNAME, CLUSTERID, TITLE,TAG ORDER BY NUM DESC ) A WHERE ROWNUM <= {0}) WHERE RN >= {1}", endNumber, startNumber);
                DataSet ds = sqlHelper.ExecuteDateSet(strSql.ToString(), null);
                if (ds != null)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            public int GetClusterResultPagerCount(string jobName, string clusterid)
            {
                string strsql = "SELECT COUNT(*) FROM (SELECT COUNT(*) FROM CLUSTERRESULT WHERE JOBNAME='" + jobName + "' and CLUSTERID=" + clusterid + " group by TITLE)";
                object obj = sqlHelper.GetSingle(strsql, null);

                return obj == null ? 0 : Convert.ToInt32(obj);
            }
            public void Edit(string jobname, string cluserid, int HotTag, string url)
            {
                string strSql = "UPDATE CLUSTERRESULT SET TAG=" + HotTag.ToString() + " where URL='" + url + "' AND JOBNAME='" + jobname + "' AND CLUSTERID=" + cluserid;
                sqlHelper.ExecuteSql(strSql, null);
            }
            public override List<CITYTOTALHITSEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM CITYTOTALHITS ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<CITYTOTALHITSEntity> list = new List<CITYTOTALHITSEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        CITYTOTALHITSEntity entity = new CITYTOTALHITSEntity();
                        //if (!Convert.IsDBNull(row["ID"]))
                        //{
                        //    entity.ID = Convert.ToInt32(row["ID"]);
                        //}
                        entity.PROVINCE = row["PROVINCE"].ToString();
                        if (!Convert.IsDBNull(row["TOTALHITS"]))
                        {
                            entity.TOTALHITS = Convert.ToInt32(row["TOTALHITS"]);
                        }
                        if (!Convert.IsDBNull(row["INTIME"]))
                        {
                            entity.INTIME = Convert.ToDateTime(row["INTIME"]);
                        }
                        entity.TAG = row["TAG"].ToString();

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
                strSql.Append(" FROM CITYTOTALHITS");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                return sqlHelper.ExecuteDateSet(strSql.ToString(), param);
            }
            
            public DataSet GetGroupByProvince(string startTime, string endTime)
            {
                SqlParameter[] parameters = {
					new SqlParameter("@STARTTIME",SqlDbType.DateTime),
					new SqlParameter("@ENDTIME",SqlDbType.DateTime)
					};
                parameters[0].Value = DateTime.Parse(startTime);
                parameters[1].Value = DateTime.Parse(endTime);
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT Province,SUM(TotalHits) AS TotalHits  FROM  CityTotalHits WHERE  InTime   BETWEEN @STARTTIME AND @ENDTIME ");
                strSql.Append(" GROUP BY Province");
                return sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);

            }
            #region paging methods

            /// <summary>
            /// 获取分页记录总数
            /// </summary>
            /// <param name="where">条件，等同于GetPaer()方法的where</param>
            /// <returns>返回记录总数</returns>
            public int GetPagerRowsCount(string where, SqlParameter[] param)
            {
                string sql = "select count(*) from CITYTOTALHITS ";
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
                PagerSql.Append("FROM (SELECT * FROM CITYTOTALHITS ");
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

            #endregion

        }
    }
}

