using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using QJVRMS.Business;

namespace WebUI.Handlers
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class LightBoxHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            HttpResponse Response = context.Response;
            HttpRequest Request = context.Request;
            Response.ContentType = "text/plain";

            string action = string.Empty;
            string ret="";
            action = Request.QueryString["action"];

            if (!string.IsNullOrEmpty(action))
            {
                string resourceId = Request.QueryString["resourceid"];
                string lightboxId = Request.QueryString["lightboxid"];

                if (action.ToLower().Equals("addtolightbox"))
                {
                    if (AddToLightBox(new Guid(resourceId), new Guid(lightboxId)))
                    {
                        ret = "1";
                    }
                    else
                    {
                        ret = "0";
                    }
                
                
                }
                else if (action.ToLower().Equals("delfromlightbox"))
                {
                    if (DelFromLightBox(new Guid(resourceId), new Guid(lightboxId)))
                    {
                        ret = "1";
                    }
                    else
                    {
                        ret = "0";
                    }                
                }
            
            
            }

            Response.Write(ret);


        }


        private bool AddToLightBox(Guid resourceId, Guid lightboxId)
        {
            Resource r = new Resource();
            return r.AddToLightBox(resourceId, lightboxId);
        }

        private bool DelFromLightBox(Guid resourceId, Guid lightboxId)
        {
            Resource r = new Resource();
            return r.DelFromLightBox(resourceId, lightboxId);
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
