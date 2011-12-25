<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Feature_Info.ascx.cs"
    Inherits="WebUI.UserControls.Feature_Info" %>
<table>
    <thead>
        <tr>
            <td class="feature_title" colspan="2">
                <asp:Literal ID="lblFeatureName" runat="server"></asp:Literal>
            </td>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>
                <asp:Image ID="imgCoverImage" runat="server" Width="120" Height="90" />
            </td>
            <td>
                <asp:Literal ID="lblFeatureDes" runat="server"></asp:Literal>
                <a href="#"><img src="/image/feature/more.jpg"
                    width="31" height="11" /></a>
            </td>
        </tr>
    </tbody>
</table>
