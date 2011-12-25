using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using QJVRMS.Common; //依赖于 common 中的 VideoController 类
using System.Collections;
using System.Reflection;

namespace VideoEngineConsole
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        private static string logFile = string.Empty;
        private static string videoRootPath = string.Empty;
        private static string videoPreviewPath = string.Empty;
        private static string ffmpegFilePath = string.Empty;
        private static string ffmpegRmFilePath = string.Empty;
        private static string mencoderFilePath = string.Empty;
        private static string specialVideoFormats = string.Empty;


        static void Main(string[] args)
        {


            if (!initData()) //初始化数据，主要是读取配置信息
            {
                return;
            }
            if (isRunning()) //检测程序是否正在运行 
            {
                return;
            }

            try
            {
                ConvertFromQueue();
            }
            catch(Exception e1)
            {
                writeLog(logFile, e1.Source+e1.Message);
                return;
            }
        }


        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <returns></returns>
        private static bool initData()
        {
            string logFilePath = ConfigurationManager.AppSettings["logFilePath"];
            if (string.IsNullOrEmpty(logFile))
            {
                logFilePath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            }
            else
            {
                if (!Directory.Exists(logFilePath))
                {
                    try
                    {
                        Directory.CreateDirectory(logFilePath);
                    }
                    catch
                    { }
                }
            }

            logFile             = Path.Combine(logFilePath, DateTime.Now.ToString("yyyyMMddHH") + ".txt");
            videoRootPath       = ConfigurationManager.AppSettings["videoRootPath"];
            videoPreviewPath    = ConfigurationManager.AppSettings["videoPreviewPath"];
            ffmpegFilePath      = ConfigurationManager.AppSettings["ffmpegFilePath"];
            ffmpegRmFilePath    = ConfigurationManager.AppSettings["ffmpegRmFilePath"];
            mencoderFilePath    = ConfigurationManager.AppSettings["mencoderFilePath"];
            specialVideoFormats = ConfigurationManager.AppSettings["specialVideoFormats"];
            
            if (string.IsNullOrEmpty(videoRootPath))
            {
                writeLog(logFile, "配置文件中找不到 videoRootPath 节点");
                return false;
            }
            else if (!Directory.Exists(videoRootPath))
            {
                Directory.CreateDirectory(videoRootPath);
            }

            if (string.IsNullOrEmpty(videoPreviewPath))
            {
                writeLog(logFile, "配置文件中找不到 videoPreviewPath 节点");
                return false;
            }
            else if (!Directory.Exists(videoPreviewPath))
            {
                Directory.CreateDirectory(videoPreviewPath);
            }

            if ((!File.Exists(ffmpegFilePath)) && (!File.Exists(ffmpegRmFilePath)))
            {
                writeLog(logFile, string.Format("两个可执行程序都不存在，{0}和{1}", ffmpegFilePath, ffmpegRmFilePath));
                return false ;
            }

            if (string.IsNullOrEmpty(specialVideoFormats))
            {
                specialVideoFormats = ".rm,.rmvb,.ra,.3gp";
            }

            return true;
            
        }

        /// <summary>
        /// 处理队列中的视频
        /// </summary>
        private static void ConvertFromQueue()
        {
            writeLog("正在获取需要转换的视频队列");
            int i = 0;
            int iCount = 2; //循环 iCount 次
            string videoFullFilePath = string.Empty;

            while (i<iCount)
            {
                DataTable dt = GetQueue();
                if (dt.Rows.Count == 0)
                {
                    return;
                }
                foreach (DataRow dr in dt.Rows)
                {
                    videoFullFilePath = Path.Combine(videoRootPath, dr["ServerFolderName"].ToString());
                    videoFullFilePath = Path.Combine(videoFullFilePath, dr["ServerFileName"].ToString());
                    ConvertVideo(dr["ItemSerialNumber"].ToString(), videoFullFilePath, dr["ServerFolderName"].ToString());
                    
                }
                i++;
            }
        
        }

        /// <summary>
        /// 转换一个视频文件
        /// </summary>
        /// <param name="itemSerialNumber"></param>
        /// <param name="videoFilePath"></param>
        /// <param name="userName"></param>
        private static void ConvertVideo(string itemSerialNumber,string videoFilePath,string userName)
        {
            writeLog(string.Format("正在试图转换{0}",videoFilePath));
            bool isCompleted = false;
            string errorMessage = string.Empty;
            
            try
            {
                //转flv
                //取文件信息
                //截图
                //转flv小的视频                
                int nowStatus = (int)VideoStatus.UnConverted ;
                string exeFilePath = string.Empty;
                string fileExt = Path.GetExtension(videoFilePath).ToLower();
                string flvFilePath=Path.Combine(Path.Combine(videoPreviewPath,"Flv"),userName);
                string imgFilePath = Path.Combine(Path.Combine(videoPreviewPath, "image"), userName);
                string swfFilePath = Path.Combine(Path.Combine(videoPreviewPath, "swf"), userName);
                string smallFlvFilePath = Path.Combine(Path.Combine(videoPreviewPath, "smallFlv"), userName);
                if (!Directory.Exists(flvFilePath))
                {
                    Directory.CreateDirectory(flvFilePath);
                }
                if (!Directory.Exists(imgFilePath))
                {
                    Directory.CreateDirectory(imgFilePath);
                }
                if (!Directory.Exists(swfFilePath))
                {
                    Directory.CreateDirectory(swfFilePath);
                }
                if (!Directory.Exists(smallFlvFilePath))
                {
                    Directory.CreateDirectory(smallFlvFilePath);
                }

                flvFilePath = Path.Combine(flvFilePath,itemSerialNumber+".flv");
                imgFilePath = Path.Combine(imgFilePath, itemSerialNumber +".jpg");
                swfFilePath = Path.Combine(swfFilePath, itemSerialNumber + ".swf");
                smallFlvFilePath = Path.Combine(smallFlvFilePath, itemSerialNumber + ".flv");
                
                if (specialVideoFormats.Contains(fileExt))
                {
                    exeFilePath = ffmpegRmFilePath;
                }
                else
                {
                    exeFilePath = ffmpegFilePath;
                }

                writeLog(string.Format("使用这个Exe文件转换，{0}", exeFilePath+fileExt));
                if (File.Exists(exeFilePath))
                { 
                    VideoController vc = new VideoController(exeFilePath);

                    if (fileExt.EndsWith("flv"))
                    {
                        //flv文件只拷贝过去即可
                        File.Copy(videoFilePath, flvFilePath,true);
                    }
                    else
                    {
                        //转换成flv
                        vc.ConvertToFlv(videoFilePath, flvFilePath);
                    }

                    //没有转成flv
                    if (!File.Exists(flvFilePath) || new FileInfo(flvFilePath).Length < 10)
                    {
                        File.Delete(flvFilePath);

                        //尝试用 mencoder 来转换，这部分先留着，看看能有多少不成功的

                    }
                    else
                    {
                        //截图，取第一帧图像
                        long imageSize=0;
                        double startSecond = 0.001;
                        do
                        {
                            writeLog("截图\r\n"+ vc.ConvertToThumbnails(videoFilePath, imgFilePath,startSecond));
                            imageSize = new FileInfo(imgFilePath).Length;
                            startSecond = startSecond + 1;
                        }
                        while(imageSize<4000 && startSecond<11); 
                        //当图片小于4000字节时，认为图片颜色过于单一，在下一秒继续取图，共取10次，
                        //也可以利用图片灰度来做比较
                        

                        //转成小的flv
                        vc.ConvertToSmallFlv(flvFilePath, smallFlvFilePath);
                    }

                    //取文件信息
                    VideoFile v =  vc.getVideoInfo(videoFilePath);
                    writeLog("获取文件的MetaData"+v.ClipLength);
                   
                    //写回服务器
                    WS.VideoStorageService vss = new WS.VideoStorageService();
                    //vss.UpdateVideoMetaData(itemSerialNumber, v.ClipLength, v.Bitrate, v.ClipSize);
                    
                    Dictionary<string, string> dct = new Dictionary<string, string>();

                    //通过反射得到所有的附加属性以及相应的值
                    Type t = v.GetType();
                    PropertyInfo[] piArray = t.GetProperties();
                    foreach (PropertyInfo pi in piArray)
                    {
                        if (pi.GetValue(v, null) != null)
                        {
                            dct.Add(pi.Name, pi.GetValue(v, null).ToString());
                        }
                    }
                    
                    List<ResourceWS.DictionaryEntry> lst = new List<ResourceWS.DictionaryEntry>();

                    foreach (string key in dct.Keys)
                    {
                        ResourceWS.DictionaryEntry de = new ResourceWS.DictionaryEntry();
                        de.Key = key;
                        de.Value = dct[key];
                        lst.Add(de);
                    }
                    ResourceWS.DictionaryEntry[] result = lst.ToArray();                    
                    ResourceWS.ResourceService rs = new ResourceWS.ResourceService();
                    rs.insertResourceAttributes(itemSerialNumber, result);
                    if (File.Exists(flvFilePath) && File.Exists(imgFilePath))
                    {
                        nowStatus = (int)VideoStatus.Converted;
                    }
                    else
                    {
                        nowStatus = (int)VideoStatus.ConvertError;
                    }

                    //将结果更新到数据库中
                    vss.UpdateVideoStatus(itemSerialNumber, nowStatus);
                }
                else
                {
                    errorMessage = string.Format("在转换文件{0}时，没有找到可执行程序：{1}",videoFilePath,exeFilePath);
                    writeLog(logFile, errorMessage);
                }
            }
            catch(Exception e1)
            {
                writeLog(logFile,e1.StackTrace+ e1.Message);
            }
           
        
        }

        /// <summary>
        /// 获取队列
        /// </summary>
        /// <returns></returns>
        private static DataTable GetQueue()
        {
            WS.VideoStorageService vss = new WS.VideoStorageService();
            DataSet ds = vss.GetUnConvertedVideos();
            return  ds.Tables[0];        
        }


        /// <summary>
        /// 判断是否有该进程正在运行
        /// </summary>
        /// <returns></returns>
        private static bool isRunning()
        {
            Process p = System.Diagnostics.Process.GetCurrentProcess();
            Process[] pb = Process.GetProcessesByName(p.ProcessName);
            if (pb.Length == 1)
            {
                return false;
            }
            return true;        
        }



        /// <summary>
        /// 设置窗口是否可见
        /// </summary>
        /// <param name="visible"></param>
        /// <param name="title"></param>
        public static void setConsoleWindowVisibility(bool visible, string title)
        {
            IntPtr hWnd = FindWindow(null, title);
            if (hWnd != IntPtr.Zero)
            {
                if (!visible)            
                    ShowWindow(hWnd, 0);             
                else               
                    ShowWindow(hWnd, 1); 
            }
        }



        /// <summary>
        /// 日志，写入一条记录
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="message"></param>
        private static void writeLog(string filePath, string message)
        {
            FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine( DateTime.Now+"\t"+ message);
            sw.Close();
            fs.Close();        
        }

        private static void writeLog(string message)
        {
            writeLog(logFile, message);
        }

    }
}
