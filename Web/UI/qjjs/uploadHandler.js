//已用空间
var sizeUsed = 0;//fr DB 单位（MB）
//剩余空间
var sizeRemained = 0;//fr DB  单位（MB）
//今日最大尺寸
var sizeMaxToday=100.0;//暂定 测试 用 --> 30000000   单位（MB）
//当天上传的图片的总数量 对应sizeMaxToday   需要满足 sizeToday < sizeMaxToday
var sizeToday=0;//fr DB  单位（MB）
//绑定分类//fr DB

var sizeBatch=0;//每次批量上传的总尺寸 单位（B）

var sizeBatchLoaded=0;//本批已经上传的图片的总大小 单位（B）


var isStopUpload = 0;//是否停止上传
var newFileID = "";//用来记住每次批量传图时的图片编号

var errorImgs = "";//最终的错误信息

var auth_id="";



//文件初始化
$(function(){	  
	              
	                sizeUsed = parseFloat($("#hidden_sizeUsed").val());
	                sizeRemained = parseFloat($("#hidden_sizeRemained").val());
	                sizeToday = parseFloat($("#hidden_tUploaded").val());	                
	                auth_id = $("#hidden_auth_id").val();		                
           
	                $("#span_sizeUsed").html( sizeUsed.toString());
	                $("#span_sizeAll").html( (sizeUsed+sizeRemained).toString());
	                var percentAll = Math.floor((sizeUsed/(sizeUsed+sizeRemained))*100);	              
	                $("#em_sizePercent").attr("style","width:"+percentAll.toString()+"%");
	                $("#span_sizeToday").html( sizeToday.toString());
	                $("#span_sizeTodayMax").html( sizeMaxToday.toString());
	                var percentToday = Math.floor((sizeToday/sizeMaxToday)*100);	              
	                $("#em_sizePercentToday").attr("style","width:"+percentToday.toString()+"%");

	          
})
//文件选择结束事件
function fileDialogComplete(numFilesSelected, numFilesQueued) {
try {
        
        var obj = swfu.getStats();   //得到对象
        var files_queuedCount = obj.files_queued;            //当前队列中的图片数量
        var files_queuedCountLimited = this.customSettings.file_upload_limit_count;//预设的一次最大可传图片数量            
        var blCancelUploadForNumLimit = false;//是否因为图片数量过多而禁止再加新图片到队列
        var blCancelUploadForSizeLimit = false;//是否因为图片尺寸过多而禁止再加新图片到队列
      
        var sizeTodayTemp = sizeToday*1048576+sizeBatch;//临时变量 判断将要上传的图片是否超过 当天最大限制  为已经上传的尺寸 + 还没有上传的图片尺寸 
    
       if(files_queuedCount > files_queuedCountLimited)//判断数量是否已超过限制
       {     
            blCancelUploadForNumLimit = true;
       }
        
          var count =  GetSWFCount();
       
         for(var i = 0; i< count  ; i++) 			
         {  
            var fileObj = swfu.getFile(i); 
         
            if( fileObj.filestatus == -1 && (!fileList.$(fileObj.id)))    //并且table文件中没有此文件
               {      
                   if(!blCancelUploadForNumLimit && !blCancelUploadForSizeLimit)//如果不超过数量限制
                   {                               
                        if(sizeTodayTemp+fileObj.size > sizeMaxToday*1048576)//判断 如果加上此文件 是不是超尺寸
                        {
                            blCancelUploadForSizeLimit = true;
                            this.cancelUpload(fileObj.id);                            
                        }  
//                        else if(fileObj.size< this.customSettings.file_upload_size_min)    //判断文件是不是 受限（最小尺寸）
//                        {
//                            alert("文件"+fileObj.name+"小于30K！");
//                            this.cancelUpload(fileObj.id);     
//                        }  
                        else if(fileObj.name.length>25)    //判断文件是不是 受限（最小尺寸）
                        {
                            var nameLen =  GetLength (fileObj.name);                       
                            if(nameLen > 50)
                            {
                                alert("["+fileObj.name+"]文件名超长！请不要超过25个汉字，或50个英文字符长度。");
                                this.cancelUpload(fileObj.id);     
                            }
                        }  
                        else//正常情况
                        {                          
                          
                            sizeBatch = sizeBatch+fileObj.size; 
                            sizeTodayTemp = sizeTodayTemp+fileObj.size;   
                            fileList.run(fileObj.id,fileObj.name,fileObj.size); 
                            fileList.button('11');    
                        }                           
                   }
                   else                       
                   {
                        this.cancelUpload(fileObj.id);
                   }
               }   
                 
    	}    
    	if(blCancelUploadForNumLimit)      alert("您选择的文件数量超过限制，每次最多可上传20张！");      
    	if(blCancelUploadForSizeLimit)      alert("您选择的文件尺寸总大小不能超过当天100M限制！");        	

	} catch (ex) {
		this.debug(ex);
	}
}
//文件加载错误事件
function fileQueueError(file, errorCode, message) {
	try {		
		var errorName = "";
		if (errorCode === SWFUpload.errorCode_QUEUE_LIMIT_EXCEEDED) {
			errorName = "上传的文件数量超过限制，每次最多可上传十张！";
		}

		if (errorName !== "") {
			alert(errorName);
			return;
		}

		switch (errorCode) {
		case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:			
			alert("文件["+file.name+"]为空！");
			break;
		case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:		
			alert("文件["+file.name+"]超过最大尺寸限制，文件最大可传50M！");
			break;
		case SWFUpload.QUEUE_ERROR.QUEUE_LIMIT_EXCEEDED:
		    alert("文件选择的数量超过限制，每次最多可上传十张！");
		    break;
		case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
		    alert("文件["+file.name+"]类型不符合！");
		    break;
		default:
			alert(message);
			break;
		}

	} catch (ex) {
		this.debug(ex);
	}
}

