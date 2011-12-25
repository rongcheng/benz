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
using QJVRMS.Business.Interface;
using QJVRMS.Business.ResourceType;

namespace WebUI.Modules.Manage
{
    public partial class DownLoadSubPage : AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
          
                this.t_Date.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1";
                this.e_Date.Text = DateTime.Now.ToShortDateString();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.IsInControl = true;
            base.OnInit(e);
        }



        protected void BindLog()
        {
            string username = txtLoginName.Text.Trim();

            if (t_Date.Text != string.Empty
                && e_Date.Text != string.Empty)
            {
                DateTime begin = Convert.ToDateTime(t_Date.Text);
                DateTime end = Convert.ToDateTime(e_Date.Text);

                DataSet ds;
                ds = QJVRMS.Business.ImageStorage.GetDownLoadMessage(username, begin, end);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {


                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = null;//ds;
                    GridView1.DataBind();
                }
            }
        }



        protected void searchDate_Click(object sender, EventArgs e)
        {
            BindLog();
        }

        protected string GetImgUrl(string ItemSerialNum, string ImageType,string folder,string resourceType)
        {
            IResourceType obj = ResourceTypeFactory.getResourceTypeByString(resourceType);

            if (resourceType.ToLower().Equals("image"))
            {
                return UIBiz.CommonInfo.GetImageUrl(170, folder, ItemSerialNum, ImageType);
            }
            else
            {

                return obj.GetPreviewPathRead(folder, ItemSerialNum + ".jpg", "image");
            }

            return obj.GetPreviewPathRead(folder, ItemSerialNum, ImageType);
            


            

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            BindLog();
        }

       
    }
}
