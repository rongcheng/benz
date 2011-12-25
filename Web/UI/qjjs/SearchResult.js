//在此定义全局变量
var size ="";
var ParamsIncludePageIndex = "";
var Params= "";
var KeywordLastSearch="";
var Brand="";
//var RMRFLast = "2"
var LastSearchContent="";
var SearchData = "";
var SortFlag ="";
var PageSizeNew = "100";
var CheckValues="2||||";


var title = "全景网 图片库 图片网站 创意图片 广告图片";

//page ready handler
$(function(){
    var url   =   String(window.document.location);
    url = decodeURI(url);   
    var   strQuery = GetParamValue(url,"q");          
    if(strQuery  !="")
    {
        var paramArr=[];
        paramArr = strQuery.split('|');         

        //get keyword
        var   strkeyword   =   paramArr[0]; 
        while(strkeyword.indexOf("_") > -1)
        strkeyword = strkeyword.replace("_", ""); 
 
        parent.document.title=strkeyword+" | "+ title;
      
       
        //bind the keyword 
        if(!isSpecialCharacter(strkeyword.toLowerCase()))
        {          
           $("#txt_Keyword").val( strkeyword );  
           $("#txt_Keyword2").val( strkeyword ); 
        }      
      
        //bind size     
        size =   GetParamValue(url,"size");        
        selectSize.newData(size);    
        
        //bind sort
        SortFlag =  GetCookie("onlineNewSortFlag");       
        if(SortFlag!=null&&SortFlag!="")      
            imporTab.newData(SortFlag);     
        else      
        {
            SortFlag = ""
            imporTab.newData(1);         
        }        
      
        
         //bind sort
        var BackgroundFlag =  GetCookie("onlineNewBackgroundFlag");          
        if(BackgroundFlag=="1")   {          
            selectBG.newData(BackgroundFlag);    
           $("#cssSearchResult1").attr("href","/css/commonBlack.css");
		    $("#cssSearchResult2").attr("href","/css/frameSetBlack.css");
		    $("#header h1 img").attr("src","/image/common/logoBlack.gif");
		    }
        else      
        {
            BackgroundFlag = "0"
            selectBG.newData(BackgroundFlag);         
        }
        
    
        //bind RMRF
        var RMRF = paramArr[5];
        //RMRFLast = RMRF; 
              
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
        
        //get Brand
        Brand = paramArr[6]; 
        //get CheckValues
        CheckValues = paramArr[5]+"|"+Brand+"|"+paramArr[7]+"|"+paramArr[8]+"|"+paramArr[9];         
              
        //get the value of new search or in-result search ,for the page may be transferred from current page of the other channel after login.
        var newOrResult =  paramArr[2].toString();
        if(newOrResult == "0")
        KeywordLastSearch = paramArr[1].toString()+"*"+strkeyword;
        else
        KeywordLastSearch = strkeyword;  
    
        //reset PageSize  | have bound on the page.
        PageSizeNew =  GetCookie("onlineNewPageSize");    
        if(PageSizeNew == null || PageSizeNew == "") PageSizeNew = "100";   
         selectPage.newData(PageSizeNew);         
        strQuery =  ResetlinkParameter(strQuery,PageSizeNew,7);
        while(strQuery.indexOf("_") > -1)
        strQuery = strQuery.replace("_", "");
        Params = strQuery;      
      
        //save the search data to the variable.
        SearchData = "q="+encodeURI(strQuery)+"&Fr=1&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag+"&isScroll="+isScroll;  
     
        //提交AJAX请求
        AjaxSubmit();
    } 
    else
    {
        Params = "";
        KeywordLastSearch = "";      
    }    
});


$(function(){ 
    //图片属性中查询，四列显示，每对互斥关系搜索，其他可交叉  
     $(".sidebar_top div:first a").click(function(){
     var keyword = $(this).text(); 
     RelatedSearch(keyword,1);
     })
     //人物中查询，唯一搜索，全部互斥
    $(".sidebar_top div:eq(1) a").click(function(){
         var keyword = $(this).text(); 
         RelatedSearch(keyword,2);
     })
     //图片分类中查询 不互斥
    $(".sidebar_top div:eq(2) a").click(function(){
         var keyword = $(this).text(); 
         RelatedSearch(keyword,0);
    })  
  })  
  //
