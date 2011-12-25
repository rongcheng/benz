using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using QJVRMS.Common;
using QJVRMS.Business;

namespace WebUI {
    public partial class error : AuthPage {
        public string strContent = string.Empty;
        protected void Page_Load(object sender, EventArgs e) {
            this.Title = "错误页";

            string content = get_LinkParam("c");
            this.divContent.InnerHtml = content;
            //strContent = content.Replace("\"","\\\"");
            Session["ErrorHtml"] = content;
        }

        private void SendMail(string html) {
            XmlDocument doc = Tool.GetDocument("/xml/System.xml");
            Tool tool = new Tool();
            string host = tool.GetValue(doc, "host");
            string username = tool.GetValue(doc, "userName");
            string pass = tool.GetValue(doc, "password");
            string from = tool.GetValue(doc, "from");
            string adminUid = ConfigurationManager.AppSettings["superAdminId"];
            string adminEmail = new MemberShipManager().GetUserEmailByUserID(adminUid);
            string to = adminEmail;
            string subject = "错误信息";
            Tool.sendMail(host, username, pass, from, to, subject, html);
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
