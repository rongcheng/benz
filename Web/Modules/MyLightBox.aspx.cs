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
    public partial class MyLightBox :AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bind();
                bindGrid();
            }

        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            string txtName = this.txtName.Text.Trim().Replace("'", "''");

            Resource r = new Resource();
            if (r.AddLightBox(txtName, CurrentUser.UserId, "", DateTime.Now))
            {
                ShowMessage("添加成功");
            }
            else
            {
                ShowMessage("添加失败，请稍后再试");
            }

            bindGrid();

        }

        private void bind()
        {
            Resource r = new Resource();
            DataSet ds = r.GetMyLightBox(CurrentUser.UserId);
            this.rptKey.DataSource = ds.Tables[0].DefaultView;
            this.rptKey.DataBind();
        }

        private void bindGrid()
        {
            Resource r = new Resource();
            DataSet ds = r.GetMyLightBox(CurrentUser.UserId);
            this.grvMyLightBox.DataSource = ds.Tables[0].DefaultView;
            this.grvMyLightBox.DataBind();
        }

        

        protected void rptKey_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                if (e.CommandName.ToLower().Equals("del"))
                {
                    string id=e.CommandArgument.ToString();


                    Resource r = new Resource();
                    r.DelLightBox(new Guid(id));



                    bind();

                }

            }

        }

        protected void grvMyLightBox_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvMyLightBox.EditIndex = -1;
            bindGrid();
        }

        protected void grvMyLightBox_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            //Response.Write("del");
        }

        protected void grvMyLightBox_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.grvMyLightBox.EditIndex = e.NewEditIndex;
            bindGrid();
            
        }

        protected void grvMyLightBox_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            

        }

        protected void grvMyLightBox_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = this.grvMyLightBox.DataKeys[e.RowIndex].Value.ToString();
            
            Resource r = new Resource();
            r.DelLightBox(new Guid(id));

            bindGrid();
        }

        protected void grvMyLightBox_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           // Response.Write(e.CommandArgument);
        }

        protected void grvMyLightBox_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id = this.grvMyLightBox.DataKeys[e.RowIndex].Value.ToString();
            
            string str = ((TextBox)this.grvMyLightBox.Rows[e.RowIndex].Cells[0].Controls[0]).Text.Trim();
            Resource r = new Resource();
            r.EditLightBox(new Guid(id), str, "", DateTime.MaxValue);

            this.grvMyLightBox.EditIndex = -1;
            bindGrid();
        }
    }
}
