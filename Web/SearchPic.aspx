<%@ Page Language="C#" MasterPageFile="~/MPages/MasterPage.Master" AutoEventWireup="true"
    Codebehind="SearchPic.aspx.cs" Inherits="WebUI.SearchPic" %>

<%@ Register Src="UserControls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="uc1" %>

<%@ Register Src="UserControls/Calendar.ascx" TagName="Calendar" TagPrefix="uc7" %>
<%@ Register Src="UserControls/Search.ascx" TagName="Search" TagPrefix="uc6" %>
<%@ Register Assembly="QJ.WebControls" Namespace="QJ.WebControls" TagPrefix="qj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="advanced_search">
        <h2>
            高级搜索</h2>
        <div class="gutter">
            <p>
                关键字：
                <asp:TextBox ID="Kwords" runat="server" MaxLength="100" ToolTip="请输入中英文关键字或图片编号查询"
                    EnableViewState="false"></asp:TextBox>
            </p>
            <p>
                上传时间：<asp:CheckBox ID="chkUpDate" runat="server" />
                &nbsp;&nbsp;<uc1:AjaxCalendar ID="BeginDate" runat="server" />
                ～ &nbsp;&nbsp;&nbsp;
                <uc1:AjaxCalendar ID="EndDate" runat="server" />
            </p>
            <p>
                分类：
                <asp:DropDownList ID="DdlCatalogid" runat="server">
                </asp:DropDownList>
            </p>
            <p>
                <asp:ImageButton ID="btnSearch" CssClass="btnSearch" runat="server" ImageUrl="/images/search.jpg"
                    OnClick="btnSearch_Click" EnableViewState="false" /></p>
            <asp:HiddenField ID="hidLastSearch" runat="server" />
        </div>
    </div>
</asp:Content>
