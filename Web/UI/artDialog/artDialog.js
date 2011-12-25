/*
 * artDialog v2.0.9 beta
 * Date: 2010-05-02
 * http://code.google.com/p/artdialog/
 * (c) 2009-2010 tangbin, http://www.planeArt.cn
 *
 * This is licensed under the GNU LGPL, version 2.1 or later.
 * For details, see: http://creativecommons.org/licenses/LGPL/2.1/
 */
(function() {
	//获取artDialog路径,用于调用外部文件
	var path,
	t = document.getElementsByTagName('script');
	t = t[t.length - 1].src.replace(/\\/g, '/');
	path = (t.lastIndexOf('/') < 0) ? '.' : t.substring(0, t.lastIndexOf('/'));
	
	//对话框控制台
	var load = function(o, y, n, c) {
		//在顶级页面弹出
		if (o.parent) {
			o.parent = false;
			try{
				return window.top.artDialog(o, y, n, c);
			}catch(e){};
		};
		
		if (o.id && document.getElementById(o.id) && (o.url || o.content)) return dialog(o.id);//如果定义了ID，不允许重复
		if (o.lock) o.fixed = true;//执行锁屏也同时执行静止定位
		
		return dialog(o.id).									/*-------*/
		newId(o.id).											/*设置ID */
		style(o.style).											/*设置风格*/
		content(o.title || '\u63D0\u793A', o.content, o.url).	/*设置内容*/
		yesBtn(y, o.yesText || '\u786E\u5B9A').					/*确定按钮*/
		noBtn(n, o.noText || '\u53D6\u6D88').					/*取消按钮*/
		size(o.width || 'auto', o.height || 'auto').			/*设置大小*/
		lock(o.lock).											/*锁屏遮罩*/
		align(o.left || 'center', o.top || 'center', o.fixed).	/*设置坐标*/
		btnFocus(y, n).											/*定位焦点*/
		close(c || 'null').										/*执行关闭*/
		time(o.time);											/*定时关闭*/
	},
	
	box,
	boxs = [],
	onmouse = false,
	dom = document.documentElement || document.body,
	IE = !+'\v1';
	IE6 = IE && ([/MSIE (\d)\.0/i.exec(navigator.userAgent)][0][1] == 6),
	ie6PngRepair = false,
	Each = function(a, b) {
		for (var i = 0, len = a.length; i < len; i++) b(a[i], i);
	},
	z = 999999,//对话框初始叠加高度(重要：Opera浏览器z-index的最大值限制都小于其他浏览器)
	hideId = 'artDialogTemp',//用于预加载背景图的对话框ID
	pageLock = 0,//锁屏遮罩计数器
	C = {
		x:	0,	//距离文档的X轴坐标
		y:	0,	//距离文档的Y轴坐标
		t:	0,	//对话框top值
		l:	0,	//对话框left值
		w:	0,	//对话框宽度
		h:	0,	//对话框高度
		st:	0,	//被滚动条卷去的文档高度
		sl:	0,	//被滚动条卷去的文档宽度
		ddw:0,	//浏览器内容可视宽度
		ddh:0,	//浏览器内容可视高度
		dbw:0,	//页面总宽度
		dbh:0,	//页面总高度
		ml:	0,	//left最大范围限制
		mt:	0	//top最大范围限制
	},
	winLoad = function (fn) {
		var oldonload = window.onload;
		if (typeof window.onload != 'function') {
			window.onload = fn;
		} else {
			window.onload = function() {
				if (oldonload) {
					oldonload();
				};
				fn();
			};
		};
	},
	
	//创建xhtml元素节点
	$ce = function (name){
		return document.createElement(name);
	},
	
	//创建文本节点
	$ctn = function (txt){
		return document.createTextNode(txt);
	},
	
	/*
	 *	样式操作
	 */
	hasClass = function(o, c){
		var reg = new RegExp('(\\s|^)'+c+'(\\s|$)');
		return o.className.match(reg);
	},
	addClass = function(o, c){		//添加类
		if(!hasClass(o, c)){
			o.className += ' '+c;
		};
	},
	removeClass = function(o, c){	//移除类
		if(hasClass(o, c)){
			var reg = new RegExp('(\\s|^)'+c+'(\\s|$)');
			o.className = o.className.replace(reg,' ');
		};
	},
	getClass = function(o, c){		//读取元素CSS属性值
		return o.currentStyle ? o.currentStyle[c] : document.defaultView.getComputedStyle(o, false)[c];   
	},
	addStyle = function(s) {		//向head添加样式
		var D = document,
		T = this.style;
		if(!T){
			T = this.style = D.createElement('style');
			T.setAttribute('type', 'text/css');
			D.getElementsByTagName('head')[0].appendChild(T);
		};
		T.styleSheet && (T.styleSheet.cssText += s) || T.appendChild(D.createTextNode(s));
	},
	
	//鼠标事件分配
	cmd = function(evt, x) {
		var e = evt || window.event;
		onmouse = true;
		box = this;
		box.zIndex();
		C.x = e.clientX;
		C.y = e.clientY;
		C.t = parseInt(this.target.style.top);
		C.l = parseInt(this.target.style.left);
		C.w = this.target.clientWidth;
		C.h = this.target.clientHeight;
		C.ddw = dom.clientWidth;
		C.ddh = dom.clientHeight;
		C.dbw = Math.max(dom.clientWidth, dom.scrollWidth);
		C.dbh = Math.max(dom.clientHeight, dom.scrollHeight);
		C.sl = dom.scrollLeft;
		C.st = dom.scrollTop;
		
		//保存最大拖动范围限制数据
		if (getClass(box.target, 'position') == 'fixed' || getClass(box.target, 'fixed') == 'true') {
			C.ml = C.ddw - C.w;
			C.mt = C.ddh - C.h;
		} else {
			C.ml =  C.dbw  - C.w;
			C.mt =  C.dbh - C.h;
		};
		
		if (x) {
			document.onmousemove = function(a) {
				resize.call(box, a, x);//调整对话框大小
			};
		} else {
			document.onmousemove = function(a) {
				drag.call(box, a);//拖动
			};
		};
		document.onmouseup = function() {
			onmouse = false;
			document.onmouseup = null;
			if (document.body.releaseCapture) box.target.releaseCapture();//IE释放鼠标监控
		};
		
		//IE下鼠标超出视口仍可监控
		if (document.body.setCapture) {
			box.target.setCapture();
			box.target.onmousewheel = function (){
				window.scrollTo(0, document.documentElement.scrollTop - window.event.wheelDelta / 4);
			};
		};
	},

	//拖动
	drag = function(a) {
		if (onmouse === false) return false;
		var e = a || window.event,
		_x = e.clientX,
		_y = e.clientY,
		_l = parseInt(C.l - C.x + _x - C.sl + dom.scrollLeft);
		_t = parseInt(C.t - C.y + _y - C.st + dom.scrollTop);
		if (_l > C.ml) _l = C.ml;
		if (_t > C.mt ) _t = C.mt;	
		if (_l < 0) _l = 0;
		if (_t < 0) _t = 0;
		box.target.style.left = _l + 'px';
		box.target.style.top = _t + 'px';
		return false;
	},
	
	//调整对话框大小
	resize = function(a, x) {
		if (onmouse === false) return false;
		var e = a || window.event,
		_x = e.clientX,
		_y = e.clientY,
		_w = C.w + _x - C.x + x.w,
		_h = C.h + _y - C.y + x.h;
		if (_w > 0) x.obj.style.width = _w + 'px';
		if (_h > 0) x.obj.style.height = _h + 'px';
		return false;
	},
	
	//对话框核心
	dialog = function(id) {
		var j = -1;
		Each(boxs,
			function(o, i) {
				if (id) {
					if (o.id === id) j = i;
				} else {
					if (o.free === true) j = i;
				};
			}
		);
		if (j >= 0) return boxs[j];//对象重用
		
		/*
		 *	九宫格布局
		 *	
		 *	基于table 与 div,自适应
		 */
		var ui_title_wrap = $ce('td'),					//标题栏
		ui_title = $ce('div'),							//标题与按钮外套
		ui_title_text = $ce('div'),						//标题文字内容
		ui_close = $ce('a');							//关闭按钮
		ui_title_wrap.className = 'ui_title_wrap';
		ui_title.className = 'ui_title';
		ui_title_text.className ='ui_title_text';
		ui_close.className ='ui_close';
		ui_close.href = 'javascript:void(0)';
		ui_close.appendChild($ctn('×'));
		ui_title.appendChild(ui_title_text);
		ui_title.appendChild(ui_close);
		ui_title_wrap.appendChild(ui_title);
		
		var ui_content_wrap = $ce('td'),				//内容区
		ui_content = $ce('div'),
		ui_url = $ce('iframe');
		ui_content_wrap.className = 'ui_content_wrap';
		ui_content.className = 'ui_content';
		ui_content_wrap.appendChild(ui_content);
		
		var yesBtn = $ce('button'),						//确定按钮
		yesWrap = $ce('span'),
		noBtn = $ce('button'),							//取消按钮
		noWrap = $ce('span');
		yesWrap.className = 'ui_yes';
		noWrap.className = 'ui_no';
		
		var ui_bottom_wrap = $ce('td'),					//底部按钮区
		ui_bottom = $ce('div'),
		ui_btns = $ce('div'),
		ui_resize = $ce('div');
		ui_bottom_wrap.className = 'ui_bottom_wrap';
		ui_bottom.className = 'ui_bottom';
		ui_btns.className = 'ui_btns';
		ui_resize.className = 'ui_resize';
		ui_bottom.appendChild(ui_btns);
		ui_bottom.appendChild(ui_resize);
		ui_bottom_wrap.appendChild(ui_bottom);
		
		var ui_dialog_main = $ce('table'),				//内容表格
		cTbody = $ce('tbody');
		ui_dialog_main.className = 'ui_dialog_main';
		for(var r = 0; r < 3; r++){
			_tr = $ce('tr');
			if (r == 0) _tr.appendChild(ui_title_wrap);
			if (r == 1) _tr.appendChild(ui_content_wrap);
			if (r == 2) _tr.appendChild(ui_bottom_wrap);
			cTbody.appendChild(_tr);
		};
		ui_dialog_main.appendChild(cTbody);
		
		var bTable = $ce('table'),						//外边框表格
		bTbody = $ce('tbody');
		for(var r=0;r<3;r++){
			_tr = $ce('tr');
			for(var d=0; d<3; d++){
				_td = $ce('td');
				if(r == 1 && d == 1) {
					_td.className = 'r' +r+ 'd' +d;
					_td.appendChild(ui_dialog_main);
				}else{
					_td.className = 'ui_border r' +r+ 'd' +d;
				}
				_tr.appendChild(_td);
			};
			bTbody.appendChild(_tr);
		};
		bTable.appendChild(bTbody);

		var ui_dialog = $ce('div');
		ui_dialog.className = 'ui_dialog';
		ui_dialog.appendChild(bTable);
		if (IE6) {
			var ui_ie6_select_mask = $ce('iframe');
			ui_ie6_select_mask.className = 'ui_ie6_select_mask';
			ui_dialog.appendChild(ui_ie6_select_mask);
		};

		var ui_overlay = $ce('div');					//锁屏遮罩
		ui_overlay.className = 'ui_overlay';
		ui_overlay.appendChild($ce('div'));
		
		var ui_dialog_wrap = $ce('div');				//对话框外套
		ui_dialog_wrap.className = 'ui_dialog_wrap';
		ui_dialog_wrap.appendChild(ui_dialog);
		/*九宫格布局结束*/
		
		//拖动事件
		Each([ui_title_text], function(o, i) {
			o.onmousedown = function(a) {
				cmd.call($, a, false);
				addClass(ui_dialog_wrap, 'ui_move');
			};
			o.onmouseout = function() {
				removeClass(ui_dialog_wrap, 'ui_move');
			};
			o.onselectstart = function(){
				return false;//禁止选择文字
			};
		});
		
		//调整窗口大小的把柄事件
		ui_resize.onmousedown = function(a) {
			var d = ui_dialog,
			c = ui_content_wrap;
			cmd.call($, a, {obj:c, w:c.clientWidth - d.clientWidth, h:c.clientHeight - d.clientHeight});
		};
		
		//鼠标靠近按钮触发样式
		if (IE6) {
			Each([yesWrap, noWrap], function(o, i) {
				o.onmouseover = function() {
					addClass(o, 'ui_hover');
				};
				o.onmouseout = function() {
					removeClass(o, 'ui_hover');
				};
			});
		};
		
		//关闭按钮事件
		ui_close.onclick = function(){
			$.close()
		};
		document.onkeyup = function(evt){
			var e = evt || window.event;
			if(e.keyCode == 27) $.close();//ESC键关闭弹出层
		};
		
		//向页面插入对话框
		document.body.appendChild(ui_dialog_wrap);
		
		var $ = {};
		$.target = ui_dialog;
		$.target.style.zIndex = ++z;
		
		//设置ID(id值)
		$.newId = function(id) {
			$.id = ui_dialog_wrap.id = id;
			ui_content.id = id + '_content';
			ui_url.id = id + '_url';
			return $;
		};
		
		//消息内容构建(标题, HTML内容, iframe地址)
		$.content = function(title, content, url) {
			ui_title_text.innerHTML = '<span class="ui_title_icon"></span>' +title;
			
			if (content) {
				ui_content.innerHTML = '<span class="ui_dialog_icon"></span>' + content;
			} else if (url) {
				var show = function(){
					ui_url.style.visibility = 'visible';
				};
				addClass(ui_content, 'ui_iframe');
				ui_url.src = url;
				ui_content.appendChild(ui_url);

				if (ui_url.attachEvent){
					ui_url.attachEvent('onload', function(){
						show();
					});
				} else {
					ui_url.onload = function(){
						show();
					};
				};
			} else {
				return $;
			};
						
			ui_dialog.style.visibility = 'visible';//显示对话框
			
			$.free = false;
			return $;
		};
		
		//尺寸(宽度, 高度)
		$.size = function(w, h) {
			if(parseInt(w) == w) w = w + 'px';
			if(parseInt(h) == h) h = h + 'px';
			ui_content_wrap.style.width = w;
			ui_content_wrap.style.height = h;
			return $;
		};
		
		//位置(横坐标, 纵坐标, 静止定位)
		$.align = function(left, top, fixed) {
			var dd = document.documentElement,
			db = document.body;
			C.l = 0;
			C.t = 0;
			C.w = ui_dialog.clientWidth;
			C.h = ui_dialog.clientHeight;
			C.ddw = dom.clientWidth;
			C.ddh = dom.clientHeight;
			C.dbw = Math.max(dom.clientWidth, dom.scrollWidth);
			C.dbh = Math.max(dom.clientHeight, dom.scrollHeight);
			//C.sl = dom.scrollLeft;
			//C.st = dom.scrollTop;
			//兼容chrome与Safari的写法
			C.sl = Math.max(dd.scrollLeft, db.scrollLeft);
			C.st = Math.max(dd.scrollTop, db.scrollTop);
			var minX, minY, maxX, maxY, centerX, centerY;
			if (fixed) {
				if (IE6) addClass(document.getElementsByTagName('html')[0], 'ui_ie6_fixed');
				addClass(ui_dialog_wrap, 'ui_fixed');
				
				minX = 0;
				maxX = C.ddw - C.w;
				centerX = maxX / 2;
				minY = 0;
				maxY = C.ddh - C.h;
				//黄金比例垂直居中
				var hc =  C.ddh * 0.382 - C.h / 2;
				centerY = (hc > 0) ?  hc : maxY / 2;
			} else {
				minX = C.sl;
				maxX = C.ddw + minX - C.w;
				centerX = maxX / 2;
				minY = C.st;
				maxY = C.ddh + minY - C.h;
				//黄金比例垂直居中
				var hc = C.ddh * 0.382 - C.h / 2 + minY;
				centerY =  (hc > minY) ? hc : (maxY + minY) / 2;
			};
			if(left == 'center'){
				C.l = centerX;
			}else if(left == 'left'){
				C.l = minX;
			}else if(left == 'right'){
				C.l = maxX;
			}else{
				if (fixed) left = left - C.sl;//把原点移到浏览器视口
				if (left < minX) {
					left = left + C.w;
				} else if (left > maxX) {
					left = left - C.w;
				};
				C.l = left;
			};
			if (top == 'center'){
				C.t = centerY;
			} else if (top == 'top'){
				C.t = minY;
			} else if (top == 'bottom'){
				C.t = maxY;
			} else {
				if (fixed) top = top - C.st;//把原点移到浏览器视口
				if (top < minY) {
					top = top + C.h;
				} else if (top > maxY) {
					top = top - C.h;
				};
				C.t = top;
			};
			if (C.l < 0) C.l = 0;
			if (C.t < 0) C.t = 0;
			if (ui_dialog_wrap.id == hideId) C.l = '-99999';//让预加载背景图的对话框偏离可视范围
			ui_dialog.style.left = C.l + 'px';
			ui_dialog.style.top = C.t + 'px';
			$.zIndex(ui_dialog);
			return $;
		};
		
		//确定按钮(回调函数, 按钮文本)
		$.yesBtn = function(fn, txt){
			if (fn) {
				yesBtn.innerHTML = txt;
				yesWrap.appendChild(yesBtn);
				ui_btns.appendChild(yesWrap);
				yesBtn.onclick = function() {
					var f = fn();
					if (f == undefined || f) $.close();//如果回调函数返回false则不关闭对话框
				};
				
			};
			return $;
		};	
		
		//取消按钮(回调函数, 按钮文本)
		$.noBtn = function(fn, txt){
			if (fn) {
				noBtn.innerHTML = txt;
				noWrap.appendChild(noBtn);
				ui_btns.appendChild(noWrap);
				noBtn.onclick = function() {
					var f = fn();
					if (f == undefined || f) $.close();//如果回调函数返回false则不关闭对话框
				};
			}
			return $;
		};
		
		//焦点定位(确定按钮, 取消按钮)
		//注意此函数最好在定位后执行，否则会造成页面滚动
		$.btnFocus = function(y, n){
			try{
				if (n) {
					noBtn.focus();
				} else
				if (y) {
					yesBtn.focus();
				} else {
					ui_close.focus();
				};
			}catch(e){};
			return $;
		};

		//关闭对话框(回调函数)
		$.closeFn = null;
		$.close = function(f) {
			if (f) {
				if (typeof f === 'function') $.closeFn = f;
				return $;
			};
			
			onmouse = false;
			$.free = true;
			
			var closeWin = function(){
				if (ie6PngRepair) {							//如果开启了'IE6 png修复'则销毁元素，防止下一次重复调用而出现背景图像错位
					ui_dialog_wrap.parentNode.removeChild(ui_dialog_wrap);
					$.free = false;
					return false;
				};
				
				ui_dialog.style.cssText = ui_title_text.innerHTML = ui_content.innerHTML = ui_btns.innerHTML = ui_dialog_wrap.id = ui_content.id = '';//清空设置
				removeClass(ui_content, 'ui_iframe');		//移除嵌入框架专属样式
				removeClass(ui_dialog_wrap, 'ui_fixed');	//移除静止定位属性
				bTable.className = '';						//移除风格属性
				
				//执行回调函数
				if ($.closeFn) {
					$.closeFn();
					$.closeFn = null;
				};
			};
			
			if (ui_dialog_wrap.className.indexOf('ui_lock') > -1){
				$.alpha(ui_overlay, 1, function(){
					ui_overlay.style.cssText = '';			//隐藏遮罩
					if (pageLock == 1) removeClass(document.getElementsByTagName('html')[0], 'ui_page_lock');//移除锁屏样式
					pageLock --;
					removeClass(ui_dialog_wrap, 'ui_lock');
					closeWin();
				});
			} else {
				closeWin();
			};
		};
		
		//定时关闭对话框(秒)
		$.time = function(t) {
			if (t) setTimeout(function(){
				$.close();
			}, 1000 * t);
			return $;
		};
		
		//附加风格(className)
		$.style = function(s) {
			if (s) {
				bTable.className = s;
			};
			return $;
		};
		
		//增大对话框叠加高度(元素)
		$.zIndex = function(o) {
			var x = o ? o : ui_dialog;
			x.style.zIndex = ++z;
			ui_dialog_wrap.style.zIndex = ++z;//IE6与Opera叠加高度受具有绝对或者相对定位的父元素z-index控制
			return $;
		};
		
		//锁屏
		$.lock = function(o){
			if (o) {
				var h = document.getElementsByTagName('html')[0];
				addClass(h, 'ui_page_lock');
				addClass(ui_dialog_wrap, 'ui_lock');
				$.zIndex(ui_overlay);
				ui_dialog_wrap.appendChild(ui_overlay);
				ui_overlay.style.visibility = 'visible';//显示遮罩
				pageLock ++;
				$.alpha(ui_overlay, 0);
			};
			return $;
		};
		
		//透明渐变(元素, 初始透明值[0,1], 回调函数, [自身调用赋值])
		$.alpha = function(obj, int , fn, x){
			if(!x) i = int;
			s = 0.2;
			s = (int == 0) ? s : -s;
			i += s;
			if(obj.filters) {
				obj.filters.alpha.opacity = i * 100;
			} else {
				obj.style.opacity = i;
			};
			if (i > 0 && i < 1) {
				setTimeout(function(){
					$.alpha(obj, int, fn, i)
				}, 5);
			} else if(fn) {
				fn();
			};
			return $;
		};
		
		//保存列队
		return boxs[boxs.push($) - 1];
	};//dialog end
	
	/*{
	 *	artDialog兼容框架样式[内部版本1.0.8 2010-04-24]
	 *	
	 *	支持跨浏览器全屏屏遮罩, IE6完美静止定位支持, IE6下拉控件遮盖, 消息智能对齐
	 *	关闭'覆盖IE6下拉控件'的功能，请在皮肤CSS中写入这句：* html .ui_ie6_select_mask { display:none!important }
	 */
	addStyle('.ui_title_icon,.ui_content,.ui_dialog_icon,.ui_btns span{display:inline-block;*zoom:1;*display:inline}.ui_dialog{visibility:hidden;text-align:left;position:absolute;top:0;left:-99999em;_overflow:hidden}.ui_dialog table{border:0;margin:0;border-collapse:collapse}.ui_dialog td{padding:0}.ui_title_icon,.ui_dialog_icon{vertical-align:middle}.ui_title_text{overflow:hidden;cursor:default;-moz-user-select:none;user-select:none}.ui_close{display:block;position:absolute;outline:none}.ui_content_wrap{text-align:center}.ui_content{margin:10px;text-align:left}.ui_content.ui_iframe{margin:0;*padding:0;display:block;height:100%}.ui_iframe iframe{visibility:hidden;width:100%;height:100%;border:none;overflow:auto}.ui_bottom{position:relative}.ui_resize{position:absolute;right:0;bottom:0;z-index:1;cursor:nw-resize;_font-size:0}.ui_btns{text-align:right;white-space:nowrap}.ui_btns span{margin:5px 10px}.ui_btns button{cursor:pointer}.ui_overlay{visibility:hidden;position:absolute;top:0;left:0;width:100%;height:100%;filter:alpha(opacity=0);opacity:0;_overflow:hidden}.ui_overlay div{height:100%}* html .ui_ie6_select_mask{width:99999em;height:99999em;position:absolute;top:0;left:0;z-index:-1}.ui_move .ui_title_text{cursor:move}html>body .ui_dialog_wrap.ui_fixed .ui_dialog{position:fixed}* html .ui_dialog_wrap.ui_fixed .ui_dialog{fixed:true}* html.ui_ie6_fixed{background:url(*) fixed}* html.ui_ie6_fixed body{height:100%}* html .ui_dialog_wrap.ui_fixed{width:100%;height:100%;position:absolute;left:expression(documentElement.scrollLeft+documentElement.clientWidth-this.offsetWidth);top:expression(documentElement.scrollTop+documentElement.clientHeight-this.offsetHeight)}html.ui_page_lock>body{overflow:hidden}html.ui_page_lock{*overflow:hidden}* html.ui_page_lock select,* html.ui_page_lock .ui_ie6_select_mask{visibility:hidden}html.ui_page_lock>body .ui_dialog_wrap.ui_lock{width:100%;height:100%;position:fixed;top:0;left:0}* html body{margin:0}@media all and (-webkit-min-device-pixel-ratio:10000),not all and (-webkit-min-device-pixel-ratio:0){.ui_content_wrap,.r0d0,.r0d2,.r2d2,.r2d0{display:block}}');/*}*/
	
	if (IE6) {
		document.execCommand('BackgroundImageCache', false, true);//开启IE6 CSS背景图片默认缓存
		
		/*
		 *	IE6 PNG 32 透明与背景位置修复
		 *
		 *	在CSS文件 写入 * html { ie6PngRepair:true; } 即可开启此功能，默认关闭
		 *	开启此功能会导致artDialog遮盖不住IE6的下拉控件，建议针对IE6制作全透明的png 8位或者gif的背景
		 *	开启此功能让调整对话框大小拖动手柄失效
		 */
		ie6PngRepair = getClass(document.getElementsByTagName('html')[0], 'ie6PngRepair') == 'true' ? true : false;//检验皮肤CSS中是否开启此功能
		if (ie6PngRepair) {
			var script = $ce('script');
			script.src = path + '/iepngfix/iepngfix_tilebg.js';
			document.getElementsByTagName('head')[0].appendChild(script);
			addStyle('.ui_resize, .ui_ie6_select_mask { display:none; } .ui_border, .ui_title_wrap *, .ui_dialog_icon  { behavior: url("' +path+ '/iepngfix/iepngfix.htc")}');//修复png半透明(没有写上按钮，因为应用到按钮点小问题)
		};/*}*/
	};
	
	//页面载入即弹出一个对话框，让其在隐秘处预先加载皮肤背景图片:-)
	winLoad(function(){
		artDialog({id:hideId}, function(){}, function(){});
	});
	
	window.artDialog = load;//对外暴露唯一变量
})();