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
    

    public class RoleTemplateRepository : IRoleTemplateRepository
    {
        #region Fields

        //private readonly IDatabase _database;
        private readonly RoleTemplateEntity.ROLETEMPLATEDAO _roleTemplateDAO;
        private readonly ICache _cacheResolver;

        #endregion Fields

        #region Constructors

        public RoleTemplateRepository(ICache cacheResolver)
        {
            //this._database = database;
            this._roleTemplateDAO = new RoleTemplateEntity.ROLETEMPLATEDAO();
            this._cacheResolver = cacheResolver;
        }

        #endregion Constructors

        #region Methods

        public void Delete(RoleTemplateEntity roleTemplate)
        {
            _roleTemplateDAO.Delete(roleTemplate.ID.Value);
            //_database.Delete<RoleTemplate>( roleTemplate);
        }

        public List<RoleTemplateEntity> GeAllRoleTemplates()
        {
            return _roleTemplateDAO.Find("", null);
            //return _database.Query(CompiledQueries.RoleQueries.GetRoleTemplates)
            //    .ToList();
        }

        public RoleTemplateEntity GetRoleTemplateByRoleName(string roleName)
        {
            return AspectF.Define
                .Cache<RoleTemplateEntity>(_cacheResolver, CacheKeys.RoleKeys.RoleTemplateByRoleName(roleName))
                .Return<RoleTemplateEntity>(() =>
                    _roleTemplateDAO.GetRoleTemplateByRoleName(roleName).FirstOrDefault());

            //List<RoleTemplateEntity> list = _roleTemplateDAO.GetRoleTemplateByRoleName(roleName);
            //if (list.Count > 0) return list[0];

            //return null;
            //return AspectF.Define
            //    .Cache<RoleTemplate>(_cacheResolver, CacheKeys.RoleKeys.RoleTemplateByRoleName(roleName))
            //    .Return<RoleTemplate>(() =>
            //        _database.Query(CompiledQueries.RoleQueries.GetRoleTemplateByRoleName, roleName)
            //        .FirstOrDefault());
        }

        public RoleTemplateEntity GetRoleTemplateByTemplateUserName(string userName)
        {
            return AspectF.Define
                .Cache<RoleTemplateEntity>(_cacheResolver, CacheKeys.UserKeys.RoleTemplateByUser(userName))
                .Return<RoleTemplateEntity>(() =>
                    _roleTemplateDAO.GetRoleTemplateByTemplateUserName(userName).FirstOrDefault());

            //List<RoleTemplateEntity> list = _roleTemplateDAO.GetRoleTemplateByTemplateUserName(userName);
            //if (list.Count > 0) return list[0];

            //return null;

            //return AspectF.Define
            //    .Cache<RoleTemplate>(_cacheResolver, CacheKeys.UserKeys.RoleTemplateByUser(userName))
            //    .Return<RoleTemplate>(() =>
            //        _database.Query(CompiledQueries.RoleQueries.GetRoleTemplateByTemplateUserName, userName)
            //        .FirstOrDefault());
        }

        public RoleTemplateEntity GetRoleTemplatesByUserId(int userId)
        {
            SqlParameter[] parameters = {
						new SqlParameter("@TEMPLATEUSERID", SqlDbType.Int)};
            parameters[0].Value = userId;

            return AspectF.Define
                .Cache<RoleTemplateEntity>(_cacheResolver, CacheKeys.TemplateKeys.RoleTemplateByUser(userId))
                .Return<RoleTemplateEntity>(() =>
                    _roleTemplateDAO.Find("TEMPLATEUSERID=@TEMPLATEUSERID", parameters).FirstOrDefault());

            //List<RoleTemplateEntity> list = _roleTemplateDAO.Find("TEMPLATEUSERID=@TEMPLATEUSERID", parameters);
            //if (list.Count > 0) return list[0];

            //return null;

            //return AspectF.Define
            //    .Cache<RoleTemplate>(_cacheResolver, CacheKeys.TemplateKeys.RoleTemplateByUser(userId))
            //    .Return<RoleTemplate>(() =>
            //        _database.Query(CompiledQueries.RoleQueries.GetRoleTemplatesByUserId, userId)
            //        .FirstOrDefault());
        }

        public RoleTemplateEntity Insert(RoleTemplateEntity roleTemplate)
        {
            _roleTemplateDAO.Add(roleTemplate);
            // _database.Insert<AspNetUser, AspNetRole, RoleTemplate>(
            //    user, role,    
            //    (u, rt) => rt.AspNetUser = u,
            //    (r, rt) => rt.AspNetRole = r,
            //    roleTemplate);

            return roleTemplate;
        }

        public void Update(RoleTemplateEntity roleTemplate)
        {
            _roleTemplateDAO.Update(roleTemplate);
            //_database.Update<RoleTemplate>(roleTemplate);
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