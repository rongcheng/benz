﻿<%@ Page Language="C#" EnableSessionState="True" MasterPageFile="~/MPages/QJ_MasterPage.Master" AutoEventWireup="true" CodeBehind="error.aspx.cs" Inherits="WebUI.error" Title="无标题页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<STYLE type=text/css>
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
<TABLE height="100%" cellSpacing=0 cellPadding=0 width="100%" align=center border=0>
</TR>
</TABLE>
</asp:Content>