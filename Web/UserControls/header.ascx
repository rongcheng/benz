<%@ Control Language="C#" AutoEventWireup="true" Codebehind="header.ascx.cs" Inherits="WebUI.UserControls.header" %>
<div class="logoContainer">
    <div class="logoA">
    </div>
    
    <div class="logOut">
        <div  class="showUserName"  >
            <asp:Label ID="lblLoginName" runat="server"  Font-Bold="true"></asp:Label></div>
        欢迎您！
        <asp:LoginStatus ID="logStatus" runat="server"  LogoutText="注销" />
        <a href="/Secure/ChangePWD.aspx" target="_blank" runat="server" id="btnModifyPwd">修改密码</a>
        <!--end of row-->
    </div>
</div>
<div class="nav">
    <ul>
        <li><a href="/" class="on" id="first">首页</a></li>
        <li><a href="/SearchResource.aspx" target="_top">高级搜索</a></li>
        
        <li><a href="/Modules/AddResource.aspx?funid=ea2dbbc5-9fa1-4b95-a35c-904fa8233e90">资源上传</a></li>
        <asp:Literal ID="ManageLink" runat="server"></asp:Literal>
        <li><a href="/Modules/UserProfile.aspx">我的账户</a></li>
    </ul>
</div>

<script language="javascript" type="text/javascript">
 
function gotoChangePWD()
{
    var href="/Secure/ChangePWD.aspx";
    window.open(href,'_blank','width=300,height=160,scrollbars=no,toolbar=no, menubar=no, scrollbars=no, resizable=yes,location=no, status=no');
}
</script>

