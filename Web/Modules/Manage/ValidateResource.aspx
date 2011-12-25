<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_FuncPage.Master" EnableSessionState="True" AutoEventWireup="true" CodeBehind="ValidateResource.aspx.cs" Inherits="WebUI.Modules.Manage.ValidateResource" Title="审核资源" %>
<%@ Register Src="~/UserControls/DataResource.ascx" TagName="DataResource" TagPrefix="uc30" %>
<%@ Register Src="~/UserControls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="uc2" %>
<%@ Register Assembly="QJ.WebControls" Namespace="QJ.WebControls" TagPrefix="qj" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" language="JavaScript" src="/ui/qjjs/common.js"></script> 
<script language="javascript" type="text/javascript">
var nowIndex=0;
var ItemID="";
function doValidate(itemId)
{
    ItemID = itemId;
    var url1="/Modules/Manage/Validating.aspx?ItemId="+itemId;
    art.dialog({id:'id_validate', title:'资源审核', iframe:url1, width:400, height:180}).close(function(){}); 
}

function closeDialog1()
{
    alert("操作成功");
    art.dialog({id:'id_validate'}).close();
    
    var _arr=ItemID.split(",");
    for(var i=0;i<_arr.length;i++)
    {
        $("#imgContainer-"+_arr[i]).hide(600);
        //$("#imgContainer-"+ItemID[i]).css("border","1px solid red");
        //alert("#imgContainer-"+ItemID);
    }

}
function doBatchValidate()
{
   var obj=$(".resourceShow input[type=checkbox][checked]");
   var _ids="";
   if(obj.size()==0)
   {
        alert("请至少选择一项进行操作");
   }
   else
   {    obj.each(function(){
            _ids+=$(this).val()+","; 
        });        
        ItemID=_ids;
        var url1="/Modules/Manage/Validating.aspx?ItemId="+_ids;
        art.dialog({id:'id_validate', title:'资源审核', iframe:url1, width:400, height:180}).close(function(){}); 


        //alert(_ids);
   }

}
var bChecked=false;
function doSelectAll()
{    
   $(".resourceShow input[type=checkbox]").attr("checked",!bChecked);
   $("input[name=selectAll]").attr("checked",!bChecked);
   bChecked=!bChecked;
}



function ResetPageCount(val){
   //alert(val);
   
   var cookiePage=GetCookie("valiDatePageCount");
   if(cookiePage!=val)
   {
        SetCookie('valiDatePageCount',val);
        //location.href=location.href;
        //$("#ctl00_ContentPlaceHolder1_btnSearchMyUpload").trigger("click");
        document.getElementById("ctl00_ContentPlaceHolder1_btnSearchMyUpload").click();
   }
   //document.forms[0].submit();
   //location.href=location.href;

}

$(function(){

     var selectPageSize=new select({
		    "id":"pageSize"
		    ,"width":"36"
		    ,"text":["20张","40张","60张","80张","100张"]
	        ,"fn":"ResetPageCount"
    });

    selectPageSize.newData("<%=PageBar1.PageSize %>");
    selectPageSize.onClick=function(){
        alert("google");
        }
})
</script>
<script language="javascript" type="text/javascript">
    
    //给Cookie赋值
    function SetCookie(cookieName, cookieData)
    {
        var expires = new Date ();
        expires.setTime(expires.getTime() + 365 * (24 * 60 * 60 * 1000));   
        document.cookie = cookieName + "=" + escape(cookieData) + ";path=/;expires=" + expires.toGMTString();
    }
    function GetCookie(name)
    {
        var dc = document.cookie;
        var prefix = name + "=";
        var begin = dc.indexOf("; " + prefix);
        if (begin == -1)
        {
            begin = dc.indexOf(prefix);
            if (begin != 0) return null;
        }
        else
        {
            begin += 2;
        }
        var end = document.cookie.indexOf(";", begin);
        if (end == -1)
        {
            end = dc.length;
        }
        return unescape(dc.substring(begin + prefix.length, end));
    } 
