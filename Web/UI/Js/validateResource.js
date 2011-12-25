

function doValidateTiJiao(itemId,sourceObject)
{
    var url1="/Modules/Manage/Validating.aspx?ItemId="+itemId+"&validateStatus=1";
    $.get(url1,null,
    function(data) {
       if(data=="0")
       {
          alert("提交失败，请稍后重试");
       }
       else if(data=="1")
       {
            alert("提交成功，请等待审核");
            //sourceObject.innerHTML="审核中";
            sourceObject.disabled=true;
            sourceObject.onclick=function(){};
       }
    });
}