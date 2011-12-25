<%@ Page Language="C#" Theme="MainSkin" MasterPageFile="~/MPages/QJ_FuncPage.Master" AutoEventWireup="true" CodeBehind="ReleaseNotice.aspx.cs" Inherits="WebUI.Modules.Manage.ReleaseNotice" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../../UI/Css/feature.css"rel="Stylesheet" type="text/css" />
<script src="../../UI/Js/js.js" type="text/javascript"></script>
<script src="../../UI/tinymce/tiny_mce_src.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
var htmlload = "<img alt='' src='../../image/common/loading.gif'/>&nbsp;数据加载中...";
tinyMCE.init({
    mode : "textareas",
    theme : "simple",
    height:"400px",
    width:"600px"
    });
function DeleteNotice(id){
    if(confirm("你确定要删除？")){
        var myxhr = new xmlHttpNotices("delete");
        if(myxhr){
            try{
                myxhr.doNotices("type=Delete&id="+id);
            }
            catch(e){
                alert("Can't cannect to server:\n"+e.toString());
            }
        }
    }
}
function GetNoticesPage(userName, size, index){
    document.getElementById("hiddenPage").value = index;
    var obj = document.getElementById("<%=Content.ClientID %>");
    if(obj.getAttribute('loaded') == 'true'){
        return;
    }
    else{
        obj.innerHMTL = htmlload;
        var myxhr = new xmlHttpNotices("<%=Content.ClientID %>");
        if(myxhr){
            try{
                myxhr.doNotices("type=Page&name="+userName+"&size="+size+"&index="+index);
            }
            catch(e){
                alert("Can't cannect to server:\n"+e.toString());
            }
        }
    }
}
function GetNotice(noticeId, name){
    Show('');
    document.getElementById("divEdit").innerHTML = "<input type=\"button\" id=\"btnEditId\" onclick=\"UpdateNotice('"+noticeId+"', '"+name+"')\" value=\"保存\" class=\"btn\" style=\" font-size:12px;\" />";
    var obj = document.getElementById("UpdataNotice");
    if(obj.getAttribute('loaded') == 'true'){
        return;
    }
    else{
        obj.innerHMTL = htmlload;
        var myxhr = new xmlHttpNotices("UpdataNotice");
        if(myxhr){
            try{
                myxhr.doNotices("type=Single&noticeid="+noticeId+"&name="+name);
            }
            catch(e){
                alert("Can't cannect to server:\n"+e.toString());
            }
        }
    }
}
function Show(type){
    if(type == "add"){
        document.getElementById("AddNotice").style.display = "";
        document.getElementById("UpdataNotice").style.display = "none";
    }
    else{
        document.getElementById("AddNotice").style.display = "none";
        document.getElementById("UpdataNotice").style.display = "";
    }
}
function Add(name, content){
    document.getElementById("txtNameId").value = name;
    tinyMCE.getInstanceById('FreeTextBoxEdit').getBody().innerHTML = content
}
function UpdateNotice(noticeId, logName){
    var noticeName = document.getElementById("txtNameId");
    var noticeContent = tinyMCE.getInstanceById('FreeTextBoxEdit').getBody().innerHTML;
    
    if(noticeName.value == "") {
        alert("发布公告名称不能为空！");
        return;
    }
    if(noticeContent == ""){
        alert("发布公告内容不能为空！");
        return;
    }
    else{
        if(noticeContent.length > 1500){
            alert("发布公告字数不能大于1000！");
            return;
        }
    }
    var myxhr = new xmlHttpNotices("update");
    if(myxhr){
        try{
            myxhr.doNotices("type=Update&name="+logName+"&noticeId="+noticeId+"&noticeName="+encodeURIComponent(noticeName.value)+"&noticeContent="+encodeURIComponent(noticeContent));
        }
        catch(e){
            alert("Can't cannect to server:\n"+e.toString());
        }
    }
}

