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
    public partial class newsMarquee : BaseUserControl
    {
        protected string ntype = "0";

        public string NType
        {
            get { return this.ntype; }
            set { this.ntype = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                GetTopNews();
            }
        }

        protected string GetTitle(string rawTitle,string newsId)
        {
            string raw = "<a target='_blank' href='" + AppRootPath + "/Modules/ShowNews.aspx?newsId=" + newsId + "'>{0}</a>";

            if (rawTitle.Length > 20)
            {
                rawTitle = rawTitle.Substring(0, 20) + "...";
            }

            raw = string.Format(raw, rawTitle);

            return raw;
        }

        protected void GetTopNews()
        {
            DataTable dt = QJVRMS.Business.News.GetNewsList("%%",char.Parse(NType));

            this.newsMarqueeList.DataSource = dt;
            this.newsMarqueeList.DataBind();

         
        }
        
    }


   

}