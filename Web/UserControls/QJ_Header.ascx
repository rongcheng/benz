<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QJ_Header.ascx.cs" Inherits="WebUI.UserControls.QJ_Header" %>
<%@ Register Src="QJ_HeaderCommon.ascx" TagName="HeaderCommon" TagPrefix="uc1" %>

 <script language="javascript" type="text/javascript">
  function newOrder()
 {
 art.dialog({id:'newOrderWnd', title:'申请订单', iframe:'/Modules/OrderNew.aspx?funId=<%=WebUI.UIBiz.CommonInfo.OrderNewFunctionID %>', width:'550', height:'350'});
 }
 function closeDialog()
{
    art.dialog({id:'newOrderWnd'}).close();

}
 </script>
<div class="header" id="header">	
<h1><a href="/" target="_top"><img src="/image/common/logo.gif" alt="<%=WebUI.UIBiz.CommonInfo.WebSite_Title %>" /></a></h1>
<div class="services" >
	<asp:Label ID="lblLoginName" runat="server"  Font-Bold="true"></asp:Label>        欢迎您！
    <asp:LoginStatus ID="logStatus" runat="server"  LogoutText="注销" 
         onloggingout="logStatus_LoggingOut"  />
    <a href="/Secure/ChangePWD.aspx" target="_blank" runat="server" id="btnModifyPwd">修改密码</a>
</div>
<uc1:HeaderCommon ID="HeaderCommon1" runat="server" />
</div>

<div class="nav">
<h2>全景网 - 栏目导航</h2>
<ul>	
<li<%=currentCss[0] %>><a href="/default.aspx" target="_top">首页</a></li>
<li<%=currentCss[6] %>><a href="javascript:void(0);" onclick="showCat();return false;" id="headCatLink">分类</a></li>
<li<%=currentCss[5] %>><a href="/Feature.aspx" target="_top">查看专题</a></li>
<li<%=currentCss[2] %>><a href="/Modules/AddResource.aspx?funid=ea2dbbc5-9fa1-4b95-a35c-904fa8233e90"  target="_top">资源上传</a></li>
<%--<li<%=currentCss[6] %>><a href="javascript:newOrder();">拍摄申请</a></li>--%>
<li<%=currentCss[3] %>><asp:Literal ID="ManageLink" runat="server"></asp:Literal></li>	
<li<%=currentCss[4] %>><a href="/Modules/UserProfile.aspx">我的账户</a></li>	
<%--<li<%=currentCss[6] %>><a href="/Modules/OrdersPA/ShoppingCart.aspx">我的购物车</a></li>--%>

<li><a href="http://boss.quanjing.com" target="_blank">Boss系统</a></li>
</ul>
</div>

<div id="headCat" >
<div id="headCatZhongjian">
<img src="/images/headCatZhongjian.jpg" />
</div>
<div id="headCatContent" >
   <asp:Repeater ID="rptBigCat" runat="server" OnItemDataBound="rptBigCat_ItemDataBoud">
    <ItemTemplate>
    <div id="cat-<%# this.rptBigCat.Items.Count + 1 %>-info" class="catItemContainer">
        <p class="catItemHead"><%#Eval("CatalogName")%></p>
        <ul>
     <asp:Repeater ID="rptSmallCat" runat="server">
        <ItemTemplate>
        <li>
<a href='/ResourceList.aspx?mi=<%# this.rptBigCat.Items.Count%>&showCata=1&CatalogID=<%#Eval("CatalogId")%>&showtype=<%#Eval("ShowType")%>&cname=<%#Eval("CatalogName")%>'>
        <%#Eval("CatalogName")%></a>
        </li>
        </ItemTemplate>
    </asp:Repeater>
    </ul>
    <p class=""></p>
    </div>
   </ItemTemplate>
   </asp:Repeater>   
   
</div>
</div>

<script type="text/javascript">
var isCatShow=false;
function showCat()
{
    var x,y; 
    var p=$("#headCatLink");
    var po=p.offset();
    var newx=po.left-200;
    document.getElementById("headCat").style.left=newx+"px";
    if(isCatShow)
    {
        $("#headCat").hide(1000);
        $("#headCatLink").removeClass("headCatLink");
    }
    else
    {
        $("#headCat").slideDown(200);    
        $("#headCatLink").addClass("headCatLink");
    }
    isCatShow=!isCatShow;
   
}

$(document).ready(function(){
   $("#headCatContent").bind('mouseleave',function(){
    $("#headCat").hide(1000);
    $("#headCatLink").removeClass("headCatLink");
    isCatShow=!isCatShow;
   });
}
)
</script>
