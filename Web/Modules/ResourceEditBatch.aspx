<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResourceEditBatch.aspx.cs" MasterPageFile="~/MPages/QJ_MasterPage.Master"  Inherits="WebUI.Modules.ResourceEditBatch" %>





<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">



<script language="javascript" type="text/javascript">


var tabCount=6;
var catTabCount=1;
$(function()
{
    tabCount=<%=this.rptKeywordCat.Items.Count  %>;
   for(var i=1;i<=tabCount;i++)
    {
        obj="#key-tab-"+i+"-info";
        $(obj).hide();
    }
    $("#key-tab-1-info").show();
    
}

);

function showKeyTab(id)
{
    var obj="key-tab-"+id;
    for(var i=1;i<=tabCount;i++)
    {
        obj="#key-tab-"+i+"-info";
        sss=".key-tab-"+i;
        
        if(id==i)
        {
            $(obj).fadeIn(1000);
            $(sss).css("color","black");
        }
        else
        {
            $(obj).hide();
            $(sss).css("color","#888888");
        }
    }
}

function addKeyword(str)
{
    var obj=document.getElementById("<%=TxtKeyword.ClientID %>");
    var _txt=obj.value;
    if(_txt.indexOf(str)<0)
    {
        if(_txt.substring(_txt.length-1)!=",")
        {
            document.getElementById("<%=TxtKeyword.ClientID %>").value+=","+str;
        }
        else
        {
            document.getElementById("<%=TxtKeyword.ClientID %>").value+=str+",";
        }
    }
    
    
}


function openCatalog()
    {
        var itemIds="<%=itemIds %>";
        
        artDialog({id:'dg_test4330', title:'设置资源分类:', url:'CatalogSelBatch.aspx?ids='+itemIds, width:500, height:200}).close(function(){}); return false
    }
    
    
    
</script>
<style type="text/css">
.manage
{
    border:0px solid red;
    margin-top:10px;
    margin-left:10px;
    width:100%;
}

#resourceText td
{
    line-height:30px;
}

#uploadAttachment
{
    border:1px solid #aaaaaa;
    padding:5px 5px 5px 5px;
    margin-top:10px;
    margin-right:10px;
    
    background-color:#efefef;
}

#attList td
{
    padding-left:6px;
    
}

#key-1
 {
 	margin-bottom:5px;
 	
 	}
 #key-1 ul
 {	color:Red; }
 #key-1 ul li
 {
 	display:inline;
 	margin-right:2px;
 	background-color:#ddd;
 	padding:3px 10px 3px 10px;
 }

.key-tab-selected
{
	background-color:#3F3F3F;
	color:White;
}
 #key-1 div
 {
 	border:1px solid #ddd;
 	padding:10px;
 	width:425px;
 }

.txtKey
{
	width:425px;
	height:70px;
	
	}
</style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="manage">
        <div id="imageEdit">
           
                <h2>图片信息编辑</h2><br />
               

                                </div>
                                
     
                                <table id="resourceText" >
                     
                                    <tr>
                                        <td align="right" style="height: 22px">
                                            文件标题：</td>
                                        <td align="left" style="height: 22px">
                                            <asp:TextBox ID="txt_Caption" runat="server" Width="400px" ></asp:TextBox><asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator1" runat="server" ErrorMessage="" Text="必填" ControlToValidate="txt_Caption"></asp:RequiredFieldValidator></td>
                                    </tr>
                                   
                                  <tr>
                                        <td align="right" valign="top">
                                            所属分类：</td>
                                        <td align="left"><a href="#" onclick="openCatalog()">设置分类</a>
                                            </td>
                                    </tr>  
                           
                                    <tr>
                                        <td align="right" valign="top">关键字：</td>
                                        <td>
            <div id="key-1">
            <ul>
           
                <asp:Repeater ID="rptKeywordCat" runat="server">
                <ItemTemplate>
                <li><a href="#"   class="key-tab-<%# this.rptKeywordCat.Items.Count + 1 %>"  onclick="showKeyTab(<%# this.rptKeywordCat.Items.Count + 1 %>)"><span><%#Eval("keyword") %></span></a></li>     
                </ItemTemplate>
                </asp:Repeater>
            </ul>
            <asp:Repeater ID="rptKeywordDetail1" runat="server" OnItemDataBound="rptKeywordDetail1_ItemDataBoud">
            <ItemTemplate><div id="key-tab-<%# this.rptKeywordDetail1.Items.Count + 1 %>-info" class="">
                <p>
            <asp:Repeater ID="rptKeywordDetail" runat="server">
                <ItemTemplate>
                
                <a href="javascript:addKeyword('<%#Eval("keyword") %>')"><%#Eval("keyword") %></a>
                 
                </ItemTemplate>
           </asp:Repeater>
           </p>
                </div>
           </ItemTemplate>
           </asp:Repeater>
                
                  <asp:TextBox ID="TxtKeyword" runat="server" TextMode="MultiLine" Height="50px" Width="400px"  ></asp:TextBox>
                  
                  </td>
                  
                    
                                    <tr>
                                        <td align="right" style="height: 22px" valign="top">
                                           备注：</td>
                                        <td align="left" style="height: 22px">
                                            <asp:TextBox ID="TxtDescription" runat="server" Height="75px" Width="400px" TextMode="MultiLine"></asp:TextBox></td>
                                    </tr>
                                    
                  </tr>
                  </table>
           
        </div>
        <br />
   <asp:Button ID="BtnUpdate" runat="server" Text="批量修改文件信息" Width="130px" OnClick="BtnUpdate_Click" />
   
    </div>
</asp:Content>