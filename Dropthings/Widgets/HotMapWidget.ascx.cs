using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dropthings.Widget.Framework;
using System.Xml.Linq;
using Dropthings.Util;

public partial class Widgets_HotMapWidget : System.Web.UI.UserControl, IWidget
{
    #region Fields

    private IWidgetHost _Host;
    //private XElement _State;

    #endregion Fields
    //public string JobName
    //{
    //    get { return State.Element("jobname").Value; }
    //    set { State.Element("jobname").Value = value; }
    //}

    //private XElement State
    //{
    //    get
    //    {
    //        string state = this._Host.GetState();
    //        if (string.IsNullOrEmpty(state))
    //            state = string.Format("<state><jobname>{0}</jobname></state>", ConstantHelper.HotJobName);
    //        if (_State == null) _State = XElement.Parse(state);
    //        return _State;
    //    }
    //}

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
        this._Host = host;
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        string[] scriptToLoad = { ResolveClientUrl("~/widgets/HotMapWidget.js?v=" + ConstantHelper.ScriptVersionNo) };
        //var startUpCode = string.Format("window.hotMapLoader{0} = new HotMapWidget('{1}','{2}'); window.hotMapLoader{0}.load();",
        //        this._Host.ID, this.HotMapContainer.ClientID, this.JobName);
        var startUpCode = string.Format("HotMapWidget.load('{0}','{1}');", this.HotMapContainer.ClientID, ConstantHelper.HotJobName);

        WidgetHelper.RegisterWidgetScript(this, scriptToLoad, startUpCode);
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
