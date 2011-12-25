using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using QJVRMS.Business.Interface;
using QJVRMS.Business.ResourceType;
using QJVRMS.Business;
using QJVRMS.Common;

namespace WebUI {
    public partial class Save : System.Web.UI.Page {
        string filepath = string.Empty;
        string username = string.Empty;
        string resourceseq = string.Empty;

        protected void Page_Load(object sender, EventArgs e) {
            if (Request.Files.Count > 0) {
                try {
                    HttpPostedFile file = Request.Files[0];
                    if (!string.IsNullOrEmpty(file.FileName)) {
                        if (file.FileName.IndexOf("|") != -1) {
                            filepath = file.FileName.Split('|')[0].Trim();
                            username = file.FileName.Split('|')[1].Trim();
                            resourceseq = file.FileName.Split('|')[2].Trim();
                        } string filetype = Path.GetExtension(filepath).ToLower();

                        string path = this.MapPath("temp") + "\\" + filepath;

                        file.SaveAs(path);

                        if (File.Exists(path)) {
                            if (SaveImage(username, filetype, path)) {
                                File.Delete(path);
                            }
                        }

                        Response.Write("Success\r\n");
                    }
                    else {
                        Response.Write("Error\r\n");
                    }
                }
                catch {
                    Response.Write("Error\r\n");
                }
            }
        }

        private bool SaveImage(string userName, string fileType, string tempPath) {
            ImageType obj = new ImageType();
            //yangguang
            //string savePath = obj.SourcePath;
            string savePath = obj.GetSourcePath();
            savePath = Path.Combine(savePath, userName);
            //string resourceseq = new Resource().GetSN(ResourceTypeFactory.getResourceType(fileType.Substring(1)).ResourceSNPrefix);

            string fileFullPath = Path.Combine(savePath, resourceseq + fileType);
            bool bImage = false;
            try {
                if (!Directory.Exists(savePath)) {
                    Directory.CreateDirectory(savePath);
                }
                //保存原图
                if (SaveFile(fileFullPath, tempPath)) {

                    string SlImageRootPath = Path.Combine(obj.PreviewPath_170, userName);
                    if (!Directory.Exists(SlImageRootPath))
                        Directory.CreateDirectory(SlImageRootPath);

                    ArrayList sarray = new ArrayList();
                    sarray.Add(fileFullPath);
                    ArrayList aarray = new ArrayList();
                    aarray.Add(Path.Combine(SlImageRootPath, resourceseq + fileType));
                    ImageController.ToZipImage(sarray, aarray, 170);

                    SlImageRootPath = Path.Combine(obj.PreviewPath_400, username);
                    if (!Directory.Exists(SlImageRootPath))
                        Directory.CreateDirectory(SlImageRootPath);

                    sarray = new ArrayList();
                    sarray.Add(fileFullPath);
                    aarray = new ArrayList();
                    aarray.Add(Path.Combine(SlImageRootPath, resourceseq + fileType));
                    ImageController.ToZipImage(sarray, aarray, 400);

                    bImage = true;
                }
            }
            catch (Exception e1) {
                LogWriter.WriteExceptionLog(e1, true);
                return false;
            }
            finally {

            }

            return bImage;
        }

        private bool SaveFile(string fileFullPath, string tempPath) {
            try {
                if (File.Exists(fileFullPath))
                    File.Delete(fileFullPath);
                FileStream ts = new FileStream(tempPath, FileMode.Open, FileAccess.Read);
                byte[] nbytes = new byte[ts.Length];
                int nReadSize = 0;
                nReadSize = ts.Read(nbytes, 0, nbytes.Length);

                if (nReadSize > 0) {
                    FileStream fs = new FileStream(fileFullPath, FileMode.Create, FileAccess.ReadWrite);
                    fs.Write(nbytes, 0, nReadSize);
                    fs.Close();
                }

                ts.Close();

                return true;
            }
            catch {
                return false;
            }
        }
    }
}
