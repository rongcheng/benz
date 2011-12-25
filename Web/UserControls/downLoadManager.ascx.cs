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
    public partial class downLoadManager : System.Web.UI.UserControl
    {
        public DateTime beginDates;
        public DateTime endDates;
        public string username;

        protected void Page_Load(object sender, EventArgs e)
        {


            if(!Page.IsPostBack)
            {
                ViewState["begin"] = this.beginDate.Text;
                ViewState["end"] = this.endDate.Text;

                DataBind();
          
            }
        }

        public void DataBind()
        {
           //DataTable dt = QJVRMS.Business.ImageStorage.GetUserByAll();
           // if(dt!=null && dt.Rows.Count > 0)
           // {
           //     //for(int i=0;i<dt.Rows.Count;i++)
           //     //{
           //         this.downloadUser.Items.Clear();

                    

           //         this.downloadUser.DataSource = dt;

           //         this.downloadUser.DataValueField = "username";
           //         this.downloadUser.DataTextField = "username";
                
           //         ListItem li = new ListItem("È«²¿", "0");
           //         this.downloadUser.Items.Add(li);

           //         this.downloadUser.DataBind();
           //    // }
           // }
        }

        protected void searchDate_Click(object sender, EventArgs e)
        {
            beginDates = Convert.ToDateTime(this.beginDate.Text);
            endDates = Convert.ToDateTime(this.endDate.Text);

            this.begin.Value = this.beginDate.Text;
            this.end.Value = this.endDate.Text;

            username = this.downloadUser.SelectedValue.ToString();

            DataSet ds = null;

       //     ds = QJVRMS.Business.ImageStorage.SearchDownloadManagerByLoginNameAndDate(username);

            DataTable dt;

            dt = ds.Tables[0];

        }  
    }
}