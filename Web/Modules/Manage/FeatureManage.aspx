<%@ Page Language="C#" Theme="MainSkin" MasterPageFile="~/MPages/QJ_FuncPage.Master" AutoEventWireup="true" CodeBehind="FeatureManage.aspx.cs" Inherits="WebUI.Modules.Manage.FeatureManage" Title="无标题页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="../../UI/Script/jquery-1.4.min.js" type="text/javascript"></script>
<script src="../../UI/Js/js.js" type="text/javascript"></script>
    <style type="text/css">
    #wm{}

#wmPic{}
#wmText{}
span a.btn{text-align:center;display:inline-block;width: 61px;height:21px;line-height:21px;background-image: url(../../image/imgDetail/button_bg.gif);margin-right:5px;border:0px solid red;}
</style>
<link href="../../UI/Css/feature.css" rel="Stylesheet" type="text/css" />

<script language="javascript" type="text/javascript">
var htmlload = "<img alt='' src='../../image/common/loading.gif'/>&nbsp;数据加载中...";
function ShowConver(path, featureId, fileName, folderName){
    var srcEdit = "";
    var srcAdd = "";
    var hiddenedit = "";
    var hiddenadd = "";
    
    if(document.getElementById("editImg")){
        srcEdit = document.getElementById("editImg").src;
        document.getElementById("editImg").src = path;
    }
    if(document.getElementById("CoverImage")){
        hiddenedit = document.getElementById("CoverImage").value;
        document.getElementById("CoverImage").value = fileName;
    }
    if(document.getElementById("addImg")){
        srcAdd = document.getElementById("addImg").src;
        document.getElementById("addImg").src = path;
    }
    if(document.getElementById("txtCoverImage")){
        hiddenadd = document.getElementById("txtCoverImage").value;
        document.getElementById("txtCoverImage").value = fileName;
    }
    if(confirm("你确定把这张图片设为封面？")){
        var myxhr = new xmlHttpObject("cover");
        if(myxhr){
            try{
                myxhr.doFeature("type=Cover&featureId="+featureId+"&name="+fileName+"&src="+encodeURIComponent(path)+"&foldername="+folderName);
            }
            catch(e){
                alert("Can't cannect to server:\n"+e.toString());
            }
        }
    }
    else{
        if(document.getElementById("editImg")){
            document.getElementById("editImg").src = srcEdit;
        }
        if(document.getElementById("CoverImage")){
            document.getElementById("CoverImage").value = hiddenedit;
        }
        if(document.getElementById("addImg")){
            document.getElementById("addImg").src = srcAdd;
        }
        if(document.getElementById("txtCoverImage")){
            document.getElementById("txtCoverImage").value = hiddenadd;
        }
    }
}
function Delete(id){
    if(confirm("你确定要删除？")){
        var myxhr = new xmlHttpObject("delete");
        if(myxhr){
            try{
                myxhr.doFeature("type=Delete&id="+id);
            }
            catch(e){
                alert("Can't cannect to server:\n"+e.toString());
            }
        }
    }
}
function GetImagePage(featureId, size, index){
    var obj = document.getElementById("DetailContent");
    if(obj.getAttribute('loaded') == 'true'){
        return;
    }
    else{
        obj.innerHMTL = htmlload;
        var myxhr = new xmlHttpObject("DetailContent");
        if(myxhr){
            try{
                myxhr.doFeature("type=Image&featureId="+featureId+"&size="+size+"&index="+index);
            }
            catch(e){
                alert("Can't cannect to server:\n"+e.toString());
            }
        }
    }
}
function GetFeaturesPage(userName, size, index){
    document.getElementById("hiddenPage").value = index;
    var obj = document.getElementById("<%=Content.ClientID %>");
    if(obj.getAttribute('loaded') == 'true'){
        return;
    }
    else{
        obj.innerHMTL = htmlload;
        var myxhr = new xmlHttpObject("<%=Content.ClientID %>");
        if(myxhr){
            try{
                myxhr.doFeature("type=Page&name="+userName+"&size="+size+"&index="+index);
            }
            catch(e){
                alert("Can't cannect to server:\n"+e.toString());
            }
        }
    }
}
function Show(type){
    if(type == "add"){
        var item = document.getElementsByTagName("tr");
        for(var i=0;i<item.length;i++){
            item[i].style.backgroundColor = "White";
        }
        document.getElementById("AddFeature").style.display = "";
        document.getElementById("UpdataFeature").style.display = "none";
        document.getElementById("DetailContent").style.display = "none";
        document.getElementById("DetailContent").innerHTML = "";
        var str = "<table width=\"100%\" border=\"0\" ><tr><td align=\"left\" style=\"width:100px; font-size:13px; font-weight:100;\">增加专题:</td>"
        +"</tr><tr><td align=\"left\" style=\"width:100px;\">专题名称：</td><td align=\"left\"><input type=\"text\" id=\"txtFeatureName\" style=\"width:300px;\" maxlength=\"20\" /><font style=\"color:Red;\">*</font></td>"
        +"</tr><tr><td align=\"left\" style=\"width:100px;\">专题描述：</td><td align=\"left\"><input type=\"text\" id=\"txtFeatureDes\" style=\"width:550px\" maxlength=\"150\" /></td>"
        +"</tr><tr><td align=\"left\" style=\"width:100px;\">状态：</td>"
            +"<td align=\"left\"><select id=\"selectaddid\"><option value=\"0\" selected=\"selected\">下线</option><option value=\"1\">上线</option>"
               +"</select></td></tr><tr><td align=\"left\" style=\"width:100px;\">封面图片：</td><td align=\"left\"><input type=\"hidden\" id=\"txtCoverImage\" /><img id=\"addImg\" src=\"\" alt=\"封面\" />(在添加的图片中选择本专题封面)</td>"
        +"</tr><tr><td align=\"left\" style=\"width:100px;\"><input type=\"button\" value=\"保存\" class=\"btn\" style=\"font-size:12px;\" onclick=\"AddFeature('<%=logName %>')\" /></td><td></td></tr></table> ";
        document.getElementById("AddFeature").innerHTML = str;
    }
    else{
        document.getElementById("AddFeature").style.display = "none";
        document.getElementById("AddFeature").innerHTML = "";
        document.getElementById("UpdataFeature").style.display = "";
        document.getElementById("DetailContent").style.display = "";
        document.getElementById("DetailContent").innerHTML = "";
    }
}

