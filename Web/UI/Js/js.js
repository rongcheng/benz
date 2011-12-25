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
        if(objectId == "update"){
            alert(xmlResponse);
            var name = document.getElementById("hiddenName").value;
            GetFeaturesPage(name, 10, 1);
        }
        else if(objectId == "add"){
            if(xmlResponse.indexOf(';') != -1){
                var message = xmlResponse.split(';')[0];
                alert(message);
                var featureId = xmlResponse.split(';')[1];
                document.getElementById("DetailContent").style.display = "";
                var HTML = "<div style=\"padding:3px 0px 3px 0px;border-bottom:#d4d0c8 solid 1px;\"><input type=\"button\" class=\"btn\" style=\"font-size:12px;\" onclick=\"AddImage('" + featureId + "')\" value=\"添加图片\" /></div>";
                HTML += "<div>";
                HTML += "<ul class=\"detailClass\">";
                HTML += "</ul></div>";
                
                document.getElementById("DetailContent").innerHTML = HTML;
            }
            else{
                alert(xmlResponse);
            }
            var name = document.getElementById("hiddenName").value;
            GetFeaturesPage(name, 10, 1);
        }
        else if(objectId == "save"){
            alert(xmlResponse);
            if(parent.window.opener){
                var featureId = document.getElementById("hiddenFeatureId").value;
                var type = document.getElementById("hiddentype").value;
                if(type != "order")
                    parent.window.opener.GetImagePage(featureId, 20, 1);
            }
            var item = document.getElementsByName("newAdd");
            for(var i = 0;i<item.length;i++){
                var o = parent.addImages.document.getElementById(item[i].value);
                if(o){
                    o.checked = false;
                }
            }
            document.getElementById("AddUl").innerHTML = "";
        }
        else if(objectId == "delete"){
            if(xmlResponse != ""){
                var obj = document.getElementById(xmlResponse);
                $(obj).parent("li").remove();
                alert("删除成功！");
            }
        }
        else if(objectId == "oldwater"){
            if(xmlResponse == ""){
                alert("保存设置失败");
            }
            else{
                document.getElementById("hiddenState").value = objectId;
                alert("保存设置成功");
                OnPreview();
            }
        }
        else if(objectId == "newwater"){
            if(xmlResponse == ""){
                alert("保存设置失败");
            }
            else{
                document.getElementById("hiddenState").value = objectId;
                alert(xmlResponse.split('|')[0]);
                OnPreview();
                document.getElementById("thumbnails").innerHTML = "";
                document.getElementById("oldImgId").src = "../../xml/"+xmlResponse.split('|')[1]+".gif";
                document.getElementById("radoldid").checked = true;
            }
        }
        else if(objectId == "default"){
            if(xmlResponse == ""){
                alert("默认设置失败");
            }
            else{
                document.getElementById("hiddenState").value = objectId;
                alert(xmlResponse.split('|')[0]);
                var oo = document.getElementById("ddlSelect");
                oo.options[2].selected = true;
                OnPreview();
                document.getElementById("thumbnails").innerHTML = "";
                document.getElementById("oldImgId").src = "../../xml/"+xmlResponse.split('|')[1]+".gif";
                document.getElementById("radoldid").checked = true;
            }
        }
        else if(objectId == "cover"){
            if(xmlResponse == ""){
                alert("设置失败");
            }
            else{
                if(document.getElementById("editImg"))
                    document.getElementById("editImg").src = xmlResponse.split(';')[0];
                if(document.getElementById("CoverImage"))
                    document.getElementById("CoverImage").value = xmlResponse.split(';')[1];
                if(document.getElementById("addImg"))
                    document.getElementById("addImg").src = xmlResponse.split(';')[0];
                if(document.getElementById("txtCoverImage"))
                    document.getElementById("txtCoverImage").value = xmlResponse.split(';')[1];
                alert("设置成功");
            }
        }
        else if(objectId == "System")
        {
            AddValue();
            alert(xmlResponse);
        }
        else{
	        document.getElementById(objectId).innerHTML = xmlResponse;
	        var it = document.getElementsByName("image");
	        if(it.length > 0){
	            var bVar = true;
	            for(var i=0;i<it.length;i++){
	                if(it[i].checked == false){
	                    bVar = false;
	                    break;
	                }
	            }
	            if(bVar){
	                var oItem = document.getElementsByName("fullname");
                    for(var i=0;i<oItem.length;i++){
                        oItem[i].checked = true;
                    }
	            }
	        }
	    }
	}
	
	this.doFeature = function(body){
		xmlObject.open("POST", "GetFeature.aspx", true) ;
		xmlObject.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
		xmlObject.send(body) ;
	}
	
	this.doShow = function(body){
		xmlObject.open("POST", "Modules/Manage/GetFeature.aspx", true) ;
		xmlObject.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
		xmlObject.send(body) ;
	}
	
	this.doContent = function(body){
	    xmlObject.open("POST", "GetContent.aspx", true);
	    xmlObject.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
	    xmlObject.send(body);
	}
	
	this.doError = function(body){
	    xmlObject.open("POST", "GetContent.aspx", true);
	    xmlObject.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
	    xmlObject.send(body);
	}
}
function GetEdit(featureId, name){
    var item = document.getElementsByTagName("tr");
  
    GetFeature(featureId, name);
    GetImagePage(featureId, 12, 1);
}
function GetFeature(featureId, name){
    Show('');
    var obj = document.getElementById("UpdataFeature");
    if(obj.getAttribute('loaded') == 'true'){
        return;
    }
    else{
        obj.innerHMTL = "<img alt='' src='../../image/common/loading.gif'/>&nbsp;数据加载中...";
        var myxhr = new xmlHttpObject("UpdataFeature");
        if(myxhr){
            try{
                myxhr.doFeature("type=Single&featureid="+featureId+"&name="+name);
            }
            catch(e){
                alert("Can't cannect to server:\n"+e.toString());
            }
        }
    }
}
function xmlHttpNotices(id){
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
        if(objectId == "update"){
            alert(xmlResponse);
            var name = document.getElementById("hiddenName").value;
            GetNoticesPage(name, 10, 1);
        }
        else if(objectId == "add"){
            alert(xmlResponse);
            var name = document.getElementById("hiddenName").value;
            GetNoticesPage(name, 10, 1);
        }
        else if(objectId == "delete"){
            if(xmlResponse != ""){
                var obj = document.getElementById(xmlResponse);
                alert("删除成功！");
                GetNoticesPage(name, 10, 1);
            }
        }
        else if(objectId == "UpdataNotice"){
            if(xmlResponse != ""){
                Add(xmlResponse.split('|')[0], xmlResponse.split('|')[1]);
            }
        }
        else
	        document.getElementById(objectId).innerHTML = xmlResponse;
	}
	
	this.doNotices = function(body){
	    xmlObject.open("POST", "GetNotices.aspx", true);
	    xmlObject.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
	    xmlObject.send(body);
	}
	
	this.doShow = function(body){
		xmlObject.open("POST", "Modules/Manage/GetNotices.aspx", true) ;
		xmlObject.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
		xmlObject.send(body) ;
	}
}

