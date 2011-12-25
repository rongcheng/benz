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
        Normal,         //�����仯
        HighQuality,    //ƽ��
        HighSpeed,      //���
    }

    public enum AliasPos
    {
        RightTop,       //����
        Center,         //����
        LeftTop,        //����

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
            int toHeight, toWidth;   //Ŀ��ߴ�
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
                //�½�һ������

                //���÷ֱ���
                if (MaxLength < 1500)
                    bitmap.SetResolution(72, 72);

                Graphics g = Graphics.FromImage(bitmap);

                //���ø������������β�ֵ��
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                //���ø�����,���ٶȳ���ƽ���̶�
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

                //��ջ�������͸������ɫ���
                g.Clear(Color.Transparent);

                //��ָ��λ�ò��Ұ�ָ����С����ԭͼƬ
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
                throw new System.ArgumentException("ѹ���ļ�������ƥ��!");
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
                    //2010-3-22 ciqq ������ѹ��
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
        /// ��ָ��ͼƬ����ָ��ͼƬ��Сѹ��
        /// </summary>
        /// <param name="srcFilePath">ԭʼͼƬ·��</param>
        /// <param name="toImageSize">Ŀ��ͼƬ����</param>
        /// <param name="toFilePath">Ŀ��ͼƬ�洢λ��</param>
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
        ///// �����߳�ѹ��
        ///// </summary>
        ///// <param name="srcFilePath">ԭʼͼƬ·��</param>
        ///// <param name="toImageScale">���߳�</param>
        ///// <param name="toFilePath">Ŀ��ͼƬ�洢λ��</param>
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
        /// ��ȡͼƬ������С
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
        /// ��ֱ��ת
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationFile"></param>
        //public void Flip(string sourceFile, string destinationFile)
        //{

        //    object[] p = { sourceFile, "-flip",  destinationFile };
        //    imageMagickDoConvert(p);
        //}

        ///// <summary>
        ///// ˮƽ��ת
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
        ///// ��ת����
        ///// </summary>
        ///// <param name="sourceFile">ԭʼͼƬ�ļ�</param>
        ///// <param name="destinationFile">Ŀ���ļ�</param>
        ///// <param name="degree">˳ʱ����ת���ٶ�</param>
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
        /// ���ԣ�ֻ���������ֽ�����ͼ��
        /// </summary>
        public byte[] FinalImage
        {
            get
            {
                MemoryStream ms = new MemoryStream();
                //m_Image.Save(ms, ImageFormat.Jpeg);
                //2010-3-22 ciqq ���ӱ��������������ͼƬ����������
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
        /// ���ԣ�ֻ������������ͼ��ĸ߶�
        /// </summary>
        public long Height
        {
            get { return m_Image.Height; }
        }

        /// <summary>
        /// ���ԣ�ֻ������������ͼ��Ŀ��
        /// </summary>
        public long Width
        {
            get { return m_Image.Width; }
        }

        /// <summary>
        /// ���ԣ�ֻ������������ͼ����ļ���С
        /// </summary>
        public float size
        {
            get { return (m_Image.Height * m_Image.Width) * 3 / 1024 / 1024; }
        }

    }
}
