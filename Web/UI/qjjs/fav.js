// Favorites @ www.quanjing.com
function getFirstChild(obj){
   for (var i=0; i<obj.childNodes.length; i++){
      if (obj.childNodes[i].nodeType == 1){
      return obj.childNodes[i];
      }
   }
}
var openWindow={
	obj:function(obj){var x=(!obj)?0:1;this.hs(x);this.bodyBg(x);this.hb(x);this.upDiv(obj);},
	hs:function(x){
		var s=document.getElementsByTagName("select");
		for(var i=0;i<s.length;i++){s[i].style.visibility=(x==0)?"hidden":"";}
	},
	hb:function(x){
		var o=document.documentElement||document.body;
		o.style.overflow=(x==1)?"hidden":"";
	},
	bodyBg:function(x){
		if(QJ.$("bodys")){QJ.$("bodys").style.display=(x==0)?"none":"";}
		else if(!QJ.$("bodys")&&x==1){
			var c={id:"bodys",style:{position:"absolute",left:"0",top:"0",zIndex:"998",width:"100%",height:document.body.clientHeight+"px",backgroundColor:"#000",filter:"alpha(opacity=50)",opacity:"0.5"}}
			QJ.cTag("div",c,document.body);
		}
	},
	upDiv:function(obj){
		if(!obj){
			QJ.$("temp").appendChild(getFirstChild(QJ.$("openContent")));
			QJ.$("openbox").style.display="none";
			if(QJ.web()=="ie"){QJ.$("openbox").style.width="";}
		}
		else{
			QJ.$("openContent").appendChild(obj);
			QJ.$("openbox").style.display="block";
			if(QJ.web()=="ie"){
				QJ.$("openbox").style.width="1%";
				QJ.$("openbox").style.width=(QJ.$("openContent").scrollWidth+12)+"px";
				QJ.fix(QJ.$("openbox").getElementsByTagName("span"));
			}
			QJ.$("openbox").style.marginTop=(-(QJ.$("openContent").scrollHeight/2)+((window.pageYOffset)?window.pageYOffset:document.documentElement.scrollTop)+"px");
			QJ.$("openbox").style.marginLeft=(-(QJ.$("openContent").scrollWidth/2+12)+"px");
		}
	}
}
QJ.addEvent(document,"onkeydown",function(e){
	var ev=window.event||e;
	if(ev.keyCode==27){fav.closeWin();}
});
var fav={
	hover_:function(obj,css){
		if(obj==null||css==null){return false}
		var
		my=this,
		layer=obj.getElementsByTagName("ul")[0],
		regExp=new RegExp("\\b"+css+"\\b","g"),
		a=function(){this.className+=(" "+css);},
		b=function(){my.setTiem=setTimeout(function(){my.cssTab(obj,regExp)},200);},
		c=function(){clearTimeout(my.setTiem);};
		this.cssTab=function(obj,regExp){
			obj.className=obj.className.replace(regExp,"");
		}
		QJ.addEvent(obj,"onclick",a);
		QJ.addEvent(obj,"onmouseout",b);
		QJ.addEvent(obj,"onmouseover",c);
	},
	select_:function(obj,css){
		var p_=obj.getElementsByTagName("span")[0];
		var li_=obj.getElementsByTagName("a");
		var regExp=new RegExp("\\b"+css+"\\b","g");
		fav.hover_(obj,css);
		for(var i=0;i<li_.length;i++){
			QJ.addEvent(li_[i],"onclick",function(event){
				QJ.xpp(event);
				p_.innerHTML=this.innerHTML;
				p_.title=this.hash.replace(/\#/,"");
				obj.className=obj.className.replace(regExp,"");
				this.blur();return false;
			});
		}
	},
	openWin:function(obj){
		if(!obj){return false}
		openWindow.obj(obj);
	},
	closeWin:function(){
		openWindow.obj();
	},
	openWinx:function(obj,x){
		if(!x){
			x=1;
			obj.style.height=document.body.clientHeight+"px";
			obj.style.display="block";
		}
		else{
			x=0;
			obj.style.display="none";
		}
		openWindow.hs(x);openWindow.bodyBg(x);openWindow.hb(x);
	}
}
var checkbox={
	obj:function(obj,list,x){
		if(obj==null||list==null){return false;}
		list=this.checkbox(list);
		this.list_fn(obj,list);
		if(typeof(obj.length)=="undefined"){
			this.all_fn(obj,list);
			if(x[0]==0){this.addon_0(x[1],x[2],list);this.addon_0(x[1],x[2],[obj]);}
		}
		else{
			this.alls(obj,list,x);
			for(var i=0;i<obj.length;i++){this.all_fn(obj[i],list);}
			if(x[0]==0){this.addon_0(x[1],x[2],list);this.addon_0(x[1],x[2],obj);}
		}
	},
	checkbox:function(obj){var objs=[];for(i=0;i<obj.length;i++){if(obj[i].type=="checkbox"){objs.push(obj[i]);}}return objs;	},
	of:function(obj,x){var obj=(typeof(obj.length)=="undefined")?[obj]:obj;for(var i=0;i<obj.length;i++){obj[i].checked=x;}},
	alls:function(obj,list,x){for(var i=0;i<obj.length;i++){QJ.addEvent(obj[i],"onclick",function(){if(this.checked){checkbox.of(obj,"checked");}else{checkbox.of(obj,"");}});}},
	all_fn:function(obj,list){QJ.addEvent(obj,"onclick",function(){if(this.checked){checkbox.of(list,"checked")}else{checkbox.of(list,"")}});},
	list_fn:function(obj,list){for(var i=0;i<list.length;i++){QJ.addEvent(list[i],"onclick",function(){checkbox.of(obj,"")});}},
	addon_0:function(obj,css,list){
		var z=new RegExp("\\b"+css+"\\b","g");
		for(var i=0;i<list.length;i++){
			QJ.addEvent(list[i],"onclick",function(){
				if(list.length==2){obj.className=(this.checked)?obj.className.replace(z,""):obj.className+" "+css;}
				else for(var n=0;n<list.length;n++){
					if(list[n].checked){obj.className=obj.className.replace(z,"");return false}
					obj.className+=" "+css;
				}
			});
		}
	}
}
var mm={
	obj:function(a,b,wh,s,min_,max_){
		if(!a||!b){return false}
		s=(!s)?20:s;
		this.whs=function(){return (wh=="width")?b.clientWidth:b.clientHeight;};
		QJ.addEvent(a,"onclick",function(){
			clearInterval(mm.maxmin);
			var wh_=mm.whs();
			if(!max_){min_=wh_}
			if(wh_>min_){mm.if_(b,wh,s,min_); $("#edit_notes").attr("style","width:110px; position:absolute; left:0; margin-left:0; background-position:-30px -22px; ")}
			else if(wh_<max_){mm.if_(b,wh,s,max_); $("#edit_notes").attr("style","width:110px; position:absolute; left:0; margin-left:0; background-position:-140px -22px; ")}
		})
	},
	if_:function(obj,wh,s,x){
		var wh_=this.whs();
		if(wh_>x){this.maxmin=setInterval(function(){mm.goto(obj,wh,x,0)},s);}
		if(wh_<x){this.maxmin=setInterval(function(){mm.goto(obj,wh,x,1)},s);}
	},
	goto:function(obj,wh,x,mm){
		wh_=this.whs();
		var x_=(x==0)?1:x;
		if(mm==0&&wh_>x_){wh_=wh_-(((wh_/10)<1)?1:(wh_/5));}
		else if(mm==1&&wh_<x){if(wh_==0){wh_=10;}wh_=wh_+(wh_/5);}
		else{clearInterval(this.maxmin);wh_=x;}
		obj.style[wh]=wh_+"px";
	}
};
var notes=function(e){
	mm.obj(e,QJ.$("notes"),"height",25,0,127);
};