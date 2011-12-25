<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="uploadLog.aspx.cs" Inherits="WebUI.Modules.Manage.uploadLog"
Title="上传日志" MasterPageFile="~/MPages/QJ_FuncPage.Master" %>


<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="../../UserControls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../UserControls/SysFunction.ascx" TagName="SysFunction" TagPrefix="uc2" %>
<%@ Register Assembly="QJ.WebControls" Namespace="QJ.WebControls" TagPrefix="qj" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
	position:relative;
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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
             <div class="content_manage" style="border:0px solid red; margin-bottom:20px;">
<h4>上传日志</h4>   </div><br />&nbsp;<br />
    <table class="searchTit" style="margin-left:10px;">
        <tr>
            <td>
                用户登录ID：<asp:TextBox ID="txtLoginName" runat="server" Width="80px"></asp:TextBox>
                开始日期： &nbsp;&nbsp;
                <uc3:AjaxCalendar ID="t_Date" runat="server"></uc3:AjaxCalendar>
                -- 结束日期：
                <uc3:AjaxCalendar ID="e_Date" runat="server"></uc3:AjaxCalendar>
                <asp:Button ID="searchDate" runat="server" Text="确定" OnClick="searchDate_Click" />
            </td>
        </tr>
        <tr>
            <td height="10px">
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView CssClass="table" BorderWidth="0" ID="GridView1"  EmptyDataRowStyle-Font-Bold="true"
                            EmptyDataText="没有上传记录" runat="server" AutoGenerateColumns="False" Width="730px"
                           >
    <HeaderStyle HorizontalAlign="left" BackColor="#666666" BorderColor="#D4D0C8" 
                BorderStyle="Solid" BorderWidth="1px" ForeColor="#fffFFF" Width="200px" Height="30px"></HeaderStyle>
    <RowStyle BorderColor="White" 
                BorderStyle="Solid" BorderWidth="1px" CssClass="both"/>
    <AlternatingRowStyle BorderColor="White" 
                BorderStyle="Solid" BorderWidth="1px" CssClass="cell" />
        
                            <Columns>
                                <asp:TemplateField HeaderText="图片">
                                    <ItemTemplate>
                                        <a href="/picdetail.aspx?itemid=<%#Eval("id") %>" target="_blank"><img id="Img1" onerror="src='/images/other.jpg'" src="<%# GetImgUrl(Eval("ItemSerialNumber").ToString(),Eval("ServerFileName").ToString(),Eval("ServerFolderName").ToString(),Eval("ResourceType").ToString())%>" class="img1"/></a>
                                    </ItemTemplate>
                                    <ItemStyle Width="99px" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="文件标题" DataField="Caption" HeaderStyle-Width="200px" />
                                <asp:BoundField HeaderText="上传者" DataField="ServerFolderName" />                               
                                <asp:BoundField HeaderText="上传日期" DataField="UploadDate"  DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                             
                                <asp:TemplateField HeaderText="文件大小">
                                <ItemTemplate>
                                <%#QJVRMS.Common.Tool.toFileSize(Convert.ToInt64(Eval("FileSize").ToString())) %>
                                </ItemTemplate>                                
                                </asp:TemplateField>                                
                            </Columns>                           
                        </asp:GridView>
                        
 <webdiyer:AspNetPager ID="AspNetPager1" runat="server" 
                            ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" 
                TextBeforePageIndexBox="转到: " HorizontalAlign="right" PageSize="20" 
                EnableTheming="true" CssClass="Pager_Number" ShowPrevNext="false" 
                            onpagechanging="AspNetPager1_PageChanging"  >
                </webdiyer:AspNetPager> 
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    
</asp:Content>
