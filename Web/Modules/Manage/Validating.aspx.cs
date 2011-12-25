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
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;


namespace WebUI.Modules.Manage
{
    public partial class Validating :  AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 string toStatus = Request["validateStatus"];
                 string itemID = Request["itemId"];

                if (string.IsNullOrEmpty(toStatus) || string.IsNullOrEmpty(itemID))
                {
                    //Response.Write("0");
                    //Response.End();
                }
                else if (toStatus == "1")
                {
                    string[] ids = new string[] { itemID };
                    Resource obj = new Resource();
                    obj.ValidateResourceByIDs(ids, (int)ResourceEntity.ResourceStatus.IsProcessing, "");
                    Response.Write("1");
                    Response.End();
                    
                }

                this.hItemId.Value = itemID;


               
            }
        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {
            string _s = this.rbl.SelectedItem.Text.Trim();
            string subject = "";
            ResourceEntity.ResourceStatus nowStatus;
            if (_s.Equals("通过"))
            {
                nowStatus = ResourceEntity.ResourceStatus.IsPass;
                subject = "您提交的以下图片已通过审核";
            }
            else
            {
                nowStatus = ResourceEntity.ResourceStatus.NotPass;
                subject = "您提交的以下图片没有被通过";
            }
            string reason = this.txtReason.Text.Trim().Replace("'", "''");

            string _hItemIds = this.hItemId.Value.Trim().Trim(new char[]{','});
            string[] ids = _hItemIds.Split(",".ToCharArray());

            //string[] ids = new string[] { this.hItemId.Value };
            Resource obj = new Resource();
            obj.ValidateResourceByIDs(ids, (int)nowStatus, reason);


            string templatePath = Server.MapPath("../resourceValidateEmailTemplateTotal.htm");
            string mailBody= new StreamReader(templatePath).ReadToEnd();
            string authorEmail = string.Empty;
            StringBuilder sb = new StringBuilder("");

            foreach (string _itemId in ids)
            {
                ResourceEntity re = obj.GetResourceInfoByItemId(_itemId);

                if (string.IsNullOrEmpty(authorEmail))
                {
                    authorEmail=new MemberShipManager().GetUserEmailByUserID(re.userId.ToString());
                }

                sb.Append("<li>");
                sb.Append(""+re.Caption+" (");
                sb.Append(re.FileName+")");
                sb.Append("</li>");

                //写入日志
                LogEntity model = new LogEntity();
                model.id = Guid.NewGuid();
                model.userId = CurrentUser.UserId;
                model.userName = CurrentUser.UserLoginName;
                model.EventType = ((int)LogType.ValidateResource).ToString();
                model.EventResult = ((int)nowStatus).ToString();
                model.EventContent = _itemId;
                model.IP = Request.UserHostAddress;
                model.AddDate = DateTime.Now;
                new Logs().Add(model);

            }

            //统一发一次邮件

            //发送邮件，由系统邮件发送
            string smtpHost = ConfigurationManager.AppSettings["smtpHost"];
            string smtpUserName = ConfigurationManager.AppSettings["smtpUserName"];
            string smtpPassword = ConfigurationManager.AppSettings["smtpPassword"];

            //string adminUid = ConfigurationManager.AppSettings["superAdminId"];
            //string adminEmail = new MemberShipManager().GetUserEmailByUserID(adminUid);

            
            string fromEmail;
            string mailTo = authorEmail;
            string body = "";

            Tool t = new Tool();
            XmlDocument doc = Tool.GetDocument("/xml/System.xml");
            smtpHost = t.GetValue(doc, "host");
            smtpUserName = t.GetValue(doc, "userName");
            smtpPassword = t.GetValue(doc, "password");

            fromEmail = t.GetValue(doc, "from");

            //body = mailBody.Replace("{caption}", re.Caption);
            //body = body.Replace("{clientfilename}", re.FileName);
            //body = body.Replace("{filelength}", QJVRMS.Common.Tool.toFileSize(re.FileSize));
            //body = body.Replace("{uploaddate}", re.uploadDate.ToString());
            //body = body.Replace("{reason}", reason);
            //body = body.Replace("{host}", Request.Url.Authority);
            //body = body.Replace("{apppath}", Request.ApplicationPath);
            //body = body.Replace("{resourceStatus}", ((int)nowStatus).ToString());

            body = mailBody.Replace("{result}", subject);
            body = body.Replace("{filelist}", sb.ToString());
            body = body.Replace("{reason}", reason);
            body = body.Replace("{host}", Request.Url.Authority);
            body = body.Replace("{apppath}", Request.ApplicationPath);
            body = body.Replace("{resourceStatus}", ((int)nowStatus).ToString());


            Session["Validate"] = body;
            //Tool.sendMail(smtpHost, smtpUserName, smtpPassword, fromEmail, mailTo, subject, body);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script language='javascript'>doClose('" + mailTo + "', '" + subject + "')</script>");
            
        }
    }
}
