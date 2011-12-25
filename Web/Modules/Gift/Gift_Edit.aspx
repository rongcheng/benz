<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_FuncPage.Master" AutoEventWireup="true"
    Codebehind="Gift_Edit.aspx.cs" Inherits="WebUI.Modules.Gift.Gift_Edit" Title="图片库" %>

<%@ Register Src="/UserControls/CatalogTree.ascx" TagName="CatalogTree" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../../UI/Script/jquery-1.3.2.min.js" type="text/javascript"></script>

    <div>
        <table align="center" border="1">
            <tr>
                <th colspan="3" style="text-align: left;">
                    礼品信息编辑</th>
            </tr>
            <tr>
                <td>
                    礼品名称：</td>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox></td>
                <td rowspan="5">
                    <div class="uploadTree" style="float: left; overflow-y: auto; height: 400px; width: 220px;
                        border: 1px #C8C8C8 solid;">
                        <uc1:CatalogTree UploadRight="true" ID="catalogTree" runat="server" TreeNodeType="Leaf">
                        </uc1:CatalogTree>
                    </div>
                </td>
            </tr>
            <tr style="display:none">
                <td >
                    礼品分类：</td>
                <td>
                    <asp:DropDownList ID="ddlGiftType" runat="server"  Visible="false" >
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    库存数量（个）：</td>
                <td>
                    <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td valign="top">
                    标题图片：
                </td>
                <td>
                    <asp:FileUpload ID="fuImage" runat="server" onchange="getFullPath(this);" />
                    <br />
                    <asp:Image ID="imgGift" runat="server" Width="120" Height="120" BorderColor="Silver"
                        BorderStyle="Solid" BorderWidth="2px" />
                </td>
            </tr>
            <tr>
                <td valign="top">
                    备注：</td>
                <td>
                    <asp:TextBox ID="txtRemark" runat="server" Rows="5" TextMode="MultiLine" Width="220px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" OnClientClick="return checkForm();" />
                    <asp:Button ID="btnBack" runat="server" Text="返回" OnClientClick="history.back();return false;" />
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        
        function getFullPath(obj) 
        { 
            var Path = ""; 
            if(obj) 
            { 
                //ie 
                if (window.navigator.userAgent.indexOf("MSIE")>=1) 
                { 
                    obj.select(); 
                    Path = document.selection.createRange().text; 
                } 
                //firefox 
                else if(window.navigator.userAgent.indexOf("Firefox")>=1) 
                { 
                    if(obj.files) 
                    { 
                        Path = obj.files.item(0).getAsDataURL(); 
                    } 
                    Path = obj.value; 
                } 
                Path = obj.value; 
            } 
            document.getElementById('<%=imgGift.ClientID %>').src = Path;
        } 

        function checkForm()
        {
            if($("#<%=txtTitle.ClientID %>").val() == "")
            {
                $("#<%=txtTitle.ClientID %>").select();
                alert("礼品标题不能为空！");
                return false;
            }
            
            if(!isNumber($("#<%=txtQuantity.ClientID %>").val()))
            {
                $("#<%=txtQuantity.ClientID %>").select();
                alert("库存数量必须填正整数！");
                return false;
            }
            
//            if($("#<%=imgGift.ClientID %>").val() == "")
//            {
//                alert("请选择图片！");
//                return false;
//            }
            
            return true;
        }
    </script>

</asp:Content>
