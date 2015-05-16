using System;
namespace Dropthings.Data.Repository
{
    public interface IRoleTemplateRepository : IDisposable
    {
        void Delete(Dropthings.Data.RoleTemplateEntity roleTemplate);
        System.Collections.Generic.List<Dropthings.Data.RoleTemplateEntity> GeAllRoleTemplates();
        Dropthings.Data.RoleTemplateEntity GetRoleTemplateByRoleName(string roleName);
        Dropthings.Data.RoleTemplateEntity GetRoleTemplateByTemplateUserName(string userName);
        Dropthings.Data.RoleTemplateEntity GetRoleTemplatesByUserId(int userId);
        Dropthings.Data.RoleTemplateEntity Insert(Dropthings.Data.RoleTemplateEntity roleTemplate);
        void Update(Dropthings.Data.RoleTemplateEntity roleTemplate);
    }
}
