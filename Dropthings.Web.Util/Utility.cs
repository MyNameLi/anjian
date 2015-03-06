using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Security.Policy;
using System.Data;
using System.Threading;
using System.Collections;
using System.Diagnostics;
using Dropthings.Util;

namespace Dropthings.Web.Util
{
    public static class Utility
    {

        #region Search

        #region 日期随机函数
        /********************************** 
         * 函数名称:DateRndName 
         * 功能说明:日期随机函数 
         * 参    数:ra:随机数 
         * 调用示例: 
         *          GetRemoteObj o = new GetRemoteObj(); 
         *          Random ra = new Random(); 
         *          string s = o.DateRndName(ra); 
         *          Response.Write(s); 
         *          o.Dispose(); 
         * ********************************/
        /// <summary>  
        /// 日期随机函数  
        /// </summary>  
        /// <param name="ra">随机数</param>  
        /// <returns></returns>  
        public static string DateRndName(Random ra)
        {
            DateTime d = DateTime.Now;
            string s = null, y, m, dd, h, mm, ss;
            y = d.Year.ToString();
            m = d.Month.ToString();
            if (m.Length < 2) m = "0" + m;
            dd = d.Day.ToString();
            if (dd.Length < 2) dd = "0" + dd;
            h = d.Hour.ToString();
            if (h.Length < 2) h = "0" + h;
            mm = d.Minute.ToString();
            if (mm.Length < 2) mm = "0" + mm;
            ss = d.Second.ToString();
            if (ss.Length < 2) ss = "0" + ss;
            s += y + m + dd + h + mm + ss;
            s += ra.Next(100, 999).ToString();
            return s;
        }
        #endregion

        #region 取得文件后缀
        /********************************** 
         * 函数名称:GetFileExtends 
         * 功能说明:取得文件后缀 
         * 参    数:filename:文件名称 
         * 调用示例: 
         *          GetRemoteObj o = new GetRemoteObj(); 
         *          string url = @"http://www.baidu.com/img/logo.gif"; 
         *          string s = o.GetFileExtends(url); 
         *          Response.Write(s); 
         *          o.Dispose(); 
         * ********************************/
        /// <summary>  
        /// 取得文件后缀  
        /// </summary>  
        /// <param name="filename">文件名称</param>  
        /// <returns></returns>  
        public static string GetFileExtends(string filename)
        {
            string ext = null;
            if (filename.IndexOf('.') > 0)
            {
                string[] fs = filename.Split('.');
                ext = fs[fs.Length - 1];
            }
            return ext;
        }
        #endregion

        #region 替换网页中的换行和引号
        /********************************** 
         * 函数名称:ReplaceEnter 
         * 功能说明:替换网页中的换行和引号 
         * 参    数:HtmlCode:html源代码 
         * 调用示例: 
         *          GetRemoteObj o = new GetRemoteObj(); 
         *          string Url = @"http://www.baidu.com"; 
         *          strion HtmlCode = o.GetRemoteHtmlCode(Url); 
         *          string s = o.ReplaceEnter(HtmlCode); 
         *          Response.Write(s); 
         *          o.Dispose(); 
         * ********************************/
        /// <summary>  
        /// 替换网页中的换行和引号  
        /// </summary>  
        /// <param name="HtmlCode">HTML源代码</param>  
        /// <returns></returns>  
        public static string ReplaceEnter(string HtmlCode)
        {
            string s = "";
            if (HtmlCode == null || HtmlCode == "")
                s = "";
            else
                s = HtmlCode.Replace("\"", "");
            s = s.Replace("\r\n", "");
            s = s.Replace("\r", "");
            s = s.Replace("\n", "");
            return s;
        }
        #endregion

        #region 执行正则提取出值
        /********************************** 
         * 函数名称:GetRegValue 
         * 功能说明:执行正则提取出值 
         * 参    数:HtmlCode:html源代码 
         * 调用示例: 
         *          GetRemoteObj o = new GetRemoteObj(); 
         *          string Url = @"http://www.baidu.com"; 
         *          strion HtmlCode = o.GetRemoteHtmlCode(Url); 
         *          string s = o.ReplaceEnter(HtmlCode); 
         *          string Reg="<title>.+?</title>"; 
         *          string GetValue=o.GetRegValue(Reg,HtmlCode) 
         *          Response.Write(GetValue); 
         *          o.Dispose(); 
         * ********************************/
        /// <summary>  
        /// 执行正则提取出值  
        /// </summary>  
        /// <param name="RegexString">正则表达式</param>  
        /// <param name="RemoteStr">HtmlCode源代码</param>  
        /// <returns></returns>  
        public static string GetRegValue(string RegexString, string RemoteStr)
        {
            string MatchVale = "";
            Regex r = new Regex(RegexString);
            Match m = r.Match(RemoteStr);
            if (m.Success)
            {
                MatchVale = m.Value;
            }
            return MatchVale;
        }
        #endregion

