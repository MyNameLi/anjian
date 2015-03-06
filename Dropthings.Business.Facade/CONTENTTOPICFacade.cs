using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dropthings.Data;
using System.Data;

namespace Dropthings.Business.Facade
{
	public class CONTENTTOPICFacade
	{
        private static readonly CONTENTTOPICEntity.CONTENTTOPICDAO dao = new CONTENTTOPICEntity.CONTENTTOPICDAO();

        public static void add(CONTENTTOPICEntity entity) {
            dao.Add(entity);
        }

        public static CONTENTTOPICEntity GetEntityById(long id) {
            return dao.FindById(id);
        }

        public static bool delete(int id) {
            return dao.Delete(id);
        }

        public static void delete(string id)
        {
            dao.Delete(id);
        }

        public static void update(CONTENTTOPICEntity entity) {
            dao.Update(entity);
        }

        public static DataTable GetPagerDt(string strwhere, string orderby, int pagesize, int pagenumber)
        {
            return dao.GetPager(strwhere, orderby, pagesize, pagenumber);
        }

        public static int GetPagerCount(string where) {
            return dao.GetPagerRowsCount(where);
        }

        public static bool updateset(int id, string columnname, string status)
        {
            return dao.UpdateSet(id, columnname, status);
        }
	}
}
