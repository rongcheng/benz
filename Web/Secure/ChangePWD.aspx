<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ChangePWD.aspx.cs" Inherits="WebUI.Secure.ChangePWD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=WebUI.UIBiz.CommonInfo.WebSite_Title %></title>
    <style type="text/css">
*{ padding:0; margin:0;}
body{ font-size:12px;}
.title{ font-size:14px;}
.changePWD{ margin:25px 10px;}
</style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="changePWD" width="100%">
            <tr>
                <td colspan="2" style="height: 25px;">
                    <img src="/images/wi.jpg" alt="" /><b class="title"> 修 改 密 码</b></span></td>
            </tr>
            <tr>
                <td align="right">
                    旧密码：
                </td>
                <td>
                     <asp:TextBox ID="txtOldPwd" runat="server" CssClass="inputstyle" MaxLength="10" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">新密码：</asp:Label></td>
                <td>
                    <asp:TextBox ID="NewPassword" runat="server" MaxLength="10" CssClass="inputstyle" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                        ErrorMessage="请输入新密码" ToolTip="请输入新密码" ValidationGroup="ctl00$ChangePWD">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="NewPassword"
                        ErrorMessage="*" ValidationExpression="^(\w){3,10}$" ValidationGroup="ctl00$ChangePWD"
                        Display="Dynamic" Font-Size="Small">只能输入3-10个字母、数字、下划线</asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">确认新密码：</asp:Label></td>
                <td>
                    <asp:TextBox ID="ConfirmNewPassword" runat="server" MaxLength="10" CssClass="inputstyle" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword"
                        ErrorMessage="请输入确认密码" ToolTip="请输入确认密码" ValidationGroup="ctl00$ChangePWD">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword"
                        ControlToValidate="ConfirmNewPassword" ErrorMessage="密码与确认密码不匹配" ValidationGroup="ctl00$ChangePWD"
                        Display="Dynamic" Font-Size="Small"></asp:CompareValidator></td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left">
                    <asp:LinkButton ID="ChangePasswordImageButton" runat="server" AlternateText="修改密码"
                        CommandName="ChangePassword" ValidationGroup="ctl00$ChangePWD" ForeColor="Gray"
                        OnClick="ChangePasswordImageButton_Click">确定</asp:LinkButton>
                    &nbsp;
                    <asp:LinkButton ID="CancelImageButton" runat="server" AlternateText="取消" CausesValidation="False"
                        CommandName="Cancel" ForeColor="Gray"  OnClientClick="window.close();" Width="63px">取消</asp:LinkButton>
                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label></td>
            </tr>
        </table>
        <asp:HiddenField ID="hiUserId" runat="server" />
    </form>
</body>
</html>
