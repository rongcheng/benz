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

using QJVRMS.Business;

namespace WebUI.Modules.Gift
{
    public partial class Gift_List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();
                LoadData();
            }
        }

        private void InitData()
        {
            GiftBiz biz = new GiftBiz();
            DataTable dt = biz.GetGiftTypeList();

            ddlGiftType.DataSource = dt;
            ddlGiftType.DataTextField = "TypeName";
            ddlGiftType.DataValueField = "TypeID";
            ddlGiftType.DataBind();

            ddlGiftType.Items.Insert(0, new ListItem("È«²¿", string.Empty));
        }

        private void LoadData()
        {
            GiftBiz biz = new GiftBiz();
            DataTable dt = biz.GetGiftList(txtTitle.Text.Trim(), ddlGiftType.SelectedValue, string.Empty);

            gvGiftList.DataSource = dt;
            gvGiftList.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvGiftList.PageIndex = 0;
            LoadData();
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            GiftBiz biz = new GiftBiz();
            biz.DeleteGift(lnk.CommandArgument);

            LoadData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvGiftList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGiftList.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void imgGift_DataBinding(object sender, EventArgs e)
        {
            Image img = sender as Image;
            try
            {          
           
             ImageStorage imageModel = ImageStorage.GetImageStorageModel(new Guid(img.ImageUrl));
           
                img.ImageUrl = UIBiz.CommonInfo.GetImageUrl(170, imageModel.FolderName, imageModel.ItemSerialNum, imageModel.ImageType);
            }
            catch(Exception ex)
            {
                return;
            }
        }
    }
}
