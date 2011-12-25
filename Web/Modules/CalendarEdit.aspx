<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalendarEdit.aspx.cs" Inherits="WebUI.Modules.CalendarEdit" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register src="../UserControls/AjaxCalendar.ascx" tagname="AjaxCalendar" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <script src="../UI/Js/js.js" type="text/javascript"></script>
    <style type="text/css">
    BODY {	FONT-SIZE: 12px;	BACKGROUND: #fff;	FONT-FAMILY: 'lucida grande',tahoma,helvetica,arial,'bitstream vera sans',sans-serif;	TEXT-ALIGN: center;FONT-FAMILY: 'Arial'}
    </style>
    <script language="javascript" type="text/javascript">
function Juage(){
    var theme = document.getElementById("txtTheme");
    var site = document.getElementById("txtSite");
    var label = document.getElementById("ddlLabel");
    var dTime = document.getElementById("ddlDTime");
    var eTime = document.getElementById("ddlETime");
    var sDate = document.getElementById("AjaxCalendarS_txtDate").value;
    var content = document.getElementById("txtContent");
    
    if(theme.value == ""){
        alert("主题不能为空");
        theme.focus();
        return false;
    }
    var d = dTime.options[dTime.selectedIndex].value;
    var e = eTime.options[eTime.selectedIndex].value;
    var i = comptime(d.split(':')[0], d.split(':')[1], e.split(':')[0], e.split(':')[1]);
    if(i <= 0){
        alert("开始时间不能大于结束时间");
        return false;
    }
    if(content.value == ""){
        alert("内容不能为空");
        content.focus();
        return false;
    }
    
    return true;
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
    function comptime(dh, dm, eh, em){
        var d = new Date(2000,01,01,dh,dm,00);
        var e = new Date(2000,01,01,eh,em,00);
        
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
    
    function OnShow(type){
        if(type == "look"){
            document.getElementById("look").style.display = "none";
            document.getElementById("edit").style.display = "";
        }
        if(type == "edit"){
            document.getElementById("look").style.display = "";
            document.getElementById("edit").style.display = "none";
        }
    }
    function add(time, username, id){
        if(window.parent){
            window.parent.ShowSingle(time, username, id);
        }
    }
    window.onload=function(){

    document.getElementById("txtTheme").focus();

}

</script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
    </asp:ScriptManager>
    <input type="hidden" id="hiddentime" value="<%=time %>" />
    <input type="hidden" id="hiddenid" value="<%=id %>" />
    <input type="hidden" id="hiddenname" value="<%=name %>" />
    <input type="hidden" id="hiddenparam" value="<%=param %>" />
    <div id="look"  style="padding-top:30px;">
        <table width="95%">
        <tr>
            <td align="left" style="width:300px;">
                主题：<asp:Label ID="lbTheme" runat="server" Text=""></asp:Label></td>
            <td align="left">
                地点：<asp:Label ID="lbSite" runat="server" Text=""></asp:Label>
            </td>
        </tr> 
        <tr>
            <td align="left">
                标签：<asp:Label ID="lbLabel" runat="server" Text=""></asp:Label>
            </td>
            <td align="left">
                状态：<asp:Label ID="lbState" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                开始时间：
                <asp:Label ID="lbSdate" runat="server" Text=""></asp:Label>
                &nbsp;&nbsp;
                结束时间：
                <asp:Label ID="lbEdate" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                内容：
                <asp:Label ID="lbContent" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <a href="javascript:OnShow('look')">编辑</a>
                &nbsp;&nbsp;
                <a href="javascript:DeleteCalendar('<%=calendarId %>')">删除</a>
            </td>
        </tr>
    </table>
    </div>
    <div id="edit" style="display:none;">
        <table width="100%">
        <tr>
            <td style="width:100px;" align="right">
                主题：</td>
            <td colspan="3" align="left">
                <asp:TextBox ID="txtTheme" runat="server" 
                    style="margin-left: 0px; width:400px;" Width="20px" MaxLength="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:100px;" align="right">
                地点：</td>
            <td width="600px" align="left">
                <asp:TextBox ID="txtSite" runat="server"  Width="200px" MaxLength="30"></asp:TextBox>
            &nbsp;&nbsp;标签：<asp:DropDownList ID="ddlLabel" runat="server">
                    <asp:ListItem>无</asp:ListItem>
                    <asp:ListItem>重要</asp:ListItem>
                    <asp:ListItem>商务</asp:ListItem>
                    <asp:ListItem>个人</asp:ListItem>
                    <asp:ListItem>假期</asp:ListItem>
                    <asp:ListItem>必须出席</asp:ListItem>
                    <asp:ListItem>需要出差</asp:ListItem>
                    <asp:ListItem>需要准备</asp:ListItem>
                </asp:DropDownList></td>

        </tr>
        <tr>
            <td style="width:100px;" align="right">
                                开始时间：</td>
            <td align="left">
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
        </tr>
        <tr>
            <td style="width:100px;" align="right">
                结束时间：
            </td>
            <td align="left">
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
        </tr>
        <tr>
            <td style="width:100px;" align="right">
                内容：</td>
            <td align="left">
                <asp:TextBox ID="txtContent" runat="server" Height="111px" TextMode="MultiLine" 
                    Width="352px" MaxLength="150"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td align="left">
                <asp:Button ID="btnSave" OnClientClick="return Juage();" runat="server" onclick="btnSave_Click" Text="保 存" />
                &nbsp;&nbsp;
                <a href="javascript:OnShow('edit')">查看</a>
                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
