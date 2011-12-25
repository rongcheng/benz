<%@ Control Language="C#" AutoEventWireup="true" Codebehind="UserInfo.ascx.cs" Inherits="WebUI.UserControls.UserInfo" %>
<table width="100%" cellspacing="0" cellpadding="0">
    <tr>
        <td width="100px">
            登录名：</td>
        <td>
            <asp:TextBox ID="txtLoginName" runat="server" MaxLength="20"></asp:TextBox>(AD账号，没有域前缀)
            <asp:RequiredFieldValidator ID="ReqToLoginName" runat="server" ControlToValidate="txtLoginName"
                ErrorMessage="登录名必填" SetFocusOnError="True" ValidationGroup="ValidationGroup_UserInfo">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtLoginName"
                ValidationExpression="^[a-zA-Z]{1}([a-zA-Z0-9]|[._]){1,19}$" ErrorMessage="登录名只能输入2-20个以字母开头、可带数字、“_”、“.”的字符串"
                ValidationGroup="ValidationGroup_UserInfo">*</asp:RegularExpressionValidator></td>
    </tr>
    <tr>
        <td>
            姓名：</td>
        <td>
            <asp:TextBox ID="txtUserName" runat="server" MaxLength="10"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ReqToUserName" runat="server" ControlToValidate="txtUserName"
                ErrorMessage="姓名必填" SetFocusOnError="True" ValidationGroup="ValidationGroup_UserInfo">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr runat="server" id="pwdPan">
        <td>
            密码：</td>
        <td>
            <asp:TextBox TextMode="password" ID="txtPwd" runat="server" MaxLength="10"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ReqToPwd" runat="server" ControlToValidate="txtPwd"
                ErrorMessage="密码必填" SetFocusOnError="True" ValidationGroup="ValidationGroup_UserInfo">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ValidationExpression="^(\w){3,10}$"
                ErrorMessage="密码只能输入3-10个字母、数字、下划线" ControlToValidate="txtPwd" ValidationGroup="ValidationGroup_UserInfo">*</asp:RegularExpressionValidator></td>
    </tr>
    <tr runat="server" id="repwdPan">
        <td style="height: 26px">
            重复密码：</td>
        <td style="height: 26px">
            <asp:TextBox ID="txtRePwd" TextMode="password" runat="server" MaxLength="10"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ReqToRePwd" runat="server" ControlToValidate="txtRePwd"
                ErrorMessage="重复密码必填" SetFocusOnError="True" ValidationGroup="ValidationGroup_UserInfo">*</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPwd"
                ControlToValidate="txtRePwd" ErrorMessage="两次密码不一致" ValidationGroup="ValidationGroup_UserInfo">*</asp:CompareValidator></td>
    </tr>
    <tr>
        <td style="height: 24px">
            电话：</td>
        <td style="height: 24px">
            <asp:TextBox ID="txtTel" runat="server" MaxLength="25"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtTel"
                ValidationExpression="(^[0-9]{3,4}\-[0-9]{3,8}$|^[0-9]{3,8}$|^0{0,1}13[0-9]{9}$|^0{0,1}15[0-9]{9}$)"
                ErrorMessage="电话格式不正确(若加区号请参照：010-99998888)" ValidationGroup="ValidationGroup_UserInfo">*</asp:RegularExpressionValidator></td>
    </tr>
    <tr>
        <td style="height: 24px">
            邮件：</td>
        <td>
            <asp:TextBox ID="txtEmail" runat="server" MaxLength="100"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="邮件格式不正确"
                ValidationGroup="ValidationGroup_UserInfo">*</asp:RegularExpressionValidator></td>
    </tr>
   <%-- <tr>
        <td style="height: 24px">
            下载权限：
        </td>
        <td class="noBg" >
            <asp:RadioButton ID="radDownTrue" Checked="true" runat="server" Text="有" TextAlign="right"
                GroupName="UserDownVal" />
            <asp:RadioButton ID="radDownFalse" runat="server" Text="无" TextAlign="right" GroupName="UserDownVal" />
        </td>
    </tr>--%>
    <tr>
        <td style="height: 24px">
            IP登录绑定：
        </td>
        <td class="noBg">
            <asp:RadioButton ID="RadioIPTrue" runat="server" Text="是" TextAlign="right" GroupName="UserIPVal" />
            <asp:RadioButton ID="RadioIPFalse" runat="server" Checked="true" Text="否" TextAlign="right"
                GroupName="UserIPVal" />
        </td>
    </tr>
    <tr>
        <td style="height: 24px">
            是否有效：
        </td>
        <td class="noBg">
            <asp:RadioButton ID="radTrue" Checked="true" runat="server" Text="有效" TextAlign="right"
                GroupName="UserVal" />
            <asp:RadioButton ID="radFalse" runat="server" Text="无效" TextAlign="right" GroupName="UserVal" />
        </td>
    </tr>
    
</table>
