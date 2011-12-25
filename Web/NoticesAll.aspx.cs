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

namespace WebUI {
    public partial class NoticesAll : AuthPage {
        protected void Page_Load(object sender, EventArgs e) {
            this.Title = "发布公告列表";
            if (!IsPostBack) {
                QJVRMS.Business.NoticeFactory noticeFactory = new QJVRMS.Business.NoticeFactory();
                this.Content.InnerHtml = noticeFactory.ShowNoticesAll(string.Empty, 20, 1);
            }
        }
    }
}
