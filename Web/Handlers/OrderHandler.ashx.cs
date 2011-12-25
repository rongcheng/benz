using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using QJVRMS.Business;
using QJVRMS.Common;
using WebUI.UIBiz;

namespace WebUI.Handlers
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class OrderHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest Request = context.Request;
            HttpResponse Response = context.Response;

            Response.ContentType = "text/plain";

            string action = Request.QueryString["action"];

            if (!string.IsNullOrEmpty(action))
            {
                Orders obj = new Orders();
                if (action.Trim().ToLower().Equals("delresource"))
                {
                    string orderId = Request.QueryString["orderId"];
                    string resourceId = Request.QueryString["resourceId"];

                    
                    try
                    {
                        if (obj.DelResourceFromOrders(orderId, resourceId) > 0)
                        {
                            Response.Write("删除成功");
                            Response.End();
                        }
                    }
                    catch (Exception ex)
                    {
                        LogWriter.WriteExceptionLog(ex);
                    }
                    Response.Write("删除失败");
                    Response.End();
                }
                else if (action.Trim().ToLower().Equals("orderalert"))
                { 
                    //是否提醒
                    string userId=Request.QueryString["userId"];
                    if(!string.IsNullOrEmpty(userId))
                    {
                        
                        if (obj.IsOrderAlertAdmin(new Guid(userId)))
                        {
                            Response.Write("0"); //有权限的人
                            Response.End();
                        }
                        else if (obj.IsOrderAlert(new Guid(userId)))
                        {
                            Response.Write("1"); //普通用户
                            Response.End();
                        }


                        
                    }
                    Response.Write("2"); //不用提醒
                    Response.End();
                }
            }


            

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
