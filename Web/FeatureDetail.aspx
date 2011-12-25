<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_MasterPage.Master" EnableSessionState="True" AutoEventWireup="true" CodeBehind="FeatureDetail.aspx.cs" Inherits="WebUI.FeatureDetail" Title="无标题页" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="UserControls/DataResource.ascx" tagname="DataResource" tagprefix="uc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" language="JavaScript" src="ui/qjjs/common.js"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
</cc1:ToolkitScriptManager>
<div id="magazine_content">
	<div class="tools">
	<p class="keywordText" id="searchResultTips">
	<asp:Literal ID="msgSearchResult" runat="server"></asp:Literal>
	</p>
	<p class="fn">	
		<span class="page" id="resourceType">类型<em></em></span>
		<span  class="page" id="pageSize" >每页<em></em></span>   
	</p>
    </div>
    <script language="javascript" type="text/javascript">
function ResetPageCount(){
//alert("ResetPageCount");
}
function ResetResourceType(){
//alert("ResetResourceType");
}
 var selectPageSize=new select({
		"id":"pageSize"
		,"width":"36"
		,"text":["20张","40张","60张","80张","100张"]
	    ,"fn":"ResetPageCount"
	});
	 var selectResourceType=new select({
		"id":"resourceType"
		,"width":"36"
		,"text":["所有","图片","视频","文档","其它"]
	    ,"fn":"ResetResourceType"
	});
	 selectPageSize.newData("20");    
	 selectResourceType.newData("0");    
	
</script>
<script language="javascript" type="text/javascript">
function QJDetail(itemid)
{
    window.open("Modules/Video/Detail.aspx?ItemID="+itemid);
}

</script>
	<div class="dir">
	    <span><asp:Label ID="lb_ResultCount" runat="server"></asp:Label> </span> 	
    </div>
	    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
	        <ContentTemplate>
                <uc1:DataResource ID="DataResource1" ShowDownload="true" ShowPreview="true" ShowFavor="true" ShowEdit="true" runat="server" />  
                <div style="text-align: right; width: 100%">
                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" 
                TextBeforePageIndexBox="转到: " HorizontalAlign="right" PageSize="12" 
                EnableTheming="true" CssClass="Pager_Number" ShowPrevNext="false" 
                            onpagechanging="AspNetPager1_PageChanging"  >
                    </webdiyer:AspNetPager>
                </div>
            </ContentTemplate>

	    </asp:UpdatePanel>     
</div>
</asp:Content>
