<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Data_List.ascx.cs"
    Inherits="WebUI.UserControls.Data_List" %>
<%@ Register Src="Feature_Info.ascx" TagName="FeatureInfo" TagPrefix="uc1" %>
<asp:DataList ID="dlList" runat="server" RepeatDirection="Horizontal">
    <ItemTemplate>
        <uc1:FeatureInfo ID="featureInfo1" runat="server" CurrentItem="<%#Container.DataItem %>">
        </uc1:FeatureInfo>
    </ItemTemplate>
</asp:DataList>