//相关搜索
function RelatedSearch(keyword,flag)  
{   
    //判断KeywordLastSearch里时候有Keyword的互斥词
    //有的话就替换，没有的话就为最新的搜索关键字
    var paramKeywordLast ="";
    if(flag == 0)
    {
        paramKeywordLast = KeywordLastSearch;
    }
    else
    {
        paramKeywordLast = KeywordReplace(flag,keyword,KeywordLastSearch);
    }  
    var keywordNew = "";  
    Params = keyword + "|" + paramKeywordLast + "|0|"+PageSizeNew+"|1|" + CheckValues;     
     //保存搜索值
    KeywordLastSearch = paramKeywordLast+"*"+keyword;         
    SearchData = "q="+encodeURI(Params)+"&Fr=1&CEFlag="+CEFlag+"&sortFlag="+SortFlag +"&size="+size+"&isScroll="+isScroll;  
    
    parent.document.title=keyword+" | "+ title;
    
    AjaxSubmitSpecial(keyword);         
}   
  //过滤关键字 for 相关搜索 
  function KeywordReplace(flag,keyword,lastSearchStr)
  {
      var newSearchStr = "";
      var SearchStrNow = lastSearchStr+"*";
      if(flag == 1)//两两互斥
      {
        if(keyword == "户内" || keyword == "户外" )              
        {
            newSearchStr = SearchStrNow.replace("*户内*","*").replace("*户外*","*");
        }
        else  if(keyword == "无人" || keyword == "静物" )              
        {
            newSearchStr = SearchStrNow.replace("*无人*","*").replace("*静物*","*");
        }
        else  if(keyword == "白天" || keyword == "晚上" )              
        {
            newSearchStr = SearchStrNow.replace("*白天*","*").replace("*晚上*","*");
        }
        else  if(keyword == "特写" || keyword == "聚焦" )              
        {
            newSearchStr = SearchStrNow.replace("*特写*","*").replace("*聚焦*","*");
        }
        else  if(keyword == "正面" || keyword == "侧面" )              
        {
            newSearchStr = SearchStrNow.replace("*正面*","*").replace("*侧面*","*");
        }
        else  if(keyword == "仰视" || keyword == "俯视" )              
        {
            newSearchStr = SearchStrNow.replace("*仰视*","*").replace("*俯视*","*");
        }   
        else
        {
            newSearchStr = SearchStrNow;        
        }    
      }
      else//全部互斥
      {
            newSearchStr = SearchStrNow.replace("*女孩*","*").replace("*男人*","*").replace("*女人*","*").replace("*老人*","*").replace("*仅一人*","*").replace("*仅男人*","*").replace("*儿童*","*").replace("*成年女人*","*").replace("*东方人物*","*").replace("*西方人物*","*").replace("*成年男人*","*").replace("*仅年轻女性*","*").replace("*仅女人*","*");
      }    
      return newSearchStr.substr(0,newSearchStr.length-1);//过滤过的上次搜索值， 不包含新点关键字  
  }
    
//历史搜索  
function HistorySearch(key,lkey)
{   
     if(lkey=="")
     {      
        KeywordLastSearch = key; 
        Params= key + "||1|"+PageSizeNew+"|1|" + CheckValues;    
     }
     else
     {
        KeywordLastSearch = lkey+"*"+key; 
        Params= key + "|"+lkey+"|0|"+PageSizeNew+"|1|" + CheckValues; 
     }    
   
    SearchData = "q="+encodeURI(Params)+"&Fr=1&CEFlag="+CEFlag +"&size="+size+ "&sortFlag="+SortFlag+"&isScroll="+isScroll;  
    
     parent.document.title=key+" | "+ title;
   //提交AJAX请求
    AjaxSubmitSpecial(key);
}    
//相似搜索
function similarSearch(pic_id)
{          
//    var param = pic_id+"||1|"+PageSizeNew+"|1|" + CheckValues;  
    var param = pic_id+"||1|"+PageSizeNew+"|1|2||||";  
    window.open("/FrameSet.aspx?SearchType=6&q="+param+"&CEFlag="+CEFlag,"_blank");     
 } 

