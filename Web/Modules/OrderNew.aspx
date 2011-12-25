<%@ Page Language="C#" EnableSessionState="True" AutoEventWireup="true" CodeBehind="OrderNew.aspx.cs" Inherits="WebUI.Modules.OrderNew" %>
<%@ Register Src="../UserControls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server" >
    <title>申请订单</title>

    <style type="text/css" >
    body{margin-left:auto;margin-right:auto;}
    body{font-size:12px; background-color:#ffffff}    
    .orderTable{border:1px solid #dddddd}
    .l{padding-left:5px;}
    .h3{font-size:14px;line-height:25px;}
    td,textarea{font-size:12px;}
    a{color:#333333}
    #tbContent{border:1px solid #dddddd}
    </style>
    <script src="../UI/Js/js.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
    function orderOk()
    {
        var myxhr = new xmlHttpObjectError("send");
        if(myxhr){
            try{
                myxhr.doContent("type=orderNew");
            }
            catch(e){
                alert("Can't cannect to server:\n"+e.toString());
            }
        }
        alert("申请成功，已经发送给相关人员。");
        parent.closeDialog();
    }

var _type="";
var _focus="";
function selectValue(t,v)
{
	var sHidText=document.getElementById("txtContent").value;
	var _s="";

	if(t=="type")
	{
		_type="工程性质："+v;
	}
	if(t=="focus")
	{
		_focus="主要关注点："+v;	
	}
	_s = _type+"\r\n"+_focus;
	document.getElementById("txtContent").value=_s;
	
}

window.onload=function(){

    document.getElementById("txtTitle").focus();

}
function orderDate()
{
    alert("拍摄完成日期必须晚于当前日期");
}
    </script>
    <link type="text/css" rel="stylesheet" href="/UI/qjcss/vrms.css">
</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
        </cc1:ToolkitScriptManager>
    <div>
    <table width="95%" border="0" align="center" class="orderTable">
      <tr>
        <td colspan="2"><span class="h3 l">订单申请</span></td>
      </tr>
      <tr>
        <td><span class="l">标题：</span></td>
        <td>&nbsp;<asp:TextBox ID="txtTitle" runat="server" Width="200px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtTitle" ErrorMessage="*"></asp:RequiredFieldValidator></td>
      </tr>
      <tr>
        <td width="120"><span class="l">需要完成日期：</span></td>
        <td>&nbsp;<uc2:AjaxCalendar ID="de_Date" runat="server" />
                    </td>
      </tr>

      <tr>
        <td><span class="l">用途：</span></td>
        <td>&nbsp;<asp:DropDownList ID="ddlUsage" runat="server" 
                onselectedindexchanged="ddlUsage_SelectedIndexChanged">
            </asp:DropDownList>
                    </td>
      </tr>
      <tr>
        <td width="120"><span class="l">工程性质：</span></td>
        <td>
        <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal">
        <asp:ListItem Selected="True" Text="完工项目" Value="完工项目"></asp:ListItem>
        <asp:ListItem Selected="false" Text="在建工程" Value="在建工程"></asp:ListItem>
        </asp:RadioButtonList></td>
      </tr>
       <tr>
        <td width="120"><span class="l">主要关注点：</span></td>
        <td><asp:RadioButtonList ID="rblFocus" runat="server" RepeatDirection="Horizontal">
        <asp:ListItem Text="三一员工" Value="三一员工" Selected="True"></asp:ListItem>
        <asp:ListItem Text="三一产品" Value="三一产品"></asp:ListItem>
        <asp:ListItem Text="非三一人物（客户等）" Value="非三一人物（客户等）"></asp:ListItem>
        </asp:RadioButtonList></td>
      </tr>
      <tr>
        <td valign="top"><span class="l">需求描述：</span><br />
        
        </td>
        <td>&nbsp;<asp:TextBox ID="txtContent" 
            runat="server" TextMode="MultiLine" Height="90px" Width="270px"></asp:TextBox>
  
            </td>
      </tr>
      
      <tr>
        <td colspan="2">&nbsp;<asp:Button ID="btnSave" runat="server" Text="提交" onclick="btnSave_Click" />&nbsp;
        <a href="orderSample.gif"  target="_blank" class="l" id="openHelp">查看样例</a></td>
      </tr>
    </table>
    
    </div>
    </form>
</body>
</html>
