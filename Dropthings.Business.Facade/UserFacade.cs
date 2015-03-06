using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Dropthings.Data;
using System.Configuration;
using System.Web.SessionState;

namespace Dropthings.Business.Facade
{
    public class UserFacade : IRequiresSessionState
    {
        private static string SessionKey = ConfigurationManager.AppSettings["SessionKey"].ToString();
        public static string[] GetUser()
        {
            HttpCookie application = HttpContext.Current.Request.Cookies[SessionKey];
            if (application == null || string.IsNullOrEmpty(application.Value))
            {
                return null;
            }
            else
            {
                return application.Value.Split('|');
            }
        }

        public static string GetUserId()
        {
            string[] userinfo = GetUser();
            if (userinfo != null && userinfo.Length > 0)
            {
                return userinfo[0];
            }
            else
            {
                return null;
            }
        }
       
        public static string GetUserName()
        {
            string[] userinfo = GetUser();
            if (userinfo != null && userinfo.Length > 0)
            {
                return HttpUtility.UrlDecode(userinfo[1], Encoding.Default);
            }
            else
            {
                return null;
            }
        }

        public static string GetUserRoleList() {
            string[] userinfo = GetUser();
            if (userinfo != null && userinfo.Length > 0)
            {
                return userinfo[2];
            }
            else
            {
                return null;
            }
        }

        public static string GetUserRoleNameStr()
        {
            string[] userinfo = GetUser();
            if (userinfo != null && userinfo.Length > 0)
            {
                return userinfo[3];
            }
            else
            {
                return null;
            }
        }
    }    
}
