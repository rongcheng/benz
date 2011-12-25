<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="searchLeft.ascx.cs" Inherits="WebUI.UserControls.searchLeft" %>
<div style="padding-top:10px;">
  <div style="text-align:left"><img src="/images/wi.jpg" alt="" /> <span class="new_word"><b> 图
        片 库 搜 索</b></span> </div>
  <div style="text-align:left">
    <asp:Label ID="labKwords" runat="server" Text=""></asp:Label>
  </div>
  <div style="text-align:left;">
    <div id="sou_an">
      <table onkeydown="javascript:return DefaultButton(event,'<%=this.btnSearch.ClientID %>')">
        <tr>
          <td><asp:TextBox ID="Kwords" runat="server" MaxLength="200" CssClass="inputstyle" ></asp:TextBox></td>
          <td><asp:ImageButton ID="btnSearch" runat="server" ImageUrl="/images/search.jpg" OnClick="btnSearch_Click" /></td>
        </tr>
      </table>
    
      (请输入中英文关键字或图片编号查询)
      <asp:HiddenField ID="hidLastSearch" runat="server" />
        <br />
        方向：
    </div>
    <div class="clear"> </div>
    
   <%-- <div style="padding-top: 8px;"> <strong>方向</strong> <br />
      <input id="chkHorizontal" checked="checked" name="chkOrientation" type="checkbox"
                value="h" runat="server" />
      <label for="SearchOption1_chkHorizontal">横图</label>&nbsp;
      <input id="chkVertical" checked="checked" name="chkOrientation" type="checkbox" value="v"
                runat="server" />
      <label for="SearchOption1_chkVertical">竖图</label><br />
      <input id="chkSquare" checked="checked" name="chkOrientation" type="checkbox" value="s"
                runat="server" />
      <label for="SearchOption1_chkSquare">方图</label>&nbsp;
	  <input id="chkPanoramic" checked="checked" name="chkOrientation" type="checkbox"
                value="p" runat="server" />
      <label for="SearchOption1_chkPanoramic">全部</label>
    </div>--%>
    
    <div>
        <asp:RadioButtonList ID="mapType" runat="server" RepeatColumns="2" RepeatDirection="Vertical" RepeatLayout="Table" TextAlign="right">
        <asp:ListItem Selected="True" Text="全部" Value="p"></asp:ListItem>
        <asp:ListItem Text="横图" Value="h"></asp:ListItem>
        <asp:ListItem Text="竖图" Value="v"></asp:ListItem>
        <asp:ListItem Text="方图" Value="s"></asp:ListItem>
        </asp:RadioButtonList>
    </div>
   
  </div>
</div>
<script language="javascript" type="text/javascript">


function check()
{
    if(document.getElementById("SearchOption1_Kwords").value =="")
    {
        alert("请输入关键字 !");
        return false;
    }
    
//    if(!document.getElementById("SearchOption1$chkHorizontal").checked && !document.getElementById("SearchOption1$chkVertical").checked && !document.getElementById("SearchOption1$chkSquare").checked && !document.getElementById("SearchOption1$chkPanoramic").checked)
//    {
//        alert("请选择方向 !");
//        return false;
//    }
    //return true;
}
</script>
<script type="text/javascript">
var __nonAgentMSDOMBrowser = (window.navigator.appName.toLowerCase().indexOf('explorer') == -1);
function DefaultButton(evt, target) 
{
        var evt = evt ? evt : (window.event ? window.event : null);
        var obj = event.srcElement ? event.srcElement : event.target;
        
        if (evt.keyCode == 13 && !(obj && (obj.tagName.toLowerCase() == "textbox"))) 
        {
            var defaultButton;
            if (__nonAgentMSDOMBrowser) {
                defaultButton = document.getElementById(target);
            }
            else {
                defaultButton = document.all[target];
            }
          
            if (defaultButton && typeof(defaultButton.click) != "undefined") {
                defaultButton.click();
                evt.cancelBubble = true;
                if (evt.stopPropagation) evt.stopPropagation();
                return false;
            }
        }
    return true;
}
</script>
<script type="text/javascript">
	function Catagory_Changed2()
	{	
	
	if(document.getElementById('selCata') == null)
	{
	    return;
	}
	else
	{
    var selObj= document.getElementById("selCata");
		curidx = selObj.selectedIndex;

	    //判断rm、rf类型
	    if(selObj.value == "")
	    {
	        return;
	    }
	    
	    else
	    {
	        SetupImgType2("cata_detail" + curidx);
	    }
	}	
  }
</script>