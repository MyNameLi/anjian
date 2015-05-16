using System;
namespace Dropthings.Data.Repository
{
    public interface IRoleRepository : IDisposable
    {
        System.Collections.Generic.List<Dropthings.Data.RolesEntity> GetAllRole();
        Dropthings.Data.RolesEntity GetRoleByRoleName(string roleName);
        void CreateRole(string roleName);
        void DeleteRole(string roleName);
    }
}
