<%@ Control Language="C#" AutoEventWireup="true" Codebehind="UsageManage.ascx.cs"
    Inherits="WebUI.UserControls.UsageManage" EnableViewState="false" %>
<table width="100%" >
    <tr>
        <td>
            <asp:Button ID="btn_ShowAddUsage" runat="server" Text="新建用途" OnClick="btn_ShowAddUsage_ServerClick" />
        </td>
    </tr>
    <tr>
        <td valign="top">
            <asp:GridView ID="gv_Usage" Width="100%" DataKeyNames="UsageID" AutoGenerateColumns="False"
                AllowPaging="false" runat="server" OnRowCancelingEdit="gv_Usage_RowCancelingEdit"
                OnRowEditing="gv_Usage_RowEditing" OnRowUpdating="gv_Usage_RowUpdating" OnRowDeleting="gv_Usage_RowDeleting"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="Dotted" BorderWidth="1px" 
        CellPadding="4" ForeColor="Black">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <b>用途</b>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "UsageName").ToString().Trim() %>'
                                runat="server" ID="col1" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            &nbsp;<asp:TextBox Text='<%# DataBinder.Eval(Container.DataItem, "UsageName").ToString().Trim() %>'
                                runat="server" ID="UsageName" Width="90%" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <b>简介</b>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "UsageDesc").ToString().Trim() %>'
                                runat="server" ID="col2" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            &nbsp;<asp:TextBox Text='<%# DataBinder.Eval(Container.DataItem, "UsageDesc").ToString().Trim() %>'
                                runat="server" ID="UsageDesc" Width="90%" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" />
                    <asp:CommandField ShowDeleteButton="True" DeleteText="&lt;span onclick=&quot;return window.confirm('您确定删除吗？')&quot;&gt;删除&lt;/span&gt;" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lb_Info" runat="server" Text="" ForeColor="red"></asp:Label>
        </td>
    </tr>
</table>
<table visible="false" runat="server" id="panelShow">
    <tr>
        <td>
            用途名称：
        </td>
        <td align="left">
            <asp:TextBox ID="txt_UsageName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_UsageName"
                ErrorMessage="*">必填</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            描述：
        </td>
        <td align="left">
            <asp:TextBox ID="txt_UsageDesc" runat="server" Width="333px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td align="left">
            <asp:Button ID="btn_AddUsageAndDone" Text="确定" runat="server" OnClick="btn_AddUsageAndDone_Click"
                Width="57px" />
            <asp:Button ID="btn_CancelAdd" runat="server" OnClick="btn_CancelAdd_Click" Text="取消"
                Width="64px" /></td>
    </tr>
</table>
