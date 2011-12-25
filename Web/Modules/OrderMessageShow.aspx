<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderMessageShow.aspx.cs" Inherits="WebUI.Modules.OrderMessageShow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <script src="../UI/Script/jquery-1.2.6.pack.js" type="text/javascript"></script>
     <link href="/UI/artDialog211/skin/chrome/chrome.css" rel="stylesheet" type="text/css" />
    <script src="/UI/artDialog211/artDialog.js" type="text/javascript"></script>
    <style type="text/css">
    *{margin:0;padding:0}
    body{ font-size:12px; margin:5px;line-height:22px;}
    .u{ display:block; float:left; width:260px; background-color:#eeeeee; padding-left:5px;}
    .t{display:block; float:left; width:200px; text-align:right;background-color:#eeeeee;padding-right:5px}
    .c{display:inline-block; padding:5px}
    ul{ width:480px; margin-left:auto; margin-right:auto}
    li{ list-style:none; margin-bottom:5px;float:left;width:200px}
    .txt{ margin-left:10px;}
    textarea { font-size:12px;}
    #container{ margin:10px;}
    
    </style>
    <script language="javascript" type="text/javascript">
function showOrder(id)
{ 
    var url='/Modules/OrderDetail.aspx?Id='+id;
    //alert(url);
   // art.dialog({id:'newOrderWnd',title:'订单详情', iframe:url, width:'550', height:'350'});
    location.href=url;
}
function closeDialog()
{
  art.dialog({id:'newOrderWnd'}).close();
}
 </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        
        <ul>
        <asp:Repeater ID="rptOrders" runat="server">
        <ItemTemplate>        
        
        <li><a href="/Modules/OrderDetail.aspx?Id=<%#Eval("Id")%>"><%#Eval("title")%></a></li>
        </ItemTemplate>
        </asp:Repeater>
        
        </ul>
       <div style="clear:both"></div> 
    </div>
    </form>
</body>
</html>
