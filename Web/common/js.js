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
            //var index = document.getElementById("hiddenPage").value;
            var name = document.getElementById("hiddenName").value;
            GetFeaturesPage(name, 10, 1);
        }
        else if(objectId == "add"){
            alert(xmlResponse);
            var name = document.getElementById("hiddenName").value;
            GetFeaturesPage(name, 10, 1);
        }
        else if(objectId == "save"){
            alert(xmlResponse);
            if(window.opener){
                var featureId = document.getElementById("hiddenFeatureId").value;
                window.opener.GetImagePage(featureId, 20, 1);
            }
        }
        else if(objectId == "delete"){
            if(xmlResponse != ""){
                var obj = document.getElementById(xmlResponse);
                $(obj).parent("li").remove();
                //var parent = obj.parentElement || obj.parentNode;
                parent.outerHTML = "";
                alert("删除成功！");
            }
        }
        else if(objectId == "water"){
            alert(xmlResponse);
        }
        else
	        document.getElementById(objectId).innerHTML = xmlResponse;
	}
	
	this.doHTML = function(body){
		xmlObject.open("POST", "GetFeature.aspx", true) ;
		xmlObject.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
		xmlObject.send(body) ;
	}
	
	this.doContent = function(body){
	    xmlObject.open("POST", "GetContent.aspx", true);
	    xmlObject.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
	    xmlObject.send(body);
	}
}
function GetEdit(featureId, name){
    var item = document.getElementsByTagName("tr");
    for(var i=0;i<item.length;i++){
        if(item[i].id == "tr"+featureId){
            item[i].style.backgroundColor = "Green";
        }
        else{
            item[i].style.backgroundColor = "White";
        }
    }
    
    GetFeature(featureId, name);
    GetImagePage(featureId, 10, 1);
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
                myxhr.doHTML("type=Single&featureid="+featureId+"&name="+name);
            }
            catch(e){
                alert("Can't cannect to server:\n"+e.toString());
            }
        }
    }
}

