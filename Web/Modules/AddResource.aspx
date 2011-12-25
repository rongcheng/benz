<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddResource.aspx.cs" Inherits="WebUI.Modules.AddResource" 
MasterPageFile="~/MPages/QJ_MasterPage.Master" Theme="MainSkin"
EnableSessionState="True" EnableViewState="true" Title="上传图片" %>

<%@ Register Src="../UserControls/CatalogTree.ascx" TagName="CatalogTree" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="header1" ContentPlaceHolderID="head" runat="server">
<link href="../UI/swfUpload/progressbar.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../UI/swfUpload/swfupload.js"></script> 
<script type="text/javascript" src="../UI/swfUpload/handlers.js" charset="gb2312" ></script>
<link href="../UI/Css/jquery-ui.css" rel="stylesheet" type="text/css"/>

<script language="javascript" type="text/javascript">

function myUploadSuccess2()
{
    if(confirm("上传成功! 是否去查看新上传的资源？"))
    {
        location.href="/Modules/UserProfile.aspx?tabid=3";
    }
}

function rblCopyrightChanged()
{
    var selectedvalue=$("#<%=rblCopyright.ClientID %> :radio[checked]").val();
    if(selectedvalue=="0")
    {
        $(".noCopyright").css("display","block");
    }
    else if(selectedvalue=="1")
    {
        $(".noCopyright").css("display","none");
    }

}  


var swfu; 
var swfUploadBaseUrl = "../UI/swfUpload/";
var filesizeLimit = "1000 MB"
window.onload = function () { 
	var settings_object = { 
		upload_url : "upload.aspx", 
		flash_url : swfUploadBaseUrl+"swfupload.swf", 
		flash9_url : swfUploadBaseUrl+"swfupload_FP9.swf",
		file_size_limit : filesizeLimit ,
		file_upload_limit : 0,
		
		file_types: "*.*",
		file_types_description:"所有文件类型",
		
		post_params : {
                    "ASPSESSID" : "<%=Session.SessionID %>",
                    "AUTHID" : "<%=(Request.Cookies[FormsAuthentication.FormsCookieName]==null?"":Request.Cookies[FormsAuthentication.FormsCookieName].Value)%>",
                    "uploadType":"resourcefile"
                },

		button_placeholder_id : "spanSWFUploadButton" ,
		button_image_url : "../images/swfuploadButton.gif",
		button_width: 100,
		button_height: 35,
		
	
		button_text : '<span class="button">选择文件<span class="buttonSmall"></span></span>',
		button_text_style : '.button { font-size: 12pt;text-indent:30px;font-weight:bolder;color:#999999;} .buttonSmall { font-size: 10pt; }',
		button_text_top_padding: 7,
		button_text_left_padding: 0,


		
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
		upload_start_handler:uploadStart,
		
		custom_settings : {
					upload_target : "divFileProgressContainer"
				},
		debug:false
		
	}; 
	swfu = new SWFUpload(settings_object); 
	document.getElementById("delList").style.display="none";
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
    
    
    catTabCount=<%=this.rptBigCat.Items.Count  %>;
   for(var i=1;i<=catTabCount;i++)
    {
        obj="#cat-tab-"+i+"-info";
        $(obj).hide();
    }
    $("#cat-tab-1-info").show();
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
    var obj=document.getElementById("<%=txtKeyWords.ClientID %>");
    var _txt=obj.value;
    if(_txt.indexOf(str)<0)
    {
        document.getElementById("<%=txtKeyWords.ClientID %>").value+=str+",";
    }
    
    
}


