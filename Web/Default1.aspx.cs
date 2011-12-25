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

namespace WebUI
{
    public partial class Default1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {


            //string keyword = this.Kwords.Text.ToString().Trim().Replace("'", "''");

           // Response.Redirect("/ResourceList.aspx?keyword=" + Server.UrlEncode(keyword) + "&BeginDate=&EndDate=&Catalogid=" + "00000000-0000-0000-0000-000000000000");//以后加开始和结束日期
        }
    }
}
