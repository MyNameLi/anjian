<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InfoPush.aspx.cs" Inherits="Admin_IdolNew_InfoPush" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>��Ϣ����</title>
    <link rel="Stylesheet" href="../css/tab.css" type="text/css" />
    <link rel="Stylesheet" href="../css/frame.css" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.js"></script>
    <script type="text/javascript" src="../js/Config.js"></script>
    <script type="text/javascript" src="../js/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../js/divMove.js"></script>
    <script type="text/javascript" src="../js/listTable.js"></script>
    <script type="text/javascript" src="../js/IdolPager.js"></script>
    <script type="text/javascript" src="../js/InfoPush.js"></script>
</head>
<body style="background: #ffffff;">
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td height="30">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="15" height="30">
                                <img src="../images/tab_03.gif" width="15" height="30" />
                            </td>
                            <td background="../images/tab_05.gif" align="left">
                                <img src="../images/311.gif" width="16" height="16" /><span class="STYLE4">��Ϣ����</span>
                                &nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" id="choose_all_leader" />ȫѡ
                            </td>
                            <td width="14">
                                <img src="../images/tab_07.gif" width="14" height="30" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td width="9" background="../images/tab_12.gif">
                                    &nbsp;
                                </td>
                                <td bgcolor="#d3e7fc">
                                    <table width="99%" cellspacing="1" cellpadding="0" border="0" bgcolor="#CECECE" align="center">
                                        <tbody>
                                            <tr>
                                                <td bgcolor="#ffffff">
                                                    <ul class="form_list">
                                                        <li id="leader_list">
                                                            <%--<span class="name" >ѡ���쵼��</span>
                                        <div></div>
                                        <span class="input"><input type="radio" pid="24" value="166" name="leader_list_item"></span><span class="name"><b>����</b></span>--%>
                                                        </li>
                                                        <div class="clear">
                                                        </div>
                                                    </ul>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <br>
                                    <table width="99%" cellspacing="1" cellpadding="0" border="0" bgcolor="#CECECE" align="center">
                                        <tbody>
                                            <tr>
                                                <td width="45%" height="26" background="../images/tab_14.gif" align="left">
                                                    ������Ϣ
                                                </td>
                                                <td width="7%" background="../images/tab_14.gif" align="center">
                                                    ����
                                                </td>
                                                <td width="7%" background="../images/tab_14.gif" align="center">
                                                    �Ƿ���
                                                </td>
                                            </tr>
                                        </tbody>
                                        <tbody id="push_info_list">
                                            <%--<tr>
                                   <td height="26" bgcolor="#FFFFFF" align="center"><a title="Ҧ��:�й������ձ���Ʒ���� ���ڱ����ҹ�����ȫ" target="_blank" href="http://news.163.com/11/0419/12/720L7P5Q00014JB5.html">Ҧ��:�й������ձ���Ʒ���� ���ڱ����ҹ�����ȫ</a></td>
                                 <td bgcolor="#FFFFFF" align="center"><img width="9" height="9" src="../images/010.gif">[ <a name="info_delete" href="javascript:void(null);">ɾ��</a>]</td>
                                <td bgcolor="#FFFFFF" align="center"><input type="radio" name="info_record">��
