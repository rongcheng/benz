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

namespace WebUI.Modules.Manage
{
    public partial class OrderDetailPic : System.Web.UI.Page
    {
        public string orderId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                orderId = Request.QueryString["orderId"];
                if (!string.IsNullOrEmpty(orderId))
                {
                    bindOrderPicture(orderId);
                }
            }
        }

        private void bindOrderPicture(string orderId)
        {
            
            DataSet ds = Resource.GetResourcesByOrderId(orderId);
            DataTable dtSource = ds.Tables[0];
            int rowCount = dtSource.Rows.Count;

            this.lb_ResultCount.Text = "该订单共包含 <strong>" + rowCount.ToString() + "</strong> 条数据";

            
           
            this.DataResource1.DataSource = dtSource;
            this.DataResource1.DataBind();
        }
    }
}
