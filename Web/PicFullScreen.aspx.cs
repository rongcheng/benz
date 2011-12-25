using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using QJVRMS.Business;
using System.IO;
using QJVRMS.Business.ResourceType;
using QJVRMS.Common;
using QJVRMS.Business.Interface;

using System.Drawing;
using System.Xml;


namespace WebUI
{
    public partial class PicFullScreen : System.Web.UI.Page
    {

        const string IMAGEPATH = "xml/water.gif";
        const string XMLPATH = "xml/mark.xml";
        protected string sourceImage = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GetImageInfo(new Guid(Request["ItemId"]));
            }
            catch (Exception ex)
            {
                LogWriter.WriteExceptionLog(ex);
                Response.Write("<script language='javascript'>alert('不存在此图片或您没有权限浏览!');window.close();</script>");
                Response.End();
            }

        }


        protected void GetImageInfo(Guid itemId)
        {
            Resource rs = new Resource();
            ResourceEntity r = rs.GetResourceInfoByItemId(itemId.ToString());

            if (!r.ResourceType.ToLower().Equals("image"))
            {
                return;
            
            }



            //水印图片
            FileStream s1 = new FileStream(Server.MapPath(IMAGEPATH), FileMode.Open, FileAccess.Read);
            byte[] b1 = new byte[int.Parse(s1.Length.ToString())];
            s1.Read(b1, 0, int.Parse(s1.Length.ToString()));
            s1.Close();

            

            ImageType obj = new ImageType();
            //yangguang
            //string strPhysicalPath = Path.Combine(Path.Combine(obj.SourcePath,r.FolderName),r.ServerFileName);
            string strPhysicalPath = obj.GetSourcePath(r.FolderName, r.ServerFileName);
            if (File.Exists(strPhysicalPath))
            {
                

                Stream iStream = null;
                String tmpFilePath=string.Empty;
                try
                {
                    byte[] buffer = new Byte[10000];
                    int length;
                    long dataToRead;
                    string filepath = strPhysicalPath;
                    string filename = Path.GetFileName(filepath);


                    //压缩
                    String tmpFileName=DateTime.Now.ToString("yyyyMMddhhmmss")+".bin";
                    //yangguang
                    //tmpFilePath=Path.Combine(obj.SourcePath,tmpFileName);
                    tmpFilePath = Path.Combine(obj.SourcePaths[obj.PathNumber].Trim(), tmpFileName);//obj.GetSourcePath(string.Empty, tmpFileName);

                    System.Drawing.Image srcImage = System.Drawing.Image.FromFile(filepath);
                    int imgWidth = srcImage.Width;
                    int imgHeight = srcImage.Height;


                    if (imgWidth > 1000 || imgHeight > 1000)
                    {
                        ArrayList sarray = new ArrayList();
                        sarray.Add(strPhysicalPath);
                        ArrayList aarray = new ArrayList();
                        aarray.Add(tmpFilePath);
                        ImageController.ToZipImage(sarray, aarray, 1000);

                        iStream = new FileStream(tmpFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    }
                    else
                    {
                        iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    }

                    //用户选择的原图
                    
                    byte[] b = new byte[int.Parse(iStream.Length.ToString())];
                    iStream.Read(b, 0, int.Parse(iStream.Length.ToString()));
                    iStream.Close();


                    QJImagePro.Handles.PictureHandle pic = QJImagePro.PictureFactory.CreateHandle(QJImagePro.PictureType.Jpeg);
                    string showType = GetShowType();
                    int type = string.IsNullOrEmpty(showType) ? 0 : int.Parse(showType);
                    byte[] by = pic.Watermarking(b, b1, 72, type);
                    Response.ContentType = "image/Jpeg";
                    Response.BinaryWrite(by);
                }
                catch (Exception ex)
                {
                    LogWriter.WriteExceptionLog(ex);
                }
                finally
                {
                    if (iStream != null)
                    {
                        iStream.Close();
                    }
                    if (File.Exists(tmpFilePath))
                    {
                        File.Delete(tmpFilePath);
                    }
                }
            
            }

        }


        private string GetShowType()
        {
            string result = string.Empty;
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                doc.Load(Server.MapPath(XMLPATH));

                if (doc != null)
                {
                    XmlNode node = doc.SelectSingleNode("Root/ShowType");
                    result = node.InnerText;
                }
            }
            catch
            {
                return string.Empty;
            }

            return result;
        }

    }
}
