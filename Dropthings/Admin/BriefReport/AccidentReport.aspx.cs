using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VTemplate.Engine;
using System.Text;
using Dropthings.Data;
using Dropthings.Business.Facade;
using System.IO;

public partial class Admin_BriefReport_AccidentReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ClearInfo();
        }

    }
    /// <summary>
    /// 清空页面数据
    /// </summary>
    public void ClearInfo()
    {
        this.JCData.Text = DateTime.Now.ToString("yyyy-MM-dd");// string.Empty;
        this.Title.Text = string.Empty;
        this.TiteHref.Text = string.Empty;
        this.InformationSource.Text = string.Empty;
        this.OccurrenceTime.Text = string.Empty;
        this.OccurrenceAddress.Text = string.Empty;
        this.NumberOfDeath.Text = "0";
        this.NumberOfInjured.Text = "0";
        this.ReleaseTime.Text = string.Empty;
        this.AccidentProcess.Text = string.Empty;
        this.AccidentStory.Text = string.Empty;
        this.PublicOpinionAnalysis.Text = string.Empty;
        this.CheckShenHe.Checked = false;
        this.Label2.Text = "制作人：" + UserFacade.GetUserName();
        //this.Label1.Text = string.Empty;
    }
    /// <summary>
    /// 上传报告，并记录入库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            this.Label1.Text = "生成...";
            AccidentModel model = GetModel();
            string docName = "事故信息报告-" + DateTime.Parse(model.JCData).ToString("MMdd");
            docName = DcoumentName(docName);
            string pathTemp = Server.MapPath("../reporttemplate/AccidentReport.vm");
            string saveDoccumentPath = Server.MapPath("../reportcontent/" + docName + ".doc");
            TemplateDocument document = new TemplateDocument(pathTemp, Encoding.GetEncoding("gb2312"), TemplateDocumentConfig.Default);

            document.SetValue("entity", model);
            document.RenderTo(saveDoccumentPath, Encoding.UTF8);
            AddREPORTLISTEntity(model, docName);
            this.Label1.Text = "生成成功";
        }
        catch
        {
            this.Label1.Text = "生成错误";
        }

        ClearInfo();
    }
    /// <summary>
    /// 生成报告名称，
    /// 报告名称编号从1开始，如果文件存在重复累计下一个编号
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public string DcoumentName(string name)
    {
        string fileName = "";
        bool existsFile = true;
        int i = 1;
        while (existsFile)
        {
            fileName = name + "-" + i;
            string path = Server.MapPath("../reportcontent/" + fileName + ".doc");
            existsFile = File.Exists(path);
            i = i + 1;
        }
        return fileName;
    }
    /// <summary>
    /// 把文件信息添加到数据库，以方便查询、下载、删除
    /// </summary>
    /// <param name="model"></param>
    /// <param name="docName"></param>
    public void AddREPORTLISTEntity(AccidentModel model, string docName)
    {
        string userName = UserFacade.GetUserName();
        REPORTLISTEntity addentity = new REPORTLISTEntity();
        addentity.TITLE = docName;
        addentity.CREATER = userName;
        addentity.URL = docName + ".doc";//model.JCData + "事故信息.doc";
        addentity.CREATETIME = DateTime.Now;
        addentity.STATUS = CheckShenHe.Checked ? "1" : "3";
        addentity.TYPE = 4;
        ReportListFacade.Add(addentity);
    }
   /// <summary>
   /// 根据填写的数据的报告数据生成一个报告实体
   /// </summary>
   /// <returns></returns>
    public AccidentModel GetModel()
    {
        AccidentModel model = new AccidentModel();
        model.JCData = this.JCData.Text;
        model.Title = this.Title.Text;
        model.TiteHref = this.TiteHref.Text;
        model.InformationSource = this.InformationSource.Text;
        model.OccurrenceTime = this.OccurrenceTime.Text;
        model.OccurrenceAddress = this.OccurrenceAddress.Text;
        model.NumberOfDeath = this.NumberOfDeath.Text;
        model.NumberOfInjured = this.NumberOfInjured.Text;
        model.ReleaseTime = this.ReleaseTime.Text;
        model.AccidentProcess = this.AccidentProcess.Text;
        model.AccidentStory = this.AccidentStory.Text;
        model.PublicOpinionAnalysis = this.PublicOpinionAnalysis.Text;

        return model;
    }
    public class AccidentModel
    {
        /// <summary>
        /// 检测日期
        /// </summary>
        public string JCData { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 标题连接
        /// </summary>
        public string TiteHref { get; set; }
        /// <summary>
        /// 信息来源
        /// </summary>
        public string InformationSource { get; set; }
        /// <summary>
        /// 发生时间
        /// </summary>
        public string OccurrenceTime { get; set; }
        /// <summary>
        /// 发生地点
        /// </summary>
        public string OccurrenceAddress { get; set; }
        /// <summary>
        /// 死亡人数
        /// </summary>
        public string NumberOfDeath { get; set; }
        /// <summary>
        /// 受伤人数
        /// </summary>
        public string NumberOfInjured { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public string ReleaseTime { get; set; }
        /// <summary>
        /// 事故经过
        /// </summary>
        public string AccidentProcess { get; set; }
        /// <summary>
        /// 处理报道
        /// </summary>
        public string AccidentStory { get; set; }
        /// <summary>
        /// 舆情分析
        /// </summary>
        public string PublicOpinionAnalysis { get; set; }
    }
}