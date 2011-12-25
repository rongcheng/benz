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
    public partial class SourceManage : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        //    if (!this.IsPostBack)
        //    {
                this.BindGV(); 
            //}
            this.lb_Info.Text = "";
            this.btn_CancelAdd.CausesValidation = false;
        }

        private void BindGV()
        {
            //����Դ������Ϊ��ǰ�û�ID          
            List<Source> sourceList = Source.GetSourceList();
            this.gv_Source.DataSource = sourceList;
            this.gv_Source.DataBind();
        }

        protected void btn_ShowAddSource_ServerClick(object sender, EventArgs e)
        {
            this.panelShow.Visible = true;
            this.btn_ShowAddSource.Visible = false;
        }

        protected void btn_AddSource_Click(object sender, EventArgs e)
    {         
            //if(string.IsNullOrEmpty(this.txt_SourceName.Text.Trim()))
            //{
            //    this.lb_Info.Text = "��������Դ";
            //    return;
            //}
            Source us = new Source();
            us.SourceName = this.txt_SourceName.Text.Trim();
            us.SourceDesc = this.txt_SourceDesc.Text.Trim();
            us.GroupID = this.CurrentUser.UserGroupId;
            if (Source.AddSource(us))
            {
                this.lb_Info.Text = "��ӳɹ�";
            }
            else
            {
                this.lb_Info.Text = "���ʧ��";
            }          
            this.BindGV(); 
        }

        protected void btn_AddSourceAndDone_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_SourceName.Text.Trim()))
            {
                this.lb_Info.Text = "��������Դ";
                return;
            }
            Source us = new Source();
            us.SourceName = this.txt_SourceName.Text.Trim();
            us.SourceDesc = this.txt_SourceDesc.Text.Trim();
            us.GroupID = this.CurrentUser.UserGroupId;
            if (Source.AddSource(us))
            {
                this.lb_Info.Text = "��ӳɹ�";
            }
            else
            {
                this.lb_Info.Text = "���ʧ��";
            }           
            this.BindGV();
            //
            this.panelShow.Visible = false;
            this.btn_ShowAddSource.Visible = true;
        }

        protected void btn_CancelAdd_Click(object sender, EventArgs e)
        {
            this.panelShow.Visible = false;
            this.btn_ShowAddSource.Visible = true;
        }

        #region GridView�¼�

        protected void gv_Source_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_Source.EditIndex = -1;
            this.BindGV();
        }

        protected void gv_Source_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int SourceID = int.Parse(this.gv_Source.DataKeys[e.RowIndex].Value.ToString());

            if (Source.DeleteSourceBySourceID(SourceID))
            {
                this.lb_Info.Text = "ɾ���ɹ�";
            }
            else
            {
                this.lb_Info.Text = "ɾ��ʧ��";
            }
            this.BindGV();
        }

        protected void gv_Source_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_Source.EditIndex = e.NewEditIndex;
            this.BindGV();
        }

        protected void gv_Source_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Source us = new Source();
            us.SourceID = int.Parse(this.gv_Source.DataKeys[e.RowIndex].Value.ToString());
            us.SourceName = ((TextBox)this.gv_Source.Rows[e.RowIndex].Cells[0].Controls[1]).Text;
            if (string.IsNullOrEmpty(us.SourceName))
            {
                this.lb_Info.Text = "��Դ����Ϊ��";
                this.BindGV();
                this.gv_Source.SelectedIndex = e.RowIndex;
                return;
                
            }
            us.SourceDesc = ((TextBox)this.gv_Source.Rows[e.RowIndex].Cells[1].Controls[1]).Text;

            if (Source.UpdateSource(us))
            {
                this.lb_Info.Text = "���³ɹ�";
            }
            else
            {
                this.lb_Info.Text = "����ʧ��";
            }
            this.gv_Source.EditIndex = -1;
            this.BindGV();
        }
        #endregion

       

      
    }
}