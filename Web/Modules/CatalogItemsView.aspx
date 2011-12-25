<%@ Page Language="C#" MasterPageFile="~/MPages/MasterPage.Master" AutoEventWireup="true" CodeBehind="CatalogItemsView.aspx.cs" Inherits="WebUI.Modules.CatalogItemsView" Title="图片库" %>

<%@ Register Src="../UserControls/CatalogTree.ascx" TagName="CatalogTree" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript" type="text/javascript">

function GoimageView(url)
{
	window.location.href=url;
}
            
</script>
   <table width="100%" height="100%">
        <tr>
            <td colspan=2 align=right><asp:Label ID="pageCountLB" runat=server></asp:Label>
                <asp:TextBox ID="pageCounttxt" runat="server"  Width="20px" Height="20px" Text="1"></asp:TextBox>
                <asp:Button  id="goPageBT"  runat=server value="Go"  />
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" style="width:197px">
                <div style="overflow-y:auto; height: 800px; border-right: thin inset;
                        border-top: thin inset; border-left: thin inset; border-bottom: thin inset;">
                    <uc1:CatalogTree ID="catalogTree" runat="server"></uc1:CatalogTree>
                </div>   
               </td>
            <td valign=top align=left>
             
                <table id="imagelist_table" name="imagelist_table" style="width:100%">
                    <tbody id="imagelist_tbody"  runat="server">
				
					</tbody>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
