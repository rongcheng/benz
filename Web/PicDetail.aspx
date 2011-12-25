<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MPages/QJ_MasterPage.Master"
    Codebehind="PicDetail.aspx.cs" Inherits="WebUI.PicDetail" EnableSessionState="True" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
    <script src="UI/Js/js.js"type="text/javascript"></script>
    <title><asp:Literal ID="pageTitle" runat="server"></asp:Literal></title>
    
    <script language="javascript" type="text/javascript">
    var bool = true;
    var htmlload = "<img alt='' src='/image/common/loading.gif'/>&nbsp;数据加载中...";
    function GetExif(){
        var obj = document.getElementById("exifDiv");
        //if(obj.innerHTML == ""){
            var foldername = document.getElementById("hiddenFolderName").value;//"<%=folderName %>";
            var filename = document.getElementById("hiddenServiceFileName").value;//"<%=serviceFileName %>";
        
            if(obj.getAttribute('loaded') == 'true'){
                return;
            }
            else{
                obj.innerHMTL = htmlload;
                var myxhr = new xmlHttpObject("exifDiv");
                if(myxhr){
                    try{
                        myxhr.doContent("type=exif&folder="+ encodeURIComponent(foldername) +"&file="+ encodeURIComponent(filename));
                    }
                    catch(e){
                        alert("Can't cannect to server:\n"+e.toString());
                    }
                }
            }
        //}
    }
    function OpenDownload(FileName, FileType, itemId, folder, resourceType){
        var _url = "down.aspx?FileName="+FileName+"&FileType="+FileType+"&itemId="+itemId+"&folder="+folder+"&resourceType="+resourceType;        
        art.dialog({id:'downRedirect', title:'下载图片', iframe:_url, width:300, height:250}).close(function(){}); 
    }
    var B = false;
    var preid = "<%=preId %>";
    var nextid = "<%=nextId %>";
    var guid = "<%=guid %>";
    function showPic(obj,ev){  
        if(preid == "" || nextid == ""){
            return;
        }
        var x,y; 
        x = mousePosition(ev).x;
        y = mousePosition(ev).y;

        var x1 = elemOffset(obj).x;//table的x坐标
        var y1 = elemOffset(obj).y;//table的y坐标

        if(x < x1 + 300){
            obj.style.cursor = "url('images/SizeE.cur'), auto";
            B = true;
        }
        else{
            obj.style.cursor = "url('images/SizeWE.cur'), auto";
            B = false;
        }
    }
        
    function OnHref(){
        if(preid == "" || nextid == ""){
            return;
        }
        if(B){
            location.href = "PicDetail.aspx?type=1&ItemID="+preid+"&guid="+guid;
        }
        else{
            location.href = "PicDetail.aspx?type=1&ItemID="+nextid+"&guid="+guid;
        }
    }
    function mousePosition(e) {if(e.pageX && e.pageY) {return {x:e.pageX,y : e.pageY};}var scrollElem = (document.compatMode && document.compatMode!="BackCompat")? document.documentElement : document.body;return {x:e.clientX + scrollElem.scrollLeft,y: e.clientY + scrollElem.scrollTop};}
    function elemOffset(elem){var t = elem.offsetTop;var l = elem.offsetLeft;while( elem = elem.offsetParent){t += elem.offsetTop;l += elem.offsetLeft;}return {x:l,y:t};}
    
    
    function show(type){
        switch(type){
            case "0":
                document.getElementById("td1").className = "currentAll";
                document.getElementById("td2").className = "nowAll";
                document.getElementById("baseDiv").style.display = "none";
                document.getElementById("exifDiv").style.display = "";
                GetExif();
            break;
            case "1":
                document.getElementById("td1").className = "nowAll";
                document.getElementById("td2").className = "currentAll";
                document.getElementById("baseDiv").style.display = "";
                document.getElementById("exifDiv").style.display = "none";
            break;
        }
    }
    </script>
    
    <style type="text/css">
    .exifvalue{
	    padding:2px 2px 2px 5px;
	    font-weight:normal;
    }

    .exifname{
	    color: #990000;
	    font-weight:bold;
    }
    .currentAll{border-bottom:#cccccc solid 1px;
              text-align:center; padding:3px 3px 3px 3px; background-color:#eeeeee; }
    .nowAll{ border-bottom:#cccccc solid 1px; 
         text-align:center; padding:3px 3px 3px 3px;
          cursor:pointer;}
.Contentu { 
margin-left:0px; width:100%;border:0px solid red;text-align:left; float:left;
  background-color:#eeeeee;
} 
.Contentu ul { 
} 
.Contentu li { 
 text-align: center; 
 list-style-type: none; 
 float: left;  
 height:70px;
 color: #000000;
} 
.Contentu li span
{
	 width:100px;
	}
.Contentu li a { 
 display: block; 
 color: #000000; 
 text-decoration: none;  
 float: left; 
 height:70px;
} 
.Contentu li a:hover { 
 color: #F3D46E; 
 background-repeat: repeat-x; 
 height:70px;
} 
.liimg
{
    width:87px; height:60px; 
    filter:alpha(opacity=30);
    -moz-opacity:0.5;
    opacity: 0.5;
    margin-bottom:5px;
    margin-top:5px;
    border-left:solid 0px black;
    border-right:solid 1px black;
    border-top:solid 1px black;
    border-bottom:solid 1px black;
}
	
.liimg1
{
    border:solid 2px #c8c8c8;
    width:117px;
    height:70px;
}
#<%=pDownload.ClientID %>
{
    display:inline;
}
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
    </cc1:ToolkitScriptManager>
    <input type="hidden" id="hiddenFolderName" value="<%=folderName %>" />
    <input type="hidden" id="hiddenServiceFileName" value="<%=serviceFileName %>" />
    <input type="hidden" id="hiddenPre" value="<%=preId %>" />
    <input type="hidden" id="hiddenNext" value="<%=nextId %>" />
    <input type="hidden" id="hiddenItemId" value="<%=id %>" />
    
    <div class="single_photo">
    <table>
        <tr>
            <td style=" background-color:#eeeeee; border-right:#cccccc 1px solid;border-left:#cccccc 1px solid; border-top:#cccccc 1px solid;border-bottom:#cccccc 1px solid;">
            <div class="single_photo_img" id="imgContent" style="border-width:0px;">
            <table border="0" width="600px" height="400px" onmousemove="showPic(this,event);" onclick="OnHref();">
                <tr>
                    <td align="center" valign="middle">
                        <img id="imgsrc" alt="" src="CreateImage.aspx?f=<%=folderName %>&s=<%=serviceFileName %>" />
                    </td>
                </tr>
            </table>
            </div>
        </td>
            <td valign="top" style=" width:310px; border-right:#cccccc 1px solid; border-top:#cccccc 1px solid;border-bottom:#cccccc 1px solid;">
            <div id="fullContent" >
            <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td id="td2" class="currentAll" onclick="show('1')">基本信息</td>
                <td id="td1" class="nowAll" onclick="show('0')">EXIF信息</td>    
            </tr>
            </table>
            <div class="single_photo_txt" id="baseDiv">
                <table width="100%">
                    <tr>
                        <td align="left" valign="top" width="65px"><span>标题:</span></td>
                        <td align="left" valign="top" style="word-wrap:break-word;word-break:break-all;"><strong><asp:Literal ID="lb_Caption" runat="server"></asp:Literal></strong></td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" width="65px"><span>图片编号:</span></td>
                        <td align="left" valign="top" style="word-wrap:break-word;word-break:break-all;"><strong><asp:Literal ID="lb_SN" runat="server"></asp:Literal></strong></td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" width="65px"><span>图片尺寸:</span></td>
                        <td align="left" valign="top" style="word-wrap:break-word;word-break:break-all;"><strong><asp:Literal ID="lb_Size" runat="server"></asp:Literal></strong></td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" width="65px"><span>方向:</span></td>
                        <td align="left" valign="top" style="word-wrap:break-word;word-break:break-all;"><strong><asp:Literal ID="lb_Hvsp" runat="server"></asp:Literal></strong></td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" width="65px"><span>作者:</span></td>
                        <td align="left" valign="top" style="word-wrap:break-word;word-break:break-all;"><strong><asp:Literal ID="lb_Author" runat="server"></asp:Literal></strong></td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" width="65px"><span>所属分类:</span></td>
                        <td align="left" valign="top" style="word-wrap:break-word;word-break:break-all;"><strong><asp:Literal ID="lb_Category" runat="server"></asp:Literal></strong></td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" width="65px"><span>关键字:</span></td>
                        <td align="left" valign="top" style="word-wrap:break-word;word-break:break-all;"><strong><asp:Literal ID="lb_Keyword" runat="server"></asp:Literal></strong></td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" width="65px"><span>上传时间:</span></td>
                        <td align="left" valign="top" style="word-wrap:break-word;word-break:break-all;"><strong><asp:Literal ID="lb_uploadDate" runat="server"></asp:Literal></strong></td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" width="65px"><span>拍摄时间:</span></td>
                        <td align="left" valign="top" style="word-wrap:break-word;word-break:break-all;"><strong><asp:Literal ID="lb_shotDate" runat="server"></asp:Literal></strong></td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" width="65px"><span>原文件名:</span></td>
                        <td align="left" valign="top" style="word-wrap:break-word;word-break:break-all;"><strong><asp:Literal ID="lb_FileName" runat="server"></asp:Literal></strong></td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" width="65px"><span>文件大小:</span></td>
                        <td align="left" valign="top" style="word-wrap:break-word;word-break:break-all;"><strong><asp:Literal ID="lb_fileLength" runat="server"></asp:Literal></strong></td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" width="65px"><span>备注:</span></td>
                        <td align="left" valign="top" style="word-wrap:break-word;word-break:break-all;"><strong><asp:Literal ID="lb_Description" runat="server"></asp:Literal></strong></td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" width="65px"><span>浏览次数:</span></td>
                        <td align="left" valign="top" style="word-wrap:break-word;word-break:break-all;"><strong><asp:Literal ID="lb_viewCount" runat="server"></asp:Literal></strong></td>
                    </tr>
                    
                    
                </table>
                <table width="100%" height="50px">
                    <tr>
                        <td valign="bottom">
                        <div style="float:left;">
                            <asp:Panel runat="server" ID="pDownload"><a href="javascript:OpenDownload('<%=lb_ItemSerialNum.Text %>','<%=lb_ImageType.Text %>','<%=Request["ItemId"] %>','<%=folder %>','image');" class="button" >下载</a> </asp:Panel>
                        </div>
                        <div style="float:left;">
                            <asp:Panel runat="server" ID="pEdit"><a href="Modules/ResourceEdit.aspx?itemId=<%=Request["ItemId"] %>" target="_blank" class="button" >编辑</a> </asp:Panel>
                        </div>
                            <a class='favorite_folder floatL' href='javascript:void(0)' onclick='addToFolder("<%=Request["ItemId"] %>")'>收藏</a>
                            <a class='favorite_folder floatL' href='javascript:void(0)' onclick='fullScreenView("<%=Request["ItemId"] %>")'>全屏</a>
                        </td>
                    </tr>
                </table>
                <asp:Literal ID="lb_ItemSerialNum" runat="server" Visible="false"></asp:Literal>
                <asp:Literal ID="lb_ImageType" runat="server" Visible="false"></asp:Literal>
            </div>
            <div style="display:none;" class="single_photo_txt" id="exifDiv">
            </div>
            </div>
        </td>
        </tr>
        <tr>
            <td colspan="2" style="border-right:#cccccc 1px solid;border-left:#cccccc 1px solid;border-bottom:#cccccc 1px solid;">
            <div id="Content" runat="server" class="Contentu">
            </div>
            </td>
        </tr>
    </table>

    </div>
    
    <div style="clear:both;height:20px;"></div>
    <script language="javascript" type="text/javascript">//<img id="imgId" src="image/common/1.png" alt="查看相片详细信息" />
    function fullScreenView(itemId){
        window.open("PicFullScreen.aspx?ItemId="+itemId, '', 'toolbar=no, menubar=no, scrollbars=yes, resizable=yes,location=no, status=yes','_blank'); 
    }

    function addToFolder(itemId,path,serNum){
	    WebAppPost("<%= AppRootPath %>/Modules/CallbackExec.aspx?fun=afolder&itemid="+itemId+"&userId=<%= this.CurrentUser.UserId.ToString()%>"+"&path="+path+"&serNum="+serNum);
	}

	function OnWebRequestCompleted(executor, eventArgs) {
        if(executor.get_responseAvailable()) 
        {
             if(executor.get_responseData()=="true")
             {
                alert('添加成功');
             }
             else
             {
                alert("添加失败:资源已存在收藏夹中或收藏夹的配额已满");
             }

        }
        else
        {
            if (executor.get_timedOut())
                alert("操作超时");
            else if (executor.get_aborted())
                alert("请求已经终止");
            else
                alert("添加失败");
        }
    }
    </script>
</asp:Content>
