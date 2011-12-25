<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_MainPage.Master" AutoEventWireup="true"
    CodeBehind="Feature.aspx.cs" Inherits="WebUI.Feature" %>

<%@ Register Src="/UserControls/Data_List.ascx" TagName="FeatureList" TagPrefix="uc1" %>
<%@ Register Src="/UserControls/SubCatalogNavigater.ascx" TagName="Catalog" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="sub_frame_left">
    <uc2:Catalog ID="catalogTree" runat="server"></uc2:Catalog>
</div>
<div class="sub_frame_right">
    <uc1:FeatureList ID="featureList" runat="server" ListType="Feature" Width="740px"
        ShowColumnCount="1" PageIndex="1" PageSize="6"></uc1:FeatureList>
</div>
</asp:Content>
