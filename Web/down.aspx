<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="down.aspx.cs" Inherits="WebUI.down" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <style type="text/css">
    html,body{background:#fff;}
    body,textarea{font:12px Tahoma,"宋体",Arial,sans-serif;}
    body{padding:0;margin:0;}
body {text-align:center; _text-align:inherit;}
    </style>
    <script language="javascript" type="text/javascript">
    var t = "<%=total %>";
    window.onload = function(){
        t = 250+t*15;
        window.parent.art.dialog({id:'downRedirect'}).size(300,t);
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="resourceType" runat="server" />
<asp:HiddenField ID="serverFileName" runat="server" />
    <table width="90%" style=" margin-top:10px">
        <tr>
            <td align="right" width="50px">编号：</td>
            <td align="left"><%=filename %></td>
        </tr>  
        <tr>
            <td align="right" width="50px">用途：</td>
            <td align="left"><asp:DropDownList ID="selectUsage" runat="server" Width="120px">
                </asp:DropDownList>
            </td>
        </tr>        
         <tr>
            <td align="right" width="50px">备注：</td>
            <td align="left">
            <asp:TextBox ID="txtenduser" runat="server" TextMode="MultiLine" Rows="5" Width="180px"></asp:TextBox>
                </td>
        </tr>
        <tr>      
            <td></td>
            <td align="left">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr><td align="left" valign="middle" width="8px"><asp:Image ID="Image1" runat="server" ImageUrl="~/images/image.gif" /></td>
            <td align="left" valign="middle" style="padding-left:2px;"><asp:Label ID="lbLabel" runat="server" Text=""></asp:Label></td></tr>
            <tr>
            <td align="left" colspan="2" style="padding-top:3px;"><asp:ImageButton ID="imbtn" runat="server" 
                    ImageUrl="~/images/sucaiwcom25730000sk.gif" oncommand="ImageButton1_Command" />
                <asp:LinkButton ID="lbDownSource" runat="server" Visible="false" Text="" CommandArgument="" CommandName=""   > </asp:LinkButton>   </td>
            </tr>
            </table>                               
            </td>
        </tr>  
        <tr>
            <td colspan="2">
            <asp:Repeater ID="rptDownloadList" runat="server" OnItemCommand="rptDownloadList_ItemCommand">
    <HeaderTemplate>
    <table align="center" style="border:#d4d0c8 dashed 1px;">
    </HeaderTemplate>
    <ItemTemplate>
    <tr>
        <td align="left" valign="middle" width="8px"><asp:Image ID="Image1" runat="server" ImageUrl="~/images/Soft_common.gif" /></td>   
        <td align="left" valign="middle" style="padding-left:3px;">
            <asp:LinkButton ID="lbDown" ToolTip="下载" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FileNameFileLength") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "FileName") %>' CommandName="commandname"></asp:LinkButton>
        </td>
    </tr>  
    </ItemTemplate>
    <FooterTemplate>
    </table>
    </FooterTemplate>
    </asp:Repeater>
            </td>
        </tr>     
    </table>
    
    </form>
</body>
</html>
