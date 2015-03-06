<%@ WebHandler Language="C#" Class="User" %>

using System;
using System.Web;
using System.Text;
using Dropthings.Util;

public class User : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string userName = context.Request.Form["User_Name"];
        string passWord = context.Request.Form["Pass_Word"];
        string action = context.Request.Form["action"];
        if (!string.IsNullOrEmpty(action))
        {
            switch (action)
            {
                case "clear_cookie":
                    HttpCookie userLogin = context.Request.Cookies["user_login"];
                    if (userLogin != null)
                    {
                        userLogin.Expires = DateTime.Now.AddDays(-10);
                        userLogin.Value = "";
                        context.Response.SetCookie(userLogin);
                        context.Response.Write("{\"SuccessCode\":1}");
                    }
                    break;
                case "user_login":
                    StringBuilder jsonStr = new StringBuilder();
                    if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(passWord))
                    {
                        if (userName == "admin" || userName == "luolin" || userName == "huangyi")
                        {
                            if (passWord == "admin")
                            {
                                logVisitor(context);
                                HttpCookie userCookie = new HttpCookie("user_login");
                                userCookie.Value = userName;
                                context.Response.SetCookie(userCookie);
                                jsonStr.Append("{\"SuccessCode\":1}");
                            }
                            else
                            {
                                jsonStr.Append("{\"SuccessCode\":0}");
                            }
                        }
                        else
                        {
                            jsonStr.Append("{\"SuccessCode\":0}");
                        }
                    }
                    else
                    {
                        jsonStr.Append("{\"SuccessCode\":0}");
                    }
                    context.Response.Write(jsonStr.ToString());
                    break;
                case "log_visitors":
                    logVisitor(context);
                    break;
                    
                case "checkLogin":
                    
                    break;
                default:
                    break;
            }
        }
     
    }

    private void logVisitor(HttpContext context)
    {
        string pageUrl = context.Request.Form["page_url"].ToString();
        string visitorAddr = context.Request.UserHostAddress;
        //LogWriter.WriteErrLog(visitorAddr + "访问了" + pageUrl);
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}