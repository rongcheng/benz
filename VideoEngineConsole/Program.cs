using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using QJVRMS.Common; //������ common �е� VideoController ��
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


            if (!initData()) //��ʼ�����ݣ���Ҫ�Ƕ�ȡ������Ϣ
            {
                return;
            }
            if (isRunning()) //�������Ƿ��������� 
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
        /// ��ʼ������
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
                writeLog(logFile, "�����ļ����Ҳ��� videoRootPath �ڵ�");
                return false;
            }
            else if (!Directory.Exists(videoRootPath))
            {
                Directory.CreateDirectory(videoRootPath);
            }

            if (string.IsNullOrEmpty(videoPreviewPath))
            {
                writeLog(logFile, "�����ļ����Ҳ��� videoPreviewPath �ڵ�");
                return false;
            }
            else if (!Directory.Exists(videoPreviewPath))
            {
                Directory.CreateDirectory(videoPreviewPath);
            }

            if ((!File.Exists(ffmpegFilePath)) && (!File.Exists(ffmpegRmFilePath)))
            {
                writeLog(logFile, string.Format("������ִ�г��򶼲����ڣ�{0}��{1}", ffmpegFilePath, ffmpegRmFilePath));
                return false ;
            }

            if (string.IsNullOrEmpty(specialVideoFormats))
            {
                specialVideoFormats = ".rm,.rmvb,.ra,.3gp";
            }

            return true;
            
        }

        /// <summary>
        /// ��������е���Ƶ
        /// </summary>
        private static void ConvertFromQueue()
        {
            writeLog("���ڻ�ȡ��Ҫת������Ƶ����");
            int i = 0;
            int iCount = 2; //ѭ�� iCount ��
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
        /// ת��һ����Ƶ�ļ�
        /// </summary>
        /// <param name="itemSerialNumber"></param>
        /// <param name="videoFilePath"></param>
        /// <param name="userName"></param>
        private static void ConvertVideo(string itemSerialNumber,string videoFilePath,string userName)
        {
            writeLog(string.Format("������ͼת��{0}",videoFilePath));
            bool isCompleted = false;
            string errorMessage = string.Empty;
            
            try
            {
                //תflv
                //ȡ�ļ���Ϣ
                //��ͼ
                //תflvС����Ƶ                
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

                writeLog(string.Format("ʹ�����Exe�ļ�ת����{0}", exeFilePath+fileExt));
                if (File.Exists(exeFilePath))
                { 
                    VideoController vc = new VideoController(exeFilePath);

                    if (fileExt.EndsWith("flv"))
                    {
                        //flv�ļ�ֻ������ȥ����
                        File.Copy(videoFilePath, flvFilePath,true);
                    }
                    else
                    {
                        //ת����flv
                        vc.ConvertToFlv(videoFilePath, flvFilePath);
                    }

                    //û��ת��flv
                    if (!File.Exists(flvFilePath) || new FileInfo(flvFilePath).Length < 10)
                    {
                        File.Delete(flvFilePath);

                        //������ mencoder ��ת�����ⲿ�������ţ��������ж��ٲ��ɹ���

                    }
                    else
                    {
                        //��ͼ��ȡ��һ֡ͼ��
                        long imageSize=0;
                        double startSecond = 0.001;
                        do
                        {
                            writeLog("��ͼ\r\n"+ vc.ConvertToThumbnails(videoFilePath, imgFilePath,startSecond));
                            imageSize = new FileInfo(imgFilePath).Length;
                            startSecond = startSecond + 1;
                        }
                        while(imageSize<4000 && startSecond<11); 
                        //��ͼƬС��4000�ֽ�ʱ����ΪͼƬ��ɫ���ڵ�һ������һ�����ȡͼ����ȡ10�Σ�
                        //Ҳ��������ͼƬ�Ҷ������Ƚ�
                        

                        //ת��С��flv
                        vc.ConvertToSmallFlv(flvFilePath, smallFlvFilePath);
                    }

                    //ȡ�ļ���Ϣ
                    VideoFile v =  vc.getVideoInfo(videoFilePath);
                    writeLog("��ȡ�ļ���MetaData"+v.ClipLength);
                   
                    //д�ط�����
                    WS.VideoStorageService vss = new WS.VideoStorageService();
                    //vss.UpdateVideoMetaData(itemSerialNumber, v.ClipLength, v.Bitrate, v.ClipSize);
                    
                    Dictionary<string, string> dct = new Dictionary<string, string>();

                    //ͨ������õ����еĸ��������Լ���Ӧ��ֵ
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

                    //��������µ����ݿ���
                    vss.UpdateVideoStatus(itemSerialNumber, nowStatus);
                }
                else
                {
                    errorMessage = string.Format("��ת���ļ�{0}ʱ��û���ҵ���ִ�г���{1}",videoFilePath,exeFilePath);
                    writeLog(logFile, errorMessage);
                }
            }
            catch(Exception e1)
            {
                writeLog(logFile,e1.StackTrace+ e1.Message);
            }
           
        
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns></returns>
        private static DataTable GetQueue()
        {
            WS.VideoStorageService vss = new WS.VideoStorageService();
            DataSet ds = vss.GetUnConvertedVideos();
            return  ds.Tables[0];        
        }


        /// <summary>
        /// �ж��Ƿ��иý�����������
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
        /// ���ô����Ƿ�ɼ�
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
        /// ��־��д��һ����¼
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
