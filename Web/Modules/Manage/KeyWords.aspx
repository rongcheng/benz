<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_FuncPage.Master" AutoEventWireup="true" CodeBehind="KeyWords.aspx.cs" Inherits="WebUI.Modules.Manage.KeyWords" Title="关键字管理" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script language="javascript" type="text/javascript">
  function newKeyword(id)
 {
    var url1='KeywordsDetail.aspx?id='+id;
    artDialog({id:'newKeyword', title:'设置关键词', url:url1, width:500, height:320}).close(function(){}); 
 }
 function closeKeyword()
 {
    artDialog({id:'newKeyword'}).close();
 }
 </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<div class="content_manage">
<h4>关键字管理</h4>

<div style="clear:both"   ></div>
<div style="margin-left:0px;margin-top:10px">
<asp:GridView ID="grvKeyCatalog" runat="server" BackColor="White" 
        BorderColor="#CCCCCC" BorderStyle="Dotted" BorderWidth="1px" 
        CellPadding="4" ForeColor="Black" GridLines="Horizontal" 
        AutoGenerateColumns="False" onrowdeleted="grvKeyCatalog_RowDeleted" 
        onrowdeleting="grvKeyCatalog_RowDeleting" 
        onrowediting="grvKeyCatalog_RowEditing" 
        onrowcancelingedit="grvKeyCatalog_RowCancelingEdit" 
        onrowupdating="grvKeyCatalog_RowUpdating" DataKeyNames="Id">
    <Columns>
        <asp:BoundField DataField="keyword" HeaderText="类别" >
            <ItemStyle Width="400px" />
        </asp:BoundField>
        
         <asp:BoundField DataField="Sort" HeaderText="序号" >
            <ItemStyle Width="50px" />
        </asp:BoundField>
        
        <asp:CommandField ShowEditButton="True" />
        <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                    CommandName="Delete" Text="删除" OnClientClick="return confirm('删除分类，该分类下的所有关键词将被删除，确定吗？')" ForeColor="Black"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
        <ItemTemplate>
        
        <a href="javascript:newKeyword('<%# Eval("id") %>')" style="color:Black">管理关键字</a>
        </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
 
 <div style="clear:both"   ></div>
<div style="line-height:29px;height:29px;background-color:#F1F1F1;margin-top:10px;padding-left:5px;">添加类别</div>
<div style="line-height:39px;height:39px;">名称：<asp:TextBox ID="txtName" runat="server"></asp:TextBox> 序号：<asp:TextBox ID="txtOrder" Width="40px" MaxLength="4" runat="server"></asp:TextBox>
<asp:Button ID="btnSave" runat="server" Text="添加" onclick="btnSave_Click" />
</div>
    
    
    </div></div>
</asp:Content>


