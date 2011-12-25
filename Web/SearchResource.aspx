<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_MasterPage.Master"  AutoEventWireup="true" CodeBehind="SearchResource.aspx.cs" Inherits="WebUI.SearchResource" %>
<%@ Register Src="UserControls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="uc1" %>
<%@ Register Src="UserControls/Search.ascx" TagName="Search" TagPrefix="uc6" %>
<%@ Register Assembly="QJ.WebControls" Namespace="QJ.WebControls" TagPrefix="qj" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
    <title>高级搜索</title>    
</asp:Content>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
        </cc1:ToolkitScriptManager>
        <div class="tools" style="margin-bottom:3px; width:960px;">
	<p class="keywordText" id="searchResultTips">
	高级搜索 >>
	</p>
    </div>
    <div class="advanced_search">
        <div class="gutter">
            <p> <span>关键字：</span>
                <asp:TextBox ID="Kwords" runat="server" MaxLength="100" ToolTip="请输入中英文关键字或图片编号查询"
                    EnableViewState="false" CssClass="input_txt"></asp:TextBox>
            </p>
            <p style="line-height:normal">
                <span>上传时间：</span><asp:CheckBox ID="chkUpDate" runat="server" />
                &nbsp;&nbsp;<uc1:AjaxCalendar ID="BeginDate" runat="server" />
                ～ &nbsp;&nbsp;&nbsp;
                <uc1:AjaxCalendar ID="EndDate" runat="server"  />
            </p>
            <p>
                <span>分类：</span>
                <asp:DropDownList ID="DdlCatalogid" runat="server">
                </asp:DropDownList>
            </p>
            <p><span>资源类型：</span>
            <asp:CheckBox ID="ckbRTImage" runat="server"  Text="图片" Checked="true"/>
            <%--<asp:CheckBox ID="ckbRTVideo" runat="server" Text="视频" Checked="true"/>
            <asp:CheckBox ID="ckbDocument" runat="server" Text="电子文档" Checked="true"/>--%>
            <asp:CheckBox ID="ckbOther" runat="server" Text="其它" Checked="true"/>
            </p>
            <p><span>所属机构：</span><asp:DropDownList ID="ddlGroup" runat="server"></asp:DropDownList></p>
            <p>
                
                <br />
                <asp:Button ID="btnSearch1" CssClass="adv_btn_search" runat="server" Text="搜&nbsp;&nbsp;索"
                    OnClick="btnSearch_Click" EnableViewState="false"  />
               
               
                    </p>
            <asp:HiddenField ID="hidLastSearch" runat="server" />
        </div>
    </div>
</asp:Content>
