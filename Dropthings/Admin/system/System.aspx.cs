using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dropthings.Business.Facade;
using Dropthings.Data;
using Dropthings.Util;
using System.Text;
using System.Configuration;

public partial class Admin_system_System : System.Web.UI.Page
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
            try
            {
                switch (act)
                {          
                    case "initstatus":
                        string databaseList = Paramas["database_list"];
                        res = GetStatusStr(databaseList);
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

    protected string GetStatusStr(string databaseList) {
        string[] List = databaseList.Split(',');
        StringBuilder backstr = new StringBuilder();
        if (List.Length > 0) {
            backstr.Append("{");
            for (int i = 0, len = List.Length; i < len; i++) {
                string database = List[i];
                backstr.AppendFormat("\"{0}\":", database);
                backstr.Append("{");
                backstr.AppendFormat("\"alldata\":\"{0}\",", GetCount(database, 1));
                backstr.AppendFormat("\"daydata\":\"{0}\"", GetCount(database, 2));
                backstr.Append("},");
            }
            backstr.Append("\"Success\":1}");
        }
        return backstr.ToString();
    }

    protected string GetCount(string database, int type)
    {
        QueryParamEntity queryparam = new QueryParamEntity();
        queryparam.DataBase = database;      
        if (type == 2)
        {
            queryparam.MinDate = ConfigurationManager.AppSettings["TimeSpan"];
        }
        IdolQuery query = IdolQueryFactory.GetDisStyle("query");
        query.queryParamsEntity = queryparam;
        return query.GetTotalCount();
    }
}
