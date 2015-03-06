using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dropthings.Data;
using Dropthings.Util;

namespace Dropthings.Business.Facade
{
	public static class AlarmFacade
	{
        private static readonly ALARMEntity.ALARMDAO dao = new ALARMEntity.ALARMDAO();

        public static DataTable GetSiteWarningPagerDataTable(string where, string orderby,int start, int pagesize)
        {
            return dao.GetSiteWarningPager(where, orderby, pagesize, start);
        }

        public static DataTable GetWordRuleWarningPagerDataTable(string where, string orderby, int start, int pagesize)
        {
            return dao.GetWordRuleWarningPager(where, orderby, pagesize, start);
        }

        public static int GetCount(string where) {
            return dao.GetPagerRowsCount(where);
        }

        public static string GetSiteWarningJsonStr(string where, string orderby, int start, int pagesize,string countwhere)
        {
            DataTable dt = GetSiteWarningPagerDataTable(where, orderby, start, pagesize);
            int totalcount = GetCount(countwhere);
            StringBuilder jsonstr = new StringBuilder();
            if (dt.Rows.Count > 0) {
                jsonstr.Append("{");
                int count = 1;
                foreach (DataRow row in dt.Rows)
                {
                    jsonstr.AppendFormat("\"entity_{0}\":", count);
                    jsonstr.Append("{");
                    jsonstr.AppendFormat("\"dochref\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["DOCREF"].ToString()));
                    jsonstr.AppendFormat("\"doctitle\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["DOCTITLE"].ToString()));
                    jsonstr.AppendFormat("\"docsite\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["DOCSITE"].ToString()));
                    jsonstr.AppendFormat("\"doctime\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["DOCTIME"].ToString()));
                    jsonstr.AppendFormat("\"alarmreplynum\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["ALARMREPLYNUM"].ToString()));
                    jsonstr.AppendFormat("\"replynum\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["INVITATION"].ToString()));
                    jsonstr.AppendFormat("\"alarmclickrate\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["ALARMCLICKRATE"].ToString()));
                    jsonstr.AppendFormat("\"clickrate\":\"{0}\"", EncodeByEscape.GetEscapeStr(row["PAGEVIEW"].ToString()));
                    jsonstr.Append("},");
                    count++;
                }
                jsonstr.AppendFormat("\"Count\":{0}", totalcount);
                jsonstr.Append("}");
            }
            return jsonstr.ToString();
        }

        public static string GetWordWarningJsonStr(string where, string orderby, int start, int pagesize,string countwhere)
        {
            int totalcount = GetCount(countwhere);
            DataTable dt = GetWordRuleWarningPagerDataTable(where, orderby, start, pagesize);            
            StringBuilder jsonstr = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                jsonstr.Append("{");
                int count = 1;
                foreach (DataRow row in dt.Rows)
                {
                    jsonstr.AppendFormat("\"entity_{0}\":", count);
                    jsonstr.Append("{");
                    jsonstr.AppendFormat("\"dochref\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["DOCREF"].ToString()));
                    jsonstr.AppendFormat("\"doctitle\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["DOCTITLE"].ToString()));
                    jsonstr.AppendFormat("\"docsite\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["DOCSITE"].ToString()));
                    jsonstr.AppendFormat("\"doctime\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["DOCTIME"].ToString()));
                    jsonstr.AppendFormat("\"alarmrulenum\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["RULEALARMNUM"].ToString()));
                    jsonstr.AppendFormat("\"rulenum\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["THRESHOLDS"].ToString()));
                    jsonstr.AppendFormat("\"wordrule\":\"{0}\"", EncodeByEscape.GetEscapeStr(row["WORDRULE"].ToString()));
                    jsonstr.Append("},");
                    count++;
                }
                jsonstr.AppendFormat("\"Count\":{0}", totalcount);
                jsonstr.Append("}");
            }
            return jsonstr.ToString();
        }
	}
}
