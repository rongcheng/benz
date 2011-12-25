<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AjaxCalendar.ascx.cs" Inherits="WebUI.UserControls.AjaxCalendar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:TextBox ID="txtDate" runat="server" Width="80px"></asp:TextBox>
<cc1:CalendarExtender ID="calExtend" runat="server" Animated="false" TargetControlID="txtDate" Format="yyyy-MM-dd" >
</cc1:CalendarExtender>
