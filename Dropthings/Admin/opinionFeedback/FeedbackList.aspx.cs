using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dropthings.Data;
using Dropthings.Business.Facade;
using System.Data;

public partial class Admin_opinionFeedback_FeedbackList : System.Web.UI.Page
{
    protected REPORTLISTEntity.REPORTLISTDAO Dao = new REPORTLISTEntity.REPORTLISTDAO();
    public string UserName { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        string switch_on = string.IsNullOrEmpty(Request["action"]) ? "" : Request["action"];
        if (!IsPostBack)
        {
            switch (switch_on)
            {
                case "del":
                    RefreshPage();
                    break;
                default:
                    PageDataBind();
                    break;
            }

        }
    }

    protected void PageDataBind()
    {
        //PagerList.PageSize = 20;
        UserName = UserFacade.GetUserName();
        //string SessionKey = ConfigurationManager.AppSettings["SessionKey"].ToString();
        string[] userinfo = UserFacade.GetUser();
        string queryCondition = "STATUS=3 AND TYPE=5";// string.Empty;
        if (userinfo != null && userinfo.Length > 0)
        {
            PagerList.RecordCount = Dao.GetPagerRowsCount(queryCondition, null);
            DataTable dt = Dao.GetPager(queryCondition, null, " CREATETIME DESC", PagerList.PageSize, PagerList.CurrentPageIndex);
            if (dt != null)
            {
                dataList.DataSource = dt;
            }
            dataList.DataBind();
        }

    }
    protected void RefreshPage()
    {
        PageDataBind();
    }
    protected void PagerList_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        PagerList.CurrentPageIndex = e.NewPageIndex;
        PageDataBind();
    }
}