function AddNotice(logName){
    var noticeName = document.getElementById("txtNoticesName");
    var noticeContent = tinyMCE.getInstanceById('FreeTextBoxAdd').getBody().innerHTML;
    
    if(noticeName.value == "") {
        alert("发布公告名称不能为空！");
        return;
    }
    if(noticeContent == ""){
        alert("发布公告内容不能为空！");
        return;
    }
    else{
        if(noticeContent.length > 1500){
            alert("发布公告字数不能大于1000！");
            return;
        }
    }
    var myxhr = new xmlHttpNotices("add");
    if(myxhr){
        try{
            myxhr.doNotices("type=Add&name="+logName+"&noticeName="+encodeURIComponent(noticeName.value)+"&noticeContent="+encodeURIComponent(noticeContent));
        }
        catch(e){
            alert("Can't cannect to server:\n"+e.toString());
        }
    }
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<input type="hidden" id="hiddenPage" value="1" />
<input type="hidden" id="hiddenName" value="<%=logName %>" />
<div id="wm">
    <h4 style="margin-bottom:5px;">发布公告列表</h4>
    <input type="button" onclick="Show('add')" class="btn" style=" font-size:12px;" value="发布公告" />
    <div id="Content" runat="server" style="margin-top:3px;">
    </div>
    <br />
    <div id="AddNotice" style="display:none;">
       <table width="100%" border="0" >
        <tr>
            <td align="left" style="width:100px; font-size:13px; font-weight:100; padding:5px 5px 5px 5px; background-color:#f3f3f3;">发布公告:</td>
            <td style="padding:5px 5px 5px 5px; background-color:#f3f3f3;">&nbsp;</td>
        </tr>
        <tr>
            <td align="left" style="width:100px; padding:5px 5px 5px 5px; background-color:#fbfbfb;">公告名称：</td>
            <td align="left" style=" padding:5px 5px 5px 5px; background-color:#fbfbfb;"><input type="text" id="txtNoticesName" style="width:400px;" maxlength="30" /><font style="color:Red;">*</font></td>
        </tr>
        <tr>
            <td align="left" style="width:100px; padding:5px 5px 5px 5px; background-color:#f3f3f3;">公告内容：</td>
            <td align="left" style="width:100px; padding:5px 5px 5px 5px; background-color:#f3f3f3;">
                <textarea id="FreeTextBoxAdd" name="content" style="width:100%; font-size:13px;" rows="50" cols="20"></textarea>
                (不超过1000个字)
            </td>
        </tr>
        <tr>
            <td align="left" style="width:100px;padding:5px 5px 5px 5px; background-color:#fbfbfb;"><input type="button" value="保存" class="btn" style=" font-size:12px;" onclick="AddNotice('<%=logName %>')" /></td>
            <td style="padding:5px 5px 5px 5px; background-color:#fbfbfb;">&nbsp;</td>
        </tr>
       </table> 
    </div>
    
    <div id="UpdataNotice" style="display:none;">
        <table width="100%" border="0" >
        <tr>
            <td align="left" style="width:100px; font-size:13px; font-weight:100; padding:5px 5px 5px 5px; background-color:#f3f3f3;">编辑公告:</td>
            <td style="padding:5px 5px 5px 5px; background-color:#f3f3f3;">&nbsp;</td>
        </tr>
        <tr>
            <td align="left" style="width:100px; padding:5px 5px 5px 5px; background-color:#fbfbfb;">公告名称：</td>
            <td align="left" style=" padding:5px 5px 5px 5px; background-color:#fbfbfb;">
                <input type="text" id="txtNameId" style="width:400px;" maxlength="30" /><font style="color:Red;">*</font>
            </td>
        </tr>
        <tr>
            <td align="left" style="width:100px; padding:5px 5px 5px 5px; background-color:#f3f3f3;">公告内容：</td>
            <td align="left" style="width:100px; padding:5px 5px 5px 5px; background-color:#f3f3f3;">
                <textarea id="FreeTextBoxEdit" name="content" style="width:100%; font-size:13px;" rows="50" cols="20"></textarea>
                (不超过1000个字)
            </td>
        </tr>
        <tr>
            <td align="left" style="width:100px;padding:5px 5px 5px 5px; background-color:#fbfbfb;">
                <div id="divEdit"></div>
                
            </td>
            <td style="padding:5px 5px 5px 5px; background-color:#fbfbfb;">&nbsp;</td>
        </tr>
       </table> 
    </div> 
</div>
</asp:Content>
