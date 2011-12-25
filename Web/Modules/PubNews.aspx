<%@ Page Language="C#" MasterPageFile="~/MPages/FuncPage.Master" AutoEventWireup="true"
    Codebehind="PubNews.aspx.cs" Inherits="WebUI.Modules.PubNews" Title="新闻发布" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="../UserControls/SysFunction.ascx" TagName="SysFunction" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>
        信息发布</h3>
    <table width="500">
        <tr>
            <td>
                类型:
                <asp:DropDownList ID="ddlTypeSea" runat="server">
                    <asp:ListItem Value="0">平安新闻</asp:ListItem>
                    <asp:ListItem Value="1">内部公告</asp:ListItem>
                </asp:DropDownList>
                标题:
                <asp:TextBox ID="txtTitleSea" runat="server" MaxLength="20"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
                    <ContentTemplate>
                        <asp:GridView Width="400px" PageSize="4" AllowPaging="true" EmptyDataText="没有新闻"
                            EmptyDataRowStyle-HorizontalAlign="center" ID="newsList" runat="server" AutoGenerateColumns="false"
                            OnRowCommand="newsList_RowCommand" OnPageIndexChanging="newsList_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="title" HeaderText="标题" />
                                <asp:BoundField DataField="createDate" ItemStyle-HorizontalAlign="center" DataFormatString="{0:yyyy-MM-dd}"
                                    HeaderText="发布时间 " />
                                <asp:TemplateField ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="modify" CommandArgument='<%# Eval("newsId").ToString() %>'>编辑</asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Del" CommandArgument='<%# Eval("newsId").ToString() %>'
                                            OnClientClick="return window.confirm('您确定删除吗?')">删除</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="hiSelNewsId" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="btnToAddNews" runat="server" OnClick="btnToAddNews_Click">添加信息</asp:LinkButton>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table runat="server" id="newsPanel" visible="False">
                <tr>
                    <td colspan="2"><hr /></td>
                </tr>
                <tr>
                    <td>
                        标题:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        类型:</td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server">
                            <asp:ListItem Value="0">平安新闻</asp:ListItem>
                            <asp:ListItem Value="1">内部公告</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        内容:
                    </td>
                    <td>
                        <asp:TextBox ID="txtContent" runat="server" TextMode="multiLine" Height="100px" Width="350px"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display:none">
                    <td>
                        是否有效:</td>
                    <td>
                        <asp:DropDownList ID="ddlVal" runat="server">
                            <asp:ListItem Value="1">有效</asp:ListItem>
                            <asp:ListItem Value="0">无效</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>
                        置顶:</td>
                    <td>
                        <asp:CheckBox ID="chkTop" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnPub" runat="server" OnClick="btnPub_Click" Text="发布" />
                        <asp:Button ID="btnUpdate" runat="server" Text="更新" OnClick="btnUpdate_Click" /></td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnToAddNews" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="newsList" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
