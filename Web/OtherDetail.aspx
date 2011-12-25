<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OtherDetail.aspx.cs" Inherits="WebUI.OtherDetail"   MasterPageFile="~/MPages/QJ_MasterPage.Master"%>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
<style>
.l
{
	display:inline-block;
	width:100px;
	text-align:right;
	margin-right:10px;
	
	}
	.l1
{
	display:inline;
	}
</style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
    </cc1:ToolkitScriptManager>
    <div class="single_photo">
    <table>
        <tr>
           
            <td valign="top" style=" width:610px; border:#cccccc 1px solid;">
            <div id="fullContent" >
            <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td id="td2" class="currentAll" onclick="show('1')"><div style="padding:3px; background-color:#f6f6f1;height:25px;line-height:25px">文件基本信息</div></td>
                  
            </tr>
            </table>
            <div class="single_photo_txt" id="baseDiv">
                <ul>
                    <li><span class="l">文件编号:</span><strong><asp:Literal ID="lb_ItemSerialNum" runat="server"></asp:Literal></strong></li>
                    <li><span class="l">原始文件名:</span><strong><asp:Literal ID="lb_FileName" runat="server"></asp:Literal></strong></li>
                    <li><span class="l">原始文件大小:</span><strong><asp:Literal ID="lb_fileLength" runat="server"></asp:Literal></strong></li>
                    <li><span class="l">标题:</span><strong><asp:Literal ID="lb_Caption" runat="server"></asp:Literal></strong></li>
                    <li><span class="l">作者:</span><strong><asp:Literal ID="lb_Author" runat="server"></asp:Literal></strong></li>
                            
                    <li><span class="l">关键字:</span><strong><asp:Literal ID="lb_Keyword" runat="server"></asp:Literal></strong></li>
                    <li><span class="l">文件类型:</span><strong><asp:Literal ID="lb_ImageType" runat="server"></asp:Literal></strong></li>
                    <li><span class="l">备注:</span><strong><asp:Literal ID="lb_Description" runat="server"></asp:Literal></strong></li>
                    <li><span class="l">上传时间:</span> <strong><asp:Literal ID="lb_uploadDate" runat="server"></asp:Literal></strong></li>
                    <asp:Panel ID="pSource" runat="server">
                    <li><span class="l">有效期:</span><strong><asp:Literal ID="lb_enableDate" runat="server"></asp:Literal></strong></li>
                    </asp:Panel>
                    <li>
                        <span class="l">浏览次数:</span><strong><asp:Literal ID="lb_viewCount" runat="server"></asp:Literal></strong>
                    </li>
                    <li style="height:30px;">
                    </li>
                    <li>
                        <asp:Panel runat="server" ID="pDownload" CssClass="l1"><a href="javascript:OpenDownload('<%=lb_ItemSerialNum.Text %>','<%=lb_ImageType.Text %>','<%=Request["ItemId"] %>','<%=folder %>','other');" class="button" >下载</a> </asp:Panel> 
                        <a class='favorite_folder floatL' href='javascript:void(0)' onclick='addToFolder("<%=Request["ItemId"] %>")'>收藏</a>                     
                    </li>
                </ul>
            </div>
        
            </div>
        </td>
        </tr>
        
    </table>

    </div>
    
    <div style="clear:both;height:20px;"></div>
   <script language="javascript" type="text/javascript">//<img id="imgId" src="image/common/1.png" alt="查看相片详细信息" />
    function addToFolder(itemId,path,serNum){
	    WebAppPost("<%= AppRootPath %>/Modules/CallbackExec.aspx?fun=afolder&itemid="+itemId+"&userId=<%= this.CurrentUser.UserId.ToString()%>"+"&path="+path+"&serNum="+serNum);
	}
	
	function OpenDownload(FileName, FileType, itemId, folder, resourceType){
        var _url = "down.aspx?FileName="+FileName+"&FileType="+FileType+"&itemId="+itemId+"&folder="+folder+"&resourceType="+resourceType;        
        art.dialog({id:'downRedirect', title:'下载图片', iframe:_url, width:300, height:250}).close(function(){}); 
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
