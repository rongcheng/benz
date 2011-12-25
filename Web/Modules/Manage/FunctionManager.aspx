<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_FuncPage.Master" Theme="MainSkin" AutoEventWireup="true"
    Codebehind="FunctionManager.aspx.cs" Inherits="WebUI.Modules.Manage.FunctionManager"  Title="功能管理"%>

<%@ Register Src="../../UserControls/SysFunction.ascx" TagName="SysFunction" TagPrefix="uc2" %>
<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
.bigRow
{
	background-color:#dddddd;
	}
.smallRow
{
	background-color:#222222;
	}
	 .grvpager
 {
 	text-align:left;
}
.table{border:0; border-collapse:collapse;}
.manage_table th{ border-bottom:#d4d0c8 solid 1px; border-right:#d4d0c8 solid 1px; margin:0; text-align:center; background-color:#666; color:#fff; font-weight:700; height:30px;}
.cell td{border-collapse:collapse; padding:3px;background-color:#fbfbfb; border:solid 1px white;  margin:0; height:24px; line-height:24px; text-align:left; overflow:hidden;}
.both td{border-collapse:collapse; padding:3px;background-color:#F3F3F3;  border:solid 1px white; margin:0; height:24px; line-height:24px; text-align:left; overflow:hidden;}

</style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="content_manage">
<h4>功能管理</h4>

    <br /><asp:Button ID="btn_AddFunction" runat="server" Text="新增功能" OnClick="btn_AddFunction_Click" />
      </div> <br /><div class="content_manage1"><table width="100%">

        <tr>
            <td>
                <asp:GridView ID="gv_Function" DataKeyNames="FunctionId,ParentFunctionId" AutoGenerateColumns="False"
                  CssClass="table" BorderWidth="0"  AllowPaging="false" runat="server" OnRowDeleting="gv_Usage_RowDeleting" OnRowEditing="gv_Function_RowEditing"
                 onrowdatabound="gv_Function_RowDataBound" >
                     <HeaderStyle HorizontalAlign="Center" BackColor="#666666" BorderColor="#D4D0C8" 
                BorderStyle="Solid" BorderWidth="1px" ForeColor="#fffFFF" Width="200px" Height="30px"></HeaderStyle>
    <RowStyle BorderColor="White" 
                BorderStyle="Solid" BorderWidth="1px" CssClass="both" />
    <AlternatingRowStyle BorderColor="White" 
                BorderStyle="Solid" BorderWidth="1px" CssClass="cell" />
        
                    <Columns>                     
                    
                        <asp:BoundField DataField="FunctionName" HeaderText="功能名称" />
                        <asp:BoundField DataField="UrlPath" HeaderText="功能链接" />
                        <asp:BoundField DataField="Description" HeaderText="描述" />
                        <asp:BoundField DataField="orderFlag" HeaderText="排序标志位" />
                        <asp:CommandField HeaderText="操作" ShowEditButton="true" ShowDeleteButton="True" DeleteText="&lt;span onclick=&quot;return window.confirm('您确定删除吗？')&quot;&gt;删除&lt;/span&gt;" />
                        
                    </Columns>
                    <PagerStyle BorderStyle="None" CssClass="grvpager" HorizontalAlign="Left" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td height="20px">
            </td>
        </tr>
        <tr id="tr_FunctionInfo" runat="server" visible="false">
            <td>
                <table width="100%">
                    <tr>
                        <td align="right">
                            功能名称：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txt_FunctionName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                ControlToValidate="txt_FunctionName">必填</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">所属大类：
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlParentFunction" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td align="right">
                            功能链接：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txt_UrlPath" runat="server" Width="251px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                ControlToValidate="txt_UrlPath">必填</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="right">
                            描述：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txt_Description" runat="server" Width="252px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            排序号：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txt_orderFlag" runat="server" Width="51px"></asp:TextBox>
                            &nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                ControlToValidate="txt_orderFlag">必填</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btn_Conform" Text="确定" runat="server" OnClick="btn_Conform_Click" />
                            <asp:Button ID="btn_Cancel" Text="取消" runat="server" OnClick="btn_Cancel_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left">
                <input id="Hidden1" type="hidden" runat="server" />
                <asp:Label ID="lb_Info" runat="server" Text="" ForeColor="red"></asp:Label></td>
        </tr>
    </table>
  </div>
</asp:Content>
