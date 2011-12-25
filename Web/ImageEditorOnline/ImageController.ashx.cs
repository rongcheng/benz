using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using QJVRMS.Common;
using System.IO;

namespace WebUI.ImageEditorOnline
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ImageController : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            HttpResponse Response = context.Response;
            HttpRequest Request = context.Request;
            HttpServerUtility Server = context.Server;


            Response.ContentType = "text/plain";

            string action = Request["action"];
            string sourceFilePath = Request["srcFile"];
            string param=Request["param"];

            if (string.IsNullOrEmpty(action) || string.IsNullOrEmpty(sourceFilePath) || string.IsNullOrEmpty(param))
            {
                Response.Write("参数不足");
                return;
            }

            string physicalSourcePath = Server.MapPath(sourceFilePath);
            string fileName = Path.GetFileNameWithoutExtension(physicalSourcePath) ;
            string destinationFilePath=  string.Empty;
            QJVRMS.Common.ImageController obj = new QJVRMS.Common.ImageController();
            
            //switch (action)
            //{ 
            //    case "gray":
            //        fileName = fileName + "_" + action + ".jpg";
            //        destinationFilePath = Server.MapPath("TempPath/" + fileName);
            //        obj.ToGray(physicalSourcePath, destinationFilePath);
            //        break;
            //    case "rotate":
            //        fileName = fileName + "_" + action + "_"+param+".jpg";
            //        destinationFilePath = Server.MapPath("TempPath/" + fileName);
            //        obj.Rotate(physicalSourcePath, destinationFilePath, Convert.ToInt32(param));
            //        break;
            //    case "border":
            //        string[] _arr=param.Split(new char[]{','});
            //        fileName = fileName + "_" + action + "_" + _arr[0] + ".jpg";
            //        destinationFilePath = Server.MapPath("TempPath/" + fileName);
            //        obj.AddBorder(physicalSourcePath, destinationFilePath, _arr[1],Convert.ToInt32(_arr[0]));
            //        break;
            //    case "flip":
            //        fileName = fileName + "_" + action + ".jpg";
            //        destinationFilePath = Server.MapPath("TempPath/" + fileName);
            //        obj.Flip(physicalSourcePath, destinationFilePath);
            //        break;
            //    case "flop":
            //        fileName = fileName + "_" + action + ".jpg";
            //        destinationFilePath = Server.MapPath("TempPath/" + fileName);
            //        obj.Flop(physicalSourcePath, destinationFilePath);
            //        break;

            //}
            Response.Write("TempPath/" + fileName);
            //File.Delete(destinationFilePath);
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
