using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.JScript;

public partial class Admin_opinionFeedback_Preview : System.Web.UI.Page
{
    Dropthings.Data.REPORTLISTEntity.REPORTLISTDAO Dao = new Dropthings.Data.REPORTLISTEntity.REPORTLISTDAO();
    public Dropthings.Data.REPORTLISTEntity Entity = new Dropthings.Data.REPORTLISTEntity();
    public string StrContext { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInfo();
        }
    }
    public void LoadInfo()
    {
        string id = "562";// Request["id"];
        Entity = Dao.FindById(int.Parse(id));

        //entity.ID;
        //entity.TITLE;
        //entity.OPINIONCONTENT;
        //entity.CREATETIME;
        //entity.CREATER
        StrContext = GlobalObject.unescape(Entity.OPINIONCONTENT);
    }
}