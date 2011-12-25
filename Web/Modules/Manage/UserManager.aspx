<%@ Page Language="C#" Theme="MainSkin" MasterPageFile="~/MPages/QJ_FuncPage.Master"
    AutoEventWireup="true" Codebehind="UserManager.aspx.cs" Inherits="WebUI.Modules.Manage.UserManager" Title="用户资料" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="../../UserControls/DeptTree.ascx" TagName="DeptTree" TagPrefix="uc4" %>
<%@ Register Src="../../UserControls/SysFunction.ascx" TagName="SysFunction" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/UserInfo.ascx" TagName="UserInfo" TagPrefix="uc3" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
.table{border:0; border-collapse:collapse;}
.manage_table th{ border-bottom:#d4d0c8 solid 1px; border-right:#d4d0c8 solid 1px; margin:0; text-align:center; background-color:#666; color:#fff; font-weight:700; height:30px;}
.cell td{border-collapse:collapse; padding:3px;background-color:#fbfbfb; border:solid 1px white;  margin:0; height:24px; line-height:24px; text-align:left; overflow:hidden;}
.both td{border-collapse:collapse; padding:3px;background-color:#F3F3F3;  border:solid 1px white; margin:0; height:24px; line-height:24px; text-align:left; overflow:hidden;}
.grvpager
 {
 	text-align:left;
}
</style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="content_manage">
<h4>用户管理</h4>
<p style="visibility:hidden">
    <a href="UserManager.aspx">用户管理</a>&nbsp;<a href="ADUserManager.aspx">AD用户管理</a>
</p>

 <div class="content_manage_pannel">
 登录名：
                    <asp:TextBox ID="txtSeaLoginName" runat="server" CssClass="txt" ></asp:TextBox>
                    姓名：
                    <asp:TextBox ID="txtSeaName" runat="server"  CssClass="txt"></asp:TextBox>
                    <asp:DropDownList ID="groupList" runat="server" Visible="false">
                    </asp:DropDownList>
                    状态：<asp:DropDownList ID="Status" runat="server">
                    <asp:ListItem Value="0">有效</asp:ListItem>
                    <asp:ListItem Value="1">无效</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnSearch" runat="server" Text="搜索" CausesValidation="False" OnClick="btnSearch_Click" />
</div> 


 <asp:GridView EmptyDataText="没有要搜索的用户"  AllowPaging="True" ID="userList" 
        DataKeyNames="UserId" runat="server" 
                    AutoGenerateColumns="False" OnSelectedIndexChanging="userList_SelectedIndexChanging"
                    OnRowDeleting="userList_RowDeleting" Width="100%" OnRowDataBound="userList_RowDataBound" 
                                    CssClass="table" BorderWidth="0"
        OnPageIndexChanging="userList_PageIndexChanging" GridLines="None" >
        <HeaderStyle HorizontalAlign="Center" BackColor="#666666" BorderColor="#D4D0C8" 
                BorderStyle="Solid" BorderWidth="1px" ForeColor="#fffFFF" Width="200px" Height="30px"></HeaderStyle>
    <RowStyle BorderColor="White" 
                BorderStyle="Solid" BorderWidth="1px" CssClass="both" />
    <AlternatingRowStyle BorderColor="White" 
                BorderStyle="Solid" BorderWidth="1px" CssClass="cell" />
        
                    <Columns>
                        <asp:BoundField DataField="userName" HeaderText="姓名" >
                            <ItemStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="LoginName" HeaderText="登录名" >
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Tel" HeaderText="电话" >
                            <ItemStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Email" HeaderText="邮箱" >
                            <ItemStyle Width="120px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="状态">
                            <ItemTemplate>
                                <asp:Label ID="labStatus" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="60px" />
                        </asp:TemplateField>
                        <asp:CommandField SelectText="编辑" ShowSelectButton="True" >
                            <ItemStyle Width="60px" />
                        </asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True" DeleteText="&lt;span onclick='return window.confirm(&quot;用户将被删除，确定删除吗？&quot;)'&gt;删除&lt;/span&gt;">
                            
                        </asp:CommandField>
                    </Columns>
    
                    <PagerStyle BorderStyle="None" CssClass="grvpager" />
    
                    <AlternatingRowStyle BackColor="White"  />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                </asp:GridView>
                
         </div>
         <div style="clear:both"></div>
         <div style="margin-left:38px;text-align:left">  


       <p class="save" style="margin-top:10px" >
        <asp:Button CausesValidation="False" ID="btnNewUser" runat="server" Text="新建用户" OnClick="btnNewUser_Click" />
    </p>
    <div runat="server" visible="false" id="userOtherInfo"  style="margin-top:10px">
        <div class="region">
            <h3>
                用户基本信息</h3>
            <div class="info">
                <uc3:UserInfo ID="UserInfo" runat="server" ></uc3:UserInfo>
            </div>
            <div class="info">
                <h3>
                    所属机构</h3>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td>
                                    机构名称：</td>
                                <td>
                                    <asp:Label ID="lbDeptName" Font-Bold="true" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td valign="top" align="left">
                                    <div style="overflow-y: scroll; height: 150px; width: 300px; border-right: thin inset;
                                        border-top: thin inset; border-left: thin inset; border-bottom: thin inset;">
                                        <uc4:DeptTree ExpandDepth="2" ID="deptTree" runat="server" />
                                    </div>
                                    <asp:HiddenField ID="hiSelDeptId" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <p class="save">
                <asp:Button Visible="false" ID="btnSaveUserInfo" runat="server" Text="保存信息" OnClick="btnSaveUserInfo_Click"
                    ValidationGroup="ValidationGroup_UserInfo" />
            </p>
        </div>
        <div class="region">
            <table width="100%" runat="server" id="resetPwdPan" visible="false">
                <tr>
                    <td colspan="2">
                        <h3>
                            重置密码</h3>
                    </td>
                </tr>
                <tr>
                    <td width="100px">
                        新密码：
                    </td>
                    <td>
                        <asp:TextBox ID="txtNewPwd" TextMode="Password" MaxLength="10" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNewPwd"
                            ValidationGroup="validationGroup_ResetPwd" Text="必填"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNewPwd"
                            ValidationExpression="^(\w){3,10}$" ValidationGroup="validationGroup_ResetPwd"
                            Text="密码只能输入3-10个字母、数字、下划线"></asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <td>
                        重复新密码：
                    </td>
                    <td>
                        <asp:TextBox ID="txtReNewPwd" TextMode="Password" MaxLength="10" runat="server"></asp:TextBox><asp:CompareValidator
                            ID="CompareValidator1" runat="server" ErrorMessage="" Text="密码不一致" ControlToValidate="txtReNewPwd"
                            ControlToCompare="txtNewPwd" ValidationGroup="validationGroup_ResetPwd"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <p class="save">
                            <asp:Button ID="btnResetPwd" Text="重置密码" OnClientClick="return window.confirm('您确定重置吗？')"
                                runat="server" OnClick="btnResetPwd_Click" ValidationGroup="validationGroup_ResetPwd" />
                        </p>
                    </td>
                </tr>
            </table>
        </div>
        <div class="region">
            <table width="100%" runat="server" id="roleSetPan" visible="false">
                <tr>
                    <td colspan="2">
                        <h3>
                            用户角色</h3>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="noBg">
                        <asp:GridView ID="roleList" Width="200px" DataKeyNames="RoleId" runat="server" AutoGenerateColumns="false"
                            OnRowDataBound="roleList_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRole" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RoleName" HeaderText="角色名" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <p class="save">
                            <asp:Button ID="btnSetRole" runat="server" Text="设置用户角色" OnClick="btnSetRole_Click" />
                        </p>
                    </td>
                </tr>
            </table>
            <p class="addNewUser">
                <asp:Button Visible="false" ID="btnSaveNewUser" runat="server" Text="建立用户" OnClick="btnSaveNewUser_Click"
                    ValidationGroup="ValidationGroup_UserInfo" />
            </p>
        </div>
    </div>
    </div>
    <asp:HiddenField ID="hiUserId" runat="server" />
</asp:Content>
