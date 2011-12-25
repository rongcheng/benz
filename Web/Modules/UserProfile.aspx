<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_MasterPage.Master" AutoEventWireup="true"
    CodeBehind="UserProfile.aspx.cs" Inherits="WebUI.Modules.UserProfile" EnableSessionState="True" %>

<%@ Register Src="../UserControls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="QJ.WebControls" Namespace="QJ.WebControls" TagPrefix="qj" %>
<%@ Register Src="~/UserControls/DataResource.ascx" TagName="DataResource" TagPrefix="uc30" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="header1" ContentPlaceHolderID="head" runat="server">
<title><%=WebUI.UIBiz.CommonInfo.WebSite_Title %></title>   
    <link href="../UI/Css/calendar.css" type="text/css" rel="Stylesheet" />
    <script src="../UI/Js/js.js" type="text/javascript"></script>
    <script src="../UI/Js/validateResource.js" type="text/javascript"></script>

    
    <style type="text/css">
        .ajax__calendar_container
        {
            z-index: 100;
        }
        .downloadLog
        {
        	border:1px solid #cccccc;        	
        	}
        	
        	#Img11 {
max-height:50px;
max-width:50px;
_width:expression(document.body.clientWidth > 50 ? "50px" : "auto");
}
    </style>
<script language="javascript" type="text/javascript">
  function orderDetail(id)
 {
 var _url="OrderDetail.aspx?id="+id;
 art.dialog({id:'orderWnd', title:'订单详情', iframe:_url, width:500, height:320}).close(function(){}); 

 
 }
 function newOrder()
 {
 art.dialog({id:'orderWnd', title:'申请订单', iframe:'OrderNew.aspx?funId=257be0db-3c51-458d-913c-b568bef7b154', width:550, height:350}).close(function(){}); 
 return false
 
 }
 function newMyLightbox()
 {
    var _url = "myLightBox.aspx";
 
    artDialog({id:'mylightbox1', title:'管理我的收藏夹', url:_url, width:530, height:330}).close(function(){});
 }
 function closeDialog()
{

    art.dialog({id:'orderWnd'}).close();

}
function openEdit(time){
    var _url = "CalendarManager.aspx?type=Add&time="+time;
    artDialog({id:'editcalendarId', title:'添加日程信息', url:_url, width:600, height: 300}).close(function(){});
}
function openManager(id, time){
    var _url = "CalendarEdit.aspx?calendarId="+id+"&time="+time;
    artDialog({id:'managerId', title:'编辑当天日程信息', url:_url, width:600, height: 300}).close(function(){});
}
function closeManager(){
    artDialog({id:'managerId'}).close();
}
function openFull(time){
    var _url = "CalendarFull.aspx?time="+time;
    artDialog({id:'fullid', title:''+time+'日程信息', url:_url, width:650, height:300}).close(function(){});
}
function closeEdit(){
    artDialog({id:'editcalendarId'}).close();
}
function Show(year, month, username){
    ShowHead(year, month, username);
    ShowContent(year, month, username);
}
function ShowHead(year, month, username){
    var obj = document.getElementById("<%=head.ClientID %>");
    if(obj.getAttribute('loaded') == 'true'){
        return;
    }
    else{
        obj.innerHMTL = "<img alt='' src='../image/common/loading.gif'/>&nbsp;数据加载中...";
        var myxhr = new xmlHttpCalendar("<%=head.ClientID %>");
        if(myxhr){
            try{
                myxhr.doCalendar("type=Head&year="+year+"&month="+month+"&name="+username);
            }
            catch(e){
                alert("Can't cannect to server:\n"+e.toString());
            }
        }
    }
}
function ShowContent(year, month, username){
    var obj = document.getElementById("<%=content.ClientID %>");
    if(obj.getAttribute('loaded') == 'true'){
        return;
    }
    else{
        obj.innerHMTL = "<img alt='' src='../image/common/loading.gif'/>&nbsp;数据加载中...";
        var myxhr = new xmlHttpCalendar("<%=content.ClientID %>");
        if(myxhr){
            try{
                myxhr.doCalendar("type=Content&year="+year+"&month="+month+"&name="+username);
            }
            catch(e){
                alert("Can't cannect to server:\n"+e.toString());
            }
        }
    }
}
    </script>

    <style type="text/css">
        a.ahead:link{ text-decoration:none;  cursor:pointer; width:100%;}
        a.ahead:visited{ text-decoration:none;  cursor:pointer; width:100%;}
        a.ahead:hover{ text-decoration:none;  cursor:pointer; width:100%;}
        a.ahead:active{ text-decoration:none;  cursor:pointer; width:100%;}
        .head{background-color:#d4d0c8;padding-bottom: 20px;padding-top: 5px;padding-left: 2px;margin-bottom:5px;cursor:pointer; }
        .head tr td strong{text-align: center;color: #FFFFFF;font-weight: 700; padding:2px 2px 2px 2px;}
        .head a{padding-right: 10px;background-color: #d4d0c8; color:#ffffff;}
        .contentcan { margin-left:20px;}
        .contentcan ul{ width:850px; list-style:none;}
        .contentcan ul li { border:#d4d0c8 solid 1px; padding:2px 2px 2px 2px; margin-bottom:2px;}
        
.table{border:0; border-collapse:collapse;}
.manage_table th{ border-bottom:#d4d0c8 solid 1px; border-right:#d4d0c8 solid 1px; margin:0; text-align:center; background-color:#666; color:#fff; font-weight:700; height:30px;}
.cell td{border-collapse:collapse; padding:3px;background-color:#fbfbfb; border:solid 1px white;  margin:0; height:24px; line-height:24px;  overflow:hidden;}
.both td{border-collapse:collapse; padding:3px;background-color:#F3F3F3;  border:solid 1px white; margin:0; height:24px; line-height:24px;  overflow:hidden;}
.grvpager
 {
 	text-align:left;
}.grvpager{	background-color:white!important; color:#333!important; margin-top:3px;}
.grvpager a{background-color:white!important; color:#333!important;}

.grvpager table{width:auto;}
.grvpager table a,.grvpager table span
{
border:1px solid #CCCCCC;
float:left;
height:20px;
line-height:20px;
margin-right:2px;
overflow:hidden;
padding:0 6px;
	}
.grvpager table span
{
background-color:#DDDDDD;
border-color:#CCCCCC;
color:#555555;
font-weight:700;
}


.grvpager table a
{

}
.grvpager table a:hover
{
border:1px solid #000000;

}


.grvpager a,.grvpager span
{
border:1px solid #CCCCCC;
float:left;
height:20px;
line-height:20px;
margin-right:2px;
overflow:hidden;
padding:0 6px;
	}
.grvpager span
{
background-color:#DDDDDD;
border-color:#CCCCCC;
color:#555555;
font-weight:500;
}


.grvpager a
{

}
.grvpager a:hover
{
border:1px solid #000000;

}
.tdcenter
{
	text-align:center;
	
	}
.zipImg
{
    background: url("/images/zip.gif") no-repeat scroll 0 0 transparent;
    padding-left: 29px;
    margin-right:10px;
    float:right;
}
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
    </cc1:ToolkitScriptManager>
                    
                    <span __designer:mapid="a1"> 
                        
</span>
    <cc1:TabContainer ID="profileContainer" runat="server" ActiveTabIndex="1">
        <cc1:TabPanel ID="userInfo" runat="server" HeaderText="我的资料" Font-Size="30px">
            <ContentTemplate>
                
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <ul style="width: 674px" class="userInfo">
                    <li><b>登录名：</b>
                        <asp:Label ID="loginname" runat="server"></asp:Label>
                    </li>
                    <li><b>姓&nbsp;&nbsp;名：</b>
                        <asp:Label ID="name" runat="server"></asp:Label>
                    </li>
                    <li><b>电&nbsp;&nbsp;话：</b>
                        <asp:Label ID="tel" runat="server"></asp:Label>
                    </li>
                    <li><b>邮&nbsp;&nbsp;件：</b>
                        <asp:Label ID="email" runat="server"></asp:Label>
                    </li>
                    <li><b>账户状态：</b>
                        <asp:Label ID="state" runat="server"></asp:Label>
                    </li>
                    <li><b>所属用户组：</b>
                        <asp:Label ID="userGroup" runat="server" Width="381px"></asp:Label>
                    </li>
                </ul>
                
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            </ContentTemplate>
            
        </cc1:TabPanel>
        
        <cc1:TabPanel ID="ligBox" runat="server" HeaderText="收藏夹">
            <HeaderTemplate>
                收藏夹
            </HeaderTemplate>
            <ContentTemplate>
            
                <br />
             &nbsp;请选择收藏夹： <asp:DropDownList ID="ddlMyLightBox" runat="server" Width="100px" 
                    AutoPostBack="True" onselectedindexchanged="ddlMyLightBox_SelectedIndexChanged"></asp:DropDownList>   
             <asp:Button ID="btnMyLightBox" runat="server" Text="查看" 
                    onclick="btnMyLightBox_Click" Visible="False" />  <a href="javascript:newMyLightbox();">管理收藏夹</a> |
                    
                    <span style=" margin-left: 10px;"> <asp:LinkButton runat="server" ID="lbClearMyLightbox" 
        onclick="lbClearMyLightbox_Click" OnClientClick="return confirm('确定清空所选收藏夹吗？');">清空收藏夹</asp:LinkButton>
                        </span>
                <br /><br />
                
      &nbsp;该收藏夹共有<asp:Literal ID="lightboxRowCount" runat="server" Text="0"></asp:Literal>条记录  &nbsp;<asp:HyperLink 
                    ID="MyLightBoxDownload1" runat="server" Text="打包下载"  Target="_blank" 
                    Visible="False" CssClass="zipImg"></asp:HyperLink><br /><br />          
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<br />
                <uc30:DataResource ID="drMyFavorite" ShowPreview="true" ShowFavDelete="true" runat="server">
                    
                </uc30:DataResource>                
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <table width="100%">
                    <tr>
                        <qj:PageBar ID="pageBar" runat="server" ShowInputBox="Always" Width="500px" NextPageText="<img src='/images/next.gif' align='absmiddle' border='0'>"
                            PrevPageText="<img src='/images/prev.gif' align='absmiddle' border='0'>" CssClass=""
                            TextAfterPager="" TextBeforePager=""  >
                        </qj:PageBar>
                        
                        
                        <webdiyer:AspNetPager ID="AspNetPagerLightBox" runat="server" 
                            ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" 
                TextBeforePageIndexBox="转到: " HorizontalAlign="right" PageSize="20" 
                EnableTheming="true" CssClass="Pager_Number" ShowPrevNext="false" 
                            onpagechanging="AspNetPagerLightBox_PageChanging"  >
                </webdiyer:AspNetPager>                
                
                    </tr>
                </table>
                
                &nbsp;&nbsp;<asp:HyperLink ID="MyLightBoxDownload" runat="server" Text="打包下载"  
                    Target="_blank" Visible="False" CssClass="zipImg"></asp:HyperLink>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 

<div style="clear:both"></div>
            </ContentTemplate>
            
        </cc1:TabPanel>
        
        
       
        
             

        
        <cc1:TabPanel ID="tabCalendar" runat="server" HeaderText="日程管理" Font-Size="30px" >
            <ContentTemplate>
                
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <div class="p010">
                    <div class="mt15" style="text-align:center" id="head" runat="server">
                    </div>
                    <DIV class="c">
    </DIV>
                    <div id="list">
	                    <IMG src="../images/list_t.gif" />
	                    <TABLE class="ta" cellSpacing="0" cellPadding="0" width="763" border="0">
            <TBODY>
                <TR>
                    <TD class="ltd b_sol" width="109">
                        星期一</TD>
                    <TD class="ltd b_sol" width="109">
                        星期二</TD>
                    <TD class="ltd b_sol" width="109">
                        星期三</TD>
                    <TD class="ltd b_sol" width="109">
                        星期四</TD>
                    <TD class="ltd b_sol" width="109">
                        星期五</TD>
                    <TD class="ltd b_sol" width="109">
                        星期六</TD>
                    <TD class="ltd b_sol" width="109">
                        星期日</TD>
                </TR>
            </TBODY>
        </TABLE>
	                    <div class="ta3" style="WIDTH: 763px;	POSITION: relative" id="content" runat="server">
	                    </div>
	                    <IMG src="../images/list_b.gif" />
	                </div>
                </div>
                
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            </ContentTemplate>
            
        </cc1:TabPanel>
        
        <cc1:TabPanel ID="tabMyUpload" runat="server" HeaderText="我的上传" Font-Size="30px">
            <ContentTemplate>
                
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <div class="userUpload" id="pMyUpload1" runat="server">
                    <div style="margin-bottom: 10px; margin-top: 10px;">
                        开始日期： &nbsp;<uc2:AjaxCalendar ID="myUpload_StartDate" runat="server" />
                        &nbsp; 结束日期：<uc2:AjaxCalendar ID="myUpload_EndDate" runat="server" />
                        &nbsp; 状态：<asp:DropDownList ID="ddlResoureStatus" runat="server">
                        </asp:DropDownList>
                        <asp:Button ID="btnSearchMyUpload" runat="server" Text="确定" OnClick="btnSearchMyUpload_Click" />
                        <div style="display:inline"><asp:LinkButton ID="lbMyuploadDays" OnClick="lbMyuploadDays_Click" runat="server">进入汇总</asp:LinkButton></div>
                    </div>
                    <uc30:DataResource ID="drMyUpload" ShowEdit="true" ShowTiJiao="true" ShowPreview="true" ShowIsProcessing="true"
                        ShowNotPass="true" ShowStatus="true"  runat="server"></uc30:DataResource>
                    <div style="text-align: right; width: 96%">
                        <qj:PageBar ID="pbMyUpload" runat="server" PageSize="10" Visible="false" ShowInputBox="Always"
                            Width="500" NextPageText="<img src='/images/next.gif' align='absmiddle' border='0'>"
                            PrevPageText="<img src='/images/prev.gif' align='absmiddle' border='0'>" OnPageChanged="pbMyUpload_PageChanged">
                        </qj:PageBar>
                        
                      <table style="float:right"><tr><td></td><td>
                      <webdiyer:AspNetPager ID="AspNetPagerUpload" runat="server" 
                            ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" 
                TextBeforePageIndexBox="转到: " HorizontalAlign="right" PageSize="10" 
                EnableTheming="true" CssClass="Pager_Number" ShowPrevNext="false" 
                            onpagechanging="AspNetPagerUpload_PageChanging"  >
                </webdiyer:AspNetPager>
                      </td></tr></table>
                
                
                
                    </div>
                    <div style="clear:both"></div>
                </div>
                
  <div class="userUpload"  id="pMyUpload2" runat="server">
                
<ul id="stat">
开始日期： &nbsp;<uc2:AjaxCalendar ID="myUploadStat_StartDate" runat="server" />
&nbsp; 结束日期：<uc2:AjaxCalendar ID="myUploadStat_EndDate" runat="server" />
<asp:Button ID="Button1" runat="server" Text="确定" OnClick="btnMyUploadStat_Click" />
<br />                        
<asp:GridView ID="grvMyUploadStat" runat="server" EmptyDataText="没有上传记录" AutoGenerateColumns="False"
    Width="100%" CssClass="table" BorderWidth="0" onrowdatabound="grvMyUploadStat_RowDataBound">
          
     <HeaderStyle HorizontalAlign="Center" BackColor="#666666" BorderColor="#D4D0C8" 
                BorderStyle="Solid" BorderWidth="1px" ForeColor="#fffFFF" Width="200px" Height="30px"></HeaderStyle>
    <RowStyle CssClass="both" />
    <AlternatingRowStyle CssClass="cell"/>
    <Columns>
        <asp:BoundField HeaderText="日期" DataField="StatDate" HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField HeaderText="新上传个数" DataField="NewUpload"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="tdcenter"/>
        <asp:BoundField HeaderText="审批中个数" DataField="IsProcessing" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"/>
        <asp:BoundField HeaderText="已上线个数" DataField="IsPass" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"/>
        <asp:BoundField HeaderText="未通过个数" DataField="NotPass" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"/>
        <asp:BoundField HeaderText="合计" DataField="c" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center"/>
        <asp:TemplateField HeaderText="操作" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
 <asp:LinkButton ID="lbMyuploadStatDays" runat="server" CommandName="" OnCommand="ShowMyUploadStat" CommandArgument='<%#Eval("StatDate") %>'>详情</asp:LinkButton>
 <asp:LinkButton ID="lbMyuploadStatTiJiao" runat="server" CommandName="MyuploadStatTiJiao"  OnCommand="lbMyuploadStatTiJiao_Click"   CommandArgument='<%#Eval("StatDate") %>' OnClientClick="return confirm('你确定全部提交审核吗？')"> 全部提交</asp:LinkButton>              
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <EmptyDataRowStyle Font-Bold="True" />
</asp:GridView>




                </div>
                
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            </ContentTemplate>
            
        </cc1:TabPanel>
        
         <cc1:TabPanel ID="myDownload" runat="server" HeaderText="我的下载">
            <HeaderTemplate>
                我的下载
            </HeaderTemplate>
            <ContentTemplate>
                
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <table>
                    <tr>
                        <td>
                            开始日期： &nbsp;<uc2:AjaxCalendar ID="dt_Date" runat="server" />
                            &nbsp; 结束日期：<uc2:AjaxCalendar ID="de_Date" runat="server" />
                            &nbsp;
                            <asp:Button ID="dsearchDate" runat="server" Text="确定" OnClick="searchDate_Click" />
                        </td>
                    </tr>
                </table>
                
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="dlogList" runat="server" EmptyDataText="没有下载记录" AutoGenerateColumns="False"
                            Width="100%" CssClass="downloadLog">
                            <Columns>
                                <asp:TemplateField HeaderText="资源图片" HeaderStyle-Width="90px" >
                                    <ItemTemplate>
                                        <img onerror="src='/images/other.jpg'" id="Img11" alt="" src="<%# GetImgUrl(Eval("FileName").ToString(), Eval("FileType").ToString(),Eval("folder").ToString())%>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="资源ID" DataField="FileName" />
                                <asp:BoundField HeaderText="下载者" DataField="Username" Visible="false" />
                                <asp:BoundField HeaderText="资源类型" DataField="FileType" HeaderStyle-Width="80px"  />
                                <asp:BoundField HeaderText="下载日期" DataField="Download_Date" HeaderStyle-Width="150px" />
                                <asp:BoundField HeaderText="用途" DataField="usages" />
                                <asp:BoundField HeaderText="最终用户" DataField="EndUser" Visible="false" />
                            </Columns>
                            <EmptyDataRowStyle Font-Bold="True" />
                        </asp:GridView>
                        <qj:PageBar ID="downPageBar" runat="server" ShowInputBox="Always" OnPageChanged="downPageBar_PageChanged"
                            Width="600px" NextPageText="<img src='/images/next.gif' align='absmiddle' border='0'>"
                            PrevPageText="<img src='/images/prev.gif' align='absmiddle' border='0'>"  >
                        </qj:PageBar>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dsearchDate" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            </ContentTemplate>
            
        </cc1:TabPanel>
        
        
           <cc1:TabPanel ID="tabMyOrder" runat="server" HeaderText="我的订单" Font-Size="30px" Visible="false">
            <ContentTemplate>
            
            
                <iframe src="/Modules/OrdersPA/Orders_List.aspx" frameborder="0" style="width: 100%;
                    height: 500px;"></iframe>
                    
                    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <div class="userUpload" style="display:none">
                    <div style="margin-bottom: 10px; margin-top: 10px;">
                        开始日期： &nbsp;<uc2:AjaxCalendar ID="myOrder_StartDate" runat="server" />
                        &nbsp; 结束日期：<uc2:AjaxCalendar ID="myOrder_EndDate" runat="server" />
                        &nbsp;<asp:DropDownList ID="ddlStatus" runat="server">
            </asp:DropDownList>
                        <asp:Button ID="btnSearchMyOrder" runat="server" Text="确定" OnClick="btnSearchMyOrder_Click" />
                        <input type="button" id="OrderNew" onclick="newOrder()" value="申请拍摄" class="btn" />
                    </div>
                    <asp:GridView ID="grvOrders" runat="server" AllowPaging="True" CssClass="table" BorderWidth="0" AutoGenerateColumns="False" OnPageIndexChanging="grvOrders_PageIndexChanging"
                        Width="95%" onrowcommand="grvOrders_RowCommand" onrowdatabound="grvOrders_RowDataBound">
                        <HeaderStyle HorizontalAlign="Center" BackColor="#666666" BorderColor="#D4D0C8" 
                BorderStyle="Solid" BorderWidth="1px" ForeColor="#fffFFF" Width="200px" Height="30px"></HeaderStyle>
    <RowStyle CssClass="both" />
    <AlternatingRowStyle CssClass="cell"/>
                        <Columns>
                            <asp:BoundField DataField="Status" HeaderText="" Visible="true" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Title" HeaderText="标题" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="RequestDate" HeaderText="需要完成日期" DataFormatString="{0:yyyy-MM-dd}"  HeaderStyle-HorizontalAlign="Left"/>
                            <asp:BoundField DataField="AddDate" HeaderText="提交日期"  HeaderStyle-HorizontalAlign="Left"/>
                            <asp:TemplateField HeaderText="状态">
                            <ItemTemplate>
                            <%#GetStatus(Eval("Status").ToString())%>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <a href="javascript:orderDetail('<%#Eval("ID") %>');">查看详情</a> &nbsp;&nbsp;
                                    <asp:LinkButton ID="btnConfirmOrder" runat="server" Text="确认此订单" OnClientClick="return confirm('继续此操作吗？');"  CommandName="confirmOrder" CommandArgument='<%#Eval("ID") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                        <PagerStyle BorderStyle="None" CssClass="grvpager" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#DDDDDD" Font-Bold="True" ForeColor="black" />
                    </asp:GridView>
                </div>
                
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            </ContentTemplate>
            
        </cc1:TabPanel>
        
        
        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="我的短消息"  Visible="false">
            <HeaderTemplate>
                我的短消息
            </HeaderTemplate>
            <ContentTemplate>
            
            
            
            </ContentTemplate>
        </cc1:TabPanel>
       
    </cc1:TabContainer>

    <script language="javascript">
     function downhigh(pic_id,ptype,itemId,folder)
	{
	    var href = "/downRedirect.aspx?FileName=" + pic_id + "&FileType=" + ptype+"&itemId="+itemId+"&folder="+folder;
	    window.open(href,'_blank','width=600,height=500');
	}
	
    var delSource;
    
    function DelItem1(itemId,userId,ev)
    {
         if( !window.confirm('确定从收藏夹中删除吗?')) return;
         delSource = GetEleByEvent(ev);
         WebAppPost("<%= AppRootPath %>/Modules/CallbackExec.aspx?fun=delilb&itemid="+itemId+"&userId=<%= this.CurrentUser.UserId.ToString()%>");
    }
    
    function DelItem(itemId,userId,ev)
    {
        var lightboxid;
         if( !window.confirm('确定从收藏夹中删除吗?')) return;
         
         lightboxid=$("#ctl00_ContentPlaceHolder1_profileContainer_ligBox_ddlMyLightBox").val();
         //alert(lightboxid);
         //return;
         $.ajax({type: "GET", url: "/handlers/lightboxhandler.ashx",data: "action=delfromlightbox&resourceid="+ itemId+"&lightboxid="+lightboxid,
            success: function(msg){
               
                if(msg=="1")
                {
                    alert("删除成功");              
                    $("#imgContainer-"+itemId).hide(500);
                }
                else
                {
                    alert("发生未知错误，删除失败");
                }
            }
        });
    }
    
    
    
    function OnWebRequestCompleted(executor, eventArgs) 
    {
        if(executor.get_responseAvailable()) 
        {
             if(executor.get_responseData()=="true")
             {
                var divTemp = delSource.parentNode;
                
                while( divTemp && divTemp.className != "imgContainer")
                    divTemp = divTemp.parentNode;
               
                var controlDiv = new Sys.UI.Control(divTemp);
                controlDiv.set_visible(false);
                alert('删除成功');
             }
             else
             {
                alert("删除失败");
             }

        }
        else
        {
            if (executor.get_timedOut())
                alert("操作超时");
            else if (executor.get_aborted())
                alert("请求已经终止");
            else
                alert("删除失败");
        }
    }
    </script>

</asp:Content>
