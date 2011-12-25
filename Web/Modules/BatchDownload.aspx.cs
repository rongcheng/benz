using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using QJVRMS.Business;
using QJVRMS.Business.ResourceType;
using ICSharpCode.SharpZipLib.Zip;
using System.Collections.Generic;
using System.IO;
using QJVRMS.Business.Interface;


namespace WebUI.Modules
{
    public partial class BatchDownload : AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                string ids = Request.QueryString["ids"];
                if (string.IsNullOrEmpty(ids))
                {
                    return;
                }

                ids = ids.TrimEnd(";".ToCharArray());
                string[] arrIds = ids.Split(";".ToCharArray());



                ImageType objImageType = new ImageType();
                IResourceType rt = null; 

                string physicalPath = string.Empty;
                List<string> zipFileList = new List<string>();
                foreach (string id in arrIds)
                {

                    ResourceEntity re = null;
                    Resource r = new Resource();
                    re = r.GetResourceInfoByItemId(id);

                    //physicalPath = objImageType.GetSourcePath(re.FolderName, re.ServerFileName);

                    rt = ResourceTypeFactory.getResourceTypeByString(re.ResourceType);
                    physicalPath = rt.GetSourcePath(re.FolderName, re.ServerFileName);

                    if (!string.IsNullOrEmpty(physicalPath))
                    {
                        zipFileList.Add(physicalPath);
                    }

                    //记录下载日志
                    Resource.Production_Hires_Down_Log(re.ItemSerialNum,Path.GetExtension(re.ServerFileName), CurrentUser.UserName, "", "", re.FolderName, false, re.ResourceType);


                }





                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("content-disposition", "attachment; filename=\"Download.zip\"");
                Response.CacheControl = "Private";
                Response.Cache.SetExpires(DateTime.Now.AddMinutes(3));

                byte[] buffer = new byte[4096];

                ZipOutputStream zipOutputStream = new ZipOutputStream(Response.OutputStream);
                zipOutputStream.SetLevel(3);

                foreach (string fileName in zipFileList)
                {
                    

                    Stream fs = File.OpenRead(fileName);	// or any suitable inputstream

                    ZipEntry entry = new ZipEntry(ZipEntry.CleanName(Path.GetFileName(fileName)));
                    entry.Size = fs.Length;
                    zipOutputStream.PutNextEntry(entry);

                    int count = fs.Read(buffer, 0, buffer.Length);
                    while (count > 0)
                    {
                        zipOutputStream.Write(buffer, 0, count);
                        count = fs.Read(buffer, 0, buffer.Length);
                        if (!Response.IsClientConnected)
                        {
                            break;
                        }
                        Response.Flush();
                    }
                    fs.Close();
                }
                zipOutputStream.Close();

                Response.Flush();
                Response.End();
         

                

            }

        }
    }
}
