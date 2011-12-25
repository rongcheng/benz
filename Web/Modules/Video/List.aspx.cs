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
using System.IO;

namespace WebUI.Modules.Video
{
    public partial class List : AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.cataTree.CatalogSel += new EventHandler(cataTree_CatalogSel);
            

        }


        //Í¼Æ¬Â·¾¶
        protected string GetImgUrl(string FolderName, string ItemSerialNum,int status)
        {
            string _ret=string.Empty;
            if (status == 2)
            {
                _ret = "/images/videoconverterror.gif";
            }
            else if (status == 0)
            {
                _ret = "/images/videoconverting.gif";
            }
            else
            {
                string videoPreviewPath = ConfigurationManager.AppSettings["videoPreviewPathRead"];
                if (!string.IsNullOrEmpty(videoPreviewPath))
                {
                    _ret= videoPreviewPath+ "image/"+FolderName+"/"+ItemSerialNum + ".jpg";
                }

            }
            //return UIBiz.CommonInfo.GetImageUrl(170, FolderName, ItemSerialNum, ImageType);

            return Server.UrlEncode(_ret);

        }

        //flvÂ·¾¶
        protected string GetFlvUrl(string FolderName, string ItemSerialNum, int status)
        {
            string _ret = string.Empty;
            if (status == 2)
            {
                //_ret = "/images/videoconverterror.gif";
            }
            else if (status == 0)
            {
                //_ret = "/images/videoconverting.gif";
            }
            else
            {
                string videoPreviewPath = ConfigurationManager.AppSettings["videoPreviewPathRead"];
                if (!string.IsNullOrEmpty(videoPreviewPath))
                {
                    _ret = videoPreviewPath + "flv/" + FolderName + "/" + ItemSerialNum + ".flv";
                }

            }
            //return UIBiz.CommonInfo.GetImageUrl(170, FolderName, ItemSerialNum, ImageType);

            return Server.UrlEncode(_ret);

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
            GetVideoList();
        }

        void cataTree_CatalogSel(object sender, EventArgs e)
        {
            GetVideoList();
        }


        public void GetVideoList()
        {

            int rowCount = 0;
            DataTable dtSource = QJVRMS.Business.VideoStorageClass.SearchVideo(string.Empty,
                string.Empty,
                string.Empty, 
                this.cataTree.CurrentSelNode.Value, 
                CurrentUser.UserId.ToString(),
                pageBar.PageSize,
                this.pageBar.CurrentPageIndex+1, 
                ref rowCount);
 

            this.pageBar.RecordCount = rowCount;

            this.videoList.DataSource = dtSource;
            this.videoList.DataBind();
 
        }


        /// <summary>
        /// °´±àºÅËÑË÷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSea_Click(object sender, EventArgs e)
        {
            using (DataTable dt = QJVRMS.Business.ImageStorageClass.GetImageByNum(this.txtVideoSN.Text.Trim(), this.CurrentUser.UserId))
            {
                this.videoList.DataSource = dt;
                this.videoList.DataBind();
            }

            this.pageBar.RecordCount = 0;
        }
    }
}
