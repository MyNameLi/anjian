using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dropthings.Data;
using Dropthings.Util;

namespace Dropthings.Business.Facade
{
    public class LexiconWordFacade
    {
        private static LEXICONWORDEntity.LEXICONWORDDAO dao = new LEXICONWORDEntity.LEXICONWORDDAO();

        public static IList<LEXICONWORDEntity> GetList(string where)
        {
            return dao.Find(where);
        }

        public static void DeleteById(int id)
        {
            dao.DeleteById(id);
        }

        public static void DeleteByTypeId(int TypeId)
        {
            dao.DeleteByTypeId(TypeId);
        }

        public static void UpDateWordReg(string value, int id)
        {
            dao.UpSetWordReg(value, id);
        }

        public static void UpDateWordWeight(int value, int id)
        {
            dao.UpSetWordWeight(value, id);
        }

        public static int Add(LEXICONWORDEntity entity)
        {
            return dao.AddEntity(entity);
        }

        public static string GetListJsonStr(string where)
        {
            IList<LEXICONWORDEntity> list = GetList(where);
            StringBuilder jsonstr = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                jsonstr.Append("{");
                int count = 0;
                foreach (LEXICONWORDEntity entity in list)
                {
                    jsonstr.AppendFormat("\"entity_{0}\":", count);
                    jsonstr.Append("{");
                    jsonstr.AppendFormat("\"id\":{0},", entity.ID);
                    jsonstr.AppendFormat("\"word\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.WORD));
                    jsonstr.AppendFormat("\"weight\":\"{0}\",", entity.WEIGHT);
                    jsonstr.AppendFormat("\"typeid\":\"{0}\"", entity.TYPEID);
                    jsonstr.Append("},");
                    count++;
                }
                jsonstr.Append("\"SuccessCode\":1}");
            }
            return jsonstr.ToString();
        }

        public static string GetListJsonStr(string where, bool backbone)
        {
            IList<LEXICONWORDEntity> list = GetList(where);
            StringBuilder jsonstr = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                jsonstr.Append("[");
                int count = 0;
                foreach (LEXICONWORDEntity entity in list)
                {

                    jsonstr.Append("{");
                    jsonstr.AppendFormat("\"id\":{0},", entity.ID);
                    jsonstr.AppendFormat("\"word\":\"{0}\",", entity.WORD);
                    jsonstr.AppendFormat("\"weight\":\"{0}\",", entity.WEIGHT);
                    jsonstr.AppendFormat("\"typeid\":\"{0}\"", entity.TYPEID);
                    jsonstr.Append("},");
                    count++;
                }
                jsonstr.Remove(jsonstr.Length - 1, 1);
                jsonstr.Append("]");
            }
            // string result=jsonstr.ToString();
            return jsonstr.ToString();//result.Remove(result.Length - 3, 1);
        }


    }
}
