<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" MasterPageFile="~/MPages/QJ_MasterPage.Master" Inherits="WebUI.Modules.Video.Detail" %>
 <asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
    <link href="UI/qjcss/vrms.css" rel="stylesheet" type="text/css" />
    <script src="/UI/Script/UI.js" language="javascript" type="text/javascript"></script>
    <title><asp:Literal ID="pageTitle" runat="server"></asp:Literal></title>    
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="single_video">
        <div class="single_video_flv">
            
            <div id="container"><a href="http://www.macromedia.com/go/getflashplayer">Get the Flash Player</a> to see this player.</div>
    <script type="text/javascript" src="/ui/flvplayer/swfobject.js"></script>
	<script type="text/javascript">
		var s1 = new SWFObject("/ui/flvplayer/mediaplayer.swf","mediaplayer","600","370","8");
		s1.addParam("allowfullscreen","true");
		s1.addVariable("autostart","false");
		s1.addVariable("width","600");
		s1.addVariable("height","370");
		s1.addVariable("file","<%=flvFilePath%>");
		s1.addVariable("image","<%=imageFilePath %>");
		
		s1.write("container");
	</script>
	
        </div>
        <div class="single_video_txt">
            <ul>
                <li><span>文件编号:</span><strong>
                    <asp:Literal ID="lb_ItemSerialNum" runat="server"></asp:Literal></strong></li>
                <li><span>原始文件名:</span><strong><asp:Literal ID="lb_FileName" runat="server"></asp:Literal></strong></li>
                <li><span>标题:</span><strong><asp:Literal ID="lb_Caption" runat="server"></asp:Literal></strong></li>
                <li><span>文件描述:</span><strong><asp:Literal ID="lb_Description" runat="server"></asp:Literal></strong></li>                
                <li><span>关键字:</span><strong><asp:Literal ID="lb_Keyword" runat="server"></asp:Literal></strong></li>
                <li><span>文件类型:</span><strong><asp:Literal ID="lb_FileType" runat="server"></asp:Literal></strong></li>
                <li><span>文件大小:</span><strong><asp:Literal ID="lb_FileLength" runat="server"></asp:Literal></strong></li>
                <li><span>片长:</span><strong><asp:Literal ID="lb_duration" runat="server"></asp:Literal></strong></li>
                <li><span>尺寸:</span><strong><asp:Literal ID="lb_wh" runat="server"></asp:Literal></strong></li>
                <li><span>比特率:</span><strong><asp:Literal ID="lb_bitrate" runat="server"></asp:Literal></strong></li>
                <li><span>拍摄时间:</span><strong><asp:Literal ID="lb_shotDate" runat="server"></asp:Literal></strong></li>
                <li><span>上传时间:</span> <strong>
                    <asp:Literal ID="lb_uploadDate" runat="server"></asp:Literal></strong></li>
                <li><span>有效期:</span><strong><asp:Literal ID="lb_enableDate" runat="server"></asp:Literal></strong></li>
                <br />
                <li><a href="/downRedirect.aspx?FileName=<%=lb_ItemSerialNum.Text %>&itemId=<%=Request["ItemId"] %>&FileType=<%=lb_FileType.Text %>&folder=<%=folder %>&resourceType=video" target="_blank">下载</a>
                <a class='favorite_folder floatL' href='javascript:void(0)' onclick='addToFolder("<%=Request["ItemId"] %>")'>收藏</a>
                </li>
            </ul>
        </div>
    </div>
    <div style="clear:both;height:20px;"></div>
    <script language="javascript" type="text/javascript">
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
    }</script>
</asp:Content>