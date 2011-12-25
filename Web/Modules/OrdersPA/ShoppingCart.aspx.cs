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
using System.Collections.Generic;

using QJVRMS.Business;

namespace WebUI.Modules.Orders
{
    public partial class ShoppingCart : AuthPage
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
            HttpCookie cookie = Request.Cookies.Get("ShoppingCart");
            DataTable dt = new DataTable();
            if (cookie != null)
            {
                if (cookie["GiftId"] != null && cookie["GiftCount"] != null)
                {
                    string[] giftIds = cookie["GiftId"].Split(new char[] { ',' });
                    string[] giftCounts = cookie["GiftCount"].Split(new char[] { ',' });

                    GiftBiz biz = new GiftBiz();

                    dt.Columns.Add("giftid");
                    dt.Columns.Add("gifttitle");
                    dt.Columns.Add("giftcount");
                    dt.Columns.Add("Quantity");

                    for (int i = 0; i < giftIds.Length; i++)
                    {
                        if (giftIds[i] != string.Empty)
                        {
                            DataRow newRow = dt.NewRow();
                            newRow["giftid"] = giftIds[i];
                            newRow["giftcount"] = giftCounts[i];

                            GiftInfo model = biz.GetModel(giftIds[i]);
                            if (model == null)
                            {
                                continue;
                            }
                            newRow["gifttitle"] = model.Title;
                            newRow["Quantity"] = model.Quantity;

                            dt.Rows.Add(newRow);
                        }
                    }
                }
            }
            MemberShipManager msm = new MemberShipManager();
            User cuser  = msm.GetUser(CurrentUser.UserId);
            Contactor.Text = CurrentUser.UserName;
            Tel.Text = cuser.Telphone;
            Email.Text = cuser.Email;
            order_bar.Visible = (dt.Rows.Count > 0);
            lnkSubmitOrder.Visible = false;

            gvShoppingCartList.DataSource = dt;
            gvShoppingCartList.DataBind();
        }

        /// <summary>
        /// ���ɶ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkCreateOrder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(address.Text))
            {
                ShowMessage("����д���͵�ַ.");
                address.Focus();
                return;
            }           
            
           string orderId = CreateOrder(1);
            if (orderId != string.Empty)
            {
                MemberShipManager msm = new MemberShipManager();
                string url = "http://" + Request.Url.Host + "/Modules/Orders/Orders_Info.aspx?orderId=" + orderId;

                List<string> mailAddress = new List<string>();
                //ȡ��������
                mailAddress.Add(msm.GetUser(SuperAdminLoginId).Email);
                //mailAddress.Add(ConfigurationManager.AppSettings["ManagerMail"]);
               
                 
                string title = "����/��Ʒ����-���� "+CurrentUser.UserName ;
                System.Text.StringBuilder content = new System.Text.StringBuilder();
                content.Append("�û� ");
                content.Append(CurrentUser.UserName);
                content.Append(" �ύ�¶���");
                content.Append("<br/>");
                content.Append("<a target='_blank' href='" + url + "'>" + url + "</a>");
                content.Append("<br/>");
                content.Append("��¼ϵͳ�������Ƶ�ַ�������");

                //MailManager.SendMail(mailAddress, title, content.ToString());
                //ShowAndRedirect("�������ύ", "/Modules/UserProfile.aspx?mode=order");
                ShowMessage("�������ύ");
                Response.Redirect("/Modules/UserProfile.aspx?tabId=2");
                
            }
        }

        /// <summary>
        /// ���ɶ������ύ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkSubmitOrder_Click(object sender, EventArgs e)
        {
            CreateOrder(1);
        }

        /// <summary>
        /// ���ɶ�������ָ������״̬
        /// </summary>
        private string CreateOrder(int orderState)
        {           
            OrdersBiz biz = new OrdersBiz();
            OrderInfo orderModel = new OrderInfo();
            GiftBiz giftBiz = new GiftBiz();

            //��Ӷ�����Ϣ
            orderModel.OrderId = biz.GetNewOrderId();
            orderModel.UserId = CurrentUser.UserId.ToString();
            orderModel.Operator = CurrentUser.OperatorId.ToString();
            orderModel.State = orderState;
            orderModel.Address = address.Text;
            orderModel.Contactor = Contactor.Text;
            orderModel.Tel = Tel.Text;
            orderModel.Email = Email.Text;

            Orders_DetailInfo[] details = new Orders_DetailInfo[gvShoppingCartList.Rows.Count];
            GiftInfo[] giftInfoList = new GiftInfo[gvShoppingCartList.Rows.Count];
            int i = 0;

            //��Ӷ�����ϸ���ȼ�������Ƿ��㹻
            foreach (GridViewRow row in gvShoppingCartList.Rows)
            {
                Label lblGiftId = row.FindControl("lblGiftId") as Label;
                TextBox txtCount = row.FindControl("txtCount") as TextBox;
                TextBox txtUsage = row.FindControl("txtUsage") as TextBox;

                Orders_DetailInfo detailModel = new Orders_DetailInfo();
                detailModel.OrderId = orderModel.OrderId;
                detailModel.GiftId = lblGiftId.Text;
                detailModel.GiftCount = int.Parse(txtCount.Text);
                detailModel.Usage = txtUsage.Text;

                GiftInfo giftModel = giftBiz.GetModel(detailModel.GiftId);

                if (giftModel.Quantity < detailModel.GiftCount)
                {
                    lErrorInfo.Text = "��Ʒ��" + giftModel.Title + "���������㣬ʣ������Ϊ��" + giftModel.Quantity.ToString() + "��";
                    return string.Empty;
                }

                giftInfoList[i] = giftModel;
                details[i] = detailModel;
                i++;
            }

            //��������
            for (i = 0; i < details.Length; i++)
            {
                biz.AddOrders_Detail(details[i]);
                giftInfoList[i].Quantity -= details[i].GiftCount;
                giftBiz.UpdateGift(giftInfoList[i]);
            }
            biz.AddOrders(orderModel);

            //������ﳵ
            HttpCookie cookie = Request.Cookies["ShoppingCart"];
            cookie.Expires = DateTime.Now.AddHours(-2);
            Response.Cookies.Add(cookie);
            return orderModel.OrderId;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            address.Visible = true;
            lnkCreateOrder.Visible = true;
        }
    }
}
