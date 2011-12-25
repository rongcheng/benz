var Params = "||1|20|1|2||||";
var ParamsIncludePageIndex="";
var PageSizeNew="100";
var SearchData = "";
var size="";
var SortFlag="1";
var photographer="";
var phType="";


$(function(){
  var url   =   String(window.document.location); 
    url = decodeURI(url);
      photographer = GetParamValue(url,"photographer");    
      phType = GetParamValue(url,"phType");    
 //bind sort
        SortFlag =  GetCookie("onlineNewSortFlag");  
             
        if(SortFlag!=null&&SortFlag!="")      
            imporTab.newData(SortFlag);     
        else      
            imporTab.newData("1");        
            
    if(CEFlag=="2"){       
        $("#type_RM").attr("checked","checked");
        $("#type_RF").attr("checked","checked");        
    }
    else
     rmrfTabs.newData("2"); 
            
   PageSizeNew =  GetCookie("onlineNewPageSize");    
   if(PageSizeNew == null || PageSizeNew == "") PageSizeNew = "100"; 
    selectPage.newData(PageSizeNew);
 Params =  ResetlinkParameter(Params,PageSizeNew,7); 
  SearchData = "q="+encodeURI(Params)+"&Fr=6&CEFlag="+CEFlag+"&size=&sortFlag="+SortFlag+"&photographer="+photographer+"&phType="+phType;
 //提交AJAX请求
         AjaxSubmit();   
});  
//重新加载页面
function Reload()
{    
     var param = ParamsIncludePageIndex;
  if (param == null || param == "")    
        param = Params;       
      //提交AJAX请求
       SearchData = "q="+encodeURI(param)+"&Fr=6&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag+"&photographer="+photographer+"&phType="+phType;
      
         AjaxSubmit();    
}

//Reset result (page size | pic size | RMRF | Sort)
function ResetPageCount(pageSizeNewValue)
{
    if( PageSizeNew = ""||PageSizeNew == null||PageSizeNew != pageSizeNewValue)
    { 
        //给Cookie赋新值
        PageSizeNew = pageSizeNewValue.toString();
        SetCookie("onlineNewPageSize",PageSizeNew);
        //刷新页面时，取出搜索串的值     
        Params = ResetlinkParameter(Params,PageSizeNew,7);          
        SearchData = "q="+encodeURI(Params)+"&Fr=6&CEFlag="+CEFlag+"&size="+size +"&sortFlag="+SortFlag+"&photographer="+photographer+"&phType="+phType;
        AjaxSubmit();
    }    
}
function ResetSize(sizeNewValue)
{
    if(size!=sizeNewValue)
    {
        size = sizeNewValue;
        SearchData = "q="+encodeURI(Params)+"&Fr=6&CEFlag="+CEFlag+"&sortFlag="+SortFlag +"&size="+size+"&photographer="+photographer+"&phType="+phType;
        AjaxSubmit();
    }
}
function ResetRMRF(rmrfValueNew)
{        
  Params = ResetlinkParameter(Params,rmrfValueNew,5);              
    SearchData ="q="+encodeURI(Params)+"&Fr=6&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag +"&photographer="+photographer+"&phType="+phType; 
  
    AjaxSubmit();
}
function ResetSort(sortValueNew)
{        
    SortFlag =sortValueNew;
     SetCookie("onlineNewSortFlag",SortFlag);    
    SearchData ="q="+encodeURI(Params)+"&Fr=6&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag +"&photographer="+photographer+"&phType="+phType;
    AjaxSubmit();
}
function ResetHvsp(hvspValueNew)
{
 Params = ResetlinkParameter(Params,hvspValueNew,2);          
    SearchData ="q="+encodeURI(Params)+"&Fr=6&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag +"&photographer="+photographer+"&phType="+phType;
    AjaxSubmit();
}


//翻页事件
function SearchImg(pageIndex){
    $("#divImgHolder").html("<div style=\"margin-left:25px; margin-top:10px;\"><p><img src=\"/image/frameSet/loading.gif\" alt=\"正在检索图片 请稍候... ...\" /></p></div>");      
    if(pageIndex >0)
    { 
        ParamsIncludePageIndex = ResetlinkParameter(Params,pageIndex,6);
    SearchData ="q="+encodeURI(ParamsIncludePageIndex)+"&Fr=6&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag +"&photographer="+photographer+"&phType="+phType;
        
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
//相似搜索
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
        alert("您访问的页面出现问题，请稍候再试");
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