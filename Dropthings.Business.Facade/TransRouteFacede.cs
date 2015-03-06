using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dropthings.Data;

namespace Dropthings.Business.Facade
{
	public class TransRouteFacede
	{
        private static readonly TRANSROUTEEntity.TRANSROUTEDAO dao = new TRANSROUTEEntity.TRANSROUTEDAO();

        public static IList<TRANSROUTEEntity> Find(string strWhere) {
            return dao.Find(strWhere);
        }
	}
}
