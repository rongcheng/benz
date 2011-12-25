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

namespace WebUI.Modules.Orders
{
    public partial class Orders_Detail_List : AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lErrorInfo.Text = string.Empty;
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["orderid"]))
                {
                    lOrderId.Text = Request["orderid"];
                    ViewState["OrderId"] = Request["orderid"];
                    LoadData();
                }

            }
        }

        private void LoadData()
        {
            OrdersBiz biz = new OrdersBiz();

            DataTable dt = biz.GetOrders_DetailList(-1, ViewState["OrderId"].ToString(), string.Empty, string.Empty);

            gvOrders_Detail.DataSource = dt;
            gvOrders_Detail.DataBind();
        }

        protected void gvOrders_Detail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrders_Detail.PageIndex = e.NewPageIndex;
            LoadData();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            OrdersBiz biz = new OrdersBiz();
            GiftBiz giftBiz = new GiftBiz();

            LinkButton lnk = sender as LinkButton;
            GridViewRow row = lnk.Parent.Parent as GridViewRow;
            TextBox txtGiftCount = row.FindControl("txtGiftCount") as TextBox;
            TextBox txtUsage = row.FindControl("txtUsage") as TextBox;

            int newCount = int.Parse(txtGiftCount.Text.Trim());

            Orders_DetailInfo model = biz.GetOrders_DetailModel(int.Parse(lnk.CommandArgument));
            GiftInfo giftModel = giftBiz.GetModel(model.GiftId);

            //更新数量的话，更新礼品信息
            int newQuantity = giftModel.Quantity;
            newQuantity -= newCount - model.GiftCount;

            if (newQuantity < 0)
            {
                lErrorInfo.Text = "礼品【" + giftModel.Title + "】数量不足，剩余数量为：" + giftModel.Quantity.ToString() + "！";
                return;
            }
            giftModel.Quantity = newQuantity;
            model.GiftCount = newCount;
            model.Usage = txtUsage.Text.Trim();

            if (biz.UpdateOrders_Detail(model) > 0 && giftBiz.UpdateGift(giftModel) > 0)
            {
                ShowMessage("更新成功！");
            }
            else
            {
                ShowMessage("更新失败！");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            OrdersBiz biz = new OrdersBiz();
            GiftBiz giftBiz = new GiftBiz();

            LinkButton lnk = sender as LinkButton;

            Orders_DetailInfo model = biz.GetOrders_DetailModel(int.Parse(lnk.CommandArgument));
            GiftInfo giftModel = giftBiz.GetModel(model.GiftId);

            //删除订单明细后，更新礼品信息
            giftModel.Quantity += model.GiftCount;

            if (biz.DeleteOrders_Detail(int.Parse(lnk.CommandArgument)) > 0 && giftBiz.UpdateGift(giftModel) > 0)
            {
                ShowMessage("更新成功！");
            }
            else
            {
                ShowMessage("更新失败！");
            }
        }

        protected void imgGift_DataBinding(object sender, EventArgs e)
        {
            Image img = sender as Image;
            GiftInfo giftInfo = new GiftBiz().GetModel(img.ImageUrl);
            
            ImageStorage imageModel = ImageStorage.GetImageStorageModel(new Guid(giftInfo.ImageId));
            if(imageModel!=null )img.ImageUrl = UIBiz.CommonInfo.GetImageUrl(170, imageModel.FolderName, imageModel.ItemSerialNum, imageModel.ImageType);
           
        }
    }
}
