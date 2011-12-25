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

namespace WebUI.Modules.Manage
{
    public partial class FunctionManager : AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            this.BindFunction();
            this.btn_Cancel.CausesValidation = false;
            this.lb_Info.Text = "";

        }

        protected override void OnInit(EventArgs e)
        {
            this.IsInControl = true;
            base.OnInit(e);
        }

        private void BindFunction()
        {
            List<Function> FunctionListAll = Function.GetFunctionList();
            this.gv_Function.DataSource = FunctionListAll;
            this.gv_Function.DataBind();
        }

        private void BindTopFunction()
        {
            IList<Function> topFunctionList = new Function().GetTopFunctionList();
            this.ddlParentFunction.DataSource = topFunctionList;
            this.ddlParentFunction.DataTextField = "FunctionName";
            this.ddlParentFunction.DataValueField = "FunctionID";
            this.ddlParentFunction.DataBind();

            ListItem item = new ListItem("--平台管理--", "");
            this.ddlParentFunction.Items.Insert(0, item);
        
        }


        protected void gv_Usage_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string FunctionID = this.gv_Function.DataKeys[e.RowIndex].Value.ToString();

            if (Function.DeleteFunctionByFunctionID(new Guid(FunctionID)))
            {
                this.lb_Info.Text = "删除成功";
            }
            else
            {
                this.lb_Info.Text = "删除失败";
            }
            this.BindFunction();
        }
        protected void gv_Function_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.tr_FunctionInfo.Visible = true;
            this.txt_FunctionName.Text = this.gv_Function.Rows[e.NewEditIndex].Cells[0].Text;
            this.txt_UrlPath.Text = this.gv_Function.Rows[e.NewEditIndex].Cells[1].Text;
            this.txt_Description.Text = this.gv_Function.Rows[e.NewEditIndex].Cells[2].Text;
            this.txt_orderFlag.Text = this.gv_Function.Rows[e.NewEditIndex].Cells[3].Text;
            this.Hidden1.Value = this.gv_Function.DataKeys[e.NewEditIndex]["FunctionId"].ToString();
            this.BindTopFunction();

            if (this.gv_Function.DataKeys[e.NewEditIndex]["ParentFunctionId"] == null)
            {
                this.ddlParentFunction.SelectedValue = "";
            }
            else
            {
                this.ddlParentFunction.SelectedValue = this.gv_Function.DataKeys[e.NewEditIndex]["ParentFunctionId"].ToString();
            }
            
        }

        protected void btn_AddFunction_Click(object sender, EventArgs e)
        {
            this.tr_FunctionInfo.Visible = true;
            this.txt_FunctionName.Text = "";
            this.txt_UrlPath.Text = "";
            this.txt_Description.Text = "";
            this.txt_orderFlag.Text = "";          
            this.Hidden1.Value = "";
            this.BindTopFunction();
        }

        protected void btn_Conform_Click(object sender, EventArgs e)
        {
            try
            {
                int.Parse(this.txt_orderFlag.Text.Trim());
            }
            catch
            {
                this.lb_Info.Text = "排序号必须为数字";
                return;
            }
            Function fl = new Function();

            fl.Description = this.txt_Description.Text.Trim();
            fl.FunctionName = this.txt_FunctionName.Text.Trim();
            fl.OrderFlag = int.Parse(this.txt_orderFlag.Text.Trim());
            fl.UrlPath = this.txt_UrlPath.Text.Trim();


            if (this.ddlParentFunction.SelectedValue.Equals(""))
            {
                fl.ParentFunctionId = null;
            }
            else
            {
                fl.ParentFunctionId = new Guid(this.ddlParentFunction.SelectedValue);
            }
             

            if (this.Hidden1.Value == "")//新增
            {
                if (Function.AddFunction(fl))
                {
                    this.lb_Info.Text = "新增成功";
                }
                else
                {
                    this.lb_Info.Text = "新增失败";
                }
            }
            else//更新
            {
                fl.FunctionID = new Guid(this.Hidden1.Value);
                if (Function.UpdateFunction(fl))
                {
                    this.lb_Info.Text = "更新成功";
                }
                else
                {
                    this.lb_Info.Text = "更新失败";
                }
            }
            this.tr_FunctionInfo.Visible = false;
            this.BindFunction();
          
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.tr_FunctionInfo.Visible = false;
        }

        protected void gv_Function_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType==DataControlRowType.DataRow)
            {
                if (this.gv_Function.DataKeys[e.Row.RowIndex]["ParentFunctionId"] == null)
                {
                    //e.Row.CssClass = "bigRow";
                    e.Row.Style.Add("background-color", "#eeeeee");
                    //Response.Write(e.Row.RowIndex);
                }
                else
                {
                    //e.Row.CssClass = "smallRow";
                    e.Row.Style.Add("background-color", "#ffffff");
                }
            }
        }
    }
}
