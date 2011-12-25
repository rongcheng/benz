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

namespace WebUI.Modules.Manage
{
    public partial class ManageInfo : AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request["manage"]  == null || this.Request["manage"] == "usage")
            {
                Control uc = new Control();
                uc = Page.LoadControl("/UserControls/UsageManage.ascx");
                //this.uc_Cells.Controls.Clear();
                this.uc_Cells.Controls.Add(uc);
            }

            if (this.Request["manage"] == "source")
            {
                Control uc1 = new Control();
                uc1 = Page.LoadControl("/UserControls/SourceManage.ascx");
                //this.uc_Cells.Controls.Clear();
                this.uc_Cells.Controls.Add(uc1);
            }


           
        }

        protected override void OnInit(EventArgs e)
        {
            this.IsInControl = true;
            base.OnInit(e);
        }


       
    }
}
