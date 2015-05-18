<%@ page language="C#" autoeventwireup="true" inherits="Admin_category_Training, Dropthings_deploy1" enableEventValidation="false" theme="GreenBlue" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf8" />
    <title>分类管理</title>
    <link rel="Stylesheet" href="../css/tab.css" type="text/css" />
    <link rel="Stylesheet" href="../css/frame.css" type="text/css" />
    <link href="../css/test.css" rel="Stylesheet" type="text/css" rev="stylesheet" media="all" />
    <script type="text/javascript" src="../js/jquery-1.4.2.js"></script>
    <script type="text/javascript" src="../js/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../js/divMove.js"></script>
    <script src="../js/Config.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../js/listTable.js"></script>
    <script type="text/javascript" src="../js/SqlSearch.js"></script>
    <script type="text/javascript" src="../js/ajaxfileupload.js"></script>
    <script type="text/javascript" src="../js/OterhPager.js"></script>
    <script type="text/javascript" src="../js/train.js"></script>
</head>
<body style="background:#ffffff;">
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="30"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="15" height="30"><img src="../images/tab_03.gif" width="15" height="30" /></td>
        <td background="../images/tab_05.gif" align="left"><img src="../images/311.gif" width="16" height="16" /> <span class="STYLE4">分类列表</span>
        &nbsp;&nbsp;&nbsp;<a href="javascript:void(null)" id="add_new_catagory">增加根目录</a>
         &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<a pid="0" href="javascript:void(null)" id="btn_category_back">返回</a>
         
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
                <td width="40%" background="../images/tab_14.gif" align="center">分类名称</td>
                <td width="13%" background="../images/tab_14.gif" align="center">关键词训练</td>      
                <td width="13%" background="../images/tab_14.gif" align="center">增加子分类</td>  
                <td width="8%" background="../images/tab_14.gif" align="center">数据推送</td>
                <td width="5%" background="../images/tab_14.gif" align="center">编辑</td>
                <td width="5%" background="../images/tab_14.gif" align="center">删除</td>
                <td width="10%" background="../images/tab_14.gif" align="center">查看子分类</td>
              </tr>          
              <tbody id="all_data_list">                
             <%-- <tr>                
                <td bgcolor="#FFFFFF" align="center">舆情分类</td>
                <td bgcolor="#FFFFFF" align="center"><img src="../images/037.gif" width="9" height="9" />[ <a href="javascript:void(null);">关键词训练</a> ]</td>
                <td bgcolor="#FFFFFF" align="center"><img src="../images/037.gif" width="9" height="9" />[ <a href="javascript:void(null);">增加子分类</a> ]</td>
                <td bgcolor="#FFFFFF" align="center"><img src="../images/037.gif" width="9" height="9" />[ <a href="javascript:void(null);" >编辑</a> ]</td>
                <td bgcolor="#FFFFFF" align="center"><img src="../images/010.gif" width="9" height="9" />[ <a href="javascript:void(null);" >删除</a> ]</td>
              </tr> --%>
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
                &nbsp;
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
<div id="column_edit_frame" class="layerdiv" style="width:80%;">
  <%--<span class="layer_line"></span>--%>
  <div class="layer_outer">
  <%--<span class="layer_top_L"></span>
  <span class="layer_top_R"></span>--%>
  <div class="layer_inner"> 
    <div class="layer_move" id="move_column" title="点击此处可自由拖动">&nbsp;</div>
    <a class="btn_close" href="javascript:void(null);" id="close_edit_frame"></a>
    <div class="clear"></div>
    <div class="layer_T">
      <h1><b>分类管理</b></h1>
      <span class="btn"></span>
      <div class="clear"></div>
    </div>
    <div class="layer_C">
        <table cellspacing="0" cellpadding="0" width="100%">
          <tbody id="category_train_frame">
            <tr>
            <td class="form_name" >分类名称：</td>
            <td class="form_field">
                <input id="CategoryName" pid="valueList" type="text" />
            </td>
            </tr>             
            <tr>
            <td colspan="2" class="form_field"><h1>初始训练：</h1></td>                      
            </tr>
            <tr>
                <td class="form_name" >分类起始条件：</td>
                <td class="form_field">
                    <textarea id="cate_train_info" style="font-size:12px;"  rows="5" cols="35"></textarea>
                </td>
            </tr> 
            <tr>
                <td class="form_name" >排序规则：</td>
                <td class="form_field">
                     <select id="CatePath">
				        <option value="Relevance">相关度</option>
				        <option value="date">时间</option>
				    </select>
                </td>
            </tr>   
            <tr>
                <td class="form_name" >父级分类：</td>
                <td class="form_field">
                    <select id="ParentCate" disabled="disabled" style="width:245px">
                        <option value="0">根目录</option>
                    </select>
                </td>
            </tr>  
            <tr>
                <td class="form_name" >是否有效：</td>
                <td class="form_field">
                    <select id="IsEffect">
					    <option value="1">是</option>
					    <option value="0">否</option>    
					</select>
                </td>
            </tr> 
            <tr>
                <td class="form_name" >查询方式：</td>
                <td class="form_field">
                    <select id="QueryType">
					    <option value="commonquery">普通查询</option> 
					    <option value="categoryquery">分类查询</option>									       
					</select>
                </td>
            </tr> 
            <tr>
                <td class="form_name" >查询关键字：</td>
                <td class="form_field">
                    <textarea id="Keyword" style="font-size:12px;"  rows="5" cols="35"></textarea>
                </td>
            </tr>   
            <tr>
                <td class="form_name" >查询相关度：</td>
                <td class="form_field">
                    <input type="text" id="MinScore" style=" width:100px;" value="20" />（请输入10至90之间的数字）
                </td>
            </tr>
            <tr name="event_div">
                <td class="form_field" colspan="2"><h1>其他关键字：</h1></td>                
            </tr>
            <tr name="event_div">
                <td class="form_name" >事故原因：</td>
                <td class="form_field">
                    <input type="text" id="EventReson"  class="input_text31"/>
                </td>         
            </tr>   
            <tr name="event_div">
                <td class="form_name" >事故救援：</td>
                <td class="form_field">
                    <input type="text" id="EventMeasure" class="input_text31" />
                </td>         
            </tr>  
            <tr name="event_div">
                <td class="form_name" >安监相关：</td>
                <td class="form_field">
                    <input type="text" id="EventAbout" class="input_text31" />
                </td>         
            </tr>  
            <tr name="event_div">
                <td class="form_name" >相关度：</td>
                <td class="form_field">
                    <input type="text" id="EventMinScore" size="5" value="20" />（请输入10至90之间的数字）
                </td>         
            </tr>     
            <tr name="event_div">
                <td class="form_field" colspan="2"><h1>事件信息：</h1></td>                
            </tr>    
            <tr name="event_div">
                <td class="form_name" >发生时间：</td>
                <td class="form_field">
                     <input type="text" id="EventDate" class="input_text31" onfocus="WdatePicker({isShowClear:true,readOnly:true,dateFmt:'yyyy-MM-dd'})" />
                </td>         
            </tr>      
            <tr name="event_div">
                <td class="form_name" >事件类型：</td>
                <td class="form_field">
                    <select id="EventType">
			            <option value="0">---请选择事件类型---</option>
			            <option value="1">处理中</option>
			            <option value="2">已处理</option>
			            <option value="3">瞒报</option>
			        </select>
                </td>         
            </tr> 
            <tr>
                <td class="form_field" colspan="2" ><h1>相关信息：</h1></td>                
            </tr>
            <tr>
                <td class="form_name" >排序：</td>
                <td class="form_field">
                    <input type="text" id="EventSort" class="input_text31"  />
                </td>         
            </tr>
            <tr name="event_div">
                <td class="form_name" >图片地址：</td>
                <td class="form_field">
                    <input type="text" id="ImgPath" class="input_text31"  /><br />
			        <input type="file" name="post_file_path" id="post_file_path" />
			        <input type="button" class="input_button31" value="上传" onclick="javascript:listTable.UpLoadFile('post_file_path','ImgPath','CommonFileLoad.ashx');" />
                </td>         
            </tr>    
            <tr name="event_div">
                <td class="form_name" >是否最新：</td>
                <td class="form_field">
                    <select id="IsNew">
			            <option value="0">否</option>
			            <option value="1">是</option>									            
			        </select>
                </td>         
            </tr>
            <tr>
                <td class="form_field" colspan="2" ><h1>其他训练：</h1></td>                
            </tr>
            <tr>
                <td class="form_name" >文章训练：</td>
                <td class="form_field">
                    <input name="button2" style="vertical-align:middle;" type="checkbox"  id="article_trainning" />
                </td>         
            </tr>
            
            <tr>
                <td colspan="2">
                    <div id="trainingArticle" style="display:none;">
                        <div class="newTopic clearfix" style="margin-top:15px;">					    
					        <div class="article_list" style="border-bottom:gray solid 1px; font-weight:bolder;">
					            <ul>
					                <li class="article_list_num">训练文章：</li>					            					            
					                <li class="article_list_href">文章链接</li>
					                <li class="article_list_del">操作</li>
					                <li style="text-align:right;"></li>
    					            
					            </ul>
					        </div>				   
    					    
					        <div style="clear:both;" id="choose_article_list_id">
					            <ul>
					        	    <li style="float:right; padding-right:10px;"> <a href="javascript:void(null);" id="look_article_list" pid="hidden">展开文章列表</a></li>
					            </ul>					       
					        </div>
					    </div>
					    <div class="publicTopicTrain">
						    <div class="trainTitle clearfix">
							    <h3>训练文章：</h3>
							    <div class="trainSearch">
    								
									    <input name="text" type="text" class="input_text34" id="key_words" />
									    <input name="searchButton" type="button" value="搜索" id="BtnSearch" class="input_button35" />
    								
							    </div>
						    </div>
						    <div class="publicFeelSearchContent trainBody">
							    <ol class="publicFeelSearchResult" id="SearchResult">
								    <!--<li>
									    <div class="trainSelect"><input  type="checkbox" name="train_article_list" /> 34%</div>
									    <h2><a href="#">《团圆》启幕柏林电影节 张艺谋短片祝寿柏林影展</a></h2>
									    <div class="d"><span>千龙网</span> - 47分钟前</div>
									    <p>医生圈论坛—医学社区2008年，礼来的销售业绩继续保持了前两年快速增长的态势，尤其是在胰岛素领域的增长远远高于市场平均水平；勃林格殷格翰<b>...</b></p>
								    </li>	-->
								    <li><center>请搜索文章......</center></li>															
							    </ol>
							    <div class="publicFeelSearchPage" id="PagerList"></div>
							    <div class="trainOperate">
								    <div class="trainSelect"><label><input name="checkAll" type="checkbox" id="choose_all" /> 全选</label></div>
								    <input name="train" type="button" value="保存选中的文章" class="input_button33" id="save_docs" />
							    </div>
						    </div>
					    </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <input name="button" type="button" value="确定" id="btnSubmit" pid="add" class="input_button31" />
									<span style="color:Red;" id="Message"></span> </td>        
            </tr>
          </tbody>
        </table>
    </div>    
  </div>
  <%--<span class="layer_bottom_L"></span>
  <span class="layer_bottom_R"></span>--%>
  </div>
  <%--<span class="layer_line"></span>--%>
