using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using QJVRMS.Business;

namespace WebUI.Modules.Manage
{
    public partial class OrderStat :AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.myOrder_StartDate.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.AddMonths(-3).Month.ToString() + "-1";
                this.myOrder_EndDate.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                bind();
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bind();
        }


        private void bind()
        {
            DateTime start = Convert.ToDateTime(this.myOrder_StartDate.Text);
            DateTime end = Convert.ToDateTime(this.myOrder_EndDate.Text).AddDays(1);

            QJVRMS.Business.Orders obj = new QJVRMS.Business.Orders();
            DataSet ds = obj.GetOrderStatus(start, end);

            this.rptOrderStat.DataSource = ds.Tables[0].DefaultView;
            this.rptOrderStat.DataBind();
        
        }

        public string GetLength(string per)
        {
            int count = Convert.ToInt32(per.Replace("%", ""));
            count = count * 3;
            return count.ToString();

           

        }


    }
}
