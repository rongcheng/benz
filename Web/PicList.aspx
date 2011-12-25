<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MPages/MasterPage.Master"
    Codebehind="PicList.aspx.cs" Inherits="WebUI.PicList" %>

<%@ Register Src="UserControls/DeptGridShow.ascx" TagName="DeptGridShow" TagPrefix="uc2" %>
<%@ Register Src="UserControls/DeptTree.ascx" TagName="DeptTree" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="UserControls/CatalogMenu.ascx" TagName="CatalogMenu" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/DataPic.ascx" TagName="DataPic" TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/Search_ReSetPageSize.ascx" TagName="Search_ReSetPageSize"
    TagPrefix="uc5" %>
<%@ Register Assembly="QJ.WebControls" Namespace="QJ.WebControls" TagPrefix="qj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="sidebar">
        <div class="library" id="cataNav" runat="server">
            &nbsp;<asp:Label ID="lb_CatalogNav" runat="server" Font-Bold="true"></asp:Label></div>
        <div class="library" id="deptNav" runat="server">
            &nbsp;
            <asp:Label ID="lbDeptName" runat="server"></asp:Label></div>

        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="library">
                    &nbsp;
                    <asp:Label ID="lb_ResultCount" runat="server"></asp:Label></div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnPostDeptId" EventName="ServerClick" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="library">
            <uc5:Search_ReSetPageSize ID="Search_ReSetPageSize1" runat="server" />
        </div>
        <uc1:CatalogMenu ID="cataMenu" runat="server" />
    </div>
    <div class="main_content">
        <div class="trigger" id="DeptTrigger" runat="server">
            <img src="/images/trigger.gif" width="33" height="67" alt="" />
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="text-align: right; width: 100%">
                    <qj:PageBar ID="PageBar2" runat="server" Visible="true" ShowInputBox="Always" OnPageChanged="PageBar1_PageChanged"
                        Width="500" NextPageText="<img src='/images/next.gif' align='absmiddle' border='0'>">
                    </qj:PageBar>
                </div>
                <uc3:DataPic ID="DataPic1" runat="server"></uc3:DataPic>
                <div style="text-align: right; width: 100%">
                    <qj:PageBar ID="PageBar1" runat="server" PageSize="3" Visible="true" ShowInputBox="Always"
                        OnPageChanged="PageBar1_PageChanged" Width="500" NextPageText="<img src='/images/next.gif' align='absmiddle' border='0'>"
                        PrevPageText="<img src='/images/prev.gif' align='absmiddle' border='0'>">
                    </qj:PageBar>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="chkDept" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnPostDeptId" EventName="ServerClick" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div runat="server" id="selDept" style="display: none; background-color: White; width: 650px;
        height: 500px; overflow-y: scroll">
        <asp:CheckBox ID="chkDept" AutoPostBack="true" Text="按机构浏览" runat="server" OnCheckedChanged="chkDept_CheckedChanged" />
        <asp:LinkButton ID="btnCloseModal" runat="server">关闭</asp:LinkButton>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <uc2:DeptGridShow ID="DeptGridShow2" runat="server"></uc2:DeptGridShow>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnPostDeptId" EventName="ServerClick" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <cc1:ModalPopupExtender ID="ModalDeptSel" runat="server" BackgroundCssClass="modalBackground"
        PopupControlID="selDept" TargetControlID="DeptTrigger" CancelControlID="btnCloseModal">
    </cc1:ModalPopupExtender>

    <script language="javascript">
        function SelDept(deptId,isLeft,name)
        {
            $get('<%=this.hiSelDeptId.ClientID %>').value = deptId;
            $get('<%=this.hiIsSelLeft.ClientID %>').value = isLeft;
            $get('<%=this.hiDeptName.ClientID %>').value = name;
            
            $get('<%=this.lbDeptName.ClientID %>').innerText = name;
            <%= Page.ClientScript.GetPostBackEventReference(this.btnPostDeptId, string.Empty) %>;
        }
    </script>

    <input id="hiSelDeptId" type="hidden" runat="server" />
    <input id="hiIsSelLeft" type="hidden" runat="server" />
    <input id="hiDeptName" type="hidden" runat="server" />
    <input id="btnPostDeptId" type="button" value="btnPostDeptId" style="display: none"
        runat="server" onserverclick="btnPostDeptId_ServerClick" />
</asp:Content>
