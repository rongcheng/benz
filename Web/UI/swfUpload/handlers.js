//已用空间
//var sizeUsed = 0;//fr DB 单位（MB）
//剩余空间
//var sizeRemained = 0;//fr DB  单位（MB）
//今日最大尺寸
//var sizeMaxToday=30.0;//暂定 测试 用 --> 30000000   单位（MB）
//当天上传的图片的总数量 对应sizeMaxToday   需要满足 sizeToday < sizeMaxToday
//var sizeToday=0;//fr DB  单位（MB）
//绑定分类//fr DB

var sizeBatch=0;//每次批量上传的总尺寸 单位（B）

var sizeBatchLoaded=0;//本批已经上传的图片的总大小 单位（B）


var isStopUpload = 0;//是否停止上传
//var newFileID = "";//用来记住每次批量传图时的图片编号

var errorImgs = "";//最终的错误信息

//var auth_id="";


function setSelectFileText(strFile)
{
    document.getElementById("selectedFile").value=strFile;

}

function myDoUpload()
{

//    if (typeof(Page_ClientValidate) == 'function') {
//        if(Page_ClientValidate(''))
//        {        }
//    }
    	       
           var _checkboxCount = $("#uploadCategory input:checked").length;
           
           var _catVal=$("#ctl00_ContentPlaceHolder1_hidCatIds").val();
           //if (_checkboxCount == 0)
           if(_catVal.length<5)
           {
                alert("没有选择分类！");                
           }
   
           else 
           { 
                //document.getElementById("selectFileMessage").style.display="none";
                try {    
		            //swfu.startUpload();	
		            return true;            		 	
	            } catch (ex) {
	                alert(ex);
	                document.getElementById("ctl00_ContentPlaceHolder1_btnUpload").disabled=false;
	                return false;
	            }  
            }        
 
    
    return false;
}



function myPreLoad() {
	if (!this.support.loading) {
		alert("需要flash9以上的版本.");
		return false;
	}
}
function loadFailed() {
	alert("初始化错误，请重试，如仍有错误，请与管理员联系。");
}

function fileQueued(file) {
	try {
		setSelectFileText(file.name);
		document.getElementById("selectFileMessage").style.display="none";
		document.getElementById("ctl00_ContentPlaceHolder1_btnUpload").disabled=false;	
		
		//alert(document.getElementById("upFile").innerHTML);
		//document.getElementById("divFileProgressContainer").innerHTML=document.getElementById("divFileProgressContainer").innerHTML+file.name+"<br />";
	} catch (e) {
	}

}


