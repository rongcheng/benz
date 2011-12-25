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

namespace WebUI.Modules
{
    public partial class ShowDeletedImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string id=Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))
            {
                Resource r = new Resource();
                byte[] buffer = r.GetDeletedImage(new Guid(id));

                if (buffer.Length > 100)
                {
                    Response.ClearContent();
                    Response.ContentType = "image/Jpeg";
                    Response.BinaryWrite(buffer);
                }
            }

            Response.End();

        }
    }
}
