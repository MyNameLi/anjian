using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dropthings.Data;

namespace Dropthings.Business.Facade
{
    public static class ColumnFacade
    {
        private static COLUMNDEFEntity.COLUMNDEFDAO dao = new COLUMNDEFEntity.COLUMNDEFDAO();
        public static COLUMNDEFEntity GetEntityById(long id) {
            COLUMNDEFEntity entity = dao.FindById(id);
            return entity;
        }

        public static IList<COLUMNDEFEntity> GetListBySiteId(int siteid, int parentid)
        {
            string strWhere = " SITEID=" + siteid.ToString() + " AND COLUMNSTATUS=1 AND PARENTID=" + parentid.ToString();
            return dao.Find(strWhere);
        }

        public static IList<COLUMNDEFEntity> GetListBySiteId(int siteid)
        {
            string strWhere = " SITEID=" + siteid.ToString() + " AND COLUMNSTATUS=1";
            return dao.Find(strWhere);
        }
    }
}
