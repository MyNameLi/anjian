<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SystemMenu.ascx.cs" Inherits="Widgets_SystemMenu" %>
<div class="left-center">
    <div class="nav" id="menu_nav">
    </div>
</div>

<script type="text/javascript">
    jQuery(function() {
        Common.Navigation(1, "menu_nav");
    });
</script>

