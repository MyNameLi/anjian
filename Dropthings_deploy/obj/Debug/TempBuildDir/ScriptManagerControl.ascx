<%@ control language="C#" autoeventwireup="true" inherits="ScriptManagerControl, Dropthings_deploy" %>
<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"
    ScriptMode="Release">
    <Services>
        <asp:ServiceReference InlineScript="false" Path="~/API/Proxy.svc/ajax" />
        <asp:ServiceReference InlineScript="false" Path="~/API/Widget.svc/ajax" />
        <asp:ServiceReference InlineScript="false" Path="~/API/Page.svc/ajax" />
    </Services>
    <%--<Scripts>--%>
    <%--<asp:ScriptReference Path="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js" />
        <asp:ScriptReference Path="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.0/jquery-ui.min.js" />--%>
    <%--<asp:ScriptReference Path="~/Scripts/jquery-1.4.2.js" />
        <asp:ScriptReference Path="~/Scripts/jquery-ui-1.7.1.custom.min.js" />
        <asp:ScriptReference Path="~/Scripts/jquery.micro_template.js" />
        <asp:ScriptReference Path="~/Scripts/tabscroll.js" />
        <asp:ScriptReference Path="~/Scripts/Myframework.js" />--%>
    <%--GooKo begin--%>
    <%--<asp:ScriptReference Path="~/Scripts/Bar.js" />
        <asp:ScriptReference Path="~/Scripts/excanvas.min.js" />
        <asp:ScriptReference Path="~/Scripts/jquery.flash.js" />
        <asp:ScriptReference Path="~/Scripts/jquery.flot.js" />
        <asp:ScriptReference Path="~/Scripts/jquery.flot.selection.js" />
        <asp:ScriptReference Path="~/Scripts/jquery.query.js" />
        <asp:ScriptReference Path="~/Scripts/Pie.js" />
        <asp:ScriptReference Path="~/Scripts/common.js" />--%>
    <%--GooKo end--%>
    <%--<asp:ScriptReference Path="~/Scripts/Ensure.js" />--%>
    <%--</Scripts>--%>
</asp:ScriptManager>
