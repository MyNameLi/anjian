using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dropthings.Util
{
    public class GetDictItem
    {
        public static string[] GetDictParam(Dictionary<string, string[]> dict, string key)
        {
            return dict[key];
        }

        public static string GetStr(string key)
        {
            return key;
        }
        
    }
}
