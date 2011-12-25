<%@ Control Language="C#" AutoEventWireup="true" Codebehind="UserLogin.ascx.cs" Inherits="WebUI.UserControls.UserLogin" %>
<div id="bgBox" class="logInBox1" >
    <div class="inputBox" onkeydown="javascript:return Set_DefaultButton(event,'<%=this.btnLogin.ClientID %>')">
        <table width="100%" border="0" cellpadding="0" cellspacing="7">
            <tr>
                <td align="right" height="22" valign="middle">
                    用户名:</td>
                <td>
                    <asp:TextBox ID="txtloginName" CssClass="searchBox" MaxLength="20" Width="120px"
                        runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqLogin" runat="server"
                            ErrorMessage="*" Display="Static" ControlToValidate="txtloginName"></asp:RequiredFieldValidator></td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="21%" height="22" align="right" valign="middle">
                    密码:</td>
                <td width="57%">
                    <asp:TextBox ID="txtPassword" CssClass="searchBox" runat="server" Width="120px" MaxLength="20"
                        TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqPwd" runat="server" ErrorMessage="*" Display="Static"
                        ControlToValidate="txtPassword"></asp:RequiredFieldValidator></td>
                <td width="22%">
                    <input class="searchBox" id="btnLogin" style="width: 50px; height: 20px; background: url(/images/pingan_clickBtn_bg.gif) top left repeat-x;"
                        runat="server" type="button" value="登&nbsp;录" onserverclick="btnLogin_ServerClick" />
                </td>
            </tr>
        </table>
    </div>
    <div class="showBox" id="showBox">
        <asp:Label ID="Label2" runat="server" ForeColor="Red" Width="124px"></asp:Label>
    </div>
</div>
 