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

public partial class Admin_New_list : System.Web.UI.Page
{
    protected string siteid = String.Empty;
    protected ARTICLEEntity.ARTICLEDAO Dao = new ARTICLEEntity.ARTICLEDAO();
    protected static Dictionary<string, string> typedict = new Dictionary<string, string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            siteid = Session["{#siteid}"].ToString();
            InnitSelect();
            PageDataBind();
            Ajax();
        }
    }
    protected void InnitSelect()
    {

        COLUMNDEFEntity.COLUMNDEFDAO WebDao = new COLUMNDEFEntity.COLUMNDEFDAO();
        IList<COLUMNDEFEntity> list = WebDao.Find(" ID>0 AND COLUMNSTATUS=1 AND PARENTID=91 AND SITEID=" + siteid + " order by COLUMNORDER", null);
        ListItem item = new ListItem("请选择栏目", "-1");
        SelectClomun.Items.Add(item);
        ColumnID.Items.Add(item);
        InnitSelectChild(list, 91, 0);
    }
    protected void InnitSelectChild(IList<COLUMNDEFEntity> list, int parent, int level)
    {
        foreach (COLUMNDEFEntity entity in list)
        {
            if (entity.PARENTID.Value == parent)
            {
                string ItemName = HttpUtility.HtmlDecode(GetLevelStr(level) + entity.COLUMNNAME);
                ListItem item = new ListItem(ItemName, entity.ID.ToString());
                ColumnID.Items.Add(item);
                SelectClomun.Items.Add(item);
                InnitSelectChild(list, entity.ID.Value, level + 1);
            }
        }
    }
    private string GetLevelStr(int level)
    {
        StringBuilder str = new StringBuilder();
        for (var i = 0; i <= level; i++)
        {
            str.AppendFormat("{0}", "&nbsp;");
        }
        return str.ToString();
    }

    protected void PageDataBind()
    {
        siteid = Session["{#siteid}"].ToString();
        string strWhere = " SITEID=" + siteid + " AND ARTICLESTATUS=0";
        string clumnid = SelectClomun.SelectedValue;
        if (clumnid != "-1") {
            strWhere = strWhere + " AND COLUMNID=" + clumnid;
        }
        else
        {
            strWhere = strWhere + " AND COLUMNID IN (SELECT ID FROM COLUMNDEF WHERE PARENTID=91)";
        }
        string orderby = " ARTICLEBASEDATE DESC";
        PagerList.RecordCount = Dao.GetPagerRowsCount(strWhere, null);
        DataTable dt = Dao.GetPager(strWhere, null, orderby, PagerList.PageSize, PagerList.CurrentPageIndex);
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
                        if (Dao.Delete(idList))
                        {
                            res = "{\"Success\":1}";
                        }
                        break;
                    case "Add":
                        ARTICLEEntity entity = new ARTICLEEntity();
                        entity.ARTICLETITLE = Paramas["ArticleTitle"];
                        entity.ARTICLEOTHERTITLE = Paramas["ArticleOtherTitle"];
                        entity.ARTICLEDISSTYLE = System.Convert.ToInt32(Paramas["ArticleDisStyle"]);
                        entity.ARTICLEEXTERNALURL = Paramas["ArticleExternalUrl"];
                        entity.ARTICLEAUTHOR = Paramas["ArticleAuthor"];
                        entity.ARTICLESOURCE = Paramas["ArticleSource"];
                        entity.ARTICLEIMGPATH = Paramas["ArticleImgPath"];
                        entity.ARTICLESUMMARY = Paramas["ArticleSummary"];
                        entity.ARTICLEBASEDATE = System.Convert.ToDateTime(Paramas["ArticleBaseDate"]);
                        entity.ARTICLEEDITDATE = DateTime.Now;
                        entity.ARTICLETAG = Paramas["ArticleTag"];
                        entity.ARTICLECONTENT = CommonLabel.ReplaceTheRootPath(Paramas["ArticleContent"]);
                        entity.ARTICLEAUDIT = System.Convert.ToInt32(Paramas["ArticleAudit"]);
                        entity.ARTICLERELESE = 0;
                        entity.ARTICLEAUDIT = 0;
                        entity.ARTICLESTATUS = 0;
                        entity.COLUMNID = System.Convert.ToInt32(Paramas["ColumnID"]);
                        entity.SITEID = Convert.ToInt32(siteid);
                        Dao.Add(entity);
                        res = "{\"Success\":1}";
                        break;
                    case "initEdit":
                        ARTICLEEntity EditEntity = Dao.FindById(System.Convert.ToInt64(idList));
                        res = GetJsonStr(EditEntity);
                        break;
                    case "EditOne":
                        ARTICLEEntity Lentity = Dao.FindById(System.Convert.ToInt64(Paramas["ID"]));
                        Lentity.ARTICLETITLE = Paramas["ArticleTitle"];
                        Lentity.ARTICLEOTHERTITLE = Paramas["ArticleOtherTitle"];
                        Lentity.ARTICLEDISSTYLE = System.Convert.ToInt32(Paramas["ArticleDisStyle"]);
                        Lentity.ARTICLEEXTERNALURL = Paramas["ArticleExternalUrl"];
                        Lentity.ARTICLEAUTHOR = Paramas["ArticleAuthor"];
                        Lentity.ARTICLESOURCE = Paramas["ArticleSource"];
                        Lentity.ARTICLEIMGPATH = Paramas["ArticleImgPath"];
                        Lentity.ARTICLESUMMARY = Paramas["ArticleSummary"];
                        Lentity.ARTICLEBASEDATE = System.Convert.ToDateTime(Paramas["ArticleBaseDate"]);
                        Lentity.ARTICLEEDITDATE = DateTime.Now;
                        Lentity.ARTICLETAG = Paramas["ArticleTag"];
                        Lentity.ARTICLECONTENT = CommonLabel.ReplaceTheRootPath(Paramas["ArticleContent"]);
                        Lentity.ARTICLEAUDIT = System.Convert.ToInt32(Paramas["ArticleAudit"]);
                        //Lentity.ARTICLERELESE = 0;
                        Lentity.COLUMNID = System.Convert.ToInt32(Paramas["ColumnID"]);
                        Dao.Update(Lentity);
                        res = "{\"Success\":1}";
                        break;
                    case "UpdateRelese":
                        string[] columnList = new string[2] { "ARTICLERELESE", "PUBLISHPATH" };
                        string[] valuelist = new string[2];
                        valuelist[0] = "1";
                        valuelist[1] = Paramas["publishpath"];
                        if (Dao.UpdateSet(System.Convert.ToInt32(idList), columnList, valuelist))
                        {
                            res = "{\"Success\":1}";
                        }
                        break;
                    case "EditArticleAudit":
                        if (Dao.UpdateSet(idList, "ARTICLEAUDIT", Paramas["status"]))
                        {
                            res = "{\"Success\":1}";
                        }
                        break;
                    case "editstatus":
                        if (Dao.UpdateSet(idList, "ARTICLEDISSTYLE", Paramas["status"]))
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
    protected string GetJsonStr(ARTICLEEntity EditEntity)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ArticleTitle", EncodeByEscape.GetEscapeStr(EditEntity.ARTICLETITLE));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ArticleOtherTitle", EncodeByEscape.GetEscapeStr(EditEntity.ARTICLEOTHERTITLE));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ArticleDisStyle", EncodeByEscape.GetEscapeStr(EditEntity.ARTICLEDISSTYLE.ToString()));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ArticleExternalUrl", EncodeByEscape.GetEscapeStr(EditEntity.ARTICLEEXTERNALURL));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ArticleAuthor", EncodeByEscape.GetEscapeStr(EditEntity.ARTICLEAUTHOR));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ArticleSource", EncodeByEscape.GetEscapeStr(EditEntity.ARTICLESOURCE));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ArticleSummary", EncodeByEscape.GetEscapeStr(EditEntity.ARTICLESUMMARY));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ArticleImgPath", EncodeByEscape.GetEscapeStr(EditEntity.ARTICLEIMGPATH));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ArticleBaseDate", EncodeByEscape.GetEscapeStr(EditEntity.ARTICLEBASEDATE.Value.ToString("yyyy-MM-dd hh:mm:ss")));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ArticleTag", EncodeByEscape.GetEscapeStr(EditEntity.ARTICLETAG));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ArticleContent", EncodeByEscape.GetEscapeStr(EditEntity.ARTICLECONTENT));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ArticleAudit", EncodeByEscape.GetEscapeStr(EditEntity.ARTICLECONTENT.ToString()));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ColumnID", EncodeByEscape.GetEscapeStr(EditEntity.COLUMNID.ToString()));
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();
    }
    protected void btn_look_Click(object sender, EventArgs e)
    {
        PageDataBind();
    }
}
