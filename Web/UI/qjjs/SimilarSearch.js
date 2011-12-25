var ParamsIncludePageIndex = "";
var Params= "";
var PageSizeNew = "100";
var Pic_id = "";
var CheckValues="2||||";
var SearchData = "";
var size="";
var SortFlag="1";

$(function(){
    var url   =   String(window.document.location); 
    url = decodeURI(url);
    var   strQuery = GetParamValue(url,"q");    
    if(strQuery  !="")
    { 
        var paramArr=[];
        paramArr = strQuery.split('|');         
        Pic_id = paramArr[0];       

       
        //bind size     
        size =   GetParamValue(url,"size");      
        selectSize.newData(size);    
        
        //bind sort
        SortFlag =  GetCookie("onlineNewSortFlag");       
        if(SortFlag!=null&&SortFlag!="")      
            imporTab.newData(SortFlag);     
        else      
            imporTab.newData(1);         
    
        //bind RMRF
        var RMRF = paramArr[5];       
        if(CEFlag=="2"){
            if(RMRF == "0")
            {
                $("#type_RM").attr("checked","checked");
                $("#type_RF").attr("checked","");
            }
            else if(RMRF == "1") 
            {
                $("#type_RM").attr("checked","");
                $("#type_RF").attr("checked","checked"); 
            }
            else if(RMRF == "2") 
            {
                $("#type_RM").attr("checked","checked");
                $("#type_RF").attr("checked","checked"); 
            }
        }
        else
         rmrfTabs.newData(RMRF);
        
        //bind the HVSP
        var hvsp = paramArr[8];
        viewTabs.newData(hvsp);   
        
         //get CheckValues
        CheckValues = paramArr[5]+"|"+ paramArr[6]+"|"+paramArr[7]+"|"+paramArr[8]+"|"+paramArr[9];   
	
         Params=strQuery; 
         
          PageSizeNew =  GetCookie("onlineNewPageSize");    
        if(PageSizeNew == null || PageSizeNew == "") PageSizeNew = "100";   
         selectPage.newData(PageSizeNew);       
      Params =  ResetlinkParameter(Params,PageSizeNew,7);
        SearchData = "q="+encodeURI(Params)+"&Fr=4&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag;
      
         
         //提交AJAX请求
         AjaxSubmit();       
     }
     else
     {
       Params = "";
     }	
 });


//相似搜索
function similarSearch(pic_id)
{    
     var param = pic_id+"||1|"+PageSizeNew+"|1|" + CheckValues;  
    window.open("/FrameSet.aspx?SearchType=6&q="+encodeURI(param)+"&CEFlag="+CEFlag,"_blank");
   
}
//登录后重新加载
function Reload()
{   
    var param = ParamsIncludePageIndex; 
    if (param == null || param == "")    
        param = Params;       
      //提交AJAX请求
       SearchData = "q="+encodeURI(param)+"&Fr=4&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag;
      
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
        SearchData = "q="+encodeURI(Params)+"&Fr=4&CEFlag="+CEFlag+"&size="+size +"&sortFlag="+SortFlag;
        AjaxSubmit();
    }    
}
function ResetSize(sizeNewValue)
{
    if(size!=sizeNewValue)
    {
        size = sizeNewValue;
        SearchData = "q="+encodeURI(Params)+"&Fr=4&CEFlag="+CEFlag+"&sortFlag="+SortFlag +"&size="+size;
        AjaxSubmit();
    }
}
function ResetRMRF(rmrfValueNew)
{        
  Params = ResetlinkParameter(Params,rmrfValueNew,5);     
    SearchData ="q="+encodeURI(Params)+"&Fr=4&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag ;  
    AjaxSubmit();
}
function ResetSort(sortValueNew)
{        
    SortFlag =sortValueNew;
     SetCookie("onlineNewSortFlag",SortFlag);    
    SearchData ="q="+encodeURI(Params)+"&Fr=4&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag ;  
    AjaxSubmit();
}
function ResetHvsp(hvspValueNew)
{
 Params = ResetlinkParameter(Params,hvspValueNew,2);          
    SearchData ="q="+encodeURI(Params)+"&Fr=4&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag ;
    AjaxSubmit();
}




//$(function(){    
//    $("#btn_viewFlash").click(function(){    
//       SetCookie("onlineNewFlashParameter",Params);
//       SetCookie("onlineNewFlashParameterRmrfV","");
//       SetCookie("onlineNewFlashParameterSize",""); 
//       SetCookie("onlineNewFlashParameterPicId",Pic_id); 
//        
//        if (document.cookie.indexOf("LoginStatus=IsLogin") == -1)
//        {
//             $("#hidden_transferPage").val('2');
//	        $("#user_login").attr({style:"left: 50%; top: 100px;  z-index:999;margin-left:-240px;"})
//            $("#user_login").show(); 
//		     $("#user_login input").eq(0).focus();
//		     $("#user_login input").eq(0).val('');
//		     $("#user_login input").eq(1).val('');
//              return false;
//        }
//        else
//        {       
//            SetCookie("onlineNewFlashParameterCEFlag",CEFlag);	        
//            window.open("/Flash.aspx","_blank");
//        }        							
//	});
//})
//翻页事件
function SearchImg(pageIndex){
    $("#divImgHolder").html("<div style=\"margin-left:25px; margin-top:10px;\"><p><img src=\"/image/frameSet/loading.gif\" alt=\"正在检索图片 请稍候... ...\" /></p></div>");      
    if(pageIndex >0)
    { 
        ParamsIncludePageIndex = ResetlinkParameter(Params,pageIndex,6);
    SearchData ="q="+encodeURI(ParamsIncludePageIndex)+"&Fr=4&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag ; 
        
         //提交AJAX请求
        AjaxSubmit();
    }   
}
//输入框翻页
 function SearchImgByTxt(currentPage,pageCount)
 {  
     var pattern = /^\d+$/;
     if(currentPage.search(pattern)!=0)
     {
         alert("页数必须为整数！");  
          return false;
     }
     else
     {
         if(currentPage>pageCount)
         {
           alert( "页面必须介于1到"+pageCount+"之间");
         }
         else
         {
            if(currentPage==0) 
            {
                currentPage = 1;
            }
            SearchImg(currentPage)
         } 
     }
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