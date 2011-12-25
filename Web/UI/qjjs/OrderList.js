getlist();

function getlist()
{
   $.ajax({
        type: "GET",
        url: "GetOrderList.ashx",
        data:"",
        cache: false,
        success: function(msg){
        $("#main_show_box").html(msg);
        },
        error:function(){
        alert("您访问的页面出现问题，请稍候再试");
        }
       });
}       
       
function deletep(pid)
{
    if(confirm("是否确定要删除这个订单？"))
    {
        $.ajax({
                type: "GET",
                url: "Delete.ashx",
                data:"pid="+pid,
                cache: false,
                success: function(msg){
        
                if(msg=="1")
                {        
                    alert("删除成功！");
                    getlist();
                }
                else
                    alert("删除失败，请稍后重试！");
        
                },
                error:function(){
                alert("您访问的页面出现问题，请稍候再试");
                }
               });
    }
}

function modifyp(pid)
{
    var href="/order/Order.aspx?pid=" + pid;
    window.open(href,'_blank','width=500,height=610');
}
