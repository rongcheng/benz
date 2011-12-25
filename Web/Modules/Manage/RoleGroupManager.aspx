<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_FuncPage.Master" Theme="MainSkin" AutoEventWireup="true"
    Codebehind="RoleGroupManager.aspx.cs" Inherits="WebUI.Modules.Manage.RoleGroupManager"
    Title="用户组管理" %>

<%@ Register Src="~/UserControls/InfoShow.ascx" TagName="InfoShow" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
.table{border:0; border-collapse:collapse;}
.manage_table th{ border-bottom:#d4d0c8 solid 1px; border-right:#d4d0c8 solid 1px; margin:0; text-align:center; background-color:#666; color:#fff; font-weight:700; height:30px;}
.cell td{border-collapse:collapse; padding:3px;background-color:#fbfbfb; border:solid 1px white;  margin:0; height:24px; line-height:24px; text-align:left; overflow:hidden;}
.both td{border-collapse:collapse; padding:3px;background-color:#F3F3F3;  border:solid 1px white; margin:0; height:24px; line-height:24px; text-align:left; overflow:hidden;}
.ctr{display:none}
.ptr a{font-size:16px}
</style>

    <script src="../../UI/Script/jquery-1.4.min.js" type="text/javascript"></script>
<script language="javascript">


function showCtr(objTr)
{   
   var flag=$(objTr).html();
   if(flag=="+")
   {
        $(objTr).html("-");
   }
   else
   {
        $(objTr).html("+") 
   }
   $(objTr).parent().parent().nextUntil('.ptr').toggle();
   //alert($(objTr).parent().parent()[0]);
}

</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="content_manage">
<h4>角色管理</h4>
<div class="content_manage_pannel"><p class="save">

                    用户类型：<asp:DropDownList ID="groupDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="groupDDL_SelectedIndexChanged"
                        Visible="False" />
                    <asp:Button ID="btnNewRole" runat="server" Text="新建角色" OnClick="btnNewRole_Click"
                        CausesValidation="False" /></p>
</div>
                        
 <asp:GridView ID="roleList" DataKeyNames="RoleId" runat="server" AutoGenerateColumns="False" 
                     CssClass="table" BorderWidth="0"   
                      OnRowDeleting="roleList_RowDeleting" 
                      OnSelectedIndexChanging="roleList_SelectedIndexChanging" GridLines="None">
  <HeaderStyle HorizontalAlign="Center" BackColor="#666666" BorderColor="#D4D0C8" 
                BorderStyle="Solid" BorderWidth="1px" ForeColor="#fffFFF" Width="200px" Height="30px"></HeaderStyle>
    <RowStyle BorderColor="White" 
                BorderStyle="Solid" BorderWidth="1px" CssClass="both" />
    <AlternatingRowStyle BorderColor="White" 
                BorderStyle="Solid" BorderWidth="1px" CssClass="cell" />
        
                        <Columns>
                            <asp:BoundField DataField="roleName" HeaderText="角色名">
                                <ItemStyle Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Description" HeaderText="描述">
                                <ItemStyle Width="300px" />
                            </asp:BoundField>
                            <asp:CommandField ShowSelectButton="True" SelectText="编辑">
                                <ItemStyle Width="50px" />
                            </asp:CommandField>
                            <asp:CommandField ShowDeleteButton="True" DeleteText="&lt;span onclick='return window.confirm(&quot;用户组的权限和用户将被删除，确定删除吗？&quot;)'&gt;删除&lt;/span&gt;">
                                <ItemStyle Width="50px" />
                            </asp:CommandField>
                            
                            <asp:TemplateField>
                            <ItemTemplate> 
            <a href="#" onclick="art.dialog({id:'dg_test4330', title:'查看用户', iframe:'GetUsersByRole.aspx?roleid=<%# Eval("RoleId") %>', width:500, height:320}).close(function(){}); return false">查看用户</a>
                            
                            </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle BorderStyle="None" CssClass="grvpager" />
                    </asp:GridView>
</div>
         <div style="clear:both"></div>
    <div class="region" style="margin-top:20px; margin-left:10px">
        <table runat="server" id="rolePan" visible="false" width="100%">
            <tr>
                <td>
                    <h3>
                        角色信息
                    </h3>
                </td>
            </tr>
            <tr>
                <td>
                    角色名称：
                    <asp:TextBox ID="txtRoleName" runat="server" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqToRoleName" runat="server" ControlToValidate="txtRoleName"
                        ErrorMessage="*"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>
                    角色描述：
                    <asp:TextBox ID="txtRoleDescri" runat="server" Width="300px" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <h3>
                        授权功能</h3>
                </td>
            </tr>
            <tr>
                <td class="noBg">
                    <asp:DataList ID="functionList" DataKeyField="functionId" runat="server" RepeatColumns="5"
                        RepeatDirection="Horizontal">
                        <ItemTemplate>
                            <asp:CheckBox ID="roleChk" runat="server" Checked='<%#Eval("roleChk") %>' />
                            <%#Eval("FunctionName")%>
                            &nbsp;
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
            <tr>
                <td>
                    <p>
                        <asp:Button ID="btnSaveRoleInfo" runat="server" Text="保存信息" OnClick="btnSaveRoleInfo_Click" /></p>
                </td>
            </tr>
            
            <tr><td>
            <%--这里是分类权限--%>
            <br />
            <table  style=" width:600px; line-height:23px;">
                <tr style=" background-color:#ccc"><td>分类</td><td>禁止图片浏览</td><td>图片上传</td><td>图片下载</td><td>图片编辑</td></tr>
                <asp:Repeater ID="rptCategoryTop" runat="server" 
                    onitemdatabound="rptCategoryTop_ItemDataBound">
                <ItemTemplate>
                <tr style=" background-color:#efefef" class="ptr" ><td> <a href="javascript:void(0)" onclick="showCtr(this)">+</a> <%#Eval("CatalogName") %>
                <asp:HiddenField ID="topCatId" Value=""  runat="server"/>
                </td><td><asp:CheckBox ID="funTopReadChk" runat="server" /></td>
                <td><asp:CheckBox ID="funTopUpChk" runat="server" /></td>                
                <td><asp:CheckBox ID="funTopDownChk" runat="server" /></td>
                <td><asp:CheckBox ID="funTopEditChk" runat="server" /></td></tr>
                
                <asp:Repeater id="rptCategoryChild" runat="server" onitemdatabound="rptCategoryChild_ItemDataBound"> 
      <ItemTemplate> 
      <tr class="ctr"><td> &nbsp;&nbsp;&nbsp;<%#Eval("CatalogName") %><asp:HiddenField ID="childCatId" Value=""  runat="server"/></td>
      <td><asp:CheckBox ID="funChildReadChk" runat="server" /></td><td><asp:CheckBox ID="funChildUpChk" runat="server" /></td>
      
      <td><asp:CheckBox ID="funChildDownChk" runat="server" /></td>
      <td><asp:CheckBox ID="funChildEditChk" runat="server" /></td></tr>       
      </ItemTemplate> 
    </asp:Repeater> 
                </ItemTemplate>
                </asp:Repeater>
            </table>
            <br />
            <asp:Button ID="btnSetRoles" runat="server" Text="保存分类权限" 
                    onclick="btnSetRoles_Click" />
            
            </td></tr>
        </table>
        
    </div>
    <asp:HiddenField ID="hiRoleId" runat="server" />
</asp:Content>
