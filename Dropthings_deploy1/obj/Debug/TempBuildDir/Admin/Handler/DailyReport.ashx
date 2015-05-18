<%@ WebHandler Language="C#" Class="DailyReport" %>
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Dropthings.Business.Facade;
using Dropthings.Data;
using Dropthings.Util;

public class DailyReport : BriefReportTemplatePageBase
{
    protected override void InitPageTemplate()
    {
        string timeStr = string.Empty;
        string ReportStartTime = string.Empty;
        string ReportEndTime = string.Empty;
        string ReportTitle = string.Empty;
        string ReportPeople = string.Empty;
        string ReportOrg = string.Empty;
        string ReportObject = string.Empty;
        string ReportMobile = string.Empty;    
        try
        {
            Dictionary<string, string> LeaderDict = new Dictionary<string, string>();           
            foreach (string key in Context.Request.Form.Keys)
            {
                if (key == "time_str")
                {
                    timeStr = Context.Request.Form[key];
                }
                else if (key == "report_start_time")
                {
                    ReportStartTime = Context.Request["report_start_time"];
                }
                else if (key == "report_end_time")
                {
                    ReportEndTime = Context.Request["report_end_time"];
                }
                else if (key == "report_title")
                {
                    ReportTitle = EncodeByEscape.GetUnEscapeStr(Context.Request["report_title"]);
                }
                else if (key == "report_people")
                {
                    ReportPeople = EncodeByEscape.GetUnEscapeStr(Context.Request["report_people"]);
                }
                else if (key == "report_org")
                {
                    ReportOrg = EncodeByEscape.GetUnEscapeStr(Context.Request["report_org"]);
                }
                else if (key == "report_object")
                {
                    ReportObject = EncodeByEscape.GetUnEscapeStr(Context.Request["report_object"]);
                }
                else if (key == "report_mobile")
                {
                    ReportMobile = EncodeByEscape.GetUnEscapeStr(Context.Request["report_mobile"]);
                }
                else
                {
                    string[] l_key = EncodeByEscape.GetUnEscapeStr(key).Split('@');
                    string url_list = Context.Request.Form[key];
                    LeaderDict.Add(l_key[0], url_list);
                }
            }
            this.Document.SetValue("ReportOrg", ReportOrg);
            this.Document.SetValue("ReportObject", ReportObject);
            this.Document.SetValue("ReportTitle", ReportTitle);
            this.Document.SetValue("ReportPeople", ReportPeople);
            this.Document.SetValue("ReportMobile", ReportMobile);
            this.Document.SetValue("ReportTime", GetTimeStr(ReportStartTime));
            //this.Document.SetValue("DailyList", DailyReportResult.GetDailyEntityList(ReportStartTime));
            this.Document.SetValue("LearderInfo", LeaderDict);
            this.Document.SetValue("LearderInfoKeys", LeaderDict.Keys);
            //this.Document.SetValue("StatisticInfoPic", DailyReportResult.GetStatisticInfoPic(ReportStartTime, ReportEndTime, ReportTitle));
            base.PublishFileName = "Daily.html";            
           
        }
        catch (Exception ex)
        {           
            Context.Response.Write("{\"Error\":\"1\",\"error_msg\":\""+ex.ToString()+"\"}");            
        }               
    }    
    private string GetTimeStr(string timestr){
        string[] str = timestr.Split('/');
        string ltimestr = str[2] + "-" + str[1] + "-" + str[0];
        DateTime time = Convert.ToDateTime(ltimestr);
        return time.ToString("yyyy年MM月dd日");
    }
}
