var url  ="";
var Params="||1|24|1|2||||";
var ParamsIncludePageIndex = "";
var PageSizeNew = "24";
var CheckValues = "2||||";
var CheckValuesLast = "2||||";
var Keyword="";
var LinkParams="";
var blGetTab1Data = "0";
var SearchTab = "G2";

//页面加载
$(function(){

    url   =   String(window.document.location); //链接地址URL
    //SearchTab
     SearchTab =GetParamValue(url,"SearchTab");     
     if(SearchTab == null ||SearchTab == "") 
     {
        SearchTab = "G2";
     }
   
     PageSizeNew =  GetNewPageSize();
     LinkParams ="||1|"+PageSizeNew+"|1|"; 
    
    //初始化页
    if(CEFlag=="2")
    {    
        CheckValues = CheckValuesLast = "1||||";
    }


     initialDragFunction();
     if(SearchTab == "G2")
     GetList("G2_4");
     else
     GetList("G1_1");
     initial();  
     
	$("#search_type_holder li").click( function(){
		$("#search_type_holder li").removeClass("on");
		$(this).addClass("on");
		$("#search_type_holder").removeClass("type1");
		$("#search_type_holder").removeClass("type2");
        if ( $("#search_type_holder li").eq(0).hasClass("on") ){
			    $("#search_type_holder").addClass("type1");
			    $("#tab1").show();
			    $("#tab2").hide();
			    $("#main, #search_type_holder").attr("style","width:960px");
			    $(".header,.nav,.search,.footer").removeClass("widthAuto");
			    $(".header,.nav,.search,.footer").addClass("width960");			
			    if(blGetTab1Data == "0")
			    {			 
		            initialClickFunction();
                    blGetTab1Data = "1";                   
			    }
			    
		    }else{		 
			    $("#search_type_holder").addClass("type2");
			    $("#tab1").hide();
			    $("#tab2").show();
			    $("#main, #search_type_holder").attr("style","width:100%");
			    $(".header,.nav,.search,.footer").removeClass("width960");
			     $(".header,.nav,.search,.footer").addClass("widthAuto");			
			    if(Params != "")
			    {			       
			        //判断条件是否有变化，如果变化则需要重新加
			        if(CheckValues != CheckValuesLast)
			        {	

			            CheckValuesLast = CheckValues;	
			            Params = Keyword+LinkParams+CheckValues;     
                        ParamsIncludePageIndex = "";		        	        
    			        Reload();	
			        }            
			    }
			    else
		        {		        
		           if(SearchTab == "G2")
                     GetList("G2_4");
                     else
                     GetList("G1_1");
                     initial();
		        }
		    }
	});	
})
function initialClickFunction()
{
        $.ajax({
            type: "GET",
            url: "/Handler/GetCata.ashx",
            cache:false,
            data: "SearchTab="+SearchTab,
            success: function(msg){
            $("#tab1").html(msg);
            }
            });  
}
function initialDragFunction()
{
        if(SearchTab == "G1")
            {
                $("#class3").removeClass("class_holder");
                $("#class3").addClass("class_holder2");
            }
            else
            {
              $("#class3").removeClass("class_holder2");
                $("#class3").addClass("class_holder");
            }
            //for show on the initial , added by mashilei 12222008
             $("ul.property_title li:first").attr({style:"font-weight:bold;"});
             
             $.ajax({
                   type: "GET",
                   url: "/Handler/GetCata.ashx",
                   data: "cataid="+SearchTab+"&div=1",
                   cache:false,
                   success: function(msg){
                   $("#class3").html(msg);
                   }
                 });
                 $("#class1,#class2").attr({style:"display:none;"});
                 $("#advancedImageList").html("<div><br/><p>&nbsp;&nbsp;&nbsp;请在左侧拖动关键字以搜索您需要的图片</p><br/></div>");                
}

