using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using QJVRMS.Business.Interface;
using System.IO;
using System.Web;
using System.Net;

namespace QJVRMS.Business.ResourceType
{
    /// <summary>
    /// 图片类型的相关设置
    /// </summary>
    public class ImageType : IResourceType
    {
        public ImageType()
        { 
            
        }

        public string SourcePath
        {
            get 
            {
                return ConfigurationManager.AppSettings["imageSourcePath"];
            }
        }

        public string[] SourcePaths {
            get {
                return ConfigurationManager.AppSettings["imageSourcePath"].Split(',');
            }
        }

        public string VideoPath {
            get {
                return ConfigurationManager.AppSettings["videoSourcePath"];
            }
        }

        public string[] VideoPaths {
            get {
                return ConfigurationManager.AppSettings["videoSourcePath"].Split(',');
            }
        }

        public string PreviewPath
        {
            get
            {
                return ConfigurationManager.AppSettings["imagePreviewPath"];
            }
        }

        public string[] PreviewPaths {
            get {
                return ConfigurationManager.AppSettings["imagePreviewPath"].Split(',');
            }
        }
        //public string PreviewPath_170 {
        //    get {
        //        return Path.Combine(PreviewPath, "170");
        //    }
        //}

        //public string PreviewPath_400 {
        //    get {
        //        return Path.Combine(PreviewPath, "400");
        //    }
        //}
        public string PreviewPath_170
        {
            get
            {
                return Path.Combine(PreviewPaths[PathNumber], "170");
            }
        }

        public string PreviewPath_400
        {
            get
            {
                return Path.Combine(PreviewPaths[PathNumber], "400");
            }
        }

        public string PreviewPath_170_Read
        {
            get
            {
                return ConfigurationManager.AppSettings["imagePreviewPath_Read"].ToString()+"/170/";
            }
        }

        public string[] PreviewPath_Reads {
            get {
                return ConfigurationManager.AppSettings["imagePreviewPath_Read"].Split(',');
            }
        }

        public string PreviewPath_400_Read
        {
            get
            {
                return ConfigurationManager.AppSettings["imagePreviewPath_Read"].ToString()+"/400/";
            }
        }

        public string DetailPage
        {
            get
            {
                return "/PicDetail.aspx";
            }
        }

        public string ResourceType
        {
            get
            {
                return "image";
            }
        }


        public string ResourceSNPrefix
        {
            get
            {
                return "IMG";
            }
        }

        public string DetailPageUrl
        {
            get
            {
                return "/PicDetail.aspx";
            }
        }

        //支持的图像扩展名
        public string[] FileExtention
        {
            get
            {
                int i=0;
                string[] ret = new string[14] ;
                ret[i++] = "JPG";
                ret[i++] = "JPEG";
                ret[i++] = "GIF";
                ret[i++] = "TIFF";
                ret[i++] = "TIF";
                ret[i++] = "PNG";
                ret[i++] = "BMP";
                ret[i++] = "PCX";
                ret[i++] = "TGA";
                ret[i++] = "EXIF";
                ret[i++] = "FPX";
                ret[i++] = "CR2";
                ret[i++] = "NEF";
                ret[i++] = "PSD";
                return ret;
            }
        }
        public int PathNumber {
            get;
            set;
        }

        public string GetVideoPath() {
            string resultPath = string.Empty;
            string discName = string.Empty;
            long freeSpace = 0;

            string[] paths = VideoPaths;
            for (int i = 0; i < paths.Length; i++) {
                discName = paths[i].Trim().Substring(0, 1);
                freeSpace = GetHardDiskFreeSpace(discName);
                if (freeSpace > 0) {
                    PathNumber = i;
                    resultPath = paths[i].Trim();
                    break;
                }
            }

            return resultPath;
        }

        public string GetSourcePath() {
            string resultPath = string.Empty;
            string discName = string.Empty;
            long freeSpace = 0;

            string[] paths = SourcePaths;
            for(int i=0;i<paths.Length;i++){
                discName = paths[i].Trim().Substring(0, 1);
                freeSpace = GetHardDiskFreeSpace(discName);
                if (freeSpace > 0) {
                    PathNumber = i;
                    resultPath = paths[i].Trim();
                    break;
                }
            }

            return resultPath;
        }

