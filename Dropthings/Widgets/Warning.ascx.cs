using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dropthings.Widget.Framework;
using Dropthings.Util;

public partial class Widgets_Warning : System.Web.UI.UserControl,IWidget
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private IWidgetHost _Host;

    #region IWidget 成员

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
        this._Host = host;
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

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        string[] scriptToLoad = { ResolveClientUrl("~/Scripts/SqlPager.js?v=" + ConstantHelper.ScriptVersionNo), ResolveClientUrl("~/Widgets/WaringWidget.js?v=" + ConstantHelper.ScriptVersionNo) };
        var startUpCode = string.Format("window.WaringLoader_{0} = Warning ; window.WaringLoader_{0}.Load();",       
        this.ClientID);

        WidgetHelper.RegisterWidgetScript(this, scriptToLoad, startUpCode);
    }

    #endregion

    #region IEventListener 成员

    public void AcceptEvent(object sender, EventArgs e)
    {
        
    }

    #endregion
}