function fileQueueError(file, errorCode, message) {

	try {
		
		switch (errorCode) {
		case SWFUpload.QUEUE_ERROR.QUEUE_LIMIT_EXCEEDED:
			//alert("You have attempted to queue too many files.\n" + (message === 0 ? "You have reached the upload limit." : "You may select " + (message > 1 ? "up to " + message + " files." : "one file.")));
			alert("您只能选择一个文件。");
			return;
		case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
			alert("您选择的文件太大。");
			this.debug("Error Code: File too big, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
			return;
		case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
			alert("您选择的文件没有任何内容，请重新选择一个。");
			this.debug("Error Code: Zero byte file, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
			return;
		case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
			alert("您选择的文件类型不被支持。");
			this.debug("Error Code: Invalid File Type, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
			return;
		default:
			alert("发生错误，请稍候重试。");
			this.debug("Error Code: " + errorCode + ", File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
			return;
		}
	} catch (e) {
	}



}

function initPage()
{
    //alert("hello"+$("#upFile .lt").css("display"));
    $("#upFile .lt").css("display","none");
    //alert();
    //fileList.button('11'); 
    
}

function fileDialogStart() {
	//setSelectFileText("");
	//this.cancelUpload();
}

function fileDialogComplete(numFilesSelected, numFilesQueued) {
//	try {
//		if (numFilesQueued > 0) {
//			//alert("you selected a file");
//			//this.startUpload();
//			//setSelectFileText();
//			//divFileProgressContainer
//			//document.getElementById("divFileProgressContainer").innerHTML="sssss";
//		}
//	} catch (ex) {
//		this.debug(ex);
//	}



try {
        
        var obj = swfu.getStats();   //得到对象
        var files_queuedCount = obj.files_queued;            //当前队列中的图片数量
        var files_queuedCountLimited = this.customSettings.file_upload_limit_count;//预设的一次最大可传图片数量            
        var blCancelUploadForNumLimit = false;//是否因为图片数量过多而禁止再加新图片到队列
        var blCancelUploadForSizeLimit = false;//是否因为图片尺寸过多而禁止再加新图片到队列
      
       // var sizeTodayTemp = sizeToday*1048576+sizeBatch;//临时变量 判断将要上传的图片是否超过 当天最大限制  为已经上传的尺寸 + 还没有上传的图片尺寸 
    
//       if(files_queuedCount > files_queuedCountLimited)//判断数量是否已超过限制
//       {     
//            blCancelUploadForNumLimit = true;
//       }
//        
          var count =  GetSWFCount();
          //alert(count);
       
         for(var i = 0; i< count  ; i++) 			
         {  
            var fileObj = swfu.getFile(i); 
         
            if( fileObj.filestatus == -1 && (!fileList.$(fileObj.id)))    //并且table文件中没有此文件
               {      
//                   if(!blCancelUploadForNumLimit && !blCancelUploadForSizeLimit)//如果不超过数量限制
//                   {                               
//                        if(sizeTodayTemp+fileObj.size > sizeMaxToday*1048576)//判断 如果加上此文件 是不是超尺寸
//                        {
//                            blCancelUploadForSizeLimit = true;
//                            this.cancelUpload(fileObj.id);                            
//                        }  
//                        else if(fileObj.size< this.customSettings.file_upload_size_min)    //判断文件是不是 受限（最小尺寸）
//                        {
//                            alert("文件"+fileObj.name+"小于30K！");
//                            this.cancelUpload(fileObj.id);     
//                        }  
//                        else if(fileObj.name.length>28)    //判断文件是不是 受限（最小尺寸）
//                        {
//                            var nameLen =  GetLength (fileObj.name);                       
//                            if(nameLen > 53)
//                            {
//                                alert("["+fileObj.name+"]文件名超长！请不要超过25个汉字，或50个英文字符长度。");
//                                this.cancelUpload(fileObj.id);     
//                            }
//                        }  
//                        else//正常情况
//                        {                          
                          
                            sizeBatch = sizeBatch+fileObj.size; 
                            //sizeTodayTemp = sizeTodayTemp+fileObj.size;  
                            //alert(fileObj.id+":"+fileObj.name+":"+fileObj.size);
                            fileList.run(fileObj.id,fileObj.name,fileObj.size); 
                            fileList.button('11');     
                            
//                        }                           
//                   }
//                   else                       
//                   {
//                        this.cancelUpload(fileObj.id);
//                   }
               }   
               
                 
    	}    
//    	if(blCancelUploadForNumLimit)      alert("您选择的文件数量超过限制，每次最多可上传十张！");      
//    	if(blCancelUploadForSizeLimit)      alert("您选择的文件尺寸总大小不能超过当天30M限制！");  


     $("#upFile .lt").css("display","block");
     document.getElementById("upFile").style.display="block";   
     document.getElementById("delList").style.display="inline-block";    
     //alert("gggggggggggggggggg");	

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


function uploadProgress1(file, bytesLoaded) {
    //alert("go hear");
	try {
		var percent = Math.ceil((bytesLoaded / file.size) * 100);

		var progress = new FileProgress(file,  this.customSettings.upload_target);
		progress.setProgress(percent);
		if (percent === 100) {
			//progress.setStatus(file.name+"上传成功");
			progress.setStatus(file.name+"上传中......  "+percent+"%");
			progress.toggleCancel(false, this);
		} else {
			progress.setStatus(file.name+"上传中......  "+percent+"%");
			progress.toggleCancel(true, this);
		}
	} catch (ex) {
		this.debug(ex);
	}
}

function uploadSuccess1(file, serverData) {
	try {
		//addImage("thumbnail.aspx?id=" + serverData);
        //alert(serverData);
        document.getElementById("uploadFileName").value=serverData;
		var progress = new FileProgress(file,  this.customSettings.upload_target);

		//progress.setStatus("Thumbnail Created.");
		progress.toggleCancel(false);


	} catch (ex) {
		this.debug(ex);
	}
}

function uploadComplete1(file) {
	try {
		/*  I want the next upload to continue automatically so I'll call startUpload here */
		if (this.getStats().files_queued > 0) {
			this.startUpload();
		} else {
			var progress = new FileProgress(file,  this.customSettings.upload_target);
			progress.setComplete();
			progress.setStatus("文件上传成功，正在写入数据库");
			progress.toggleCancel(false);
			
			
			//调用
		    if (typeof(Page_ClientValidate) == 'function') Page_ClientValidate('');
		     __doPostBack('ctl00$ContentPlaceHolder1$btnUpload','');
    		 
    		 
		}
		
		

	} catch (ex) {
		this.debug(ex);
	}
}

function uploadError1(file, errorCode, message) {
	try {
		
		if (errorCode === SWFUpload.UPLOAD_ERROR.FILE_CANCELLED) {
			// Don't show cancelled error boxes
			return;
		}
		
//		var txtFileName = document.getElementById("txtFileName");
//		txtFileName.value = "";
//		validateForm();
        setSelectFileText("")
		
		// Handle this error separately because we don't want to create a FileProgress element for it.
		switch (errorCode) {
		case SWFUpload.UPLOAD_ERROR.MISSING_UPLOAD_URL:
			alert("There was a configuration error.  You will not be able to upload a resume at this time.");
			this.debug("Error Code: No backend file, File name: " + file.name + ", Message: " + message);
			return;
		case SWFUpload.UPLOAD_ERROR.UPLOAD_LIMIT_EXCEEDED:
			alert("You may only upload 1 file.");
			this.debug("Error Code: Upload Limit Exceeded, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
			return;
		case SWFUpload.UPLOAD_ERROR.FILE_CANCELLED:
		case SWFUpload.UPLOAD_ERROR.UPLOAD_STOPPED:
			break;
		default:
			alert("An error occurred in the upload. Try again later.");
			this.debug("Error Code: " + errorCode + ", File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
			return;
		}

		file.id = "singlefile";	// This makes it so FileProgress only makes a single UI element, instead of one for each file
		var progress = new FileProgress(file, this.customSettings.progress_target);
		progress.setError();
		progress.toggleCancel(false);

		switch (errorCode) {
		case SWFUpload.UPLOAD_ERROR.HTTP_ERROR:
			progress.setStatus("Upload Error");
			this.debug("Error Code: HTTP Error, File name: " + file.name + ", Message: " + message);
			break;
		case SWFUpload.UPLOAD_ERROR.UPLOAD_FAILED:
			progress.setStatus("Upload Failed.");
			this.debug("Error Code: Upload Failed, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
			break;
		case SWFUpload.UPLOAD_ERROR.IO_ERROR:
			progress.setStatus("Server (IO) Error");
			this.debug("Error Code: IO Error, File name: " + file.name + ", Message: " + message);
			break;
		case SWFUpload.UPLOAD_ERROR.SECURITY_ERROR:
			progress.setStatus("Security Error");
			this.debug("Error Code: Security Error, File name: " + file.name + ", Message: " + message);
			break;
		case SWFUpload.UPLOAD_ERROR.FILE_CANCELLED:
			progress.setStatus("Upload Cancelled");
			this.debug("Error Code: Upload Cancelled, File name: " + file.name + ", Message: " + message);
			break;
		case SWFUpload.UPLOAD_ERROR.UPLOAD_STOPPED:
			progress.setStatus("Upload Stopped");
			this.debug("Error Code: Upload Stopped, File name: " + file.name + ", Message: " + message);
			break;
		}
	} catch (ex) {
	}


}


function fadeIn(element, opacity) {
	var reduceOpacityBy = 5;
	var rate = 30;	// 15 fps


	if (opacity < 100) {
		opacity += reduceOpacityBy;
		if (opacity > 100) {
			opacity = 100;
		}

		if (element.filters) {
			try {
				element.filters.item("DXImageTransform.Microsoft.Alpha").opacity = opacity;
			} catch (e) {
				// If it is not set initially, the browser will throw an error.  This will set it if it is not set yet.
				element.style.filter = 'progid:DXImageTransform.Microsoft.Alpha(opacity=' + opacity + ')';
			}
		} else {
			element.style.opacity = opacity / 100;
		}
	}

	if (opacity < 100) {
		setTimeout(function () {
			fadeIn(element, opacity);
		}, rate);
	}
}








//批量上传
function UploadPic()
{     
    if(!myDoUpload())
    {
        return;
    }
    //重置按钮和变量值
    fileList.trButton();//单个移除按钮
    isStopUpload = 0;
    fileList.button('00');
    swfu.setButtonDisabled(true);
    //是否最大尺寸
//    var isMaxSize = document.getElementById("R_max").checked?1:0;
//    swfu.addPostParam("auth_id",auth_id);
//    swfu.addPostParam("isMaxSize",isMaxSize);
//    swfu.addPostParam("GroupID",document.getElementById('GroupList').value);

    //判断文件是否有重复
    //得到当前要上传的文件名
   var count =  GetSWFCount();
    var fileNameStr = "";
   for(var i = 0; i< count  ; i++) 			
   {
       var ob = swfu.getFile(i);            
       if( ob.filestatus == -1 )    //并且table文件中没有此文件
       {          
            if(fileNameStr.indexOf(ob.name)>-1)//首先判断当前的文件里是否有重复
            {
                alert("文件"+ob.name+"重复，请检查要上传的文件，避免重复上传！");
                fileList.button('11');
                swfu.setButtonDisabled(false);
                fileList.trButton();
                return false;
            }
            else
            {
                 fileNameStr += ob.name+"|";
            }
       }     
   }
   if(fileNameStr!="")
    fileNameStr = fileNameStr.substring(0,fileNameStr.length-1);		
	fileList.allFiles(1); 

	//如果重复 --》 返回重复的图片名 及配对的上传过的新图片名 提供给客户比对 0|aaa:aa;bbb:bb
    //如果不重复 --》返回新的图片编号1|00285 存储在客户端全局变量（可供下次传图片，如果停止 或重新开始 需要重新执行判断重复的方法） 并开始上传
	UploadPerPic();
//	 $.ajax({
//	       type: "GET",
//	       url: "/Upload/UploadInfo.ashx",
//	       data: "Oper=CheckDUP&fileNameStr="+fileNameStr+"&timeStamp="+new Date().getTime(),	
//	       success: function(msg){		  	   
//	           var msgCode  = msg.substring(0,1);
//	           var msgInfo  = msg.substring(2);
//	           if(msgCode == "0")//重复
//	           {	               
//    	            fileList.button('11');
//                    swfu.setButtonDisabled(false);
//                    fileList.trButton(); 	               
//	                fileList.allFiles(1,msgInfo.replace(/\;/g,"|")); 
//	                return false;
//	           }
//	           else//没有重复
//	           {
//	                newFileID = msgInfo;//给全局变量赋值（新的ID）	        
//	                errorImgs = "";
//	                UploadPerPic();      //开始上传
//	           } 
//	       },	           	          
//           error:function(){
//            alert( "检查文件是否重复时遇到问题，请稍后再试!") ; 
//            fileList.allFiles(5);             
//           }
//	     });

}

//单张上传
function UploadPerPic()
{           
    //var param = {"uploadType":"cqq1:"};
    //swfu.setPostParam(param);
         
    fileList.button('01');//设置按钮状态     
    swfu.startUpload();//开始上传
}

//edit:2010-06-22
function uploadStart(file)
{
    //var param = {"uploadType1":"cqq1:"};
    //swfu.setPostParams(param); 
    
    
    //swfu.addFileParam(file.id,"SN" , "data");
    
   
    $.get("/Modules/GetSNByResourceType.ashx",{Rnd:Math.floor(Math.random()*10000), Action: "get", fileName: file.name },

              function(data) {
                    //alert("客户端："+data);
                  //var param = {"uploadType1":"cqq1:"+ data};
                  //var param1 = {"uploadType2":"cqq2:"+ data};
                  //swfu.setPostParams(param);
                  
                  //swfu.addPostParam(param);
                  ///swfu.addFileParam(param);
                  //swfu.addFileParam(file.id,"SN" , data);
                  
                  document.getElementById("uploadFileName").value=document.getElementById("uploadFileName").value+data+",";    
    });
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
   //sizeBatch=0; 
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
		
		//设置单个文件的上传进度
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
	//alert("服务器端："+serverData);
		    if(serverData != "1" && false)//不成功 		 
		    {	
		      alert("图片["+file.name+"]处理错误。\r\n错误信息："+serverData);	        		      
    			//设置
		        errorImgs+=file.name+"|";
		        //更新图片的状态		      
		        fileList.trButton(file.id,"error");		        
		    }	
		    else//成功
		    {		   
		        //sizeUsed += file.size/1048576;                 
               // sizeRemained = sizeRemained - file.size/1048576;
                //sizeToday += file.size/1048576;                                
                
                  // $("#span_sizeUsed").html( sizeUsed.toString().replace(/(^.+\..{2})(.+)/g,"$1"));	 
                  //   var percentAll = Math.floor((sizeUsed/(sizeUsed+sizeRemained))*100);	              
	              //  $("#em_sizePercent").attr("style","width:"+percentAll.toString()+"%");             
	              //  $("#span_sizeToday").html(sizeToday.toString().replace(/(^.+\..{2})(.+)/g,"$1"));
	               //  var percentToday = Math.floor((sizeToday/sizeMaxToday)*100);	              
	              //  $("#em_sizePercentToday").attr("style","width:"+percentToday.toString()+"%");       
	              
	            //document.getElementById("uploadFileName").value=document.getElementById("uploadFileName").value+serverData+",";    
		        fileList.trButton(file.id,"end");		      	     
		    }	
		    //alert(document.getElementById("uploadFileName").value  )  
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
		    //alert(document.getElementById("uploadFileName").value  )  
		    
		    //调用
		    if (typeof(Page_ClientValidate) == 'function') Page_ClientValidate('');
		     __doPostBack('ctl00$ContentPlaceHolder1$btnUpload','');
		        
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


/*
	A simple class for displaying file information and progress
	Note: This is a demonstration only and not part of SWFUpload.
	Note: Some have had problems adapting this class in IE7. It may not be suitable for your application.
*/

// Constructor
// file is a SWFUpload file object
// targetID is the HTML element id attribute that the FileProgress HTML structure will be added to.
// Instantiating a new FileProgress object with an existing file will reuse/update the existing DOM elements
function FileProgress(file, targetID) {
	this.fileProgressID = file.id;

	this.opacity = 100;
	this.height = 0;

	this.fileProgressWrapper = document.getElementById(this.fileProgressID);
	if (!this.fileProgressWrapper) {
		this.fileProgressWrapper = document.createElement("div");
		this.fileProgressWrapper.className = "progressWrapper";
		this.fileProgressWrapper.id = this.fileProgressID;

		this.fileProgressElement = document.createElement("div");
		this.fileProgressElement.className = "progressContainer";

		var progressCancel = document.createElement("a");
		progressCancel.className = "progressCancel";
		progressCancel.href = "#";
		progressCancel.style.visibility = "hidden";
		progressCancel.appendChild(document.createTextNode(" "));

		var progressText = document.createElement("div");
		progressText.className = "progressName";
		progressText.appendChild(document.createTextNode(file.name));

		var progressBar = document.createElement("div");
		progressBar.className = "progressBarInProgress";

		var progressStatus = document.createElement("div");
		progressStatus.className = "progressBarStatus";
		progressStatus.innerHTML = "&nbsp;";

		this.fileProgressElement.appendChild(progressCancel);
		this.fileProgressElement.appendChild(progressText);
		this.fileProgressElement.appendChild(progressStatus);
		this.fileProgressElement.appendChild(progressBar);

		this.fileProgressWrapper.appendChild(this.fileProgressElement);

		document.getElementById(targetID).appendChild(this.fileProgressWrapper);
	} else {
		this.fileProgressElement = this.fileProgressWrapper.firstChild;
		this.fileProgressElement.childNodes[1].innerHTML = file.name;
	}

	this.height = this.fileProgressWrapper.offsetHeight;

}
FileProgress.prototype.setProgress = function (percentage) {
	this.fileProgressElement.className = "progressContainer green";
	this.fileProgressElement.childNodes[3].className = "progressBarInProgress";
	this.fileProgressElement.childNodes[3].style.width = percentage + "%";
};
FileProgress.prototype.setComplete = function () {
	this.appear();
	this.fileProgressElement.className = "progressContainer blue";
	this.fileProgressElement.childNodes[3].className = "progressBarComplete";
	this.fileProgressElement.childNodes[3].style.width = "";

	var oSelf = this;
	setTimeout(function () {
		oSelf.disappear();
	}, 10000);
};
FileProgress.prototype.setError = function () {
	this.appear();
	this.fileProgressElement.className = "progressContainer red";
	this.fileProgressElement.childNodes[3].className = "progressBarError";
	this.fileProgressElement.childNodes[3].style.width = "";

	var oSelf = this;
	setTimeout(function () {
		oSelf.disappear();
	}, 5000);
};
FileProgress.prototype.setCancelled = function () {
	this.appear();
	this.fileProgressElement.className = "progressContainer";
	this.fileProgressElement.childNodes[3].className = "progressBarError";
	this.fileProgressElement.childNodes[3].style.width = "";

	var oSelf = this;
	setTimeout(function () {
		oSelf.disappear();
	}, 2000);
};
FileProgress.prototype.setStatus = function (status) {
	this.fileProgressElement.childNodes[2].innerHTML = status;
};

// Show/Hide the cancel button
FileProgress.prototype.toggleCancel = function (show, swfUploadInstance) {
	this.fileProgressElement.childNodes[0].style.visibility = show ? "visible" : "hidden";
	if (swfUploadInstance) {
		var fileID = this.fileProgressID;
		this.fileProgressElement.childNodes[0].onclick = function () {
			swfUploadInstance.cancelUpload(fileID);
			return false;
		};
	}
};

// Makes sure the FileProgress box is visible
FileProgress.prototype.appear = function () {
		if (this.fileProgressWrapper.filters) {
			try {
				this.fileProgressWrapper.filters.item("DXImageTransform.Microsoft.Alpha").opacity = 100;
			} catch (e) {
				// If it is not set initially, the browser will throw an error.  This will set it if it is not set yet.
				this.fileProgressWrapper.style.filter = "progid:DXImageTransform.Microsoft.Alpha(opacity=100)";
			}
		} else {
			this.fileProgressWrapper.style.opacity = 1;
		}
		
		this.fileProgressWrapper.style.height = "";
		this.height = this.fileProgressWrapper.offsetHeight;
		this.opacity = 100;
		this.fileProgressWrapper.style.display = "";

};

// Fades out and clips away the FileProgress box.
FileProgress.prototype.disappear = function () {

	var reduceOpacityBy = 15;
	var reduceHeightBy = 4;
	var rate = 30;	// 15 fps

	if (this.opacity > 0) {
		this.opacity -= reduceOpacityBy;
		if (this.opacity < 0) {
			this.opacity = 0;
		}

		if (this.fileProgressWrapper.filters) {
			try {
				this.fileProgressWrapper.filters.item("DXImageTransform.Microsoft.Alpha").opacity = this.opacity;
			} catch (e) {
				// If it is not set initially, the browser will throw an error.  This will set it if it is not set yet.
				this.fileProgressWrapper.style.filter = "progid:DXImageTransform.Microsoft.Alpha(opacity=" + this.opacity + ")";
			}
		} else {
			this.fileProgressWrapper.style.opacity = this.opacity / 100;
		}
	}

	if (this.height > 0) {
		this.height -= reduceHeightBy;
		if (this.height < 0) {
			this.height = 0;
		}

		this.fileProgressWrapper.style.height = this.height + "px";
	}

	if (this.height > 0 || this.opacity > 0) {
		var oSelf = this;
		setTimeout(function () {
			oSelf.disappear();
		}, rate);
	} else {
		this.fileProgressWrapper.style.display = "none";
	}
};