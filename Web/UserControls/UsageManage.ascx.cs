using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using QJVRMS.Business;


namespace WebUI.UserControls
{
    public partial class UsageManage : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if(!this.IsPostBack)
            //{
                this.BindGV();  
            //}
            this.lb_Info.Text = "";
            this.btn_CancelAdd.CausesValidation = false;
        }

        private void BindGV()
        {
            //绑定用途表，参数为当前用户ID;
            List<Usage> usageList = Usage.GetUsageList();
            this.gv_Usage.DataSource = usageList;
            this.gv_Usage.DataBind();
        }
        protected void btn_ShowAddUsage_ServerClick(object sender, EventArgs e)
        {
            // this.tr_AddUsage.Visible = true;
            panelShow.Visible = true;
            this.btn_ShowAddUsage.Visible = false;
        }

        //protected void btn_AddUsage_Click(object sender, EventArgs e)
        //{           
        //    Usage us = new Usage();
        //    us.UsageName = this.txt_UsageName.Text.Trim();
        //    us.UsageDesc = this.txt_UsageDesc.Text.Trim();
        //    us.GroupID = this.CurrentUser.UserGroupId;
        //    if (Usage.AddUsage(us))
        //    {
        //        QJVRMS.Business.Usage.usageTable = null;
        //        this.lb_Info.Text = "添加成功";
        //    }
        //    else
        //    {
        //        this.lb_Info.Text = "添加失败";
        //    }                               
           
        //    this.BindGV(); 
        //}

        protected void btn_AddUsageAndDone_Click(object sender, EventArgs e)
        {
           
            Usage us = new Usage();
            us.UsageName = this.txt_UsageName.Text.Trim();
            us.UsageDesc = this.txt_UsageDesc.Text.Trim();
            us.GroupID = this.CurrentUser.UserGroupId;
            if (Usage.AddUsage(us))
            {
                this.lb_Info.Text = "添加成功";
                QJVRMS.Business.Usage.usageTable = null;
            }
            else
            {
                this.lb_Info.Text = "添加失败";
            }               
          
            this.BindGV();

            this.panelShow.Visible = false;
            this.btn_ShowAddUsage.Visible = true;
        }

        protected void btn_CancelAdd_Click(object sender, EventArgs e)
        {
            panelShow.Visible = false;
            this.btn_ShowAddUsage.Visible = true;
        }

        #region GridView事件

        protected void gv_Usage_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_Usage.EditIndex = -1;
            this.BindGV();
        }

        protected void gv_Usage_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int UsageID =  int.Parse(this.gv_Usage.DataKeys[e.RowIndex].Value.ToString());

            if (Usage.DeleteUsageByUsageID(UsageID))
            {
                QJVRMS.Business.Usage.usageTable = null;
                this.lb_Info.Text = "删除成功";
            }
            else
            {
                this.lb_Info.Text = "删除失败";
            }
            this.BindGV();
        }

        protected void gv_Usage_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_Usage.EditIndex = e.NewEditIndex;
            this.BindGV();
        }

        protected void gv_Usage_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Usage us = new Usage();
            us.UsageID = int.Parse(this.gv_Usage.DataKeys[e.RowIndex].Value.ToString());
            us.UsageName = ((TextBox)this.gv_Usage.Rows[e.RowIndex].Cells[0].Controls[1]).Text;
            if (string.IsNullOrEmpty(us.UsageName))
            {
                this.lb_Info.Text = "用途不能为空";
                this.BindGV();
                this.gv_Usage.SelectedIndex = e.RowIndex;
                return;

            }
            us.UsageDesc = ((TextBox)this.gv_Usage.Rows[e.RowIndex].Cells[1].Controls[1]).Text;

            if (Usage.UpdateUsage(us))
            {
                QJVRMS.Business.Usage.usageTable = null;
                this.lb_Info.Text = "更新成功";
            }
            else
            {
                this.lb_Info.Text = "更新失败";
            }
            this.gv_Usage.EditIndex = -1;
            this.BindGV();
        }
        #endregion

       

       
      

       
    }
}