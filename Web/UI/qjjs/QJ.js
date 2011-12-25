var QJ={
	$:function(obj,tag){//获取对象、标签、指定对象内的标签：obj=对象，tag=标签（可选）
		if(!tag){return document.getElementById(obj);}
		else if(typeof obj=='string'){return document.getElementById(obj).getElementsByTagName(tag);}
		else{return obj.getElementsByTagName(tag);}
	},
	$css:function(css,obj){//获取(指定对象内的)Class对象(数组):css=样式名称，obj=对象（可选）
		if(!obj){obj=this.$(document,'*');}
		var objs=[];
		var cssName=new RegExp("\\b"+css+"\\b");
		for(i=0;i<obj.length;i++){if(obj[i].className.match(cssName)){objs.push(obj[i]);}}
		return objs=(objs.length==1)?objs[0]:objs;
	},
	loadImage:function(url,callBack){//图片预载:url=图片路径，callBack=方法
		var img=new Image();
		img.onload=function(){callBack.call(img);}
		img.src=url;
	},
	addEvent:function(obj,on,ft){//给事件叠加新方法:obj=对象，on=事件，ft=方法
		obj.attachEvent?obj.attachEvent(on,function(){ft.call(obj)}):obj.addEventListener(on.slice(2),ft,false);
	},
	getDefaultStyle:function(obj,attribute){//返回最终样式结果：obj=对象，attribute=样式属性
		return obj.currentStyle?obj.currentStyle[attribute]:document.defaultView.getComputedStyle(obj,false)[attribute];
	},
	cc:function(obj,c){//批量给对象写入属性与属性值：obj=对象，c=属性集合
		for(var i in c){
			if(typeof c[i]=="string"){obj[i]=c[i];}
			else{for(var n in c[i]){obj[i][n]=c[i][n]}}
		}
	},
	cTag:function(tag,c,fobj){//创建HTML标签:tag=标签，c=属性集合，fobj=创建标签将要插入的父级对象（可选）
		var obj=document.createElement(tag);
		this.cc(obj,c);
		if(!fobj){return obj;}
		else{fobj.appendChild(obj);}
	},
	web:function(){
		var ua=navigator.userAgent.toLowerCase();
		return (ua.match(/msie/))?"ie":(s=ua.match(/firefox/))?s:(s=ua.match(/chrome/))?s:(s=ua.match(/opera/))?s:(s=ua.match(/safari/))?s:0;
		},
	xpp:function(e,stopdefault){//阻止冒泡
		var e = e?e:window.event;
		if(stopdefault!==false){stopdefault=true;}
		if(e.stopPropagation){e.stopPropagation();}
		else{e.cancelBubble=true;}
		if(stopdefault){
			if (e.preventDefault){e.preventDefault();}
			else{e.returnValue=false;}
		}
	},
	fix:function(obj){
		var ua=navigator.userAgent.toLowerCase();
		if(ua.match(/msie \b([0-9])/)&&ua.match(/msie \b([0-9])/)[1]==6){
			for(var i=0;i<obj.length;i++){
				var imgURL=this.getDefaultStyle(obj[i],"backgroundImage");
				if(imgURL.match(/png\"\)$/)=='png")'){
					if(this.getDefaultStyle(obj[i],"backgroundRepeat")=="no-repeat"){obj[i].style.filter="progid:DXImageTransform.Microsoft.AlphaImageLoader(src='"+imgURL.replace(/(^\burl\(\")(.+)(\"\))/,"$2")+"',sizingMethod='image')";}
					else{obj[i].style.filter="progid:DXImageTransform.Microsoft.AlphaImageLoader(src='"+imgURL.replace(/(^\burl\(\")(.+)(\"\))/,"$2")+"',sizingMethod='scale')";}
					obj[i].style.backgroundImage="none";
				}
			}
		}
	}
}