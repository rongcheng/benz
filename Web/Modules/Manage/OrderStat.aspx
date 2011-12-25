<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_FuncPage.Master" AutoEventWireup="true" CodeBehind="OrderStat.aspx.cs" Inherits="WebUI.Modules.Manage.OrderStat" Title="订单完成情况统计" %>
<%@ Register Src="../../UserControls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    
<style type="text/css">
#stat li .l{width:100px;display:block;float:left}
#stat li .r{display:inline-block;width:60px}
#stat li .rr{display:inline-block;width:300px}
.column
{
	display:inline-block;
	_display:inline-block;
	
 background-color:#eeeeee;
 height:10px;
 line-height:10px;
 margin-right:6px;
 float:left;
 font-size:0px;
 margin-top:7px;
        }

#stat {margin-top:20px;margin-left:20px;line-height:25px;border:1px solid #D9D9D9;width:500px;padding:10px;}

</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="content_manage">
<h4>订单统计</h4>
</div>
<div class="content_manage_pannel content_manage1">
    <div style="margin-bottom:10px;margin-top:10px;">开始日期： &nbsp;<uc2:AjaxCalendar ID="myOrder_StartDate" runat="server" />
            &nbsp; 结束日期：<uc2:AjaxCalendar ID="myOrder_EndDate"   runat="server" />
            &nbsp; 
            <asp:Button ID="btnSearch" runat="server" Text="确定" OnClick="btnSearch_Click" />                         
    </div>
</div>
<div class="content_manage1">
<ul id="stat">
<li style="background-color:#F5F5F5"><span class="l">订单状态</span><span class="r">次数</span></li>
<asp:Repeater ID="rptOrderStat" runat="server">
<ItemTemplate>
<li>
<span class="l"><%#Eval("Status_CN") %></span>
<span class="r" ><%#Eval("OrderCount") %> </span>
<span class="rr"><div style="width:<%#GetLength(Eval("OrderCountPer").ToString()) %>px;" class="column"></div><div ><%#Eval("OrderCountPer") %></div></span>

</li>
</ItemTemplate>
</asp:Repeater>
</ul>
</div>

</asp:Content>