        #region 替换HTML源代码
        /********************************** 
         * 函数名称:RemoveHTML 
         * 功能说明:替换HTML源代码 
         * 参    数:HtmlCode:html源代码 
         * 调用示例: 
         *          GetRemoteObj o = new GetRemoteObj(); 
         *          string Url = @"http://www.baidu.com"; 
         *          strion HtmlCode = o.GetRemoteHtmlCode(Url); 
         *          string s = o.ReplaceEnter(HtmlCode); 
         *          string Reg="<title>.+?</title>"; 
         *          string GetValue=o.GetRegValue(Reg,HtmlCode) 
         *          Response.Write(GetValue); 
         *          o.Dispose(); 
         * ********************************/
        /// <summary>  
        /// 替换HTML源代码  
        /// </summary>  
        /// <param name="HtmlCode">html源代码</param>  
        /// <returns></returns>  
        public static string RemoveHTML(string HtmlCode)
        {
            string MatchVale = HtmlCode;
            foreach (Match s in Regex.Matches(HtmlCode, "<.+?>"))
            {
                MatchVale = MatchVale.Replace(s.Value, "");
            }
            return MatchVale;
        }
        #endregion

        #region 匹配页面的链接
        /********************************** 
         * 函数名称:GetHref 
         * 功能说明:匹配页面的链接 
         * 参    数:HtmlCode:html源代码 
         * 调用示例: 
         *          GetRemoteObj o = new GetRemoteObj(); 
         *          string Url = @"http://www.baidu.com"; 
         *          strion HtmlCode = o.GetRemoteHtmlCode(Url); 
         *          string s = o.GetHref(HtmlCode); 
         *          Response.Write(s); 
         *          o.Dispose(); 
         * ********************************/
        /// <summary>  
        /// 获取页面的链接正则  
        /// </summary>  
        /// <param name="HtmlCode"></param>  
        /// <returns></returns>  
        public static List<string> GetHref(string HtmlCode)
        {
            List<string> MatchVale = new List<string>();

            string Reg = @"(h|H)(r|R)(e|E)(f|F) *= *('|"")?((\w|\\|\/|\.|:|-|_)+)('|""| *|>)?";
            foreach (Match m in Regex.Matches(HtmlCode, Reg))
            {
                MatchVale.Add((m.Value).ToLower().Replace("href=", "").Trim());
            }
            return MatchVale;
        }
        #endregion

        #region 匹配页面的图片地址
        /********************************** 
         * 函数名称:GetImgSrc 
         * 功能说明:匹配页面的图片地址 
         * 参    数:HtmlCode:html源代码;imgHttp:要补充的http.当比如:<img src="bb/x.gif">则要补充http://www.baidu.com/,当包含http信息时,则可以为空 
         * 调用示例: 
         *          GetRemoteObj o = new GetRemoteObj(); 
         *          string Url = @"http://www.baidu.com"; 
         *          strion HtmlCode = o.GetRemoteHtmlCode(Url); 
         *          string s = o.GetImgSrc(HtmlCode,"http://www.baidu.com/"); 
         *          Response.Write(s); 
         *          o.Dispose(); 
         * ********************************/
        /// <summary>  
        /// 匹配页面的图片地址  
        /// </summary>  
        /// <param name="HtmlCode"></param>  
        /// <param name="imgHttp">要补充的http://路径信息</param>  
        /// <returns></returns>  
        public static string GetImgSrc(string HtmlCode, string imgHttp)
        {
            string MatchVale = "";
            string Reg = @"<img.+?>";
            foreach (Match m in Regex.Matches(HtmlCode, Reg))
            {
                MatchVale += GetImg((m.Value).ToLower().Trim(), imgHttp) + "||";
            }
            return MatchVale;
        }
        /// <summary>  
        /// 匹配<img src="" />中的图片路径实际链接  
        /// </summary>  
        /// <param name="ImgString"><img src="" />字符串</param>  
        /// <returns></returns>  
        public static string GetImg(string ImgString, string imgHttp)
        {
            string MatchVale = "";
            string Reg = @"src=.+\.(bmp|jpg|gif|png|)";
            foreach (Match m in Regex.Matches(ImgString.ToLower(), Reg))
            {
                MatchVale += (m.Value).ToLower().Trim().Replace("src=", "");
            }
            return (imgHttp + MatchVale);
        }
        #endregion

