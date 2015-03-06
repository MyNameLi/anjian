using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dropthings.Data;
using System.Data;
using Dropthings.Util;

namespace Dropthings.Business.Facade
{
	public class ClusterInfoFacade
	{
        private static readonly CLUSTERINFOEntity.CLUSTERINFODAO dao = new CLUSTERINFOEntity.CLUSTERINFODAO();

        public static IList<CLUSTERINFOEntity> Find(int ClusterId) {
            return dao.FindByClusterID(ClusterId);
        }

        public static string GetListStr(int ClusterId)
        {
            IList<CLUSTERINFOEntity> list = Find(ClusterId);
            StringBuilder jsonstr = new StringBuilder();
            if (list.Count > 0) {
                int count = 1;
                jsonstr.Append("{");
                foreach (CLUSTERINFOEntity entity in list) {
                    jsonstr.AppendFormat("\"entity_{0}\":", count);
                    jsonstr.Append("{");
                    jsonstr.AppendFormat("\"title\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.TITLE));
                    jsonstr.AppendFormat("\"url\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.URL));
                    jsonstr.AppendFormat("\"site\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.SITE));
                    jsonstr.AppendFormat("\"date\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.BASEDATE.Value.ToString("yyyy-MM-dd hh:mm")));
                    jsonstr.AppendFormat("\"tag\":\"{0}\",", entity.TAG);
                    jsonstr.AppendFormat("\"clusterid\":\"{0}\"", entity.CLUSTERID);
                    jsonstr.Append("},");
                    count++;
                }
                jsonstr.Append("\"SuccessCode\":1}");
            }
            return jsonstr.ToString();
        }

        public static bool Delete(int ClusterId)
        {
            return dao.Delete(ClusterId);
        }

        public static bool Delete(string ClusterId)
        {
            return dao.Delete(ClusterId);
        }

        public static void add(CLUSTERINFOEntity entity) {
            dao.Add(entity);
        }
	}
}
