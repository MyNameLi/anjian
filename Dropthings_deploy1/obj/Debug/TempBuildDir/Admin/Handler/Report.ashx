<%@ WebHandler Language="C#" Class="Report" %>

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Dropthings.Business.Facade;
using Dropthings.Data;
using Dropthings.Util;

public class Report : BriefReportTemplatePageBase
{
    protected override void InitPageTemplate()
    {
        //简报制作时间
        string timeStr = string.Empty;
        //简报生成的开始时间
        string ReportStartTime = string.Empty;
        //简报生成的结束时间
        string ReportEndTime = string.Empty;
        //简报期号
        string ReportTitle = string.Empty;
        //责任编辑
        string ReportPeople = string.Empty;
        //抄送单位
        string ReportOrg = string.Empty;
        //报送单位
        string ReportObject = string.Empty;
        //电话
        string ReportMobile = string.Empty;
        //制作单位
        string MakeUnit = string.Empty;
        //编制单位
        string OrgUnit = string.Empty;
        //处长
        string ReportDean = string.Empty;
        //编辑
        string ReportEditer = string.Empty;
        //分送单位
        string SendUnit = string.Empty;
        //时政要闻摘选
        string NewsList = string.Empty;
        //网民热议
        string WmryList = string.Empty;
        //网名博客热议
        string WmryBlogList = string.Empty;        
        //舆情综述
        string yqzs = string.Empty;
        //图片类型
        IList<string> PicTypeList = new List<string>(); 
        try
        {                
                 
            Dictionary<string, string> categoryList = new Dictionary<string, string>();
            Dictionary<string, string> categorynewsList = new Dictionary<string, string>();
            Dictionary<string, string> specialList = new Dictionary<string, string>();
            Dictionary<string, string> specialnewsList = new Dictionary<string, string>();
            Dictionary<string, string> clustersList = new Dictionary<string, string>();
            Dictionary<string, string> clustersnewsList = new Dictionary<string, string>();  
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
                else if (key == "make_unit")
                {
                    MakeUnit = EncodeByEscape.GetUnEscapeStr(Context.Request["make_unit"]);
                }
                else if (key == "org_unit")
                {
                    OrgUnit = EncodeByEscape.GetUnEscapeStr(Context.Request["org_unit"]);
                }
                else if (key == "report_dean")
                {
                    ReportDean = EncodeByEscape.GetUnEscapeStr(Context.Request["report_dean"]);
                }
                else if (key == "report_editer")
                {
                    ReportEditer = EncodeByEscape.GetUnEscapeStr(Context.Request["report_editer"]);
                }
                else if (key == "send_unit")
                {
                    SendUnit = EncodeByEscape.GetUnEscapeStr(Context.Request["send_unit"]);
                }
                else if (key == "piclist")
                {
                    string piclist = Context.Request["piclist"];
                    if (!string.IsNullOrEmpty(piclist))
                    {
                        string[] list = piclist.Split(',');
                        foreach (string pic in list) {
                            PicTypeList.Add(pic);
                        }
                    }
                }               
                else
                {
                    string[] l_key = key.Split('_');
                    string info = Context.Request.Form[key];
                    if (l_key[0] == "event")
                    {
                        categoryList.Add(l_key[1], EncodeByEscape.GetUnEscapeStr(l_key[2]));
                        categorynewsList.Add(l_key[1], info);
                    }                    
                    else if (l_key[0] == "special")
                    {
                        specialList.Add(l_key[1], EncodeByEscape.GetUnEscapeStr(l_key[2]));
                        specialnewsList.Add(l_key[1], info);
                    }
                    else if(l_key[0] == "news"){
                        NewsList = info;
                    }
                    else if (l_key[0] == "wmry")
                    {
                        WmryList = info;
                    }
                    else if (l_key[0] == "wmryblog")
                    {
                        WmryBlogList = info;
                    }              
                    else if (l_key[0] == "all") {
                        yqzs = EncodeByEscape.GetUnEscapeStr(info);
                    }
                    else if (l_key[0] == "clusters")
                    {
                        clustersList.Add(l_key[1], EncodeByEscape.GetUnEscapeStr(l_key[1]));
                        clustersnewsList.Add(l_key[1], info);
                    }
                }
            }
            if (PicTypeList.Count > 0) {
                foreach (string pictype in PicTypeList) {
                    this.Document.SetValue(pictype, pictype);
                }
            }
            
            this.Document.SetValue("ReportOrg", ReportOrg);
            this.Document.SetValue("ReportObject", ReportObject);
            this.Document.SetValue("ReportTitle", ReportTitle);
            this.Document.SetValue("ReportPeople", ReportPeople);
            this.Document.SetValue("ReportMobile", ReportMobile);
            this.Document.SetValue("ReportStartTime", ReportStartTime);
            this.Document.SetValue("ReportEndTime", ReportEndTime);                     
            this.Document.SetValue("ReportTime", timeStr);
            this.Document.SetValue("MakeUnit", MakeUnit);
            this.Document.SetValue("OrgUnit", OrgUnit);
            this.Document.SetValue("ReportDean", ReportDean);
            this.Document.SetValue("ReportEditer", ReportEditer);
            this.Document.SetValue("SendUnit", SendUnit);            
            this.Document.SetValue("Yqzs", yqzs);
            this.Document.SetValue("ReportMonthTime", GetMonthTimeStr(timeStr));                       
            this.Document.SetValue("categoryList", ReportResult.GetCategotyList(categoryList, ReportTitle, categorynewsList));
            this.Document.SetValue("specialList", ReportResult.GetCategotyList(specialList, ReportTitle, specialnewsList));
            this.Document.SetValue("clustersList", ReportResult.GetCategotyList(clustersList, ReportTitle, clustersnewsList));
            this.Document.SetValue("NewsList", NewsList);
            this.Document.SetValue("WmryList", WmryList);
            this.Document.SetValue("WmryBlogList", WmryBlogList);
            base.PublishFileName = "zbreport.html";                       
        }
        catch (Exception ex)
        {            
                       
        }               
    }

    private string GetMonthTimeStr(string timeStr)
    {
        DateTime time = Convert.ToDateTime(timeStr);
        return time.ToString("yyyy年MM月");
    }
}
