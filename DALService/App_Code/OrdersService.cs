using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;

using QJVRMS.DataAccess;


/// <summary>
/// OrdersService 的摘要说明
/// </summary>
[WebService(Namespace = "http://qjDataAccess.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class OrdersService : System.Web.Services.WebService
{

    public OrdersService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public int AddOrders(string orderId, string userId, string operatorId, int state, string remark,string address,string Contactor,string Tel,string Email)
    {
        SqlParameter[] parameters = new SqlParameter[] { 
            new SqlParameter("@orderId",orderId),
            new SqlParameter("@userId",new Guid(userId)),
            new SqlParameter("@operator",new Guid(operatorId)),
            new SqlParameter("@state",state),
            new SqlParameter("@remark",remark),
            new SqlParameter("@address",address),
            new SqlParameter("@Contactor",Contactor),
            new SqlParameter("@Tel",Tel),
            new SqlParameter("@Email",Email)
        };

        return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "UP_Orders_ADD", parameters);
    }

    [WebMethod]
    public int UpdateOrders(string orderId, string userId, string operatorId, int state, string remark, string address, string Contactor, string Tel, string Email)
    {
        SqlParameter[] parameters = new SqlParameter[] { 
            new SqlParameter("@orderId",orderId),
            new SqlParameter("@userId",new Guid(userId)),
            new SqlParameter("@operator",new Guid(operatorId)),
            new SqlParameter("@state",state),
            new SqlParameter("@remark",remark),
            new SqlParameter("@address",address),
            new SqlParameter("@Contactor",Contactor),
            new SqlParameter("@Tel",Tel),
            new SqlParameter("@Email",Email)
        };

        return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "UP_Orders_Update", parameters);
    }

    [WebMethod]
    public int DeleteOrders(string orderId)
    {
        SqlParameter[] parameters = new SqlParameter[] { 
            new SqlParameter("@orderId",orderId)
        };

        return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "UP_Orders_Delete", parameters);
    }

    [WebMethod]
    public DataTable GetOrderModel(string orderId)
    {
        SqlParameter[] parameters = new SqlParameter[] { 
            new SqlParameter("@orderId",orderId)
        };

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "UP_Orders_GetModel", parameters).Tables[0];
    }


    [WebMethod]
    public DataTable GetOrdersList(string orderId, string userId, string startDate, string endDate, string operatorId, int state)
    {
        SqlParameter[] parameters = new SqlParameter[] { 
            new SqlParameter("@orderId",orderId),
            new SqlParameter("@userId",userId),
            new SqlParameter("@startDate",startDate),
            new SqlParameter("@endDate",endDate),
            new SqlParameter("@operator",operatorId),
            new SqlParameter("@state",state)
        };

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "UP_Orders_GetList", parameters).Tables[0];
    }

    [WebMethod]
    public string GetNewOrderId()
    {
        string flag = "PO";
        string dateFlag = DateTime.Now.ToString("yyyyMMdd");

        SqlParameter[] parameters = new SqlParameter[] { 
            new SqlParameter("@ParamID",flag),
            new SqlParameter("@Date",dateFlag)
        };
        DataTable dt = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_GetSysSeq", parameters).Tables[0];
        if (dt.Rows.Count > 0)
        {
            return flag + dateFlag + dt.Rows[0][0].ToString().PadLeft(4, '0');
        }
        return null;
    }

    [WebMethod]
    public int AddOrders_Detail(string orderId, string giftId, int giftCount, string usage, string remark)
    {
        SqlParameter[] parameters = new SqlParameter[] { 
            new SqlParameter("@orderId",orderId),
            new SqlParameter("@giftId",giftId),
            new SqlParameter("@giftCount",giftCount),
            new SqlParameter("@usage",usage),
            new SqlParameter("@remark",remark)
        };

        return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "UP_Orders_Detail_ADD", parameters);
    }

    [WebMethod]
    public int UpdateOrders_Detail(int id, string orderId, string giftId, int giftCount, string usage, string remark)
    {
        SqlParameter[] parameters = new SqlParameter[] { 
            new SqlParameter("@id",id),
            new SqlParameter("@orderId",orderId),
            new SqlParameter("@giftId",giftId),
            new SqlParameter("@giftCount",giftCount),
            new SqlParameter("@usage",usage),
            new SqlParameter("@remark",remark)
        };

        return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "UP_Orders_Detail_Update", parameters);
    }

    [WebMethod]
    public int DeleteOrders_Detail(int id)
    {
        SqlParameter[] parameters = new SqlParameter[] { 
            new SqlParameter("@id",id)
        };

        return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "UP_Orders_Detail_Delete", parameters);
    }

    [WebMethod]
    public DataTable GetOrders_DetailModel(int id)
    {
        SqlParameter[] parameters = new SqlParameter[] { 
            new SqlParameter("@id",id)
        };

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "UP_Orders_Detail_GetModel", parameters).Tables[0];
    }

    [WebMethod]
    public DataTable GetOrders_DetailList(int id, string orderId, string giftId, string usage)
    {
        SqlParameter[] parameters = new SqlParameter[] { 
            new SqlParameter("@id",id),
            new SqlParameter("@orderId",orderId),
            new SqlParameter("@giftId",giftId),
            new SqlParameter("@usage",usage)
        };

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "UP_Orders_Detail_GetList", parameters).Tables[0];
    }
}

