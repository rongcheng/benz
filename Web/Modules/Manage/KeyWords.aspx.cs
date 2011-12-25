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

namespace WebUI.Modules.Manage
{
    public partial class KeyWords : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.bindCatalog();
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string txtName = this.txtName.Text.Trim().Replace("'", "''");
            string txtOrder = this.txtOrder.Text.Trim();

            int sort;
            int.TryParse(txtOrder, out sort);

            QJVRMS.Business.KeyWords obj = new QJVRMS.Business.KeyWords();
            obj.Add(0, txtName, sort);

            bindCatalog();
        }

        private void bindCatalog()
        {
            QJVRMS.Business.KeyWords obj = new QJVRMS.Business.KeyWords();
            DataSet ds=obj.GetKeywordsByParentid(0);
            
            this.grvKeyCatalog.DataSource = ds.Tables[0].DefaultView;
            this.grvKeyCatalog.DataBind();
           
        }

        protected void grvKeyCatalog_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {

        }

        protected void grvKeyCatalog_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(this.grvKeyCatalog.DataKeys[e.RowIndex].Value.ToString());

            QJVRMS.Business.KeyWords obj = new QJVRMS.Business.KeyWords();
            obj.Delete(id);

            this.bindCatalog();
            
        }

        protected void grvKeyCatalog_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.grvKeyCatalog.EditIndex = e.NewEditIndex;
            this.bindCatalog();

        }

        protected void grvKeyCatalog_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            

            string keyword=((TextBox)(this.grvKeyCatalog.Rows[e.RowIndex].Cells[0].Controls[0])).Text.ToString().Trim().Replace("'", "''");
            int sort;
            int.TryParse(((TextBox)(this.grvKeyCatalog.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim().Replace("'", "''"), out sort);
            int parentId = 0;

            int id=Convert.ToInt32(this.grvKeyCatalog.DataKeys[e.RowIndex].Value.ToString());

            QJVRMS.Business.KeyWords obj = new QJVRMS.Business.KeyWords();
            obj.UpdateById(id, parentId, keyword, sort);

            this.grvKeyCatalog.EditIndex = -1;
            this.bindCatalog();

        }

        protected void grvKeyCatalog_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvKeyCatalog.EditIndex = -1;
            this.bindCatalog();

        }
    }
}
