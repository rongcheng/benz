<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_FuncPage.Master" AutoEventWireup="true" CodeBehind="PicReview.aspx.cs" Inherits="WebUI.Modules.Manage.PicReview" Title="无标题页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>审核图片</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<style type="text/css">
    #dspic
    {
        margin-bottom: 10px;
        padding-top: 10px;
    }
    #dspic .item
    {
        width: 180px;
        text-align: center;
        border: 0px solid #dddddd;
        float: left;
        margin-top: 10px;
    }
    #dspic .item img
    {
        border: 2px solid #dddddd;
        padding: 1px;
    }
    #dspic .item span
    {
        display: block;
        line-height: 25px;
    }
    #dspic .item span a
    {
    	display:inline-block;
        width: 62px;
        height:21px;
        line-height:21px;
        background-image: url(../../image/imgDetail/button_bg.gif);
        margin-right:5px;
    }
</style>
<h3>待审核的图片</h3>

<div id="dspic">
    <div class="item">
    <img src="http://sany.quanjing.com/imagePreview/170/dmadmin/IMG10070600006.jpg" />
    <span>上传者：马晓燕</span>
    <span>所属部门：市场部</span>
    <span>上传时间：2010-7-5</span>   
    <span><a href="javascript:alert('操作完成');">通过</a><a href="javascript:alert('操作完成');">不通过</a></span> 
    </div>
    

    
    <div class="item">
    <img src="http://sany.quanjing.com/imagePreview/170/dmadmin/IMG10070600005.jpg" />
    <span>上传者：李志文</span>
    <span>所属部门：市场部</span>
    <span>上传时间：2010-7-5</span>    
    <span><a href="javascript:alert('操作完成');">通过</a><a href="javascript:alert('操作完成');">不通过</a></span> 
    </div>
    
    
    <div class="item">
    <img src="http://sany.quanjing.com/imagePreview/170/dmadmin/IMG10070600007.jpg" />
    <span>上传者：张朝阳</span>
    <span>所属部门：市场部</span>
    <span>上传时间：2010-7-5</span>    
    <span><a href="javascript:alert('操作完成');">通过</a><a href="javascript:alert('操作完成');">不通过</a></span> 
    </div>
    
    
    <div class="item">
    <img src="http://sany.quanjing.com/imagePreview/170/dmadmin/IMG10070600008.jpg" />
    <span>上传者：李彦宏</span>
    <span>所属部门：市场部</span>
    <span>上传时间：2010-7-5</span>   
    <span><a href="javascript:alert('操作完成');">通过</a><a href="javascript:alert('操作完成');">不通过</a></span>  
    </div>

</div>


</asp:Content>
