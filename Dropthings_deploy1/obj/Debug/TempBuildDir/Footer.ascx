<%@ control language="C#" autoeventwireup="true" inherits="Footer, Dropthings_deploy1" enableviewstate="false" %>
<div id="footer">
    <div id="footer_wrapper">

        <p class="copyright">&copy; <a href="#"> ��Ȩ��Ϣ</a>. <asp:Literal ID="ltlCopyright" EnableViewState="false" runat="server" Text="<%$Resources:SharedResources, Copyright%>" /> <br />
		<%--<asp:Literal ID="ltlNewsAndUpdates" EnableViewState="false" runat="server" Text="<%$Resources:SharedResources, NewsAndUpdates%>" />--%></p>
        <!--<p class="license"> -->
        <%--<asp:Literal ID="ltlPersonalMessage" EnableViewState="false" runat="server" Text="<%$Resources:SharedResources, PersonalMessage%>" />--%>
        <!-- </p> -->
        <!-- Begin Shinystat Free code -->
        <!--<div id="counter">-->
        <%--<a href="http://www.shinystat.com" target="_top">
        <img src="http://www.shinystat.com/cgi-bin/shinystat.cgi?USER=dropthings" alt="Free hit counters" border="0" /></a>--%>
        <!--</div>-->
        <!-- End Shinystat Free code -->

    </div>
</div>