//登录后重新载入
function Reload()
{     
    var param = ParamsIncludePageIndex;
    if (param == "")      param = Params;  
    
    SearchData ="q="+encodeURI(param)+"&Fr=1&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag+"&isScroll="+isScroll;  
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
        SearchData = "q="+encodeURI(Params)+"&Fr=1&CEFlag="+CEFlag+"&size="+size +"&sortFlag="+SortFlag+"&isScroll="+isScroll;  
        AjaxSubmit();
    }    
}
function ResetSize(sizeNewValue)
{
    if(size!=sizeNewValue)
    {
        size = sizeNewValue;
        SearchData = "q="+encodeURI(Params)+"&Fr=1&CEFlag="+CEFlag+"&sortFlag="+SortFlag +"&size="+size+"&isScroll="+isScroll;  
        AjaxSubmit();
    }
}
function ResetRMRF(rmrfValueNew)
{   
//RMRFLast = rmrfValueNew;
  Params = ResetlinkParameter(Params,rmrfValueNew,5);    
   CheckValues =  ResetlinkParameter(CheckValues,rmrfValueNew,5);          
    SearchData ="q="+encodeURI(Params)+"&Fr=1&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag+"&isScroll="+isScroll;  
    AjaxSubmit();
}
function ResetBG(bgValue)
{   
	if(bgValue=="0"){
		$("#cssSearchResult1").attr("href","/css/common.css");
		$("#cssSearchResult2").attr("href","/css/frameSet.css");
		$("#header h1 img").attr("src","/image/common/logo.gif");
	}
	else{
		$("#cssSearchResult1").attr("href","/css/commonBlack.css");
		$("#cssSearchResult2").attr("href","/css/frameSetBlack.css");
		$("#header h1 img").attr("src","/image/common/logoBlack.gif");
	}	
	SetCookie("onlineNewBackgroundFlag",bgValue);
}
function ResetSort(sortValueNew)
{        
    SortFlag =sortValueNew;
     SetCookie("onlineNewSortFlag",SortFlag);    
    SearchData ="q="+encodeURI(Params)+"&Fr=1&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag +"&isScroll="+isScroll;  
    AjaxSubmit();
}
function ResetHvsp(hvspValueNew)
{
 Params = ResetlinkParameter(Params,hvspValueNew,2);    
 CheckValues =  ResetlinkParameter(CheckValues,hvspValueNew,2);    
    SearchData ="q="+encodeURI(Params)+"&Fr=1&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag+"&isScroll="+isScroll;  
    AjaxSubmit();
}

$(function(){    
$("#btn_viewFlash").click(function(){ 
    SetCookie("onlineNewFlashParameter",Params);
    SetCookie("onlineNewFlashParameterPicId","");   
    SetCookie("onlineNewFlashParameterSize",size);
 
    SetCookie("onlineNewFlashParameterCEFlag",CEFlag);
    SetCookie("onlineNewFlashParameterSortFlag",SortFlag);
    window.open("/Flash.aspx","_blank");   					
	});
})
  
