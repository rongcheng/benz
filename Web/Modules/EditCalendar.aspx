<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_MasterPage.Master" AutoEventWireup="true" CodeBehind="EditCalendar.aspx.cs" Inherits="WebUI.Modules.EditCalendar" Title="无标题页" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register src="../UserControls/AjaxCalendar.ascx" tagname="AjaxCalendar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script language="javascript" type="text/javascript">
//function Juage(){
//    var theme = document.getElementById("<%=txtTheme.ClientID %>");
//    var site = document.getElementById("<%=txtSite.ClientID %>");
//    var label = document.getElementById("<%=ddlLabel.ClientID %>");
//    var dTime = document.getElementById("<%=ddlDTime.ClientID %>");
//    var eTime = document.getElementById("<%=ddlETime.ClientID %>");
//    var sDate = document.getElementById("ctl00_ContentPlaceHolder1_AjaxCalendarS_txtDate").value;
//    var eDate = document.getElementById("ctl00_ContentPlaceHolder1_AjaxCalendarE_txtDate").value;
//    var content = document.getElementById("<%=txtContent.ClientID %>");
//    
//    if(theme.value == ""){
//        alert("主题不能为空");
//        theme.focus();
//        return false;
//    }
//    if(compdate(sDate, eDate) < 0){
//        alert("开始日期不能大于结束日期");
//        return false;
//    }
//    if(compdate(sDate, eDate) == 0){
//        var d = dTime.options[dTime.selectedIndex].value;
//        var e = eTime.options[eTime.selectedIndex].value;
//        var i = comptime(d.split(':')[0], d.split(':')[1], e.split(':')[0], e.split(':')[1]);
//        if(i <= 0){
//            alert("开始时间不能大于结束时间");
//            return false;
//        }
//    }
//    if(content.value == ""){
//        alert("内容不能为空");
//        content.focus();
//        return false;
//    }
//    
//    return true;
//}
//function compdate(begin, end){
//    var begins = begin.split('-');
//    if(begins.length != 3)
//        begins = begin.split('/');
//    var ends = end.split('-');
//    if(ends.length != 3)
//        ends = end.split('/');
//    var d = new Date(begins[0], begins[1], begins[2], 00, 00, 00);
//    var e = new Date(ends[0], ends[1], ends[2], 00, 00, 00);
//    var a =(e-d)/3600/1000;
//        
//        if(a<0){
//            return -1;
//        }else if (a>0){
//            return 1;
//        }else if (a==0){
//            return 0;
//        }else{
//            return 'exception'
//        }
//}
//    function comptime(dh, dm, eh, em){
//        var d = new Date(2000,01,01,dh,dm,00);
//        var e = new Date(2000,01,01,eh,em,00);
//        
//        var a =(e-d)/3600/1000;
//        
//        if(a<0){
//            return -1;
//        }else if (a>0){
//            return 1;
//        }else if (a==0){
//            return 0;
//        }else{
//            return 'exception'
//        }
//    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
        </asp:ScriptManager>
