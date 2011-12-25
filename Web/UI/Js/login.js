
function showLoginBox()
{
    document.getElementById("error_div").style.display = "none";
	document.getElementById("content_div").style.display = "block";

}
$(function(){
    $("#a_login").click(function(){
    doLogin();
    return false;
    })
    
    
   var u=$("#txtloginName").val();
    var p=$("#txtPassword").val();
    if(u!="" && p!="")
    {  
    doLogin();
    }
})

function doLogin()
{

           document.getElementById("error_div").style.display = "block";
           document.getElementById("content_div").style.display = "none";
            
           $("#error_div").addClass("error_div_load");
           $("#error_div").html(" &nbsp; &nbsp; <img src=/images/loading_2.gif align='absmiddle'> 正在登陆中......");
           

    var u=$("#txtloginName").val();
    var p=$("#txtPassword").val();
    if(u=="" || p=="")
    {        
       $("#error_div").html(" &nbsp; &nbsp; <font color=red>出错了：</font> &nbsp;用户名和密码不能为空");
       setTimeout("$('#error_div').hide();",2000);
       setTimeout("$('#content_div').fadeIn(2000);",2000);
	       
        if(u=="")
        {
            $("#txtloginName").focus();
        }
        else
        {
            $("#txtPassword").focus();
        }

        return;
    }
    $.get("/Handlers/loginHandler.ashx?action=Login",{"userName":u,"password":p},
    function(data) {    
       //alert(data);
       if(data=="0")
       {
            alert("用户名和密码不能为空");
            
       }
       else if(data=="2")
       {
           
           $("#error_div").removeClass("error_div_load");
           $("#error_div").addClass("error_div");
           $("#error_div").text("");
           
           document.getElementById("error_div").style.display = "block";
           document.getElementById("content_div").style.display = "none";
           
	       setTimeout("$('#error_div').hide();",3000);
	       setTimeout("$('#content_div').fadeIn(3000);",3000);
       }
       else if(data=="1")
       {           
           location.href="/";
       }
    });
    
}


