var Params = "||1|20|1|2||||";
var ParamsIncludePageIndex="";
var PageSizeNew="100";
var SearchData = "";
var size="";
var SortFlag="1";


$(function(){
 //bind sort
        SortFlag =  GetCookie("onlineNewSortFlag");       
        if(SortFlag!=null&&SortFlag!="")      
            imporTab.newData(SortFlag);     
        else      
            imporTab.newData(1);        
            
      PageSizeNew =  GetCookie("onlineNewPageSize");    
      if(PageSizeNew == null || PageSizeNew == "") PageSizeNew = "100";            
      Params =  ResetlinkParameter(Params,PageSizeNew,7);
      selectPage.newData(PageSizeNew);
      SearchData = "q="+Params+"&Fr=5&CEFlag="+CEFlag+"&size=&sortFlag="+SortFlag;
      AjaxSubmit();   

});  

function Reload()
{    
     var param = ParamsIncludePageIndex;
  if (param == null || param == "")    
        param = Params;       
         SearchData = "q="+param+"&Fr=5&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag;
      
         AjaxSubmit();    
}

//Reset result (page size | pic size | RMRF | Sort)
function ResetPageCount(pageSizeNewValue)
{
    if( PageSizeNew = ""||PageSizeNew == null||PageSizeNew != pageSizeNewValue)
    {   PageSizeNew = pageSizeNewValue.toString();
        SetCookie("onlineNewPageSize",PageSizeNew);
       
        Params = ResetlinkParameter(Params,PageSizeNew,7);          
        SearchData = "q="+Params+"&Fr=5&CEFlag="+CEFlag+"&size="+size +"&sortFlag="+SortFlag;
        AjaxSubmit();
    }    
}
function ResetSize(sizeNewValue)
{
    if(size!=sizeNewValue)
    {
        size = sizeNewValue;
        SearchData = "q="+Params+"&Fr=5&CEFlag="+CEFlag+"&sortFlag="+SortFlag +"&size="+size;
        AjaxSubmit();
    }
}
function ResetRMRF(rmrfValueNew)
{        
  Params = ResetlinkParameter(Params,rmrfValueNew,5);              
    SearchData ="q="+Params+"&Fr=5&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag ; 
  
    AjaxSubmit();
}
function ResetSort(sortValueNew)
{        
    SortFlag =sortValueNew;
     SetCookie("onlineNewSortFlag",SortFlag);    
    SearchData ="q="+Params+"&Fr=5&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag ;  
    AjaxSubmit();
}
function ResetHvsp(hvspValueNew)
{
 Params = ResetlinkParameter(Params,hvspValueNew,2);          
    SearchData ="q="+Params+"&Fr=5&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag ;
    AjaxSubmit();
}



function SearchImg(pageIndex){
    $("#divImgHolder").html("<div style=\"margin-left:25px; margin-top:10px;\"><p><img src=\"/image/frameSet/loading.gif\" alt=\"图片加载中　请稍候.. ...\" /></p></div>");      
    if(pageIndex >0)
    { 
        ParamsIncludePageIndex = ResetlinkParameter(Params,pageIndex,6);
        SearchData ="q="+ParamsIncludePageIndex+"&Fr=5&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag ;        
     
        AjaxSubmit();
    }   
}


function SearchImgByTxt(currentPage,pageCount)
{

    var reg = new RegExp("^[0-9]+$");//
    if(!reg.test(currentPage))
    {
        alert("页码必须是数字");
        return false;
    }
     if(currentPage>pageCount || currentPage <= 0)
     {
        alert( "页码必须在０和"+pageCount+"之间")
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

function similarSearch(pic_id)
{    
     var param = pic_id+"||1|60|1|2||||";  
    window.open("/FrameSet.aspx?SearchType=6&q="+encodeURI(param)+"&CEFlag="+CEFlag,"_blank");
   
}

function AjaxSubmit()
{
 $.ajax({
       type: "GET",
       url: "/Handler/GetImage.ashx",
       data:SearchData,
       cache:false,
       success: function(msg){     
           var msgArr = msg.split('%$@'); 
                   
                        if(msgArr[0] == "0")//NRF
                        {  
                            $("#divImgHolder").html('');                           
                        }
                        else
                        { 
                              $("#divImgHolder").html(msgArr[2]);     
                              ShowTooltip();    
                               FavFunction();                                  
                        }
                         $("#imgCount").html(msgArr[0]);
                         $("#searchResultTips").html(msgArr[1]);     
                    },	           	          
       error:function(){
        alert("您访问的页面出现问题，请稍后再试！");
       }
 });
}
//tooltip预览图
function ShowTooltip()
{
$(".img img").tooltip({
      track: true,
      delay: 1000,
      showURL: false,
      bodyHandler: function() {
      return $("<img/>").attr("src", this.lowsrc);
      }
      });
 
 }  