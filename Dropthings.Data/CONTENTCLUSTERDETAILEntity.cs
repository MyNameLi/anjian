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
	public partial class CONTENTCLUSTERDETAILEntity
	{
		private SqlHelper _oracleHelper;
		
		#region const fields
	    public const string DBName = "Sentiment";
        public const string TableName = "CONTENTCLUSTERDETAIL";
        public const string PrimaryKey = "PK_CONTENTCLUSTERDETAIL";
        #endregion

        #region columns
		public struct Columns
		{
			public const string ID = "ID";	
			public const string URL = "URL";	
			public const string CLUSTERID = "CLUSTERID";	
			public const string TITLE = "TITLE";	
			public const string SUMMARY = "SUMMARY";	
			public const string SITENAME = "SITENAME";	
			public const string DOMAINSITE = "DOMAINSITE";	
			public const string DREDATE = "DREDATE";	
			public const string ADDDATE = "ADDDATE";	
			public const string TASKID = "TASKID";	
		}
		#endregion
		
		#region constructors
		public CONTENTCLUSTERDETAILEntity()
		{
			_oracleHelper = new SqlHelper(DBName);
		}
		
		public CONTENTCLUSTERDETAILEntity(int id,string url,string clusterid,string title,string summary,string sitename,string domainsite,DateTime dredate,DateTime adddate,int taskid)
		{
				this.ID=id;
				
				this.URL=url;
				
				this.CLUSTERID=clusterid;
				
				this.TITLE=title;
				
				this.SUMMARY=summary;
				
				this.SITENAME=sitename;
				
				this.DOMAINSITE=domainsite;
				
				this.DREDATE=dredate;
				
				this.ADDDATE=adddate;
				
				this.TASKID=taskid;
				
		}
		#endregion
		
		#region Properties
			
			public int? ID
			{
				get;
				set;
			}
		
			
			public string URL
			{
				get;
				set;
			}
		
			
			public string CLUSTERID
			{
				get;
				set;
			}
		
			
			public string TITLE
			{
				get;
				set;
			}
		
			
			public string SUMMARY
			{
				get;
				set;
			}
		
			
			public string SITENAME
			{
				get;
				set;
			}
		
			
			public string DOMAINSITE
			{
				get;
				set;
			}
		
			
			public DateTime? DREDATE
			{
				get;
				set;
			}
		
			
			public DateTime? ADDDATE
			{
				get;
				set;
			}
		
			
			public int? TASKID
			{
				get;
				set;
			}
		
		#endregion
				
		public class CONTENTCLUSTERDETAILDAO : SqlDAO<CONTENTCLUSTERDETAILEntity>
		{
			private SqlHelper _oracleHelper;
            public const string DBName = "SentimentConnStr";

            public CONTENTCLUSTERDETAILDAO()
            {
                _oracleHelper = new SqlHelper(DBName);
            }
			
			public override void Add(CONTENTCLUSTERDETAILEntity entity)
        	{
				
           	StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into CONTENTCLUSTERDETAIL(");
			strSql.Append("URL,CLUSTERID,TITLE,SUMMARY,SITENAME,DOMAINSITE,DREDATE,ADDDATE,TASKID)");
			strSql.Append(" values (");
			strSql.Append("@URL,@CLUSTERID,@TITLE,@SUMMARY,@SITENAME,@DOMAINSITE,@DREDATE,@ADDDATE,@TASKID)");			
			SqlParameter[] parameters = {
					new SqlParameter("@URL",SqlDbType.NVarChar),
					new SqlParameter("@CLUSTERID",SqlDbType.NVarChar),
					new SqlParameter("@TITLE",SqlDbType.NVarChar),
					new SqlParameter("@SUMMARY",SqlDbType.NVarChar),
					new SqlParameter("@SITENAME",SqlDbType.NVarChar),
					new SqlParameter("@DOMAINSITE",SqlDbType.NVarChar),
					new SqlParameter("@DREDATE",SqlDbType.DateTime),
					new SqlParameter("@ADDDATE",SqlDbType.DateTime),
					new SqlParameter("@TASKID",SqlDbType.Int)
					};
				parameters[0].Value = entity.URL;
				parameters[1].Value = entity.CLUSTERID;
				parameters[2].Value = entity.TITLE;
				parameters[3].Value = entity.SUMMARY;
				parameters[4].Value = entity.SITENAME;
				parameters[5].Value = entity.DOMAINSITE;
				parameters[6].Value = entity.DREDATE;
				parameters[7].Value = entity.ADDDATE;
				parameters[8].Value = entity.TASKID;
				_oracleHelper.ExecuteSql(strSql.ToString(),parameters);				
        	}
			
			public override void Update(CONTENTCLUSTERDETAILEntity entity)
        {
			
           StringBuilder strSql=new StringBuilder();
			strSql.Append("update CONTENTCLUSTERDETAIL set ");
				strSql.Append("URL=@URL,");
				strSql.Append("CLUSTERID=@CLUSTERID,");
				strSql.Append("TITLE=@TITLE,");
				strSql.Append("SUMMARY=@SUMMARY,");
				strSql.Append("SITENAME=@SITENAME,");
				strSql.Append("DOMAINSITE=@DOMAINSITE,");
				strSql.Append("DREDATE=@DREDATE,");
				strSql.Append("ADDDATE=@ADDDATE,");
				strSql.Append("TASKID=@TASKID");
		
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@URL",SqlDbType.NVarChar),
					new SqlParameter("@CLUSTERID",SqlDbType.NVarChar),
					new SqlParameter("@TITLE",SqlDbType.NVarChar),
					new SqlParameter("@SUMMARY",SqlDbType.NVarChar),
					new SqlParameter("@SITENAME",SqlDbType.NVarChar),
					new SqlParameter("@DOMAINSITE",SqlDbType.NVarChar),
					new SqlParameter("@DREDATE",SqlDbType.DateTime),
					new SqlParameter("@ADDDATE",SqlDbType.DateTime),
					new SqlParameter("@TASKID",SqlDbType.Int),
					new SqlParameter("@ID",SqlDbType.Int)
				};
				parameters[0].Value = entity.URL;
				parameters[1].Value = entity.CLUSTERID;
				parameters[2].Value = entity.TITLE;
				parameters[3].Value = entity.SUMMARY;
				parameters[4].Value = entity.SITENAME;
				parameters[5].Value = entity.DOMAINSITE;
				parameters[6].Value = entity.DREDATE;
				parameters[7].Value = entity.ADDDATE;
				parameters[8].Value = entity.TASKID;
				parameters[9].Value = entity.ID;
				
			_oracleHelper.ExecuteSql(strSql.ToString(),parameters);
        }
		
			public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update CONTENTCLUSTERDETAIL set ");
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
                string strSql = "delete from CONTENTCLUSTERDETAIL where ID=" + ID;
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
                string strSql = "delete from CONTENTCLUSTERDETAIL where ID in (" + ID + ")";
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
		
			public override void Delete(CONTENTCLUSTERDETAILEntity entity)
			{
				StringBuilder strSql=new StringBuilder();
				strSql.Append("delete from CONTENTCLUSTERDETAIL ");
				strSql.Append(" where ID=@primaryKeyId");
				SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
				parameters[0].Value = entity.ID;
				_oracleHelper.ExecuteSql(strSql.ToString(),parameters);
			}
			
			public override CONTENTCLUSTERDETAILEntity FindById(long primaryKeyId)
        	{
				StringBuilder strSql=new StringBuilder();
				strSql.Append("select * from CONTENTCLUSTERDETAIL ");
				strSql.Append(" where ID=@primaryKeyId");
				SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
				parameters[0].Value = primaryKeyId;
				DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
					CONTENTCLUSTERDETAILEntity entity=new CONTENTCLUSTERDETAILEntity();
					if(!Convert.IsDBNull(row["ID"]))
					{
						entity.ID = Convert.ToInt32(row["ID"]);
					}
					entity.URL=row["URL"].ToString();		
					entity.CLUSTERID=row["CLUSTERID"].ToString();		
					entity.TITLE=row["TITLE"].ToString();		
					entity.SUMMARY=row["SUMMARY"].ToString();		
					entity.SITENAME=row["SITENAME"].ToString();		
					entity.DOMAINSITE=row["DOMAINSITE"].ToString();		
					if(!Convert.IsDBNull(row["DREDATE"]))
					{
						entity.DREDATE = Convert.ToDateTime(row["DREDATE"]);
					}
					if(!Convert.IsDBNull(row["ADDDATE"]))
					{
						entity.ADDDATE = Convert.ToDateTime(row["ADDDATE"]);
					}
					if(!Convert.IsDBNull(row["TASKID"]))
					{
						entity.TASKID = Convert.ToInt32(row["TASKID"]);
					}
					return entity;
				}
				else
				{
					return null;
				}
			}

            public List<CONTENTCLUSTERDETAILEntity> FindByClusterId(string ClusterId)
            {
                string strwhere = " CLUSTERID=@CLUSTERID";
                SqlParameter[] parameters = {
						new SqlParameter("@CLUSTERID",SqlDbType.NVarChar)};
                parameters[0].Value = ClusterId;
                return Find(strwhere, parameters);
            }

            public List<CONTENTCLUSTERDETAILEntity> FindByTaskId(int TaskId)
            {
                string strwhere = " TASKID=@TASKID";
                SqlParameter[] parameters = {
						new SqlParameter("@TASKID", SqlDbType.Int)};
                parameters[0].Value = TaskId;
                return Find(strwhere, parameters);
            }
			
			public override List<CONTENTCLUSTERDETAILEntity> Find(string strWhere, SqlParameter[] parameters)
			{
				StringBuilder strSql=new StringBuilder();
				strSql.Append("select *");
				strSql.Append(" FROM CONTENTCLUSTERDETAIL ");
				if(strWhere.Trim()!="")
				{
					strSql.Append(" where "+strWhere);
				}
			
				DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
					List<CONTENTCLUSTERDETAILEntity> list= new List<CONTENTCLUSTERDETAILEntity>();
					foreach(DataRow row in ds.Tables[0].Rows)
					{
						CONTENTCLUSTERDETAILEntity entity=new CONTENTCLUSTERDETAILEntity();
						    if(!Convert.IsDBNull(row["ID"]))
							{
								entity.ID = Convert.ToInt32(row["ID"]);
							}
							entity.URL=row["URL"].ToString();
							entity.CLUSTERID=row["CLUSTERID"].ToString();
							entity.TITLE=row["TITLE"].ToString();
							entity.SUMMARY=row["SUMMARY"].ToString();
							entity.SITENAME=row["SITENAME"].ToString();
							entity.DOMAINSITE=row["DOMAINSITE"].ToString();
						    if(!Convert.IsDBNull(row["DREDATE"]))
							{
								entity.DREDATE = Convert.ToDateTime(row["DREDATE"]);
							}
						    if(!Convert.IsDBNull(row["ADDDATE"]))
							{
								entity.ADDDATE = Convert.ToDateTime(row["ADDDATE"]);
							}
						    if(!Convert.IsDBNull(row["TASKID"]))
							{
								entity.TASKID = Convert.ToInt32(row["TASKID"]);
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
				StringBuilder strSql=new StringBuilder();
				strSql.Append("select *");
				strSql.Append(" FROM CONTENTCLUSTERDETAIL");
				if(strWhere.Trim()!="")
				{
					strSql.Append(" where "+strWhere);
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
				string sql = "select count(*) from CONTENTCLUSTERDETAIL ";
				if (!string.IsNullOrEmpty(where))
				{
					sql += "where " + where;
				}
		
				object obj = _oracleHelper.GetSingle(sql,param);
		
				return obj == null ? 0 : Convert.ToInt32(obj);
			}

            public int GetPagerRowsCountByClusterId(string ClusterId)
            {
                string strwhere = " CLUSTERID=@CLUSTERID";
                SqlParameter[] parameters = {
						new SqlParameter("@CLUSTERID",SqlDbType.NVarChar)};
                parameters[0].Value = ClusterId;
                return GetPagerRowsCount(strwhere, parameters);
            }

            public int GetPagerRowsCountByTaskId(int TaskId)
            {
                string strwhere = " TASKID=@TASKID";
                SqlParameter[] parameters = {
						new SqlParameter("@TASKID", SqlDbType.Int)};
                parameters[0].Value = TaskId;
                return GetPagerRowsCount(strwhere, parameters);
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
				int startNumber = pageSize * (pageNumber-1) + 1;
				int endNumber = pageSize * pageNumber;
		
				StringBuilder PagerSql=new StringBuilder();
				PagerSql.Append("SELECT * FROM (");
				PagerSql.Append(" SELECT A.*, ROWNUM RN ");
				PagerSql.Append("FROM (SELECT * FROM CONTENTCLUSTERDETAIL ");
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
				PagerSql.AppendFormat(" ) A WHERE ROWNUM <= {0})",endNumber);
				PagerSql.AppendFormat(" WHERE RN >= {0}",startNumber);
		
				return _oracleHelper.ExecuteDateSet(PagerSql.ToString(),param).Tables[0];
			}


            public DataTable GetPager(string where, string orderBy, int pageSize, int pageNumber) {
                return GetPager(where, null, orderBy, pageSize, pageNumber);
            }
			#endregion
	
		}
	}
}

