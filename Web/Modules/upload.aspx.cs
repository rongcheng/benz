using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using WebUI;
using System.Drawing;
using QJVRMS.Business;
using QJVRMS.Common;
using WebUI.UIBiz;

using System.Net;
using System.Runtime;
using System.Text;
using System.Threading;
using System.Collections.Specialized;
using QJVRMS.Business.Interface;
using QJVRMS.Business.ResourceType;
/// <summary>
/// 上传图片和附件，该程序只做简单的上传操作，不做其他的业务相关的操作
/// by ciqq 2010-3-31
/// </summary>
public partial class upload : AuthPage {
    string uploadSN = string.Empty;
    string savedFileName = string.Empty;

    protected void Page_Load(object sender, EventArgs e) {

        if (Session["sn"] != null) {
            uploadSN = Session["sn"].ToString();
        }

        if (!string.IsNullOrEmpty(uploadSN)) {
            string[] arr = uploadSN.Split(new char[] { ':' });
            savedFileName = arr[0];

        }

        string uploadType = Request["uploadType"];
        if (!string.IsNullOrEmpty(uploadType))
        {
            uploadType = uploadType.ToString().ToLower();
            if (uploadType.Equals("resourcefile")) {
                if (!string.IsNullOrEmpty(savedFileName)) {
                    uploadResource();
                    return;
                }
            }
            if (uploadType.Equals("attachmentfile")) {
                uploadAttachfile();
                return;
            }
        }
    }

    private void uploadSourceFile(HttpPostedFile hpf) {
        string filename = hpf.FileName;
        string fileType = Path.GetExtension(filename);
        string savePath = string.Empty;
        //yangguang
        //if (fileType.IndexOf(".") > -1)
        //{
        //    savePath = ResourceTypeFactory.getResourceType(fileType.Substring(1)).SourcePath;
        //}
        //else
        //{
        //    savePath = ResourceTypeFactory.getResourceType(fileType).SourcePath;
        //}
        if (fileType.IndexOf(".") > -1) {
            savePath = ResourceTypeFactory.getResourceType(fileType.Substring(1)).GetSourcePath();
        }
        else {
            savePath = ResourceTypeFactory.getResourceType(fileType).GetSourcePath();
        }
        
        savePath = Path.Combine(savePath, CurrentUser.UserLoginName);


        string resourceseq = Path.GetFileNameWithoutExtension(savedFileName);
        string fileFullPath = Path.Combine(savePath, resourceseq + fileType);

        try {
            if (!Directory.Exists(savePath)) {
                Directory.CreateDirectory(savePath);
            }
            //Console.WriteLine("Pre   Content Length-----" + hpf.ContentLength);
            //Console.WriteLine("Pre Current-----" + hpf.InputStream.Seek(0, SeekOrigin.Current));
            //Console.WriteLine("Pre  position-----" + hpf.InputStream.Position);
            //Console.WriteLine("Pre  Length-----" + hpf.InputStream.Length);
            hpf.SaveAs(fileFullPath);
            //Console.WriteLine("Re Content Length-----" + hpf.ContentLength);
            //Console.WriteLine("Re Current-----" + hpf.InputStream.Seek(0, SeekOrigin.Current));
            //Console.WriteLine("Re  Position -----" + hpf.InputStream.Position);
            //Console.WriteLine("Re Length-----" + hpf.InputStream.Length);

            //Console.WriteLine("End -----" + hpf.InputStream.Seek(0, SeekOrigin.End));
            //Console.WriteLine("End  postion-----" + hpf.InputStream.Position);
        }
        catch (Exception ep) {
            LogWriter.WriteExceptionLog(ep, true);
        }
    }

    private void uploadOtherFile(HttpPostedFile hpf) {
        uploadSourceFile(hpf);
    }

    private void uploadDocumentFile(HttpPostedFile hpf) {
        uploadSourceFile(hpf);
    }

