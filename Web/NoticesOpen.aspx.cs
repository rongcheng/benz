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

namespace WebUI {
    public partial class NoticesOpen : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                string noticeId = get_LinkParam("noticeId");
                QJVRMS.Business.NoticeFactory noticeFactory = new QJVRMS.Business.NoticeFactory();
                string title = string.Empty;
                this.Content.InnerHtml = noticeFactory.ShowNoticeContent(noticeId, ref title);
                this.Title = title;
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
