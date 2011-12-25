<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataResource.ascx.cs" Inherits="WebUI.UserControls.DataResource" %>
<script language="javascript" type="text/javascript">
function OpenDownload(FileName, FileType, itemId, folder, resourceType){
    var _url = "down.aspx?FileName="+FileName+"&FileType="+FileType+"&itemId="+itemId+"&folder="+folder+"&resourceType="+resourceType;        
    artDialog({id:'downRedirect', title:'下载图片', url:_url, width:300, height: 360}).close(function(){}); 
}
function notpassover(obj,id)
{
    //alert("fff");
    //obj.style.borderColor='blue';
    //obj.style.backgroundColor='yellow'
    //obj.innerHTML="dddddddddddd";
    var _id="notpass-"+id;
    obj.style.display="none";
    document.getElementById(_id).style.display="block";
    
}
function notpassout(obj,id)
{
    var _id="imgContainer1-"+id;
    obj.style.display="none";
    document.getElementById(_id).style.display="block";
    
}
</script>
<style>
.pcss{border:0px solid red;height:100%;}
#myLightBox
{
    border: 1px solid #999999;
    height: 100px;
    padding: 5px;
    position: absolute;
    width: 164px;
    display:none;
    background-color:White;
}
#myLightBox ul
{
    margin-top:5px;
}
#myLightBox ul li
{
    line-height:22px;   
}
#myLightBox ul li a
{
    line-height:22px;
}
#myLightBox a
{
   /*float:right;*/    
}
.btnCloseLightBox
{
    float:right;    
}
</style>
<div class="resourceShow">
<asp:DataList ID="DataList1" runat="server"  
    RepeatDirection="Horizontal" EnableViewState="false" OnItemDataBound="DataList1_ItemDataBound" RepeatLayout="Flow">
    <ItemTemplate>
        <div class="imgContainer" id="imgContainer-<%# Convert.ToString(Eval("Id"))%>" >
            <div class="container" <asp:Literal ID="lrover" runat="server"></asp:Literal> >
<asp:Panel ID="pImage" runat="server" Visible="false" CssClass="pcss">
<a href="/PicDetail.aspx?ItemID=<%# Convert.ToString(Eval("Id"))%>" target="_blank" class="box">
<img id="Img1" alt="" onmousemove="showPic(this.src,event,this);" onmouseout="hiddenPic();"
src="<%# GetImgUrl(  Convert.ToString(Eval("ServerFileName")),Convert.ToString(Eval("ServerFolderName")))%>" />
  <asp:Label CssClass="rStatus" ID="spanStatus" runat="server" Visible="false" Text="通过"></asp:Label>
 </a>
</asp:Panel>
<asp:Panel ID="pVideo" runat="server" Visible="false">
<div class="box">
<object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0' width='170' height='128' id='QuanJingFilm' align='middle'>
        <param name='allowScriptAccess' value='always' />
        <param name='allowFullScreen' value='false' />
        <param name='movie' value='../../UI/flash/QJFilm.swf' />
        <param name='quality' value='autohigh' />
        <param name='bgcolor' value='#ffffff' />
        <param name='wmode' value='opaque' />
        <param name='FlashVars' value='videoUrl=<%# GetSmallFlvUrl(Convert.ToString(Eval("ServerFolderName")),Convert.ToString(Eval("ItemSerialNUmber")),Convert.ToInt32(Eval("status")))%>&imgUrl=<%# GetVideoImgUrl(Convert.ToString(Eval("ServerFolderName")),Convert.ToString(Eval("ItemSerialNUmber")),Convert.ToInt32(Eval("status")))%>&SerailNumber=<%#Eval("Id").ToString() %>' />
        <embed src='../../UI/flash/qjFilm.swf' quality='autohigh' bgcolor='#ffffff' width='170' height='128' wmode='opaque' flashvars='videoUrl=<%# GetSmallFlvUrl(Convert.ToString(Eval("ServerFolderName")),Convert.ToString(Eval("ItemSerialNUmber")),Convert.ToInt32(Eval("status")))%>&imgUrl=<%# GetVideoImgUrl(Convert.ToString(Eval("ServerFolderName")),Convert.ToString(Eval("ItemSerialNUmber")),Convert.ToInt32(Eval("status")))%>&SerailNumber=<%#Eval("Id").ToString() %>'
            name='QuanJingFilm' align='middle' allowscriptaccess='always' allowfullscreen='false' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' />
</object>    
</div>            
</asp:Panel>
<asp:Panel ID="pDocument" runat="server" Visible="false">
<a href="/OtherDetail.aspx?ItemID=<%# Convert.ToString(Eval("Id"))%>" target="_blank" class="box">
<img id="Img3" alt="" onmousemove="showPic(this.src,event,this);" onmouseout="hiddenPic();"
src="<%# GetDocumentImage(  Convert.ToString(Eval("ServerFileName")),Convert.ToString(Eval("ServerFolderName")))%>" />
  
 </a>