    private void uploadResource() {
        HttpPostedFile objFile = Request.Files["Filedata"];
        if (objFile != null) {
            string filename = objFile.FileName;
            string fileType = Path.GetExtension(filename).ToLower();
            if (fileType.IndexOf(".") == -1)
            {
                fileType = "."+fileType;
            }
            //string resourceType = new Resource().GetResourceTypeByFileExtention(fileType);
            string resourceType = ResourceTypeFactory.getResourceType(fileType.Substring(1)).ResourceType;

            if (resourceType.Equals("image")) {
                if (!this.CheckImageType(fileType) || fileType == "ai") {
                    uploadFile();
                }
                else {
                    if (fileType == ".cr2" || fileType == ".nef") {
                        string path = Server.MapPath(@"\temp\" + filename);
                        string name = filename.Replace(Path.GetExtension(filename), string.Empty);
                        objFile.SaveAs(path);
                        TransformMode(path);
                        Thread.Sleep(5000);
                        SaveImage(name, Path.GetExtension(filename));
                    }
                    else if (fileType == ".psd") {
                        string name = Guid.NewGuid().ToString();
                        string path = Server.MapPath(@"\temp\" + name + fileType);
                        objFile.SaveAs(path);
                        TransformPSD(path, name);
                        Thread.Sleep(15000);
                        SaveImagePSD(name, Path.GetExtension(filename));
                    }
                    else {
                        SaveImage();
                    }
                }
            }
            else if (resourceType.Equals("video")) {
                uploadVideo(objFile);
            }
            else if (resourceType.Equals("document")) {
                uploadDocumentFile(objFile);
            }
            else if (resourceType.Equals("other")) {
                uploadOtherFile(objFile);
            }
        }
    }
    private void TransformPSD(string path, string name) {
        System.Diagnostics.Process proc = new System.Diagnostics.Process();
        try {
            string newpath = Server.MapPath(@"\temp\" + name + ".jpg");

            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();

            proc.StandardInput.WriteLine(@"convert.exe -density 72 " + path + " " + newpath);
            proc.StandardInput.WriteLine("exit");

            proc.Close();

        }
        catch{
            proc.Close();
        }
    }
    private void TransformMode(string path) {
        System.Diagnostics.Process proc = new System.Diagnostics.Process();
        try {

            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo("dcraw.exe", @"-e " + path);
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = false;
            proc.StartInfo = startInfo;
            proc.Start();
            proc.Close();
        }
        catch {
            proc.Close();
        }
    }

    /// <summary>
    /// 上传视频文件
    /// </summary>
    /// <param name="hpf"></param>
    private void uploadVideo(HttpPostedFile hpf) {
        uploadSourceFile(hpf);

    }

    /// <summary>
    /// 上传附件
    /// </summary>
    private void uploadAttachfile() {
        string _ret = string.Empty;
        string errorCode = "00";
        string resourceId = Request["resourceid"].ToString();
        string foldername = Request["foldername"].ToString();
        string sourceFolder = string.Empty;

        ResourceEntity re = new Resource().GetResourceInfoByItemId(resourceId);
        ImageType imageType = new ImageType();
        if (re.ResourceType.ToLower().Equals("image")) {
            //yangguang
            //sourceFolder = imageType.SourcePath;
            sourceFolder = imageType.GetSourcePath();
        }
        else if (re.ResourceType.ToLower().Equals("video")) {
            sourceFolder = imageType.GetVideoPath();
        }

        sourceFolder = Path.Combine(sourceFolder, foldername);
        sourceFolder = Path.Combine(sourceFolder, WebUI.UIBiz.CommonInfo.AttachFolder);


        if (!Directory.Exists(sourceFolder)) {
            Directory.CreateDirectory(sourceFolder);
        }

        HttpPostedFile f = Request.Files["Filedata"];
        string filename = f.FileName;
        string fileType = Path.GetExtension(filename);


        string fileFullPath = Path.Combine(sourceFolder, filename);

        if (filename.Length > 255) {
            _ret = "附件名称过长,需小于255个字符!";
            errorCode = "01";
        }
        else if (File.Exists(fileFullPath)) {
            _ret = "附件名称重复，请修改名称后重新上传!";
            errorCode = "02";
        }
        else {
            try {
                f.SaveAs(fileFullPath);

                if (Resource.AddAttach(resourceId, filename, f.ContentLength)) {
                    _ret = "添加附件成功!";
                    errorCode = "03";
                }
                else {
                    _ret = "添加附件失败!";
                    errorCode = "04";
                }
            }
            catch (PathTooLongException pe) {
                _ret = "附件名称过长!";
                errorCode = "05";
            }
        }

        Response.Write(errorCode);
        Response.End();

    }


    private void uploadFile() {
        HttpPostedFile f = Request.Files["Filedata"];
        string filename = f.FileName;
        string filetype = Path.GetExtension(filename).ToLower();
        string imgseq = ImageStorageClass.GetImageSeq(DateTime.Now);
        string ImageRootPath = WebUI.UIBiz.CommonInfo.ImageRootPath;
        string sourcePath = ImageRootPath + "\\" + CurrentUser.UserLoginName + "\\" + imgseq + filetype;
        try {
            string sourceFolder = Path.Combine(ImageRootPath, CurrentUser.UserLoginName);
            if (!Directory.Exists(sourceFolder)) {
                Directory.CreateDirectory(sourceFolder);
            }

            //保存原图

            f.SaveAs(sourcePath);
            Response.Write(imgseq + filetype + ":" + f.FileName);
        }
        catch { }

    }
    private void SaveImagePSD(string fileName, string fileType) {
        string path = Server.MapPath(@"\temp\" + fileName + "-0.jpg");
        if (!File.Exists(path)) {
            path = Server.MapPath(@"\temp\" + fileName + ".jpg");
        }
        string sPath = Server.MapPath(@"\temp\" + fileName + fileType);

        FileStream fs = new FileStream(sPath, FileMode.Open, FileAccess.Read);
        int l = int.Parse(fs.Length.ToString());
        byte[] b = new byte[l];
        fs.Read(b, 0, l);
        fs.Flush();
        fs.Close();

        fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        int ll = int.Parse(fs.Length.ToString());
        byte[] bb = new byte[ll];
        fs.Read(bb, 0, ll);
        fs.Flush();
        fs.Close();

        ImageType obj = new ImageType();
        //yangguang
        //string savePath = obj.SourcePath;
        string savePath = obj.GetSourcePath(obj.SourcePaths);
        savePath = Path.Combine(savePath, CurrentUser.UserLoginName);
        string resourceseq = Path.GetFileNameWithoutExtension(savedFileName);
        string fileFullPath = Path.Combine(savePath, resourceseq + fileType);
        string fileFullPath1 = Path.Combine(savePath, resourceseq + ".jpg");
        try {
            if (!Directory.Exists(savePath)) {
                Directory.CreateDirectory(savePath);
            }

            FileStream stream = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write);
            stream.Write(b, 0, l);
            stream.Flush();
            stream.Close();

            stream = new FileStream(fileFullPath1, FileMode.Create, FileAccess.Write);
            stream.Write(bb, 0, ll);
            stream.Flush();
            stream.Close();

            string SlImageRootPath;
            SlImageRootPath = Path.Combine(obj.PreviewPath_170, CurrentUser.UserLoginName);
            if (!Directory.Exists(SlImageRootPath)) {
                Directory.CreateDirectory(SlImageRootPath);
            }

            ArrayList sarray = new ArrayList();
            sarray.Add(path);
            ArrayList aarray = new ArrayList();
            aarray.Add(Path.Combine(SlImageRootPath, resourceseq + ".jpg"));
            ImageController.ToZipImage(sarray, aarray, 170);

            SlImageRootPath = Path.Combine(obj.PreviewPath_400, CurrentUser.UserLoginName);
            if (!Directory.Exists(SlImageRootPath)) {
                Directory.CreateDirectory(SlImageRootPath);
            }
            sarray = new ArrayList();
            sarray.Add(path);
            aarray = new ArrayList();
            aarray.Add(Path.Combine(SlImageRootPath, resourceseq + ".jpg"));
            ImageController.ToZipImage(sarray, aarray, 400);

            System.Drawing.Image m_Image = System.Drawing.Image.FromFile(path);
            Int32 height = Convert.ToInt32(m_Image.Height.ToString());
            Int32 width = Convert.ToInt32(m_Image.Width.ToString());
            string hvsp = string.Empty;
            if (height > width)
                hvsp = "v";
            else if (width > height)
                hvsp = "h";
            else
                hvsp = "s";

            Dictionary<string, string> dct = new Dictionary<string, string>();
            dct.Add("Width", width.ToString());
            dct.Add("Height", height.ToString());
            dct.Add("Hvsp", hvsp);
            List<QJVRMS.Business.ResourceWS.DictionaryEntry> lst = new List<QJVRMS.Business.ResourceWS.DictionaryEntry>();

            foreach (string key in dct.Keys) {
                QJVRMS.Business.ResourceWS.DictionaryEntry de = new QJVRMS.Business.ResourceWS.DictionaryEntry();
                de.Key = key;
                de.Value = dct[key];
                lst.Add(de);
            }

            QJVRMS.Business.ResourceWS.DictionaryEntry[] result = lst.ToArray();
            Resource r = new Resource();
            r.insertResourceAttributes(resourceseq, result);
            string directoryPath = Server.MapPath(@"\temp\");
            DirectoryInfo dir = new DirectoryInfo(directoryPath);

            foreach (FileInfo info in dir.GetFiles()) {
                DeleteTempFile(info.FullName);
            }
            Response.Write(resourceseq + ".jpg" + ":" + savedFileName);
        }
        catch (Exception e1) {
            Response.Write("保存图片出现错误" + e1.Message);
            LogWriter.WriteExceptionLog(e1, true);
        }
        finally { }
    }
    private void SaveImage(string fileName, string fileType) {
        string path = Server.MapPath(@"\temp\" + fileName + ".thumb.jpg");
        string sPath = Server.MapPath(@"\temp\" + fileName + fileType);

        FileStream fs = new FileStream(sPath, FileMode.Open, FileAccess.Read);
        int l = int.Parse(fs.Length.ToString());
        byte[] b = new byte[l];
        fs.Read(b, 0, l);
        fs.Flush();
        fs.Close();

        fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        int ll = int.Parse(fs.Length.ToString());
        byte[] bb = new byte[ll];
        fs.Read(bb, 0, ll);
        fs.Flush();
        fs.Close();

        ImageType obj = new ImageType();
        //yangguang
        //string savePath = obj.SourcePath;
        string savePath = obj.GetSourcePath(obj.SourcePaths);
        savePath = Path.Combine(savePath, CurrentUser.UserLoginName);
        string resourceseq = Path.GetFileNameWithoutExtension(savedFileName);
        string fileFullPath = Path.Combine(savePath, resourceseq + fileType);
        string fileFullPath1 = Path.Combine(savePath, resourceseq + ".jpg");
        try {
            if (!Directory.Exists(savePath)) {
                Directory.CreateDirectory(savePath);
            }

            FileStream stream = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write);
            stream.Write(b, 0, l);
            stream.Flush();
            stream.Close();

            stream = new FileStream(fileFullPath1, FileMode.Create, FileAccess.Write);
            stream.Write(bb, 0, ll);
            stream.Flush();
            stream.Close();

            string SlImageRootPath;
            SlImageRootPath = Path.Combine(obj.PreviewPath_170, CurrentUser.UserLoginName);
            if (!Directory.Exists(SlImageRootPath)) {
                Directory.CreateDirectory(SlImageRootPath);
            }

            ArrayList sarray = new ArrayList();
            sarray.Add(path);
            ArrayList aarray = new ArrayList();
            aarray.Add(Path.Combine(SlImageRootPath, resourceseq + ".jpg"));
            ImageController.ToZipImage(sarray, aarray, 170);

            SlImageRootPath = Path.Combine(obj.PreviewPath_400, CurrentUser.UserLoginName);
            if (!Directory.Exists(SlImageRootPath)) {
                Directory.CreateDirectory(SlImageRootPath);
            }
            sarray = new ArrayList();
            sarray.Add(path);
            aarray = new ArrayList();
            aarray.Add(Path.Combine(SlImageRootPath, resourceseq + ".jpg"));
            ImageController.ToZipImage(sarray, aarray, 400);

            System.Drawing.Image m_Image = System.Drawing.Image.FromFile(path);
            Int32 height = Convert.ToInt32(m_Image.Height.ToString());
            Int32 width = Convert.ToInt32(m_Image.Width.ToString());
            string hvsp = string.Empty;
            if (height > width)
                hvsp = "v";
            else if (width > height)
                hvsp = "h";
            else
                hvsp = "s";

            Dictionary<string, string> dct = new Dictionary<string, string>();
            dct.Add("Width", width.ToString());
            dct.Add("Height", height.ToString());
            dct.Add("Hvsp", hvsp);
            List<QJVRMS.Business.ResourceWS.DictionaryEntry> lst = new List<QJVRMS.Business.ResourceWS.DictionaryEntry>();

            foreach (string key in dct.Keys) {
                QJVRMS.Business.ResourceWS.DictionaryEntry de = new QJVRMS.Business.ResourceWS.DictionaryEntry();
                de.Key = key;
                de.Value = dct[key];
                lst.Add(de);
            }

            QJVRMS.Business.ResourceWS.DictionaryEntry[] result = lst.ToArray();
            Resource r = new Resource();
            r.insertResourceAttributes(resourceseq, result);
            string directoryPath = Server.MapPath(@"\temp\");
            DirectoryInfo dir = new DirectoryInfo(directoryPath);

            foreach (FileInfo info in dir.GetFiles()) {
                DeleteTempFile(info.FullName);
            }
            Response.Write(resourceseq + ".jpg" + ":" + savedFileName);
        }
        catch (Exception e1) {
            Response.Write("保存图片出现错误" + e1.Message);
            LogWriter.WriteExceptionLog(e1, true);
        }
        finally { }
    }
    private void SaveImage() {
        HttpPostedFile f = Request.Files["Filedata"];
        string filename = f.FileName;
        string filetype = Path.GetExtension(filename).ToLower();
        ImageType obj = new ImageType();
        string savePath = obj.GetSourcePath(obj.SourcePaths);
        savePath = Path.Combine(savePath, CurrentUser.UserLoginName);
        string resourceseq = Path.GetFileNameWithoutExtension(savedFileName);
        string fileFullPath = Path.Combine(savePath, resourceseq + filetype);

        bool slImage;
        try {
            if (!Directory.Exists(savePath)) {
                Directory.CreateDirectory(savePath);
            }
            //保存原图
            f.SaveAs(fileFullPath);

            string SlImageRootPath;
            SlImageRootPath = Path.Combine(obj.PreviewPath_170, CurrentUser.UserLoginName);
            if (!Directory.Exists(SlImageRootPath)) {
                Directory.CreateDirectory(SlImageRootPath);
            }

            ArrayList sarray = new ArrayList();
            sarray.Add(fileFullPath);
            ArrayList aarray = new ArrayList();
            aarray.Add(Path.Combine(SlImageRootPath, resourceseq + filetype));
            ImageController.ToZipImage(sarray, aarray, 170);
        
            SlImageRootPath = Path.Combine(obj.PreviewPath_400, CurrentUser.UserLoginName);
            if (!Directory.Exists(SlImageRootPath)) {
                Directory.CreateDirectory(SlImageRootPath);
            }

            sarray = new ArrayList();
            sarray.Add(fileFullPath);
            aarray = new ArrayList();
            aarray.Add(Path.Combine(SlImageRootPath, resourceseq + filetype));
            ImageController.ToZipImage(sarray, aarray, 400);


            //将原图的长x宽保存起来

            System.Drawing.Image m_Image = System.Drawing.Image.FromFile(fileFullPath);

            Int32 height = Convert.ToInt32(m_Image.Height.ToString());
            Int32 width = Convert.ToInt32(m_Image.Width.ToString());
            string hvsp = string.Empty;


            if (height > width) {
                hvsp = "v";
            }
            else if (width > height) {
                hvsp = "h";
            }
            else {
                hvsp = "s";
            }

            Dictionary<string, string> dct = new Dictionary<string, string>();
            dct.Add("Width", width.ToString());
            dct.Add("Height", height.ToString());
            dct.Add("Hvsp", hvsp);
            List<QJVRMS.Business.ResourceWS.DictionaryEntry> lst = new List<QJVRMS.Business.ResourceWS.DictionaryEntry>();

            foreach (string key in dct.Keys) {
                QJVRMS.Business.ResourceWS.DictionaryEntry de = new QJVRMS.Business.ResourceWS.DictionaryEntry();
                de.Key = key;
                de.Value = dct[key];
                lst.Add(de);
            }

            QJVRMS.Business.ResourceWS.DictionaryEntry[] result = lst.ToArray();

            Resource r = new Resource();
            r.insertResourceAttributes(resourceseq, result);

            Response.Write(resourceseq + filetype + ":" + f.FileName);

        }
        catch (Exception e1) {
            Response.Write("保存图片出现错误" + e1.Message);
            LogWriter.WriteExceptionLog(e1, true);
        }
        finally {

        }
    }

    protected bool CheckImageType(string type) {
        type = type.ToLower();
        switch (type) {
            case ".jpg":
                return true;
            case ".png":
                return true;
            case ".psd":
                return true;
            case ".ai":
                return true;
            case ".jpeg":
                return true;
            case ".gif":
                return true;
            case ".bmp":
                return true;
            case ".tiff":
                return true;
            case ".tif":
                return true;
            case ".pcx":
                return true;
            case ".tga":
                return true;
            case ".exif":
                return true;
            case ".fpx":
                return true;
            case ".cr2":
                return true;
            case ".nef":
                return true;
            default:
                return false;
        }
    }

    private void DeleteTempFile(string path) {
        try {
            File.Delete(path);
        }
        catch {
            return;
        }
    }
}
