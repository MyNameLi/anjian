<%@ WebHandler Language="C#" Class="ManageFile" %>

using System;
using System.Web;
using System.Text;
using System.IO;
using Dropthings.Util;
using Dropthings.Data;
using Dropthings.Business.Facade;
using System.Threading;

public class ManageFile : IHttpHandler
{    
    public void ProcessRequest(HttpContext context)
    {
        string type = context.Request["type"];
        string reportID = context.Request["id"];
        string timeStr = context.Request["time_str"];
        string ReportTitle = EncodeByEscape.GetUnEscapeStr(context.Request["report_title"]);
        string ReportPeople = context.Request["report_people"];
        string path = context.Server.MapPath("../reportcontent/");
        string fileName = context.Request["file_name"];
        string Ecoding = context.Request["ecoding"];
        string htmlStr = EncodeByEscape.GetUnEscapeStr(context.Request["html_str"]);
        string checkstr = context.Request["chec_kstr"];
        switch (type)
        {
            case "editFile":
                string FileTimeStr = timeStr + DateTime.Now.Hour.ToString() + "时" + DateTime.Now.Minute.ToString() + "分" + DateTime.Now.Second.ToString() + "秒";
                string CopyPath = string.Format("{0}({1}{2}-{3}-{4}).doc", ReportTitle, timeStr, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                string newFilePath = path + CopyPath;
                FileStream SaveFile = File.Create(newFilePath);
                byte[] data = Encoding.GetEncoding(Ecoding).GetBytes(htmlStr);
                SaveFile.Write(data, 0, data.Length);
                SaveFile.Flush();
                SaveFile.Close();
                REPORTLISTEntity entity = new REPORTLISTEntity();
                entity.TITLE = ReportTitle;
                entity.CREATER = ReportPeople;
                entity.URL = CopyPath;
                entity.CREATETIME = Convert.ToDateTime(FileTimeStr);
                entity.STATUS = checkstr;
                entity.TYPE = 1;
                ReportListFacade.Add(entity);
                context.Response.Write("{\"SucceseCode\":\"1\",\"path\":\"content/" + CopyPath + "\"}");
                break;
            case "lookFile":
                StringBuilder htmlstr = new StringBuilder();
                string[] fileList = Directory.GetFiles(path);
                foreach (string file in fileList)
                {
                    htmlstr.AppendFormat("<li><a href=\"content/{0}\">{1}</a></li>", getFileName(file), getFileName(file).Split('.')[0]);
                }
                context.Response.Write("{\"SucceseCode\":\"1\",\"list\":\"" + EncodeByEscape.GetEscapeStr(htmlstr.ToString()) + "\"}");
                break;
            case "deleteFile":
                try
                {
                    FileManage.DeleteFile(context.Server.MapPath("~/Admin/reportcontent/") + fileName);
                    if (ReportListFacade.delete(reportID))
                    {
                        context.Response.Write("{\"SucceseCode\":\"1\"}");
                    }
                    else {
                        context.Response.Write("{\"SucceseCode\":\"0\",\"error\":\"删除数据库记录失败！\"}");
                    }
                }
                catch (Exception exec)
                {
                    context.Response.Write("{\"SucceseCode\":\"0\",\"error\":\"" + exec.Message + "\"}");
                }
                break;
            case "uploadreport":
                string reportName = EncodeByEscape.GetUnEscapeStr(context.Request["reportname"]);
                string reportCreater = EncodeByEscape.GetUnEscapeStr(context.Request["reportcreater"]);
                string reportCreateTime = EncodeByEscape.GetUnEscapeStr(context.Request["reportcreatetime"]);
                string reportType = context.Request["reporttype"];
                string reportTag = context.Request["reporttag"];
                string savepath = "../reportcontent/";
                HttpPostedFile reportfile = context.Request.Files[0];
                string filename = getFileName(reportfile.FileName);
                IOFile.UploadFile(reportfile, filename, savepath);
                REPORTLISTEntity addentity = new REPORTLISTEntity();
                addentity.TITLE = reportName;
                addentity.CREATER = reportCreater;
                addentity.URL = filename;
                addentity.CREATETIME = Convert.ToDateTime(reportCreateTime);
                addentity.STATUS = reportTag;
                addentity.TYPE = Convert.ToInt32(reportType);
                ReportListFacade.Add(addentity);
                context.Response.Write("{\"Success\":\"1\"}");
                break;
            default:                
                break;
        }
    }

    private string getFileName(string fileName)
    {
        int index = fileName.LastIndexOf("\\");
        return fileName.Substring(index + 1);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}

