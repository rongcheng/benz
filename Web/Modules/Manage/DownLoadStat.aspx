<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_FuncPage.Master" AutoEventWireup="true" CodeBehind="DownLoadStat.aspx.cs" Inherits="WebUI.Modules.Manage.DownLoadStat" Title="无标题页" %>
<%@ Register Src="../../UserControls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
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
#Img1
{
	max-height:50px;
	max-width:50px;
	_width:expression(document.body.clientWidth > 50 ? "50px" : "auto");
}
#imgDetail
{
    position:absolute;
    top:100px;
    left:100px;   
    border:0px solid red; 
    display:none;
}
</style>
<script language="javascript" type="text/javascript">
$(function()
{
    $("body").append("<div id='imgDetail'><img src='' /></div>");
    imgAction();
    reload();
    
})

function imgAction()
{
    $(".img1").mouseover(function(){
        var w=$(this).width();
        var p=$(this).offset();
        
        $("#imgDetail img").attr("src",$(this).attr("src"));
        $("#imgDetail").css("left",p.left+w+5).css("top",p.top).show();
    
    }).mouseout(function(){
        $("#imgDetail").hide();
    
    })
}

function EndRequestHandler() { 

     imgAction();
} 
function reload() { 
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler); 
} 
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="content_manage">
<h4>下载排行</h4>
</div>
<div style="clear:both"></div>
<div class="content_manage_pannel content_manage1">
      
开始日期： &nbsp;&nbsp;
<uc3:AjaxCalendar ID="t_Date" runat="server"></uc3:AjaxCalendar>
 结束日期：
<uc3:AjaxCalendar ID="e_Date" runat="server"></uc3:AjaxCalendar>
<asp:Button ID="searchDate" runat="server" Text="确定" OnClick="searchDate_Click" />
<br />
</div>

<div style="clear:both"></div>
<div class=" content_manage1">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GridView1" CssClass="table" BorderWidth="0" PageSize="12" AllowPaging="True" EmptyDataRowStyle-Font-Bold="true"
                EmptyDataText="没有下载记录" runat="server" AutoGenerateColumns="False" Width="700px"
                OnPageIndexChanging="GridView1_PageIndexChanging"  >
                                   <HeaderStyle HorizontalAlign="Center" BackColor="#666666" BorderColor="#D4D0C8" 
                BorderStyle="Solid" BorderWidth="1px" ForeColor="#fffFFF" Width="200px" Height="30px"></HeaderStyle>
    <RowStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CssClass="both" />
    <AlternatingRowStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CssClass="cell" />        
                <Columns>
                    <asp:TemplateField HeaderText="图片">
                        <ItemTemplate>
                            <img id="Img1" onerror="src='/images/other.jpg'" src="<%# GetImgUrl(Eval("FileName").ToString(), Eval("FileType").ToString(),Eval("folder").ToString(),Eval("ResourceType").ToString())%>" class="img1"/>
                        </ItemTemplate>
                        <ItemStyle Width="99px" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="文件名" DataField="FileName" />
                    <asp:BoundField HeaderText="上传者" DataField="folder"  HeaderStyle-Width="100px" />
                    <asp:BoundField HeaderText="下载次数" DataField="downloadCount"  HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center"/>
                    
                </Columns>
                <EmptyDataRowStyle Font-Bold="True" />
                <PagerStyle BorderStyle="None" CssClass="grvpager" />
            </asp:GridView>
            
            
            
        </ContentTemplate>
    </asp:UpdatePanel>
</div>

</asp:Content>