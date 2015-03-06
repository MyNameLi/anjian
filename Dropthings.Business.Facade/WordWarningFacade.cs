using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dropthings.Data;

namespace Dropthings.Business.Facade
{
	public static class WordWarningFacade
	{
        private static readonly WORDWARNINGEntity.WORDWARNINGDAO dao = new WORDWARNINGEntity.WORDWARNINGDAO();

        public static IList<WORDWARNINGEntity> FindByWhere(string where)
        {
            return dao.Find(where);
        }
	}
}
