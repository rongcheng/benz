function openNewDiv() {
    if (document.getElementById("selectContent"))
	    document.removeChild(document.getElementById("selectContent"));
    var newDiv = document.createElement("div");
    newDiv.id = "selectContent";
    newDiv.className = "roll";
    var h = document.documentElement.scrollTop || window.pageYOffset || document.body.scrollTop;
    if(document)
	    newDiv.style.top = h + 20 + "px";
    else
	    newDiv.style.top = h + 20 + "px";
    newDiv.style.left = (parseInt(document.body.clientWidth) - 430) / 2 + 600 +"px";
    var html = "<table width=\"100%\" border=\"0\" cellpadding=\"1\" cellspacing=\"1\" style=\"border:#d4d0c8 dashed 1px;\">";
    html += "<tr><td>";
    html += "<input type=\"button\" id=\"btnSave\" style=\"width:45px;\" class=\"btn\" value=\"保存\" onclick=\"Save()\" />";
    html += "&nbsp;";
    html += "<input type=\"button\" id=\"btnDelete\" style=\"width:45px;\" class=\"btn\" value=\"清除\" onclick=\"Clear()\" />";
    html += "</td></tr>";
    html += "<tr><td align=\"left\" style=\"text-align:left\">";
    html += "<ul id=\"AddUl\" class=\"selectClass\"></ul>";
    html += "</td></tr>";
    html += "</table>";
    newDiv.innerHTML = html;
    document.body.appendChild(newDiv);
    var scrollPos;
    window.onscroll= function(){ 
        scrollPos = document.documentElement.scrollTop || window.pageYOffset || document.body.scrollTop;
	    newDiv.style.top = scrollPos + 20 + "px";//document.documentElement.scrollTop + 70;
	    newDiv.style.left = (parseInt(document.body.clientWidth) - 430) / 2 + 600 +"px";
    };
    window.onresize = function(){
        scrollPos = document.documentElement.scrollTop || window.pageYOffset || document.body.scrollTop;
	    newDiv.style.top = scrollPos + 20 + "px";//document.documentElement.scrollTop + 70;
	    newDiv.style.left = (parseInt(document.body.clientWidth) - 430) / 2 + 600 +"px";
    };
    return true;
}