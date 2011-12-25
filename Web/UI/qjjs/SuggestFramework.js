/*
 * Suggest Framework
 * Copyright (c) 2005-06 Matthew Ratzloff <matt@builtfromsource.com>
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */

var sfw = new Array();

String.prototype.decode = function()
{
	return decodeURI(this);
};

String.prototype.encode = function()
{
	var result = "";
	if(this == "") return this;

	if(typeof encodeURIComponent == "function")
	{
		result = encodeURIComponent(this);
	}
	else
	{
		var alpha  = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_-";
		var string = this.toUTF8();
		result = "";
		for(var i = 0; i < string.length; i++)
		{
			if(alpha.indexOf(string.charAt(i)) == -1)
				result += "%" + string.charCodeAt(i).toHex();
			else
				result += string.charAt(i);
		}
	}
	return result;
};

String.prototype.toHex = function()
{
	var hex = "0123456789ABCDEF";
	return hex.charAt(this.value >> 4) + hex.charAt(this.value & 0xF);
};

String.prototype.toUTF8 = function()
{
	var a, b, i = 0;
	var result  = "";

	while(i < this.length)
	{
		a = this.charCodeAt(i++);
		if(a >= 0xDC00 && a < 0xE000) continue;
		if(a >= 0xD800 && a < 0xDC00)
		{
			if(i >= this.length) continue;
			b = this.charCodeAt(i++);
			if(s < 0xDC00 || a >= 0xDE00) continue;
			a = ((a - 0xD800) << 10) + (b - 0xDC00) + 0x10000;
		}

		if(a < 0x80)  
			result += String.fromCharCode(a); 
		else if(a < 0x800) 
			result += String.fromCharCode(0xC0 + (a >> 6), 0x80 + (a & 0x3F));
		else if(a < 0x10000)
			result += String.fromCharCode(0xE0 + (a >> 12), 0x80 + (a >> 6 & 0x3F), 0x80 + (a & 0x3F));
		else
			result += String.fromCharCode(0xF0 + (a >> 18), 0x80 + (a >> 12 & 0x3F), 0x80 + (a >> 6 & 0x3F), 0x80 + (a & 0x3F)); 
	}
	return result;
};

String.prototype.trim = function()
{
	return this.replace(/^[\s]+|[\s]+$/, "");
};

function searchIt(instance)
{		
	//注：输入框回车事件时，所调用的方法，根据实际情况调用各自的方法
		if(sfw[instance].inputContainer.value != "")
		{ 		
		   if(window.location.href.indexOf("SearchResult")<0 && window.location.href.indexOf("VideoResult")<0) 	
		    SearchImgDefault();			 
		   else		
		    SearchImg(0);
		
		}
		else
		{		  
			return false;
		}
}
	
	

var keypad=true;
function sfwCreate(instance)
{
	if(sfw[instance].name && sfw[instance].action)
	{		
		var n = sfw[instance].name.replace(/\$/g,"_");//add by mashilei on 090226//注：用户控件所遇到的问题，id与Name 一致
//		sfw[instance].inputContainer = document.getElementById(sfw[instance].name);//modified by mashilei on 090226
		sfw[instance].inputContainer = document.getElementById(n);
		sfw[instance].inputContainer.autocomplete = "off";
		//sfw[instance].inputContainer.onblur       = function() { sfwHideOutput(instance); };
		sfw[instance].inputContainer.onclick      = function() { keypad=true; sfwShowOutput(instance); };
		sfw[instance].inputContainer.onfocus      = function() { keypad=true; sfwShowOutput(instance); };
		sfw[instance].inputContainer.onkeypress   = function(event) { if(sfwGetKey(event) == 13) searchIt(instance);	};	 

		sfw[instance].inputContainer.onkeydown    = function(event) { keypad=true; sfwProcessKeys(instance, event); };
		sfw[instance].outputContainer = document.createElement("div");
		sfw[instance].outputContainer.id             = sfw[instance].name + "_list";
		sfw[instance].outputContainer.className      = "SuggestFramework_List";
		sfw[instance].outputContainer.style.position = "absolute";
		sfw[instance].outputContainer.style.zIndex   = "1000";
		sfw[instance].outputContainer.style.width    = sfw[instance].inputContainer.clientWidth + "px";			
		
		sfw[instance].outputContainer.style.top      = "34px";	
		sfw[instance].outputContainer.style.left     =  "0";
		sfw[instance].outputContainer.style.wordWrap = "break-word";
		sfw[instance].outputContainer.style.cursor   = "default";		
		sfw[instance].inputContainer.parentNode.insertBefore(sfw[instance].outputContainer, sfw[instance].inputContainer.nextSibling);

		if(sfw[instance].columns > 1 && sfw[instance].capture > 1)
		{
			sfw[instance].hiddenInput = document.createElement("input");
			sfw[instance].hiddenInput.id   = "_" + sfw[instance].name;
			sfw[instance].hiddenInput.name = "_" + sfw[instance].name;
			sfw[instance].hiddenInput.type = "hidden";
			sfw[instance].inputContainer.parentNode.insertBefore(sfw[instance].hiddenInput, sfw[instance].inputContainer.nextSibling);
		}

		if(!sfwCreateConnection())
		{
			sfw[instance].proxy = document.createElement("iframe");
			sfw[instance].proxy.id = "proxy";
			sfw[instance].proxy.style.width   = "0";
			sfw[instance].proxy.style.height  = "0";
			sfw[instance].proxy.style.display = "none";
			document.body.appendChild(sfw[instance].proxy);

			if(window.frames && window.frames["proxy"])
				sfw[instance].proxy = window.frames["proxy"];
			else if(document.getElementById("proxy").contentWindow)
				sfw[instance].proxy = document.getElementById("proxy").contentWindow;
			else
				sfw[instance].proxy = document.getElementById("proxy");
		}

		sfwHideOutput(instance);
		sfwThrottle(instance);
	}
	else
	{
		throw 'Suggest Framework Error: Instance "' + sfw[instance].name + '" not initialized';
	}
};