function UpdateFeature(featureId, logName){
    var featureName = document.getElementById("FeatureName");
    var coverImage = document.getElementById("CoverImage");
    var featureDes = document.getElementById("FeatureDes");
    var obj = document.getElementById("selectupdateid");
    
    if(featureName.value == "") {
        alert("专题名称不能为空！");
        return;
    }
    var v = obj.options[obj.selectedIndex].value;
    
    var myxhr = new xmlHttpObject("update");
    if(myxhr){
        try{
            myxhr.doFeature("type=Update&name="+logName+"&featureId="+featureId+"&featureName="+encodeURIComponent(featureName.value)+"&cover="+encodeURIComponent(coverImage.value)+"&des="+encodeURIComponent(featureDes.value)+"&state="+v);
        }
        catch(e){
            alert("Can't cannect to server:\n"+e.toString());
        }
    }
}

function AddFeature(logName){
    var featureName = document.getElementById("txtFeatureName");
    var coverImage = document.getElementById("txtCoverImage");
    var featureDes = document.getElementById("txtFeatureDes");
    var obj = document.getElementById("selectaddid");
    
    if(featureName.value == "") {
        alert("专题名称不能为空！");
        return;
    }
    var v = obj.options[obj.selectedIndex].value;
    
    var myxhr = new xmlHttpObject("add");
    if(myxhr){
        try{
            myxhr.doFeature("type=Add&name="+logName+"&featureName="+encodeURIComponent(featureName.value)+"&cover="+encodeURIComponent(coverImage.value)+"&des="+encodeURIComponent(featureDes.value)+"&state="+v);
        }
        catch(e){
            alert("Can't cannect to server:\n"+e.toString());
        }
    }
}

function AddImage(featureId){
    //window.open("AddImages.aspx?featureId="+featureId,"add","width=1000,height=620,top=0,left=0,Location=yes,Toolbar=yes,Resizable=yes,scrollbars=yes");
    window.open("ImageFrame.aspx?featureId="+featureId,"add","width="+(screen.availWidth-10)+",height="+screen.availHeight-30+",top=0,left=0,Location=yes,Toolbar=yes,Resizable=yes,scrollbars=yes");
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<input type="hidden" id="hiddenPage" value="1" />
<input type="hidden" id="hiddenName" value="<%=logName %>" />
<div id="wm">
    <h4 style="margin-bottom:5px;">专题列表</h4>
    <input type="button" onclick="Show('add')" class="btn" style=" font-size:12px;" value="增加专题" />
    <div id="Content" runat="server" style="margin-top:3px;"></div>
    <br />
    <br />
    <div id="AddFeature" style="display:none;"></div>
    <div id="UpdataFeature"></div> 
    <div id="DetailContent" style="padding-top:3px;"></div>   
</div>
</asp:Content>
