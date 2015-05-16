using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dropthings.Data.Repository
{
    public interface IUsersInRoleRepository : IDisposable
    {
        bool DeleteByUserId(int userId);
        bool Delete(int userid, int roleid);
        void Add(UsersInRolesEntity entity);
        string GetUserRoleIdList(int userId);        
    }
}
