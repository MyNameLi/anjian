namespace Dropthings.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using OmarALZabir.AspectF;
    using Dropthings.Util;
    using System.Data.SqlClient;
    using System.Data;
    

    public class RoleRepository : Dropthings.Data.Repository.IRoleRepository
    {
        #region Fields

        //private readonly IDatabase _database;
        private readonly RolesEntity.RolesDAO _roleDAO;
        private readonly ICache _cacheResolver;

        #endregion Fields

        #region Constructors

        public RoleRepository( ICache cacheResolver)
        {
            //this._database = database;
            this._roleDAO = new RolesEntity.RolesDAO();
            this._cacheResolver = cacheResolver;
        }

        #endregion Constructors

        #region Methods

        private void RemoveRoleNameDependentItems(string roleName)
        {
            RemoveRoleCollection();
            _cacheResolver.Remove(CacheKeys.RoleKeys.RoleByRoleName(roleName));
        }

        private void RemoveRoleCollection()
        {
            _cacheResolver.Remove(CacheKeys.RoleKeys.AllRoles());
        }

        public void CreateRole(string roleName)
        {
            _roleDAO.Add(new RolesEntity(0, roleName, ""));
        }

        public void DeleteRole(string roleName)
        {
            SqlParameter[] parameters = { new SqlParameter("@ROLENAME",SqlDbType.NVarChar) };
            parameters[0].Value = roleName;

            RolesEntity role = _roleDAO.Find("ROLENAME=@ROLENAME", parameters).FirstOrDefault();
            if (null != role)
            {
                _roleDAO.Delete(role);
            }
        }

        public List<RolesEntity> GetAllRole()
        {
            return AspectF.Define
                .Cache<List<RolesEntity>>(_cacheResolver, CacheKeys.RoleKeys.AllRoles())
                .Return<List<RolesEntity>>(() =>
                    _roleDAO.Find("", null));

            //return _roleDAO.Find("", null);

            //return AspectF.Define
            //    .Cache<List<AspNetRole>>(_cacheResolver, CacheKeys.RoleKeys.AllRoles())
            //    .Return<List<AspNetRole>>(() =>
            //        _database.Query(CompiledQueries.RoleQueries.GetAllRole)
            //        .ToList());
        }

        public RolesEntity GetRoleByRoleName(string roleName)
        {
            SqlParameter[] parameters = { new SqlParameter("@ROLENAME",SqlDbType.NVarChar) };
            parameters[0].Value = roleName;

            return AspectF.Define
                .Cache<RolesEntity>(_cacheResolver, CacheKeys.RoleKeys.RoleByRoleName(roleName))
                .Return<RolesEntity>(() =>
                    _roleDAO.Find("ROLENAME=@ROLENAME", parameters).FirstOrDefault());

            //List<RolesEntity> list = _roleDAO.Find("ROLENAME=@ROLENAME", parameters);
            //if (list.Count > 0) return list[0];

            //return null;

            //return AspectF.Define
            //    .Cache<AspNetRole>(_cacheResolver, CacheKeys.RoleKeys.RoleByRoleName(roleName))
            //    .Return<AspNetRole>(() =>
            //        _database.Query(
            //            CompiledQueries.RoleQueries.GetRoleByRoleName, roleName)
            //        .First());
        }

        #endregion Methods

        #region IDisposable Members

        public void Dispose()
        {
            //_database.Dispose();
        }

        #endregion
    }
}