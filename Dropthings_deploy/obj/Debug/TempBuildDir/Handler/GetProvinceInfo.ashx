<%@ WebHandler Language="C#" Class="GetProvinceInfo" %>

using System;
using System.Web;
using System.Text;
using Dropthings.Business.Facade;

public class GetProvinceInfo : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/json";
        string act = context.Request["action"];
        string stratTime = context.Request["startTime"];
        if (stratTime == null || stratTime == "")
            stratTime = GetTime(context.Request["defaultTime"], int.Parse(context.Request["defaultTimeNumber"]));
        string endTime = context.Request["endTime"];
        if (endTime == null || endTime == "")
            endTime = DateTime.Now.ToString("yyyy/MM/dd");

        string result = "";
        switch (act)
        {
            case "query":
                result = GetCityInfo(context, stratTime, endTime);
                context.Response.Write(result);
                break;
            case "all":
                result = InitMapData(stratTime, endTime);//获取当天数据
                context.Response.Write(result);
                break;
            default:
                break;
        }
    }
    //初始化地图
    private string InitMapData(string stratTime, string endTime)
    {
        var resultTable = CityTotalHitsFacade.GroupByProvince(stratTime, endTime);
        StringBuilder dbuilder = new StringBuilder();
        dbuilder.Append("\"data\":[");
        int count = 0;
        foreach (System.Data.DataRow item in resultTable.Rows)
        {
            int totalhits = item[1] == null ? 0 : int.Parse(item[1].ToString());
            count += totalhits;
            dbuilder.Append("{");
            dbuilder.AppendFormat("\"name\":\"{0}\",\"value\":{1},\"startTime\":\"{2}\",\"endTime\":\"{3}\"", item[0], totalhits, stratTime, endTime);
            dbuilder.Append("},");
        }
        if (dbuilder.Length > 10)
            dbuilder.Remove(dbuilder.Length - 1, 1);
        dbuilder.Append("]");
        dbuilder.Append(",\"count\":" + count);
        dbuilder.Append(",\"sTime\":\"" + stratTime + "\"");
        dbuilder.Append(",\"eTime\":\"" + endTime + "\"");
        return "{" + dbuilder.ToString() + "}";
    }

    private string GetCityInfo(HttpContext context, string stratTime, string endTime)
    {
        //获取查询条件
        string cityName = context.Request["cityname"].ToString();
        //执行查询语句
        IdolQuery provinceIdolQuery = IdolQueryFactory.GetDisStyle(context.Request["action"]);
        QueryParamEntity provinceIdolEntity = QueryParamsDao.GetEntity(context);
        provinceIdolEntity.Text = CategoryFacade.GetCategoryEntityByExpress(cityName).KEYWORD;
        provinceIdolEntity.MinDate = DateTime.Parse(stratTime).ToString("dd/MM/yyyy");
        provinceIdolEntity.MaxDate = DateTime.Parse(endTime).ToString("dd/MM/yyyy");
        //执行Idol查询数据库
        provinceIdolQuery.queryParamsEntity = provinceIdolEntity;
        String IdolEntityList = provinceIdolQuery.GetHtmlStr();
        //foreach (IdolNewsEntity item in IdolEntityList)
        //{
        //    //item.TotalCount
        //}
        return IdolEntityList;
    }

    /*时间截取*/
    private string GetTime(string action, int number)
    {
        DateTime timeNow = DateTime.Now;
        string result = "";
        switch (action)
        {
            case "day":
                result = timeNow.AddDays(number).ToString("yyyy/MM/dd");
                break;
            case "month":
                result = timeNow.ToString("yyyy/MM/01");
                break;
            default:
                break;
        }
        return result;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    }