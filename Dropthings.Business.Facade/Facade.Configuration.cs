namespace Dropthings.Business.Facade
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;

    using Dropthings.Data;
    using Dropthings.Model;
    using System.Web.Security;
    using Dropthings.Configuration;
    using OmarALZabir.AspectF;
    using Dropthings.Util;

    partial class Facade
    {
        #region Methods

        public RoleTemplateEntity GetRoleTemplate(string templateUserName)
        {
            var userid = this.userRepository.GetUserIDFromUserName(templateUserName);
            var template = this.roleTemplateRepository.GetRoleTemplatesByUserId(userid);

            return template;
        }

        public bool CheckRoleTemplateIsRegisterUserTemplate(RoleTemplateEntity template)
        {
            //MembershipUser user = this.GetUser(template.AspNetUser.UserId);
            UsersEntity user = userRepository.GetUserByUserID(template.TEMPLATEUSERID.Value);
            
            UserTemplateSetting settingTemplate = GetUserSettingTemplate();

            return settingTemplate.RegisteredUserSettingTemplate.UserName.Equals(user.USERNAME);
        }

        public bool CheckRoleTemplateIsAnonymousUserTemplate(RoleTemplateEntity template)
        {
            //MembershipUser user = this.GetUser(template.AspNetUser.UserId);
            UsersEntity user = userRepository.GetUserByUserID(template.TEMPLATEUSERID.Value);

            UserTemplateSetting settingTemplate = GetUserSettingTemplate();

            return settingTemplate.AnonUserSettingTemplate.UserName.Equals(user.USERNAME);
        }

        public UserTemplateSetting GetUserSettingTemplate()
        {
            return AspectF.Define.Cache<UserTemplateSetting>(Services.Get<ICache>(), CacheKeys.TemplateKeys.UserTemplateSetting())
                .Return<UserTemplateSetting>(() =>
                    {
                        UserSettingTemplateSettingsSection settings = (UserSettingTemplateSettingsSection)ConfigurationManager.GetSection(UserSettingTemplateSettingsSection.SectionName);
                        var setting = new UserTemplateSetting
                        {
                            CloneAnonProfileEnabled = settings.CloneAnonProfileEnabled,
                            CloneRegisteredProfileEnabled = settings.CloneRegisteredProfileEnabled,
                            AnonUserSettingTemplate = settings.UserSettingTemplates[UserSettingTemplateSettingsSection.AnonTemplateKey],
                            RegisteredUserSettingTemplate = settings.UserSettingTemplates[UserSettingTemplateSettingsSection.RegTemplateKey],
                            AllUserSettingTemplate = new List<UserSettingTemplateElement>()
                        };

                        foreach (UserSettingTemplateElement element in settings.UserSettingTemplates)
                        {
                            setting.AllUserSettingTemplate.Add(element);
                        }

                        return setting;
                    });
        }

        #endregion Methods

    }
}