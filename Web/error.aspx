<%@ Page Language="C#" EnableSessionState="True" MasterPageFile="~/MPages/QJ_MasterPage.Master" AutoEventWireup="true" CodeBehind="error.aspx.cs" Inherits="WebUI.error" Title="无标题页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<STYLE type=text/css>ul{ list-style:none;}li{ padding-bottom:2px; padding-top:2px;}A:link {	COLOR: #555555; TEXT-DECORATION:underline; }A:visited {	COLOR: #555555; TEXT-DECORATION: none}A:active {	COLOR: #555555; TEXT-DECORATION: none}A:hover {	COLOR: #6f9822; TEXT-DECORATION: none}.text {	FONT-SIZE: 12px; COLOR: #555555; FONT-FAMILY: ""; TEXT-DECORATION:none;}.STYLE1 {font-size: 13px}.STYLE2 {font-size: 12px}.STYLE3 {font-size: 11px}</STYLE>
<script language="javascript" type="text/javascript">
function OpenDetail(){
    if(document.getElementById("<%=divContent.ClientID %>").style.display == "none"){
        document.getElementById("<%=divContent.ClientID %>").style.display = "";
    }
    else{
        document.getElementById("<%=divContent.ClientID %>").style.display = "none";
    }
}
function xmlHttpObject(id){
	var xmlObject = createXmlHttpRequest() ;
	var objectId = id ;
	function createXmlHttpRequest(){
		var xmlHttp ;
		if(window.ActiveXObject){
			xmlHttp = new ActiveXObject("Microsoft.XMLHTTP") ;
		}
		else if(window.XMLHttpRequest){
			xmlHttp = new XMLHttpRequest() ;
		}
		
		return xmlHttp ;
	}
	
	xmlObject.onreadystatechange = processRequest;
	
	function processRequest(){
		if(xmlObject.readyState == 4){
			if(xmlObject.status == 200){
				try{				
					handleServerResponse();
				}
				catch(e){
					alert("Error reading the response:" + e.toString());
				}
			}
			else{
				alert("There was a problem retrieving the date:\n" + xmlObject.statusText);
			}
		}
		else{
			return;
		}
	}
	
	function handleServerResponse(){
		xmlResponse = xmlObject.responseText;
	}
	this.doError = function(body){
	    xmlObject.open("POST", "GetContent.aspx", true);
	    xmlObject.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
	    xmlObject.send(body);
	}
}

window.onload = function(){
//    var html = "<%=strContent %>";
//    
//    if(html == "")
//        return;
    var myxhr = new xmlHttpObject("error");
    if(myxhr){
        try{
            myxhr.doError("type=error");
        }
        catch(e){
            alert("Can't cannect to server:\n"+e.toString());
        }
    }
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<TABLE height="100%" cellSpacing=0 cellPadding=0 width="100%" align=center border=0><TR>    <TD vAlign="middle" align="center">    <TABLE cellSpacing=0 cellPadding=0 width=500 align=center border=0>    <TR>        <TD width=17 height=17><IMG height=17 src="images/co_01.gif" width=17 /></TD>        <TD width=316 background="images/bg01.gif"></TD>        <TD width=17 height=17><IMG height=17 src="images/co_02.gif" width=17 /></TD>    </TR>    <TR>        <TD background=images/bg02.gif></TD>        <TD>            <TABLE class=text cellSpacing=0 cellPadding=10 width="100%" align=center border=0>            <TR>                <TD>                    <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>                        <TR>                            <TD width=20>　</TD>                            <TD><IMG height=66 src="images/404error.gif" width=400 /></TD>                        </TR>                    </TABLE>                </TD>            </TR>            <TR>            <TD>                <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>                <TR>                    <TD height=1></TD>                </TR>                </TABLE>                <BR />                <TABLE class=text cellSpacing=0 cellPadding=0 width="100%" border=0>                <TR>                    <TD width=20>　</TD>                    <TD align="left">                        <p>请尝试以下操作：</P>                        <ul>                            <li>1.如果您已经在地址栏中输入该网页的地址，请确认其拼写正确。</li>                            <li>2.打开<A href="/"><font color="#BA1C1C"><%=Request.Url.Authority %></font></A>主页，然后查找指向您感兴趣信息的链接。</li>                            <li>3.单击<A href="javascript:history.back(1)"><font color="#BA1C1C">后退</font></A>链接，尝试其他链接。</li>                        </ul>                       <BR />                        <BR />                        <P align="right">如果您在浏览本站时，多次出现此错误，请与管理员联系</P>                        <div align="left"><a href="javascript:OpenDetail()">详细信息</a></div>                     </TD>                 </TR>                 </TABLE>                 <div id="divContent" style="display:none; margin-top:5px;" runat="server">                 </div>             </TD>             </TR>             </TABLE>          </TD>        <TD background="images/bg03.gif"></TD>    </TR>    <TR>        <TD width=17 height=17><IMG height=17 src="images/co_03.gif" width=17 /></TD>        <TD background="images/bg04.gif" height=17></TD>        <TD width=17 height=17><IMG height=17 src="images/co_04.gif" width=17 /></TD>    </TR>    </TABLE>    <TABLE class=text cellSpacing=0 cellPadding=0 width=500 align=center border=0>    <TR>        <TD></TD>     </TR>     </TABLE></TD>
</TR>
</TABLE>
</asp:Content>
