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

namespace WebUI.UserControls
{
    public partial class QJ_Search_Default : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string isSearchInResult = Request.QueryString["isSearchInResult"];
            if (!string.IsNullOrEmpty(isSearchInResult))
            {
                this.rblSearch.SelectedValue = isSearchInResult;

            }
          
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }
    }
}