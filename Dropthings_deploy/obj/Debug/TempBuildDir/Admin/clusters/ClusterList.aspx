<%@ page language="C#" autoeventwireup="true" inherits="Admin_clusters_ClusterList, Dropthings_deploy" enableEventValidation="false" theme="GreenBlue" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf8" />
    <title>����۽�����</title>
    <link rel="Stylesheet" href="../css/tab.css" type="text/css" />
    <link rel="Stylesheet" href="../css/frame.css" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.js"></script>
    <script type="text/javascript" src="../js/My97DatePicker/WdatePicker.js"></script>

    <script src="../js/Config.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/divMove.js"></script>
    <script type="text/javascript" src="../js/listTable.js"></script>
    <script type="text/javascript" src="../js/IdolPager.js"></script>
    <script type="text/javascript" src="../js/SqlSearch.js"></script>
    <script type="text/javascript" src="../js/ClusterList.js"></script>
</head>
<body style="background:#ffffff;">
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="30"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="15" height="30"><img src="../images/tab_03.gif" width="15" height="30" /></td>
        <td background="../images/tab_05.gif" align="left"><img src="../images/311.gif" width="16" height="16" /> <span class="STYLE4">��ǩ�б�</span>
        &nbsp;&nbsp;&nbsp;<a href="javascript:void(null);" id="btn_ref">ˢ��</a>&nbsp;&nbsp;&nbsp;
        <a href="javascript:void(null);" id="btn_back">��ԭ</a>
        </td>
        <td width="14"><img src="../images/tab_07.gif" width="14" height="30" /></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="9" background="../images/tab_12.gif">&nbsp;</td>
        <td bgcolor="#d3e7fc">
        
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CECECE">            
                  <tr>    
                    <td width="6%" height="26" background="../images/tab_14.gif" align="center">���</td>                                          
                    <td height="25" width="40%" background="../images/tab_14.gif" align="center">�ȵ�����</td>                           
                    <td width="9%" background="../images/tab_14.gif" align="center">�ö�</td>       
                    <td width="9%" background="../images/tab_14.gif" align="center">����</td>                               
                    <td width="7%" background="../images/tab_14.gif" align="center">�༭��Ϣ</td>
                    <td width="7%" background="../images/tab_14.gif" align="center">ɾ��</td>
                  </tr>          
                  <tbody id="all_data_list">     
                
                  <%--<tr>                    
                    <td bgcolor="#FFFFFF" align="center"><%#Eval("LableName")%></td>
                    <td bgcolor="#FFFFFF" align="center"><a href="#"><%#Eval("LableStr")%></a></td>
                    <td bgcolor="#FFFFFF" align="center"><img src="../images/037.gif" width="9" height="9" />[ <a href="javascript:void(null);" onClick="javascript:listTable.initEdit(<%#Eval("ID") %>,'ID','back_msg');">�༭</a> ]</td>
                    <td bgcolor="#FFFFFF" align="center"><img src="../images/010.gif" width="9" height="9" />[ <a href="javascript:void(null);" onClick="javascript:listTable.Remove(<%#Eval("ID") %>,'��ȷ��Ҫɾ��ô��');">ɾ��</a> ]</td>
                  </tr>     --%>           
                    </tbody>  
                
        </table></td>
        <td width="9" background="../images/tab_16.gif">&nbsp;</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td height="29"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="15" height="29"><img src="../images/tab_20.gif" width="15" height="29" /></td>
        <td background="../images/tab_21.gif"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="21%" align="left">
                <a href="javascript:void(null);" id="choose_all" name="0" onClick="javascript:listTable.chooseAll('choose_all','all_data_list');">
                    <img alt="ȫѡ" src="../images/q.gif" width="40" height="19" /> 
                </a>
                <a href="javascript:void(null);" onClick="javascript:listTable.InnitAdd(true);">
                    <img alt="����" src="../images/x.gif" width="40" height="19" /> 
                </a>
                <a href="javascript:void(null);" onClick="javascript:listTable.RemoveAll('all_data_list','removeall','��ȷ��Ҫɾ��ѡ������ô��');">
                    <img alt="ɾ��" src="../images/s.gif" width="64" height="19" />
                </a>
            </td>
            <td width="79%" class="STYLE1">   
                <table><tr>     
                <td>       
                <span id="item_info_id"></span></td>
                <td><table class="page" ><tr id="item_pager_list"></tr></table></td>
                </tr> </table>            
            </td>
          </tr>
        </table></td>
        <td width="14"><img src="../images/tab_22.gif" width="14" height="29" /></td>
      </tr>
    </table></td>
  </tr>
</table>

