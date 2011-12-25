<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataPicFolder.ascx.cs" Inherits="WebUI.UserControls.DataPicFolder" %>
<script language="javascript" type="text/javascript" src="/Jscript/preview.js"></script>

<br />
<asp:DataList ID="DataList1" runat="server" BackColor="#FFFFFF" RepeatColumns="4" RepeatDirection="Horizontal" EnableViewState="false">
    <ItemTemplate>
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" style="width:180px;height:181px;padding:3px;" valign="bottom">
                   <img id="Img1" alt="" src="<%# GetImgUrl(  Convert.ToString(Eval("FolderName")),Convert.ToString(Eval("ItemSerialNum")),Convert.ToString(Eval("ImageType")))%>" /></td>
            </tr>
            <tr>
                <td align="center">
				<table width="170" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td align="left">
	<span class="pic_id">
                        <%# DataBinder.Eval( Container.DataItem,"Pic_ID")%>
                    </span>
                    <%# GetKind(DataBinder.Eval(Container.DataItem, "pictype"))%></td>
  </tr>
</table>

                   
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table border="0" cellpadding="0" cellspacing="0" style="width:170px;">
                        <tr>
                            <td style="text-align:left;">
                  <a href="PicDetail.aspx?ItemID=<%#Convert.ToString(Eval("ItemId"))%>"  target="_blank"  style="font-size:small">预览</a>                                     　　
                                </td>
                            <td style="text-align:center; width: 33px;">
                                <a href="javascript:lightbox_pic_remove('<%# DataBinder.Eval(Container.DataItem,"Pic_ID") %>','<%= IntFolder %>');">移除</a>
                            </td>
                            <td style="text-align:left;">
                               <%# GetCmd(Convert.ToString(Eval("ItemSerialNum")), Convert.ToString(Eval("ImageType")))%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                转移到：<select id="Folder" onchange="javascript:lightbox_pic_move('<%# DataBinder.Eval(Container.DataItem,"album_id") %>',this.value)" class="inputstyle" style="width:120px;">
                                    <%# SetFolderList() %>
                                </select>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height:30px"></td>
            </tr>
        </table>
    </ItemTemplate>
</asp:DataList>
<script type="text/javascript" language="javascript">

    function downhigh(pic_id,ptype)
	{
    	if (window.confirm("下载会产生费用，请确认是否订购此图！") != 0)
    	{
    	    var href = "/downRedirect.aspx?FileName=" + pic_id + "&FileType=" + ptype;
    	    window.open(href,'_blank','width=600,height=500');
    	}
	}
   
</script>