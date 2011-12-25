using System;
using System.Collections.Generic;
using System.Text;
using QJVRMS.Business.OrderWS;
using System.Data;

using System.Configuration;
using QJVRMS.Common;
using System.Xml;

namespace QJVRMS.Business {

    /// <summary>
    /// 订单处理类
    /// </summary>
    public class Orders {

        /// <summary>
        /// 添加一个订单
        /// </summary>
        /// <param name="title"></param>
        /// <param name="requestDate"></param>
        /// <param name="requestSize"></param>
        /// <param name="usage"></param>
        /// <param name="contents"></param>
        /// <param name="status"></param>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        public void Add(string title, DateTime requestDate, int requestSize, string usage, string contents, int status, string userId, string userName) {
            OrderWS.OrderService os = new QJVRMS.Business.OrderWS.OrderService();
            os.Add(title, requestDate, requestSize, usage, contents, status, userId, userName);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetOrdersByUserId(string userId, int PageSize, int PageNum, DateTime startDate, DateTime endDate, int status) {

            OrderService os = new OrderWS.OrderService();
            return os.GetOrdersByUserId(userId, PageSize, PageNum, startDate, endDate, status);
        }


        /// <summary>
        /// 返回订单实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetOrdersById(string id) {
            OrderService os = new OrderWS.OrderService();
            return os.GetOrdersById(id);


        }

        public void sendNewOrder(string body) {
            string mailFrom = ConfigurationManager.AppSettings["mailFrom"];
            string mailSubject = ConfigurationManager.AppSettings["mailSubject"];
            if (string.IsNullOrEmpty(mailSubject))
            {
                mailSubject = "有新的订单";
            }

            Tool t = new Tool();
            mailFrom = t.GetValue(Tool.GetDocument("/xml/System.xml"), "from");

            sendNewOrder(mailFrom, mailSubject, body);
        }

        public void sendNewOrder(string subject, string body) {
            string mailFrom = ConfigurationManager.AppSettings["mailFrom"];
            Tool t = new Tool();
            mailFrom = t.GetValue(Tool.GetDocument("/xml/System.xml"), "from");
            sendNewOrder(mailFrom, subject, body);
        } 

        public void sendNewOrder(string fromEmail, string subject, string body) {
            string smtpHost = ConfigurationManager.AppSettings["smtpHost"];
            string smtpUserName = ConfigurationManager.AppSettings["smtpUserName"];
            string smtpPassword = ConfigurationManager.AppSettings["smtpPassword"];
            string mailTo = ConfigurationManager.AppSettings["mailTo"];

            Tool t = new Tool();
            XmlDocument doc = Tool.GetDocument("/xml/System.xml");
           
            smtpHost = t.GetValue(doc, "host");
            smtpUserName = t.GetValue(doc, "userName");
            smtpPassword = t.GetValue(doc, "password");
            mailTo = t.GetValue(doc, "to");

            Tool.sendMail(smtpHost, smtpUserName, smtpPassword, fromEmail, mailTo, subject, body);

        }

        public void sendNewOrderToUser(string toEmail, string subject, string body)
        {
            string smtpHost = ConfigurationManager.AppSettings["smtpHost"];
            string smtpUserName = ConfigurationManager.AppSettings["smtpUserName"];
            string smtpPassword = ConfigurationManager.AppSettings["smtpPassword"];
            string mailFrom = ConfigurationManager.AppSettings["mailFrom"];
            string mailTo = toEmail;

            Tool t = new Tool();
            XmlDocument doc = Tool.GetDocument("/xml/System.xml");
            mailFrom = t.GetValue(doc, "from");
            smtpHost = t.GetValue(doc, "host");
            smtpUserName = t.GetValue(doc, "userName");
            smtpPassword = t.GetValue(doc, "password");
         

            Tool.sendMail(smtpHost, smtpUserName, smtpPassword, mailFrom, mailTo, subject, body);
        }


