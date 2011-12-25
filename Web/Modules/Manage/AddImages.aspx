<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddImages.aspx.cs" Inherits="WebUI.Modules.Manage.AddImages" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>填加专题图片</title>

    <link href="../../UI/Css/feature.css" type="text/css" rel="Stylesheet" />
    <style type="text/css">
    #wm{}
    #wm span{line-height:35px;display: block;}
    #wmPic{}
    #wmText{}
    .btn{text-align:center;display:inline-block;width: 61px;height:21px;line-height:21px;background-image: url(../../image/imgDetail/button_bg.gif);margin-right:5px;border:0px solid red;}
    .btn{ height:23px; width:90px; line-height:23px; background:url(../../images/nav_bg.gif) repeat-x; border:solid 1px #666; cursor:pointer;}
    body {font-size:12px; font-family:Arial, Helvetica, sans-serif; margin:0; color:Black; width:98%;}
    a {color:Black;}
    .roll { width:110px; text-align:center;position: absolute;z-index: 999; 
            background-color:White; padding-top:5px;filter:alpha(opacity=80);   
      -moz-opacity:0.5;   
      -khtml-opacity: 0.5;   
      opacity: 0.5;    }
    
    .OnMouseUp
    {
         border-bottom:dashed 1px #d4d0c8; border-top:dashed 1px #d4d0c8;
         border-left:dashed 1px #d4d0c8; border-right:dashed 1px #d4d0c8;cursor:pointer;	
         background-color:#fafafa;
         padding:2px 2px 2px 2px;width:22%;
    }
    .OnMouseUp div{border-top:#d4d0c8 dashed 1px; padding:2px 2px 2px 2px; margin-top:2px;}
    .grvpager
 {
 	text-align:left;
}.grvpager{	background-color:white!important; color:#333!important; margin-top:3px;}
.grvpager a{background-color:white!important; color:#333!important;}

.grvpager table{width:auto;}
.grvpager table a,.grvpager table span
{
border:1px solid #CCCCCC;
float:left;
height:20px;
line-height:20px;
margin-right:2px;
overflow:hidden;
padding:0 6px;
	}
.grvpager table span
{
background-color:#DDDDDD;
border-color:#CCCCCC;
color:#555555;
font-weight:700;
}


.grvpager table a
{

}
.grvpager table a:hover
{
border:1px solid #000000;

}


.grvpager a,.grvpager span
{
border:1px solid #CCCCCC;
float:left;
height:20px;
line-height:20px;
margin-right:2px;
overflow:hidden;
padding:0 6px;
	}
.grvpager span
{
background-color:#DDDDDD;
border-color:#CCCCCC;
color:#555555;
font-weight:500;
}


.grvpager a
{

}
.grvpager a:hover
{
border:1px solid #000000;

}
    </style>
    <script src="../../UI/Js/js.js" type="text/javascript"></script>
    <script src="../../UI/Js/create.js" type="text/javascript"></script>
    <script src="../../UI/Script/jquery-1.4.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
    var htmlload = "<img alt='' src='../../image/common/loading.gif'/>&nbsp;数据加载中...";
    function OnMUp(id){
        var obj = document.getElementById(id+"td");
        obj.className = "OnMouseUp";
    }
    function OnMOut(id){
        var obj = document.getElementById(id+"td");
        obj.className = "imagetd";
    }
    function GetTop(){
        var obj = document.getElementById("naviContent");
        if(obj.getAttribute('loaded') == 'true'){
            return;
        }
        else{
            obj.innerHTML = htmlload;
            var myxhr = new xmlHttpObject("naviContent");
            if(myxhr){
                try{
                    myxhr.doFeature("type=Top");
                }
                catch(e){
                    alert("Can't cannect to server:\n"+e.toString());
                }
            }
        }
    }
    function OnChild(parentId){
        var obj = document.getElementById(parentId);
        if(obj.style.display == "none"){
            obj.style.display = "";
            document.getElementById("image"+parentId).src = "../../image/common/reduce.gif";
            if(obj.innerHTML == ""){
                if(obj.getAttribute('loaded') == 'true'){
                    return;
                }
                else{
                    obj.innerHMTL = htmlload;
                    var myxhr = new xmlHttpObject(parentId);
                    if(myxhr){
                        try{
                            myxhr.doFeature("type=Child&parentId="+parentId);
                        }
                        catch(e){
                            alert("Can't cannect to server:\n"+e.toString());
                        }
                    }
                }
            }
        }
        else{
            obj.style.display = "none";
            document.getElementById("image"+parentId).src = "../../image/common/plus.gif";
        }
    }
    
    function OnShow(catalogId){
        var param = "";
        var item = parent.saveImages.document.getElementsByName("newAdd");
        for(var i=0;i<item.length;i++){
            param += item[i].value + ";";
        }
        var id = document.getElementById("hiddenUserId").value;
        var featureId = document.getElementById("hiddenFeatureId").value;
        var obj = document.getElementById("Content");
        var t = document.getElementById("hiddentype").value;
        if(obj.getAttribute('loaded') == 'true'){
            return;
        }
        else{
            obj.innerHMTL = htmlload;
            var myxhr = new xmlHttpObject("Content");
            if(myxhr){
                try{
                    myxhr.doFeature("type=Catalog&catalogId="+catalogId+"&size=32&index=1&id="+id+"&featureId="+featureId+"&param="+param+"&t="+t);
                }
                catch(e){
                    alert("Can't cannect to server:\n"+e.toString());
                }
            }
        }
    }
    function Search(){
        var txt = document.getElementById("txtContent");
        var word = txt.value;
        if(word == ""){
            alert("检索词不能为空");
            return;
        }
        var param = "";
        var item = parent.saveImages.document.getElementsByName("newAdd");
        for(var i=0;i<item.length;i++){
            param += item[i].value + ";";
        }
        var id = document.getElementById("hiddenUserId").value;
        var featureId = document.getElementById("hiddenFeatureId").value;
        var obj = document.getElementById("Content");
        var t = document.getElementById("hiddentype").value;
        if(obj.getAttribute('loaded') == 'true'){
            return;
        }
        else{
            obj.innerHMTL = htmlload;
            var myxhr = new xmlHttpObject("Content");
            if(myxhr){
                try{
                    myxhr.doFeature("type=Search&search="+encodeURIComponent(word)+"&size=32&index=1&id="+id+ "&featureId="+featureId+"&param="+param+"&t="+t);
                }
                catch(e){
                    alert("Can't cannect to server:\n"+e.toString());
                }
            }
        }
    }
    function GetSearchPage(word, size, index){
        var param = "";
        var item = parent.saveImages.document.getElementsByName("newAdd");
        for(var i=0;i<item.length;i++){
            param += item[i].value + ";";
        }
        var obj = document.getElementById("Content");
        var featureId = document.getElementById("hiddenFeatureId").value;
        var t = document.getElementById("hiddentype").value;
        if(obj.getAttribute('loaded') == 'true'){
            return;
        }
        else{
            obj.innerHMTL = htmlload;
            var myxhr = new xmlHttpObject("Content");
            if(myxhr){
                try{
                    myxhr.doFeature("type=Search&search="+encodeURIComponent(word)+"&size="+size+"&index="+index + "&featureId="+featureId + "&param="+param+"&t="+t);
                }
                catch(e){
                    alert("Can't cannect to server:\n"+e.toString());
                }
            }
        }
    }
    
    function GetCatalogPage(catalogId, size, index){
        var param = "";
        var item = parent.saveImages.document.getElementsByName("newAdd");
        for(var i=0;i<item.length;i++){
            param += item[i].value + ";";
        }
        var obj = document.getElementById("Content");
        var featureId = document.getElementById("hiddenFeatureId").value;
        var t = document.getElementById("hiddentype").value;
        if(obj.getAttribute('loaded') == 'true'){
            return;
        }
        else{
            obj.innerHMTL = htmlload;
            var myxhr = new xmlHttpObject("Content");
            if(myxhr){
                try{
                    myxhr.doFeature("type=Catalog&catalogId="+catalogId+"&size="+size+"&index="+index + "&featureId="+featureId+"&param="+param+"&t="+t);
                }
                catch(e){
                    alert("Can't cannect to server:\n"+e.toString());
                }
            }
        }
    }
    
    function AddImage(cid, name){
        var obj = document.getElementById(cid);
        var id = obj.id;
        var img = document.getElementById(id+"img");
        var w = img.width/2 + "px";
        var h = img.height/2+"px";
        var src = img.src;
        var bool = false;
        if(obj.checked){
            parent.saveImages.DeleteImage(id);
            obj.checked = false;
            document.getElementById(id+"check").style.display = "none";
        }
        else{
            if(parent.saveImages.AddImage(id, name, w, h, src)){
                alert("["+name+"]已经被选择！");
                return;
            }
            else{
                obj.checked = true;
                document.getElementById(id+"check").style.display = "block";
            }
        }
    }
    function OnCheckBox(obj){
        var item;
        if(obj.checked){
            OnFull();
            item = document.getElementsByName("fullname");
            for(var i=0;i<item.length;i++){
                item[i].checked = true;
            }
        }
        else{
            OnClear();
            item = document.getElementsByName("fullname");
            for(var i=0;i<item.length;i++){
                item[i].checked = false;
            }
        }
    }
    function OnFull(){
        var item = document.getElementsByTagName("img");
        for(var i=0;i<item.length;i++){
            if(item[i].id.indexOf("img") != -1){
                var id = item[i].id.substring(0, 36);
                var obj = document.getElementById(id);
                if(obj){
                    if(obj.checked == false){
                        AddImage(id, obj.value);
                    }
                }
            }
        }
    }
    function OnClear(){
        var item = document.getElementsByTagName("img");
        for(var i=0;i<item.length;i++){
            if(item[i].id.indexOf("img") != -1){
                var id = item[i].id.substring(0, 36);
                var obj = document.getElementById(id);
                if(obj)
                    obj.checked = false;
                var objimg = document.getElementById(id+"check");
                if(objimg)
                    objimg.style.display = "none";
            }
        }
        parent.saveImages.document.getElementById("AddUl").innerHTML = "";
    }
    </script>
    <script language="javascript" type="text/javascript">
    function OnMouseOver(obj){
        obj.className = "key2";
    }
    function OnMouseOut(obj){
        obj.className = "key1";
    }
    function OnClickKey(objid, id){
        var obj = document.getElementById(objid);
        var v = document.getElementById(id);
        if(v.style.display == "none"){
            obj.className = "key3";
            v.style.display = "";
        }
        else{
            obj.className = "key1";
            v.style.display = "none";
        }
    }
    
    function OnClickNavi(objid, id){
        var obj = document.getElementById(objid);
        var v = document.getElementById(id);
        
        if(v.style.display == "none"){
            obj.className = "key3";
            v.style.display = "";
            if(v.innerHTML == ""){
                GetTop();
            } 
        }else{
            obj.className = "key1";
            v.style.display = "none";
        }
    }
    
    window.onload = function(){
        OnClickNavi('naviDiv', 'naviContent');
        OnClickKey('keyDiv', 'keyContent');
    }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="hiddenUserId" value="<%=userId %>" />
    <input type="hidden" id="hiddenFeatureId" value="<%=featureId %>" />
    <input type="hidden" id="hiddentype" value="<%=type %>" />
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td align="left" valign="top" width="200px">
            <div id="left" style="width:95%; height:700px; float:left; padding: 5px 2px 5px 2px;">
            <div class="key1" id="keyDiv" onmouseover="OnMouseOver(this)" onclick="OnClickKey('keyDiv', 'keyContent')">关键词检索</div>
            <div id="keyContent" style="display:none;">
            输入检索词：<br />
            <input type="text" id="txtContent" style="width:140px; margin:5px 5px 5px 5px;" maxlength="100" onkeydown="if(event.keyCode=='13'){document.getElementById('btnSearch').focus();}"/><br />
            <input type="button" id="btnSearch" class="btn" value="查询" style="margin:0px 5px 5px 5px;" onclick="Search()" />
            </div>
            <div class="key1" id="naviDiv" onmouseover="OnMouseOver(this)" onclick="OnClickNavi('naviDiv', 'naviContent')">分类检索</div>
            <div id="naviContent" style="display:none;"></div> 
            </div>
        </td>
        <td align="left" valign="top">
            <div id="midden" style="width:100%; height:700px;border-left:#d4d0c8 solid 1px; margin:0; padding-top:5px; float:left;">
            <div id="Content"></div>
            </div>
        </td>
    </tr>
    </table>
    </form>
</body>
</html>
