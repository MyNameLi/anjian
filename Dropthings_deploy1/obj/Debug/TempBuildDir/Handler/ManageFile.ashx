<%@ WebHandler Language="C#" Class="ManageFile" %>

using System;
using System.Web;
using System.Text;
using System.IO;
using Dropthings.Util;
using Dropthings.Data;
using Dropthings.Business.Facade;

public class ManageFile : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {
        string type = context.Request["type"];
        string timeStr = context.Request["time_str"];
        string ReportTitle = context.Request["report_title"];
        string ReportPeople = context.Request["report_people"];
        string path = context.Server.MapPath("~/content/");
        string fileName = context.Request["file_name"];
        switch (type)
        { 
            case "editFile":
                string FileTimeStr = timeStr + DateTime.Now.Hour.ToString() + "时" + DateTime.Now.Minute.ToString() + "分" + DateTime.Now.Second.ToString() + "秒";
                string CopyPath = string.Format("{0}({1}{2}-{3}-{4}).html", ReportTitle, timeStr, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                File.Copy(path +fileName+ ".html", path + CopyPath);
                File.Delete(path +fileName+ ".html");
                REPORTLISTEntity entity = new REPORTLISTEntity();
                entity.TITLE = ReportTitle;
                entity.CREATER = ReportPeople;
                entity.URL = CopyPath;
                entity.CREATETIME = Convert.ToDateTime(FileTimeStr);
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
            default:
                break;
        }
    }

    private string getFileName(string fileName)
    {
        int index = fileName.LastIndexOf("\\");
        return fileName.Substring(index + 1);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}

