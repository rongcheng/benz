<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetailPic.aspx.cs" EnableSessionState="True" Inherits="WebUI.Modules.Manage.OrderDetailPic"  MasterPageFile="~/MPages/QJ_MasterPage.Master" %>

<%@ Register src="../../UserControls/DataResource.ascx" tagname="DataResource" tagprefix="uc1" %>
<%@ Register Assembly="QJ.WebControls" Namespace="QJ.WebControls" TagPrefix="qj" %>
<asp:Content ID="header1" ContentPlaceHolderID="head" runat="server">
<script language="javascript" type="text/javascript">
function DelItem(itemId,userId)
{
    if(!confirm("确定删除吗？"))
    return;

    var url1="/handlers/orderHandler.ashx";
    var data="action=delresource&orderId=<%=orderId %>&resourceId="+itemId;
    $.ajax({
        type:"Get",
        url:url1,
        data:data,
        success:function(msg){
            alert(msg);
            $("#imgContainer-"+itemId).hide(400);
        }
    
    });
    
}

</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<asp:Label ID="lb_ResultCount" runat="server" Text=""></asp:Label>
<uc1:DataResource ID="DataResource1" ShowPreview="true" ShowFavDelete="true" runat="server" />
</asp:Content>
