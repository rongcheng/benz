<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default1.aspx.cs" EnableViewState="false" Inherits="WebUI.Default1" %>

<%@ Register Src="UserControls/QJ_Bottom.ascx" TagName="Bottom" TagPrefix="uc3" %>
<%@ Register Src="UserControls/QJ_Header.ascx" TagName="Header" TagPrefix="uc2" %>
<%@ Register Src="UserControls/QJ_Search_Default.ascx" TagName="Search_Default" TagPrefix="uc1" %>
<%@ Register Src="UserControls/QJ_MenuDiv.ascx" TagName="MenuDiv" TagPrefix="uc5" %>

<%@ Register Src="UserControls/QuickLink.ascx" TagName="QuickLink" TagPrefix="uc_4" %>
<%@ Register Src="UserControls/statControl.ascx" TagName="statControl" TagPrefix="uc_3" %>
<%@ Register Src="UserControls/CatalogMenu91.ascx" TagName="CatalogMenu" TagPrefix="uc_1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>全景视觉资源管理系统</title>   
    <link href="/UI/qjcss/index.css" rel="stylesheet" type="text/css" />     
    <script type="text/javascript" language="javascript" src="UI/qjjs/jquery-1.2.6.pack.js"></script> 
    <script type="text/javascript" src="UI/qjjs/common.js"></script> 
    <script type="text/javascript" src="UI/qjjs/swfobject.js"></script> 
</head>
<body  >
    <form id="form1" runat="server" onsubmit="return false;">     
        <uc2:Header ID="Header1" ShowNav="1"  runat="server" />   
        <uc1:Search_Default ID="Search_Default1" runat="server" />
            
            
            <div id="media_flash" class="content" style="padding-top:8px;border:0px solid red">           

    <div class="main_content">
        <object id="FlashID" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="780"
            height="365">
            <param name="movie" value="/UI/flash/showPIC6.swf" />
            <param name="quality" value="high" />
            <param name="wmode" value="opaque" />
            <param name="swfversion" value="6.0.65.0" />
            <!-- 此 param 标签提示使用 Flash Player 6.0 r65 和更高版本的用户下载最新版本的 Flash Player。如果您不想让用户看到该提示，请将其删除。 -->
            <param name="expressinstall" value="/UI/flash/expressInstall.swf" />
            <!-- 下一个对象标签用于非 IE 浏览器。所以使用 IECC 将其从 IE 隐藏。 -->
            <!--[if !IE]>-->
            <object type="application/x-shockwave-flash" data="/UI/flash/showPIC6.swf" width="780"
                height="365">
                <!--<![endif]-->
                <param name="quality" value="high" />
                <param name="wmode" value="opaque" />
                <param name="swfversion" value="6.0.65.0" />
                <param name="expressinstall" value="/UI/flash/expressInstall.swf" />
                <!-- 浏览器将以下替代内容显示给使用 Flash Player 6.0 和更低版本的用户。 -->
                <div>
                    <h4>
                        此页面上的内容需要较新版本的 Adobe Flash Player。</h4>
                    <p>
                        <a href="http://www.adobe.com/go/getflashplayer">
                            <img src="http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif"
                                alt="获取 Adobe Flash Player" /></a></p>
                </div>
                <!--[if !IE]>-->
            </object>
            <!--<![endif]-->
        </object>
    </div>
                
    <div class="sidebar">
        <div class="library_box">
            <h3>资源库信息</h3>
            <div class="library">
                <div class="row">
                    <uc_3:statControl ID="statControl" runat="server" />
                </div>
                <!--end of row-->
                
            </div>
            <!--end of library-->
        </div>
        <uc_1:CatalogMenu ID="cataMenu91" runat="server" />
    </div>
    <!-- end of main_content -->
                    
                    
                    
            </div>
            
            
            
<div class="content">
	
</div>
<!-- End content -->


<uc5:MenuDiv ID="MenuDiv1" runat="server" />
 <uc3:Bottom ID="Bottom1" runat="server" />
<script type="text/javascript" language="javascript">
$("#txt_Keyword").focus(); 
</script>
</form>
</body>
</html>
