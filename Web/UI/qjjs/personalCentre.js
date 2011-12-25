//定义全局变量
var pageNum = 20;
var userLayoutFlag="0";
var userHiresFlag="0";

$(function(){
    //用一个隐藏域来记录本页是否已登录
    userLayoutFlag = document.getElementById("hiddenUserLayoutFlag").value;
    userHiresFlag = document.getElementById("hiddenUserHiresFlag").value;
    if(userHiresFlag == "2"||userHiresFlag == "4"||userHiresFlag == "6") CEFlag="2";//媒体客户
    else CEFlag = "1";//创意客户


   

     //判断是不是创意用户--创意用户不显示订单管理
     if(CEFlag == "2")//编辑
     {
          //item_content3
          selectDate("select_Orders");
          $("#item3").attr({style:"display:block;"});
     }
     else
     { 
          $("#item3").attr({style:"display:none;"});                  
     }
       //判断用户是不是具有小样图权限
     if(userLayoutFlag != "0")//编辑
     {
          //item_content5
          //selectDate("select_Layout");
          $("#item5").attr({style:"display:block;"});   
     }
     else
     { 
          $("#item5").attr({style:"display:none;"});                  
     }
     
     //InitFolder();
    var url   =   String(window.document.location);
    url = decodeURI(url);     
    var pos = url.indexOf("#");
    var identityLightbox="";
    if(pos>-1)   
    { 
     identityLightbox = url.substr(url.indexOf("#"),9); 
     if(identityLightbox=="#lightbox") 
     {
     //显示收藏夹
         $(".individual_tabs li").removeClass("on");
	    $("#item2").addClass("on");		
	    $("#ManageFav").addClass("on");
		$("#createFav").removeClass("on");
	    $("#item_content1,  #item_content2,#item_content3, #item_content4, #item_content5, #item_content6").hide();		
	    $.ajax({
           type: "GET",
           url: "/Handler/FavoriteHandler.ashx",
           cache:false,
           data: "Oper=GetFavList", 
           success: function(msg){if(msg.substr(0,3)!="Err") {$("#FavTableBody").html(msg);loadFavMethod();} else alert(msg);},
           error:function(){alert("您访问的页面出现问题，请稍后再试");}
        }); 
	    $("#item_content2").show();
     }
   }
   else
   {   
     //得到相关数据并展示
         GetPageInitializeInfo();      
        $(".individual_tabs li").addClass("on");
        $("#item_content1, #item_content2, #item_content3, #item_content4, #item_content5, #item_content6").hide();  
        $("#item_content1").show();
    }


    //首页点击事件
	$(".individual_tabs #item1").click( function(){
	    $(".individual_tabs li").addClass("on");
		    $("#item_content1, #item_content2, #item_content3, #item_content4, #item_content5, #item_content6").hide();
		    $("#item_content1").show();    		
    		GetPageInitializeInfo();	    
	});
	
	
	
	//收藏夹点击事件
	//$("#folderPage, #item2").click( function(){window.open("/Favorite/Favorites.aspx?CEFlag="+CEFlag,"_blank");});	
	
	$(".individual_tabs #item2").click( function(){
		$(".individual_tabs li").removeClass("on");
		$(this).addClass("on");		
		$("#ManageFav").addClass("on");
		$("#createFav").removeClass("on");
		$("#item_content1,  #item_content2,#item_content3, #item_content4, #item_content5, #item_content6").hide();		
		$.ajax({
	       type: "GET",
	       url: "/Handler/FavoriteHandler.ashx",
	       cache:false,
	       data: "Oper=GetFavList", 
	       success: function(msg){if(msg.substr(0,3)!="Err") {$("#FavTableBody").html(msg);loadFavMethod();} else alert(msg);},
	       error:function(){alert("您访问的页面出现问题，请稍后再试");}
	    }); 
		$("#item_content2").show();
	});
		
	
	//订单管理
	$(".individual_tabs #item3").click( function(){
		$(".individual_tabs li").removeClass("on");
		$(this).addClass("on");
		$("#download_item1").addClass("on");
		$("#download_item2").removeClass("on");
		$("#item_content1,  #item_content2,#item_content3, #item_content4, #item_content5, #item_content6").hide();
		var SearchDate = document.getElementById("select_Orders").options[document.getElementById("select_Orders").selectedIndex].value;
		$.ajax({
	       type: "GET",
	       url: "/Handler/OrderImages.ashx",
	       cache:false,
	       data: "DownState=N&searchDate="+SearchDate+"&CEFlag="+CEFlag, 
	       success: function(msg){ $("#div_OrdersImg").html(msg);},
	       error:function(){alert("您访问的页面出现问题，请稍候再试");}
	    }); 
		$("#item_content3").show();
	});
	
	//小样图
	$(".individual_tabs #item5").click( function(){
		$(".individual_tabs li").removeClass("on");
		$(this).addClass("on");
		$("#layout_item1").addClass("on");
		$("#item_content1, #item_content2,  #item_content3, #item_content4, #item_content5, #item_content6").hide();
		ShowLayoutPics(1);
		$("#item_content5").show();
	});

	//全景新闻
	$(".individual_tabs #item6,.indi_ul #message1,#MessagePage").click( function(){	
	    $(".individual_tabs li").removeClass("on");
	    $(".i_sidebar li").removeClass("on");
	    $("#item6").addClass("on");
	    $("#message1").addClass("on");
	    $("#item_content1, #item_content2, #item_content3, #item_content4, #item_content5, #item_content6").hide();
	    $("#item_content6").show();
	    $("#receive_box_holder").show();
	    $.ajax({
           type: "GET",
           url: "/Handler/siteMessage.ashx",
           cache:false,	          
           success: function(msg){ $("#getSiteMessage").html(msg); },
           error:function(){ alert("您访问的页面出现问题，请稍候再试");}
        }); 	 
	});	
	//帐号管理
	$(".individual_tabs #item4,.indi_ul #indi_item1").click( function(){		
	    $(".individual_tabs li").removeClass("on");
	    $(".i_sidebar li").removeClass("on");
	    $("#item4").addClass("on");
	    $("#indi_item3").addClass("on");
	    $("#item_content1, #item_content2, #item_content3, #item_content4, #item_content5, #item_content6").hide();  
	    $("#item_content4").show();
	    $("#indi_content2, #indi_content3").hide();			  
	    $("#receive_box_holder").show();
	    $.ajax({
           type: "GET",
           url: "/Handler/siteMessage.ashx",
           cache:false,           
           success: function(msg){$("#getSiteMessage").html(msg);},
           error:function(){alert("您访问的页面出现问题，请稍候再试");}
        }); 
	});
    //帐号管理--密码管理
	$(".indi_ul #indi_item2").click( function(){	
		$(".indi_ul li").removeClass("on");
		$(this).addClass("on");
		$("#indi_content2, #indi_content3").hide();
		$("#indi_content2").show();
	});
	//帐号管理--修改个人信息
	$(".individual_tabs #item4,.indi_ul #indi_item3").click( function(){	
        $(".indi_ul li").removeClass("on");
        $("#indi_item3").addClass("on");
        $(" #indi_content2, #indi_content3").hide();		
        $.ajax({
           type: "GET",
           url: "/Handler/personal.ashx",	
           data: "type=1", 
           cache:false,
           success: function(msg){
               var clientListArr = msg.split('|||'); 
               //绑定个人信息
               var clientInfoArr = clientListArr[2].split('|');	
                  $("#realName").val($.trim(clientInfoArr[0].toString())) ;
                  $("#emailAddress").val($.trim(clientInfoArr[1].toString())) ;
                  $("#tel").val($.trim(clientInfoArr[2].toString())) ;
                  $("#msn").val($.trim(clientInfoArr[3].toString())) ;
                  $("#corpName").val($.trim(clientInfoArr[4].toString())) ;
                  $("#cityName").val($.trim(clientInfoArr[7].toString())) ;
                  $("#contactAddress").val($.trim(clientInfoArr[8].toString())) ;
                  $("#zip").val($.trim(clientInfoArr[9].toString())) ; 
                  $("#accept_email").attr("value",$.trim(clientInfoArr[10].toString()));
                  if($.trim(clientInfoArr[10].toString()) == "1")
                  {
                      $("#accept_email").attr("checked",true);//打勾
                  }
                  $("#userName").text($.trim(clientInfoArr[11].toString()));
    //              if (document.all) 
    //              {
    //                document.getElementById('userName').innerHTML = $.trim(clientInfoArr[11].toString());
    //              }
    //              else 
    //              {
    //                document.getElementById('userName').textContent = $.trim(clientInfoArr[11].toString());
    //              }
                  
                  var uSex = $.trim(clientInfoArr[12].toString());
              
                  if(uSex == "0" || uSex == "") document.getElementById("ddl_Gander").options[0].selected = true;            
                  else if(uSex == "1")  document.getElementById("ddl_Gander").options[1].selected = true;           

                  $("#corpTypes").empty();//清空下拉框
                  $("#belongArea").empty();//清空下拉框             
               
                  //绑定公司类型
               var industryTypeArr = clientListArr[0].split('||');
               var indexSelect=0;	      
               $.each(industryTypeArr,function(i){
               var singleType = industryTypeArr[i].split('|');

               document.getElementById("corpTypes").options.add(new Option(singleType[1].toString(), singleType[0].toString())); 
               if($.trim(singleType[0].toString()) == $.trim(clientInfoArr[5].toString())) indexSelect = $.trim(clientInfoArr[5].toString());         
               });	    
               //document.getElementById("corpTypes").options[indexSelect].selected=true;
               $("#corpTypes").attr("value",indexSelect);//设置value=-sel3的项目为当前选中项
               
               //绑定所属地区
               var belongAreaArr = clientListArr[1].split('||');
               var indexSelectA = 0;
               $.each(belongAreaArr,function(i){
               var singleTypeA = belongAreaArr[i].split('|');

               document.getElementById("belongArea").options.add(new Option(singleTypeA[1].toString(), singleTypeA[0].toString())); 
               if($.trim(singleTypeA[0].toString()) == $.trim(clientInfoArr[6].toString()))
               {	
   	                 indexSelectA = $.trim(clientInfoArr[6].toString());
               }
               });	   
               //document.getElementById("belongArea").options[indexSelectA].selected=true;
               $("#belongArea").attr("value",indexSelectA);//设置value=-sel3的项目为当前选中项   
           },
            error:function(){alert("您访问的页面出现问题，请稍候再试");}
         });
		
        $("#indi_content3").show();    
	});
})	  
//Ready方法结束

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//
function GetPageInitializeInfo()
{
 //首页得到收藏夹数量及消息数量
      $.ajax({
               type: "GET",
                url: "/Handler/siteMessage.ashx",
                cache:false,
                data: "favoriteOper=GetFolderMessageCount", 
               success: function(msg){        	             
               var infoArr = msg.split('|');
               if(infoArr[3] <= 0)
               {
                 infoArr[3] = 0;
               }
               $("#item_content1").show();
               $("#userName").html(infoArr[0].toString());	
               $("#item_content1 h2").html(infoArr[0].toString()+"，欢迎您再次回来！");
               $("#label_tip1").html("您共有<strong>"+infoArr[1].toString()+"</strong> 条新站内信息，");        

                $("#label_tip2").html("您有<strong>"+infoArr[2].toString()+"</strong>个收藏夹,还可建<strong>"+infoArr[3].toString()+"</strong>个收藏夹，")  ;
               },	           	          
               error:function(){	
               alert("您访问的页面出现问题，请稍候再试");	           
               }
            }); 
}

    
    function checkAllMessage(checked)//xiaoxi
    {
        var contain = document.getElementById("getSiteMessage");
        var chkList = contain.getElementsByTagName("input");  
        if(checked == '1')
        {          
            for(var i = 0; i < chkList.length; i ++)
            {
                if(chkList[i].type=="checkbox") chkList[i].checked = true;                
            }
        }
        else
        {
             for(var i = 0; i < chkList.length; i ++)
            {
                if(chkList[i].type=="checkbox") chkList[i].checked = false;                
            }
        } 
    }
    
    function showContentByMId(MId)
    {   
         $.ajax({
	       type: "GET",
	       url: "/Handler/siteMessage.ashx",
	       data: "MId="+MId + "&showC=1&updateState=1", 
	       cache:false,
	       success: function(msg){
	           $("#divMessageContent").html(msg);	      
	           document.getElementById("divMessageContent").style.display = "block";
	       },
	       error:function(){ alert("您访问的页面出现问题，请稍候再试");}
	       }); 
    }
    
    //删除一条记录
    function deleteDivContent(MId2)
    {      
          $.ajax({
	       type: "GET",
	       url: "/Handler/siteMessage.ashx",
	       data: "MId="+MId2, 
	       cache:false,
	       success: function(msg){
	           $("#getSiteMessage").html(msg);	  	   
	           document.getElementById("divMessageContent").style.display = "none";
	       },
	        error:function(){  alert("您访问的页面出现问题，请稍候再试"); }
	       });        	
    }
    
    //可以删除多条记录
    function DeleteSiteMessage()
    {
        var divContent = document.getElementById('getSiteMessage');
        var checkList = document.getElementsByName('chkID');
        var MId = "";       
        for(var i=0; i< checkList.length; i++)
        {
            if(checkList[i].type=="checkbox" && checkList[i].checked == true)
            {
                MId += "|" + checkList[i].value;
            }
        }         
        if(MId.length == 0)  alert('请选择要删除的记录！');     
        else if(MId.length != 0)
        {
            if(confirm("确定要删除下列选择的消息么？"))
            {
        
               MId = MId.substr(1,MId.length-1);
                
               $.ajax({
	           type: "GET",
	           url: "/Handler/siteMessage.ashx",
	           data: "MId="+MId, 
	           cache:false,
	           success: function(msg){
	               $("#getSiteMessage").html(msg);
	               $("#divMessageContent").hide();//隐藏ｄｉｖ
	           },
	           error:function(){alert("您访问的页面出现问题，请稍候再试"); }
	          }); 
	        }
        }      
    }
    
    function getSiteMessageByPage(pageIndex)//获取站内消息方法
    {
         var targetDiv = "#getSiteMessage";
         $.ajax({
           type: "GET",
           url: "/Handler/siteMessage.ashx",
           data: "pageIndex="+pageIndex,
           cache:false,
           success: function(msg){
             $(targetDiv).html(msg);
           },
	        error:function(){ alert("您访问的页面出现问题，请稍候再试");}
         }); 
    }
    function getSiteMessageByPageByTxt(currentPage,pageCount)
    {  
         var reg = new RegExp("^[0-9]+$");//
        if(!reg.test(currentPage))
        {
           alert("请输入页数进行翻页！");
           return false;
        }
         if(currentPage > pageCount)  alert( "页面必须介于1到"+pageCount+"之间");
         else
         {
             if(currentPage == 0) currentPage = 1;
             getSiteMessageByPage(currentPage);
         }
        
    }

    function SearchImgByTxt(currentPage,pageCount,folderID,oper)
    {
        var reg = new RegExp("^[0-9]+$");//
        if(reg.test(currentPage))
        {
            if(currentPage>pageCount)   alert( "页面必须介于1到"+pageCount+"之间");           
            else  SearchImg(currentPage,folderID,oper);             
         }
    }
	
    //$("#btnModifyClient").click( function()
    function ModifyClientInfo()
    {    
        //var userName = $.trim($("#userName").val());
        var realName = $.trim($("#realName").val());
        var emailAddress = $.trim($("#emailAddress").val());
        var tel = $.trim($("#tel").val());
        var msn = $.trim($("#msn").val());
        var corpName = $.trim($("#corpName").val());
        
        var corpType = document.getElementById("corpTypes").options[document.getElementById("corpTypes").selectedIndex].value;
        var corpArea = document.getElementById("belongArea").options[document.getElementById("belongArea").selectedIndex].value;	 
        
        
        //userType
        var userType = "0";
    //	    if(document.getElementById("userCY").checked == true)
    //	    {
    //	        userType = "1";
    //	    }
    //	    else if(document.getElementById("userBJ").checked == true)
    //	    {
    //	        userType = "0";
    //	    }
        //userSex
        var userSex;
        if(document.getElementById("ddl_Gander").options[0].selected == true) userSex = "0";
        else if(document.getElementById("ddl_Gander").options[1].selected == true) userSex = "1";
      
        //var userSex = document.getElementById("ddl_Gander").options[document.getElementById("ddl_Gander").selectedIndex].value;//sex
        
        var cityName = $.trim($("#cityName").val());
        var contactAddress = $.trim($("#contactAddress").val());
        var zip = $.trim($("#zip").val());
        
        var accept_email = "";
        
        if($("#accept_email").attr("checked"))  accept_email = "1";
        else accept_email = "0";
         	    
        if(realName == "")
        {
            alert('请输入用户名！');
            document.getElementById('realName').focus();
            return false;
        }
        if(emailAddress == "")
        {
            alert('请输入邮件地址！');
            document.getElementById('emailAddress').focus();
            return false;
        }
         var re = new RegExp("^[\\w.-]+@([0-9a-z][\\w-]+\\.)+[a-z]{2,3}$","i");   
         if(!re.test(emailAddress))   
         {   alert("无效email地址，请重新输入！");  
             document.getElementById('emailAddress').focus();   
             return false; 
         }
        if(tel == "")
        {
            alert('请输入联系电话');
            document.getElementById('tel').focus();
            return false;
        }
       if(document.getElementById('corpName').value == "")
        {
            alert('请输入公司名称');
            document.getElementById('corpName').focus();
            return false;
        }
        if(document.getElementById('corpTypes').value == "")
        {
            alert('请选择公司类型');
            document.getElementById('corpTypes').focus();
            return false;
        }
        if(document.getElementById('belongArea').value == "")
        {
            alert('请选择所属地区');
            document.getElementById('belongArea').focus();
            return false;
        }
           
         $.ajax({
           type: "GET",
           url: "/Handler/personal.ashx",	
           data: "type=3&realName="+escape($.trim(realName))+"&emailAddress="+escape($.trim(emailAddress))+"&tel="+escape(tel)+"&msn="+escape(msn)+"&corpName="+escape(corpName)+"&corpType="+escape(corpType)+"&belongArea="+escape(corpArea)+"&cityName="+escape(cityName)+"&contactAddress="+escape(contactAddress)+"&zip="+escape(zip)+"&accept_email="+escape(accept_email)+"&uType="+ userType+"&uSex="+ userSex , 
           cache:false,
           success: function(msg) {	  if(msg.toString() == "success") alert("修改个人信息成功！"); },
           error:function(){ alert("您访问的页面出现问题，请稍候再试"); }
        });
    
	}
	
	
        //修改个人信息-密码
       // $("#modifyP").click( function(){
       function ModifyPWD(){     
        
        var oldPwd = $.trim($("#oldPwd").val());
	    var newPwd = $.trim($("#NewPassword").val());
	    var confimPwd = $.trim($("#confirmPassword").val());
	    if(oldPwd == "")
	    {
	        alert("请输入旧密码！");
	        return false;
	    }
	    if(newPwd == "")
	    {
	        alert("请输入新密码！");
	        return false;
	    }
	    if(confimPwd == "")
	    {
	        alert("请再次输入新密码！"); 
	        return false;
	    }
	    else
	    {
	        if(newPwd != confimPwd)
	         {
	            alert("两次输入的新密码不一致！");
	            return false;
	         }
	   }
	    
	    $.ajax({
        type: "GET",
        url: "/Handler/personal.ashx",	
        data: "type=2&oldPwd=" + oldPwd + "&newPwd=" + newPwd, 
        cache:false,
        success: function(msg){        	
           if(msg == "succeed") alert("密码修改成功！");    
           else if(msg == "failed") alert("密码修改失败！");
           else if(msg == "old pwd is wrong") alert("原始密码错误！");
           else if(msg == "user is not exists") alert("不存在当前用户！");
           
           $("#oldPwd").val('');
           $("#NewPassword").val('');                  
           $("#confirmPassword").val('');           
       },
	   error:function(){alert("您访问的页面出现问题，请稍候再试"); }
        });
		
	}
	
	
  
  //以下 是订单相关的代码  
