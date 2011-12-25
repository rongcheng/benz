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
    public partial class Orders_Info : AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

            OrderInfo model = biz.GetOrderInfo(ViewState["OrderId"].ToString());

            lOrderId.Text = model.OrderId;
            lCreateTime.Text = model.CreateDate.ToString() ;
            lState.Text = OrdersBiz.GetStateString(model.State);
            Address.Text = model.Address;
            
            MemberShipManager manager = new MemberShipManager();
            User user = manager.GetUser(new Guid(model.UserId));
            lUserName.Text = user.UserName;
            Contactor.Text = model.Contactor;
            tel.Text = model.Tel;            
            email.Text = model.Email;
            GroupName.Text = user.GroupName;

            DataTable dt = biz.GetOrders_DetailList(-1, ViewState["OrderId"].ToString(), string.Empty, string.Empty);

            gvOrders_Detail.DataSource = dt;
            gvOrders_Detail.DataBind();
        }

        protected void imgGift_DataBinding(object sender, EventArgs e)
        {
            Image img = sender as Image;
            GiftInfo giftInfo = new GiftBiz().GetModel(img.ImageUrl);
            try
            {
                ImageStorage imageModel = ImageStorage.GetImageStorageModel(new Guid(giftInfo.ImageId));
                img.ImageUrl = UIBiz.CommonInfo.GetImageUrl(170, imageModel.FolderName, imageModel.ItemSerialNum, imageModel.ImageType);
            }
            catch(Exception ex)
            {
                return;
            }
        }
    }
}
