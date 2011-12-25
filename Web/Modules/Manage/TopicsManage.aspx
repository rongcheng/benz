<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_FuncPage.Master" AutoEventWireup="true" CodeBehind="TopicsManage.aspx.cs" Inherits="WebUI.Modules.Manage.TopicsManage" Title="无标题页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    #wm
    {
    	
    	}
#wm span
{
	line-height:35px;

	 display: block;
    	
}
#wmPic
{}
#wmText
{}

span a.btn
{
	text-align:center;
	display:inline-block;
        width: 61px;
        height:21px;
        line-height:21px;
        background-image: url(../../image/imgDetail/button_bg.gif);
        margin-right:5px;
        border:0px solid red;
	}
</style>
<script language="javascript" type="text/javascript">


function a()
{
    if(document.getElementById("addZT").style.display=="")
    {
        document.getElementById("addZT").style.display="none";
        }
    else
    {
        
        document.getElementById("addZT").style.display="";
     
    }
   return ;
   }



</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<div id=wm>
    
    
    <h3>专题列表</h3>
    <br />
    <table width="500" border="0">
  <tr>
    <td height="25" bgcolor="#dddddd">专题名称</td>
    <td bgcolor="#dddddd">图片数量</td>
    <td width="100" bgcolor="#dddddd">操作</td>
  </tr>
  <tr>
    <td height="25">专题一</td>
    <td>29</td>
    <td>编辑 删除</td>
  </tr>
  <tr>
    <td height="25">专题二</td>
    <td>11</td>
    <td>编辑 删除</td>
  </tr>
  <tr>
    <td height="25">专题三</td>
    <td>8</td>
    <td>编辑 删除</td>
  </tr>
  <tr>
    <td height="25">专题四</td>
    <td>16</td>
    <td>编辑 删除</td>
  </tr>
</table>
<br />
<h3><a href="javascript:a();">增加专题</a></h3> 
<div id="addZT" style="display:none">
    <span>专题名称：<input type="text" value=""/></span>
    <span>专题描述：<textarea rows=10 cols=44></textarea></span>
    <span>选择图片：<textarea rows=10 cols=44>弹出一个窗口，可以查询图片，然后添加到这里</textarea></span>
    <span style="margin-top:20px"><a href="javascript:alert('操作完成');" class="btn">确定</a></span> 
   </div>    
</div>
</asp:Content>