//翻页事件
function SearchImgOrder( pageIndex )
{     	
    $("#loadBoxIn").attr({style:"display:none;position:absolute;left:500px;top:350px;z-index:600; background-color:White; border:2px black solid;"});
    var SearchDate = document.getElementById("select_Orders").options[document.getElementById("select_Orders").selectedIndex].value;
    var DownState = document.getElementById("hidden_orderDownState").value;

    if(DownState==null||DownState=="") DownState="N";		
	$.ajax({
       type: "GET",
       url: "/Handler/OrderImages.ashx",
       data: "DownState="+DownState+"&searchDate="+SearchDate+"&pageIndex="+pageIndex+"&CEFlag="+CEFlag, 
       cache:false,
       success: function(msg){	 
            $("#loadBoxIn").attr({style:"display:none;"});  
           $("#div_OrdersImg").html(msg);	   	 
       },
      error:function(){        
         $("#loadBoxIn").attr({style:"display:none;"});          
         alert("您访问的页面出现问题，请稍候再试");	
      }
    });  
      
}



function SearchImgByTxtOrder(currentPage,pageCount)
{
    var reg = new RegExp("^[0-9]+$");//
    if(reg.test(currentPage))
    {
        if(currentPage>pageCount) alert( "页面必须介于1到"+pageCount+"之间");
         else SearchImgOrder(currentPage);       
     }
}


