using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Xml;
using Dropthings.Data;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Web.Services.Description;

/// <summary>
///UserAPI 的摘要说明
/// </summary>
[WebService(Namespace = "http://10.16.6.100/API/UserAPI.asmx/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ToolboxItem(false)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
[SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.RequestElement)] 

public class UserAPI : System.Web.Services.WebService
{

    public UserAPI()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    private string testXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + "<accounts>"
                                + "<account>"
                                + "	<accId>1381</accId>"
                                + "	<name>姓名</name>"
                                + "	<email>邮箱 </email>"
                                + "	<gender>性别</gender>"
                                + "<employeeNumber>员工编号</employeeNumber>"
                                + "	<telephoneNumber>座机</telephoneNumber>"
                                + "	<mobile>移动电话</mobile>"
                                + "<description>描述</description>"
                                + "<startTime>2011-4-5</startTime>"
                                + "<serialNumber>排序序号</serialNumber>"
                                + "<roomNumber>房间号码</roomNumber>"
                                + "	<position>用户职务</position>"
                                + "<status>用户状态</status>"
                                + "<roleInfo>角色信息</roleInfo>"
                                + "<orgNumber >组织机构ID</orgNumber>"
                                + "<orgName>组织机构名称</orgName>"
                                + "</account>"
                                + "</accounts>";

    [WebMethod]    
    public string addUserInfo(string userInfos)
    {
        List<string> succUserIds = new List<string>();
        List<string> failUserIds = new List<string>();
        string errorMsg = string.Empty;

        XmlDocument docUser = new XmlDocument();
        docUser.LoadXml(userInfos);
        XmlNodeList accounts = docUser.GetElementsByTagName("account");

        if (accounts != null && accounts.Count > 0)
        {
            foreach (XmlNode account in accounts)
            {
                XmlNode userIdNode = account.SelectSingleNode("accId");

                try
                {
                    if (userIdNode != null && !string.IsNullOrEmpty(userIdNode.InnerText))
                    {
                        UsersEntity user = new UsersEntity();
                        user.USERNAME = GetXmlNodeInnerText(account.SelectSingleNode("name"));
                        user.CREATEDATE = DateTime.Now;
                        user.PASSWORD = "123456";                        
                        user.DESCRIPTION = GetXmlNodeInnerText(account.SelectSingleNode("description"));
                        user.EMAIL = GetXmlNodeInnerText(account.SelectSingleNode("email"));                     
                        user.EMPLOYEENUMBER = GetXmlNodeInnerText(account.SelectSingleNode("employeeNumber"));
                        user.GENDER = GetXmlNodeInnerText(account.SelectSingleNode("gender"));
                        user.MOBILE = GetXmlNodeInnerText(account.SelectSingleNode("mobile"));
                        user.ORGNAME = GetXmlNodeInnerText(account.SelectSingleNode("orgName"));
                        user.ORGNUMBER = GetXmlNodeInnerText(account.SelectSingleNode("orgNumber"));
                        user.POSITION = GetXmlNodeInnerText(account.SelectSingleNode("position"));
                        user.ROLEINFO = GetXmlNodeInnerText(account.SelectSingleNode("roleInfo"));
                        user.ROOMNUMBER = GetXmlNodeInnerText(account.SelectSingleNode("roomNumber"));
                        user.SERIALNUMBER = GetXmlNodeInnerText(account.SelectSingleNode("serialNumber"));
                        user.STATUS = GetXmlNodeInnerText(account.SelectSingleNode("status"));
                        user.TELNUMBER = GetXmlNodeInnerText(account.SelectSingleNode("telephoneNumber"));
                        user.ACCID = userIdNode.InnerText;
                        UsersEntity.UsersDAO userDAO = new UsersEntity.UsersDAO();
                        userDAO.Add(user);
                        succUserIds.Add(userIdNode.InnerText);
                    }
                    else
                    {
                        errorMsg = "AcctIdNode is null";
                    }
                }
                catch (Exception ex)
                {
                    failUserIds.Add(userIdNode.InnerText);
                    errorMsg = ex.Message;
                }
            }
        }
        else
        {
            errorMsg = "AccountList is null";
        }

        string rtnXml = BuildRtnMsg(succUserIds, failUserIds, errorMsg);
        return rtnXml;
    }

    [WebMethod]    
    public string modifyUserInfo(string userInfos)
    {
        List<string> succUserIds = new List<string>();
        List<string> failUserIds = new List<string>();
        string errorMsg = string.Empty;

        XmlDocument docUser = new XmlDocument();
        docUser.LoadXml(userInfos);
        XmlNodeList accounts = docUser.GetElementsByTagName("account");

        if (accounts != null && accounts.Count > 0)
        {
            foreach (XmlNode account in accounts)
            {
                XmlNode userIdNode = account.SelectSingleNode("accId");

                try
                {
                    if (userIdNode != null && !string.IsNullOrEmpty(userIdNode.InnerText))
                    {
                        string AccId =  userIdNode.InnerText;
                        UsersEntity.UsersDAO userDAO = new UsersEntity.UsersDAO();
                        UsersEntity user = userDAO.FindEntityByAccID(AccId);                       

                        if (user != null)
                        {
                            user.USERNAME = GetXmlNodeInnerText(account.SelectSingleNode("name"));
                            user.CREATEDATE = DateTime.Now;
                            user.DESCRIPTION = GetXmlNodeInnerText(account.SelectSingleNode("description"));
                            user.EMAIL = GetXmlNodeInnerText(account.SelectSingleNode("email"));
                            user.EMPLOYEENUMBER = GetXmlNodeInnerText(account.SelectSingleNode("employeeNumber"));
                            user.GENDER = GetXmlNodeInnerText(account.SelectSingleNode("gender"));
                            user.MOBILE = GetXmlNodeInnerText(account.SelectSingleNode("mobile"));
                            user.ORGNAME = GetXmlNodeInnerText(account.SelectSingleNode("orgName"));
                            user.ORGNUMBER = GetXmlNodeInnerText(account.SelectSingleNode("orgNumber"));
                            user.POSITION = GetXmlNodeInnerText(account.SelectSingleNode("position"));
                            user.ROLEINFO = GetXmlNodeInnerText(account.SelectSingleNode("roleInfo"));
                            user.ROOMNUMBER = GetXmlNodeInnerText(account.SelectSingleNode("roomNumber"));
                            user.SERIALNUMBER = GetXmlNodeInnerText(account.SelectSingleNode("serialNumber"));
                            user.STATUS = GetXmlNodeInnerText(account.SelectSingleNode("status"));
                            user.TELNUMBER = GetXmlNodeInnerText(account.SelectSingleNode("telephoneNumber"));
                            user.ACCID = userIdNode.InnerText;
                            userDAO.Update(user);
                            succUserIds.Add(userIdNode.InnerText);
                        }
                        else
                        {
                            errorMsg = "user is not exist";
                        }
                    }
                    else
                    {
                        errorMsg = "AcctIdNode is null";
                    }
                }
                catch (Exception ex)
                {
                    failUserIds.Add(userIdNode.InnerText);
                    errorMsg = ex.Message;
                }
            }
        }
        else
        {
            errorMsg = "AccountList is null";
        }

        string rtnXml = BuildRtnMsg(succUserIds, failUserIds, errorMsg);
        return rtnXml;
    }

    [WebMethod]    
    public string delUser(string userIDs)
    {
        List<string> succUserIds = new List<string>();
        List<string> failUserIds = new List<string>();
        string errorMsg = string.Empty;

        XmlDocument docUser = new XmlDocument();
        docUser.LoadXml(userIDs);
        XmlNodeList accountIds = docUser.GetElementsByTagName("accId");

        if (accountIds != null && accountIds.Count > 0)
        {
            foreach (XmlNode accountIdNode in accountIds)
            {

                try
                {
                    if (accountIdNode != null && !string.IsNullOrEmpty(accountIdNode.InnerText))
                    {
                        string AccId = accountIdNode.InnerText;
                        UsersEntity.UsersDAO userDAO = new UsersEntity.UsersDAO();
                        UsersEntity user = userDAO.FindEntityByAccID(AccId);   
                        if (user != null)
                        {
                            userDAO.Delete(user);
                            succUserIds.Add(accountIdNode.InnerText);
                        }
                        else
                        {
                            errorMsg = "AccId " + AccId + " is not exist";
                        }
                    }
                    else
                    {
                        errorMsg = "AcctIdNode is null";
                    }
                }
                catch (Exception ex)
                {
                    failUserIds.Add(accountIdNode.InnerText);
                    errorMsg = ex.Message;
                }
            }
        }
        else
        {
            errorMsg = "AccountList is null";
        }

        string rtnXml = BuildRtnMsg(succUserIds, failUserIds, errorMsg);
        return rtnXml;
    }

    private string BuildRtnMsg(List<string> succUserIds, List<string> failUserIds, string errorMsg)
    {
        StringBuilder rtnXml = new StringBuilder();
        rtnXml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        rtnXml.Append("<results>");
        rtnXml.Append("<result returncode=\"1000\">");
        foreach (string succUserId in succUserIds)
        {
            rtnXml.Append("<accId>").Append(succUserId).Append("</accId>");
        }
        rtnXml.Append("</result>");
        rtnXml.Append("<result returncode=\"1100\">");
        foreach (string failUserId in failUserIds)
        {
            rtnXml.Append("<accId>").Append(failUserId).Append("</accId>");
        }
        rtnXml.Append("</result>");
        rtnXml.Append("<errorMsg>").Append(errorMsg).Append("</errorMsg>");
        rtnXml.Append("</results>");

        return rtnXml.ToString();
    }


    private string GetXmlNodeInnerText(XmlNode node) {
        if (node != null)
        {
            return node.InnerText;
        }
        else {
            return null;
        }
    }
}

