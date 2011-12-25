<%@ Page Language="C#" MasterPageFile="~/MPages/MainPage.Master" AutoEventWireup="true"
    Codebehind="ShowNews.aspx.cs" Inherits="WebUI.Modules.ShowNews" Title="新闻" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="news_holder">
        <h4>
            <asp:Literal ID="lbTitle" runat="server"></asp:Literal>
        </h4>
        <div class="news_info">
            时间：<asp:Literal ID="lbPubDate" runat="server"></asp:Literal>
            编辑：<asp:Literal ID="lbPubUser" runat="server"></asp:Literal>
        </div>
        <p>
            <asp:Literal ID="lbContent" runat="server"></asp:Literal></p>
    </div>
</asp:Content>
