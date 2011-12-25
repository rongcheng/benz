/*
演示脚本
*/


(function(){
		  
	//判断IE6
	
	//document.getElementById简化函数
	var $ = function (id){
		return 'string' == typeof id ? document.getElementById(id) : id;
	};
	
	//页面就绪，允许你绑定一个在DOM文档载入完成后执行的函数
	var domReady = !+'\v1' ? function(f){(function(){
			try{
				document.documentElement.doScroll('left');
			} catch (error){
				setTimeout(arguments.callee, 0);
				return;
			};
			f();
		})();
	} : function(f){
		document.addEventListener('DOMContentLoaded', f, false);
	};
	
	//在页面就绪后绑定事件,你也可以使用window.onload
	domReady(function(){
		$('page').style.display = 'block';
		$('noscript').style.display = 'none';	
		
		
		//--------------------------ardDialog演示脚本开始------------------------------//
		$('btn1').onclick = function(){
			art.dialog({title:'图片查看',fixed:true, content:'<img width="817" height="479" src="http://www.hunanyishi.cn/images/main.jpg" />'});
			return false;
		};
		

		$('btn2').onclick = function(){
			art.dialog({id:'testIframe', iframe:'_demo/iframe.html' }, function(){
				var msg = art.dialog({id:'testIframe'}).data.iframe;// 使用内置接口获取iframe对象

				var text = msg.document.getElementById('googleText');
				if (text.value == '') {
					text.focus();
					msg.document.getElementById('tips').innerHTML = '必须填写关键字！';
					return false; // 返回false即可阻止对话框关闭
				};
				msg.document.getElementById('googleForm').submit();
			});
			return false;
		};
		$('btn2Goto').onclick = function(){
			art.dialog({title:'唐斌-腾讯微博', iframe:'http://www.google.com', width:'900', height:'500'});
			return false;
		};
		
		$('btn3').onclick = function(){
			art.dialog({title:'功夫兔', content:'<object width="420" height="363"><param name="movie" value="http://www.tudou.com/v/bXwe7XgTxuA"></param><param name="allowFullScreen" value="true"></param><param name="allowscriptaccess" value="always"></param><param name="wmode" value="opaque"></param><embed src="http://www.tudou.com/v/bXwe7XgTxuA" type="application/x-shockwave-flash" allowscriptaccess="always" allowfullscreen="true" wmode="opaque" width="420" height="363"></embed></object>', fixed:true});
			return false;
		};
		
		$('btn4').onclick = function(){
			art.dialog({content:'你人品稳定么？', fixed:true, yesText:'我很稳定', style:'confirm', id:'bnt4_test'}, function(){
				var msg = art.dialog({id:'bnt4_test'}).data.content; // 使用内置接口获取消息容器对象
				msg.innerHTML = '你骗人！';
				return false;
			}, function(){alert('你是坏人');});//style参数可以填写多个，用空格隔开。具体有什么请看皮肤css文件
			return false;
		};
		
		$('btn5').onclick = function(){
			art.dialog({content:'你是坏人', style:'succeed', width:'14em', lock:true}, function(){
				art.dialog({content:'hello world!', lock:true});
				return false;//回调函数返回false可以阻止对话框默认关闭
			}, function(){});
			return false;
		};
		
		$('btn6').onclick = function(){
			var _this = this;
			
			if ($('menu_4834783')) {
				art.dialog({id:'menu_4834783'}).close();
				_this.innerHTML = '弹出菜单';
				return;
			};
			
			art.dialog({id:'menu_4834783', title:'菜单', content:'请输入：<input style="width:200px;" id="M_dfd" type="text" value="hello world!" />',menuBtn:this}, function(){
				var a = $('M_dfd').value;
				art.dialog({content:a, lock:true, time:1});
			}).close(function(){
				_this.innerHTML = '弹出菜单';
			});
			_this.innerHTML = '关闭菜单';
			return false;
		};

		$('btn6runCodeBtn').onclick = function(){
			art.dialog({id:'dg_btn6runCodeBtn', content:'你确定运行吗？', menuBtn:$('runCodeBtn')}, function(){
				$('runCodeBtn').click();
			}, function(){});
		};
		
		$('btn7').onclick = function(){
			art.dialog({mouse:true, id:'dg_test34243', content:'您收到 <strong>2</strong> 条消息',left:'right',width:'15em', top:'bottom', fixed:true});//left和top坐标可以使用关键字，当然也可以使用数字
			return false;
		};
		$('btn7Close').onclick = function(){
			art.dialog({id:'dg_test34243'}).close();
			return false;
		};
		//--------------------------ardDialog演示脚本结束------------------------------//
		


		//导航
		var demoNav = function(){
			var html = $('navWrap').innerHTML;
			art.dialog({id:'pageNav', content:html, fixed:true, top:'bottom', style:'noSkin'})
		}();
		

		//皮肤切换
		function mySkin(s){
			$('artDialogSkin').href = s;
		};
		try {
			$('skin_3').click();

			$('skin_0').onclick = function(){
				mySkin('skin/aero/aero.css?' + new Date());
			};
			$('skin_1').onclick = function(){
				mySkin('skin/chrome/chrome.css?' + new Date());
			};
			$('skin_2').onclick = function(){
				mySkin('skin/facebook/facebook.css?' + new Date());
			};
			$('skin_3').onclick = function(){
				mySkin('skin/default.css?' + new Date());
			};
			$('showBg').onclick = function(){
				if (!$('test_3544')) {
					document.getElementsByTagName('body')[0].className = 'showBg';
					art.dialog({id:'test_3544', content:'<div id="topMenu" style="background:#000; width:200px; height:2em;line-height:2em;text-align:center; filter:alpha(opacity=70); opacity:0.7;">[<a href="#" style="color:#FFF" onclick="showWin();return false">打开新对话框</a>]&nbsp;&nbsp;[<a id="bgCloseBtn" href="#" style="color:#FFF" onclick="bgShow();return false">关闭</a>]</div>', left:'left', top:'top', style:'noSkin', fixed:true});
				} else {
					bgShow();
				};
				return false;
			};
		} catch(e) {};

		
		//运行代码
		$('runCodeBtn').onclick = function(){
			var fn = $('runCode').value;
			try {
				eval(fn);
			} catch(e) {
				art.dialog({title:'警告', content:'悟空，不要乱搞！<br />' + e, lock:true}).close(function(){
					$('runCode').value = "art.dialog({content:'hello world!'})";
				});
			};
			return false;
		};
		
		
		var mailName = '1987.tangbin' + '@' + 'g' + 'mail' + '.com';
		$('myMail').innerHTML = mailName;
	});

})();

//显示一个新对话框
function showWin(){
			art.dialog({content:'欢迎使用 "art.dialog" 对话框组件！', fixed:true, lock:false,style:'succeed'}, function(){art.dialog({title:' ', content:'谢谢观赏', lock:true, time:2})});
};

//显示背景
function bgShow(){
	var body = document.getElementsByTagName('body')[0];
	if(body.className != 'showBg') {
		body.className = 'showBg';
		document.getElementById('bgCloseBtn').innerHTML = '关闭背景';
	} else {
		body.className = '';
		document.getElementById('bgCloseBtn').innerHTML = '打开背景';
	};
};