using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dropthings.Data;
using Dropthings.Util;
using Dropthings.Business.Facade;
using System.Data;
using System.Text;

public partial class Admin_user_TabList : System.Web.UI.Page
{
    private PageEntity.PAGEDAO pageDao = new PageEntity.PAGEDAO();
    private PAGEOFROLEEntity.PAGEOFROLEDAO por = new PAGEOFROLEEntity.PAGEOFROLEDAO();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PageDataBind();
            Ajax();
        }
    }

    protected void PagerList_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        
        PagerList.CurrentPageIndex = e.NewPageIndex;
        PageDataBind();
    }
    protected void PageDataBind()
    {
        this.PagerList.RecordCount = pageDao.GetPagerRowsCount(" userid=-1 ",null);
        DataTable dt = pageDao.GetDataSet(" userid=-1 ", null).Tables[0];
        if (dt != null && dt.Rows.Count > 0)
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
                    
                    case "remove":
                        por.Delete(Convert.ToInt32(idList));
                        pageDao.Delete(Convert.ToInt32(idList));
                        res = "{\"Success\":1}";
                        break;
                    case "Add":
                        PageEntity pe1 = new PageEntity()
                        {
                            TITLE = Paramas["TITLE"],
                            USERID = -1,
                            CREATEDDATE = DateTime.Now,
                            VERSIONNO = 1,
                            LAYOUTTYPE = 2,
                            PAGETYPE = 0,
                            COLUMNCOUNT = 2,
                            LASTUPDATED = DateTime.Now,
                            ISLOCKED = true,
                            LASTLOCKEDSTATUSCHANGEDAT = DateTime.Now,
                            ISDOWNFORMAINTENANCE = false,
                            LASTDOWNFORMAINTENANCEAT = DateTime.Now,
                            SERVEASSTARTPAGEAFTERLOGIN = false,
                            ORDERNO = Convert.ToInt32(Paramas["ORDERNO"]),
                            URL = Paramas["URL"]
                        };
                        pageDao.Add(pe1);
                        res = "{\"Success\":1}";
                        break;
                    case "initEdit":
                        string strWhere = " ID=" + idList;
                        DataTable EditDt =pageDao.GetDataSet(strWhere,null).Tables[0];
                        res = GetJsonStr(EditDt);
                        break;
                    case "EditOne":
                        int editUserId = Convert.ToInt32(Paramas["ID"]);
                        PageEntity pe = new PageEntity() {
                            TITLE = Paramas["TITLE"],
                            USERID = -1,
                            VERSIONNO = 1,
                            LAYOUTTYPE = 2,
                            PAGETYPE = 0,
                            COLUMNCOUNT = 2,
                            CREATEDDATE = DateTime.Now,
                            LASTUPDATED = DateTime.Now,
                            ISLOCKED = true,
                            LASTLOCKEDSTATUSCHANGEDAT = DateTime.Now,
                            ISDOWNFORMAINTENANCE = false,
                            LASTDOWNFORMAINTENANCEAT = DateTime.Now,
                            SERVEASSTARTPAGEAFTERLOGIN = false,
                            ORDERNO = Convert.ToInt32(Paramas["ORDERNO"]),
                            URL = Paramas["URL"],
                            ID=editUserId
                        };
                        pageDao.Update(pe);
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

    protected string GetJsonStr(DataTable dt)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ID", EncodeByEscape.GetEscapeStr(dt.Rows[0]["ID"].ToString()));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "TITLE", EncodeByEscape.GetEscapeStr(dt.Rows[0]["TITLE"].ToString()));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "URL", EncodeByEscape.GetEscapeStr(dt.Rows[0]["URL"].ToString().Trim()));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ORDERNO", EncodeByEscape.GetEscapeStr(dt.Rows[0]["ORDERNO"].ToString()));
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();
    }
}
