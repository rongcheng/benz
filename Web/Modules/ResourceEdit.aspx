<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResourceEdit.aspx.cs" MasterPageFile="~/MPages/QJ_MasterPage.Master" EnableSessionState="true" Inherits="WebUI.Modules.ResourceEdit" %>

<%@ Register Src="../UserControls/Calendar.ascx" TagName="Calendar" TagPrefix="uc1" %>


<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">

<link href="../UI/swfUpload/progressbar.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../UI/swfUpload/swfupload.js"></script> 
<script type="text/javascript" src="../UI/swfUpload/handlers_attach.js" charset="gb2312" ></script>


<script language="javascript" type="text/javascript">
var swfu; 
window.onload = function () { 

	var settings_object = { 
		upload_url : "upload.aspx", 
		flash_url : "../UI/swfUpload/swfupload.swf", 
		flash9_url : "../UI/swfUpload/swfupload_FP9.swf",
		file_size_limit : "500 MB" ,
		file_upload_limit : 1,
		file_queue_limit : 1, 
		file_types: "*.*",
		file_types_description:"所有类型",
		
		post_params : {
                    "ASPSESSID" : "<%=Session.SessionID %>",
                    "AUTHID" : "<%=(Request.Cookies[FormsAuthentication.FormsCookieName]==null?"":Request.Cookies[FormsAuthentication.FormsCookieName].Value)%>",
                    "uploadType":"attachmentfile",
                    "resourceid":"<%=this.Hidden_ItemId.Value %>",
                    "foldername":"<%=this.Hidden_FolderName.Value %>"
                },

		button_placeholder_id : "spanSWFUploadButton" ,
		button_image_url : "../UI/swfUpload/XPButtonNoText_61x22.png",
		button_width: 61,
		button_height: 22,
		button_text : '<span class="button">浏 览</span>',
		button_text_style : '.button {font-size: 12pt;magin-left:10px; } .buttonSmall { font-size: 10pt; }',
		button_text_top_padding: 1,
		button_text_left_padding: 13,
		
		swfupload_preload_handler :myPreLoad,
		swfupload_load_failed_handler : loadFailed,
		
		file_queued_handler : fileQueued,
		file_queue_error_handler : fileQueueError,
		file_dialog_complete_handler : fileDialogComplete,
		file_dialog_start_handler: fileDialogStart,
		
		upload_progress_handler : uploadProgress,
		upload_error_handler : uploadError,
		upload_success_handler : uploadSuccess,
		upload_complete_handler : uploadComplete,
		
		custom_settings : {
					upload_target : "divFileProgressContainer"
				},
		debug:false
		
	}; 
	swfu = new SWFUpload(settings_object); 

	setSelectFileText("");	
};

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
    line-height:22px;
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
           
                <h2>资源信息编辑</h2><br />
                <asp:Panel ID="imagePanel" runat="server">
                    <table  >
                        <tr>
                            <td align="left" valign="top" style=" width: 430px; ">
                                <img id="imgsrc" alt="" src="" runat="server" visible="false" />
                                <asp:Literal ID="resourceImage" runat="server"></asp:Literal>
                                
                                
                                <div id="uploadAttachment" >
                                
                                <table class="info" width="400" cellspacing="0" cellpadding="0" border="0" style="border-collapse: collapse">
                        <tr>
                            <td width="80px">上传附件：</td>
                            <td>
                                <asp:FileUpload ID="attachFile" runat="server" Visible="false" /><asp:Button ID="btnUpAttach" runat="server"
                                    Text="上传附件" OnClick="btnUpAttach_Click"  Visible="false"/>
