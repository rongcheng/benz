using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using QJVRMS.Business.ResourceType;
using System.IO;
using QJVRMS.Common;

namespace WebUI.Handlers
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class slideShowDetail : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            string strServerFolderName = context.Request.QueryString["folderName"] == null ? "" : context.Request.QueryString["folderName"].Trim();
            string strServerFileName = context.Request.QueryString["fileName"] == null ? "" : context.Request.QueryString["fileName"].Trim();

            ImageType objImg = new ImageType();
            //yangguang
            //string picUrl = objImg.SourcePath + "/" + strServerFolderName + "/" + strServerFileName;
            string picUrl = objImg.GetSourcePath(strServerFolderName, strServerFileName);

            context.Response.Clear();
            context.Response.ContentType = "image/jpeg";
            context.Response.AddHeader("Content-Disposition", "filename=" +Path.GetFileNameWithoutExtension(strServerFileName)  + ".jpg");

            Stream iStream = null;
            try
            {
                byte[] buffer = new Byte[10000];
                int length;
                long dataToRead;

                iStream = new FileStream(picUrl, FileMode.Open, FileAccess.Read, FileShare.Read);

                dataToRead = iStream.Length;                

                while (dataToRead > 0)
                {
                    if (context.Response.IsClientConnected)
                    {
                        length = iStream.Read(buffer, 0, 10000);
                        context.Response.OutputStream.Write(buffer, 0, length);
                        context.Response.Flush();

                        buffer = new Byte[10000];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteExceptionLog(ex);
            }
            finally
            {
                if (iStream != null)
                {
                    iStream.Close();
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
