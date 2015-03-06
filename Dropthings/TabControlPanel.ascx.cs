using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dropthings.Util;
using Dropthings.Business.Facade;
using OmarALZabir.AspectF;
using Dropthings.Data;

public partial class TabControlPanel : System.Web.UI.UserControl
{

    private int AddStuffTabIndex
    {
        get { object val = ViewState["AddStuffTabIndex"]; if (val == null) return 0; else return (int)val; }
        set { ViewState["AddStuffTabIndex"] = value; }
    }

    public PageEntity CurrentTab { get; set; }
    public bool IsTemplateUser { get; set; }
    public bool IsOnlyTab { get; set; }
    public bool IsOwner { get; set; }

    public delegate void NewWidgetAddedDelegate(WidgetInstanceEntity wi);
    private NewWidgetAddedDelegate _OnNewWidgetCallback;

    protected void Tab_Load(object sender, EventArgs e)
    {

    }

    public void InitTabs(PageEntity currentTab, bool isTemplateUser, bool isOnlyTab, bool isOwner, NewWidgetAddedDelegate onNewWidget)
    {
        this.IsTemplateUser = isTemplateUser;
        this.IsOnlyTab = isOnlyTab;
        this.IsOwner = isOwner;
        this.CurrentTab = currentTab;
        this._OnNewWidgetCallback = onNewWidget;

        this.LoadAddStuff();
        this.LockTabContent();
        this.LoadOptionsForTemplateUser();
    }
    #region guotongyu 2011-11-25 update
    //protected void SaveNewTitleButton_Clicked(object sender, EventArgs e)
    //{
    //    var newTitle = this.NewTitleTextBox.Text.Trim();

    //    if (newTitle != this.CurrentTab.TITLE)
    //    {
    //        var facade = Services.Get<Facade>();
    //        {
    //            facade.ChangeTabName(newTitle);
    //        }

    //        ReloadTab();
    //    }
    //}
    #endregion
    protected void SaveTabLockSettingButton_Clicked(object sender, EventArgs e)
    {
        var isLocked = this.TabLocked.Checked;

        if (isLocked != this.CurrentTab.ISLOCKED)
        {
            var facade = Services.Get<Facade>();
            {
                if (isLocked)
                {
                    facade.LockTab();
                }
                else
                {
                    facade.UnLockTab();
                }
            }

            ReloadTab();
        }
    }

    protected void SaveTabMaintenenceSettingButton_Clicked(object sender, EventArgs e)
    {
        var isInMaintenenceModeLocked = this.TabMaintanance.Checked;

        if (isInMaintenenceModeLocked != this.CurrentTab.ISDOWNFORMAINTENANCE)
        {
            var facade = Services.Get<Facade>();
            {
                facade.ChangeTabMaintenenceStatus(isInMaintenenceModeLocked);
            }

            ReloadTab();
        }
    }

    protected void SaveTabServeAsStartPageSettingButton_Clicked(object sender, EventArgs e)
    {
        var shouldServeAsStartTab = this.TabServeAsStartPage.Checked;

        if (shouldServeAsStartTab != this.CurrentTab.SERVEASSTARTPAGEAFTERLOGIN.GetValueOrDefault())
        {
            var facade = Services.Get<Facade>();
            {
                facade.ChangeServeAsStartPageAfterLoginStatus(shouldServeAsStartTab);
            }

            ReloadTab();
        }
    }

    protected void ShowAddContentPanel_Click(object sender, EventArgs e)
    {
        this.AddContentPanel.Visible = true;
        this.HideAddContentPanel.Visible = true;
        this.ShowAddContentPanel.Visible = false;

        this.LoadAddStuff();
    }

    private void HideChangeSettingsPanel()
    {
        this.HideChangeTabTitle.Visible = false;
        this.ShowChangeTabTitle.Visible = true;

        this.ChangeTabSettingsPanel.Visible = false;
        this.ShowChangeTabTitle.Text = (String)GetGlobalResourceObject("SharedResources", "ChangeSettings");
    }
    
