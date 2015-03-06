using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dropthings.Data;

namespace Dropthings.Business.Facade
{
    public static class TemplateFacade
    {
        private static TEMPLATEEntity.TEMPLATEDAO dao = new TEMPLATEEntity.TEMPLATEDAO();
        public static IList<TEMPLATEEntity> GetList(string strwhere) {
            IList<TEMPLATEEntity> list = dao.Find(strwhere);
            return list;
        }
    }
}
