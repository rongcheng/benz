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
using WebUI.UIBiz;
using QJVRMS.Common;

namespace WebUI.UserControls
{
    public partial class UserLogin_Sany1 : System.Web.UI.UserControl
    {
        public string strUserName = "";
        public string strPassword = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                Response.End();
            }

            /*后添加的*/

            string userName = Request.QueryString["userName"];
            string password = Request.QueryString["password"];



            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                //Response.Write("0");
            }
            else
            {
                strUserName = userName;
                strPassword = password;

                //this.Parent.Page.ClientScript.RegisterStartupScript(this.GetType(), "sss", "alert('hi')"); 
                //Page.RegisterStartupScript("Test","<script>alert('hi');doLogin();</script>"); 

           
                
            }

        }
    }
}