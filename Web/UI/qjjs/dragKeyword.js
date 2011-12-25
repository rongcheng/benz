function GetList(url){
    var ExistsKeywords = GetExistsKeyword();   
    $.ajax({
       type: "GET",
       url: "/Handler/GetCata.ashx",
       data: "cataid="+url+"&ExistsKeywords="+escape(ExistsKeywords),
       success: function(msg){
       $("#KeywordContent").html(msg);      
         
        // make images in the gallery draggable
	    $('.keyword_content a, .keyword_including a,.keyword_content_BackGround a, .keyword_exception a').draggable({
		    helper: 'clone'
	    });

	    // make the keyword_content box droppable
	    $('.keyword_content').droppable({
		    accept: '.keyword_including a, .keyword_exception a',
		    activeClass: 'keyword_content_active',
		    
		    opacity: '0.5',
		    drop: function(ev, ui) {
			    ui.draggable.fadeOut('fast', function() {
				    $(this).fadeIn('fast');
			    }).appendTo($(this));
		    }
	    });
 // make the keyword_content box droppable
	    $('.keyword_content_BackGround').droppable({
		    accept: '.keyword_including a, .keyword_exception a',
		    activeClass: 'keyword_content_active',
		    
		    opacity: '0.5',
		    drop: function(ev, ui) {
			    ui.draggable.fadeOut('fast', function() {
				    $(this).fadeIn('fast');
			    }).appendTo($(this));
		    }
	    });
	    // make the keyword_including box droppable
	    $('.keyword_including').droppable({
		    accept: '.keyword_content a,.keyword_content_BackGround a, .keyword_exception a',
		    activeClass: 'including_active',
		    opacity: '0.5',
		    drop: function(ev, ui) {
			    $(this).addClass('dropped');
			    ui.draggable.fadeOut('normal', function() {
				    $(this).fadeIn('fast')
			    }).appendTo($(this));
		    }
	    });
    	
	    // make the keyword_including box droppable
	    $('.keyword_exception').droppable({
		    accept: '.keyword_content a,.keyword_content_BackGround a,.keyword_including a',
		    activeClass: 'exception_active',
		    opacity: '0.5',
		    drop: function(ev, ui) {
			    $(this).addClass('dropped');
			    ui.draggable.fadeOut('normal', function() {
				    $(this).fadeIn('fast')
			    }).appendTo($(this));
		    }
	    });
         //
         //if(document.getElementById('KeywordContent').className=='keyword_content')
       // {
          //document.getElementById('KeywordContent').className='keyword_content';
          $("#KeywordContent").removeClass("keyword_content_BackGround");
          $("#KeywordContent").addClass("keyword_content");
        // }
         
       },	           	          
       error:function(){	
      
       alert("您访问的页面出现问题，请稍候再试");	           
       }
     });
        
}

function GetCata(url,divid){
//for show on the initial , added by mashilei 12222008
 $("ul.property_title li:first").attr({style:"display:inline; line-height:180%; margin-left:4px;"});
 
    $.ajax({
       type: "GET",
       url: "/Handler/GetCata.ashx",
       data: "cataid="+url+"&div=1",
       success: function(msg){
       $("#"+divid).html(msg);
       }
     });
}

function GetCataMaster(){
    $.ajax({
       type: "GET",
       url: "/Handler/GetCata.ashx",
       data: "master=1",
       success: function(msg){
       $("#CataMaster").html(msg);
       }
     });
}
    //判断是否存在自定义的关键字，如果有就提示，没有的话就加入
    //得到拖拽框中的?
function GetExistsKeyword()
{
    var keys="";	
    var keyArrResult=[];
	
	
    $(".keyword_including a").each(
			    function(i,v){					   
				    keyArrResult.push($(v).text());
			    });
				
     $(".keyword_exception a").each(
    function(i,v){
	    keyArrResult.push($(v).text());
    });	    
	
    keys =  keyArrResult.join('|');
    return keys;
}