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

namespace WebUI.Modules.Manage {
    public partial class ReleaseNotice : AuthPage {
        public string logName = string.Empty;
        protected void Page_Load(object sender, EventArgs e) {
            this.Title = "发布公告";
            if (!IsPostBack) {
                QJVRMS.Business.NoticeFactory noticeFactory = new QJVRMS.Business.NoticeFactory();
                logName = CurrentUser.UserLoginName;
                this.Content.InnerHtml = noticeFactory.GetNoticesContent(logName, 10, 1);
                
            }
        }
    }
}
