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
        alert('�����뿪ʼ���ڣ�');
        return false;
    }
    if(endDate == "")
    {
        alert('������������ڣ�');
        return false;
    }
    return true;
}

</script>


�û� 
<asp:DropDownList ID="downloadUser" runat="server">
    
</asp:DropDownList>

�������� <asp:TextBox ID="beginDate" runat="server"></asp:TextBox>--
<asp:TextBox ID="endDate" runat="server"></asp:TextBox>

<asp:HiddenField ID="begin" runat="server" />
<asp:HiddenField ID="end" runat="server" />

<asp:Button ID="searchDate" runat="server" Text="ȷ��" OnClick="searchDate_Click" OnClientClick="return validateCustomer();" />
