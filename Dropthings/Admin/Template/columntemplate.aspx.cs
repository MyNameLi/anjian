using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dropthings.Data;
using Dropthings.Util;
using System.Text;

public partial class Admin_Template_columntemplate : System.Web.UI.Page
{
    protected string siteid = String.Empty;
    protected COLUMNDEFEntity.COLUMNDEFDAO Dao = new COLUMNDEFEntity.COLUMNDEFDAO();
    protected COLUMNTEMPLATEEntity.COLUMNTEMPLATEEntityDao templateDao = new COLUMNTEMPLATEEntity.COLUMNTEMPLATEEntityDao();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            siteid = Session["{#siteid}"].ToString();
            Ajax();
        }
    }
    protected void Ajax()
    {
        if (Request["ajaxString"] == "1")
        {
            string act = Request["act"];
            if (!string.IsNullOrEmpty(act))
            {
                Dictionary<string, string> Paramas = new Dictionary<string, string>();
                foreach (string key in Request.Form.AllKeys)
                {
                    Paramas.Add(key, EncodeByEscape.GetUnEscapeStr(Request[key]));
                }
                string res = string.Empty;
                string idList = Request["idList"];
                string val = Request["val"];
                try
                {
                    switch (act)
                    {
                        case "innittree":
                            IList<COLUMNDEFEntity> list = Dao.Find(" ID>0 AND SITEID=" + siteid + " order by COLUMNORDER ASC", null);
                            StringBuilder treestr = new StringBuilder();
                            GetTreeStr(list, 0, ref treestr);
                            string backstr = EncodeByEscape.GetEscapeStr(treestr.ToString());
                            res = "{\"Success\":1,\"TreeStr\":\"" + backstr + "\"}";
                            break;
                        case "innittemplatelist":
                            string strwhere = " ColumnID=" + idList;
                            IList<COLUMNTEMPLATEEntity> templatelist = templateDao.Find(strwhere, null);
                            if (templatelist.Count > 0)
                            {
                                res = GetJsonStr(templatelist);
                            }
                            else
                            {
                                res = "{\"Success\":1}";
                            }
                            break;
                        case "Add":
                            COLUMNTEMPLATEEntity addEntity = new COLUMNTEMPLATEEntity();
                            addEntity.COLUMNID = Convert.ToInt32(Paramas["ColumnID"]);
                            addEntity.TEMPLATEID = Convert.ToInt32(Paramas["TemplateID"]);
                            addEntity.TEMPLATENAME = Paramas["TemplateName"];
                            addEntity.TEMPLATETYPE = Convert.ToInt32(Paramas["TemplateType"]);
                            addEntity.SITEID = Convert.ToInt32(siteid);
                            templateDao.Add(addEntity);
                            res = "{\"Success\":1}";
                            break;
                        case "innitedit":
                            string strinniteditwhere = " COLUMNID=" + idList + " AND TEMPLATEID = " + Paramas["templateid"];
                            IList<COLUMNTEMPLATEEntity> templateeditlist = templateDao.Find(strinniteditwhere, null);
                            if (templateeditlist.Count > 0)
                            {
                                res = GetJsonStr(templateeditlist);
                            }
                            else
                            {
                                res = "{\"Success\":1}";
                            }
                            break;
                        case "EditOne":
                            string streditwhere = " COLUMNID=" + Paramas["ColumnID"] + " AND TEMPLATEID = " + Paramas["TemplateID"];
                            COLUMNTEMPLATEEntity editEntity = templateDao.FindEntity(streditwhere, null);
                            editEntity.COLUMNID = Convert.ToInt32(Paramas["ColumnID"]);
                            editEntity.TEMPLATEID = Convert.ToInt32(Paramas["TemplateID"]);
                            editEntity.TEMPLATENAME = Paramas["TemplateName"];
                            editEntity.TEMPLATETYPE = Convert.ToInt32(Paramas["TemplateType"]);
                            templateDao.Update(editEntity);
                            res = "{\"Success\":1}";
                            break;
                        case "delete":
                            templateDao.delete(Convert.ToInt32(idList), Convert.ToInt32(Paramas["templateid"]));
                            res = "{\"Success\":1}";
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    res = "{\"Error\":1,\"ErrorStr\":\"" + EncodeByEscape.GetEscapeStr(e.ToString()) + "\"}";
                }
                finally
                {
                    Response.Write(res);
                    Response.End();
                }
            }

        }
    }

    private void GetTreeStr(IList<COLUMNDEFEntity> list, int parentID, ref StringBuilder treestr)
    {
        foreach (COLUMNDEFEntity entity in list)
        {
            if (entity.PARENTID.Value == parentID)
            {
                treestr.Append("<div style=\"clear:both;");
                if (parentID > 0)
                {
                    treestr.Append(" margin-left:10px;\">");
                }
                else
                {
                    treestr.Append("\">");
                }
                treestr.AppendFormat("<span style=\"float:left;\">{0}</span>", entity.COLUMNNAME);
                treestr.Append("<span style=\"float:right;\">");
                treestr.AppendFormat("<a href=\"javascript:void(null);\" name=\"column_template_look\" pid=\"{0}\">查看域</a>", entity.ID);
                treestr.Append("&nbsp;&nbsp;&nbsp;");
                treestr.AppendFormat("<a href=\"javascript:void(null);\" name=\"column_template_add\" pid=\"{0}\">新增域</a>", entity.ID);
                treestr.Append("</span>");
                GetTreeStr(list, entity.ID.Value, ref treestr);
                treestr.Append("</div>");
            }
        }
    }

    protected string GetJsonStr(IList<COLUMNTEMPLATEEntity> templatelist)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        int count = 1;
        foreach (COLUMNTEMPLATEEntity entity in templatelist)
        {
            JsonStr.AppendFormat("\"entity_{0}\":", count);
            JsonStr.Append("{");
            JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ColumnID", EncodeByEscape.GetEscapeStr(entity.COLUMNID.ToString()));
            JsonStr.AppendFormat("\"{0}\":\"{1}\",", "TemplateID", EncodeByEscape.GetEscapeStr(entity.TEMPLATEID.ToString()));
            JsonStr.AppendFormat("\"{0}\":\"{1}\",", "TemplateType", EncodeByEscape.GetEscapeStr(entity.TEMPLATETYPE.ToString()));
            JsonStr.AppendFormat("\"{0}\":\"{1}\",", "HtmlStr", EncodeByEscape.GetEscapeStr(entity.HTMLSTR.ToString()));
            JsonStr.AppendFormat("\"{0}\":\"{1}\",", "TemplateName", EncodeByEscape.GetEscapeStr(entity.TEMPLATENAME.ToString()));
            JsonStr.AppendFormat("\"{0}\":\"{1}\"", "RuleHtml", EncodeByEscape.GetEscapeStr(entity.NEWSIDLIST.ToString()));
            JsonStr.Append("},");
            count++;
        }
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();

    }
}
