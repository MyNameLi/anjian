var _SimpleCalendar = new Object;
var SimpleCalendar = _SimpleCalendar.property = {
    Load: function() {
        Common.GetMonth("clock_month");
        Common.GetDay("clock_day");
        Common.DownFlash("clock_falsh", "flash/clock.swf", 96, 96);
    }
}