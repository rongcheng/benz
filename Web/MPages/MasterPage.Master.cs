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

namespace WebUI
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {

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
