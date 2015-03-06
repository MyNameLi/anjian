using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Workflow.Runtime;

using Dropthings.Business;
using Dropthings.Data;
using Dropthings.Web.Framework;
using Dropthings.Widget.Framework;

using Dropthings.Business.Facade;
using Dropthings.Model;
using Dropthings.Business.Facade.Context;
using Dropthings.Util;

public partial class WidgetTabHost : System.Web.UI.UserControl
{
    #region Fields

    //public event EventHandler OnReloadPage;
    private const string WIDGET_CONTAINER = "WidgetContainer.ascx";

    private EventBrokerService _EventBroker = new EventBrokerService();
    private Dictionary<string, Control> _WidgetControlMap = new Dictionary<string, Control>();
    private string[] updatePanelIDs = new string[] { "LeftUpdatePanel", "MiddleUpdatePanel", "RightUpdatePanel" };

    #endregion Fields

    #region Properties

    public PageEntity CurrentTab
    {
        get; set;
    }

    public List<WidgetInstanceEntity> WidgetInstances
    {
        get; set;
    }

    #endregion Properties

    #region Methods

    /// <summary>
    /// Since widgets are not following NamingContainer instead they are defining
    /// their ID on their own, we need to see if the Control ID passed to this function
    /// is either of an Widget or of a control inside a widget
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public override Control FindControl(string id)
    {
        var anyOf = new char[] { '$', ':' };
        int num = id.IndexOfAny(anyOf);

        // If this is the last control ID, no parent, then it should be of an Widget
        // or some control directly inside this control
        if (num == -1)
        {
            // See if it's a widget
            if (_WidgetControlMap.ContainsKey(id))
                return _WidgetControlMap[id];
            else
                return base.FindControl(id);
        }

        string firstControlId = id.Substring(0, num);
        if (_WidgetControlMap.ContainsKey(firstControlId))
        {
            var widgetControl = _WidgetControlMap[firstControlId];
            return widgetControl.FindControl(id.Substring(num+1));
        }
        else
        {
            return base.FindControl(id);
        }
    }

    public void LoadWidgets(PageEntity tab, string widgetContainerPath)
    {
        this.CurrentTab = tab;

        this.SetupWidgetZones(widgetContainerPath);
    }

    public void RefreshZone(int widgetZoneId)
    {
        var widgetZone = this.FindControl("WidgetZone" + widgetZoneId) as WidgetInstanceZone;
        if (widgetZone != null) widgetZone.Refresh();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private void SetupWidgetZones(string widgetContainerPath)
    {
        this.Controls.Clear();

        List<ColumnEntity> columns;
        var facade = Services.Get<Facade>();
        {
            columns = facade.GetColumnsInTab(CurrentTab.ID.Value);
        }

        // -- Workflow way. Obselete.
        //var columns = WorkflowHelper.Run<GetColumnsInPageWorkflow, GetColumnsInPageWorkflowRequest, GetColumnsInPageWorkflowResponse>(
        //        new GetColumnsInPageWorkflowRequest { PageId = this.CurrentPage.ID, UserName = Profile.UserName }
        //    ).Columns;

        _WidgetControlMap.Clear();
        columns.Each<ColumnEntity>( (column, index) =>
        {
            var widgetZone = LoadControl("~/WidgetInstanceZone.ascx") as WidgetInstanceZone;
            
            if (widgetZone != null)
            {
                widgetZone.ID = GetWidgetZoneControlId(column.WIDGETZONEID.Value);
                widgetZone.WidgetZoneId = column.WIDGETZONEID.Value;
                widgetZone.WidgetContainerPath = widgetContainerPath;
                widgetZone.WidgetZoneClass = "widget_zone";
                widgetZone.WidgetClass = "widget";
                widgetZone.NewWidgetClass = "new_widget";
                widgetZone.HandleClass = "widget_header";
                widgetZone.UserName = Profile.UserName;
                widgetZone.IsLocked = this.CurrentTab.ISLOCKED.Value && !this.CurrentTab.ISDOWNFORMAINTENANCE.Value;
            }

            var panel = new Panel {ID = "WidgetZonePanel" + index, CssClass = "column"};
            panel.Style.Add(HtmlTextWriterStyle.Width, column.COLUMNWIDTH.Value.Percent());

            this.Controls.Add(panel);
            panel.Controls.Add(widgetZone);

            if (widgetZone != null)
            {
                var widgets = widgetZone.LoadWidgets(this._EventBroker);
                foreach (IWidgetHost widgetHost in widgets)
                {
                    var c = widgetHost as Control;
                    if (c != null) _WidgetControlMap.Add(c.UniqueID, c);
                }
            }
        });
    }

    private string GetWidgetZoneControlId(int widgetZoneId)
    {
        return WidgetInstanceZone.WIDGET_ZONE_ID_PREFIX + widgetZoneId;
    }

    public void AddNewWidget(WidgetInstanceEntity wi)
    {
        var widgetZoneControl = this.FindControl(GetWidgetZoneControlId(wi.WIDGETZONEID.Value)) as WidgetInstanceZone;
        widgetZoneControl.AddNewWidget(_EventBroker, wi);       
    }

    #endregion Methods
}