//搜索
function SearchImg(pageIndex){
    var keyword = $("#txt_Keyword").val();
    keyword =  $.trim(keyword);   
     while(keyword.indexOf('*')>-1)  keyword = keyword.replace(/\*/g,""); 
    if(pageIndex ==0 &&  keyword == "")
    {
        alert('请输入关键字！');
        $("#txt_Keyword").focus();
        return false;
    } 
    if(pageIndex >0)//翻页事件
    {
      $("#divImgHolder").html("<div style=\"margin-left:25px; margin-top:10px;\"><p><img src=\"/image/frameSet/loading.gif\" alt=\"正在检索图片 请稍候... ...\" /></p></div>");      
    
      ParamsIncludePageIndex = ResetlinkParameter(Params,pageIndex,6);           
      SearchData = "q="+encodeURI(ParamsIncludePageIndex)+"&Fr=1&CEFlag=" + CEFlag +"&size="+size+"&sortFlag="+SortFlag+"&isScroll="+isScroll;  
      AjaxSubmit();
    }
    else//点击搜索按钮事件
    {   
    ParamsIncludePageIndex="";
        
        var keyLength = GetLength(keyword);  
        if(keyLength > 1500)
        {
            alert('对不起，您输入的关键字超过了1500个字符长度，请重新输入！');    
            return false;
        }
       
       var newOrResult = "1";     //1 新搜索 0 在结果中搜索	
       var pageParams="|"+PageSizeNew+"|1|";       
       
        
       //here find which radio is checked 
       if(document.getElementById('search_new').checked)
       {       
            newOrResult = "1";//新搜索     
            if(CEFlag=="2")
            {
                //rmrfTabs.newData("1");
                //CheckValues = "1||||";
                
                if(document.getElementById("type_RM").checked==true && document.getElementById("type_RF").checked==false)
                {
                    CheckValues = "0||||";
                    //rmrfTabs.newData("0");
                }
                else if(document.getElementById("type_RM").checked==false && document.getElementById("type_RF").checked==true)
                {
                    CheckValues = "1||||";
                    //rmrfTabs.newData("1");
                }
                else
                {
                    CheckValues = "2||||";
                    $("#type_RM").attr("checked","checked");
                    $("#type_RF").attr("checked","checked");
                    //rmrfTabs.newData("2");
                }
            }
            else
            {
                rmrfTabs.newData("2");
                CheckValues = "2||||";
            }
            SortFlag="1"//排序重置
            size="";
            selectSize.newData(""); 
            imporTab.newData("1");           
            viewTabs.newData("");
                     
       }
       else
       {       
            newOrResult = "0";//在结果中搜索
       }         
        parent.document.title=keyword+" | "+ title;
       
        //在此处理"|"问题 add on 1015
         while(keyword.indexOf('||')>-1)
        {
            keyword = keyword.replace(/\|\|/g,"|");        
        }
        keyword = keyword.replace(/\|/g," or ");      
        
       

         //保存上次搜索值  
         var KeywordLastSearchTemp =  KeywordLastSearch;
        if(newOrResult=="0")//在结果中搜索
        {                
             KeywordLastSearch=KeywordLastSearch+"*"+keyword; 
        }
        else//新搜索
        {
             KeywordLastSearch = keyword; 
        }
        Params = keyword+"|"+KeywordLastSearchTemp+"|" + newOrResult + pageParams +CheckValues;	             
       
        //保存搜索串    
        SearchData = "q="+encodeURI(Params)+"&Fr=1&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag+"&isScroll="+isScroll;    
    
        $("#txt_Keyword2").val( keyword);            
	    AjaxSubmit();
    }  
}
//下侧搜索
function SearchPicByKeyword(evt){
 if(evt.keyCode==13)
   SearchImg2();  
}
function SearchImg2()
{
    ParamsIncludePageIndex="";
    var keyword = $("#txt_Keyword2").val();
    keyword =  $.trim(keyword);   
     while(keyword.indexOf('*')>-1)  keyword = keyword.replace(/\*/g,""); 
    if( keyword == "")
    {
        alert('请输入关键字！');
        $("#txt_Keyword2").focus();
        return false;
    } 
   
   //开始搜索
    var keyLength = GetLength(keyword);  
    if(keyLength > 1500)
    {
        alert('对不起，您输入的关键字超过了1500个字符长度，请重新输入！');    
        return false;
    }  
    var pageParams="|"+PageSizeNew+"|1|";       
 
 
   if(document.getElementById('search_new_bottom').checked)
   {       
        newOrResult = "1";//新搜索     
        if(CEFlag=="2")
        {
         rmrfTabs.newData("1");
         CheckValues = "1||||";
        }
        else
        {
         rmrfTabs.newData("2");
            CheckValues = "2||||";
        }            
         
        SortFlag="1"//排序重置
        size="";
        selectSize.newData(""); 
        imporTab.newData("1");          
        viewTabs.newData("");
   }
   else
   {       
        newOrResult = "0";//在结果中搜索
   }    
   
    parent.document.title=keyword+" | "+ title;
   
    //在此处理"|"问题 add on 1015
     while(keyword.indexOf('||')>-1)
    {
        keyword = keyword.replace(/\|\|/g,"|");        
    }
    keyword = keyword.replace(/\|/g," or ");      

     //保存上次搜索值  
     var KeywordLastSearchTemp =  KeywordLastSearch;
  
    if(newOrResult=="0")//在结果中搜索
    {                
         KeywordLastSearch=KeywordLastSearch+"*"+keyword; 
    }
    else//新搜索
    {
         KeywordLastSearch = keyword; 
    }     
   
  
    Params = keyword+"|"+KeywordLastSearchTemp+"|" + newOrResult + pageParams +CheckValues;	
        
    //保存搜索串    
    SearchData = "q="+encodeURI(Params)+"&Fr=1&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag+"&isScroll="+isScroll;  
     $("#txt_Keyword").val( keyword+" ");    
    AjaxSubmit();  
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
        alert( "页面必须介于1到"+pageCount+"之间")
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

// //提交AJAX请求
function AjaxSubmit()
{
        $.ajax({
                   type: "GET",
                   url: "/Handler/GetImage.ashx",               
                   data:SearchData,
                   cache: false,
                   success: function(msg){	  
             
                   var msgArr = msg.split('%$@'); 
                   
                        if(msgArr[0] == "0")//NRF
                        {                             
                            //下边占位隐藏                           
                            $("#p_search2").attr({style:"visibility:hidden"});
                            $("#divImgHolder").html('');                           
                        }
                        else
                        {                           
                            $("#p_search2").attr({style:"visibility:visible"});    
                            $("#divImgHolder").html(msgArr[2]);  
                            //滚屏
                            if(isScroll == "1") {TotalPageCount =parseInt((parseInt(msgArr[0])-1)/ScrollPageSize+1); }                           
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


//提交AJAX请求 处理ywxs
function AjaxSubmitSpecial(key)
{
 $.ajax({
       type: "GET",
       url: "/Handler/GetImage.ashx",     
       data:SearchData,     
       cache: false,
       success: function(msg){	      
           if(!isSpecialCharacter(key.toLowerCase())) 
           {             
              var showKey = replaceSpecialCharacter(KeywordLastSearch);
              $("#txt_Keyword").val( showKey.replace(/\*/g," ")+" ");
              $("#txt_Keyword2").val(  showKey.replace(/\*/g," ")+" ");    
           }
           else
           {
            $("#txt_Keyword").val('');
            $("#txt_Keyword2").val('');
           }
          
           var msgArr = msg.split('%$@');  
            if(msgArr[0] == "0")//NRF
            {                             
                //下边占位隐藏               
                $("#p_search2").attr({style:"visibility:hidden"});
                $("#divImgHolder").html('');                           
            }
            else
            { 
                $("#p_search2").attr({style:"visibility:visible"});    
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
function isSpecialCharacter(key)
{
if(key == 'ywxs'|| key == 'zt15'|| key == 'zt13'|| key == 'zt9'|| key == 'zt8')  return true;
else return false;           
}
function replaceSpecialCharacter(key)
{
return key.replace("ywxs*","").replace("zt15*","").replace("zt13*","").replace("zt9*","").replace("zt8*","");      
}

function getEvent() { 
    if (document.all) { 
        return window.event;//如果是ie 
    } 
    func = getEvent.caller;//如果是ff 
    while (func != null) { 
        var arg0 = func.arguments[0]; 
        if (arg0) { 
            if ((arg0.constructor == Event || arg0.constructor == MouseEvent) || (typeof (arg0) == "object" && arg0.preventDefault && arg0.stopPropagation)) { 
                return arg0; 
            } 
        } 
        func = func.caller; 
    } 
    return null; 
} 
function DirKey(){

    if(document.activeElement.tagName != "INPUT"){
        var Key = getEvent().keyCode ? getEvent().keyCode : getEvent().which;
        if(Key == "37" || Key == "39"){       
            DirKeyToCtlPage(Key);
        }
    }
}
function DirKeyToCtlPage(Key){
    if(Key == "37"){//左
        $(".pager a").eq(0).click();        
    }
    else if(Key == "39"){//右
        $(".pager a").eq(1).click();      
    }
      document.documentElement.scrollTop=0;    
}


 function GetLength (str) {    
    ///<summary>获得字符串实际长度，中文2，英文1</summary>    
    ///<param name="str">要获得长度的字符串</param>    
    var realLength = 0, len = str.length, charCode = -1;    
    for (var i = 0; i < len; i++) {    
        charCode = str.charCodeAt(i);    
        if (charCode >= 0 && charCode <= 128) realLength += 1;    
        else realLength += 2;    
    }    
    return realLength;    
};



