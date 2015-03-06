using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dropthings.Data;

namespace Dropthings.Business.Facade
{
    public static class ReportListFacade
    {
        private static REPORTLISTEntity.REPORTLISTDAO Dao = new REPORTLISTEntity.REPORTLISTDAO();
        public static void Add(REPORTLISTEntity entity)
        {
            Dao.Add(entity);
        }

        public static IList<REPORTLISTEntity> Find(string where)
        {
            return Dao.Find(where);
        }

        public static bool delete(string reportid)
        {
            try
            {
                Dao.Delete(reportid);
                return true;
            }
            catch {
                return false;
            }
        }
    }
}
