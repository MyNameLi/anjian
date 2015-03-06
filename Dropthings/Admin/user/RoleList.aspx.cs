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
using System.Data;

public partial class Admin_user_RoleList : System.Web.UI.Page
{
    protected RolesEntity.RolesDAO Dao = new RolesEntity.RolesDAO();
    private PAGEOFROLEEntity.PAGEOFROLEDAO pageofroleDao = new PAGEOFROLEEntity.PAGEOFROLEDAO();
    //private Facade facade 
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
                var facade = Services.Get<Facade>();
                
                switch (act)
                {                    
                    case "remove":
                        if (Dao.Delete(Convert.ToInt32(idList)))
                        {
                            res = "{\"Success\":1}";
                        }
                        break;
                    case "Add":
                        RolesEntity entity = new RolesEntity();
                        entity.ROLENAME = Paramas["RoleName"];
                        entity.DESCRIPTION = Paramas["Description"];                       
                        Dao.Add(entity);
                        res = "{\"Success\":1}";
                        break;
                    case "initEdit":
                        RolesEntity EditEntity = Dao.FindById(Convert.ToInt64(idList));
                        res = GetJsonStr(EditEntity);
                        break;
                    case "EditOne":
                        RolesEntity Lentity = Dao.FindById(Convert.ToInt64(Paramas["ROLEID"]));
                        Lentity.ROLENAME = Paramas["RoleName"];
                        Lentity.DESCRIPTION = Paramas["Description"];                        
                        Dao.Update(Lentity);
                        res = "{\"Success\":1}";
                        break;
                    case "innituser":
                        DataTable userDt = facade.GetUserByRoleId(Paramas["RoleId"]);
                        if(userDt.Rows.Count > 0){
                            res = GetUserListJsonStr(userDt);
                        }
                        break;
                    case "deleteuserbyroleid":
                        int roleid = Convert.ToInt32(Paramas["RoleId"]);
                        int userid = Convert.ToInt32(Paramas["userid"]);
                        if (facade.DeleteUserFromUserInRoles(userid, roleid)) {
                            res = "{\"Success\":1}";
                        }
                        break;
                    case "innitwidgetlist":
                        IList<WidgetEntity> innitwidgetlist = facade.GetAllWidgets();
                        if (innitwidgetlist.Count > 0) {
                            res = GetWidGetListJsonStr(innitwidgetlist);
                        }
                        break;
                    case "innitrolewidgetlist":
                        int widgetroleid = Convert.ToInt32(Paramas["RoleId"]);
                        IList<WidgetsInRolesEntity> rolewidgetlist = facade.GetWidGetListByRoleId(widgetroleid);                        
                        res = GetRoleWidGetListJsonStr(rolewidgetlist);                        
                        break;
                    case "initialroletab":
                        List<PageEntity> pore = facade.GetTabsOfUser(-1);
                        res = getPageOfRoleJsonStr(pore);
                        break;
                    case "initeditroletab":
                        int tabroleid = Convert.ToInt32(Paramas["RoleId"]);
                        res = getPageOfRoleJsonStr(PageOfRoleFacade.Find(tabroleid));
                        break;
                    case "edittablist":
                        int tabeditroleid = Convert.ToInt32(Paramas["RoleId"]);
                        string[] tablist = Paramas["TabList"].Split(',');
                        if (tablist.Length > 0)
                        {
                            PageOfRoleFacade.DeletePageOfRoleByRoleId(tabeditroleid);
                            PAGEOFROLEEntity pofentity = new PAGEOFROLEEntity();
                            pofentity.ROLEID = tabeditroleid;
                            int pageId;
                            for (int i = 0; i < tablist.Length; i++)
                            {
                                if (int.TryParse(tablist[i], out pageId))
                                {
                                    pofentity.PAGEID = pageId;
                                    PageOfRoleFacade.Add(pofentity);
                                }
                            }
                        }
                        res = "{\"Success\":1}";
                        break;
                    case "editwidgetlist":
                        int editwidgetroleid = Convert.ToInt32(Paramas["RoleId"]);
                        facade.DeleteWidGetByRoleId(editwidgetroleid);
                        string widgetlist = Paramas["widgetlist"];
                        if (!string.IsNullOrEmpty(widgetlist))
                        {
                            InsertRoleWidGet(widgetlist, editwidgetroleid);                           
                        }                        
                        res = "{\"Success\":1}";
                        break;
                    case "innitmenulist":
                        IList<MENULISTEntity> menuentitylist = facade.GetMenuListByWhere("");
                        StringBuilder treestr = new StringBuilder();
                        GetTreeStr(menuentitylist, 0, ref treestr);
                        string backstr = EncodeByEscape.GetEscapeStr(treestr.ToString());
                        res = "{\"Success\":1,\"TreeStr\":\"" + backstr + "\"}";
                        break;
                    case "innitrolemenulist":
                        int menuroleid = Convert.ToInt32(Paramas["RoleId"]);
                        IList<MENUINROLEEntity> menuinrolelist = facade.GetMenuInRoleByRoleId(menuroleid);                        
                        res = GetRoleMenuListJsonStr(menuinrolelist);                        
                        break;
                    case "editmenulist":
                        int editmenuroleid = Convert.ToInt32(Paramas["RoleId"]);
                        facade.DeleteMenuInRoleByRoleId(editmenuroleid);
                        string menulist = Paramas["menulist"];
                        if (!string.IsNullOrEmpty(menulist))
                        {
                            InsertRoleMenu(menulist, editmenuroleid);                            
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

    private string getPageOfRoleJsonStr(IList<PageEntity> pore)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        foreach (PageEntity item in pore)
        {
            JsonStr.AppendFormat("\"{0}\":\"{1}\",", item.ID, EncodeByEscape.GetEscapeStr(item.TITLE));
        }
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();
    }
    private string getPageOfRoleJsonStr(IList<PAGEOFROLEEntity> list)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        foreach (PAGEOFROLEEntity entity in list)
        {
            JsonStr.AppendFormat("\"{0}\":\"{1}\",", entity.PAGEID, entity.ROLEID);
        }
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();
    }
    protected void PagerList_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        PagerList.CurrentPageIndex = e.NewPageIndex;
        PageDataBind();
    }
    protected string GetJsonStr(RolesEntity EditEntity)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "RoleName", EncodeByEscape.GetEscapeStr(EditEntity.ROLENAME));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "Description", EncodeByEscape.GetEscapeStr(EditEntity.DESCRIPTION));       
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();
    }

    protected string GetUserListJsonStr(DataTable dt)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        foreach (DataRow row in dt.Rows) {
            JsonStr.AppendFormat("\"{0}\":\"{1}\",", row["USERID"], EncodeByEscape.GetEscapeStr(row["USERNAME"].ToString())); 
        }               
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();
    }

    protected string GetWidGetListJsonStr(IList<WidgetEntity> list)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        foreach (WidgetEntity entity in list)
        {
            JsonStr.AppendFormat("\"{0}\":\"{1}\",", entity.ID, EncodeByEscape.GetEscapeStr(entity.NAME));
        }
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();
    }

    protected string GetRoleWidGetListJsonStr(IList<WidgetsInRolesEntity> list)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        foreach (WidgetsInRolesEntity entity in list)
        {
            JsonStr.AppendFormat("\"{0}\":\"{1}\",", entity.WIDGETID, entity.ROLEID);
        }
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();
    }

    protected string GetRoleMenuListJsonStr(IList<MENUINROLEEntity> list) {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        foreach (MENUINROLEEntity entity in list)
        {
            JsonStr.AppendFormat("\"{0}\":\"{1}\",", entity.MENUID, entity.ROLEID);
        }
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();
    }

    protected void InsertRoleWidGet(string widgetlist,int roleid) {
        var facade = Services.Get<Facade>();
        string[] list = widgetlist.Split(',');
        foreach (string key in list) {
            WidgetsInRolesEntity entity = new WidgetsInRolesEntity();
            entity.ROLEID = roleid;
            entity.WIDGETID = Convert.ToInt32(key);
            facade.Add(entity);
        }
    }

    protected void InsertRoleMenu(string menulist, int roleid) {
        var facade = Services.Get<Facade>();
        string[] list = menulist.Split(',');
        foreach (string key in list)
        {
            MENUINROLEEntity entity = new MENUINROLEEntity();
            entity.ROLEID = roleid;
            entity.MENUID = Convert.ToInt32(key);
            facade.InsertMenuInRoleEntity(entity);
        }
    }

    protected void GetTreeStr(IList<MENULISTEntity> list, int parentID, ref StringBuilder treestr) {
        foreach (MENULISTEntity entity in list)
        {
            if (entity.PARENTID.Value == parentID)
            {
                treestr.Append("<div style=\"clear:both; line-height:20px; overflow:hidden;height:auto;");
                if (parentID > 0)
                {
                    treestr.Append(" margin-left:10px;\">");
                }
                else
                {
                    treestr.Append("\">");
                }
                treestr.Append("<span style=\"float:left;\">");
                treestr.AppendFormat("<input type=\"checkbox\" pid=\"{0}\" name=\"item_menu_list\" value=\"{1}\" />", entity.PARENTID, entity.ID);
                treestr.AppendFormat("{0}</span>", entity.NAME);                              
                GetTreeStr(list, entity.ID.Value, ref treestr);
                treestr.Append("</div>");
            }
        }
    }
}
