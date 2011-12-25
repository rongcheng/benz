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
    public partial class statControl : System.Web.UI.UserControl
    {

         
        public DataTable StatTable
        {
            get
            {
                if (Cache["Stat"] == null)
                {
                    Cache.Insert("Stat", QJVRMS.Business.Resource.GetStatResources(),
                       null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 30));
                }
                return Cache["Stat"] as DataTable;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.GetStatData();
            }
        }

        protected void GetStatData()
        {
            //this.labAllimage.Text = StatTable.Rows[0]["isum"].ToString();
            //this.labTimage.Text = StatTable.Rows[0]["tsum"].ToString();
            //this.labAllVideo.Text = StatTable.Rows[0]["VideoAllCount"].ToString();
            //this.labTodayVideo.Text = StatTable.Rows[0]["VideoTodayCount"].ToString();

            this.labAllResources.Text = StatTable.Rows[0]["ResourcesAllCount"].ToString();
            this.labTodayResources.Text = StatTable.Rows[0]["ResourcesTodayCount"].ToString();
        }
    }
}