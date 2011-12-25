<%@ Page Language="C#" Theme="MainSkin" MasterPageFile="~/MPages/QJ_FuncPage.Master" AutoEventWireup="true"
    Codebehind="ManageInfo.aspx.cs" Inherits="WebUI.Modules.Manage.ManageInfo"  Title="来源、用途管理"%>

<%@ Register Src="../../UserControls/SysFunction.ascx" TagName="SysFunction" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="content_manage">
<h4>用途管理</h4>

 
    <table width="100%">
        <tr>
            <td>
            
                <p align="left">
                    <asp:LinkButton  CausesValidation="false" ID="btnlk_SourceManage" PostBackUrl="ManageInfo.aspx?manage=source"
                        runat="server" Visible="false">来源管理</asp:LinkButton>
                    &nbsp; 
                    <asp:LinkButton  CausesValidation="false" ID="btnlk_UsageManage" runat="server" PostBackUrl="ManageInfo.aspx?manage=usage" Visible="false">用途管理</asp:LinkButton></p>
            </td>
        </tr>
        <tr>
            <td id="uc_Cells" runat="server" align="left" class="allborder">
            </td>
        </tr>
    </table>
    
    </div>
</asp:Content>