<input type="text" readonly="readonly" id="selectedFile" name="selectedFile" size="23" />                                 
<input type="hidden" name="uploadFileName" id="uploadFileName" value="" />  
<asp:HiddenField ID="resourceType" runat="server" />
<asp:HiddenField ID="serverFolder" runat="server" />                              
<div id="spanSWFUploadButton" style="float:left"></div>
<a href="javascript:swfu.startUpload();"  
style="background-image:url(/UI/swfUpload/XPButtonNoText_61x22.png);width:60px;height:20px;border:0px solid red;padding-left:2px;padding-top:3px;display:block;float:right">上传附件</a>
<div id="divFileProgressContainer"></div>
                            </td>
                        </tr>
                    </table>
                                </div>
                                
                            </td>
                            <td style="height: 183px" valign="top">
                                <table id="resourceText" >
                                    <tr align="right">
                                        <td align="right" width="100px">
                                            文件编号：</td>
                                        <td align="left">
                                            <asp:Label ID="lb_ItemSerialNum" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            文件名称：</td>
                                        <td align="left">
                                            <asp:Label ID="lb_FileName" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="height: 22px">
                                            文件标题：</td>
                                        <td align="left" style="height: 22px">
                                            <asp:TextBox ID="txt_Caption" runat="server" Width="400px" ></asp:TextBox><asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator1" runat="server" ErrorMessage="" Text="必填" ControlToValidate="txt_Caption"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <%--<tr>
                                        <td align="right" style="height: 22px">
                                            地点：</td>
                                        <td align="left" style="height: 22px">
                                            <asp:TextBox ID="txt_Address" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="height: 22px">
                                            人物：</td>
                                        <td align="left" style="height: 22px">
                                            <asp:TextBox ID="txt_Character" runat="server"></asp:TextBox></td>
                                    </tr>--%>
                                  <tr>
                                        <td align="right" valign="top">
                                            所属分类：</td>
                                        <td align="left">
                                            <asp:Label ID="lb_catalogs" runat="server"></asp:Label></td>
                                    </tr>  
                                  <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <%--<a href="CatalogSel.aspx?itemid=<%=Hidden_ItemId.Value%>&itemnum=<%=this.hiNum.Value %>"
                                                target="_blank">设置分类</a>--%>
                                                <a href="#" onclick="artDialog({id:'dg_test4330', title:'设置资源分类:<%=this.hiNum.Value %>', url:'CatalogSel.aspx?itemid=<%=Hidden_ItemId.Value%>&itemnum=<%=this.hiNum.Value %>', width:500, height:320}).close(function(){}); return false">设置分类</a>
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
                
                
           
        </div>
  <asp:TextBox ID="TxtKeyword" runat="server" TextMode="MultiLine" Height="50px" Width="400px"  ></asp:TextBox></td>
                                    </tr>
                                    <tr style="display:none">
                                        <td align="right">
                                            有效起始时间：</td>
                                        <td align="left">
                                            <uc1:Calendar ID="Calendar_StartDate" runat="server"></uc1:Calendar>
                                        </td>
                                    </tr>
                                    <tr  style="display:none">
                                        <td align="right">
                                            有效结束时间：</td>
                                        <td align="left">
                                            <uc1:Calendar ID="Calendar_EndDate" runat="server"></uc1:Calendar>
                                        </td>
                                    </tr>
                                    <tr id="tr_shotDate" runat="server"  style="display:none">
                                        <td align="right" style="height: 15px">
                                            拍摄时间：</td>
                                        <td align="left" style="height: 15px">
                                            <uc1:Calendar ID="Calendar_ShotDate" runat="server"></uc1:Calendar>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td align="right">
                                            文件类型：</td>
                                        <td align="left">
                                            <asp:Label ID="lb_ImageType" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr id="tr_Hvsp" runat="server">
                                        <td align="right">
                                            方向：</td>
                                        <td align="left">
                                            <asp:Label ID="lb_Hvsp" runat="server"></asp:Label></td>
                                    </tr>--%>
                                    <tr  style="display:none">
                                        <td align="right">
                                            上传时间：</td>
                                        <td align="left">
                                            <asp:Label ID="lb_uploadDate" runat="server"></asp:Label></td>
                                    </tr>
                                    
                                    
                                    <tr>
                                        <td align="right" style="height: 22px" valign="top">
                                           备注：</td>
                                        <td align="left" style="height: 22px">
                                            <asp:TextBox ID="TxtDescription" runat="server" Height="75px" Width="400px" TextMode="MultiLine"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right"> 附件：</td>
                                        <td>
                                            <asp:GridView  DataKeyNames="attachId,fileName" ID="attList" ShowHeader="False" Width="200px" EmptyDataRowStyle-Font-Bold="true" EmptyDataText="没有附件" runat="server" AutoGenerateColumns="False" OnRowDeleting="attList_RowDeleting" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" OnRowCommand="attList_RowCommand">
                                                <Columns>
                                                    <asp:BoundField DataField="fileName" Visible="false" />
                                                    <asp:TemplateField>
                                                    <ItemTemplate>
                                                    <asp:LinkButton ID="lbDown" runat="server" ToolTip="点击下载" Text='<%# DataBinder.Eval(Container.DataItem, "FileNameFileLength") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "FileName") %>' CommandName="downloadFile"  >
               </asp:LinkButton> 
                                                    </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField DeleteText="&lt;span onclick=&quot;return window.confirm('确定删除吗?')&quot;&gt;删除&lt;/span&gt;" ShowDeleteButton="True" >
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:CommandField>
                                                </Columns>
                                                <EmptyDataRowStyle Font-Bold="True" />
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                               <div style="text-align:left;padding-left:90px"> <asp:Button ID="BtnUpdate" runat="server" Text="修改文件信息" Width="99px" OnClick="BtnUpdate_Click" />
                                <asp:Button ID="btnDel" runat="server" OnClientClick="return window.confirm('你确定要删除吗?')"
                                    OnClick="btnDel_Click" Text="删除文件" />
                                <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label></div>
                                <input id="Hidden_ItemId" type="hidden" runat="server" />
                                <input id="Hidden_FolderName" type="hidden" runat="server" />
                                <input id="hiNum" type="hidden" runat="server" /></td>
                        </tr>
                    </table>
                    
                    <asp:HiddenField ID="hiFolder" runat="server" />
                </asp:Panel>
      
        </div>
    </div>
</asp:Content>