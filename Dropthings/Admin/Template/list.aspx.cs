using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dropthings.Data;
using Dropthings.Util;
using System.Data;
using System.Text;

public partial class Admin_Template_list : System.Web.UI.Page
{
    protected string siteid = String.Empty;
    protected TEMPLATEEntity.TEMPLATEDAO Dao = new TEMPLATEEntity.TEMPLATEDAO();
    protected static Dictionary<string, string> typedict = new Dictionary<string, string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            siteid = Session["{#siteid}"].ToString();
            InnitParams();
            InnitSelect();
            PageDataBind();
            Ajax();
        }
    }
    protected void InnitParams()
    {

        TEMPLATETYPEEntity.TEMPLATETYPEDAO typedao = new TEMPLATETYPEEntity.TEMPLATETYPEDAO();
        IList<TEMPLATETYPEEntity> typelist = typedao.Find("", null);
        foreach (TEMPLATETYPEEntity entity in typelist)
        {
            if (!typedict.ContainsKey(entity.ID.ToString()))
            {
                typedict.Add(entity.ID.ToString(), entity.NAME);
            }
        }
    }
    protected void InnitSelect()
    {
        TEMPLATETYPEEntity.TEMPLATETYPEDAO WebDao = new TEMPLATETYPEEntity.TEMPLATETYPEDAO();
        IList<TEMPLATETYPEEntity> list = WebDao.Find("", null);
        foreach (TEMPLATETYPEEntity entity in list)
        {
            ListItem item = new ListItem(entity.NAME, entity.ID.ToString());
            TemplateType.Items.Add(item);
        }
    }

    protected void PageDataBind()
    {
        string strWhere = " SITEID=" + siteid;
        PagerList.RecordCount = Dao.GetPagerRowsCount(strWhere, null);
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
                switch (act)
                {
                    case "remove":

                        TEMPLATEEntity removeEntity = Dao.FindById(System.Convert.ToInt64(idList));
                        string filepath = HttpContext.Current.Server.MapPath(removeEntity.TEMPLATEPATH);
                        FileManage.DeleteFile(filepath);
                        Dao.Delete(System.Convert.ToInt32(idList));
                        res = "{\"Success\":1}";
                        break;
                    case "Add":
                        TEMPLATEEntity entity = new TEMPLATEEntity();
                        entity.TEMPLATENAME = Paramas["TemplateName"];
                        entity.TEMPLATEDES = Paramas["TemplateDes"];
                        entity.TEMPLATEPATH = Paramas["TemplatePath"];
                        entity.TEMPLATETYPE = System.Convert.ToInt32(Paramas["TemplateType"]);
                        entity.TEMPLATEPARAM = Paramas["TemplateParam"];
                        entity.SITEID = Convert.ToInt32(siteid);
                        Dao.Add(entity);
                        string AddPath = HttpContext.Current.Server.MapPath(Paramas["TemplatePath"]);
                        string AddFileStr = Paramas["TemplateCotent"];
                        FileManage.EditFile(AddPath, AddFileStr);
                        res = "{\"Success\":1}";
                        break;
                    case "initEdit":
                        TEMPLATEEntity EditEntity = Dao.FindById(System.Convert.ToInt64(idList));
                        res = GetJsonStr(EditEntity);
                        break;
                    case "EditOne":
                        TEMPLATEEntity Lentity = Dao.FindById(System.Convert.ToInt64(Paramas["ID"]));
                        Lentity.TEMPLATENAME = Paramas["TemplateName"];
                        Lentity.TEMPLATEDES = Paramas["TemplateDes"];
                        Lentity.TEMPLATEPATH = Paramas["TemplatePath"];
                        Lentity.TEMPLATETYPE = System.Convert.ToInt32(Paramas["TemplateType"]);
                        Lentity.TEMPLATEPARAM = Paramas["TemplateParam"];
                        Dao.Update(Lentity);
                        string EditPath = HttpContext.Current.Server.MapPath(Paramas["TemplatePath"]);
                        string EditfileStr = Paramas["TemplateCotent"];
                        FileManage.EditFile(EditPath, EditfileStr);
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
    protected string GetJsonStr(TEMPLATEEntity EditEntity)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "TemplateName", EncodeByEscape.GetEscapeStr(EditEntity.TEMPLATENAME));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "TemplateDes", EncodeByEscape.GetEscapeStr(EditEntity.TEMPLATEDES));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "TemplatePath", EncodeByEscape.GetEscapeStr(EditEntity.TEMPLATEPATH));
        string filepath = HttpContext.Current.Server.MapPath(EditEntity.TEMPLATEPATH);
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "TemplateCotent", EncodeByEscape.GetEscapeStr(FileManage.ReadStr(filepath)));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "TemplateParam", EncodeByEscape.GetEscapeStr(EditEntity.TEMPLATEPARAM));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "TemplateType", EncodeByEscape.GetEscapeStr(EditEntity.TEMPLATETYPE.ToString()));
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();
    }

    protected string GetTemplateType(string type)
    {
        if (typedict.ContainsKey(type))
        {
            return typedict[type];
        }
        else
        {
            return "未指定类型";
        }
    }
}
