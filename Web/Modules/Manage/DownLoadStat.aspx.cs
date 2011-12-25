using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using QJVRMS.Business;
using QJVRMS.Business.Interface;
using QJVRMS.Business.ResourceType;

namespace WebUI.Modules.Manage
{
    public partial class DownLoadStat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.t_Date.Text = DateTime.Now.ToString("yyyy-MM-01");
                this.e_Date.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            }

        }

        protected void searchDate_Click(object sender, EventArgs e)
        {
            this.bindGrid();
        }

        protected string GetImgUrl(string ItemSerialNum, string ImageType, string folder)
        {
            
            QJVRMS.Business.ResourceType.ImageType obj = new QJVRMS.Business.ResourceType.ImageType();
            //yangguang
            //string imagePath = obj.PreviewPath_170_Read + "/" + folder + "/" + ItemSerialNum +  ImageType;
            if (ImageType.ToLower() == ".cr2" || ImageType.ToLower() == ".nef" || ImageType.ToLower() == ".psd")
                ImageType = ".jpg";
            string imagePath = obj.GetPreviewPathRead(folder, ItemSerialNum + ImageType, "170");

            return imagePath;
            //return UIBiz.CommonInfo.GetImageUrl(170, folder, ItemSerialNum, ImageType);

            

        }

        protected string GetImgUrl(string ItemSerialNum, string ImageType, string folder,string resourceType)
        {

            
            //return UIBiz.CommonInfo.GetImageUrl(170, folder, ItemSerialNum, ImageType);



            IResourceType obj = ResourceTypeFactory.getResourceTypeByString(resourceType);

            if (resourceType.ToLower().Equals("image"))
            {
               // return UIBiz.CommonInfo.GetImageUrl(170, folder, ItemSerialNum, ImageType);

               // QJVRMS.Business.ResourceType.ImageType obj = new QJVRMS.Business.ResourceType.ImageType();
                //yangguang
                //string imagePath = obj.PreviewPath_170_Read + "/" + folder + "/" + ItemSerialNum +  ImageType;
                if (ImageType.ToLower() == ".cr2" || ImageType.ToLower() == ".nef" || ImageType.ToLower() == ".psd")
                    ImageType = ".jpg";
                string imagePath = obj.GetPreviewPathRead(folder, ItemSerialNum + ImageType, "170");

                return imagePath;
            }
            else if (resourceType.ToLower().Equals("video"))
            {

                return obj.GetPreviewPathRead(folder, ItemSerialNum + ".jpg", "image");
            }


            return "";



        }

        private void bindGrid()
        {
            string resourceType = "image";
            resourceType = "";

            DateTime startDate;
            if (!DateTime.TryParse(this.t_Date.Text, out startDate))
            {
                startDate = DateTime.MinValue;
            }
            DateTime endDate;
            if (!DateTime.TryParse(this.e_Date.Text, out endDate))
            {
                endDate = DateTime.MaxValue;
            }

            endDate = endDate.AddDays(1);

            Resource obj = new Resource();
            DataTable dt=obj.GetDownloadStatic(resourceType, startDate, endDate);
            if (dt != null)
            {
                this.GridView1.DataSource = dt.DefaultView;
                this.GridView1.DataBind();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            this.bindGrid();
        }
    }
}
