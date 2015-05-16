using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace Dropthings.Data
{
    [Serializable]
    public partial class ARTICLEEntity
    {
        private SqlHelper _oracleHelper;

        #region const fields
        public const string DBName = "Sentiment";
        public const string TableName = "ARTICLE";
        public const string PrimaryKey = "PK_ARTICLEDB";
        #endregion

        #region columns
        public struct Columns
        {
            public const string ID = "ID";
            public const string ARTICLETITLE = "ARTICLETITLE";
            public const string ARTICLEOTHERTITLE = "ARTICLEOTHERTITLE";
            public const string ARTICLEIMGPATH = "ARTICLEIMGPATH";
            public const string ARTICLEDISSTYLE = "ARTICLEDISSTYLE";
            public const string ARTICLEEXTERNALURL = "ARTICLEEXTERNALURL";
            public const string ARTICLEAUTHOR = "ARTICLEAUTHOR";
            public const string ARTICLESOURCE = "ARTICLESOURCE";
            public const string ARTICLESUMMARY = "ARTICLESUMMARY";
            public const string ARTICLEBASEDATE = "ARTICLEBASEDATE";
            public const string ARTICLEEDITDATE = "ARTICLEEDITDATE";
            public const string ARTICLETAG = "ARTICLETAG";
            public const string ARTICLECONTENT = "ARTICLECONTENT";
            public const string ARTICLEAUDIT = "ARTICLEAUDIT";
            public const string ARTICLERELESE = "ARTICLERELESE";
            public const string ARTICLESTATUS = "ARTICLESTATUS";
            public const string COLUMNID = "COLUMNID";
            public const string PUBLISHPATH = "PUBLISHPATH";
            public const string SITEID = "SITEID";
            public const string ARTICLESITETYPE = "ARTICLESITETYPE";
        }
        #endregion

        #region constructors
        public ARTICLEEntity()
        {
            _oracleHelper = new SqlHelper(DBName);
        }

        public ARTICLEEntity(long id, string articletitle, string articleothertitle, string articleimgpath, int articledisstyle, string articleexternalurl, string articleauthor, string articlesource, string articlesummary, DateTime articlebasedate, DateTime articleeditdate, string articletag, string articlecontent, int articleaudit, int articlerelese, int articlestatus, int columnid, string publishpath, int siteid , int articlesitetype)
        {
            this.ID = id;

            this.ARTICLETITLE = articletitle;

            this.ARTICLEOTHERTITLE = articleothertitle;

            this.ARTICLEIMGPATH = articleimgpath;

            this.ARTICLEDISSTYLE = articledisstyle;

            this.ARTICLEEXTERNALURL = articleexternalurl;

            this.ARTICLEAUTHOR = articleauthor;

            this.ARTICLESOURCE = articlesource;

            this.ARTICLESUMMARY = articlesummary;

            this.ARTICLEBASEDATE = articlebasedate;

            this.ARTICLEEDITDATE = articleeditdate;

            this.ARTICLETAG = articletag;

            this.ARTICLECONTENT = articlecontent;

            this.ARTICLEAUDIT = articleaudit;

            this.ARTICLERELESE = articlerelese;

            this.ARTICLESTATUS = articlestatus;

            this.COLUMNID = columnid;

            this.PUBLISHPATH = publishpath;

            this.SITEID = siteid;

            this.ARTICLESITETYPE = articlesitetype;

        }
        #endregion

        #region Properties

        public long? ID
        {
            get;
            set;
        }


        public string ARTICLETITLE
        {
            get;
            set;
        }


        public string ARTICLEOTHERTITLE
        {
            get;
            set;
        }


        public string ARTICLEIMGPATH
        {
            get;
            set;
        }


        public int? ARTICLEDISSTYLE
        {
            get;
            set;
        }


        public string ARTICLEEXTERNALURL
        {
            get;
            set;
        }


        public string ARTICLEAUTHOR
        {
            get;
            set;
        }


        public string ARTICLESOURCE
        {
            get;
            set;
        }


        public string ARTICLESUMMARY
        {
            get;
            set;
        }


        public DateTime? ARTICLEBASEDATE
        {
            get;
            set;
        }


        public DateTime? ARTICLEEDITDATE
        {
            get;
            set;
        }


        public string ARTICLETAG
        {
            get;
            set;
        }


        public string ARTICLECONTENT
        {
            get;
            set;
        }


        public int? ARTICLEAUDIT
        {
            get;
            set;
        }


        public int? ARTICLERELESE
        {
            get;
            set;
        }


        public int? ARTICLESITETYPE
        {
            get;
            set;
        }

        public int? ARTICLESTATUS
        {
            get;
            set;
        }


        public int? COLUMNID
        {
            get;
            set;
        }


        public string PUBLISHPATH
        {
            get;
            set;
        }


        public int? SITEID
        {
            get;
            set;
        }

        #endregion

        public class ARTICLEDAO : SqlDAO<ARTICLEEntity>
        {
            private SqlHelper _oracleHelper;
            public const string DBName = "SentimentConnStr";

            public ARTICLEDAO()
            {
                _oracleHelper = new SqlHelper(DBName);
            }

            public override void Add(ARTICLEEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into ARTICLE(");
                strSql.Append("ARTICLETITLE,ARTICLEOTHERTITLE,ARTICLEIMGPATH,ARTICLEDISSTYLE,ARTICLEEXTERNALURL,ARTICLEAUTHOR,ARTICLESOURCE,ARTICLESUMMARY,ARTICLEBASEDATE,ARTICLEEDITDATE,ARTICLETAG,ARTICLECONTENT,ARTICLEAUDIT,ARTICLERELESE,ARTICLESTATUS,COLUMNID,PUBLISHPATH,SITEID,ARTICLESITETYPE)");
                strSql.Append(" values (");
                strSql.Append("@ARTICLETITLE,@ARTICLEOTHERTITLE,@ARTICLEIMGPATH,@ARTICLEDISSTYLE,@ARTICLEEXTERNALURL,@ARTICLEAUTHOR,@ARTICLESOURCE,@ARTICLESUMMARY,@ARTICLEBASEDATE,@ARTICLEEDITDATE,@ARTICLETAG,@ARTICLECONTENT,@ARTICLEAUDIT,@ARTICLERELESE,@ARTICLESTATUS,@COLUMNID,@PUBLISHPATH,@SITEID,@ARTICLESITETYPE)");
                SqlParameter[] parameters = {
					new SqlParameter("@ARTICLETITLE",SqlDbType.NVarChar),
					new SqlParameter("@ARTICLEOTHERTITLE",SqlDbType.NVarChar),
					new SqlParameter("@ARTICLEIMGPATH",SqlDbType.NVarChar),
					new SqlParameter("@ARTICLEDISSTYLE",SqlDbType.Int),
					new SqlParameter("@ARTICLEEXTERNALURL",SqlDbType.NVarChar),
					new SqlParameter("@ARTICLEAUTHOR",SqlDbType.NVarChar),
					new SqlParameter("@ARTICLESOURCE",SqlDbType.NVarChar),
					new SqlParameter("@ARTICLESUMMARY",SqlDbType.NVarChar),
					new SqlParameter("@ARTICLEBASEDATE",SqlDbType.DateTime),
					new SqlParameter("@ARTICLEEDITDATE",SqlDbType.DateTime),
					new SqlParameter("@ARTICLETAG",SqlDbType.NVarChar),
					new SqlParameter("@ARTICLECONTENT",OracleDbType.NClob),
					new SqlParameter("@ARTICLEAUDIT",SqlDbType.Int),
					new SqlParameter("@ARTICLERELESE",SqlDbType.Int),
					new SqlParameter("@ARTICLESTATUS",SqlDbType.Int),
					new SqlParameter("@COLUMNID",SqlDbType.Int),
					new SqlParameter("@PUBLISHPATH",SqlDbType.NVarChar),
					new SqlParameter("@SITEID",SqlDbType.Int),
                    new SqlParameter("@ARTICLESITETYPE",SqlDbType.Int)
					};
                parameters[0].Value = entity.ARTICLETITLE;
                parameters[1].Value = entity.ARTICLEOTHERTITLE;
                parameters[2].Value = entity.ARTICLEIMGPATH;
                parameters[3].Value = entity.ARTICLEDISSTYLE;
                parameters[4].Value = entity.ARTICLEEXTERNALURL;
                parameters[5].Value = entity.ARTICLEAUTHOR;
                parameters[6].Value = entity.ARTICLESOURCE;
                parameters[7].Value = entity.ARTICLESUMMARY;
                parameters[8].Value = entity.ARTICLEBASEDATE;
                parameters[9].Value = entity.ARTICLEEDITDATE;
                parameters[10].Value = entity.ARTICLETAG;
                parameters[11].Value = entity.ARTICLECONTENT;
                parameters[12].Value = entity.ARTICLEAUDIT;
                parameters[13].Value = entity.ARTICLERELESE;
                parameters[14].Value = entity.ARTICLESTATUS;
                parameters[15].Value = entity.COLUMNID;
                parameters[16].Value = entity.PUBLISHPATH;
                parameters[17].Value = entity.SITEID;
                parameters[18].Value = entity.ARTICLESITETYPE;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override void Update(ARTICLEEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update ARTICLE set ");
                strSql.Append("ARTICLETITLE=@ARTICLETITLE,");
                strSql.Append("ARTICLEOTHERTITLE=@ARTICLEOTHERTITLE,");
                strSql.Append("ARTICLEIMGPATH=@ARTICLEIMGPATH,");
                strSql.Append("ARTICLEDISSTYLE=@ARTICLEDISSTYLE,");
                strSql.Append("ARTICLEEXTERNALURL=@ARTICLEEXTERNALURL,");
                strSql.Append("ARTICLEAUTHOR=@ARTICLEAUTHOR,");
                strSql.Append("ARTICLESOURCE=@ARTICLESOURCE,");
                strSql.Append("ARTICLESUMMARY=@ARTICLESUMMARY,");
                strSql.Append("ARTICLEBASEDATE=@ARTICLEBASEDATE,");
                strSql.Append("ARTICLEEDITDATE=@ARTICLEEDITDATE,");
                strSql.Append("ARTICLETAG=@ARTICLETAG,");
                strSql.Append("ARTICLECONTENT=@ARTICLECONTENT,");
                strSql.Append("ARTICLEAUDIT=@ARTICLEAUDIT,");
                strSql.Append("ARTICLERELESE=@ARTICLERELESE,");
                strSql.Append("ARTICLESTATUS=@ARTICLESTATUS,");
                strSql.Append("COLUMNID=@COLUMNID,");
                strSql.Append("PUBLISHPATH=@PUBLISHPATH,");
                strSql.Append("SITEID=@SITEID,");
                strSql.Append("ARTICLESITETYPE=@ARTICLESITETYPE");
                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@ARTICLETITLE",SqlDbType.NVarChar),
					new SqlParameter("@ARTICLEOTHERTITLE",SqlDbType.NVarChar),
					new SqlParameter("@ARTICLEIMGPATH",SqlDbType.NVarChar),
					new SqlParameter("@ARTICLEDISSTYLE",SqlDbType.Int),
					new SqlParameter("@ARTICLEEXTERNALURL",SqlDbType.NVarChar),
					new SqlParameter("@ARTICLEAUTHOR",SqlDbType.NVarChar),
					new SqlParameter("@ARTICLESOURCE",SqlDbType.NVarChar),
					new SqlParameter("@ARTICLESUMMARY",SqlDbType.NVarChar),
					new SqlParameter("@ARTICLEBASEDATE",SqlDbType.DateTime),
					new SqlParameter("@ARTICLEEDITDATE",SqlDbType.DateTime),
					new SqlParameter("@ARTICLETAG",SqlDbType.NVarChar),
					new SqlParameter("@ARTICLECONTENT",OracleDbType.NClob),
					new SqlParameter("@ARTICLEAUDIT",SqlDbType.Int),
					new SqlParameter("@ARTICLERELESE",SqlDbType.Int),
					new SqlParameter("@ARTICLESTATUS",SqlDbType.Int),
					new SqlParameter("@COLUMNID",SqlDbType.Int),
					new SqlParameter("@PUBLISHPATH",SqlDbType.NVarChar),
					new SqlParameter("@SITEID",SqlDbType.Int),
                    new SqlParameter("@ARTICLESITETYPE",SqlDbType.Int),
					new SqlParameter("@ID",OracleDbType.Int64)
				};
                parameters[0].Value = entity.ARTICLETITLE;
                parameters[1].Value = entity.ARTICLEOTHERTITLE;
                parameters[2].Value = entity.ARTICLEIMGPATH;
                parameters[3].Value = entity.ARTICLEDISSTYLE;
                parameters[4].Value = entity.ARTICLEEXTERNALURL;
                parameters[5].Value = entity.ARTICLEAUTHOR;
                parameters[6].Value = entity.ARTICLESOURCE;
                parameters[7].Value = entity.ARTICLESUMMARY;
                parameters[8].Value = entity.ARTICLEBASEDATE;
                parameters[9].Value = entity.ARTICLEEDITDATE;
                parameters[10].Value = entity.ARTICLETAG;
                parameters[11].Value = entity.ARTICLECONTENT;
                parameters[12].Value = entity.ARTICLEAUDIT;
                parameters[13].Value = entity.ARTICLERELESE;
                parameters[14].Value = entity.ARTICLESTATUS;
                parameters[15].Value = entity.COLUMNID;
                parameters[16].Value = entity.PUBLISHPATH;
                parameters[17].Value = entity.SITEID;
                parameters[18].Value = entity.ARTICLESITETYPE;
                parameters[19].Value = entity.ID;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public bool UpdateSet(int ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update ARTICLE set ");
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

            public bool UpdateSet(int ID, string[] ColumnList, string[] ValueList)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("update ARTICLE set ");
                    int Len = ColumnList.Length;
                    for (int i = 0; i < Len; i++)
                    {
                        string ColumnName = ColumnList[i];
                        string Value = ValueList[i];
                        if (i == Len - 1)
                        {
                            StrSql.AppendFormat("{0}='{1}'", ColumnName, Value);
                        }
                        else
                        {
                            StrSql.AppendFormat("{0}='{1}',", ColumnName, Value);
                        }
                    }
                    StrSql.Append(" where ID=" + ID);
                    _oracleHelper.ExecuteSql(StrSql.ToString(), null);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public bool UpdateSet(string ID, string ColumnName, string Value)
            {
                try
                {
                    StringBuilder StrSql = new StringBuilder();
                    StrSql.Append("UPDATE ARTICLE SET ");
                    StrSql.Append(ColumnName + "=" + Value);
                    StrSql.Append(" WHERE ID IN (" + ID + ")");
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
                string strSql = "delete from ARTICLE where ID=" + ID;
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
                string strSql = "delete from ARTICLE where ID in (" + ID + ")";
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

            public bool Delete(string url,int parentid)
            {
                string strSql = "delete from ARTICLE where ARTICLEEXTERNALURL in (" + url + ") AND COLUMNID IN (SELECT ID FROM COLUMNDEF WHERE PARENTID=" + parentid.ToString() + ")";
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

            public override void Delete(ARTICLEEntity entity)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from ARTICLE ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)
					};
                parameters[0].Value = entity.ID;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public override ARTICLEEntity FindById(long primaryKeyId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from ARTICLE ");
                strSql.Append(" where ID=@primaryKeyId");
                SqlParameter[] parameters = {
						new SqlParameter("@primaryKeyId", SqlDbType.Int)};
                parameters[0].Value = primaryKeyId;
                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    ARTICLEEntity entity = new ARTICLEEntity();
                    if (!Convert.IsDBNull(row["ID"]))
                    {
                        entity.ID = Convert.ToInt64(row["ID"]);
                    }
                    entity.ARTICLETITLE = row["ARTICLETITLE"].ToString();
                    entity.ARTICLEOTHERTITLE = row["ARTICLEOTHERTITLE"].ToString();
                    entity.ARTICLEIMGPATH = row["ARTICLEIMGPATH"].ToString();
                    if (!Convert.IsDBNull(row["ARTICLEDISSTYLE"]))
                    {
                        entity.ARTICLEDISSTYLE = Convert.ToInt32(row["ARTICLEDISSTYLE"]);
                    }
                    entity.ARTICLEEXTERNALURL = row["ARTICLEEXTERNALURL"].ToString();
                    entity.ARTICLEAUTHOR = row["ARTICLEAUTHOR"].ToString();
                    entity.ARTICLESOURCE = row["ARTICLESOURCE"].ToString();
                    entity.ARTICLESUMMARY = row["ARTICLESUMMARY"].ToString();
                    if (!Convert.IsDBNull(row["ARTICLEBASEDATE"]))
                    {
                        entity.ARTICLEBASEDATE = Convert.ToDateTime(row["ARTICLEBASEDATE"]);
                    }
                    if (!Convert.IsDBNull(row["ARTICLEEDITDATE"]))
                    {
                        entity.ARTICLEEDITDATE = Convert.ToDateTime(row["ARTICLEEDITDATE"]);
                    }
                    entity.ARTICLETAG = row["ARTICLETAG"].ToString();
                    entity.ARTICLECONTENT = row["ARTICLECONTENT"].ToString();
                    if (!Convert.IsDBNull(row["ARTICLEAUDIT"]))
                    {
                        entity.ARTICLEAUDIT = Convert.ToInt32(row["ARTICLEAUDIT"]);
                    }
                    if (!Convert.IsDBNull(row["ARTICLERELESE"]))
                    {
                        entity.ARTICLERELESE = Convert.ToInt32(row["ARTICLERELESE"]);
                    }
                    if (!Convert.IsDBNull(row["ARTICLESTATUS"]))
                    {
                        entity.ARTICLESTATUS = Convert.ToInt32(row["ARTICLESTATUS"]);
                    }
                    if (!Convert.IsDBNull(row["COLUMNID"]))
                    {
                        entity.COLUMNID = Convert.ToInt32(row["COLUMNID"]);
                    }
                    entity.PUBLISHPATH = row["PUBLISHPATH"].ToString();
                    if (!Convert.IsDBNull(row["SITEID"]))
                    {
                        entity.SITEID = Convert.ToInt32(row["SITEID"]);
                    }
                    if (!Convert.IsDBNull(row["ARTICLESITETYPE"]))
                    {
                        entity.ARTICLESITETYPE = Convert.ToInt32(row["ARTICLESITETYPE"]);
                    }
                    return entity;
                }
                else
                {
                    return null;
                }
            }

            public bool IsExistUrl(int siteid, int columnid, string url)
            {
                int timespan = Convert.ToInt32(ConfigurationManager.AppSettings["TimeSpan"]);
                string strwhere = " SITEID=@SITEID AND COLUMNID=@COLUMNID AND ARTICLEEXTERNALURL=@ARTICLEEXTERNALURL";
                SqlParameter[] parameters = {
                        new SqlParameter("@SITEID", SqlDbType.Int),
						new SqlParameter("@COLUMNID", SqlDbType.Int),
                        new SqlParameter("@ARTICLEEXTERNALURL",SqlDbType.NVarChar)                               
                };
                parameters[0].Value = siteid;
                parameters[1].Value = columnid;
                parameters[2].Value = url;
                IList<ARTICLEEntity> list = Find(strwhere, parameters);
                if (list.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public List<ARTICLEEntity> Find(int top, string strWhere)
            {
                return Find(top, strWhere, null);
            }

            public List<ARTICLEEntity> Find(int top, string strWhere, SqlParameter[] parameters)
            {
                StringBuilder PagerSql = new StringBuilder();
                PagerSql.Append("SELECT * FROM (");
                PagerSql.Append(" SELECT A.*, ROWNUM RN ");
                PagerSql.Append("FROM (SELECT * FROM ARTICLE ");
                if (!string.IsNullOrEmpty(strWhere))
                {
                    PagerSql.Append(" where " + strWhere);
                }
                PagerSql.AppendFormat(" ) A WHERE ROWNUM <= {0})", top);

                DataSet ds = _oracleHelper.ExecuteDateSet(PagerSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<ARTICLEEntity> list = new List<ARTICLEEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ARTICLEEntity entity = new ARTICLEEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt64(row["ID"]);
                        }
                        entity.ARTICLETITLE = row["ARTICLETITLE"].ToString();
                        entity.ARTICLEOTHERTITLE = row["ARTICLEOTHERTITLE"].ToString();
                        entity.ARTICLEIMGPATH = row["ARTICLEIMGPATH"].ToString();
                        if (!Convert.IsDBNull(row["ARTICLEDISSTYLE"]))
                        {
                            entity.ARTICLEDISSTYLE = Convert.ToInt32(row["ARTICLEDISSTYLE"]);
                        }
                        entity.ARTICLEEXTERNALURL = row["ARTICLEEXTERNALURL"].ToString();
                        entity.ARTICLEAUTHOR = row["ARTICLEAUTHOR"].ToString();
                        entity.ARTICLESOURCE = row["ARTICLESOURCE"].ToString();
                        entity.ARTICLESUMMARY = row["ARTICLESUMMARY"].ToString();
                        if (!Convert.IsDBNull(row["ARTICLEBASEDATE"]))
                        {
                            entity.ARTICLEBASEDATE = Convert.ToDateTime(row["ARTICLEBASEDATE"]);
                        }
                        if (!Convert.IsDBNull(row["ARTICLEEDITDATE"]))
                        {
                            entity.ARTICLEEDITDATE = Convert.ToDateTime(row["ARTICLEEDITDATE"]);
                        }
                        entity.ARTICLETAG = row["ARTICLETAG"].ToString();
                        entity.ARTICLECONTENT = row["ARTICLECONTENT"].ToString();
                        if (!Convert.IsDBNull(row["ARTICLEAUDIT"]))
                        {
                            entity.ARTICLEAUDIT = Convert.ToInt32(row["ARTICLEAUDIT"]);
                        }
                        if (!Convert.IsDBNull(row["ARTICLERELESE"]))
                        {
                            entity.ARTICLERELESE = Convert.ToInt32(row["ARTICLERELESE"]);
                        }
                        if (!Convert.IsDBNull(row["ARTICLESTATUS"]))
                        {
                            entity.ARTICLESTATUS = Convert.ToInt32(row["ARTICLESTATUS"]);
                        }
                        if (!Convert.IsDBNull(row["COLUMNID"]))
                        {
                            entity.COLUMNID = Convert.ToInt32(row["COLUMNID"]);
                        }
                        entity.PUBLISHPATH = row["PUBLISHPATH"].ToString();
                        if (!Convert.IsDBNull(row["ARTICLESITETYPE"]))
                        {
                            entity.ARTICLESITETYPE = Convert.ToInt32(row["ARTICLESITETYPE"]);
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

            public List<ARTICLEEntity> Find(string strWhere)
            {
                return Find(strWhere, null);
            }

            public override List<ARTICLEEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM ARTICLE ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<ARTICLEEntity> list = new List<ARTICLEEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ARTICLEEntity entity = new ARTICLEEntity();
                        if (!Convert.IsDBNull(row["ID"]))
                        {
                            entity.ID = Convert.ToInt64(row["ID"]);
                        }
                        entity.ARTICLETITLE = row["ARTICLETITLE"].ToString();
                        entity.ARTICLEOTHERTITLE = row["ARTICLEOTHERTITLE"].ToString();
                        entity.ARTICLEIMGPATH = row["ARTICLEIMGPATH"].ToString();
                        if (!Convert.IsDBNull(row["ARTICLEDISSTYLE"]))
                        {
                            entity.ARTICLEDISSTYLE = Convert.ToInt32(row["ARTICLEDISSTYLE"]);
                        }
                        entity.ARTICLEEXTERNALURL = row["ARTICLEEXTERNALURL"].ToString();
                        entity.ARTICLEAUTHOR = row["ARTICLEAUTHOR"].ToString();
                        entity.ARTICLESOURCE = row["ARTICLESOURCE"].ToString();
                        entity.ARTICLESUMMARY = row["ARTICLESUMMARY"].ToString();
                        if (!Convert.IsDBNull(row["ARTICLEBASEDATE"]))
                        {
                            entity.ARTICLEBASEDATE = Convert.ToDateTime(row["ARTICLEBASEDATE"]);
                        }
                        if (!Convert.IsDBNull(row["ARTICLEEDITDATE"]))
                        {
                            entity.ARTICLEEDITDATE = Convert.ToDateTime(row["ARTICLEEDITDATE"]);
                        }
                        entity.ARTICLETAG = row["ARTICLETAG"].ToString();
                        entity.ARTICLECONTENT = row["ARTICLECONTENT"].ToString();
                        if (!Convert.IsDBNull(row["ARTICLEAUDIT"]))
                        {
                            entity.ARTICLEAUDIT = Convert.ToInt32(row["ARTICLEAUDIT"]);
                        }
                        if (!Convert.IsDBNull(row["ARTICLERELESE"]))
                        {
                            entity.ARTICLERELESE = Convert.ToInt32(row["ARTICLERELESE"]);
                        }
                        if (!Convert.IsDBNull(row["ARTICLESTATUS"]))
                        {
                            entity.ARTICLESTATUS = Convert.ToInt32(row["ARTICLESTATUS"]);
                        }
                        if (!Convert.IsDBNull(row["COLUMNID"]))
                        {
                            entity.COLUMNID = Convert.ToInt32(row["COLUMNID"]);
                        }
                        entity.PUBLISHPATH = row["PUBLISHPATH"].ToString();
                        if (!Convert.IsDBNull(row["SITEID"]))
                        {
                            entity.SITEID = Convert.ToInt32(row["SITEID"]);
                        }
                        if (!Convert.IsDBNull(row["ARTICLESITETYPE"]))
                        {
                            entity.ARTICLESITETYPE = Convert.ToInt32(row["ARTICLESITETYPE"]);
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
                strSql.Append(" FROM ARTICLE");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                return _oracleHelper.ExecuteDateSet(strSql.ToString(), param);
            }


            public DataTable GetArticleColumnDt(string urlStr,int parentid) {
                string strSql = "SELECT A.ARTICLEEXTERNALURL,B.COLUMNNAME FROM ARTICLE A,COLUMNDEF B "
                + "where A.COLUMNID=B.ID AND A.ARTICLEEXTERNALURL in (" + urlStr + ") AND B.PARENTID=" + parentid.ToString();
                DataSet ds = _oracleHelper.ExecuteDateSet(strSql, null);
                if (ds != null)
                {
                    return ds.Tables[0];
                }
                else {
                    return null;
                }
            }
            #region paging methods

            /// <summary>
            /// 获取分页记录总数
            /// </summary>
            /// <param name="where">条件，等同于GetPaer()方法的where</param>
            /// <returns>返回记录总数</returns>

            public int GetPagerRowsCount(string where)
            {
                return GetPagerRowsCount(where, null);
            }

            public int GetPagerRowsCount(string where, SqlParameter[] param)
            {
                string sql = "select count(*) from ARTICLE ";
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


            public DataTable GetPager(string where, string orderBy, int pageSize, int pageNumber)
            {
                return GetPager(where, null, orderBy, pageSize, pageNumber);
            }


            public DataTable GetPager(string where, SqlParameter[] param, string orderBy, int pageSize, int pageNumber)
            {
                int startNumber = pageSize * (pageNumber - 1) + 1;
                int endNumber = pageSize * pageNumber;

                StringBuilder PagerSql = new StringBuilder();
                PagerSql.Append("SELECT * FROM (");
                PagerSql.Append(" SELECT A.*, ROWNUM RN ");
                PagerSql.Append("FROM (SELECT * FROM ARTICLE ");
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

