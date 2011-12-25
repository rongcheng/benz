<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_FuncPage.Master" AutoEventWireup="true"
    Codebehind="DeptManager.aspx.cs" Inherits="WebUI.Modules.Manage.DeptManager"
    Title="机构管理" %>

<%@ Register Src="../../UserControls/DeptTree.ascx" TagName="DeptTree" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <div class="content_manage" style="border:0px solid red; margin-bottom:20px;">
<h4>机构管理</h4>   </div><br />&nbsp;<br />
    <table width="500" cellspacing="0" cellpadding="0" style="margin-left:10px;">
        <tr>
            <td valign="top" width="120" height="100%">
                <div style="float: left; overflow-y: auto; height: 500px; width: 220px; border: 1px #C8C8C8 solid;">
                    <uc1:DeptTree ID="DeptTree" runat="server"></uc1:DeptTree>
                </div>
            </td>
            <td valign="top">
                <div class="back_holder">
                    <table class="info" width="100%" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <h3>
                                    当前机构：<asp:Label ID="labCurDeptName" Font-Bold="true" runat="server"></asp:Label></h3>
                                <br />
                            </td>
                        </tr>
                    </table>
                    <table class="info" width="100%" cellspacing="0" cellpadding="0" runat="server" id="deptInfo"
                        visible="false">
                        <tr>
                            <td>
                                机构名称：
                                <asp:TextBox ID="txtCurDeptName" runat="server" MaxLength="25"></asp:TextBox>
                                排序：
                                <asp:TextBox ID="txtOrderFlag" Width="40px" MaxLength="3" style="text-align:center" ToolTip="0-999" runat="server"></asp:TextBox>
                                <asp:Button ID="btnModify" runat="server" Text="修改" OnClick="btnModify_Click" />
                                <asp:Button ID="btnDelCata" runat="server" OnClientClick="return window.confirm('确认删除吗?')"
                                    Text="删除" OnClick="btnDelCata_Click" />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                子级机构名称：
                                <asp:TextBox ID="txtChildDeptName" runat="server" MaxLength="25"></asp:TextBox>
                                <asp:Button ID="btnAddChildDept" runat="server" Text="添加机构" EnableViewState="False"
                                    OnClick="btnAddChildDept_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hiSelGroupId" runat="server" />
</asp:Content>
