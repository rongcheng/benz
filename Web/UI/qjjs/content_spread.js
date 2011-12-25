$(document).ready(function(){
	$(".bianji_title span").toggle(
		function(){
			$(this).parent().next().css("height","auto");
			$(this).html("[-] 收拢");
		},
		function(){
			$(this).parent().next().css("height","210px");
			$(this).html("[+] 展开");
		}
	);	
});