</td>
                            </tr>--%>
                                        </tbody>
                                    </table>
                                </td>
                                <td width="9" background="../images/tab_16.gif">
                                    &nbsp;
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" height="29" cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td width="15" height="29">
                                    <img width="15" height="29" src="../images/tab_20.gif">
                                </td>
                                <td background="../images/tab_21.gif">
                                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td width="79%" id="push_info_btn">
                                                    <a id="btn_push_info" href="javascript:void(null);">
                                                        <img src="../images/btn_search.gif"></a>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td width="14">
                                    <img width="14" height="29" src="../images/tab_22.gif">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="30">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="15" height="30">
                                <img src="../images/tab_03.gif" width="15" height="30" />
                            </td>
                            <td background="../images/tab_05.gif" align="left">
                                <img src="../images/311.gif" width="16" height="16" /><span class="STYLE4">������ʷ��¼</span>
                                &nbsp;&nbsp;&nbsp;&nbsp;<a href="javascript:void(null)" id="look_history_list" pid="0">չ����ʷ��¼</a>
                            </td>
                            <td width="14">
                                <img src="../images/tab_07.gif" width="14" height="30" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr name="history_list_frame" style="display: none;">
                <td>
                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td width="9" background="../images/tab_12.gif">
                                    &nbsp;
                                </td>
                                <td bgcolor="#d3e7fc">
                                    <table width="99%" cellspacing="1" cellpadding="0" border="0" bgcolor="#CECECE" align="center">
                                        <tbody>
                                            <tr>
                                                <td bgcolor="#ffffff">
                                                    <ul class="form_list">
                                                        <li id="history_leader_list">
                                                            <%--<span class="name" >ѡ���쵼��</span>
                                        <div></div>
                                        <span class="input"><input type="radio" pid="24" value="166" name="leader_list_item"></span><span class="name"><b>����</b></span>--%>
                                                        </li>
                                                        <div class="clear">
                                                        </div>
                                                        <li><span class="name">ʱ�䣺</span> <span class="input">
                                                            <input type="text" id="push_time" onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})" /></span>
                                                        </li>
                                                        <div class="clear">
                                                        </div>
                                                        <li><a href="javascript:void(null);" id="look_push_history">
                                                            <img src="../images/btn_search.gif"></a> </li>
                                                        <div class="clear">
                                                        </div>
                                                    </ul>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <br>
                                    <table width="99%" cellspacing="1" cellpadding="0" border="0" bgcolor="#CECECE" align="center">
                                        <tbody>
                                            <tr>
                                                <td width="100%" height="26" background="../images/tab_14.gif" align="center">
                                                    ������Ϣ
                                                </td>
                                            </tr>
                                        </tbody>
                                        <tbody id="history_leader_info">
                                            <%--<tr>
                                   <td height="26" bgcolor="#FFFFFF" align="center"><a title="Ҧ��:�й������ձ���Ʒ���� ���ڱ����ҹ�����ȫ" target="_blank" href="http://news.163.com/11/0419/12/720L7P5Q00014JB5.html">Ҧ��:�й������ձ���Ʒ���� ���ڱ����ҹ�����ȫ</a></td>
                                 <td bgcolor="#FFFFFF" align="center"><img width="9" height="9" src="../images/010.gif">[ <a name="info_delete" href="javascript:void(null);">ɾ��</a>]</td>
                                <td bgcolor="#FFFFFF" align="center"><input type="radio" name="info_record">��
