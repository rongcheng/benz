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

namespace WebUI
{
    public partial class bossLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userName = Request.QueryString["userName"];
            string password = Request.QueryString["password"];



            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                Response.Write("0");
            }
            else
            {
                if (MemberShipManager.loginBoss(userName, password))
                {
                    Response.Write("1");
                }
                else
                {
                    Response.Write("0");
                }
            }



            Response.End();

        }
    }
}
