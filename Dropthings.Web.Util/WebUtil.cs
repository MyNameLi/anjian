#region Header

// Copyright (c) Omar AL Zabir. All rights reserved.
// For continued development and updates, visit http://msmvps.com/omar

#endregion Header

/// <summary>
/// Summary description for ActionValidator
/// </summary>
namespace Dropthings.Web.Util
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Text;
    using System.Web.SessionState;

    public static class WebUtil
    {
        #region Methods

        public static string GetEncodeUrl(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                return System.Web.HttpUtility.UrlEncode(url);
            }
            else
            {
                return "";
            }
        }

        public static void ShowMessage(Label label, string message, bool isError)
        {
            label.ForeColor = System.Drawing.Color.DodgerBlue;
            label.Text = message;

            if (isError)
            {
                label.ForeColor = System.Drawing.Color.Red;
                //label.Font.Bold = true;
            }
        }

        #endregion

        #region 显示消息提示对话框
        /// <summary>
        /// 显示消息提示对话框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void Show(System.Web.UI.Page page, string msg)
        {
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "message", "alert('" + msg + "');", true);
        }

        /// <summary>
        /// 控件点击 消息确认提示框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void ShowConfirm(System.Web.UI.WebControls.WebControl Control, string msg)
        {
            //Control.Attributes.Add("onClick","if (!window.confirm('"+msg+"')){return false;}");
            Control.Attributes.Add("onclick", "return confirm('" + msg + "');");
        }

        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        public static void ShowAndRedirect(System.Web.UI.Page page, string msg, string url)
        {
            //Response.Write("<script>alert('帐户审核通过！现在去为企业充值。');window.location=\"" + pageurl + "\"</script>");
            //page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg + "');window.location=\"" + url + "\"</script>");
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "message", "alert('" + msg + "');window.location='" + url + "';", true);
        }
        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        public static void ShowAndRedirects(System.Web.UI.Page page, string msg, string url)
        {
            StringBuilder Builder = new StringBuilder();
            //Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("alert('{0}');", msg);
            Builder.AppendFormat("top.location.href='{0}';", url);
            //Builder.Append("</script>");
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "message", Builder.ToString(), true);
            //page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());
        }

        /// <summary>
        /// 输出自定义脚本信息
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="script">输出脚本</param>
        public static void ResponseScript(System.Web.UI.Page page, string script)
        {
            //page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>" + script + "</script>");
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "message", script, true);
        }

        #endregion

        #region 从HttpRequest中获取指定名称的QueryString值| added by 张涛2009-09-28
        /// <summary>
        /// 从查询字符串中获取String类型的值
        /// </summary>
        /// <param name="request"></param>
        /// <param name="queryStringName"></param>
        /// <returns></returns>
        public static String GetStringFromSession(string sessionName)
        {
            HttpSessionState session = HttpContext.Current.Session;

            string result = string.Empty;
            if (null != session[sessionName])
                result = session[sessionName].ToString();
            return result;
        }
        /// <summary>
        /// 从查询字符串中获取Int32类型的值
        /// </summary>
        /// <param name="request"></param>
        /// <param name="queryStringName"></param>
        /// <returns></returns>
        public static Int32 TryGetInt32FromSession(string sessionName)
        {
            HttpSessionState session = HttpContext.Current.Session;

            string result = string.Empty;
            Int32 value = 0;
            if (null != session[sessionName])
                result = session[sessionName].ToString();
            int.TryParse(result, out value);
            return value;
        }
        /// <summary>
        /// 从查询字符串中获取Boolean类型的值
        /// </summary>
        /// <param name="request"></param>
        /// <param name="queryStringName"></param>
        /// <returns></returns>
        public static Boolean TryGetBooleanFromSession(string sessionName)
        {
            HttpSessionState session = HttpContext.Current.Session;

            string result = string.Empty;
            bool value = false;
            if (null != session[sessionName])
                result = session[sessionName].ToString();
            bool.TryParse(result, out value);
            return value;
        }

        /// <summary>
        /// 从查询字符串中获取Decimal类型的值
        /// </summary>
        /// <param name="request"></param>
        /// <param name="queryStringName"></param>
        /// <returns></returns>
        public static Decimal TryGetDecimalFromSession(string sessionName)
        {
            HttpSessionState session = HttpContext.Current.Session;

            string result = string.Empty;
            Decimal value = 0;
            if (null != session[sessionName])
                result = session[sessionName].ToString();
            Decimal.TryParse(result, out value);
            return value;
        }

        /// <summary>
        /// 从查询字符串中获取String类型的值
        /// </summary>
        /// <param name="request"></param>
        /// <param name="queryStringName"></param>
        /// <returns></returns>
        public static String GetStringFromQueryString(string queryStringName)
        {
            HttpRequest request = HttpContext.Current.Request;

            string result = string.Empty;
            if (!string.IsNullOrEmpty(request.QueryString[queryStringName]))
                result = request.QueryString[queryStringName].ToString();
            return result;
        }
        /// <summary>
        /// 从查询字符串中获取Int32类型的值
        /// </summary>
        /// <param name="request"></param>
        /// <param name="queryStringName"></param>
        /// <returns></returns>
        public static Int32 TryGetInt32FromQueryString(string queryStringName)
        {
            HttpRequest request = HttpContext.Current.Request;

            string result = string.Empty;
            Int32 value = 0;
            if (!string.IsNullOrEmpty(request.QueryString[queryStringName]))
                result = request.QueryString[queryStringName].ToString();
            int.TryParse(result, out value);
            return value;
        }
        /// <summary>
        /// 从查询字符串中获取Boolean类型的值
        /// </summary>
        /// <param name="request"></param>
        /// <param name="queryStringName"></param>
        /// <returns></returns>
        public static Boolean TryGetBooleanFromQueryString(string queryStringName)
        {
            HttpRequest request = HttpContext.Current.Request;

            string result = string.Empty;
            bool value = false;
            if (!string.IsNullOrEmpty(request.QueryString[queryStringName]))
                result = request.QueryString[queryStringName].ToString();
            bool.TryParse(result, out value);
            return value;
        }

        /// <summary>
        /// 从查询字符串中获取Decimal类型的值
        /// </summary>
        /// <param name="request"></param>
        /// <param name="queryStringName"></param>
        /// <returns></returns>
        public static Decimal TryGetDecimalFromQueryString(string queryStringName)
        {
            HttpRequest request = HttpContext.Current.Request;
            string result = string.Empty;
            Decimal value = 0;
            if (!string.IsNullOrEmpty(request.QueryString[queryStringName]))
                result = request.QueryString[queryStringName].ToString();
            Decimal.TryParse(result, out value);
            return value;
        }
        #endregion Methods
    }
}