using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dropthings.Data;

namespace Dropthings.Business.Facade
{
    public class LexiconTypeFacade
    {
        private static LEXICONTYPEEntity.LEXICONTYPEDAO dao = new LEXICONTYPEEntity.LEXICONTYPEDAO();

        public static IList<LEXICONTYPEEntity> GetList(string where)
        {
            return dao.Find(where);
        }

        public static LEXICONTYPEEntity FindById(long id)
        {
            return dao.FindById(id);
        }

        public static bool Delete(int id) {
            try
            {
                dao.Delete(id);
                return true;
            }
            catch {
                return false;
            }
        }

        public static void UpDate(LEXICONTYPEEntity entity)
        {
            dao.Update(entity);
        }

        public static void Add(LEXICONTYPEEntity entity)
        {
            dao.Add(entity);
        }

        public static int AddEntity(LEXICONTYPEEntity entity)
        {
            return dao.AddEntity(entity);
        }

        public static string getTreeStr(string where, int rootid)
        {
            StringBuilder treestr = new StringBuilder();
            IList<LEXICONTYPEEntity> list = GetList(where);
            if (list.Count > 0)
            {
                treestr.Append("[");
                foreach (LEXICONTYPEEntity entity in list)
                {
                    int parentid = entity.PARENTID.Value;
                    if (parentid == rootid)
                    {
                        treestr.Append("{");
                        treestr.AppendFormat("\"name\":\"{0}\",", entity.TYPENAME);
                        treestr.AppendFormat("\"id\":\"{0}\",", entity.ID);
                        getchildstr(list, entity.ID.Value, ref treestr);
                        treestr.Append("\"successcode\":1},");
                    }
                }
                treestr.Append("]");
            }
            string backstr = treestr.ToString();
            backstr = backstr.Replace(",\"successcode\":1", "");
            //backstr = backstr.Replace(",nodes:[]", "");
            backstr = backstr.Replace(",]", "]");
            return backstr;
        }

        private static void getchildstr(IList<LEXICONTYPEEntity> list, int parentid, ref StringBuilder treestr)
        {
            treestr.Append("nodes:[");
            foreach (LEXICONTYPEEntity entity in list)
            {
                if (entity.PARENTID.Value == parentid)
                {
                    treestr.Append("{");
                    treestr.AppendFormat("\"name\":\"{0}\",", entity.TYPENAME);
                    treestr.AppendFormat("\"id\":\"{0}\",", entity.ID);
                    getchildstr(list, entity.ID.Value, ref treestr);
                    treestr.Append("\"successcode\":1},");
                }
            }
            treestr.Append("],");
        }


        public static string getSelectStr(string where, int rootid)
        {
            StringBuilder treestr = new StringBuilder();
            treestr.Append("<option value=\"0\">—根目录—</option>");
            IList<LEXICONTYPEEntity> list = GetList(where);
            if (list.Count > 0)
            {

                foreach (LEXICONTYPEEntity entity in list)
                {
                    int parentid = entity.PARENTID.Value;
                    if (parentid == rootid)
                    {
                        treestr.AppendFormat("<option value=\"{0}\">{1}</option>", entity.ID, entity.TYPENAME);                   
                        getSelectchildstr(list, entity.ID.Value, ref treestr,1);                      
                    }
                }
                
            }
            string backstr = treestr.ToString();            
            return backstr;
        }

        private static void getSelectchildstr(IList<LEXICONTYPEEntity> list, int parentid, ref StringBuilder treestr, int level)
        {
            foreach (LEXICONTYPEEntity entity in list)
            {
                if (entity.PARENTID.Value == parentid)
                {
                    treestr.AppendFormat("<option value=\"{0}\">{1}{2}</option>", entity.ID, GetSpanStr(level), entity.TYPENAME);
                    getSelectchildstr(list, entity.ID.Value, ref treestr,level + 1);                    
                }
            }            
        }

        private static string GetSpanStr(int level) {
            StringBuilder spanStr = new StringBuilder();
            for (int i = 0; i < level; i++) {
                spanStr.Append("　");
            }
            return spanStr.ToString();
        }

    }
}