        public string GetSourcePath(string[] paths) {
            string resultPath = string.Empty;
            string discName = string.Empty;
            long freeSpace = 0;

            for (int i = 0; i < paths.Length; i++) {
                discName = paths[i].Trim().Substring(0, 1);
                freeSpace = GetHardDiskFreeSpace(discName);
                if (freeSpace > 0) {
                    PathNumber = i;
                    resultPath = paths[i].Trim();
                    break;
                }
            }

            return resultPath;
        }

        public string GetSourcePath(string userName, string fileName) {
            string resultPath = string.Empty;
            string path = string.Empty;

            string[] paths = SourcePaths;
            for (int i = 0; i < paths.Length; i++) {
                path = paths[i];
                if (!string.IsNullOrEmpty(userName))
                    path += "/" + userName;
                if(!string.IsNullOrEmpty(fileName))
                    path += "/" + fileName;
                //path = paths[i] + "/" + userName + "/" + fileName;
                if (File.Exists(path)) {
                    PathNumber = i;
                    resultPath = path;
                    break;
                }
            }

            return resultPath;
        }

        public string GetPreviewPath(string userName, string fileName, string type) {
            string resultPath = string.Empty;
            string path = string.Empty;

            string[] paths = PreviewPaths;
            for (int i = 0; i < paths.Length; i++) {
                path = paths[i];
                if (!string.IsNullOrEmpty(type))
                    path += "/" + type;
                if (!string.IsNullOrEmpty(userName))
                    path += "/" + userName;
                if (!string.IsNullOrEmpty(fileName))
                    path += "/" + fileName;
                if (File.Exists(path)) {
                    resultPath = path;
                    break;
                }
            }

            return resultPath;
        }

        public string GetPreviewPathRead(string userName, string fileName, string type) {
            string resultPath = string.Empty;
            string path = string.Empty;

            string[] paths = PreviewPaths;
            for (int i = 0; i < paths.Length; i++) {
                path = paths[i];
                if (!string.IsNullOrEmpty(type))
                    path += "/" + type;
                if (!string.IsNullOrEmpty(userName))
                    path += "/" + userName;
                if (!string.IsNullOrEmpty(fileName))
                    path += "/" + fileName;
                if (File.Exists(path)) {
                    resultPath = PreviewPath_Reads[i].Trim();
                    if (!string.IsNullOrEmpty(type))
                        resultPath += "/" + type;
                    if (!string.IsNullOrEmpty(userName))
                        resultPath += "/" + userName;
                    if (!string.IsNullOrEmpty(fileName))
                        resultPath += "/" + fileName;
                    break;
                }
            }

            return resultPath;
        }

        //private bool IsUrlExist(string url) {
        //    HttpWebRequest myWebRequest = null;
        //    HttpWebResponse myWebResponse = null;
        //    try {
        //        myWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
        //        myWebRequest.Headers = "HEAD";
        //        myWebRequest.Timeout = 1000;

        //        myWebResponse = (HttpWebResponse)myWebRequest.GetResponse();

        //        return myWebResponse.StatusCode == HttpStatusCode.OK;
        //    }
        //    catch {
        //        return false;
        //    }
        //    finally {
        //        if (myWebResponse != null) {
        //            myWebResponse.Close();
        //            myWebResponse = null;
        //        }
        //        if (myWebRequest != null) {
        //            myWebRequest.Abort();
        //            myWebRequest = null;
        //        }
        //    }

        //}

        public long GetHardDiskFreeSpace(string hardDiskName) {
            long freeSpace = new long();
            hardDiskName = hardDiskName + ":\\";
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives) {
                if (drive.Name == hardDiskName.ToUpper()) {
                    freeSpace = drive.TotalFreeSpace / (1024 * 1024 * 1024);
                }
            }
            return freeSpace;
        }
    }
}
