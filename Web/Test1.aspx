<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test1.aspx.cs"  Debug="true" Inherits="WebUI.Test1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <style type="text/css">
    
    font{width:100px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <a href="http://sany.quanjing.com/handlers/keywordHandler.ashx?q=男人"> 点击</a>
    </div>
    <asp:FileUpload ID="FileUpload1" runat="server" Width="225px" />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    </form>
</body>
</html>
