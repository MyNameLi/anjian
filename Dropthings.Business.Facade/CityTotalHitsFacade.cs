using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dropthings.Data;

namespace Dropthings.Business.Facade
{
    public static class CityTotalHitsFacade
    {
        private static CITYTOTALHITSEntity.CITYTOTALHITSDAO dao = new CITYTOTALHITSEntity.CITYTOTALHITSDAO();

        //全国趋势图
        public static DataTable GroupByProvince(string startTime, string endTime)
        {
            var ds = dao.GetGroupByProvince(startTime, endTime);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
    }
}
