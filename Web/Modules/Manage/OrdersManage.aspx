<%@ Page Language="C#" EnableSessionState="True"MasterPageFile="~/MPages/QJ_FuncPage.Master" AutoEventWireup="true" CodeBehind="OrdersManage.aspx.cs" Inherits="WebUI.Modules.Manage.OrdersManage" Title="订单管理" %>
<%@ Register Src="../../UserControls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="../../UI/Js/js.js" type="text/javascript"></script>
 <script language="javascript" type="text/javascript"> 
 function orderDetail(id)
 {
 var _url="../OrderDetail.aspx?id="+id;
 art.dialog({id:'orderWnd', title:'订单详情',iframe:_url, width:520, height:350}).close(function(){}); 
 }
 function orderNotPass(id)
 {
  var _url="OrderNotPass.aspx?orderid="+id;
 art.dialog({id:'orderNotPass', title:'退回订单', iframe:_url, width:500, height:150}).close(function(){}); 

 
 }
 function doClose()
 {
    art.dialog({id:'orderNotPass'}).close();
 }
 
 function closeDialog1()
{
    art.dialog({id:'orderWnd'}).close();

}
function selectImage(id)
{
    window.open("/Modules/Manage/ImageFrame.aspx?type=order&featureId="+id);
}



$(function(){
    $(".cssConfirm").click(function(){
        return confirm("确定吗？");
    });
    
});
function sendMail(mail, subject){
    var myxhr = new xmlHttpObjectError("send");
    if(myxhr){
        try{
            myxhr.doError("type=send&mail="+encodeURIComponent(mail)+"&subject="+encodeURIComponent(subject));
        }
        catch(e){
            alert("Can't cannect to server:\n"+e.toString());
        }
    }
}
 </script>
 <style type="text/css">
 .oneline{display:inline}
 .grvpager
 {
 	text-align:left;
}
.table{border:0; border-collapse:collapse;}
.manage_table th{ border-bottom:#d4d0c8 solid 1px; border-right:#d4d0c8 solid 1px; margin:0; text-align:center; background-color:#666; color:#fff; font-weight:700; height:30px;}
.cell td{border-collapse:collapse; padding:3px;background-color:#fbfbfb; border:solid 1px white;  margin:0; height:24px; line-height:24px; text-align:left; overflow:hidden;}
.both td{border-collapse:collapse; padding:3px;background-color:#F3F3F3;  border:solid 1px white; margin:0; height:24px; line-height:24px; text-align:left; overflow:hidden;}

 </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="content_manage">
<h4>订单管理</h4>
</div>
   <div class="content_manage_pannel content_manage1">
        <div style="margin-bottom:10px;margin-top:10px;">开始日期： &nbsp;<uc2:AjaxCalendar ID="myOrder_StartDate" runat="server" />
                &nbsp; 结束日期：<uc2:AjaxCalendar ID="myOrder_EndDate"   runat="server" />
                &nbsp; 
            <asp:DropDownList ID="ddlStatus" runat="server">
            </asp:DropDownList>
                <asp:Button ID="btnSearchMyOrder" runat="server" Text="确定" OnClick="btnSearchMyOrder_Click" />                         
        </div>
   </div>
<div class="content_manage1">
      <asp:GridView ID="grvOrders" runat="server" AllowPaging="True" 
         CssClass="table" BorderWidth="0"
        AutoGenerateColumns="False" 
        OnPageIndexChanging="grvOrders_PageIndexChanging" Width="100%" DataKeyNames="id" 
        onrowcommand="grvOrders_RowCommand" onrowdatabound="grvOrders_RowDataBound">
           
     <HeaderStyle HorizontalAlign="Center" BackColor="#666666" BorderColor="#D4D0C8" 
                BorderStyle="Solid" BorderWidth="1px" ForeColor="#fffFFF" Width="200px" Height="30px"></HeaderStyle>
    <RowStyle BorderColor="White" 
                BorderStyle="Solid" BorderWidth="1px" CssClass="both" />
    <AlternatingRowStyle BorderColor="White" 
                BorderStyle="Solid" BorderWidth="1px" CssClass="cell" />        
        <Columns>
            <asp:BoundField DataField="Status" HeaderText="" Visible="true"  />
            <asp:BoundField DataField="Title" HeaderText="标题" />
            <asp:BoundField DataField="RequestDate" HeaderText="需求日期" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="RequestSize" HeaderText="尺寸" Visible="false" />       
            <asp:BoundField DataField="AddDate" HeaderText="提交日期" />
            <asp:TemplateField HeaderText="状态">
            <ItemTemplate>
            <%#GetStatus(Eval("Status").ToString()) %>
            </ItemTemplate>            
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="操作">
            <ItemTemplate>
            <asp:Panel ID="Panel1" runat="server" CssClass="oneline"><a href="javascript:orderDetail('<%#Eval("ID") %>');" >查看详情</a></asp:Panel><asp:Panel ID="Panel2" runat="server" CssClass="oneline">
            <asp:LinkButton ID="lbIsProcessing" runat="server" CommandName="IsProcessing" CommandArgument='<%#Eval("ID") %>' CssClass="oneline" OnClientClick="return confirm('确定吗？');">受理</asp:LinkButton></asp:Panel>
            <asp:LinkButton ID="lbNotPass" runat="server" CommandName="NotPass" CommandArgument='<%#Eval("ID") %>'  CssClass="oneline" Visible="false">退回</asp:LinkButton>
            <asp:Panel ID="pNotPass" runat="server" CssClass="oneline"><a href="javascript:orderNotPass('<%#Eval("ID") %>');">退回</a></asp:Panel>
            <asp:Panel ID="pImage" runat="server" CssClass="oneline"><a href="javascript:selectImage('<%#Eval("ID") %>');"  >选图</a></asp:Panel>
            <asp:Panel ID="pImageDel" runat="server" CssClass="oneline"><a href="/Modules/Manage/orderdetailpic.aspx?orderid=<%#Eval("ID") %>" target="_blank">管理</a></asp:Panel>
            <asp:LinkButton ID="lbComplete" runat="server" CommandName="Complete" CommandArgument='<%#Eval("ID") %>'  CssClass="oneline" OnClientClick="return confirm('确定吗？');">完成</asp:LinkButton>&nbsp; 
            </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
        <PagerStyle BorderStyle="None" CssClass="grvpager" HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#DDDDDD" Font-Bold="True" ForeColor="black" HorizontalAlign="Left" />
    </asp:GridView>
</div>
<div class="userUpload">           </div>
</asp:Content>
