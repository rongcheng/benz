using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace QJVRMS.Business
{
    /// <summary>
    /// 日志实体类
    /// </summary>
    public class LogEntity
    {
        public LogEntity()
        { }
        #region Model
        private Guid _id;
        private Guid _userid;
        private string _username;
        private string _eventtype;
        private string _eventresult;
        private string _eventcontent;
        private string _ip;
        private DateTime _adddate;
        /// <summary>
        /// 
        /// </summary>
        public Guid id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid userId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string userName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EventType
        {
            set { _eventtype = value; }
            get { return _eventtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EventResult
        {
            set { _eventresult = value; }
            get { return _eventresult; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EventContent
        {
            set { _eventcontent = value; }
            get { return _eventcontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IP
        {
            set { _ip = value; }
            get { return _ip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime AddDate
        {
            set { _adddate = value; }
            get { return _adddate; }
        }
        #endregion Model
    }


    /// <summary>
    /// 日志操作类
    /// </summary>
    public class Logs
    {
        
        
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(LogEntity model)
        {
            LogWS.LogService ls = new QJVRMS.Business.LogWS.LogService();
            return ls.Add(model.id, model.userId, model.userName, model.EventType, model.EventResult, model.EventContent, model.IP, model.AddDate);
        }


        /// <summary>
        /// 获得日志列表
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public DataSet GetLogs(string userName,string LogType, DateTime startDate, DateTime endDate, int pageSize, int pageIndex)
        {
            LogWS.LogService ls = new QJVRMS.Business.LogWS.LogService();
            return ls.GetLogs(userName, LogType,startDate, endDate, pageSize, pageIndex);             
        }


        /// <summary>
        /// 构造一个日志类型的DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetLogType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("CnName", typeof(string));
            dt.Columns.Add("EnName", typeof(string));

            DataRow dr;

            dr=dt.NewRow();
            dr["ID"] = (int)LogType.Login;
            dr["EnName"] = LogType.Login.ToString();
            dr["CnName"] = "登陆";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = (int)LogType.Logout;
            dr["EnName"] = LogType.Logout.ToString();
            dr["CnName"] = "退出";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = (int)LogType.ValidateResource;
            dr["EnName"] = LogType.ValidateResource.ToString();
            dr["CnName"] = "审核图片";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = (int)LogType.ValidateOrder;
            dr["EnName"] = LogType.ValidateOrder.ToString();
            dr["CnName"] = "处理订单";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = (int)LogType.EditResource;
            dr["EnName"] = LogType.EditResource.ToString();
            dr["CnName"] = "编辑图片";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = (int)LogType.DeleteResource;
            dr["EnName"] = LogType.DeleteResource.ToString();
            dr["CnName"] = "删除图片";
            dt.Rows.Add(dr);

            return dt;
        }

        public string GetLogTypeCnNameByID(string id)
        {
            DataTable dt = GetLogType();
            DataRow[] drs=dt.Select("ID=" + id);
            if (drs.Length > 0)
            {
                return drs[0]["CnName"].ToString();
            }
            return "";
        }
    }



    /// <summary>
    /// 操作类型
    /// </summary>
    public enum LogType
    { 
        Login=0,//登陆
        Logout=1,//退出
        ValidateResource=2,    //审核资源
        ValidateOrder=3,       //处理订单
        EditResource=4,        //编辑资源
        DeleteResource=5       //删除资源
    }
}
