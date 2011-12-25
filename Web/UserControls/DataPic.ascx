<%@ Control Language="C#" AutoEventWireup="true" Codebehind="DataPic.ascx.cs" Inherits="WebUI.UserControls.DataPic" %>
<table>
    <tr>
        <td>
            <asp:DataList ID="DataList1" BackColor="#FFFFFF" runat="server" RepeatColumns="4"
                RepeatDirection="Horizontal" Width="745px" EnableViewState="false" ItemStyle-VerticalAlign="bottom">
                <ItemTemplate>
                    <div class="imgContainer">
                        <div class="container">
                            <a href="PicDetail.aspx?ItemID=<%# Convert.ToString(Eval("ItemId"))%>" target="_blank">
                                <img id="Img1" alt="" onmousemove="showPic(this.src,event);" onmouseout="hiddenPic();"
                                    src="<%# GetImgUrl(  Convert.ToString(Eval("FolderName")),Convert.ToString(Eval("ItemSerialNum")),Convert.ToString(Eval("ImageType")))%>" />
                            </a>
                        </div>
                        <div class="pic_info">
                            <p class="first">
                                <em>
                                    <%--<asp:Literal ID="picSerNum" runat="server" Text='<%# Convert.ToString(Eval("ItemSerialNum"))%>'></asp:Literal>--%>
                                    <span title="<%# Convert.ToString(Eval("filename"))%>"><%#GetLeftString(Convert.ToString(Eval("filename")), 20) %></span>
                                </em>
                            </p>
                            <p>
                                <a class="small_sample  floatL" href="PicDetail.aspx?ItemID=<%#Convert.ToString(Eval("ItemId"))%>"
                                    target="_blank">预览</a>
                                <%# GetCmd(Eval("ItemSerialNum").ToString(), Eval("ImageType").ToString(), Eval("ItemId").ToString(), Eval("FolderName").ToString())%>
                            </p>
                            <p>
                                <a class="favorite_folder floatL" href="javascript:void(0)" onclick="addToFolder('<%# Eval("ItemId")%>','<%# WebUI.UIBiz.CommonInfo.GetImageUrl(0,Eval("FolderName").ToString(),Eval("ItemSerialNum").ToString(),Eval("ImageType").ToString()) %>','<%# Eval("ItemSerialNum").ToString()%>')">
                                    收藏夹</a></p>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </td>
    </tr>
</table>
<div id="<%=JarId%>" style="display: none; position: absolute; z-index: 1;">
</div>

<script type="text/javascript" language="javascript">
    function showPic(rawUrl,ev)
    {  
         var x,y; 
         x = ev.clientX; 
         y = ev.clientY;       
            $get("<%=JarId%>").innerHTML = "<img id='<%=JarImageId%>' src='" + rawUrl.replace("/170/","/400/") + "'>"; 
         
         x += getScrollXY().x; 
         y += getScrollXY().y; 
         var imageWidth =  $get('<%=JarImageId%>').width;
         var imageWidthH =  $get('<%=JarImageId%>').height;
         var ioffset = ev.clientX+imageWidth - document.documentElement.clientWidth;

         x += 10;//坐标差
         
         if( ioffset > 0 ){
            x = x - imageWidth-20;
         }
         var iy = ev.clientY+320 - document.documentElement.clientHeight;
         y+=10;
         if( iy >0 ){
            y = y - imageWidthH-20;
         }
         
         $get("<%=JarId%>").style.position = 'absolute';
         $get("<%=JarId%>").style.left = x +"px"; 
         $get("<%=JarId%>").style.top = y + "px";
         $get("<%=JarId%>").style.display = "block"; 
    } 

    function hiddenPic()
    { 
      $get("<%=JarId%>").innerHTML = ""; 
      $get("<%=JarId%>").style.display = "none"; 
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
                alert("添加失败:图片已存在或收藏夹的配额已满");
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