function sfwCreateConnection()
{
	var asynchronousConnection;

//	try
//	{
//		asynchronousConnection = new ActiveXObject("Microsoft.XMLHTTP");
//	}
//	catch(e)
//	{
//		if(typeof XMLHttpRequest != "undefined")
//			asynchronousConnection = new XMLHttpRequest();
//	}

    if(window.ActiveXObject)
    {
        asynchronousConnection = new ActiveXObject("Microsoft.XMLHTTP");
    }
	else
	{
		if(typeof XMLHttpRequest != "undefined")
			asynchronousConnection = new XMLHttpRequest();
	}

	return asynchronousConnection;
};

function sfwGetKey(e)
{
	return ((window.event) ? window.event.keyCode : e.which);
};

function sfwHideOutput(instance)
{
	sfw[instance].outputContainer.style.display = "none";
};

function sfwHighlight(instance, index)
{
	sfw[instance].suggestionsIndex = index;

	//for(var i in sfw[instance].suggestions)
	for(var i =0; i< sfw[instance].suggestions.length; i++)
	{
		var suggestionColumns = document.getElementById(sfw[instance].name + "_suggestions[" + i + "]").getElementsByTagName("td");
		for(var j in suggestionColumns)
			suggestionColumns[j].className = "SuggestFramework_Normal";
	}
	//alert(sfw[instance].name + "_suggestions[" + (sfw[instance].suggestionsIndex + 1)/1 + "]");
	var suggestionColumns = document.getElementById(sfw[instance].name + "_suggestions[" + sfw[instance].suggestionsIndex+ "]").getElementsByTagName("td");
	for(var i in suggestionColumns)
		suggestionColumns[i].className = "SuggestFramework_Highlighted";
};

function sfwIsHidden(instance)
{
	return ((sfw[instance].outputContainer.style.display == "none") ? true : false);
};

function sfwProcessKeys(instance, e)
{
	var keyDown   = 40;
	var keyUp     = 38;
	var keyTab    = 9;
	var keyEnter  = 13;
	var keyEscape = 27;

	if(!sfwIsHidden(instance))
	{
		switch(sfwGetKey(e))
		{
			case keyDown:   sfwSelectNext(instance);     return;
			case keyUp:     sfwSelectPrevious(instance); return;
			case keyEnter:  sfwSelectThis(instance);     return;
			case keyTab:    sfwSelectThis(instance);     return;
			case keyEscape: sfwHideOutput(instance);     return;
			default: return;
		}
	}
};

function sfwProcessProxyRequest(instance)
{
	var result = ((sfw[instance].proxy.document) ? sfw[instance].proxy.document : sfw[instance].proxy.contentDocument);
	result = result.body.innerHTML.replace(/\r|\n/g, " ").trim();

	if(typeof eval(result) == "object")
		sfwSuggest(instance, eval(result));
	else
		setTimeout("sfwProcessProxyRequest(" + instance + ")", 100);
};



var tag=0;

function sfwQuery(instance)
{
	sfwThrottle(instance);
	var phrase = sfw[instance].inputContainer.value;
	if(phrase == sfw[instance].previous) return;
	if(phrase == "") sfwHideOutput(instance);
	sfw[instance].previous = phrase;
	
	
	tag+=1;
	if(tag>1){
		var url = sfw[instance].action + "?type=" + sfw[instance].name + "&q=" + phrase.trim().encode() + "&timeStamp="+new Date().getTime();
		if(phrase!="" && phrase.match(" ")==null){
			sfwRequest(instance, url);
		}else{
			sfwSuggest(instance,"");
		}
	}

};



