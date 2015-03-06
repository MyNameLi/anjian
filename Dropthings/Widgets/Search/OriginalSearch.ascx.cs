using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dropthings.Widget.Framework;
using Dropthings.Util;

public partial class Widgets_Search_OriginalSearch : System.Web.UI.UserControl, IWidget
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        string[] scriptToLoad = { "https://www.google.com/jsapi", ResolveClientUrl("~/Widgets/Search/OriginalSearch.js?v=" + ConstantHelper.ScriptVersionNo) };
        var startUpCode = string.Format("window.OriginalSearchLoader{0}_{1} = OriginalSearch ; window.OriginalSearchLoader{0}_{1}.Load('{2}'); ",
                this._Host.ID, this.ClientID, ResolveClientUrl("~/Widgets/Search/default.css"));

        WidgetHelper.RegisterWidgetScript(this, scriptToLoad, startUpCode);
    }

    private IWidgetHost _Host;

    #region IWidget Members

    public void Closed()
    {

    }

    public void Collasped()
    {

    }

    public void Expanded()
    {

    }

    public void HideSettings(bool userClicked)
    {

    }

    public new void Init(IWidgetHost host)
    {
        _Host = host;
    }

    public void Maximized()
    {

    }

    public void Restored()
    {

    }

    public void ShowSettings(bool userClicked)
    {

    }

    #endregion

    #region IEventListener Members

    public void AcceptEvent(object sender, EventArgs e)
    {

    }

    #endregion
}
