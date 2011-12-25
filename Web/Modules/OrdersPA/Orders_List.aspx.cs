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
            
            //������ǹ���Աֻ�ܿ��Լ���
            if (!IsSuperAdmin)
            {
                currentUser = CurrentUser.UserId.ToString();
            }
            else
            {
             //   lk_back.Visible = true; //����Ա��ʾ����"����ҳ"������                
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
                ShowMessage("�ύ�ɹ���");
                LoadData();
            }
            else
            {
                ShowMessage("�ύʧ�ܣ�");
            }
        }

        protected void lnkBack_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            if (ChangeState(lnk.CommandArgument, 0) > 0)
            {
                ShowMessage("�˻سɹ���");
                LoadData();
            }
            else
            {
                ShowMessage("�˻�ʧ�ܣ�");
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            OrdersBiz biz = new OrdersBiz();
            LinkButton lnk = sender as LinkButton;

            if (biz.DeleteOrders(lnk.CommandArgument) > 0)
            {
                ShowMessage("ɾ���ɹ���");
                LoadData();
            }
            else
            {
                ShowMessage("ɾ��ʧ�ܣ�");
            }
        }

        
        protected void lnkOver_Click(object sender, EventArgs e)
        {
            OrdersBiz biz = new OrdersBiz();
            LinkButton lnk = sender as LinkButton;

            if (ChangeState(lnk.CommandArgument, 2) > 0)
            {
                ShowMessage("ͨ���ɹ���");
                LoadData();
            }
            else
            {
                ShowMessage("ͨ��ʧ�ܣ�");
            }
        }

        /// <summary>
        /// ���Ķ���״̬
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
        /// ȡ�û�����
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
