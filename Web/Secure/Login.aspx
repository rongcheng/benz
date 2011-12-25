<%@ Page Language="C#" AutoEventWireup="true"  EnableTheming="False" Codebehind="Login.aspx.cs" Inherits="WebUI.Login"
     %>

<%@ Register Src="~/UserControls/UserLogin_Sany.ascx" TagName="UserLogin" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/QJ_Bottom.ascx" TagName="Bottom" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Pragma" content="no-cache"/>
<meta http-equiv="Cache-Control" content="no-cache"/>
<meta http-equiv="Expires" content="0"/>
    <title><%=WebUI.UIBiz.CommonInfo.WebSite_Title %> 登陆页面</title>    
    <style type="text/css">   
    body{
	padding:0px;
	margin:0px;
	background:#FFF;
}
  .logInBox1{
	width:600px;
	height:400px;
	margin:60px auto;
	position:relative;
	background:url(/images/logInBg_01.jpg) top left no-repeat;
    }
    .inputBox{
	width:332px;
	height:65px;
	position:absolute;
	left: 20px;
	top: 295px;
	font-size:12px;
}
.searchBox{
	height:16px;
	border:solid 1px #666;
	background:#FFF;
}
.showBox{
	width:193px;
	height:50px;
	position:absolute;
	top: 295px;
	padding-top:9px;
	padding-left:12px;
	left: 357px;
	border-left:solid 1px #CCC;
	font-size:12px;
	color:#999;
}
 
 </style>
  <script src="/UI/Script/UI.js" language="javascript"></script>

</head>
<body id="login">
    
        <uc1:UserLogin ID="UserLogin1" runat="server" />
        <uc3:Bottom ID="Bottom1" runat="server" />
    
   
</body>
</html>
