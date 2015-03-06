using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Dropthings.Util;
using Dropthings.Data.SqlServerEntity;
public partial class Expand_ThesaurusList : System.Web.UI.Page
{
    private keywordsEntity.keywordsDAO keywordDao = new keywordsEntity.keywordsDAO();
    private newsURLEntity.newsURLDAO lexiconWord = new newsURLEntity.newsURLDAO();
    public static string pWhere = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Ajax();
        }
    }
    /// <summary>
    /// DATATABLE转换为Tree形式的JSON格式
    /// </summary>
    /// <param name="rows"></param>
    /// <returns></returns>
    public string DataTableToJson(DataTable dt)
    {

        StringBuilder resultJson = new StringBuilder();
        int rowsCount = dt.Rows.Count;
        if (rowsCount > 0)
        {
            resultJson.Append("[");
            foreach (DataRow item in dt.Rows)
            {
                resultJson.Append("{");
                resultJson.AppendFormat("\"name\":\"{0}\",", item["title"]);
                resultJson.AppendFormat("\"id\":\"{0}\"", item["kid"]);
                resultJson.Append("},");
            }
            resultJson.Remove(resultJson.Length - 1, 1);
            resultJson.Append("]");

        }
        else
        {
            resultJson.Append("null");
        }

        return resultJson.ToString();
    }

    protected string GetJsonStr(keywordsEntity EditEntity)
    {
        StringBuilder JsonStr = new StringBuilder();
        JsonStr.Append("{");
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "kid", EncodeByEscape.GetEscapeStr(EditEntity.kid.ToString()));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "title", EncodeByEscape.GetEscapeStr(EditEntity.title));
        JsonStr.AppendFormat("\"{0}\":\"{1}\",", "keyword", EncodeByEscape.GetEscapeStr(EditEntity.keyword));
        JsonStr.Append("\"Success\":1}");
        return JsonStr.ToString();
    }
    public DataTable GetWords()
    {
        return lexiconWord.GetDataSet("", null).Tables[0];
    }

    protected void Ajax()
    {
        if (Request["ajaxString"] == "1")
        {
            Dictionary<string, string> Paramas = new Dictionary<string, string>();

            foreach (string key in Request.Form.AllKeys)
            {
                Paramas.Add(key, EncodeByEscape.GetUnEscapeStr(Request[key]));
            }
            string res = string.Empty;
            string act = Request["act"];
            string idList = Request["idList"];
            string val = Request["val"];
            try
            {
                switch (act)
                {
                    //左侧树操作
                    //初始化数列表
                    case "initial":
                        DataTable dt = keywordDao.GetDataSet("", null).Tables[0];
                        //DataTable Worddt = GetWords();
                        //RecursiveChild(dt, Worddt.Select());//根据根节点进行查询.
                        res = DataTableToJson(dt);
                        break;
                    //左侧树操作
                    //初始化弹出层数据
                    case "initEdit":
                        keywordsEntity lexiconType = keywordDao.FindById(Convert.ToInt64(idList));
                        res = GetJsonStr(lexiconType);
                        break;
                    //左侧树操作
                    //增加树节点
                    case "Add":
                        keywordsEntity addlexiconType = new keywordsEntity();
                        addlexiconType.title = Paramas["title"];
                        addlexiconType.keyword = Paramas["keyword"];
                        keywordDao.Add(addlexiconType);
                        res = "{\"Success\":1}";
                        break;
                    //左侧树操作
                    //修改树节点
                    case "EditOne":
                        keywordsEntity editlexiconType = keywordDao.FindById(Convert.ToInt64(Paramas["Id"]));
                        editlexiconType.keyword = Paramas["keyword"];
                        editlexiconType.title = Paramas["title"];
                        keywordDao.Update(editlexiconType);
                        res = "{\"Success\":1}";
                        break;
                    //左侧树操作
                    //删除树节点
                    case "remove":
                        keywordsEntity delentity = new keywordsEntity() { kid = int.Parse(idList) };
                        keywordDao.Delete(delentity);
                        res = "{\"Success\":1}";
                        break;
                    case "processed":
                        // keywordsEntity delentity = new keywordsEntity() { kid = int.Parse(idList) };
                        lexiconWord.UpdateEntitys("1", idList);
                        res = "{\"Success\":1}";
                        break;
                    case "ignore":
                        //keywordsEntity delentity = new keywordsEntity() { kid = int.Parse(idList) };
                        lexiconWord.UpdateEntitys("2", idList);
                        res = "{\"Success\":1}";
                        break;
                    default:
                        break;
                }


            }
            catch (Exception ex)
            {
                res = "{\"Error\":1,\"ErrorStr\":\"" + ex.ToString() + "\"}";
            }
            finally
            {
                Response.Write(res);
                Response.End();
            }

        }
    }
}
