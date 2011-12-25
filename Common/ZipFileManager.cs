using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace QJVRMS.Common
{

    /// <summary>
    /// 压缩图片文件管理
    /// </summary>
    public class ZipFileManager
    {

        public static readonly string ZipFolderPath = AppDomain.CurrentDomain.BaseDirectory + "ZipTemp";
        public static readonly string ZipHighFolderPath = Path.Combine(ZipFolderPath, "high");
        public static readonly string ZipLowFolderPath = Path.Combine(ZipFolderPath, "low");
        public static readonly string ZipTenFolderPath = Path.Combine(ZipFolderPath, "10m");
        public static readonly string ZipTwnFolderPath = Path.Combine(ZipFolderPath, "20m");


        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public static DirectoryInfo CreateFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                return Directory.CreateDirectory(folderPath);


            return new DirectoryInfo(folderPath);
        }


        public static void CreateFolder()
        {
            //if (!Directory.Exists(ZipFolderPath))
            // {

            Directory.CreateDirectory(ZipFolderPath);

            Directory.CreateDirectory(ZipHighFolderPath);
            Directory.CreateDirectory(ZipLowFolderPath);
            Directory.CreateDirectory(ZipTenFolderPath);
            Directory.CreateDirectory(ZipTwnFolderPath);
            //  }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="delFilePath"></param>
        public static void DeleteFile(string delFilePath)
        {
            try
            {
                File.Delete(delFilePath);
            }
            catch
            {
            }
        }


        public static void DeleteFolder(string folderPath)
        {
            try
            {
                //foreach(string filePath in  Directory.GetFileSystemEntries(folderPath))
                //{

                //}

                Directory.Delete(folderPath, true);
            }
            catch
            {
            }
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="aimFilePath"></param>
        /// <param name="fileByte"></param>
        /// <returns></returns>
        public static bool CreateFile(string aimFilePath, byte[] fileByte)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(aimFilePath, FileMode.Create);
                fs.Write(fileByte, 0, fileByte.Length);

            }
            catch
            {
                return false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close(); fs.Dispose();
                }
            }

            return true;
        }


    }
}
