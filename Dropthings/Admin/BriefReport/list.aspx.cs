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
using System.Configuration;
using System.Xml;

public partial class Admin_BriefReport_list : System.Web.UI.Page
{
    protected REPORTLISTEntity.REPORTLISTDAO Dao = new REPORTLISTEntity.REPORTLISTDAO();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PageDataBind();
        }
    }
    protected void PageDataBind()
    {
        string SessionKey = ConfigurationManager.AppSettings["SessionKey"].ToString();
        string[] userinfo = UserFacade.GetUser();
        string queryCondition = "STATUS=3";// string.Empty;
        if (userinfo != null && userinfo.Length > 0)
        {
            int userID = Convert.ToInt32(UserFacade.GetUserId());
            List<string> step = GetStepByUserID(userID);
            if (step.Contains("first") && step.Contains("second"))
            {
                queryCondition = "";
            }
            else if (step.Contains("second"))
            {
                queryCondition = "STATUS!=1";
            }
            else if (step.Contains("first"))
            {
                queryCondition = "STATUS!=2";
            }
            else
            {
                queryCondition = "STATUS=3";
            }
            queryCondition += queryCondition == "" ? "Type<5" : " AND Type<5";
            PagerList.RecordCount = Dao.GetPagerRowsCount(queryCondition, null);
            DataTable dt = Dao.GetPager(queryCondition, null, " CREATETIME DESC", PagerList.PageSize, PagerList.CurrentPageIndex);
            if (dt != null)
            {
                dataList.DataSource = dt;
            }
            dataList.DataBind();
        }
    }

    private List<string> GetStepByUserID(int userID)
    {
        return GetAuditData(userID.ToString());
    }

    private List<string> GetAuditData(string attrValue)
    {
        XmlDocument xmlDoc = new XmlDocument();
        string filePath = Server.MapPath("~/Audit.config.xml");
        xmlDoc.Load(filePath);
        List<string> steps = new List<string>();
        if (xmlDoc.ChildNodes.Count > 0)
        {
            XmlNodeList list = xmlDoc.SelectNodes("//step");
            if (list.Count > 0)
            {
                foreach (XmlNode node in list)
                {
                    string name = node.Attributes["name"].Value.ToLower().Trim();

                    XmlNodeList auditorList = node.SelectNodes("auditors/auditor");
                    foreach (XmlNode child in auditorList)
                    {
                        string cid = child.Attributes["id"].Value;
                        if (cid == attrValue)
                        {
                            steps.Add(name);
                            break;
                        }
                    }
                }
            }
        }
        return steps; // string.Join(",", steps.ToArray());
    }

    protected void PagerList_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        PagerList.CurrentPageIndex = e.NewPageIndex;
        PageDataBind();
    }
}
