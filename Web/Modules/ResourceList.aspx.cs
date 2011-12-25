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
using System.Text;
using System.IO;

namespace WebUI.Modules
{
    public partial class ResourceList : AuthPage
    {
        private int _curpage;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.cataTree.CatalogSel += new EventHandler(cataTree_CatalogSel);

            if (!IsPostBack)
            {

            }

        }



        private void bindResource()
        {
            int pageSize = pageBar.PageSize;
            int pageIndex = _curpage;
            bindResource(pageSize, pageIndex, -1);
        }

        private void bindResource(int pageSize, int pageIndex, int validateStatus)
        {

            int rowCount = 0;
            DataSet ds = QJVRMS.Business.Resource.Search(string.Empty,
                string.Empty,
                string.Empty,
                this.cataTree.CurrentSelNode.Value,
                CurrentUser.UserId.ToString(),
                pageBar.PageSize,
                _curpage,
                ref rowCount, string.Empty,
                string.Empty
                );


            DataTable dt1 = ds.Tables[0];
            DataTable dt = ds.Tables[1];

            if (dt1.Rows.Count > 0)
            {
                rowCount = Convert.ToInt32(dt1.Rows[0][0].ToString());
            }
            if (dt.Rows.Count > 0)
            {
                this.drResource.DataSource = dt;
                this.drResource.DataBind();
            }

            this.pageBar.PageSize = pageSize;
            this.pageBar.RecordCount = rowCount;
        }


        protected void pageBar_PageChanged(object src, QJ.WebControls.PageChangedEventArgs e)
        {

            this._curpage = e.NewPageIndex;
            if (_curpage != 0) _curpage--;
            this.pageBar.CurrentPageIndex = e.NewPageIndex;
            int validateStatus = -1;
            bindResource(this.pageBar.PageSize, this._curpage, validateStatus);

        }

        void cataTree_CatalogSel(object sender, EventArgs e)
        {
            bindResource();
        }


        private void GetResourceList()
        {
            int rowCount = 0;
            DataSet dtSource = QJVRMS.Business.Resource.Search(string.Empty,
                string.Empty,
                string.Empty,
                this.cataTree.CurrentSelNode.Value,
                CurrentUser.UserId.ToString(),
                pageBar.PageSize,
                _curpage,
                ref rowCount, string.Empty,
                string.Empty

                );

            DataTable dt0 = dtSource.Tables[0];
            DataTable dt1 = dtSource.Tables[1];


            this.pageBar.RecordCount = rowCount;

            //this.resourceList.DataSource = dt1;
            //this.resourceList.DataBind();

        }




        /// <summary>
        /// °´±àºÅËÑË÷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSea_Click(object sender, EventArgs e)
        {

            int rowCount = 0;
            DataSet dtSource = QJVRMS.Business.Resource.Search(this.txtResourceSN.Text.Trim(),
                string.Empty,
                string.Empty,
                string.Empty,
                CurrentUser.UserId.ToString(),
                100,
                0,
                ref rowCount, string.Empty,
                string.Empty

                );

            DataTable dt0 = dtSource.Tables[0];
            DataTable dt1 = dtSource.Tables[1];


            this.pageBar.RecordCount = rowCount;

            this.drResource.DataSource = dt1;
            this.drResource.DataBind();


            this.pageBar.RecordCount = 0;

        }




    }
}
