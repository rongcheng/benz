<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CatalogTree.ascx.cs" Inherits="WebUI.UserControls.CatalogTree" %>
<asp:TreeView ID="cataTreeView" runat="server" ShowLines="True" OnSelectedNodeChanged="cataTreeView_SelectedNodeChanged">
    <SelectedNodeStyle BackColor="#C0C0FF" />
</asp:TreeView>
