using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dropthings.Data;
using Dropthings.Util;
using System.Text;
using Dropthings.Business.Facade;
using System.Data;
using System.Configuration;
using IdolACINet;
using System.Xml;

public partial class Admin_clusters_ClusterList : System.Web.UI.Page
{
    protected static IdolNewsEntity.IdolNewsDao IdolNewsDao = new IdolNewsEntity.IdolNewsDao();    
    protected static Connection cnn = new Connection(ConfigurationManager.AppSettings["IdolHttp"], 9001);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) {
            Ajax();
        }
    }

    protected void Ajax()
    {
        if (Request["ajaxString"] == "1")
        {
            Dictionary<string, string> Paramas = new Dictionary<string, string>();
            foreach (string key in Request.Form.AllKeys)
            {
                Paramas.Add(key, EncodeByEscape.GetUnEscapeStr(Request[key]));
            }
            string res = string.Empty;
            string idlist = Request["idList"];
            string act = Request["act"];
            try
            {
                switch (act)
                {
                    case "getclusterlist":
                        string strwhere = Paramas["strwhere"];
                        string strorder = Paramas["orderby"];
                        int Start = Convert.ToInt32(Paramas["start"]);
                        int PageSize = Convert.ToInt32(Paramas["page_size"]);
                        res = ClusterListFacade.GetPagerJsonStr(strwhere, strorder, PageSize, Start);                        
                        break;
                    case "editdistype":
                        string val = Paramas["status"];
                        if (ClusterListFacade.UpSetDisType(Convert.ToInt32(idlist), Convert.ToInt32(val)))
                        {
                            res = "{\"Success\":1}";
                        }
                        else {
                            res = "{\"Error\":1}";
                        }                        
                        break;
                    case "editparam":
                        string paramval = Paramas["val"];
                        if (ClusterListFacade.UpSetParam(Convert.ToInt32(idlist), Convert.ToInt32(paramval)))
                        {
                            res = "{\"Success\":1}";
                        }
                        else {
                            res = "{\"Error\":1}";
                        }                        
                        break;
                    case "editclustername":
                        string clustername = Paramas["val"];
                        if (ClusterListFacade.UpSetClusterName(Convert.ToInt32(idlist), clustername))
                        {
                            res = "{\"Success\":1}";
                        }
                        else {
                            res = "{\"Error\":1}";
                        }                        
                        break;
                    case "remove":
                        if (ClusterListFacade.delete(Convert.ToInt32(idlist)))
                        {                            
                            ClusterInfoFacade.Delete(Convert.ToInt32(idlist));
                            res = "{\"Success\":1}";
                        }                        
                        break;
                    case "removeall":
                        if (ClusterListFacade.delete(idlist))
                        {
                            ClusterInfoFacade.Delete(idlist);
                            res = "{\"Success\":1}";
                        }          
                        break;
                    case "initJobList":
                        Dictionary<string, string> joblist = ClusterResultFacade.GetJobList();
                        StringBuilder backstr = new StringBuilder();
                        if (joblist.Count > 0)
                        {
                            backstr.Append("{");
                            foreach (string job in joblist.Keys)
                            {                                
                                string jobname = job.Split('-')[0];
                                string jobtime = job.Split('-')[1];
                                string timespan = joblist[job];
                                string disjobtime = jobname + "(" + TimeHelp.ConvertToActualTimeString(jobtime, timespan).Substring(0, 10) + ")";
                                backstr.AppendFormat("\"{0}\":\"{1}\",", EncodeByEscape.GetEscapeStr(job), EncodeByEscape.GetEscapeStr(disjobtime));
                            }
                            backstr.Append("\"Success\":1}");
                        }
                        res = backstr.ToString();
                        break;
                    case "initClusterIdList":
                        string JobName = Paramas["jobname"];
                        Dictionary<string,string> ClusterIdlist = ClusterResultFacade.GetClusterIdList(JobName);
                        StringBuilder idbackstr = new StringBuilder();
                        if (ClusterIdlist.Keys.Count > 0)
                        {
                            int count = 1;
                            idbackstr.Append("{");
                            foreach (string cluserid in ClusterIdlist.Keys)
                            {
                                idbackstr.AppendFormat("\"entity_{0}\":", count);
                                idbackstr.Append("{");
                                idbackstr.AppendFormat("\"clusterid\":\"{0}\",", cluserid);
                                idbackstr.AppendFormat("\"title\":\"{0}\"", EncodeByEscape.GetEscapeStr(ClusterIdlist[cluserid]));
                                idbackstr.Append("},");
                                count++;
                            }
                            idbackstr.Append("\"Success\":1}");
                        }
                        res = idbackstr.ToString();
                        break;
                    case "initEdit":
                        CLUSTERLISTEntity EditEntity = ClusterListFacade.FindEntity(Convert.ToInt64(idlist));
                        res = GetJsonStr(EditEntity);
                        break;
                    case "EditOne":
                        CLUSTERLISTEntity Lentity = ClusterListFacade.FindEntity(Convert.ToInt64(Paramas["ID"]));
                        Lentity.CLUSTERNAME = Paramas["clustername"];
                        string editdistype = Paramas["distype"];
                        Lentity.DISTYPE = Convert.ToInt32(editdistype);
                        string param = Paramas["param"];
                        if (!string.IsNullOrEmpty(param))
                        {
                            Lentity.PARAM = Convert.ToInt32(param);
                        }
                        else
                        {
                            Lentity.PARAM = 0;
                        }
                        Lentity.EDITDATE = DateTime.Now;
                        ClusterListFacade.UpDate(Lentity);
                        res = "{\"Success\":1}";
                        break;
                    case "Add":
                        CLUSTERLISTEntity entity = new CLUSTERLISTEntity();
                        entity.CLUSTERNAME = Paramas["clustername"];
                        entity.DISTYPE = Convert.ToInt32(Paramas["distype"]);
                        string addparam = Paramas["param"];
                        if (!string.IsNullOrEmpty(addparam))
                        {
                            entity.PARAM = Convert.ToInt32(addparam);
                        }
                        else
                        {
                            entity.PARAM = 0;
                        }
                        entity.EDITDATE = DateTime.Now;
                        ClusterListFacade.add(entity);
                        res = "{\"Success\":1}";
                        break;
                    case "initclusterinfo":
                        string clusterid = Paramas["clusterid"];
                        res = ClusterInfoFacade.GetListStr(Convert.ToInt32(clusterid));
                        break;
                    case "pushclusterinfo":
                        string pushclusterid = Paramas["clusterid"];
                        string[] urllist = Paramas["urllist"].Split('+');
                        string[] typelist = Paramas["typelist"].Split('+');
                        ClusterInfoFacade.Delete(pushclusterid);
                        PushClusterInfo(pushclusterid, urllist, typelist);
                        res = "{\"Success\":1}";
                        break;
                    case "restoreclusterlist":
                        ClusterListFacade.ReStore();
                        res = "{\"Success\":1}";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                res = "{\"Error\":1,\"ErrorStr\":\"" + ex.ToString() + "\"}";
            }
            finally
            {
                Response.Write(res);
                Response.End();
            }
        }

    }
    protected string GetJsonStr(CLUSTERLISTEntity EditEntity)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "clustername", EncodeByEscape.GetEscapeStr(EditEntity.CLUSTERNAME));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "distype", EditEntity.DISTYPE);
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "param", EditEntity.PARAM);
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();
    }

    protected void PushClusterInfo(string clusterid, string[] urllist, string[] typelist) {
        Dictionary<string, string> urltypedict = new Dictionary<string, string>();
        for (int i = 0, j = urllist.Length; i < j; i++) { 
            string key = urllist[i];
            string val = typelist[i];
            if (!urltypedict.ContainsKey(key)) {
                urltypedict.Add(key, val);
            }
        }
        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("text", "*");
        dict.Add("MatchReference", UrlEncode.GetIdolQueryUrlStr(urllist));
        dict.Add("print", "all");
        dict.Add("Start", "1");
        dict.Add(QueryParams.MaxResults, urllist.Length.ToString());
        dict.Add("databasematch", ConfigurationManager.AppSettings["DATABASE"]);
        dict.Add("Combine", "DREREFERENCE");
        XmlDocument xmldoc = IdolNewsDao.GetXmlDoc("query", dict);
        if (xmldoc != null)
        {
            IList<IdolNewsEntity> newslist = IdolNewsDao.GetNewsList(xmldoc);
            if (newslist.Count > 0)
            {
                foreach (IdolNewsEntity entity in newslist)
                {
                    string href = entity.Href;
                    string timestr = entity.TimeStr;
                    CLUSTERINFOEntity infoentity = new CLUSTERINFOEntity();
                    infoentity.CLUSTERID = Convert.ToInt32(clusterid);
                    infoentity.URL = href;
                    infoentity.TITLE = entity.Title;
                    infoentity.SITE = entity.SiteName;
                    if (!string.IsNullOrEmpty(timestr))
                    {
                        infoentity.BASEDATE = Convert.ToDateTime(timestr);
                    }
                    else {
                        infoentity.BASEDATE = DateTime.Now;
                    }
                    if (urltypedict.ContainsKey(href))
                    {
                        infoentity.TAG = Convert.ToInt32(urltypedict[href]);
                    }
                    else {
                        infoentity.TAG = 0;
                    }
                    ClusterInfoFacade.add(infoentity);                    
                }
            }
        }
    }
}
