using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dropthings.Business.Facade;
using Dropthings.Util;
using OmarALZabir.AspectF;
using Dropthings.Web.Util;

public partial class statistic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected override void OnInit(EventArgs e)
    {
        string[] userinfo = UserFacade.GetUser();
        if (userinfo == null || userinfo.Length == 0)
        {
            Response.Redirect("~/LoginPage.aspx");
            return;
        }

        base.OnInit(e);
        var facade = Services.Get<Facade>();
        facade.Context.CurrentUserId = UserFacade.GetUserId();
        facade.Context.CurrentUserName = UserFacade.GetUserName();
        AspectF.Define
            .Retry(errors => errors.ToArray().Each(x => Response.Write(x.ToString())), Services.Get<ILogger>())
            //.Log(Services.Get<ILogger>(), "OnInit: " + Profile.UserName)
            .Do(() =>
            {

                // Check if revisit is valid or not
                if (!base.IsPostBack)
                {
                    // Block cookie less visit attempts
                    //if (Profile.IsFirstVisit)
                    //{
                    //    if (!ActionValidator.IsValid(ActionValidator.ActionTypeEnum.FirstVisit)) Response.End();
                    //}
                    //else
                    //{
                    //    if (!ActionValidator.IsValid(ActionValidator.ActionTypeEnum.Revisit)) Response.End();
                    //}
                    //2010-12-22 张涛添加
                    this.Response.Cookies.Add(new HttpCookie("user_login", "admin"));
                }
                else
                {
                    // Limit number of postbacks
                    if (!ActionValidator.IsValid(ActionValidator.ActionTypeEnum.Postback)) Response.End();
                }
            });
    }

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        Page.Theme = null;
    }
}
