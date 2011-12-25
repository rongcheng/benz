<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_MainPage.Master"  AutoEventWireup="true"
    Codebehind="Default.aspx.cs" Inherits="WebUI.Default" %>

<%@ Register Src="UserControls/QuickLink.ascx" TagName="QuickLink" TagPrefix="uc4" %>
<%@ Register Src="UserControls/statControl.ascx" TagName="statControl" TagPrefix="uc3" %>
<%@ Register Src="UserControls/CatalogMenu91.ascx" TagName="CatalogMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControls/QJ_Search_Default.ascx" TagName="Search_Default" TagPrefix="uc_1" %>
<%@ Register Src="UserControls/statControl.ascx" TagName="statControl" TagPrefix="uc_3" %>
<%@ Register Src="UserControls/CatalogMenu91.ascx" TagName="CatalogMenu" TagPrefix="uc_1" %>
<asp:Content ID="header1" ContentPlaceHolderID="head" runat="server">
<title><%=WebUI.UIBiz.CommonInfo.WebSite_Title %></title>   
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="margin-top:-10px;border:0px solid red"></div>
   <div id="media_flash" class="content" style="padding-top:0px;border:0px solid red">       
<div class="main_content" style="width:780px">
<link href="UI/Css/jquery.suggest.css" rel="stylesheet" type="text/css" />
<script src="UI/Js/AutoCompleteDefault.js" type="text/javascript"></script>
<script type="text/javascript">
//    $(document).ready(function() {
//        
//        $("#txt_Keyword").suggest("http://sany.quanjing.com/handlers/keywordHandler.ashx?action=getKeys",
//            {
//               
//            }
//        );
//        $(".ac_results").css("width","309px").css("border","1px solid #cccccc").css("border-top","0px");
//    });
</script>
<!-- End search -->
<script type="text/javascript">

//搜索
function SearchDefault()
{  
    var isSearchInResult= $("input[@type=radio][@checked]").val();
    var keyword = $("#txt_Keyword").val().trim();
    var keyword_last=$("#txt_Keyword_Last").val();
    var pageSizeParam;
    var resourceTypeParam="image,other";
    var catalogIdParam="00000000-0000-0000-0000-000000000000";
    if(keyword.length==0)
    {
        alert("关键词不能为空");
        $("#txt_Keyword").focus();
        return;
    }
    var param = encodeURI(keyword);  
    var hrefLink ="/ResourceList.aspx?keyword=" + encodeURI(keyword) + "&Catalogid="+catalogIdParam+"&resourceType="+resourceTypeParam+"&BeginDate=&EndDate=&isSearchInResult="+isSearchInResult;
    location.href=hrefLink; 
  
 }
 document.onkeydown = function(e){    
   
        if(!e) e = window.event;//火狐中是 window.evente.which||w.keyCode
        if((e.keyCode || e.which) == 13){

         $("form").submit( function () {
              return false;
            } ); 

         SearchDefault();
        }
    }
 function orderDetail(id)
 {
 var _url="/Modules/OrderDetail.aspx?id="+id;
 art.dialog({id:'orderWnd', title:'订单详情',iframe:_url, width:520, height:350}).close(function(){}); 
 }
 
 function OnNotices(id){
    var _url = "NoticesOpen.aspx?noticeId="+id;
    art.dialog({id:'noticesWnd', title:'最新公告',iframe:_url, width:700, height:350}).close(function(){}); 
 }
</script>
<div style="float:left; padding-top:10px;width:556px;">


<div class="search" style="padding-top:10px;padding-bottom:10px;float: left;width:550px; height:32px">
	<h2>图片搜索</h2>
	<p class="input" style="width:430px;"><input type="text" id="txt_Keyword" style="width:300px; margin-right:5px;"  name="txt_Keyword" title="关键字搜索" class="textBg" autocomplete="off"  />
	  
	    <button type="button" onclick="SearchDefault();">搜索</button> 
	         <p class="more1"  >
	    <a href="/SearchResource.aspx" style="color:#888888">高级搜索</a> 
		
	</p>	            
	</p>
	
</div>


        <object id="FlashID" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="554"
            height="360">
          <param name="movie" value="/UI/flash/sany_indexflash.swf?<%=imgString %>" />
          <param name="quality" value="high" />
          <param name="wmode" value="opaque" />
          <param name="swfversion" value="6.0.65.0" />
          <!-- 此 param 标签提示使用 Flash Player 6.0 r65 和更高版本的用户下载最新版本的 Flash Player。如果您不想让用户看到该提示，请将其删除。 -->

          <!-- 下一个对象标签用于非 IE 浏览器。所以使用 IECC 将其从 IE 隐藏。 -->
          <!--[if !IE]>-->
          <object type="application/x-shockwave-flash" data="/UI/flash/sany_indexflash.swf?<%=imgString %>" width="557"
                height="354">
            <!--<![endif]-->
            <param name="quality" value="high" />
            <param name="wmode" value="opaque" />
            <param name="swfversion" value="6.0.65.0" />

          <!--<![endif]-->
        </object></div>
        
