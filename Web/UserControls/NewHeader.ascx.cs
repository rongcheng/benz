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
using WebUI.UIBiz;

namespace WebUI.UserControls
{
    public partial class NewHeader:BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.ManageLink.Text = "<li><a href=\"/Modules/Manage/Sysmanager.aspx\">管理</a></li>";
                this.ManageLink.Visible = false;

                if (Request.IsAuthenticated)
                {
                    this.lblLoginName.Text = CurrentUser.UserName;//获取用户登录名称
                    if (QJVRMS.Business.Function.GetUserFunctionRight(CurrentUser.UserId) || CurrentUser.UserId == CommonInfo.SuperAdminId)
                        this.ManageLink.Visible = true;     
                }

                if (WebUI.UIBiz.CommonInfo.AuthByAD)
                {
                    this.btnModifyPwd.Visible = false;
                }
            }

        }
    }
}