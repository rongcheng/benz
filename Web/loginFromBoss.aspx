<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="loginFromBoss.aspx.cs" Inherits="WebUI.loginFromBoss1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <script src="/UI/Js/login.js?rnd=<%=DateTime.Now.ToString("yyyyMMddhhmmss") %>" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input type="text" id="txtloginName" css="searchBox" MaxLength="20" />
    <input type="password" id="txtPassword" css="searchBox" MaxLength="20" />
    </div>
    </form>
</body>
</html>
