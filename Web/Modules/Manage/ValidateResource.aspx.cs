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
using System.Text;
using System.IO;
using QJVRMS.Business;

namespace WebUI.Modules.Manage
{
    public partial class ValidateResource : AuthPage
    {
        private int _curpageMyUpload;
        protected void Page_Load(object sender, EventArgs e)
        {


            this.PageBar1.PageSize = NowPageCount();
            if (!Page.IsPostBack)
            {
               
                //this.myUpload_StartDate.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.AddMonths(-3).Month.ToString() + "-1";

                this.myUpload_StartDate.Text = DateTime.Now.AddYears(-1).ToShortDateString();
                this.myUpload_EndDate.Text = DateTime.Now.AddDays(1).ToShortDateString();

                bindNewUpload(PageBar1.PageSize,0,"");
                bindNewResourceStat();

                this.p1.Visible = false;
                this.p2.Visible = true;

            }

        }

        protected void lbNewResourceByUser_Click(object sender,EventArgs e)
        {
            this.p1.Visible = false;
            this.p2.Visible = true;
        }

        private void bindNewResourceStat()
        {
            DataSet ds = new Resource().GetNewResourceStatByUser(Guid.Empty);
            this.grvNewResource.DataSource = ds.Tables[0].DefaultView;
            this.grvNewResource.DataBind();
        }
        protected void ShowNewResource(object sender, CommandEventArgs e)
        {
            string userId = e.CommandArgument.ToString();
            this.hidUserId.Value = userId;
            bindNewUpload(PageBar1.PageSize, 0, this.hidUserId.Value);

            this.p1.Visible = true;
            this.p2.Visible = false;
            
        }

        private void bindNewUpload(int pageSize,int pageIndex)
        {
            string beginDate = this.myUpload_StartDate.Text;
            string endDate = this.myUpload_EndDate.Text;
            string userId = this.hidUserId.Value;
            int pageCount = 0;
            int rowCount = 0;

            DataSet ds = new Resource().GetResourceByUserID(beginDate, endDate, userId, pageSize,pageIndex, ref pageCount, "", (int)ResourceEntity.ResourceStatus.IsProcessing);
            DataTable dt = ds.Tables[1];
            DataTable dt1 = ds.Tables[0];

            if (dt1.Rows.Count > 0)
            {
                rowCount = Convert.ToInt32(dt1.Rows[0][0].ToString());
            }

            if (dt.Rows.Count > 0)
            {
                this.drMyUpload.DataSource = dt;
                this.drMyUpload.DataBind();
            }

            this.PageBar1.PageSize = pageSize;
            this.PageBar1.RecordCount = rowCount;
        }

        private void bindNewUpload(int pageSize, int pageIndex,string userId)
        {
            string beginDate = this.myUpload_StartDate.Text;
            string endDate = this.myUpload_EndDate.Text;
            
            int pageCount = 0;
            int rowCount = 0;

            DataSet ds = new Resource().GetResourceByUserID(beginDate, endDate, userId, pageSize, pageIndex, ref pageCount, "", (int)ResourceEntity.ResourceStatus.IsProcessing);
            DataTable dt = ds.Tables[1];
            DataTable dt1 = ds.Tables[0];

            if (dt1.Rows.Count > 0)
            {
                rowCount = Convert.ToInt32(dt1.Rows[0][0].ToString());
            }

            if (dt.Rows.Count > 0)
            {
                this.drMyUpload.DataSource = dt;
                this.drMyUpload.DataBind();
            }

            this.PageBar1.PageSize = pageSize;
            this.PageBar1.RecordCount = rowCount;
        }

        //分页
        protected void PageBar1_PageChanged(object src, QJ.WebControls.PageChangedEventArgs e)
        {
            this._curpageMyUpload = e.NewPageIndex;
            if (_curpageMyUpload != 0) _curpageMyUpload--;

            this.PageBar1.CurrentPageIndex = e.NewPageIndex;

            int validateStatus = (int)ResourceEntity.ResourceStatus.IsProcessing;


            bindNewUpload(this.PageBar1.PageSize, this._curpageMyUpload);

            

        }


        protected void searchDate_Click(object sender, EventArgs e)
        {
            this._curpageMyUpload = 0;
            if (_curpageMyUpload != 0) _curpageMyUpload--;

            this.PageBar1.CurrentPageIndex = 1;

            int validateStatus =(int)ResourceEntity.ResourceStatus.IsProcessing;

            bindNewUpload(this.PageBar1.PageSize, this._curpageMyUpload);

            

        }


        protected int NowPageCount()
        {

            HttpCookie pageCountCookie = Request.Cookies["valiDatePageCount"];
            int defaultCount = UIBiz.CommonInfo.PageCount;
            if (pageCountCookie == null)
            {
                return UIBiz.CommonInfo.PageCount;
            }
            else
            {
                int.TryParse(pageCountCookie.Value, out defaultCount);
                return defaultCount;
            }
        }




    }
}