function GetSWFCount()
{
 var obj = swfu.getStats();   //得到对象
 var count =  obj.files_queued+obj.successful_uploads  + obj.upload_errors +obj.upload_cancelled +obj.queue_errors ;//得到队列中的总数量
 return count;
}


//批量上传
function UploadPic()
{
    //重置按钮和变量值
    fileList.trButton();//单个移除按钮
    isStopUpload = 0;
    fileList.button('00');
    swfu.setButtonDisabled(true);
    swfu.addPostParam("auth_id",auth_id);
    swfu.addPostParam("CatalogID",document.getElementById('CatalogList').value);

    //判断文件是否有重复
    //得到当前要上传的文件名
   var count =  GetSWFCount();
    var fileNameStr = "";
   for(var i = 0; i< count  ; i++) 			
   {
       var ob = swfu.getFile(i);            
       if( ob.filestatus == -1 )    //并且table文件中没有此文件
       {          
            if(fileNameStr.indexOf(encodeURIComponent(ob.name))>-1)//首先判断当前的文件里是否有重复
            {
                alert("文件"+encodeURIComponent(ob.name)+"重复，请检查要上传的文件，避免重复上传！");
                fileList.button('11');
                swfu.setButtonDisabled(false);
                fileList.trButton();
                return false;
            }
            else
            {
                 fileNameStr += encodeURIComponent(ob.name)+"|";
            }
       }     
   }
   if(fileNameStr!="")
    fileNameStr = fileNameStr.substring(0,fileNameStr.length-1);		
	fileList.allFiles(1); 

	//如果重复 --》 返回重复的图片名 及配对的上传过的新图片名 提供给客户比对 0|aaa:aa;bbb:bb
    //如果不重复 --》返回新的图片编号1|00285 存储在客户端全局变量（可供下次传图片，如果停止 或重新开始 需要重新执行判断重复的方法） 并开始上传
	 $.ajax({
	       type: "GET",
	       url: "/Upload/UploadInfo.ashx",
	       data: "Oper=CheckDUP&fileNameStr="+fileNameStr+"&timeStamp="+new Date().getTime(),	
	       success: function(msg){	
	           var msgCode  = msg.substring(0,1);
	           var msgInfo  = msg.substring(2);//文件名|文件名

	           if(msgCode == "0")//重复
	           {	               
    	            fileList.button('11');
                    swfu.setButtonDisabled(false);
                    fileList.trButton(); 	               
	                fileList.allFiles(1,msgInfo.replace(/\;/g,"|")); 
	                return false;
	           }
	           else//没有重复
	           {
	                newFileID = msgInfo;//给全局变量赋值（新的ID）	 
	                errorImgs = "";
	                UploadPerPic();      //开始上传
	           } 
	       },	           	          
           error:function(){
            alert( "检查文件是否重复时遇到问题，请稍后再试!") ; 
            fileList.allFiles(5);             
           }
	     });

}

