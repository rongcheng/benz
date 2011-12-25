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
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Xml;

namespace WebUI {
    public partial class CreateIamge : System.Web.UI.Page {
        const string IMAGEPATH = "xml/water.gif";
        const string XMLPATH = "xml/mark.xml";
        protected void Page_Load(object sender, EventArgs e) {
            // 400预览图路径
            string folderName = get_LinkParam("f");
            string serverFileName = get_LinkParam("s");
            string rotate = get_LinkParam("r");
            string yRootPath = string.Empty;
            try {
                ImageType obj = new ImageType();
                //yangguang
                //yRootPath = obj.PreviewPath_400;
                //图片全路径
                //yRootPath = yRootPath +"/"+ folderName + @"/" + serverFileName;
                yRootPath = obj.GetPreviewPath(folderName, serverFileName, "400");
                if (rotate == "90" || rotate == "180" || rotate == "270") {
                    //用户选择的原图
                    FileStream fs = new FileStream(yRootPath, FileMode.Open, FileAccess.Read);
                    byte[] bss = new byte[int.Parse(fs.Length.ToString())];
                    fs.Read(bss, 0, int.Parse(fs.Length.ToString()));
                    fs.Close();
                    MemoryStream mstraeam = new MemoryStream(bss);
                    Bitmap map = new Bitmap(mstraeam);
                    Bitmap bmp = KiRotate(map, rotate);
                    Guid guid = Guid.NewGuid();
                    string p = Server.MapPath(@"temp/" + guid.ToString() + ".jpg");
                    bmp.Save(p, ImageFormat.Jpeg);
                    FileStream ms = new FileStream(p, FileMode.Open, FileAccess.Read);
                    byte[] br = new byte[int.Parse(ms.Length.ToString())];
                    Session["BitmapImage"] = br;
                    ms.Read(br, 0, int.Parse(ms.Length.ToString()));
                    ms.Close();

                    //水印图片
                    FileStream fs1 = new FileStream(Server.MapPath(IMAGEPATH), FileMode.Open, FileAccess.Read);
                    byte[] bytes = new byte[int.Parse(fs1.Length.ToString())];
                    fs1.Read(bytes, 0, int.Parse(fs1.Length.ToString()));
                    fs1.Close();

                    QJImagePro.Handles.PictureHandle picr = QJImagePro.PictureFactory.CreateHandle(QJImagePro.PictureType.Jpeg);
                    string showTyper = GetShowType();
                    int typer = string.IsNullOrEmpty(showTyper) ? 0 : int.Parse(showTyper);
                    byte[] byr = picr.Watermarking(br, bytes, 72, typer);

                    if (File.Exists(p))
                        File.Delete(p);
                    HttpContext.Current.Response.ClearContent();
                    HttpContext.Current.Response.ContentType = "image/Jpeg";
                    HttpContext.Current.Response.BinaryWrite(byr);
                }
                else {
                    //用户选择的原图
                    FileStream s = new FileStream(yRootPath, FileMode.Open, FileAccess.Read);
                    byte[] b = new byte[int.Parse(s.Length.ToString())];
                    s.Read(b, 0, int.Parse(s.Length.ToString()));
                    s.Close();
                    //水印图片
                    FileStream s1 = new FileStream(Server.MapPath(IMAGEPATH), FileMode.Open, FileAccess.Read);
                    byte[] b1 = new byte[int.Parse(s1.Length.ToString())];
                    s1.Read(b1, 0, int.Parse(s1.Length.ToString()));
                    s1.Close();

                    QJImagePro.Handles.PictureHandle pic = QJImagePro.PictureFactory.CreateHandle(QJImagePro.PictureType.Jpeg);
                    string showType = GetShowType();
                    int type = string.IsNullOrEmpty(showType) ? 0 : int.Parse(showType);
                    byte[] by = pic.Watermarking(b, b1, 72, type);

                    //if (rotate == "90" || rotate == "180" || rotate == "270") {
                    //    System.IO.MemoryStream m = new MemoryStream(by);
                    //    Bitmap map = new Bitmap(m);
                    //    Bitmap bmp = KiRotate(map, rotate);


                    //    HttpContext.Current.Response.ContentType = "image/Jpeg";
                    //    bmp.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Jpeg);
                    //    bmp.Dispose();
                    //    map.Dispose();
                    //}
                    //else {
                    Session["BitmapImage"] = null;
                    HttpContext.Current.Response.ClearContent();
                    HttpContext.Current.Response.ContentType = "image/Jpeg";
                    HttpContext.Current.Response.BinaryWrite(by);
                    //}
                }
            }
            catch(Exception ee) {
                string a = ee.Message;
                if (File.Exists(yRootPath)) {
                    FileStream s = new FileStream(yRootPath, FileMode.Open, FileAccess.Read);
                    byte[] b = new byte[int.Parse(s.Length.ToString())];
                    s.Read(b, 0, int.Parse(s.Length.ToString()));
                    s.Close();

                    HttpContext.Current.Response.ClearContent();
                    HttpContext.Current.Response.ContentType = "image/Jpeg";
                    HttpContext.Current.Response.BinaryWrite(b);
                }
            }
        }

        private Bitmap KiRotate(Bitmap img, string degree) {
            try {
                switch (degree) {
                    case "90":
                        img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case "180":
                        img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case "270":
                        img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                }
                //
                return img;
            }
            catch {
                return null;
            }
        }


        private string GetShowType() {
            string result = string.Empty;
            XmlDocument doc = null;
            try {
                doc = new XmlDocument();
                doc.Load(Server.MapPath(XMLPATH));

                if (doc != null) {
                    XmlNode node = doc.SelectSingleNode("Root/ShowType");
                    result = node.InnerText;
                }
            }
            catch {
                return string.Empty;
            }

            return result;
        }

        private string get_LinkParam(string paramname) {
            string paramcontent = string.Empty;

            switch (Request.RequestType) {
                case "POST":
                    if (Request.Form[paramname] != null && Request.Form[paramname].ToString() != string.Empty) {
                        paramcontent = Request.Form[paramname].ToString();
                    }
                    break;
                case "GET":
                    if (Request.QueryString[paramname] != null && Request.QueryString[paramname].ToString() != string.Empty) {
                        paramcontent = HttpUtility.UrlDecode(Request.QueryString[paramname].ToString());
                    }
                    break;
            }

            return paramcontent.Trim();
        }
    }
}