function sfwRequest(instance, url)
{
	if(sfw[instance].connection = sfwCreateConnection())
	{
		sfw[instance].connection.onreadystatechange = function() {
			if(sfw[instance].connection.readyState == 4)
			{
				if(sfw[instance].connection.status == 200)
				sfwSuggest(instance, eval(sfw[instance].connection.responseText));
			}
		};
		sfw[instance].connection.open("GET", url, true);
		sfw[instance].connection.send(null);
	}
	else
	{
		sfw[instance].proxy.location.replace(url);
		sfwProcessProxyRequest(instance);
	}
};



function sfwSelectThis(instance, index, mouse)
{
	if(sfw[instance].columns > 1 && sfw[instance].capture > 1)
		sfw[instance].hiddenInput.value = sfw[instance].suggestions[sfw[instance].suggestionsIndex][sfw[instance].capture - 1];


	if(!isNaN(index)) { sfw[instance].suggestionsIndex = index; }

    if(sfw[instance].suggestionsIndex > -1)//added by mashilei on 090226 and modified by mask on 090414
    {   
	    var selection = sfw[instance].suggestions[sfw[instance].suggestionsIndex];    	
	    if(sfw[instance].columns > 1) { selection = selection[0]; }
//		if(type_flag==0){ 
			sfw[instance].inputContainer.value = selection;
			keypad=false;
			sfwQuery(instance);
			//searchIt(instance);
//		}
//		if(type_flag==0&&mouse==true){ sfw[instance].inputContainer.value = selection; keypad=true; sfwQuery(instance);}
//		if(type_flag==1){ sfw[instance].inputContainer.value = selection;}
	    sfw[instance].previous = selection;
		sfw[instance].inputContainer.focus();
	}
	else
	{	 
	     sfw[instance].previous = sfw[instance].inputContainer.value ;
	}
	sfwHideOutput(instance);
}

//var sfw[instance].suggestions[(sfw[instance].suggestionsIndex = 1;
function sfwSelectNext(instance)
{

	sfwSetTextSelectionRange(instance);
	if(typeof sfw[instance].suggestions[(sfw[instance].suggestionsIndex + 1)] != "undefined")
	{
		if(typeof sfw[instance].suggestions[sfw[instance].suggestionsIndex] != "undefined")
			document.getElementById(sfw[instance].name + "_suggestions[" + sfw[instance].suggestionsIndex + "]").className = "SuggestFramework_Normal";
		sfw[instance].suggestionsIndex++;
//		if(sfw[instance].suggestionsIndex ==0){ sfw[instance].suggestionsIndex =1 }
		sfwHighlight(instance, sfw[instance].suggestionsIndex);
	}
};

function sfwSelectPrevious(instance)
{

	sfwSetTextSelectionRange(instance);
	//alert(sfw[instance].suggestions[(sfw[instance].suggestionsIndex - 1)] != "0");
	//alert(sfw[instance].suggestions[(sfw[instance].suggestionsIndex - 1)] != "1");
	if((typeof sfw[instance].suggestions[(sfw[instance].suggestionsIndex - 1)] != "undefined"))
	{
		//alert();
		//if( (sfw[instance].suggestions[sfw[instance].suggestionsIndex] != "0") || (sfw[instance].suggestions[sfw[instance].suggestionsIndex] != "1")){
			document.getElementById(sfw[instance].name + "_suggestions[" + sfw[instance].suggestionsIndex + "]").className = "SuggestFramework_Normal";
			sfw[instance].suggestionsIndex--;
			sfwHighlight(instance, sfw[instance].suggestionsIndex);
		//}
	}
};

function sfwSetTextSelectionRange(instance, start, end)
{
	if(!start) var start = sfw[instance].inputContainer.value.length;
	if(!end)   var end   = sfw[instance].inputContainer.value.length;

	if(sfw[instance].inputContainer.setSelectionRange)
	{
		sfw[instance].inputContainer.setSelectionRange(start, end);
	}
	else if(sfw[instance].inputContainer.createTextRange)
	{
		var selection = sfw[instance].inputContainer.createTextRange();
		selection.moveStart("character", start);
		selection.moveEnd("character", end);
		selection.select();
	}
};

function sfwShowOutput(instance)
{

	if(typeof sfw[instance].suggestions != "undefined" && sfw[instance].suggestions.length)
	{
		sfw[instance].outputContainer.style.display = "block";
		}
};


//var type_flag=0;

