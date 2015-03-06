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

public partial class Admin_category_Training : System.Web.UI.Page
{
    protected LABLEEntity.LABLEDAO Dao = new LABLEEntity.LABLEDAO();
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
            string act = Request["action"];            
            try
            {
                switch (act)
                {
                    case "getcategorydata":
                        string strwhere = Request["strwhere"];
                        string strorder = Request["orderby"];
                        int Start = Convert.ToInt32(Request["start"]);
                        int PageSize = Convert.ToInt32(Request["page_size"]);
                        res = CategoryFacade.GetDataStr(strwhere, strorder, Start, PageSize);
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
