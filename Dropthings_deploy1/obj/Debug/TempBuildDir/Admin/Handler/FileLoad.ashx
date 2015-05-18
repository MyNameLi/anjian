<%@ WebHandler Language="C#" Class="FileLoad" %>
using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using System.Text;
using Dropthings.Util;
using Dropthings.Data;
using Dropthings.Business.Facade;

public class FileLoad : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        string res = string.Empty;
        try
        {
            if (context.Session["Lable"] == null) {
                CommonLabel.InnitLableApplication();
            }
            Dictionary<string, string> labledict = (Dictionary<string, string>)context.Session["Lable"];
            string savepath = string.Empty;
            string rootpath = string.Empty;
            rootpath = labledict["{#rootpath}"];
            savepath = context.Session["{#sitepublishpath}"] + "upload/";            
            HttpPostedFile file = context.Request.Files[0];
            string extension = IOFile.Extension(file.FileName);
            string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
            IOFile.UploadFile(file, filename, savepath);
            string savefilepath = EncodeByEscape.GetEscapeStr(savepath.Replace("~", rootpath) + filename);
            res = "{\"Success\":1,\"path\" : \"" + savefilepath + "\"}";
        }
        catch(Exception e) {
            res = "{\"Error\":1,\"ErrorStr\" : \"" + e.ToString() + "\"}";
        }
        context.Response.Write(res);
        context.Response.End();
    }

   
    public bool IsReusable {
        get {
            return false;
        }
    }
}