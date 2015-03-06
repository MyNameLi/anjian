using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dropthings.Business.Facade
{
    public class AmChartColor
    {
        private static string[] ColorList = new string[] { "#07a16a", "#a10752", "#9aa107", "#077ba1" };
        private static int len = 4;
        public static string GetColor(int type) {
            int index = type - 1;

            if (index < len)
            {
                return ColorList[index];
            }
            else {
                return "";
            }
        }
        public static string GetColumnColor(int index)
        {
            return ColumnColors[index];
        }
        private static string[] ColumnColors = new string[] { 
            "AFD8F8", "F6BD0F", "8BBA00", "FF8E46", "008E8E", "D64646",
            "8E468E","588526","B3AA00","008ED6","9D080D","A186BE"
        };
    }
}