</script>
<style type="text/css">
.table{border:0; border-collapse:collapse; background:url(../../image/common/table_bg.gif);}
.manage_table th{ border-bottom:#d4d0c8 solid 1px; border-right:#d4d0c8 solid 1px; margin:0; text-align:center; background-color:#666; color:#fff; font-weight:700; height:30px;}
.cell td{border-collapse:collapse; padding:3px; margin:0; height:24px; border:solid 1px white;  line-height:24px; text-align:left; background:url(../../image/common/td_bg.gif) no-repeat 0 100%; overflow:hidden;}
.both td{border-collapse:collapse; padding:3px; margin:0; height:24px; border:solid 1px white; line-height:24px; text-align:left; background:url(../../image/common/td_bg.gif) no-repeat 0 100%; overflow:hidden;}

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:HiddenField ID="hidUserId" runat="server" />

<div class="content_manage">
<h4>审核资源</h4>
</div>
<div class="content_manage_pannel content_manage1" style="float:left; padding-left:10px;height:100%;width:740px">

<div class="userUpload" id="p1" runat="server">
<div style="margin-bottom:10px;margin-top:10px;">开始日期： &nbsp;<uc2:AjaxCalendar ID="myUpload_StartDate" runat="server" />
        &nbsp; 结束日期：<uc2:AjaxCalendar ID="myUpload_EndDate" runat="server" />
        &nbsp; 
        <asp:Button ID="btnSearchMyUpload" runat="server" Text="确定" OnClick="searchDate_Click" />
        <div style="display:inline"><asp:LinkButton ID="lbNewResourceByUser" OnClick="lbNewResourceByUser_Click" runat="server">进入汇总</asp:LinkButton></div>
</div>
      
 <div style="border-bottom:1px solid #ddd;padding-bottom:5px;margin-bottom:5px;text-align:right; position:relative;z-index:99"> 
 
 
 <div class="tools" style="text-align:right;width:99%;" >
 <p class="fn" style="border: 0px solid blue; margin-top: 0px; width: 80px;top:4px;right:130px; position:absolute">
		<span id="pageSize" class="page">每页<em></em></span>  
  </p>
<div style="clear: both; display: inline;"></div>
  
  
  
  
  
  <input type="checkbox" name="selectAll" id="Checkbox1" onclick="doSelectAll()"/> 全选
                <a href="javascript:doBatchValidate();" class="btn" style="display:inline-block;text-align:center" >批量审核</a> 
                
                
                
                
</div>
  
  
  
                </div>       
                              
<uc30:DataResource ID="drMyUpload" ShowCheckBox="true"  ShowPreview="true"  ShowValidate="true" ShowNotPass="true" runat="server"></uc30:DataResource>
<div style="text-align: right; width: 100%">
    <qj:PageBar ID="PageBar1" runat="server" PageSize="12" Visible="true" ShowInputBox="Always"
        Width="500" NextPageText="<img src='/images/next.gif' align='absmiddle' border='0'>"
        PrevPageText="<img src='/images/prev.gif' align='absmiddle' border='0'>"  OnPageChanged="PageBar1_PageChanged" >
    </qj:PageBar>
</div>
 
  <div style="border-top:1px solid #ddd;padding-top:5px;margin-top:10px;text-align:right">  
  
  
  
  <input type="checkbox" name="selectAll" id="selectAdd" onclick="doSelectAll()"/> 全选
                <a href="javascript:doBatchValidate();" class="btn" style="display:inline-block;text-align:center" >批量审核</a> 
                </div>            
                
</div>
<div class="userUpload" id="p2" runat="server" >


<asp:GridView ID="grvNewResource" runat="server" EmptyDataText="没有记录" AutoGenerateColumns="False"
    Width="80%" CssClass="table" BorderWidth="0">
    <HeaderStyle HorizontalAlign="Center" BackColor="#666666" BorderColor="#D4D0C8" 
                BorderStyle="Solid" BorderWidth="1px" ForeColor="#fffFFF" Width="200px" Height="30px"></HeaderStyle>
    <RowStyle CssClass="both" />
    <AlternatingRowStyle CssClass="cell"/>
    <Columns>
        <asp:BoundField HeaderText="用户名" DataField="UserName">
        </asp:BoundField>
        <asp:BoundField HeaderText="新上传个数" DataField="c">
        </asp:BoundField>
        <asp:BoundField HeaderText="最后上传日期" DataField="UploadDate">
        </asp:BoundField>
        <asp:TemplateField HeaderText="操作">
        <ItemTemplate>
            <asp:LinkButton ID="lbNewResource" runat="server" CommandName="" OnCommand="ShowNewResource" CommandArgument='<%#Eval("userid") %>'>详情</asp:LinkButton>
        </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <EmptyDataRowStyle Font-Bold="True" />
</asp:GridView>


</div>
</div>
                
                
                
</asp:Content>
