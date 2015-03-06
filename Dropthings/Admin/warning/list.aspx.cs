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

public partial class Admin_warning_list : System.Web.UI.Page
{
    protected WARNINGMSGEntity.WARNINGMSGDAO Dao = new WARNINGMSGEntity.WARNINGMSGDAO();
    protected WEBSITEEntity.WEBSITEDAO websiteDao = new WEBSITEEntity.WEBSITEDAO();
    protected Dictionary<string, string> websitedict = new Dictionary<string, string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Innitwebsitedict();
            PageDataBind();
            Ajax();
        }
    }

    protected void Innitwebsitedict() {
        IList<WEBSITEEntity> list = websiteDao.Find("");
        foreach (WEBSITEEntity entity in list) {
            websitedict.Add(entity.ID.ToString(), entity.WEBSITENAME);
        }
    }

    protected void PageDataBind()
    {
        string strWhere = "USERNAME='admin'";
        PagerList.RecordCount = Dao.GetPagerRowsCountByUserName("admin");
        DataTable dt = Dao.GetPager(strWhere, null, null, PagerList.PageSize, PagerList.CurrentPageIndex);
        if (dt != null)
        {
            dataList.DataSource = dt;
        }
        dataList.DataBind();
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
                var facade = Services.Get<Facade>();
                switch (act)
                {
                    case "removeall":
                        if (Dao.Delete(idList))
                        {
                            res = "{\"Success\":1}";
                        }
                        break;
                    case "remove":
                        if (Dao.Delete(Convert.ToInt32(idList)))
                        {
                            res = "{\"Success\":1}";
                        }
                        break;
                    case "innitwebsite":
                        IList<WEBSITEEntity> websitelist = websiteDao.GetWebSiteListByUserName("admin");
                        if (websitelist.Count > 0)
                        {
                            res = GetWebSiteListJsonStr(websitelist);
                        }
                        else {
                            res = "{\"Success\":1,\"sitecount\":0}";
                        }
                        break;
                    case "addwarning":
                        string addwebsite = Paramas["websitelist"];
                        if (!string.IsNullOrEmpty(addwebsite)) {
                            InsertWarning(addwebsite);
                            res = "{\"Success\":1}";
                        }
                        break;
                    case "editpageview":
                        if (Dao.UpdateSet(Convert.ToInt32(idList), "PAGEVIEW", val)) {
                            res = "{\"Success\":1}";
                        }
                        break;
                    case "editinvitation":
                        if (Dao.UpdateSet(Convert.ToInt32(idList), "INVITATION", val))
                        {
                            res = "{\"Success\":1}";
                        }
                        break;
                    case "innitacceptlist":
                        WARNINGMSGEntity innitentity = Dao.FindById(Convert.ToInt64(idList));
                        if (innitentity != null)
                        {
                            IList<UsersEntity> belonglist = facade.GetUserList(innitentity.ACCEPTERS, true);
                            IList<UsersEntity> otherlist = facade.GetUserList(innitentity.ACCEPTERS, false);
                            res = "{\"belonglist\" :" + GetUsersListJsonStr(belonglist) + ",\"otherlist\":" + GetUsersListJsonStr(otherlist) + ",\"Success\":1}";
                        }
                        break;
                    case "addaccept":
                        if (Dao.UpdateSet(Convert.ToInt32(idList), "ACCEPTERS", Paramas["accpetlist"]))
                        {
                            res = "{\"Success\":1}";
                        }
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
    protected void PagerList_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        PagerList.CurrentPageIndex = e.NewPageIndex;
        PageDataBind();
    }

    protected string GetWebSiteListJsonStr(IList<WEBSITEEntity> list)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        foreach (WEBSITEEntity entity in list)
        {
            JsonStr.AppendFormat("\"{0}\":\"{1}\",", entity.ID, EncodeByEscape.GetEscapeStr(entity.WEBSITENAME));
        }
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();
    }

    protected string GetUsersListJsonStr(IList<UsersEntity> list)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        foreach (UsersEntity entity in list)
        {
            JsonStr.AppendFormat("\"{0}\":\"{1}\",", entity.USERID, EncodeByEscape.GetEscapeStr(entity.USERNAME));
        }
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();
    }

    protected void InsertWarning(string websitelist) {
        string[] sitelist = websitelist.Split(',');
        foreach (string id in sitelist) {
            WARNINGMSGEntity entity = new WARNINGMSGEntity();
            entity.WEBSITEID = Convert.ToInt32(id);
            entity.PAGEVIEW = 0;
            entity.INVITATION = 0;
            entity.USERNAME = "admin";
            Dao.Add(entity);
        }
    }

    protected string GetSiteName(string websiteid) {
        return websitedict[websiteid];
    }
}
