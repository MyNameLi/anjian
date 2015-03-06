#region Header

// Copyright (c) Omar AL Zabir. All rights reserved.
// For continued development and updates, visit http://msmvps.com/omar

#endregion Header

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Workflow.Runtime;

using Dropthings.Business;
using Dropthings.Web.Framework;
using Dropthings.Web.Util;
using Dropthings.Business.Facade;
using Dropthings.Model;
using Dropthings.Business.Facade.Context;
using Dropthings.Util;
using Dropthings.Data;
using System.Text;

public partial class LoginPage : System.Web.UI.Page
{
    #region Methods

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        string checkCode = WebUtil.GetStringFromSession("CheckCode");
        if (checkCode.ToLower() != this.VerifyCode.Text.Trim().ToLower())
        {
            WebUtil.ShowMessage(this.LabelError, "您输入的验证码错误", true);
            this.LabelError.Visible = true;
            //MessageBox.Show(this.Page, "您输入的验证码错误！");
        }
        else
        {
            var facade = Services.Get<Facade>();
            var user = facade.ValidateUser(Email.Text, Password.Text);
            if (user != null)
            {
                //var profile = Profile.GetProfile(Email.Text);
                //profile.IsFirstVisitAfterLogin = true;
                //profile.Save();           

                string SessionKey = ConfigurationManager.AppSettings["SessionKey"].ToString();
                string userid = user.USERID.ToString();
                //Byte[] gb2 = Encoding.UTF8.GetBytes(user.USERNAME);
                //string  HttpUtility.UrlEncode(user.USERNAME, Encoding.Default);
                string userName = HttpUtility.UrlEncode(user.USERNAME, Encoding.Default);
                string RoleIdStr = facade.GetUserRoleIdList(user.USERID.Value);
                string RoleNameStr = facade.GetUserRoleIdList(user.USERID.Value);
                HttpCookie cookie = new HttpCookie(SessionKey);
                cookie.Value = userid + "|" + userName + "|" + RoleIdStr + "|" + RoleNameStr;
                cookie.Expires = DateTime.Now.AddDays(10);
                HttpContext.Current.Response.SetCookie(cookie);
                facade.Context.CurrentUserId = user.USERID.ToString();
                facade.Context.CurrentUserName = user.USERNAME;
                //AppContext.Current.CurrentUserId = user.USERID.ToString();
                //AppContext.Current.CurrentUserName = user.USERNAME.ToString();

                //string pageTitle                                                                            = Server.UrlDecode((string.IsNullOrEmpty(Request.Url.Query) ? Resources.SharedResources.NewTabTitle : Request.Url.Query).TrimStart('?').Split('&')[0]);
                //string pageTitle = Server.UrlDecode((string.IsNullOrEmpty(Request.Url.Query) ? "" : Request.Url.Query).TrimStart('?').Split('&')[0]);
                string pageTitle = Server.UrlDecode((string.IsNullOrEmpty(Request.Url.Query) ? Resources.SharedResources.NewTabTitle : Request.Url.Query).TrimStart('?').Split('&')[0]);
                facade.FirstVisitHomeTab(user.USERNAME, pageTitle, false, true);
                Response.Redirect("~/Admin/Default.aspx");
                //FormsAuthentication.RedirectFromLoginPage(Email.Text, RememberMeCheckbox.Checked);
            }
            else
            {
                WebUtil.ShowMessage(this.LabelError, "您输入的用户名或密码错误", true);
                this.LabelError.Visible = true;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        //{
        //    var facade = Services.Get<Facade>();

        //    if (!string.IsNullOrEmpty(facade.Context.CurrentUserName))
        //    {
        //        Response.Redirect("Default.aspx");
        //    }
        //}
    }

    #endregion Methods
}