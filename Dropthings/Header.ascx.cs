#region Header

// Copyright (c) Omar AL Zabir. All rights reserved.
// For continued development and updates, visit http://msmvps.com/omar

#endregion Header

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Dropthings.Util;
using Dropthings.Business.Facade;

public partial class Header : System.Web.UI.UserControl
{
    #region Methods

    protected void Page_Load(object sender, EventArgs e)
    {
        var facade = Services.Get<Facade>();
        var userName = facade.Context.CurrentUserName;
        if (userName == string.Empty)//Context.Profile.IsAnonymous)
        {
            LoginLinkButton.Visible = true;
            LogoutLinkButton.Visible = false;
            //AccountLinkButton.Visible = false;
            HyperLinkManage.Visible = false;
            StartOverButton.Visible = true;
        }
        else
        {
            LoginLinkButton.Visible = false;
            LogoutLinkButton.Visible = true;
            StartOverButton.Visible = false;
            //AccountLinkButton.Visible = true;
            HyperLinkManage.Visible = true;
            UserNameLabel.Text = userName;
            UserNameLabel.Visible = true;
        }
    }

    #endregion Methods
}