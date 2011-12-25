function table(obj){
	this.$=function(obj,tag){//取id|tag
		if(!tag){return document.getElementById(obj);}
		else if(typeof obj=='string'){return document.getElementById(obj).getElementsByTagName(tag);}
		else{return obj.getElementsByTagName(tag);}
	}
	this.cTag=function(tag,c,fobj){//创建html对象
		var obj=(typeof tag!="object")?document.createElement(tag):tag;
		if(c!="")this.cc(obj,c);
		if(!fobj){return obj;}
		else{fobj.appendChild(obj);}
	}
	this.cc=function(obj,c){//给对象添加属性/属性值
		for(var i in c){
			if(typeof c[i]=="string"){if(i=="onclick")obj[i]=function(){eval(c[i]);};else obj[i]=c[i];}
			else{for(var n in c[i]){obj[i][n]=c[i][n]}}
		}
	}
	if(!this.$(obj.id)) return alert("ID对象找不到哦"+obj.id);
	var
	my=this,
	$box=this.$(obj.id,"div")[0],
	$ul=this.$($box,"ul")[0],
	$count1=this.$("count","span")[0],
	$count2=this.$("count","span")[1],
	delList=this.$("delList"),
	Upload=this.$("Upload");
	this.run=function(id,name,size){//添加文件列表
		this.sizeMax(size,"+");
		var
		$li=this.cTag("li",{"id":id}),
		$li_name=this.cTag("span",{"innerHTML":name,"className":"name"},$li),
		$li_size=this.cTag("span",{"innerHTML":""+Math.round(size/1024)+"","size":""+size+"","className":"size"},$li),
		$li_state=this.cTag("em",{"title":"移除","className":""+obj.trButtonClassName.initial+"","onclick":"my.remove('"+id+"',this);"},$li);
		$layer=this.cTag("p",{"id":id+"layer","className":"layer"},$li);
		this.opacity($li,0);
		this.cTag($li,"",$ul);
		new this.fade($li,"max");
	}
	this.size=0;
	this.sizeMax=function(size,i){//计算并文件总大小
		if(i=="+"){this.size+=size;this.fileMax(1,"+");}
		else if(i=="-"){this.size-=size;if(this.size==0)this.fileMax(this.file,"-");else this.fileMax(1,"-");}
		var sizeMax=(this.size/1024/1024).toString().replace(/(^.+\..{2})(.+)/g,"$1");
		$count2.innerHTML=sizeMax;
	}
	this.file=0;
	this.fileMax=function(file,i){//计算并文件总数量
		if(i=="+")this.file+=file;
		else if(i=="-")this.file-=file;
		$count1.innerHTML=this.file;
	}
	this.remove=function(id,_this_){//删除单行文件列表
		if(_this_)_this_.onclick=null;
		var size=this.$(id,"span")[1].size;
		new this.fade(this.$(id),"min","$ul.removeChild(my.$('"+id+"'));");
		this.sizeMax(size,'-');
		if(obj.remove)eval(obj.remove);
	}
	this.allRemove=function(){//删除全部文件列表
		this.sizeMax(this.size,"-");
		this.for_(this.$($ul,"li").length,"new this.fade(my.$($ul,\"li\")[i],\"min\",\"$ul.removeChild(my.$($ul,'li')[0]);\");");
		if(obj.allRemove)eval(obj.allRemove);
	}
	this.up=function(id,i){//上传文件进度
		this.$(id+"layer").style.width=i+"%";
	}
	this.fade=function(obj,x,fn){
		var fadeMy=this,lvMin=100,lvMax=0;
		this.fadeGtMax=function(){
			if(lvMax<100){
				lvMax+=10;
				my.opacity(obj,lvMax)
				setTimeout(function(){fadeMy.fadeGtMax()},50);
			}
			else eval(fn);
		}
		this.fadeGtMin=function(){
			if(lvMin>0){
				lvMin-=10;
				my.opacity(obj,lvMin);
				setTimeout(function(){fadeMy.fadeGtMin()},50);
			}
			else{
				obj.style.visibility="hidden";
				eval(fn);
			}
		}
		if(x=="max")this.fadeGtMax();
		else this.fadeGtMin();
	}
	this.opacity=function(obj,lv){
		if(typeof lv=="number"){
			if(document.all)obj.style.filter="alpha(opacity="+lv+")";
			else obj.style.opacity=lv/100;
		}
	}
	this.button=function(x){//按钮状态
	    //alert(obj.id+obj.buttonClassName);
		if(x=="00"){
			delList.onclick=Upload.onclick=function(){this.blur();};
			delList.className=Upload.className=obj.buttonClassName.disabled;
		}
		else if(x=="11"){
			this.cc(delList,{"className":""+obj.buttonClassName.initial+"","onclick":"my.allRemove();this.blur();"});
			this.cc(Upload,{"className":""+obj.buttonClassName.initial+"","innerHTML":"开始上传","onclick":"eval('"+obj.upFile+"');this.blur();"});
		}
		else if(x=="01"){
			this.cc(delList,{"className":""+obj.buttonClassName.disabled+"","onclick":"this.blur();"});
			this.cc(Upload,{"className":""+obj.buttonClassName.initial+"","innerHTML":"停止上传","onclick":"eval('"+obj.stopFileUpLoad+"');this.blur();"});
		}
		else if(x=="10"){
			this.cc(delList,{"className":""+obj.buttonClassName.initial+"","onclick":"my.allRemove();this.blur();"});
			this.cc(Upload,{"className":""+obj.buttonClassName.disabled+"","innerHTML":"开始上传","onclick":"this.blur();"});
		}
	}
	this.trButton=function(id,x){//列表按钮状态
		if(!id)	this.for_(this.$($ul,"em").length,"if(this.$($ul,'em')[i].className=='"+obj.trButtonClassName.loading+"'){this.$($ul,'em')[i].className='"+obj.trButtonClassName.initial+"'}else if(this.$($ul,'em')[i].className=='"+obj.trButtonClassName.initial+"'){this.$($ul,'em')[i].className='"+obj.trButtonClassName.loading+"'}");
		if(x==obj.trButtonClassName.end){
			this.cc(this.$(id,"em")[0],{"title":"已完成","className":""+obj.trButtonClassName.end+"","onclick":""});
			new this.fade(my.$(id+'layer'),"min");
		}
		if(x==obj.trButtonClassName.error){
			this.cc(this.$(id,"em")[0],{"title":"出错了","className":""+obj.trButtonClassName.error+"","onclick":""});
			new this.fade(my.$(id+'layer'),"min");
		}
	}
	this.for_=function(len,fn){for(var i=0;i<len;i++){eval(fn);}}
}
var fileList=new table({
	id:"upFile",//主框架ID
	upFile:"UploadPic()",//开始上传文件
	stopFileUpLoad:"stopFileUpLoad()",//停止文件上传
	remove:"removeFile(id);",//单项删除
	allRemove:"removeFiles()",//全部删除
	buttonClassName:{//按钮(控制)状态
		initial:"initial",//初始
		disabled:"disabled"//不可用
	},
	trButtonClassName:{//按钮(列表)状态
		initial:"initial",//初始(移除)
		loading:"loading",//上传中
		end:"end",//上传完毕
		error:"error"//上传出错
	}
});


