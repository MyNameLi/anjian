using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dropthings.Data;

namespace Dropthings.Business.Facade
{
    public class IdolNewQuery
    {

        public IList<IdolNewsEntity> NewsList = new List<IdolNewsEntity>();

       
        public IdolNewQuery(IList<IdolNewsEntity> list)
        {
            NewsList = list;
        }

        public Dictionary<string, int> GetTopNewsBySiteName(int top)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (IdolNewsEntity entity in NewsList)
            {
                if (!string.IsNullOrEmpty(entity.SiteName))
                {
                    if (dict.Keys.Contains(entity.SiteName))
                    {
                        dict[entity.SiteName] += 1;
                    }
                    else
                    {
                        dict[entity.SiteName] = 1;
                    }
                }
            }
            var dicOrderValue = dict.OrderByDescending(c => c.Value);
            IEnumerable<KeyValuePair<string, int>> result = dicOrderValue.Take(top);
            Dictionary<string, int> resultdict = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> item in result)
            {
                resultdict.Add(item.Key, item.Value);
            }
            return resultdict;
        }

        public IList<IdolNewsEntity> GetNewsByType(string type)
        {
            IList<IdolNewsEntity> list = new List<IdolNewsEntity>();
            foreach (IdolNewsEntity entity in NewsList)
            {
                if (entity.DocType == type)
                    list.Add(entity);
            }
            return list;
        }

        public IList<IdolNewsEntity> GetNewsByTag(string tag)
        {
            IList<IdolNewsEntity> list = new List<IdolNewsEntity>();
            foreach (IdolNewsEntity entity in NewsList)
            {
                if (entity.Tag == tag)
                    list.Add(entity);
            }
            return list;
        }

        public IList<IdolNewsEntity> GetNews(string type, string tag)
        {
            IList<IdolNewsEntity> list = new List<IdolNewsEntity>();
            foreach (IdolNewsEntity entity in NewsList)
            {
                if (entity.DocType == type && entity.Tag == tag)
                    list.Add(entity);
            }
            return list;
        }

        public Dictionary<string, IList<IdolNewsEntity>> GetNewsGroupByCluster(int count)
        {
            Dictionary<string, IList<IdolNewsEntity>> dict = new Dictionary<string, IList<IdolNewsEntity>>();
            foreach (IdolNewsEntity entity in NewsList)
            {
                if (dict.Count > count)
                    break;
                if (!string.IsNullOrEmpty(entity.ClusterId))
                {
                    if (dict.Keys.Contains(entity.ClusterId))
                    {
                        dict[entity.ClusterId].Add(entity);
                    }
                    else
                    {
                        IList<IdolNewsEntity> list = new List<IdolNewsEntity>();
                        list.Add(entity);
                        dict.Add(entity.ClusterId, list);
                    }
                }                
            }
            return dict;
        }
    }
}
