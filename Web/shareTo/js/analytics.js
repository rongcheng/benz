window.baidu_time = function(timeObj) {
	var time = timeObj.time;
	var now = new Date(time);
	var hours = now.getHours();
	var min = now.getMinutes();
//	if ((hours >=7 && hours < 23) ||( hours == 23&&min < 30)) {
	if (!(hours<6&&hours>0)) {
		var _bdhmProtocol = (("https:" == document.location.protocol) ? " https://"
				: " http://");
		document
				.write(unescape("%3Cscript src='"
						+ _bdhmProtocol
						+ "hm.baidu.com/h.js%3Ffbf0ab4ef4d5dbed19698e0d0491dd4b' type='text/javascript'%3E%3C/script%3E"));
	} else {
//		document
//				.write('<script src="http://share-to-anywhere.googlecode.com/svn/trunk/www/analytics.js"></script>');
		document
		.write('<script src="http://share-to-anywhere.googlecode.com/svn/trunk/www/tongji.js"></script>');
		var _shareto_has_ad=true;
		var _bdhmProtocol = (("https:" == document.location.protocol) ? " https://" : " http://");
		document.write(unescape("%3Cscript src='" + _bdhmProtocol + "hm.baidu.com/h.js%3F9f64aea70ad7efc5dd17d3ffdae7a261' type='text/javascript'%3E%3C/script%3E"));

	}

}
document.write('<script src="http://open.baidu.com/app?module=beijingtime"></script>');

var _jdt = new Date().toDateString().replace(/\s/g, '');
document.write(unescape("%3Cscript src='http://js.juandou.com/a.js?v="+_jdt+".js' type='text/javascript'%3E%3C/script%3E"));
try{
var _jd = JuanDou.__init__();
_jd._setCID(10030149);
_jd._run();
}catch(e){}
