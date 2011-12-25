using System;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

using QJVRMS.Common;
using QJVRMS.Business;
using System.Data;
using QJVRMS.DataAccess;
using System.Data.SqlClient;
using System.Text;

/// <summary>
///OrderService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class OrderService : System.Web.Services.WebService
{

    public OrderService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }



    /// <summary>
    /// 增加一条数据
    /// </summary>
    [WebMethod]
    public void Add(string title,DateTime requestDate,int requestSize,string usage,string contents,int status,string userId,string userName)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("insert into Orders(");
        strSql.Append("id,title,RequestDate,RequestSize,AddDate,usage,contents,status,userId,userName)");
        strSql.Append(" values (");
        strSql.Append("@id,@title,@RequestDate,@RequestSize,@AddDate,@usage,@contents,@status,@userId,@userName)");
        SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@title", SqlDbType.NVarChar,200),
					new SqlParameter("@RequestDate", SqlDbType.DateTime),
					new SqlParameter("@RequestSize", SqlDbType.Int,4),
					new SqlParameter("@AddDate", SqlDbType.DateTime),
					new SqlParameter("@usage", SqlDbType.NVarChar,200),
					new SqlParameter("@contents", SqlDbType.NVarChar),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
                    new SqlParameter("@userid", SqlDbType.NVarChar,50),
                    new SqlParameter("@userName", SqlDbType.NVarChar,50)                                    
                                    };
        parameters[0].Value = Guid.NewGuid();
        parameters[1].Value = title;
        parameters[2].Value = requestDate;
        parameters[3].Value = requestSize;
        parameters[4].Value = DateTime.Now;
        parameters[5].Value = usage;
        parameters[6].Value = contents;
        parameters[7].Value = status;
        parameters[8].Value = userId;
        parameters[9].Value = userName;

        SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, strSql.ToString(), parameters);
    }


    /// <summary>
    /// 获得数据列表
    /// </summary>
    [WebMethod]
    public DataSet GetOrdersByUserId(string userId,int PageSize, int PageNum,DateTime startDate,DateTime endDate,int status)
    {
        SqlParameter[] paramater = new SqlParameter[]
        {
            new SqlParameter("@userId",SqlDbType.NVarChar,50),
            new SqlParameter("@startDate",SqlDbType.DateTime),
            new SqlParameter("@endDate",SqlDbType.DateTime),
            new SqlParameter("@pageSize",SqlDbType.Int),
            new SqlParameter("@pageNum",SqlDbType.Int),
            new SqlParameter("@status",SqlDbType.Int)
        };
        paramater[0].Value = userId;
        paramater[1].Value = startDate;
        paramater[2].Value = endDate;

        paramater[3].Value = PageSize;
        paramater[4].Value = PageNum;
        paramater[5].Value = status;

        using (DataSet ds = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_GetOrders", paramater))
        {
            return ds;
        }
    }


    [WebMethod]
    public DataSet GetOrdersById(string id)
    {
        string sql = "select * from orders where id=@id";
        SqlParameter[] paramater = new SqlParameter[]
        {
            new SqlParameter("@id",SqlDbType.NVarChar,50)
            
        };
        paramater[0].Value = id;

        using (DataSet ds = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, paramater))
        {
            return ds;
        }
    }

    [WebMethod]
    public int GetOrderResourceCount(string id)
    {
        string sql = "SELECT COUNT(0) FROM dbo.OrderDetail WHERE OrderId=@id";
        SqlParameter[] paramater = new SqlParameter[]
        {
            new SqlParameter("@id",SqlDbType.NVarChar,50)
            
        };
        paramater[0].Value = id;

        return (int)SqlHelper.ExecuteScalar(CommonInfo.ConQJVRMS, CommandType.Text, sql, paramater);

        
    }


    [WebMethod]
    public bool UpdateStatus(string id, int status)
    {
        string sql = "Update orders set status=@status where id=@id;Update orders set IsRead=0 where id=@id AND status<>4";
        SqlParameter[] paramater = new SqlParameter[]
        {
            new SqlParameter("@id",SqlDbType.NVarChar,50),
            new SqlParameter("@status",SqlDbType.Int,8)
            
        };
        paramater[0].Value = id;
        paramater[1].Value = status;

        int ret = SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, paramater);
        return ret > 0;
    }


    [WebMethod]
    public void AddResourceToOrders(string orderid, string[] resourceIds)
    {
        if (resourceIds.Length < 0) return;
        string sql = "insert into OrderDetail(ODId,OrderId,ImageId) values(newid(),@orderid,@resourceid)";
        
        foreach (string resourceId in resourceIds)
        {
            if (string.IsNullOrEmpty(resourceId)) continue;

            SqlParameter[] paramater = new SqlParameter[]
            {
                new SqlParameter("@orderid",SqlDbType.UniqueIdentifier),
                new SqlParameter("@resourceid",SqlDbType.UniqueIdentifier)
                
            };
            paramater[0].Value = new Guid(orderid);
            paramater[1].Value =new Guid( resourceId);

            SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, paramater);
        }

    }

    [WebMethod]
    public int DelResourceFromOrders(string orderId, string resourceId)
    {
        string sql = "delete from OrderDetail where orderId=@oid and imageId=@rid";
        SqlParameter[] paramater = new SqlParameter[]
            {
                new SqlParameter("@oid",SqlDbType.UniqueIdentifier),
                new SqlParameter("@rid",SqlDbType.UniqueIdentifier)
                
            };
        paramater[0].Value = new Guid(orderId);
        paramater[1].Value = new Guid(resourceId);


        return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, paramater);
    }


    [WebMethod]

    public int AddOrderNotPassReason(string orderId,string reason)
    {
        string strSql = "Insert Into resource_NotPassReason(Id,resourceId,reason,resourceType) Values(newId(),@ID,@reason,'order')";
        SqlParameter[] Parameters = new SqlParameter[2];
        Parameters[0] = new SqlParameter("@ID", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = new Guid(orderId);

        Parameters[1] = new SqlParameter("@reason", SqlDbType.NVarChar, 500);
        Parameters[1].Value = reason;

        return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, strSql, Parameters);
    
    }

    [WebMethod]

    public string GetOrderNotPassReason(string orderId)
    {
        string strSql = "select * from  resource_NotPassReason where resourceId=@orderId and resourceType='order'";
        SqlParameter[] Parameters = new SqlParameter[1];
        Parameters[0] = new SqlParameter("@orderId", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = new Guid(orderId);

        DataSet ds= SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, strSql, Parameters);
        if (ds.Tables[0].Rows.Count > 0)
        {
            return ds.Tables[0].Rows[0]["reason"].ToString();
        }

        return "";
    }



    [WebMethod]
    public DataSet GetOrderStatus(DateTime startDate,DateTime endDate)
    {
        
        SqlParameter[] paramater = new SqlParameter[]
        {
            new SqlParameter("@startDate",SqlDbType.DateTime),
            new SqlParameter("@endDate",SqlDbType.DateTime)            
        };
        paramater[0].Value = startDate;
        paramater[1].Value = endDate;

        
        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_OrderStatus", paramater);


    }

    [WebMethod]
    public bool IsOrderAlert(Guid userId)
    {
        string sql = "SELECT count(*) FROM  orders  WHERE userId=@userId AND isread=0";
        SqlParameter[] ps = new SqlParameter[] { 
            new SqlParameter("@userId",SqlDbType.UniqueIdentifier)
        };
        ps[0].Value = userId;

        int i=(int)SqlHelper.ExecuteScalar(CommonInfo.ConQJVRMS, CommandType.Text, sql, ps);
        return i > 0;
    }

    [WebMethod]
    public bool IsOrderAlertAdmin(Guid userId)
    {
        SqlParameter[] ps = new SqlParameter[] { 
            new SqlParameter("@userId",SqlDbType.UniqueIdentifier)
        };
        ps[0].Value = userId;

        int i = (int)SqlHelper.ExecuteScalar(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_IsAdminOrder", ps);
        return i > 0;
    }

    [WebMethod]
    public void SetOrderReadStatus(Guid userId)
    {
        string sql = "UPDATE Orders SET isread=1 WHERE userId=@userId";
        SqlParameter[] ps = new SqlParameter[] { 
            new SqlParameter("@userId",SqlDbType.UniqueIdentifier)
        };
        ps[0].Value = userId;
        SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, ps);    
    }

    [WebMethod]
    public DataTable ShowOrders(string userName) {
        string sql = "select top 7 id, title, status from Orders where userName = @userName order by AddDate desc";
        SqlParameter[] ps = new SqlParameter[] { 
            new SqlParameter("@userName",SqlDbType.NVarChar)
        };
        ps[0].Value = userName;
        try {
            DataTable dt = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS,
                 CommandType.Text, sql, ps).Tables[0];
            return dt;
        }
        catch {
            return null;
        }
    }


    [WebMethod]
    public bool AddOrderMessage(Guid id,Guid orderId,string contents,DateTime adddate,string userName,int isUserRead,int isAdminRead)
    {
        string sql = "insert into OrderMessage values(@id,@orderId,@contents,@adddate,@userName,0,@isUserRead,@isAdminRead)";
        SqlParameter[] ps = new SqlParameter[] { 
            new SqlParameter("@id",SqlDbType.UniqueIdentifier),
            new SqlParameter("@orderId",SqlDbType.UniqueIdentifier),
            new SqlParameter("@contents",SqlDbType.NVarChar,500),
            new SqlParameter("@adddate",SqlDbType.DateTime),
            new SqlParameter("@userName",SqlDbType.NVarChar,20),
            new SqlParameter("@isUserRead",SqlDbType.Int),
            new SqlParameter("@isAdminRead",SqlDbType.Int)
        };

        ps[0].Value = id;
        ps[1].Value = orderId;
        ps[2].Value = contents;
        ps[3].Value = adddate;
        ps[4].Value = userName;
        ps[5].Value = isUserRead;
        ps[6].Value = isAdminRead;

        try
        {
            int i=SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, ps);
            return i>0;
        }
        catch
        {
            return false;
        }
    }

    [WebMethod]
    public DataTable GetOrderMessageByOrderId(Guid orderId)
    {
        string sql = "select * From OrderMessage where OrderId = @orderId order by adddate desc";
        SqlParameter[] ps = new SqlParameter[] { 
            new SqlParameter("@orderId",SqlDbType.UniqueIdentifier)
        };
        ps[0].Value = orderId;
        try
        {
            DataTable dt = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS,
                 CommandType.Text, sql, ps).Tables[0];
            return dt;
        }
        catch
        {
            return null;
        }
    
    }


    [WebMethod]
    public bool UpdateOrderMessageStatusUser(Guid orderId, int isRead)
    {
        string sql = "Update OrderMessage set isUserRead=@isUserRead where OrderId = @orderId ";
        SqlParameter[] ps = new SqlParameter[] { 
            new SqlParameter("@orderId",SqlDbType.UniqueIdentifier),
            new SqlParameter("@isUserRead",SqlDbType.Int)
        };
        ps[0].Value = orderId;
        ps[1].Value = isRead;
        try
        {
            int i= SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, ps);
            return i>0;
        }
        catch
        {
            return false;
        }
    }

    [WebMethod]
    public bool UpdateOrderMessageStatusAdmin(Guid orderId, int isRead)
    {
        string sql = "Update OrderMessage set isAdminRead=@isAdminRead where OrderId = @orderId ";
        SqlParameter[] ps = new SqlParameter[] { 
            new SqlParameter("@orderId",SqlDbType.UniqueIdentifier),
            new SqlParameter("@isAdminRead",SqlDbType.Int)
        };
        ps[0].Value = orderId;
        ps[1].Value = isRead;
        try
        {
            int i = SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, ps);
            return i > 0;
        }
        catch
        {
            return false;
        }
    }


    [WebMethod]
    public DataTable IsOrderMessageAlertAdmin()
    {
        string sql = "select distinct b.* from orderMessage a left join Orders b on a.OrderId=b.id where a.isAdminRead=0 and b.status<4";
        try
        {
            DataTable dt = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql).Tables[0];
            return dt;
        }
        catch
        {
            return null;
        }
    }

    [WebMethod]
    public DataTable IsOrderMessageAlertUser(Guid userId)
    {
        string sql = "select  distinct b.* from orderMessage a left join Orders b on a.OrderId=b.id where a.isUserRead=0 and b.userId=@userID and b.status<4";
        SqlParameter[] ps = new SqlParameter[] { 
            new SqlParameter("@userId",SqlDbType.UniqueIdentifier)
        };
        ps[0].Value = userId;

        try
        {
            DataTable dt = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql,ps).Tables[0];
            return dt;
        }
        catch
        {
            return null;
        }
    }


}

