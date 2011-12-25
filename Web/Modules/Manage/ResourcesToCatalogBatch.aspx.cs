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

namespace WebUI.Modules.Manage
{
    public partial class ResourcesToCatalogBatch : AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string featureId = get_LinkParam("featureId");

                featureId = "71f60c99-982c-442e-8ae5-adab0e6fe46a";

                string type = get_LinkParam("type");
                string userId = CurrentUser.UserId.ToString();
                Response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
                Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\" ><head runat=\"server\"><title>批量修改分类</title></head>");
                Response.Write("<frameset rows=\"65%,*\" >");
                Response.Write("<frame name=\"addImages\" src=\"AddImages.aspx?featureId=" + featureId + "&type=" + type + "&userId=" + userId + "\">");
                Response.Write("<frame name=\"saveImages\" src=\"SaveImagesToCatalog.aspx?featureId=" + featureId + "&type=" + type + "&userId=" + userId + "\">");
                Response.Write("</frameset> ");
                Response.Write("</html>");
            }

        }


        private string get_LinkParam(string paramname)
        {
            string paramcontent = string.Empty;

            switch (Request.RequestType)
            {
                case "POST":
                    if (Request.Form[paramname] != null && Request.Form[paramname].ToString() != string.Empty)
                    {
                        paramcontent = Request.Form[paramname].ToString();
                    }
                    break;
                case "GET":
                    if (Request.QueryString[paramname] != null && Request.QueryString[paramname].ToString() != string.Empty)
                    {
                        paramcontent = HttpUtility.UrlDecode(Request.QueryString[paramname].ToString());
                    }
                    break;
            }

            return paramcontent.Trim();
        }
       //
    }
}
