using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using QJVRMS.Business;
using System.IO;
using System.Web.SessionState;
using QJVRMS.Business.ResourceType;

namespace WebUI.Modules
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetSNByResourceType : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest Request = context.Request;
            HttpResponse Response = context.Response;

            Response.ContentType = "text/plain";

            string filename = Request["fileName"];
            string retSn = string.Empty;
            string dot = "";
            if (!string.IsNullOrEmpty(filename))
            {
                string fileType = Path.GetExtension(filename).ToLower();



                if (!string.IsNullOrEmpty(fileType))
                {
                    if (fileType.IndexOf(".") > -1)
                    {
                        fileType = fileType.Substring(1);
                        dot = ".";
                    }
                }
                
                retSn = new Resource().GetSN(ResourceTypeFactory.getResourceType(fileType).ResourceSNPrefix);                
                retSn = retSn +dot+fileType+":"+filename;
            }
            context.Session["sn"] = retSn;
            Response.Write(retSn); //返回带有扩展名的新文件
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
