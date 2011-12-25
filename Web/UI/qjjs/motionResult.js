
var size ="";
var ParamsIncludePageIndex = "";
var Params= "";
var KeywordLastSearch="";
var Brand="";
//var RMRFLast = "2"


var LastSearchContent="";
var SearchData = "";
var SortFlag ="";
var PageSizeNew = "20";
var CheckValues="2||||";


var title = "Quanjing";


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
//        while(strkeyword.indexOf("_") > -1)
//        strkeyword = strkeyword.replace("_", ""); 
 
        //parent.document.title=strkeyword+" | "+ title;
      
       
        //bind the keyword 
        if(!isSpecialCharacter(strkeyword.toLowerCase()))
        {          
           $("#txt_Keyword").val( strkeyword );  
           $("#txt_Keyword2").val( strkeyword ); 
        }   

        
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
        if(PageSizeNew == null || PageSizeNew == "") PageSizeNew = "20";   
       //  selectPage.newData(PageSizeNew);         
        strQuery =  ResetlinkParameter(strQuery,PageSizeNew,7);
        while(strQuery.indexOf("_") > -1)
        strQuery = strQuery.replace("_", "");
        Params = strQuery;      
      
        //save the search data to the variable.
        SearchData = "q="+encodeURI(strQuery)+"&Fr=1&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag;  
     

        AjaxSubmit();
    } 
    else
    {
        Params = "";
        KeywordLastSearch = "";   
       
        $("#divImgHolder").html("<p style=\"margin:20px auto auto 20px; font-size:14px;\">please input keyword to search moives. ( keywords recommended: <a href=\"/motionresult.aspx?q=hand||1|60|1|2||||\">hand</a> , <a href=\"/motionresult.aspx?q=hand||1|60|1|2||||\">baby</a> )</p>");   
    }    
});

    

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
   
    SearchData = "q="+encodeURI(Params)+"&Fr=1&CEFlag="+CEFlag +"&size="+size+ "&sortFlag="+SortFlag;  
    
     parent.document.title=key+" | "+ title;

    AjaxSubmitSpecial(key);
}    

function similarSearch(pic_id)
{          
//    var param = pic_id+"||1|"+PageSizeNew+"|1|" + CheckValues;  
    var param = pic_id+"||1|"+PageSizeNew+"|1|2||||";  
    //window.open("/FrameSet.aspx?SearchType=6&q="+param+"&CEFlag="+CEFlag,"_blank");     
    window.open("/SimilarMotion.aspx?q="+param+"&CEFlag="+CEFlag,"_blank");     
 } 

function Reload()
{     
    var param = ParamsIncludePageIndex;
    if (param == "")      param = Params;  
    
    SearchData ="q="+encodeURI(param)+"&Fr=1&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag;  
     AjaxSubmit();
}  
  

function SearchImg(pageIndex){
    var keyword = $("#txt_Keyword").val();
    keyword =  $.trim(keyword);   
     while(keyword.indexOf('*')>-1)  keyword = keyword.replace(/\*/g,""); 
    if(pageIndex ==0 &&  keyword == "")
    {
        alert("please input  keyword");
        $("#txt_Keyword").focus();
        return false;
    } 
    if(pageIndex >0)
    {
      $("#divImgHolder").html("<div style=\"margin-left:25px; margin-top:10px;\"><p><img src=\"/image/frameSet/loading.gif\" alt=\"please wait... ...\" /></p></div>");      
    
      ParamsIncludePageIndex = ResetlinkParameter(Params,pageIndex,6);           
      SearchData = "q="+encodeURI(ParamsIncludePageIndex)+"&Fr=1&CEFlag=" + CEFlag +"&size="+size+"&sortFlag="+SortFlag;  
      AjaxSubmit();
    }
    else
    {   
    ParamsIncludePageIndex="";
        
        var keyLength = GetLength(keyword);  
        if(keyLength > 1500)
        {
            alert("sorry for too longer keyword ,please input again");    
            return false;
        }
       
       var newOrResult = "1";     
       var pageParams="|"+PageSizeNew+"|1|";       
       
        
       //here find which radio is checked 
       if(document.getElementById('search_new').checked)
       {       
            newOrResult = "1";
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
            SortFlag="1"
            size="";
            selectSize.newData(""); 
            imporTab.newData("1");           
            viewTabs.newData("");
                     
       }
       else
       {       
            newOrResult = "0";
       }         
        parent.document.title=keyword+" | "+ title;
       
     
         while(keyword.indexOf('||')>-1)
        {
            keyword = keyword.replace(/\|\|/g,"|");        
        }
        keyword = keyword.replace(/\|/g," or ");      
        
       

       
         var KeywordLastSearchTemp =  KeywordLastSearch;
        if(newOrResult=="0")
        {                
             KeywordLastSearch=KeywordLastSearch+"*"+keyword; 
        }
        else
        {
             KeywordLastSearch = keyword; 
        }
        Params = keyword+"|"+KeywordLastSearchTemp+"|" + newOrResult + pageParams +CheckValues;	             
       
     
        SearchData = "q="+encodeURI(Params)+"&Fr=1&CEFlag="+CEFlag+"&size="+size+"&sortFlag="+SortFlag;    
    
        $("#txt_Keyword2").val( keyword);            
	    AjaxSubmit();
    }  
}


function SearchImgByTxt(currentPage,pageCount)
{ 
  var pattern = /^\d+$/;
 if(currentPage.search(pattern)!=0)
 {
      alert("page index must be a number");  
      return false;
 }
 else
 {
     if(currentPage>pageCount)
     {
        alert( "page number must be from 1 to "+pageCount)
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
                   url: "/Handler/GetMotions.ashx",               
                   data:SearchData,
                   cache: false,
                   success: function(msg){	  
             
                   var msgArr = msg.split('%$@'); 
                
                  
                        if(msgArr[0] == "0")//NRF
                        {                            
                            $("#divImgHolder").html('');                           
                        }
                        else{  
                                 
                            $("#divImgHolder").html(msgArr[2]);
                        }
                         $("#imgCount").html(msgArr[0]);                      
                         $("#searchResultTips").html(msgArr[1]);     
                    },	           	          
                   error:function(){	                    
                       alert("there is an error occurred , please try again");	           
                   }                   
             });
}



function AjaxSubmitSpecial(key)
{
 $.ajax({
       type: "GET",
       url: "/Handler/GetMotions.ashx",     
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
         alert("there is an error occurred , please try again");	           
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
        return window.event;
    } 
    func = getEvent.caller;
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
    if(Key == "37"){
        $(".pager a").eq(0).click();        
    }
    else if(Key == "39"){
        $(".pager a").eq(1).click();      
    }
      document.documentElement.scrollTop=0;    
}


 function GetLength (str) {      
    var realLength = 0, len = str.length, charCode = -1;    
    for (var i = 0; i < len; i++) {    
        charCode = str.charCodeAt(i);    
        if (charCode >= 0 && charCode <= 128) realLength += 1;    
        else realLength += 2;    
    }    
    return realLength;    
};




