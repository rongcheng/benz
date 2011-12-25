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
using QJVRMS.Business;
using System.IO;
using QJVRMS.Common;

namespace WebUI.Modules.Manage
{
    public partial class OrdersManage :AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.myOrder_StartDate.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.AddMonths(-3).Month.ToString() + "-1";
                this.myOrder_EndDate.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                bindStatus();
                this.bindMyOrders();
            }
        }

        protected void btnSearchMyOrder_Click(object sender, EventArgs e)
        {
            this.bindMyOrders();           
        }


        private void bindStatus()
        {
            this.ddlStatus.DataSource = QJVRMS.Business.Orders.GetOrderStatus();
            this.ddlStatus.DataTextField = "CnName";
            this.ddlStatus.DataValueField = "ID";
            this.ddlStatus.DataBind();

            ListItem topItem = new ListItem("全部", "-1");
            this.ddlStatus.Items.Insert(0, topItem);
            this.ddlStatus.SelectedIndex = 0;
        }

        protected void bindMyOrders()
        {
            DateTime begin = Convert.ToDateTime(this.myOrder_StartDate.Text);
            DateTime end = Convert.ToDateTime(this.myOrder_EndDate.Text).AddDays(1);
            
            string userId = CurrentUser.UserId.ToString();
            int status = 0;
            int.TryParse(this.ddlStatus.SelectedValue, out status);

            QJVRMS.Business.Orders obj = new QJVRMS.Business.Orders();
            DataSet ds = obj.GetOrdersByUserId("", 1, 1, begin, end,status);

            this.grvOrders.DataSource = ds.Tables[0];
            this.grvOrders.DataBind();
        
        }
        protected void grvOrders_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvOrders.PageIndex = e.NewPageIndex;
            this.bindMyOrders();

        }

        protected void grvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToLower().Equals("page"))
            {
                return;
            }

            string mailBody = string.Empty;
            string mailSubject = string.Empty;

            int toStatus=0;
            if (e.CommandName.ToLower().Equals("isprocessing"))
            {
                toStatus=(int)OrderStatus.IsProcessing;
                mailSubject = "您的订单被受理";
                
            }
            else if (e.CommandName.ToLower().Equals("notpass"))
            {
                toStatus=(int)OrderStatus.NotPass;
                mailSubject = "您的订单没有被受理";
            }
            else if (e.CommandName.ToLower().Equals("selectimage"))
            {
                //toStatus=(int)OrderStatus.IsProcessing;
            }
            else if (e.CommandName.ToLower().Equals("complete"))
            {
                toStatus=(int)OrderStatus.Completed;
                mailSubject = "您的订单已经完成，请上线查看相关图片";
            }
            string id = e.CommandArgument.ToString();



            QJVRMS.Business.Orders obj = new QJVRMS.Business.Orders();
            if(obj.UpdateStatus(id,toStatus))
            {
                
                ShowMessage("操作完成");
                bindMyOrders();



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

                        string templatePath = Server.MapPath("../orderMailTemplate.htm");
                        mailBody = new StreamReader(templatePath).ReadToEnd();
                        mailBody = mailBody.Replace("{txtAddDate}", _addDate);
                        mailBody = mailBody.Replace("{txtUserName}", _userName);
                        mailBody = mailBody.Replace("{txtUserRealName}", _userRealName);
                        mailBody = mailBody.Replace("{txtContents}", _content.Replace("\r\n", "<br>"));
                        mailBody = mailBody.Replace("{txtUsage}", _usage);
                        mailBody = mailBody.Replace("{txtRD}", _rd);
                        mailBody = mailBody.Replace("{txtTitle}", _title);
                        //mailBody = mailBody.Replace("{host}", Request.Url.Authority);
                        //mailBody = mailBody.Replace("{apppath}", Request.ApplicationPath);
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

        protected void grvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Response.Write(e.Row.Cells[0].Text);

                int state = Convert.ToInt32(e.Row.Cells[0].Text);
                if (state == (int)OrderStatus.New)
                { 
                    ((LinkButton)e.Row.FindControl("lbIsProcessing")).Visible=true;
                    ((LinkButton)e.Row.FindControl("lbNotPass")).Visible=false;
                    //((LinkButton)e.Row.FindControl("lbSelectImage")).Visible=false;
                    ((LinkButton)e.Row.FindControl("lbComplete")).Visible = false;

                    ((Panel)e.Row.FindControl("pImage")).Visible = false;
                    ((Panel)e.Row.FindControl("pImageDel")).Visible = false;
                    ((Panel)e.Row.FindControl("pNotPass")).Visible = true;
                    

                
                }
                else if (state == (int)OrderStatus.IsProcessing)
                {
                    ((LinkButton)e.Row.FindControl("lbIsProcessing")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lbNotPass")).Visible = false;
                    //((LinkButton)e.Row.FindControl("lbSelectImage")).Visible = true;
                    ((LinkButton)e.Row.FindControl("lbComplete")).Visible = true;

                    ((Panel)e.Row.FindControl("pImage")).Visible = true;
                    ((Panel)e.Row.FindControl("pImageDel")).Visible = true;
                    ((Panel)e.Row.FindControl("pNotPass")).Visible = false;

                }
                else if (state == (int)OrderStatus.NotPass)
                {
                    ((LinkButton)e.Row.FindControl("lbIsProcessing")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lbNotPass")).Visible = false;
                    //((LinkButton)e.Row.FindControl("lbSelectImage")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lbComplete")).Visible = false;

                    ((Panel)e.Row.FindControl("pImage")).Visible = false;
                    ((Panel)e.Row.FindControl("pImageDel")).Visible = false;
                    ((Panel)e.Row.FindControl("pNotPass")).Visible = false;

                }
                else if (state == (int)OrderStatus.Completed)
                {
                    ((LinkButton)e.Row.FindControl("lbIsProcessing")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lbNotPass")).Visible = false;
                    //((LinkButton)e.Row.FindControl("lbSelectImage")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lbComplete")).Visible = false;

                    ((Panel)e.Row.FindControl("pImage")).Visible = false;
                    ((Panel)e.Row.FindControl("pImageDel")).Visible = false;

                    ((Panel)e.Row.FindControl("pNotPass")).Visible = false;

                }
                else if (state == (int)OrderStatus.Confirmed)
                {
                    ((LinkButton)e.Row.FindControl("lbIsProcessing")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lbNotPass")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lbComplete")).Visible = false;
                    ((Panel)e.Row.FindControl("pImage")).Visible = false;
                    ((Panel)e.Row.FindControl("pImageDel")).Visible = false;
                    ((Panel)e.Row.FindControl("pNotPass")).Visible = false;
                }

                //if (this.ddlStatus.SelectedValue == "-1")
                //{
                //    e.Row.Cells[5].Visible=true;                
                //}
                //else
                //{
                //    e.Row.Cells[5].Visible=false;
                //}
            }
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
         
        }

        public string GetStatus(string status)
        {
            DataTable dt = QJVRMS.Business.Orders.GetOrderStatus();
            DataRow[] drs=dt.Select("ID="+status);
            if (drs.Length == 1)
            {
                return drs[0]["CnName"].ToString();
            }

            return "";
            
        }
       
    }
}