<div>

    <table width="70%">
        <tr>
            <td style="width:80px" align="right">
                主题：</td>
            <td colspan="3">
                <asp:TextBox ID="txtTheme" runat="server" 
                    style="margin-left: 0px; width:400px;" Width="20px" MaxLength="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:80px" align="right">
                地点：</td>
            <td width="200px">
                <asp:TextBox ID="txtSite" runat="server" Width="180px" MaxLength="30"></asp:TextBox>
            </td>
            <td style="width:80px" align="right">
                标签：</td>
            <td>
                <asp:DropDownList ID="ddlLabel" runat="server">
                    <asp:ListItem>无</asp:ListItem>
                    <asp:ListItem>重要</asp:ListItem>
                    <asp:ListItem>商务</asp:ListItem>
                    <asp:ListItem>个人</asp:ListItem>
                    <asp:ListItem>假期</asp:ListItem>
                    <asp:ListItem>必须出席</asp:ListItem>
                    <asp:ListItem>需要出差</asp:ListItem>
                    <asp:ListItem>需要准备</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width:80px" align="right">
                开始时间：</td>
            <td>
                <uc1:AjaxCalendar ID="AjaxCalendarS" runat="server" />
                <asp:DropDownList ID="ddlDTime" runat="server">
                    <asp:ListItem>0:00</asp:ListItem>
                    <asp:ListItem>0:30</asp:ListItem>
                    <asp:ListItem>1:00</asp:ListItem>
                    <asp:ListItem>1:30</asp:ListItem>
                    <asp:ListItem>2:00</asp:ListItem>
                    <asp:ListItem>2:30</asp:ListItem>
                    <asp:ListItem>3:00</asp:ListItem>
                    <asp:ListItem>3:30</asp:ListItem>
                    <asp:ListItem>4:00</asp:ListItem>
                    <asp:ListItem>4:30</asp:ListItem>
                    <asp:ListItem>5:00</asp:ListItem>
                    <asp:ListItem>5:30</asp:ListItem>
                    <asp:ListItem>6:00</asp:ListItem>
                    <asp:ListItem>6:30</asp:ListItem>
                    <asp:ListItem>7:00</asp:ListItem>
                    <asp:ListItem>7:30</asp:ListItem>
                    <asp:ListItem>8:00</asp:ListItem>
                    <asp:ListItem>8:30</asp:ListItem>
                    <asp:ListItem>9:00</asp:ListItem>
                    <asp:ListItem>9:30</asp:ListItem>
                    <asp:ListItem>10:00</asp:ListItem>
                    <asp:ListItem>10:30</asp:ListItem>
                    <asp:ListItem>11:00</asp:ListItem>
                    <asp:ListItem>11:30</asp:ListItem>
                    <asp:ListItem>12:00</asp:ListItem>
                    <asp:ListItem>12:30</asp:ListItem>
                    <asp:ListItem>13:00</asp:ListItem>
                    <asp:ListItem>13:30</asp:ListItem>
                    <asp:ListItem>14:00</asp:ListItem>
                    <asp:ListItem>14:30</asp:ListItem>
                    <asp:ListItem>15:00</asp:ListItem>
                    <asp:ListItem>15:30</asp:ListItem>
                    <asp:ListItem>16:00</asp:ListItem>
                    <asp:ListItem>16:30</asp:ListItem>
                    <asp:ListItem>17:00</asp:ListItem>
                    <asp:ListItem>17:30</asp:ListItem>
                    <asp:ListItem>18:00</asp:ListItem>
                    <asp:ListItem>18:30</asp:ListItem>
                    <asp:ListItem>19:00</asp:ListItem>
                    <asp:ListItem>19:30</asp:ListItem>
                    <asp:ListItem>20:00</asp:ListItem>
                    <asp:ListItem>20:30</asp:ListItem>
                    <asp:ListItem>21:00</asp:ListItem>
                    <asp:ListItem>21:30</asp:ListItem>
                    <asp:ListItem>22:00</asp:ListItem>
                    <asp:ListItem>22:30</asp:ListItem>
                    <asp:ListItem>23:00</asp:ListItem>
                    <asp:ListItem>23:30</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td colspan="3">  
            </td>
        </tr>
        <tr>
            <td style="width:80px" align="right">
                结束时间：</td>
            <td>
                <uc1:AjaxCalendar ID="AjaxCalendarE" runat="server" />
                <asp:DropDownList ID="ddlETime" runat="server">
                    <asp:ListItem>0:00</asp:ListItem>
                    <asp:ListItem>0:30</asp:ListItem>
                    <asp:ListItem>1:00</asp:ListItem>
                    <asp:ListItem>1:30</asp:ListItem>
                    <asp:ListItem>2:00</asp:ListItem>
                    <asp:ListItem>2:30</asp:ListItem>
                    <asp:ListItem>3:00</asp:ListItem>
                    <asp:ListItem>3:30</asp:ListItem>
                    <asp:ListItem>4:00</asp:ListItem>
                    <asp:ListItem>4:30</asp:ListItem>
                    <asp:ListItem>5:00</asp:ListItem>
                    <asp:ListItem>5:30</asp:ListItem>
                    <asp:ListItem>6:00</asp:ListItem>
                    <asp:ListItem>6:30</asp:ListItem>
                    <asp:ListItem>7:00</asp:ListItem>
                    <asp:ListItem>7:30</asp:ListItem>
                    <asp:ListItem>8:00</asp:ListItem>
                    <asp:ListItem>8:30</asp:ListItem>
                    <asp:ListItem>9:00</asp:ListItem>
                    <asp:ListItem>9:30</asp:ListItem>
                    <asp:ListItem>10:00</asp:ListItem>
                    <asp:ListItem>10:30</asp:ListItem>
                    <asp:ListItem>11:00</asp:ListItem>
                    <asp:ListItem>11:30</asp:ListItem>
                    <asp:ListItem>12:00</asp:ListItem>
                    <asp:ListItem>12:30</asp:ListItem>
                    <asp:ListItem>13:00</asp:ListItem>
                    <asp:ListItem>13:30</asp:ListItem>
                    <asp:ListItem>14:00</asp:ListItem>
                    <asp:ListItem>14:30</asp:ListItem>
                    <asp:ListItem>15:00</asp:ListItem>
                    <asp:ListItem>15:30</asp:ListItem>
                    <asp:ListItem>16:00</asp:ListItem>
                    <asp:ListItem>16:30</asp:ListItem>
                    <asp:ListItem>17:00</asp:ListItem>
                    <asp:ListItem>17:30</asp:ListItem>
                    <asp:ListItem>18:00</asp:ListItem>
                    <asp:ListItem>18:30</asp:ListItem>
                    <asp:ListItem>19:00</asp:ListItem>
                    <asp:ListItem>19:30</asp:ListItem>
                    <asp:ListItem>20:00</asp:ListItem>
                    <asp:ListItem>20:30</asp:ListItem>
                    <asp:ListItem>21:00</asp:ListItem>
                    <asp:ListItem>21:30</asp:ListItem>
                    <asp:ListItem>22:00</asp:ListItem>
                    <asp:ListItem>22:30</asp:ListItem>
                    <asp:ListItem>23:00</asp:ListItem>
                    <asp:ListItem>23:30</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td colspan="3">
                
            </td>
        </tr>
        <tr>
            <td style="width:80px" align="right">
                内容：</td>
            <td colspan="3">
                <asp:TextBox ID="txtContent" runat="server" Height="111px" TextMode="MultiLine" 
                    Width="352px" MaxLength="150"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="btnSave" OnClientClick="return Juage();" runat="server" onclick="btnSave_Click" Text="保 存" />
                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>

</div>
</asp:Content>
