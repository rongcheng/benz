<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="searchLeft.ascx.cs" Inherits="WebUI.UserControls.searchLeft" %>
<div style="padding-top:10px;">
  <div style="text-align:left"><img src="/images/wi.jpg" alt="" /> <span class="new_word"><b> ͼ
        Ƭ �� �� ��</b></span> </div>
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
    
      (��������Ӣ�Ĺؼ��ֻ�ͼƬ��Ų�ѯ)
      <asp:HiddenField ID="hidLastSearch" runat="server" />
        <br />
        ����
    </div>
    <div class="clear"> </div>
    
   <%-- <div style="padding-top: 8px;"> <strong>����</strong> <br />
      <input id="chkHorizontal" checked="checked" name="chkOrientation" type="checkbox"
                value="h" runat="server" />
      <label for="SearchOption1_chkHorizontal">��ͼ</label>&nbsp;
      <input id="chkVertical" checked="checked" name="chkOrientation" type="checkbox" value="v"
                runat="server" />
      <label for="SearchOption1_chkVertical">��ͼ</label><br />
      <input id="chkSquare" checked="checked" name="chkOrientation" type="checkbox" value="s"
                runat="server" />
      <label for="SearchOption1_chkSquare">��ͼ</label>&nbsp;
	  <input id="chkPanoramic" checked="checked" name="chkOrientation" type="checkbox"
                value="p" runat="server" />
      <label for="SearchOption1_chkPanoramic">ȫ��</label>
    </div>--%>
    
    <div>
        <asp:RadioButtonList ID="mapType" runat="server" RepeatColumns="2" RepeatDirection="Vertical" RepeatLayout="Table" TextAlign="right">
        <asp:ListItem Selected="True" Text="ȫ��" Value="p"></asp:ListItem>
        <asp:ListItem Text="��ͼ" Value="h"></asp:ListItem>
        <asp:ListItem Text="��ͼ" Value="v"></asp:ListItem>
        <asp:ListItem Text="��ͼ" Value="s"></asp:ListItem>
        </asp:RadioButtonList>
    </div>
   
  </div>
</div>
<script language="javascript" type="text/javascript">


function check()
{
    if(document.getElementById("SearchOption1_Kwords").value =="")
    {
        alert("������ؼ��� !");
        return false;
    }
    
//    if(!document.getElementById("SearchOption1$chkHorizontal").checked && !document.getElementById("SearchOption1$chkVertical").checked && !document.getElementById("SearchOption1$chkSquare").checked && !document.getElementById("SearchOption1$chkPanoramic").checked)
//    {
//        alert("��ѡ���� !");
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

	    //�ж�rm��rf����
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