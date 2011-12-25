<%@ Page Language="C#" MasterPageFile="~/MPages/SubPage.Master" AutoEventWireup="true"
    Codebehind="Orders_List.aspx.cs" Inherits="WebUI.Modules.Orders.Orders_List" 
    Title="我的订单" %>

<%@ Register Src="~/UserControls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 800px;" align="center">
        <tr>
            <td align="center">
                订单号：<asp:TextBox ID="txtOrderId" runat="server"></asp:TextBox>
                订单时间：<uc1:AjaxCalendar ID="txtStartDate" runat="server" />
                --
                <uc1:AjaxCalendar ID="txtEndDate" runat="server" />
                状态：<asp:DropDownList ID="ddlState" runat="server">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">未提交</asp:ListItem>
                    <asp:ListItem Value="1">已提交</asp:ListItem>
                    <asp:ListItem Value="2">已完成</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" />
                &nbsp;&nbsp; <a href="/Modules/Manage/Sysmanager.aspx" runat="server" id="lk_back" visible="false" style="font-size:larger" >返回管理页</a>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:GridView ID="gvOrdersList" runat="server" Width="100%" AllowPaging="True" 
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Literal ID="Literal1" runat="server" Text="<%# gvOrdersList.Rows.Count + 1 %>"></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="orderid" HeaderText="订单号" />
                        <asp:BoundField DataField="giftTypeCount" HeaderText="礼品种类" />
                        <asp:BoundField DataField="giftSumCount" HeaderText="总数量" />
                        <asp:BoundField DataField="createdate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="创建时间" />
                        <asp:TemplateField HeaderText="申请人">
                            <ItemTemplate>
                                <asp:Label ID="lblUser" runat="server" OnDataBinding="lblUser_DataBinding" Text='<%# Bind("userid") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="状态">
                            <ItemTemplate>
                                <asp:Label ID="lblState" runat="server" OnDataBinding="lblState_DataBinding" Text='<%# Bind("state") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlInfo" Target="_blank" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"orderid","Orders_Info.aspx?orderid={0}") %>'>查看</asp:HyperLink>
                                <asp:HyperLink ID="hlEdit" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"orderid","Orders_Detail_List.aspx?orderid={0}") %>'
                                    Visible='<%# (Convert.ToInt32(Eval("state"))==0 && Eval("UserId").ToString()==CurrentUser.UserId.ToString())?true:false%>'>编辑</asp:HyperLink>
                                <asp:LinkButton ID="lnkSubmit" runat="server" CommandArgument='<%# bind("orderid") %>'
                                    OnClick="lnkSubmit_Click" OnClientClick="return confirm('确认提交么？');" Visible='<%# (Eval("UserId").ToString()==CurrentUser.UserId.ToString() && Convert.ToInt32(Eval("state"))==0)?true:false%>'>提交</asp:LinkButton>
                                <asp:LinkButton ID="lnkBack" runat="server" CommandArgument='<%# bind("orderid") %>'
                                    OnClick="lnkBack_Click" OnClientClick="return confirm('确认退回么？');" Visible='<%# (IsSuperAdmin && Convert.ToInt32(Eval("state"))==1)?true:false%>'>退回</asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# bind("orderid") %>'
                                    OnClick="lnkDelete_Click" OnClientClick="return confirm('确认删除么？');" Visible='<%# (IsSuperAdmin || (Convert.ToInt32(Eval("state"))==0 && Eval("UserId").ToString()==CurrentUser.UserId.ToString()))?true:false%>'>删除</asp:LinkButton>
                                <asp:LinkButton ID="lnkOver" runat="server" CommandArgument='<%# bind("orderid") %>'
                                    OnClick="lnkOver_Click" Visible='<%# (IsSuperAdmin && Convert.ToInt32(Eval("state"))==1)?true:false%>'>通过</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="color: Red;">
                <asp:Literal ID="lErrorInfo" runat="server"></asp:Literal></td>
        </tr>
    </table>
</asp:Content>
