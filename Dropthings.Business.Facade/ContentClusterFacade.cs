using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dropthings.Data;
using Dropthings.Util;
using System.Data;

namespace Dropthings.Business.Facade
{
	public class ContentClusterFacade
	{
        private static CONTENTCLUSTEREntity.CONTENTCLUSTERDAO dao = new CONTENTCLUSTEREntity.CONTENTCLUSTERDAO();

        public static IList<CONTENTCLUSTEREntity> GetListByClusterId(string clusterid) {
            return dao.FindByClusterId(clusterid);
        }

        public static IList<CONTENTCLUSTEREntity> GetListByTaskId(int taskid)
        {
            return dao.FindByTaskId(taskid);
        }

        public static int GetPagerCount(string TaskOrClusterId, int type) {
            if (type == 1)
            {
                return dao.GetPagerRowsCountByClusterId(TaskOrClusterId);
            }
            else {
                return dao.GetPagerRowsCountByTaskId(Convert.ToInt32(TaskOrClusterId));
            }
        }

        public static DataTable GetPagerDt(string TaskOrClusterId, int type, string orderby, int pageSize, int pageNumber)
        {
            string where = string.Empty;
            if (type == 1)
            {
                where = " CLUSTERID=" + TaskOrClusterId;
            }
            else
            {
                where = " TASKID=" + TaskOrClusterId;
            }
            return dao.GetPager(where, orderby, pageSize, pageNumber);
        }

        public static string GetDataStr(string taskid, string orderby, int start, int pagesize)
        {
            DataTable dt = GetPagerDt(taskid, 2, orderby, pagesize, start);
            int totalcount = GetPagerCount(taskid, 2);
            StringBuilder jsonstr = new StringBuilder();
            if (dt.Rows.Count > 0)
            {                
                jsonstr.Append("{");
                int count = 1;
                foreach (DataRow row in dt.Rows)
                {
                    jsonstr.AppendFormat("\"entity_{0}\":", count);
                    jsonstr.Append("{");
                    jsonstr.AppendFormat("\"id\":\"{0}\",", row["ID"].ToString());
                    jsonstr.AppendFormat("\"url\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["MAINURL"].ToString()));
                    jsonstr.AppendFormat("\"title\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["TITLE"].ToString()));
                    jsonstr.AppendFormat("\"summary\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["SUMMARY"].ToString()));
                    jsonstr.AppendFormat("\"sitename\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["SITENAME"].ToString()));
                    jsonstr.AppendFormat("\"adddate\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["ADDDATE"].ToString()));
                    jsonstr.AppendFormat("\"totalsub\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["TOTALSUB"].ToString()));
                    jsonstr.AppendFormat("\"clusterid\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["CLUSTERID"].ToString()));
                    jsonstr.AppendFormat("\"domainsite\":\"{0}\"", EncodeByEscape.GetEscapeStr(row["DOMAINSITE"].ToString()));
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