<div style=" float:left; margin-top:13px;" class="content_index">
  	<div class="news">
    	<h2><a href="NoticesAll.aspx" target="_blank"><img src="image/common/index_topic1.gif" width="180" height="38" alt="最新公告" /></a></h2>
        <ul id="NoticesUL" runat="server">
       	 </ul>
    </div>
    <div class="person">
    	<h2><a href="Modules/UserProfile.aspx?tabid=2"><img src="image/common/index_topic2.gif" width="180" height="38" alt="个人事务" /></a></h2>
        <ul id="CalendarUL" runat="server">
      </ul>
    </div>
</div>   
      </div>
                
    <div class="sidebar" style="width:180px">
        <div class="library_box">
            <h3>资源库信息</h3>
            <div class="library">
                <div class="row">
                    <uc_3:statControl ID="statControl" runat="server" />
                </div>
                <!--end of row-->
                
            </div>
            <!--end of library-->
        </div>
        
        <div id="indexLeftSide">
        <a href="/ResourceList.aspx?keyword=&BeginDate=&EndDate=&Catalogid=00000000-0000-0000-0000-000000000000&resourceType=&groupId="><img src="/images/index_left_new.gif" alt="最新资源" /></a>
        <img src="/images/index_left_bg.gif" alt="" />
        <a href="/Feature.aspx"><img src="/images/index_left_feature.gif" alt="专题图片" /></a>
        <img src="/images/index_left_bg.gif" alt="" />
        <a href="/ResourceList.aspx?ishot=isHot&begindate=&enddate=&resourceType="><img src="/images/index_left_hot.gif" alt="热门资源" /> </a>      
        <img src="/images/index_left_bg.gif" alt="" />
        <a href="/ResourceList.aspx?isDownloadhot=isDownloadHot&begindate=&enddate=&resourceType="><img src="/images/index_left_down.jpg" alt="下载排行" style="margin-left:9px" /> </a>      
        <!--img src="/images/index_left_bottom.gif" class="bottomImg" alt="" /-->       
        </div>
    </div>
    <!-- end of main_content -->
                  
</div>
            
 
            
            
<div class="content">
	
</div>
<!-- End content -->

    <script language="javascript" type="text/javascript">
 
        function gotoChangePWD()
        {
            var href="/Secure/ChangePWD.aspx";
            window.open(href,'_blank','width=300,height=160,scrollbars=no,toolbar=no, menubar=no, scrollbars=no, resizable=yes,location=no, status=no');
        }
        
var msg1="您的订单已被处理，<br/><br/><a href='/Modules/UserProfile.aspx?tabid=2'style='color:red'>点击这里查看详情</a>";
var msg2="有新的订单等待您去处理，<br/><br/><a href='/Modules/Manage/OrdersManage.aspx?mi=1&funId=d1fcce9d-fcb1-4fac-b03d-eb1197f5eefe'style='color:red'>点击这里查看详情</a>";
var msg3="有新的图片等待您去审核，<br/><br/><a href='/Modules/Manage/ValidateResource.aspx?mi=0&funId=d9e8122b-21e1-4b18-b88a-1034256d179d'style='color:red'>点击这里查看详情</a>";
var msg4="有新的订单沟通信息，<br/><br/><a href='javascript:showOrderMessage();'style='color:red'>点击这里查看详情</a>";
var _showResult="0";
function isAlert(userId)
{
    var msgShow="";
    var isSuper="0";
    if("<%=IsSuperAdmin.ToString()%>"=="True")
    {
        isSuper="1";
    }
    $.ajax({
        type: "GET",
        cache:false,
        url: "/Handlers/alertHandler.ashx",
        data: "action=alert&userid="+userId+"&isSuperAdmin="+isSuper,
        success:function(msg)
        {
            var o=eval('('+msg+')');           
            if(o.isResourceAlert==1)
            {
                msgShow=msg3+"<br/><br/>";
            }
            
            if(o.isOrderAlert==1)
            {
                msgShow=msgShow+msg2+"<br/><br/>";
                
            }
            else if(o.isOrderAlert==2)
            {
                msgShow=msgShow+msg1+"<br/><br/>";                
            }
            
            if(o.isOrderMessageAlert==1)
            {
                msgShow=msgShow+msg4;
            }
            
            showAlertWindow1(msgShow);
        }    
    });
    
}

function showOrderMessage()
{
   var url="/Modules/OrderMessageShow.aspx";
   art.dialog({id:'orderMsg',title:'有最新沟通信息的订单列表：', iframe:url});
}
function showAlertWindow1(msg)
{
    art.dialog({mouse:true, time:100,id:'orderAlert', content:msg,left:'right',width:'200px',height:'80px', top:'bottom', fixed:true});
}
function initInputText()
{
    

}
function closeAlertWindow()
{
    art.dialog({id:'orderAlert'}).close();
}

$(document).ready(function(){
    isAlert('<%=CurrentUser.UserId.ToString() %>');
    setInterval("isAlert('<%=CurrentUser.UserId.ToString() %>')",1000*60*3); //2分钟刷新一次
    $("#txt_Keyword").val("                                   ");
    $("#txt_Keyword").click(function(){
        if(!isCatShow)
        {
            closeAlertWindow();
            if($.trim($("#txt_Keyword").val()).length==0)
            {
            $("#txt_Keyword").val("");
            }
        }
    });
});



    </script>
   
</asp:Content>