<div id="column_edit_frame" class="layerdiv">
  <%--<span class="layer_line"></span>--%>
  <div class="layer_outer">
  <%--<span class="layer_top_L"></span>
  <span class="layer_top_R"></span>--%>
  <div class="layer_inner"> 
    <div class="layer_move" id="move_column" title="����˴��������϶�"><h1>�ȵ����</h1></div>
    <a class="btn_close" href="javascript:void(null);" id="close_edit_frame"></a>
    <div class="clear"></div>
    <%--<div class="layer_T">
      <h1><b>�ȵ����</b></h1>
      <span class="btn"></span>
      <div class="clear"></div>
    </div>--%>
    <div class="layer_C">
        <table cellspacing="0" cellpadding="0" class="form_list">
          <tbody>            
            <tr>
                <td class="form_name" >�ȵ�������</td>
                <td class="form_field">
                    <textarea id="clustername" cols="40" rows="2" pid="valueList">
                    </textarea>
                </td>
            </tr> 
            <tr>
                <td class="form_name" >�Ƿ��ö���</td>
                <td class="form_field">
                    <select id="distype"  pid="valueList">
                        <option value="0">��</option>
                        <option value="1">��</option>
                    </select>
                </td>
            </tr> 
            <tr>
                <td class="form_name" >����</td>
                <td class="form_field">
                    <input id="param"  pid="valueList" value="0" />                        
                </td>
            </tr>                 
          </tbody>
        </table>
    </div>
    <div class="layer_B">
        <a href="javascript:void(null);">
            <img src="../images/btn_send.gif" alt="" onClick="javascript:listTable.Add('valueList','back_msg');" id="btn_add"/>
        </a>
        <a href="javascript:void(null);">
            <img src="../images/btn_rewrite.gif" alt="" onClick="javascript:listTable.Rest();" id="btn_reset"/>
        </a>
         <a href="javascript:void(null);">
            <img src="../images/btn_send.gif" alt="" onClick="javascript:listTable.EditOne('valueList');" id="btn_edit"/>
        </a>
        <span style="color:Red;" id="back_msg"></span>
       </div>
  </div>
  <%--<span class="layer_bottom_L"></span>
  <span class="layer_bottom_R"></span>--%>
  </div>
  <%--<span class="layer_line"></span>--%>
</div>

