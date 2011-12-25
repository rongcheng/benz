var packId = "";
var PageSizeNew="60";
var PageIndexNew = "1";
var SearchData = "";


$(function() {
    var   url   =   String(window.document.location);     
    packId  =  GetParamValue(url,"packId");  
     
     PageSizeNew =  GetCookie("onlineNewPageSize");
     if(PageSizeNew == ""||PageSizeNew == null)  
     {
        PageSizeNew="60";
     }  	 
     SearchData = "packId="+packId+ "&pageSize="+ PageSizeNew, 
      AjaxSubmit();    
});
//相似搜索
function similarSearch(pic_id)
{ 
    var param = pic_id+"||1|20|1|||||";
    window.open("/FrameSet.aspx?SearchType=6&q="+param+"&CEFlag=1","_blank");     
 }   
//重新加载页面
function Reload()
{         
   SearchData = "packId="+packId+ "&pageSize=" + PageSizeNew+ "&pageIndex="+ PageIndexNew ; 
    AjaxSubmit();    
}

///////////////////////////////////////////////////////////////////////////
function SearchImg(pageIndex){
 $("#divImgHolder").html("<div style=\"margin-left:25px; margin-top:10px;\"><p><img src=\"/image/frameSet/loading.gif\" alt=\"正在检索图片 请稍候... ...\" /></p></div>");      
    if(pageIndex >0)
    { 
        PageIndexNew = pageIndex;        
        SearchData ="packId="+packId+ "&pageIndex="+ PageIndexNew + "&pageSize=" + PageSizeNew;        
         //提交AJAX请求
        AjaxSubmit();
    }    
}


 function SearchImgByTxt(currentPage,pageCount)
 {  
    var reg = new RegExp("^[0-9]+$");//
    if(!reg.test(currentPage))
    {
        alert("请输入页数进行翻页！");
        return false;
    }
     if(currentPage>pageCount || currentPage <= 0)
     {
        alert( "页面必须介于1到"+pageCount+"之间")
     }
     else
     {
         if(currentPage == 0)
          {
            currentPage = 1;
          }
         SearchImg(currentPage)
     }	
}
function ResetPageCount(pageSizeNewValue)
{
    if( PageSizeNew = ""||PageSizeNew == null||PageSizeNew != pageSizeNewValue)
    { 
        //给Cookie赋新值
        PageSizeNew = pageSizeNewValue.toString();
        SetCookie("onlineNewPageSize",PageSizeNew);
        //刷新页面时，取出搜索串的值     
        Params = ResetlinkParameter(Params,PageSizeNew,7);          
        SearchData ="packId="+packId+ "&pageIndex=1&pageSize=" + PageSizeNew;
        AjaxSubmit();
    }    
}

function AjaxSubmit()
{
 $.ajax({
       type: "GET",
       url: "/Production/ProductionImages.ashx",
       data:SearchData,
       cache:false,
       success: function(msg){
           var msgArr = msg.split('@'); 
                   
                        if(msgArr[0] == "0")//NRF
                        {                             
                            //下边占位隐藏
                            $("#divImgHolder").html('');                           
                        }
                        else
                        {
                              $("#divImgHolder").html(msgArr[2]);                             
                        }
                         $("#imgCount").html(msgArr[0]);
                         $("#searchResultTips").html(msgArr[1]);     
                    },	           	          
       error:function(){
        alert("您访问的页面出现问题，请稍候再试");
       }
 });
}
