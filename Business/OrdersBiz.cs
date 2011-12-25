using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace QJVRMS.Business
{
    public class OrdersBiz
    {
        /// <summary>
        /// ��Ӷ���
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddOrders(OrderInfo model)
        {
            OrdersService.OrdersService service = new OrdersService.OrdersService();
            return service.AddOrders(model.OrderId, model.UserId, model.Operator, model.State, model.Remark,model.Address,model.Contactor,model.Tel,model.Email);
        }

        /// <summary>
        /// ���¶�����Ϣ
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateOrders(OrderInfo model)
        {
            OrdersService.OrdersService service = new OrdersService.OrdersService();
            return service.UpdateOrders(model.OrderId, model.UserId, model.Operator, model.State, model.Remark,model.Address,model.Contactor,model.Tel,model.Email);
        }

        /// <summary>
        /// ɾ��������Ϣ
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int DeleteOrders(string orderId)
        {
            OrdersService.OrdersService service = new OrdersService.OrdersService();
            int result = service.DeleteOrders(orderId);
            if (result > 0)
            {
                GiftBiz biz = new GiftBiz();
                DataTable dt = GetOrders_DetailList(-1, orderId, string.Empty, string.Empty);
                foreach (DataRow row in dt.Rows)
                {
                    GiftInfo model = biz.GetModel(row["giftid"].ToString());
                    int count = int.Parse(row["giftcount"].ToString());
                    model.Quantity += count;
                    result *= biz.UpdateGift(model);
                }
            }
            return result;
        }

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderInfo GetOrderInfo(string orderId)
        {
            OrdersService.OrdersService service = new OrdersService.OrdersService();
            DataTable dt = service.GetOrderModel(orderId);

            OrderInfo model = new OrderInfo();
            model.OrderId = orderId;
            if (dt.Rows.Count > 0)
            {
                model.UserId = dt.Rows[0]["UserId"].ToString();
                if (dt.Rows[0]["CreateDate"].ToString() != "")
                {

                    model.CreateDate = DateTime.Parse(dt.Rows[0]["CreateDate"].ToString());
                }
                model.Operator = dt.Rows[0]["Operator"].ToString();
                if (dt.Rows[0]["State"].ToString() != "")
                {
                    model.State = int.Parse(dt.Rows[0]["State"].ToString());
                }
                model.Remark = dt.Rows[0]["Remark"].ToString();
                model.Address = dt.Rows[0]["Address"].ToString();
                model.Contactor = dt.Rows[0]["Contactor"].ToString();
                model.Tel = dt.Rows[0]["Tel"].ToString();
                model.Email = dt.Rows[0]["Email"].ToString();
                return model;
            }
            return null;
        }

        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="operatorId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public DataTable GetOrdersList(string orderId, string userId, string startDate, string endDate, string operatorId, int state)
        {
            OrdersService.OrdersService service = new OrdersService.OrdersService();
            return service.GetOrdersList(orderId, userId, startDate, endDate, operatorId, state);
        }

        /// <summary>
        /// ��ȡ�µĶ���id
        /// </summary>
        /// <returns></returns>
        public string GetNewOrderId()
        {
            OrdersService.OrdersService service = new OrdersService.OrdersService();
            return service.GetNewOrderId();
        }

        /// <summary>
        /// ����µĶ�����ϸ
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddOrders_Detail(Orders_DetailInfo model)
        {
            OrdersService.OrdersService service = new OrdersService.OrdersService();
            return service.AddOrders_Detail(model.OrderId, model.GiftId, model.GiftCount, model.Usage, model.Remark);
        }

        /// <summary>
        /// ���¶�����ϸ��Ϣ
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateOrders_Detail(Orders_DetailInfo model)
        {
            OrdersService.OrdersService service = new OrdersService.OrdersService();
            return service.UpdateOrders_Detail(model.Id, model.OrderId, model.GiftId, model.GiftCount, model.Usage, model.Remark);
        }

        /// <summary>
        /// ɾ��������ϸ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteOrders_Detail(int id)
        {
            OrdersService.OrdersService service = new OrdersService.OrdersService();
            return service.DeleteOrders_Detail(id);
        }

        /// <summary>
        /// ��ȡ������ϸ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Orders_DetailInfo GetOrders_DetailModel(int id)
        {
            OrdersService.OrdersService service = new OrdersService.OrdersService();
            DataTable dt = service.GetOrders_DetailModel(id);

            Orders_DetailInfo model = new Orders_DetailInfo();
            model.Id = id;
            if (dt.Rows.Count > 0)
            {
                model.OrderId = dt.Rows[0]["OrderId"].ToString();
                model.GiftId = dt.Rows[0]["GiftId"].ToString();
                if (dt.Rows[0]["GiftCount"].ToString() != "")
                {
                    model.GiftCount = int.Parse(dt.Rows[0]["GiftCount"].ToString());
                }
                model.Usage = dt.Rows[0]["Usage"].ToString();
                model.Remark = dt.Rows[0]["Remark"].ToString();
                return model;
            }
            return null;
        }

        /// <summary>
        /// ��ȡ������ϸ�б�
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderId"></param>
        /// <param name="giftId"></param>
        /// <param name="usage"></param>
        /// <returns></returns>
        public DataTable GetOrders_DetailList(int id, string orderId, string giftId, string usage)
        {
            OrdersService.OrdersService service = new OrdersService.OrdersService();
            return service.GetOrders_DetailList(id, orderId, giftId, usage);
        }

        /// <summary>
        /// ��ȡ״̬�ַ���
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string GetStateString(int state)
        {
            switch (state)
            {
                case 0: return "δ�ύ";
                case 1: return "���ύ";
                case 2: return "�����";
                case 9: return "��ɾ��";
            }
            return string.Empty;
        }
    }

    [Serializable]
    public class OrderInfo
    {
        #region Model
        private string _orderid;
        private string _userid;
        private DateTime _createdate;
        private string _operator;
        private int _state;
        private string _remark;
        private string _address;
        private string _contactor;
        private string _tel;
        private string _email;
        /// <summary>
        /// 
        /// </summary>
        public string OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// ������id
        /// </summary>
        public string Operator
        {
            set { _operator = value; }
            get { return _operator; }
        }
        /// <summary>
        /// ����״̬��0-δ�ύ��1-���ύ��2-��ɣ�9-ɾ��
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// ���͵�ַ
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// �ջ���
        /// </summary>
        public string Contactor
        {
            set { _contactor = value; }
            get { return _contactor; }
        }
        /// <summary>
        /// �ջ��绰
        /// </summary>
        public string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// �ջ�������
        /// </summary>
        public string Email
        {
            set { _email  = value; }
            get { return _email; }
        }
        #endregion Model
    }

    [Serializable]
    public class Orders_DetailInfo
    {
        #region Model
        private int _id;
        private string _orderid;
        private string _giftid;
        private int _giftcount;
        private string _usage;
        private string _remark;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GiftId
        {
            set { _giftid = value; }
            get { return _giftid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int GiftCount
        {
            set { _giftcount = value; }
            get { return _giftcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Usage
        {
            set { _usage = value; }
            get { return _usage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model
    }
}
