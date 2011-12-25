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
    public partial class ImageEditor :  AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.cataTree.CatalogSel += new EventHandler(cataTree_CatalogSel);
        }

      

        //Í¼Æ¬Â·¾¶
        protected string GetImgUrl(string FolderName, string ItemSerialNum, string ImageType)
        {

            return UIBiz.CommonInfo.GetImageUrl(170, FolderName, ItemSerialNum, ImageType);

        }
         
        /// <summary>
        /// Í¼Æ¬ÏÂÔØ
        /// </summary>
        /// <param name="picId"></param>
        /// <returns></returns>
        protected string GetCmd(string picId, string ptype)
        {
            string strPicid = picId.ToString();

            if (Request.IsAuthenticated && bool.Parse(CurrentUser.IsDownLoad))
            {

                return "<a target='_self' href=\"javascript:downhigh('" + strPicid + "','" + ptype + "')\">ÏÂÔØ</a>";
            }

            else
            {
                return "";
            }

        }

        protected void pageBar_PageChanged(object src, QJ.WebControls.PageChangedEventArgs e)
        {
            this.pageBar.CurrentPageIndex = e.NewPageIndex;
            GetImageList();
        }

        void cataTree_CatalogSel(object sender, EventArgs e)
        {
            GetImageList();
        }


        public void GetImageList()
        {

            int rowCount = 0;
            DataTable dtSource = QJVRMS.Business.ImageStorage.SearchImage(string.Empty,
                string.Empty,
                string.Empty, 
                this.cataTree.CurrentSelNode.Value, 
                CurrentUser.UserId.ToString(),
                pageBar.PageSize,
                this.pageBar.CurrentPageIndex, 
                ref rowCount);
 

            this.pageBar.RecordCount = rowCount;//sp.recordCount;
//            this.pageBar.CurrentPageIndex = pageIndex;


            this.imageList.DataSource = dtSource;
            this.imageList.DataBind();
 
        }


        /// <summary>
        /// °´±àºÅËÑË÷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSea_Click(object sender, EventArgs e)
        {
            using (DataTable dt = QJVRMS.Business.ImageStorageClass.GetImageByNum(this.txtImageNum.Text.Trim(), this.CurrentUser.UserId))
            {
                this.imageList.DataSource = dt;
                this.imageList.DataBind();
            }

            this.pageBar.RecordCount = 0;
        }
    }
}
