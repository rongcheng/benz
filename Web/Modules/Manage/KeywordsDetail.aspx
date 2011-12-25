<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KeywordsDetail.aspx.cs" Inherits="WebUI.Modules.Manage.KeywordsDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
     <style type="text/css" >
    body{font-size:12px;background-color:white}
    .item{color:black;display:inline;border:0px solid blue;width:100px;padding:5px 5px 5px 5px}    
    .item a{display:inline;text-decoration:none
    }
    #k 
    
    </style>
    <script language="javascript" type="text/javascript">
    
    function del()
    {
        return confirm("确定要删除该关键词吗？");
    
    }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
<div style="margin:10px auto auto 10px;border:1px solid #cccccc;display:block;width:450px;height:260px;padding:1px 5px 5px 5px;" id="k">
    <h3>管理该类别下的关键词：</h3>

    <asp:Repeater ID="rptKey" runat="server" onitemcommand="rptKey_ItemCommand"  >
    <ItemTemplate>
    <div class="item"><%#Eval("keyword") %> 
   
        <asp:LinkButton ID="lbDel" runat="server" CommandName="Del" CommandArgument='<%# Eval("id")%>' CausesValidation="false" OnClientClick="return del()" >x</asp:LinkButton>
       
    </div>
    </ItemTemplate>
    </asp:Repeater>
    
    <div style="clear:both"   ></div>
<div style="line-height:29px;height:29px;background-color:#F1F1F1;margin-top:10px;padding-left:5px;">添加关键字</div>
<div style="line-height:39px;height:39px;">关键字名称：<asp:TextBox ID="txtName" runat="server"></asp:TextBox> 
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ErrorMessage="*" ControlToValidate="txtName"></asp:RequiredFieldValidator>
    <%--序号：<asp:TextBox ID="txtOrder" Width="40px" MaxLength="4" runat="server"></asp:TextBox>--%>
<asp:Button ID="btnSave" runat="server" Text="添加" onclick="btnSave_Click" 
        style="height: 26px" />
    <asp:HiddenField ID="hId" runat="server" />
</div>
    </div>
    </form>
</body>
</html>
