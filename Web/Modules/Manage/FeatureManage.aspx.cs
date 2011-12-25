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
    public partial class FeatureManage : AuthPage {
        public string logName = string.Empty;
        protected void Page_Load(object sender, EventArgs e) {
            this.Title = "专题管理";
            if (!IsPostBack) {
                QJVRMS.Business.FeatureFactory featureFactory = new QJVRMS.Business.FeatureFactory();
                logName = CurrentUser.UserLoginName;
                this.Content.InnerHtml = featureFactory.GetFeaturesContent(logName, 10, 1);
            }
        }
    }
}