        #region 替换通过正则获取字符串所带的正则首尾匹配字符串
        /********************************** 
         * 函数名称:GetHref 
         * 功能说明:匹配页面的链接 
         * 参    数:HtmlCode:html源代码 
         * 调用示例: 
         *          GetRemoteObj o = new GetRemoteObj(); 
         *          string Url = @"http://www.baidu.com"; 
         *          strion HtmlCode = o.GetRemoteHtmlCode(Url); 
         *          string s = o.RegReplace(HtmlCode,"<title>","</title>"); 
         *          Response.Write(s); 
         *          o.Dispose(); 
         * ********************************/
        /// <summary>  
        /// 替换通过正则获取字符串所带的正则首尾匹配字符串  
        /// </summary>  
        /// <param name="RegValue">要替换的值</param>  
        /// <param name="regStart">正则匹配的首字符串</param>  
        /// <param name="regEnd">正则匹配的尾字符串</param>  
        /// <returns></returns>  
        public static string RegReplace(string RegValue, string regStart, string regEnd)
        {
            string s = RegValue;
            if (RegValue != "" && RegValue != null)
            {
                if (regStart != "" && regStart != null)
                {
                    s = s.Replace(regStart, "");
                }
                if (regEnd != "" && regEnd != null)
                {
                    s = s.Replace(regEnd, "");
                }
            }
            return s;
        }

        /********************************** 以下为测试过的,在魏言项目里使用的采集方法.按方法顺序调用既可采集
        * 函数名称:SniffwebCode 
        * 功能说明:获得页面HTML代码中开始标记和结束标记中间的数据:测试可用
        * 参    数:HTML源代码 ,开始标记,结束标记
        * 调用示例: 
        *string url = @"http://sohe.inhe.net/Search/SearchingGuild.aspx"; 
        *string s = o.GetRemoteHtmlCode(url);//获得网站源码
        *s=this.SniffwebCode(s,this.TextBox2.Text,this.TextBox3.Text);//获得指定HTML开始标记和结束标记中间的数据
        * ********************************/
        /// <summary> 
        /// 获得HTML代码开始标记和结束标记中间的数据 
        /// </summary> 
        /// <param name="code">HTML代码</param> 
        /// <param name="wordsBegin">开始标记</param> 
        /// <param name="wordsEnd">结束标记</param> 
        /// <returns></returns> 
        public static string SniffwebCode(string code, string wordsBegin, string wordsEnd)
        {
            string NewsTitle = "";
            Regex regex1 = new Regex("" + wordsBegin + @"(?<title>[\s\S]+?)" + wordsEnd + "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            for (Match match1 = regex1.Match(code); match1.Success; match1 = match1.NextMatch())
            {
                NewsTitle = match1.Groups["title"].ToString();
            }
            return NewsTitle;
        }

        #endregion

        #endregion

        public static void DownFile(string Url, string Path)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            long size = response.ContentLength;
            //创建文件流对象
            using (FileStream fs = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] b = new byte[1025];
                int n = 0;
                while ((n = stream.Read(b, 0, 1024)) > 0)
                {
                    fs.Write(b, 0, n);
                }
            }
        }

        /// <summary>
        /// 提取HTML代码中的网址 
        /// </summary>
        /// <param name="htmlCode">html源代码</param>
        /// <param name="strRegex">eg. string regexPattern = @"(href\s*=\s*)[""''](?<url>[^''""]+)[""'']";</param>
        /// <returns></returns>
        public static List<String> GetMatchesStr(string htmlCode, string strRegex)
        {
            List<String> al = new List<String>();

            Regex r = new Regex(strRegex, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            MatchCollection m = r.Matches(htmlCode);

            for (int i = 0; i < m.Count; i++)
            {
                if (al.IndexOf(m[i].Value) < 0) al.Add(m[i].Value);
            }

            al.Sort();

            return al;
        }

        /// <summary>
        /// 获取页面静态Html代码
        /// </summary>
        public static string GetHtml(string url)
        {
            StreamReader sr = null;
            string str = "";

            try
            {
                //读取远程路径
                WebRequest request = WebRequest.Create(url);
                //request.Credentials = new NetworkCredential("username","pwd");

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                sr = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet));
                str = sr.ReadToEnd();

                //str = EncodeHelper.ConvertToUTF8String(str, Encoding.GetEncoding(response.CharacterSet));

                sr.Close();
            }
            catch (Exception err)
            {
            }

            return str;
        }

    }
}
