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

public partial class Admin_lable_list : System.Web.UI.Page
{
    protected LABLEEntity.LABLEDAO Dao = new LABLEEntity.LABLEDAO();
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
                switch (act)
                {
                    case "removeall":
                        Dao.Delete(idList);
                        res = "{\"Success\":1}";
                        break;
                    case "remove":
                        if (Dao.Delete(Convert.ToInt32(idList)))
                        {
                            res = "{\"Success\":1}";
                        }
                        break;
                    case "Add":
                        LABLEEntity entity = new LABLEEntity();
                        entity.LABLENAME = Paramas["LableName"];
                        entity.LABLESTR = Paramas["LableStr"];
                        entity.LABLEDES = Paramas["LableDes"];
                        Dao.Add(entity);
                        res = "{\"Success\":1}";
                        break;
                    case "initEdit":
                        LABLEEntity EditEntity = Dao.FindById(Convert.ToInt64(idList));
                        res = GetJsonStr(EditEntity);
                        break;
                    case "EditOne":
                        LABLEEntity Lentity = Dao.FindById(Convert.ToInt64(Paramas["ID"]));
                        Lentity.LABLENAME = Paramas["LableName"];
                        Lentity.LABLESTR = Paramas["LableStr"];
                        Lentity.LABLEDES = Paramas["LableDes"];
                        Dao.Update(Lentity);
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
    protected string GetJsonStr(LABLEEntity EditEntity)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "LableName", EncodeByEscape.GetEscapeStr(EditEntity.LABLENAME));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "LableDes", EncodeByEscape.GetEscapeStr(EditEntity.LABLEDES));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "LableStr", EncodeByEscape.GetEscapeStr(EditEntity.LABLESTR));
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();
    }

    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
        CommonLabel.InnitLableApplication();
    }
}
