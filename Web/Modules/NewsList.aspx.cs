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

namespace WebUI.Modules
{
    public partial class NewsList : AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.Request["type"]))
                {
                    Int16 index = 0;
                    Int16.TryParse(Request["type"], out index);
                    this.infoTabs.ActiveTabIndex = index;
                }
                GetInfo();
            }
        }

        protected void GetInfo()
        {
            using (DataTable dt = QJVRMS.Business.News.GetNewsList("%%", '0'))
            {
                this.newsList.DataSource = dt;
                this.newsList.DataBind();
            }


            using (DataTable dt = QJVRMS.Business.News.GetNewsList("%%", '1'))
            {
                this.bulletList.DataSource = dt;
                this.bulletList.DataBind();
            }

        }
    }
}
