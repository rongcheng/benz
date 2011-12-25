<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MPages/FuncPage.Master" EnableSessionState="true" CodeBehind="Upload.aspx.cs" Inherits="WebUI.Modules.Video.Upload" %>
<%@ Register Src="../../UserControls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="uc3" %>
<%@ Register Src="../../UserControls/CatalogTree.ascx" TagName="CatalogTree" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link href="../../UI/swfUpload/progressbar.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" src="../../UI/swfUpload/swfupload.js"></script> 
<script type="text/javascript" src="../../UI/swfUpload/handlers_video.js" charset="gb2312" ></script>
<script language="javascript" type="text/javascript">
var swfu; 
window.onload = function () { 

	var settings_object = { 
		upload_url : "../upload.aspx", 
		flash_url : "../../UI/swfUpload/swfupload.swf", 
		flash9_url : "../../UI/swfUpload/swfupload_FP9.swf",
		file_size_limit : "500 MB" ,
		file_upload_limit : 1,
		file_queue_limit : 1, 
		file_types: "<%=new QJVRMS.Common.VideoController().GetVideoFormats()%>",
		file_types_description:"所有视频类型",		
		post_params : {
                    "ASPSESSID" : "<%=Session.SessionID %>",
                    "AUTHID" : "<%=(Request.Cookies[FormsAuthentication.FormsCookieName]==null?"":Request.Cookies[FormsAuthentication.FormsCookieName].Value)%>",
                    "uploadType":"resourcefile"                    
                },

		button_placeholder_id : "spanSWFUploadButton" ,
		button_image_url : "../../UI/swfUpload/XPButtonNoText_61x22.png",
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

	document.getElementById("ctl00_ContentPlaceHolder1_btnUpload").disabled=true;
};
</script>
    <input type="hidden" name="hiddate" id="hiddate" runat="server" />
    <input type="hidden" name="imageType" id="imageType" runat="server" />
    <h2>
        视频上传</h2>
    <table id="table2" name="table2" width="700px">
        <tr>
            <td valign="top" align="left" style="width: 200px">
                <div class="uploadTree" style="float: left; overflow-y: auto; height: 500px; width: 220px;
                    border: 1px #C8C8C8 solid;">
                    <uc1:CatalogTree UploadRight="true" ID="catalogTree" runat="server" TreeNodeType="Leaf">
                    </uc1:CatalogTree>
                </div>
            </td>
            <td valign="top" align="left">
                <div class="back_holder">
                    <table class="info" id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%"
                        border="0" runat="server">
                        <tr>
                            <td width="100px">
                                文件 ：</td>
                            <td>
<input type="text" readonly="readonly" id="selectedFile" name="selectedFile" />
<input type="hidden" name="uploadFileName" id="uploadFileName" value="" />                                
<div id="spanSWFUploadButton"></div><span style="color:Red"> &nbsp;*</span>
<div id="divFileProgressContainer"></div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                标题：</td>
                            <td>
                                <input name="txt_Caption" type="text" runat="server" id="txt_Caption" class="qjDAM_textarea_style" />
                                <font color="red">*</font>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_Caption">必填</asp:RequiredFieldValidator></td>
                        </tr>
                        <%--<tr>
                            <td>
                                地点：</td>
                            <td>
                                <input name="txt_Address" type="text" runat="server" id="txt_Address" class="qjDAM_textarea_style" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                人物：</td>
                            <td>
                                <input name="txt_Character" type="text" runat="server" id="txt_Character" class="qjDAM_textarea_style" />
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                关键字：</td>
                            <td style="width: 150px; height: 24px;">
                                <input name="keyWord" type="text" runat="server" id="keyWord" class="qjDAM_textarea_style" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                拍摄日期：</td>
                            <td>
                                <uc3:AjaxCalendar ID="t_Date" runat="server" />
                                <span style="color:Red">*</span></td>
                        </tr>
                        <tr>
                            <td>
                                文件描述：</td>
                            <td>
                                <textarea rows="5" id="description" name="description" runat="server" class="qjDAM_textarea_style"
                                    style="width: 200px" cols="1"></textarea>
                          
                            </td>
                        </tr>
                        <tr>
                            <td>
                                有效起始日期：</td>
                            <td>
                                <uc3:AjaxCalendar ID="Calendar_StartDate" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                有效结束日期：</td>
                            <td>
                                <uc3:AjaxCalendar ID="Calendar_EndDate" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <input class="qjDAM_inputbtn_style"  id="btnUpload" type="button" value=" 确定 " name="btnUpload"
                                    runat="server" onserverclick="btnUpload_ServerClick" onclick="return myDoUpload();" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
