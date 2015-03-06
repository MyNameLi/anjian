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

public partial class Admin_user_MenuList : System.Web.UI.Page
{
    protected MENULISTEntity.MENULISTDAO Dao = new MENULISTEntity.MENULISTDAO();   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InnitSelect();
            Ajax();
        }
    }

    protected void InnitSelect()
    {
        IList<MENULISTEntity> list = Dao.Find(" ID>0 order by SEQUECE ASC", null);
        ListItem item = new ListItem("根栏目", "0");
        ParentID.Items.Add(item);
        InnitSelectChild(list, 0, 0);        
    }

    protected void InnitSelectChild(IList<MENULISTEntity> list, int parent, int level)
    {
        foreach (MENULISTEntity entity in list)
        {
            if (entity.PARENTID.Value == parent)
            {
                string ItemName = HttpUtility.HtmlDecode(GetLevelStr(level) + entity.NAME);
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
                            IList<MENULISTEntity> list = Dao.Find(" ID>0 order by SEQUECE ASC", null);
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
                                MENULISTEntity entity = Dao.FindById(innitid);
                                res = GetJsonStr(entity);
                            }
                            break;
                        case "Add":
                            MENULISTEntity addentity = new MENULISTEntity();
                            addentity.NAME = Paramas["Name"];
                            addentity.URLPATH = Paramas["UrlPath"];
                            addentity.LEFTURL = Paramas["LeftPath"];
                            addentity.PARENTID = Convert.ToInt32(Paramas["ParentID"]);
                            addentity.SEQUECE = Convert.ToInt32(Paramas["Sequece"]);
                            Dao.Add(addentity);
                            res = "{\"Success\":1}";
                            break;
                        case "EditOne":
                            MENULISTEntity Lentity = Dao.FindById(System.Convert.ToInt64(Paramas["ID"]));
                            Lentity.NAME = Paramas["Name"];
                            Lentity.URLPATH = Paramas["UrlPath"];
                            Lentity.LEFTURL = Paramas["LeftPath"];
                            Lentity.PARENTID = Convert.ToInt32(Paramas["ParentID"]);
                            Lentity.SEQUECE = Convert.ToInt32(Paramas["Sequece"]);
                            Dao.Update(Lentity);
                            res = "{\"Success\":1}";
                            break;
                         /*case "innittelemplante":
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
                             break;*/
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

    private void GetTreeStr(IList<MENULISTEntity> list, int parentID, ref StringBuilder treestr, int level)
    {
        foreach (MENULISTEntity entity in list)
        {
            if (entity.PARENTID.Value == parentID)
            {
                treestr.AppendFormat("<tr id=\"column_list_{0}\" name=\"column_parent_{1}\"><td height=\"18\" bgcolor=\"#FFFFFF\">", entity.ID, entity.PARENTID);
                treestr.Append("<div align=\"left\" class=\"STYLE2 STYLE1\">");
                treestr.Append(getSpanStr(level));
                treestr.AppendFormat("{0}</div></td>", entity.NAME);                
                treestr.AppendFormat("<td height=\"18\" bgcolor=\"#FFFFFF\"><div align=\"center\" class=\"STYLE2 STYLE1\">{0}</div></td>", entity.URLPATH);
                

                treestr.Append("<td height=\"18\" bgcolor=\"#FFFFFF\"><div align=\"center\">");
                treestr.Append("<img src=\"../images/037.gif\" width=\"9\" height=\"9\" />");
                treestr.AppendFormat("<span class=\"STYLE1\"> [</span><a href=\"javascript:void(null);\" name=\"menu_edit\" pid=\"{0}\">编辑属性</a>", entity.ID);
                treestr.Append("<span class=\"STYLE1\">]</span></div></td>");

                treestr.Append("<td height=\"18\" bgcolor=\"#FFFFFF\"><div align=\"center\">");
                treestr.Append("<img src=\"../images/037.gif\" width=\"9\" height=\"9\" />");
                treestr.AppendFormat("<span class=\"STYLE1\"> [</span><a href=\"javascript:void(null);\" name=\"menu_delete\" pid=\"{0}\">删除</a>", entity.ID);
                treestr.Append("<span class=\"STYLE1\">]</span></div></td>");

                treestr.Append("</tr>");
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

    protected string GetJsonStr(MENULISTEntity EditEntity)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "Name", EncodeByEscape.GetEscapeStr(EditEntity.NAME));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "UrlPath", EncodeByEscape.GetEscapeStr(EditEntity.URLPATH));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "LeftPath", EncodeByEscape.GetEscapeStr(EditEntity.LEFTURL));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "ParentID", EncodeByEscape.GetEscapeStr(EditEntity.PARENTID.ToString()));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "Sequece", EncodeByEscape.GetEscapeStr(EditEntity.SEQUECE.ToString())); 
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();

    }
}
