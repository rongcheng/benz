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

namespace WebUI.MPages
{
    public partial class MainPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {


            string keyword = this.Kwords.Text.ToString().Trim().Replace("'", "''");

            Response.Redirect("/PicList.aspx?keyword=" + Server.UrlEncode(keyword) + "&BeginDate=&EndDate=&Catalogid=" + "00000000-0000-0000-0000-000000000000");//以后加开始和结束日期
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
