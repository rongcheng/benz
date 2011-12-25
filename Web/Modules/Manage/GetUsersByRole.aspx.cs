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


namespace WebUI.Modules.Manage
{
    public partial class GetUsersByRole : AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.bindData();
        }

        private void bindData()
        {
            string roleId = Request.QueryString["roleId"];
            if (!string.IsNullOrEmpty(roleId))
            {
                DataTable dt = new MemberShipManager().GetUsersByRoleId(new Guid(roleId));
                if (dt.Rows.Count > 0)
                {
                    this.rptUsers.DataSource = dt.DefaultView;
                    this.rptUsers.DataBind();
                    this.ltNoUsers.Visible = false;
                }
                else
                {
                    this.ltNoUsers.Visible = true;
                
                }
            }
        }


    }
}
