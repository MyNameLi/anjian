using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dropthings.Widget.Framework;
using System.Xml.Linq;
using Dropthings.Util;

public partial class Widgets_LatestNewsWidget : System.Web.UI.UserControl, IWidget
{
    #region Fields

    private IWidgetHost _Host;
    //private XElement _State;

    #endregion Fields
    //private XElement State
    //{
    //    get
    //    {
    //        if (_State == null)
    //        {
    //            string stateXml = this._Host.GetState();
    //            if (string.IsNullOrEmpty(stateXml))
    //            {
    //                //stateXml = "<state><type>MostPopular</type><tag /></state>";
    //                _State = new XElement("state",
    //                    new XElement("url", "MostPopular"),
    //                    new XElement("count", "3"));
    //            }
    //            else
    //            {
    //                _State = XElement.Parse(stateXml);
    //            }
    //        }
    //        return _State;
    //    }
    //}

    //public int Count
    //{
    //    get { return State.Element("count") == null ? 3 : int.Parse(State.Element("count").Value); }
    //    set
    //    {
    //        if (State.Element("count") == null)
    //            State.Add(new XElement("count", value));
    //        else
    //            State.Element("count").Value = value.ToString();
    //    }
    //}

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
        SettingsPanel.Visible = false;
        if (userClicked)
        {
            //this.Count = int.Parse(LatestNewsCountDropDownList.SelectedValue);
            this.SaveState();
        }
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
        SettingsPanel.Visible = true;
        if (userClicked)
        {
            LatestNewsCountDropDownList.SelectedIndex = -1;
            //LatestNewsCountDropDownList.Items.FindByText(this.Count.ToString()).Selected = true;
        }
    }

    #endregion

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        string[] scriptToLoad = { /*ResolveClientUrl("~/Scripts/hcmarquee.js?v=" + ConstantHelper.ScriptVersionNo), */ResolveClientUrl("~/Widgets/LatestNewsWidget.js?v=" + ConstantHelper.ScriptVersionNo) };
        //var startUpCode = string.Format("window.hotKeywordsLoader{0}_{1} = HotKeywords; window.hotKeywordsLoader{0}_{1}.Load();",
        var startUpCode = string.Format("window.latestNewsLoader{0} = LatestNewsWidget; window.latestNewsLoader{0}.container=\"{1}\"; window.latestNewsLoader{0}.load();",
                this._Host.ID, this.LatestNewsContainer.ClientID);

        WidgetHelper.RegisterWidgetScript(this, scriptToLoad, startUpCode);
    }

    protected void SaveSettings_Click(object sender, EventArgs e)
    {
        _Host.HideSettings(true);
    }

    private void SaveState()
    {
        //var xml = this.State.Xml();
        //this._Host.SaveState(xml);
        //this._State = null;
    }
    #region IEventListener 成员

    public void AcceptEvent(object sender, EventArgs e)
    {
    }

    #endregion
}