<div id="cluster_edit_frame" class="layerdiv" style=" width:100%;">
  <%--<span class="layer_line"></span>--%>
  <div class="layer_outer">
  <%--<span class="layer_top_L"></span>
  <span class="layer_top_R"></span>--%>
  <div class="layer_inner"> 
    <div class="layer_move" id="move_cluster" title="����˴��������϶�">&nbsp;</div>
    <a class="btn_close" href="javascript:void(null);" id="close_cluster_edit_frame"></a>
    <div class="clear"></div>    
    <div class="clear"></div>
    <div class="layer_T">
      <h1><b>��Ϣ�б�</b></h1>
      <span class="btn"></span>
      <div class="clear"></div>
    </div>
    <div class="layer_C">
        <span id="cluster_info_id" style="display:none;"></span>
        <table width="99%" cellspacing="1" cellpadding="0" border="0" bgcolor="#CECECE" align="center">             
           <tbody>
                <tr>
                    <td width="25%" height="26" background="../images/tab_14.gif" align="center">����</td>
                    <td width="10%" background="../images/tab_14.gif" align="center">վ��</td>
                    <td width="10%" background="../images/tab_14.gif" align="center">ʱ��</td>
                    <td width="7%" background="../images/tab_14.gif" align="center">����</td>
                    <td width="7%" background="../images/tab_14.gif" align="center">�Ƿ�����</td>
                </tr>
            </tbody>
            <tbody id="push_info_list">
                <%--<tr>
                    <td height="26" bgcolor="#FFFFFF" align="center"><a title="Ҧ��:�й������ձ���Ʒ���� ���ڱ����ҹ�����ȫ" target="_blank" href="http://news.163.com/11/0419/12/720L7P5Q00014JB5.html">Ҧ��:�й������ձ���Ʒ���� ���ڱ����ҹ�����ȫ</a></td>
                    <td height="26" bgcolor="#FFFFFF" align="center"></td>
                    <td height="26" bgcolor="#FFFFFF" align="center"></td>
                    <td bgcolor="#FFFFFF" align="center"><img width="9" height="9" src="../images/010.gif">[ <a name="info_delete" href="javascript:void(null);">ɾ��</a>]</td>
                    <td bgcolor="#FFFFFF" align="center"><input type="radio" name="info_record">��</td>
                </tr> --%>                           
            </tbody>
        </table>
    </div>
    <div class="layer_B">
        <a href="javascript:void(null);">
            <img src="../images/btn_send.gif" id="btn_push_info"/>
        </a>            
        <span style="color:Red;" id="push_back_msg"></span>     
    </div>
    <div class="clear"></div>
    <div class="layer_T">
      <h1><b>��Ϣ����</b></h1>
      <span class="btn"></span>
      <div class="clear"></div>
    </div>
    <div class="layer_C">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">                
            <tr>           
            <td>     
                     
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="9" background="../images/tab_12.gif">&nbsp;</td>
                        <td bgcolor="#d3e7fc">
                            <div class="clear"  style="height:20px;"></div>     
                            <table align="center" bgcolor="#CECECE" border="0" cellpadding="0" cellspacing="1" width="99%">
                                <tbody><tr>
                                    <td bgcolor="#ffffff">
                                      <ul class="form_list">
                                        <li>
                                      
                              <span class="name"></span>
                              <span class="input"><input type="radio" name="search_codition" checked="checked" value="query"/></span>
                              <span class="name"><b>�߼�����</b></span>
                              <span class="name">&nbsp;</span>
                              <span class="input"><input type="radio" name="search_codition" value="categoryquery"/></span>
                              <span class="name"><b>�����ѯ</b></span>
                              <span class="name">&nbsp;</span>
                              <span class="input"><input type="radio" name="search_codition" value="cluster"/></span>
                              <span class="name"><b>�ȵ�۽�</b></span>
                            </li>
                            <li name="search_frame">
                              <span  id="query_search_frame">
                                  <span class="name">�ؼ��֡���</span>
                                  <span class="input"><input id="keyword" style="width:350px;" type="text" value="*"></span>
                              </span>
                              <span style="display:none;" id="categoryquery_search_frame">
                                  <span class="name" >�֡����ࣺ</span>
                                  <span class="input"  >
                                    <select id="category_id" ></select>
                                    <select id="category_child_id" ></select>
                                  </span>
                              </span>
                              <span class="name">�����ضȣ�</span>
                              <span class="input"><input id="min_score" style="width:48px;" type="text"></span>
                            </li>
                            <li name="search_frame">
                              <span class="name">ʱ��ѡ��</span>
                              <span class="input"><input id="start_time" onFocus="WdatePicker({isShowClear:true,readOnly:true,dateFmt:'dd/MM/yyyy'})" style="width:100px;" type="text"></span>
                              <span class="name">��</span>
                              <span class="input"><input id="end_time" onFocus="WdatePicker({isShowClear:true,readOnly:true,dateFmt:'dd/MM/yyyy'})" style="width:100px;" type="text"></span>
                              <span class="name">����ʽ��</span>
                              <span class="input"><select id="sort_style" style="width:60px;">
                                    <option selected="selected" value="Date">ʱ��</option>
                                    <option value="Relevance">��ض�</option>
                                </select></span>
                              <span class="name">��Ϣ���ͣ�</span>
                              <span class="input"><select id="info_type" style="width:60px;">>
                                    <option selected="selected" value="all">ȫ��</option>
                                    <option value="news">����</option>
                                    <option value="blog">����</option>
                                    <option value="bbs">��̳</option>
                                </select></span>
                            </li>
                            <li name="search_frame">
                              <span class="name"><a id="btn_look_info" style="font-size:18px;" href="javascript:void(null);">
                                <img src="../images/btn_search.gif" border="0" />
                              </a></span>
                                     <span style="display:none;">
                           
                           <div class="clear" style="height:10px;"></div>
                           </span>   
                                        </li>
                                <li name="cluster_frame" style=" display:none;">
                                    <span class="name">����۽����ƣ�</span>
                                    <span class="input"><select id="job_name_list"></select></span>
                                    <span class="name">���۽��㣺</span>
                                    <span class="input"><select id="job_cluster_id"></select></span>
                                    <span class="name"><a id="btn_look_cluster" style="font-size:18px;" href="javascript:void(null);">
                                        <img src="../images/btn_search.gif" border="0" />
                                      </a></span>
                                    
                                </li>
                                      </ul>
                                    </td>
                                </tr></tbody>
                            </table>
                            <br />
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CECECE">
                                <tr>
                                    <td width="6%" height="26" background="../images/tab_14.gif" align="center">ѡ��</td>
                                    <td width="45%" background="../images/tab_14.gif" align="center">����</td>
                                    <td width="14%" background="../images/tab_14.gif" align="center">ʱ��</td>
                                    <td width="14%" background="../images/tab_14.gif" align="center">����վ��</td>
                                    <td width="7%" background="../images/tab_14.gif" align="center">����</td>
                                    <td width="7%" background="../images/tab_14.gif" align="center">��ض�</td>
                                    <td width="7%" background="../images/tab_14.gif" align="center">�鿴����</td>
                                </tr>
                                <tbody id="idol_all_data_list">
                                    
                                </tbody>
                            </table>
                        </td>
                        <td width="9" background="../images/tab_16.gif">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td >
                <table width="100%" border="0" cellspacing="0" cellpadding="0" height="29">
                    <tr>
                        <td width="15" height="29"><img src="../images/tab_20.gif" width="15" height="29" /></td>
                        <td background="../images/tab_21.gif">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="21%" align="left">&nbsp;
                                        <a href="javascript:void(null);" name="0" onClick="javascript:listTable.chooseAll('choose_all','idol_all_data_list');">
                                            <img alt="ȫѡ" src="../images/q.gif" width="40" height="19" />
                                        </a>
                                        <a href="javascript:void(null);" id="all_news_push">
                                            <img alt="ȫ������" src="../images/btn_select_all.gif" width="64" height="19" />
                                        </a>
                                    </td>
                                    <td width="79%" class="STYLE1">
                                        <div id="idol_pager_list"></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="14"><img src="../images/tab_22.gif" width="14" height="29" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
    </div>
  </div>
  <%--<span class="layer_bottom_L"></span>
  <span class="layer_bottom_R"></span>--%>
  </div>
  <%--<span class="layer_line"></span>--%>
</div>
    </div>
    </form>
</body>
</html>