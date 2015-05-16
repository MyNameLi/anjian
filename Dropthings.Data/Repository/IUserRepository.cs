using System;
using System.Collections.Generic;
using System.Data;

namespace Dropthings.Data.Repository
{
    public interface IUserRepository : IDisposable
    {
        List<Dropthings.Data.RolesEntity> GetRolesOfUser(int userid);
        Dropthings.Data.UsersEntity GetUserByUserID(int userid);
        Dropthings.Data.UsersEntity GetUserFromUserName(string userName);

        Dropthings.Data.UsersEntity GetUser(string userName,string password);

        int GetUserIDFromUserName(string userName);
       
        int CreateUser(string registeredUserName, string password, string email);

        int CreateUser(UsersEntity entity);

        DataTable GetUsersDTFromStrSql(string strSql);
        
         void Update(UsersEntity member);

         void DeleteUser(string useName);

         bool DeleteUser(int useid);

         IList<UsersEntity> GetUsersList(string idlist, bool tag);

         DataTable GetUserPagerDataTable(string where, string orderBy, int pageSize, int pageNumber);
    }
}
