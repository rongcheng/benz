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
    public partial class OrderMessageShow : AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                bind();
            }
        }

        private void bind()
        {
            QJVRMS.Business.Orders objOrder = new QJVRMS.Business.Orders();
            DataTable dt = null;
            if (objOrder.IsOrderAlertAdmin(CurrentUser.UserId) || IsSuperAdmin)
            {
                dt = objOrder.IsOrderMessageAlertAdmin();                
            }
            else
            {
                dt = objOrder.IsOrderMessageAlertUser(CurrentUser.UserId);                
            }

            this.rptOrders.DataSource = dt.DefaultView;
            this.rptOrders.DataBind();
        
        }
    }
}