</asp:Panel>
<asp:Panel ID="pOther" runat="server" Visible="false">
<a href="/OtherDetail.aspx?ItemID=<%# Convert.ToString(Eval("Id"))%>" target="_blank" class="box"><img id="Img2" alt="" src="/images/other.jpg" /></a>
</asp:Panel>                         
            </div>
<div id="notpass-<%# Convert.ToString(Eval("Id"))%>"  onmouseout="notpassout(this,'<%# Convert.ToString(Eval("Id"))%>')" class="container" style="display:none;border:1px solid #cccccc; background-color:#f2f2f8;"><br />未通过的原因：<%#GetNotPassReason(Eval("ID").ToString())%></div>
            
<p class="first">
<asp:Literal ID="imgInfo" runat="server" Visible="false"></asp:Literal>    
<span style="float:right"><input  type="checkbox" value='<%# Convert.ToString(Eval("Id"))%>' name="" id="ckbItemId" runat="server" visible="true" /></span>

<em><span title="<%# Convert.ToString(Eval("Caption"))%>"><%#GetLeftString(Convert.ToString(Eval("Caption")), 15)%></span></em>
</p>              
            <div class="pic_info">

                <div class="func1">
                <asp:Panel ID="pPreview" runat="server" Visible="false"  CssClass="fLeft">
                <a class='small_sample  floatL' href='<%# GetPreviewUrl(Eval("ResourceType").ToString(),Eval("ID").ToString()) %>' target='_blank'>预览</a>
                </asp:Panel>
                <asp:Panel ID="pFavor" runat="server"  Visible="false" CssClass="fLeft">
                <a class='small_sample floatL' href='javascript:void(0)' onclick='addToFolder1("<%#Eval("ID").ToString()%>",this)'>收藏</a>
                
                </asp:Panel>
                <asp:Panel ID="pDownload" runat="server"  Visible="false" CssClass="fLeft">
                <!--a class='small_sample' target='_blank' href='downRedirect.aspx?FileName=<%#Eval("ItemSerialNumber").ToString()%>&FileType=<%#System.IO.Path.GetExtension(Eval("ItemSerialNumber").ToString()) %>&itemId=<%#Eval("ID").ToString()%>&folder=<%#Eval("ServerFolderName").ToString()%>&resourceType=<%#Eval("ResourceType").ToString()%>'>下载</a-->
                <a class="small_sample" href="javascript:OpenDownload('<%#Eval("ItemSerialNumber").ToString()%>','<%#System.IO.Path.GetExtension(Eval("ItemSerialNumber").ToString()) %>','<%#Eval("ID").ToString()%>','<%#Eval("ServerFolderName").ToString()%>','<%#Eval("ResourceType").ToString()%>');">下载</a>
                </asp:Panel>
                
                <asp:Panel ID="pEdit" runat="server"  Visible="false" CssClass="fLeft">
                <a class='small_sample' target='_blank' href='/Modules/ResourceEdit.aspx?itemId=<%#Eval("ID").ToString()%>'>编辑</a>
                </asp:Panel>
                <asp:Panel ID="pValidate" runat="server"  Visible="false" CssClass="fLeft">
                <a class='small_sample' href="javascript:doValidate('<%#Eval("ID").ToString()%>')">审核</a>
                </asp:Panel>
                <asp:Panel ID="pTiJiao" runat="server"  Visible="false" CssClass="fLeft">
                <a class='small_sample' href="javascript:void(0)" onclick="doValidateTiJiao('<%#Eval("ID").ToString()%>',this)">提交</a>
                </asp:Panel>
                <asp:Panel ID="pNotPass_1" runat="server"  Visible="false" CssClass="fLeft">
                <a class='small_sample' href=javascript:void(0)' title='<%#GetNotPassReason(Eval("ID").ToString())%>'>未过</a>
                </asp:Panel>
                <asp:Panel ID="pIsProcessing_1" runat="server"  Visible="false" CssClass="fLeft">
                <a class='small_sample' href="javascript:void(0)">在审</a>
                </asp:Panel>
                <asp:Panel ID="pFavDelete" runat="server"  Visible="false" CssClass="fLeft">
       
                <a class='small_sample' href='javascript:void(0)' onclick='DelItem("<%#Eval("ID").ToString()%>","<%#Eval("UserID").ToString()%>",event)'>删除</a>
                </asp:Panel>
                
                <div style="clear:both"></div>
                </div>
                <asp:Literal ID="imageV" runat="server" Visible="false"></asp:Literal>
                
                 
            </div>
            
        
        </div>
        
    </ItemTemplate>
</asp:DataList>
<div style="clear:both;"></div>
<div id="myLightBox">
<span><a class="btnCloseLightBox" href="javascript:void(0)" onclick="$('#myLightBox').hide()" title="关闭收藏夹" >x</a>请选择收藏夹</span>
<ul>
<asp:Repeater ID="rptLightBox" runat="server">
<ItemTemplate><li><a href="javascript:addToLightBox('<%#Eval("id")%>')" title="加入收藏夹"><%#Eval("Title") %></a></li>  </ItemTemplate>
</asp:Repeater>
</ul>
<input type="hidden" id="hidId" />
</div>
</div>
<div id="<%=JarId%>" style="z-index:1000;display: none; position: absolute;border:1px solid #555555;padding:3px;background-color:#dddddd">
</div> 

