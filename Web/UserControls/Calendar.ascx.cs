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

namespace WebUI.UserControls
{
    public partial class Calendar : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string script =  "<script type='text/javascript' language='javascript' src='{0}/UI/Script/calendar/calendar.js'></script>"
                   +"<script type='text/javascript' language='javascript' src='{0}/UI/Script/calendar/calendar-setup.js'></script>"
                   +"<script type='text/javascript' language='javascript' src='{0}/UI/Script/calendar/calendar-lang.js'></script>";

            script = string.Format(script, AppRootPath);
            Page.RegisterClientScriptBlock("cal1",script);

            string css = "<link href='{0}/UI/Script/calendar/calendar-win2k-cold-2.css' rel='stylesheet' type='text/css' />"
                         + "   <link type='text/css' rel='stylesheet' href='{0}/UI/Script/calendar/calendar-brown.css' />";

            css = string.Format(css, AppRootPath);
            Page.RegisterClientScriptBlock("CssCal",css);
           
        }

        public string Text
        {
            get { return theDate.Value; }
            set { theDate.Value = value; }
        }
    }
}