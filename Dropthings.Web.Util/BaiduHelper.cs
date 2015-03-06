using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Dropthings.Util;

namespace Dropthings.Web.Util
{
    public class BaiduHelper
    {
        /// <summary>
        /// 根据条件格式化为百度新闻搜索页面的URL
        /// </summary>
        /// <param name="rn">每页显示的条数</param>
        /// <param name="pn">当前页起始条数</param>
        /// <param name="inputEncoding">关键字编码：utf8/gb2312</param>
        /// <param name="keyWords">URL编码后的关键字</param>
        /// <param name="tn">新闻搜索方式：newsA搜索方式为新闻全文，newstitle为按新闻标题</param>
        /// <returns></returns>
        public static string FormatNewsUrl(string rn, string tn, string inputEncoding, string pn, string keyWords)
        {
            if (string.IsNullOrEmpty(tn))
                tn = "newsA";
            if (string.IsNullOrEmpty(inputEncoding))
                tn = "gb2312";
            string url = string.Format("http://news.baidu.com/ns?cl=2&rn={0}&tn={1}&et=0&si=&ie={2}&ct=1&pn={3}&word={4}", rn, tn, inputEncoding, pn, keyWords);
            return url;
        }
        /// <summary>
        /// 根据条件格式化为百度网页搜索页面的URL
        /// </summary>
        /// <param name="rn">每页显示的条数</param>
        /// <param name="pn">当前页起始条数</param>
        /// <param name="keyWords">URL编码后的关键字</param>
        /// <returns></returns>
        public static string FormatWebUrl(string rn, string pn, string keyWords)
        {
            string url = string.Format("http://www.baidu.com/s?wd={0}&pn={1}&rn={2}&usm={3}", keyWords, pn, rn);
            return url;
            //http://www.baidu.com/s?wd=%CF%C2%D1%A9&pn=20&usm=3
        }

        /// <summary>
        /// 获取百度新闻搜索结果
        /// </summary>
        /// <param name="content">百度新闻搜索页面的源代码</param>
        /// <param name="GenSubSameNews">是否获取相同新闻，暂时没有实现</param>
        /// <returns>JSON格式的新闻搜索结果</returns>
        public static string GenerateBaiduNewsInJson(string content, bool GenSubSameNews)
        {
            string regtex = "<table cellspacing=0 cellpadding=2>([\\s\\S]+?)<td class=\"text\"><a href=\"([\\s\\S]+?) \"  mon=\"a=5[\\s\\S]+?<span><b>([\\s\\S]+?)</b></span></a> <font color=#6f6f6f> <nobr>([\\s\\S]+?) ([\\s\\S]+?)</nobr></font><br><font size=-1>([\\s\\S]+?)(<a href=\"/ns\\?([\\s\\S]+?) \"><font color=#008000>(\\d+)条相同新闻&gt;&gt;</a></font>)?\n</td></tr></table><br>";

            StringBuilder json = new StringBuilder();
            json.Append("[");

            Regex r;
            Match m;
            r = new Regex(regtex, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            for (m = r.Match(content); m.Success; m = m.NextMatch())
            {
                string news = GetOneNewsInJson(m);
                json.Append(news);
            }
            json.Append("]");

            return json.Replace(",]", "]").ToString();
        }

        private static string GetOneNewsInJson(Match m)
        {
            StringBuilder news = new StringBuilder();
            //string temp = "";
            //foreach (Group g in m.Groups)
            //{
            //    temp += g.Index + " " + g.Value + "<br />";
            //}
            news.Append("{");
            news.AppendFormat("\"Reference\":\"{0}\",", m.Groups[2].Value.ToJson());
            news.AppendFormat("\"Title\":\"{0}\",", EncodeByEscape.GetEscapeStr(m.Groups[3].Value).ToJson());
            news.AppendFormat("\"SiteName\":\"{0}\",", Utility.RemoveHTML(m.Groups[4].Value).ToJson());
            news.AppendFormat("\"Date\":\"{0}\",", Utility.RemoveHTML(m.Groups[5].Value).ToJson());
            news.AppendFormat("\"Content\":\"{0}\"", EncodeByEscape.GetEscapeStr(m.Groups[6].Value).ToJson());
            news.Append("},");
            return news.ToString();
        }

        public static string GetAllBaiduNewsCount(string source)
        {
            var count = Utility.SniffwebCode(source, "百度一下，找到相关新闻约", "篇");

            return count.Replace(",", "");
        }
    }
}
