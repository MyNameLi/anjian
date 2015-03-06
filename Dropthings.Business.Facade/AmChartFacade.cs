using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dropthings.Business.Facade
{
	public class AmChartFacade
	{
        public static string GetPieXmlStr(Dictionary<string, string> dict)
        {
            StringBuilder xmlstr = new StringBuilder();
            if (dict != null && dict.Keys.Count > 0)
            {
                xmlstr.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><pie>");
                int count = 1;
                foreach (string key in dict.Keys)
                {
                    xmlstr.AppendFormat("<slice title=\"{0}\" color=\"{1}\">{2}</slice>", key , AmChartColor.GetColor(count), dict[key]);
                    count++;
                }
                xmlstr.Append("</pie>");
            }
            return xmlstr.ToString();
        }

        public static string GetISVIPPieXmlStr(Dictionary<string, string> dict)
        {
            StringBuilder xmlstr = new StringBuilder();
            if (dict != null && dict.Keys.Count > 0)
            {
                xmlstr.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><pie>");
                int count = 1;

                foreach (string key in dict.Keys)
                {
                    xmlstr.AppendFormat("<slice title=\"{0}\" color=\"{1}\">{2}</slice>", key == "0" ? "非加V" : "加V", AmChartColor.GetColor(count), dict[key]);
                    count++;
                }
                xmlstr.Append("</pie>");
            }
            return xmlstr.ToString();
        }
        public static string GetPROVINCEPieXmlStr(string caption, string xAxisName, string yAxisName, Dictionary<string, string> dict)
        {
            StringBuilder xmlstr = new StringBuilder();
            if (dict != null && dict.Keys.Count > 0)
            {
                xmlstr.AppendFormat("<graph caption='{0}' xAxisName='{1}' yAxisName='{2}'", caption, xAxisName, yAxisName);
                xmlstr.Append("baseFontSize='12' decimalPrecision='0' formatNumberScale='0'>");
                int count = 0;
              //  int totalcount = 0;
                foreach (string key in dict.Keys)
                {
                    if (count != 10)
                    {
                        xmlstr.AppendFormat("<set name='{0}' value='{1}' color='{2}' />", key, dict[key], AmChartColor.GetColumnColor(count));
                        
                    }
                    else
                    {
                        break;
                       //xmlstr.AppendFormat("<slice title=\"{0}\" color=\"{1}\">{2}</slice>", key, AmChartColor.GetColor(count), dict[key]);
                    }
                    count++;
                }
                //xmlstr.AppendFormat("<slice title=\"其他\" color=\"{0}\">{1}</slice>", AmChartColor.GetColor(count), totalcount);
                xmlstr.Append("</graph>");
            }
            return xmlstr.ToString();
        }
        //private static string abcd(string caption, string xAxisName, string yAxisName, Dictionary<string, string> datadict)
        //{
        //    StringBuilder xmlstr = new StringBuilder();
        //    if (datadict != null && datadict.Keys.Count > 0)
        //    {
        //        xmlstr.AppendFormat("<graph caption='{0}' xAxisName='{1}' yAxisName='{2}'", caption, xAxisName, yAxisName);
        //        xmlstr.Append("baseFontSize='12' decimalPrecision='0' formatNumberScale='0'>");
        //        int index = 0;
        //        foreach (string key in datadict.Keys)
        //        {
        //            xmlstr.AppendFormat("<set name='{0}' value='{1}' color='{2}' />", key, datadict[key], AmChartColor.GetColumnColor(index));
        //            index++;
        //        }
        //        xmlstr.Append("</graph>");
        //    }
        //    return xmlstr.ToString();
        //}
        public static string GetTrendDataStr(Dictionary<string, string> dict)
        {
            StringBuilder datastr = new StringBuilder();
            if (dict.Keys.Count > 0)
            {
                foreach (string key in dict.Keys)
                {
                    datastr.AppendFormat("{0},{1},{1}", key, dict[key], dict[key]);
                    datastr.Append("\r\n");
                }
            }
            return datastr.ToString();
        }

        public static Dictionary<string, string> GetRequestParamDict(string info)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(info))
            {
                string[] allinfo = info.Split('|');
                foreach (string key in allinfo)
                {
                    string[] data = key.Split('=');
                    string paramskey = data[0];
                    string paramsvalue = data[1];
                    if (!dict.ContainsKey(paramskey))
                    {
                        dict.Add(paramskey, paramsvalue);
                    }
                }
            }
            return dict;
        }
	}
}
