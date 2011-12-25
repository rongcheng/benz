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
using QJVRMS.Common;
using System.IO;

namespace WebUI.Modules
{
    public partial class OrderDetail :AuthPage
    {
        public static string _orderId = string.Empty;



        //public static string _orderId = string.Empty;
        public static int isUserRead = 0;
        public static int isAdminRead = 0;



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["id"];
                
                if (!string.IsNullOrEmpty(id))
                {
                    _orderId = id;
                    GetOrderDetail(id);





                    bind();

                    QJVRMS.Business.Orders obj = new QJVRMS.Business.Orders();
                    //订单下面的操作按钮
                    //有订单操作权限的人
                    if (obj.IsOrderAlertAdmin(CurrentUser.UserId) || IsSuperAdmin)
                    {
                        obj.UpdateOrderMessageStatusAdmin(new Guid(_orderId), 1);
                        isUserRead = 0;
                        isAdminRead = 1;
                    }
                    else
                    {
                        //这里没有判断是不是自己的订单
                        obj.UpdateOrderMessageStatusUser(new Guid(_orderId), 1);
                        isUserRead = 1;
                        isAdminRead = 0;

                    }





                }
            }
            
        }

        private void GetOrderDetail(string id)
        {
            QJVRMS.Business.Orders obj = new QJVRMS.Business.Orders();
            DataTable dt=obj.GetOrdersById(id).Tables[0];

            if (dt.Rows.Count > 0)
            { 
                DataRow dr=dt.Rows[0];
                this.txtContent.Text = dr["contents"].ToString().Replace("\n","<br/>");
                this.txtRD.Text = dr["RequestDate"].ToString();
                
                this.txtTitle.Text = dr["title"].ToString();
                this.txtUsage.Text = dr["usage"].ToString();
                this.txtUserName.Text = dr["userName"].ToString();
                this.txtAddDate.Text = dr["adddate"].ToString();

                int imgCount = obj.GetOrderResourceCount(id);
                if (imgCount > 0)
                {
                    this.linkImages.Visible = true;
                    this.linkImages.NavigateUrl = "~/ResourceList.aspx?orderid=" + id;
                }
                else
                {
                    this.linkImages.Visible = false;
                }

                bool isOrderUser=(this.txtUserName.Text.ToLower()==CurrentUser.UserLoginName.ToLower());



               
                if (Convert.ToInt32(dr["status"].ToString()) == (int)OrderStatus.NotPass)
                {
                    string reason = obj.GetOrderNotPassReason(id);
                    this.notPassReason.Text = reason;
                    this.pNotPass.Visible = true;
                }
                else
                {
                    this.pNotPass.Visible = false;
                }

                

                //订单下面的操作按钮
                //有订单操作权限的人
                if (!obj.IsOrderAlertAdmin(CurrentUser.UserId) && !IsSuperAdmin && !isOrderUser)
                {
                    this.tbCommand.Visible = false;
                    return;
                }

                this.tbCommand.Visible = true;
                int state = Convert.ToInt32(dr["status"].ToString());
                if (state == (int)OrderStatus.New)
                {
                    this.lbIsProcessing.Visible = true;
                    this.pNotPass1.Visible = true;
                    this.pImage.Visible = false;
                    this.pImageDel.Visible = false;
                    this.lbComplete.Visible = false;

                }
                else if (state == (int)OrderStatus.IsProcessing)
                {
                    this.lbIsProcessing.Visible = false;
                    this.pNotPass1.Visible = false;
                    this.pImage.Visible = true;
                    this.pImageDel.Visible = true;
                    this.lbComplete.Visible = true;

                }
                else if (state == (int)OrderStatus.NotPass)
                {
                    this.lbIsProcessing.Visible = false;
                    this.pNotPass1.Visible = false;
                    this.pImage.Visible = false;
                    this.pImageDel.Visible = false;
                    this.lbComplete.Visible = false;

                }
                else if (state == (int)OrderStatus.Completed)
                {
                    this.lbIsProcessing.Visible = false;
                    this.pNotPass1.Visible = false;
                    this.pImage.Visible = false;
                    this.pImageDel.Visible = false;
                    this.lbComplete.Visible = false;


                }
                else if (state == (int)OrderStatus.Confirmed)
                {
                    this.lbIsProcessing.Visible = false;
                    this.pNotPass1.Visible = false;
                    this.pImage.Visible = false;
                    this.pImageDel.Visible = false;
                    this.lbComplete.Visible = false;
                    this.pMessage.Visible = false;

                }


                if (isOrderUser)
                {
                    if (state == (int)OrderStatus.Confirmed)
                    {
                        this.tbCommand.Visible = true;
                    }
                    else
                    {
                        this.lbIsProcessing.Visible = false;
                        this.pNotPass1.Visible = false;
                        this.pImage.Visible = false;
                        this.pImageDel.Visible = false;
                        this.lbComplete.Visible = false;
                        this.pMessage.Visible = true;
                    }
                }
            }


        
        }

        protected void lbIsProcessing_Click(object sender, EventArgs e)
        {
            string mailBody = string.Empty;
            string mailSubject = string.Empty;
            string id = _orderId;
            int toStatus = (int)OrderStatus.IsProcessing;
            mailSubject = "您的订单被受理";
            QJVRMS.Business.Orders obj = new QJVRMS.Business.Orders();
            if (obj.UpdateStatus(id, toStatus))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>doAccept()</script>");

                //发送邮件
                mailBody = "订单";
                try
                {
                    DataSet ds = obj.GetOrdersById(id);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];

                        string _title = dr["title"].ToString();
                        string _rd = dr["RequestDate"].ToString();
                        string _size = dr["RequestSize"].ToString();
                        string _usage = dr["usage"].ToString();
                        string _content = dr["contents"].ToString();
                        string _addDate = dr["AddDate"].ToString();
                        string _userName = dr["userName"].ToString();


                        MemberShipManager objUserOP = new MemberShipManager();
                        User objUser = objUserOP.GetUser(dr["userName"].ToString());
                        string _userRealName = objUser.UserName;
                        string _userEmail = objUser.Email;

                        string templatePath = Server.MapPath("orderMailTemplate.htm");
                        mailBody = new StreamReader(templatePath).ReadToEnd();
                        mailBody = mailBody.Replace("{txtAddDate}", _addDate);
                        mailBody = mailBody.Replace("{txtUserName}", _userName);
                        mailBody = mailBody.Replace("{txtUserRealName}", _userRealName);
                        mailBody = mailBody.Replace("{txtContents}", _content.Replace("\r\n", "<br>"));
                        mailBody = mailBody.Replace("{txtUsage}", _usage);
                        mailBody = mailBody.Replace("{txtRD}", _rd);
                        mailBody = mailBody.Replace("{txtTitle}", _title);

                        string link = "<a href='http://{0}/{1}/Modules/UserProfile.aspx?tabid=2&orderStatus=" + toStatus.ToString() + "' target='_blank'>去我的订单中查看详细信息</a>";
                        mailBody = mailBody.Replace("{link}", string.Format(link, Request.Url.Authority, Request.ApplicationPath));

                        Session["MailBody"] = mailBody;
                        //obj.sendNewOrderToUser(_userEmail, mailSubject, mailBody);
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script language='javascript'>sendMail('" + _userEmail + "', '" + mailSubject + "')</script>");
                    }

                }
                catch (Exception ex)
                {
                    LogWriter.WriteExceptionLog(ex);
                }


            }
            
           
        }

        protected void lbComplete_Click(object sender, EventArgs e)
        {
            string mailBody = string.Empty;
            string mailSubject = string.Empty;
            string id = _orderId;
            int toStatus = (int)OrderStatus.Completed;
            mailSubject = "您的订单已经完成，请上线查看相关图片";
            QJVRMS.Business.Orders obj = new QJVRMS.Business.Orders();
            if (obj.UpdateStatus(id, toStatus))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>doAccept();</script>");
               
                //发送邮件
                mailBody = "订单";
                try
                {
                    DataSet ds = obj.GetOrdersById(id);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];

                        string _title = dr["title"].ToString();
                        string _rd = dr["RequestDate"].ToString();
                        string _size = dr["RequestSize"].ToString();
                        string _usage = dr["usage"].ToString();
                        string _content = dr["contents"].ToString();
                        string _addDate = dr["AddDate"].ToString();
                        string _userName = dr["userName"].ToString();


                        MemberShipManager objUserOP = new MemberShipManager();
                        User objUser = objUserOP.GetUser(dr["userName"].ToString());
                        string _userRealName = objUser.UserName;
                        string _userEmail = objUser.Email;

                        string templatePath = Server.MapPath("orderMailTemplate.htm");
                        mailBody = new StreamReader(templatePath).ReadToEnd();
                        mailBody = mailBody.Replace("{txtAddDate}", _addDate);
                        mailBody = mailBody.Replace("{txtUserName}", _userName);
                        mailBody = mailBody.Replace("{txtUserRealName}", _userRealName);
                        mailBody = mailBody.Replace("{txtContents}", _content.Replace("\r\n", "<br>"));
                        mailBody = mailBody.Replace("{txtUsage}", _usage);
                        mailBody = mailBody.Replace("{txtRD}", _rd);
                        mailBody = mailBody.Replace("{txtTitle}", _title);

                        string link = "<a href='http://{0}/{1}/Modules/UserProfile.aspx?tabid=2&orderStatus=" + toStatus.ToString() + "' target='_blank'>去我的订单中查看详细信息</a>";
                        mailBody = mailBody.Replace("{link}", string.Format(link, Request.Url.Authority, Request.ApplicationPath));


                        Session["MailBody"] = mailBody;
                        //obj.sendNewOrderToUser(_userEmail, mailSubject, mailBody);
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script language='javascript'>sendMail('" + _userEmail + "', '" + mailSubject + "')</script>");

                    }

                }
                catch (Exception ex)
                {
                    LogWriter.WriteExceptionLog(ex);
                }


            }

        }




        private void bind()
        {
            QJVRMS.Business.Orders obj = new QJVRMS.Business.Orders();
            DataTable dt = obj.GetOrderMessageByOrderId(new Guid(_orderId));
            this.rptOrderMessage.DataSource = dt;
            this.rptOrderMessage.DataBind();



        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            string content = this.txtMessage.Text.Trim().Replace("'", "''");
            if (content.Length < 1)
            {
                ShowMessage("内容不能为空");
                return;
            }
            QJVRMS.Business.Orders obj = new QJVRMS.Business.Orders();
            if (obj.AddOrderMessage(Guid.NewGuid(), new Guid(_orderId), content, DateTime.Now, CurrentUser.UserLoginName, isUserRead, isAdminRead))
            {
                ShowMessage("成功");
                bind();

                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>showGouTongYes();</script>");


            }
            else
            {
                ShowMessage("失败");
            }

        }
    }
}
