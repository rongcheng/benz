<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_FuncPage.Master" AutoEventWireup="true" CodeBehind="LogList.aspx.cs" Inherits="WebUI.Modules.LogList" Title="日志列表" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="../UserControls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/SysFunction.ascx" TagName="SysFunction" TagPrefix="uc2" %>
<%@ Register Assembly="QJ.WebControls" Namespace="QJ.WebControls" TagPrefix="qj" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
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
.img1
{
	max-height:50px;
	max-width:50px;
	_width:expression(document.body.clientWidth > 50 ? "50px" : "auto");
}
.btnDel
{display:inline-block;text-align:center;line-height:23px;margin-top:5px;float:right}
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="content_manage" style="border:0px solid red; margin-bottom:20px;">
<h4>日志列表</h4>   </div><br />&nbsp;<br />
    <table class="searchTit" style="margin-left:10px;">
        <tr>
            <td>
                用户登录ID：<asp:TextBox ID="txtLoginName" runat="server" Width="80px"></asp:TextBox>
                操作类型：<asp:DropDownList ID="ddlLogType" runat="server"></asp:DropDownList>
                开始日期： &nbsp;&nbsp;
                <uc3:AjaxCalendar ID="t_Date" runat="server"></uc3:AjaxCalendar>
                -- 结束日期：
                <uc3:AjaxCalendar ID="e_Date" runat="server"></uc3:AjaxCalendar>
                <asp:Button ID="searchDate" runat="server" Text="确定" OnClick="searchDate_Click" />
            </td>
        </tr>
        <tr>
            <td height="10px"></td>
        </tr>
        <tr>
            <td align="center">
               
                        <asp:GridView CssClass="table" BorderWidth="0" ID="GridView1"  EmptyDataRowStyle-Font-Bold="true"
                            EmptyDataText="没有记录" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" 
                            Width="730px" onrowdatabound="GridView1_RowDataBound" 
                           >
    <HeaderStyle HorizontalAlign="left" BackColor="#666666" BorderColor="#D4D0C8" 
                BorderStyle="Solid" BorderWidth="1px" ForeColor="#fffFFF" Width="200px" Height="30px"></HeaderStyle>
    <RowStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CssClass="both"/>
    <AlternatingRowStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CssClass="cell" />
                            <Columns>
                                <asp:BoundField HeaderText="用户名" DataField="UserName" /> 
                                                        <asp:BoundField HeaderText="操作类型" DataField="EventType" />               
                                <asp:BoundField HeaderText="操作结果" DataField="EventResult"  />
                                <asp:BoundField HeaderText="备注" DataField="EventContent"  />
                                <asp:BoundField HeaderText="IP" DataField="IP"  />
                                <asp:BoundField HeaderText="日期" DataField="AddDate"  DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                                                             
                            </Columns>      
                                            
                        </asp:GridView>
 <webdiyer:AspNetPager ID="AspNetPager1" runat="server" 
                            ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" 
                TextBeforePageIndexBox="转到: " HorizontalAlign="right" PageSize="10" 
                EnableTheming="true" CssClass="Pager_Number" ShowPrevNext="false" 
                            onpagechanging="AspNetPager1_PageChanging"  >
</webdiyer:AspNetPager> 


            </td>
        </tr>
    </table>
</asp:Content>
