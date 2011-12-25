<%@ Control Language="C#" AutoEventWireup="true" Codebehind="SourceManage.ascx.cs"
    Inherits="WebUI.UserControls.SourceManage" EnableViewState="false" %>
<table width="1">
    <tr>
        <td>
            <asp:Button ID="btn_ShowAddSource" runat="server" Text="新建来源" OnClick="btn_ShowAddSource_ServerClick" />
        </td>
    </tr>
    <tr>
        <td valign="top">
            <asp:GridView ID="gv_Source" Width="100%" DataKeyNames="SourceID" AutoGenerateColumns="False"
                AllowPaging="false" runat="server" OnRowCancelingEdit="gv_Source_RowCancelingEdit"
                OnRowEditing="gv_Source_RowEditing" OnRowUpdating="gv_Source_RowUpdating" OnRowDeleting="gv_Source_RowDeleting" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Dotted" BorderWidth="1px" 
        CellPadding="4" ForeColor="Black">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <b>来源</b>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "SourceName").ToString().Trim() %>'
                                runat="server" ID="col1" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            &nbsp;
                            <asp:TextBox Text='<%# DataBinder.Eval(Container.DataItem, "SourceName").ToString().Trim() %>'
                                runat="server" ID="SourceName" Width="90%" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <b>简介</b>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "SourceDesc").ToString().Trim() %>'
                                runat="server" ID="col2" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            &nbsp;
                            <asp:TextBox Text='<%# DataBinder.Eval(Container.DataItem, "SourceDesc").ToString().Trim() %>'
                                runat="server" ID="SourceDesc" Width="90%" />
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
            来源：
        </td>
        <td align="left">
            <asp:TextBox ID="txt_SourceName" runat="server" class="txt"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_SourceName"
                ErrorMessage="*">必填</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            描述：
        </td>
        <td align="left">
            <asp:TextBox ID="txt_SourceDesc" runat="server" Width="333px" class="txt"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:Button ID="btn_AddSourceAndDone" Text="确定" runat="server" OnClick="btn_AddSourceAndDone_Click"
                Width="58px" />
            <asp:Button ID="btn_CancelAdd" runat="server" OnClick="btn_CancelAdd_Click" Text="取消"
                Width="62px" /></td>
    </tr>
</table>
