using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dropthings.Util;

namespace Dropthings.Business.Facade
{
    public abstract class ResultDisplay
    {
        public IList<IdolNewsEntity> list = new List<IdolNewsEntity>();

        public abstract string Display();
        
        
    }

    public class FirstStyleDis : ResultDisplay 
    {
        public override string Display()
        {

            Dictionary<string, string> tagDict = new Dictionary<string, string>();
            tagDict.Add("other", "");
            tagDict.Add("news", "n");
            tagDict.Add("comment", "c");
            tagDict.Add("p", "p");
            tagDict.Add("n", "n");
            tagDict.Add("m", "");
            string totalcount = "0";
            StringBuilder jsonstr = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                totalcount = list[0].TotalCount;
                StringBuilder html = new StringBuilder();
                foreach (IdolNewsEntity entity in list)
                {
                    string Tag = tagDict[entity.DocType] + tagDict[entity.Tag];
                    html.AppendFormat("<li><dl><dt><input id=\"" + entity.DocId + "\" type=\"checkbox\" class=\"selected_doc\" pid=\"{1}\" />{0}</dt><dd class=\"l_title\"><a href=\"{1}\" title=\"{2}\" target=\"_blank\">", entity.weight + "%", entity.Href, entity.Title);
                    html.AppendFormat("{0}</a></dd>", (entity.Title.Length > 27) ? entity.Title.Substring(0, 27) + "..." : entity.Title);
                    html.Append("<dd class=\"l_tag\"><span><font color='black'>" + Tag + "</font>&nbsp;&nbsp;" + entity.SiteName + "</span> - " + entity.TimeStr + "</dd>");
                    html.AppendFormat("<dd class=\"l_content\">{0}<b>...</b></dd>", entity.Content);
                    html.Append("</dl></li>"); 
                }
                jsonstr.Append("{\"HtmlStr\":\"");
                jsonstr.Append(EncodeByEscape.GetEscapeStr(html.ToString()));    
                jsonstr.Append("\",\"TotalCount\":\"");
                jsonstr.Append(totalcount);
                jsonstr.Append("\"}");
            }
            return jsonstr.ToString();
        }
    }

    public class SecondStyleDis : ResultDisplay
    {
        public override string Display()
        {
            string totalcount = "0";
            StringBuilder jsonstr = new StringBuilder();

            if (list != null && list.Count > 0)
            {
                totalcount = list[0].TotalCount;
                StringBuilder html = new StringBuilder();
                foreach (IdolNewsEntity entity in list)
                {
                    html.AppendFormat("<li><h2><a href=\"{0}\" title=\"{1}\" target=\"_blank\">", entity.Href, entity.Title);
                    html.AppendFormat("{0}</a></h2>", (entity.Title.Length > 27) ? entity.Title.Substring(0, 27) + "..." : entity.Title);
                    html.Append("<div class=\"d\"><span>&nbsp;&nbsp;" + entity.SiteName + "</span> - " + entity.TimeStr + "</div>");
                    html.AppendFormat("<p>{0}<b>...</b></p>", entity.Content);
                    html.Append("</li>");                    
                }
                jsonstr.Append("{\"HtmlStr\":\"");
                jsonstr.Append(EncodeByEscape.GetEscapeStr(html.ToString()));
                jsonstr.Append("\",\"TotalCount\":\"");
                jsonstr.Append(totalcount);
                jsonstr.Append("\"}");
            }
            return jsonstr.ToString();
        }
    }

    public class ThirdStyleDis : ResultDisplay
    {
        public override string Display()
        {
            int count = 0;
            StringBuilder html = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                foreach (IdolNewsEntity entity in list)
                {
                    var Articletitle = entity.Title;
                    html.Append("<li><div class=\"trainSelect\"><input  type=\"checkbox\" name=\"train_article_list\" id=\"article_" + entity.DocId + "\" pid=\"" + entity.Href + "\"/>&nbsp;</div>");

                    html.AppendFormat("<h2><a href=\"{0}\" title=\"{1}\" target=\"_blank\">", entity.Href, Articletitle);
                    html.AppendFormat("{0}</a></h2>", (Articletitle.Length > 27) ? Articletitle.Substring(0, 27) + "..." : Articletitle);
                    html.AppendFormat("<div class=\"d\"><span>{0}</span> - {1}</div>", entity.SiteName, entity.TimeStr);
                    html.AppendFormat("<p>{0}<b>...</b></p>", entity.Content);
                    html.Append("</li>");
                    if (count == list.Count - 1)
                    {
                        html.Replace('※', ' ');
                        html.Append("※" + entity.TotalCount.ToString());
                    }
                    count++;
                }
            }
            return html.ToString();
        }
    }

    public class ForthStyleDis : ResultDisplay
    {
        public override string Display()
        {
            string totalcount = "0";
            StringBuilder html = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                totalcount = list[0].TotalCount;
                
                foreach (IdolNewsEntity entity in list)
                {
                    html.AppendFormat("<div id=\"idol_article_{0}\">", entity.DocId);
                    html.AppendFormat("<div class=\"gw_news_title\"><a href=\"{0}\" target=\"_blank\">{1}</a><span>{2}</span><span>{3}</span></div>", entity.Href, entity.Title, entity.TimeStr, entity.SiteName);
                    html.AppendFormat("<div class=\"gw_news_text\">{0}</div>", entity.Content);
                    //html.AppendFormat("<a name=\"article_delete\" class=\"btn_delete\" pid=\"{0}\" href=\"javascript:void(null);\">删除</a>", entity.DocId);
                    html.Append("<div style=\"display: none; width:100%;\" name=\"doc_").Append(entity.DocId).Append("\" class=\"gw_news_info\"> <img border=\"0\"  src=\"img/info.gif\"></div><br/>");
                    html.AppendFormat("<div style=\"display: none; width:100%;\" name=\"doc_{0}\" id=\"suggest_{1}\"></div></div>", entity.DocId, entity.DocId);
                }
                html.Replace('※', ' ');
                html.Append("※" + totalcount);
            }
            return html.ToString();
        }
    }

    public class FifthStyleDIs : ResultDisplay
    {
        public override string Display()
        {
            string totalcount = "0";
            StringBuilder jsonstr = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                totalcount = list[0].TotalCount;
                StringBuilder html = new StringBuilder();
                foreach (IdolNewsEntity entity in list)
                {
                    html.Append("<li><h2><span id=\"sentiment_" + entity.DocId + "\">");
                    if(entity.Tag == "n"){
                        html.Append("【负面】&nbsp;&nbsp;");
                    }else if(entity.Tag == "p"){
                        html.Append("【正面】&nbsp;&nbsp;");
                    }else if(entity.Tag == "m"){
                        html.Append("【中立】&nbsp;&nbsp;");
                    }
                    html.AppendFormat("</span><a href=\"{0}\" title=\"{1}\" target=\"_blank\">", entity.Href, entity.Title);
                    html.AppendFormat("{0}</a></h2>", (entity.Title.Length > 20) ? entity.Title.Substring(0, 20) + "..." : entity.Title);
                    html.Append("<div class=\"d\"><span>&nbsp;&nbsp;" + entity.SiteName + "</span> - " + entity.TimeStr + "</div>");
                    html.AppendFormat("<p>{0}<b>...</b></p>", entity.Content);
                    html.Append("<div style=\"text-align:center; height:25px; line-height:25px;\"><span  name=\"comment_div\" style=\"display:none;\">");
                    html.AppendFormat("【<a href=\"javascript:void(null);\" pid=\"{0}\"  id=\"btn_design_bad\">设置为有害</a>】&nbsp;&nbsp;&nbsp;&nbsp;", entity.DocId);                    
                    html.AppendFormat("【<a href=\"javascript:void(null);\" pid=\"{0}\" id=\"btn_design_neutral\">删除该文章</a>】", entity.DocId);
                    html.Append("</span></div></li>");
                }
                jsonstr.Append("{\"HtmlStr\":\"");
                jsonstr.Append(EncodeByEscape.GetEscapeStr(html.ToString()));
                jsonstr.Append("\",\"TotalCount\":\"");
                jsonstr.Append(totalcount);
                jsonstr.Append("\"}");
            }
            return jsonstr.ToString();
        }
    }

    public class JsonStyleDis : ResultDisplay
    {
        public override string Display()
        {
            int count = 1;
            StringBuilder jsonstr = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                jsonstr.Append("{");
                string totalcount = list[0].TotalCount;
                jsonstr.AppendFormat("\"totalcount\":{0},", totalcount);
                foreach (IdolNewsEntity entity in list)
                {
                    jsonstr.AppendFormat("\"entity_{0}\":", count);
                    jsonstr.Append("{");
                    jsonstr.AppendFormat("\"title\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.Title));
                    jsonstr.AppendFormat("\"href\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.Href));
                    jsonstr.AppendFormat("\"time\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.TimeStr));
                    jsonstr.AppendFormat("\"site\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.SiteName));
                    jsonstr.AppendFormat("\"author\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.Author));
                    jsonstr.AppendFormat("\"replynum\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.ReplyNum));
                    jsonstr.AppendFormat("\"clicknum\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.ClickNum));
                    jsonstr.AppendFormat("\"docid\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.DocId));
                    jsonstr.AppendFormat("\"content\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.Content));
                    jsonstr.AppendFormat("\"allcontent\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.AllContent));
                    jsonstr.AppendFormat("\"conturl\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.ContUrl));
                    jsonstr.AppendFormat("\"samenum\":\"{0}\",", entity.SameNum);
                    jsonstr.AppendFormat("\"weight\":\"{0}\"", EncodeByEscape.GetEscapeStr(entity.weight));
                    jsonstr.Append("},");
                    count++;
                }
                jsonstr.Append("\"Success\":1}");
            }
            return jsonstr.ToString();
        }
    }

    public class SevenStyleDis : ResultDisplay
    {
        public override string Display()
        {

            Dictionary<string, string> tagDict = new Dictionary<string, string>();
            tagDict.Add("other", "");
            tagDict.Add("news", "n");
            tagDict.Add("comment", "c");
            tagDict.Add("p", "p");
            tagDict.Add("n", "n");
            tagDict.Add("m", "");
            string totalcount = "0";
            StringBuilder html = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                int count = 0;
                totalcount = list[0].TotalCount;
                
                foreach (IdolNewsEntity entity in list)
                {
                    string Tag = tagDict[entity.DocType] + tagDict[entity.Tag];
                    html.AppendFormat("<li><dl><dt><input id=\"" + entity.DocId + "\" type=\"checkbox\" class=\"selected_doc\" pid=\"{1}\" />{0}</dt><dd class=\"l_title\"><a href=\"{1}\" title=\"{2}\" target=\"_blank\">", entity.weight + "%", entity.Href, entity.Title);
                    html.AppendFormat("{0}</a></dd>", (entity.Title.Length > 27) ? entity.Title.Substring(0, 27) + "..." : entity.Title);
                    html.Append("<dd class=\"l_tag\"><span><font color='black'>" + Tag + "</font>&nbsp;&nbsp;" + entity.SiteName + "</span> - " + entity.TimeStr + "</dd>");
                    html.AppendFormat("<dd class=\"l_content\">{0}<b>...</b></dd>", entity.Content);
                    html.Append("</dl></li>");
                    if (count == list.Count - 1)
                    {
                        html.Replace('※', ' ');
                        html.Append("※" + totalcount.ToString());
                    }
                    count++;
                }
                
            }
            return html.ToString();
        }
    }
    /// <summary>
    /// 微博话题
    /// </summary>
    public class EightStyleDis : ResultDisplay
    {
        public override string Display()
        {
            int count = 1;
            StringBuilder jsonstr = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                jsonstr.Append("{");
                string totalcount = list[0].TotalCount;
                jsonstr.AppendFormat("\"totalcount\":{0},", totalcount);
                foreach (IdolNewsEntity entity in list)
                {
                    jsonstr.AppendFormat("\"entity_{0}\":", count);
                    jsonstr.Append("{");
                    jsonstr.AppendFormat("\"DISPLAYCONTENT\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.AllContent));
                    jsonstr.AppendFormat("\"SITENAME\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.SiteName));
                    jsonstr.AppendFormat("\"REPLYNUM\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.ReplyNum));
                    jsonstr.AppendFormat("\"TIMESTAMP\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.TimesTamp));
                    jsonstr.AppendFormat("\"AUTHORURL\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.AuthorURL));
                    jsonstr.AppendFormat("\"AUTHORNAME\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.AuthorName));
                    jsonstr.AppendFormat("\"FORWARDNUM\":\"{0}\"", EncodeByEscape.GetEscapeStr(entity.ForwardNum));
                    
                    jsonstr.Append("},");
                    count++;
                }
                jsonstr.Append("\"Success\":1}");
            }
            return jsonstr.ToString();
        }
    }
    public class DisplayFactory
    {
        public static ResultDisplay GetDisStyle(DisplayType disType)
        {
            switch (disType)
            {
                case DisplayType.FirstDisplay:
                    return new FirstStyleDis();
                case DisplayType.SecondDisplay:
                    return new SecondStyleDis();
                case DisplayType.ThirdStyleDis:
                    return new ThirdStyleDis();
                case DisplayType.ForthStyleDis:
                    return new ForthStyleDis();
                case DisplayType.FifthStyleDis:
                    return new FifthStyleDIs();
                case DisplayType.SixthStyleDis:
                    return new JsonStyleDis();
                case DisplayType.SevevStyleDis:
                    return new SevenStyleDis();
                case DisplayType.EightStyleDis:
                    return new EightStyleDis();
                default:
                    return null;
            }
        }
    }

    public enum DisplayType
    {
        FirstDisplay = 1,
        SecondDisplay = 2,
        ThirdStyleDis = 3,
        ForthStyleDis = 4,
        FifthStyleDis = 5,
        SixthStyleDis = 6,
        SevevStyleDis = 7,
        EightStyleDis = 8
    }
}