</td>
                            </tr>--%>
                                        </tbody>
                                    </table>
                                </td>
                                <td width="9" background="../images/tab_16.gif">
                                    &nbsp;
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr name="history_list_frame" style="display: none;">
                <td>
                    <table width="100%" height="29" cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td width="15" height="29">
                                    <img width="15" height="29" src="../images/tab_20.gif">
                                </td>
                                <td background="../images/tab_21.gif">
                                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td width="79%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td width="14">
                                    <img width="14" height="29" src="../images/tab_22.gif">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="30">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="15" height="30">
                                <img src="../images/tab_03.gif" width="15" height="30" />
                            </td>
                            <td background="../images/tab_05.gif" align="left">
                                <img src="../images/311.gif" width="16" height="16" /><span class="STYLE4">��Ϣ�б�</span>
                            </td>
                            <td width="14">
                                <img src="../images/tab_07.gif" width="14" height="30" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="9" background="../images/tab_12.gif">
                                &nbsp;
                            </td>
                            <td bgcolor="#d3e7fc">
                                <table align="center" bgcolor="#CECECE" border="0" cellpadding="0" cellspacing="1"
                                    width="99%">
                                    <tbody>
                                        <tr>
                                            <td bgcolor="#ffffff">
                                                <ul class="form_list">
                                                    <li><span class="name"></span><span class="input">
                                                        <input type="radio" name="search_codition" checked="checked" value="query" /></span>
                                                        <span class="name"><b>�߼�����</b></span> <span class="name">&nbsp;</span> <span class="input">
                                                            <input type="radio" name="search_codition" value="categoryquery" /></span> <span
                                                                class="name"><b>�����ѯ</b></span> </li>
                                                    <li><span id="query_search_frame"><span class="name">�ؼ��� ��</span> <span class="input">
                                                        <input id="keyword" style="width: 350px;" type="text" value="*"></span> </span><span
                                                            style="display: none;" id="categoryquery_search_frame"><span class="name">�� �ࣺ</span>
                                                            <span class="input">
                                                                <select id="category_id">
                                                                </select>
                                                                <select id="category_child_id">
                                                                </select>
                                                            </span></span><span class="name">�����ضȣ�</span> <span class="input">
                                                                <input id="min_score" style="width: 48px;" type="text"></span> </li>
                                                    <li><span class="name">ʱ��ѡ��</span> <span class="input">
                                                        <input id="start_time" onfocus="WdatePicker({isShowClear:true,readOnly:true,dateFmt:'dd/MM/yyyy'})"
                                                            style="width: 100px;" type="text"></span> <span class="name">��</span> <span class="input">
                                                                <input id="end_time" onfocus="WdatePicker({isShowClear:true,readOnly:true,dateFmt:'dd/MM/yyyy'})"
                                                                    style="width: 100px;" type="text"></span> <span class="name">����ʽ��</span>
                                                        <span class="input">
                                                            <select id="sort_style" style="width: 60px;">
                                                                <option selected="selected" value="Date">ʱ��</option>
                                                                <option value="Relevance">��ض�</option>
                                                            </select></span> <span class="name">��Ϣ���ͣ�</span> <span class="input">
                                                                <select id="info_type" style="width: 60px;">
                                                                    >
                                                                    <option selected="selected" value="all">ȫ��</option>
                                                                    <option value="news">����</option>
                                                                    <option value="blog">����</option>
                                                                    <option value="bbs">��̳</option>
                                                                </select></span> </li>
                                                    <li><span class="name"><a id="btn_look_info" style="font-size: 18px;" href="javascript:void(null);">
                                                        <img src="../images/btn_search.gif" border="0" />
                                                    </a></span><span style="display: block;">����Դ��<input type="checkbox" name="databse_list"
                                                        value="NewsSource" />NewsSource&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <input type="checkbox" name="databse_list" value="safety" />safety&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <input type="checkbox" name="databse_list" value="portalsafety" />portalsafety&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <input type="checkbox" name="databse_list" value="bbs" />bbs&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <input type="checkbox" name="databse_list" value="NewPortal" checked="checked" />NewPortal&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <input type="checkbox" name="databse_list" value="NewBBS" />NewBBS&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <div class="clear" style="height: 10px;">
                                                        </div>
                                                    </span></li>
                                                </ul>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CECECE">
                                    <tr>
                                        <td width="6%" height="26" background="../images/tab_14.gif" align="center">
                                            ѡ��
                                        </td>
                                        <td width="45%" background="../images/tab_14.gif" align="center">
                                            ����
                                        </td>
                                        <td width="14%" background="../images/tab_14.gif" align="center">
                                            ʱ��
                                        </td>
                                        <td width="14%" background="../images/tab_14.gif" align="center">
                                            ����վ��
                                        </td>
                                        <td width="7%" background="../images/tab_14.gif" align="center">
                                            ����
                                        </td>
                                        <td width="7%" background="../images/tab_14.gif" align="center">
                                            ��ض�
                                        </td>
                                        <td width="7%" background="../images/tab_14.gif" align="center">
                                            �鿴����
                                        </td>
                                    </tr>
                                    <tbody id="all_data_list">
                                    </tbody>
                                </table>
                            </td>
                            <td width="9" background="../images/tab_16.gif">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" height="29">
                        <tr>
                            <td width="15" height="29">
                                <img src="../images/tab_20.gif" width="15" height="29" />
                            </td>
                            <td background="../images/tab_21.gif">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="21%" align="left">
                                            &nbsp; <a href="javascript:void(null);" id="choose_all" name="0" onclick="javascript:listTable.chooseAll('choose_all','all_data_list');">
                                                <img alt="ȫѡ" src="../images/q.gif" width="40" height="19" />
                                            </a><a href="javascript:void(null);" id="all_news_push">
                                                <img alt="ȫ������" src="../images/btn_select_all.gif" width="64" height="19" />
                                            </a>
                                        </td>
                                        <td width="79%" class="STYLE1">
                                            <div id="pager_list">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="14">
                                <img src="../images/tab_22.gif" width="14" height="29" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
