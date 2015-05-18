<%@ page language="C#" autoeventwireup="true" inherits="Admin_ManageWidgets, Dropthings_deploy" validaterequest="false" theme="GreenBlue" enableEventValidation="false" %>

<%@ OutputCache Location="None" NoStore="true" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>挂件管理</title>
</head>
<style>
    label
    {
        width: 200px;
    }
    td
    {
        padding: 5px;
    }
</style>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>
            挂件管理</h1>
        <asp:Button ID="CreateNew" runat="server" Text="注册挂件" OnClick="AddNew_Clicked" />
        <h2>
            编辑挂件</h2>
        <asp:Panel ID="EditForm" runat="server" Visible="false">
            <asp:Label ID="ErrorMessage" runat="server" EnableViewState="false" ForeColor="Red"
                Font-Size="20pt" />
            <div>
                <label>
                    编号:
                </label>
                <input id="Field_ID" runat="server" type="text" disabled="disabled" />
                <br />
                <label>
                    名称:
                </label>
                <input id="Field_Name" maxlength="255" runat="server" type="text" />
                <br />
                <label>
                    描述:
                </label>
                <input id="Field_Description" maxlength="255" runat="server" type="text" />
                <br />
                <label>
                    Url:
                </label>
                <input id="Field_Url" maxlength="255" runat="server" type="text" />
                <br />
                <label>
                    默认状态:
                </label>
                <input id="Field_DefaultState" maxlength="255" runat="server" type="text" />
                <br />
                <label>
                    Icon图标:
                </label>
                <input id="Field_Icon" maxlength="255" runat="server" type="text" />
                <br />
                <label>
                    排序:
                </label>
                <input id="Field_OrderNo" maxlength="2" value="1" runat="server" type="text" />
                <br />
                <label>
                    默认角色:
                </label>
                <input id="Field_RoleName" maxlength="255" value="guest" runat="server" type="text" />
                <br />
                <label>
                    是否锁定:
                </label>
                <input id="Field_IsLocked" runat="server" type="checkbox" />
                <br />
                <label>
                    是否默认:
                </label>
                <input id="Field_IsDefault" runat="server" type="checkbox" />
                <br />
                <label>
                    挂件类型:
                </label>
                <input id="Field_WidgetType" maxlength="2" value="0" runat="server" type="text" />
                <br />
                <label>
                    能够使用该挂件的角色:</label>
                <asp:CheckBoxList ID="WidgetRoles" runat="server">
                </asp:CheckBoxList>
                <br />
                <asp:Button ID="SaveWidget" runat="server" OnClick="SaveWidget_Clicked" Text="保存" />
                <asp:Button Text="删除" ID="DeleteWidget" OnClick="DeleteWidget_Clicked" runat="server"
                    OnClientClick="javascript:return confirm('您确定要删除该挂件吗？');" />
            </div>
        </asp:Panel>
        <asp:DataGrid ID="Widgets" runat="server" BackColor="LightGoldenrodYellow" BorderColor="Tan"
            BorderWidth="1px" ForeColor="Black" GridLines="None" AutoGenerateColumns="False"
            OnItemCommand="Widgets_ItemCommand">
            <FooterStyle BackColor="Tan" />
            <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
            <AlternatingItemStyle BackColor="PaleGoldenrod" />
            <Columns>
                <asp:ButtonColumn ButtonType="LinkButton" CommandName="Edit" Text="编辑" />
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:LinkButton CommandName="Delete" CommandArgument='<%# Eval("ID") %>' runat="server"
                            ID="Delete" Text="删除" OnClientClick="javascript:return confirm('您确定要删除该挂件吗？');" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="ID" HeaderText="编号" />
                <asp:BoundColumn DataField="Name" HeaderText="名称" ItemStyle-Font-Bold="true" />
                <asp:BoundColumn DataField="Url" HeaderText="Url" />
                <asp:BoundColumn DataField="Description" HeaderText="描述" />
                <asp:TemplateColumn HeaderText="状态">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DefaultState") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DefaultState") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="Icon" HeaderText="Icon图标" />
                <asp:BoundColumn DataField="OrderNo" HeaderText="排序" />
                <asp:BoundColumn DataField="RoleName" HeaderText="默认角色" />
                <asp:BoundColumn DataField="IsLocked" HeaderText="是否锁定?" />
                <asp:BoundColumn DataField="IsDefault" HeaderText="是否默认?" />
                <asp:BoundColumn DataField="VersionNo" HeaderText="版本" />
                <asp:BoundColumn DataField="WidgetType" HeaderText="挂件类型" />
            </Columns>
            <HeaderStyle BackColor="Tan" Font-Bold="True" />
        </asp:DataGrid>
    </div>
    </form>
</body>
</html>
