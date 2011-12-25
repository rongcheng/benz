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
    public partial class TJInfo : AuthPage
    {
        protected static DataSet ds = QJVRMS.Business.Group.GetGroupUsersStat();
        protected static DataTable dt = QJVRMS.Business.Catalog.GetCategoryPicCount();


        protected void Page_Load(object sender, EventArgs e)
        {
            //if (this.Request["manage"] == null || this.Request["manage"] == "source")
            //{
            //    Control uc = new Control();
            //    uc = Page.LoadControl("/UserControls/UserGroupTJ.ascx");

            //    this.uc_Cells.Controls.Add(uc);
            //}

            //if (this.Request["manage"] == "usage")
            //{
            //    Control uc1 = new Control();
            //    uc1 = Page.LoadControl("/UserControls/CategoryTJ.ascx");

            //    this.uc_Cells.Controls.Add(uc1);
            //}

            if (!this.IsPostBack)
            {
              

                BindGroup();
                BindRoleUsers();

                CataStat();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.IsInControl = true;
            base.OnInit(e);
        }




        #region 分类统计



        protected void CataStat()
        {
            this.cataStat.DataSource = dt;
            this.cataStat.DataBind();
        }

        
        #endregion


        #region 用户统计

        


        protected void BindGroup()
        {

            QJVRMS.Business.IGroup g = QJVRMS.Business.Group.GetRootGroup();

            ListItem li = new ListItem();
            li.Text = g.GroupName;
            li.Value = g.GroupId.ToString();

            this.ddlUserType.Items.Add(li);


            //DataTable groupTable = ds.Tables[0];

            //this.ddlUserType.DataSource = groupTable;
            //this.ddlUserType.DataTextField = "GroupName";
            //this.ddlUserType.DataValueField = "GroupId";
            //this.ddlUserType.DataBind();

        }

        public void BindRoleUsers()
        {
  
            DataTable roleTable = ds.Tables[1];


            string groupId = this.ddlUserType.SelectedValue;

            DataView dv = roleTable.DefaultView;
            dv.RowFilter = "GroupId='" + groupId + "'";

            this.statList.DataSource = dv;
            this.statList.DataBind();


        }


        protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRoleUsers();
        }

        #endregion



    }
}