function xmlHttpCalendar(id){
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
		if(objectId == "delete"){
            if(xmlResponse != ""){
                var time =document.getElementById("hiddentime").value;
                var id = document.getElementById("hiddenid").value;
                var name = document.getElementById("hiddenname").value;
                var p = document.getElementById("hiddenparam").value;
                var items = p.split(';');
                for(var i=0;i<items.length;i++){
                    if(items[i] != ""){
                        id = items[i].replace(/-/g, "").replace(/\//g, "");
                        add(items[i], name, id);
                    }
                }
                //add(time, name, id);
                alert(xmlResponse.split(';')[0]);
                window.parent.closeManager();
            }
            else   
                alert("删除失败");
        }
        else
	        document.getElementById(objectId).innerHTML = xmlResponse;
	}
	
	this.doCalendar = function(body){
	    xmlObject.open("POST", "GetCalendar.aspx", true);
	    xmlObject.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
	    xmlObject.send(body);
	}
	
	this.doShow = function(body){
		xmlObject.open("POST", "Modules/GetCalendar.aspx", true) ;
		xmlObject.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
		xmlObject.send(body) ;
	}
}

function ShowCalendar(time, username){
    var obj = document.getElementById(time);
    if(obj.style.display == "none")
        obj.style.display = "";
    else
        obj.style.display = "none";
    if(obj.getAttribute('loaded') == 'true'){
        return;
    }
    else{
        obj.innerHMTL = "<img alt='' src='../image/common/loading.gif'/>&nbsp;数据加载中...";
        var myxhr = new xmlHttpCalendar(time);
        if(myxhr){
            try{
                myxhr.doCalendar("type=Show&time="+time+"&name="+username);
            }
            catch(e){
                alert("Can't cannect to server:\n"+e.toString());
            }
        }
    }
}

function ShowSingle(time, username, id){
    var obj = document.getElementById(id);
    if(!obj)
        return;
    if(obj.getAttribute('loaded') == 'true'){
        return;
    }
    else{
        obj.innerHMTL = "<img alt='' src='../image/common/loading.gif'/>&nbsp;数据加载中...";
        var myxhr = new xmlHttpCalendar(id);
        if(myxhr){
            try{
                myxhr.doCalendar("type=Single&time="+time+"&name="+username);
            }
            catch(e){
                alert("Can't cannect to server:\n"+e.toString());
            }
        }
    }
}

function DeleteCalendar(calendarId){
    if(confirm("你确定要删除？")){
        var myxhr = new xmlHttpCalendar("delete");
        if(myxhr){
            try{
                myxhr.doCalendar("type=Delete&calendarId="+calendarId);
            }
            catch(e){
                alert("Can't cannect to server:\n"+e.toString());
            }
        }
    }
}

function xmlHttpObjectError(id){
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
	    xmlObject.open("POST", "../../GetContent.aspx", true);
	    xmlObject.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
	    xmlObject.send(body);
	}
	
	this.doContent = function(body){
	    xmlObject.open("POST", "../GetContent.aspx", true);
	    xmlObject.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
	    xmlObject.send(body);
	}
}