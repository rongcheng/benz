<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClearResources.aspx.cs" Inherits="WebUI.Modules.Manage.ClearResources" MasterPageFile="~/MPages/QJ_FuncPage.Master" Title="图片清理" %>

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
}
#imgDetail
{
    position:absolute;
    top:100px;
    left:100px;   
    border:0px solid red; 
    display:none;
}
.btnDel
{display:inline-block;text-align:center;line-height:23px;margin-top:5px;float:right}
</style>
<script language="javascript" type="text/javascript">
var bChecked=false;
function doSelectAll()
{
   $("input[type=checkbox]").attr("checked",!bChecked);
   //$("input[name=selectAll]").attr("checked",!bChecked);
   bChecked=!bChecked;
}
function doBatchDel()
{
   var obj=$("input[name=chbResourceID][checked]");
   var icount=obj.size();
   if(icount==0)
   {
        alert("请至少选择一项进行操作");
        return false;
   }
   else
   {
        var s="";
        $("input[name=chbResourceID][checked]").each(function(data){           
            s=s+this.value;
            if(data<icount-1)
            {
                s=s+",";
            }
        });
       $("#<%=chbIds.ClientID %>").val(s);
       //alert(s);
       return confirm("确定要删除这些资源吗？");
   }
   return true;
}


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
<h4>图片清理</h4>   </div><br />&nbsp;<br />
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
            <td height="10px"></td>
        </tr>
        <tr>
            <td align="center">
               
                        <asp:GridView CssClass="table" BorderWidth="0" ID="GridView1"  EmptyDataRowStyle-Font-Bold="true"
                            EmptyDataText="没有记录" runat="server" AutoGenerateColumns="False" Width="730px"
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
                                        <a href="/picdetail.aspx?itemid=<%#Eval("id") %>" target="_blank"><img id="Img1" onerror="src='/images/other.jpg'" src="<%# GetImgUrl(Eval("ServerFileName").ToString(),Eval("ServerFolderName").ToString())%>" class="img1"/></a>
                                    </ItemTemplate>
                                    <ItemStyle Width="79px" />
                                </asp:TemplateField>
                                
                                <asp:BoundField HeaderText="上传者" DataField="UploadUser" />                               
                                <asp:BoundField HeaderText="上传日期" DataField="UploadDate"  DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                                <asp:BoundField HeaderText="审核者" DataField="ValidateUser" HeaderStyle-Width="100px" />
                                <asp:BoundField HeaderText="审核日期" DataField="ValidateDate"  DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                                <asp:BoundField HeaderText="拒绝原因" DataField="Reason"   />
                                <asp:TemplateField HeaderText="">
                                <HeaderTemplate><input type="checkbox" name="selectAll" id="Checkbox1" onclick="doSelectAll()"/> 
                                </HeaderTemplate>
                                <ItemTemplate>
                                <input type="checkbox" name="chbResourceID" value="<%#Eval("ID") %>" id="chb" />
                                </ItemTemplate>                                
                                </asp:TemplateField>                                
                            </Columns>      
                                            
                        </asp:GridView>
 <webdiyer:AspNetPager ID="AspNetPager1" runat="server" 
                            ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" 
                TextBeforePageIndexBox="转到: " HorizontalAlign="right" PageSize="10" 
                EnableTheming="true" CssClass="Pager_Number" ShowPrevNext="false" 
                            onpagechanging="AspNetPager1_PageChanging"  >
</webdiyer:AspNetPager> 

<div class="btnDel">
<asp:HiddenField ID="chbIds" runat="server" />
<asp:Button ID="btnDelete" runat="server" Text="删除" CssClass="btnDel" 
        OnClientClick="return doBatchDel();" onclick="btnDelete_Click"   />
</div>                                           


                        &nbsp;                       


            </td>
        </tr>
    </table>
</asp:Content>
