using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dropthings.Data;

using System.Transactions;
using System.Web.Security;
using Dropthings.Configuration;
using OmarALZabir.AspectF;
using Dropthings.Util;
using System.Web.Profile;
using System.Data.Objects.DataClasses;
using System.Data;

namespace Dropthings.Business.Facade
{
	partial class Facade
    {
        #region Methods

        public bool DeleteByUserId(int userId) {
            return this.usersInRoleRepository.DeleteByUserId(userId);
        }

        public void Add(UsersInRolesEntity entity) {
            this.usersInRoleRepository.Add(entity);
        }

        public string GetUserRoleIdList(int userId)
        {
            return this.usersInRoleRepository.GetUserRoleIdList(userId);
        }

        public IList<WidgetsInRolesEntity> GetWidGetListByRoleId(int roleid) {
            return this.widgetsInRolesRepository.GetWidGetListByRoleId(roleid);
        }

        public void DeleteWidGetByRoleId(int roleid) {
            this.widgetsInRolesRepository.Delete(roleid);
        }

        public void Add(WidgetsInRolesEntity entity) {
            this.widgetsInRolesRepository.Add(entity);
        }

        #endregion
    }
}
