using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;
using QJVRMS.Business.Interface;

namespace QJVRMS.Business.ResourceType
{
    /// <summary>
    /// 文档类型的相关设置
    /// </summary>
    public class DocumentType : IResourceType
    {
        public string SourcePath
        {
            get
            {
                return ConfigurationManager.AppSettings["documentSourcePath"];
            }
        }

        public string[] SourcePaths {
            get {
                return ConfigurationManager.AppSettings["documentSourcePath"].Split(',');
            }
        }

        public string PreviewPath
        {
            get
            {
                return ConfigurationManager.AppSettings["documentPreviewPath"];
            }
        }

        public string[] PreviewPaths {
            get {
                return ConfigurationManager.AppSettings["documentPreviewPath"].Split(',');
            }
        }

        public string[] PreviewPath_Reads {
            get {
                return null;
            }
        }

        public string DetailPage
        {
            get
            {
                return "/OtherDetail.aspx";
            }
        }

        public string ResourceType
        {
            get
            {
                return "document";
            }
        }

        public string ResourceSNPrefix
        {
            get
            {
                return "DOC";
            }
        }

        //public string[] FileExtention
        //{
        //    get
        //    {
        //        return new string[] { };
        //    }
        //}
        public int PathNumber { get; set; }
        public string[] FileExtention
        {
            get
            {
                int i = 0;
                string[] ret = new string[5];
                ret[i++] = "PDF";
                ret[i++] = "TXT";
                ret[i++] = "DOC";
                ret[i++] = "XLS";
                ret[i++] = "PPT";
                return ret;
            }
        }

        public string GetSourcePath() {
            string resultPath = string.Empty;
            string discName = string.Empty;
            long freeSpace = 0;

            string[] paths = SourcePaths;
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
                if (!string.IsNullOrEmpty(fileName))
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
