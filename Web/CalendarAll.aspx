<%@ Page Language="C#" EnableSessionState="True" EnableViewState="true"  MasterPageFile="~/MPages/QJ_MasterPage.Master" AutoEventWireup="true" CodeBehind="CalendarAll.aspx.cs" Inherits="WebUI.CalendarAll" Title="无标题页" %>
<%@ Register src="UserControls/AjaxCalendar.ascx" tagname="AjaxCalendar" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../../UI/Css/feature.css"rel="Stylesheet" type="text/css" />
<script src="UI/Js/js.js" type="text/javascript"></script>
<style type="text/css">
.currentAll{ border-left:#d4d0c8 solid 1px; 
             border-right:#d4d0c8 solid 1px; 
             border-top:#d4d0c8 solid 1px;
              text-align:center; padding:3px 3px 3px 3px; background-color:#d4d0c8; width:200px;}
.nowAll{ border-bottom:#d4d0c8 solid 1px;
         text-align:center; padding:3px 3px 3px 3px;width:200px;
          cursor:pointer;}
.divAll{ border-bottom:#d4d0c8 solid 1px; 
         border-left:#d4d0c8 solid 1px; 
         border-right:#d4d0c8 solid 1px;
          padding:10px 10px 10px 10px;}
btn{text-align:center;display:inline-block;width: 61px;height:21px;line-height:21px;background-image: url(mage/imgDetail/button_bg.gif);margin-right:5px;border:0px solid red;}
</style>
<script language="javascript" type="text/javascript">
function show(type){
    switch(type){
        case "0":
            document.getElementById("td1").className = "currentAll";
            document.getElementById("td2").className = "nowAll";
            document.getElementById("td3").className = "nowAll";
            document.getElementById("divDay").style.display = "";
            document.getElementById("divMonth").style.display = "none";
            document.getElementById("divState").style.display = "none";
        break;
        case "1":
            document.getElementById("td1").className = "nowAll";
            document.getElementById("td2").className = "currentAll";
            document.getElementById("td3").className = "nowAll";
            document.getElementById("divDay").style.display = "none";
            document.getElementById("divMonth").style.display = "";
            document.getElementById("divState").style.display = "none";
        break;
        case "2":
            document.getElementById("td1").className = "nowAll";
            document.getElementById("td2").className = "nowAll";
            document.getElementById("td3").className = "currentAll";
            document.getElementById("divDay").style.display = "none";
            document.getElementById("divMonth").style.display = "none";
            document.getElementById("divState").style.display = "";
        break;
    }
}
function SaveAll(type){
    var monthtime = "";
    var stime = "";
    var etime = "";
    var state = "";
    var creator = document.getElementById("hiddenname").value;
    switch(type){
        case 0://按月查找
        var y = document.getElementById("<%=ddlYear.ClientID %>");
        var year = y.options[y.selectedIndex].value;
        var m = document.getElementById("<%=ddlMonth.ClientID %>");
        var month = m.options[m.selectedIndex].value;
        monthtime = year+"-"+month+"-1";
        break;
        case 1:
        stime = document.getElementById("ctl00_ContentPlaceHolder1_AjaxCalendarS_txtDate").value;
        etime = document.getElementById("ctl00_ContentPlaceHolder1_AjaxCalendarE_txtDate").value; 
        break;
        case 2:
        var s = document.getElementById("selectState");
        state = s.options[s.selectedIndex].value;
        break;
    }
    if(compdate(stime, etime) < 0){
        alert("开始日期不能大于结束日期");
        return false;
    }
 
    var obj = document.getElementById("<%=Content.ClientID %>");
    if(obj.getAttribute('loaded') == 'true'){
        return;
    }
    else{
        obj.innerHMTL = "<img alt='' src='../image/common/loading.gif'/>&nbsp;数据加载中...";
        var myxhr = new xmlHttpCalendar("<%=Content.ClientID %>");
        if(myxhr){
            try{
                myxhr.doShow("type=Save&time="+monthtime+"&stime="+stime+"&etime="+etime+"&state="+state+"&name="+creator+"&t="+type);
            }
            catch(e){
                alert("Can't cannect to server:\n"+e.toString());
            }
        }
    }
}
function SearchPage(monthtime, stime, etime, state, type, size, index){
    var creator = document.getElementById("hiddenname").value;
    var obj = document.getElementById("<%=Content.ClientID %>");
    if(obj.getAttribute('loaded') == 'true'){
        return;
    }
    else{
        obj.innerHMTL = "<img alt='' src='../image/common/loading.gif'/>&nbsp;数据加载中...";
        var myxhr = new xmlHttpCalendar("<%=Content.ClientID %>");
        if(myxhr){
            try{
                myxhr.doShow("type=Page&time="+monthtime+"&stime="+stime+"&etime="+etime+"&state="+state+"&name="+creator+"&t="+type+"&size="+size+"&index="+index);
            }
            catch(e){
                alert("Can't cannect to server:\n"+e.toString());
            }
        }
    }
}
function compdate(begin, end){
    var begins = begin.split('-');
    if(begins.length != 3)
        begins = begin.split('/');
    var ends = end.split('-');
    if(ends.length != 3)
        ends = end.split('/');
    var d = new Date(begins[0], begins[1], begins[2], 00, 00, 00);
    var e = new Date(ends[0], ends[1], ends[2], 00, 00, 00);
    var a =(e-d)/3600/1000;
        
        if(a<0){
            return -1;
        }else if (a>0){
            return 1;
        }else if (a==0){
            return 0;
        }else{
            return 'exception'
        }
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
</cc1:ToolkitScriptManager>
<input type="hidden" id="hiddenname" value="<%=name %>" />
<div id="wm">
    <h4 style="margin-bottom:5px; width:100%;">您的日程信息</h4>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td id="td2" class="currentAll" onclick="show('1')">按月查找</td>
            <td id="td1" class="nowAll" onclick="show('0')">按日期查找</td>
            <td id="td3" class="nowAll" onclick="show('2')">按状态查找</td>      
        </tr>
    </table>
    <div id="divMonth"  class="divAll">
        <asp:DropDownList ID="ddlYear" runat="server">
        </asp:DropDownList>年
        <asp:DropDownList ID="ddlMonth" runat="server">
        </asp:DropDownList>月
        <input type="button" onclick="SaveAll(0)" class="btn" value="查找" />
    </div>
    <div id="divDay" class="divAll" style="display:none;">
        日期：<uc1:AjaxCalendar ID="AjaxCalendarS" runat="server" />到<uc1:AjaxCalendar ID="AjaxCalendarE" runat="server" />
        <input type="button" onclick="SaveAll(1)" class="btn" value="查找" />
    </div>
    <div id="divState" style="display:none;" class="divAll">
        <select id="selectState">
            <option value="2" selected>没有开始</option>
            <option value="0">正在进行</option>
            <option value="1">已过期</option>
        </select>
        <input type="button" onclick="SaveAll(2)" class="btn" value="查找" />
    </div>
    <div id="Content" style="margin-top:3px;" runat="server">
    </div>
</div>
</asp:Content>
