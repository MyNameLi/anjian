using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dropthings.Business.Facade;
using Dropthings.Business.Facade.Context;
using Dropthings.Data;
using Dropthings.Util;
using OmarALZabir.AspectF;
using System.Diagnostics;

public partial class Admin_ManageWidgets : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.LoadWidgets();
    }

    private void LoadWidgets()
    {
        using (Facade facade = new Facade(AppContext.GetContext(Context)))
        {
            Widgets.DataSource = facade.GetAllWidgets();
            Widgets.DataBind();
        }
    }

    protected void DeleteWidget_Clicked(object sender, EventArgs e)
    {
        try
        {
            using (Facade facade = new Facade(AppContext.GetContext(Context)))
            {
                facade.DeleteWidget(int.Parse(Field_ID.Value));
            }
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "delete success", "alert('删除成功！');", true);
        }
        catch (Exception x)
        {
            ErrorMessage.Text = x.Message;
            ErrorMessage.Visible = true;
        }
    }

    protected void SaveWidget_Clicked(object sender, EventArgs e)
    {
        try
        {
            var control = LoadControl(Field_Url.Value);
        }
        catch (Exception x)
        {
            Debug.WriteLine(x.ToString());
            Services.Get<ILogger>().LogException(x);
            ErrorMessage.Text = x.Message;
            ErrorMessage.Visible = true;
            return;
        }

        using (Facade facade = new Facade(AppContext.GetContext(Context)))
        {
            var widgetId = int.Parse(Field_ID.Value.Trim());
            if (widgetId == 0)
            {
                var newlyAddedWidget = facade.AddWidget(
                    Field_Name.Value,
                    Field_Url.Value,
                    Field_Icon.Value,
                    Field_Description.Value,
                    Field_DefaultState.Value,
                    Field_IsDefault.Checked,
                    Field_IsLocked.Checked,
                    int.Parse(Field_OrderNo.Value),
                    Field_RoleName.Value,
                    int.Parse(Field_WidgetType.Value));

                widgetId = newlyAddedWidget.ID.Value;
                SetWidgetRoles(widgetId);
                this.EditWidget(newlyAddedWidget);
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "update success", "alert('添加成功！');", true);
            }
            else
            {
                facade.UpdateWidget(widgetId,
                    Field_Name.Value,
                    Field_Url.Value,
                    Field_Icon.Value,
                    Field_Description.Value,
                    Field_DefaultState.Value,
                    Field_IsDefault.Checked,
                    Field_IsLocked.Checked,
                    int.Parse(Field_OrderNo.Value),
                    Field_RoleName.Value,
                    int.Parse(Field_WidgetType.Value));

                SetWidgetRoles(widgetId);
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "update success", "alert('修改成功！');", true);
            }

            this.LoadWidgets();
            EditForm.Visible = false;
        }
    }

    private void SetWidgetRoles(int widgetId)
    {
        var roles = new List<string>();
        foreach (ListItem item in WidgetRoles.Items)
            if (item.Selected)
                roles.Add(item.Text);
        using (Facade facade = new Facade(AppContext.GetContext(Context)))
        {
            facade.AssignWidgetRoles(widgetId, roles.ToArray());
        }
    }

    protected void AddNew_Clicked(object sender, EventArgs e)
    {
        EditForm.Visible = true;
        ClearForm(role => false);
    }

    private void ClearForm(Func<string, bool> isRoleChecked)
    {
        Field_DefaultState.Value = "<state><count>6</count></state>";
        Field_Description.Value = "挂件描述";
        Field_Url.Value = "~/Widgets/";
        Field_Icon.Value = "widgets/Rss.gif";
        Field_ID.Value = "0";
        Field_IsDefault.Checked = false;
        Field_IsLocked.Checked = false;
        Field_Name.Value = "新挂件";
        Field_OrderNo.Value = "0";
        Field_RoleName.Value = "user";
        Field_WidgetType.Value = "0";

        WidgetRoles.Items.Clear();
        Facade facade = new Facade(AppContext.GetContext(Context));
        {
            facade.GetAllRole().Each(role =>
                {
                    ListItem newRoleItem = new ListItem(role.ROLENAME, role.ROLEID.ToString());
                    newRoleItem.Selected = isRoleChecked(role.ROLENAME);
                    WidgetRoles.Items.Add(newRoleItem);

                });
        }
    }
    protected void Widgets_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            WidgetEntity widget = (Widgets.DataSource as List<WidgetEntity>)[e.Item.ItemIndex];

            EditWidget(widget);
        }
        else if (e.CommandName == "Delete")
        {
            try
            {
                using (Facade facade = new Facade(AppContext.GetContext(Context)))
                {
                    facade.DeleteWidget(int.Parse(e.CommandArgument.ToString()));
                }
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "delete success", "alert('删除成功！');", true);
                this.LoadWidgets();
            }
            catch (Exception x)
            {
                ErrorMessage.Text = x.Message;
                ErrorMessage.Visible = true;
            }
        }
    }

    private void EditWidget(WidgetEntity widget)
    {
        using (Facade facade = new Facade(AppContext.GetContext(Context)))
        {
            ClearForm(role => facade.IsWidgetInRole(widget.ID.Value, role));
        }

        EditForm.Visible = true;

        Field_DefaultState.Value = widget.DEFAULTSTATE;
        Field_Description.Value = widget.DESCRIPTION;
        Field_Url.Value = widget.URL;
        Field_Icon.Value = widget.ICON;
        Field_ID.Value = widget.ID.ToString();
        Field_IsDefault.Checked = widget.ISDEFAULT.Value;
        Field_IsLocked.Checked = widget.ISLOCKED.Value;
        Field_Name.Value = widget.NAME;
        Field_OrderNo.Value = widget.ORDERNO.ToString();
        Field_RoleName.Value = widget.ROLENAME.ToString();
        Field_WidgetType.Value = widget.WIDGETTYPE.ToString();
    }
}
