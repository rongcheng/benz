<%@ Page Language="C#" EnableSessionState="True" AutoEventWireup="true" CodeBehind="Validating.aspx.cs" Inherits="WebUI.Modules.Manage.Validating" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
   
    <link href="../../UI/qjcss/vrms.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    body{font-size:12px; background:#FfFfFf;line-height:25px;}
    body,p,ul,li{margin:0;padding:0}
    ul,li{list-style:none}
    .l{width:100px;border:0px solid red;float:left}
    .c{margin:10px 0 0 10px;padding:10px 0 0 10px;border:0px solid red}
    .r{}
    li{clear:both}
    textarea{font-size:12px}
    </style>

    <script src="../../UI/Script/jquery-1.2.6.pack.js" type="text/javascript"></script>
    <script src="../../UI/Js/js.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
    function checkValidate()
    {
        var checkedValue=$("#<%=rbl.ClientID %> :radio[checked]").val();
        var reason=$("#<%=txtReason.ClientID %>").val();
        if(checkedValue=="不通过" && reason=="")
        {
            alert("不通过时，请注明原因");
            return false;
        }
        return true;
    }
    
    function doClose(mail, subject)
    {
        var myxhr = new xmlHttpObjectError("validate");
        if(myxhr){
            try{
                myxhr.doError("type=validate&mail="+encodeURIComponent(mail)+"&subject="+encodeURIComponent(subject));
            }
            catch(e){
                alert("Can't cannect to server:\n"+e.toString());
            }
        }
        parent.closeDialog1();
    }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hItemId" runat="server" />
    <br />
    <table border="0" align="center">
    <tr>
    <td width="100px">审核结果：</td>
    <td><asp:RadioButtonList ID="rbl" runat="server" RepeatDirection="Horizontal" >
        <asp:ListItem Selected="True">通过</asp:ListItem>
        <asp:ListItem>不通过</asp:ListItem>
    
    </asp:RadioButtonList></td>
    </tr>
    <tr>
    <td valign="top">备注：</td>
    <td><asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine"></asp:TextBox></td>
    </tr>
    <tr colspan="2">
    <td>
    <asp:Button ID="btnValidate" runat="server" Text="确定" 
            onclick="btnValidate_Click" OnClientClick="return checkValidate();" />
    </td>
    </tr>
    </table>
    </form>
</body>
</html>
