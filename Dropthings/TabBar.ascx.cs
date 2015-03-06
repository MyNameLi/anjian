using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Workflow.Runtime;

using Dropthings.Business;
using Dropthings.Data;
using Dropthings.Web.Framework;
using Dropthings.Business.Facade;
using Dropthings.Business.Facade.Context;

using Dropthings.Util;

public partial class TabBar : System.Web.UI.UserControl
{
    #region Fields

    private const string WIDGET_CONTAINER = "WidgetContainer.ascx";

    private string[] updatePanelIDs = new string[] { "LeftUpdatePanel", "MiddleUpdatePanel", "RightUpdatePanel" };

    #endregion Fields

    #region Properties

    public PageEntity CurrentTab
    {
        get;
        set;
    }

    public IEnumerable<PageEntity> Pages
    {
        get;
        set;
    }

    public List<PageEntity> LockedTabs
    {
        get;
        set;
    }

    public int CurrentUserId
    {
        get;
        set;
    }

    public bool IsTemplateUser { get; set; }

    public bool EnableTabSorting
    {
        get
        {
            if (!ConstantHelper.EnableTabSorting && IsTemplateUser)
            {
                return ConstantHelper.EnableTabSorting;
            }

            return ConstantHelper.EnableTabSorting;
        }
    }


    #endregion Properties

    #region Methods

    public void LoadTabs(int currentUserId, IEnumerable<PageEntity> tabs, List<PageEntity> sharedTabs, PageEntity tab, bool isTemplateUser)
    {
        this.IsTemplateUser = isTemplateUser;
        this.CurrentTab = tab;
        this.Pages = tabs;
        this.LockedTabs = sharedTabs;
        this.CurrentUserId = currentUserId;
        this.SetupTabs();
    }

    public void RedirectToTab(PageEntity page)
    {
        if (!page.ISLOCKED.Value || CurrentUserId == page.USERID.Value)
        {
            Response.Redirect("Default.aspx?" + Server.UrlEncode(page.UserTabName));
        }
        else
        {
            Response.Redirect("Default.aspx?" + Server.UrlEncode(page.LockedTabName));
        }
    }

    protected override void CreateChildControls()
    {
        base.CreateChildControls();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void addNewTabLinkButton_Click(object sender, EventArgs e)
    {
        // -- Workflow way. Obselete.
        //var response = WorkflowHelper.Run<AddNewTabWorkflow, AddNewTabWorkflowRequest, AddNewTabWorkflowResponse>(
        //            new AddNewTabWorkflowRequest { LayoutType = "1", UserName = Profile.UserName }
        //        );

        //RedirectToTab(response.NewPage);

        var facade = Services.Get<Facade>();
        {
            var page = facade.CreateTab(Resources.SharedResources.NewTabTitle, 2);
            RedirectToTab(page);
        }
    }

    private void SetupTabs()
    {
        tabList.Controls.Clear();
        var facade = Services.Get<Facade>();

        // True if the currently logged in user is the user defined in web.config
        // as the template user for the registered user template or anonymous user template
        //var templateSetting = facade.GetUserSettingTemplate();
        var isTemplateUser = false;// !Profile.IsAnonymous
            //&& (Profile.UserName.IsSameAs(templateSetting.RegisteredUserSettingTemplate.UserName)
            //|| Profile.UserName.IsSameAs(templateSetting.AnonUserSettingTemplate.UserName));

        var viewablePages = this.Pages.ToList();

        if (this.LockedTabs != null)
        {
            viewablePages.AddRange(this.LockedTabs);
            viewablePages = viewablePages.OrderBy(o => o.ORDERNO.GetValueOrDefault()).ToList();
        }

        foreach (var page in viewablePages)
        {
            var li = new HtmlGenericControl("li");
            li.ID = "Tab" + page.ID.ToString();
            li.Attributes["class"] = "tab " + (page.ID == CurrentTab.ID ? "activetab" : "inactivetab") +
                                     (page.ISLOCKED.Value && !isTemplateUser ? " nodrag" : string.Empty);

            /* var liWrapper = new HtmlGenericControl("div");
             li.Controls.Add(liWrapper);
             liWrapper.Attributes["class"] = "tab_wrapper";*/

            if (page.ID == CurrentTab.ID)
            {
                var tabTextDiv = new HtmlGenericControl("span");
                tabTextDiv.Attributes["class"] = "current_tab";
                tabTextDiv.InnerHtml = "<b>" + page.TITLE + "</b>";
                li.Controls.Add(tabTextDiv);
            }
            else
            {
                var url = "?";
                if (!page.ISLOCKED.Value || CurrentUserId.ToString() == facade.Context.CurrentUserId)
                {
                    url += Server.UrlEncode(page.UserTabName);
                }
                else
                {
                    url += Server.UrlEncode(page.LockedTabName);
                }
                string href = url;
                if (page.USERID == -1) {
                    href = page.URL;
                }
                var tabLink = new HyperLink { Text = "<b>" + page.TITLE + "</b>", NavigateUrl = href };
                li.Controls.Add(tabLink);
            }
            tabList.Controls.Add(li);
        }

        var addNewTabLinkButton = new LinkButton();
        addNewTabLinkButton.ID = "AddNewPage";
        addNewTabLinkButton.CssClass = "newtab_add newtab_add_block";
        addNewTabLinkButton.Click += new EventHandler(addNewTabLinkButton_Click);
        #region 2011-11-21 郭同禹 禁用自动添加选项卡
        //var li2 = new HtmlGenericControl("li");
        //li2.Attributes["class"] = "newtab";
        //li2.Controls.Add(addNewTabLinkButton);
        //tabList.Controls.Add(li2);
        #endregion
        addNewTabLinkButton.Click += new EventHandler(addNewTabLinkButton_Click);

        // ILIAS 1/13/2009: Turning it off because new users are not getting the "Add New Page" link
        //if (Roles.Enabled)
        //{
        //    if (!Roles.IsUserInRole("ChangePageSettings"))
        //        addNewTabLinkButton.Visible = false;
        //}
    }

    #endregion Methods
}