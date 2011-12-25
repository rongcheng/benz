<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetUsersByRole.aspx.cs" 

Inherits="WebUI.Modules.Manage.GetUsersByRole" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>用户列表</title>
    <style type="text/css" >
    body{font-size:12px;background-color:white}
    .item{color:black;float:;display:inline-block;border:0px solid blue;width:100px;padding:5px 5px 5px 5px}    
    
    </style>
</head>
<body>
    <form id="form1" >
    <div style="margin:10px auto auto 10px;border:1px solid #cccccc;display:block;width:450px;height:260px;padding:1px 5px 5px 5px;">
    <h3>拥有该角色的用户列表：</h3>
    <asp:Literal ID="ltNoUsers" runat="server" Text="没有用户"  ></asp:Literal>
    <asp:Repeater ID="rptUsers"  runat="server">
    <ItemTemplate>
    
    <div class="item">
        <%# Eval("LoginName").ToString() %>
    </div>
    
    </ItemTemplate>
    </asp:Repeater>
    
    </div>
    </form>
</body>
</html>
