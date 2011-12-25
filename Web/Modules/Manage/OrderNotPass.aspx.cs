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


namespace WebUI.Modules.Manage
{
    public partial class OrderNotPass : AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 string itemID = Request["orderId"];
                 this.hItemId.Value = itemID;
            }
        }


        protected void btnValidate_Click(object sender, EventArgs e)
        {
            
            string reason = this.txtReason.Text.Trim().Replace("'", "''");
            string id = this.hItemId.Value;
            QJVRMS.Business.Orders obj = new QJVRMS.Business.Orders();
            if (obj.UpdateStatus(id,(int)OrderStatus.NotPass))
            {
                obj.AddOrderNotPassReason(id, reason);
                ShowMessage("操作完成");


                //发送邮件
                string mailBody = "订单";
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
                     

                        string link = "<a href='http://{0}/{1}/Modules/UserProfile.aspx?tabid=2&orderStatus="+((int)OrderStatus.NotPass).ToString()+"' target='_blank'>去我的订单中查看详细信息</a>";
                        mailBody = mailBody.Replace("{link}", string.Format(link, Request.Url.Authority, Request.ApplicationPath));

                        //这里需要改变一下，将收件人的地址加上
                        string mailSubject = "您的订单没有被受理";
                        Session["MailBody"] = mailBody;
                        //obj.sendNewOrderToUser(_userEmail, mailSubject, mailBody);

                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script language='javascript'>doClose('" + _userEmail + "', '" + mailSubject + "')</script>");
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.WriteExceptionLog(ex);
                }





            }

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script language='javascript'>doClose();</script>");

        }
    }
}
