using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text;
using QJVRMS.Business;

namespace WebUI.Handlers
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class keywordHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest Request = context.Request;
            HttpResponse Response = context.Response;

            Response.ContentType = "text/plain";
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            sb.Append("[");
            string key = Request.QueryString["q"];
            if (!string.IsNullOrEmpty(key))
            {
                string[] ret=ResourceIndex.GetRelatedKeywords(key);
                int icount=ret.Length;
                for(int i=0;i<icount;i++)
                {
                    sb.Append("'");
                    sb.Append(ret[i]);
                    sb.Append("'");

                    sb1.Append(ret[i]);

                    if (i < icount - 1)
                    {
                        sb.Append(",");
                        sb1.Append("\r\n");
                    }
                    
                    
                }
                
            }
            sb.Append("]");
            Response.Write(sb1.ToString());
            Response.End();

            //返回一个关键字数组 ['','',''] 
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
