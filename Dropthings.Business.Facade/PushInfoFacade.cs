using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dropthings.Data;
using Dropthings.Util;

namespace Dropthings.Business.Facade
{
	public class PushInfoFacade
	{
        private static readonly PUSHINFOEntity.PUSHINFODAO dao = new PUSHINFOEntity.PUSHINFODAO();

        public static void add(PUSHINFOEntity entity)
        {
            dao.Add(entity);
        }

        public static void delete(int userid)
        {
            dao.DeleteByUserId(userid);
        }

        public static IList<PUSHINFOEntity> GetListByUserId(string useridlist,string pushTime)
        {
            return dao.FindByUserid(useridlist, pushTime);
        }

        public static IList<string> GetUrlByUserId(string useridlist)
        {
            IList<PUSHINFOEntity> list = GetListByUserId(useridlist, null);
            IList<string> urllist = new List<string>();
            foreach (PUSHINFOEntity entity in list) {
                urllist.Add(entity.URL);
            }
            return urllist;
        }

        public static IList<PUSHINFOEntity> GetListByRoleId(string roleidlist,string pushTime)
        {
            return dao.FindByRoleid(roleidlist, pushTime);
        }

        public static IList<string> GetUrlRoleId(string roleidlist)
        {
            IList<PUSHINFOEntity> list = GetListByRoleId(roleidlist, null);
            IList<string> urllist = new List<string>();
            foreach (PUSHINFOEntity entity in list)
            {
                urllist.Add(entity.URL);
            }
            return urllist;
        }

        public static string GetJsonStr(string RoleOrUserId,string pushTime, int type)
        {
            IList<PUSHINFOEntity> list = new List<PUSHINFOEntity>();
            if (type == 1)
            {
                list = GetListByUserId(RoleOrUserId, pushTime);
            }
            else if (type == 2)
            {
                list = GetListByRoleId(RoleOrUserId, pushTime);
            }
            if (list != null && list.Count > 0)
            {
                Dictionary<string, int> dict = new Dictionary<string, int>();
                StringBuilder url = new StringBuilder();
                foreach (PUSHINFOEntity entity in list)
                {                    
                    if (url.Length > 0)
                    {
                        url.Append("+");
                    }
                    url.Append(entity.URL);
                    if (!dict.ContainsKey(entity.URL))
                    {
                        dict.Add(entity.URL, entity.PUSHTYPE.Value);
                    }
                }
                QueryParamEntity queryparam = new QueryParamEntity();
                queryparam.Text = "*";
                queryparam.MatchReference = url.ToString();
                queryparam.Combine = "DREREFERENCE";
                queryparam.PageSize = list.Count;
                IdolQuery query = IdolQueryFactory.GetDisStyle("query");
                query.queryParamsEntity = queryparam;
                IList<IdolNewsEntity> newslist = query.GetResultList();
                StringBuilder jsonstr = new StringBuilder();
                int count = 1;
                jsonstr.Append("{");
                foreach (IdolNewsEntity newentity in newslist)
                {                    
                    jsonstr.AppendFormat("\"entity_{0}\":", count);                    
                    jsonstr.Append("{");
                    jsonstr.AppendFormat("\"href\":\"{0}\",", EncodeByEscape.GetEscapeStr(newentity.Href));
                    if (dict.ContainsKey(newentity.Href))
                    {
                        jsonstr.AppendFormat("\"type\":\"{0}\",", dict[newentity.Href]);
                    }
                    jsonstr.AppendFormat("\"title\":\"{0}\",", EncodeByEscape.GetEscapeStr(newentity.Title));
                    jsonstr.AppendFormat("\"time\":\"{0}\",", EncodeByEscape.GetEscapeStr(newentity.TimeStr.Substring(0, 10)));
                    jsonstr.AppendFormat("\"site\":\"{0}\"", EncodeByEscape.GetEscapeStr(newentity.SiteName));
                    jsonstr.Append("},");
                    count++;
                }
                jsonstr.Append("\"Success\":1}");
                return jsonstr.ToString();
            }
            else
            {
                return "";
            }
        }
	}
}