function showCatTab(id)
{
    var obj="cat-tab-"+id;
    for(var i=1;i<=catTabCount;i++)
    {
        obj="#cat-tab-"+i+"-info";
        sss=".cat-tab-"+i;
        
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

function addCatword(strName,strCatId){
    var bool = false;
    var ul = document.getElementById("AllCatalog");
    var item = document.getElementsByName("newAdd");
    for(var i=0;i<item.length;i++){
        if(item[i].id == strCatId){
            bool = true;
            break;
        }
    }
    if(bool){
        alert("["+strName+"]已经被选择！");
        return;
    }
    else{
        var li = document.createElement("LI");
        var html = "<input type=\"checkbox\" id=\""+strCatId+"\" onclick=\"Delete(this)\" checked value=\""+strCatId+"\" name=\"newAdd\">"+strName;
        li.innerHTML = html;
        ul.appendChild(li);
        var objCatId=document.getElementById("<%=hidCatIds.ClientID %>");
        var _txtCatId=objCatId.value;
        if(_txtCatId.indexOf(strCatId)<0){
            document.getElementById("<%=hidCatIds.ClientID%>").value+=strCatId+",";
        } 
    }
 
}
function Delete(obj) {
    $(obj).parent("li").remove();
    var v = obj.value+",";
    var txt = document.getElementById("<%=hidCatIds.ClientID %>").value;
    var s = txt.replace(v,"");
    document.getElementById("<%=hidCatIds.ClientID %>").value = s;
}
</script>

<style type="text/css" >
 .noCopyright{display:none}   
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
 	width:475px;
 }
 
 #cat-1
 {
 	margin-bottom:5px;
 	
 	}
 #cat-1 ul
 {	color:Red; }
 #cat-1 ul li
 {
 	display:inline;
 	margin-right:2px;
 	background-color:#ddd;
 	padding:3px 10px 3px 10px;
 }

.cat-tab-selected
{
	background-color:#3F3F3F;
	color:White;
}
 #cat-1 div
 {
 	border:1px solid #ddd;
 	padding:10px;
 	width:475px;
 }
   
   
   
   
.upFile div{border:0px solid blue}
.upFile {width:486px;  position:relative; margin-left:5px;border:0px solid red;  font-size:12px; background:url(/images/swfupload.gif1) no-repeat; }
.upFile p { margin:0; padding:0; }
.upFile .lt { position:relative; float:left; width:100%;  padding:4px;background:#f9f9f9; border:1px solid #ddd ; display:none}
.upFile .lt p { height:24px; line-height:26px; overflow:hidden; border-bottom:1px solid #fff; color:#000; background:none; }
.upFile .lt ul { background:#fff;position:relative; z-index:2; width:100%; max-height:300px; _height:expression(this.scrollHeight > 300 ? "300px" : "auto");overflow-y:auto; }
.upFile .lt li { position:relative; z-index:2; float:left; clear:left; width:100%; height:25px; overflow:hidden; text-indent:5px; text-align:left; background:url(/images/dotted_.gif) repeat-x left bottom; }
.upFile .lt li span { position:absolute; left:0; top:0; z-index:2; display:block; height:20px;line-height:20px; padding-top:5px; font-weight:normal; color:#666; }
.upFile .lt li span.name { width:315px;border:0px solid red;overflow:hidden }
.upFile .lt li span.size { left:320px; width:70px; }
.upFile .lt li em {border:0px solid red; position:absolute; left:390px; top:5px; z-index:2; display:block; width:20px; height:20px; overflow:hidden; text-indent:-999em; background:url(/images/swfupload.gif) no-repeat 5px -325px; }
.upFile .lt .initial { background-position:-35px -324px; cursor:pointer; }
.upFile .lt .loading { background-position:-14px -324px; }
.upFile .lt .end { background-position:5px -324px; }
.upFile .lt .error { background-position:-205px -323px; }
.upFile .lt li p { position:relative; z-index:1; float:left; width:0; border:0; height:24px; margin-bottom:1px; border-right:2px solid #8abb64; margin-left:-2px; background:#d9f1c7; }
.upFile .rt { position:relative; float:left; width:10px; height:100%; padding-top:15px; }


.allFiles {  margin:10px 0 0 0; }
.allFiles div.box { position:relative; width:465px; min-height:55px; height:auto!important; height:55px; padding:8px 8px 0; border:1px solid #CCCCCC;font-size:12px;font-weight:normal }
.allFiles div h3 { padding:1px 0 0 22px; font-size:14px; color:#666; background:url(/images/divFileProgressContainerICO.gif) no-repeat -60px 0; }
.allFiles div.ok h3 { background-position:-20px -40px;}
.allFiles div.stop h3 { background-position:-40px -20px;}
.allFiles div.error h3 { background-position:0 -60px;}
.allFiles ol { margin:.5em .5em 0; }
.allFiles ol li { padding-left:1.3em; margin:0 0 0.6em 1.5em; color:#666; background:url(/images/swfupload.gif) no-repeat -210px -324px; }
.allFiles ol li em { margin:0 .2em; text-decoration:blink; font-style:normal; color:#f96; }
.allFiles span.loading,
.allFiles span.loading span { display:block; height:12px; margin-bottom:5px; overflow:hidden; background:url(/images/divFileProgressContainer_.gif) repeat-x 0 -12px; }
.allFiles span.loading span { margin:0; background-position:0 0; }
.allFiles p { margin-bottom:5px; color:#999; }
.allFiles p strong { font-weight:normal; color:#666; }
.allFiles p.fllFiles_help { margin-left:2em; color:#999; }


#delList,#Upload 
{ 

position:relative;
display:inline-block; 
width:100px; height:35px; line-height:36px!important; 
line-height:38px; overflow:hidden; text-indent:30px; margin:0 0 4px 11px;  
text-decoration:none; color:#999; 
background:url(/images/swfuploadButton.gif) no-repeat; 
border:0px solid red;
}

#delList { background-position:-100px 0; display:none }
#delList:hover { background-position:-100px -35px; }
#delList:active { background-position:-100px -70px; }
#delList.disabled { background-position:-100px -105px!important; }

.info td
{
	margin-left:20px;
	
	}
	
.uploadSuccess
{
	line-height:25px;
	border:1px solid #dddddd;
	
	}
    #keyWord
    {
        height: 96px;
        width: 246px;
    }
 .selectClass
{
    text-align: left;
    list-style-type: none;
    padding: 0;
    margin: 0;
    width: 100%;
}
.selectClass li
{
    padding: 3px 0px 3px 0px;
    line-height: 15px;
    text-align: left;
    margin: 0;
    width: 15%;
    float: left;
}
</style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
</cc1:ToolkitScriptManager>

    <input type="hidden" name="hiddate" id="hiddate" runat="server" />
    <input type="hidden" name="imageType" id="imageType" runat="server" />
    <input type="hidden" readonly="readonly" id="selectedFile" name="selectedFile" />
    
    <input type="hidden" name="uploadFileName" id="uploadFileName" value="" />   
    <input type="hidden" name="hidCatIds" id="hidCatIds" value=""  runat="server" />  
    <div class="tools" style="margin-bottom:3px; width:965px;">
	<p class="keywordText" id="searchResultTips">
	资源上传
	</p>

    </div>
    <table id="table2" name="table2" width="100%" style="border:1px solid #e2e2e2;margin-top:0px">
        <tr>

            <td valign="top" align="left">
                <div class="back_holder">      
                   <table class="info" id="Table1" height="100%" cellspacing="5" cellpadding="5" width="100%"
                        border="0" runat="server" >
                        <tr>
                            <td align="right">
                                标题：</td>
                           <td >
                                <input name="txt_Caption" type="text" runat="server" id="txt_Caption" class="qjDAM_textarea_style" />
                           </td>
                        </tr>                        
                        <tr>
                            <td align="right" valign="top">分类：</td>
                            <td style=" height: 24px;">                    
                                <div id="cat-1">
                            <ul>           
                                <asp:Repeater ID="rptBigCat" runat="server">
                                <ItemTemplate>
                                <li><a href="#"   class="cat-tab-<%# this.rptBigCat.Items.Count + 1 %>"  onclick="showCatTab(<%# this.rptBigCat.Items.Count + 1 %>)"><span><%#Eval("CatalogName")%></span></a></li>     
                                </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                            <asp:Repeater ID="rptBigCat1" runat="server" OnItemDataBound="rptBigCat1_ItemDataBoud">
                            <ItemTemplate><div id="cat-tab-<%# this.rptBigCat1.Items.Count + 1 %>-info" class="">
                                <p>
                            <asp:Repeater ID="rptSmallCat" runat="server">
                                <ItemTemplate>
                                
                                <a href="javascript:addCatword('<%#Eval("CatalogName") %>','<%#Eval("CatalogId") %>')"><%#Eval("CatalogName")%></a>
                                 
                                </ItemTemplate>
                           </asp:Repeater>
                           </p>
                                </div>
                           </ItemTemplate>
                           </asp:Repeater>
                                        
                          </div>
                           <div id="Content">
                                <ul id="AllCatalog" class="selectClass">
                                </ul>
                           </div>                         
                            </td>
                        </tr>                       
                        <tr>
                            <td align="right" valign="top">关键字：</td>
                            <td style=" height: 24px;">
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
                            <asp:TextBox ID="txtKeyWords" runat="server" Height="71px" TextMode="MultiLine" 
                                    Width="495px"></asp:TextBox>
                                <input name="keyWord" type="text" runat="server" id="keyWord" class="qjDAM_textarea_style" visible="false"  />
                            </td>
                        </tr>
                        <tr >
                            <td align="right" valign="top">来源：</td>
                            <td>                         
                            <table>
                            <tr><td width="200px;"> 
                            
                            <asp:RadioButtonList ID="rblCopyright" runat="server" RepeatDirection="Horizontal"  >
                            <asp:ListItem  Selected="True" Value="1">自有</asp:ListItem>
                            <asp:ListItem Value="0">第三方</asp:ListItem>
                            </asp:RadioButtonList>
                            <br />
                            <div class="noCopyright">
                            <table >
                            <tr >
                            <td align="right">有效起始日期：</td>
                            <td><uc3:AjaxCalendar ID="startDate" runat="server" /></td>
                        </tr>
                        <tr >
                            <td align="right">有效结束日期：</td>
                            <td><uc3:AjaxCalendar ID="endDate" runat="server" /></td>
                        </tr>                 
                            </table>
                            </div>
                            </td><td valign="top">
                            
                           <table><tr><td align="right">
                                作者：</td>
                            <td >
                                <input name="txt_Author" type="text" runat="server" id="txt_Author" class="qjDAM_textarea_style" />
                                </td></tr></table> 
                        
                            </td></tr>
                            
                            </table>
                            </td>                
                        </tr>
                          <tr>
                            <td align="right" valign="top">备注：</td>
                            <td><textarea rows="4" id="description" name="description" runat="server" class="qjDAM_textarea_style"
                                    style="width: 495px" cols="1"></textarea>                          
                            </td>
                        </tr>                       
                        <tr style="border:0px solid red"> 
                        <td></td>                         
                            <td  ><br />                          
                        <div class="upFile" id="upFile">
                    <div class="lt">
                        <p><span style="width: 320px; float: left; padding-left: 5px;">文件名</span>大小(k)</p>
                        <ul>
                        </ul>
                    </div>
                    <div class="rt1" style="clear:both;">
                        <p>
                        </p>                       
                    </div>
                    <p class="count" id="count" style="display:none">总计<span>0</span>个资源 / 总大小<span>0</span>MB。</p>
                        <div id="allFiles" class="allFiles"></div>                         
                        <div id="spanSWFUploadButton" ></div>
                        <a id="delList" class="disabled" href="javascript:void(0);">清空列表</a>
                        <a id="Upload" class="disabled" href="javascript:void(0);">开始上传</a>
                         </div>
                            <input class="qjDAM_inputbtn_style"  id="btnUpload" type="button" value=" 确定 " name="btnUpload"
                                    runat="server" onserverclick="btnUpload_ServerClick" onclick="return myDoUpload();" visible="false" />
                            </td>
                        </tr>
                    </table>
                    <script type="text/javascript" src="../UI/swfUpload/uploadFile.js" charset="gb2312"></script>
                    <script>initPage();function keyWord_onclick() {}</script>                     
                </div>
            </td>
        </tr>
    </table>
   

</asp:Content>
