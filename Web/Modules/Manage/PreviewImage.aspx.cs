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
using System.Xml;

namespace WebUI.Modules.Manage {
    public partial class PreviewImage : System.Web.UI.Page {
        const string EXAMPLEPATH = "../../xml/137-0015.jpg";
        const string SAVEPATH = "../../xml/water.gif";
        const string XMLPATH = "../../xml/mark.xml";
        const string TEMPPATH = "../../xml/temp.gif";

        protected void Page_Load(object sender, EventArgs e) {
            try {
                string state = get_LinkParam("state");
                string path = Server.MapPath(SAVEPATH);
                if (string.IsNullOrEmpty(state)) {
                    string temppath = Server.MapPath(TEMPPATH);
                    if (File.Exists(temppath))
                        path = temppath;
                }
                string showType = get_LinkParam("show");
                
                if (string.IsNullOrEmpty(showType)) {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(Server.MapPath(XMLPATH));
                    if (doc != null) {
                        XmlNode node = doc.SelectSingleNode("Root/ShowType");
                        showType = node.InnerText;
                    }
                }
                ImageType obj = new ImageType();

                FileStream s = new FileStream(Server.MapPath(EXAMPLEPATH), FileMode.Open, FileAccess.Read);
                byte[] b = new byte[int.Parse(s.Length.ToString())];
                s.Read(b, 0, int.Parse(s.Length.ToString()));
                s.Close();
     
                FileStream s1 = new FileStream(path, FileMode.Open, FileAccess.Read);
                byte[] b1 = new byte[int.Parse(s1.Length.ToString())];
                s1.Read(b1, 0, int.Parse(s1.Length.ToString()));
                s1.Close();

                QJImagePro.Handles.PictureHandle pic = QJImagePro.PictureFactory.CreateHandle(QJImagePro.PictureType.Jpeg);
                byte[] by = pic.Watermarking(b, b1, 72, int.Parse(showType));

                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ContentType = "image/Jpeg";
                HttpContext.Current.Response.BinaryWrite(by);
            }
            catch {
                Response.Write("图片路径不正确!");
                Response.End();
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
    }
}
