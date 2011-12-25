
function doValidate(myDate,objId)
{
    if(objId!=undefined)
    {
        if(objId=="shotDate")
        {
            
            var dt1=new Date();
            var dt2=new Date(Date.parse(myDate));
            if(dt1<dt2)
            {
                alert("拍摄时间应比现在早");               
                
            }
        
        }
    }

}
