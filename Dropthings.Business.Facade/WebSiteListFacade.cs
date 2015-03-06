using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using Dropthings.Data;

namespace Dropthings.Business.Facade
{
	public static class WebSiteListFacade
	{
        private readonly static WEBSITELISTEntity.WEBSITELISTDAO dao = new WEBSITELISTEntity.WEBSITELISTDAO();

        public static IList<WEBSITELISTEntity> Find() {
            IList<WEBSITELISTEntity> list = dao.Find("");
            return list;
        }

        public static WEBSITELISTEntity FindById(long id) {
            return dao.FindById(id);
        }

        public static bool Add(WEBSITELISTEntity entity)
        {
            try
            {
                dao.Add(entity);
                return true;
            }
            catch {
                return false;
            }
        }

        public static bool UpDate(WEBSITELISTEntity entity)
        {
            try
            {
                dao.Update(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Delete(int Id)
        {
            try
            {
                dao.Delete(Id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Delete(string Idlist)
        {
            try
            {
                dao.Delete(Idlist);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static int GetPagerCount(string where) {
            return dao.GetPagerRowsCount(where);    
        }

        public static DataTable GetPagerDataTable(string where, string orderBy, int pageSize, int pageNumber)
        {
            return dao.GetPager(where, orderBy, pageSize, pageNumber);
        }
	}
}
