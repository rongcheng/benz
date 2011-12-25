<%@ Page Language="C#" MasterPageFile="~/MPages/SubPage.Master" AutoEventWireup="true"
    Codebehind="Orders_Detail_List.aspx.cs" Inherits="WebUI.Modules.Orders.Orders_Detail_List"
    Title="订单明细" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>
        【<asp:Literal ID="lOrderId" runat="server"></asp:Literal>】订单明细</h3>
    <table align="center" style="width: 800px;">
        <tr>
            <td align="right">
                <asp:HyperLink ID="hlOrderList" runat="server" NavigateUrl="~/Modules/OrdersPA/Orders_List.aspx">返回订单列表</asp:HyperLink></td>
        </tr>
        <tr>
            <td align="center">
                <asp:GridView ID="gvOrders_Detail" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    Width="100%" OnPageIndexChanging="gvOrders_Detail_PageIndexChanging" EmptyDataText="对不起，没有查询到对应的订单明细">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Literal ID="Literal1" runat="server" Text="<%# gvOrders_Detail.Rows.Count + 1 %>"></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="giftid" HeaderText="礼品编号" />
                        <asp:BoundField DataField="gifttitle" HeaderText="礼品标题" />
                        <asp:TemplateField HeaderText="数量">
                            <ItemTemplate>
                                <asp:TextBox ID="txtGiftCount" runat="server" Text='<%# Bind("giftcount") %>' Width="40px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="用途">
                            <ItemTemplate>
                                <asp:TextBox ID="txtUsage" runat="server" Text='<%# Bind("remark") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="样图">
                            <ItemTemplate>
                                <asp:Image ID="imgGift" runat="server" Height="80px" ImageUrl='<%# bind("giftid") %>'
                                    OnDataBinding="imgGift_DataBinding" Width="80px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# bind("id") %>' OnClick="lnkEdit_Click">更新</asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# bind("id") %>'
                                    OnClick="lnkDelete_Click" Visible="false">删除</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="color: Red;">
                <asp:Literal ID="lErrorInfo" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
</asp:Content>
