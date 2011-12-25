<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_FuncPage.Master" AutoEventWireup="true" CodeBehind="IndexFlashImagesConfig.aspx.cs" Inherits="WebUI.Modules.Manage.IndexFlashImagesConfig" Title="首页Flash图片配置" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<style type="text/css">
.table{border:0; border-collapse:collapse;}
.manage_table th{ border-bottom:#d4d0c8 solid 1px; border-right:#d4d0c8 solid 1px; margin:0; text-align:center; background-color:#666; color:#fff; font-weight:700; height:30px;}
.cell td{border-collapse:collapse; padding:3px;background-color:#fbfbfb; border:solid 1px white;  margin:0; height:24px; line-height:24px; text-align:left; overflow:hidden;}
.both td{border-collapse:collapse; padding:3px;background-color:#F3F3F3;  border:solid 1px white; margin:0; height:24px; line-height:24px; text-align:left; overflow:hidden;}
.grvpager
 {
 	text-align:left;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



<div class="content_manage">
<h4>首页flash图片</h4>

<br />
    <asp:GridView ID="grvImages" runat="server" AutoGenerateColumns="False"   
        CssClass="table" DataKeyNames="id"
        onrowdatabound="grvImages_RowDataBound" 
        onrowcommand="grvImages_RowCommand" onrowupdating="grvImages_RowUpdating" 
                                    onrowediting="grvImages_RowEditing" 
                                    onrowcancelingedit="grvImages_RowCancelingEdit" 
                                    onrowdeleting="grvImages_RowDeleting" onrowupdated="grvImages_RowUpdated">
        <HeaderStyle HorizontalAlign="Center" BackColor="#666666" BorderColor="#D4D0C8" 
                BorderStyle="Solid" BorderWidth="1px" ForeColor="#fffFFF" Width="200px" Height="30px"></HeaderStyle>
    <RowStyle BorderColor="White" 
                BorderStyle="Solid" BorderWidth="1px" CssClass="both" />
    <AlternatingRowStyle BorderColor="White" 
                BorderStyle="Solid" BorderWidth="1px" CssClass="cell" />
        <Columns>
            <asp:TemplateField HeaderText="图片预览">
            <ItemTemplate>
            <img src="<%#Eval("src") %>" width="200px" />
            </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Description" HeaderText="文字描述"  
                ControlStyle-Width="150px" >
<ControlStyle Width="150px"></ControlStyle>
            </asp:BoundField>
            <asp:BoundField DataField="link" HeaderText="链接" ControlStyle-Width="100px" >
<ControlStyle Width="100px"></ControlStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Order" HeaderText="顺序" ControlStyle-Width="50px" >
            
         
            
<ControlStyle Width="50px"></ControlStyle>
            </asp:BoundField>
            
         
            
            <asp:TemplateField HeaderText="状态">
            <ItemTemplate><%#GetCode(Eval("Status").ToString())%></ItemTemplate>            
            <EditItemTemplate>            
                <asp:RadioButtonList ID="rblIsUsed" runat="server" RepeatDirection="Vertical" Width="80px">
                <asp:ListItem Value="1">可用</asp:ListItem>
                <asp:ListItem Value="0" >禁用</asp:ListItem>
                </asp:RadioButtonList>
           
            
            </EditItemTemplate>
            </asp:TemplateField>
            
            
            <asp:CommandField ShowEditButton="True" CausesValidation="false" DeleteText="&lt;span onclick='return window.confirm(&quot;确定删除吗？&quot;)'&gt;删除&lt;/span&gt;" ShowDeleteButton="true" />
            
            <asp:TemplateField HeaderText="" Visible="false">
            <ItemTemplate>
            <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("id")%>' CommandName="Update" Text="编辑" CausesValidation="false" Visible="false"></asp:LinkButton> 
            &nbsp;
            <asp:LinkButton ID="btnDel" runat="server" CommandArgument='<%#Eval("id")%>' CommandName="del" Text="&lt;span onclick='return window.confirm(&quot;确定删除吗？&quot;)'&gt;删除&lt;/span&gt;" CausesValidation="false"></asp:LinkButton> 
            
            </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>
    
    
    
    
     <p class="save" style="margin-top:10px" >
        <asp:Button CausesValidation="False" ID="btnNewUser" runat="server" Text="上传新图片" OnClick="btnUpload_Click" />
    </p>
    <p class="save" style="margin-top:10px" >
        <asp:HiddenField ID="txtHiddenId" runat="server" />
                                </p>
    <div runat="server" visible="false" id="divUpload"  style="margin-top:10px">

    
    <table class="table" cellpadding=1 cellspacing=1  width=100%>
    <tr><th colspan="2">上传新的图片</th></tr>
    <tr><td width="100">选择文件</td><td>
        <asp:FileUpload ID="fuImage" runat="server" /> (只能是jpg格式的图片，宽>550，宽：高<2)
        </td></tr>
    <tr><td>文字描述</td><td>
        <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
        </td></tr>
    <tr><td>链接</td><td> <asp:TextBox ID="txtLink" runat="server"></asp:TextBox></td></tr>
    <tr><td>顺序</td><td> <asp:TextBox ID="txtOrder" runat="server" Text="1"></asp:TextBox><asp:RangeValidator
            ID="RangeValidator1" runat="server" ErrorMessage="输入有误" ControlToValidate="txtOrder" MaximumValue="100" 
                                                MinimumValue="1" Type="Integer"></asp:RangeValidator>(1-100之间)</td></tr>
    <tr><td>状态</td><td>
        <asp:RadioButtonList ID="rblStatus" runat="server" Width="170px" 
            RepeatDirection="Horizontal" >
        <asp:ListItem Selected=True Value="1">可用</asp:ListItem>
        <asp:ListItem Value="0">禁用</asp:ListItem>
        </asp:RadioButtonList>
        </td></tr>
    <tr><td></td><td>
        <asp:Button ID="btnUpload" runat="server" Text="上传" 
            onclick="btnUpload_Click1" />
        </td></tr>
    
    
    </table>
    
    
        
    
    </div>
    
    
</div>





</asp:Content>
