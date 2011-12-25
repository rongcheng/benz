
function doEffect(strAction)
{
    if(strAction=="gray")
    {
        doImage("gray","none");
    }
    else if(strAction=="rotate")
    {
        doImage("rotate",50);
    }
    else if(strAction=="border")
    {
        doImage("border","12,red");
    }
    else
    {
        doImage(strAction,"none");    
    }
}



function doImage(cmd,param)
{
    var url="/imageeditoronline/imagecontroller.ashx?action="+cmd+"&param="+param+"&srcFile=rc2.jpg";    
    $.get(url,"",function(data){
        
        $("#destinationImage").html("<img src='"+data+"' >");
        $("#sourceImage").hide();
        
        artDialog({id:'setting'}).close();
        
    });
}


$(function(){   
    
   $('#btn3').click(function(){
	artDialog({id:'setting', menuBtn:'sourceImage',lock:true,title:'参数设置', url:'setting/rotate.htm'}).close(function(){});
	}); 
	
	$('#aGray').click(function(){
	    doEffect("gray");
	});

	$('#aRotate').click(function(){
	artDialog({id:'setting',lock:true,title:'参数设置', url:'setting/rotate.htm',width:300,height:100}).close(function(){}); 
    });
    
    $('#aBorder').click(function(){
	artDialog({id:'setting',lock:true,title:'参数设置', url:'setting/border.htm',width:300,height:100}).close(function(){}); 
    });
    
     $('#aFlop').click(function(){
	artDialog({id:'setting',lock:true,title:'参数设置', url:'setting/flop.htm',width:300,height:100}).close(function(){}); 
    });

});   