<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FastRssWidget.ascx.cs"
    Inherits="Widgets_FastRssWidget" EnableViewState="false" %>
<asp:Panel ID="SettingsPanel" runat="Server" Visible="False">
    <asp:Literal ID="ltlShow" EnableViewState="false" runat="server" Text="<%$Resources:SharedResources, Show%>" />
    <asp:DropDownList ID="FeedCountDropDownList" runat="Server">
        <asp:ListItem>1</asp:ListItem>
        <asp:ListItem>2</asp:ListItem>
        <asp:ListItem>3</asp:ListItem>
        <asp:ListItem>4</asp:ListItem>
        <asp:ListItem>5</asp:ListItem>
        <asp:ListItem>6</asp:ListItem>
        <asp:ListItem>7</asp:ListItem>
        <asp:ListItem>8</asp:ListItem>
        <asp:ListItem>9</asp:ListItem>
        <asp:ListItem>10</asp:ListItem>
        <asp:ListItem>11</asp:ListItem>
        <asp:ListItem>12</asp:ListItem>
        <asp:ListItem>13</asp:ListItem>
        <asp:ListItem>14</asp:ListItem>
        <asp:ListItem>15</asp:ListItem>
        <asp:ListItem>16</asp:ListItem>
        <asp:ListItem>17</asp:ListItem>
        <asp:ListItem>18</asp:ListItem>
        <asp:ListItem>19</asp:ListItem>
        <asp:ListItem>20</asp:ListItem>
    </asp:DropDownList>
    <asp:Literal ID="Literal1" EnableViewState="false" runat="server" Text="<%$Resources:SharedResources, Items%>" />
    <asp:Button ID="SaveSettings" runat="Server" OnClick="SaveSettings_Click" Text="<%$Resources:SharedResources, Save%>" />
</asp:Panel>
<asp:BulletedList ID="RssContainer" BulletStyle="Disc" runat="server" CssClass="news_list">
<asp:ListItem Text="数据正在加载中……"></asp:ListItem>
</asp:BulletedList>
<%--<asp:Panel ID="RssContainer1" runat="server" CssClass="news_list">
</asp:Panel>--%>
