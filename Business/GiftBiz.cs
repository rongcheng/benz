using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace QJVRMS.Business
{
    public class GiftBiz
    {
        /// <summary>
        /// 获取类型列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetGiftTypeList()
        {
            GiftService.GiftService service = new QJVRMS.Business.GiftService.GiftService();
            return service.GetGiftTypeList();
        }

        /// <summary>
        /// 获取新ID
        /// </summary>
        /// <returns></returns>
        public string GetNewId()
        {
            GiftService.GiftService service = new QJVRMS.Business.GiftService.GiftService();
            return service.GetNewId();
        }

        /// <summary>
        /// 添加礼品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddGift(GiftInfo model)
        {
            GiftService.GiftService service = new QJVRMS.Business.GiftService.GiftService();
            return service.AddGift(model.Id, model.Title, model.TypeId, model.Quantity, model.ImageId, model.Status, model.Remark);
        }

        /// <summary>
        /// 更新礼品信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateGift(GiftInfo model)
        {
            GiftService.GiftService service = new QJVRMS.Business.GiftService.GiftService();
            return service.UpdateGift(model.Id, model.Title, model.TypeId, model.Quantity, model.ImageId, model.Status, model.Remark);
        }

        /// <summary>
        /// 删除礼品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteGift(string id)
        {
            GiftService.GiftService service = new QJVRMS.Business.GiftService.GiftService();
            return service.DeleteGift(id);
        }


        /// <summary>
        /// 获取礼品列表
        /// </summary>
        /// <param name="title"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public DataTable GetGiftList(string title, string typeId, string imageId)
        {
            GiftService.GiftService service = new QJVRMS.Business.GiftService.GiftService();
            return service.GetGiftList(title, typeId, imageId);
        }

        /// <summary>
        /// 获取礼品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GiftInfo GetModel(string id)
        {
            GiftService.GiftService service = new QJVRMS.Business.GiftService.GiftService();
            DataTable dt = service.GetGiftModel(id);
            GiftInfo model = new GiftInfo();
            if (dt.Rows.Count > 0)
            {
                model.Id = id;
                model.Title = dt.Rows[0]["Title"].ToString();
                model.TypeId = dt.Rows[0]["TypeId"].ToString();
                if (dt.Rows[0]["Quantity"].ToString() != "")
                {
                    model.Quantity = int.Parse(dt.Rows[0]["Quantity"].ToString());
                }
                model.ImageId = dt.Rows[0]["ImageId"].ToString();
                if (dt.Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(dt.Rows[0]["Status"].ToString());
                }
                if (dt.Rows[0]["CreateTime"].ToString() != "")
                {

                    model.CreateTime = DateTime.Parse(dt.Rows[0]["CreateTime"].ToString());
                }
                model.Remark = dt.Rows[0]["Remark"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }
    }

    [Serializable]
    public class GiftInfo
    {
        #region Model
        private string _id;
        private string _title;
        private string _typeid;
        private int _quantity;
        private string _imageid;
        private int _status;
        private DateTime _createtime;
        private string _remark;
        /// <summary>
        /// 
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TypeId
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Quantity
        {
            set { _quantity = value; }
            get { return _quantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ImageId
        {
            set { _imageid = value; }
            get { return _imageid; }
        }
        /// <summary>
        /// 状态：1-正常，9-删除
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
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
