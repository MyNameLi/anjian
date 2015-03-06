using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dropthings.Data;
using System.Data;

namespace Dropthings.Business.Facade
{
	public class NoteMessageFacade
	{
        private static NoteMessageEntity.NoteMessageDAO dao = new NoteMessageEntity.NoteMessageDAO();

        public static IList<NoteMessageEntity> GetList(string strWhere) {
            return dao.Find(strWhere);
        }

        public static void UpdateSetStatus(int value,long id){
            dao.UpdateSetStatus(value, id);
        }

        public static int GetPagerCount(string strWhere) {
            return dao.GetPagerRowsCount(strWhere);
        }

        public static NoteMessageEntity GetEntity(long id) {
            return dao.FindById(id);
        }

        public static void Delete(long id){
            dao.Delete(id);
        }

        public static void Delete(string idlist){
            dao.Delete(idlist);
        }

        public static DataTable GetPageDt(string where, string orderby, int pageSize, int pageNum) {
            return dao.GetPager(where, orderby, pageSize, pageNum);
        }
	}
}
