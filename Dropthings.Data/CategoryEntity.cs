using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;
using System.Text;
using log4net;

namespace Dropthings.Data
{
    [Serializable]
    public partial class CATEGORYEntity
    {
        private SqlHelper sqlHelper;

        #region const fields
        public const string DBName = "Sentiment";
        public const string TableName = "CATEGORY";
        public const string PrimaryKey = "PK_CATEGORY";
        #endregion

        #region columns
        public struct Columns
        {
            public const string ID = "ID";
            public const string CATEGORYID = "CATEGORYID";
            public const string DATABASEID = "DATABASEID";
            public const string CATEGORYNAME = "CATEGORYNAME";
            public const string CATEDISPLAY = "CATEDISPLAY";
            public const string PARENTCATE = "PARENTCATE";
            public const string CATEPATH = "CATEPATH";
            public const string CREATEBY = "CREATEBY";
            public const string CREATETIME = "CREATETIME";
            public const string CATETYPE = "CATETYPE";
            public const string CATESOURCE = "CATESOURCE";
            public const string CATETRAININFO = "CATETRAININFO";
            public const string QUERYTYPE = "QUERYTYPE";
            public const string ISEFFECT = "ISEFFECT";
            public const string PROJECTCODE = "PROJECTCODE";
            public const string KEYWORD = "KEYWORD";
            public const string MINSCORE = "MINSCORE";
            public const string SEQUEUE = "SEQUEUE";
            public const string EVENTRESON = "EVENTRESON";
            public const string EVENTMEASURE = "EVENTMEASURE";
            public const string EVENTABOUT = "EVENTABOUT";
            public const string EVENTMINSCORE = "EVENTMINSCORE";
            public const string EVENTDATE = "EVENTDATE";
            public const string EVENTTYPE = "EVENTTYPE";
        }
        #endregion

        #region constructors
        public CATEGORYEntity()
        {
            sqlHelper = new SqlHelper(DBName);
        }

        public CATEGORYEntity(int id, long categoryid, string databaseid, string categoryname, string catedisplay, int parentcate, string catepath, string createby, DateTime createtime, int catetype, string catesource, string catetraininfo, string querytype, int iseffect, int projectcode, string keyword, string minscore, int sequeue, string eventreson, string eventmeasure, string eventabout, string eventminscore, DateTime eventdate, int eventtype)
        {
            this.ID = id;

            this.CATEGORYID = categoryid;

            this.DATABASEID = databaseid;

            this.CATEGORYNAME = categoryname;

            this.CATEDISPLAY = catedisplay;

            this.PARENTCATE = parentcate;

            this.CATEPATH = catepath;

            this.CREATEBY = createby;

            this.CREATETIME = createtime;

            this.CATETYPE = catetype;

            this.CATESOURCE = catesource;

            this.CATETRAININFO = catetraininfo;

            this.QUERYTYPE = querytype;

            this.ISEFFECT = iseffect;

            this.PROJECTCODE = projectcode;

            this.KEYWORD = keyword;

            this.MINSCORE = minscore;

            this.SEQUEUE = sequeue;

            this.EVENTRESON = eventreson;

            this.EVENTMEASURE = eventmeasure;

            this.EVENTABOUT = eventabout;

            this.EVENTMINSCORE = eventminscore;

            this.EVENTDATE = eventdate;

            this.EVENTTYPE = eventtype;

        }
        #endregion

        #region Properties

        public int? ID
        {
            get;
            set;
        }


        public long? CATEGORYID
        {
            get;
            set;
        }


        public string DATABASEID
        {
            get;
            set;
        }


        public string CATEGORYNAME
        {
            get;
            set;
        }


        public string CATEDISPLAY
        {
            get;
            set;
        }


        public int? PARENTCATE
        {
            get;
            set;
        }


        public string CATEPATH
        {
            get;
            set;
        }


        public string CREATEBY
        {
            get;
            set;
        }


        public DateTime? CREATETIME
        {
            get;
            set;
        }


        public int? CATETYPE
        {
            get;
            set;
        }


        public string CATESOURCE
        {
            get;
            set;
        }


        public string CATETRAININFO
        {
            get;
            set;
        }


        public string QUERYTYPE
        {
            get;
            set;
        }


        public int? ISEFFECT
        {
            get;
            set;
        }


        public int? PROJECTCODE
        {
            get;
            set;
        }


        public string KEYWORD
        {
            get;
            set;
        }


        public string MINSCORE
        {
            get;
            set;
        }


