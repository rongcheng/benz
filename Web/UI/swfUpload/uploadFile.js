function table(obj){
	this.$=function(obj,tag){//ȡid|tag
		if(!tag){return document.getElementById(obj);}
		else if(typeof obj=='string'){return document.getElementById(obj).getElementsByTagName(tag);}
		else{return obj.getElementsByTagName(tag);}
	}
	this.cTag=function(tag,c,fobj){//����html����
		var obj=(typeof tag!="object")?document.createElement(tag):tag;
		if(c!="")this.cc(obj,c);
		if(!fobj){return obj;}
		else{fobj.appendChild(obj);}
	}
	this.cc=function(obj,c){//�������������/����ֵ
		for(var i in c){
			if(typeof c[i]=="string"){if(i=="onclick")obj[i]=function(){eval(c[i]);};else obj[i]=c[i];}
			else{for(var n in c[i]){obj[i][n]=c[i][n]}}
		}
	}
	if(!this.$(obj.id)) return alert("ID�����Ҳ���Ŷ"+obj.id);
	var
	my=this,
	$box=this.$(obj.id,"div")[0],
	$ul=this.$($box,"ul")[0],
	$count1=this.$("count","span")[0],
	$count2=this.$("count","span")[1],
	delList=this.$("delList"),
	Upload=this.$("Upload");
	this.run=function(id,name,size){//����ļ��б�
		this.sizeMax(size,"+");
		var
		$li=this.cTag("li",{"id":id}),
		$li_name=this.cTag("span",{"innerHTML":name,"className":"name"},$li),
		$li_size=this.cTag("span",{"innerHTML":""+Math.round(size/1024)+"","size":""+size+"","className":"size"},$li),
		$li_state=this.cTag("em",{"title":"�Ƴ�","className":""+obj.trButtonClassName.initial+"","onclick":"my.remove('"+id+"',this);"},$li);
		$layer=this.cTag("p",{"id":id+"layer","className":"layer"},$li);
		this.opacity($li,0);
		this.cTag($li,"",$ul);
		new this.fade($li,"max");
	}
	this.size=0;
	this.sizeMax=function(size,i){//���㲢�ļ��ܴ�С
		if(i=="+"){this.size+=size;this.fileMax(1,"+");}
		else if(i=="-"){this.size-=size;if(this.size==0)this.fileMax(this.file,"-");else this.fileMax(1,"-");}
		var sizeMax=(this.size/1024/1024).toString().replace(/(^.+\..{2})(.+)/g,"$1");
		$count2.innerHTML=sizeMax;
	}
	this.file=0;
	this.fileMax=function(file,i){//���㲢�ļ�������
		if(i=="+")this.file+=file;
		else if(i=="-")this.file-=file;
		$count1.innerHTML=this.file;
	}
	this.remove=function(id,_this_){//ɾ�������ļ��б�
		if(_this_)_this_.onclick=null;
		var size=this.$(id,"span")[1].size;
		new this.fade(this.$(id),"min","$ul.removeChild(my.$('"+id+"'));");
		this.sizeMax(size,'-');
		if(obj.remove)eval(obj.remove);
	}
	this.allRemove=function(){//ɾ��ȫ���ļ��б�
		this.sizeMax(this.size,"-");
		this.for_(this.$($ul,"li").length,"new this.fade(my.$($ul,\"li\")[i],\"min\",\"$ul.removeChild(my.$($ul,'li')[0]);\");");
		if(obj.allRemove)eval(obj.allRemove);
	}
	this.up=function(id,i){//�ϴ��ļ�����
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
	this.button=function(x){//��ť״̬
	    //alert(obj.id+obj.buttonClassName);
		if(x=="00"){
			delList.onclick=Upload.onclick=function(){this.blur();};
			delList.className=Upload.className=obj.buttonClassName.disabled;
		}
		else if(x=="11"){
			this.cc(delList,{"className":""+obj.buttonClassName.initial+"","onclick":"my.allRemove();this.blur();"});
			this.cc(Upload,{"className":""+obj.buttonClassName.initial+"","innerHTML":"��ʼ�ϴ�","onclick":"eval('"+obj.upFile+"');this.blur();"});
		}
		else if(x=="01"){
			this.cc(delList,{"className":""+obj.buttonClassName.disabled+"","onclick":"this.blur();"});
			this.cc(Upload,{"className":""+obj.buttonClassName.initial+"","innerHTML":"ֹͣ�ϴ�","onclick":"eval('"+obj.stopFileUpLoad+"');this.blur();"});
		}
		else if(x=="10"){
			this.cc(delList,{"className":""+obj.buttonClassName.initial+"","onclick":"my.allRemove();this.blur();"});
			this.cc(Upload,{"className":""+obj.buttonClassName.disabled+"","innerHTML":"��ʼ�ϴ�","onclick":"this.blur();"});
		}
	}
	this.trButton=function(id,x){//�б�ť״̬
		if(!id)	this.for_(this.$($ul,"em").length,"if(this.$($ul,'em')[i].className=='"+obj.trButtonClassName.loading+"'){this.$($ul,'em')[i].className='"+obj.trButtonClassName.initial+"'}else if(this.$($ul,'em')[i].className=='"+obj.trButtonClassName.initial+"'){this.$($ul,'em')[i].className='"+obj.trButtonClassName.loading+"'}");
		if(x==obj.trButtonClassName.end){
			this.cc(this.$(id,"em")[0],{"title":"�����","className":""+obj.trButtonClassName.end+"","onclick":""});
			new this.fade(my.$(id+'layer'),"min");
		}
		if(x==obj.trButtonClassName.error){
			this.cc(this.$(id,"em")[0],{"title":"������","className":""+obj.trButtonClassName.error+"","onclick":""});
			new this.fade(my.$(id+'layer'),"min");
		}
	}
	this.for_=function(len,fn){for(var i=0;i<len;i++){eval(fn);}}
}
var fileList=new table({
	id:"upFile",//�����ID
	upFile:"UploadPic()",//��ʼ�ϴ��ļ�
	stopFileUpLoad:"stopFileUpLoad()",//ֹͣ�ļ��ϴ�
	remove:"removeFile(id);",//����ɾ��
	allRemove:"removeFiles()",//ȫ��ɾ��
	buttonClassName:{//��ť(����)״̬
		initial:"initial",//��ʼ
		disabled:"disabled"//������
	},
	trButtonClassName:{//��ť(�б�)״̬
		initial:"initial",//��ʼ(�Ƴ�)
		loading:"loading",//�ϴ���
		end:"end",//�ϴ����
		error:"error"//�ϴ�����
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
	this.allFile_h1=function(str){//������ʾģ��
		this.cc(this.$("allFiles_h1"),{"innerHTML":str});
		new this.opacity(this.$("allFiles_h1"),0);
		new this.fade(this.$("allFiles_h1"),"max");
	}
	this.allFile_h2=function(str,strs){//������ʾģ��
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
	this.allFile_1=function(str){//���ͼƬ�Ƿ��ظ�
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
	this.allFile_2=function(str){//�ϴ���
		this.cc(this.$("allFiles_p1"),{"innerHTML":"�����ϴ� [<strong>"+str[0]+"</strong>]"});
		this.cc(this.$("allFiles_allLoading"),{"style":{"width":str[1]+"%"}});
		this.cc(this.$("allFiles_loading"),{"innerHTML":str[1]});
		//this.cc(this.$("allFiles_speed"),{"innerHTML":str[2]});
		//this.cc(this.$("allFiles_time"),{"innerHTML":str[3]});
		if(str[1]==0){
			new this.fade(this.$("allFiles_p1"),"max");
			new this.fade(this.$("allFiles_p2"),"max");
		}
	}
	this.allFile_3=function(str){//�����ϴ����
		this.cc(this.$("allFiles_p1"),{"innerHTML":"���ϴ���� [<strong>"+str[0]+"</strong>]"});
		this.cc(this.$("allFiles_speed"),{"innerHTML":"0"});
		//this.cc(this.$("allFiles_time"),{"innerHTML":"-"});
		new this.fade(this.$("allFiles_p1"),"max");
		new this.fade(this.$("allFiles_p2"),"max");
	}
	this.allFile_4=function(str){//ȫ���ϴ����
		if(str)this.allFile_h2(text4[1],str);
		else this.allFile_h1(text4[0]);
	}
	this.allFile_5=function(){//������������
		this.removeTags(5);
	}
	this.allFile_6=function(){//ֹͣ�ϴ�
		this.allFile_h1(text6[0]);
	}
	if(i!=5&&!this.$("allFiles_frame")){//����HTML�ṹ
		$box=this.cTag("div",{"id":"allFiles_frame","className":"box"});
		this.cTag("h3",{"id":"allFiles_h1","className":"hot"},$box);
		this.cTag("p",{"id":"allFiles_p1"},$box);
		this.cTag("span",{"id":"allFiles_p2","className":"loading","innerHTML":"<span id='allFiles_allLoading' style='width:0'></span>"},$box);
		//this.cTag("p",{"id":"allFiles_p3","innerHTML":"�������ϴ���<strong id='allFiles_loading'>-</strong> % | ��ǰ�ٶȣ�<strong id='allFiles_speed'>-</strong>  | Ԥ��ʣ��ʱ�䣺<strong id='allFiles_time'>-</strong> ��"},$box);
		this.cTag("p",{"id":"allFiles_p3","innerHTML":"�������ϴ���<strong id='allFiles_loading'>-</strong> % "},$box);
		this.allFile_cTag($box,this.$("allFiles"));
	}
	var
	text1=["���ڼ���ļ����Ƿ��ظ�...","�����������ļ�!","�ļ��������������ϴ��ļ���ԭ�ļ�����ͬ�������Ƿ�Ϊ��ͬ�ļ����������ͬ����������ϴ�������ͬ����ȡ���ϴ����ļ���"],
	text4=["ȫ���ļ��ϴ��ɹ�������д�����ݿ�...... ","�ϴ���ϣ������ļ�δ��ɣ�"],
	text6=["ֹͣ�ϴ�"];
	this.removeTags(i,string);//���ݲ���[i]������������HTML�ṹ.
	if(string)var str=string.split("|");//��[|]Ϊ�����ת������[string]Ϊ��������
	i=parseFloat(i);//ת������[i]Ϊ��������
	var fn_="allFile_"+i;this[fn_](str);//���ݲ���[i]�ֱ�ִ�ж�Ӧ�ķ���
}
//fileList.allFiles(1);                                      //���ͼƬ
//fileList.allFiles(1,"ImgName|ImgName");                    //�����ظ�ͼƬ
//fileList.allFiles(2,"ImgName|�������|ÿ���ٶ�|ʣ��ʱ��"); //ͼƬ�ϴ���
//fileList.allFiles(3,"ImgName");                            //ͼƬ�ϴ���ϣ�������
//fileList.allFiles(4);                                      //�ϴ��ɹ�
//fileList.allFiles(4,"ImgName|ImgName");                    //�ϴ���ϣ�����ͼƬ�б