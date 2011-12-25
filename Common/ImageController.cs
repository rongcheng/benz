using System;
using System.Collections.Generic;
using System.Text;
//using ImageMagickObject;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace QJVRMS.Common
{
    public enum ZPRI
    {
        Normal,         //不做变化
        HighQuality,    //平滑
        HighSpeed,      //锐度
    }

    public enum AliasPos
    {
        RightTop,       //右上
        Center,         //居中
        LeftTop,        //左上

    }

    public class ImageController 
    {

        private Image m_Image;
        public ImageController(string SrcImagePath)
        {
            m_Image = Image.FromFile(SrcImagePath);
        }

        public ImageController()
        { }

        public bool ZoomIn(int MaxLength, ZPRI intMode)
        {
            int toHeight, toWidth;   //目标尺寸
            if ((MaxLength <= m_Image.Height) || (MaxLength <= m_Image.Width))
            {
                if (m_Image.Height >= m_Image.Width)
                {
                    toHeight = MaxLength;
                    toWidth = m_Image.Width * MaxLength / m_Image.Height;
                }
                else
                {
                    toWidth = MaxLength;
                    toHeight = m_Image.Height * MaxLength / m_Image.Width;
                }

                Bitmap bitmap = new System.Drawing.Bitmap(toWidth, toHeight);
                //新建一个画板

                //设置分辨率
                if (MaxLength < 1500)
                    bitmap.SetResolution(72, 72);

                Graphics g = Graphics.FromImage(bitmap);

                //设置高质量二次线形插值法
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                //设置高质量,低速度呈现平滑程度
                switch (intMode)
                {
                    case ZPRI.Normal:
                        g.SmoothingMode = SmoothingMode.None;
                        break;
                    case ZPRI.HighQuality:
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        break;
                    case ZPRI.HighSpeed:
                        g.SmoothingMode = SmoothingMode.HighSpeed;
                        break;
                }

                //清空画布并以透明背景色填充
                g.Clear(Color.Transparent);

                //在指定位置并且按指定大小绘制原图片
                g.DrawImage(m_Image, new Rectangle(0, 0, toWidth, toHeight),
                    new Rectangle(0, 0, m_Image.Width, m_Image.Height),
                    GraphicsUnit.Pixel);

                m_Image = (Image)bitmap;
                g.Dispose();
                return true;
            }
            else
                return false;
        }

        public static ArrayList ToZipImage(ArrayList sourceFileList, ArrayList aimFileList, int MaxLength)
        {
            if (sourceFileList.Count != aimFileList.Count)
            {
                throw new System.ArgumentException("压缩文件数量不匹配!");
            }


            ArrayList successZipList = new ArrayList(sourceFileList.Count);

            for (int i = 0; i < sourceFileList.Count; i++)
            {
                ImageController jpg = null;
                byte[] buffer = null;

                try
                {
                    jpg = new ImageController(sourceFileList[i].ToString());

                    //jpg.ZoomIn(MaxLength, ZPRI.HighSpeed);
                    //2010-3-22 ciqq 高质量压缩
                    jpg.ZoomIn(MaxLength, ZPRI.HighQuality); 

                    buffer = jpg.FinalImage;

                    ZipFileManager.CreateFile(aimFileList[i].ToString(), buffer);

                    successZipList.Add(sourceFileList[i].ToString());
                }
                catch
                {
                }
                finally
                {
                    if (jpg != null)
                        jpg.Dispose();
                }
            }

            GC.Collect();

            return successZipList;
        }
        /// <summary>
        /// 将指定图片按照指定图片大小压缩
        /// </summary>
        /// <param name="srcFilePath">原始图片路径</param>
        /// <param name="toImageSize">目标图片容量</param>
        /// <param name="toFilePath">目标图片存储位置</param>
        /// <returns></returns>
        //public static bool ConvertImageToSize(string srcFilePath, float toImageSize, string toFilePath)
        //{
        //    Image srcImage = Image.FromFile(srcFilePath);


        //    try
        //    {
        //        float srcImageSrc = GetImageSize(srcImage);
        //        if (srcImageSrc < toImageSize)
        //        {
        //            return false;
        //        }

        //        float toHeight = (int)(srcImage.Height * Math.Sqrt(toImageSize) / Math.Sqrt(srcImageSrc));
        //        float toWidth = (int)(srcImage.Width * Math.Sqrt(toImageSize) / Math.Sqrt(srcImageSrc));

        //        ImageMagickObject.MagickImageClass mic = new ImageMagickObject.MagickImageClass();
        //        object[] obj = { srcFilePath, "-resize", toWidth, toFilePath };
        //        mic.Convert(ref obj);

        //        return true;

        //    }
        //    catch(Exception ex)
        //    {
        //         LogWriter.WriteExceptionLog(ex, true);

        //        return false;
        //    }
        //    finally
        //    {
        //        if (srcImage != null)
        //            srcImage.Dispose();
        //    }
        //}


        ///// <summary>
        ///// 按最大边长压缩
        ///// </summary>
        ///// <param name="srcFilePath">原始图片路径</param>
        ///// <param name="toImageScale">最大边长</param>
        ///// <param name="toFilePath">目标图片存储位置</param>
        ///// <returns></returns>
        //public static bool ConvertImageToScale(string srcFilePath, int toImageScale, string toFilePath)
        //{
        //    Image srcImage = Image.FromFile(srcFilePath);


        //    try
        //    {
            
        //        ImageMagickObject.MagickImageClass mic = new ImageMagickObject.MagickImageClass();
        //        object[] obj = { "-resize", toImageScale.ToString()+"x"+  toImageScale.ToString(), srcFilePath, toFilePath };
        //        mic.Convert(ref obj);

        //        return true;

        //    }

        //    catch (Exception ex)
        //    {
        //         LogWriter.WriteExceptionLog(ex, true);

        //        return false;
        //    }
        //    finally
        //    {
        //        if (srcImage != null)
        //            srcImage.Dispose();
        //    }
        //}

      


        /// <summary>
        /// 获取图片容量大小
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private static float GetImageSize(Image image)
        {
            if (image == null) throw new ArgumentNullException();

            return (image.Height * image.Width) * 3 / 1024 / 1024;
        }

        public void Dispose()
        {
            this.m_Image.Dispose();
        }





        /// <summary>
        /// 垂直翻转
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationFile"></param>
        //public void Flip(string sourceFile, string destinationFile)
        //{

        //    object[] p = { sourceFile, "-flip",  destinationFile };
        //    imageMagickDoConvert(p);
        //}

        ///// <summary>
        ///// 水平翻转
        ///// </summary>
        ///// <param name="sourceFile"></param>
        ///// <param name="destinationFile"></param>
        //public void Flop(string sourceFile, string destinationFile)
        //{

        //    object[] p = { sourceFile, "-flop", destinationFile };
        //    imageMagickDoConvert(p);
        //} 



        //public void AddBorder(string sourceFile, string destinationFile,string borderColor,int borderWidth)
        //{

        //    object[] p = { sourceFile, "-bordercolor", borderColor, "-border", borderWidth.ToString(),destinationFile };
        //    imageMagickDoConvert(p);
        //} 


        //public void ToGray(string sourceFile, string destinationFile)
        //{
        //    // convert  test.png  -colorspace Gray   gray_colorspace.png
        //    object[] p = { sourceFile, "-colorspace", "Gray", destinationFile };
        //    imageMagickDoConvert(p);
        //} 

        ///// <summary>
        ///// 旋转操作
        ///// </summary>
        ///// <param name="sourceFile">原始图片文件</param>
        ///// <param name="destinationFile">目标文件</param>
        ///// <param name="degree">顺时针旋转多少度</param>
        ///// <returns></returns>
        //public string Rotate(string sourceFile, string destinationFile, int degree)
        //{
        //    object[] p = { sourceFile, "-rotate", degree.ToString(), destinationFile };
        //    return imageMagickDoConvert(p).ToString();
        //}

        //public void Resize(string sourceFile, string destinationFile, int toWidth, int toHeight, ImageResizeMode mode)
        //{
        //    string cmdParam = string.Empty;
        //    switch (mode)
        //    {
        //        case ImageResizeMode.WH:
        //            cmdParam = string.Format("{0}x{1}!", toWidth, toHeight);
        //            break;
        //        case ImageResizeMode.W:
        //            cmdParam = toWidth.ToString();
        //            break;
        //        case ImageResizeMode.H:
        //            cmdParam = string.Format("x{0}", toHeight);
        //            break;
        //        case ImageResizeMode.MaxBorder:
        //            cmdParam = string.Format("{0}x{1}", toWidth, toHeight);
        //            break;
        //    }
        //    object[] p = { sourceFile, "-resize", "100x200<", "-quality", "80", destinationFile };
        //    imageMagickDoConvert(p);
        //}

        //private object imageMagickDoConvert(object[] param)
        //{
        //    try
        //    {
        //        ImageMagickObject.MagickImageClass obj = new MagickImageClass();
        //        return obj.Convert(ref param);
        //    }
        //    catch (Exception e1)
        //    {
        //        LogWriter.WriteExceptionLog(e1, true);
        //        return false;
        //    }
            
        //}


        public enum ImageResizeMode
        {
            WH,
            W,
            H,
            MaxBorder
        }



        /// <summary>
        /// 属性：只读，返回字节数组图像
        /// </summary>
        public byte[] FinalImage
        {
            get
            {
                MemoryStream ms = new MemoryStream();
                //m_Image.Save(ms, ImageFormat.Jpeg);
                //2010-3-22 ciqq 增加编码器参数，提高图片的生成质量
                EncoderParameters encoderParams = new EncoderParameters();
                long[] quality = new long[1];
                quality[0] = 80;
                EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                encoderParams.Param[0] = encoderParam;
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICI = null;
                for (int x = 0; x<arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICI = arrayICI[x];
                        break;
                    }
                }
                if (jpegICI != null)
                {
                    m_Image.Save(ms,jpegICI,encoderParams);
                }
                else
                {
                    m_Image.Save(ms, ImageFormat.Jpeg);
                }
                return ms.ToArray();
            }
        }


        public bool isBlackImage(string imageFile)
        {
            bool _ret = false;





            return _ret;
        
        
        }



        /// <summary>
        /// 属性：只读，返回最终图像的高度
        /// </summary>
        public long Height
        {
            get { return m_Image.Height; }
        }

        /// <summary>
        /// 属性：只读，返回最终图像的宽度
        /// </summary>
        public long Width
        {
            get { return m_Image.Width; }
        }

        /// <summary>
        /// 属性：只读，返回最终图像的文件大小
        /// </summary>
        public float size
        {
            get { return (m_Image.Height * m_Image.Width) * 3 / 1024 / 1024; }
        }

    }
}
