using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace QJVRMS.Common
{
    /// <summary>
    /// ��Ƶ�ļ������࣬�������롢��ʽת������ͼ������ffmpeg��mencoderʵ��
    /// Created By ciqq 2010-3-26
    /// </summary>
    public class VideoController
    {
        private string ffmpegPath = string.Empty;
        private string mencoderPath = string.Empty;

        private const string cmdWatermark = "";
        public VideoController()
        { 
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="ffmpegPath"> ����ffmpegӦ�ó����·������ </param>
        public VideoController(string ffmpegPath)
        {
            this.ffmpegPath = ffmpegPath;
        }
        public VideoController(string ffmpegPath, string mencoderPath)
        {
            this.ffmpegPath = ffmpegPath;
            this.mencoderPath = mencoderPath;
        }


        /// <summary>
        /// ���� ffmpeg ����
        /// </summary>
        /// <param name="arg">����Ĳ���</param>
        /// <returns>ffmpeg�ķ�������</returns>
        public string  runFfmpeg(string arg)
        {
            string _ret = string.Empty;
            if (File.Exists(ffmpegPath))
            {                
                ProcessStartInfo psi = new ProcessStartInfo(ffmpegPath,arg);
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.RedirectStandardError = true;
                psi.RedirectStandardOutput = true;
                try
                {
                    Process ffmpegProcess = new Process();
                    ffmpegProcess.StartInfo = psi;
                    ffmpegProcess.Start();
                    ffmpegProcess.WaitForExit();
                    _ret = ffmpegProcess.StandardError.ReadToEnd();
                }
                catch (Exception)
                {
                    
                }                
            }
            return _ret;            
        }

        /// <summary>
        /// ���� ffmpeg ���򣬲����ؽ��
        /// </summary>
        /// <param name="arg">����Ĳ���</param>
        /// <returns>ffmpeg�ķ�������</returns>
        public void runFfmpegNoResult(string arg)
        {
            if (File.Exists(ffmpegPath))
            {
                ProcessStartInfo psi = new ProcessStartInfo(ffmpegPath, arg);
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                try
                {
                    Process ffmpegProcess = new Process();
                    ffmpegProcess.StartInfo = psi;
                    ffmpegProcess.Start();
                    ffmpegProcess.WaitForExit();
                }
                catch (Exception)
                {

                }
            }
        }






        public void runExe(string exeFilePath)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = exeFilePath;
            psi.UseShellExecute = true;

            try
            {
                Process p = new Process();
                p.StartInfo = psi;
                
                p.Start();
            }

            catch
            { }
        
        
        }


        /// <summary>
        /// ��ȡһ����Ƶ�ļ�����Ϣ��ͨ��ffmpeg -i ��ȡ
        /// </summary>
        /// <param name="videoPath">��Ƶ�ļ�������·��</param>
        /// <returns>����һ����ϣ������ļ�����Ϣ��Duration Bitrate VideoSize</returns>
        public VideoFile getVideoInfo(string videoPath)
        {
            VideoFile v = new VideoFile();
            
            string _ret = runFfmpeg(string.Format("-i {0}",videoPath));
            if (!string.IsNullOrEmpty(_ret))
            {
                string p = @"[D|d]uration:(?<duration>.+?),";
                Match m = Regex.Match(_ret, p);
                if (m.Success)
                {
                    string duration = m.Groups["duration"].Value.Trim();
                    string[] times=duration.Split(new char[]{':','.'});
                    if (times.Length == 4)
                    {
                        TimeSpan dur= new TimeSpan(0, Convert.ToInt16(times[0]), Convert.ToInt16(times[1]), Convert.ToInt16(times[2]), Convert.ToInt16(times[3]));
                        v.ClipLength = dur.ToString();
                    }                    
                }

                p = @"bitrate:(?<bitrate>.+?s)";
                m = Regex.Match(_ret, p);
                if (m.Success)
                {
                    v.Bitrate = m.Groups["bitrate"].Value.Trim();                    
                }

                p = @"(?<videosize>(\d{2,4}x\d{2,4}))";
                m = Regex.Match(_ret, p);
                if (m.Success)
                {
                    
                    v.ClipSize = m.Groups["videosize"].Value.Trim();
                }
                //������Ϣ�������������ȡ��
                /*
                 filename,
                 * video_format
                 * video_bitrate
                 * video_width
                 * video_height
                 * video_fps
                 * video_aspect
                 * audio_format
                 * audio_bitrate
                 * audio_rate
                 * audio_nch
                 * length
                 * video_codec
                 * audio_codec
                 */
            }
            return v;
        }


        public void getThumbnails()
        {
 
        
        }
        //private Delegate d=
        private string  doConvert()
        {
           // Console.WriteLine("Hello World");
            return "sss";        
        }

        public void ConvertToFlv(string sourceFilePath,string flvFilePath)
        {
            string cmd = string.Empty;
            if (sourceFilePath.ToLower().EndsWith(".avi___"))
            {
                cmd = string.Format(" -i {0} -y -ab 32 -ar 22050 -f flv -s 480x360 {1}", sourceFilePath, flvFilePath);

            }
            else
            {
                cmd = string.Format(" -i {0} -y -ab 32 -ar 22050 -b 800k {1}", sourceFilePath, flvFilePath);
            }
            //return runFfmpeg(cmd);        
            runFfmpegNoResult(cmd);
        }

        public void ConvertToFlvByMencoder(string sourceFilePath, string flvFilePath)
        {
            string cmd = string.Empty;
            cmd = "";

        
        }




        public void ConvertToSmallFlv(string sourceFilePath, string flvFilePath)
        {
            string cmd = string.Empty;
            if (sourceFilePath.ToLower().EndsWith(".avi___"))
            {
                cmd = string.Format(" -i {0} -y -ab 32 -ar 22050 -f flv -s 480x360 {1}", sourceFilePath, flvFilePath);

            }
            else
            {
                cmd = string.Format(" -i {0} -y -ab 32 -ar 22050 -f flv -s  176x144 {1}", sourceFilePath, flvFilePath);
            }
            //return runFfmpeg(cmd);        
            runFfmpegNoResult(cmd);
        }


        public void ConvertToSWF(string sourceFilePath, string swfFilePath)
        {
            string cmd = string.Empty;
            if (sourceFilePath.ToLower().EndsWith(".avi___"))
            {
                cmd = string.Format(" -i {0} -y -ab 32 -ar 22050 -f flv -s 480x360 {1}", sourceFilePath, swfFilePath);

            }
            else
            {
                cmd = string.Format(" -i {0} -y -ab 32 -ar 22050  -s 176x144 {1}", sourceFilePath, swfFilePath);
            }
            //return runFfmpeg(cmd);        
            runFfmpegNoResult(cmd);
        }


        public string ConvertToThumbnails(string sourceFilePath, string imgFilePath)
        {
            string cmd = string.Format("-i {0} -y -f image2 -ss 1 -s 170x128 {1}", sourceFilePath, imgFilePath);
            return runFfmpeg(cmd);
        }

        public string ConvertToThumbnails(string sourceFilePath, string imgFilePath,double startSecond)
        {
            string cmd = string.Format("-i {0} -y -f image2 -ss {2} -s 170x128 {1}", sourceFilePath, imgFilePath,startSecond.ToString());
            return runFfmpeg(cmd);
        }


        /// <summary>
        /// ������Ƶ��ʽ������ *.avi,*.flv,*.wmv
        /// </summary>
        /// <returns></returns>
        public string GetVideoFormats()
        {
            StringBuilder sb = new StringBuilder("");
            string[] _arr = Enum.GetNames(typeof(VideoFormat));
            int ilength=_arr.Length;
            for (int i = 0; i < ilength; i++)
            {
                sb.Append("*." + _arr[i]);
                if (i < ilength - 1)
                {
                    sb.Append(";");
                }
            }
            return sb.ToString().Replace("_", "").ToLower(); ;
        }

    }

    
    public class VideoFile
    {
        private string _path;
        //public string Path { get;set;}

        private string _clipLength;
        private string _clipSize;
        private string _bitrate;


        public string ClipLength
        {
            get { return _clipLength; }
            set { _clipLength = value; }
        }

        public string ClipSize
        {
            get { return _clipSize; }
            set { _clipSize = value; }
        }

        public string Bitrate
        {
            set { _bitrate = value; }
            get { return _bitrate; }
        }

        public string Path
        { 
            get{return _path;}
            set { _path = value; }
        }
    }

    /// <summary>
    /// ��������Ƶ��ʽ
    /// </summary>
    public enum VideoFormat
    { 
        _3GP,
        ASF,
        AVI,
        DAT,
        DIVX,
        FLV,
        MOV,
        MP4,
        MPEG,
        MPG,
        RA,
        RM,
        RMVB,        
        SWF,
        WMV    
    }

    /// <summary>
    /// ��Ƶת��״̬
    /// </summary>
    public enum VideoStatus
    { 
        UnConverted=0, 
        Converted=1,
        ConvertError=2
    }
}