function sfwSuggest(instance, list)
{
	if(typeof(list) !="undefined"){//added by mask 090414
		
		//if(list[0]==""){alert("f"); sfw[instance].outputContainer.style.display = "none";}
		
		sfw[instance].suggestions               = list;
		sfw[instance].suggestionsIndex          = -1;
		sfw[instance].outputContainer.innerHTML = "";

//		if(list[0]=="0"){ type_flag=0;}
//		if(list[0]=="1"){ type_flag=1;}
//		

	
		var table = '<table style="width: 100%; margin: 0; padding: 0" cellspacing="0" cellpadding="0">';
		if(sfw[instance].heading && sfw[instance].suggestions.length)
		{
			var heading = sfw[instance].suggestions.shift();
			var thead   = '<thead>';
			var headingContainer = '<tr>';
			for(var i = 0; i < sfw[instance].columns; i++)
			{
				var value  = (String) ((sfw[instance].columns > 1) ? heading[i] : heading);
				var column = '<td class="SuggestFramework_Heading"';
				if(sfw[instance].columns > 1 && i == sfw[instance].columns - 1)
					column += ' style="text-align: right"';
				column += '>' + value.decode().trim() + '</td>';
				headingContainer += column;
			}
			headingContainer += '</tr>';
			thead  += headingContainer;
			thead  += '</thead>';
			table  += thead;
		}
		var tbody = '<tbody>';
		//for(var i in sfw[instance].suggestions)
		
		for(var i =0; i< sfw[instance].suggestions.length; i++)
		{
			var suggestionContainer = '<tr id="' + sfw[instance].name + '_suggestions[' + i + ']">';
			for(var j = 0; j < sfw[instance].columns; j++)
			{
				var value  = (String) ((sfw[instance].columns > 1) ? sfw[instance].suggestions[i][j] : sfw[instance].suggestions[i]);
				var column = '<td class="SuggestFramework_Normal"';
				if(sfw[instance].columns > 1 && j == sfw[instance].columns - 1)
					column += ' style="text-align: right"';
				column += '>' + value.decode().trim() + '</td>';
				suggestionContainer += column;
			}
			suggestionContainer += '</tr>';
			table += suggestionContainer;
		}
		tbody += '</tbody>';
		table += tbody;
		table += '</table>';		
		//alert(table);
		//sfw[instance].outputContainer.innerHTML = table;
		$("#txt_Keyword_list").html(table);
	
		//for(var i in sfw[instance].suggestions)
		for(var i =0; i< sfw[instance].suggestions.length; i++)
		{
			var row = document.getElementById(sfw[instance].name + '_suggestions[' + i + ']');
			row.onmouseover = new Function("sfwHighlight(" + instance + ", " + i + ")");
			var mouse=true;
			//row.onmousedown = new Function("sfwSelectThis(" + instance + ", " + i + "," + mouse +")");
			row.onmousedown = new Function("sfwSelectThis(" + instance + ");searchIt(" + instance + ");");
		}
	
		if(keypad==true){sfwShowOutput(instance);}
	}
	else{
		sfw[instance].outputContainer.style.display = "none";
	}
};

function sfwThrottle(instance)
{
	//setTimeout("sfwQuery(" + instance + ")", sfw[instance].delay);
	setTimeout("sfwQuery(" + instance + ")", sfw[instance].delay);
	
};

function initializeSuggestFramework()
{
	
	function getAttributeByName(node, attributeName)
	{
		if(typeof NamedNodeMap != "undefined")
		{
			if(node.attributes.getNamedItem(attributeName))
				return node.attributes.getNamedItem(attributeName).value;
		}
		else
		{
			return node.getAttribute(attributeName);
		}
	}

	var inputElements = document.getElementsByTagName("input");	

	try
	{
		for(var instance = 0; instance < inputElements.length; instance++)
		{		   
			if(getAttributeByName(inputElements[instance], "name") &&
			   getAttributeByName(inputElements[instance], "type") == "text" && 
			   getAttributeByName(inputElements[instance], "action"))
			{
				sfw[instance] = new Object();
				sfw[instance].action  = getAttributeByName(inputElements[instance], "action");
				sfw[instance].capture = 1;
				sfw[instance].columns = 1;
				sfw[instance].delay   = 1000;
				sfw[instance].heading = false;
				sfw[instance].name    = getAttributeByName(inputElements[instance], "name");
				sfw[instance].suggestions    ="";
				
				

				if(getAttributeByName(inputElements[instance], "capture"))
					sfw[instance].capture = getAttributeByName(inputElements[instance], "capture");
				if(getAttributeByName(inputElements[instance], "columns"))
					sfw[instance].columns = getAttributeByName(inputElements[instance], "columns");
				if(getAttributeByName(inputElements[instance], "delay"))
					sfw[instance].delay = getAttributeByName(inputElements[instance], "delay");
				if(getAttributeByName(inputElements[instance], "heading"))
					sfw[instance].heading = getAttributeByName(inputElements[instance], "heading");

				sfwCreate(instance);
				//sfw[instance].focus();
				//alert("f");
			}
		}
	}
	catch(e) {}
};