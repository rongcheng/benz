<%@ Page Language="C#" AutoEventWireup="true" Inherits="WebUI.DownHigh" Codebehind="DownHigh.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=WebUI.UIBiz.CommonInfo.WebSite_Title %></title>
<%--<meta name="keywords" content="全景正片,全景视觉,全景视拓,全景,图片,图片库,创意图片,编辑图片,新闻图片,专题图片,小样图,高分辨,摄影师,在线图片,图片下载,图片搜索,版权管理,免版税,运动图片,娱乐图片,专题推荐,收藏夹,奥运图片,东方人物,中国元素,版权杂志,人物图片" />
<meta name="description" content="全景图片库--中国最大的创意平台" />--%>
   <%-- <script language="javascript" type="text/javascript" src="/jscript/request.js"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div><%=strFile%>
     请稍后，如果不能正常下载，请点击上面的黄色提示条，并选择“下载文件”选项即可。<br /> <a href='javascript:window.close()'>关闭</a>
    </div>
    </form>
</body>
</html>
