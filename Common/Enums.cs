using System;
using System.Collections.Generic;
using System.Text;

namespace QJVRMS.Common
{
    public class Enums
    {
        /// <summary>
        /// 返回视频格式，形如 *.avi,*.flv,*.wmv
        /// </summary>
        /// <returns></returns>
        public static string GetVideoFormats()
        {
            StringBuilder sb = new StringBuilder("");
            string[] _arr = Enum.GetNames(typeof(enumVideoFormat));
            int ilength = _arr.Length;
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


        /// <summary>
        /// 返回图片格式，形如 *.png,*.gif,*.jpg
        /// </summary>
        /// <returns></returns>
        public static string GetImageFormats()
        {
            StringBuilder sb = new StringBuilder("");
            string[] _arr = Enum.GetNames(typeof(enumImageFormat));
            int ilength = _arr.Length;
            for (int i = 0; i < ilength; i++)
            {
                sb.Append("*." + _arr[i]);
                if (i < ilength - 1)
                {
                    sb.Append(";");
                }
            }
            return sb.ToString().ToLower(); 
        }


        /// <summary>
        /// 返回文档格式，形如 *.doc,*.txt,*.ppt
        /// </summary>
        /// <returns></returns>
        public static string GetDocumentFormats()
        {
            StringBuilder sb = new StringBuilder("");
            string[] _arr = Enum.GetNames(typeof(enumDocumentFormat));
            int ilength = _arr.Length;
            for (int i = 0; i < ilength; i++)
            {
                sb.Append("*." + _arr[i]);
                if (i < ilength - 1)
                {
                    sb.Append(";");
                }
            }
            return sb.ToString().ToLower();
        }



    }

    /// <summary>
    /// 常见的图片格式
    /// </summary>
    public enum enumImageFormat
    { 
        JPG,
        JPEG,
        GIF,
        TIFF,
        PNG,
        BMP,
        PCX,
        TGA,
        EXIF,
        FPX        
    }


    /// <summary>
    /// 常见的视频格式
    /// </summary>
    public enum enumVideoFormat
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
    /// 常见的文档格式
    /// </summary>
    public enum enumDocumentFormat
    {
        PDF,
        TXT,
        DOC,
        XLS,
        PPT
    }



    /// <summary>
    /// 视频转换状态
    /// </summary>
    public enum enumVideoStatus
    {
        UnConverted = 0,
        Converted = 1,
        ConvertError = 2
    }
}
