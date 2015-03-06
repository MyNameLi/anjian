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

public partial class Admin_Info_List : System.Web.UI.Page
{
    private static string userId = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) {
            userId = UserFacade.GetUserId();
            PageDataBind();
            Ajax();
        }
    }    

    protected void PageDataBind() {
        string strwhere = string.Empty;
        string orderBy = " AddDate DESC,ID DESC";
        if (!string.IsNullOrEmpty(userId)) {
            strwhere = " AccepterId=" + userId;
        }
        PagerList.RecordCount = NoteMessageFacade.GetPagerCount(strwhere);
        DataTable dt = NoteMessageFacade.GetPageDt(strwhere, orderBy, PagerList.PageSize, PagerList.CurrentPageIndex);
        if (dt != null)
        {
            dataList.DataSource = dt;
        }
        dataList.DataBind();
    }

    protected void PagerList_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        PagerList.CurrentPageIndex = e.NewPageIndex;
        PageDataBind();
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
                        NoteMessageFacade.Delete(idList);
                        res = "{\"Success\":1}";
                        break;
                    case "remove":
                        NoteMessageFacade.Delete(Convert.ToInt64(idList));
                        res = "{\"Success\":1}";                        
                        break;    
                    case "lookinfo":
                        
                        NoteMessageEntity entity = NoteMessageFacade.GetEntity(Convert.ToInt64(idList));
                        if (entity.Status.Value == 0) {
                            NoteMessageFacade.UpdateSetStatus(1, Convert.ToInt64(idList)); 
                        }
                        IdolNewsEntity newsEntity = GetNewsEntity(entity.InfoUrl);
                        if (entity != null && newsEntity != null)
                        {
                            res = GetEntityJsonStr(entity, newsEntity);
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

    protected IdolNewsEntity GetNewsEntity(string url) {
        QueryParamEntity paramEntity = new QueryParamEntity();
        paramEntity.Start = 1;
        paramEntity.PageSize = 1;
        paramEntity.MatchReference = url;
        paramEntity.Print = "All";
        IdolQuery query = IdolQueryFactory.GetDisStyle("query");
        query.queryParamsEntity = paramEntity;
        IList<IdolNewsEntity> list = query.GetResultList();
        if (list != null && list.Count > 0)
        {
            return list[0];
        }
        else {
            return null;
        }
    }

    protected string GetEntityJsonStr(NoteMessageEntity entity, IdolNewsEntity newsEntity) {
        StringBuilder jsonstr = new StringBuilder();
        jsonstr.Append("{");
        jsonstr.AppendFormat("\"Message\":\"{0}\",",EncodeByEscape.GetEscapeStr(entity.Message));
        jsonstr.AppendFormat("\"InfoTitle\":\"{0}\",", EncodeByEscape.GetEscapeStr(entity.InfoTitle));
        jsonstr.AppendFormat("\"InfoContent\":\"{0}\",", EncodeByEscape.GetEscapeStr(newsEntity.AllContent));
        jsonstr.Append("\"Success\":1}");
        return jsonstr.ToString();
    }
}
