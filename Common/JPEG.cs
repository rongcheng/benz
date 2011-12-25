//****************************************************
// ���ܣ�JPEGͼ���ļ�����
//       
// ���ߣ�Ҧ˷
// ʱ�䣺2006-4-24
// �޸ļ�¼��ʱ�䣺
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

    public enum Resolution
    {
        LowRes,          //72DBI
        HighRes,         //300DPI
    }

    public class JPEGImage
    {
        private Image m_Image;

        /// <summary>
        /// �������Ĺ�����(����)
        /// ���ֽ������ȡͼ���ļ�
        /// </summary>
        /// <param name="SrcImageStream"></param>
        public JPEGImage(byte[] SrcImageStream)
        {
            MemoryStream memStream = new MemoryStream(SrcImageStream);
            m_Image = new Bitmap(memStream);
            memStream.Dispose();
        }

        /// <summary>
        /// ��������СͼƬ�ߴ�
        /// ����˫���β�ֵ����Ҫ���ṩ���߳��ߴ����Сģʽ
        /// </summary>
        /// <param name="MaxLength"></param>
        /// <param name="intMode"></param>
        /// <returns></returns>
        public bool ZoomIn(int MaxLength, ZPRI intMode, Resolution Res)
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
                switch (Res)
                {
                    case Resolution.LowRes:
                        bitmap.SetResolution(72, 72);
                        break;
                    default:
                        break;
                }
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
                bitmap.Dispose();
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// �����������ʽͼ��ˮӡ
        /// </summary>
        /// <param name="wmImg"></param>
        /// <param name="Position"></param>
        /// <param name="Transparency"></param>
        /// <returns></returns>
        public bool AddWaterMark(Byte[] wmImg, AliasPos Position, float Transparency)
        {
            //��ȡˮӡͼ�񣬲����ֽ�����ת����ͼ��
            MemoryStream memStream = new MemoryStream(wmImg);
            Image markImg = new Bitmap(memStream);

            //������ɫ����
            float[][] ptsArray ={ 
            new float[] {1, 0, 0, 0, 0},
            new float[] {0, 1, 0, 0, 0},
            new float[] {0, 0, 1, 0, 0},
            new float[] {0, 0, 0, Transparency, 0}, //ע�⣺�˴�Ϊ0.0fΪ��ȫ͸����1.0fΪ��ȫ��͸��
            new float[] {0, 0, 0, 0, 1}};

            ColorMatrix colorMatrix = new ColorMatrix(ptsArray);
            //�½�һ��Image����
            ImageAttributes imageAttributes = new ImageAttributes();
            //����ɫ������ӵ�����
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Default);

            //����λͼ��ͼ��

            Bitmap newBitmap = new Bitmap(m_Image.Width, m_Image.Height, PixelFormat.Format24bppRgb);
            //���÷ֱ���
            newBitmap.SetResolution(m_Image.HorizontalResolution, m_Image.VerticalResolution);
            //����Graphics
            Graphics g = Graphics.FromImage(newBitmap);
            //�������
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //����ԭͼ����ͼ��
            g.DrawImage(m_Image, new Rectangle(0, 0, m_Image.Width, m_Image.Height), 0, 0, m_Image.Width, m_Image.Height, GraphicsUnit.Pixel);

            //���ˮӡ
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

            //�ͷ�
            memStream.Dispose();
            imageAttributes.Dispose();
            g.Dispose();



            return true;
        }

        /// <summary>
        /// д��IPTC��Ϣ
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
        /// ���ԣ�ֻ���������ֽ�����ͼ��
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
        /// ͨ��Http��ȡԶ��ͼ���ļ���ͨ��IO��ȡ�ļ���
        /// </summary>
        /// <param name="ImgURL"></param>
        /// <returns></returns>
        public static byte[] GetImg(string ImgURL, SrcType SrcURL)
        {
            ///���󲢻�ȡͼ��
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

                    ///ת���ļ���Ϊ�������ֽ�����
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
                //404���󣬷���NULL
                return null;
            }
        }

        /// <summary>
        /// ˽�з�����ConvertStreamToByteBuffer���Ѹ������ļ���ת��Ϊ�������ֽ����顣
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
        /// ��������ͼ
        /// </summary>
        /// <param name="originalImagePath">Դͼ·��������·����</param>
        /// <param name="thumbnailPath">����ͼ·��������·����</param>
        /// <param name="width">����ͼ���</param>
        /// <param name="height">����ͼ�߶�</param>
        /// <param name="mode">��������ͼ�ķ�ʽ</param>    
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
                case "HW"://ָ���߿����ţ����ܱ��Σ�                
                    break;
                case "W"://ָ�����߰�����                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://ָ���ߣ�������
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://ָ���߿�ü��������Σ�                
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

            //�½�һ��bmpͼƬ
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //�½�һ������
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //���ø�������ֵ��
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //���ø�����,���ٶȳ���ƽ���̶�
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //��ջ�������͸������ɫ���
            g.Clear(Color.Transparent);

            //��ָ��λ�ò��Ұ�ָ����С����ԭͼƬ��ָ������
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            try
            {
                //��jpg��ʽ��������ͼ
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
            int toheight, towidth;   //Ŀ��ߴ�
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

                //�½�һ��bmpͼƬ
                Bitmap bitmap = new System.Drawing.Bitmap(towidth, toheight);

                bitmap.SetResolution(72, 72);//����ͼ��ֱ���

                //�½�һ������
                Graphics g = System.Drawing.Graphics.FromImage(bitmap);

                //���ø�������ֵ��
                //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High; ���ֿ���ѡ��һ 
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                //���ø�����,���ٶȳ���ƽ���̶�
                switch (mode)
                {
                    //"N"����������
                    case "N":
                        g.SmoothingMode = SmoothingMode.None;
                        break;

                    //"H"����ƽ��	
                    case "H":
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        break;

                    //"HS"�������	
                    case "HS":
                        g.SmoothingMode = SmoothingMode.HighSpeed;
                        break;
                }

                //���ø�����,���ٶȳ���ƽ���̶�
                //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; ������������ѡ��һ

                //��ջ�������͸������ɫ���
                g.Clear(Color.Transparent);



                //��ָ��λ�ò��Ұ�ָ����С����ԭͼƬ��ָ������
                g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                    new Rectangle(x, y, ow, oh),
                    GraphicsUnit.Pixel);


                try
                {
                    originalImage = (Image)bitmap;
                    //��jpg��ʽ��������ͼ
                    //bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);//����������֮ͬ���������Ͳ�ͬ
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

