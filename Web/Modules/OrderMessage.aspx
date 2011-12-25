<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderMessage.aspx.cs" Inherits="WebUI.Modules.OrderMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <script src="../UI/Script/jquery-1.2.6.pack.js" type="text/javascript"></script>
    <style type="text/css">
    *{margin:0;padding:0}
    body{ font-size:12px; margin:5px;line-height:22px;}
    .u{ display:block; float:left; width:260px; background-color:#eeeeee; padding-left:5px;}
    .t{display:block; float:left; width:200px; text-align:right;background-color:#eeeeee;padding-right:5px}
    .c{display:inline-block; padding:5px}
    ul{ width:480px; margin-left:auto; margin-right:auto}
    li{ list-style:none; margin-bottom:5px;}
    .txt{ margin-left:10px;}
    textarea { font-size:12px;}
    
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
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
        <input  type="button" onclick="location.href='OrderDetail.aspx?id=<%=_orderId %>'"  value="返回"/>
    
    </div>
    </div>
    </form>
</body>
</html>
