$(document).ready(function(){
						   
//===============for click=================
    var img_href_first=$("#click_list a.on").attr("href");
	$("#big_image_click a").bind("click", function(){
				window.open (img_href_first , '', 'height=495, width=780, toolbar=no, menubar=no, scrollbars=yes, resizable=yes,location=no, status=yes','_blank');
			});
			
	$("#click_list a").click(function(){
		var img_title,img_id,img_name,img_src,img_href;
		img_title=$(this).attr("title");
		img_id=img_title.substring(0,img_title.indexOf(" "));
		img_name=img_title.substring(img_title.indexOf(" "),img_title.length );
		img_src=$("img",this).attr("lowsrc");
		img_href=$(this).attr("href");
		
		$("#big_image_click img").attr("src",img_src);
		$("#big_image_click a").unbind("click");
		$("#big_image_click a").bind("click", function(){
			window.open (img_href , '', 'height=495, width=780, toolbar=no, menubar=no, scrollbars=yes, resizable=yes,location=no, status=yes','_blank');
		});
		$("#img_info_click span").html(img_id);
		$("#img_info_click strong").html(img_name);
		
		$("#click_list a").removeClass("on");
		$(this).addClass("on");
		slide_controller_li_click($("#click_list a").index( $(this) ));
		clearInt();
		return false;

	});	
	
	$("#img_prev").click(function(){
		var img_title,img_id,img_name,img_src,img_href,$obj_click;
		$obj_click=$("#click_list a.on");
		img_title=$obj_click.parent().prev().find("a").attr("title");
		if(typeof(img_title)!="undefined"){			
			img_id=img_title.substring(0,img_title.indexOf(" "));
			img_name=img_title.substring(img_title.indexOf(" "),img_title.length );
			img_src=$obj_click.parent().prev().find("img").attr("lowsrc");
			img_href=$obj_click.parent().prev().find("a").attr("href");
			
			$("#big_image_click img").attr("src",img_src);
			$("#big_image_click a").unbind("click");
			$("#big_image_click a").bind("click", function(){
				window.open (img_href , '', 'height=495, width=780, toolbar=no, menubar=no, scrollbars=yes, resizable=yes,location=no, status=yes','_blank');
			});
			$("#img_info_click span").html(img_id);
			$("#img_info_click strong").html(img_name);
			
			$("#click_list a").removeClass("on");
			$obj_click.parent().prev().find("a").addClass("on");
		    slide_controller_li_click(i - 1);
		    clearInt();
		}
	});
	
	$("#img_next").click(function(){
		var img_title,img_id,img_name,img_src,img_href,$obj_click_click;
		$obj_click=$("#click_list a.on");
		img_title=$obj_click.parent().next().find("a").attr("title");
		if(typeof(img_title)!="undefined"){	
			img_id=img_title.substring(0,img_title.indexOf(" "));
			img_name=img_title.substring(img_title.indexOf(" "),img_title.length );
			img_src=$obj_click.parent().next().find("img").attr("lowsrc");
			img_href=$obj_click.parent().next().find("a").attr("href");
			
			$("#big_image_click img").attr("src",img_src);
			
			$("#big_image_click a").unbind("click");
			$("#big_image_click a").bind("click", function(){
				window.open (img_href , '', 'height=495, width=780, toolbar=no, menubar=no, scrollbars=yes, resizable=yes,location=no, status=yes','_blank');
			});

			$("#img_info_click span").html(img_id);
			$("#img_info_click strong").html(img_name);
			
			$("#click_list a").removeClass("on");
			$obj_click.parent().next().find("a").addClass("on");
		    slide_controller_li_click(i + 1);
		    clearInt();
		}
	});
	
	
//===============for slide=================
var t=false; //定时器
var time=2000; //切换间隔
var i=0; //起始序列
var $obj=$("#slide_list li") //大图列表对象
var len=$("#slide_list li").length; //滚动内容个数
var playstatus=true; //播放标记


	function setInt(){
	    if(t != false)
	        clearInt();
		t=setInterval(function(){roll()},time);
	}
	
	function clearInt(){
		if(t) 
		{
		    clearInterval(t);
		    t = false;
		}
	}
	
	function set_img_info_slide(num){
		var img_title,img_id,img_name,img_src;
		// get value
		img_title=$obj.eq(num).find("img").attr("title");
		img_id=img_title.substring(0,img_title.indexOf(" "));
		img_name=img_title.substring(img_title.indexOf(" "),img_title.length );
		
		// set value
		$("#img_info_slide span").html(img_id);
		$("#img_info_slide strong").html(img_name);
	}
	
	function roll(){

		if(i==len-1){	
			//$obj.eq(i).fadeOut();
			$obj.attr("style","display:none");
			$obj.eq(0).fadeIn();
			set_img_info_slide(0);
			$("#slide_controller li").removeClass("on");
			$("#slide_controller li").eq(0).addClass("on");
			//$obj.eq(0).attr("style","display:block");
			i=-1;
		}else{
			//$obj.eq(i).fadeOut();
			$obj.attr("style","display:none");
			$obj.eq(i+1).fadeIn();
			set_img_info_slide(i+1);
			$("#slide_controller li").removeClass("on");
			$("#slide_controller li").eq(i+1).addClass("on");
			//$obj.eq(i+1).attr("style","display:block");
		}
		i++;
		$("#click_list a").eq(i).click();
		setInt();
	}
	
	
	
	$("#slide_controller a.play_control").toggle(
		function(){
			clearInt();
			$("#slide_controller").removeClass("slide_on");
			playstatus = false;
		},
		function(){
			setInt();
			$("#slide_controller").addClass("slide_on");
			playstatus = true;
		}
	);
	
	$("#slide_controller li").click(function(){
		slide_controller_li_click($("#slide_controller li").index( $(this) ));
	});
	
	function slide_controller_li_click(j)
	{
	    $("#slide_controller li").removeClass("on");
		$("#slide_controller li").eq(j).addClass("on");
		clearInt();
		i = j;
		$obj.attr("style","display:none");
		$obj.eq(j).attr("style","display:block");
		set_img_info_slide(j);
		if(playstatus){
			setInt();
		}
	}
	
	clearInt();
//	setInt();
	
//===============for tab between click and slide=================
	$("#click_view .btn_slideshow").click(function(){
		$("#slide_view").show();
		$("#click_view").hide();
		setInt();
	});
	
	$("#slide_view .btn_back").click(function(){
		$("#slide_view").hide();
		$("#click_view").show();
	    clearInt();
	});
});



