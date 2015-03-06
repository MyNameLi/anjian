using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.UI;
using System.Web;
using System.IO;

namespace Dropthings.Widget.Framework
{
    public class WidgetHelper
    {
        public static void RegisterWidgetScript(Control controlToBind, string[] scriptToLoad, string startUpCode)
        {
            var scriptLoader = new StringBuilder();
            int length = scriptToLoad.Length;
            scriptLoader.Append("ensure( { js: [");
            for (int i = 0; i < length; i++)
            {
                if (i == length - 1)
                {
                    scriptLoader.Append(string.Format("\"{0}\"", scriptToLoad[i]));
                }
                else
                {
                    scriptLoader.Append(string.Format("\"{0}\",", scriptToLoad[i]));
                }
            }
            scriptLoader.Append("] }, function() { ");
            scriptLoader.Append(string.Format("{0}", startUpCode));
            scriptLoader.Append(" } );");

            ScriptManager.RegisterStartupScript(controlToBind, controlToBind.GetType(), controlToBind.ToString() + DateTime.Now.Ticks.ToString(),
                scriptLoader.ToString(), true);
        }
    }
}
