using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dropthings.Util;

namespace Dropthings.Business.Facade
{
	public class TopicFacade
	{
        public static string weibotopicjson(DataTable dt, int totalcount)
        {
            StringBuilder jsonstr = new StringBuilder();
            int count = 1;
            jsonstr.Append("{");
            foreach (DataRow row in dt.Rows)
            {
                jsonstr.AppendFormat("\"entity_{0}\":", count);
                jsonstr.Append("{");
                jsonstr.AppendFormat("\"ID\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["ID"].ToString()));
                jsonstr.AppendFormat("\"NAME\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["NAME"].ToString()));
                jsonstr.AppendFormat("\"KEYWORD\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["KEYWORDS"].ToString()));
                jsonstr.AppendFormat("\"STARTTIME\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["STARTTIME"].ToString()));
                jsonstr.AppendFormat("\"INDUSTRY\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["INDUSTRY"].ToString()));
                jsonstr.AppendFormat("\"ORIGINALCOUNT\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["ORIGINALCOUNT"].ToString()));
                jsonstr.AppendFormat("\"FORWARDCOUNT\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["FORWARDCOUNT"].ToString()));
                jsonstr.AppendFormat("\"COMMENTCOUNT\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["COMMENTCOUNT"].ToString()));
                jsonstr.AppendFormat("\"ISSTOP\":\"{0}\"", EncodeByEscape.GetEscapeStr(row["ISSTOP"].ToString()));
                jsonstr.Append("},");
                count++;
            }
            jsonstr.Append("\"Count\":" + totalcount + "}");
            return jsonstr.ToString();
        }

        public static string weibotopicjson(DataTable dt)
        {
            StringBuilder jsonstr = new StringBuilder();
            int count = 1;
            jsonstr.Append("{");
            foreach (DataRow row in dt.Rows)
            {
                jsonstr.AppendFormat("\"entity_{0}\":", count);
                jsonstr.Append("{");
                jsonstr.AppendFormat("\"ID\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["ID"].ToString()));
                jsonstr.AppendFormat("\"NAME\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["NAME"].ToString()));
                jsonstr.AppendFormat("\"KEYWORD\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["KEYWORDS"].ToString()));
                jsonstr.AppendFormat("\"STARTTIME\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["STARTTIME"].ToString()));
                jsonstr.AppendFormat("\"INDUSTRY\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["INDUSTRY"].ToString()));
                jsonstr.AppendFormat("\"ORIGINALCOUNT\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["ORIGINALCOUNT"].ToString()));
                jsonstr.AppendFormat("\"FORWARDCOUNT\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["FORWARDCOUNT"].ToString()));
                jsonstr.AppendFormat("\"COMMENTCOUNT\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["COMMENTCOUNT"].ToString()));
                jsonstr.AppendFormat("\"ISSTOP\":\"{0}\"", EncodeByEscape.GetEscapeStr(row["ISSTOP"].ToString()));
                jsonstr.Append("},");
                count++;
            }
            jsonstr.Append("\"Success\":1}");
            return jsonstr.ToString();
        }
	}
}
