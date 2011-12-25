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
using System.Net.Mail;
using System.IO;
using QJVRMS.Common;
using System.Collections.Generic;

namespace WebUI.Modules
{
    public partial class OrderNew :AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.de_Date.Text = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd"); //默认一个月
                this.bindUsage();

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string _title=this.txtTitle.Text.Trim().Replace("'","''");
            string _rd=this.de_Date.Text;
            string _usage = this.ddlUsage.SelectedValue;
            string _content=this.txtContent.Text;

            _content += "\r\n工程性质：" + this.rblType.SelectedValue;
            _content += "\r\n主要关注点：" + this.rblFocus.SelectedValue;

            DateTime dtRq=DateTime.Now.AddMonths(1);
            try
            {
                dtRq = Convert.ToDateTime(_rd);
            }
            catch (Exception ex)
            {
                dtRq = DateTime.Now.AddMonths(1);
            }

            if (dtRq < DateTime.Today)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>orderDate();</script>");
                return;
            
            }
            
            string userId = CurrentUser.UserId.ToString();
            string userName = CurrentUser.UserLoginName.ToString();
            string userRealName=CurrentUser.UserName.ToString();
            QJVRMS.Business.Orders obj = new QJVRMS.Business.Orders();
            obj.Add(_title, dtRq, 0, _usage, _content, (int)OrderStatus.New,userId,userName);
            
            //发送邮件
            string mailBody="有新订单";
            try
            {
                string templatePath = Server.MapPath("orderMailTemplate.htm");
                mailBody = new StreamReader(templatePath).ReadToEnd();
                mailBody = mailBody.Replace("{txtAddDate}", DateTime.Now.ToString());
                mailBody = mailBody.Replace("{txtUserName}", userName);
                mailBody = mailBody.Replace("{txtUserRealName}", userRealName);
                mailBody = mailBody.Replace("{txtContents}", _content.Replace("\r\n","<br>"));
                mailBody = mailBody.Replace("{txtUsage}", _usage);
                mailBody = mailBody.Replace("{txtRD}", dtRq.ToString("yyyy-MM-dd"));
                mailBody = mailBody.Replace("{txtTitle}", _title);

                //mailBody = mailBody.Replace("{host}", Request.Url.Authority);
                //mailBody = mailBody.Replace("{apppath}", Request.ApplicationPath);
                
                //string link="<a href='http://{0}/{1}/Modules/UserProfile.aspx?tabid=2' target='_blank'>去我的订单中查看详细信息</a>";
                string link = "<a href='http://{0}/{1}/Modules/Manage/OrdersManage.aspx?mi=1' target='_blank'>马上去处理该订单</a>";
                mailBody = mailBody.Replace("{link}",string.Format(link,Request.Url.Authority,Request.ApplicationPath));
            }
            catch (Exception ex)
            {
                LogWriter.WriteExceptionLog(ex);
            }

            Session["OrderNew"] = mailBody;
            //obj.sendNewOrder(mailBody);
       
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>orderOk();</script>");

       
        }

        protected void ddlUsage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bindUsage()
        {
            List<Usage> usageList = Usage.GetUsageList();
            this.ddlUsage.DataSource = usageList;
            this.ddlUsage.DataTextField = "UsageName";
            this.ddlUsage.DataValueField = "UsageName";
            this.ddlUsage.DataBind();
        }
    }
}