</div>


<div id="category_term_frame" class="layerdiv" style=" width:80%;">
  <%--<span class="layer_line"></span>--%>
  <div class="layer_outer">
  <%--<span class="layer_top_L"></span>
  <span class="layer_top_R"></span>--%>
  <div class="layer_inner"> 
    <div class="layer_move" id="category_term_move" title="点击此处可自由拖动">&nbsp;</div>
    <a class="btn_close" href="javascript:void(null);" id="category_term_close"></a>
    <div class="clear"></div>
    <div class="layer_T">
      <h1><b>初始关键词</b></h1>
      <span class="btn"></span>
      <div class="clear"></div>
    </div>
    <div class="layer_C" style="overflow:hidden;height:auto;">
        <span style="display:none;" id="retrain_term_category"></span>
        <ol id="original_term">
					                
	    </ol>
	    <div class="clear"></div>
	    <span style="float:left;"><input type="button" value="保存所选关键字" class="input_button32" id="save_term" /></span>
    </div>
    <div class="layer_T">
      <h1><b>训练关键词</b></h1>
      <span class="btn"></span>
      <div class="clear"></div>
    </div>
    <div class="layer_C"  style="overflow:hidden;height:auto;">
        <ol id="train_term" >
            <ul>				               
                <li style="float:left; margin-left:10px; width:130px;" id="afresh_term"><a href="javascript:void(null);" id="term_add" title="增加关键词"><img src="../images/add.gif" width="20px" height="20px" /></a></li>
            </ul>
        </ol>
    </div>
    <div class="layer_B">
        <a href="javascript:void(null);">
            <img src="../images/btn_send.gif" alt=""  id="re_train"/>
        </a>        
        <span style="color:Red;" id="back_msg"></span>
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
