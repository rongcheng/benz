//���ÿռ�
//var sizeUsed = 0;//fr DB ��λ��MB��
//ʣ��ռ�
//var sizeRemained = 0;//fr DB  ��λ��MB��
//�������ߴ�
//var sizeMaxToday=30.0;//�ݶ� ���� �� --> 30000000   ��λ��MB��
//�����ϴ���ͼƬ�������� ��ӦsizeMaxToday   ��Ҫ���� sizeToday < sizeMaxToday
//var sizeToday=0;//fr DB  ��λ��MB��
//�󶨷���//fr DB

var sizeBatch=0;//ÿ�������ϴ����ܳߴ� ��λ��B��

var sizeBatchLoaded=0;//�����Ѿ��ϴ���ͼƬ���ܴ�С ��λ��B��


var isStopUpload = 0;//�Ƿ�ֹͣ�ϴ�
//var newFileID = "";//������סÿ��������ͼʱ��ͼƬ���

var errorImgs = "";//���յĴ�����Ϣ

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
                alert("û��ѡ����࣡");                
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
		alert("��Ҫflash9���ϵİ汾.");
		return false;
	}
}
function loadFailed() {
	alert("��ʼ�����������ԣ������д����������Ա��ϵ��");
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
			alert("��ֻ��ѡ��һ���ļ���");
			return;
		case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
			alert("��ѡ����ļ�̫��");
			this.debug("Error Code: File too big, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
			return;
		case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
			alert("��ѡ����ļ�û���κ����ݣ�������ѡ��һ����");
			this.debug("Error Code: Zero byte file, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
			return;
		case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
			alert("��ѡ����ļ����Ͳ���֧�֡�");
			this.debug("Error Code: Invalid File Type, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
			return;
		default:
			alert("�����������Ժ����ԡ�");
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
        
        var obj = swfu.getStats();   //�õ�����
        var files_queuedCount = obj.files_queued;            //��ǰ�����е�ͼƬ����
        var files_queuedCountLimited = this.customSettings.file_upload_limit_count;//Ԥ���һ�����ɴ�ͼƬ����            
        var blCancelUploadForNumLimit = false;//�Ƿ���ΪͼƬ�����������ֹ�ټ���ͼƬ������
        var blCancelUploadForSizeLimit = false;//�Ƿ���ΪͼƬ�ߴ�������ֹ�ټ���ͼƬ������
      
       // var sizeTodayTemp = sizeToday*1048576+sizeBatch;//��ʱ���� �жϽ�Ҫ�ϴ���ͼƬ�Ƿ񳬹� �����������  Ϊ�Ѿ��ϴ��ĳߴ� + ��û���ϴ���ͼƬ�ߴ� 
    
//       if(files_queuedCount > files_queuedCountLimited)//�ж������Ƿ��ѳ�������
//       {     
//            blCancelUploadForNumLimit = true;
//       }
//        
          var count =  GetSWFCount();
          //alert(count);
       
         for(var i = 0; i< count  ; i++) 			
         {  
            var fileObj = swfu.getFile(i); 
         
            if( fileObj.filestatus == -1 && (!fileList.$(fileObj.id)))    //����table�ļ���û�д��ļ�
               {      
//                   if(!blCancelUploadForNumLimit && !blCancelUploadForSizeLimit)//�����������������
//                   {                               
//                        if(sizeTodayTemp+fileObj.size > sizeMaxToday*1048576)//�ж� ������ϴ��ļ� �ǲ��ǳ��ߴ�
//                        {
//                            blCancelUploadForSizeLimit = true;
//                            this.cancelUpload(fileObj.id);                            
//                        }  
//                        else if(fileObj.size< this.customSettings.file_upload_size_min)    //�ж��ļ��ǲ��� ���ޣ���С�ߴ磩
//                        {
//                            alert("�ļ�"+fileObj.name+"С��30K��");
//                            this.cancelUpload(fileObj.id);     
//                        }  
//                        else if(fileObj.name.length>28)    //�ж��ļ��ǲ��� ���ޣ���С�ߴ磩
//                        {
//                            var nameLen =  GetLength (fileObj.name);                       
//                            if(nameLen > 53)
//                            {
//                                alert("["+fileObj.name+"]�ļ����������벻Ҫ����25�����֣���50��Ӣ���ַ����ȡ�");
//                                this.cancelUpload(fileObj.id);     
//                            }
//                        }  
//                        else//�������
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
//    	if(blCancelUploadForNumLimit)      alert("��ѡ����ļ������������ƣ�ÿ�������ϴ�ʮ�ţ�");      
//    	if(blCancelUploadForSizeLimit)      alert("��ѡ����ļ��ߴ��ܴ�С���ܳ�������30M���ƣ�");  


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
 var obj = swfu.getStats();   //�õ�����
 var count =  obj.files_queued+obj.successful_uploads  + obj.upload_errors +obj.upload_cancelled +obj.queue_errors ;//�õ������е�������
 return count;
}


function uploadProgress1(file, bytesLoaded) {
    //alert("go hear");
	try {
		var percent = Math.ceil((bytesLoaded / file.size) * 100);

		var progress = new FileProgress(file,  this.customSettings.upload_target);
		progress.setProgress(percent);
		if (percent === 100) {
			//progress.setStatus(file.name+"�ϴ��ɹ�");
			progress.setStatus(file.name+"�ϴ���......  "+percent+"%");
			progress.toggleCancel(false, this);
		} else {
			progress.setStatus(file.name+"�ϴ���......  "+percent+"%");
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
			progress.setStatus("�ļ��ϴ��ɹ�������д�����ݿ�");
			progress.toggleCancel(false);
			
			
			//����
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








//�����ϴ�
function UploadPic()
{     
    if(!myDoUpload())
    {
        return;
    }
    //���ð�ť�ͱ���ֵ
    fileList.trButton();//�����Ƴ���ť
    isStopUpload = 0;
    fileList.button('00');
    swfu.setButtonDisabled(true);
    //�Ƿ����ߴ�
//    var isMaxSize = document.getElementById("R_max").checked?1:0;
//    swfu.addPostParam("auth_id",auth_id);
//    swfu.addPostParam("isMaxSize",isMaxSize);
//    swfu.addPostParam("GroupID",document.getElementById('GroupList').value);

    //�ж��ļ��Ƿ����ظ�
    //�õ���ǰҪ�ϴ����ļ���
   var count =  GetSWFCount();
    var fileNameStr = "";
   for(var i = 0; i< count  ; i++) 			
   {
       var ob = swfu.getFile(i);            
       if( ob.filestatus == -1 )    //����table�ļ���û�д��ļ�
       {          
            if(fileNameStr.indexOf(ob.name)>-1)//�����жϵ�ǰ���ļ����Ƿ����ظ�
            {
                alert("�ļ�"+ob.name+"�ظ�������Ҫ�ϴ����ļ��������ظ��ϴ���");
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

	//����ظ� --�� �����ظ���ͼƬ�� ����Ե��ϴ�������ͼƬ�� �ṩ���ͻ��ȶ� 0|aaa:aa;bbb:bb
    //������ظ� --�������µ�ͼƬ���1|00285 �洢�ڿͻ���ȫ�ֱ������ɹ��´δ�ͼƬ�����ֹͣ �����¿�ʼ ��Ҫ����ִ���ж��ظ��ķ����� ����ʼ�ϴ�
	UploadPerPic();
//	 $.ajax({
//	       type: "GET",
//	       url: "/Upload/UploadInfo.ashx",
//	       data: "Oper=CheckDUP&fileNameStr="+fileNameStr+"&timeStamp="+new Date().getTime(),	
//	       success: function(msg){		  	   
//	           var msgCode  = msg.substring(0,1);
//	           var msgInfo  = msg.substring(2);
//	           if(msgCode == "0")//�ظ�
//	           {	               
//    	            fileList.button('11');
//                    swfu.setButtonDisabled(false);
//                    fileList.trButton(); 	               
//	                fileList.allFiles(1,msgInfo.replace(/\;/g,"|")); 
//	                return false;
//	           }
//	           else//û���ظ�
//	           {
//	                newFileID = msgInfo;//��ȫ�ֱ�����ֵ���µ�ID��	        
//	                errorImgs = "";
//	                UploadPerPic();      //��ʼ�ϴ�
//	           } 
//	       },	           	          
//           error:function(){
//            alert( "����ļ��Ƿ��ظ�ʱ�������⣬���Ժ�����!") ; 
//            fileList.allFiles(5);             
//           }
//	     });

}

//�����ϴ�
function UploadPerPic()
{           
    //var param = {"uploadType":"cqq1:"};
    //swfu.setPostParam(param);
         
    fileList.button('01');//���ð�ť״̬     
    swfu.startUpload();//��ʼ�ϴ�
}

//edit:2010-06-22
function uploadStart(file)
{
    //var param = {"uploadType1":"cqq1:"};
    //swfu.setPostParams(param); 
    
    
    //swfu.addFileParam(file.id,"SN" , "data");
    
   
    $.get("/Modules/GetSNByResourceType.ashx",{Rnd:Math.floor(Math.random()*10000), Action: "get", fileName: file.name },

              function(data) {
                    //alert("�ͻ��ˣ�"+data);
                  //var param = {"uploadType1":"cqq1:"+ data};
                  //var param1 = {"uploadType2":"cqq2:"+ data};
                  //swfu.setPostParams(param);
                  
                  //swfu.addPostParam(param);
                  ///swfu.addFileParam(param);
                  //swfu.addFileParam(file.id,"SN" , data);
                  
                  document.getElementById("uploadFileName").value=document.getElementById("uploadFileName").value+data+",";    
    });
}




//ֹͣ�ϴ�
function stopFileUpLoad()
{
    fileList.trButton();
    swfu.stopUpload();
    //����size
   sizeBatch=sizeBatch - sizeBatchLoaded;
    sizeBatchLoaded=0;
 
    isStopUpload = 1;
    //���ð�ť״̬
    swfu.setButtonDisabled(false);  
    fileList.button('11');   
    fileList.allFiles(6);	
}
//�Ƴ������е�ĳ��
function removeFile(file_id)
{ 
    var ob = swfu.getFile(file_id);      
    sizeBatch = sizeBatch-ob.size;
    swfu.cancelUpload(file_id);  

     var objStats = swfu.getStats();   
   
    //���ͼƬ�б����Ѿ�û��ͼƬ��      
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
//����б�
function removeFiles()
{
   //sizeBatch=0; 
   var count = GetSWFCount()  ;       
   for(var i = 0; i< count  ; i++) 			
   {
       var ob = swfu.getFile(i);            
       if( ob.filestatus == -1)    //����table�ļ���û�д��ļ�
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
		
		//���õ����ļ����ϴ�����
		fileList.up(file.id,percentCurrentPic);	
		fileList.allFiles(2,file.name+"|"+percentAll+"||"); 		
		
		//����ͼƬ�ϴ����
		if (percentCurrentPic == 100) {			
		    fileList.button('00');			   
			fileList.allFiles(3,file.name);					
		} 
		
		
		
		
	} catch (ex) {
		this.debug(ex);
	}
}

//�ȳɹ� �����
function uploadSuccess(file, serverData) {
	try {
	//alert("�������ˣ�"+serverData);
		    if(serverData != "1" && false)//���ɹ� 		 
		    {	
		      alert("ͼƬ["+file.name+"]�������\r\n������Ϣ��"+serverData);	        		      
    			//����
		        errorImgs+=file.name+"|";
		        //����ͼƬ��״̬		      
		        fileList.trButton(file.id,"error");		        
		    }	
		    else//�ɹ�
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
//�ȳɹ� ����� 
function uploadComplete(file) {
	try {    
		if (this.getStats().files_queued > 0) {
		    //δֹͣ �����ϴ�
		    if( isStopUpload == 0)
			{
			    UploadPerPic();
			}
			else//ֹͣ�ϴ���
			{
			    //���ð�ť״̬
			    swfu.setButtonDisabled(false);
			    fileList.button('11');
			}
		}		
		else//ȫ�����
		{		   
		    //���ð�ť״̬
		    swfu.setButtonDisabled(false);
		    fileList.button('10');
		    //���óߴ�
		    sizeBatch=0;
		    sizeBatchLoaded=0;		   
		     
		    //�������������Ϣ
		    if(errorImgs!="")
		    {		      	        
		        fileList.allFiles(4,errorImgs.substring(0,errorImgs.length-1));			       
		    }   
		    else		    
		    {
	            //����������ʾ		      
	            fileList.allFiles(4);          
		    }        
		    //alert(document.getElementById("uploadFileName").value  )  
		    
		    //����
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