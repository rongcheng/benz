<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyLightBox.aspx.cs" Inherits="WebUI.Modules.MyLightBox" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>管理我的收藏夹</title>
     <style type="text/css" >
    body{font-size:12px;background-color:white}
    .item{color:black;display:inline;border:0px solid blue;width:100px;padding:5px 5px 5px 5px}    
    .item a{display:inline;text-decoration:none
    }

.table{border:0; border-collapse:collapse; background:url(../../image/common/table_bg.gif);}
.manage_table th{ border-bottom:#d4d0c8 solid 1px; border-right:#d4d0c8 solid 1px; margin:0; text-align:center; background-color:#666; color:#fff; font-weight:700; height:30px;}
.cell td{border-collapse:collapse; padding:3px; margin:0; height:24px; border:solid 1px white;  line-height:24px; text-align:left; background:url(../../image/common/td_bg.gif) no-repeat 0 100%; overflow:hidden;}
.both td{border-collapse:collapse; padding:3px; margin:0; height:24px; border:solid 1px white; line-height:24px; text-align:left; background:url(../../image/common/td_bg.gif) no-repeat 0 100%; overflow:hidden;}


    
    </style>
    <script language="javascript" type="text/javascript">
    
    function del()
    {
        return confirm("确定要删除该收藏夹吗？");
    
    }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
<div style="margin:10px auto auto 10px;border:1px solid #cccccc;display:block;width:450px;height:260px;padding:1px 5px 5px 5px;" id="k">
    <h3>管理我的收藏夹：</h3>

    <asp:Repeater ID="rptKey" runat="server" onitemcommand="rptKey_ItemCommand"  Visible="false" >
    <ItemTemplate>
    <div class="item"><%#Eval("title") %> 
   
        <asp:LinkButton ID="lbDel" runat="server" CommandName="Del" CommandArgument='<%# Eval("id")%>' CausesValidation="false" OnClientClick="return del()" >x</asp:LinkButton>
       
    </div>
    </ItemTemplate>
    </asp:Repeater>

    <div style="clear:both"  class="both"  >
        <asp:GridView ID="grvMyLightBox" runat="server" AutoGenerateColumns="False"  datakeynames="ID"
            onrowcancelingedit="grvMyLightBox_RowCancelingEdit" 
            onrowdeleted="grvMyLightBox_RowDeleted" 
            onrowdeleting="grvMyLightBox_RowDeleting" 
            onrowediting="grvMyLightBox_RowEditing" 
            onrowupdated="grvMyLightBox_RowUpdated" 
            onrowcommand="grvMyLightBox_RowCommand" 
            onrowupdating="grvMyLightBox_RowUpdating" Width="419px"
             CssClass="table"
            >
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="收藏夹名称" />
                <asp:CommandField ShowEditButton="True" CausesValidation="false" />
           
                <asp:TemplateField>
                <ItemTemplate>
                <asp:LinkButton ID="lbDel" CausesValidation="false" CommandName="Delete" Text="删除"  OnClientClick="return del();" runat="server"></asp:LinkButton>
                </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
<div style="line-height:29px;height:29px;background-color:#F1F1F1;margin-top:10px;padding-left:5px;">添加收藏夹</div>
<div style="line-height:39px;height:39px;">收藏夹名称：<asp:TextBox ID="txtName" runat="server"></asp:TextBox> 
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ErrorMessage="*" ControlToValidate="txtName"></asp:RequiredFieldValidator>
    <%#Eval("title") %>
<asp:Button ID="btnSave" runat="server" Text="添加" onclick="btnSave_Click" 
        style="height: 26px" />
    <asp:HiddenField ID="hId" runat="server" />
</div>
    </div>
    </form>
</body>
</html>
