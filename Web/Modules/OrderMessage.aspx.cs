using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using QJVRMS.Business;

namespace WebUI.Modules
{
    public partial class OrderMessage :AuthPage
    {
        public static string _orderId = string.Empty;
        public static int isUserRead = 0;
        public static int isAdminRead = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string orderId = Request.QueryString["orderId"];
                if (string.IsNullOrEmpty(orderId))
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>window.close();</script>");
                    return;
                }

                _orderId = orderId;
                bind();

                QJVRMS.Business.Orders obj = new QJVRMS.Business.Orders();
                //订单下面的操作按钮
                //有订单操作权限的人
                if (obj.IsOrderAlertAdmin(CurrentUser.UserId) || IsSuperAdmin)
                {
                    obj.UpdateOrderMessageStatusAdmin(new Guid(_orderId), 1);
                    isUserRead = 0;
                    isAdminRead = 1;
                }
                else
                {
                    //这里没有判断是不是自己的订单
                    obj.UpdateOrderMessageStatusUser(new Guid(_orderId), 1);
                    isUserRead = 1;
                    isAdminRead = 0;
                
                }

                //bool isOrderUser = (this.txtUserName.Text.ToLower() == CurrentUser.UserLoginName.ToLower());
            
            }

        }

        private void bind()
        {
            QJVRMS.Business.Orders obj = new QJVRMS.Business.Orders();
            DataTable dt = obj.GetOrderMessageByOrderId(new Guid(_orderId));
            this.rptOrderMessage.DataSource = dt;
            this.rptOrderMessage.DataBind();


        
        }
    

        protected void Button1_Click(object sender, EventArgs e)
        {
            string content = this.txtMessage.Text.Trim().Replace("'", "''");
            if (content.Length < 1)
            {
                ShowMessage("内容不能为空");
                return;
            }
            QJVRMS.Business.Orders obj = new QJVRMS.Business.Orders();
            if (obj.AddOrderMessage(Guid.NewGuid(), new Guid(_orderId), content, DateTime.Now, CurrentUser.UserLoginName,isUserRead,isAdminRead))
            {
                ShowMessage("成功");
                bind();               


            }
            else
            {
                ShowMessage("失败");
            }

        }
    }
}
