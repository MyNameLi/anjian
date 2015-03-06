using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Dropthings.Widget.Framework;
using Dropthings.Util;

public partial class Widgets_Search_AdvancedSearchCondition : System.Web.UI.UserControl, IWidget
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        string[] scriptToLoad = { ResolveClientUrl("~/Scripts/jquery.query.js?v=" + ConstantHelper.ScriptVersionNo), 
                                    ResolveClientUrl("~/Scripts/jquery.flash.js?v=" + ConstantHelper.ScriptVersionNo), 
                                    ResolveClientUrl("~/Scripts/inputcue.js?v=" + ConstantHelper.ScriptVersionNo), 
                                    ResolveClientUrl("~/Scripts/Pager.js?v=" + ConstantHelper.ScriptVersionNo), 
                                    ResolveClientUrl("~/Scripts/My97DatePicker/WdatePicker.js?v=" + ConstantHelper.ScriptVersionNo), 
                                    ResolveClientUrl("~/Widgets/search/AdvancedSearch.js?v=" + ConstantHelper.ScriptVersionNo) };
        var startUpCode = string.Format("window.AdvancedSearchLoader{0}_{1} = advanceSearch ; window.AdvancedSearchLoader{0}_{1}.Load(); ",
                this._Host.ID, this.ClientID);

        WidgetHelper.RegisterWidgetScript(this, scriptToLoad, startUpCode);
    }

    private IWidgetHost _Host;

    #region IWidget Members

    void IWidget.Closed()
    {

    }

    void IWidget.Collasped()
    {

    }

    void IWidget.Expanded()
    {

    }

    void IWidget.HideSettings(bool userClicked)
    {

    }

    void IWidget.Init(IWidgetHost host)
    {
        this._Host = host;
    }

    void IWidget.Maximized()
    {

    }

    void IWidget.Restored()
    {

    }

    void IWidget.ShowSettings(bool userClicked)
    {

    }

    #endregion

    #region IEventListener Members

    void IEventListener.AcceptEvent(object sender, EventArgs e)
    {

    }

    #endregion
}
