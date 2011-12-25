<%@ Page Language="C#" MasterPageFile="~/MPages/SubPage.Master" Theme="MainSkin"
    AutoEventWireup="true" Codebehind="downRedirect.aspx.cs" Inherits="WebUI.downRedirect"
     %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:HiddenField ID="resourceType" runat="server" />
<asp:HiddenField ID="serverFileName" runat="server" />
    <table width="60%" style="margin: 25px 10px;line-height:25px;">
        <tr>
            <td colspan="2" style="height: 25px;">
                <img src="/images/wi.jpg" alt="" /><b style="font-size: 14px"> 下 载 确 认</b></td>
        </tr>
        <tr>
            <td align="right">图片编号：</td>
            <td><%=filename %></td>
        </tr>
       
        <tr>
            <td align="right">用途：</td>
            <td><asp:DropDownList ID="selectUsage" runat="server">
                </asp:DropDownList>
            </td>
        </tr>        
         <tr>
            <td align="right" valign="top">备注：</td>
            <td>
            <asp:TextBox ID="txtenduser" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                相关下载：
            </td>
            <td>
                <asp:LinkButton ID="lbDownSource" runat="server" Text="" CommandArgument="" CommandName="" OnCommand="lbDownSource_Command"  > </asp:LinkButton>         
                <asp:Repeater ID="rptDownloadList" runat="server" OnItemCommand="rptDownloadList_ItemCommand">
                <ItemTemplate>
               <br /><asp:LinkButton ID="lbDown" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FileNameFileLength") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "FileName") %>' CommandName="commandname"  >
               </asp:LinkButton>  
                </ItemTemplate>
                </asp:Repeater>
            </td>
        </tr>
        
    </table>
</asp:Content>