function initial()
{      
	$(".class_toggle_arrow").click( function(){ $(".class_wrapper").toggle(); });
    //输入框点击
	$('#AddInput  input[type="button"]').click( InputInclude);
	$('#ExceptAddInput  input[type="button"]').click( InputExcept);	
	// make images in the gallery draggable
	$('#keywordIncluding a, #keywordException a,#KeywordContent a').draggable({
		helper: 'clone'
	});
	// make the keyword_content box droppable
	$('#KeywordContent').droppable({
		accept: '#keywordIncluding a, #keywordException a',
		activeClass: 'keyword_content_active',
		opacity: '0.5',
		drop: function(ev, ui) {
		    $(this).addClass('dropped');
			ui.draggable.fadeOut('fast', function() {
				$(this).fadeIn('fast');
			}).appendTo($(this));			
			comparetTo(".keyword_including a","#keywordException a",true,true);				
		}
	});
	// make the keyword_including box droppable
	$('#keywordIncluding').droppable({
		accept: '#KeywordContent a, #keywordException a',
		activeClass: 'including_active',
		opacity: '0.5',
		drop: function(ev, ui) {
			$(this).addClass('dropped');
			ui.draggable.fadeOut('normal', function() {
				$(this).fadeIn('fast');
				}).appendTo($(this));
		
		comparetTo("#keywordIncluding a","#keywordException a",false);	
		}
	});		
	// make the keyword_including box droppable
	$('#keywordException').droppable({
		accept: '#KeywordContent a, #keywordIncluding a',
		activeClass: 'exception_active',
		opacity: '0.5',
		drop: function(ev, ui) {
			$(this).addClass('dropped');
			ui.draggable.fadeOut('normal', function() {
				$(this).fadeIn('fast')
			}).appendTo($(this));
					
			comparetTo("#keywordIncluding a","#keywordException a",true,false);			
		}
	});
   $(":checkbox").click(check);
   
    
}
function comparetTo(a,b,isRes,isInter){
	
	  var globalKey="";	
		var keyConNew=[];
		var keyArr=[];
		var keyArrExcep=[];
		//if(){
			$(a).each(
					function(i,v){						
						keyArr.push($(v).attr("value"));
					}
			);
			$(b).each(
					function(i,v){					
						keyArrExcep.push($(v).attr("value"));
					}
			);
			if(!isInter){
				for(var i=0;i<keyArr.length;i++){
						for(var j=0;j<keyArrExcep.length;j++){
								if(keyArr[i]==keyArrExcep[j]) {
									if(isRes){
											 keyArr.splice(i,1);
										}else{
											 keyArrExcep.splice(j,1);
										}
									}
					  }
				}
			}else{
				$("#KeywordContent a").each(
				function(i,v){				
					keyConNew.push($(v).attr("value"));
				});

				for(var i=0;i<keyConNew.length;i++){
				   for(var j=0;j<keyArr.length;j++){
						if(keyArr[j]==keyConNew[i]) keyArr.splice(j,1);
				  }
				 }
				 for(var i=0;i<keyConNew.length;i++){
				  for(var j=0;j<keyArrExcep.length;j++){
						if(keyArrExcep[j]==keyConNew[i]) keyArrExcep.splice(j,1);
				   }
				 }
			}              
        			 
        	if(keyArr.length==0 )
			{
			   globalKey="";
			}
			   
	        else 	        
	        {
	              if(keyArrExcep.length==0)
	              {
                     globalKey= keyArr.join(" and ");
                 }          
                 else
                 {
                 //在此处理keywordException里的多余的东方人物
                  for(var j=0;j<keyArrExcep.length;j++){
						if(keyArrExcep[j].indexOf(' 东方人物')>-1) 
						{						   
						    keyArrExcep[j] = keyArrExcep[j].replace(" 东方人物", "");
						}
				   }
                    globalKey= keyArr.join(" and ")+" not "+keyArrExcep.join(" not ");
                }	
            }        
	
        Keyword = globalKey;
         Params = Keyword+LinkParams+CheckValues;      
       
	    $.ajax({
	       type: "GET",
	       url: "/Handler/GetImage.ashx",
	       cache:false,
	       data: "q="+encodeURI(Params)+"&CEFlag="+ CEFlag+"&Fr=9",
	       success: function(msg){
	            $(".advanced_search_holder").attr({style:"display:block;"});
	           $("#advancedImageList").html(msg);
	            ShowTooltip();        
            FavFunction();    
	       },	           	          
           error:function(){	     
           alert("您访问的页面出现问题，请稍候再试");	           
           }
	     });
	}  
//复选框事件
function check()
{
   Keyword= GetKeywords();      
   //获取复选框的
   CheckValues =GetCheckBoxValue();
       if(Keyword!="")
       {   
            Params = Keyword+LinkParams+CheckValues;              
           if($("#tab2").attr("style") != "display: none;")
           { 	
           	    CheckValuesLast = CheckValues;                
	            $.ajax({
               type: "GET",
               url: "/Handler/GetImage.ashx",
               cache:false,	              
               data: "q="+encodeURI(Params)+"&CEFlag="+ CEFlag+"&Fr=9",
               success: function(msg){	               
                    $(".advanced_search_holder").attr({style:"display:block;"});
                    $("#advancedImageList").html(msg);
                     ShowTooltip();        
                      FavFunction();    
               },
               error:function(){	                
                    alert("您访问的页面出现问题，请稍候再试");
               }
	           
               });
            }
     }
}
function GetCheckBoxValue()
{
  //获取RMRF的
        var rmrf = "";
        if($("#type_RM").attr("checked")) rmrf = "0";
        if($("#type_RF").attr("checked")) rmrf += "1";
        if(rmrf == "01"|| rmrf == "") rmrf="2";            
        return rmrf+"||||";
}
function ownKeywordAddInput(evt)
{
     if(evt.keyCode==13)
      {
      InputInclude();
      }
      }
