using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Xml;

namespace QJVRMS.Common
{
    public class Tool
    {

        public static string toFileSize(long Size)
        {
            string m_strSize = "";
            long FactSize = 0;
            FactSize = Size;
            if (FactSize < 1024.00)
                m_strSize = FactSize.ToString("F2") + " Byte";
            else if (FactSize >= 1024.00 && FactSize < 1048576)
                m_strSize = (FactSize / 1024.00).ToString("F2") + " K";
            else if (FactSize >= 1048576 && FactSize < 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00).ToString("F2") + " M";
            else if (FactSize >= 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00 / 1024.00).ToString("F2") + " G";
            return m_strSize;
        }

        public static void sendMail(string smtpHost, string smtpUser, string smtpPassword, string mailFrom, string mailTo, string mailSubject, string mailBody)
        {
            mailSubject = mailSubject + "-来自全景资源管理平台";
            MailAddress from = new MailAddress(mailFrom);
            MailAddress to = new MailAddress(mailTo);

            MailMessage message = new MailMessage(from,to);
      
            message.ReplyTo = from;
            message.Subject = mailSubject;
            message.Body = mailBody;
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;

            SmtpClient client = new SmtpClient(smtpHost);
            client.Timeout = 5000;//5秒-超时时间
            client.Credentials = new NetworkCredential(smtpUser, smtpPassword);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                client.Send(message);
                //client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                //client.SendAsync(message, "success");
            }
            catch (Exception ex)
            {
                LogWriter.WriteExceptionLog(ex);
            }
        }

        //private static void SendCompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e) {
        //    if (e.Error != null) {
        //    }
        //}


        public static XmlDocument GetDocument(string path)
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                doc.Load(System.Web.HttpContext.Current.Server.MapPath(path));
                return doc;
            }
            catch
            {
                return null;
            }
        }

        public string GetValue(XmlDocument doc, string name)
        {
            return doc.SelectSingleNode("//Item").Attributes[name].Value;
        }
    }
}
