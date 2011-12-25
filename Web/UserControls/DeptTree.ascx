<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeptTree.ascx.cs" Inherits="WebUI.UserControls.DeptTree" %>
<asp:TreeView ID="deptTreeView" runat="server"  SelectedNodeStyle-BackColor="#99ccff" ExpandDepth="1" OnSelectedNodeChanged="deptTreeView_SelectedNodeChanged"
    ShowLines="True">
</asp:TreeView>
