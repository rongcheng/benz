<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="downLoadManager.ascx.cs" Inherits="WebUI.UserControls.downLoadManager" %>

<script type="text/javascript">
function validateCustomer()
{
    var beginDate;
    var endDate;
    beginDate = document.getElementById('downLoadManager1$beginDate').value;
    //alert(name);
    endDate = document.getElementById('downLoadManager1$endDate').value;
    //alert(password);
    if(beginDate == "")
    {
        alert('请输入开始日期！');
        return false;
    }
    if(endDate == "")
    {
        alert('请输入结束日期！');
        return false;
    }
    return true;
}

</script>


用户 
<asp:DropDownList ID="downloadUser" runat="server">
    
</asp:DropDownList>

下载日期 <asp:TextBox ID="beginDate" runat="server"></asp:TextBox>--
<asp:TextBox ID="endDate" runat="server"></asp:TextBox>

<asp:HiddenField ID="begin" runat="server" />
<asp:HiddenField ID="end" runat="server" />

<asp:Button ID="searchDate" runat="server" Text="确定" OnClick="searchDate_Click" OnClientClick="return validateCustomer();" />
