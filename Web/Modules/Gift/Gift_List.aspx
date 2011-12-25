<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_FuncPage.Master" AutoEventWireup="true"
    Codebehind="Gift_List.aspx.cs" Inherits="WebUI.Modules.Gift.Gift_List" Title="礼品管理" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>
        礼品管理</h3>
    <table style="width: 90%;" align="center">
        <tr>
            <td align="center">
                礼品标题：<asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                礼品类型：<asp:DropDownList ID="ddlGiftType" runat="server">
                </asp:DropDownList>
                <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <a href="Gift_Edit.aspx">新建礼品</a>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:GridView ID="gvGiftList" runat="server" AllowPaging="True" AutoGenerateColumns="False" Width="100%" OnPageIndexChanging="gvGiftList_PageIndexChanging" EmptyDataText="对不起，没有查询到符合条件的记录">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="编号" />
                        <asp:BoundField DataField="Title" HeaderText="标题" />
                        <asp:BoundField DataField="typename" HeaderText="类型" />
                        <asp:BoundField DataField="Quantity" HeaderText="库存数量（个）" />
                        <asp:BoundField DataField="createtime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="创建时间" />
                        <asp:TemplateField HeaderText="样图">
                            <ItemTemplate>
                                <asp:Image ID="imgGift" runat="server" Height="80px" ImageUrl='<%# bind("imageid") %>'
                                    OnDataBinding="imgGift_DataBinding" Width="80px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlEdit" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"id","Gift_Edit.aspx?id={0}") %>'>编辑</asp:HyperLink>&nbsp;
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# bind("id") %>' OnClick="lnkDelete_Click" OnClientClick='return confirm("确定删除该礼品么？");'>删除</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
