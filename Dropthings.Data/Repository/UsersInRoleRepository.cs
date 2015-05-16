namespace Dropthings.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Linq.Expressions;
    using OmarALZabir.AspectF;
    using Dropthings.Util;
    

    public class UsersInRoleRepository : Dropthings.Data.Repository.IUsersInRoleRepository
    {
                #region Fields

        //private readonly IDatabase _database;
        private readonly UsersInRolesEntity.UsersInRolesDAO Dao;
        private readonly ICache _cacheResolver;

        #endregion Fields

        #region Constructors

        public UsersInRoleRepository(ICache cacheResolver)
        {
            //this._database = database;
            this.Dao = new UsersInRolesEntity.UsersInRolesDAO();
            this._cacheResolver = cacheResolver;
        }

        #endregion Constructors

        #region Methods

        public bool DeleteByUserId(int userId) {
            return Dao.DeletebyUserID(userId);
        }

        public bool Delete(int userId,int roleid)
        {
            return Dao.DeletebyRoleIDAndUserId(userId, roleid);
        }

        public void Add(UsersInRolesEntity entity){
            Dao.Add(entity);
        }

        public string GetUserRoleIdList(int userId) {
            string strWhere = " USERID=" + userId.ToString();
            IList<UsersInRolesEntity> list = Dao.Find(strWhere, null);
            StringBuilder roleIdList = new StringBuilder();
            foreach (UsersInRolesEntity entity in list) {
                if (roleIdList.Length > 0) {
                    roleIdList.Append(",");
                }
                roleIdList.Append(entity.ROLEID);
            }
            return roleIdList.ToString();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            //_database.Dispose();
        }

        #endregion
    }
}