table.prototype.allFiles=function(i,string){
	var this_=this;
	this.allFile_cTag=function(a,b,c){
		this.cTag(a,c,b);
		new this.opacity(a,0);
		new this.fade(a,"max");
	}
	this.removeTags=function(x,str){
		var css="box",d1=d2="";
		if(x==1){if(!str){d2="none";}else{d1=d2="none"}css="box search";}
		else if(x==2||x==3){d1="none";}
		else if(x==4){if(!str){css="box ok";d2="none";}else {css="box error";d1=d2="none"}}
		else if(x==5){
			if(this.$("allFiles_frame"))new this.fade(this.$("allFiles_frame"),"min","if(obj.parentNode)obj.parentNode.removeChild(obj)");return
		}
		else if(x==6){d2="none";css="box stop";}
		if(this.$("allFiles_h2")){
			this.$("allFiles_frame").removeChild(this.$("allFiles_h2"));
			this.$("allFiles_frame").removeChild(this.$("allFiles_ol"));
		}
		if(this.$("fllFiles_help"))this.$("allFiles_frame").removeChild(this.$("fllFiles_help"));
		this.$("allFiles_frame").className=css;
		this.$("allFiles_h1").style.display=d1;
		this.$("allFiles_p1").style.display=this.$("allFiles_p2").style.display=this.$("allFiles_p3").style.display=d2;
	}
	this.allFile_h1=function(str){//单行显示模板
		this.cc(this.$("allFiles_h1"),{"innerHTML":str});
		new this.opacity(this.$("allFiles_h1"),0);
		new this.fade(this.$("allFiles_h1"),"max");
	}
	this.allFile_h2=function(str,strs){//多行显示模板
		var
		len=strs.length,
		$h3=this.cTag("h3",{"id":"allFiles_h2","innerHTML":str}),
		$ol=this.cTag("ol",{"id":"allFiles_ol"});
		for(var i=0;i<len;i++){
			this.cTag("li",{"innerHTML":strs[i]},$ol);
		}
		this.allFile_cTag($h3,$box);
		this.allFile_cTag($ol,$box);
	}
	this.allFile_1=function(str){//检查图片是否重复
		if(str){
			var strs=[],
			len=str.length;
			for(var i=0;i<len;i++){
				strs.push(str[i].split(":").join(" (<em>").replace(/(.+)/g, "$1</em>)"));
			}
			this.allFile_h2(text1[1],strs);
			this.cTag("p",{"id":"fllFiles_help","className":"fllFiles_help","innerHTML":text1[2]},$box)
		}
		else this.allFile_h1(text1[0]);
	}
	this.allFile_2=function(str){//上传中
		this.cc(this.$("allFiles_p1"),{"innerHTML":"正在上传 [<strong>"+str[0]+"</strong>]"});
		this.cc(this.$("allFiles_allLoading"),{"style":{"width":str[1]+"%"}});
		this.cc(this.$("allFiles_loading"),{"innerHTML":str[1]});
		//this.cc(this.$("allFiles_speed"),{"innerHTML":str[2]});
		//this.cc(this.$("allFiles_time"),{"innerHTML":str[3]});
		if(str[1]==0){
			new this.fade(this.$("allFiles_p1"),"max");
			new this.fade(this.$("allFiles_p2"),"max");
		}
	}
	this.allFile_3=function(str){//单个上传完毕
		this.cc(this.$("allFiles_p1"),{"innerHTML":"已上传完毕 [<strong>"+str[0]+"</strong>]"});
		this.cc(this.$("allFiles_speed"),{"innerHTML":"0"});
		//this.cc(this.$("allFiles_time"),{"innerHTML":"-"});
		new this.fade(this.$("allFiles_p1"),"max");
		new this.fade(this.$("allFiles_p2"),"max");
	}
	this.allFile_4=function(str){//全部上传完毕
		if(str)this.allFile_h2(text4[1],str);
		else this.allFile_h1(text4[0]);
	}
	this.allFile_5=function(){//隐藏所有内容
		this.removeTags(5);
	}
	this.allFile_6=function(){//停止上传
		this.allFile_h1(text6[0]);
	}
	if(i!=5&&!this.$("allFiles_frame")){//创建HTML结构
		$box=this.cTag("div",{"id":"allFiles_frame","className":"box"});
		this.cTag("h3",{"id":"allFiles_h1","className":"hot"},$box);
		this.cTag("p",{"id":"allFiles_p1"},$box);
		this.cTag("span",{"id":"allFiles_p2","className":"loading","innerHTML":"<span id='allFiles_allLoading' style='width:0'></span>"},$box);
		//this.cTag("p",{"id":"allFiles_p3","innerHTML":"总体已上传：<strong id='allFiles_loading'>-</strong> % | 当前速度：<strong id='allFiles_speed'>-</strong>  | 预计剩余时间：<strong id='allFiles_time'>-</strong> 秒"},$box);
		this.cTag("p",{"id":"allFiles_p3","innerHTML":"总体已上传：<strong id='allFiles_loading'>-</strong> % "},$box);
		this.allFile_cTag($box,this.$("allFiles"));
	}
	var
	text1=["正在检查文件名是否重复...","发现有重名文件!","文件名与括号内已上传文件的原文件名相同，请检查是否为相同文件，如果不相同请更名后再上传，如相同则请取消上传此文件。"],
	text4=["全部文件上传成功，正在写入数据库...... ","上传完毕！以下文件未完成："],
	text6=["停止上传"];
	this.removeTags(i,string);//依据参数[i]，决定显隐的HTML结构.
	if(string)var str=string.split("|");//以[|]为间隔符转换参数[string]为数组类型
	i=parseFloat(i);//转换参数[i]为数字类型
	var fn_="allFile_"+i;this[fn_](str);//依据参数[i]分别执行对应的方法
}
//fileList.allFiles(1);                                      //检查图片
//fileList.allFiles(1,"ImgName|ImgName");                    //发现重复图片
//fileList.allFiles(2,"ImgName|总体进度|每秒速度|剩余时间"); //图片上传中
//fileList.allFiles(3,"ImgName");                            //图片上传完毕，处理中
//fileList.allFiles(4);                                      //上传成功
//fileList.allFiles(4,"ImgName|ImgName");                    //上传完毕，错误图片列表