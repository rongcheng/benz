using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;
using QJVRMS.DataAccess;


/// <summary>
/// BaseInfoService 基础信息服务
/// 用途，来源等信息 
/// </summary>
[WebService(Namespace = "http://qjDataAccess.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class BaseInfoService : System.Web.Services.WebService
{

    public BaseInfoService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public DataTable GetSourceTable()
    {
        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Source_GetSource").Tables[0];
    }

    [WebMethod]
    public bool DeleteSourceBySourceID(int SourceID)
    {

        SqlParameter[] Parameters = new SqlParameter[1];

        Parameters[0] = new SqlParameter("@SourceID", SqlDbType.Int);
        Parameters[0].Value = SourceID;

        int result = SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Source_DeleteSource", Parameters);
        return result == 1;
    }

    [WebMethod]
    public bool UpdateSource(int sourceId, string sourceName, string desc)
    {

        SqlParameter[] Parameters = new SqlParameter[3];

        Parameters[0] = new SqlParameter("@SourceID", SqlDbType.Int);
        Parameters[1] = new SqlParameter("@SourceName", SqlDbType.NVarChar);
        Parameters[2] = new SqlParameter("@SourceDesc", SqlDbType.NVarChar);

        Parameters[0].Value = sourceId;
        Parameters[1].Value = sourceName;
        Parameters[2].Value = desc;

        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Source_UpdateSource", Parameters) > 0;
        }
        catch (Exception ex)
        {
            QJVRMS.Common.LogWriter.WriteExceptionLog(ex);
            return false;
        }
    }

    [WebMethod]
    public bool AddSource(string sourceName, string desc)
    {

        SqlParameter[] Parameters = new SqlParameter[2];


        Parameters[0] = new SqlParameter("@SourceName", SqlDbType.NVarChar);
        Parameters[1] = new SqlParameter("@SourceDesc", SqlDbType.NVarChar);
       

        Parameters[0].Value = sourceName;
        Parameters[1].Value = desc;
      
        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Source_AddSource", Parameters) > 0;
        }
        catch (Exception ex)
        {
          
            QJVRMS.Common.LogWriter.WriteExceptionLog(ex);
            return false;
        }

    }

    [WebMethod]
    public DataTable GetUsageTable()
    {
        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Usage_GetUsage").Tables[0];

    }

    [WebMethod]
    public  bool DeleteUsageByUsageID(int UsageID)
    {

        SqlParameter[] Parameters = new SqlParameter[1];

        Parameters[0] = new SqlParameter("@UsageID", SqlDbType.Int);
        Parameters[0].Value = UsageID;

        int result = SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Usage_DeleteUsage", Parameters);
        return result == 1;
    }

    [WebMethod]
    public bool UpdateUsage(int usageId, string usageName, string desc)
    {

        SqlParameter[] Parameters = new SqlParameter[3];

        Parameters[0] = new SqlParameter("@UsageID", SqlDbType.Int);
        Parameters[1] = new SqlParameter("@UsageName", SqlDbType.NVarChar);
        Parameters[2] = new SqlParameter("@UsageDesc", SqlDbType.NVarChar);

        Parameters[0].Value = usageId;
        Parameters[1].Value = usageName;
        Parameters[2].Value = desc;


        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Usage_UpdateUsage", Parameters) > 0;
        }
        catch (Exception ex)
        {
            QJVRMS.Common.LogWriter.WriteExceptionLog(ex);
            return false;
        }

    }

    [WebMethod]
    public bool AddUsage(string usageName,string desc)
    {

        SqlParameter[] Parameters = new SqlParameter[2];


        Parameters[0] = new SqlParameter("@UsageName", SqlDbType.NVarChar);
        Parameters[1] = new SqlParameter("@UsageDesc", SqlDbType.NVarChar);

        Parameters[0].Value = usageName;
        Parameters[1].Value = desc;

        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Usage_AddUsage", Parameters) > 0;
        }
        catch(Exception ex)
        {
            QJVRMS.Common.LogWriter.WriteExceptionLog(ex);
            return false;
        }
        
    }
}

