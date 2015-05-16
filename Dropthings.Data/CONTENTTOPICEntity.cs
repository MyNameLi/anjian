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
	public partial class CONTENTTOPICEntity
	{
		private SqlHelper _oracleHelper;
		
		#region const fields
	    public const string DBName = "Sentiment";
        public const string TableName = "CONTENTTOPIC";
        public const string PrimaryKey = "PK_CONTENTTOPIC";
        #endregion

        #region columns
		public struct Columns
		{
			public const string ID = "ID";	
			public const string NEWSURL = "NEWSURL";	
			public const string NEWSTITLE = "NEWSTITLE";	
			public const string KEYWORD = "KEYWORD";	
			public const string BAIDUCONTENTID = "BAIDUCONTENTID";	
			public const string SEARCHTYPE = "SEARCHTYPE";	
			public const string TASKSTATUS = "TASKSTATUS";	
			public const string DOMAINSITE = "DOMAINSITE";	
			public const string RECEIVEDATE = "RECEIVEDATE";	
			public const string STARTDATE = "STARTDATE";	
			public const string ENDDATE = "ENDDATE";	
		}
		#endregion
		
		#region constructors
		public CONTENTTOPICEntity()
		{
			_oracleHelper = new SqlHelper(DBName);
		}
		
		public CONTENTTOPICEntity(int id,string newsurl,string newstitle,string keyword,string baiducontentid,string searchtype,string taskstatus,string domainsite,DateTime receivedate,DateTime startdate,DateTime enddate)
		{
				this.ID=id;
				
				this.NEWSURL=newsurl;
				
				this.NEWSTITLE=newstitle;
				
				this.KEYWORD=keyword;
				
				this.BAIDUCONTENTID=baiducontentid;
				
				this.SEARCHTYPE=searchtype;
				
				this.TASKSTATUS=taskstatus;
				
				this.DOMAINSITE=domainsite;
				
				this.RECEIVEDATE=receivedate;
				
				this.STARTDATE=startdate;
				
				this.ENDDATE=enddate;
				
		}
		#endregion
		
		#region Properties
			
			public int? ID
			{
				get;
				set;
			}
		
			
			public string NEWSURL
			{
				get;
				set;
			}
		
			
			public string NEWSTITLE
			{
				get;
				set;
			}
		
			
			public string KEYWORD
			{
				get;
				set;
			}
		
			
			public string BAIDUCONTENTID
			{
				get;
				set;
			}
		
			
			public string SEARCHTYPE
			{
				get;
				set;
			}
		
			
			public string TASKSTATUS
			{
				get;
				set;
			}
		
			
			public string DOMAINSITE
			{
				get;
				set;
			}
		
			
			public DateTime? RECEIVEDATE
			{
				get;
				set;
			}
		
			
			public DateTime? STARTDATE
			{
				get;
				set;
			}
		
			
			public DateTime? ENDDATE
			{
				get;
				set;
			}
		
		#endregion
				
		public class CONTENTTOPICDAO : SqlDAO<CONTENTTOPICEntity>
		{
			private SqlHelper _oracleHelper;
            public const string DBName = "SentimentConnStr";

            public CONTENTTOPICDAO()
            {
                _oracleHelper = new SqlHelper(DBName);
            }
			
			public override void Add(CONTENTTOPICEntity entity)
        	{
				
           	StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into CONTENTTOPIC(");
			strSql.Append("NEWSURL,NEWSTITLE,KEYWORD,BAIDUCONTENTID,SEARCHTYPE,TASKSTATUS,DOMAINSITE,RECEIVEDATE,STARTDATE,ENDDATE)");
			strSql.Append(" values (");
			strSql.Append("@NEWSURL,@NEWSTITLE,@KEYWORD,@BAIDUCONTENTID,@SEARCHTYPE,@TASKSTATUS,@DOMAINSITE,@RECEIVEDATE,@STARTDATE,@ENDDATE)");			
			SqlParameter[] parameters = {
					new SqlParameter("@NEWSURL",SqlDbType.NVarChar),
					new SqlParameter("@NEWSTITLE",SqlDbType.NVarChar),
					new SqlParameter("@KEYWORD",SqlDbType.NVarChar),
					new SqlParameter("@BAIDUCONTENTID",SqlDbType.NVarChar),
					new SqlParameter("@SEARCHTYPE",SqlDbType.NVarChar),
					new SqlParameter("@TASKSTATUS",SqlDbType.NVarChar),
					new SqlParameter("@DOMAINSITE",SqlDbType.NVarChar),
					new SqlParameter("@RECEIVEDATE",SqlDbType.DateTime),
					new SqlParameter("@STARTDATE",SqlDbType.DateTime),
					new SqlParameter("@ENDDATE",SqlDbType.DateTime)
					};
				parameters[0].Value = entity.NEWSURL;
				parameters[1].Value = entity.NEWSTITLE;
				parameters[2].Value = entity.KEYWORD;
				parameters[3].Value = entity.BAIDUCONTENTID;
				parameters[4].Value = entity.SEARCHTYPE;
				parameters[5].Value = entity.TASKSTATUS;
				parameters[6].Value = entity.DOMAINSITE;
				parameters[7].Value = entity.RECEIVEDATE;
				parameters[8].Value = entity.STARTDATE;
				parameters[9].Value = entity.ENDDATE;
				_oracleHelper.ExecuteSql(strSql.ToString(),parameters);				
        	}

            public override void Update(CONTENTTOPICEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update CONTENTTOPIC set ");
                strSql.Append("NEWSURL=@NEWSURL,");
                strSql.Append("NEWSTITLE=@NEWSTITLE,");
                strSql.Append("KEYWORD=@KEYWORD,");
                strSql.Append("BAIDUCONTENTID=@BAIDUCONTENTID,");
                strSql.Append("SEARCHTYPE=@SEARCHTYPE,");
                strSql.Append("TASKSTATUS=@TASKSTATUS,");
                strSql.Append("DOMAINSITE=@DOMAINSITE,");
                strSql.Append("RECEIVEDATE=@RECEIVEDATE,");
                strSql.Append("STARTDATE=@STARTDATE,");
                strSql.Append("ENDDATE=@ENDDATE");

                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@NEWSURL",SqlDbType.NVarChar),
					new SqlParameter("@NEWSTITLE",SqlDbType.NVarChar),
					new SqlParameter("@KEYWORD",SqlDbType.NVarChar),
					new SqlParameter("@BAIDUCONTENTID",SqlDbType.NVarChar),
					new SqlParameter("@SEARCHTYPE",SqlDbType.NVarChar),
					new SqlParameter("@TASKSTATUS",SqlDbType.NVarChar),
					new SqlParameter("@DOMAINSITE",SqlDbType.NVarChar),
					new SqlParameter("@RECEIVEDATE",SqlDbType.DateTime),
					new SqlParameter("@STARTDATE",SqlDbType.DateTime),
					new SqlParameter("@ENDDATE",SqlDbType.DateTime),
					new SqlParameter("@ID",SqlDbType.Int)
				};
                parameters[0].Value = entity.NEWSURL;
                parameters[1].Value = entity.NEWSTITLE;
                parameters[2].Value = entity.KEYWORD;
                parameters[3].Value = entity.BAIDUCONTENTID;
                parameters[4].Value = entity.SEARCHTYPE;
                parameters[5].Value = entity.TASKSTATUS;
                parameters[6].Value = entity.DOMAINSITE;
                parameters[7].Value = entity.RECEIVEDATE;
                parameters[8].Value = entity.STARTDATE;
                parameters[9].Value = entity.ENDDATE;
                parameters[10].Value = entity.ID;

                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }
		
			public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update CONTENTTOPIC set ");
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
                string strSql = "delete from CONTENTTOPIC where ID=" + ID;
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
                string strSql = "delete from CONTENTTOPIC where ID in (" + ID + ")";
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
		
			public override void Delete(CONTENTTOPICEntity entity)
			{
				StringBuilder strSql=new StringBuilder();
				strSql.Append("delete from CONTENTTOPIC ");
				strSql.Append(" where ID=@primaryKeyId");
				SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
				parameters[0].Value = entity.ID;
				_oracleHelper.ExecuteSql(strSql.ToString(),parameters);
			}
			
			public override CONTENTTOPICEntity FindById(long primaryKeyId)
        	{
				StringBuilder strSql=new StringBuilder();
				strSql.Append("select * from CONTENTTOPIC ");
				strSql.Append(" where ID=@primaryKeyId");
				SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
				parameters[0].Value = primaryKeyId;
				DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
					CONTENTTOPICEntity entity=new CONTENTTOPICEntity();
					if(!Convert.IsDBNull(row["ID"]))
					{
						entity.ID = Convert.ToInt32(row["ID"]);
					}
					entity.NEWSURL=row["NEWSURL"].ToString();		
					entity.NEWSTITLE=row["NEWSTITLE"].ToString();		
					entity.KEYWORD=row["KEYWORD"].ToString();		
					entity.BAIDUCONTENTID=row["BAIDUCONTENTID"].ToString();		
					entity.SEARCHTYPE=row["SEARCHTYPE"].ToString();		
					entity.TASKSTATUS=row["TASKSTATUS"].ToString();		
					entity.DOMAINSITE=row["DOMAINSITE"].ToString();		
					if(!Convert.IsDBNull(row["RECEIVEDATE"]))
					{
						entity.RECEIVEDATE = Convert.ToDateTime(row["RECEIVEDATE"]);
					}
					if(!Convert.IsDBNull(row["STARTDATE"]))
					{
						entity.STARTDATE = Convert.ToDateTime(row["STARTDATE"]);
					}
					if(!Convert.IsDBNull(row["ENDDATE"]))
					{
						entity.ENDDATE = Convert.ToDateTime(row["ENDDATE"]);
					}
					return entity;
				}
				else
				{
					return null;
				}
			}
			
			public override List<CONTENTTOPICEntity> Find(string strWhere, SqlParameter[] parameters)
			{
				StringBuilder strSql=new StringBuilder();
				strSql.Append("select *");
				strSql.Append(" FROM CONTENTTOPIC ");
				if(strWhere.Trim()!="")
				{
					strSql.Append(" where "+strWhere);
				}
			
				DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
					List<CONTENTTOPICEntity> list= new List<CONTENTTOPICEntity>();
					foreach(DataRow row in ds.Tables[0].Rows)
					{
						CONTENTTOPICEntity entity=new CONTENTTOPICEntity();
						    if(!Convert.IsDBNull(row["ID"]))
							{
								entity.ID = Convert.ToInt32(row["ID"]);
							}
							entity.NEWSURL=row["NEWSURL"].ToString();
							entity.NEWSTITLE=row["NEWSTITLE"].ToString();
							entity.KEYWORD=row["KEYWORD"].ToString();
							entity.BAIDUCONTENTID=row["BAIDUCONTENTID"].ToString();
							entity.SEARCHTYPE=row["SEARCHTYPE"].ToString();
							entity.TASKSTATUS=row["TASKSTATUS"].ToString();
							entity.DOMAINSITE=row["DOMAINSITE"].ToString();
						    if(!Convert.IsDBNull(row["RECEIVEDATE"]))
							{
								entity.RECEIVEDATE = Convert.ToDateTime(row["RECEIVEDATE"]);
							}
						    if(!Convert.IsDBNull(row["STARTDATE"]))
							{
								entity.STARTDATE = Convert.ToDateTime(row["STARTDATE"]);
							}
						    if(!Convert.IsDBNull(row["ENDDATE"]))
							{
								entity.ENDDATE = Convert.ToDateTime(row["ENDDATE"]);
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
				strSql.Append(" FROM CONTENTTOPIC");
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
				string sql = "select count(*) from CONTENTTOPIC ";
				if (!string.IsNullOrEmpty(where))
				{
					sql += "where " + where;
				}
		
				object obj = _oracleHelper.GetSingle(sql,param);
		
				return obj == null ? 0 : Convert.ToInt32(obj);
			}

            public int GetPagerRowsCount(string where) {
                return GetPagerRowsCount(where, null);
            }
			
			/// <summary>
			/// 查询分页信息，返回当前页码的记录集
			/// </summary>
			/// <param name="where">查询条件，可为empty</param>
			/// <param name="orderBy">排序条件，可为empty</param>
			/// <param name="pageSize">每页显示记录数</param>
			/// <param name="pageNumber">当前页码</param>
			/// <returns>datatable</returns>
            /// 

            public DataTable GetPager(string where, string orderBy, int pageSize, int pageNumber) {
                return GetPager(where, null, orderBy, pageSize, pageNumber);
            }

			public DataTable GetPager(string where, SqlParameter[] param, string orderBy, int pageSize, int pageNumber)
			{
				int startNumber = pageSize * (pageNumber-1) + 1;
				int endNumber = pageSize * pageNumber;
		
				StringBuilder PagerSql=new StringBuilder();
				PagerSql.Append("SELECT * FROM (");
				PagerSql.Append(" SELECT A.*, ROWNUM RN ");
				PagerSql.Append("FROM (SELECT * FROM CONTENTTOPIC ");
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

			#endregion
	
		}
	}
}

