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

public partial class Admin_sitemanage_list : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //InnitSelect();
            PageDataBind();
            Ajax();
        }
    }
    protected void PageDataBind()
    {
        PagerList.RecordCount = WebSiteListFacade.GetPagerCount("");
        DataTable dt = WebSiteListFacade.GetPagerDataTable("", null, PagerList.PageSize, PagerList.CurrentPageIndex);
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
                switch (act)
                {
                    case "removeall":
                        WebSiteListFacade.Delete(idList);
                        res = "{\"Success\":1}";
                        break;
                    case "remove":
                        if (WebSiteListFacade.Delete(Convert.ToInt32(idList)))
                        {
                            res = "{\"Success\":1}";
                        }
                        break;
                    case "Add":
                        WEBSITELISTEntity entity = new WEBSITELISTEntity();
                        entity.SITENAME = Paramas["SiteName"];
                        entity.DOMAINNAME = Paramas["DomainName"];
                        entity.TEMPLATEPATH = Paramas["TemplatePath"];
                        entity.PUBLISHPATH = Paramas["PublishPath"];
                        WebSiteListFacade.Add(entity);
                        res = "{\"Success\":1}";
                        break;
                    case "initEdit":
                        WEBSITELISTEntity EditEntity = WebSiteListFacade.FindById(Convert.ToInt64(idList));
                        res = GetJsonStr(EditEntity);
                        break;
                    case "EditOne":
                        WEBSITELISTEntity Lentity = WebSiteListFacade.FindById(Convert.ToInt64(Paramas["ID"]));
                        Lentity.SITENAME = Paramas["SiteName"];
                        Lentity.DOMAINNAME = Paramas["DomainName"];
                        Lentity.TEMPLATEPATH = Paramas["TemplatePath"];
                        Lentity.PUBLISHPATH = Paramas["PublishPath"];
                        WebSiteListFacade.UpDate(Lentity);
                        res = "{\"Success\":1}";
                        break;
                    case "entersitemanage":
                        WEBSITELISTEntity Entity = WebSiteListFacade.FindById(Convert.ToInt64(idList));

                        if (System.Web.HttpContext.Current.Session["{#sitepublishpath}"] != null)
                        {
                            System.Web.HttpContext.Current.Session["{#sitepublishpath}"] = Entity.PUBLISHPATH;
                        }
                        else {
                            System.Web.HttpContext.Current.Session.Add("{#sitepublishpath}", Entity.PUBLISHPATH);
                        }
                        if (System.Web.HttpContext.Current.Session["{#siteid}"] != null)
                        {
                            System.Web.HttpContext.Current.Session["{#siteid}"] = Entity.ID.Value;
                        }
                        else
                        {
                            System.Web.HttpContext.Current.Session.Add("{#siteid}", Entity.ID.Value);
                        }
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
    protected void PagerList_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        PagerList.CurrentPageIndex = e.NewPageIndex;
        PageDataBind();
    }
    protected string GetJsonStr(WEBSITELISTEntity EditEntity)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "SiteName", EncodeByEscape.GetEscapeStr(EditEntity.SITENAME));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "DomainName", EncodeByEscape.GetEscapeStr(EditEntity.DOMAINNAME));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "TemplatePath", EncodeByEscape.GetEscapeStr(EditEntity.TEMPLATEPATH));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "PublishPath", EncodeByEscape.GetEscapeStr(EditEntity.PUBLISHPATH));
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();
    }
}
