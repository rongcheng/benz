<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_FuncPage.Master" EnableSessionState="True" EnableViewState="true" AutoEventWireup="true" CodeBehind="WaterMark.aspx.cs" Inherits="WebUI.Modules.Manage.WaterMark" Title="无标题页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Expires" content="0" />
<link href="../../UI/Css/feature.css" rel="Stylesheet" type="text/css" />
<link href="../../UI/Css/water.css" rel="Stylesheet" type="text/css" />
<script src="../../UI/Js/js.js" type="text/javascript"></script>
<script src="../../UI/Js/handlers.js" type="text/javascript"></script>
<script src="../../UI/swfUpload/swfupload.js" type="text/javascript"></script>
<style type="text/css">
#wm{}
#wm span{line-height:35px;height:35px;display: block;}
#wmPic{}
#wmText{}

span a.btn{text-align:center;display:inline-block;width: 61px;height:21px;line-height:21px;background-image: url(../../image/imgDetail/button_bg.gif);margin-right:5px;border:0px solid red;}

.currentAll{ border-left:#d4d0c8 solid 1px; 
             border-right:#d4d0c8 solid 1px; 
             border-top:#d4d0c8 solid 1px;
              text-align:center; padding:6px 3px 6px 3px; background-color:#d4d0c8; width:200px;}
.nowAll{ 
         text-align:center; padding:6px 3px 6px 3px;width:200px;
          cursor:pointer;}
</style>
<script language="javascript" type="text/javascript">
var htmlload = "<img alt='' src='../../image/common/loading.gif'/>&nbsp;数据加载中...";
var type = "<%=showType %>";

function a(){
    document.getElementById("wmPic").style.display="";
    document.getElementById("wmText").style.display="none";
    return ;
}
function b(){
    document.getElementById("wmPic").style.display="none";
    document.getElementById("wmText").style.display="";
    return;
}
function OnDefault(){
    
    var myxhr;
    if(confirm("您确定恢复默认的水印")){
        myxhr = new xmlHttpObject("default");
        if(myxhr){
            try{
                myxhr.doFeature("type=default&show=2");
            }
            catch(e){
                alert("Can't connect to server:\n"+e.toString());
            }
        }
    }
}
function OnSave(){
    var obj = document.getElementById("ddlSelect");
    var v = obj.options[obj.selectedIndex].value;
    var myxhr; 
    if(document.getElementById("radoldid").checked == true){
        if(confirm("您确定保存原来的图片为水印")){
            myxhr = new xmlHttpObject("oldwater");
            if(myxhr){
                try{
                    myxhr.doFeature("type=OldWater&show="+v);
                }
                catch(e){
                    alert("Can't connect to server:\n"+e.toString());
                }
            }
        }
    }
    else if(document.getElementById("radnewid").checked == true){
        if(confirm("您确定把上传的图片设置为水印")){
            myxhr = new xmlHttpObject("newwater");
            if(myxhr){
                try{
                    myxhr.doFeature("type=NewWater&show="+v);
                }
                catch(e){
                    alert("Can't connect to server:\n"+e.toString());
                }
            }
        }
    }
}

function OnPreview(){
    var obj = document.getElementById("ddlSelect");
    var v = obj.options[obj.selectedIndex].value;
    var now = new Date();
    if(document.getElementById("radoldid").checked == true){
        document.getElementById("iframeid").src = "PreviewImage.aspx?show="+v+"&state=water&t="+now.getMilliseconds();
    }
    else{
        document.getElementById("iframeid").src = "PreviewImage.aspx?show="+v+"&t="+now.getMilliseconds();
    }
    if(document.getElementById("iframeid").document)
        document.getElementById("iframeid").document.location;
    else
        document.getElementById("iframeid").contentDocument.location;
}
var swfu;
window.onload = function () {
	swfu = new SWFUpload({
		upload_url: "UploadImage.aspx",
        post_params : {
            "ASPSESSID" : "<%=Session.SessionID %>"
        },
		file_size_limit : "2 MB",
		file_types : "*.jpg;*.gif;*.png",
		file_types_description : "JPG Images",
		file_upload_limit : "0",    
		file_queue_error_handler : fileQueueError,
		file_dialog_complete_handler : fileDialogComplete,
		upload_progress_handler : uploadProgress,
		upload_error_handler : uploadError,
		upload_success_handler : uploadSuccess,
		upload_complete_handler : uploadComplete,
		button_image_url : "../../images/XPButtonNoText_160x22.png",
		button_placeholder_id : "spanButtonPlaceholder",
		button_width: 160,
		button_height: 22,
		button_text : '<span class="button">上 传<span class="buttonSmall">(2 MB Max)</span></span>',
		button_text_style : '.button {color:red;border: solid 1px #CEE2F2; font-family: Helvetica, Arial, sans-serif; font-size: 14pt; text-align:center;} .buttonSmall { font-size: 10pt; }',
		button_text_top_padding: 1,
		button_text_left_padding: 5,
		flash_url : "../../UI/swfUpload/swfupload.swf",
		custom_settings : {
			upload_target : "divFileProgressContainer"
		},
		debug: false
	});
			
    var obj = document.getElementById("ddlSelect");
    for(var i=0;i<obj.options.length;i++){
        if(obj.options[i].value == type)
            obj.options[i].selected = true;
        else
        obj.options[i].selected = false;
    }
}
</script>
<script language="javascript" type="text/javascript">
function show(type){
    switch(type){
        case "0":
            document.getElementById("td1").className = "currentAll";
            document.getElementById("td2").className = "nowAll";
            document.getElementById("WaterId").style.display = "";
            document.getElementById("ParamId").style.display = "none";
        break;
        case "1":
            document.getElementById("td1").className = "nowAll";
            document.getElementById("td2").className = "currentAll";
            document.getElementById("WaterId").style.display = "none";
            document.getElementById("ParamId").style.display = "";
        break;
    }
}

function mSmtp(){
    var smtp = document.getElementById("<%=mSmtpId.ClientID %>");
    var name = document.getElementById("<%=mNameId.ClientID %>");
    var pass = document.getElementById("<%=mPassId.ClientID %>");
    var port = document.getElementById("<%=mPortId.ClientID %>");
    var from = document.getElementById("<%=mFromId.ClientID %>");
    var to = document.getElementById("<%=mToId.ClientID %>");
    if(smtp.value == ""){
        alert("发送邮件服务器不能为空");
        smtp.focus();
        return false;
    }
    if(name.value == ""){
        alert("用户名不能为空");
        name.focus();
        return false;
    }
    if(pass.value == ""){
        alert("密码不能为空");
        pass.focus();
        return false;
    }
    if(port.value == ""){
        alert("端口号不能为空");
        port.focus();
        return false;
    }
    var r =/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/; 
    if(from.value == ""){
        alert("发件人邮箱不能为空");
        from.focus();
        return false;
    }
    if(!r.test(from.value)){
        alert("发件人邮箱格式不正确");
        from.focus();
        return false;
    }
    if(to.value == ""){
        alert("收件人邮箱不能为空");
        from.focus();
        return false;
    }
    if(!r.test(to.value)){
        alert("收件人邮箱格式不正确");
        from.focus();
        return false;
    }
    if(confirm("您确定保存修改内容")){
        var myxhr = new xmlHttpObject("System");
        if(myxhr){
            try{
                myxhr.doFeature("type=System&s="+smtp.value+"&n="+name.value+"&p="+pass.value+"&t="+port.value+"&from="+from.value+"&to="+to.value+"&u="+document.getElementById('hiddenUserName').value);
            }
            catch(e){
                alert("Can't connect to server:\n"+e.toString());
            }
        } 
    } 
}

function OnHistory(){
    var obj = document.getElementById("history");
    if(obj.style.display == "none"){
        obj.style.display = "block";
        obj.innerHTML = "";
        var myxhr = new xmlHttpObject("history");
        if(myxhr){
            try{
                myxhr.doFeature("type=history");
            }
            catch(e){
                alert("Can't connect to server:\n"+e.toString());
            }
        } 
    }
    else
        obj.style.display = "none";
     
}
function OnPre(c){
    var myxhr = new xmlHttpObject("history");
    if(myxhr){
        try{
            myxhr.doFeature("type=history&c="+c+"&t=pre");
        }
        catch(e){
            alert("Can't connect to server:\n"+e.toString());
        }
    }
}
function OnNext(c){
    var myxhr = new xmlHttpObject("history");
    if(myxhr){
        try{
            myxhr.doFeature("type=history&c="+c+"&t=next");
        }
        catch(e){
            alert("Can't connect to server:\n"+e.toString());
        }
    }
}

function AddValue(){
    document.getElementById("hHost").value = document.getElementById("<%=mSmtpId.ClientID %>").value;
    document.getElementById("hName").value = document.getElementById("<%=mNameId.ClientID %>").value;
    document.getElementById("hPass").value = document.getElementById("<%=mPassId.ClientID %>").value;
    document.getElementById("hPort").value = document.getElementById("<%=mPortId.ClientID %>").value;
    document.getElementById("hFrom").value = document.getElementById("<%=mFromId.ClientID %>").value;
    document.getElementById("hTo").value = document.getElementById("<%=mToId.ClientID %>").value;
}
function aValue(){
    document.getElementById("<%=mSmtpId.ClientID %>").value = document.getElementById("hHost").value;
    document.getElementById("<%=mNameId.ClientID %>").value = document.getElementById("hName").value;
    document.getElementById("<%=mPassId.ClientID %>").value = document.getElementById("hPass").value;
    document.getElementById("<%=mPortId.ClientID %>").value = document.getElementById("hPort").value;
    document.getElementById("<%=mFromId.ClientID %>").value = document.getElementById("hFrom").value;
    document.getElementById("<%=mToId.ClientID %>").value = document.getElementById("hTo").value;
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<input type="hidden" id="hiddenState" value="" />
<input type="hidden" id="hiddenJ" value="10" />
<input type="hidden" id="hiddenUserName" value="<%=username %>" />
<input type="hidden" id="hHost" value="<%=host %>" />
<input type="hidden" id="hName" value="<%=name %>" />
<input type="hidden" id="hPass" value="<%=pass %>" />
<input type="hidden" id="hPort" value="<%=port %>" />
<input type="hidden" id="hFrom" value="<%=from %>" />
<input type="hidden" id="hTo" value="<%=to %>" />
<h4>系统设置</h4>
<div style="width:100%; text-align:left; margin-bottom:5px; margin-top:5px; border-bottom:#d4d0c8 solid 1px;">
    <table width="60%" cellpadding="0" cellspacing="0">
        <tr>
            <td id="td2" class="currentAll" onclick="show('1')">参数设置</td>
            <td id="td1" class="nowAll" onclick="show('0')">水印设置</td>    
        </tr>
    </table>
</div>
<div id="ParamId">
    <table width="100%">
        <tr>
            <td height="10px" colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td align="left" colspan="2">系统发件邮箱参数设置：</td>
        </tr>
        <tr>
            <td align="center">
                <table width="350px">
                    <tr>
                        <td align="right" valign="middle" style="vertical-align:middle;">发送邮件服务器：</td>
                        <td align="left" valign="middle" style="vertical-align:middle;">
                            <input type="text" id="mSmtpId" style="width:200px;" runat="server" /><span style="color:Red">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle" style="vertical-align:middle;">用户名：</td>
                        <td align="left" valign="middle" style="vertical-align:middle;">
                            <input type="text" id="mNameId" style="width:200px;" runat="server" /><span style="color:Red">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle" style="vertical-align:middle;">密码：</td>
                        <td align="left" valign="middle" style="vertical-align:middle;">
                            <input type="text" id="mPassId" style="width:200px;" runat="server" /><span style="color:Red">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle" style="vertical-align:middle;">端口：</td>
                        <td align="left" valign="middle" style="vertical-align:middle;">
                            <input type="text" id="mPortId" style="width:200px;" runat="server" /><span style="color:Red">*</span>
                        </td>
                    </tr>
                </table>
            </td>
            <td align="left">
                <table width="350px">
                    <tr>
                        <td align="right" valign="middle" style="vertical-align:middle;">发件人邮箱：</td>
                        <td align="left" valign="middle" style="vertical-align:middle;">
                            <input type="text" id="mFromId" style="width:200px;" runat="server" /><span style="color:Red">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle" style="vertical-align:middle;">订单处理人邮箱：</td>
                        <td align="left" valign="middle" style="vertical-align:middle;">
                            <input type="text" id="mToId" style="width:200px;" runat="server" /><span style="color:Red">*</span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="20px" colspan="2" align="center">
            <input type="button" id="mSid" value="保存" class="btn" onclick="return mSmtp();" />
            &nbsp;
            <input type="button" id="Button1" value="还原" class="btn" onclick="aValue();" />
            &nbsp;
            <input type="button" id="lid" value="查看历史" class="btn" onclick="OnHistory()" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <div id="history" style="display:none; margin-top:10px; ">
                    <table width="500px" style="border:solid 1px #d4d0c8;padding:5px 5px 5px 5px">
                    <tr style="background-color:#fbfbfb;">
                        <td style="padding:5px 5px 5px 5px;" align="right" valign="middle" width="110px">发送邮件服务器：</td>
                        <td style="padding:5px 5px 5px 5px;" align="left" valign="middle" width="130px">
                            <div id="dSmtpId">ertertertertert3</div>
                        </td>
                        <td style="padding:5px 5px 5px 5px;" align="right" valign="middle"  width="110px">用户名：</td>
                        <td style="padding:5px 5px 5px 5px;" align="left" valign="middle" >
                            <div id="dUsernameId">ertertertert</div>
                        </td>
                    </tr>
                    <tr style="background-color:#f3f3f3;">
                        <td style="padding:5px 5px 5px 5px;" align="right" valign="middle"  width="110px">密码：</td>
                        <td style="padding:5px 5px 5px 5px;" align="left" valign="middle"  width="130px">
                            <div id="dPassId"></div>
                        </td>
                        <td style="padding:5px 5px 5px 5px;" align="right" valign="middle"  width="110px">端口：</td>
                        <td style="padding:5px 5px 5px 5px;" align="left" valign="middle" >
                            <div id="dPortId"></div>
                        </td>
                    </tr>
                    <tr style="background-color:#fbfbfb;">
                        <td style="padding:5px 5px 5px 5px;" align="right" valign="middle"  width="110px">发件人邮箱：</td>
                        <td style="padding:5px 5px 5px 5px;" align="left" valign="middle"  width="130px">
                            <div id="dFromId"></div>
                        </td>
                        <td style="padding:5px 5px 5px 5px;" align="right" valign="middle"  width="110px">订单处理人邮箱：</td>
                        <td style="padding:5px 5px 5px 5px;" align="left" valign="middle" >
                            <div id="dToId"></div>
                        </td>
                    </tr>
                    <tr style="background-color:#f3f3f3;">
                        <td style="padding:5px 5px 5px 5px;" align="right" valign="middle"  width="110px">修改时间：</td>
                        <td style="padding:5px 5px 5px 5px;" align="left" valign="middle"  width="130px">
                            <div id="dMarkId"></div>
                        </td>
                        <td style="padding:5px 5px 5px 5px;" align="right" valign="middle" >修改人：</td>
                        <td style="padding:5px 5px 5px 5px;" align="left" valign="middle" >
                            <div id="dPersonId"></div>
                        </td>
                    </tr>
                    <tr style="background-color:#fbfbfb;">
                        <td style="padding:5px 5px 5px 5px;" colspan="4" align="center">
                            <a href="javascript:OnPre();" >上一条</a>
                            &nbsp;&nbsp;
                            <a href="javascript:OnNext();">下一条</a>
                        </td>
                    </tr>
                </table>
                </div>
            </td>
        </tr>
        <tr>
            <td height="10px" colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <span style="color:Red; font-size:12px;">注：</span>
                <br />
                <span style="font-size:12px;">我们为您保存了小于等于10次的修改记录。</span>
                <br />
                <span style="font-size:12px;"></span>
            </td>
        </tr>
    </table>
</div>
<div id="WaterId" style="display:none;">
    <table>
        <tr>
            <td style=" width:400px;">
            <div style=" margin-top:10px;">
                <span style="font-size:13px;">1.上传水印图片</span>
                <div id="content" style="margin-top:5px;">
                    <div id="swfu_container" style="margin: 0px 10px;">
                        <div>
				            <span id="spanButtonPlaceholder"></span>
				            <div id="divFileProgressContainer" style="height: 75px;"></div>
		                </div>
		            <div id="thumbnails"></div>
		                    <div id="oldid" style="padding-top:10px; padding-bottom:5px;">
		                        <%=content %>
		                    </div>
                    </div>
                </div>
                <span style="font-size:13px;">2.选择图片显示位置</span>
                    <select id="ddlSelect" onchange="javascript:document.getElementById('thumbnails').style.display = 'block';">
                        <option value="0" selected="selected">中央</option>
                        <option value="1">左上角</option>
                        <option value="2">右上角</option>
                        <option value="3">右下角</option>
                        <option value="4">左下角</option>
                    </select>
                <div style="width:100%; text-align:center; margin-top:20px;">
                <input type="button" class="btn" id="btnDefault" value="恢复默认" onclick="OnDefault()" />
                &nbsp;&nbsp;
                <input type="button" class="btn" id="btnSave" value="保存" onclick="OnSave()" />
                &nbsp;&nbsp;
                <input type="button" class="btn" id="btnPreview" value="预览" onclick="OnPreview()" />
                </div>
                <br />
                <span style="color:Red; font-size:12px;">注：</span>
                <br />
                <span style="font-size:12px;">1.点击【保存】按钮后，上传图片和设置显示位置才能生效。</span>
                <br />
                <span style="font-size:12px;">2.您也可以不用上传图片，只设置水印图片的显示位置。</span>
            </div>
            </td>
            <td>
                <iframe src="PreviewImage.aspx?state=water&t=<%=time %>" name="iframename" id="iframeid" width="300px" height="440px">
            
                </iframe>
            </td>
        </tr>
    </table>
</div>
</asp:Content>

