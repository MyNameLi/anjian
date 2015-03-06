using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dropthings.Widget.Framework;
using Dropthings.Util;

public partial class Widgets_PublicCategory_NonColliery : System.Web.UI.UserControl, IWidget
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        string[] scriptToLoad = { ResolveClientUrl("~/Scripts/Pager.js?v=" + ConstantHelper.ScriptVersionNo), ResolveClientUrl("~/Widgets/PublicCategory/Colliery.js?v=" + ConstantHelper.ScriptVersionNo), ResolveClientUrl("~/Widgets/PublicCategory/NonColliery.js?v=" + ConstantHelper.ScriptVersionNo) };
        var startUpCode = string.Format("window.NonCollieryLoader{0} = NonColliery ; window.NonCollieryLoader{0}.Load(); ",
                this.ClientID);

        WidgetHelper.RegisterWidgetScript(this, scriptToLoad, startUpCode);
    }

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
