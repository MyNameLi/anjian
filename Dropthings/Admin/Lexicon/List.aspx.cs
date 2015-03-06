using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Dropthings.Util;
using Dropthings.Data;
using Dropthings.Business.Facade;

public partial class Admin_Lexicon_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
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
                    case "inittree":
                        res = LexiconTypeFacade.getTreeStr("", 0);
                        break;
                    case "getparentSelect":
                        res = LexiconTypeFacade.getSelectStr("", 0);
                        break;
                    case "addlexicontype":
                        LEXICONTYPEEntity addentity = new LEXICONTYPEEntity();
                        addentity.TYPENAME = Paramas["TypeName"];
                        addentity.TYPEINTRODUCE = Paramas["TypeIntroduce"];
                        addentity.PARENTID = Convert.ToInt32(Paramas["TypeParentId"]);
                        addentity.ORGID = Convert.ToInt32(Paramas["TypeOrgId"]);
                        LexiconTypeFacade.Add(addentity);
                        res = "{\"Success\":1}";
                        break;
                    case "editlexicontype":
                        LEXICONTYPEEntity editLentity = LexiconTypeFacade.FindById(Convert.ToInt64(Paramas["ID"]));
                        editLentity.TYPENAME = Paramas["TypeName"];
                        editLentity.TYPEINTRODUCE = Paramas["TypeIntroduce"];
                        editLentity.PARENTID = Convert.ToInt32(Paramas["TypeParentId"]);
                        editLentity.ORGID = Convert.ToInt32(Paramas["TypeOrgId"]);
                        LexiconTypeFacade.UpDate(editLentity);
                        res = "{\"Success\":1}";
                        break;
                    case "initlexicontype":
                        LEXICONTYPEEntity InitEntity = LexiconTypeFacade.FindById(Convert.ToInt64(idList));
                        res = GetJsonStr(InitEntity);
                        break;
                    case "getlexiconwordlist":
                        string strwhere = " TypeId=" + idList;
                        res = LexiconWordFacade.GetListJsonStr(strwhere);
                        break;
                    case "insertlexcion":
                        LEXICONWORDEntity wordaddentity = new LEXICONWORDEntity();
                        wordaddentity.WEIGHT = Convert.ToInt32(Paramas["weight"]);
                        wordaddentity.TYPEID = Convert.ToInt32(Paramas["typeid"]);
                        wordaddentity.WORD = Paramas["wordreg"];
                        int addid = LexiconWordFacade.Add(wordaddentity);
                        if (addid > 0)
                        {
                            res = "{\"Success\":1,\"id\":" + addid.ToString() + "}";
                        }
                        break;
                    case "deletelexcionword":
                        LexiconWordFacade.DeleteById(Convert.ToInt32(idList));
                        res = "{\"Success\":1}";
                        break;
                    case "updatewordreg":
                        string wordreg = Paramas["wordreg"];
                        LexiconWordFacade.UpDateWordReg(wordreg, Convert.ToInt32(idList));
                        res = "{\"Success\":1}";
                        break;
                    case "updatewordweight":
                        string wordweight = Paramas["wordweight"];
                        LexiconWordFacade.UpDateWordWeight(Convert.ToInt32(wordweight), Convert.ToInt32(idList));
                        res = "{\"Success\":1}";
                        break;
                    case "deletelexcionType":
                        int typeid = Convert.ToInt32(idList);
                        if (LexiconTypeFacade.Delete(typeid)) {
                            LexiconWordFacade.DeleteByTypeId(typeid);
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

    private string GetJsonStr(LEXICONTYPEEntity entity)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "TypeName", EncodeByEscape.GetEscapeStr(entity.TYPENAME));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "TypeIntroduce", EncodeByEscape.GetEscapeStr(entity.TYPEINTRODUCE));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "TypeParentId", entity.PARENTID);
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "TypeOrgId", entity.ORGID);
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();
    }
}
