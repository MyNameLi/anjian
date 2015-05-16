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
    public partial class TASKEntity
    {
        private SqlHelper _oracleHelper;

        #region const fields
        public const string DBName = "Sentiment";
        public const string TableName = "TASK";
        public const string PrimaryKey = "PK_TASK";
        #endregion

        #region columns
        public struct Columns
        {
            public const string TASKID = "TASKID";
            public const string TASKNAME = "TASKNAME";
            public const string URLENTRY = "URLENTRY";
            public const string SITECODE = "SITECODE";
            public const string SPIDERDEGREE = "SPIDERDEGREE";
            public const string ISAGENT = "ISAGENT";
            public const string AGENTSERVERIP = "AGENTSERVERIP";
            public const string AGENTSERVERPORT = "AGENTSERVERPORT";
            public const string AGENTSERVERUSER = "AGENTSERVERUSER";
            public const string AGENTSERVERPWD = "AGENTSERVERPWD";
            public const string ISLOGIN = "ISLOGIN";
            public const string LOGINSITE = "LOGINSITE";
            public const string LOGINDATA = "LOGINDATA";
            public const string ISPOST = "ISPOST";
            public const string ISUPDATE = "ISUPDATE";
            public const string UPDATETIMESPAN = "UPDATETIMESPAN";
            public const string PAGEURLREG = "PAGEURLREG";
            public const string PAGECONTENTREG = "PAGECONTENTREG";
            public const string SITEID = "SITEID";
            public const string URLPREFIX = "URLPREFIX";
            public const string TASKDES = "TASKDES";
            public const string TASKTYPE = "TASKTYPE";
        }
        #endregion

        #region constructors
        public TASKEntity()
        {
            _oracleHelper = new SqlHelper(DBName);
        }

        public TASKEntity(int taskid, string taskname, string urlentry, string sitecode, int spiderdegree, int isagent, string agentserverip, int agentserverport, string agentserveruser, string agentserverpwd, int islogin, string loginsite, string logindata, int ispost, int isupdate, int updatetimespan, string pageurlreg, string pagecontentreg, int siteid, string urlprefix, string taskdes, string tasktype)
        {
            this.TASKID = taskid;

            this.TASKNAME = taskname;

            this.URLENTRY = urlentry;

            this.SITECODE = sitecode;

            this.SPIDERDEGREE = spiderdegree;

            this.ISAGENT = isagent;

            this.AGENTSERVERIP = agentserverip;

            this.AGENTSERVERPORT = agentserverport;

            this.AGENTSERVERUSER = agentserveruser;

            this.AGENTSERVERPWD = agentserverpwd;

            this.ISLOGIN = islogin;

            this.LOGINSITE = loginsite;

            this.LOGINDATA = logindata;

            this.ISPOST = ispost;

            this.ISUPDATE = isupdate;

            this.UPDATETIMESPAN = updatetimespan;

            this.PAGEURLREG = pageurlreg;

            this.PAGECONTENTREG = pagecontentreg;

            this.SITEID = siteid;

            this.URLPREFIX = urlprefix;

            this.TASKDES = taskdes;

            this.TASKTYPE = tasktype;

        }
        #endregion

        #region Properties

        public int? TASKID
        {
            get;
            set;
        }


        public string TASKNAME
        {
            get;
            set;
        }


        public string URLENTRY
        {
            get;
            set;
        }


        public string SITECODE
        {
            get;
            set;
        }


        public int? SPIDERDEGREE
        {
            get;
            set;
        }


        public int? ISAGENT
        {
            get;
            set;
        }


        public string AGENTSERVERIP
        {
            get;
            set;
        }


        public int? AGENTSERVERPORT
        {
            get;
            set;
        }


        public string AGENTSERVERUSER
        {
            get;
            set;
        }


        public string AGENTSERVERPWD
        {
            get;
            set;
        }


        public int? ISLOGIN
        {
            get;
            set;
        }


        public string LOGINSITE
        {
            get;
            set;
        }


        public string LOGINDATA
        {
            get;
            set;
        }


        public int? ISPOST
        {
            get;
            set;
        }


        public int? ISUPDATE
        {
            get;
            set;
        }


        public int? UPDATETIMESPAN
        {
            get;
            set;
        }


        public string PAGEURLREG
        {
            get;
            set;
        }


        public string PAGECONTENTREG
        {
            get;
            set;
        }


        public int? SITEID
        {
            get;
            set;
        }


        public string URLPREFIX
        {
            get;
            set;
        }


        public string TASKDES
        {
            get;
            set;
        }


        public string TASKTYPE
        {
            get;
            set;
        }

        #endregion

        public class TASKDAO : SqlDAO<TASKEntity>
        {
            private SqlHelper _oracleHelper;
            public const string DBName = "SentimentConnStr";

            public TASKDAO()
            {
                _oracleHelper = new SqlHelper(DBName);
            }

            public override void Add(TASKEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into TASK(");
                strSql.Append("TASKNAME,URLENTRY,SITECODE,SPIDERDEGREE,ISAGENT,AGENTSERVERIP,AGENTSERVERPORT,AGENTSERVERUSER,AGENTSERVERPWD,ISLOGIN,LOGINSITE,LOGINDATA,ISPOST,ISUPDATE,UPDATETIMESPAN,PAGEURLREG,PAGECONTENTREG,SITEID,URLPREFIX,TASKDES,TASKTYPE)");
                strSql.Append(" values (");
                strSql.Append("@TASKNAME,@URLENTRY,@SITECODE,@SPIDERDEGREE,@ISAGENT,@AGENTSERVERIP,@AGENTSERVERPORT,@AGENTSERVERUSER,@AGENTSERVERPWD,@ISLOGIN,@LOGINSITE,@LOGINDATA,@ISPOST,@ISUPDATE,@UPDATETIMESPAN,@PAGEURLREG,@PAGECONTENTREG,@SITEID,@URLPREFIX,@TASKDES,@TASKTYPE)");
                SqlParameter[] parameters = {
					new SqlParameter("@TASKNAME",SqlDbType.NVarChar),
					new SqlParameter("@URLENTRY",SqlDbType.NVarChar),
					new SqlParameter("@SITECODE",SqlDbType.NVarChar),
					new SqlParameter("@SPIDERDEGREE",SqlDbType.Int),
					new SqlParameter("@ISAGENT",SqlDbType.Int),
					new SqlParameter("@AGENTSERVERIP",SqlDbType.NVarChar),
					new SqlParameter("@AGENTSERVERPORT",SqlDbType.Int),
					new SqlParameter("@AGENTSERVERUSER",SqlDbType.NVarChar),
					new SqlParameter("@AGENTSERVERPWD",SqlDbType.NVarChar),
					new SqlParameter("@ISLOGIN",SqlDbType.Int),
					new SqlParameter("@LOGINSITE",SqlDbType.NVarChar),
					new SqlParameter("@LOGINDATA",SqlDbType.NVarChar),
					new SqlParameter("@ISPOST",SqlDbType.Int),
					new SqlParameter("@ISUPDATE",SqlDbType.Int),
					new SqlParameter("@UPDATETIMESPAN",SqlDbType.Int),
					new SqlParameter("@PAGEURLREG",SqlDbType.NVarChar),
					new SqlParameter("@PAGECONTENTREG",SqlDbType.NVarChar),
					new SqlParameter("@SITEID",SqlDbType.Int),
					new SqlParameter("@URLPREFIX",SqlDbType.NVarChar),
					new SqlParameter("@TASKDES",SqlDbType.NVarChar),
					new SqlParameter("@TASKTYPE",SqlDbType.NVarChar)
					};
                parameters[0].Value = entity.TASKNAME;
                parameters[1].Value = entity.URLENTRY;
                parameters[2].Value = entity.SITECODE;
                parameters[3].Value = entity.SPIDERDEGREE;
                parameters[4].Value = entity.ISAGENT;
                parameters[5].Value = entity.AGENTSERVERIP;
                parameters[6].Value = entity.AGENTSERVERPORT;
                parameters[7].Value = entity.AGENTSERVERUSER;
                parameters[8].Value = entity.AGENTSERVERPWD;
                parameters[9].Value = entity.ISLOGIN;
                parameters[10].Value = entity.LOGINSITE;
                parameters[11].Value = entity.LOGINDATA;
                parameters[12].Value = entity.ISPOST;
                parameters[13].Value = entity.ISUPDATE;
                parameters[14].Value = entity.UPDATETIMESPAN;
                parameters[15].Value = entity.PAGEURLREG;
                parameters[16].Value = entity.PAGECONTENTREG;
                parameters[17].Value = entity.SITEID;
                parameters[18].Value = entity.URLPREFIX;
                parameters[19].Value = entity.TASKDES;
                parameters[20].Value = entity.TASKTYPE;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public int AddGetTaskId(TASKEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into TASK(");
                strSql.Append("TASKNAME,URLENTRY,SITECODE,SPIDERDEGREE,ISAGENT,AGENTSERVERIP,AGENTSERVERPORT,AGENTSERVERUSER,AGENTSERVERPWD,ISLOGIN,LOGINSITE,LOGINDATA,ISPOST,ISUPDATE,UPDATETIMESPAN,PAGEURLREG,PAGECONTENTREG,SITEID,URLPREFIX,TASKDES,TASKTYPE)");
                strSql.Append(" values (");
                strSql.Append("@TASKNAME,@URLENTRY,@SITECODE,@SPIDERDEGREE,@ISAGENT,@AGENTSERVERIP,@AGENTSERVERPORT,@AGENTSERVERUSER,@AGENTSERVERPWD,@ISLOGIN,@LOGINSITE,@LOGINDATA,@ISPOST,@ISUPDATE,@UPDATETIMESPAN,@PAGEURLREG,@PAGECONTENTREG,@SITEID,@URLPREFIX,@TASKDES,@TASKTYPE)");
                SqlParameter[] parameters = {
					new SqlParameter("@TASKNAME",SqlDbType.NVarChar),
					new SqlParameter("@URLENTRY",SqlDbType.NVarChar),
					new SqlParameter("@SITECODE",SqlDbType.NVarChar),
					new SqlParameter("@SPIDERDEGREE",SqlDbType.Int),
					new SqlParameter("@ISAGENT",SqlDbType.Int),
					new SqlParameter("@AGENTSERVERIP",SqlDbType.NVarChar),
					new SqlParameter("@AGENTSERVERPORT",SqlDbType.Int),
					new SqlParameter("@AGENTSERVERUSER",SqlDbType.NVarChar),
					new SqlParameter("@AGENTSERVERPWD",SqlDbType.NVarChar),
					new SqlParameter("@ISLOGIN",SqlDbType.Int),
					new SqlParameter("@LOGINSITE",SqlDbType.NVarChar),
					new SqlParameter("@LOGINDATA",SqlDbType.NVarChar),
					new SqlParameter("@ISPOST",SqlDbType.Int),
					new SqlParameter("@ISUPDATE",SqlDbType.Int),
					new SqlParameter("@UPDATETIMESPAN",SqlDbType.Int),
					new SqlParameter("@PAGEURLREG",SqlDbType.NVarChar),
					new SqlParameter("@PAGECONTENTREG",SqlDbType.NVarChar),
					new SqlParameter("@SITEID",SqlDbType.Int),
					new SqlParameter("@URLPREFIX",SqlDbType.NVarChar),
					new SqlParameter("@TASKDES",SqlDbType.NVarChar),
					new SqlParameter("@TASKTYPE",SqlDbType.NVarChar)
					};
                parameters[0].Value = entity.TASKNAME;
                parameters[1].Value = entity.URLENTRY;
                parameters[2].Value = entity.SITECODE;
                parameters[3].Value = entity.SPIDERDEGREE;
                parameters[4].Value = entity.ISAGENT;
                parameters[5].Value = entity.AGENTSERVERIP;
                parameters[6].Value = entity.AGENTSERVERPORT;
                parameters[7].Value = entity.AGENTSERVERUSER;
                parameters[8].Value = entity.AGENTSERVERPWD;
                parameters[9].Value = entity.ISLOGIN;
                parameters[10].Value = entity.LOGINSITE;
                parameters[11].Value = entity.LOGINDATA;
                parameters[12].Value = entity.ISPOST;
                parameters[13].Value = entity.ISUPDATE;
                parameters[14].Value = entity.UPDATETIMESPAN;
                parameters[15].Value = entity.PAGEURLREG;
                parameters[16].Value = entity.PAGECONTENTREG;
                parameters[17].Value = entity.SITEID;
                parameters[18].Value = entity.URLPREFIX;
                parameters[19].Value = entity.TASKDES;
                parameters[20].Value = entity.TASKTYPE;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
                return ReturnNewRowId();
            }

            private int ReturnNewRowId()
            {
                string sql = "select TASK_ID_SEQ.currval NEWID from dual";
                object NewId = _oracleHelper.GetSingle(sql, null);
                return Convert.ToInt32(NewId);
            }

            public override void Update(TASKEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update TASK set ");
                strSql.Append("TASKNAME=@TASKNAME,");
                strSql.Append("URLENTRY=@URLENTRY,");
                strSql.Append("SITECODE=@SITECODE,");
                strSql.Append("SPIDERDEGREE=@SPIDERDEGREE,");
                strSql.Append("ISAGENT=@ISAGENT,");
                strSql.Append("AGENTSERVERIP=@AGENTSERVERIP,");
                strSql.Append("AGENTSERVERPORT=@AGENTSERVERPORT,");
                strSql.Append("AGENTSERVERUSER=@AGENTSERVERUSER,");
                strSql.Append("AGENTSERVERPWD=@AGENTSERVERPWD,");
                strSql.Append("ISLOGIN=@ISLOGIN,");
                strSql.Append("LOGINSITE=@LOGINSITE,");
                strSql.Append("LOGINDATA=@LOGINDATA,");
                strSql.Append("ISPOST=@ISPOST,");
                strSql.Append("ISUPDATE=@ISUPDATE,");
                strSql.Append("UPDATETIMESPAN=@UPDATETIMESPAN,");
                strSql.Append("PAGEURLREG=@PAGEURLREG,");
                strSql.Append("PAGECONTENTREG=@PAGECONTENTREG,");
                strSql.Append("SITEID=@SITEID,");
                strSql.Append("URLPREFIX=@URLPREFIX,");
                strSql.Append("TASKDES=@TASKDES,");
                strSql.Append("TASKTYPE=@TASKTYPE");

                strSql.Append(" where TASKID=@TASKID");
                SqlParameter[] parameters = {
					new SqlParameter("@TASKNAME",SqlDbType.NVarChar),
					new SqlParameter("@URLENTRY",SqlDbType.NVarChar),
					new SqlParameter("@SITECODE",SqlDbType.NVarChar),
					new SqlParameter("@SPIDERDEGREE",SqlDbType.Int),
					new SqlParameter("@ISAGENT",SqlDbType.Int),
					new SqlParameter("@AGENTSERVERIP",SqlDbType.NVarChar),
					new SqlParameter("@AGENTSERVERPORT",SqlDbType.Int),
					new SqlParameter("@AGENTSERVERUSER",SqlDbType.NVarChar),
					new SqlParameter("@AGENTSERVERPWD",SqlDbType.NVarChar),
					new SqlParameter("@ISLOGIN",SqlDbType.Int),
					new SqlParameter("@LOGINSITE",SqlDbType.NVarChar),
					new SqlParameter("@LOGINDATA",SqlDbType.NVarChar),
					new SqlParameter("@ISPOST",SqlDbType.Int),
					new SqlParameter("@ISUPDATE",SqlDbType.Int),
					new SqlParameter("@UPDATETIMESPAN",SqlDbType.Int),
					new SqlParameter("@PAGEURLREG",SqlDbType.NVarChar),
					new SqlParameter("@PAGECONTENTREG",SqlDbType.NVarChar),
					new SqlParameter("@SITEID",SqlDbType.Int),
					new SqlParameter("@URLPREFIX",SqlDbType.NVarChar),
					new SqlParameter("@TASKDES",SqlDbType.NVarChar),
					new SqlParameter("@TASKTYPE",SqlDbType.NVarChar),
					new SqlParameter("@TASKID",SqlDbType.Int)
				};
                parameters[0].Value = entity.TASKNAME;
                parameters[1].Value = entity.URLENTRY;
                parameters[2].Value = entity.SITECODE;
                parameters[3].Value = entity.SPIDERDEGREE;
                parameters[4].Value = entity.ISAGENT;
                parameters[5].Value = entity.AGENTSERVERIP;
                parameters[6].Value = entity.AGENTSERVERPORT;
                parameters[7].Value = entity.AGENTSERVERUSER;
                parameters[8].Value = entity.AGENTSERVERPWD;
                parameters[9].Value = entity.ISLOGIN;
                parameters[10].Value = entity.LOGINSITE;
                parameters[11].Value = entity.LOGINDATA;
                parameters[12].Value = entity.ISPOST;
                parameters[13].Value = entity.ISUPDATE;
                parameters[14].Value = entity.UPDATETIMESPAN;
                parameters[15].Value = entity.PAGEURLREG;
                parameters[16].Value = entity.PAGECONTENTREG;
                parameters[17].Value = entity.SITEID;
                parameters[18].Value = entity.URLPREFIX;
                parameters[19].Value = entity.TASKDES;
                parameters[20].Value = entity.TASKTYPE;
                parameters[21].Value = entity.TASKID;

                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update TASK set ");
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
                string strSql = "delete from TASK where ID=" + ID;
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
                string strSql = "delete from TASK where ID in (" + ID + ")";
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

            public override void Delete(TASKEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from TASK ");
                strSql.Append(" where TASKID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.TASKID;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override TASKEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from TASK ");
                strSql.Append(" where TASKID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    TASKEntity entity = new TASKEntity();
                    if (!Convert.IsDBNull(row["TASKID"]))
                    {
                        entity.TASKID = Convert.ToInt32(row["TASKID"]);
                    }
                    entity.TASKNAME = row["TASKNAME"].ToString();
                    entity.URLENTRY = row["URLENTRY"].ToString();
                    entity.SITECODE = row["SITECODE"].ToString();
                    if (!Convert.IsDBNull(row["SPIDERDEGREE"]))
                    {
                        entity.SPIDERDEGREE = Convert.ToInt32(row["SPIDERDEGREE"]);
                    }
                    if (!Convert.IsDBNull(row["ISAGENT"]))
                    {
                        entity.ISAGENT = Convert.ToInt32(row["ISAGENT"]);
                    }
                    entity.AGENTSERVERIP = row["AGENTSERVERIP"].ToString();
                    if (!Convert.IsDBNull(row["AGENTSERVERPORT"]))
                    {
                        entity.AGENTSERVERPORT = Convert.ToInt32(row["AGENTSERVERPORT"]);
                    }
                    entity.AGENTSERVERUSER = row["AGENTSERVERUSER"].ToString();
                    entity.AGENTSERVERPWD = row["AGENTSERVERPWD"].ToString();
                    if (!Convert.IsDBNull(row["ISLOGIN"]))
                    {
                        entity.ISLOGIN = Convert.ToInt32(row["ISLOGIN"]);
                    }
                    entity.LOGINSITE = row["LOGINSITE"].ToString();
                    entity.LOGINDATA = row["LOGINDATA"].ToString();
                    if (!Convert.IsDBNull(row["ISPOST"]))
                    {
                        entity.ISPOST = Convert.ToInt32(row["ISPOST"]);
                    }
                    if (!Convert.IsDBNull(row["ISUPDATE"]))
                    {
                        entity.ISUPDATE = Convert.ToInt32(row["ISUPDATE"]);
                    }
                    if (!Convert.IsDBNull(row["UPDATETIMESPAN"]))
                    {
                        entity.UPDATETIMESPAN = Convert.ToInt32(row["UPDATETIMESPAN"]);
                    }
                    entity.PAGEURLREG = row["PAGEURLREG"].ToString();
                    entity.PAGECONTENTREG = row["PAGECONTENTREG"].ToString();
                    if (!Convert.IsDBNull(row["SITEID"]))
                    {
                        entity.SITEID = Convert.ToInt32(row["SITEID"]);
                    }
                    entity.URLPREFIX = row["URLPREFIX"].ToString();
                    entity.TASKDES = row["TASKDES"].ToString();
                    entity.TASKTYPE = row["TASKTYPE"].ToString();
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public List<TASKEntity> Find(string strWhere)
            {
                return Find(strWhere, null);
            }

            public override List<TASKEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM TASK ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<TASKEntity> list = new List<TASKEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        TASKEntity entity = new TASKEntity();
                        if (!Convert.IsDBNull(row["TASKID"]))
                        {
                            entity.TASKID = Convert.ToInt32(row["TASKID"]);
                        }
                        entity.TASKNAME = row["TASKNAME"].ToString();
                        entity.URLENTRY = row["URLENTRY"].ToString();
                        entity.SITECODE = row["SITECODE"].ToString();
                        if (!Convert.IsDBNull(row["SPIDERDEGREE"]))
                        {
                            entity.SPIDERDEGREE = Convert.ToInt32(row["SPIDERDEGREE"]);
                        }
                        if (!Convert.IsDBNull(row["ISAGENT"]))
                        {
                            entity.ISAGENT = Convert.ToInt32(row["ISAGENT"]);
                        }
                        entity.AGENTSERVERIP = row["AGENTSERVERIP"].ToString();
                        if (!Convert.IsDBNull(row["AGENTSERVERPORT"]))
                        {
                            entity.AGENTSERVERPORT = Convert.ToInt32(row["AGENTSERVERPORT"]);
                        }
                        entity.AGENTSERVERUSER = row["AGENTSERVERUSER"].ToString();
                        entity.AGENTSERVERPWD = row["AGENTSERVERPWD"].ToString();
                        if (!Convert.IsDBNull(row["ISLOGIN"]))
                        {
                            entity.ISLOGIN = Convert.ToInt32(row["ISLOGIN"]);
                        }
                        entity.LOGINSITE = row["LOGINSITE"].ToString();
                        entity.LOGINDATA = row["LOGINDATA"].ToString();
                        if (!Convert.IsDBNull(row["ISPOST"]))
                        {
                            entity.ISPOST = Convert.ToInt32(row["ISPOST"]);
                        }
                        if (!Convert.IsDBNull(row["ISUPDATE"]))
                        {
                            entity.ISUPDATE = Convert.ToInt32(row["ISUPDATE"]);
                        }
                        if (!Convert.IsDBNull(row["UPDATETIMESPAN"]))
                        {
                            entity.UPDATETIMESPAN = Convert.ToInt32(row["UPDATETIMESPAN"]);
                        }
                        entity.PAGEURLREG = row["PAGEURLREG"].ToString();
                        entity.PAGECONTENTREG = row["PAGECONTENTREG"].ToString();
                        if (!Convert.IsDBNull(row["SITEID"]))
                        {
                            entity.SITEID = Convert.ToInt32(row["SITEID"]);
                        }
                        entity.URLPREFIX = row["URLPREFIX"].ToString();
                        entity.TASKDES = row["TASKDES"].ToString();
                        entity.TASKTYPE = row["TASKTYPE"].ToString();

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
                strSql.Append(" FROM TASK");
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
                string sql = "select count(*) from TASK ";
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
                if (string.IsNullOrEmpty(where))
                {
                    where = "  1=1 ";
                }
                if (string.IsNullOrEmpty("orderBy"))
                {
                    orderBy = " ROLEID ";
                }
                PagerSql.AppendFormat("SELECT * FROM( SELECT ROW_NUMBER()OVER( ORDER BY TASKID) AS RN,* FROM dbo.TASK where {0} ) AS T WHERE T.RN BETWEEN {1} AND {2}", where, startNumber, endNumber);
                //PagerSql.Append("SELECT * FROM (");
                //PagerSql.Append(" SELECT A.*, ROWNUM RN ");
                //PagerSql.Append("FROM (SELECT * FROM TASK ");
                //if (!string.IsNullOrEmpty(where))
                //{
                //    PagerSql.Append(" where " + where);
                //}
                //if (!string.IsNullOrEmpty(orderBy))
                //{
                //    PagerSql.AppendFormat(" ORDER BY {0}", orderBy);
                //}
                //else
                //{

                //    PagerSql.Append(" ORDER BY TASKID");//默认按主键排序

                //}
                //PagerSql.AppendFormat(" ) A WHERE ROWNUM <= {0})", endNumber);
                //PagerSql.AppendFormat(" WHERE RN >= {0}", startNumber);

                return _oracleHelper.ExecuteDateSet(PagerSql.ToString(), param).Tables[0];
            }

            #endregion

        }
    }
}