function ownKeywordExceptAddInput(evt)
{
     if(evt.keyCode==13)
      {
        InputExcept();
      }
  }
function InputInclude(){

	 $('#keywordIncluding').addClass("dropped");
	 var own = $('.add_to_search #AddInput input').val();
	 own = jQuery.trim(own);								 
	  
	 if( own.length != 0) 
	 {
	     //在此处理"|"问题 add on 1015
         while(own.indexOf('|')>-1)
        {
            own = own.replace(/\|/g,"");        
        } 
        while(own.indexOf(' ')>-1)
        {
            own = own.replace(/ /g,"");        
        } 
            
	     var ExistsKeywordArr = GetExistsKeywordArr();
	     for(var i=0;i<ExistsKeywordArr.length;i++)
	     {
	        if(ExistsKeywordArr[i]==own)
	        {
	            alert('此关键字已存在，请通过拖拽操作获取关键字来搜索图片');
	            return;
	        }						       
         }						    			
		 $('#keywordIncluding').append("<a href='#c' value="+own+" style='cursor:pointer'>"+ own +"</a>"); 
		 $('.add_to_search #AddInput #baohan').val('');								
		 searchImgSelfInclude();
	 }
	 $('#keywordIncluding a').draggable({
		helper: 'clone'											 
	 });
}
	
function InputExcept()
{
    $('#keywordException').addClass("dropped");
	 var own_no = $('.add_to_search #ExceptAddInput input').val();
	 own_no = jQuery.trim(own_no);
	 if( own_no.length != 0) 
	 {
	         //在此处理"|"问题 add on 1015
         while(own_no.indexOf('|')>-1)
        {
            own_no = own_no.replace(/\|/g,"");        
        }  
         while(own_no.indexOf(' ')>-1)
        {
            own_no = own_no.replace(/ /g,"");        
        } 
        
	     var ExistsKeywordArr = GetExistsKeywordArr();
	     for(var i=0;i<ExistsKeywordArr.length;i++)
	     {
	        if(ExistsKeywordArr[i]==own_no)
	        {
	            alert('此关键字已存在，请通过拖拽操作获取关键字来搜索图片');
	            return;
	        }						       
         }	
         
		 $('#keywordException').append("<a href='#c' value="+own_no+" style='cursor:pointer'>"+ own_no +"</a>"); 
		 //$('.pop_add_own_no input').val('');
    	 $('.add_to_search #ExceptAddInput #bubaohan').val('');
		 searchImgSelfExcept();
	 }
	 $('#keywordException a').draggable({
		helper: 'clone'											 
	 });
}
//页面重新加载
function Reload()
{ 
  //判断是不是编辑频道，是则绑定自由下载
    if(CEFlag == "2")
    {
        $("#type_RF").attr("checked","checked");
        $("#type_RM").attr("checked","");
        ParamsIncludePageIndex =  ResetlinkParameter(ParamsIncludePageIndex,"1",5);	        
        Params =  ResetlinkParameter(Params,"1",5);	        
    }    
    var param = ParamsIncludePageIndex;
    if (param == "")      param = Params;     
	$.ajax({
	   type: "GET",
	   url: "/Handler/GetImage.ashx",
	   cache:false,
	   data: "q="+encodeURI(param)+"&CEFlag="+ CEFlag+"&Fr=9",
	   success: function(msg){	 
	       $(".advanced_search_holder").attr({style:"display:block;"});
	       $("#advancedImageList").html(msg);
	        ShowTooltip();        
            FavFunction();    
	   },
       error:function(){   
           $(".advanced_search_holder").attr({style:"display:block;"});
           alert("您访问的页面出现问题，请稍候再试");
       }
	 });
}
//自定义关键字 包含
function searchImgSelfInclude()
{   
	Keyword = GetKeywords();  
	Params =  Keyword+LinkParams+CheckValues;
	$.ajax({
	   type: "GET",
	   url: "/Handler/GetImage.ashx",
	   cache:false,
	   data: "q="+encodeURI(Params)+"&CEFlag="+ CEFlag+"&Fr=9",
	   success: function(msg){	 
	       $(".advanced_search_holder").attr({style:"display:block;"});
	       $("#advancedImageList").html(msg);
	        ShowTooltip();        
            FavFunction();    
	   },	           	          
       error:function(){	     
         alert("您访问的页面出现问题，请稍候再试");       
       }
	 });   
}
//自定义关键字 不包
function searchImgSelfExcept()
{      
	Keyword = GetKeywords();  
	Params =  Keyword+LinkParams+CheckValues;	
	
	$.ajax({
	   type: "GET",
	   url: "/Handler/GetImage.ashx",
	   cache:false,
	   data: "q="+encodeURI(Params)+"&CEFlag="+ CEFlag+"&Fr=9",
	   success: function(msg){	  
	        $(".advanced_search_holder").attr({style:"display:block;"});
	        $("#advancedImageList").html(msg);
	         ShowTooltip();        
            FavFunction();    
	   },	           	          
       error:function(){	          
            alert("您访问的页面出现问题，请稍候再试");         
       }
	 });   
}
//相似搜索
function similarSearch(pic_id)
{ 
   //判断是否是新搜索    
    var param = pic_id+"||1|20|1|2||||";   
    window.open("/FrameSet.aspx?SearchType=6&q="+param+"&CEFlag="+CEFlag,"_blank");      
 }
