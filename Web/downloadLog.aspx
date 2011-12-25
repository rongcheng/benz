<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    Codebehind="downloadLog.aspx.cs" Inherits="WebUI.downloadLog" %>

<%@ Register Src="~/UserControls/Calendar.ascx" TagName="Calendar" TagPrefix="uc1" %>
<%@ Register Assembly="QJ.WebControls" Namespace="QJ.WebControls" TagPrefix="qj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper">
        <div id="content">
            <h2>
                我的下载</h2>
            <div id="downloadLog" style="height: 400px">
                <table>
                    <tr>
                        <td>
                            开始日期：
                            <uc1:Calendar ID="t_Date" runat="server"></uc1:Calendar>
                            &nbsp; -- 结束日期：
                            <uc1:Calendar ID="e_Date" runat="server"></uc1:Calendar>
                            <asp:Button ID="searchDate" runat="server" Text="确定" OnClick="searchDate_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:GridView ID="GridView1" runat="server" EmptyDataRowStyle-Font-Bold="true" EmptyDataText="没有下载记录"
                    AutoGenerateColumns="False" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="图片">
                            <ItemTemplate>
                                <img id="Img1" alt="" src="<%# GetImgUrl(Convert.ToString(Eval("FileName")),Convert.ToString(Eval("FileType")))%>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="图片ID" DataField="FileName" />
                        <asp:BoundField HeaderText="下载者" DataField="Username" />
                        <asp:BoundField HeaderText="图片类型" DataField="FileType" />
                        <asp:BoundField HeaderText="下载日期" DataField="Download_Date" />
                        <asp:BoundField HeaderText="用途" DataField="usages" />
                        <asp:BoundField HeaderText="最终用户" DataField="EndUser" />
                    </Columns>
                </asp:GridView>
                <qj:PageBar ID="PageBar1" runat="server" Visible="true" ShowInputBox="Always" OnPageChanged="PageBar1_PageChanged"
                    Width="600" NextPageText="<img src='/images/next.gif' align='absmiddle' border='0'>"
                    PrevPageText="<img src='/images/prev.gif' align='absmiddle' border='0'>">
                </qj:PageBar>
            </div>
        </div>
</asp:Content>
