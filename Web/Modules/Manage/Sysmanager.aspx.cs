using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using QJVRMS.Business;
using QJVRMS.Business.SecurityControl;
using WebUI.UIBiz;

namespace WebUI.Modules.Manage
{
    public partial class Sysmanager : AuthPage
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(QJVRMS.Business.Function.GetUserFunctionRight(CurrentUser.UserId) || CurrentUser.UserId == CommonInfo.SuperAdminId))
            {
                Response.Redirect("/", true);
            }
        }
    }
}
