<%@ Page Language="C#" EnableSessionState="True" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="WebUI.Modules.OrderDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>订单详情</title>
     <style type="text/css" >
    body{margin-left:auto;margin-right:auto}
    body{font-size:12px; background-color:#ffffff}
    table td{height:30px;background-color:#ffffff}
    table{border:1px solid #dddddd}
    .l{padding-left:5px;}
    .h3{font-size:14px;line-height:25px;}
    td{font-size:12px;}
    .oneline
    {
    	display:inline;
    	
    	}
    #tbCommand a
    {
    	color:#333333;
    	margin-right:10px;
    	}
    	
    	
    	
   *{margin:0;padding:0}
    body{ font-size:12px; margin:5px;line-height:22px;}
    .u{ display:block; float:left; width:210px; background-color:#eeeeee; padding-left:5px;}
    .t{display:block; float:left; width:200px; text-align:right;background-color:#eeeeee;padding-right:5px}
    .c{display:inline-block; padding:5px}
    ul{ width:420px; margin-left:auto; margin-right:auto}
    li{ list-style:none; margin-bottom:5px;}
    .txt{ margin-left:20px;}
    textarea { font-size:12px;}
    #pGouTongJiLu{ display:none} 
    
    
    </style>
<link href="/UI/artDialog211/skin/chrome/chrome.css" rel="stylesheet" type="text/css" />
    <script src="/UI/artDialog211/artDialog.js" type="text/javascript"></script>

<script src="../UI/Script/jquery-1.2.6.pack.js" type="text/javascript"></script>
<script src="../UI/Js/js.js" type="text/javascript"></script>
 <script language="javascript" type="text/javascript"> 
 function orderDetail(id)
 {
 var _url="../OrderDetail.aspx?id="+id;
 art.dialog({id:'orderWnd', title:'订单详情',iframe:_url, width:500, height:320}).close(function(){}); 
 }
 
 function orderNotPass(id)
 {
  var _url="Manage/OrderNotPass.aspx?orderid="+id;
 //art.dialog({id:'orderNotPass', title:'退回订单', iframe:_url, width:500, height:150}).close(function(){}); 

 parent.orderNotPass(id);
  closeDialog();

 
 }
 function doClose()
 {
    art.dialog({id:'orderNotPass'}).close();
    
 }
 
 function doAccept()
 {
    alert('操作完成');
    parent.closeDialog1();
 }
 function closeDialog()
{
    
    parent.closeDialog1();

}
function selectImage(id)
{
    window.open("/Modules/Manage/ImageFrame.aspx?type=order&featureId="+id);
}

function showGouTong()
{
    $("#pGouTongJiLu").slideToggle(100);
    $("#txtMessage").focus();
    
    
}
function showGouTongYes()
{ 
    $("#pGouTongJiLu").show();
}

$(function(){
    $(".cssConfirm").click(function(){
        return confirm("确定吗？");
    });
    
    $("#tbOrder tr:even td").css("background-color","#eeeeee");
    
});
function sendMail(mail, subject){
    var myxhr = new xmlHttpObjectError("send");
    if(myxhr){
        try{
            myxhr.doContent("type=send&mail="+encodeURIComponent(mail)+"&subject="+encodeURIComponent(subject));
        }
        catch(e){
            alert("Can't cannect to server:\n"+e.toString());
        }
    }
}
 </script>
</head>
<body  >
    <form id="form1" runat="server">
    <div>
    <table width="96%" border="0" align="center"  id="tbOrder">
     
      <tr>
        <td ><span class="l">标题：</span></td>
        <td>&nbsp;<asp:Literal ID="txtTitle" runat="server"></asp:Literal></td>
      </tr>
      <tr>
        <td width="120"><span class="l">需要完成日期：</span></td>
        <td>&nbsp;<asp:Literal ID="txtRD" runat="server"></asp:Literal></td>
      </tr>
 
      <tr>
        <td><span class="l">用途：</span></td>
        <td>&nbsp;<asp:Literal ID="txtUsage" runat="server"></asp:Literal></td>
      </tr>
      <tr>
        <td valign="top"><span class="l">需求描述：</span></td>
        <td><asp:Literal ID="txtContent" runat="server"></asp:Literal></td>
      </tr>
      <tr>
        <td ><span class="l">申请者：</span></td>
        <td>&nbsp;<asp:Literal ID="txtUserName" runat="server"></asp:Literal></td>
      </tr>
      <tr>
        <td ><span class="l">申请日期：</span></td>
        <td>&nbsp;<asp:Literal ID="txtAddDate" runat="server"></asp:Literal></td>
      </tr>
      
      <tr>
        <td colspan="2">
        &nbsp;&nbsp;<asp:HyperLink ID="linkImages" Text="查看该订单的图片"  runat="server" Target="_blank"></asp:HyperLink>
        </td>
      </tr>
      <asp:Panel ID="pNotPass" runat="server" CssClass="oneline">
      <tr>
        <td valign="top" colspan="2">
        &nbsp;&nbsp;被退回的原因：<asp:Literal ID="notPassReason" runat="server"></asp:Literal>
        </td>
      </tr>
      </asp:Panel>
      
    </table>
    
    
    <table width="80%"  style="border:0px;display:" align="center" runat="server" id="tbCommand" >
     
      <tr>
      <td>
<asp:LinkButton ID="lbIsProcessing" runat="server" CommandName="IsProcessing" 
              CommandArgument='<%=_orderId %>' CssClass="cssConfirm" 
              onclick="lbIsProcessing_Click">受理</asp:LinkButton> &nbsp;
<asp:Panel ID="pNotPass1" runat="server" CssClass="oneline"><a href="javascript:orderNotPass('<%=_orderId %>');">退回</a></asp:Panel>
<asp:Panel ID="pImage" runat="server" CssClass="oneline"><a href="javascript:selectImage('<%=_orderId %>');"  >选图</a></asp:Panel>
<asp:Panel ID="pImageDel" runat="server" CssClass="oneline"><a href="/Modules/Manage/orderdetailpic.aspx?orderid=<%=_orderId %>" target="_blank">管理</a></asp:Panel>
<asp:LinkButton ID="lbComplete" runat="server" CommandName="Complete" 
              CommandArgument='<%=_orderId %>'  CssClass="cssConfirm"  
              onclick="lbComplete_Click">完成</asp:LinkButton>&nbsp;
              
<asp:Panel ID="pMessage" runat="server" CssClass="oneline"> <a href="javascript:void(0)" onclick="showGouTong();">查看沟通记录</a></asp:Panel>
      </td>
      </tr>
      </table>
      
      
      <!--查看沟通记录开始-->
      <div id="pGouTongJiLu">    
    <ul>
        <asp:Repeater ID="rptOrderMessage" runat="server">
        <ItemTemplate>
        <li>
        <span class="u"><%#Eval("UserName") %></span><span class="t"><%#Eval("adddate") %></span>
        <span class="c"><%#Eval("contents") %></span>        
        </li>
        </ItemTemplate>
        </asp:Repeater>
       
    </ul>
    
    <div class="txt">
    
    
        <asp:TextBox ID="txtMessage" runat="server" Height="97px" TextMode="MultiLine" 
            Width="405px"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="发布信息" />
        
    </div>
    </div>
      
    <!--查看沟通记录结束-->
      
      
      
    </div>
    </form>
</body>
</html>
