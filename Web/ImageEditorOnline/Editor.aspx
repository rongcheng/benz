<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Editor.aspx.cs" Inherits="WebUI.ImageEditorOnline.Editor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <script src="../UI/Script/jquery-1.2.6.pack.js" type="text/javascript"></script>
    <script src="../UI/Script/imageEditorOnline.js" type="text/javascript"></script>

    <script src="../UI/artDialog/artDialog.js" type="text/javascript"></script>
   

    <link href="../UI/Css/global.css" rel="stylesheet" type="text/css" />
    <link href="../UI/artDialog/skin/aero/aero.css" rel="stylesheet" type="text/css" />
    <link href="../UI/Css/imageEditorOnline.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div id="header1">
    <ul>
    <li><a href="#" id="aGray" >黑白</a></li>
    <li><a href="javascript:void(0)" id="aRotate">旋转</a></li>
    <li><a href="javascript:doEffect('gray')" >剪裁</a></li>
    <li><a href="javascript:doEffect('gray')" >3D效果</a></li>
    <li><a href="javascript:void(0)" id="aBorder" >边框</a></li>
    <li><a href="javascript:void" id="aFlop" >翻转</a></li>
    <li><a href="#" id="btn31" class="fav" >翻转 A</a></li>
    <li><a href="javascript:doEffect('watermark')" >水印</a></li>
    <li><a href="javascript:doEffect('resize')" >缩放</a></li>

    </ul>
    </div>
    <div class="cb"></div>
    <div id="content">
    <div id="sourceImage"><img src="rc2.jpg" /></div>
    <div id="destinationImage"></div>
    
    </div>
   
<form id="frmParam" action="">

</form>
</body>
</html>
