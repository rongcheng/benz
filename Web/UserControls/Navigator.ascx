<%@ Control Language="C#" AutoEventWireup="true" Codebehind="Navigator.ascx.cs" Inherits="WebUI.UserControls.Navigator" %>
<asp:Menu ID="navigatorMenu" runat="server" Orientation="Horizontal" DataSourceID="SiteMapDataSource"
    StaticDisplayLevels="2">
</asp:Menu>
<asp:SiteMapDataSource ID="SiteMapDataSource" runat="server" />
