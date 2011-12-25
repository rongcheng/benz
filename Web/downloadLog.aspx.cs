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
    public partial class downloadLog : AuthPage//System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                PageBar1.PageSize = UIBiz.CommonInfo.PageCount;
                this.t_Date.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1";
                this.e_Date.Text = DateTime.Now.ToShortDateString();
                bind();
            }
        }

        protected void bind()
        {
            DateTime begin = Convert.ToDateTime(t_Date.Text);
            DateTime end = Convert.ToDateTime(e_Date.Text);

            DataSet ds = QJVRMS.Business.ImageStorage.GetDownLoadMessage(CurrentUser.UserLoginName, begin, end);


            if (ds.Tables[0].Rows.Count != 0)
            {
                // PageBar1.RecordCount = ds.Tables[0].Rows.Count;
                PageBar1.RecordCount = ds.Tables[0].Rows.Count;
                PagedDataSource pd = new PagedDataSource();
                pd.DataSource = ds.Tables[0].DefaultView;
                pd.AllowPaging = true;
                pd.PageSize = PageBar1.PageSize;
                pd.CurrentPageIndex = PageBar1.CurrentPageIndex - 1;
                GridView1.DataSource = pd;//ds;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;//ds;
                GridView1.DataBind();
            }

        }

        protected void PageBar1_PageChanged(object src, QJ.WebControls.PageChangedEventArgs e)
        {

            this.PageBar1.CurrentPageIndex = e.NewPageIndex;

            bind();

        }

        //Í¼Æ¬Â·¾¶        
        protected string GetImgUrl(string ItemSerialNum, string ImageType)
        {
            return UIBiz.CommonInfo.GetImageUrl(170, CurrentUser.UserLoginName, ItemSerialNum, ImageType);
        }

        protected void searchDate_Click(object sender, EventArgs e)
        {
            bind();
        }
    }
}
