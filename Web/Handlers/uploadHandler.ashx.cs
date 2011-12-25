using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;

namespace WebUI.Handlers
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class uploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World<br/>");




            IServiceProvider provider = (IServiceProvider)context;
            HttpWorkerRequest wr = (HttpWorkerRequest)provider.GetService(typeof(HttpWorkerRequest));

            for (int i = 0; i < 20; i++)
            {
                string s = wr.GetKnownRequestHeader(i);
                context.Response.Write(s+"\r\n");
            }

            context.Response.Write(wr.GetPreloadedEntityBody().Length);
            context.Response.Write("\r\n");
            context.Response.Write(wr.GetPreloadedEntityBodyLength());
            

            //HttpWorkerRequest wr = (HttpWorkerRequest)context.Request;
            //byte[] bs = wr.GetPreloadedEntityBody();
            ////....
            //if (!wr.IsEntireEntityBodyIsPreloaded())
            //{
            //    int n = 1024;
            //    byte[] bs2 = new byte[n];


            //    using (StreamWriter sw = new StreamWriter(@"c:\aaaaa1.txt"))
            //    {
            //        while (wr.ReadEntityBody(bs2, n) > 0)
            //        {
            //            // .....


            //            sw.Write(bs2.ToString());


            //        }

            //    }
            //}
            //else
            //{
            //    context.Response.Write("this branch");
                
            //}



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
