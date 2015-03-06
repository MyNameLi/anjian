using System.Web.Security;

namespace Dropthings.Business.Facade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Dropthings.Data;
    using Dropthings.Model;
    using OmarALZabir.AspectF;
    using Dropthings.Util;

    partial class Facade
    {
        #region Constants
        private const string DEFAULT_FIRST_PAGE_NAME = "New Tab";
        #endregion

        #region Methods

        public UserSetup FirstVisitHomeTab(string userName, string pageTitle, bool isAnonymous, bool isFirstVisitAfterLogin)
        {
            return AspectF.Define
                //.Transaction()
                .Return<UserSetup>(() =>
            {
                // If user does not exist, then this is the very *FIRST VISIT* of the user and user
                // Get template setting that so that we can create pages from templates
                var response = new UserSetup();
                var userGuid = this.GetUserIDFromUserName(userName);

                //var userSettingTemplate = GetUserSettingTemplate();
                //SetUserRoles(userName, new string[] { userSettingTemplate.AnonUserSettingTemplate.RoleNames });

                //if (userSettingTemplate.CloneAnonProfileEnabled)
                //{
                //    // Get the template user so that its page setup can be cloned for new user
                //    //var templateUserGuid = this.GetUserGuidFromUserName(userSettingTemplate.AnonUserSettingTemplate.UserName);
                //    var roleTemplate = GetRoleTemplate(userSettingTemplate.AnonUserSettingTemplate.UserName);

                //    if (roleTemplate.ID != default(int?))
                //    {
                //        // Get template user pages so that it can be cloned for new user
                //        var templateUserTabs = this.pageRepository.GetTabsOfUser(roleTemplate.TEMPLATEUSERID.Value);

                //        foreach (PageEntity templateTab in templateUserTabs)
                //        {
                //            if (!templateTab.ISLOCKED.Value)
                //            {
                //                CloneTab(userGuid, templateTab);
                //            }
                //        }

                //        // If it's not the same user as the template user, then show the tabs 
                //        // from template user as read-only tabs.
                //        if (roleTemplate.TEMPLATEUSERID != userGuid)
                //        {
                //            response.UserSharedTabs = this.pageRepository.GetLockedTabsOfUser(roleTemplate.TEMPLATEUSERID.Value, false);
                //        }

                //        response.IsTemplateUser = (roleTemplate.TEMPLATEUSERID.Value == userGuid);
                //        //response.RoleTemplate = roleTemplate;
                //    }
                //}
                //else
                //{
                // Setup some default pages

                var pages = this.pageRepository.GetTabsOfUser(userGuid);

                if (pages.IsEmpty())
                {
                    var page = CreateTab(userGuid, pageTitle, 0, 0);

                    if (page != null && page.ID > 0)
                    {
                        CreateDefaultWidgetsOnTab(userName, page.ID.Value);
                        RepeatVisitHomeTab(userName, page.TITLE, isAnonymous, isFirstVisitAfterLogin);    // non-recursive. this will hit the outter most else block
                    }
                    else
                    {
                        throw new ApplicationException("First page creation failed");
                    }
                    //}

                    response.UserTabs = this.pageRepository.GetTabsOfUser(userGuid);
                    response.UserSetting = GetUserSetting(userGuid);
                    response.CurrentTab = DecideCurrentTab(userGuid, page.TITLE, response.UserTabs, response.UserSharedTabs);
                    response.CurrentUserId = userGuid;
                }
                else
                {
                    response = RepeatVisitHomeTab(userName, "", isAnonymous, isFirstVisitAfterLogin);    // non-recursive. this will hit the outter most else block
                }
                return response;
            });
        }

        public UserSetup RepeatVisitHomeTab(string userName, string pageTitle, bool isAnonymous, bool isFirstVisitAfterLogin)
        {
            return AspectF.Define
                //.Transaction()
                .Return<UserSetup>(() =>
            {
                // User is visiting again, so load user's existing page setup
                List<PageEntity> pages = new List<PageEntity>();
                var response = new UserSetup();
                var userGuid = this.GetUserIDFromUserName(userName);
                var personPages = this.pageRepository.GetTabsOfUser(userGuid);
                if (personPages != null && personPages.Count > 0)
                {
                    pages.AddRange(personPages);
                }
                string roleid = UserFacade.GetUserRoleList();
                var commonPages = this.pageRepository.GetPagesByRoleId(roleid);
                if (commonPages != null && commonPages.Count > 0) {
                    pages.AddRange(commonPages);
                }
               
                if (!pages.IsEmpty())
                {
                    // User has pages
                    response.UserTabs = pages;
                    response.UserSharedTabs = this.GetSharedTabs(userName);

                    var userSetting = GetUserSetting(userGuid);
                    /*get currenttab*/
                    //string currentPageTitle = "首页";
                    response.CurrentTab = DecideCurrentTab(userGuid, pageTitle, response.UserTabs, response.UserSharedTabs);

                    if (userSetting.CURRENTPAGEID.Value != response.CurrentTab.ID.Value)
                    {
                        SetCurrentTab(userGuid, response.CurrentTab.ID.Value);
                    }

                    response.UserSetting = GetUserSetting(userGuid);
                    response.CurrentUserId = userGuid;

                    var templateSetup = this.GetUserSettingTemplate();
                    response.IsTemplateUser = templateSetup.AnonUserSettingTemplate.UserName.IsSameAs(userName)
                        || templateSetup.RegisteredUserSettingTemplate.UserName.IsSameAs(userName);
                }
                else
                {
                    // User has no pages
                    response = FirstVisitHomeTab(userName, pageTitle, isAnonymous, isFirstVisitAfterLogin);
                }

                return response;
            });
        }

        #endregion Methods
    }
}