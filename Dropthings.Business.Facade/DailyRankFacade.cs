using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dropthings.Data;
using System.Data;
using System.Data.SqlClient;

namespace Dropthings.Business.Facade
{
    public class DailyRankFacade
    {
        private static readonly DailyRankEntity.DailyRankDao dao = new DailyRankEntity.DailyRankDao();
        //public static DataTable GetTodayLog(string startTime, string endTime, int newsType)
        //{
        //    string sqlwhere = " CREATETIME BETWEEN :STARTTIME AND :ENDTIME AND NEWSTYPE :NEWSTYPE  ORDER BY NEWSTYPE ASC ";
        //    OracleParameter[] parameters = {
        //            new OracleParameter(":STARTTIME",OracleDbType.Date),
        //            new OracleParameter(":ENDTIME",OracleDbType.Date),
        //            new OracleParameter(":NEWSTYPE",SqlDbType.Int)
        //            };
        //    parameters[0].Value = DateTime.Parse(startTime);
        //    parameters[1].Value = DateTime.Parse(endTime);
        //    parameters[2].Value = newsType;

        //    return dao.GetDataSet(sqlwhere, parameters).Tables[0];
        //}
        public static DataTable GetTodayLog(string startTime, string endTime)
        {
            string sqlwhere = " CREATETIME BETWEEN @STARTTIME AND @ENDTIME ORDER BY NEWSTYPE ASC ";
            SqlParameter[] parameters = {
					new SqlParameter("@STARTTIME",SqlDbType.DateTime),
					new SqlParameter("@ENDTIME",SqlDbType.DateTime)
		            };
            parameters[0].Value = DateTime.Parse(startTime);
            parameters[1].Value = DateTime.Parse(endTime);

            return dao.GetDataSet(sqlwhere, parameters).Tables[0]; 
        }
    }
}
