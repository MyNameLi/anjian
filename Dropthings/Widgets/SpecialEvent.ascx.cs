using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dropthings.Widget.Framework;
using Dropthings.Util;

public partial class Widgets_SpecialEvent : System.Web.UI.UserControl, IWidget
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private IWidgetHost _Host;

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        string[] scriptToLoad = { ResolveClientUrl("~/Widgets/SpecialEvent.js?v=" + ConstantHelper.ScriptVersionNo) };
        var startUpCode = string.Format("window.SepcialEventLoader{0}_{1} = SepcialEvent ; window.SepcialEventLoader{0}_{1}.Load(); ",
                this._Host.ID, this.ClientID);

        WidgetHelper.RegisterWidgetScript(this, scriptToLoad, startUpCode);
    }

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
        _Host = host;
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