    private void ShowChangeSettingsPanel()
    {
        //if tab counts 1 or less deleting disabled

        #region guotongyu 2011-11-25 update
        //if (this.IsOnlyTab)       
        //    this.DeleteTabLinkButton.Enabled = false;
        #endregion
        this.ChangeTabSettingsPanel.Visible = true;
        //this.ShowChangeTabTitle.Text = (String)GetGlobalResourceObject("SharedResources", "HideSettings");
        this.HideChangeTabTitle.Visible = true;
        this.ShowChangeTabTitle.Visible = false;

        #region guotongyu 2011-11-25 update
        // this.NewTitleTextBox.Text = this.CurrentTab.TITLE;
        #endregion
        this.TabLocked.Checked = this.CurrentTab.ISLOCKED.Value;

        //show options for the maintenence mode
        this.maintenenceOption.Visible = this.CurrentTab.ISLOCKED.Value;
        this.TabMaintanance.Checked = this.CurrentTab.ISDOWNFORMAINTENANCE.Value;

        // TODO: Find out what this code does and see if there's a better way to do this
        //this.serveAsStartTabOption.Visible = this.CurrentTab.IsLocked && _Setup.IsRoleTemplateForRegisterUser;

        this.TabServeAsStartPage.Checked = this.CurrentTab.SERVEASSTARTPAGEAFTERLOGIN.GetValueOrDefault();
    }

    private void LockTabContent()
    {
        if (this.IsOwner)
        {
            ShowAddContentPanel.Enabled = HideAddContentPanel.Enabled = ShowChangeTabTitle.Enabled = !this.CurrentTab.ISLOCKED.Value;
        }
    }


    protected void Layout1_Clicked(object sender, EventArgs e)
    {
        Services.Get<Facade>().ModifyTabLayout(1);
        ReloadTab();
    }
    protected void Layout2_Clicked(object sender, EventArgs e)
    {
        Services.Get<Facade>().ModifyTabLayout(2);
        ReloadTab();
    }
    protected void Layout3_Clicked(object sender, EventArgs e)
    {
        Services.Get<Facade>().ModifyTabLayout(3);
        ReloadTab();
    }
    protected void Layout4_Clicked(object sender, EventArgs e)
    {
        Services.Get<Facade>().ModifyTabLayout(4);
        ReloadTab();
    }
    protected void Layout5_Clicked(object sender, EventArgs e)
    {
        Services.Get<Facade>().ModifyTabLayout(5);
        ReloadTab();
    }

    private void ReloadTab()
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void LoadAddStuff()
    {
        this.WidgetListControlAdd.LoadWidgetList(newWidget =>
        {
            _OnNewWidgetCallback(newWidget);
        });

        var facade = Services.Get<Facade>();
        {
            this.WidgetListControlAdd.InstanceWidgetList = facade.GetInstanceWidgetsByTabId(this.CurrentTab.ID.Value); //new List<WidgetInstance>();
        }
    }

    private void LoadOptionsForTemplateUser()
    {
        if (IsTemplateUser)
        {
            pnlTemplateUserSettings.Visible = true;
        }
    }

    protected void ChangeTabSettingsLinkButton_Clicked(object sender, EventArgs e)
    {
        var lb = sender as LinkButton;
        if (null != lb)
        {
            var command = lb.CommandName;
            if (command == "hide")
                this.HideChangeSettingsPanel();
            else
                this.ShowChangeSettingsPanel();
            //if (this.ChangeTabSettingsPanel.Visible)
            //    this.HideChangeSettingsPanel();
            //else
            //    this.ShowChangeSettingsPanel();
        }
    }
    #region guotongyu 2011-11-25 update
    //protected void DeleteTabLinkButton_Clicked(object sender, EventArgs e)
    //{
    //    AspectF.Define.TrapLogThrow(Services.Get<ILogger>()).Do(() =>
    //    {
    //        var facade = Services.Get<Facade>();
    //        {
    //            var newCurrentTab = facade.DeleteTab(this.CurrentTab.ID.Value);
    //            RedirectToTab(newCurrentTab);
    //        }
    //    });
    //}
    #endregion
    public void RedirectToTab(PageEntity Tab)
    {
        if (!Tab.ISLOCKED.Value)
        {
            Response.Redirect("Default.aspx?" + Tab.UserTabName);
        }
        else
        {
            Response.Redirect("Default.aspx?" + Tab.LockedTabName);
        }
    }

    protected void HideAddContentPanel_Click(object sender, EventArgs e)
    {
        this.AddContentPanel.Visible = false;
        this.HideAddContentPanel.Visible = false;
        this.ShowAddContentPanel.Visible = true;
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (this.AddContentPanel.Visible)
            ScriptManager.RegisterStartupScript(this.AddContentPanel, typeof(Panel), "ShowAddContentPanel" + DateTime.Now.Ticks.ToString(),
                "DropthingsUI.showWidgetGallery();", true);
    }

}