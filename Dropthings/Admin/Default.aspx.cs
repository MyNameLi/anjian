using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dropthings.Util;
using Dropthings.Business.Facade;

public partial class Admin_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //string str = "CONNECTION TIMEOUT=60;DATA SOURCE=orcl;PERSIST SECURITY INFO=True;USER ID=GKADMIN;PASSWORD=$gkax$";
        //string desstr = DESEncrypt.Encrypt(str);
        //string dd = desstr;
        
    }

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        Page.Theme = null;
    }

    protected override void OnInit(EventArgs e)
    {
        string[] userinfo = UserFacade.GetUser();
        if (userinfo == null || userinfo.Length == 0)
        {
            Response.Redirect("~/LoginPage.aspx");
        }
        else
        {            
            base.OnInit(e);
            var facade = Services.Get<Facade>();
            facade.Context.CurrentUserId = UserFacade.GetUserId();
            facade.Context.CurrentUserName = UserFacade.GetUserName();
        }
    }
}
