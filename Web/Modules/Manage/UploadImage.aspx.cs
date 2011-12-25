using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Xml;
using QJVRMS.Business;

namespace WebUI.Modules.Manage {
    public partial class UploadImage : System.Web.UI.Page {
        const string TEMPPATH = "../../xml/temp.gif";
        const string SAVEPATH = "../../xml/water.gif";
        const string XMLPATH = "../../xml/mark.xml";

        protected void Page_Load(object sender, EventArgs e) {
            string result = string.Empty;
            try {
                HttpPostedFile jpeg_image_upload = Request.Files["Filedata"];
                string temppath = Server.MapPath(TEMPPATH);
                jpeg_image_upload.SaveAs(temppath);
                if (File.Exists(temppath)) {
                    //UpdateWaterImage(temppath);
                    //File.Delete(temppath);
                    result = CreateImage();
                    result += ";上传成功";
                }
                else {
                    result = "上传失败";
                }
            }
            catch {
                result = "上传失败";
            }
            Response.Write(result);
            Response.End();
        }

        private bool UpdateWaterImage(string path) {
            try {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                int length = int.Parse(fs.Length.ToString());
                byte[] b = new byte[length];

                fs.Read(b, 0, length);
                fs.Dispose();
                fs.Close();

                string savePath = Server.MapPath(SAVEPATH);
                File.SetAttributes(savePath, FileAttributes.Normal);

                FileStream ws = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.Write);
                ws.Write(b, 0, length);
                ws.Dispose();
                ws.Close();

                return true;
            }
            catch {
                return false;
            }
        }
        private bool GetXml(string type, string path) {
            XmlDocument doc = null;
            try {
                doc = new XmlDocument();
                doc.Load(path);

                if (doc != null) {
                    XmlNode node = doc.SelectSingleNode("Root/ShowType");
                    node.InnerText = type;

                    File.SetAttributes(path, FileAttributes.Normal);

                    doc.Save(path);
                }
                return true;
            }
            catch {
                return false;
            }
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

        private string CreateImage() {
            string thumbnail_id  = string.Empty;
            System.Drawing.Image thumbnail_image = null;
            System.Drawing.Image original_image = null;
            System.Drawing.Bitmap final_image = null;
            System.Drawing.Graphics graphic = null;
            MemoryStream ms = null;

            try {

                HttpPostedFile jpeg_image_upload = Request.Files["Filedata"];

                original_image = System.Drawing.Image.FromStream(jpeg_image_upload.InputStream);

                int new_width = original_image.Width;
                int new_height = original_image.Height;
                int target_width = new_width;
                int target_height = new_height;

                final_image = new System.Drawing.Bitmap(target_width, target_height);
                graphic = System.Drawing.Graphics.FromImage(final_image);
                graphic.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), new System.Drawing.Rectangle(0, 0, target_width, target_height));
                int paste_x = (target_width - new_width) / 2;
                int paste_y = (target_height - new_height) / 2;
                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; /* new way */

                graphic.DrawImage(original_image, paste_x, paste_y, new_width, new_height);
                ms = new MemoryStream();
                final_image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                thumbnail_id = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                Thumbnail thumb = new Thumbnail(thumbnail_id, ms.GetBuffer());

                List<Thumbnail> thumbnails = Session["file_info"] as List<Thumbnail>;
                if (thumbnails == null) {
                    thumbnails = new List<Thumbnail>();
                    HttpContext.Current.Session["file_info"] = thumbnails;
                }
                thumbnails.Add(thumb);
            }
            catch {
                return string.Empty; 
            }
            finally {
                // Clean up
                if (final_image != null) final_image.Dispose();
                if (graphic != null) graphic.Dispose();
                if (original_image != null) original_image.Dispose();
                if (thumbnail_image != null) thumbnail_image.Dispose();
                if (ms != null) ms.Close();
            }

            return thumbnail_id;
        }
    }
}
