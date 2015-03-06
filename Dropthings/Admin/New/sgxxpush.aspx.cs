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

public partial class Admin_New_sgxxpush : System.Web.UI.Page
{
    protected string siteid = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            siteid = Session["{#siteid}"].ToString();
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
                    case "getinfolist":
                        string sqlstrWhere = " SITEID=" + siteid + " AND ARTICLESTATUS=0 AND COLUMNID IN (SELECT ID FROM COLUMNDEF WHERE PARENTID=200)";
                        string strwhere = Paramas["strwhere"];
                        if (!String.IsNullOrEmpty(strwhere)) {
                            sqlstrWhere = sqlstrWhere + " AND " + strwhere;
                        }
                        /*string columnid = Paramas["columnid"];
                        if (columnid != "-1")
                        {
                            sqlstrWhere = sqlstrWhere + " AND COLUMNID=" + columnid;
                        }
                        else {
                            sqlstrWhere = sqlstrWhere + " AND COLUMNID IN (SELECT ID FROM COLUMNDEF WHERE PARENTID=200)";
                        }*/
                        string sqlstrorder = " ARTICLEBASEDATE DESC";
                        string strorder = Paramas["orderby"];
                        if (!String.IsNullOrEmpty(strorder))
                        {
                            sqlstrorder = sqlstrorder + "," + strorder;
                        }
                        int Start = Convert.ToInt32(Paramas["start"]);
                        int PageSize = Convert.ToInt32(Paramas["page_size"]);
                        res = ArticleFacade.GetPagerJsonStr(sqlstrWhere, sqlstrorder, PageSize, Start);
                        break;   
                    case "exportinfo":
                        string exportsqlstrwhere = " ID IN(" + idlist + ")";
                        IList<ARTICLEEntity> list = ArticleFacade.Find(exportsqlstrwhere);
                        if (list != null && list.Count > 0)
                        {
                            string path = string.Empty;
                            if (ArticleFacade.ExportToTxt(list,out path)) {
                                res = "{\"Success\":1,\"path\":\"" + EncodeByEscape.GetEscapeStr(path) + "\"}";
                            }
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
}
