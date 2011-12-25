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
    public class alertHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            HttpRequest Request = context.Request;
            HttpResponse Response = context.Response;

            Response.ContentType = "text/plain";

            string action = Request.QueryString["action"];
            string _ret = "";

            if (string.IsNullOrEmpty(action))
            {


            }
            else if (action.Trim().ToLower().Equals("alert"))
            {
                //是否有需要审核的图片资源，提醒
                Resource obj = new Resource();
                string userId = Request.QueryString["userId"];
                string isSuperAdmin = Request.QueryString["isSuperAdmin"];

                if (!string.IsNullOrEmpty(userId))
                {
                    if (string.IsNullOrEmpty(isSuperAdmin))
                    {
                        isSuperAdmin = "0";
                    }

                    bool IsSuperAdmin = (isSuperAdmin == "1");

                    if (obj.IsAlertAdmin(new Guid(userId), isSuperAdmin))
                    {
                        //Response.Write("1"); //有新的图片等待审核
                        //Response.End();

                        _ret = "isResourceAlert:1";
                    }
                    else
                    {
                        //Response.Write("0"); //没有需要审核的图片
                        //Response.End();
                        _ret = "isResourceAlert:0";
                    }


                    ////看是否需要订单提醒
                   
                    //Orders objOrder = new Orders();
                    //if (objOrder.IsOrderAlertAdmin(new Guid(userId)))
                    //{
                    //    //Response.Write("0"); //有权限的人
                    //    //Response.End();

                    //    _ret = _ret + ",isOrderAlert:1";
                    //}
                    //else if (objOrder.IsOrderAlert(new Guid(userId)))
                    //{
                    //    //Response.Write("1"); //普通用户
                    //    //Response.End();
                    //    _ret = _ret + ",isOrderAlert:2";
                    //}
                    //else
                    //{
                    //    _ret = _ret + ",isOrderAlert:0";
                    
                    //}


                    ////查看是否有订单沟通提醒

                    //if (objOrder.IsOrderAlertAdmin(new Guid(userId)) || IsSuperAdmin)
                    //{
                    //    DataTable dt = objOrder.IsOrderMessageAlertAdmin();
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        _ret = _ret + ",isOrderMessageAlert:1";
                    //    }
                    //    else
                    //    {
                    //        _ret = _ret + ",isOrderMessageAlert:0";
                    //    }
                    //}
                    //else
                    //{
                    //    DataTable dt1 = objOrder.IsOrderMessageAlertUser(new Guid(userId));
                    //    if (dt1!=null && dt1.Rows.Count > 0)
                    //    {
                    //        _ret = _ret + ",isOrderMessageAlert:1";
                    //    }
                    //    else
                    //    {
                    //        _ret = _ret + ",isOrderMessageAlert:0";
                    //    }
                    //}




                }
                //Response.Write("0"); //不用提醒
                Response.Write("{"+_ret+"}");
                Response.End();
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
