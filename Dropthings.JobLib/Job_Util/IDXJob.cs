using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;

namespace IdxSource
{
    public class IDXJob
    {
        public string Keywords { get; set; }

        public void save()
        {
            //获得key和输出路径
            string KeyWords = GetKeyWords();
            string outPath = GetOutPath();
            bool getSame = GetSameNews();
            int maxpage = GetAppMaxPage();
            //int GetPageCount = 

            string urlSeed = @"http://news.baidu.com/ns?bt=0&et=0&si=&rn=100&tn=newsA&ie=gb2312&ct=0&word={0}&pn={1}&cl=2";
            int pageCount = maxpage;
            int pageSize = 100;
            //string regtex = "<td class=\"text\"><a href=\"([\\s\\S]+?) \"  mon=\"a=5[\\s\\S]+?<span><b>([\\s\\S]+?)</b></span></a> <font color=#6f6f6f> <nobr>([\\s\\S]+?) ([\\s\\S]+?)</nobr></font><br><font size=-1>([\\s\\S]+?)\\.\\.\\.</font>";
            //2010-06-28调整正则
            //string regtex = "<table cellspacing=0 cellpadding=2>\n<tr>([\\s\\S]+?)<td class=\"text\"><a href=\"([\\s\\S]+?) \"  mon=\"a=5[\\s\\S]+?<span><b>([\\s\\S]+?)</b></span></a> <font color=#6f6f6f> <nobr>([\\s\\S]+?) ([\\s\\S]+?)</nobr></font><br><font size=-1>([\\s\\S]+?)</font>    \n(<a href=\"/ns\\?([\\s\\S]+?) \"><font color=#008000>(\\d+)条相同新闻&gt;&gt;</a></font>)?\n</td></tr></table><br>";
            string regtex = "<table cellspacing=0 cellpadding=2>([\\s\\S]+?)<td class=\"text\"><a href=\"([\\s\\S]+?) \"  mon=\"a=5[\\s\\S]+?<span><b>([\\s\\S]+?)</b></span></a> <font color=#6f6f6f> <nobr>([\\s\\S]+?) ([\\s\\S]+?)</nobr></font><br><font size=-1>([\\s\\S]+?)(<a href=\"/ns\\?([\\s\\S]+?) \"><font color=#008000>(\\d+)条相同新闻&gt;&gt;</a></font>)?\n</td></tr></table><br>";
            string fileSeed = outPath + @"\outPut_{0}_{1}.xml";
            XMLNodeMode nodeMode = XMLNodeMode.BaiduNews;

            Encoding encoding = Encoding.GetEncoding("gb2312");

            char[] charSeparators = new char[] { ';' };
            string[] words = KeyWords.Split(charSeparators);

            Pagetoxml.Do(urlSeed, words, pageSize, pageCount, fileSeed, regtex, nodeMode, encoding, getSame);

        }

        private int GetAppMaxPage()
        {
            string pageCount = ConfigurationManager.AppSettings["PageCount"].ToString();
            int maxpage;
            int.TryParse(pageCount, out maxpage);
            if (maxpage == 0)
                maxpage = 2;
            return maxpage;
        }
        //
        private string GetKeyWords()
        {
            if (string.IsNullOrEmpty(this.Keywords))
            {
                string KeyWordsFile = ConfigurationManager.AppSettings["KeyWordsFile"].ToString();
                //打开文件读取关键字

                string keys = ConfigurationManager.AppSettings["KeyWords"].ToString();
                return keys;
            }
            else
            {
                return this.Keywords;
            }
        }

        private bool GetSameNews()
        {
            string isGenSameNews = ConfigurationManager.AppSettings["GenSameNews"].ToString();
            bool getSame = false;
            bool.TryParse(isGenSameNews, out getSame);
            return getSame;
        }

        private string GetOutPath()
        {
            string desDirectory = ConfigurationManager.AppSettings["OutFileDirectory"].ToString();
            DirectoryInfo di = new DirectoryInfo(desDirectory);
            if (!di.Exists)
            {
                di.Create();
            }

            DateTime dt = DateTime.Now;
            string timepath = dt.ToString("yyMMddHHmm");
            DirectoryInfo subdi = di.CreateSubdirectory(timepath);

            return subdi.FullName;
        }
    }
}
