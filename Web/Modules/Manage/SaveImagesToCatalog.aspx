<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SaveImagesToCatalog.aspx.cs" Inherits="WebUI.Modules.Manage.SaveImagesToCatalog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<head>
    <title>无标题页</title>
    <style type="text/css">
        .btn
        {
            text-align: center;
            display: inline-block;
            width: 61px;
            height: 21px;
            line-height: 21px;
            background-image: url(../../image/imgDetail/button_bg.gif);
            margin-right: 5px;
            border: 0px solid red;
        }
        .btn
        {
            height: 23px;
            width: 90px;
            line-height: 23px;
            background: url(../../images/nav_bg.gif) repeat-x;
            border: solid 1px #666;
            cursor: pointer;
        }
        body
        {
            font-size: 12px;
            font-family: Arial, Helvetica, sans-serif;
            margin: 0;
            color: Black;
            width: 98%;
        }
        a
        {
            color: Black;
        }
        .roll
        {
            width: 98%;
            text-align: left;
            background-color: White;
            padding-top: 5px;
            margin-left: 10px;
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
            text-align: center;
            margin: 0;
            width: 10%;
            float: left;
        }
        .OnMouseUp
        {
             border-bottom:dashed 1px #d4d0c8; border-top:dashed 1px #d4d0c8;
             border-left:dashed 1px #d4d0c8; border-right:dashed 1px #d4d0c8;cursor:pointer;	
             background-color:#fafafa;
        }
        .OnLoad
        {
        	border-bottom:dashed 1px White; border-top:dashed 1px White;
        	border-left:dashed 1px White; border-right:dashed 1px White;
        	}
    </style>

    <script src="../../UI/Js/js.js" type="text/javascript"></script>

    <script src="../../UI/Js/create.js" type="text/javascript"></script>

    <script src="../../UI/Script/jquery-1.4.min.js" type="text/javascript"></script>
    
    <link href="/UI/artDialog211/skin/chrome/chrome.css" rel="stylesheet" type="text/css" />
    <script src="/UI/artDialog211/artDialog.js" type="text/javascript"></script>


    <script language="javascript" type="text/javascript">
    function OnMUp(id){
        var obj = document.getElementById(id+"table");
        obj.className = "OnMouseUp";
    }
    function OnMOut(id){
        var obj = document.getElementById(id+"table");
        obj.className = "OnLoad";
    }
    function AddImage(id, name, w, h, src){
//        var id = obj.value;
//        var img = document.getElementById(id+"img");
//        var w = img.width/2 + "px";
//        var h = img.height/2+"px";
//        var src = img.src;
        var bool = false;
        var ul = document.getElementById("AddUl");
        var item = document.getElementsByName("newAdd");
        for(var i=0;i<item.length;i++){
            if(item[i].id == "add"+id){
                bool = true;
                break;
            }
        }
        if(bool){
            alert("["+name+"]已经被选择！");
            return bool;
        }
        else{
            var li = document.createElement("LI");
            var html = "<table border=\"0\" width=\"100%\" height=\"115px\" id=\""+id+"table\" class=\"OnLoad\">";
            html += "<tr>";
            html += "<td align=\"left\" valign=\"bottom\" height=\"86px\">";
            html += "<img alt=\""+name+"\" style=\"cursor:pointer;\" onclick=\"Delete('"+id+"')\" onmouseover=\"OnMUp('" + id + "')\" onmouseout=\"OnMOut('" + id + "')\" border=\"0\" src=\""+src+"\" width=\""+w+"\" height=\""+h+"\"  />";
            html += "</td>";
            html += "</tr>";
            html += "<tr><td align=\"left\" valign=\"bottom\">";
            if(name.length > 6)
                name = name.substring(0, 5)+"...";
            html += "<input type=\"checkbox\" id=\"add"+id+"\" checked value=\""+id+"\" name=\"newAdd\" style=\"display:none;\">";
            html += name+"<img src=\"../../images/delete.GIF\" id=\""+id+"\" onclick=\"Delete('"+id+"')\">"; 
            html += "</td></tr>";
            html += "</table>";
            html += "<input type=\"hidden\" id=\"hidden"+id+"\" />";
            li.innerHTML = html;
            ul.appendChild(li);
        }
    }
    function DeleteImage(id){
        var addobj = document.getElementById("hidden"+id);
        if(addobj)
            $(addobj).parent("li").remove();
        
    }
    function Save(){
        var item = document.getElementsByName("newAdd");
        if(item.length == 0){
            alert("请先选择图片");
            return;
        }
        var featureId = document.getElementById("hiddenFeatureId").value;
        var type = document.getElementById("hiddentype").value;
        var param = "";
        for(var i=0;i<item.length;i++){
            if(item[i].checked == true){
                param += item[i].value + ";";
            }
        }
        
        var myxhr = new xmlHttpObject("save");
        if(myxhr){
            try{
                myxhr.doFeature("type=Save&featureId="+featureId+"&param="+encodeURIComponent(param)+"&t="+type);
            }
            catch(e){
                alert("Can't cannect to server:\n"+e.toString());
            }
        }
    }
    function Clear(){
        var item = document.getElementsByName("newAdd");
        for(var i = 0;i<item.length;i++){
            var obj = parent.addImages.document.getElementById(item[i].value);
            if(obj){
                obj.checked = false;
            }
            var imgObj = parent.addImages.document.getElementById(item[i].value+"check")
            if(imgObj)
                imgObj.style.display = "none";
        }
        
        document.getElementById("AddUl").innerHTML = "";
    }
    function Delete(id) {
        //var id = obj.id;
        var o = document.getElementById("hidden"+id);
        $(o).parent("li").remove();
        var box = parent.addImages.document.getElementById(id);
        if(box)
            box.checked = false; 
        var check = parent.addImages.document.getElementById(id+"check");
        if(check)
            check.style.display = "none";
    }
    
    
    function openCatalog()
    {
    
    
    var item = document.getElementsByName("newAdd");
        if(item.length == 0){
            alert("请先选择图片");
            return;
        }
        var featureId = document.getElementById("hiddenFeatureId").value;
        var type = document.getElementById("hiddentype").value;
        var param = "";
        for(var i=0;i<item.length;i++){
            if(item[i].checked == true){
                param += item[i].value + ";";
            }
        }
        
        
        artDialog({id:'dg_test4330', title:'设置资源分类:', url:'../CatalogSelBatch.aspx?itemid=&itemnum=', width:500, height:200}).close(function(){}); return false
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="hiddenUserId" value="<%=userId %>" />
    <input type="hidden" id="hiddenFeatureId" value="<%=featureId %>" />
    <input type="hidden" id="hiddentype" value="<%=type %>" />
    <div id="selectContent" class="roll">
        <table width="100%" border="0" cellpadding="1" cellspacing="1">
            <tr>
                <td>
                    <%--<input type="button" id="btnSave" style="width: 45px;" class="btn" value="保存" onclick="Save()" />--%>
                    <input type="button" id="btnSave" style="width: 65px;" class="btn" value="设置分类" onclick="openCatalog()" />
                    
                <%--     <a href="#" onclick="openCatalog()">设置分类</a>--%>
                    &nbsp;
                    <input type="button" id="btnDelete" style="width: 45px;" class="btn" value="清除" onclick="Clear()" />
                </td>
            </tr>
            <tr>
                <td align="left" style="text-align: left">
                    <ul id="AddUl" class="selectClass">
                    </ul>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
