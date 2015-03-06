using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Workflow.Runtime;

using Dropthings.Business;
using Dropthings.Data;
using Dropthings;
using Dropthings.Web.Framework;
using Dropthings.Web.Util;

using Dropthings.Business.Facade;
using Dropthings.Model;
using Dropthings.Business.Facade.Context;
using Dropthings.Util;

public partial class WidgetListControl : System.Web.UI.UserControl
{
    #region Fields

    private WidgetPanelRefreshHandler widgetRefreshCallback;

    #endregion Fields

    #region Delegates

    public delegate void WidgetPanelRefreshHandler(WidgetInstanceEntity WI);

    #endregion Delegates

    #region Properties

    private int AddStuffPageIndex
    {
        get { object val = ViewState["AddStuffPageIndex"]; if (val == null) return 0; else return (int)val; }
        set { ViewState["AddStuffPageIndex"] = value; }
    }

    private List<WidgetEntity> WidgetList
    {
        get
        {
            //if (Roles.Enabled && Convert.ToBoolean(ConstantHelper.EnableWidgetPermission, CultureInfo.InvariantCulture))
            //{
            //    var facade = Services.Get<Facade>();
            //    return facade.GetWidgetList(Profile.UserName, Enumerations.WidgetType.PersonalTab);
            //}
            //else
            {
                var facade = Services.Get<Facade>();
                return facade.GetWidgetList(facade.Context.CurrentUserName, Enumerations.WidgetType.PersonalTab).OrderBy(wi => wi.NAME).ToList();
            }
        }
    }

    public List<WidgetEntity> InstanceWidgetList
    {
        get;
        set;
    }

    #endregion Properties

    #region Methods

    public void LoadWidgetList(WidgetPanelRefreshHandler widgetAddedCallback)
    {
        this.LoadWidgetList();

        widgetRefreshCallback = widgetAddedCallback;
    }

    protected override void OnPreRender(EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(WidgetListControl), this.ID + "_initAddStuff",
            (Page.IsPostBack ? "" : "jQuery(document).ready(function() {")
                + "DropthingsUI.initAddStuff('widget_zone', 'new_widget');"
                + (Page.IsPostBack ? "" : "});")
                , true);

        base.OnPreRender(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void WidgetListNextButton_Click(object sender, EventArgs e)
    {
        AddStuffPageIndex++;
        this.LoadWidgetList();
    }

    protected void WidgetListPreviousLinkButton_Click(object sender, EventArgs e)
    {
        if (0 == AddStuffPageIndex) return;
        AddStuffPageIndex--;

        this.LoadWidgetList();
    }

    private void LoadWidgetList()
    {
        this.WidgetDataList.ItemCommand += new DataListCommandEventHandler(WidgetDataList_ItemCommand);

        var itemsToShow = WidgetList.Skip(AddStuffPageIndex * 30).Take(30).ToList();
        this.WidgetDataList.DataSource = itemsToShow;
        this.WidgetDataList.DataBind();

        WidgetListPreviousLinkButton.Visible = AddStuffPageIndex > 0;
        WidgetListNextButton.Visible = AddStuffPageIndex * 30 + 30 < WidgetList.Count;
    }

    void WidgetDataList_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (!ActionValidator.IsValid(ActionValidator.ActionTypeEnum.AddNewWidget)) return;

        int widgetId = int.Parse(e.CommandArgument.ToString());

        //DashboardFacade facade = new DashboardFacade(HttpContext.Current.Profile.UserName);

        //User added a new widget. The new widget is loaded for the first time. So, it's not
        //a postback experience for the widget. But for rest of the widgets, it is a postback experience.
        WidgetInstanceEntity widgetInstance;
        var facade = Services.Get<Facade>();
        {
            if (null != InstanceWidgetList)
            {
                bool isExists = false;
                foreach (WidgetEntity w in InstanceWidgetList)
                {
                    if (w.ID == widgetId)
                    {
                        isExists = true;
                        break;
                    }
                }
                if (!isExists)
                {
                    widgetInstance = facade.AddWidgetInstance(widgetId, 0, 0, 0);
                    widgetRefreshCallback(widgetInstance);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "exists tip", "alert('该模块已经存在！');", true);
                }
            }
        }

        // -- Workflow way. Obselete.
        //var response = WorkflowHelper.Run<AddWidgetWorkflow,AddWidgetRequest,AddWidgetResponse>(
        //                   new AddWidgetRequest { WidgetId = widgetId, RowNo = 0, ColumnNo = 0, ZoneId = 0, UserName = Profile.UserName }
        //               );

        //widgetRefreshCallback(response.WidgetInstanceAffected);
    }

    #endregion Methods
}