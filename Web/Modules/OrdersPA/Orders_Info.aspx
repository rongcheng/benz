<%@ Page Language="C#" MasterPageFile="~/MPages/SubPage.Master" AutoEventWireup="true"
    Codebehind="Orders_Info.aspx.cs" Inherits="WebUI.Modules.Orders.Orders_Info"
    Title="订单信息" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table align="center" style="width: 800px;" cellpadding="3" cellspacing="3" >
        <tr>
            <th align="center" colspan="2">
                <h3>
                    订单信息</h3>
            </th>
        </tr>
        <tr>
            <td colspan="2">
                <hr style="width:100%" />
            </td>
        </tr>
        <tr>
            <td>
                订单编号：
                <asp:Literal ID="lOrderId" runat="server"></asp:Literal>                
            </td>            
        </tr>
        <tr>
            <td>
                申请人  ：
                <asp:Literal ID="lUserName" runat="server"></asp:Literal>
             </td>
            <td>
                收货人:
                 <asp:Literal ID="Contactor" runat="server"></asp:Literal>              
            </td>         
        </tr>
        <tr>
            <td>
                申请时间：
                <asp:Literal ID="lCreateTime" runat="server"></asp:Literal>
            </td>
            <td>
                联系电话:
                 <asp:Literal ID="tel" runat="server"></asp:Literal>
            </td>   
        </tr>
        <tr>
            <td>
                订单状态：
                <asp:Literal ID="lState" runat="server"></asp:Literal>
            </td>
            <td>
                邮箱:
                      <asp:Literal ID="email" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>寄送地址：
                <asp:Literal ID="Address" runat="server"  ></asp:Literal>
            </td>
            <td>
                机构:
                 <asp:Literal ID="GroupName" runat="server"></asp:Literal>
            </td>
            
        </tr>
        <tr>
            <td>
                礼品清单</td>
        </tr>        
        <tr>
            <td align="center" colspan="2" >
                <asp:GridView ID="gvOrders_Detail" runat="server" AutoGenerateColumns="False" Width="100%"
                    EmptyDataText="对不起，没有查询到对应的订单明细">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Literal ID="Literal1" runat="server" Text="<%# gvOrders_Detail.Rows.Count + 1 %>"></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="giftid" HeaderText="礼品编号" />
                        <asp:BoundField DataField="gifttitle" HeaderText="礼品标题" />
                        <asp:BoundField DataField="giftcount" HeaderText="数量" />
                        <asp:BoundField DataField="usage" HeaderText="用途" />
                        <asp:TemplateField HeaderText="样图">
                            <ItemTemplate>
                                <asp:Image ID="imgGift" runat="server" Height="80px" ImageUrl='<%# bind("giftid") %>'
                                    OnDataBinding="imgGift_DataBinding" Width="80px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
