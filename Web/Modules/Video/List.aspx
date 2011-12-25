<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MPages/FuncPage.Master" 
Title="视频编辑"
CodeBehind="List.aspx.cs" Inherits="WebUI.Modules.Video.List" %>


<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="../../UserControls/SysFunction.ascx" TagName="SysFunction" TagPrefix="uc2" %>
<%@ Register Src="../../UserControls/CatalogTree.ascx" TagName="CatalogTree" TagPrefix="uc1" %>
<%@ Register Assembly="QJ.WebControls" Namespace="QJ.WebControls" TagPrefix="qj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript" type="text/javascript">
function QJDetail(itemid)
{
    window.open("Detail.aspx?ItemID="+itemid);

}

</script>
    <table width="100%" onkeydown="javascript:return Set_DefaultButton(event,'<%=this.btnSea.ClientID %>')">
        <tr>
            <td>
                <h3>
                    视频信息编辑</h3>
            </td>
            <td>
                视频编号:
        <asp:TextBox ID="txtVideoSN" runat="server"></asp:TextBox><asp:Button ID="btnSea" runat="server" Text="搜索" OnClick="btnSea_Click" />
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
                                        <asp:DataList ID="videoList" BackColor="#FFFFFF" runat="server" RepeatColumns="3"
                                            RepeatDirection="Horizontal" EnableViewState="true" ItemStyle-VerticalAlign="bottom">
                                            <ItemTemplate>
                                                <div class="imgContainer">
                                                    <div>
                                                        <a href="Detail.aspx?ItemID=<%# Convert.ToString(Eval("Id"))%>" target="_blank">
                                                           <%-- <img id="Img1" border="0" alt="" src="<%# GetImgUrl(  Convert.ToString(Eval("FilePath")),Convert.ToString(Eval("ItemSerialNumber")),Convert.ToInt32(Eval("Status")))%>" />--%>
                                                         
                    <object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0' width='170' height='128' id='GettyFilm' align='middle'>
                        <param name='allowScriptAccess' value='always' />
                        <param name='allowFullScreen' value='false' />
                        <param name='movie' value='../../UI/flash/QJFilm.swf' />
                        <param name='quality' value='autohigh' />
                        <param name='bgcolor' value='#ffffff' />
                        <param name='wmode' value='opaque' />
                        <param name='FlashVars' value='videoUrl=<%# GetFlvUrl(  Convert.ToString(Eval("FilePath")),Convert.ToString(Eval("ItemSerialNumber")),Convert.ToInt32(Eval("Status")))%>&imgUrl=<%# GetImgUrl(  Convert.ToString(Eval("FilePath")),Convert.ToString(Eval("ItemSerialNumber")),Convert.ToInt32(Eval("Status")))%>&SerailNumber=<%# Convert.ToString(Eval("Id"))%>' />
                        <embed src='../../UI/flash/qjFilm.swf' quality='autohigh' bgcolor='#ffffff' width='170' height='128' wmode='opaque' flashvars='videoUrl=<%# GetFlvUrl(  Convert.ToString(Eval("FilePath")),Convert.ToString(Eval("ItemSerialNumber")),Convert.ToInt32(Eval("Status")))%>&imgUrl=<%# GetImgUrl(  Convert.ToString(Eval("FilePath")),Convert.ToString(Eval("ItemSerialNumber")),Convert.ToInt32(Eval("Status")))%>'
                            name='GettyFilm' align='middle' allowscriptaccess='always' allowfullscreen='false' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' />
                    </object>
                
                                                        </a>
                                                    </div>
                                                    <div class="pic_info">
                                                        <p class="first">
                                                            <em>
                                                                <asp:Literal ID="Literal1" runat="server" Text='<%#  Eval("ItemSerialNumber").ToString() %>'></asp:Literal>
                                                            </em>
                                                        </p>
                                                        <p>
                                                            <a class="l" href="<%=AppRootPath %>/Modules/UpdateImage.aspx?itemId=<%# Eval("Id").ToString() %>"
                                                                target="_blank">编辑</a>
                                                        </p>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                        <asp:HiddenField ID="previousCataId" Value="" runat="server" />
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
