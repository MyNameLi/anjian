using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Dropthings.Util
{
    public static class CommonHelp
    {
        private static string FilterKeyWords = string.Empty;

        private static void InnitFilterKeyWords(){
            string KeyWords = ConfigurationManager.AppSettings["FilterKeyWords"].ToString();
            if (!string.IsNullOrEmpty(KeyWords))
            {
                string[] keywordlist = KeyWords.Split(',');
                if(keywordlist.Length > 0){
                    StringBuilder words = new StringBuilder();
                    for (int i = 0, j = keywordlist.Length; i < j; i++)
                    {
                        if (words.Length > 0) {
                            words.Append("+");
                        }
                        words.AppendFormat("\"{0}\"", keywordlist[i]);
                    }
                    FilterKeyWords = words.ToString();
                }
            }
        }

        public static string GetFilterKeyWords(){
            if (string.IsNullOrEmpty(FilterKeyWords))
            {
                InnitFilterKeyWords();
            }
            return FilterKeyWords;
        }
    }
}
