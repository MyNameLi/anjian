<%@ control language="C#" autoeventwireup="true" inherits="Widgets_SimpleCalendar, Dropthings_deploy" %>
<div class="left-top-a">
    <div class="left-tabler-rz">
        <div class="leftbottom-left">
            <div class="clock_month" id="clock_month">
            </div>
            <div class="clock_day" id="clock_day">
            </div>
        </div>
        <div class="leftbottom-right" id="clock_falsh">
        </div>
    </div>
</div>

<%--<script type="text/javascript">
    jQuery(function() {
        Common.GetMonth("clock_month");
        Common.GetDay("clock_day");
        ensure({ js: ["Scripts/jquery.flash.js"] }, function() { Common.DownFlash("clock_falsh", "flash/clock.swf", 96, 96) });
    });
</script>--%>

