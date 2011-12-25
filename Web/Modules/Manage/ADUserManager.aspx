<%@ Page Language="C#" MasterPageFile="~/MPages/FuncPage.Master" AutoEventWireup="true"
    Codebehind="ADUserManager.aspx.cs" Inherits="WebUI.Modules.Manage.ADUserManager"
    Title="AD用户管理" %>

<%@ Register Src="../../UserControls/SysFunction.ascx" TagName="SysFunction" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>
        AD用户管理</h3>
    <p>
        <a href="UserManager.aspx">用户管理</a>&nbsp;<a href="ADUserManager.aspx">AD用户管理</a></p>
    <br />
    <div class="region">
        <div class="info">
            <h3>
                AD基础信息</h3>
            <table id="Table1" width="480px" runat="server">
                <tr>
                    <td>
                        域名称：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtDomainName" MaxLength="20" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        管理员ID：</td>
                    <td>
                        <asp:TextBox ID="txtAdmin" MaxLength="20" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        管理员密码：</td>
                    <td>
                        <asp:TextBox ID="txtPwd" runat="server" MaxLength="20" TextMode="password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        OU名称：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtOUName" MaxLength="20" runat="server" ToolTip="OU=ouName,OU=ouName"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        用户ID:
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserIdList" runat="server" TextMode="multiLine" Height="50px"
                            Width="280px"></asp:TextBox>
                        <br />
                        <font color="gray">(格式:userloginname 用分号分割)</font>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnCheckUser" runat="server" Text="检查有效性" OnClick="btnCheckUser_Click" />
                    </td>
                </tr>
            </table>
            <asp:GridView Width="350px" ID="userList" DataKeyNames="UserId" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="UserName" HeaderText="姓名" />
                    <asp:BoundField DataField="UserLoginName" HeaderText=" 登录Id" />
                    <asp:BoundField DataField="Email" HeaderText="邮箱" />
                </Columns>
            </asp:GridView>
            <asp:Button ID="btnAddUsers" runat="server" OnClick="btnAddUsers_Click" Text="添加用户"
                Visible="false" />
        </div>
    </div>
</asp:Content>
