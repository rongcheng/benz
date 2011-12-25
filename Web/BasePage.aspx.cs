using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using WebUI.UIBiz;

namespace WebUI
{
    public partial class BasePage : System.Web.UI.Page
    {

     
        /// <summary>
        /// Web应用的根路径
        /// </summary>
        protected   string AppRootPath
        {
            get
            {
                if (Page.Request.ApplicationPath == "/") return string.Empty;

                return Page.Request.ApplicationPath;
            }
        } 


        protected void ShowMessage(String message)
        {
            ShowMessage(this, message);
        }

        protected void ShowMessage(Control container, String message)
        {
            string script = "alert('{0}');";
            script = string.Format(script, message);

            ScriptManager.RegisterClientScriptBlock(container,
              typeof(Page),
              "quickAlert",
              script, true);

        }
    }
}
