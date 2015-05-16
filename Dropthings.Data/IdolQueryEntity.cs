using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using IdolACINet;
using System.Configuration;

namespace Dropthings.Data
{
    public class IdolQueryEntity
    {
        public string DocId
        {
            set;
            get;
        }
        public string Title
        {
            set;
            get;
        }
        public string Href
        {
            set;
            get;
        }
        public double Weight
        {
            set;
            get;
        }

        public string TotalHits
        {
            set;
            get;
        }

        public class IdolQueryEntityDao
        {
            private Connection conn = new Connection(ConfigurationManager.AppSettings["IdolHttp"], 9000);
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
            public IList<IdolQueryEntity> GetNewsList(XmlDocument xmldoc)
            {
                IList<IdolQueryEntity> NewsList = new List<IdolQueryEntity>();
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
                nsmgr.AddNamespace("autn", "http://schemas.autonomy.com/aci/");
                XmlNodeList NodeList = xmldoc.SelectNodes("autnresponse/responsedata/autn:hit", nsmgr);
                foreach (XmlNode node in NodeList)
                {
                    IdolQueryEntity entity = new IdolQueryEntity();
                    entity.DocId = GetNodeText(node.SelectSingleNode("autn:id", nsmgr));
                    entity.Title = GetNodeText(node.SelectSingleNode("autn:title", nsmgr));
                    entity.Href = GetNodeText(node.SelectSingleNode("autn:reference", nsmgr));
                    entity.Weight = Convert.ToDouble(GetNodeText(node.SelectSingleNode("autn:weight",nsmgr)));
                    NewsList.Add(entity);
                }                
                return NewsList;                
            }


            public Queue<IdolQueryEntity> GetQueueList(XmlDocument xmldoc)
            {
                Queue<IdolQueryEntity> NewsList = new Queue<IdolQueryEntity>();
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
                nsmgr.AddNamespace("autn", "http://schemas.autonomy.com/aci/");
                XmlNodeList NodeList = xmldoc.SelectNodes("autnresponse/responsedata/autn:hit", nsmgr);
                foreach (XmlNode node in NodeList)
                {
                    IdolQueryEntity entity = new IdolQueryEntity();
                    entity.DocId = GetNodeText(node.SelectSingleNode("autn:id", nsmgr));
                    entity.Title = GetNodeText(node.SelectSingleNode("autn:title", nsmgr));
                    entity.Href = GetNodeText(node.SelectSingleNode("autn:reference", nsmgr));
                    entity.Weight = Convert.ToDouble(GetNodeText(node.SelectSingleNode("autn:weight", nsmgr)));
                    NewsList.Enqueue(entity);
                }
                return NewsList;
            }
        }
    }
}
