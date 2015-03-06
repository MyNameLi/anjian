using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Dropthings.Business.Facade;
using Dropthings.Util;

public partial class Admin_system_SnapShot : System.Web.UI.Page
{
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
            string act = Request["act"];
            string idList = Request["idList"];
            string val = Request["val"];
            try
            {
                switch (act)
                {
                    case "getsnapimgpath":
                        string url = Paramas["web_url"];
                        string path = Server.MapPath("~/SnapShot");
                        string filename = DateTime.Now.ToString("yyyyMMddHHmmss");
                        string filepath = path + "\\Snap_" + filename + ".gif";
                        Bitmap m_Bitmap = WebSiteThumbnail.GetWebSiteThumbnail(url);
                        m_Bitmap.Save(filepath);
                        res = "{\"path\":\"" + EncodeByEscape.GetEscapeStr(filepath) + "\",\"Success\":1}";
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
