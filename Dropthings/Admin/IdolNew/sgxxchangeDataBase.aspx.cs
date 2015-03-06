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
using System.Data;

public partial class Admin_IdolNew_sgxxchangeDataBase : System.Web.UI.Page
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
                        string[] urllist = baseurl.Split('+');
                        string sitelist = basewebsitelist;
                        StringBuilder posturl = new StringBuilder();
                        foreach (string url in urllist)
                        {
                            if (posturl.Length > 0)
                            {
                                posturl.Append("+");
                            }
                            posturl.Append(HttpUtility.UrlEncode(url, Encoding.UTF8));
                        }
                        //StoreNews(posturl.ToString());
                        res = "{\"Success\":1}";
                        break;
                    case "innitwebsite":
                        IList<WEBSITELISTEntity> websitelist = WebSiteListFacade.Find();
                        res = GetWebSiteJson(websitelist);
                        break;
                    case "innitsitecolumn":
                        string siteid = Paramas["siteid"];
                        IList<COLUMNDEFEntity> columnlist = ColumnFacade.GetListBySiteId(Convert.ToInt32(siteid));
                        StringBuilder optionstr = new StringBuilder();
                        GetSelectHtml(columnlist, 0, 0, ref optionstr);
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
                    case "initstorenews":
                        string articleurllist = Paramas["url_list"];
                        //res = ArchiveInfoFacade.GetArticleColumnJsonStr(articleurllist, 2);
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

    protected void StoreNews(string url)
    {
        try
        {
            //int type = 2;
            //int userid = Convert.ToInt32(UserFacade.GetUserId());
            //ArchiveInfoFacade.AddFromIdol(url, type, userid);
        }
        catch
        {
            
        }
    }
    
}
