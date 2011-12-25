<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QJ_Search_Default.ascx.cs" Inherits="WebUI.UserControls.QJ_Search_Default" %>
<div class="search" style="padding-top:5px;">
	<h2>图片搜索</h2>
	<p class="input">
	    <input type="text" id="txt_Keyword"  name="txt_Keyword" title="请输入关键词" class="textBg" autocomplete="off"  />	    
	    <button type="button" onclick="SearchImgDefault();">搜索</button>
	</p>
	<p class="more" style="margin-top:5px">
	    
	</p>	
	<p class="row">
		<asp:RadioButtonList ID="rblSearch" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
            <asp:ListItem Text=" 在结果中搜索 " Value="0" Selected="true"></asp:ListItem> 
            <asp:ListItem Text=" 新搜索" Value="1"></asp:ListItem>
        </asp:RadioButtonList>
	</p>
	<p class="fn" style="margin-top:5px;display:none" >
	<span id="btn_ScrollShow" class="scrollWindow">滚屏显示</span>
	<span id="btn_viewFlash" class="viewFlash">图墙显示</span>
	</p>
</div>
<!-- End search -->
<link href="/UI/Css/jquery.suggest.css" rel="stylesheet" type="text/css" />
<script src="/UI/Js/AutoCompleteDefault.js" type="text/javascript"></script>
<script type="text/javascript">
//$(document).ready(function() {
//    
//    $("#txt_Keyword").suggest("http://sany.quanjing.com/handlers/keywordHandler.ashx?action=getKeys",
//        {
//            //onSelect: function() {alert(this.value);}
//        }
//    );
//    $(".ac_results").css("width","288px").css("border","1px solid #cccccc").css("border-top","0px");
//});
</script>
<script type="text/javascript">

function SearchImgDefault()
{  
    var isSearchInResult= $(".search input[@type=radio][@checked]").val();
    var keyword = $("#txt_Keyword").val();
    var keyword_last=$("#ctl00_ContentPlaceHolder1_txt_Keyword_Last").val();
    var pageSizeParam;
    var resourceTypeParam="image,other";
    var catalogIdParam;
    if(isSearchInResult=="0")
    {
        catalogIdParam="<%=Request.QueryString["CatalogID"] %>";    
    }
    else
    {
        catalogIdParam="00000000-0000-0000-0000-000000000000";
    }
    var param = encodeURI(keyword);  
    
    var hrefLink ="/ResourceList.aspx?keyword=" + encodeURI(keyword) + "&lastkeyword="+encodeURI(keyword_last)+"&Catalogid="+catalogIdParam+"&resourceType="+resourceTypeParam+"&BeginDate=&EndDate=&isSearchInResult="+isSearchInResult;
    
    if(keyword.length>0)
    {
        location.href=hrefLink; 
    }
    else
    {
        alert("关键词不能为空");
        $("#txt_Keyword").focus();   
    }
 }
 
 document.onkeydown = function(e){    
   
        if(!e) e = window.event;//火狐中是 window.evente.which||w.keyCode
        if((e.keyCode || e.which) == 13){

         $("form").submit( function () {
              return false;
            } ); 

         SearchImgDefault();
        }
    }

 
</script>