        public int? SEQUEUE
        {
            get;
            set;
        }


        public string EVENTRESON
        {
            get;
            set;
        }


        public string EVENTMEASURE
        {
            get;
            set;
        }


        public string EVENTABOUT
        {
            get;
            set;
        }


        public string EVENTMINSCORE
        {
            get;
            set;
        }


        public DateTime? EVENTDATE
        {
            get;
            set;
        }


        public int? EVENTTYPE
        {
            get;
            set;
        }

        #endregion

        public class CATEGORYDAO : SqlDAO<CATEGORYEntity>
        {
            private SqlHelper sqlHelper;
            public const string DBName = "SentimentConnStr";

            public CATEGORYDAO()
            {
                sqlHelper = new SqlHelper(DBName);
            }

            public override void Add(CATEGORYEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into CATEGORY(");
                strSql.Append("CATEGORYID,DATABASEID,CATEGORYNAME,CATEDISPLAY,PARENTCATE,CATEPATH,CREATEBY,CREATETIME,CATETYPE,CATESOURCE,CATETRAININFO,QUERYTYPE,ISEFFECT,PROJECTCODE,KEYWORD,MINSCORE,SEQUEUE,EVENTRESON,EVENTMEASURE,EVENTABOUT,EVENTMINSCORE,EVENTDATE,EVENTTYPE)");
                strSql.Append(" values (");
                strSql.Append("@CATEGORYID,@DATABASEID,@CATEGORYNAME,@CATEDISPLAY,@PARENTCATE,@CATEPATH,@CREATEBY,@CREATETIME,@CATETYPE,@CATESOURCE,@CATETRAININFO,@QUERYTYPE,@ISEFFECT,@PROJECTCODE,@KEYWORD,@MINSCORE,@SEQUEUE,@EVENTRESON,@EVENTMEASURE,@EVENTABOUT,@EVENTMINSCORE,@EVENTDATE,@EVENTTYPE)");
                SqlParameter[] parameters = {
					new SqlParameter("@CATEGORYID",SqlDbType.Int),
					new SqlParameter("@DATABASEID",SqlDbType.NVarChar),
					new SqlParameter("@CATEGORYNAME",SqlDbType.NVarChar),
					new SqlParameter("@CATEDISPLAY",SqlDbType.NVarChar),
					new SqlParameter("@PARENTCATE",SqlDbType.Int),
					new SqlParameter("@CATEPATH",SqlDbType.NVarChar),
					new SqlParameter("@CREATEBY",SqlDbType.NVarChar),
					new SqlParameter("@CREATETIME",SqlDbType.DateTime),
					new SqlParameter("@CATETYPE",SqlDbType.Int),
					new SqlParameter("@CATESOURCE",SqlDbType.NVarChar),
					new SqlParameter("@CATETRAININFO",SqlDbType.NVarChar),
					new SqlParameter("@QUERYTYPE",SqlDbType.NVarChar),
					new SqlParameter("@ISEFFECT",SqlDbType.Int),
					new SqlParameter("@PROJECTCODE",SqlDbType.Int),
					new SqlParameter("@KEYWORD",SqlDbType.NVarChar),
					new SqlParameter("@MINSCORE",SqlDbType.NVarChar),
					new SqlParameter("@SEQUEUE",SqlDbType.Int),
					new SqlParameter("@EVENTRESON",SqlDbType.NVarChar),
					new SqlParameter("@EVENTMEASURE",SqlDbType.NVarChar),
					new SqlParameter("@EVENTABOUT",SqlDbType.NVarChar),
					new SqlParameter("@EVENTMINSCORE",SqlDbType.NVarChar),
					new SqlParameter("@EVENTDATE",SqlDbType.DateTime),
					new SqlParameter("@EVENTTYPE",SqlDbType.Int)
					};
                parameters[0].Value = entity.CATEGORYID;
                parameters[1].Value = entity.DATABASEID;
                parameters[2].Value = entity.CATEGORYNAME;
                parameters[3].Value = entity.CATEDISPLAY;
                parameters[4].Value = entity.PARENTCATE;
                parameters[5].Value = entity.CATEPATH;
                parameters[6].Value = entity.CREATEBY;
                parameters[7].Value = entity.CREATETIME;
                parameters[8].Value = entity.CATETYPE;
                parameters[9].Value = entity.CATESOURCE;
                parameters[10].Value = entity.CATETRAININFO;
                parameters[11].Value = entity.QUERYTYPE;
                parameters[12].Value = entity.ISEFFECT;
                parameters[13].Value = entity.PROJECTCODE;
                parameters[14].Value = entity.KEYWORD;
                parameters[15].Value = entity.MINSCORE;
                parameters[16].Value = entity.SEQUEUE;
                parameters[17].Value = entity.EVENTRESON;
                parameters[18].Value = entity.EVENTMEASURE;
                parameters[19].Value = entity.EVENTABOUT;
                parameters[20].Value = entity.EVENTMINSCORE;
                parameters[21].Value = entity.EVENTDATE;
                parameters[22].Value = entity.EVENTTYPE;
                sqlHelper.ExecuteSql(strSql.ToString(), parameters);                
            }

            public override void Update(CATEGORYEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update CATEGORY set ");
                strSql.Append("CATEGORYID=@CATEGORYID,");
                strSql.Append("DATABASEID=@DATABASEID,");
                strSql.Append("CATEGORYNAME=@CATEGORYNAME,");
                strSql.Append("CATEDISPLAY=@CATEDISPLAY,");
                strSql.Append("PARENTCATE=@PARENTCATE,");
                strSql.Append("CATEPATH=@CATEPATH,");
                strSql.Append("CREATEBY=@CREATEBY,");
                strSql.Append("CREATETIME=@CREATETIME,");
                strSql.Append("CATETYPE=@CATETYPE,");
                strSql.Append("CATESOURCE=@CATESOURCE,");
                strSql.Append("CATETRAININFO=@CATETRAININFO,");
                strSql.Append("QUERYTYPE=@QUERYTYPE,");
                strSql.Append("ISEFFECT=@ISEFFECT,");
                strSql.Append("PROJECTCODE=@PROJECTCODE,");
                strSql.Append("KEYWORD=@KEYWORD,");
                strSql.Append("MINSCORE=@MINSCORE,");
                strSql.Append("SEQUEUE=@SEQUEUE,");
                strSql.Append("EVENTRESON=@EVENTRESON,");
                strSql.Append("EVENTMEASURE=@EVENTMEASURE,");
                strSql.Append("EVENTABOUT=@EVENTABOUT,");
                strSql.Append("EVENTMINSCORE=@EVENTMINSCORE,");
                strSql.Append("EVENTDATE=@EVENTDATE,");
                strSql.Append("EVENTTYPE=@EVENTTYPE");

                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@CATEGORYID",SqlDbType.Int),
					new SqlParameter("@DATABASEID",SqlDbType.NVarChar),
					new SqlParameter("@CATEGORYNAME",SqlDbType.NVarChar),
					new SqlParameter("@CATEDISPLAY",SqlDbType.NVarChar),
					new SqlParameter("@PARENTCATE",SqlDbType.Int),
					new SqlParameter("@CATEPATH",SqlDbType.NVarChar),
					new SqlParameter("@CREATEBY",SqlDbType.NVarChar),
					new SqlParameter("@CREATETIME",SqlDbType.DateTime),
					new SqlParameter("@CATETYPE",SqlDbType.Int),
					new SqlParameter("@CATESOURCE",SqlDbType.NVarChar),
					new SqlParameter("@CATETRAININFO",SqlDbType.NVarChar),
					new SqlParameter("@QUERYTYPE",SqlDbType.NVarChar),
					new SqlParameter("@ISEFFECT",SqlDbType.Int),
					new SqlParameter("@PROJECTCODE",SqlDbType.Int),
					new SqlParameter("@KEYWORD",SqlDbType.NVarChar),
					new SqlParameter("@MINSCORE",SqlDbType.NVarChar),
					new SqlParameter("@SEQUEUE",SqlDbType.Int),
					new SqlParameter("@EVENTRESON",SqlDbType.NVarChar),
					new SqlParameter("@EVENTMEASURE",SqlDbType.NVarChar),
					new SqlParameter("@EVENTABOUT",SqlDbType.NVarChar),
					new SqlParameter("@EVENTMINSCORE",SqlDbType.NVarChar),
					new SqlParameter("@EVENTDATE",SqlDbType.DateTime),
					new SqlParameter("@EVENTTYPE",SqlDbType.Int),
					new SqlParameter("@ID",SqlDbType.Int)
				};
                parameters[0].Value = entity.CATEGORYID;
                parameters[1].Value = entity.DATABASEID;
                parameters[2].Value = entity.CATEGORYNAME;
                parameters[3].Value = entity.CATEDISPLAY;
                parameters[4].Value = entity.PARENTCATE;
                parameters[5].Value = entity.CATEPATH;
                parameters[6].Value = entity.CREATEBY;
                parameters[7].Value = entity.CREATETIME;
                parameters[8].Value = entity.CATETYPE;
                parameters[9].Value = entity.CATESOURCE;
                parameters[10].Value = entity.CATETRAININFO;
                parameters[11].Value = entity.QUERYTYPE;
                parameters[12].Value = entity.ISEFFECT;
                parameters[13].Value = entity.PROJECTCODE;
                parameters[14].Value = entity.KEYWORD;
                parameters[15].Value = entity.MINSCORE;
                parameters[16].Value = entity.SEQUEUE;
                parameters[17].Value = entity.EVENTRESON;
                parameters[18].Value = entity.EVENTMEASURE;
                parameters[19].Value = entity.EVENTABOUT;
                parameters[20].Value = entity.EVENTMINSCORE;
                parameters[21].Value = entity.EVENTDATE;
                parameters[22].Value = entity.EVENTTYPE;
                parameters[23].Value = entity.ID;

                sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update CATEGORY set ");
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

            public bool UpdateCateType()
            {
                ILog logger = LogManager.GetLogger("CategoryUpDate");
                try
                {                    
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update CATEGORY set ");
                    StrSql.Append("CATETYPE=0");
                    StrSql.Append(" where PARENTCATE=202");
                    logger.Error("the sqlstr is" + StrSql.ToString());                   
                    sqlHelper.ExecuteSql(StrSql.ToString(), null);
                    return true;
                }
                catch(Exception ex)
                {
                    logger.Error("UpDate is Error:" + ex.ToString());
                    return false;
                }
            }

            public bool Delete(int ID)
            {
                string strSql = "delete from CATEGORY where ID=" + ID;
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
                string strSql = "delete from CATEGORY where ID in (" + ID + ")";
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

            public override void Delete(CATEGORYEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from CATEGORY ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.ID;
                sqlHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override CATEGORYEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from CATEGORY ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    CATEGORYEntity entity = new CATEGORYEntity();
                    if (!Convert.IsDBNull(row["ID"]))
                    {
                        entity.ID = Convert.ToInt32(row["ID"]);
                    }
                    if (!Convert.IsDBNull(row["CATEGORYID"]))
                    {
                        entity.CATEGORYID = Convert.ToInt64(row["CATEGORYID"]);
                    }
                    entity.DATABASEID = row["DATABASEID"].ToString();
                    entity.CATEGORYNAME = row["CATEGORYNAME"].ToString();
                    entity.CATEDISPLAY = row["CATEDISPLAY"].ToString();
                    if (!Convert.IsDBNull(row["PARENTCATE"]))
                    {
                        entity.PARENTCATE = Convert.ToInt32(row["PARENTCATE"]);
                    }
                    entity.CATEPATH = row["CATEPATH"].ToString();
                    entity.CREATEBY = row["CREATEBY"].ToString();
                    if (!Convert.IsDBNull(row["CREATETIME"]))
                    {
                        entity.CREATETIME = Convert.ToDateTime(row["CREATETIME"]);
                    }
                    if (!Convert.IsDBNull(row["CATETYPE"]))
                    {
                        entity.CATETYPE = Convert.ToInt32(row["CATETYPE"]);
                    }
                    entity.CATESOURCE = row["CATESOURCE"].ToString();
                    entity.CATETRAININFO = row["CATETRAININFO"].ToString();
                    entity.QUERYTYPE = row["QUERYTYPE"].ToString();
                    if (!Convert.IsDBNull(row["ISEFFECT"]))
                    {
                        entity.ISEFFECT = Convert.ToInt32(row["ISEFFECT"]);
                    }
                    if (!Convert.IsDBNull(row["PROJECTCODE"]))
                    {
                        entity.PROJECTCODE = Convert.ToInt32(row["PROJECTCODE"]);
                    }
                    entity.KEYWORD = row["KEYWORD"].ToString();
                    entity.MINSCORE = row["MINSCORE"].ToString();
                    if (!Convert.IsDBNull(row["SEQUEUE"]))
                    {
                        entity.SEQUEUE = Convert.ToInt32(row["SEQUEUE"]);
                    }
                    entity.EVENTRESON = row["EVENTRESON"].ToString();
                    entity.EVENTMEASURE = row["EVENTMEASURE"].ToString();
                    entity.EVENTABOUT = row["EVENTABOUT"].ToString();
                    entity.EVENTMINSCORE = row["EVENTMINSCORE"].ToString();
                    if (!Convert.IsDBNull(row["EVENTDATE"]))
                    {
                        entity.EVENTDATE = Convert.ToDateTime(row["EVENTDATE"]);
                    }
                    if (!Convert.IsDBNull(row["EVENTTYPE"]))
                    {
                        entity.EVENTTYPE = Convert.ToInt32(row["EVENTTYPE"]);
                    }
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public List<CATEGORYEntity> Find(string strWhere) {
                return Find(strWhere, null);
            }

            public List<CATEGORYEntity> Find(int top,string strWhere)
            {
                return Find(top,strWhere, null);
            }

            public override List<CATEGORYEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM CATEGORY ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<CATEGORYEntity> list = new List<CATEGORYEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        CATEGORYEntity entity = new CATEGORYEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        if (!Convert.IsDBNull(row["CATEGORYID"]))
                        {
                            entity.CATEGORYID = Convert.ToInt64(row["CATEGORYID"]);
                        }
                        entity.DATABASEID = row["DATABASEID"].ToString();
                        entity.CATEGORYNAME = row["CATEGORYNAME"].ToString();
                        entity.CATEDISPLAY = row["CATEDISPLAY"].ToString();
                        if (!Convert.IsDBNull(row["PARENTCATE"]))
                        {
                            entity.PARENTCATE = Convert.ToInt32(row["PARENTCATE"]);
                        }
                        entity.CATEPATH = row["CATEPATH"].ToString();
                        entity.CREATEBY = row["CREATEBY"].ToString();
                        if (!Convert.IsDBNull(row["CREATETIME"]))
                        {
                            entity.CREATETIME = Convert.ToDateTime(row["CREATETIME"]);
                        }
                        if (!Convert.IsDBNull(row["CATETYPE"]))
                        {
                            entity.CATETYPE = Convert.ToInt32(row["CATETYPE"]);
                        }
                        entity.CATESOURCE = row["CATESOURCE"].ToString();
                        entity.CATETRAININFO = row["CATETRAININFO"].ToString();
                        entity.QUERYTYPE = row["QUERYTYPE"].ToString();
                        if (!Convert.IsDBNull(row["ISEFFECT"]))
                        {
                            entity.ISEFFECT = Convert.ToInt32(row["ISEFFECT"]);
                        }
                        if (!Convert.IsDBNull(row["PROJECTCODE"]))
                        {
                            entity.PROJECTCODE = Convert.ToInt32(row["PROJECTCODE"]);
                        }
                        entity.KEYWORD = row["KEYWORD"].ToString();
                        entity.MINSCORE = row["MINSCORE"].ToString();
                        if (!Convert.IsDBNull(row["SEQUEUE"]))
                        {
                            entity.SEQUEUE = Convert.ToInt32(row["SEQUEUE"]);
                        }
                        entity.EVENTRESON = row["EVENTRESON"].ToString();
                        entity.EVENTMEASURE = row["EVENTMEASURE"].ToString();
                        entity.EVENTABOUT = row["EVENTABOUT"].ToString();
                        entity.EVENTMINSCORE = row["EVENTMINSCORE"].ToString();
                        if (!Convert.IsDBNull(row["EVENTDATE"]))
                        {
                            entity.EVENTDATE = Convert.ToDateTime(row["EVENTDATE"]);
                        }
                        if (!Convert.IsDBNull(row["EVENTTYPE"]))
                        {
                            entity.EVENTTYPE = Convert.ToInt32(row["EVENTTYPE"]);
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

            public DataTable GetDTByCategoryId(long categoryid)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT * FROM TRENDDATA T ,CATEGORY C  WHERE T.CATEGORYID = C.CATEGORYID");
                strSql.Append(" AND C.CATEGORYID =@CATEGORYID");

                SqlParameter[] parameters = {
					new SqlParameter("@CATEGORYID",SqlDbType.BigInt)				
					};
                parameters[0].Value = categoryid;
                return sqlHelper.ExecuteDateSet(strSql.ToString(), parameters).Tables[0];
            }

            public List<CATEGORYEntity> Find(int top,string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT * FROM (");
                strSql.Append(" SELECT A.*, ROWNUM RN ");
                strSql.Append("FROM (SELECT * FROM CATEGORY ");
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strSql.Append(" where " + strWhere);
                }
                strSql.AppendFormat(" ) A WHERE ROWNUM <= {0})", top);
                

                DataSet ds = sqlHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<CATEGORYEntity> list = new List<CATEGORYEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        CATEGORYEntity entity = new CATEGORYEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt32(row["ID"]);
                        }
                        if (!Convert.IsDBNull(row["CATEGORYID"]))
                        {
                            entity.CATEGORYID = Convert.ToInt64(row["CATEGORYID"]);
                        }
                        entity.DATABASEID = row["DATABASEID"].ToString();
                        entity.CATEGORYNAME = row["CATEGORYNAME"].ToString();
                        entity.CATEDISPLAY = row["CATEDISPLAY"].ToString();
                        if (!Convert.IsDBNull(row["PARENTCATE"]))
                        {
                            entity.PARENTCATE = Convert.ToInt32(row["PARENTCATE"]);
                        }
                        entity.CATEPATH = row["CATEPATH"].ToString();
                        entity.CREATEBY = row["CREATEBY"].ToString();
                        if (!Convert.IsDBNull(row["CREATETIME"]))
                        {
                            entity.CREATETIME = Convert.ToDateTime(row["CREATETIME"]);
                        }
                        if (!Convert.IsDBNull(row["CATETYPE"]))
                        {
                            entity.CATETYPE = Convert.ToInt32(row["CATETYPE"]);
                        }
                        entity.CATESOURCE = row["CATESOURCE"].ToString();
                        entity.CATETRAININFO = row["CATETRAININFO"].ToString();
                        entity.QUERYTYPE = row["QUERYTYPE"].ToString();
                        if (!Convert.IsDBNull(row["ISEFFECT"]))
                        {
                            entity.ISEFFECT = Convert.ToInt32(row["ISEFFECT"]);
                        }
                        if (!Convert.IsDBNull(row["PROJECTCODE"]))
                        {
                            entity.PROJECTCODE = Convert.ToInt32(row["PROJECTCODE"]);
                        }
                        entity.KEYWORD = row["KEYWORD"].ToString();
                        entity.MINSCORE = row["MINSCORE"].ToString();
                        if (!Convert.IsDBNull(row["SEQUEUE"]))
                        {
                            entity.SEQUEUE = Convert.ToInt32(row["SEQUEUE"]);
                        }
                        entity.EVENTRESON = row["EVENTRESON"].ToString();
                        entity.EVENTMEASURE = row["EVENTMEASURE"].ToString();
                        entity.EVENTABOUT = row["EVENTABOUT"].ToString();
                        entity.EVENTMINSCORE = row["EVENTMINSCORE"].ToString();
                        if (!Convert.IsDBNull(row["EVENTDATE"]))
                        {
                            entity.EVENTDATE = Convert.ToDateTime(row["EVENTDATE"]);
                        }
                        if (!Convert.IsDBNull(row["EVENTTYPE"]))
                        {
                            entity.EVENTTYPE = Convert.ToInt32(row["EVENTTYPE"]);
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

            public DataSet GetTrebdDataSet(long categoryid)
            {
                string strSql = "SELECT * FROM TRENDDATA  where CATEGORYID = @CATEGORYID";               
                SqlParameter[] parameters = {
					new SqlParameter("@CATEGORYID",SqlDbType.Int)					
					};
                parameters[0].Value = categoryid;
                return sqlHelper.ExecuteDateSet(strSql, parameters);
            }

            public DataSet GetDataSet(string strWhere, SqlParameter[] param)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM CATEGORY");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                return sqlHelper.ExecuteDateSet(strSql.ToString(), param);
            }

            #region paging methods

            public int GetPagerRowsCount(string where)
            {
                return GetPagerRowsCount(where, null);
            }
            /// <summary>
            /// 获取分页记录总数
            /// </summary>
            /// <param name="where">条件，等同于GetPaer()方法的where</param>
            /// <returns>返回记录总数</returns>
            public int GetPagerRowsCount(string where, SqlParameter[] param)
            {
                string sql = "select count(*) from CATEGORY ";
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
            /// 
            public DataTable GetPager(string where, string orderBy, int pageSize, int pageNumber) {
                return GetPager(where, null, orderBy, pageSize, pageNumber);
            }

            public DataTable GetPager(string where, SqlParameter[] param, string orderBy, int pageSize, int pageNumber)
            {
                int startNumber = pageSize * (pageNumber - 1) + 1;
                int endNumber = pageSize * pageNumber;

                StringBuilder PagerSql = new StringBuilder();
                PagerSql.Append("SELECT * FROM (");
                PagerSql.Append(" SELECT A.*, ROWNUM RN ");
                PagerSql.Append("FROM (SELECT * FROM CATEGORY ");
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

