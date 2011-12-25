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

namespace WebUI.MPages
{
    public partial class QJ_FuncPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public string AppWebPath
        {
            get
            {
                if (Request.ApplicationPath == "/")
                {
                    return string.Empty;
                }

                return Request.ApplicationPath;
            }
        }
    }

}
