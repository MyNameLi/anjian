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

public partial class Admin_website_list : System.Web.UI.Page
{
    protected WEBSITEEntity.WEBSITEDAO Dao = new WEBSITEEntity.WEBSITEDAO();
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
        PagerList.RecordCount = Dao.GetPagerRowsCount("", null);
        DataTable dt = Dao.GetPager("", null, null, PagerList.PageSize, PagerList.CurrentPageIndex);
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
                    case "Add":
                        WEBSITEEntity entity = new WEBSITEEntity();
                        entity.WEBSITENAME = Paramas["WebSiteName"];
                        entity.WEBSITEURL = Paramas["WebSiteUrl"];                        
                        Dao.Add(entity);
                        res = "{\"Success\":1}";
                        break;
                    case "initEdit":
                        WEBSITEEntity EditEntity = Dao.FindById(Convert.ToInt64(idList));
                        res = GetJsonStr(EditEntity);
                        break;
                    case "EditOne":
                        WEBSITEEntity Lentity = Dao.FindById(Convert.ToInt64(Paramas["ID"]));
                        Lentity.WEBSITENAME = Paramas["WebSiteName"];
                        Lentity.WEBSITEURL = Paramas["WebSiteUrl"];                        
                        Dao.Update(Lentity);
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
    protected string GetJsonStr(WEBSITEEntity EditEntity)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "WebSiteName", EncodeByEscape.GetEscapeStr(EditEntity.WEBSITENAME));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "WebSiteUrl", EncodeByEscape.GetEscapeStr(EditEntity.WEBSITEURL));        
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();
    }

}
