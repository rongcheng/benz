<%@ Page Language="C#" AutoEventWireup="true" CodeFile="U8.aspx.cs" Inherits="WebUI.U8" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>U8检测</title>
    <style type="text/css">
    html,body{height:100%;background:#fff;}body,textarea{font:12px Tahoma,"宋体",Arial,sans-serif;}
    body,form,menu,dir,fieldset,blockquote,p,pre,ul,ol,dl,dd,h1,h2,h3,h4,h5,h6{padding:0;margin:0;}
    sup,sub{vertical-align:baseline;}table{border-collapse:collapse;}
    li{list-style:none;}
    fieldset,a img{border:0;}button,input.radio,input.checkbox{cursor:pointer;}
    input[type=radio],input[type=checkbox]{cursor:pointer;}
    .claer{clear:both;}
    .claer:after{content:"清除浮动";clear:both;display:block;font-size:0;height:0;overflow:hidden;}
    a{text-decoration:none;color:#888}a:hover{text-decoration:underline;}
    
    
    ul{ margin-top:20px; margin-left:20px;}

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style=" margin-left:20px; margin-top:20px;">
    
        <asp:Button ID="btnSearch" runat="server" onclick="btnSearch_Click" Text="查询" />
        <br />
        
        <asp:Repeater ID="rptProduct" runat="server">
        <HeaderTemplate><ul></HeaderTemplate>
        <ItemTemplate>
        <li><input type="hidden" id="pid" runat="server" value='<%#Eval("productid") %>' /><asp:CheckBox ID="chb" runat="server"/> &nbsp; <span style="width:170px;display:inline-block"><%#Eval("constid") %></span>   <%#Eval("productid") %></li>
        </ItemTemplate>
        <FooterTemplate></ul></FooterTemplate>
        </asp:Repeater>
        
        <br />
        <asp:Button ID="btnUpdate" runat="server" Text="处理" onclick="btnUpdate_Click" />
&nbsp;<asp:Label ID="lblMsg" runat="server"></asp:Label>
    
    </div>
    </form>
</body>
</html>