function GetOrderImgs( DownState)
{
    if(DownState=='Y')
    {
		    $("#download_item1").removeClass("on");
		    $("#download_item2").addClass("on");
    }
    else
    {
		    $("#download_item1").addClass("on");
		    $("#download_item2").removeClass("on");
    }

    $("#loadBoxIn").attr({style:"display:none;position:absolute;left:500px;top:350px;z-index:600; background-color:White; border:2px black solid;"});
    document.getElementById("hidden_orderDownState").value = DownState;
    var SearchDate = document.getElementById("select_Orders").options[document.getElementById("select_Orders").selectedIndex].value;
    $.ajax({
	   type: "GET",
	   url: "/Handler/OrderImages.ashx",
	   data: "DownState="+DownState+"&searchDate="+SearchDate+"&pageIndex=1&CEFlag="+CEFlag, 
	   cache:false,
	   success: function(msg){	 
	       $("#loadBoxIn").attr({style:"display:none;"});          
	       $("#div_OrdersImg").html(msg);	   	 
	   },
	    error:function(){     
	     $("#loadBoxIn").attr({style:"display:none;"});     
         alert("您访问的页面出现问题，请稍候再试");	           
         }
	  });  
}
function selectOrderChanged(SearchDate)
{
    $("#loadBoxIn").attr({style:"display:none;position:absolute;left:500px;top:350px;z-index:600; background-color:White; border:2px black solid;"});
    var DownState = document.getElementById("hidden_orderDownState").value;

    if(DownState==null||DownState=="")
    {
        DownState="N";
    }

    $.ajax({
	   type: "GET",
	   url: "/Handler/OrderImages.ashx",
	   data: "DownState="+DownState+"&searchDate="+SearchDate+"&pageIndex=1&CEFlag="+CEFlag, 
	   cache:false,
	   success: function(msg){	 
	        $("#loadBoxIn").attr({style:"display:none;"});          
	       $("#div_OrdersImg").html(msg);	   	 
	   },
	    error:function(){  
	        $("#loadBoxIn").attr({style:"display:none;"});                
           alert("您访问的页面出现问题，请稍候再试");	           
           }
	  }); 

}

