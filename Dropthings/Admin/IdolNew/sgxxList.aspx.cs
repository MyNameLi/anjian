using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dropthings.Data;
using Dropthings.Business.Facade;
using Dropthings.Util;
using System.Text;
using System.Configuration;
using System.Xml;
using IdolACINet;
using System.Threading;

public partial class Admin_IdolNew_sgxxList : System.Web.UI.Page
{
    protected static IdolNewsEntity.IdolNewsDao IdolNewsDao = new IdolNewsEntity.IdolNewsDao();
    protected static ARTICLEEntity.ARTICLEDAO articleDao = new ARTICLEEntity.ARTICLEDAO();
    protected static Connection cnn = new Connection(ConfigurationManager.AppSettings["IdolHttp"], 9001);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
            string act = Request["act"];
            string idList = Request["idList"];
            string val = Request["val"];
            try
            {
                switch (act)
                {
                    case "store":
                        string baseurl = Paramas["url"];
                        string basewebsitelist = Paramas["siteidlist"];
                        string columnid = Paramas["columnid"];
                        string[] urllist = baseurl.Split('+');
                        string[] sitelist = basewebsitelist.Split(',');
                        //for (int i = 0; i < urllist.Length; i++)
                        //{
                        //string[] list = new string[1] { urllist[i] };
                        string posturl = UrlEncode.GetIdolQueryUrlStr(urllist);
                        StoreNews(posturl, sitelist, columnid);
                        //}
                        res = "{\"Success\":1}";
                        break;
                    case "innitwebsite":
                        IList<WEBSITELISTEntity> websitelist = WebSiteListFacade.Find();
                        res = GetWebSiteJson(websitelist);
                        break;
                    case "innitsitecolumn":
                        string siteid = Paramas["siteid"];
                        IList<COLUMNDEFEntity> columnlist = ColumnFacade.GetListBySiteId(Convert.ToInt32(siteid), 200);
                        StringBuilder optionstr = new StringBuilder();
                        GetSelectHtml(columnlist, 200, 0, ref optionstr);
                        res = "{\"optionstr\":\"" + EncodeByEscape.GetEscapeStr(optionstr.ToString()) + "\",\"Success\":1}";
                        break;
                    case "innitcategory":
                        int parentcate = Convert.ToInt32(Paramas["parentcate"]);
                        IList<CATEGORYEntity> categorylist = CategoryFacade.GetCategoryEntityList("");
                        if (categorylist.Count > 0)
                        {
                            res = GetSelectJsonStr(categorylist, parentcate);
                        }
                        break;
                    case "newsdelete":
                        string l_baseurl = Paramas["url"];
                        string[] l_urllist = l_baseurl.Split('+');
                        StringBuilder l_posturl = new StringBuilder();
                        foreach (string l_url in l_urllist)
                        {
                            if (l_posturl.Length > 0)
                            {
                                l_posturl.Append("+");
                            }
                            l_posturl.Append(HttpUtility.UrlEncode(l_url, Encoding.UTF8));
                        }
                        ChangeDataBase(l_posturl.ToString());
                        res = "{\"Success\":1}";
                        break;
                    case "getcolumnname":
                        string articleurllist = Paramas["url_list"];
                        res = ArticleFacade.GetArticleColumnJsonStr(articleurllist, 200);
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
    protected void GetSelectHtml(IList<COLUMNDEFEntity> list, int parent, int level, ref StringBuilder optionstr)
    {
        foreach (COLUMNDEFEntity entity in list)
        {
            if (entity.PARENTID.Value == parent)
            {
                string ItemName = HttpUtility.HtmlDecode(GetLevelStr(level) + entity.COLUMNNAME);
                optionstr.AppendFormat("<option value=\"{0}\">{1}</option>", entity.ID, ItemName);
                GetSelectHtml(list, entity.ID.Value, level + 1, ref optionstr);
            }
        }
    }

    protected string GetSelectJsonStr(IList<CATEGORYEntity> list, int parent)
    {
        StringBuilder jsonstr = new StringBuilder();
        jsonstr.Append("{");
        int count = 1;
        foreach (CATEGORYEntity entity in list)
        {
            if (entity.PARENTCATE.Value == parent)
            {
                //string ItemName = HttpUtility.HtmlDecode(GetLevelStr(level) + entity.CATEGORYNAME);
                jsonstr.AppendFormat("\"entity_{0}\":", count);
                jsonstr.Append("{");
                jsonstr.AppendFormat("\"categoryid\":\"{0}\",", entity.CATEGORYID);
                jsonstr.AppendFormat("\"categoryname\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.CATEGORYNAME));
                jsonstr.AppendFormat("\"id\":\"{0}\"", entity.ID);
                jsonstr.Append("},");
                count++;
            }
        }
        jsonstr.Append("\"Success\":1}");
        return jsonstr.ToString();
    }

    private string GetLevelStr(int level)
    {
        StringBuilder str = new StringBuilder();
        for (var i = 0; i <= level; i++)
        {
            str.AppendFormat("{0}", "&nbsp;");
        }
        return str.ToString();
    }

    private string GetWebSiteJson(IList<WEBSITELISTEntity> websitelist)
    {
        StringBuilder jsonstr = new StringBuilder();
        if (websitelist.Count > 0)
        {
            jsonstr.Append("{");
            int count = 1;
            foreach (WEBSITELISTEntity entity in websitelist)
            {
                jsonstr.AppendFormat("\"entity_{0}\":", count);
                jsonstr.Append("{");
                jsonstr.AppendFormat("\"SiteId\":\"{0}\",", entity.ID);
                jsonstr.AppendFormat("\"SiteName\":\"{0}\"", EncodeByEscape.GetEscapeStr(entity.SITENAME));
                jsonstr.Append("},");
                count++;
            }
            jsonstr.Append("\"Success\":1}");

        }
        return jsonstr.ToString();
    }

    protected void StoreNews(string url, string[] sitelist, string columnid)
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("text", "*");
        dict.Add("MatchReference", url);
        dict.Add("print", "all");
        dict.Add("databasematch", ConfigurationManager.AppSettings["DATABASE"]);
        dict.Add("Combine", "DREREFERENCE");
        XmlDocument xmldoc = IdolNewsDao.GetXmlDoc("query", dict);
        if (xmldoc != null)
        {
            IList<IdolNewsEntity> newslist = IdolNewsDao.GetNewsList(xmldoc);
            if (newslist.Count > 0)
            {
                //TrainTag(url, columnid);
                //TrainTag(url);
                foreach (string siteid in sitelist)
                {
                    foreach (IdolNewsEntity entity in newslist)
                    {
                        string href = entity.Href;
                        if (!articleDao.IsExistUrl(Convert.ToInt32(siteid), Convert.ToInt32(columnid), href))
                        {
                            ARTICLEEntity articleEntity = new ARTICLEEntity();
                            articleEntity.ARTICLETITLE = entity.Title;
                            articleEntity.ARTICLECONTENT = entity.ShowContent;
                            articleEntity.ARTICLESOURCE = entity.SiteName;
                            if (!string.IsNullOrEmpty(entity.TimeStr))
                            {
                                articleEntity.ARTICLEBASEDATE = Convert.ToDateTime(entity.TimeStr);
                            }
                            articleEntity.ARTICLESITETYPE = GetSiteType(entity.SiteType);
                            articleEntity.ARTICLEEXTERNALURL = href;
                            articleEntity.ARTICLEDISSTYLE = 0;
                            articleEntity.ARTICLERELESE = 0;
                            articleEntity.ARTICLESTATUS = 0;
                            articleEntity.ARTICLEAUDIT = 0;
                            if (!string.IsNullOrEmpty(columnid))
                            {
                                articleEntity.COLUMNID = Convert.ToInt32(columnid);
                            }
                            articleEntity.SITEID = Convert.ToInt32(siteid);
                            articleEntity.ARTICLEEDITDATE = DateTime.Now;
                            articleDao.Add(articleEntity);
                        }
                    }
                }
            }
        }
    }

    protected int GetSiteType(string site)
    {
        site = site.ToLower();
        switch (site)
        {
            case "news":
                return 1;
            case "blog":
                return 2;
            case "bbs":
                return 3;
            default:
                return 0;
        }
    }

    protected void TrainTag(string docref, string D1Value)
    {
        Drereplace drereplace = new Drereplace();
        drereplace.ReplaceAllRefs = true;
        drereplace.SetParam("InsertValue", true);
        StringBuilder PostData = new StringBuilder();
        PostData.AppendFormat("#DREDOCREF {0}\n", docref);
        PostData.AppendFormat("#DREFIELDNAME D1\n#DREFIELDVALUE {0}\n", D1Value + "_");
        //PostData.AppendFormat("#DREFIELDNAME D2\n#DREFIELDVALUE {0}\n", D2Value);
        PostData.Append("#DREENDDATANOOP\n\n");
        drereplace.PostData = PostData.ToString();
        cnn.Execute(drereplace);

    }

    protected bool ChangeDataBase(string docref)
    {
        try
        {
            DRECHANGEMETA drechangemeta = new DRECHANGEMETA();
            drechangemeta.Type = "database";
            drechangemeta.SetParam("Refs", docref);
            drechangemeta.NewValue = "bbs";
            cnn.Execute(drechangemeta);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
