namespace Dropthings.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Dropthings.Data;

    public class UserSetup
    {
        #region Properties

        public PageEntity CurrentTab
        {
            get;
            set;
        }

        public List<PageEntity> UserTabs
        {
            get;
            set;
        }

        public List<PageEntity> UserSharedTabs
        {
            get;
            set;
        }

        public UserSettingEntity UserSetting
        {
            get;
            set;
        }

        public int CurrentUserId
        {
            get;
            set;
        }

        //public RoleTemplate RoleTemplate
        //{
        //    get; 
        //    set;
        //}

        //public bool IsRoleTemplateForRegisterUser
        //{
        //    get;
        //    set;
        //}

        #endregion Properties

        public bool IsTemplateUser { get; set; }
    }
}