//****************************************************
// 功能：JPEG图像文件处理
//       
// 作者：姚朔
// 时间：2006-4-24
// 修改记录及时间：
//****************************************************

using System;
using System.Net;
using System.Web;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


namespace QJVRMS.Common.JPEG
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

    public enum Resolution
    {
        LowRes,          //72DBI
        HighRes,         //300DPI
    }

    public class JPEGImage
    {
        private Image m_Image;

        /// <summary>
        /// 带参数的构造器(重载)
        /// 从字节数组读取图像文件
        /// </summary>
        /// <param name="SrcImageStream"></param>
        public JPEGImage(byte[] SrcImageStream)
        {
            MemoryStream memStream = new MemoryStream(SrcImageStream);
            m_Image = new Bitmap(memStream);
            memStream.Dispose();
        }

        /// <summary>
        /// 方法：缩小图片尺寸
        /// 采用双二次插值法，要求提供最大边长尺寸和缩小模式
        /// </summary>
        /// <param name="MaxLength"></param>
        /// <param name="intMode"></param>
        /// <returns></returns>
        public bool ZoomIn(int MaxLength, ZPRI intMode, Resolution Res)
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
                switch (Res)
                {
                    case Resolution.LowRes:
                        bitmap.SetResolution(72, 72);
                        break;
                    default:
                        break;
                }
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
                bitmap.Dispose();
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 方法：添加显式图像水印
        /// </summary>
        /// <param name="wmImg"></param>
        /// <param name="Position"></param>
        /// <param name="Transparency"></param>
        /// <returns></returns>
        public bool AddWaterMark(Byte[] wmImg, AliasPos Position, float Transparency)
        {
            //获取水印图像，并从字节数组转换成图像
            MemoryStream memStream = new MemoryStream(wmImg);
            Image markImg = new Bitmap(memStream);

            //创建颜色矩阵
            float[][] ptsArray ={ 
            new float[] {1, 0, 0, 0, 0},
            new float[] {0, 1, 0, 0, 0},
            new float[] {0, 0, 1, 0, 0},
            new float[] {0, 0, 0, Transparency, 0}, //注意：此处为0.0f为完全透明，1.0f为完全不透明
            new float[] {0, 0, 0, 0, 1}};

            ColorMatrix colorMatrix = new ColorMatrix(ptsArray);
            //新建一个Image属性
            ImageAttributes imageAttributes = new ImageAttributes();
            //将颜色矩阵添加到属性
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Default);

            //生成位图作图区

            Bitmap newBitmap = new Bitmap(m_Image.Width, m_Image.Height, PixelFormat.Format24bppRgb);
            //设置分辨率
            newBitmap.SetResolution(m_Image.HorizontalResolution, m_Image.VerticalResolution);
            //创建Graphics
            Graphics g = Graphics.FromImage(newBitmap);
            //消除锯齿
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //拷贝原图到作图区
            g.DrawImage(m_Image, new Rectangle(0, 0, m_Image.Width, m_Image.Height), 0, 0, m_Image.Width, m_Image.Height, GraphicsUnit.Pixel);

            //添加水印
            switch (Position)
            {
                case AliasPos.RightTop:
                    g.DrawImage(markImg, new Rectangle(m_Image.Width - markImg.Width - 10, 10, markImg.Width, markImg.Height), 0, 0, markImg.Width, markImg.Height, GraphicsUnit.Pixel, imageAttributes);
                    break;
                case AliasPos.Center:
                    g.DrawImage(markImg, new Rectangle((m_Image.Width - markImg.Width) / 2, (m_Image.Height - markImg.Height) / 2, markImg.Width, markImg.Height), 0, 0, markImg.Width, markImg.Height, GraphicsUnit.Pixel, imageAttributes);
                    break;
                case AliasPos.LeftTop:
                    g.DrawImage(markImg, new Rectangle(10, 10, markImg.Width, markImg.Height), 0, 0, markImg.Width, markImg.Height, GraphicsUnit.Pixel, imageAttributes);
                    break;
            }

            m_Image = (Image)newBitmap;

            //释放
            memStream.Dispose();
            imageAttributes.Dispose();
            g.Dispose();



            return true;
        }

        /// <summary>
        /// 写入IPTC信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool WriteInfo(string pic_id)
        {
            /*            PropertyItem ImageDescription;
                        PropertyItem DocumentName;
                        PropertyItem Copyright;
                        ImageDescription.Id = 0x010D;       //PropertyTagDocumentName 
                        ImageDescription.Type = 2;          //Specifies that Value is a null-terminated ASCII string.
                        ImageDescription.Len = pic_id.Length + 1;   //set the Len property to the length of the string including the null terminator.
                        ImageDescription.Value = (new UnicodeEncoding()).GetBytes(pic_id);   //string convert to byte[]
                        m_Image.SetPropertyItem();
                        return true;

             */
            return true;
        }

        /// <summary>
        /// 属性：只读，返回字节数组图像
        /// </summary>
        public byte[] Image
        {
            get
            {
                MemoryStream ms = new MemoryStream();
                m_Image.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
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
            get { return (m_Image.VerticalResolution * m_Image.Height * m_Image.HorizontalResolution * m_Image.Width) * 3 / 1024 / 1024; }
        }

    }

    public class JPEG
    {

        public enum SrcType
        {
            LocalPath,
            RemoteURL,
        }

        /// <summary>
        /// 通过Http获取远程图像文件或通过IO读取文件。
        /// </summary>
        /// <param name="ImgURL"></param>
        /// <returns></returns>
        public static byte[] GetImg(string ImgURL, SrcType SrcURL)
        {
            ///请求并获取图像
            try
            {
                if (SrcURL == SrcType.LocalPath)
                {
                    FileStream s = File.OpenRead(ImgURL);
                    return ConvertStreamToByteBuffer(s);
                }
                else
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ImgURL);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    ///转换文件流为二进制字节数组
                    int b1;
                    MemoryStream mem = new MemoryStream();
                    Stream resStream = response.GetResponseStream();

                    while ((b1 = resStream.ReadByte()) != -1)
                    {
                        mem.WriteByte(((byte)b1));
                    }

                    return mem.ToArray();
                }
            }
            catch (Exception)
            {
                //404错误，返回NULL
                return null;
            }
        }

        /// <summary>
        /// 私有方法：ConvertStreamToByteBuffer：把给定的文件流转换为二进制字节数组。
        /// </summary>
        /// <param name="theStream"></param>
        /// <returns></returns>
        private static byte[] ConvertStreamToByteBuffer(System.IO.Stream theStream)
        {
            int b1;
            System.IO.MemoryStream tempStream = new System.IO.MemoryStream();
            while ((b1 = theStream.ReadByte()) != -1)
            {
                tempStream.WriteByte(((byte)b1));
            }
            return tempStream.ToArray();
        }

        ///2006.5.10 
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static bool MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            Image originalImage = Image.FromFile(originalImagePath);


            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
                return false;
            }

            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
            return true;
        }

        public static bool Makenail(string originalImagePath, string thumbnailPath, int MaxLength, string mode,ref string picPath)
        {
            int toheight, towidth;   //目标尺寸
            Image originalImage = null ;
            try
            {
                originalImage = Image.FromFile(originalImagePath);
            }
            catch(Exception ex)
            {
                string a = ex.Message.ToString();
            }
            if ((MaxLength <= originalImage.Height) || (MaxLength <= originalImage.Width))
            {
                if (originalImage.Height >= originalImage.Width)
                {
                    toheight = MaxLength;
                    towidth = originalImage.Width * MaxLength / originalImage.Height;
                }
                else
                {
                    towidth = MaxLength;
                    toheight = originalImage.Height * MaxLength / originalImage.Width;
                }

                int x = 0;
                int y = 0;
                int ow = originalImage.Width;
                int oh = originalImage.Height;

                //新建一个bmp图片
                Bitmap bitmap = new System.Drawing.Bitmap(towidth, toheight);

                bitmap.SetResolution(72, 72);//设置图像分辨率

                //新建一个画板
                Graphics g = System.Drawing.Graphics.FromImage(bitmap);

                //设置高质量插值法
                //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High; 两种可任选其一 
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                //设置高质量,低速度呈现平滑程度
                switch (mode)
                {
                    //"N"代表不做修饰
                    case "N":
                        g.SmoothingMode = SmoothingMode.None;
                        break;

                    //"H"代表平滑	
                    case "H":
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        break;

                    //"HS"代表锐度	
                    case "HS":
                        g.SmoothingMode = SmoothingMode.HighSpeed;
                        break;
                }

                //设置高质量,低速度呈现平滑程度
                //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; 和上面两种任选其一

                //清空画布并以透明背景色填充
                g.Clear(Color.Transparent);



                //在指定位置并且按指定大小绘制原图片的指定部分
                g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                    new Rectangle(x, y, ow, oh),
                    GraphicsUnit.Pixel);


                try
                {
                    originalImage = (Image)bitmap;
                    //以jpg格式保存缩略图
                    //bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);//两个方法不同之处在于类型不同
                    originalImage.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    picPath = thumbnailPath;
                }
                catch (System.Exception e)
                {
                    throw e;
                }

                finally
                {
                    originalImage.Dispose();
                    bitmap.Dispose();
                    g.Dispose();
                }
                return true;
            }
            else
            { return false; }

        }
    }
}

