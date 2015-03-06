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

public partial class Admin_warning_keywordlist : System.Web.UI.Page
{
    protected WORDWARNINGEntity.WORDWARNINGDAO Dao = new WORDWARNINGEntity.WORDWARNINGDAO();       
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
            PageDataBind();
            Ajax();
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
                    case "Add":
                        WORDWARNINGEntity addEntity = new WORDWARNINGEntity();
                        addEntity.WORDRULE = Paramas["WordRule"];
                        addEntity.THRESHOLDS = Convert.ToInt32(Paramas["Thresholds"]);
                        addEntity.USERNAME = "admin";
                        Dao.Add(addEntity);
                        res = "{\"Success\":1}";
                        break;
                    case "initEdit":
                        WORDWARNINGEntity innitEditEntity = Dao.FindById(Convert.ToInt64(idList));
                        res = GetJsonStr(innitEditEntity);
                        break;
                    case "EditOne":
                        WORDWARNINGEntity editEntity = Dao.FindById(Convert.ToInt64(Paramas["ID"]));
                        editEntity.WORDRULE = Paramas["WordRule"];
                        editEntity.THRESHOLDS = Convert.ToInt32(Paramas["Thresholds"]);
                        Dao.Update(editEntity);
                        res = "{\"Success\":1}";
                        break;
                    case "editthresholds":
                        if (Dao.UpdateSet(Convert.ToInt32(idList), "THRESHOLDS", val))
                        {
                            res = "{\"Success\":1}";
                        }
                        break;
                    case "innitacceptlist":
                        WORDWARNINGEntity innitentity = Dao.FindById(Convert.ToInt64(idList));
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
    protected string GetJsonStr(WORDWARNINGEntity EditEntity)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "WordRule", EncodeByEscape.GetEscapeStr(EditEntity.WORDRULE));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "Thresholds", EncodeByEscape.GetEscapeStr(EditEntity.THRESHOLDS.ToString()));       
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();
    }

    
}
