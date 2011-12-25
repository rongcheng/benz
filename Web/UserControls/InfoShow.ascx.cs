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

namespace WebUI.UserControls
{
    /// <summary>
    /// 显示提示信息
    /// </summary>
    public partial class InfoShow : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string InfoText
        {
            set { this.infoFlag.Text = value; }
        }
    }
}