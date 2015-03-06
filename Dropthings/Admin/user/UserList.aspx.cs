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

public partial class Admin_user_UserList : System.Web.UI.Page
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
        string strwhere = string.Empty;
        string userName = SearchUserName.Text;
        if (!string.IsNullOrEmpty(userName))
        {
            strwhere = " USERNAME LIKE '%" + userName + "%' ";
        }
        var facade = Services.Get<Facade>();
        PagerList.RecordCount = facade.GetUserCountByStrWhere(strwhere);
        DataTable dt = facade.GetUserPagerDataTable(strwhere, " USERID DESC", PagerList.PageSize, PagerList.CurrentPageIndex);
        if (dt != null)
        {
            dataList.DataSource = dt;
        }
        dataList.DataBind();
    }

    protected void InnitSelect()
    {
        //var facade = Services.Get<Facade>();
        //IList<RolesEntity> list = facade.GetAllRole();
        //foreach (RolesEntity entity in list) {
        //    ListItem item = new ListItem(entity.ROLENAME, entity.ROLEID.ToString());
        //    RoleId.Items.Add(item);
        //}
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
                    case "innitRole":
                        IList<RolesEntity> roleList = facade.GetAllRole();
                        if (roleList.Count > 0)
                        {
                            res = GetRoleListJson(roleList);
                        }
                        break;
                    case "remove":
                        if (facade.DeleteUser(Convert.ToInt32(idList)))
                        {
                            if (facade.DeleteByUserId(Convert.ToInt32(idList)))
                            {
                                res = "{\"Success\":1}";
                            }
                        }
                        break;
                    case "Add":
                        UsersEntity entity = new UsersEntity();
                        entity.ACCID = Paramas["AccId"];
                        entity.USERNAME = Paramas["UserName"];
                        entity.PASSWORD = Paramas["PassWord"];
                        entity.EMAIL = Paramas["Email"];
                        entity.CREATEDATE = DateTime.Now;
                        entity.MOBILE = Paramas["Mobile"];
                        int userId = facade.CreateUser(entity);
                        InsertUserIntoRole(Paramas["RoleId"], userId);
                        PageEntity.PAGEDAO pe = new PageEntity.PAGEDAO();
                        PageEntity peEntity = new PageEntity()
                        {
                            TITLE = "首页",
                            USERID = userId,
                            CREATEDDATE = DateTime.Now,
                            VERSIONNO = 1,
                            LAYOUTTYPE = 2,
                            PAGETYPE = 0,
                            COLUMNCOUNT = 2,
                            LASTUPDATED = DateTime.Now,
                            ISLOCKED = false,
                            LASTLOCKEDSTATUSCHANGEDAT = DateTime.Now,
                            ISDOWNFORMAINTENANCE = false,
                            LASTDOWNFORMAINTENANCEAT = DateTime.Now,
                            SERVEASSTARTPAGEAFTERLOGIN = false,
                            ORDERNO = 0
                        };
                        pe.Add(peEntity);
                        peEntity.TITLE = "高级搜索";
                        peEntity.ISLOCKED = true;
                        peEntity.ORDERNO = 1;
                        pe.Add(peEntity);
                        //peEntity.TITLE = "统计页面";
                        //peEntity.ISLOCKED = true;
                        //peEntity.ORDERNO = 2;
                        //peEntity.URL = "statistic.html";
                        //pe.Add(peEntity);
                       
                        ColumnEntity.ColumnDAO ceDao = new ColumnEntity.ColumnDAO();
                        ColumnEntity ce = new ColumnEntity()
                        {
                            PAGEID = peEntity.ID,
                            WIDGETZONEID = 207,
                            COLUMNNO = 0,
                            COLUMNWIDTH = 24,
                            LASTUPDATED = DateTime.Now
                        };
                        ceDao.Add(ce);
                        ce.WIDGETZONEID = 208;
                        ce.COLUMNNO = 1;
                        ce.COLUMNWIDTH = 75;
                        ceDao.Add(ce);
                        res = "{\"Success\":1}";
                        break;
                    case "initEdit":
                        string strWhere = " USERID=" + idList;
                        DataTable EditDt = facade.GetUserRoleDataTable(strWhere);
                        res = GetJsonStr(EditDt);
                        break;
                    case "EditOne":
                        int editUserId = Convert.ToInt32(Paramas["USERID"]);
                        UsersEntity Lentity = facade.GetUser(editUserId);
                        Lentity.ACCID = Paramas["AccId"];
                        Lentity.USERNAME = Paramas["UserName"];
                        Lentity.PASSWORD = Paramas["PassWord"];
                        Lentity.EMAIL = Paramas["Email"];
                        Lentity.MOBILE = Paramas["Mobile"];
                        facade.UpdateUser(Lentity);
                        facade.DeleteByUserId(Convert.ToInt32(Paramas["USERID"]));
                        InsertUserIntoRole(Paramas["RoleId"], editUserId);
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
    protected string GetJsonStr(DataTable dt)
    {
        var facade = Services.Get<Facade>();
        int userid = Convert.ToInt32(dt.Rows[0]["USERID"]);
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "AccId", EncodeByEscape.GetEscapeStr(dt.Rows[0]["ACCID"].ToString()));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "UserName", EncodeByEscape.GetEscapeStr(dt.Rows[0]["USERNAME"].ToString()));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "PassWord", EncodeByEscape.GetEscapeStr(dt.Rows[0]["PASSWORD"].ToString().Trim()));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "Email", EncodeByEscape.GetEscapeStr(dt.Rows[0]["EMAIL"].ToString()));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "Mobile", EncodeByEscape.GetEscapeStr(dt.Rows[0]["MOBILE"].ToString()));
        string str = facade.GetUserRoleIdList(userid);
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "RoleIdList", EncodeByEscape.GetEscapeStr(facade.GetUserRoleIdList(userid)));
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();
    }

    protected void InsertUserIntoRole(string rolelist, int userId)
    {
        var facade = Services.Get<Facade>();
        string[] Editrolelist = rolelist.Split(',');
        foreach (string roleid in Editrolelist)
        {
            UsersInRolesEntity addUserRoleEntity = new UsersInRolesEntity();
            addUserRoleEntity.ROLEID = Convert.ToInt32(roleid);
            addUserRoleEntity.USERID = userId;
            facade.Add(addUserRoleEntity);
        }
    }
    protected string GetRoleListJson(IList<RolesEntity> list)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        foreach (RolesEntity entity in list)
        {
            JsonStr.AppendFormat("\"{0}\":\"{1}\",", entity.ROLEID, EncodeByEscape.GetEscapeStr(entity.ROLENAME));
        }
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();
    }

    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
        CommonLabel.InnitLableApplication();
    }
    protected void BtnSearchUsers_Click(object sender, EventArgs e)
    {
        PageDataBind();
    }
}