        /// <summary>
        /// 将资源添加到订单中
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="resourceIds"></param>
        public void AddResourceToOrders(string orderid, string[] resourceIds) {
            OrderService os = new OrderWS.OrderService();
            os.AddResourceToOrders(orderid, resourceIds);
        }
        /// <summary>
        /// 更新某个订单的状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateStatus(string id, int status) {
            OrderService os = new OrderWS.OrderService();
            return os.UpdateStatus(id, status);
        }

        /// <summary>
        ///返回某个订单中的资源数量 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetOrderResourceCount(string id) {
            OrderService os = new OrderWS.OrderService();
            return os.GetOrderResourceCount(id);

        }

        /// <summary>
        /// 删除订单中的资源
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        public int DelResourceFromOrders(string orderId, string resourceId) {
            OrderService os = new OrderWS.OrderService();
            return os.DelResourceFromOrders(orderId, resourceId);
        }

        /// <summary>
        /// 订单没有被受理时，添加未被受理的原因
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public int AddOrderNotPassReason(string orderId, string reason) {
            OrderService os = new OrderWS.OrderService();
            return os.AddOrderNotPassReason(orderId, reason);
        }

        /// <summary>
        /// 返回订单没有被受理的原因
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public string GetOrderNotPassReason(string orderId) {
            OrderService os = new OrderWS.OrderService();
            return os.GetOrderNotPassReason(orderId);
        }

        /// <summary>
        /// 返回特定时期内的订单统计
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetOrderStatus(DateTime startDate, DateTime endDate) {
            OrderService os = new OrderWS.OrderService();
            return os.GetOrderStatus(startDate, endDate);
        }


        /// <summary>
        /// 普通用户，是否有提醒信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsOrderAlert(Guid userId) {
            OrderService os = new OrderWS.OrderService();
            return os.IsOrderAlert(userId);

        }

        /// <summary>
        /// 管理员，是否有订单提醒
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsOrderAlertAdmin(Guid userId)
        {
            OrderService os = new OrderWS.OrderService();
            return os.IsOrderAlertAdmin(userId);
        }

        /// <summary>
        /// 订单被看过以后，状态设置为已读，下次不会再次提醒
        /// </summary>
        /// <param name="userId"></param>
        public void SetOrderReadStatus(Guid userId) {
            OrderService os = new OrderWS.OrderService();
            os.SetOrderReadStatus(userId);
        }


        public string ShowDefaultOrders(string userName) {
            OrderService os = new OrderWS.OrderService();
            DataTable dt = os.ShowOrders(userName);

            if (dt == null || dt.Rows.Count == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            string ordername = string.Empty;
            int status = 0;
            string type = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++) {
                sb.Append("<li>");
                sb.Append("<div class=\"title2\">");
                ordername = dt.Rows[i]["title"].ToString();
                string title = ordername;
                ordername = ordername.Length > 10 ? ordername.Substring(0, 10) + "..." : ordername;
                sb.Append("<a title=\"" + title + "\" href=\"javascript:orderDetail('" + dt.Rows[i]["id"].ToString() + "');\">" + ordername + "</a>");
                sb.Append("</div>");
                status = int.Parse(dt.Rows[i]["status"].ToString());
                switch (status) {
                    case 0:
                        type = "新订单";
                        break;
                    case 1:
                        type = "处理中";
                        break;
                    case 2:
                        type = "未通过";
                        break;
                    case 3:
                        type = "已完成";
                        break;
                    case 4:
                        type = "已确认";
                        break;
                }
                sb.Append("<div class=\"date2\">" + type + "</div>");
                sb.Append("</li>");
            }

            return sb.ToString();
        }


        /// <summary>
        /// 订单沟通
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderId"></param>
        /// <param name="contents"></param>
        /// <param name="adddate"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool AddOrderMessage(Guid id, Guid orderId, string contents, DateTime adddate, string userName,int isUserRead,int isAdminRead)
        {
            OrderService os = new OrderWS.OrderService();
            return os.AddOrderMessage(id, orderId, contents, adddate, userName,isUserRead,isAdminRead);
        }


        /// <summary>
        /// 返回某个订单的沟通记录
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataTable GetOrderMessageByOrderId(Guid orderId)
        {
            OrderService os = new OrderWS.OrderService();
            return os.GetOrderMessageByOrderId(orderId);
        }


        public bool UpdateOrderMessageStatusUser(Guid orderId, int isRead)
        {
            OrderService os = new OrderWS.OrderService();
            return os.UpdateOrderMessageStatusUser(orderId, isRead);
        }

        public bool UpdateOrderMessageStatusAdmin(Guid orderId, int isRead)
        {
            OrderService os = new OrderWS.OrderService();
            return os.UpdateOrderMessageStatusAdmin(orderId, isRead);
        }


        public DataTable IsOrderMessageAlertAdmin()
        {
            OrderService os = new OrderWS.OrderService();
            return os.IsOrderMessageAlertAdmin();
        }

        public DataTable IsOrderMessageAlertUser(Guid userId)
        {
            OrderService os = new OrderWS.OrderService();
            return os.IsOrderMessageAlertUser(userId);
        }



        /// <summary>
        /// 订单状态表，id name 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetOrderStatus() {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("CnName", typeof(string));

            DataRow dr;

            dr = dt.NewRow();
            dr["ID"] = (int)OrderStatus.New;
            dr["CnName"] = "新订单";

            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = (int)OrderStatus.IsProcessing;
            dr["CnName"] = "处理中";

            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = (int)OrderStatus.NotPass;
            dr["CnName"] = "未通过";

            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = (int)OrderStatus.Completed;
            dr["CnName"] = "已完成";

            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = (int)OrderStatus.Confirmed;
            dr["CnName"] = "已确认";

            dt.Rows.Add(dr);

            return dt;
        }
    }


    /// <summary>
    /// 订单状态
    /// 0 新订单
    /// 1 被采纳，正在安排拍摄
    /// 2 没有被采纳，通常是不合理的申请 
    /// 3 订单完成，图片已经上传上来
    /// 4 确认，用户看到图片后，确认该订单
    /// </summary>
    public enum OrderStatus {
        New = 0,
        IsProcessing = 1,
        NotPass = 2,
        Completed = 3,
        Confirmed = 4

    }
}
