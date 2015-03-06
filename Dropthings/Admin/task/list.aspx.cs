using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dropthings.Data;
using Dropthings.Util;
using System.Text;
using System.Data;
using Dropthings.Business.Facade;

public partial class Admin_task_list : System.Web.UI.Page
{
    protected TASKEntity.TASKDAO Dao = new TASKEntity.TASKDAO();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PageDataBind();
        }
    }

    protected void PageDataBind()
    {
        /*string strWhere = string.Empty;
        if (!string.IsNullOrEmpty(keyword.Value))
        {
            strWhere = " task_name='" + keyword.Value + "'";
        }*/
        PagerList.RecordCount = Dao.GetPagerRowsCount("", null);
        DataTable dt = Dao.GetPager("", null, null, PagerList.PageSize, PagerList.CurrentPageIndex);
        if (dt != null)
        {
            dataList.DataSource = dt;
        }
        dataList.DataBind();

    }
    protected void PagerList_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        PagerList.CurrentPageIndex = e.NewPageIndex;
        PageDataBind();
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        PageDataBind();
    }
    protected void BtnLoadXml(object sender, EventArgs e)
    {
        //LoadXmlForCrawl.LoadXml("");
    }
}
