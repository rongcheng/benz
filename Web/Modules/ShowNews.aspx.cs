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
    public partial class ShowNews : AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                GetNews(new Guid(Request["newsId"]));
            }
        }

        protected void GetNews(Guid newsId)
        {
            QJVRMS.Business.News news = QJVRMS.Business.News.GetNews(newsId);

            this.lbTitle.Text = news.Title;
            this.lbContent.Text = news.Content;
            this.lbPubDate.Text = news.CreateDate.ToString("yyyy-MM-dd HH-mm-ss");
            this.lbPubUser.Text = news.UserName;
        }
    }

   
}
