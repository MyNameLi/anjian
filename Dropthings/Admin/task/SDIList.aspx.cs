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

public partial class Admin_task_SDIList : System.Web.UI.Page
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

        PagerList.RecordCount = CONTENTTOPICFacade.GetPagerCount("");
        DataTable dt = CONTENTTOPICFacade.GetPagerDt("", null, PagerList.PageSize, PagerList.CurrentPageIndex);
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
                        CONTENTTOPICFacade.delete(idList);
                        res = "{\"Success\":1}";
                        break;
                    case "remove":
                        if (CONTENTTOPICFacade.delete(Convert.ToInt32(idList)))
                        {
                            res = "{\"Success\":1}";
                        }
                        break;
                    case "Add":
                        CONTENTTOPICEntity entity = new CONTENTTOPICEntity();
                        entity.NEWSURL = Paramas["NEWSURL"];
                        entity.NEWSTITLE = Paramas["NEWSTITLE"];
                        entity.KEYWORD = Paramas["KEYWORD"];
                        entity.TASKSTATUS = "0";
                        entity.RECEIVEDATE = DateTime.Now;
                        CONTENTTOPICFacade.add(entity);
                        res = "{\"Success\":1}";
                        break;
                    case "initEdit":
                        CONTENTTOPICEntity EditEntity = CONTENTTOPICFacade.GetEntityById(Convert.ToInt64(idList));
                        res = GetJsonStr(EditEntity);
                        break;
                    case "EditOne":
                        CONTENTTOPICEntity Lentity = CONTENTTOPICFacade.GetEntityById(Convert.ToInt64(Paramas["ID"]));
                        Lentity.NEWSURL = Paramas["NEWSURL"];
                        Lentity.NEWSTITLE = Paramas["NEWSTITLE"];
                        Lentity.KEYWORD = Paramas["KEYWORD"];
                        Lentity.RECEIVEDATE = DateTime.Now;                       
                        CONTENTTOPICFacade.update(Lentity);
                        res = "{\"Success\":1}";
                        break;
                    case "starttask":
                        if (CONTENTTOPICFacade.updateset(Convert.ToInt32(idList), "TASKSTATUS", "1"))
                        {
                            res = "{\"Success\":1}";
                        }
                        break;
                    case "getdetail":
                        string taskid = Request["taskid"];
                        string strorder = Request["orderby"];
                        int Start = Convert.ToInt32(Request["start"]);
                        int PageSize = Convert.ToInt32(Request["page_size"]);
                        res = ContentClusterFacade.GetDataStr(taskid, strorder, Start, PageSize);
                        break;
                    case "getmore":
                        string clusterid = Request["clusterid"];
                        string more_strorder = Request["orderby"];
                        int more_Start = Convert.ToInt32(Request["start"]);
                        int more_PageSize = Convert.ToInt32(Request["page_size"]);
                        res = ContentClusterDetailFacade.GetDataStr(clusterid, more_strorder, more_Start, more_PageSize);
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
    protected string GetJsonStr(CONTENTTOPICEntity EditEntity)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "NEWSURL", EncodeByEscape.GetEscapeStr(EditEntity.NEWSURL));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "NEWSTITLE", EncodeByEscape.GetEscapeStr(EditEntity.NEWSTITLE));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "KEYWORD", EncodeByEscape.GetEscapeStr(EditEntity.KEYWORD));
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();
    }
}
