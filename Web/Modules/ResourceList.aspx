<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MPages/QJ_FuncPage.Master"  CodeBehind="ResourceList.aspx.cs" Inherits="WebUI.Modules.ResourceList"  EnableSessionState="True"%>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="../UserControls/SysFunction.ascx" TagName="SysFunction" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/CatalogTree.ascx" TagName="CatalogTree" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/DataResource.ascx" TagName="DataResource" TagPrefix="uc30" %>
<%@ Register Assembly="QJ.WebControls" Namespace="QJ.WebControls" TagPrefix="qj" %>
<asp:Content ID="ContentHeader" runat="server" ContentPlaceHolderID="head">
<script language="javascript" type="text/javascript">
function QJDetail(itemid)
{
    window.open("Video/Detail.aspx?ItemID="+itemid);
}

</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <div class="content_manage">
<h4>信息编辑</h4>  
 <div class="content_manage_pannel">关键字或者编号:
        <asp:TextBox ID="txtResourceSN" runat="server"></asp:TextBox><asp:Button ID="btnSea" runat="server" Text="搜索" OnClick="btnSea_Click" />
 </div>
</div>
   
    <table width="99%" style="margin-left:10px;">
        <tr>
            <td valign="top">
                <div style="float: left; overflow-y: auto; height: 500px; width: 180px; border: 1px #C8C8C8 solid;">
                    <uc1:CatalogTree ID="cataTree" UploadRight="true" runat="server" />
                </div>
            </td>
            <td valign="top">
                <div class="back_holderB" >
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table style="">
                                <tr>
                                    <td>
                                    <uc30:DataResource ID="drResource" ShowEdit="true"  ShowPreview="true" runat="server"></uc30:DataResource>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <qj:PageBar ID="pageBar" PageSize="9" runat="server" Visible="true" ShowInputBox="Always"
                                            OnPageChanged="pageBar_PageChanged" Width="300" NextPageText="<img src='/images/next.gif' align='absmiddle' border='0'>"
                                            PrevPageText="<img src='/images/prev.gif' align='absmiddle' border='0'>">
                                        </qj:PageBar>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
