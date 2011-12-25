using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using QJVRMS.Business;
using System.IO;
using QJVRMS.Business.ResourceType;
using QJVRMS.Common;
using System.Xml;

namespace WebUI {
    public partial class GetContent : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            string result = string.Empty;
            string type = get_LinkParam("type");
            string id = string.Empty;
            string body = string.Empty;
            Resource rs = new Resource();
            switch (type) {
                case "orderNew"://OrderNew.aspx
                    string mailFrom = ConfigurationManager.AppSettings["mailFrom"];
                    string mailSubject1 = ConfigurationManager.AppSettings["mailSubject"];
                    if (string.IsNullOrEmpty(mailSubject1)) {
                        mailSubject1 = "有新的订单";
                    }

                    Tool t1 = new Tool();
                    mailFrom = t1.GetValue(Tool.GetDocument("/xml/System.xml"), "from");
                    if (Session["OrderNew"] != null && Session["OrderNew"].ToString() != string.Empty)
                        body = Session["OrderNew"].ToString();
                    Orders obj1 = new Orders();
                    obj1.sendNewOrder(mailFrom, mailSubject1, body);
                    break;
                case "validate"://Validating.aspx
                    string mail = get_LinkParam("mail");
                    string subject = get_LinkParam("subject");
                    
                    if (Session["Validate"] != null && Session["Validate"].ToString() != string.Empty)
                        body = Session["Validate"].ToString();
                    Tool t = new Tool();
                    XmlDocument doc = Tool.GetDocument("/xml/System.xml");
                    string smtpHost = t.GetValue(doc, "host");
                    string smtpUserName = t.GetValue(doc, "userName");
                    string smtpPassword = t.GetValue(doc, "password");

                    string fromEmail = t.GetValue(doc, "from");

                    Tool.sendMail(smtpHost, smtpUserName, smtpPassword, fromEmail, mail, subject, body);
                    break;
                case "send"://OrdersManage.aspx OrderNotPass.aspx OrderDetail.aspx
                    string _userEmail = get_LinkParam("mail");
                    string mailSubject = get_LinkParam("subject");
                    string mailBody = string.Empty;
                    if (Session["MailBody"] != null && Session["MailBody"].ToString() != string.Empty)
                        mailBody = Session["MailBody"].ToString();
                    if (!string.IsNullOrEmpty(_userEmail) && !string.IsNullOrEmpty(mailBody)) {
                        Orders obj = new Orders();
                        obj.sendNewOrderToUser(_userEmail, mailSubject, mailBody);
                    }
                    break;
                case "error": //error.aspx
                    string html = string.Empty;
                    if(Session["ErrorHtml"] != null && Session["ErrorHtml"].ToString() != string.Empty)
                        html = Session["ErrorHtml"].ToString();
                    if (!string.IsNullOrEmpty(html)) {
                        XmlDocument doc1 = Tool.GetDocument("/xml/System.xml");
                        Tool tool = new Tool();
                        string host = tool.GetValue(doc1, "host");
                        string username = tool.GetValue(doc1, "userName");
                        string pass = tool.GetValue(doc1, "password");
                        string from = tool.GetValue(doc1, "from");
                        string adminUid = ConfigurationManager.AppSettings["superAdminId"];
                        string adminEmail = new MemberShipManager().GetUserEmailByUserID(adminUid);
                        string to = adminEmail;
                        string subject1 = "错误信息";
                        Tool.sendMail(host, username, pass, from, to, subject1, html);
                    }
                    break;
                case "exif":
                    #region EXIF信息
                    string folderName = get_LinkParam("folder");
                    string fileName = get_LinkParam("file");
                    string yRootPath = string.Empty;
                    try {
                        ImageType obj = new ImageType();
                        yRootPath = obj.GetSourcePath(folderName, fileName);

                        EXIFMetaData em = new EXIFMetaData();
                        EXIFMetaData.Metadata m = em.GetEXIFMetaData(yRootPath);

                        StringBuilder sb = new StringBuilder();
                        sb.Append("<table>");
                        sb.Append("<tr><td align=\"left\" valign=\"top\" width=\"100px\">");
                        sb.Append("<span>设备制造商：</span>");
                        sb.Append("</td><td style=\"word-wrap:break-word;word-break:break-all;\">");
                        sb.Append("<strong>");
                        sb.Append(string.IsNullOrEmpty(m.EquipmentMake.DisplayValue) ? string.Empty : m.EquipmentMake.DisplayValue.Replace("\0", string.Empty));
                        sb.Append("</strong>");
                        sb.Append("</td></tr>");

                        sb.Append("<tr><td align=\"left\" valign=\"top\" width=\"100px\">");
                        sb.Append("<span>摄影机型号：</span>");
                        sb.Append("</td><td>");
                        sb.Append("<strong>");
                        sb.Append(string.IsNullOrEmpty(m.CameraModel.DisplayValue) ? string.Empty : m.CameraModel.DisplayValue.Replace("\0", string.Empty));
                        sb.Append("</strong>");
                        sb.Append("</td></tr>");

                        sb.Append("<tr><td align=\"left\" valign=\"top\" width=\"100px\">");
                        sb.Append("<span>颜色表示：</span>");
                        sb.Append("</td><td>");
                        sb.Append("<strong>");
                        sb.Append(string.IsNullOrEmpty(m.ColorSpace.DisplayValue) ? string.Empty : m.ColorSpace.DisplayValue);
                        sb.Append("</strong>");
                        sb.Append("</td></tr>");

                        sb.Append("<tr><td align=\"left\" valign=\"top\" width=\"100px\">");
                        sb.Append("<span>闪关灯模式：</span>");
                        sb.Append("</td><td>");
                        sb.Append("<strong>");
                        sb.Append(string.IsNullOrEmpty(m.Flash.DisplayValue) ? string.Empty : m.Flash.DisplayValue);
                        sb.Append("</strong>");
                        sb.Append("</td></tr>");

                        sb.Append("<tr><td align=\"left\" valign=\"top\" width=\"100px\">");
                        sb.Append("<span>焦距：</span>");
                        sb.Append("</td><td>");
                        sb.Append("<strong>");
                        sb.Append(string.IsNullOrEmpty(m.FocalLength.DisplayValue) ? string.Empty : m.FocalLength.DisplayValue);
                        sb.Append("</strong>");
                        sb.Append("</td></tr>");

                        sb.Append("<tr><td align=\"left\" valign=\"top\" width=\"100px\">");
                        sb.Append("<span>曝光时间：</span>");
                        sb.Append("</td><td>");
                        sb.Append("<strong>");
                        sb.Append(string.IsNullOrEmpty(m.ExposureTime.DisplayValue) ? string.Empty : m.ExposureTime.DisplayValue);
                        sb.Append("</strong>");
                        sb.Append("</td></tr>");

                        sb.Append("<tr><td align=\"left\" valign=\"top\" width=\"100px\"><span>ISO：</span>");
                        sb.Append("</td><td>");
                        sb.Append("<strong>");
                        sb.Append(string.IsNullOrEmpty(m.ISOSpeed.DisplayValue) ? string.Empty : m.ISOSpeed.DisplayValue);
                        sb.Append("</strong>");
                        sb.Append("</td></tr>");

                        sb.Append("<tr><td align=\"left\" valign=\"top\" width=\"100px\"><span>光源：</span>");
                        sb.Append("</td><td>");
                        sb.Append("<strong>");
                        sb.Append(string.IsNullOrEmpty(m.LightSource.DisplayValue) ? string.Empty : m.LightSource.DisplayValue);
                        sb.Append("</strong>");
                        sb.Append("</td></tr>");

                        sb.Append("<tr><td align=\"left\" valign=\"top\" width=\"100px\"><span>曝光程序：</span>");
                        sb.Append("</td><td>");
                        sb.Append("<strong>");
                        sb.Append(string.IsNullOrEmpty(m.ExposureProg.DisplayValue) ? string.Empty : m.ExposureProg.DisplayValue);
                        sb.Append("</strong>");
                        sb.Append("</td></tr>");

                        sb.Append("<tr><td align=\"left\" valign=\"top\" width=\"100px\"><span>曝光补偿：</span>");
                        sb.Append("</td><td>");
                        sb.Append("<strong>");
                        sb.Append(string.IsNullOrEmpty(m.ExposureBias.DisplayValue) ? string.Empty : m.ExposureBias.DisplayValue);
                        sb.Append("</strong>");
                        sb.Append("</td></tr>");

                        sb.Append("<tr><td align=\"left\" valign=\"top\" width=\"100px\"><span>相机拍照时间：</span>");
                        sb.Append("</td><td>");
                        sb.Append("<strong>");
                        sb.Append(string.IsNullOrEmpty(m.DatePictureTaken.DisplayValue) ? string.Empty : m.DatePictureTaken.DisplayValue.Replace("\0", string.Empty));
                        sb.Append("</strong>");
                        sb.Append("</td></tr>");

                        sb.Append("<tr><td align=\"left\" valign=\"top\" width=\"100px\"><span>宽度：</span>");
                        sb.Append("</td><td>");
                        sb.Append("<strong>");
                        sb.Append(string.IsNullOrEmpty(m.ImageWidth.DisplayValue) ? string.Empty : m.ImageWidth.DisplayValue);
                        sb.Append("</strong>");
                        sb.Append("</td></tr>");

                        sb.Append("<tr><td align=\"left\" valign=\"top\" width=\"100px\"><span>高度：</span>");
                        sb.Append("</td><td>");
                        sb.Append("<strong>");
                        sb.Append(string.IsNullOrEmpty(m.ImageHeight.DisplayValue) ? string.Empty : m.ImageHeight.DisplayValue);
                        sb.Append("</strong>");
                        sb.Append("</td></tr>");

                        sb.Append("<tr><td align=\"left\" valign=\"top\" width=\"100px\"><span>水平分辨率：</span>");
                        sb.Append("</td><td>");
                        sb.Append("<strong>");
                        sb.Append(string.IsNullOrEmpty(m.XResolution.DisplayValue) ? string.Empty : m.XResolution.DisplayValue);
                        sb.Append("</strong>");
                        sb.Append("</td></tr>");

                        sb.Append("<tr><td align=\"left\" valign=\"top\" width=\"100px\"><span>垂直分辨率：</span>");
                        sb.Append("</td><td>");
                        sb.Append("<strong>");
                        sb.Append(string.IsNullOrEmpty(m.YResolution.DisplayValue) ? string.Empty : m.YResolution.DisplayValue);
                        sb.Append("</strong>");
                        sb.Append("</td></tr>");

                        sb.Append("</table>");
                        result = sb.ToString();
                    }
                    catch {
                        if (string.IsNullOrEmpty(result)) {
                            result = "<span style=\"color:red; padding:5px 5px 5px 5px;\">获取信息失败！！</span>";
                        }
                    }
                    #endregion
                    break;
            }

            Response.Write(result);
            Response.End();
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
