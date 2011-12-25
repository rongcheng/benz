<%@ Page Language="C#" MasterPageFile="~/MPages/MainPage.Master" AutoEventWireup="true"
    Codebehind="NewsList.aspx.cs" Inherits="WebUI.Modules.NewsList" Title="信息浏览" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <cc1:TabContainer ID="infoTabs" runat="server" ActiveTabIndex="0">
        <cc1:TabPanel runat="server" HeaderText="平安新闻" ID="newsTab">
            <HeaderTemplate>
                平安新闻
            </HeaderTemplate>
            <ContentTemplate>
                <asp:GridView ID="newsList" EnableTheming="false" PagerStyle-Height="30px" PagerSettings-Mode="NumericFirstLast"
                    PagerStyle-HorizontalAlign="Right" AllowPaging="true" PageSize="20" runat="server"
                    ShowHeader="false" AutoGenerateColumns="false" GridLines="none">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <img src="/images/news_li.gif" />
                                <a href="/Modules/ShowNews.aspx?newsId=<%# Eval("newsId") %>"><%# Eval("Title") %></a>
                                <em>
                                    <%# Eval("createDate") %>
                                </em>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="内部公告" ID="bulletinTab">
            <ContentTemplate>
                <asp:GridView ID="bulletList" EnableTheming="false" PagerStyle-Height="30px" PagerSettings-Mode="NumericFirstLast"
                    PagerStyle-HorizontalAlign="Right" AllowPaging="true" PageSize="20" runat="server"
                    ShowHeader="false" AutoGenerateColumns="false" GridLines="none">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <img src="/images/news_li.gif" />
                                <a href="/Modules/ShowNews.aspx?newsId=<%# Eval("newsId") %>"><%# Eval("Title") %></a>
                                <em>
                                    <%# Eval("createDate") %>
                                </em>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>