//翻页
function SearchImg(pageIndex){
  
    $("#advancedImageList").html("<div style=\"margin-left:25px; margin-top:10px;\"><p><img src=\"/image/frameSet/loading.gif\" alt=\"正在检索图片，请稍候.. ...\" /></p></div>").css({height:"485px"});
	ParamsIncludePageIndex = ResetlinkParameter(Params,pageIndex,6);   
		
	$.ajax({
	   type: "GET",
	   url: "/Handler/GetImage.ashx",
	   cache:false,
	   data: "q="+encodeURI(ParamsIncludePageIndex)+"&CEFlag="+ CEFlag+"&Fr=9" ,
	   success: function(msg){	  
	         $(".advanced_search_holder").attr({style:"display:block;"});
	         $("#advancedImageList").css({height:""});
	        $("#advancedImageList").html(msg);
	         ShowTooltip();        
            FavFunction();    
	   },	           	          
       error:function(){        
           alert("您访问的页面出现问题，请稍候再试");           
       }
	 });   
}
function SearchImgByTxt(currentPage,pageCount)
{
    var reg = new RegExp("^[0-9]+$");//
    if(!reg.test(currentPage))
    {

      alert("页数必须为整数！");
      return false;
     }
     
    if(currentPage>pageCount)
    {
        alert("页面必须介于1和"+pageCount+"之间");
    }
    else
    {
        if(currentPage == 0)
        {
            currentPage = 1;
        }
        SearchImg(currentPage);
    } 
}

//得到拖拽框中的?
function GetKeywords()
{
    var keys="";	
    var keyArr=[];
	var keyArrExcep=[];	
	
	$("#keywordIncluding a").each(
	    function(i,v){				
		    keyArr.push($(v).attr("value"));
	    });
				
	 $("#keywordException a").each(
	function(i,v){
		keyArrExcep.push($(v).attr("value"));
	});
        			 
	if(keyArr.length==0 )
	{
	   keys="";
	}	   
    else 	        
    {
      if(keyArrExcep.length==0)
      {
         keys= keyArr.join(" and ");
      }          
      else
      {
      //在此处理keywordException里的多余的东方人
      for(var j=0;j<keyArrExcep.length;j++)
      {
		if(keyArrExcep[j].indexOf(' 东方人物')>-1) 
		{						   
		    keyArrExcep[j] = keyArrExcep[j].replace(" 东方人物", "");
		}
	   }
        keys= keyArr.join(" and ")+" not "+keyArrExcep.join(" not ");
    }	
  }           
    return $.trim(keys);
}

//判断是否存在自定义的关键字，如果有就提示，没有的话就加入
//得到拖拽框中的?
function GetExistsKeywordArr()
{       
    var keyArrResult=[];
	
    $("#keywordIncluding a").each(
			    function(i,v){					   
				    keyArrResult.push($(v).attr("value"));
			    });
				
     $("#keywordException a").each(
    function(i,v){
	    keyArrResult.push($(v).attr("value"));
    });    
	
     $("#KeywordContent a").each(
     function(i,v){
        keyArrResult.push($(v).attr("value"));
    });
  
    return keyArrResult;
}    

function GetNewPageSize()
{
 var pageSizeNew =  GetCookie("onlineNewPageSize");
 if(pageSizeNew==null||pageSizeNew==""||pageSizeNew == "20") return "24";
 else if(pageSizeNew == "40") return "48";
 else if(pageSizeNew == "60") return "72";
 else if(pageSizeNew == "80") return "96";
 else if(pageSizeNew == "100") return "120";
 else if(pageSizeNew == "200") return "192";
 else return "24";

}
 //tooltip预览图
function ShowTooltip()
{
$(".img img").tooltip({
      track: true,
      delay: 1000,
      showURL: false,
      bodyHandler: function() {
      return $("<img/>").attr("src", this.lowsrc);
      }
      });
 
 }  