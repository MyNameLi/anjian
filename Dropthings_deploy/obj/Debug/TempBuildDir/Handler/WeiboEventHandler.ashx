<%@ WebHandler Language="C#" Class="WeiboEventHandler" %>

using System;
using System.Web;
using Dropthings.Business.Facade;
using System.Collections.Generic;
using Dropthings.Data;
using System.Data;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using Dropthings.Data.SqlServerEntity;
using WeiboDataSource;
public class WeiboEventHandler : IHttpHandler {
    //private readonly string SqlConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["WeiboDBStr"].ToString();
    private WeiboDataService wds = null;
    public void ProcessRequest (HttpContext context) {
        string act = context.Request["act"];
        string retJson = string.Empty;
        if (!string.IsNullOrEmpty(act))
        {
            wds = new WeiboDataService();
            SqlHelper sqlh = null;
            string action = context.Request["action"];
            string SelType = context.Request["selType"];
            string fieldName = context.Request["fieldname"];
            string mintime = context.Request["mintime"];
            string maxtime = context.Request["maxtime"];
            string sort = context.Request["sort"];
            bool dcount;
            string documentcount = context.Request["documentcount"];
            string databasematch = context.Request["databasematch"];
            string dateperiod = context.Request["dateperiod"];
            string predict = context.Request["predict"];
            string text = context.Request["text"];
            string disnum = context.Request["disnum"];
            int dnum;
            QueryParamEntity paramEntity = new QueryParamEntity();
            
            paramEntity.action = action;
            paramEntity.FieldName = fieldName;
            paramEntity.Sort = sort;
            paramEntity.DatePeriod = dateperiod;
            if (bool.TryParse(documentcount, out dcount))
            {
                paramEntity.DocumentCount = dcount;
            }
            if (int.TryParse(disnum, out dnum))
            {
                paramEntity.DisNum = dnum;
            }
            paramEntity.DataBase = databasematch;
            paramEntity.Predict = predict;
            try
            {
                switch (act)
                {
                    case "classification":
                        //IdolQuery query = IdolQueryFactory.GetDisStyle(paramEntity.action);
                        //query.queryParamsEntity = paramEntity;
                        //string list = query.GetHtmlStr();
                        break;
                    case "InitTab":
                        sqlh = new SqlHelper("WeiboDBStr");
                        //string SqlSvrsql = "select * from Topic where IsStop=0";
                        string oraSqlWhere = String.Format("parentcate={0}",
                            Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["parentcate"]));
                        List<CATEGORYEntity> categorys = new List<CATEGORYEntity>();
                        CATEGORYEntity.CATEGORYDAO dao = new CATEGORYEntity.CATEGORYDAO();
                        categorys = dao.Find(oraSqlWhere);
                      
                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        retJson = "{\"category\":" + jss.Serialize(categorys) + //",\"topic\":" + jss.Serialize(tes) + 
                            ",\"Success\":1}";
                        break;
                    case "industryCategory":
                        string oraSqlWhere2 = String.Format("parentcate={0}",
                        Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["industryCategory"]));
                        List<CATEGORYEntity> categorys1 = new List<CATEGORYEntity>();
                        CATEGORYEntity.CATEGORYDAO dao1 = new CATEGORYEntity.CATEGORYDAO();
                        categorys = dao1.Find(oraSqlWhere2);
                        List<TopicEntity> tes1 = new List<TopicEntity>();
                        JavaScriptSerializer js1 = new JavaScriptSerializer();
                        retJson = "{\"category\":" + js1.Serialize(categorys) + ",\"Success\":1}";
                        break;
                    case "weiboCountent":
                        paramEntity.DisplayStyle = 8;
                        IdolQuery query = IdolQueryFactory.GetDisStyle(paramEntity.action);
                        query.queryParamsEntity = paramEntity;
                        retJson = query.GetHtmlStr();
                        break;
                    case "addTopic":
                        sqlh = new SqlHelper("WeiboDBStr");
                        string tname= context.Request["tName"].Replace("'","");
                        string keyword = context.Request["keyword"].Replace("'", "");
                        string industry = context.Request["industry"].Replace("'", "");
                        string sid = context.Request["sid"];
                        string stype = context.Request["submittype"];
//                        string addsql = string.Format(@"insert into Topic([Name],[Keywords],[StartTime],[Industry],
//                                                        [OriginalCount],[ForwardCount],[CommentCount],[IsStop],[IsDel])
//                                                        values('{0}','{1}',GETDATE(),'{2}','{3}','{4}','{5}','{6}','{7}')",
//                                                       tname, keyword, industry, 0, 0, 0, 0, 0
//                                                       );
                        //if (!string.IsNullOrEmpty(sid) && !string.IsNullOrEmpty(stype))
                        //{
                        //    if (stype == "edit")
                        //    {
                        //        addsql = string.Format("update topic set [name]='{0}',[keywords]='{1}',[Industry]='{2}' where id='{3}'", tname, keyword, industry, sid);
                        //    }
                        //}
                        

                        if (wds.TopicUpdate(tname, keyword, industry, sid, stype))//sqlh.ExecuteSql(addsql, null) > 0)
                        {
                            retJson = "{\"Success\":1}";
                        }
                        else
                        {
                             retJson = "{\"Success\":0}";
                        }
                        //更新话题统计量，和话题总量
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            context.Response.Write(retJson);
        }
        else
        {
            retJson = "{\"Success\":0});";
        }
    }
    private void InitTopicStatisticData()
    {
        
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}