using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace Dropthings.Data
{
    public class COLUMNTEMPLATEEntity
    {
        public int COLUMNID
        {
            set;
            get;
        }

        public int TEMPLATEID
        {
            set;
            get;
        }

        public int SITEID
        {
            set;
            get;
        }

        public int TEMPLATETYPE
        {
            set;
            get;
        }

        public string HTMLSTR
        {
            set;
            get;
        }

        public string TEMPLATENAME
        {
            set;
            get;
        }

        public string NEWSIDLIST
        {
            set;
            get;
        }

        public class COLUMNTEMPLATEEntityDao
        {
            private SqlHelper _oracleHelper;
            public const string DBName = "SentimentConnStr";

            public COLUMNTEMPLATEEntityDao()
            {
                _oracleHelper = new SqlHelper(DBName);
            }

            public void Add(COLUMNTEMPLATEEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into COLUMNTEMPLATE(");
                strSql.Append("COLUMNID,TEMPLATEID,TEMPLATETYPE,HTMLSTR,TEMPLATENAME,NEWSIDLIST,SITEID)");
                strSql.Append(" values (");
                strSql.Append("@COLUMNID,@TEMPLATEID,@TEMPLATETYPE,@HTMLSTR,@TEMPLATENAME,@NEWSIDLIST,@SITEID)");
                SqlParameter[] parameters = {
					new SqlParameter("@COLUMNID",SqlDbType.Int),
					new SqlParameter("@TEMPLATEID",SqlDbType.Int),
					new SqlParameter("@TEMPLATETYPE",SqlDbType.Int),
					new SqlParameter("@HTMLSTR",SqlDbType.Text),
                    new SqlParameter("@TEMPLATENAME",SqlDbType.NVarChar),
                    new SqlParameter("@NEWSIDLIST",SqlDbType.NVarChar),
                    new SqlParameter("@SITEID",SqlDbType.Int)
					};
                parameters[0].Value = entity.COLUMNID;
                parameters[1].Value = entity.TEMPLATEID;
                parameters[2].Value = entity.TEMPLATETYPE;
                parameters[3].Value = entity.HTMLSTR;
                parameters[4].Value = entity.TEMPLATENAME;
                parameters[5].Value = entity.NEWSIDLIST;
                parameters[6].Value = entity.SITEID;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public void delete(int COLUMNID, int TEMPLATEID)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from COLUMNTEMPLATE ");
                strSql.Append("where COLUMNID = :COLUMNID AND ");
                strSql.Append("TEMPLATEID = :TEMPLATEID ");
                SqlParameter[] parameters = {
					new SqlParameter("@COLUMNID",SqlDbType.Int),	
		            new SqlParameter("@TEMPLATEID",SqlDbType.Int)			
					};
                parameters[0].Value = COLUMNID;
                parameters[1].Value = TEMPLATEID;
                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public void Update(int COLUMNID, int TEMPLATEID, string HTMLSTR)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update COLUMNTEMPLATE set ");
                strSql.Append("HTMLSTR = :HTMLSTR");
                strSql.Append(" where COLUMNID = :COLUMNID AND TEMPLATEID = :TEMPLATEID");
                SqlParameter[] parameters = {					
					new SqlParameter("@HTMLSTR",SqlDbType.DateTime),
					new SqlParameter("@COLUMNID",SqlDbType.Int),
					new SqlParameter("@TEMPLATEID",SqlDbType.Int)					
					};
                parameters[0].Value = HTMLSTR;
                parameters[1].Value = COLUMNID;
                parameters[2].Value = TEMPLATEID;

                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }


            public void Update(COLUMNTEMPLATEEntity entity)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update COLUMNTEMPLATE set ");
                strSql.Append("HTMLSTR = :HTMLSTR,");
                strSql.Append("NEWSIDLIST = :NEWSIDLIST,");
                strSql.Append("TEMPLATENAME = :TEMPLATENAME,");
                strSql.Append("TEMPLATETYPE = :TEMPLATETYPE");
                strSql.Append(" where COLUMNID = :COLUMNID AND TEMPLATEID = :TEMPLATEID");
                SqlParameter[] parameters = {					
					new SqlParameter("@HTMLSTR",SqlDbType.Text),
                    new SqlParameter("@NEWSIDLIST",SqlDbType.NVarChar),
                    new SqlParameter("@TEMPLATENAME",SqlDbType.NVarChar),
                    new SqlParameter("@TEMPLATETYPE",SqlDbType.Int),
					new SqlParameter("@COLUMNID",SqlDbType.Int),
					new SqlParameter("@TEMPLATEID",SqlDbType.Int)					
					};
                parameters[0].Value = entity.HTMLSTR;
                parameters[1].Value = entity.NEWSIDLIST;
                parameters[2].Value = entity.TEMPLATENAME;
                parameters[3].Value = entity.TEMPLATETYPE;
                parameters[4].Value = entity.COLUMNID;
                parameters[5].Value = entity.TEMPLATEID;

                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }


            public void UpdateRuleStr(int COLUMNID, int TEMPLATEID, string RuleStr)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update COLUMNTEMPLATE set ");
                strSql.Append("RuleStr = :RuleStr");
                strSql.Append(" where COLUMNID = :COLUMNID AND TEMPLATEID = :TEMPLATEID");
                SqlParameter[] parameters = {					
					new SqlParameter("@HTMLSTR",SqlDbType.Text),
					new SqlParameter("@COLUMNID",SqlDbType.Int),
					new SqlParameter("@TEMPLATEID",SqlDbType.Int)					
					};
                parameters[0].Value = RuleStr;
                parameters[1].Value = COLUMNID;
                parameters[2].Value = TEMPLATEID;

                _oracleHelper.ExecuteSql(strSql.ToString(), parameters);
            }

            public COLUMNTEMPLATEEntity FindEntity(string strWhere, SqlParameter[] parameters)
            {
                IList<COLUMNTEMPLATEEntity> list = Find(strWhere, parameters);
                if (list.Count > 0)
                {
                    return list[0];
                }
                else
                {
                    return null;
                }
            }

            public List<COLUMNTEMPLATEEntity> Find(string strWhere)
            {
                return Find(strWhere, null);
            }

            public List<COLUMNTEMPLATEEntity> Find(string strWhere, SqlParameter[] parameters)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM COLUMNTEMPLATE ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                DataSet ds = _oracleHelper.ExecuteDateSet(strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<COLUMNTEMPLATEEntity> list = new List<COLUMNTEMPLATEEntity>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        COLUMNTEMPLATEEntity entity = new COLUMNTEMPLATEEntity();
                        if (!Convert.IsDBNull(row["COLUMNID"]))
                        {
                            entity.COLUMNID = Convert.ToInt32(row["COLUMNID"]);
                        }
                        if (!Convert.IsDBNull(row["TEMPLATEID"]))
                        {
                            entity.TEMPLATEID = Convert.ToInt32(row["TEMPLATEID"]);
                        }
                        if (!Convert.IsDBNull(row["TEMPLATETYPE"]))
                        {
                            entity.TEMPLATETYPE = Convert.ToInt32(row["TEMPLATETYPE"]);
                        }
                        entity.HTMLSTR = row["HTMLSTR"].ToString();
                        entity.TEMPLATENAME = row["TEMPLATENAME"].ToString();
                        entity.NEWSIDLIST = row["NEWSIDLIST"].ToString();
                        list.Add(entity);
                    }

                    return list;
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
