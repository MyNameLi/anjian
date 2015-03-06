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

public partial class Admin_column_list : System.Web.UI.Page
{
    protected string siteid = String.Empty;
    protected COLUMNDEFEntity.COLUMNDEFDAO Dao = new COLUMNDEFEntity.COLUMNDEFDAO();
    protected COLUMNTEMPLATEEntity.COLUMNTEMPLATEEntityDao templatedao = new COLUMNTEMPLATEEntity.COLUMNTEMPLATEEntityDao();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            siteid = Session["{#siteid}"].ToString();
            InnitSelect();
            Ajax();
        }
    }

    protected void InnitSelect()
    {        
        IList<COLUMNDEFEntity> list = Dao.Find(" ID>0 AND SITEID=" + siteid + " order by COLUMNORDER ASC", null);
        ListItem item = new ListItem("根栏目", "0");
        ParentID.Items.Add(item);
        InnitSelectChild(list, 0, 0);
        TEMPLATEEntity.TEMPLATEDAO TemplateDao = new TEMPLATEEntity.TEMPLATEDAO();
        IList<TEMPLATEEntity> TemplateList = TemplateDao.Find(" ID>0 AND SITEID=" + siteid, null);
        InnitTemplate(TemplateList);
    }
    protected void InnitTemplate(IList<TEMPLATEEntity> TemplateList)
    {
        ListItem item = new ListItem("——请选择模板——", "-1");
        ColumnTemplatePath.Items.Add(item);
        foreach (TEMPLATEEntity entity in TemplateList)
        {
            ListItem LItem = new ListItem(entity.TEMPLATENAME, entity.TEMPLATEPATH);
            ColumnTemplatePath.Items.Add(LItem);
        }
    }

    protected void InnitSelectChild(IList<COLUMNDEFEntity> list, int parent, int level)
    {
        foreach (COLUMNDEFEntity entity in list)
        {
            if (entity.PARENTID.Value == parent)
            {
                string ItemName = HttpUtility.HtmlDecode(GetLevelStr(level) + entity.COLUMNNAME);
                ListItem item = new ListItem(ItemName, entity.ID.ToString());
                ParentID.Items.Add(item);
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
                            GetTreeStr(list, 0, ref treestr, 0);
                            string backstr = EncodeByEscape.GetEscapeStr(treestr.ToString());
                            res = "{\"Success\":1,\"TreeStr\":\"" + backstr + "\"}";
                            break;
                        case "remove":
                            if (!string.IsNullOrEmpty(idList))
                            {
                                int delid = Convert.ToInt32(idList);
                                if (Dao.Delete(delid))
                                {
                                    res = "{\"Success\":1}";
                                }
                            }
                            break;
                        case "innitedit":
                            if (!string.IsNullOrEmpty(idList))
                            {
                                long innitid = Convert.ToInt64(idList);
                                COLUMNDEFEntity entity = Dao.FindById(innitid);
                                res = GetJsonStr(entity);
                            }
                            break;
                        case "Add":
                            COLUMNDEFEntity addentity = new COLUMNDEFEntity();
                            addentity.COLUMNNAME = Paramas["ColumnName"];
                            addentity.COLUMNDES = Paramas["ColumnDes"];
                            addentity.COLUMNSTATUS = Convert.ToInt32(Paramas["ColumnStatus"]);
                            addentity.COLUMNORDER = Convert.ToInt32(Paramas["ColumnOrder"]);
                            addentity.COLUMNNAMERULE = Paramas["ColumnNameRule"];
                            addentity.COLUMNTELEMPLATEPATH = Paramas["ColumnTemplatePath"];
                            addentity.ISDIS = Convert.ToInt32(Paramas["IsDis"]);
                            addentity.PARENTID = Convert.ToInt32(Paramas["ParentID"]);
                            addentity.SITEID = Convert.ToInt32(siteid);
                            Dao.Add(addentity);
                            res = "{\"Success\":1}";
                            break;
                        case "EditOne":
                            COLUMNDEFEntity Lentity = Dao.FindById(System.Convert.ToInt64(Paramas["ID"]));
                            Lentity.COLUMNNAME = Paramas["ColumnName"];
                            Lentity.COLUMNDES = Paramas["ColumnDes"];
                            Lentity.COLUMNSTATUS = Convert.ToInt32(Paramas["ColumnStatus"]);
                            Lentity.COLUMNORDER = Convert.ToInt32(Paramas["ColumnOrder"]);
                            Lentity.COLUMNNAMERULE = Paramas["ColumnNameRule"];
                            Lentity.COLUMNTELEMPLATEPATH = Paramas["ColumnTemplatePath"];
                            Lentity.ISDIS = Convert.ToInt32(Paramas["IsDis"]);
                            Lentity.PARENTID = Convert.ToInt32(Paramas["ParentID"]);
                            Dao.Update(Lentity);
                            res = "{\"Success\":1}";
                            break;
                        case "innittelemplante":
                            string strWhere = " COLUMNID =" + idList;
                            IList<COLUMNTEMPLATEEntity> templatelist = templatedao.Find(strWhere, null);
                            if (templatelist.Count > 0)
                            {
                                res = GetTemplateJson(templatelist);
                            }
                            break;
                        case "innitHtmlStr":
                            string sqlWhere = " COLUMNID =" + idList + " AND TEMPLATEID = " + Paramas["templante_id"];
                            IList<COLUMNTEMPLATEEntity> htmltemplatelist = templatedao.Find(sqlWhere, null);
                            if (htmltemplatelist.Count > 0)
                            {
                                COLUMNTEMPLATEEntity entity = htmltemplatelist[0];
                                res = "{\"HtmlStr\":\"" + EncodeByEscape.GetEscapeStr(entity.HTMLSTR) +
                                    "\",\"RuleStr\":\"" + EncodeByEscape.GetEscapeStr(entity.NEWSIDLIST) + "\",\"Success\":1}";
                            }
                            break;
                        case "EditHtmlStr":
                            string streditwhere = " COLUMNID=" + idList + " AND TEMPLATEID = " + Paramas["templante_id"];
                            COLUMNTEMPLATEEntity editEntity = templatedao.FindEntity(streditwhere, null);
                            editEntity.HTMLSTR = CommonLabel.ReplaceTheRootPath(Paramas["htmlstr"]);
                            editEntity.NEWSIDLIST = Paramas["NewsIdList"];
                            templatedao.Update(editEntity);
                            res = "{\"Success\":1}";
                            break;
                        case "UpdateRelese":
                            string columnList = "COLUMNPUBLISH";
                            string valuelist = Paramas["publishpath"];
                            if (Dao.UpdateSet(System.Convert.ToInt32(idList), columnList, valuelist))
                            {
                                res = "{\"Success\":1}";
                            }
                            break;
                        case "EditIsDis":
                            if (Dao.UpdateSet(System.Convert.ToInt32(idList), "ISDIS", Paramas["status"]))
                            {
                                res = "{\"Success\":1}";
                            }
                            break;
                        case "EditStatus":
                            if (Dao.UpdateSet(System.Convert.ToInt32(idList), "COLUMNSTATUS", Paramas["status"]))
                            {
                                res = "{\"Success\":1}";
                            }
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

    private void GetTreeStr(IList<COLUMNDEFEntity> list, int parentID, ref StringBuilder treestr, int level)
    {
        foreach (COLUMNDEFEntity entity in list)
        {
            if (entity.PARENTID.Value == parentID)
            {
                treestr.AppendFormat("<tr id=\"column_list_{0}\" name=\"column_parent_{1}\"><td height=\"18\" bgcolor=\"#FFFFFF\">", entity.ID, entity.PARENTID);
                treestr.Append("<div align=\"left\" class=\"STYLE2 STYLE1\">");
                treestr.Append(getSpanStr(level));
                treestr.AppendFormat("{0}</div></td>", entity.COLUMNNAME);

                treestr.AppendFormat("<td height=\"18\" bgcolor=\"#FFFFFF\"><div align=\"center\" class=\"STYLE2 STYLE1\"><img class=\"status_img\" pid=\"{0}\"", entity.COLUMNSTATUS);
                treestr.AppendFormat(" onclick='javascript:listTable.EditStatus(this,\"{0}\",\"EditStatus\");' src=\"../images/status_{1}.gif\" border=\"0\" /></div></td>", entity.ID, entity.COLUMNSTATUS);
                treestr.AppendFormat("<td height=\"18\" bgcolor=\"#FFFFFF\"><div align=\"center\" class=\"STYLE2 STYLE1\">{0}</div></td>", entity.COLUMNNAMERULE);
                //treestr.AppendFormat("<td height=\"18\" bgcolor=\"#FFFFFF\"><div align=\"center\" class=\"STYLE2 STYLE1\"><img class=\"status_img\" pid=\"{0}\"", entity.ISDIS);
                //treestr.AppendFormat(" onclick='javascript:listTable.EditStatus(this,\"{0}\",\"EditIsDis\");'src=\"../images/status_{1}.gif\" border=\"0\" /></div></td>", entity.ID, entity.ISDIS);

                treestr.Append("<td height=\"18\" bgcolor=\"#FFFFFF\"><div align=\"center\">");
                treestr.Append("<img src=\"../images/037.gif\" width=\"9\" height=\"9\" />");
                treestr.AppendFormat("<span class=\"STYLE1\"> [</span><a href=\"javascript:void(null);\" name=\"column_edit\" pid=\"{0}\">编辑基本属性</a>", entity.ID);
                treestr.Append("<span class=\"STYLE1\">]</span></div></td>");

                //treestr.Append("<td height=\"18\" bgcolor=\"#FFFFFF\"><div align=\"center\">");
                //treestr.Append("<img src=\"../images/037.gif\" width=\"9\" height=\"9\" />");
                //treestr.AppendFormat("<span class=\"STYLE1\"> [</span><a href=\"javascript:void(null);\" name=\"column_edit_content\" pid=\"{0}\">编辑域</a>", entity.ID);
                //treestr.Append("<span class=\"STYLE1\">]</span></div></td>");

                treestr.Append("<td height=\"18\" bgcolor=\"#FFFFFF\"><div align=\"center\">");
                treestr.Append("<img src=\"../images/037.gif\" width=\"9\" height=\"9\" />");
                treestr.AppendFormat("<span class=\"STYLE1\"> [</span><a href=\"javascript:void(null);\" name=\"column_delete\" pid=\"{0}\">删除</a>", entity.ID);
                treestr.Append("<span class=\"STYLE1\">]</span></div></td>");

                treestr.Append("<td height=\"18\" bgcolor=\"#FFFFFF\"><div align=\"center\">");
                treestr.Append("<img src=\"../images/037.gif\" width=\"9\" height=\"9\" />");
                treestr.Append("<span class=\"STYLE1\"> [</span><a href=\"javascript:void(null);\" ");
                treestr.AppendFormat("onclick='javascript:listTable.PublishColumn(\"{0}\",\"PublishFactory\",\"{1}\");'>生成</a>", entity.ID, EncodeByEscape.GetEscapeStr(entity.COLUMNTELEMPLATEPATH));
                treestr.Append("<span class=\"STYLE1\">]</span></div></td></tr>");
                GetTreeStr(list, entity.ID.Value, ref treestr, level + 1);
            }
        }
    }

    private string getSpanStr(int level)
    {
        StringBuilder spanStr = new StringBuilder();
        for (int i = 0; i < level; i++)
        {
            spanStr.Append("&nbsp;&nbsp;&nbsp;");
        }
        return spanStr.ToString();
    }

    protected string GetJsonStr(COLUMNDEFEntity EditEntity)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ColumnName", EncodeByEscape.GetEscapeStr(EditEntity.COLUMNNAME));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ColumnDes", EncodeByEscape.GetEscapeStr(EditEntity.COLUMNDES));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ColumnStatus", EncodeByEscape.GetEscapeStr(EditEntity.COLUMNSTATUS.ToString()));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ColumnOrder", EncodeByEscape.GetEscapeStr(EditEntity.COLUMNORDER.ToString()));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ColumnNameRule", EncodeByEscape.GetEscapeStr(EditEntity.COLUMNNAMERULE));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ColumnTemplatePath", EncodeByEscape.GetEscapeStr(EditEntity.COLUMNTELEMPLATEPATH));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "IsDis", EncodeByEscape.GetEscapeStr(EditEntity.ISDIS.ToString()));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ParentID", EncodeByEscape.GetEscapeStr(EditEntity.PARENTID.ToString()));
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();

    }

    protected string GetTemplateJson(IList<COLUMNTEMPLATEEntity> templatelist)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        int count = 1;
        foreach (COLUMNTEMPLATEEntity entity in templatelist)
        {
            JsonStr.AppendFormat("\"entity_{0}\":", count);
            JsonStr.Append("{");
            JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ColumnID", EncodeByEscape.GetEscapeStr(entity.COLUMNID.ToString()));
            JsonStr.AppendFormat("\"{0}\":\"{1}\",", "TelemplateID", EncodeByEscape.GetEscapeStr(entity.TEMPLATEID.ToString()));
            JsonStr.AppendFormat("\"{0}\":\"{1}\",", "TelemplateType", EncodeByEscape.GetEscapeStr(entity.TEMPLATETYPE.ToString()));
            JsonStr.AppendFormat("\"{0}\":\"{1}\"", "TelemplateName", EncodeByEscape.GetEscapeStr(entity.TEMPLATENAME));
            JsonStr.Append("},");
            count++;
        }
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();
    }
}
