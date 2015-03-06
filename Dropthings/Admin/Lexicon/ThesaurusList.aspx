<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ThesaurusList.aspx.cs" Inherits="Expand_ThesaurusList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" href="../css/tab.css" type="text/css" />
    <link rel="Stylesheet" href="../css/frame.css" type="text/css" />
    <link href="../css/zTree3.0.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../js/jquery-1.4.2.js"></script>

    <script src="../js/jquery.ztree.core-3.0.min.js" type="text/javascript"></script>

    <script src="../js/listTable.js" type="text/javascript"></script>

    <script type="text/javascript" src="../js/divMove.js"></script>

    <script src="../js/SqlSearch.js" type="text/javascript"></script>

    <script src="ThesaurusList.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="28%" cellspacing="0" cellpadding="0" border="0" align="center" style="float: left;">
            <tbody>
                <tr>
                    <td height="30">
                        <table width="100%" cellspacing="0" cellpadding="0" border="0">
                            <tbody>
                                <tr>
                                    <td width="15" height="30">
                                        <img width="15" height="30" src="../images/tab_03.gif">
                                    </td>
                                    <td background="../images/tab_05.gif" align="left">
                                        <img width="16" height="16" src="../images/311.gif">
                                        <span class="STYLE4">词库类别</span> &nbsp;&nbsp;&nbsp;&nbsp;<a href="javascript:void(null);"
                                            title="增加" onclick="javascript:ThesaurusList.InnitAdd();">
                                            <img src="../images/Lexicon_add.gif" border="0" />增加 </a>&nbsp;&nbsp;&nbsp;&nbsp;<a
                                                href="javascript:void(null);" title="修改" onclick="javascript:ThesaurusList.InitEdit('Id','back_msg');">
                                                <img src="../images/Lexicon_edit.jpg" border="0" />修改 </a>&nbsp;&nbsp;&nbsp;&nbsp;<a
                                                    href="javascript:void(null);" title="删除" onclick="javascript:ThesaurusList.Remove('该类别下的所有信息列表将被删除，您确定要删除么?');">
                                                    <img src="../images/Lexicon_delete.gif" border="0" />删除 </a>&nbsp;&nbsp;&nbsp;&nbsp;
                                        <select id="processType">
                                            <option value="0" checked="checked">未处理</option>
                                            <option value="1" checked="checked">已删除</option>
                                            <option value="2" checked="checked">已忽略</option>
                                        </select>
                                    </td>
                                    <td width="14">
                                        <img width="14" height="30" src="../images/tab_07.gif">
                                    </td>
                                </tr>
                            </tbody>
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
                                        <ul id="treeThesaurus" class="ztree">
                                        </ul>
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
                    <td height="29">
                        <table width="100%" cellspacing="0" cellpadding="0" border="0">
                            <tbody>
                                <tr>
                                    <td width="15" height="29">
                                        <img width="15" height="29" src="../images/tab_20.gif">
                                    </td>
                                    <td background="../images/tab_21.gif">
                                        &nbsp;
                                    </td>
                                    <td width="14">
                                        <img width="14" height="29" src="../images/tab_22.gif">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
        <table width="70%" cellspacing="0" cellpadding="0" border="0" align="center" style="float: left;">
            <tbody>
                <tr>
                    <td height="30">
                        <table width="100%" cellspacing="0" cellpadding="0" border="0">
                            <tbody>
                                <tr>
                                    <td width="15" height="30">
                                        <img width="15" height="30" src="../images/tab_03.gif">
                                    </td>
                                    <td background="../images/tab_05.gif" align="left">
                                        <img width="16" height="16" src="../images/311.gif">
                                        <span class="STYLE4">词库列表</span> <span id="current_type"></span>
                                        &nbsp;&nbsp;&nbsp;&nbsp;全选<input type="checkbox" id="cbProcessAllSel" />&nbsp;&nbsp;<input type="button" value="标记已删除" id="markDel" />
                                        &nbsp;&nbsp;<input type="button" value="标记已忽略" id="markIgnored" />
                                    </td>
                                    <td width="14">
                                        <img width="14" height="30" src="../images/tab_07.gif">
                                    </td>
                                </tr>
                            </tbody>
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
                                        <div id="wrapper">
                                            <div class="content">
                                                <table width="100%" cellspacing="1" border="0" cellpadding="0" class="table_list">
                                                    <tr>
                                                        <th width="30%">
                                                            标题
                                                        </th>
                                                        <th width="9%">
                                                            线索来源
                                                        </th>
                                                        <th width="40%">
                                                            导读
                                                        </th>
                                                        <th width="9%">
                                                            发布时间
                                                        </th>
                                                        <th width="7%">
                                                            处理状态
                                                        </th>
                                                        <th>
                                                            选择
                                                        </th>
                                                    </tr>
                                                    <tbody id="newsList">
                                                    </tbody>
                                                </table>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </div>
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
                    <td height="29">
                        <table width="100%" cellspacing="0" cellpadding="0" border="0">
                            <tbody>
                                <tr>
                                    <td width="15" height="29">
                                        <img width="15" height="29" src="../images/tab_20.gif">
                                    </td>
                                    <td background="../images/tab_21.gif">
                                        <span class="left" id="all_info_id">&nbsp;</span> <span class="right">
                                            <table class="page" align="right">
                                                <tr id="all_pager_list">
                                                </tr>
                                            </table>
                                        </span>
                                    </td>
                                    <td width="14">
                                        <img width="14" height="29" src="../images/tab_22.gif">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
        <div id="column_edit_frame" class="layerdiv">
            <span class="layer_line"></span>
            <div class="layer_outer">
                <span class="layer_top_L"></span><span class="layer_top_R"></span>
                <div class="layer_inner">
                    <div class="layer_move" id="move_column" title="点击此处可自由拖动">
                        &nbsp;</div>
                    <a class="btn_close" href="javascript:void(null);" id="close_edit_frame"></a>
                    <div class="clear">
                    </div>
                    <div class="layer_T">
                        <h1>
                            <b>类别管理</b></h1>
                        <span class="btn"></span>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="layer_C">
                        <table cellspacing="0" cellpadding="0" class="form_list">
                            <tbody>
                                <tr>
                                    <td class="form_name">
                                        标题名称：
                                    </td>
                                    <td class="form_field">
                                        <input id="title" pid="valueList" type="text" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_name">
                                        关键词：
                                    </td>
                                    <td class="form_field">
                                        <input id="keyword" pid="valueList" type="text" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="layer_B">
                        <a href="javascript:void(null);">
                            <img src="../images/btn_send.gif" alt="" onclick="javascript:listTable.Add('valueList','back_msg','','');"
                                id="btn_add" />
                        </a><a href="javascript:void(null);">
                            <img src="../images/btn_rewrite.gif" alt="" onclick="javascript:listTable.Rest();"
                                id="btn_reset" />
                        </a><a href="javascript:void(null);">
                            <img src="../images/btn_send.gif" alt="" onclick="javascript:ThesaurusList.EditOne('valueList');"
                                id="btn_edit" />
                        </a><span style="color: Red;" id="back_msg"></span>
                    </div>
                </div>
                <span class="layer_bottom_L"></span><span class="layer_bottom_R"></span>
            </div>
            <span class="layer_line"></span>
        </div>
    </div>
    </form>
</body>
</html>
