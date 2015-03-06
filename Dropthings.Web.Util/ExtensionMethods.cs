using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Dropthings.Web.Util
{
    public static class ExtensionMethods
    {
        public static string ToJson(this object o, int recursionLimit)
        {
            var serializer = new JavaScriptSerializer();
            serializer.RecursionLimit = recursionLimit;
            return serializer.Serialize(o);
        }
        public static string ToJson(this object o)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(o);
        }
        /// <summary>
        /// 将字符串格式化为JSON兼容的格式
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <returns>JSON兼容格式的字符串</returns>
        public static string ToJson(this string str)
        {

            StringBuilder sb = new StringBuilder(str.Length + 20);
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '/':
                        sb.Append("\\/");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }
    }
}
