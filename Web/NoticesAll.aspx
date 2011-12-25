<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_MasterPage.Master" AutoEventWireup="true" CodeBehind="NoticesAll.aspx.cs" Inherits="WebUI.NoticesAll" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../../UI/Css/feature.css"rel="Stylesheet" type="text/css" />
<script src="UI/Js/js.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
var htmlload = "<img alt='' src='image/common/loading.gif'/>&nbsp;数据加载中...";
function GetNoticesPage(userName, size, index){
    document.getElementById("hiddenPage").value = index;
    var obj = document.getElementById("<%=Content.ClientID %>");
    if(obj.getAttribute('loaded') == 'true'){
        return;
    }
    else{
        obj.innerHMTL = htmlload;
        var myxhr = new xmlHttpNotices("<%=Content.ClientID %>");
        if(myxhr){
            try{
                myxhr.doShow("type=Page&name="+userName+"&size="+size+"&index="+index);
            }
            catch(e){
                alert("Can't cannect to server:\n"+e.toString());
            }
        }
    }
}
function OnNotices(id){
    var _url = "NoticesOpen.aspx?noticeId="+id;
    art.dialog({id:'noticesWnd', title:'公告',iframe:_url, width:700, height:350}).close(function(){}); 
 }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<input type="hidden" id="hiddenPage" value="1" />
<div id="wm">
    <h4 style="margin-bottom:5px;">发布公告列表</h4>
    <div id="Content" runat="server" style="margin-top:3px;">
    </div>
</div>
</asp:Content>
