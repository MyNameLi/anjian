<%@ control language="C#" autoeventwireup="true" inherits="Widgets_EventTest_Publisher, Dropthings_deploy" %>
<b>I am a Master widget</b><br />
Say: <asp:TextBox ID="Message" runat="server" Text="Live Long, Child" /><br />
<asp:Button ID="Raise" runat="server" Text="Bless Child" OnClick="Raise_Clicked" /><br />
<br />
<asp:Label ID="Received" runat="server" Font-Bold="true" />