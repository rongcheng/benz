using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Drawing2D;


namespace QJVRMS.Common
{
    public class ImageHelper
    {

        private Image _image;

        public ImageHelper()
        { }

        public ImageHelper(string imageFilePath)
        {
            if (File.Exists(imageFilePath))
            {
                _image = Image.FromFile(imageFilePath);
            }
        }

        public ImageHelper(Stream imageStream)
        {
            _image = Image.FromStream(imageStream);
        }


        public void Dispose()
        {
            _image.Dispose();
        }

        public void Save(string saveFilePath,bool isOverWrite)
        {
            if (File.Exists(saveFilePath) && (!isOverWrite))
            {
                return;
            }
            _image.Save(saveFilePath);
        }

        public void Save(string saveFilePath)
        {
            Save(saveFilePath, true);
        }

        public Image GetImage()
        {
            return _image;
        }
       

        /// <summary>
        /// 灰度
        /// </summary>
        public void SetGrayscale()
        {
            Bitmap bmap = (Bitmap)_image;
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    byte gray = (byte)(.299 * c.R + .587 * c.G + .114 * c.B);

                    bmap.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            _image = (Image)bmap;            
        }

        /// <summary>
        /// 色相
        /// </summary>
        /// <param name="colorFilterType"></param>
        public void SetColorFilter(ColorFilterTypes colorFilterType)
        {
            //Bitmap temp = (Bitmap)_currentBitmap;
           // Bitmap bmap = (Bitmap)temp.Clone();
            Bitmap bmap = (Bitmap)_image;
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    int nPixelR = 0;
                    int nPixelG = 0;
                    int nPixelB = 0;
                    if (colorFilterType == ColorFilterTypes.Red)
                    {
                        nPixelR = c.R;
                        nPixelG = c.G - 255;
                        nPixelB = c.B - 255;
                    }
                    else if (colorFilterType == ColorFilterTypes.Green)
                    {
                        nPixelR = c.R - 255;
                        nPixelG = c.G;
                        nPixelB = c.B - 255;
                    }
                    else if (colorFilterType == ColorFilterTypes.Blue)
                    {
                        nPixelR = c.R - 255;
                        nPixelG = c.G - 255;
                        nPixelB = c.B;
                    }

                    nPixelR = Math.Max(nPixelR, 0);
                    nPixelR = Math.Min(255, nPixelR);

                    nPixelG = Math.Max(nPixelG, 0);
                    nPixelG = Math.Min(255, nPixelG);

                    nPixelB = Math.Max(nPixelB, 0);
                    nPixelB = Math.Min(255, nPixelB);

                    bmap.SetPixel(i, j, Color.FromArgb((byte)nPixelR, (byte)nPixelG, (byte)nPixelB));
                }
            }
            _image = (Image)bmap;  
        }


        /// <summary>
        /// 曲线
        /// </summary>
        /// <param name="red">红</param>
        /// <param name="green">绿</param>
        /// <param name="blue">蓝</param>
        public void SetGamma(double red, double green, double blue)
        {
            Bitmap bmap = (Bitmap)_image;
            Color c;
            byte[] redGamma = CreateGammaArray(red);
            byte[] greenGamma = CreateGammaArray(green);
            byte[] blueGamma = CreateGammaArray(blue);
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    bmap.SetPixel(i, j, Color.FromArgb(redGamma[c.R], greenGamma[c.G], blueGamma[c.B]));
                }
            }
            _image = (Image)bmap;   
        }

        /// <summary>
        /// 获取曲线数组
        /// </summary>
        /// <param name="color">色彩</param>
        /// <returns>数组</returns>
        private byte[] CreateGammaArray(double color)
        {
            byte[] gammaArray = new byte[256];
            for (int i = 0; i < 256; ++i)
            {
                gammaArray[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / color)) + 0.5));
            }
            return gammaArray;
        }


        /// <summary>
        /// 设置亮度
        /// </summary>
        /// <param name="brightness">亮度,-255到+255之间的数值</param>
        public void SetBrightness(int brightness)
        {
            Bitmap bmap = (Bitmap)_image;
            if (brightness < -255) brightness = -255;
            if (brightness > 255) brightness = 255;
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    int cR = c.R + brightness;
                    int cG = c.G + brightness;
                    int cB = c.B + brightness;

                    if (cR < 0) cR = 1;
                    if (cR > 255) cR = 255;

                    if (cG < 0) cG = 1;
                    if (cG > 255) cG = 255;

                    if (cB < 0) cB = 1;
                    if (cB > 255) cB = 255;

                    bmap.SetPixel(i, j, Color.FromArgb((byte)cR, (byte)cG, (byte)cB));
                }
            }
            _image = (Image)bmap;  
        }


        /// <summary>
        /// 设置对比度
        /// </summary>
        /// <param name="contrast">对比度,-100到+100之间的数值</param>
        public void SetContrast(double contrast)
        {
            Bitmap bmap = (Bitmap)_image;
            if (contrast < -100) contrast = -100;
            if (contrast > 100) contrast = 100;
            contrast = (100.0 + contrast) / 100.0;
            contrast *= contrast;
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    double pR = c.R / 255.0;
                    pR -= 0.5;
                    pR *= contrast;
                    pR += 0.5;
                    pR *= 255;
                    if (pR < 0) pR = 0;
                    if (pR > 255) pR = 255;

                    double pG = c.G / 255.0;
                    pG -= 0.5;
                    pG *= contrast;
                    pG += 0.5;
                    pG *= 255;
                    if (pG < 0) pG = 0;
                    if (pG > 255) pG = 255;

                    double pB = c.B / 255.0;
                    pB -= 0.5;
                    pB *= contrast;
                    pB += 0.5;
                    pB *= 255;
                    if (pB < 0) pB = 0;
                    if (pB > 255) pB = 255;

                    bmap.SetPixel(i, j, Color.FromArgb((byte)pR, (byte)pG, (byte)pB));
                }
            }
            _image = (Image)bmap;  
        }


        /// <summary>
        /// 底片效果
        /// </summary>
        public void SetInvert()
        {
            Bitmap bmap = (Bitmap)_image;
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    bmap.SetPixel(i, j, Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
                }
            }
            _image = (Image)bmap; 
        }



        /// <summary>
        /// 按比例缩放
        /// </summary>
        /// <param name="MaxLength">最大的边长</param>
        /// <returns></returns>
        public void Resize(int MaxLength)
        {
            int oWidth, oHeight;
            int toHeight, toWidth;
            oWidth = _image.Width;
            oHeight = _image.Height;
            
            bool isHorizontal=true;
            isHorizontal = oWidth > oHeight;

            
            if (isHorizontal) //横图
            {                    
                toWidth = MaxLength;
                toHeight = oHeight * toWidth / oWidth;
            }
            else
            {
                toHeight = MaxLength;
                toWidth = oWidth * MaxLength / oHeight;
            }

            Bitmap bitmap = new Bitmap(toWidth, toHeight);                

            //设置分辨率
            if (MaxLength < 1500)
                bitmap.SetResolution(72, 72);

            Graphics g = Graphics.FromImage(bitmap);

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.Default;
            
            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片
            g.DrawImage(_image, new Rectangle(0, 0, toWidth, toHeight),
                new Rectangle(0, 0,oWidth, oHeight),
                GraphicsUnit.Pixel);

            _image = (Image)bitmap;
            g.Dispose();
           
            
        }

        /// <summary>
        /// 缩放到指定的宽和高
        /// </summary>
        /// <param name="toWidth"></param>
        /// <param name="toHeight"></param>
        public void Resize(int toWidth,int toHeight)
        {
            int oWidth, oHeight;           
            oWidth = _image.Width;
            oHeight = _image.Height;

            Bitmap bitmap = new Bitmap(toWidth, toHeight);

            //设置分辨率
            if (Math.Max(toWidth,toHeight) < 1500)
                bitmap.SetResolution(72, 72);

            Graphics g = Graphics.FromImage(bitmap);

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.Default;

            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片
            g.DrawImage(_image, new Rectangle(0, 0, toWidth, toHeight),
                new Rectangle(0, 0, oWidth, oHeight),
                GraphicsUnit.Pixel);

            _image = (Image)bitmap;
            g.Dispose();
        }


        /// <summary>
        /// 翻转
        /// </summary>
        /// <param name="rotateFlipType">图像的旋转方向和用于翻转图像的轴。</param>
        public void RotateFlip(RotateFlipType rotateFlipType)
        {
            Bitmap bmap = (Bitmap)_image;
            bmap.RotateFlip(rotateFlipType);
            _image = (Bitmap)bmap;

        }


        /// <summary>
        /// 裁剪
        /// </summary>
        /// <param name="xPosition">X起始点</param>
        /// <param name="yPosition">Y起始点</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        public void Crop(int xPosition, int yPosition, int width, int height)
        {
            Bitmap bmap = (Bitmap)_image;            
            if (xPosition + width > _image.Width)
                width = _image.Width - xPosition;
            if (yPosition + height > _image.Height)
                height = _image.Height - yPosition;
            Rectangle rect = new Rectangle(xPosition, yPosition, width, height);
            _image = (Bitmap)bmap.Clone(rect, bmap.PixelFormat);

        }


        /// <summary>
        /// 插入文字
        /// </summary>
        /// <param name="text">要插入的文字</param>
        /// <param name="xPosition">X位置</param>
        /// <param name="yPosition">Y位置</param>
        /// <param name="fontName">字体</param>
        /// <param name="fontSize">大小</param>
        /// <param name="fontStyle">类型</param>
        /// <param name="colorName1">颜色</param>
        /// <param name="colorName2">颜色</param>
        public void InsertText(string text, int xPosition, int yPosition, string fontName, float fontSize, string fontStyle, string colorName1, string colorName2)
        {
            Bitmap bmap = (Bitmap)_image;   
            Graphics gr = Graphics.FromImage(bmap);
            if (string.IsNullOrEmpty(fontName))
                fontName = "Times New Roman";
            if (fontSize.Equals(null))
                fontSize = 12.0F;
            Font font = new Font(fontName, fontSize);
            if (!string.IsNullOrEmpty(fontStyle))
            {
                FontStyle fStyle = FontStyle.Regular;
                switch (fontStyle.ToLower())
                {
                    case "bold":
                        fStyle = FontStyle.Bold;
                        break;
                    case "italic":
                        fStyle = FontStyle.Italic;
                        break;
                    case "underline":
                        fStyle = FontStyle.Underline;
                        break;
                    case "strikeout":
                        fStyle = FontStyle.Strikeout;
                        break;
                }
                font = new Font(fontName, fontSize, fStyle);
            }
            if (string.IsNullOrEmpty(colorName1))
                colorName1 = "Black";
            if (string.IsNullOrEmpty(colorName2))
                colorName2 = colorName1;
            Color color1 = Color.FromName(colorName1);
            Color color2 = Color.FromName(colorName2);
            int gW = (int)(text.Length * fontSize);
            gW = gW == 0 ? 10 : gW;
            LinearGradientBrush LGBrush = new LinearGradientBrush(new Rectangle(0, 0, gW, (int)fontSize), color1, color2, LinearGradientMode.Vertical);
            gr.DrawString(text, font, LGBrush, xPosition, yPosition);
            _image = (Bitmap)bmap;
        }


        /// <summary>
        /// 插入图像
        /// </summary>
        /// <param name="imagePath">要插入的图像路径</param>
        /// <param name="xPosition">X位置</param>
        /// <param name="yPosition">Y位置</param>
        public void InsertImage(string imagePath, int xPosition, int yPosition)
        {
            Bitmap bmap = (Bitmap)_image;             
            Graphics gr = Graphics.FromImage(bmap);
            if (!string.IsNullOrEmpty(imagePath))
            {
                Bitmap i_bitmap = (Bitmap)Bitmap.FromFile(imagePath);
                Rectangle rect = new Rectangle(xPosition, yPosition, i_bitmap.Width, i_bitmap.Height);
                gr.DrawImage(Bitmap.FromFile(imagePath), rect);
            }
            _image = (Bitmap)bmap;
        }

        /// <summary>
        /// 添加水印，在9个位置
        /// </summary>
        /// <param name="imagePath">水印图片路径</param>
        /// <param name="pos">插入的位置</param>
        public void InsertImage(string imagePath, WathermarkPosition pos)
        {
            int padding=10;
            int xPosition, yPosition;
            int oWidth = _image.Width, oHeight = _image.Height;

            Bitmap i_bitmap = (Bitmap)Bitmap.FromFile(imagePath);
            int wWidth=i_bitmap.Width,wHeight=i_bitmap.Height;
            i_bitmap.Dispose();

            switch (pos)
            { 
                case WathermarkPosition.TopLeft:
                    xPosition = padding;
                    yPosition = padding;
                    break;
                case WathermarkPosition.TopCenter:
                    xPosition = (int)((oWidth - wWidth) / 2);
                    yPosition = padding;
                    break;
                case WathermarkPosition.TopRight:
                    xPosition=oWidth-wWidth-padding;
                    yPosition=padding;
                    break;
                case WathermarkPosition.CenterLeft:
                    xPosition=padding;
                    yPosition=(int)((oHeight-wHeight)/2);
                    break;
                case WathermarkPosition.Center:
                    xPosition = (int)((oWidth - wWidth) / 2);
                    yPosition = (int)((oHeight - wHeight) / 2);
                    break;
                case WathermarkPosition.CenterRight:
                    xPosition = oWidth - wWidth - padding;
                    yPosition = (int)((oHeight - wHeight) / 2);
                    break;
                case WathermarkPosition.BottomLeft:
                    xPosition=padding;
                    yPosition=oHeight-wHeight-padding;
                    break;
                case WathermarkPosition.BottomCenter:
                    xPosition=(int)((oWidth - wWidth) / 2);
                    yPosition=oHeight-wHeight-padding;
                    break;
                case WathermarkPosition.BottomRight:
                    xPosition = oWidth - wWidth - padding;
                    yPosition = oHeight - wHeight - padding;
                    break;
                default:
                    xPosition = padding;
                    yPosition = padding;
                    break;
            }
            InsertImage(imagePath, xPosition, yPosition);
        }



    }

    public enum ColorFilterTypes
    {
        Red,
        Green,
        Blue
    }

    public enum WathermarkPosition
    { 
        TopLeft,
        TopCenter,
        TopRight,
        CenterLeft,
        Center,
        CenterRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    public class ImageMetaData
    {
        private int _width;
        private int _height;
        private long _fileSize;
    }
}
