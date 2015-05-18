using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using IdolACINet;
using System.Configuration;
using Dropthings.Data;
using Dropthings.Util;


namespace DBJob
{
    public class DBTrendData
    {
        private CategoryEntity.CategoryDAO dao = new CategoryEntity.CategoryDAO();
        private TrendDataEntity.TrendDataDAO TrendDao = new TrendDataEntity.TrendDataDAO();
        private Connection conn = new Connection(ConfigurationManager.AppSettings["IdolHttp"], 9000);
        public void Do()
        {
            #region
            string strWhere = " parentcate = 202";
            IList<CategoryEntity> list = dao.Find(strWhere, null);
            int timeSpan = Convert.ToInt32(ConfigurationManager.AppSettings["TimeSpan"].ToString());
            DateTime nowTime = DateTime.Now.AddDays(timeSpan);
            foreach (CategoryEntity entity in list)
            {
                if (entity.EventDate == null)
                {
                    continue;
                }
                DateTime eventTime = Convert.ToDateTime(entity.EventDate.Value);
                int totalday = TimeHelp.GetMilliDay(nowTime, eventTime);
                if (totalday <= 90)
                {
                    if (GetCountOfTrendData(entity.CategoryID.Value) > 0)
                    {
                        RunOnce(entity, nowTime);
                    }
                    else
                    {
                        RunALL(entity, totalday, nowTime);
                    }
                }
            }
            #endregion
        }

        private void RunOnce(CategoryEntity entity, DateTime nowTime)
        {
            String timeStr = GetTimeStr(nowTime);
            CategoryQueryCommand query = new CategoryQueryCommand();
            query.Category = entity.CategoryID.Value;
            query.TotalResults = true;
            query.Databases = "safety";
            query.Params = "MinScore,MinDate,Maxdate";
            query.Values = entity.MinScore + "," + timeStr + "," + timeStr;
            query.PrintFields = "none";
            XmlDocument xmldoc = conn.Execute(query).Data;
            if (xmldoc != null)
            {
                long totalcount = Convert.ToInt64(GetTotalCount(xmldoc));
                TrendDataEntity trendEntity = new TrendDataEntity();
                trendEntity.CategoryId = entity.CategoryID;
                trendEntity.ArticleCount = totalcount;
                trendEntity.Date = nowTime;
                TrendDao.Add(trendEntity);
            }
        }

        private void RunALL(CategoryEntity entity, int days, DateTime nowTime)
        {
            int totaldays = days + 7;
            for (int i = 0; i <= totaldays; i++)
            {
                int runCount = (-1) * i;
                String timeStr = GetTimeStr(nowTime.AddDays(runCount));
                CategoryQueryCommand query = new CategoryQueryCommand();
                query.Category = entity.CategoryID.Value;
                query.TotalResults = true;
                query.Databases = "safety";
                query.PrintFields = "none";
                query.Params = "MinScore,MinDate,Maxdate";
                query.Values = entity.MinScore + "," + timeStr + "," + timeStr;
                XmlDocument xmldoc = conn.Execute(query).Data;
                if (xmldoc != null)
                {
                    long totalcount = Convert.ToInt64(GetTotalCount(xmldoc));
                    TrendDataEntity trendEntity = new TrendDataEntity();
                    trendEntity.CategoryId = entity.CategoryID;
                    trendEntity.ArticleCount = totalcount;
                    trendEntity.Date = nowTime.AddDays(runCount);
                    TrendDao.Add(trendEntity);
                }
            }
        }

        private int GetCountOfTrendData(long categoryid)
        {
            string strWhere = "categoryid =" + categoryid;
            IList<TrendDataEntity> list = TrendDao.Find(strWhere, null);
            if (list != null)
            {
                return list.Count;
            }
            else
            {
                return 0;
            }

        }

        private string GetTimeStr(DateTime time)
        {
            return time.ToString("dd-MM-yyyy").Replace("-", "/");
        }

        private string GetTotalCount(XmlDocument xmldoc)
        {
            try
            {
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
                nsmgr.AddNamespace("autn", "http://schemas.autonomy.com/aci/");
                return xmldoc.SelectSingleNode("autnresponse/responsedata/autn:totalhits", nsmgr).InnerText;
            }
            catch (Exception e)
            {
                return "0";
            }
        }
    }
}
