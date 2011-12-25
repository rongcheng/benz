
//滚动 1
var isScroll = "0";//标志是否是滚动
var ScrollPageIndex = 1;//当前滚动的页码
var TotalPageCount=0;//滚动状态下总的页数
var ScrollPageSize = 30;//每次滚动事件得到的图片数量
var ScrollPageSizeMultiple=6;
var scrollAddData = 1;//滚动状态下 图片是否已加载完成
var scrollRemainHeight=1200;
var scrollIndex=1;//滚动状态下 插入临时代替图片时 用到的计数器



//滚动
//切换
$("#btn_ScrollShow").click(function(){
    if($("#btn_ScrollShow").hasClass("pageWindow"))// 滚屏 --> 翻页
    {      
        isScroll = "0";
        //重置页图片数      
         $("#pageImg").attr("style","position:static;top:0;");
         selectPage.newData(PageSizeNew); 
        
        $("#btn_ScrollShow").removeClass("pageWindow");
        $("#btn_ScrollShow").addClass("scrollWindow");
        $("#btn_ScrollShow").text("滚屏显示");          
        SearchData ="q="+encodeURI(Params)+"&Fr=1&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag+"&isScroll="+isScroll;
        qj.removeEvent(window,"onscroll",ScrollHandler); 
       
    }
    else  if($("#btn_ScrollShow").hasClass("scrollWindow"))//  翻页--> 滚屏
    {        
        isScroll = "1";
        scrollIndex=1;
         ScrollPageIndex=ScrollPageSizeMultiple;//为滚屏提交数据的初始页码 如第一次加载180张 首次滚屏为第6+1张
        $("#pageImg").attr("style","position:absolute;top:-999em;left:0");       
        $("#btn_ScrollShow").removeClass("scrollWindow");
        $("#btn_ScrollShow").addClass("pageWindow");   
        $("#btn_ScrollShow").text("翻页显示");                            
        SearchData ="q="+encodeURI(Params)+"&Fr=1&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag+"&isScroll="+isScroll;         
        qj.addEvent(window,"onscroll",ScrollHandler);        
    }
     AjaxSubmit();
})

//滚屏事件
var ScrollHandler = function(){
    var pageH = pageHeight();//页面总高度
    var scrY = scrollY();//卷去的高度
    var winHeight = windowHeight();//页面可用高度
    var last = pageH-scrY-winHeight;//未展示的剩余高度   
 
    
    if(last<scrollRemainHeight && scrollAddData == 1)
    {
        scrollAddData=0;       
        addScrollData();
    }
    else
    {
        return false;
    }
}
//滚屏
function addScrollData(){
  
     ScrollPageIndex++; 
    if(ScrollPageIndex > TotalPageCount) return false;
     //还需要得到共有多少页
        SearchData ="q="+encodeURI(ResetlinkParameter(Params,ScrollPageIndex,6) )+"&Fr=1&ScrollPageIndex="+ScrollPageIndex+"&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag+"&isScroll="+isScroll;       
        scrollIndex++;
        AjaxSubmitScroll(scrollIndex);

}
//提交AJAX请求 滚屏
function AjaxSubmitScroll(scrollIndex)
{   
    var tempHtml = GetTempHtml(scrollIndex);  
    $("#ulImgHolder").append(tempHtml); 
    scrollAddData = 1 ;
    $.ajax({
               type: "GET",
               url: "/Handler/GetImageScroll.ashx",               
               data:SearchData,
               cache: false,
               success: function(msg){
               $("#temp"+scrollIndex).before(msg); 
               $("#ulImgHolder li").remove(".tempClass"+scrollIndex);
                ShowTooltip();
                FavFunction();
                },	           	          
               error:function(){	                    
                   alert("您访问的页面出现问题，请稍候再试");	           
               }                   
         });
}
function GetTempHtml(scrollIndex)
{
    var tempHtml="<li id=\"temp"+scrollIndex+"\"  class=\"tempClass"+scrollIndex+"\"><span class=\"img\"><a href=\"javascript:void(0);\"><img src=\"/image/frameSet/wait.gif\"></a></span><span class=\"id\"></span></li>";
    var tempHtmlSingle="<li class=\"tempClass"+scrollIndex+"\"><span class=\"img\"><a href=\"javascript:void(0);\"><img src=\"/image/frameSet/wait.gif\"></a></span><span class=\"id\"></span></li>";
    
    for(var i=1;i<ScrollPageSize;i++)
    {
    tempHtml+=tempHtmlSingle;
    }
    return tempHtml;
}


//页面高度
function pageHeight()
{
var de =  document.documentElement;
 return (de && de.scrollHeight) || document.body.scrollHeight;
//return document.body.scrollHeight;
}
//获取浏览器垂直滚动位置
function scrollY()    
{
var de =  document.documentElement;
return self.pageYOffset||(de && de.scrollTop) || document.body.scrollTop;
}
//获取视口的高度
function windowHeight()
{
 var de =  document.documentElement;
 return self.innerHeight||(de && de.clientHeight)||document.body.clientHeight;
}
