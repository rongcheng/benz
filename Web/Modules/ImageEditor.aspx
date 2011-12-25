<%@ Page Language="C#" MasterPageFile="~/MPages/FuncPage.Master" AutoEventWireup="true"
    Codebehind="ImageEditor.aspx.cs" Inherits="WebUI.Modules.ImageEditor" Title="图片编辑" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="../UserControls/SysFunction.ascx" TagName="SysFunction" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/CatalogTree.ascx" TagName="CatalogTree" TagPrefix="uc1" %>
<%@ Register Assembly="QJ.WebControls" Namespace="QJ.WebControls" TagPrefix="qj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" onkeydown="javascript:return Set_DefaultButton(event,'<%=this.btnSea.ClientID %>')">
        <tr>
            <td>
                <h3>
                    图片信息编辑</h3>
            </td>
            <td>
                图片编号:
        <asp:TextBox ID="txtImageNum" runat="server"></asp:TextBox><asp:Button ID="btnSea" runat="server" Text="搜索" OnClick="btnSea_Click" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td valign="top">
                <div style="float: left; overflow-y: auto; height: 500px; width: 220px; border: 1px #C8C8C8 solid;">
                    <uc1:CatalogTree ID="cataTree" UploadRight="true" runat="server" />
                </div>
            </td>
            <td valign="top">
                <div class="back_holderB">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table style="width: 550px">
                                <tr>
                                    <td>
                                        <asp:DataList ID="imageList" BackColor="#FFFFFF" runat="server" RepeatColumns="3"
                                            RepeatDirection="Horizontal" EnableViewState="true" ItemStyle-VerticalAlign="bottom">
                                            <ItemTemplate>
                                                <div class="imgContainer">
                                                    <div>
                                                        <a href="/PicDetail.aspx?ItemID=<%# Convert.ToString(Eval("ItemId"))%>" target="_blank">
                                                            <img id="Img1" border="0" alt="" src="<%# GetImgUrl(  Convert.ToString(Eval("FolderName")),Convert.ToString(Eval("ItemSerialNum")),Convert.ToString(Eval("ImageType")))%>" />
                                                        </a>
                                                    </div>
                                                    <div class="pic_info">
                                                        <p class="first">
                                                            <em>
                                                                <asp:Literal ID="Literal1" runat="server" Text='<%#  Eval("ItemSerialNum").ToString() %>'></asp:Literal>
                                                            </em>
                                                        </p>
                                                        <p>
                                                            <a class="l" href="<%=AppRootPath %>/Modules/UpdateImage.aspx?itemId=<%# Eval("ItemId").ToString() %>"
                                                                target="_blank">编辑</a>
                                                        </p>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <qj:PageBar ID="pageBar" PageSize="9" runat="server" Visible="true" ShowInputBox="Always"
                                            OnPageChanged="pageBar_PageChanged" Width="500" NextPageText="<img src='/images/next.gif' align='absmiddle' border='0'>"
                                            PrevPageText="<img src='/images/prev.gif' align='absmiddle' border='0'>">
                                        </qj:PageBar>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
