using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dropthings.Data;
namespace Dropthings.Business.Facade
{
	public class PageOfRoleFacade
	{
        private static PAGEOFROLEEntity.PAGEOFROLEDAO pd = new PAGEOFROLEEntity.PAGEOFROLEDAO();
        public static List<PAGEOFROLEEntity> Find(int roleid)
        {
            string where = "ROLEID=:ROLEID";
            return pd.Find(where,roleid);
        }

        public static void DeletePageOfRoleByRoleId(int roleid)
        {
            pd.DeleteByRoleId(roleid);
            
        }
        public static void Add(PAGEOFROLEEntity entity)
        {
            pd.Add(entity);
        }
	}
}
