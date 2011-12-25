using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using QJVRMS.DataAccess;

/// <summary>
/// DMDataAccess 的摘要说明
/// </summary>
[WebService(Namespace = "http://qjDataAccess.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.None)]
public class DMDataAccess : System.Web.Services.WebService
{
  
    public DMDataAccess()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

   

    #region ExecuteNonQuery

    [WebMethod(BufferResponse = true, Description = "ExecuteNonQuery", CacheDuration = 0, EnableSession = false, MessageName = "ENQ")]
    public   int ExecuteNonQuery(CommandType commandType, string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, commandType, commandText, (SqlParameter[])null);
    }

    [WebMethod(BufferResponse = true, Description = "ExecuteNonQuery", CacheDuration = 0, EnableSession = false, MessageName = "ENQWITHParam")]
    public   int ExecuteNonQuery( CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, commandType, commandText, commandParameters);
    }

    [WebMethod(BufferResponse = true, Description = "ExecuteNonQuery", CacheDuration = 0, EnableSession = false, MessageName = "ENQWITHParamValue")]
    public   int ExecuteNonQuery(string spName, params object[] parameterValues)
    {
        return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, spName, parameterValues);
    }

    #endregion


    #region ExecuteDataset

    [WebMethod(BufferResponse = true, Description = "ExecuteDataset", CacheDuration = 0, EnableSession = false, MessageName = "ED")]
    public   DataSet ExecuteDataset(CommandType commandType, string commandText)
    {
        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, commandType, commandText);
    }

    [WebMethod(BufferResponse = true, Description = "ExecuteDataset", CacheDuration = 0, EnableSession = false, MessageName = "EDWITHParam")]
    public   DataSet ExecuteDataset(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, commandType, commandText, commandParameters);
    }

    [WebMethod(BufferResponse = true, Description = "ExecuteDataset", CacheDuration = 0, EnableSession = false, MessageName = "EDWITHParamValue")]
    public   DataSet ExecuteDataset(string spName, params object[] parameterValues)
    {
        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, spName, parameterValues);
    }

    #endregion


    #region ExecuteScalar

    [WebMethod(BufferResponse = true, Description = "ExecuteScalar", CacheDuration = 0, EnableSession = false, MessageName = "ES")]
    public   object ExecuteScalar( CommandType commandType, string commandText)
    {
        return SqlHelper.ExecuteScalar(CommonInfo.ConQJVRMS, commandType, commandText);
    }

    [WebMethod(BufferResponse = true, Description = "ExecuteScalar", CacheDuration = 0, EnableSession = false, MessageName = "ESWITHParam")]
    public   object ExecuteScalar( CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        return SqlHelper.ExecuteScalar(CommonInfo.ConQJVRMS, commandType, commandText, commandParameters);
    }

    [WebMethod(BufferResponse = true, Description = "ExecuteScalar", CacheDuration = 0, EnableSession = false, MessageName = "ESWITHParamValue")]
    public   object ExecuteScalar( string spName, params object[] parameterValues)
    {
        return SqlHelper.ExecuteScalar(CommonInfo.ConQJVRMS, spName, parameterValues);
    }

    #endregion
}

