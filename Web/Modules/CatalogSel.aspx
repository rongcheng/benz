<%@ Page Language="C#"  AutoEventWireup="true" Codebehind="CatalogSel.aspx.cs"
    Inherits="WebUI.Modules.CatalogSel" Title="设置分类" %>
<%@ Register Src="../UserControls/CatalogTree.ascx" TagName="CatalogTree" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<link href="/UI/css/global.css" rel="stylesheet" type="text/css" />

<style type="text/css" >
td{font-size:12px}

.lb{ 
 display:inline-block;
 height:21px; line-height:21px; padding:0 10px 0 10px; max-width:200px; border:1px solid #cccccc; 
 font-size:12px; color:#666; text-shadow:0 1px 0 #FFF; 
 background-color:#E0E0E0;
 margin:5px 5px 5px 5px;
 }
</style>
</head>
<body>
<form id="form1" runat="server">
    <div class="manage" style="background-color:White;">
        <table width="450">            
        
            <tr>
                <td>
                    <uc1:CatalogTree  UploadRight="true" TreeNodeType="Leaf" ID="cataTree" runat="server" />
                </td>
            </tr>
              <tr>
                <td height="20">
                </td>
            </tr>
            <tr>
                <td>
                    <asp:LinkButton ID="btnSetCata" runat="server" OnClick="btnSetCata_Click" CssClass="lb"> 保 存 </asp:LinkButton>
                    &nbsp;
                    <a href="javascript:onclick=window.parent.artDialog({id:'dg_test4330'}).close()" class="lb"> 关 闭 </a>
                </td>
            </tr>
        </table>
    </div>
</form>
</body>
</html>