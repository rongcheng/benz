using System;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using QJVRMS.DataAccess;
using System.Data;

/// <summary>
///LogService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class LogService : System.Web.Services.WebService
{

    public LogService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }


    [WebMethod]
    public bool Add(Guid id,Guid userId,string userName,string eventType,string eventResult,string eventContent,string ip,DateTime addDate)
    {
        SqlParameter[] paramater = new SqlParameter[]
        {
            new SqlParameter("@id",SqlDbType.UniqueIdentifier),
            new SqlParameter("@userId",SqlDbType.UniqueIdentifier),
            new SqlParameter("@userName",SqlDbType.NVarChar,50),
            new SqlParameter("@eventType",SqlDbType.NVarChar,10),
            new SqlParameter("@eventResult",SqlDbType.NVarChar,50),
            new SqlParameter("@eventContent",SqlDbType.NVarChar,200),
            new SqlParameter("@ip",SqlDbType.NVarChar,15),
            new SqlParameter("@addDate",SqlDbType.DateTime),
        };
        int i = 0;
        paramater[i++].Value = id;
        paramater[i++].Value = userId;
        paramater[i++].Value = userName;
        paramater[i++].Value = eventType;
        paramater[i++].Value = eventResult;
        paramater[i++].Value = eventContent;
        paramater[i++].Value = ip;
        paramater[i++].Value = addDate;

        int iRet = SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Logs_ADD", paramater);
        return iRet>0;    
    }

    [WebMethod]
    public DataSet GetNotPassResources(string userName, DateTime startDate, DateTime endDate,int pageSize,int pageIndex)
    {
        SqlParameter[] ps = new SqlParameter[]
        {
            new SqlParameter("@userName",userName),
            new SqlParameter("@startDate",startDate),
            new SqlParameter("@endDate",endDate),
            new SqlParameter("@pageSize",pageSize),
            new SqlParameter("@pageNum",pageIndex)
        };

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_GetNotPassResources", ps);
    }


    [WebMethod]
    public DataSet GetLogs(string userName, string logType,DateTime startDate, DateTime endDate, int pageSize, int pageIndex)
    {
        SqlParameter[] ps = new SqlParameter[]
        {
            new SqlParameter("@userName",userName),
            new SqlParameter("@logType",logType),
            new SqlParameter("@startDate",startDate),
            new SqlParameter("@endDate",endDate),
            new SqlParameter("@pageSize",pageSize),
            new SqlParameter("@pageNum",pageIndex)
        };

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_GetLogs", ps);
    }
    
}

