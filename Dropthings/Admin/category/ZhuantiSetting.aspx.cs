using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Admin_category_ZhuantiSetting : System.Web.UI.Page
{
    static string xmlPath = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            xmlPath = Server.MapPath("../../xmldata/zhuanti.xml");
            GetTopic();
        }
        else
        {
            Ajax();
        }
    }
    public void Ajax()
    {
        string switch_on = Request["act"];
        if (string.IsNullOrEmpty(switch_on))
        {
            switch_on = "";
        }
        switch (switch_on)
        {
            case "delxmlnode":
                DelXmlNode();
                break;
            default:
                break;
        }
    }
    public void CreateHistoryTopic()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(xmlPath);

        XmlNode newNode = doc.CreateElement("historytopic");
        XmlNode url = doc.CreateElement("url");
        url.InnerText = this.ztUrl.Text;
        XmlNode imgurl = doc.CreateElement("imgurl");
        imgurl.InnerText = this.txt_imgpath.Text;
        XmlNode title = doc.CreateElement("title");
        title.InnerText = this.ztTitle.Text;
        XmlNode data = doc.CreateElement("data");
        data.InnerText = this.ztDate.Text;
        XmlNode status = doc.CreateElement("status");
        status.InnerText = "0";

        newNode.AppendChild(url);
        newNode.AppendChild(imgurl);
        newNode.AppendChild(title);
        newNode.AppendChild(data);
        newNode.AppendChild(status);

        doc.SelectSingleNode("data").AppendChild(newNode);
        doc.Save(xmlPath);
    }
    public void DelXmlNode()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(xmlPath);
        XmlNodeList nodeList = doc.SelectNodes("data/historytopic");
        string url = string.IsNullOrEmpty(Request["url"]) ? "" : Request["url"].ToString().Trim();

        for (int i = 0; i < nodeList.Count; i++)
        {
            if (nodeList[i]["url"].InnerText == url)
            {
                doc.RemoveChild(nodeList[i]);
            }
        }
        doc.Save(xmlPath);

    }
    public void UpdateXmlNode(XmlNode newNode)
    {
        //childNodes["url"].InnerText = this.ztUrl.Text;
        //childNodes["imgurl"].InnerText = this.txt_imgpath.Text;
        //childNodes["title"].InnerText = this.ztTitle.Text;
        //childNodes["data"].InnerText = this.ztDate.Text;
        XmlDocument doc = new XmlDocument();
        doc.Load(xmlPath);
        XmlNodeList nodeList = doc.SelectNodes("data/historytopic");
        for (int i = 0; i < nodeList.Count; i++)
        {
            if (nodeList[i]["url"].InnerText == newNode["url"].InnerText)
            {
                //nodeList[i]["url"].InnerText = this.ztUrl.Text;
                nodeList[i]["imgurl"].InnerText = newNode["imgurl"].InnerText;
                nodeList[i]["title"].InnerText = newNode["title"].InnerText;
                nodeList[i]["data"].InnerText = newNode["data"].InnerText;
            }
        }
        doc.Save(xmlPath);
    }

    public void GetTopic()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(xmlPath);
        XmlNodeList list = doc.SelectNodes("data/zhuanti");
        XmlNode childNodes = list[0];
        this.ztUrl.Text = childNodes["url"].InnerText;
        this.txt_imgpath.Text = childNodes["imgurl"].InnerText;
        this.ztTitle.Text = childNodes["title"].InnerText;
        this.ztDate.Text = childNodes["data"].InnerText;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(xmlPath);
        XmlNodeList list = doc.SelectNodes("data/zhuanti");
        XmlNode childNodes = list[0];
        string orgUrl = childNodes["url"].InnerText;
        string newUrl = this.ztUrl.Text;

        childNodes["url"].InnerText = this.ztUrl.Text;
        childNodes["imgurl"].InnerText = this.txt_imgpath.Text;
        childNodes["title"].InnerText = this.ztTitle.Text;
        childNodes["data"].InnerText = this.ztDate.Text;
        doc.Save(xmlPath);
        //创建历史专题
        if (orgUrl.Trim() != newUrl.Trim())
        {
            CreateHistoryTopic();
        }
        else
        {
            UpdateXmlNode(childNodes);
        }
    }


    public class TopicEntity
    {
        string Url { get; set; }
        string ImgUrl { get; set; }
        string Title { get; set; }
        string Date { get; set; }
        string Status { get; set; }
    }

}