//小样图
function SearchImgByTxtLayout(currentPage,pageCount)
{
    var reg = new RegExp("^[0-9]+$");//
    if(reg.test(currentPage))
    {
        if(currentPage>pageCount)  alert( "页面必须介于1到"+pageCount+"之间");
         else ShowLayoutPics(currentPage);       
     }
}
function ShowLayoutPics(PageIndex)
{
 TopCount = $("#select_Layout").val();

    $.ajax({
	   type: "GET",
	   url: "/Handler/LayoutImages.ashx",
	  data: "TopCount="+TopCount+"&PageSize=40&PageIndex="+PageIndex,
	  cache:false,
	  success: function(msg){$("#div_LayoutImg").html(msg);},
	    error:function(){  
	        $("#loadBoxIn").attr({style:"display:none;"});                
           alert("数据库操作超时，请稍候再试");	           
           }
	  }); 
}
    
    //收藏夹及订单 打包下载
    function downhighpack(dotype,type)
	{
	    //在此 获取所有打勾的图片编号，用逗号分割
	     var picCheckedStr="";
         if(type == "Folder")
         {
            var picCheckedArr = $("#divFavoriateImgList input[type=checkbox]");
        }
        else//Order
        {
            var picCheckedArr = $("#div_OrdersImg input[type=checkbox]");        
        }
        $.each(picCheckedArr,function(iNum,value){      
            if($(this).attr("checked") == true)
            {                 
                picCheckedStr += ","+ $(this).val();
            }   
        })
        picCheckedStr = picCheckedStr.slice(1);         
        if(picCheckedStr.length == 0)
        {
            alert("未选择任何图片");
        }
        else
        {	    
    	    if (window.confirm("下载会产生费用，请确认是否订购此图！") != 0)
    	    {
    	        var href="/downhighpack.aspx?pic_id=" + picCheckedStr + "&ptype=RM&dotype="+ dotype;    	        
    	        window.open(href,'_blank','width=600,height=500');
    	    }
        }
	}
    
    function selectDate(obj)
    {
        //绑定订单时间选择列表
        var dt = new Date(); 
        m=dt.getMonth()+1;//获得月份 
        d=dt.getDate()+1;//获取日 
        y=dt.getFullYear(); //获取年(四位) 

        var dtNow = y+"年"+m+"月";

        for(var i = 0 ; i <5 ;i++)
        { 

            var dtTempText = "";
            var dtTempValue = "";
          
            if(m-i<1)
            {
                dtTempText = (y-1).toString()+"年"+(m+12-i).toString()+"月";         
                dtTempValue = (y-1).toString()+"-"+(m+12-i).toString()+"-1";
            }
            else
            {
                dtTempText = y.toString()+"年"+(m-i).toString()+"月";         
                dtTempValue = y.toString()+"-"+(m-i).toString()+"-1";
            }
             document.getElementById(obj).options.add(new Option(dtTempText, dtTempValue)); 
        }    
    }
    
    
    
    /////////////////////////////////////////////////////////////////////////////////////////////
  	function ShowManageFav()
	{
	    $("#divNewFav").hide();
	    $("#ManageFav").addClass("on");
		$("#createFav").removeClass("on");
        $.ajax({
	       type: "GET",
	       url: "/Handler/FavoriteHandler.ashx",
	       cache:false,
	       data: "Oper=GetFavList", 
	       success: function(msg){if(msg.substr(0,3)!="Err") {$("#FavTableBody").html(msg);loadFavMethod();} else alert(msg);},
	       error:function(){alert("您访问的页面出现问题，请稍后再试");}
	    }); 	    
      $("#divManageFav").show();
	}
	function ShowNewFav()
	{	
	    $("#divNewFav").show();  	
	    $("#divManageFav").hide();	
	    $("#createFav").addClass("on");
		$("#ManageFav").removeClass("on");
	    $("#txtFavName").val('');
	    $("#txtFavMemo").val('');
	}
	function ModifyFav(FavID,FavName)
	{
	    $.ajax({
	       type: "GET",
	       url: "/Handler/FavoriteHandler.ashx",
	       cache:false,
	       data: "Oper=ModifyFav&FavID="+FavID+"&FavName="+FavName, 
	       success: function(msg){if(msg.substr(0,3)!="Err") {$("#FavTableBody").html(msg);loadFavMethod();} else alert(msg);},
	       error:function(){alert("您访问的页面出现问题，请稍后再试");}
	    }); 	
	}
	function DelFav(FavID)
	{
	    if(window.confirm("您确定要删除此收藏夹么？"))
	    {
	        $.ajax({
	           type: "GET",
	           url: "/Handler/FavoriteHandler.ashx",
	           cache:false,
	           data: "Oper=DelFav&FavID="+FavID, 
	            success: function(msg){if(msg.substr(0,3)!="Err") {$("#FavTableBody").html(msg);loadFavMethod();} else alert(msg);},
	           error:function(){alert("您访问的页面出现问题，请稍后再试");}	           
	        }); 	        
	    }	    
	}
	function AddFav()
	{
	    var FavName = $("#txtFavName").val();	
	    if($.trim(FavName)=="")
	    {
	    alert("收藏夹名称不能为空！");	    
	    return false;
	    }
	    var FavMemo = $("#txtFavMemo").val();	
	    
	    
          $.ajax({
               type: "GET",
               url: "/Handler/FavoriteHandler.ashx",
               cache:false,
               data: "Oper=AddFav&FavName="+encodeURI(FavName)+"&FavMemo="+encodeURI(FavMemo), 
                success: function(msg){ alert(msg);},
               error:function(){alert("您访问的页面出现问题，请稍后再试");}
            }); 
	
	}