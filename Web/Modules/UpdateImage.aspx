<%@ Page Language="C#" AutoEventWireup="true" EnableSessionState="True" Codebehind="UpdateImage.aspx.cs" Inherits="WebUI.Modules.UpdateImage" %>

<%@ Register Src="../UserControls/Calendar.ascx" TagName="Calendar" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=WebUI.UIBiz.CommonInfo.WebSite_Title %></title>
    <link href="/UI/css/global.css" rel="stylesheet" type="text/css" />
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
		file_size_limit : "200 MB" ,
		file_upload_limit : 1,
		file_queue_limit : 1, 
		file_types: "*.jpg;*.jpeg;*.gif;*.png;*.psd;*.ai;*.bmp;*.tiff;*.pcx;*.tga;*.exif;*.fpx;*.wmv",
		file_types_description:"所有图片类型",
		
		post_params : {
                    "ASPSESSID" : "<%=Session.SessionID %>",
                    "AUTHID" : "<%=(Request.Cookies[FormsAuthentication.FormsCookieName]==null?"":Request.Cookies[FormsAuthentication.FormsCookieName].Value)%>",
                    "uploadType":"attachFile",
                    "imageid":"<%=this.Hidden_ImgItemId.Value %>"
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
</script>
</head>
<body bgcolor="#ffffff">
    <div class="manage">
        <div id="imageEdit">
            <form id="form1" runat="server">
                <h2>
                    图片信息编辑</h2>
                <asp:Panel ID="imagePanel" runat="server">
                    <table >
                        <tr>
                            <td align="left" style="height: 183px; width: 400px">
                                <img id="imgsrc" alt="" src="" runat="server" />
                            </td>
                            <td style="height: 183px">
                                <table>
                                    <tr align="right">
                                        <td align="right">
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
                                            <asp:TextBox ID="txt_Caption" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator1" runat="server" ErrorMessage="" Text="必填" ControlToValidate="txt_Caption"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
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
                                    </tr>
                                    <tr>
                                        <td align="right" style="height: 22px">
                                            文件描述：</td>
                                        <td align="left" style="height: 22px">
                                            <asp:TextBox ID="TxtDescription" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            关键字：</td>
                                        <td>
                                            <asp:TextBox ID="TxtKeyword" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            有效起始时间：</td>
                                        <td align="left">
                                            <uc1:Calendar ID="Calendar_StartDate" runat="server"></uc1:Calendar>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            有效结束时间：</td>
                                        <td align="left">
                                            <uc1:Calendar ID="Calendar_EndDate" runat="server"></uc1:Calendar>
                                        </td>
                                    </tr>
                                    <tr id="tr_shotDate" runat="server">
                                        <td align="right" style="height: 15px">
                                            拍摄时间：</td>
                                        <td align="left" style="height: 15px">
                                            <uc1:Calendar ID="Calendar_ShotDate" runat="server"></uc1:Calendar>
                                        </td>
                                    </tr>
                                    <tr>
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
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            上传时间：</td>
                                        <td align="left">
                                            <asp:Label ID="lb_uploadDate" runat="server"></asp:Label></td>
                                    </tr>
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
                                            <a href="CatalogSel.aspx?itemid=<%=Hidden_ImgItemId.Value%>&itemnum=<%=this.hiImgNum.Value %>"
                                                target="_blank">设置分类</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            附件：</td>
                                        <td>
                                            <asp:GridView  DataKeyNames="attachId" ID="attList" ShowHeader="False" Width="200px" EmptyDataRowStyle-Font-Bold="true" EmptyDataText="没有附件" runat="server" AutoGenerateColumns="False" OnRowDeleting="attList_RowDeleting">
                                                <Columns>
                                                    <asp:BoundField DataField="fileName" />
                                                    <asp:CommandField DeleteText="&lt;span onclick=&quot;return window.confirm('确定删除吗?')&quot;&gt;删除&lt;/span&gt;" ShowDeleteButton="True" >
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:CommandField>
                                                </Columns>
                                                <EmptyDataRowStyle Font-Bold="True" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <asp:Button ID="BtnUpdate" runat="server" Text="修改文件信息" Width="99px" OnClick="BtnUpdate_Click" />
                                <asp:Button ID="btnDel" runat="server" OnClientClick="return window.confirm('你确定要删除吗?')"
                                    OnClick="btnDel_Click" Text="删除文件" />
                                <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
                                <input id="Hidden_ImgItemId" type="hidden" runat="server" />
                                <input id="hiImgNum" type="hidden" runat="server" /></td>
                        </tr>
                    </table>
                    <table class="info" width="400" cellspacing="0" cellpadding="0" border="1" style="border-collapse: collapse">
                        <tr>
                            <td width="100px">
                                上传附件：</td>
                            <td>
                                <asp:FileUpload ID="attachFile" runat="server" Visible="false" /><asp:Button ID="btnUpAttach" runat="server"
                                    Text="上传附件" OnClick="btnUpAttach_Click"  Visible="false"/>
<input type="text" readonly="readonly" id="selectedFile" name="selectedFile" />                                 
<input type="hidden" name="uploadFileName" id="uploadFileName" value="" />                                
<div id="spanSWFUploadButton" style="float:left"></div>
<a href="javascript:swfu.startUpload();"  style="background-image:url(/UI/swfUpload/XPButtonNoText_61x22.png);width:60px;height:21px;border:0px solid red;padding-left:2px;padding-top:3px;display:block;float:right">上传附件</a>


<div id="divFileProgressContainer"></div>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="hiFolder" runat="server" />
                </asp:Panel>
               
            </form>
        </div>
    </div>
</body>
</html>
