<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_FuncPage.Master" Theme="MainSkin" AutoEventWireup="true"
    Codebehind="TJInfo.aspx.cs" Inherits="WebUI.Modules.Manage.TJInfo" Title="统计管理" %>

<%@ Register Src="../../UserControls/SysFunction.ascx" TagName="SysFunction" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



<div class="content_manage">
<h4>统计管理</h4>

<div style="clear:both"   ></div>
<div style="margin-left:0px;margin-top:10px">


<table width="550px" border=0>
<tr>

<td  valign="top">
<h3>图片分类统计</h3>

<asp:GridView ID="cataStat" runat="server" EmptyDataText="没有统计信息" AutoGenerateColumns="False" Width="330px" >
<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<RowStyle BackColor="#EFF3FB" />
<Columns>
    <asp:BoundField DataField="CatalogName" HeaderText="分类名称" />
    <asp:BoundField DataField="count" HeaderText="资源数量" />
</Columns>
</asp:GridView>
</td>
<td width="50%" valign="top">
<h3>角色统计</h3>
<div style="display:none">用户类型：</div><asp:DropDownList ID="ddlUserType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged" Visible="false">
                    </asp:DropDownList>
                    <asp:GridView ID="statList" EmptyDataText="没有统计信息" runat="server" Width="200px" AutoGenerateColumns="False"
                        CellPadding="4">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:BoundField DataField="RoleName" HeaderText="角色组" />
                            <asp:BoundField DataField="amount" HeaderText="人员数量" />
                        </Columns>
                    </asp:GridView>
</td>
</tr>
</table>
     
    
    </div>
</asp:Content>
