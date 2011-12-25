<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_FuncPage.Master" Theme="MainSkin"
    AutoEventWireup="true" Codebehind="CatalogManager.aspx.cs" Inherits="WebUI.Modules.Manage.CatalogManager"
    Title="分类管理" %>

<%@ Register Src="~/UserControls/CatalogTree.ascx" TagName="CatalogTree" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="content_manage" style="border:0px solid red">
<h4>分类管理</h4>   </div><br />&nbsp;<br />
    <table width="550px" cellspacing="0" cellpadding="0" style="margin-left:40px; margin-top:5px;">
        <tr>
            <td valign="top" width="120px" height="100%">
                <div style="float: left; overflow-y: auto; height: 500px; width: 220px; border: 1px #C8C8C8 solid;">
                    <uc1:CatalogTree ID="catalogTree" runat="server"></uc1:CatalogTree>
                </div>
            </td>
            <td valign="top">
            <style type="text/css">
            .back_holder h3,.back_holder h5
            {
                font-size:12px;
                
                
                }
            </style>
                <div class="back_holder" style="color:#666; margin-left:8px;">
                    <table class="info" width="100%" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <h3>
                                    当前类别：<asp:Label ID="labCurrentCataName" Font-Bold="true" runat="server" Text=""></asp:Label></h3>
                                <br />
                            </td>
                        </tr>
                    </table>
                    <table class="info" width="100%" cellspacing="0" cellpadding="0" runat="server" id="cataInfo"
                        visible="false">
                        <tr>
                            <td>
                                分类名称：
                                <asp:TextBox ID="txtNowCataName" runat="server" MaxLength="25"></asp:TextBox>
                                排序号：
                                <asp:TextBox ID="txtOrder" runat="server" MaxLength="3" Width="46px"></asp:TextBox>
                                <asp:Button ID="btnModify" runat="server" Text="修改" OnClick="btnModify_Click" EnableViewState="False" />
                                &nbsp;
                                <asp:Button ID="btnDelCata" Visible="false" runat="server" Text="删除当前分类" OnClick="btnDelCata_Click" />
                                <br />
                                子分类名称：
                                <asp:TextBox ID="txtChildCataName" runat="server" MaxLength="25"></asp:TextBox>
                                <asp:Button ID="btnAddChildCata" runat="server" Text="添加子分类" OnClick="btnAddChildCata_Click"
                                    EnableViewState="False" />
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table class="info" width="100%" cellspacing="0" cellpadding="0" runat="server" id="roleList"
                        visible="false">
                        <tr>
                            <td>
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h3>
                                    权限分配 用户类型：<asp:DropDownList ID="groupDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="groupDDL_SelectedIndexChanged"
                                        Visible="False" /></h3>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h5>
                                    当前分类设置用户角色权限</h5>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView EnableViewState="true" ID="roleGroupList" DataKeyNames="roleId" runat="server"
                                    AutoGenerateColumns="false" OnRowEditing="roleGroupList_RowEditing" Width="400px">
                                    <Columns>
                                        <asp:BoundField ReadOnly="true" DataField="roleName" HeaderText="用户角色" ItemStyle-Width="80px" />
                                        <asp:TemplateField HeaderText="禁止图片浏览" ItemStyle-Width="80px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="funReadChk" runat="server" Checked='<%#Eval("6") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="图片上传" ItemStyle-Width="80px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="funUpChk" runat="server" Checked='<%#Eval("1") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="图片下载" ItemStyle-Width="80px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="funDownChk" runat="server" Checked='<%#Eval("5") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="图片编辑" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="funEditChk" runat="server" Checked='<%#Eval("2") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnSetRoleFun" runat="server" Text="设置" OnClick="btnSetRoleFun_Click"
                                    EnableViewState="False" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <h5>
                                    当前分类设置用户权限</h5>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                登录名：<asp:TextBox ID="txtloginName" Width="90px" runat="server"></asp:TextBox>
                                姓名：<asp:TextBox ID="txtUserName" Width="90px" runat="server"></asp:TextBox>
                                <asp:Button ID="btnSearchUser" runat="server" Text="搜索" OnClick="btnSearchUser_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="userList" Width="400px" EmptyDataText="没有搜索到用户" EmptyDataRowStyle-HorizontalAlign="center"
                                    DataKeyNames="userId" runat="server" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="loginName" HeaderText="登录名" />
                                        <asp:BoundField DataField="userName" HeaderText="姓名" />
                                        <asp:TemplateField HeaderText="禁止图片浏览" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="funReadChk" runat="server" Checked='<%#Eval("6") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="图片上传" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="funUpChk" runat="server" Checked='<%#Eval("1") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="图片下载" ItemStyle-Width="80px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="funDownChk" runat="server" Checked='<%#Eval("5") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="图片编辑" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="funEditChk" runat="server" Checked='<%#Eval("2") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnSetUserFun" runat="server" Text="设置" OnClick="btnSetUserFun_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hiCurrentCataId" runat="server" />
</asp:Content>