<script type="text/javascript" language="javascript"> 
    function myBrowser(){
        var userAgent = navigator.userAgent; //取得浏览器的userAgent字符串
        var isOpera = userAgent.indexOf("Opera") > -1;

        if (isOpera){return "Opera"}; //判断是否Opera浏览器 
        if (userAgent.indexOf("Firefox") > -1){return "FF";} //判断是否Firefox浏览器 
        if (userAgent.indexOf("Safari") > -1){return "Safari";} //判断是否Safari浏览器 
        if (userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1 && !isOpera){return "IE";} ; //判断是否IE浏览器
    }
    var setOut;
    var util={
        setTimeout:function(fun, delay) {
            if(typeof fun == 'function'){
                var argu = Array.prototype.slice.call(arguments,2);
                var f = (function(){
                    fun.apply(null, argu);
                }
                );
                return window.setTimeout(f, delay);
            }
            return window.setTimeout(fun,delay);
        }
    }

    function showPic(rawUrl, ev){
        var x,y; 
        x = ev.clientX; 
        y = ev.clientY; 
        if($get("<%=JarId%>").innerHTML == "")
            $get("<%=JarId%>").innerHTML = "<img id='<%=JarImageId%>' src='" + rawUrl.replace("/170/","/400/") + "'>"; 
        if(setOut)
                window.clearTimeout(setOut);
        if($get("<%=JarId%>").style.display == "none"){
            setOut = util.setTimeout(ShowImage, 1000, x, y);
        }
        else{
            ShowImage(x, y);
        }
    } 

    function ShowImage(w, h){ 
        if(!document.getElementById('<%=JarImageId%>')){
            return;
        }
        var x, y;
        x = w + getScrollXY().x; 
        y = h + getScrollXY().y; 
        $get("<%=JarId%>").style.display = "block"; 
        var imageWidth = 0; 
        var imageWidthH = 0;
        if($get('<%=JarImageId%>')){
            imageWidth = $get('<%=JarImageId%>').width;
            imageWidthH = $get('<%=JarImageId%>').height;
        }

        var ioffset = w + imageWidth - document.documentElement.clientWidth;
        x += 10;//坐标差
        if( ioffset > 0 ){
            x = x - imageWidth -30;
        }
      
        var iy = h + imageWidthH - document.documentElement.clientHeight;
        y+=10;
        if( iy >0 ){
            y = y - imageWidthH -30;
        }

        $get("<%=JarId%>").style.position = 'absolute';
        $get("<%=JarId%>").style.left = x +"px"; 
        $get("<%=JarId%>").style.top = y + "px";
        //$get("<%=JarId%>").style.display = "block"; 
    } 

    function hiddenPic()
    { 
      $get("<%=JarId%>").innerHTML = ""; 
      $get("<%=JarId%>").style.display = "none"; 
      if(setOut)
        window.clearTimeout(setOut);
    } 
    function downhigh(pic_id,ptype,itemId,folder)
	{
	    var href = "/downRedirect.aspx?FileName=" + pic_id + "&FileType=" + ptype+"&itemId="+itemId+"&folder="+folder;
	    window.open(href,'_blank','width=600,height=500');
	}
	
	function addToFolder(itemId,path,serNum)
	{
	    WebAppPost("<%= AppRootPath %>/Modules/CallbackExec.aspx?fun=afolder&itemid="+itemId+"&userId=<%= this.CurrentUser.UserId.ToString()%>"+"&path="+path+"&serNum="+serNum);
	}

    function addToFolder1(itemId,o)
    {
        var item="#imgContainer-"+itemId;    
        var p=$(item).position();
        //alert(p.top+"\n"+p.left);
        //alert($(this)[0].style.left);
        //var p= $(this).parent("div").offset();
        //alert(p.left);
        $("#hidId").val(itemId);        
        //$("#myLightBox").css("left",p.left-202).css("top",p.top-215);
        $("#myLightBox").css("left",p.left).css("top",p.top+$(item).height()-3);
        $("#myLightBox").slideDown();
        
        
    }
    
    function addToLightBox(lightboxid)
    {
        
        //alert("lightboxid"+lightboxid+"\r\nresourceid:"+$("#hidId").val());
        $.ajax({type: "GET", url: "/handlers/lightboxhandler.ashx",data: "action=addtolightbox&resourceid="+ $("#hidId").val()+"&lightboxid="+lightboxid,
            success: function(msg){
                alert("已经成功的添加到收藏夹");              
                $("#myLightBox").slideUp();  
            }
        });
    
    }
    
    
	function OnWebRequestCompleted(executor, eventArgs) 
    {
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

