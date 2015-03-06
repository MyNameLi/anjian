using System;
using System.Collections.Generic;
using System.Web;
using VTemplate.Engine;
using System.Text;
using System.IO;
using System.Web.SessionState;
using System.Diagnostics;
using Dropthings.Util;
using Dropthings.Data;

namespace Dropthings.Business.Facade
{
    public abstract class TemplatePageBase : IHttpHandler
    {        
        #region IHttpHandler 成员
        /// <summary>
        /// 
        /// </summary>
        public bool IsReusable
        {
            get { return false; }
        }
        public string PublishPath = string.Empty;
        public string PublishFileName = string.Empty;
        public string UpdatePath = string.Empty;
        private static Dictionary<string, string> labledict = new Dictionary<string, string>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {   
                      
            this.InitContext(context);
            this.TestType = EncodeByEscape.GetUnEscapeStr(context.Request.Form["Telemplatepath"]);
            if (this.Application == null || this.Session["Lable"] == null)
            { 
               LoadLable();               
            }
            labledict = (Dictionary<string, string>)this.Session["Lable"]; 
            //判断是否是进行测试            
            try
            {
                //输出数据   
                string fileName = string.Empty;
                this.LoadCurrentTemplate(labledict);
                this.InitPageTemplate();
                if (!string.IsNullOrEmpty(PublishFileName))
                {
                    fileName = PublishFileName;
                }
                else
                {
                    fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".html";
                }
                //this.Document.Render(this.Response.Output);
                string path = this.Server.MapPath(this.Session["{#sitepublishpath}"].ToString());
                string outpath = this.Session["{#sitepublishpath}"].ToString().Replace("~", labledict["{#rootpath}"]);
                if (!string.IsNullOrEmpty(PublishPath))
                {
                    path = path + PublishPath;
                }
                FileManage.CreateForder(path);
                path = path + fileName;
                this.Document.RenderTo(path, System.Text.Encoding.UTF8);
                if (!string.IsNullOrEmpty(UpdatePath))
                {
                    outpath = EncodeByEscape.GetEscapeStr(outpath + UpdatePath);
                }
                else {
                    outpath = EncodeByEscape.GetEscapeStr(outpath + fileName);
                }
                string filestr = EncodeByEscape.GetEscapeStr(FileManage.ReadStr(path));
                context.Response.Write("{\"path\":\"" + outpath + "\",\"SucceseCode\":\"1\",\"HtmlStr\":\"" + filestr + "\"}");
            }

            catch (Exception e)
            {
                string str = e.ToString();
                context.Response.Write("{\"ErrorCode\":\"1\",\"Reason\":\"" + str + "\"}");
            }
            
        }
        protected HttpContext Context { get; private set; }
        protected HttpApplicationState Application { get; private set; }
        protected HttpRequest Request { get; private set; }
        protected HttpResponse Response { get; private set; }
        protected HttpServerUtility Server { get; private set; }
        protected HttpSessionState Session { get; private set; }

        private void LoadLable() {
            LABLEEntity.LABLEDAO dao = new LABLEEntity.LABLEDAO();
            IList<LABLEEntity> list = dao.Find("");
            Dictionary<string,string> dict = new Dictionary<string,string>();
            foreach (LABLEEntity entity in list) {
                string key = entity.LABLENAME;
                if (!dict.ContainsKey(key)) { 
                    string str = entity.LABLESTR;
                    dict.Add(key, str);
                }
            }
            this.Session.Add("Lable", dict);
        }

        /// <summary>
        /// 初始化上下文数据
        /// </summary>
        /// <param name="context"></param>
        private void InitContext(HttpContext context)
        {
            this.Context = context;
            this.Application = context.Application;
            this.Request = context.Request;
            this.Response = context.Response;
            this.Server = context.Server;
            this.Session = context.Session;
        }
        #endregion

        #region 当前页面的测试类型
        /// <summary>
        /// 测试类型
        /// </summary>
        private string TestType { get; set; }
        #endregion

        /// <summary>
        /// 进行测试
        /// </summary>
        /// <param name="num"></param>
        /// <param name="count"></param>
        protected void StartTest(int num, int count,Dictionary<string,string> labledict)
        {
            //进行测试
            Stopwatch watcher = new Stopwatch();
            watcher.Start();
            for (int i = 0; i < count; i++)
            {
                //装载当前页面的模板
                this.LoadCurrentTemplate(labledict);
                //初始化页面模板的数据
                this.InitPageTemplate();
                //不输出.只呈现
                string text = this.Document.GetRenderText();
            }
            watcher.Stop();
            Response.Write(string.Format("第{0}次测试(共运行{1}次)花费时间: {2} ms", num, count, watcher.ElapsedMilliseconds));
            Response.Write("<br />");
            Response.Flush();            
        }

        /// <summary>
        /// 当前页面的模板文档对象
        /// </summary>
        protected TemplateDocument Document
        {
            get;
            private set;
        }
        /// <summary>
        /// 当前页面的模板文档的配置参数
        /// </summary>
        protected virtual TemplateDocumentConfig DocumentConfig
        {
            get
            {
                return TemplateDocumentConfig.Default;
            }
        }
        /// <summary>
        /// 是否读取缓存模板
        /// </summary>
        protected virtual bool IsLoadCacheTemplate
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 装载当前页面的模板文档
        /// </summary>
        public virtual void LoadCurrentTemplate(Dictionary<string,string> labledict)
        {
            if (!string.IsNullOrEmpty(this.TestType))
            {
                string filepath = this.Server.MapPath(this.TestType);
                this.LoadTemplateFile(filepath, labledict);                
            }
            else {
                string path = this.Request.FilePath;
                int beginIndex = path.LastIndexOf('/') + 1;
                int endIndex = path.LastIndexOf('.');
                path = path.Substring(beginIndex, endIndex - beginIndex);
                this.LoadTemplateFile(this.Server.MapPath("../reporttemplate/" + path + ".vm"), labledict);
            }
        }
        /// <summary>
        /// 装载模板文件
        /// </summary>
        /// <param name="fileName"></param>
        protected virtual void LoadTemplateFile(string fileName,Dictionary<string,string> labledict)
        {
            this.Document = null;
            /*if ("cache".Equals(this.TestType, StringComparison.InvariantCultureIgnoreCase) || this.IsLoadCacheTemplate)
            {
                //测试缓存模板文档
                this.Document = TemplateDocument.FromFileCache(fileName, Encoding.UTF8, this.DocumentConfig);
            }
            else
            {*/
                //测试实例模板文档
                this.Document = new TemplateDocument(fileName, Encoding.UTF8, this.DocumentConfig);
            /*}*/
        }

        /// <summary>
        /// 初始化当前页面模板数据
        /// </summary>
        protected abstract void InitPageTemplate();
    }
}
