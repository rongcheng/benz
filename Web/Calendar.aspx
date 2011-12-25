<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_MasterPage.Master" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="WebUI.Calendar" Title="无标题页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">

BODY {	PADDING-RIGHT: 0px;	PADDING-LEFT: 0px;	FONT-SIZE: 12px;	PADDING-BOTTOM: 0px;	MARGIN: 0px;	PADDING-TOP: 0px} 
BODY {	FONT-FAMILY: Arial,"ˎ̥"} 
BODY {	PADDING-RIGHT: 0px;	PADDING-LEFT: 0px;	FONT-SIZE: 12px;	PADDING-BOTTOM: 0px;	MARGIN: 0px;	PADDING-TOP: 0px}
.main {	MARGIN: 0px auto;	WIDTH: 1000px}
.main_xk {	PADDING-BOTTOM: 50px}
.club_main {	CLEAR: both;	MARGIN: 0px auto;	WIDTH: 1000px;	PADDING-TOP: 5px}
.club_main {	CLEAR: both;	PADDING-BOTTOM: 5px;	MARGIN: 0px auto;	WIDTH: 1000px}
.issue_main {	BACKGROUND: url(images/issue_bj.jpg) #fff no-repeat left top;	MARGIN: 0px auto;	WIDTH: 75%}
.issue_top {BORDER-BOTTOM: #0f439e 1px solid; padding-bottom:5px; PADDING-RIGHT: 0px;	PADDING-LEFT: 60px;	FONT-WEIGHT: bold;	FONT-SIZE: 16px;	VERTICAL-ALIGN: bottom;	COLOR: #0f439e;	PADDING-TOP: 30px;		HEIGHT: 19px; text-align:center; font-size:16px;}
.issue_main LI {	PADDING-RIGHT: 30px;	PADDING-LEFT: 60px;	PADDING-BOTTOM: 0px;	PADDING-TOP: 0px}
LI {	LIST-STYLE-TYPE: none; text-align:right; padding-right:30px;}
UL {	PADDING-RIGHT: 0px;	PADDING-LEFT: 0px;	PADDING-BOTTOM: 0px;	MARGIN: 0px;	PADDING-TOP: 0px;	LIST-STYLE-TYPE: none} 
UL {	PADDING-RIGHT: 0px;	PADDING-LEFT: 0px;	PADDING-BOTTOM: 0px;	MARGIN: 0px;	PADDING-TOP: 0px}
#context{	PADDING-LEFT: 100px; padding-right:25px;	WORD-BREAK: break-all;	BORDER-COLLAPSE: collapse; margin-top:15px;}
.newclass{ text-align:right; width:100%; padding-right:20px; padding-bottom:3px; padding-top:5px;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="main">
    <div class="main_xk">
        <div class="club_main">
            <div class="issue_main" id="Content" runat="server">
            </div>
        </div>
    </div>
</div>
</asp:Content>