//单张上传
function UploadPerPic()
{
    swfu.addPostParam("newFileId", newFileID)        
    fileList.button('01');//设置按钮状态 
    
    swfu.startUpload();//开始上传
    SetNewFileID(); 
}
//计算下一张图片的新编号
function SetNewFileID()
{
    var newFileIDTemp = newFileID;
    while(newFileIDTemp.substring(0,1) == "0")
    {
    newFileIDTemp = newFileIDTemp.substring(1);
    }
    newFileIDTemp = '0000000000'+(parseInt(newFileIDTemp)+1).toString();
    newFileID = newFileIDTemp.substring(newFileIDTemp.length-newFileID.length);
}
//停止上传
function stopFileUpLoad()
{
    fileList.trButton();
    swfu.stopUpload();
    //重置size
    sizeBatch=sizeBatch - sizeBatchLoaded;
    sizeBatchLoaded=0;
 
    isStopUpload = 1;
    //设置按钮状态
    swfu.setButtonDisabled(false);  
    fileList.button('11');   
    fileList.allFiles(6);	
}
//移除队列中的某张
function removeFile(file_id)
{ 
    var ob = swfu.getFile(file_id);      
    sizeBatch = sizeBatch-ob.size;
    swfu.cancelUpload(file_id);  

     var objStats = swfu.getStats();   
   
    //如果图片列表中已经没有图片了      
    if( document.getElementById("upFile").getElementsByTagName("li").length == 1)
    {
        fileList.button('00');  
       
    }
    else if(objStats.files_queued > 0 )
    {
        fileList.button('11');
    }
    else
    {
        fileList.button('10');
    }
     fileList.allFiles(5); 
}
//清空列表
function removeFiles()
{
   sizeBatch=0; 
   var count = GetSWFCount()  ;       
   for(var i = 0; i< count  ; i++) 			
   {
       var ob = swfu.getFile(i);            
       if( ob.filestatus == -1)    //并且table文件中没有此文件
       {                
            swfu.cancelUpload(ob.id);        
       }     
   }
   fileList.button('00');  
   fileList.allFiles(5); 
}

function uploadProgress(file, bytesLoaded) {
	try {  	
		var percentCurrentPic = Math.ceil((bytesLoaded / file.size) * 100);
		var percentAll = Math.ceil(((bytesLoaded +sizeBatchLoaded) / sizeBatch) * 100);
		
		//设置单张图片进度
		fileList.up(file.id,percentCurrentPic);	
		fileList.allFiles(2,file.name+"|"+percentAll+"||"); 		
		
		//单张图片上传完毕
		if (percentCurrentPic == 100) {			
		    fileList.button('00');			   
			fileList.allFiles(3,file.name);					
		} 
		
	} catch (ex) {
		this.debug(ex);
	}
}

//先成功 后完成
function uploadSuccess(file, serverData) {
	try {
		    if(serverData != "1")//不成功 		 
		    {	
		      alert("文件["+file.name+"]处理错误。\r\n错误信息："+serverData);	        		      
    			//设置
		        errorImgs+=file.name+"|";
		        //更新图片的状态		      
		        fileList.trButton(file.id,"error");		        
		    }	
		    else//成功
		    {		   
		        sizeUsed += file.size/1048576;                 
                sizeRemained = sizeRemained - file.size/1048576;
                sizeToday += file.size/1048576;                                
                
                   $("#span_sizeUsed").html( sizeUsed.toString().replace(/(^.+\..{2})(.+)/g,"$1"));	 
                     var percentAll = Math.floor((sizeUsed/(sizeUsed+sizeRemained))*100);	              
	                $("#em_sizePercent").attr("style","width:"+percentAll.toString()+"%");             
	                $("#span_sizeToday").html(sizeToday.toString().replace(/(^.+\..{2})(.+)/g,"$1"));
	                 var percentToday = Math.floor((sizeToday/sizeMaxToday)*100);	              
	                $("#em_sizePercentToday").attr("style","width:"+percentToday.toString()+"%");       
	                
		        fileList.trButton(file.id,"end");		      	     
		    }	
		      sizeBatchLoaded = sizeBatchLoaded+ file.size;			  
		
	} catch (ex) {
		this.debug(ex);
	}
}
//先成功 后完成 
function uploadComplete(file) {
	try {    
		if (this.getStats().files_queued > 0) {
		    //未停止 继续上传
		    if( isStopUpload == 0)
			{
			    UploadPerPic();
			}
			else//停止上传了
			{
			    //设置按钮状态
			    swfu.setButtonDisabled(false);
			    fileList.button('11');
			}
		}		
		else//全部完成
		{		   
		    //设置按钮状态
		    swfu.setButtonDisabled(false);
		    fileList.button('10');
		    //重置尺寸
		    sizeBatch=0;
		    sizeBatchLoaded=0;		   
		     
		    //如果包含错误信息
		    if(errorImgs!="")
		    {		      	        
		        fileList.allFiles(4,errorImgs.substring(0,errorImgs.length-1));			       
		    }   
		    else		    
		    {
	            //进度条及提示		      
	            fileList.allFiles(4);          
		    }                 
		}
	} catch (ex) {
		this.debug(ex);
	}
}

function uploadError(file, errorCode, message) {
	var progress;
	try {
		switch (errorCode) {
		case SWFUpload.UPLOAD_ERROR.FILE_CANCELLED:			 
			break;
		case SWFUpload.UPLOAD_ERROR.UPLOAD_STOPPED:			
            break;			
		case SWFUpload.UPLOAD_ERROR.UPLOAD_LIMIT_EXCEEDED:				
			break;
		default:		
			break;
		}
	} catch (ex3) {
		this.debug(ex3);
	}
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
}