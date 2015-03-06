using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dropthings.Data;

namespace Dropthings.Business.Facade
{
    partial class Facade
	{
        private readonly MENULISTEntity.MENULISTDAO menuDao = new MENULISTEntity.MENULISTDAO();
        private readonly MENUINROLEEntity.MENUINROLEDAO menuroleDao = new MENUINROLEEntity.MENUINROLEDAO();
        public IList<MENULISTEntity> GetMenuListByWhere(string Where) {
            return menuDao.Find(Where);
        }

        public IList<MENUINROLEEntity> GetMenuInRoleByRoleId(int roleid) {
            string strWhere = " ROLEID=" + roleid.ToString();
            return menuroleDao.Find(strWhere);
        }

        public void DeleteMenuInRoleByRoleId(int roleid) {
            menuroleDao.DeleteByRoleId(roleid);
        }

        public void InsertMenuInRoleEntity(MENUINROLEEntity entity) {
            menuroleDao.Add(entity);
        }

        public IList<MENULISTEntity> GetMenuListByRoleIdAndParent(int roleid,int parentid) {
            IList<MENULISTEntity> backlist = new List<MENULISTEntity>();
            IList<MENULISTEntity> list = menuDao.GetMenuList(roleid);
            foreach (MENULISTEntity entity in list)
            {
                if (entity.PARENTID == parentid) {
                    backlist.Add(entity);
                }
            }
            return backlist;
        }

        public IList<MENULISTEntity> GetMenuListByRoleIdAndParent(string roleidlist, int parentid)
        {
            IList<MENULISTEntity> backlist = new List<MENULISTEntity>();
            IList<MENULISTEntity> list = menuDao.GetMenuList(roleidlist);
            foreach (MENULISTEntity entity in list)
            {
                if (entity.PARENTID == parentid)
                {
                    backlist.Add(entity);
                }
            }
            return backlist;
        }

        public IList<MENULISTEntity> GetMenuListByRoleId(string roleidlist)
        {
            IList<MENULISTEntity> backlist = new List<MENULISTEntity>();
            IList<MENULISTEntity> list = menuDao.GetMenuList(roleidlist);
            foreach (MENULISTEntity entity in list)
            {
               backlist.Add(entity);                
            }
            return backlist;
        }

        public IList<MENULISTEntity> GetMenuChildListByRoleIdAndParent(int roleid, int parentid)
        {
            IList<MENULISTEntity> backlist = new List<MENULISTEntity>();
            IList<MENULISTEntity> list = menuDao.GetMenuList(roleid);
            AddChildMenu(list, parentid, ref backlist);
            return backlist;
        }

        public IList<MENULISTEntity> GetMenuChildListByRoleIdAndParent(string roleidlist, int parentid)
        {
            IList<MENULISTEntity> backlist = new List<MENULISTEntity>();
            IList<MENULISTEntity> list = menuDao.GetMenuList(roleidlist);
            AddChildMenu(list, parentid, ref backlist);
            return backlist;
        }

        private void AddChildMenu(IList<MENULISTEntity> list, int parentid, ref IList<MENULISTEntity> backlist) {
            foreach (MENULISTEntity entity in list) {
                if (entity.PARENTID == parentid) {
                    backlist.Add(entity);
                    AddChildMenu(list, entity.ID.Value, ref backlist);
                }
            }
        }
	}
}
