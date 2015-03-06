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

        public bool Login(string userName, string password)
        {
            throw new Exception("this method not implemented");
            //return Membership.ValidateUser(userName, password);
        }

        public UsersEntity GetUser(string userName)
        {
            return AspectF.Define.Cache<UsersEntity>(Services.Get<ICache>(), CacheKeys.UserKeys.UserFromUserName(userName))
                .Return<UsersEntity>(() => this.userRepository.GetUserFromUserName(userName));
        }
        public UsersEntity GetUser(int userGuid)
        {
            return AspectF.Define.Cache<UsersEntity>(Services.Get<ICache>(), CacheKeys.UserKeys.UserFromUserID(userGuid))
                .Return<UsersEntity>(() => this.userRepository.GetUserByUserID(userGuid));
        }

        public IList<UsersEntity> GetUserList(string idlist, bool tag) {
            return this.userRepository.GetUsersList(idlist, tag);
        }

        public int CreateUser(string registeredUserName, string password, string email)
        {
            return this.userRepository.CreateUser(registeredUserName, password, email);
            //MembershipUser newUser = Membership.CreateUser(registeredUserName, password, email);
            //return (Guid)newUser.ProviderUserKey;
        }

        public int CreateUser(UsersEntity entity)
        {
            return this.userRepository.CreateUser(entity);
            //MembershipUser newUser = Membership.CreateUser(registeredUserName, password, email);
            //return (Guid)newUser.ProviderUserKey;
        }


        //public UsersEntity CreateUser(string userName, string password, string email, bool isApproved, out MembershipCreateStatus status)
        //{
        //    return this.userRepository.CreateUser(userName, password, email, null, null, isApproved, out status);
        //}

        public void UpdateUser(UsersEntity member)
        {
            this.userRepository.Update(member);
        }

        public void DeleteUser(string useName)
        {
            this.userRepository.DeleteUser(useName);
        }

        public bool DeleteUser(int useId)
        {
            return this.userRepository.DeleteUser(useId);
        }

        public UserSettingEntity GetUserSetting(int userid)
        {
            var userSetting = this.userSettingRepository.GetUserSettingByUserGuid(userid);

            if (userSetting == null)
            {
                // No setting saved before. Create default setting
                List<int> tabs = this.pageRepository.GetTabIdByUserGuid(userid);
                //if (tabs.Count > 0) return null;

                userSetting = this.userSettingRepository.Insert(new UserSettingEntity
                    {
                        USERID = userid,
                        CREATEDDATE = DateTime.Now,
                        CURRENTPAGEID = tabs.First(),
                        TIMESTAMP = DateTime.Now
                    });
            }

            return userSetting;
        }

        public bool TransferOwnership(int userid, int userOldid)
        {
            var success = false;

            using (TransactionScope ts = new TransactionScope())
            {
                // TODO: Since changing the page object will change the object
                // directly into the cache, next time getting the same pages will
                // return the new user ID for the pages. We need to clone the pages.
                IEnumerable<PageEntity> pages = this.pageRepository.GetTabsOfUser(userOldid);

                var newUser = this.userRepository.GetUserByUserID(userid);
                pages.Each(page =>
                    {
                        page.USERID = userid;// newUser;
                        //page.AspNetUserReference = new EntityReference<UsersEntity>
                        //{
                        //    EntityKey = newUser.EntityKey
                        //};
                    });
                this.pageRepository.UpdateList(pages);

                var userSetting = GetUserSetting(userOldid);

                // Delete setting for the anonymous user and create new setting for the new user 
                this.userSettingRepository.Delete(userSetting);

                this.userSettingRepository.Insert(new UserSettingEntity
                {
                    USERID = userid,
                    CURRENTPAGEID = userSetting.CURRENTPAGEID,
                    CREATEDDATE = DateTime.Now
                });

                ts.Complete();
            }

            return success;
        }

        public bool UserExists(string userName)
        {
            return (this.GetUserIDFromUserName(userName) != 0);
        }

        public void CreateTemplateUser(string email, bool isActivationRequired, string password, string requestedUserName, string roleName, string templateRoleName)
        {
            var userid = this.GetUserIDFromUserName(requestedUserName);
            if (userid == 0)
            {
                var newUserGuid = CreateUser(requestedUserName, password, email);
                SetUserRoles(requestedUserName, new string[] { roleName });
                AddUserToRoleTemplate(newUserGuid, templateRoleName);

                var createdTab = CreateTab(newUserGuid, string.Empty, 0, 0);

                if (createdTab != null && createdTab.ID > 0)
                {
                    var userSetting = GetUserSetting(newUserGuid);
                    CreateDefaultWidgetsOnTab(requestedUserName, createdTab.ID.Value);
                }
                else
                {
                    throw new ApplicationException("First page creation failed");
                }
            }
        }

        public void SetupDefaultSetting()
        {
            SetupDefaultRoles();

            var settings = (UserSettingTemplateSettingsSection)System.Configuration.ConfigurationManager.GetSection(UserSettingTemplateSettingsSection.SectionName);

            foreach (UserSettingTemplateElement setting in settings.UserSettingTemplates)
            {
                CreateTemplateUser(setting.UserName, false, setting.Password, setting.UserName, setting.RoleNames, setting.TemplateRoleName);
            }
        }

        public int GetUserIDFromUserName(string userName)
        {
            return this.userRepository.GetUserIDFromUserName(userName);
        }

        public bool IsUserAnonymous(string userName)
        {
            return false;
            //var user = this.userRepository.GetUserByUserID(this.userRepository.GetUserIDFromUserName(userName));
            //if (user == null)
            //    throw new ApplicationException("User does not exist:" + userName);
            //else
            //    return user.;
        }

        public DataTable GetUserRoleDataTable(string strWhere) {
            string strSql = "select * from USERS where USERID > 0";
            if (!string.IsNullOrEmpty(strWhere)) {
                strSql += " AND " + strWhere;
            }
            return this.userRepository.GetUsersDTFromStrSql(strSql);
        }

        public DataTable GetUserByRoleId(string roleid)
        {
            string strSql = "select A.*,B.* from USERS A,USERSINROLES B where A.USERID = B.USERID AND B.ROLEID IN (" + roleid + ")";            
            return this.userRepository.GetUsersDTFromStrSql(strSql);
        }

        public bool DeleteUserFromUserInRoles(int userid, int roleid)
        {
            return this.usersInRoleRepository.Delete(userid, roleid);
        }

        public int GetUserCountByStrWhere(string strWhere) {
            int count = 0;
            DataTable dt = GetUserRoleDataTable(strWhere);
            if (dt.Rows.Count > 0) {
                count = dt.Rows.Count;
            }
            return count;
        }

        public DataTable GetUserPagerDataTable(string strWhere,string orderby,int pagesize,int pagenumber)
        {
            return this.userRepository.GetUserPagerDataTable(strWhere, orderby, pagesize, pagenumber);
        }
        #endregion
    }
}
