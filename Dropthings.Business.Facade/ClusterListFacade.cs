using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dropthings.Data;
using System.Data;
using Dropthings.Util;

namespace Dropthings.Business.Facade
{
	public class ClusterListFacade
	{
        private static readonly CLUSTERLISTEntity.CLUSTERLISTDAO dao = new CLUSTERLISTEntity.CLUSTERLISTDAO();

        public static DataTable GetPager(string strwhere, string orderby, int pagesize, int pagernumber) {
            return dao.GetPager(strwhere, orderby, pagesize, pagernumber);
        }

        public static int GetPagerCount(string strwhere) {
            return dao.GetPagerRowsCount(strwhere);
        }

        public static string GetPagerJsonStr(string strwhere, string orderby, int pagesize, int pagernumber) {
            DataTable dt = GetPager(strwhere, orderby, pagesize, pagernumber);
            int totalcount = GetPagerCount(strwhere);
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
                    jsonstr.AppendFormat("\"clustername\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["CLUSTERNAME"].ToString()));
                    jsonstr.AppendFormat("\"editdate\":\"{0}\",", EncodeByEscape.GetEscapeStr(row["EDITDATE"].ToString()));
                    jsonstr.AppendFormat("\"distype\":\"{0}\",", row["DISTYPE"].ToString());
                    jsonstr.AppendFormat("\"param\":\"{0}\"", row["PARAM"].ToString());
                    jsonstr.Append("},");
                    count++;
                }
                jsonstr.AppendFormat("\"Count\":{0}", totalcount);
                jsonstr.Append("}");
            }
            return jsonstr.ToString();
        }

        public static void add(CLUSTERLISTEntity entity) {
            dao.Add(entity);
        }

        public static CLUSTERLISTEntity FindEntity(long id)
        {
            return dao.FindById(id);            
        }

        public static bool delete(int id) {
            return dao.Delete(id);
        }

        public static bool delete(string id)
        {
            return dao.Delete(id);
        }

        public static void UpDate(CLUSTERLISTEntity entity) {
            dao.Update(entity);
        }

        public static void ReStore()
        {
            dao.ReStore();
        }

        public static bool UpSetDisType(int ID, int value)
        {
            return dao.UpdateSet(ID, "DISTYPE", value.ToString());
        }

        public static bool UpSetParam(int ID, int value)
        {
            return dao.UpdateSet(ID, "PARAM", value.ToString());
        }

        public static bool UpSetClusterName(int ID, string value)
        {
            return dao.UpdateSet(ID, "CLUSTERNAME", value);
        }
	}
}
