using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using IdolACINet;
using System.Configuration;
using Dropthings.Util;

namespace Dropthings.Data
{
    public class IdolNewsEntity
    {       
        public string Href
        {
            set;
            get;
        }

        public string DocId
        {
            get;
            set;
        }

        public string Content
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string SiteName
        {
            get;
            set;
        }

        public string TimeStr
        {
            get;
            set;
        }

        public string weight
        {
            get;
            set;
        }

        public string DocType
        {
            get;
            set;
        }

        public string Tag
        {
            get;
            set;
        }

        public string TotalCount
        {
            get;
            set;
        }

        public string ClusterId
        {
            get;
            set;
        }

        public string ClusterTitle
        {
            get;
            set;
        }

        public string ReplyNum
        {
            get;
            set;
        }

        public string ClickNum
        {
            get;
            set;
        }

        public string ShowContent
        {
            get;
            set;
        }
        public string AllContent
        {
            get;
            set;
        }

        public string Columns
        {
            get;
            set;
        }

        public string HotColumns
        {
            get;
            set;
        }

        public string SiteType
        {
            get;
            set;
        }

        public class IdolNewsDao
        {
            public IList<IdolNewsEntity> GetNewsList(XmlDocument xmldoc)
            {
                IList<IdolNewsEntity> newsList = new List<IdolNewsEntity>();
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
                nsmgr.AddNamespace("autn", "http://schemas.autonomy.com/aci/");
                XmlNodeList hits = xmldoc.SelectNodes("//autn:hit", nsmgr);
                foreach (XmlNode hit in hits)
                {
                    IdolNewsEntity entity = new IdolNewsEntity();
                    entity.TotalCount = GetArticleCount(xmldoc);
                    entity.DocId = GetNodeText(hit.SelectSingleNode("autn:id", nsmgr));
                    entity.Content = GetNodeText(hit.SelectSingleNode("autn:summary", nsmgr));
                    entity.weight = GetNodeText(hit.SelectSingleNode("autn:weight", nsmgr));
                    entity.Href = GetNodeText(hit.SelectSingleNode("autn:reference", nsmgr));
                    entity.ClusterId = GetNodeText(hit.SelectSingleNode("autn:cluster", nsmgr));
                    entity.ClusterTitle = GetNodeText(hit.SelectSingleNode("autn:clustertitle", nsmgr));
                    entity.Title = GetNodeText(hit.SelectSingleNode("autn:title", nsmgr));
                    XmlNode document = hit.SelectSingleNode("autn:content/DOCUMENT", nsmgr);
                    if (document != null)
                    {
                        string doctype = GetNodeText(document.SelectSingleNode("MYSRCTYPE"));
                        string doctag = GetNodeText(document.SelectSingleNode("SENTIMENT"));
                        entity.ClickNum = GetNodeText(document.SelectSingleNode("READNUM"));
                        entity.ReplyNum = GetNodeText(document.SelectSingleNode("REPLYNUM"));
                        entity.SiteName = GetSiteName(GetNodeText(document.SelectSingleNode("MYSITENAME")), GetNodeText(document.SelectSingleNode("DOMAINSITENAME")));
                        entity.TimeStr = TimeHelp.ConvertToDateTimeString(GetNodeText(document.SelectSingleNode("DREDATE")));
                        entity.AllContent = GetNodeText(document.SelectSingleNode("DRECONTENT"));
                        entity.ShowContent = GetNodeText(document.SelectSingleNode("DISPLAYCONTENT"));
                        entity.DocType = doctype == null ? "other" : doctype;
                        entity.Tag = doctag == null ? "other" : doctag;
                        entity.Columns = GetNodeText(document.SelectNodes("D1"));
                        entity.HotColumns = GetNodeText(document.SelectNodes("D2"));
                        entity.SiteType = GetNodeText(document.SelectNodes("C1"));
                    }
                    newsList.Add(entity);
                }
                return newsList;
            }
            private string GetSiteName(string a, string b)
            {
                if (!string.IsNullOrEmpty(a))
                {
                    return a;
                }
                return b;
            }

            public string GetArticleCount(XmlDocument xmldoc)
            { 
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
                nsmgr.AddNamespace("autn", "http://schemas.autonomy.com/aci/");
                string totalCount = "0";
                XmlNode totalCountElement = xmldoc.SelectSingleNode("//autn:totalhits",nsmgr);
                XmlNode MaxResult = xmldoc.SelectSingleNode("//dremaxresults");
                if (MaxResult != null)
                {
                    totalCount = GetMaxCount(totalCountElement, MaxResult);
                }
                else
                {
                    totalCount = GetNodeText(totalCountElement);
                }
                return totalCount;
            }

            private String GetMaxCount(XmlNode node1, XmlNode node2)
            {
                int value1 = Convert.ToInt32(GetNodeText(node1));
                int value2 = Convert.ToInt32(GetNodeText(node2));
                int comparevalue = value1 <= value2 ? value1 : value2;
                return comparevalue.ToString();
            }

            public IList<IdolNewsEntity> GetPagerList(string action, Dictionary<string, string> dict)
            {
                XmlDocument xmldoc = GetXmlDoc(action, dict);
                if (xmldoc != null)
                {
                    return GetNewsList(xmldoc);
                }
                else
                {
                    return null;
                }
            }

            public XmlDocument GetXmlDoc(string action, Dictionary<string, string> dict)
            {               
                Connection conn = new Connection(ConfigurationManager.AppSettings["IdolHttp"], 9000);
                Command query = new Command(action); 
                if (dict != null)
                {
                    foreach (string key in dict.Keys)
                    {
                        query.SetParam(key, dict[key]);
                    }
                }
                return conn.Execute(query).Data;
            }

            private string GetNodeText(XmlNode node)
            {
                try
                {
                    return node.InnerText;
                }
                catch
                {
                    return null;
                }
            }

            private string GetNodeText(XmlNodeList nodelist)
            {
                StringBuilder vallist = new StringBuilder();
                if (nodelist != null && nodelist.Count > 0) {
                    foreach (XmlNode node in nodelist) {
                        if (vallist.Length > 0) {
                            vallist.Append(",");
                        }
                        vallist.Append(GetNodeText(node));
                    }
                }
                return vallist.ToString();
            }
        }

        
    }
}
