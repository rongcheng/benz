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
using QJVRMS.Common;

namespace WebUI.UserControls
{
    public partial class BaseUserControl : System.Web.UI.UserControl
    {


        /// <summary>
        /// Web应用的根路径
        /// </summary>
        protected string AppRootPath
        {
            get
            {
                if (Page.Request.ApplicationPath == "/") return string.Empty;

                return Page.Request.ApplicationPath;
            }
        }

        /// <summary>
        /// 受控控件
        /// </summary>
        protected Control[] securityControl;
        // protected bool isSuperAdmin = false;
        private IWebUser webUser;


        /// <summary>
        /// 当前用户
        /// </summary>
        protected IWebUser CurrentUser
        {
            get
            {
                System.Security.Principal.IPrincipal ipr = HttpContext.Current.User;

                if (ipr == null) return null;
                FormsIdentity id = ipr.Identity as FormsIdentity;

                if (id == null) return null;
                
                FormsAuthenticationTicket ticket = id.Ticket;

                // Get the stored user-data, in this case, our roles
                string userData = ticket.UserData;
                string[] userStr = userData.Split(',');

                IWebUser webUser = null;

                webUser = new WebUser(new Guid(userStr[0]), new Guid(userStr[1]), userStr[2], userStr[3], userStr[4], userStr[5]);

                return webUser;
            }
            
        }

        protected bool IsSuperAdmin
        {
            get
            {
                return (CurrentUser.UserId == CommonInfo.SuperAdminId);
            }
        }

        protected string SuperAdminId
        {
            get { return CommonInfo.SuperAdminId.ToString(); }
        }

        protected void ResponseNotAuthorized()
        {
            FormsAuthentication.RedirectToLoginPage();
            Response.End();
        }


        //public void CheckSecurity()
        //{
        //    securityControl[0].a
        //}
    }
}