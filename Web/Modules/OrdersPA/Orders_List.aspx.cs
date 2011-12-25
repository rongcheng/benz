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
using QJVRMS.Business;

namespace WebUI.Modules.Orders
{
    public partial class Orders_List : AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lErrorInfo.Text = string.Empty;
            if (!IsPostBack)
            {
                LoadData();
            }
            
        }       
        private void LoadData()
        {
            string currentUser = string.Empty;
            
            //如果不是管理员只能看自己的
            if (!IsSuperAdmin)
            {
                currentUser = CurrentUser.UserId.ToString();
            }
            else
            {
             //   lk_back.Visible = true; //管理员显示返回"管理页"的链接                
            }

            OrdersBiz biz = new OrdersBiz();
            DataTable dt = biz.GetOrdersList(txtOrderId.Text.Trim(),currentUser,
                                            txtStartDate.Text, txtEndDate.Text, string.Empty,
                                            int.Parse(ddlState.SelectedValue));

            gvOrdersList.DataSource = dt;
            gvOrdersList.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvOrdersList.PageIndex = 0;
            LoadData();
        }

        protected void gvOrdersList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrdersList.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void lblState_DataBinding(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            lbl.Text = OrdersBiz.GetStateString(int.Parse(lbl.Text));
        }

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            if (ChangeState(lnk.CommandArgument, 1) > 0)
            {
                ShowMessage("提交成功！");
                LoadData();
            }
            else
            {
                ShowMessage("提交失败！");
            }
        }

        protected void lnkBack_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            if (ChangeState(lnk.CommandArgument, 0) > 0)
            {
                ShowMessage("退回成功！");
                LoadData();
            }
            else
            {
                ShowMessage("退回失败！");
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            OrdersBiz biz = new OrdersBiz();
            LinkButton lnk = sender as LinkButton;

            if (biz.DeleteOrders(lnk.CommandArgument) > 0)
            {
                ShowMessage("删除成功！");
                LoadData();
            }
            else
            {
                ShowMessage("删除失败！");
            }
        }

        
        protected void lnkOver_Click(object sender, EventArgs e)
        {
            OrdersBiz biz = new OrdersBiz();
            LinkButton lnk = sender as LinkButton;

            if (ChangeState(lnk.CommandArgument, 2) > 0)
            {
                ShowMessage("通过成功！");
                LoadData();
            }
            else
            {
                ShowMessage("通过失败！");
            }
        }

        /// <summary>
        /// 更改订单状态
        /// </summary>
        /// <param name="state"></param>
        private int ChangeState(string orderId, int state)
        {
            OrdersBiz biz = new OrdersBiz();
            OrderInfo model = biz.GetOrderInfo(orderId);
            if (model == null)
            {
                return 0;
            }
            model.Operator = CurrentUser.UserId.ToString();
            model.State = state;
            return biz.UpdateOrders(model);
        }
        /// <summary>
        /// 取用户姓名
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        protected string GetUserName(string  userID)
        {
            MemberShipManager manager = new MemberShipManager();
            User user = manager.GetUser(new Guid(userID));
            return user.UserName;
        }

        protected void lblUser_DataBinding(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            MemberShipManager manager = new MemberShipManager();
            lbl.Text = manager.GetUser(new Guid(lbl.Text)).UserName;
        }
    }
}
