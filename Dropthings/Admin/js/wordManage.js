$(document).ready(function() {   
    wordManage.TermInit();
    wordManage.TermDelInit();
    wordManage.TermValueInit();
    wordManage.TermAddInit();
});
var _wordManage = new Object;
var wordManage = _wordManage.property ={
    ManageType : null,
    ParentId : null,
    TermInit : function(){
        $.post("../Handler/Words.ashx",
            {"type":"init"},
            function(data){
                if(data)
                {
                    $("#term_list").empty().html(unescape(data["TermHtmlStr"]));        
                    wordManage.HoverInit();           
                }
            },
            "json"
        );
    },
    TermDelInit : function(){
        
        $("[id^='term_del_']").live("click",function(){
            var current_obj = this;           
            var id = $(this).attr("id").split("_")[2];
            if(confirm("您确定要删除该记录么？")){
                if($(this).parent("ul").siblings(".Sensitive_word_item").find("li").length > 1)
                {
                    alert("对不起，该记录下有子元素，不能删除！")
                }
                else
                {
                    $.post("../Handler/Words.ashx",
                        {"type":"delete","term_id" : id},
                        function(data){
                            if(data["successCode"] == 1)
                            {
                                $(current_obj).parent("li").remove();
                                $(current_obj).parent("ul").parent("div").remove();
                            }
                        },
                        "json"
                    );
                }
            }            
        });
       
    },
    HoverInit : function(){
        $(".Sensitive_word_category").hover(on,out);
        $(".Sensitive_word_item").children("li").hover(on,out);
        function on(){
            $(this).children(".term_del").show();
        };
        function out(){
            $(this).children(".term_del").hide();
        };
    },
    TermValueInit : function(){
        $("[id^='term_value_']").live("click",function(){            
            var word_id=$(this).attr("id").split('_')[2];
            var current_obj=$(this); 
            var value=$(this).html();
            var tagType=$(this).find("input").attr("type");
            if(tagType=="text") return;
            var input=document.createElement("INPUT");
            input.value=value;
            $(this).empty().append(input);
            input.focus();
            $(input).blur(function(){
                var input_value = $(this).val();
                if(input_value=="" || input_value == value)
                {
                    current_obj.empty().html(value);
                }
                else
                {    
                     $.post("../Handler/Words.ashx",
                        {"type":"edit","term_id" :word_id,"term_value":input_value},
                        function(data){
                            if(data["successCode"] == 1)
                            {
                                current_obj.empty().html(input_value);
                            }
                        },
                        "json"
                    );
                }
            });            
        });
    },
    TermAddInit : function(){
        $("[id^='add_term_']").live("click",function(){         
            var parent_id = $(this).attr("id").split("_")[2];
            if(parent_id == "0")
            {                                  
                var txt=document.createElement("INPUT");  
                $("#term_list").append(txt);                   
                $(txt).focus();
                $(txt).blur(function(){                        
                    var value = $.trim($(this).val());                        
                    if(!value){
                        $(this).remove();
                    }
                    else
                    {
                        $.post("../Handler/Words.ashx",
                            {"type":"add","parent_id" :parent_id,"term_value":value},
                            function(data){
                                if(data)
                                {
                                    var id = data["term_id"];
                                    var content = [];
                                    content.push("<div><ul class=\"Sensitive_word_category\"><span id=\"term_value_"+id+"\">"+value+"</span>");
                                    content.push("<span id=\"term_del_"+id+"\" class=\"term_del\" style=\"display: none;\">删除</span></ul>");
                                    content.push("<ul class=\"Sensitive_word_item\"><li><a id=\"add_term_"+id+"\" href=\"javascript:void(null);\">添加</a></li></ul></div>");
                                    $(txt).remove();
                                    $("#term_list").append(content.join(""));  
                                    wordManage.HoverInit();                                           
                                }
                            },
                            "json"
                        );
                    }
                });                    
            }
            else
            {
                var li = document.createElement("LI");
                var txt=document.createElement("INPUT");  
                li.appendChild(txt);
                $(this).parent("li").before(li);
                $(txt).focus();
                $(txt).blur(function(){
                    var value = $.trim($(this).val());                        
                    if(!value){
                        $(li).remove();
                    }
                    else
                    {
                        $.post("../Handler/Words.ashx",
                            {"type":"add","parent_id" :parent_id,"term_value":value},
                            function(data){
                                if(data)
                                {
                                    var id = data["term_id"];
                                    var content = [];
                                    content.push("<span id=\"term_value_"+id+"\">"+value+"</span>");
                                    content.push("<span id=\"term_del_"+id+"\" class=\"term_del\" style=\"display: none;\">删除</span>");                                       
                                    $(li).empty().html(content.join(""));  
                                    wordManage.HoverInit();       
                                }
                            },
                            "json"
                        );
                    }
                });
            }
        });       
    }
}