<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserLogin_Sany.ascx.cs" Inherits="WebUI.UserControls.UserLogin_Sany1" %>
<style type="text/css">
* {padding:0; margin:0;}
.login_body {background-color:#f7f7f7;}
.login_main {background:url(/images/bg.gif) repeat-x 0 0; height:407px; width:100%;}
.login_pannel {width:644px; height:165px; padding:241px 180px 0px 180px;margin:0 auto; background:url(/images/img1.jpg) 50% 93px no-repeat;}
.login_pannel .l {width:7px; height:165px; background:url(/images/loginUI.png) -384px 0 no-repeat; float:left;}
.login_pannel .c {width:630px; height:165px; background:url(/images/bg.gif) 0 -500px; float:left; overflow:hidden}
/*.login_pannel h2 {width:214px; height:28px; background:url(/images/loginUI.png) 0 -96px no-repeat; 
                  margin:36px 0 0 44px; float:left; display:block;} */
.login_pannel h2 {width:214px; height:40px; background:url(/images/loginUI.png) 0px -96px no-repeat; margin:36px 0 0 40px; 
float:left; display:block;border:0px solid black} 

.login_pannel .content {background:url(/images/loginUI.png) -398px 0 no-repeat; width:329px; height:165px; float:right; overflow:hidden; }
.login_pannel .error_div {display:none; background:url(/images/loginUI.png) no-repeat -83px -205px; float:right; height:103px; width:324px;}
.login_pannel .error_div_load { background:none;margin:60px 0 0 -20px;font-size:12px;}

.login_pannel .r {width:7px; height:165px; background:url(/images/loginUI.png) -391px 0 no-repeat; float:left;}
.login_pannel .content .rl {width:90px; float:left; text-align:right;font-size:12px; line-height:22px; color:#7a7a7a;}
.login_pannel .content .rr {width:239px; float:left;}
.login_pannel .content .c1 {height:22px; padding:42px 0 0 0;}
.login_pannel .content .c2 {height:22px; padding:17px 0 0 0;}
.login_pannel .content .c3 {height:37px; padding:24px 0 0 0;}
.login_pannel .content input{background:url(/images/loginUI.png) no-repeat 0 -74px; border:0; width:178px; height:18px; padding:2px;}
.login_input a {width:182px; height:37px; float:left; display:block;}
.login_input a:link,
.login_input a:visited{background:url(/images/loginUI.png) no-repeat 0 0;}
.login_input a:hover,
.login_input a:active {background-position: 0 -37px;}

.footer { margin-top:10px; padding:30px 0; border-top:dashed 1px #d9d9d9; line-height:26px; color:#c2c2c2; font-size:12px;}
.footer h2 { display:none; }
.footer p {padding:0 20px;}
.footer a { color:#838383; }
.footer strong { font-weight:normal; }
</style>

<script src="../UI/Js/login.js?rnd=<%=DateTime.Now.ToString("yyyyMMddhhmmss") %>" type="text/javascript"></script>
<script type="text/javascript">
    document.onkeydown = function(e){     
        if(!e) e = window.event;//火狐中是 window.evente.which||w.keyCode 
        if((e.keyCode || e.which) == 13){
            doLogin();
        }
    }
    
    
</script>

<div class="login_main">
	<div class="login_pannel">
    	<div class="l"></div>
        <div class="c">
        	<h2></h2>
            <div class="content">
                <div style="overflow:hidden;">
           		<div id="error_div" class="error_div"></div>
            	<div id="content_div">
                    <div class="rl c1">用户名：</div>
                    <div class="rr c1"><input type="text" id="txtloginName" css="searchBox" MaxLength="20" value="<%=strUserName %>" /></div>
                    <div class="rl c2">密　码：</div>

                    <div class="rr c2"><input type="password" id="txtPassword" css="searchBox" MaxLength="20" value="<%=strPassword %>" />
                    </div>
                </div>
                </div>
            	<div class="rl c3"></div>
                <div class="rr c3"><div class="login_input"><a id="a_login" href="javascript:void(0);"  title=""></a></div></div>
            </div>
        </div>
        <div class="r"></div>
	</div>

</div>
