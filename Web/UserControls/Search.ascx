<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Search.ascx.cs" Inherits="WebUI.UserControls.Search" %>
<div id="header">
    <h1><%=WebUI.UIBiz.CommonInfo.WebSite_Title %></h1>
        <div runat="server" id="divIsLogin" visible="false" class="userInfo">
           <b> 欢迎您，<asp:Label ID="lblLoginName" runat="server" Text="userName"></asp:Label></b>
           <ul>
            <li><a href="/Secure/ChangePWD.aspx">修改密码</a></li>
            <li><a href="/Secure/LogOut.aspx" target="_top">退出</a></li>
        </ul>
        </div>
      <div class="menu">
      <div class="search">
      <b> 图 片 库 搜 索</b><asp:Label ID="labKwords" runat="server" Text=""></asp:Label>
          ：<asp:TextBox ID="Kwords" runat="server" MaxLength="100" ToolTip="请输入中英文关键字或图片编号查询" EnableViewState="false"></asp:TextBox>
          上传时间：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
          图片分类：<asp:DropDownList ID="DropDownList1" runat="server">
          </asp:DropDownList>
          <asp:ImageButton ID="btnSearch" CssClass="btnSearch" runat="server" ImageUrl="/images/search.jpg" OnClick="btnSearch_Click" EnableViewState="false" />
      <asp:HiddenField ID="hidLastSearch" runat="server" />
          &nbsp;
      </div>
       <ul>
        <li><a href="/"  target="_top">首页</a></li>
        <li><a href="/Modules/UserProfile.aspx"  target="_top">我的账户</a></li>
        <li><a href="/downloadLog.aspx"  target="_top">我的下载</a></li>
        <li><a href="/LightBox/LightBox.aspx"  target="_top">收藏夹</a></li>
        <li><a href="/Modules/Manage/Sysmanager.aspx"  target="_top">管理</a></li>
      </ul>
    </div>
  </div>

<script type="text/javascript">
var __nonAgentMSDOMBrowser = (window.navigator.appName.toLowerCase().indexOf('explorer') == -1);
function DefaultButton(evt, target) 
{
        var evt = evt ? evt : (window.event ? window.event : null);
        var obj = event.srcElement ? event.srcElement : event.target;
        
        if (evt.keyCode == 13 && !(obj && (obj.tagName.toLowerCase() == "textbox"))) 
        {
            var defaultButton;
            if (__nonAgentMSDOMBrowser) {
                defaultButton = document.getElementById(target);
            }
            else {
                defaultButton = document.all[target];
            }
          
            if (defaultButton && typeof(defaultButton.click) != "undefined") {
                defaultButton.click();
                evt.cancelBubble = true;
                if (evt.stopPropagation) evt.stopPropagation();
                return false;
            }
        }
    return true;
}
</script>