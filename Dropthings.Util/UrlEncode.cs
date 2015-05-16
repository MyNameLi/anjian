using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Dropthings.Util
{
    public static class UrlEncode
    {
        public static string GetEncodeUrl(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                return System.Web.HttpUtility.UrlEncode(url);
            }
            else
            {
                return "";
            }
        }

        public static string GetIdolQueryUrlStr(string[] urllist)
        {
            StringBuilder posturl = new StringBuilder();
            foreach (string url in urllist)
            {
                if (posturl.Length > 0)
                {
                    posturl.Append("+");
                }
                posturl.Append(HttpUtility.UrlEncode(url, Encoding.UTF8));
            }
            return posturl.ToString();
        }
    }
}
