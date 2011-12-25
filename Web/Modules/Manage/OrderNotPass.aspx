<%@ Page Language="C#" EnableSessionState="True" AutoEventWireup="true" CodeBehind="OrderNotPass.aspx.cs" Inherits="WebUI.Modules.Manage.OrderNotPass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    
    <link href="../../UI/qjcss/vrms.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    body,textarea{font-size:12px; background:#FfFfFf;line-height:25px;}
    body,p,ul,li{margin:0;padding:0}
    ul,li{list-style:none}
    .l{width:100px;border:0px solid red;float:left}
    .c{margin:10px 0 0 10px;padding:10px 0 0 10px;border:0px solid red}
    .r{}
    li{clear:both}
    </style>

    <script src="../../UI/Script/jquery-1.2.6.pack.js" type="text/javascript"></script>
    <script src="../../UI/Js/js.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
    function checkValidate()
    {
        var reason=$("#<%=txtReason.ClientID %>").val();
        if( reason=="")
        {
            alert("请注明原因");
            return false;
        }
        return true;
    }
    
    function doClose(mail, subject)
    {  
        var myxhr = new xmlHttpObjectError("send");
        if(myxhr){
            try{
                myxhr.doError("type=send&mail="+encodeURIComponent(mail)+"&subject="+encodeURIComponent(subject));
            }
            catch(e){
                alert("Can't cannect to server:\n"+e.toString());
            }
        }
        parent.doClose();
    }
    window.onload=function(){

    document.getElementById("txtReason").focus();

}
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hItemId" runat="server" />
    <br />
    <table border="0" align="center">
    
    <tr>
    <td valign="top">退回原因：</td>
    <td><asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Height="69px" 
            Width="277px"></asp:TextBox></td>
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
