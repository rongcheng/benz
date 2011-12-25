
function qj(){//DOM common
	this.$=function(obj,tag){
		if(!tag){return document.getElementById(obj);}
		else if(typeof obj=='string'){return document.getElementById(obj).getElementsByTagName(tag);}
		else{return obj.getElementsByTagName(tag);}
	};	
	this.addEvent=function(obj,behave,fn){
		behave=behave.toLowerCase();
		if (behave.substr(0,2)=='on')behave=behave.substr(2);
		if(window.attachEvent){
			obj[behave+fn]=function(){fn.call(obj);}; 
			obj.attachEvent('on'+behave,obj[behave+fn]);
		}
		else if(window.addEventListener)obj.addEventListener(behave,fn,false);
		else obj['on'+behave]=function(){fn.call(obj);};
	};
    this.removeEvent=function(obj,behave,fn){
        behave=behave.toLowerCase();
        if (behave.substr(0,2)=='on')behave=behave.substr(2);
        if(window.detachEvent){
            obj.detachEvent('on'+behave,obj[behave+fn]);
            obj[behave+fn]=null;
        }
        else if(window.removeEventListener)obj.removeEventListener(behave,fn,false);
        else obj['on'+behave]=null;  
    }
	this.cc=function(obj,c){
		for(var i in c){
			if(typeof c[i]=="string"){obj[i]=c[i];}
			else{for(var n in c[i]){obj[i][n]=c[i][n]}}
		}
	};
	this.cTag=function(tag,c,fobj) {
		var obj=(typeof tag!="object")?document.createElement(tag):tag;
		if(c!="")this.cc(obj,c);
		if(!fobj){return obj;}
		else{fobj.appendChild(obj);}
	};
	this.NextTag=function(obj){
	   if(obj.nextSibling.nodeType==1)return obj.nextSibling;
	   else return obj.nextSibling.nextSibling;
	}
}
var qj=new qj();

//搜索结果页 > 选项卡
function searchTab(x){
	var my=this
		,obj=qj.$(x.id)
		,objs=qj.$(obj,x.tags)
		,len=objs.length
		,fn=function(){
			for(var i=0;i<len;i++)objs[i].className=objs[i].cssName;
			my.val=x.url[this.temp];
			this.className+=" "+x.css;
			eval(x.fn+"(\""+my.val+"\")");
		};
	for(var i=0;i<len;i++){
		objs[i].temp=i;
		objs[i].cssName=objs[i].className;
		qj.addEvent(objs[i],"onclick",fn);
	}
	this.newData=function(n){
		if(!n)n="";
		for(var i=0;i<x.url.length;i++){
			if(x.url[i]==n)objs[i].className+=" "+x.css;
			else objs[i].className=objs[i].cssName;
		}
	}	
}
//搜索结果页 > 下拉菜单
function select(x){
	var obj=qj.$(x.id)
		,my=this
		,box
		,span=[]
		,colesBox;
	this.initia=function(){
		var pb=0; 
		if(navigator.appVersion.split(";")[1].replace(/[ ]/g,"")=="MSIE7.0")pb="3px";
		box=qj.cTag("span",{"style":{"cssText":"position:absolute;top:-999px;left:0;z-index:999;width:"+(x.width*2+6)+"px;padding:3px 0 "+pb+" 3px;background:#888;overflow:hidden;"}})
		for(var i=0;i<x.text.length;i++){
			var	width=(i==(x.text.length-1)&&x.text.length%2)?(x.width*2+3):x.width;
			span[i]=qj.cTag("span",{"style":{"cssText":"width:"+width+"px;float:left;height:18px;padding-top:4px;margin:0 3px 3px 0;font-size:12px;color:#6c6c6c;background:#e7e7e7;text-align:center;"},"innerHTML":x.text[i]},box);
		}
		qj.cTag(box,{},obj);
	}();
	this.on=function(){
	    qj.addEvent(obj,"onclick",function(){//click show use
			box.style.top=(box.style.top=="20px")?"-999px":"20px";	
			
			if(x.id=="pageSize") 
			{
			    if(document.getElementById("ctl00$ContentPlaceHolder1$AspNetPager2_input")!=undefined)
			    {
			        document.getElementById("ctl00$ContentPlaceHolder1$AspNetPager2_input").style.visibility="hidden";
			    }
			}
		    
		});
		qj.addEvent(obj,"onmouseout",function(){
			colesBox=setTimeout(function(){box.style.top="-999px"},200);
			
			if(x.id=="pageSize") 
			{
			    if(document.getElementById("ctl00$ContentPlaceHolder1$AspNetPager2_input")!=undefined)
			    {
			        setTimeout(function(){document.getElementById("ctl00$ContentPlaceHolder1$AspNetPager2_input").style.visibility="visible";},5000);
			    }
			}
			
		});
		qj.addEvent(obj,"onmouseover",function(){//alert("");
			clearTimeout(colesBox);
		});
		for(var i=0;i<x.text.length;i++){
			qj.addEvent(box.childNodes[i],"onclick",function(){
				box.style.top="-999px";
				qj.$(box.parentNode,"em")[0].innerHTML=this.innerHTML;
				var valTemp = this.innerHTML;
				if(x.id=="pageSize") valTemp = valTemp.replace("张","");
				if(x.id=="resourceType") //val =(val=="白色")?"0":"1";
				{
				    if(valTemp=="所有") valTemp="0";
				    else if(valTemp=="图片") valTemp="1";
				    else if(valTemp=="视频") valTemp="2";
				    else if(valTemp=="文档") valTemp="3";
				    else valTemp="9"
				}
				//my.val=parseFloat(valTemp);			
				my.val=valTemp;			
				eval(x.fn+"(\""+my.val+"\")");
			})
		}
	}();
	this.newData=function(val){		
			if(x.id=="resourceType")
			{			
			    if(val=="0") val="所有";
			    else if(val=="1") val="图片";
			    else if(val=="2") val="视频";
			    else if(val=="3") val="文档";			   
			    else val="其它";
			}
			else val+="张";			
		qj.$(box.parentNode,"em")[0].innerHTML=val;
	}
}
